Imports MySql.Data.MySqlClient
Public Class mermas
    Inherits System.Web.UI.Page
    Dim tabla As New DataTable
    Dim fila As DataRow

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim cmdTabla As New cVentas
            lblResultado.Visible = False
            Session("Tabla") = cmdTabla.cargarTablaVentas
            txtfecha.Text = Date.Now.ToString("yyyy-MM-dd")
            cargarSubTipo()
        End If
    End Sub


    'Dim totalG As Double = 0
    'Dim total5 As Double = 0
    'Dim totalE As Double = 0
    'Dim total10 As Double = 0
    'Protected Sub gvDatos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDatos.RowDataBound

    '    Try
    '        If e.Row.RowType = DataControlRowType.DataRow Then

    '            total5 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "5"))
    '            total10 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "10"))
    '            totalE += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Excenta"))
    '            totalG = totalE + total5 + total10

    '        ElseIf (e.Row.RowType = DataControlRowType.Footer) Then

    '            e.Row.Cells(3).Text = "Totales:"
    '            e.Row.Cells(4).Text = totalE.ToString("N0")

    '            e.Row.Cells(5).Text = total5.ToString("N0")

    '            e.Row.Cells(6).Text = total10.ToString("N0")
    '            e.Row.Cells(7).Text = totalG.ToString("N0")
    '            e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Center
    '            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Center
    '            e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Center
    '            e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Center
    '            e.Row.Font.Bold = True

    '        End If

    '    Catch ex As Exception
    '        ex.Message.ToString()
    '    End Try

    'End Sub


    Protected Sub gvDatos_RowDataBound_delete(sender As Object, e As GridViewDeleteEventArgs) Handles gvDatos.RowDeleting

        Dim x As Integer = e.RowIndex
        tabla = Session("Tabla")
        tabla.Rows(x).Delete()
        Session("Tabla") = tabla
        gvDatos.DataSource = tabla
        gvDatos.DataBind()
    End Sub
    Sub btnGuardar_Click()
        Dim row As GridViewRow
        Dim i As Integer
        'Creacion de variables
        Dim mensaje2 As String = ""
        tabla = Session("Tabla")
        Dim mensaje1 As String = ""
        Dim FechaVencimiento As String = ""
        Dim cventa As New cVentas
        Dim codprod As Integer
        Dim txt As TextBox
        ' manejando los errores con el try
        Try
            'haciendo bucle al gridview

            codprod = cventa.InsertarCabMerma(txtfecha.Text, txtmotivo.Text)
            If IsNumeric(codprod) Then
                'codprod = codprod + 1
                For i = 0 To gvDatos.Rows.Count - 1
                    row = gvDatos.Rows(i)
                    fila = tabla.Rows(i)
                    txt = gvDatos.Rows(i).FindControl("txtCantidad")
                    mensaje1 = cventa.InsertarDetMerma(codprod, fila("Codigo"), CDec(txt.Text ))

                Next

                'falta el tema de stock
                lblResultado.Text = "Movimiento Guardado"
                lblResultado.Visible = True
                txtfecha.Text = ""
                gvDatos.DataSource = Nothing
                gvDatos.DataBind()
                tabla = Nothing
                Session("Tabla") = Nothing
                Session("Tabla") = cventa.cargarTablaVentas
                SetFocus(txtmotivo)
                txtfecha.Text = Date.Now.ToString("yyyy-MM-dd")
            Else
                lblResultado.Text = mensaje1
                lblResultado.Visible = True
            End If
        Catch ex As Exception
            lblResultado.Text = ex.Message
            lblResultado.Visible = True
        End Try

    End Sub

    Sub cargarSubTipo()
        Dim cargar As New cTProductos
        Dim grilla As New DataSet
        grilla = cargar.cgSTProd()
        rblSt.DataSource = grilla
        rblSt.DataValueField = "stpCod"
        rblSt.DataTextField = "stpDesc"
        rblSt.DataBind()

    End Sub
    Sub cargarVerduras()
        Dim cargar As New cTProductos
        Dim grilla As New DataSet
        grilla = cargar.traerSTProd(rblSt.SelectedValue)
        rblVerduras.DataSource = grilla
        rblVerduras.DataValueField = "prodCod"
        rblVerduras.DataTextField = "prodNombre"
        rblVerduras.DataBind()

    End Sub

    Protected Sub rblSt_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblSt.SelectedIndexChanged
        cargarVerduras()
    End Sub

    Protected Sub rblVerduras_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblVerduras.SelectedIndexChanged
        Dim prodCod As Integer
        prodCod = rblVerduras.SelectedValue
        Dim nrofila As Integer
        Dim txtCant As TextBox
        tabla = Session("Tabla")
        fila = tabla.NewRow
        If gvDatos.Rows.Count = 0 Then
            fila("Linea") = 0
            fila("Codigo") = prodCod
            fila("Producto") = rblVerduras.SelectedItem
            fila("Cantidad") = 1

        Else
            For i = 0 To gvDatos.Rows.Count - 1

                txtCant = gvDatos.Rows(i).FindControl("txtCantidad")

                tabla.Rows(i)("Cantidad") = txtCant.Text
            Next

            nrofila = tabla.Rows.Count
            fila("Codigo") = prodCod
            fila("Producto") = rblVerduras.SelectedItem
            fila("Cantidad") = 1
        End If
        fila("Linea") = nrofila + 1
        tabla.Rows.Add(fila)
        gvDatos.DataSource = tabla
        gvDatos.DataBind()
        Session("Tabla") = tabla



    End Sub
End Class