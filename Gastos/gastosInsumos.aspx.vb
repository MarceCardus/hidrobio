Public Class gastosInsumos
    Inherits System.Web.UI.Page
    Private tabla As New DataTable
    Private fila As DataRow
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim cmdTabla As New cGastos
        If Not IsPostBack Then
            cargarRubros()
            txtFecha.Text = Date.Now.ToString("yyyy-MM-dd")
            Session("TablaItem") = cmdTabla.cargarTablaGastos
            lblCodProv.Visible = False
            lblProv.Visible = False

        End If
    End Sub
    Sub cargarRubros()
        Dim cargar As New cGastos
        Dim grilla As New DataSet
        Dim filtro As String = " WHERE rubroNombre = 'Insumos'"
        grilla = cargar.cgRubro(filtro)
        rblRubros.DataSource = grilla
        rblRubros.DataValueField = "rubroCod"
        rblRubros.DataTextField = "rubroNombre"
        rblRubros.DataBind()
    End Sub
    Sub cargarItem()
        Dim cargar As New cGastos
        Dim ds As New DataSet
        ds = cargar.traerItem(rblRubros.SelectedValue)
        rblItem.DataSource = ds
        rblItem.DataValueField = "itemCod"
        rblItem.DataTextField = "itemNombre"
        rblItem.DataBind()

    End Sub
    Protected Sub rblRubros_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblRubros.SelectedIndexChanged
        cargarItem()
    End Sub

    Protected Sub rblItem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblItem.SelectedIndexChanged
        Dim traerprecio As New cGastos
        Dim precio As Double
        Dim impuesto As Integer
        Dim itemCod As Integer
        itemCod = rblItem.SelectedValue
        Dim datos = traerprecio.traerPrecioItem(itemCod)
        precio = datos.Item1
        impuesto = datos.Item2
        Dim nrofila As Integer
        '  Dim dr As DataRow
        Dim txtCant As TextBox
        Dim txtUnit As TextBox
        Dim ddl As DropDownList
        tabla = Session("TablaItem")
        fila = tabla.NewRow
        If gvDatos.Rows.Count = 0 Then

            fila("Linea") = 0

            'fila("Linea") = nrofila + 1
            fila("Codigo") = itemCod
            fila("Item") = rblItem.SelectedItem
            fila("Cantidad") = 1
            fila("Unitario") = precio

            If impuesto = 1 Then
                fila("Excenta") = Convert.ToDouble(precio).ToString("N0")
                fila("5") = 0
                fila("10") = 0
                fila("Impuesto") = 0
            ElseIf impuesto = 2 Then
                fila("Excenta") = 0
                fila("5") = Convert.ToDouble(precio).ToString("N0")
                fila("10") = 0
                fila("Impuesto") = 5
            Else
                fila("Excenta") = 0
                fila("5") = 0
                fila("10") = Convert.ToDouble(precio).ToString("N0")
                fila("Impuesto") = 10
            End If


        Else
            For i = 0 To gvDatos.Rows.Count - 1
                ddl = gvDatos.Rows(i).FindControl("ddlImpuesto")
                txtCant = gvDatos.Rows(i).FindControl("txtgvCant")
                txtUnit = gvDatos.Rows(i).FindControl("txtgvUnit")
                tabla.Rows(i)("Cantidad") = txtCant.Text
                tabla.Rows(i)("Unitario") = txtUnit.Text

                If ddl.SelectedValue = 5 Then
                    tabla.Rows(i)("5") = Convert.ToDouble(CDec(txtCant.Text) * CDbl(txtUnit.Text)).ToString("N0")
                    tabla.Rows(i)("10") = 0
                    tabla.Rows(i)("Excenta") = 0

                    tabla.Rows(i)("Impuesto") = 5

                ElseIf ddl.SelectedValue = 10 Then
                    tabla.Rows(i)("10") = Convert.ToDouble(CDec(txtCant.Text) * CDbl(txtUnit.Text)).ToString("N0")
                    tabla.Rows(i)("5") = 0
                    tabla.Rows(i)("Excenta") = 0
                    tabla.Rows(i)("Impuesto") = 10


                Else
                    tabla.Rows(i)("Excenta") = Convert.ToDouble(CDec(txtCant.Text) * CDbl(txtUnit.Text)).ToString("N0")
                    tabla.Rows(i)("5") = 0
                    tabla.Rows(i)("10") = 0
                    tabla.Rows(i)("Impuesto") = 0


                End If

            Next

            nrofila = tabla.Rows.Count
            fila("Codigo") = itemCod
            fila("Item") = rblItem.SelectedItem
            fila("Cantidad") = 1
            fila("Unitario") = precio
            If impuesto = 1 Then
                fila("Excenta") = Convert.ToDouble(precio).ToString("N0")
                fila("5") = 0
                fila("10") = 0
                fila("Impuesto") = 0
            ElseIf impuesto = 2 Then
                fila("Excenta") = 0
                fila("5") = Convert.ToDouble(precio).ToString("N0")
                fila("10") = 0
                fila("Impuesto") = 5
            Else
                fila("Excenta") = 0
                fila("5") = 0
                fila("10") = Convert.ToDouble(precio).ToString("N0")
                fila("Impuesto") = 10
            End If

        End If
        fila("Linea") = nrofila + 1




        tabla.Rows.Add(fila)
        gvDatos.DataSource = tabla
        gvDatos.DataBind()
        Session("TablaItem") = tabla
    End Sub
    Sub actualizarGrillaDatos()
        Dim txtCant As TextBox
        Dim txtUnit As TextBox
        Dim ddl As DropDownList
        tabla = Session("TablaItem")
        For i = 0 To gvDatos.Rows.Count - 1

            ddl = gvDatos.Rows(i).FindControl("ddlImpuesto")
            txtCant = gvDatos.Rows(i).FindControl("txtgvCant")
            txtUnit = gvDatos.Rows(i).FindControl("txtgvUnit")
            tabla.Rows(i)("Cantidad") = txtCant.Text
            tabla.Rows(i)("Unitario") = txtUnit.Text

            If ddl.SelectedValue = 5 Then
                tabla.Rows(i)("5") = Convert.ToDouble(CDec(txtCant.Text) * CDbl(txtUnit.Text)).ToString("N0")
                tabla.Rows(i)("10") = 0
                tabla.Rows(i)("Excenta") = 0

                tabla.Rows(i)("Impuesto") = 5

            ElseIf ddl.SelectedValue = 10 Then
                tabla.Rows(i)("10") = Convert.ToDouble(CDec(txtCant.Text) * CDbl(txtUnit.Text)).ToString("N0")
                tabla.Rows(i)("5") = 0
                tabla.Rows(i)("Excenta") = 0
                tabla.Rows(i)("Impuesto") = 10


            Else
                tabla.Rows(i)("Excenta") = Convert.ToDouble(CDec(txtCant.Text) * CDbl(txtUnit.Text)).ToString("N0")
                tabla.Rows(i)("5") = 0
                tabla.Rows(i)("10") = 0
                tabla.Rows(i)("Impuesto") = 0


            End If

        Next
        gvDatos.DataSource = tabla
        gvDatos.DataBind()
        Session("Tabla") = tabla
    End Sub
    Protected Sub ddlImpuesto_SelectedIndexChanged(sender As Object, e As EventArgs)
        actualizarGrillaDatos()
    End Sub

    Protected Sub txtgvCant_TextChanged(sender As Object, e As EventArgs)
        actualizarGrillaDatos()

    End Sub

    Protected Sub txtgvUnit_TextChanged(sender As Object, e As EventArgs)
        actualizarGrillaDatos()
    End Sub
    Protected Sub gvDatos_RowDataBound_delete(sender As Object, e As GridViewDeleteEventArgs) Handles gvDatos.RowDeleting

        Dim x As Integer = e.RowIndex
        tabla = Session("TablaItem")
        tabla.Rows(x).Delete()
        Session("TablaItem") = tabla
        gvDatos.DataSource = tabla
        gvDatos.DataBind()
    End Sub
    Dim totalG As Double = 0
    Dim total5 As Double = 0
    Dim totalE As Double = 0
    Dim total10 As Double = 0
    Protected Sub gvDatos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDatos.RowDataBound

        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                total5 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "5"))
                total10 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "10"))
                totalE += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Excenta"))
                totalG = totalE + total5 + total10






            ElseIf (e.Row.RowType = DataControlRowType.Footer) Then

                e.Row.Cells(3).Text = "Totales:"
                e.Row.Cells(6).Text = totalE.ToString("N0")

                e.Row.Cells(7).Text = total5.ToString("N0")

                e.Row.Cells(8).Text = total10.ToString("N0")
                e.Row.Cells(9).Text = totalG.ToString("N0")
                e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Center
                e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Center
                e.Row.Cells(9).HorizontalAlign = HorizontalAlign.Center
                e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Center
                e.Row.Font.Bold = True

            End If

        Catch ex As Exception
            ex.Message.ToString()
        End Try

    End Sub
    Sub btnBuscaProv_Click()
        Dim ds As New cProveedor
        Dim er As String
        Try
            gvProveedor.DataSourceID = Nothing
            gvProveedor.DataSource = ds.dsBuscarProveedor(txtProveedor.Text)
            gvProveedor.DataBind()

        Catch ex As Exception
            er = ex.Message

        End Try
    End Sub
    Sub gvProveedor_seleccionar()
        Dim cargar As New cClientes
        Dim grilla As New DataSet
        Dim fila As GridViewRow
        Dim info As String
        Dim nombre As String
        fila = gvProveedor.SelectedRow
        info = fila.Cells(0).Text
        ID = CInt(info)
        nombre = fila.Cells(2).Text
        lblCodProv.Text = ID
        lblProv.Text = nombre
        gvProveedor.DataSource = Nothing
        gvProveedor.DataBind()
        lblCodProv.Visible = True
        lblProv.Visible = True
    End Sub
    Sub btnGuardar_Click()
        Dim row As GridViewRow
        Dim i As Integer
        'Creacion de variables
        Dim mensaje2 As String = ""
        tabla = Session("TablaItem")
        Dim mensaje1 As String = ""
        Dim FechaVencimiento As String = ""
        Dim cgastos As New cGastos
        Dim i5 As Double = 0
        Dim i10 As Double = 0
        Dim exc As Double = 0
        Dim TF As Double = 0
        For i = 0 To gvDatos.Rows.Count - 1
            If tabla.Rows(i)("5").ToString <> 0 Then
                i5 = i5 + CDbl(tabla.Rows(i)("5").ToString)
            End If
            If tabla.Rows(i)("10").ToString <> 0 Then
                i10 = i10 + CDbl(tabla.Rows(i)("10").ToString)
            End If
            If tabla.Rows(i)("Excenta").ToString <> 0 Then
                exc = exc + CDbl(tabla.Rows(i)("Excenta").ToString)
            End If
        Next
        TF = i5 + i10 + exc
        Dim tiva5 As Double
        tiva5 = CInt(i5 / 21)
        Dim tiva10 As Double
        tiva10 = CInt(i10 / 11)
        Dim tivas As Double = tiva5 + tiva10
        Dim coditem As Integer
        Dim txtcant As TextBox
        Dim impuesto As DropDownList
        ' manejando los errores con el try
        Try
            'haciendo bucle al gridview

            coditem = cgastos.InsertarGastosCab(txtNroFact.Text, txtFecha.Text, rblTipo.SelectedValue, TF, exc, i5, i10, tiva5, tiva10, lblCodProv.Text, "")
            If IsNumeric(coditem) Then

                For i = 0 To gvDatos.Rows.Count - 1
                    row = gvDatos.Rows(i)
                    fila = tabla.Rows(i)
                    txtcant = gvDatos.Rows(i).FindControl("txtgvCant")
                    impuesto = gvDatos.Rows(i).FindControl("ddlImpuesto")
                    If impuesto.SelectedValue = 5 Then
                        fila("5") = tabla.Rows(i)("5")
                        fila("10") = 0
                        fila("Excenta") = 0
                        fila("Impuesto") = 2
                    ElseIf impuesto.SelectedValue = 10 Then
                        fila("10") = tabla.Rows(i)("10")
                        fila("Excenta") = 0
                        fila("5") = 0
                        fila("Impuesto") = 3
                    Else
                        fila("Excenta") = tabla.Rows(i)("Excenta")
                        fila("10") = 0
                        fila("5") = 0
                        fila("Impuesto") = 1
                    End If
                    mensaje1 = cgastos.InsertarGastosDetInsumos(coditem, fila("Impuesto"), CDbl(fila("10")), CDbl(fila("5")),
                                                                CDbl(fila("Excenta")), rblRubros.SelectedValue, CDec(txtcant.Text), fila("Codigo"))

                Next

                'falta el tema de stock
                lblResultado.Text = "Movimiento Guardado"
                lblResultado.Visible = True
                lblCodProv.Text = ""
                lblProv.Text = ""
                txtFecha.Text = ""
                txtNroFact.Text = ""

                gvDatos.DataSource = Nothing
                gvDatos.DataBind()
                tabla = Nothing
                Session("Tabla") = Nothing
                Session("Tabla") = cgastos.cargarTablaGastos
                SetFocus(txtNroFact)
                txtFecha.Text = Date.Now.ToString("yyyy-MM-dd")
                rblItem.ClearSelection()
                cargarRubros()
            Else
                lblResultado.Text = mensaje1
                lblResultado.Visible = True
            End If
        Catch ex As Exception
            lblResultado.Text = ex.Message
            lblResultado.Visible = True
        End Try

    End Sub
End Class