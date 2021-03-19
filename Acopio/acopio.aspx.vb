Imports MySql.Data.MySqlClient
Public Class acopio
    Inherits System.Web.UI.Page
    Dim tabla As New DataTable
    Dim fila As DataRow

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim cmdTabla As New cCompras
            lblResultado.Visible = False

            ' CargarDDLProductor()
            CargarDDLChofer()
            CargarDDLMovil()

            Session("Tabla") = cmdTabla.cargarTablaCompras
            txtfecha.Text = Date.Now.ToString("yyyy-MM-dd")
            SetFocus(txtFactura)
            cargarSubTipo()
        End If
    End Sub
    'Sub CargarDDLProductor()
    '    Dim cargar As New cProductos
    '    Dim grilla As New DataSet
    '    grilla = cargar.cDataset("vproveedores where provAcopio='SI'", "Productor")
    '    ddlProductor.DataSource = grilla
    '    ddlProductor.DataTextField = "provNombre"
    '    ddlProductor.DataValueField = "provCod"
    '    ddlProductor.DataBind()


    'End Sub
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
    Sub CargarDDLChofer()
        Dim cargar As New cProductos
        Dim grilla As New DataSet
        grilla = cargar.cDataset("vpersonales where perChofer='Si' and perEstado='Si'", "Chofer")
        ddlChofer.DataSource = grilla
        ddlChofer.DataTextField = "perNombre"
        ddlChofer.DataValueField = "perCod"
        ddlChofer.DataBind()

    End Sub
    Sub CargarDDLMovil()
        Dim cargar As New cProductos
        Dim grilla As New DataSet
        grilla = cargar.cDataset("vTraerMovil", "Movil")
        ddlMovil.DataSource = grilla
        ddlMovil.DataTextField = "movil"
        ddlMovil.DataValueField = "movCod"
        ddlMovil.DataBind()

    End Sub
    'Protected Sub btnAgregar_Click1(sender As Object, e As EventArgs) Handles btnAgregar.Click
    '    Dim impuesto As Integer
    '    Dim acumulado As Integer
    '    Dim ivaAcu As Integer
    '    Dim iva As Int32
    '    If txtCantidad.Text = "" Or txtUnitario.Text = "" Then
    '        lblCantidad.Text = "Debe Cargar una cantidad y precio"
    '        lblCantidad.Visible = True
    '    Else

    '        tabla = Session("Tabla")
    '        fila = tabla.NewRow
    '        fila("Codigo") = lblCod.Text
    '        fila("Producto") = HttpUtility.HtmlDecode(lblNombre.Text)
    '        fila("Cantidad") = txtCantidad.Text
    '        fila("Unitario") = txtUnitario.Text
    '        If rblImpuesto.SelectedValue = 21 Then
    '            impuesto = CDec(txtCantidad.Text) * CDbl(txtUnitario.Text)
    '            iva = (impuesto / 21) + ivaAcu
    '            fila("Excenta") = 0
    '            fila("5") = impuesto
    '            fila("10") = 0
    '            fila("Total5") = acumulado + impuesto
    '        Else

    '            If rblImpuesto.SelectedValue = 11 Then
    '                impuesto = CDbl(txtCantidad.Text) * CDbl(txtUnitario.Text)
    '                iva = impuesto / 11 + ivaAcu
    '                fila("Excenta") = 0
    '                fila("5") = 0
    '                fila("10") = impuesto
    '                fila("Total10") = acumulado + impuesto
    '            Else
    '                impuesto = CDbl(txtCantidad.Text) * CDbl(txtUnitario.Text)
    '                iva = 0
    '                fila("Excenta") = impuesto
    '                fila("5") = 0
    '                fila("10") = 0
    '                fila("TotalExcenta") = acumulado + impuesto
    '            End If


    '        End If

    '        fila("TotalFact") = acumulado
    '        fila("TotalIva") = ivaAcu
    '        tabla.Rows.Add(fila)
    '        gvDatos.DataSource = tabla
    '        gvDatos.DataBind()
    '        Session("Tabla") = tabla

    '    End If

    'End Sub

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

                e.Row.Cells(5).Text = "Totales:"
                e.Row.Cells(6).Text = totalE.ToString("N0")

                e.Row.Cells(7).Text = total5.ToString("N0")

                e.Row.Cells(8).Text = total10.ToString("N0")
                e.Row.Cells(9).Text = totalG.ToString("N0")
                e.Row.Cells(9).HorizontalAlign = HorizontalAlign.Center
                e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Center
                e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Center
                e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Center
                e.Row.Font.Bold = True

            End If

        Catch ex As Exception
            ex.Message.ToString()
        End Try

    End Sub
    'Protected Sub gvDatos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDatos.RowDataBound
    '    Select Case e.Row.RowType
    '        Case DataControlRowType.DataRow
    '            Dim ctrlEliminar As ImageButton = CType(e.Row.FindControl("Eliminar"), ImageButton)
    '            ctrlEliminar.OnClientClick = "return confirm('¿Esta seguro de eliminar este registro?');"
    '    End Select
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
        Dim codprod As Integer
        tabla = Session("Tabla")
        Dim mensaje1 As String = ""
        Dim FechaVencimiento As String = ""
        Dim cAcopio As New cCompras
        Dim nro As Integer = 0
        Dim i5 As Double = 0
        Dim i10 As Double = 0
        Dim exc As Double = 0
        Dim TF As Double = 0
        For i = 0 To gvDatos.Rows.Count - 1
            If tabla.Rows(i)("5").ToString <> "" Then
                i5 = i5 + CDbl(tabla.Rows(i)("5").ToString)
            End If
            If tabla.Rows(i)("10").ToString <> "" Then
                i10 = i10 + CDbl(tabla.Rows(i)("10").ToString)
            End If
            If tabla.Rows(i)("Excenta").ToString <> "" Then
                exc = exc + CDbl(tabla.Rows(i)("Excenta").ToString)
            End If


        Next
        TF = i5 + i10 + exc
        Dim tiva5 As Double
        tiva5 = i5 / 21
        Dim tiva10 As Double
        tiva10 = i10 / 11
        Dim tivas As Double = tiva5 + tiva10
        Dim txtcant As TextBox
        Dim impuesto As DropDownList
        'manejando los errores con el try
        Try
            'haciendo bucle al gridview

            codprod = cAcopio.InsertarCab(lblCodProv.Text, txtfecha.Text, txtFactura.Text, ddlChofer.SelectedValue,
                            ddlMovil.SelectedValue, exc, i5, i10, TF, tiva5, tiva10, tivas)
            If IsNumeric(codprod) Then
                'codprod = codprod + 1
                For i = 0 To gvDatos.Rows.Count - 1
                    row = gvDatos.Rows(i)
                    fila = tabla.Rows(i)
                    txtcant = gvDatos.Rows(i).FindControl("txtgvCant")
                    impuesto = gvDatos.Rows(i).FindControl("ddlImpuesto")

                    If impuesto.SelectedValue = 5 Then
                        fila("5") = tabla.Rows(i)("5")
                        fila("10") = 0
                        fila("Excenta") = 0

                    ElseIf impuesto.SelectedValue = 10 Then
                        fila("10") = tabla.Rows(i)("10")
                        fila("Excenta") = 0
                        fila("5") = 0
                    Else
                        fila("Excenta") = tabla.Rows(i)("Excenta")
                        fila("10") = 0
                        fila("5") = 0
                    End If
                    mensaje1 = cAcopio.InsertarDet(codprod, fila("Codigo"), CDec(txtcant.Text), CDbl(fila("Excenta")),
                                CDbl(fila("5")), CDbl(fila("10")))

                Next

                'falta el tema de stock
                lblResultado.Text = "Movimiento Guardado"
                lblResultado.Visible = True
                lblCodProv.Text = ""
                lblProv.Text = ""
                txtfecha.Text = ""
                txtFactura.Text = ""
                gvDatos.DataSource = Nothing
                gvDatos.DataBind()
                tabla = Nothing
                Session("Tabla") = Nothing
                Session("Tabla") = cAcopio.cargarTablaCompras
                txtfecha.Text = Date.Now.ToString("yyyy-MM-dd")
                SetFocus(txtFactura)
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

    Protected Sub rblVerduras_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblVerduras.SelectedIndexChanged
        Dim traerprecio As New cCompras
        Dim precio As Double
        Dim prodCod As Integer
        prodCod = rblVerduras.SelectedValue
        precio = traerprecio.traerPrecio(prodCod)
        Dim nrofila As Integer
        '  Dim dr As DataRow
        Dim txtCant As TextBox
        Dim txtUnit As TextBox
        Dim ddl As DropDownList
        '   Dim texto As String
        tabla = Session("Tabla")
        fila = tabla.NewRow
        If gvDatos.Rows.Count = 0 Then

            fila("Linea") = 0

            'fila("Linea") = nrofila + 1
            fila("Codigo") = prodCod
            fila("Producto") = rblVerduras.SelectedItem
            fila("Cantidad") = 1
            fila("Unitario") = precio
            fila("Excenta") = 0
            fila("5") = Convert.ToDouble(precio).ToString("N0")
            fila("10") = 0
            fila("Impuesto") = 5


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
            fila("Codigo") = prodCod
            fila("Producto") = rblVerduras.SelectedItem
            fila("Cantidad") = 1
            fila("Unitario") = precio
            fila("Excenta") = 0
            fila("5") = Convert.ToDouble(precio).ToString("N0")
            fila("10") = 0
            fila("Impuesto") = 5

        End If
        fila("Linea") = nrofila + 1




        tabla.Rows.Add(fila)
        gvDatos.DataSource = tabla
        gvDatos.DataBind()
        Session("Tabla") = tabla
        rblVerduras.ClearSelection()
    End Sub

    Protected Sub rblSt_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblSt.SelectedIndexChanged
        cargarVerduras()
    End Sub
    Sub actualizarGrillaDatos()
        Dim txtCant As TextBox
        Dim txtUnit As TextBox
        Dim ddl As DropDownList
        tabla = Session("Tabla")
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
End Class