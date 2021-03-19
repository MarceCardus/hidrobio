Imports Microsoft.Reporting.WebForms
Imports System.Dynamic
Imports System.IO

Public Class op
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim cmdTabla As New cPagos
            lblResultado.Visible = False

            txtFecha.Text = Date.Now.ToString("yyyy-MM-dd")
            Session("TablaOP") = cmdTabla.cargarTablaPagos
        End If
    End Sub

    Protected Sub rblTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblTipo.SelectedIndexChanged
        traerDatos(rblTipo.SelectedValue)
    End Sub
    Sub traerDatos(ByVal tipo As String)
        If tipo = "Acopio" Then
            Dim cargar As New cCompras
            Dim ds As New DataSet
            ds = cargar.dsGrillaPagos()
            gvDetalle.DataSource = Nothing
            gvDetalle.DataSource = ds
            gvDetalle.DataBind()
        Else
            Dim cargar As New cGastos
            Dim ds2 As New DataSet
            ds2 = cargar.dsGastosPago()
            gvDetalle.DataSource = Nothing
            gvDetalle.DataSource = ds2
            gvDetalle.DataBind()
        End If
    End Sub


    Sub traerDetalle()
        Dim cargar As New cCompras
        Dim grilla As New DataSet
        Dim fila As GridViewRow
        Dim info As String
        fila = gvDetalle.SelectedRow
        info = fila.Cells(0).Text
        ID = CInt(info)
        grilla = cargar.cGrillaDet(ID)
        gvDet.DataSource = grilla
        gvDet.DataBind()
    End Sub

    Sub agregar(ByVal cod As Integer, ByVal nroFactura As String, ByVal proveedor As String, ByVal monto As Double, ByVal tipo As String)
        Dim tabla As New DataTable
        Dim fila As DataRow


        tabla = Session("TablaOP")
        fila = tabla.NewRow


        fila("Codigo") = cod
        fila("tipo") = tipo
        fila("NroFactura") = nroFactura

        fila("Proveedor") = proveedor

        fila("Monto") = CDbl(monto).ToString("N0")


        tabla.Rows.Add(fila)
        gvDatos.DataSource = tabla
        gvDatos.DataBind()
        Session("TablaOP") = tabla

    End Sub


    Protected Sub gvDetalle_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvDetalle.RowCommand

        If e.CommandName = "verDetalle" Then
            Dim row As GridViewRow = CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)
            Dim id As String = CType(row.Cells(0), DataControlFieldCell).Text
            If rblTipo.SelectedValue = "Acopio" Then
                Dim cargar As New cCompras
                Dim ds As New DataSet
                ds = cargar.trarCompras_OP(CInt(id))
                gvDet.DataSource = ds
                gvDet.DataBind()
            ElseIf rblTipo.SelectedValue = "Gastos" Then
                Dim cargarGastos As New cGastos
                Dim dsgastos As New DataSet
                Try

                    dsgastos = cargarGastos.cGrillaDetGastos_OP(CInt(id))
                    gvDet.DataSource = dsgastos
                    gvDet.DataBind()
                Catch ex As Exception

                End Try
            End If

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModal", "$('#ModalCenter').modal();", True)
        ElseIf e.CommandName = "AgregarDetalle" Then
            Dim row As GridViewRow = CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)
            Dim Codigo As String = CInt(CType(row.Cells(0), DataControlFieldCell).Text)
            Dim NroFactura As String = CType(row.Cells(2), DataControlFieldCell).Text
            Dim Proveedor As String = CType(row.Cells(3), DataControlFieldCell).Text
            Dim Monto As String = CDbl(CType(row.Cells(4), DataControlFieldCell).Text)
            agregar(Codigo, NroFactura, Proveedor, Monto, rblTipo.SelectedValue)

        End If

    End Sub
    Dim totalG As Double = 0

    Protected Sub gvDatos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDatos.RowDataBound

        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                totalG += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Monto"))

            ElseIf (e.Row.RowType = DataControlRowType.Footer) Then

                e.Row.Cells(2).Text = "Total OP:"

                e.Row.Cells(3).Text = totalG.ToString("N0")
                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Center

                e.Row.Font.Bold = True

            End If

        Catch ex As Exception
            ex.Message.ToString()
        End Try

    End Sub
    Protected Sub gvDatos_RowDataBound_delete(sender As Object, e As GridViewDeleteEventArgs) Handles gvDatos.RowDeleting
        Dim tabla As New DataTable
        Dim x As Integer = e.RowIndex
        tabla = Session("TablaOP")
        tabla.Rows(x).Delete()
        Session("TablaOP") = tabla
        gvDatos.DataSource = tabla
        gvDatos.DataBind()
    End Sub
    Sub guardar()
        Dim tabla As New DataTable
        Dim row As GridViewRow
        Dim fila As DataRow
        Dim codOP As Integer
        Dim mensaje As String
        tabla = Session("TablaOP")
        Dim insertar As New cPagos
        Dim total As Double = 0
        For i = 0 To gvDatos.Rows.Count - 1
            total = total + CDbl(tabla.Rows(i)("Monto").ToString)
        Next
        Dim tipo As String

        Dim codigo As Integer
        Try
            'haciendo bucle al gridview

            codOP = insertar.InsertarOPCab(txtFecha.Text, ddlAutorizador.SelectedValue, ddlelaborado.SelectedValue, total)
            If IsNumeric(codOP) Then
                'codprod = codprod + 1
                For i = 0 To gvDatos.Rows.Count - 1
                    row = gvDatos.Rows(i)
                    fila = tabla.Rows(i)
                    tipo = fila("tipo")
                    codigo = fila("Codigo")
                    If tipo = "Acopio" Then
                        mensaje = insertar.InsertarDetOPCompras(codOP, tipo, codigo)
                    ElseIf tipo = "Gastos" Then

                        mensaje = insertar.InsertarDetOPGastos(codOP, tipo, codigo)
                    End If
                Next

                lblResultado.Text = "Movimiento Guardado"
                lblResultado.Visible = True

                txtFecha.Text = ""

                gvDatos.DataSource = Nothing
                gvDatos.DataBind()

                gvDetalle.DataSource = Nothing
                gvDetalle.DataBind()
                gvDet.DataSource = Nothing
                gvDet.DataBind()
                tabla = Nothing
                Session("TablaOP") = Nothing
                Session("TablaOP") = insertar.cargarTablaPagos
                txtFecha.Text = Date.Now.ToString("yyyy-MM-dd")
                SetFocus(ddlAutorizador)


                Dim cargar As New cPagos
                Dim ds As New DataSet
                ds = cargar.cReporteOP(codOP)
                Dim rds As Microsoft.Reporting.WebForms.ReportDataSource
                Dim dt As New DataTable
                dt = ds.Tables(0)
                dt.Rows(0)("Usuario") = HttpContext.Current.Session("Usuario")


                rds = New Microsoft.Reporting.WebForms.ReportDataSource("dsOp", dt)
                rvOp.LocalReport.DataSources.Clear()
                rvOp.LocalReport.DataSources.Add(rds)
                rvOp.LocalReport.ReportPath = "Pagos/Op.rdlc"
                rvOp.LocalReport.DisplayName = "informeOP" + codOP.ToString("0:N0")
                rvOp.LocalReport.Refresh()
                rvOp.Visible = True

            Else
                lblResultado.Text = "No se guardaron los datos"
                lblResultado.Visible = True
            End If
        Catch ex As Exception
            lblResultado.Text = ex.Message
            lblResultado.Visible = True
        End Try


    End Sub
End Class