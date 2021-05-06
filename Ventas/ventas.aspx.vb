Imports System.ComponentModel
Imports MySql.Data.MySqlClient
Public Class ventas
    Inherits System.Web.UI.Page
    Dim tabla As New DataTable
    Dim fila As DataRow

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        MaintainScrollPositionOnPostBack = True

        If Not IsPostBack Then
            Dim cmdTabla As New cVentas
            Dim fact As Integer
            fact = TraerNroFactura() + 1
            lblResultado.Visible = False
            txtFactura.Text = "001-009-" & fact
            Session("Tabla") = cmdTabla.cargarTablaVentas
            txtfecha.Text = Date.Now.ToString("yyyy-MM-dd")
            cargarSubTipo()
            gvDatos.Enabled = True
            txtfecha.Enabled = True
            rbEntrega.Enabled = True
            rblForma.Enabled = True
            txtcliente.Enabled = True
            rbTipo.Enabled = True
            txtFactura.Enabled = True
            lblCredito.Enabled = False
            txtDias.Enabled = False

        End If
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
                e.Row.Cells(9).HorizontalAlign = HorizontalAlign.Center
                e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Center
                e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Center
                e.Row.Font.Bold = True

            End If

        Catch ex As Exception
            ex.Message.ToString()
        End Try

    End Sub


    Protected Sub gvDatos_RowDataBound_delete(sender As Object, e As GridViewDeleteEventArgs) Handles gvDatos.RowDeleting

        Dim x As Integer = e.RowIndex
        tabla = Session("Tabla")
        tabla.Rows(x).Delete()
        Session("Tabla") = tabla
        gvDatos.DataSource = tabla
        gvDatos.DataBind()
    End Sub
    Sub btnGuardarImprimir_Click()
        Dim row As GridViewRow
        Dim i As Integer
        'Creacion de variables
        Dim mensaje2 As String = ""
        tabla = Session("Tabla")
        Dim mensaje1 As String = ""
        Dim FechaVencimiento As String = ""
        Dim cventa As New cVentas
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
        Dim codprod As Integer
        Dim txtcant As TextBox
        Dim impuesto As DropDownList
        txtFactura.Text = "001-009-" & TraerNroFactura() + 1
        ' manejando los errores con el try
        Try
            'haciendo bucle al gridview

            codprod = cventa.InsertarCab(lblclieCod.Text, txtfecha.Text, txtFactura.Text, rbTipo.SelectedValue,
                                           exc, i5, i10, TF, tiva5, tiva10, tivas, rbEntrega.SelectedValue, rblForma.SelectedValue)
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
                    mensaje1 = cventa.InsertarDet(codprod, fila("Codigo"), CDec(txtcant.Text), CDbl(fila("Excenta")),
                                CDbl(fila("5")), CDbl(fila("10")))

                Next
                imprimir(codprod)
                Dim ultimo As String
                ultimo = txtFactura.Text.Substring(txtFactura.Text.LastIndexOf("-") + 1)
                cventa.ActualizarNroFactura(CInt(ultimo))
                'falta el tema de stock
                lblResultado.Text = "Movimiento Guardado"
                lblResultado.Visible = True
                lblclieCod.Text = ""
                lblCliente.Text = ""
                txtfecha.Text = ""
                txtFactura.Text = ""
                txtcliente.Text = ""
                lblRuc.Text = ""
                gvDatos.DataSource = Nothing
                gvDatos.DataBind()
                tabla = Nothing
                Session("Tabla") = Nothing
                Session("Tabla") = cventa.cargarTablaVentas
                SetFocus(txtcliente)
                txtfecha.Text = Date.Now.ToString("yyyy-MM-dd")
                txtFactura.Text = "001-009-" & TraerNroFactura() + 1
            Else
                lblResultado.Text = mensaje1
                lblResultado.Visible = True
            End If
        Catch ex As Exception
            lblResultado.Text = ex.Message
            lblResultado.Visible = True
        End Try

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
        Dim codprod As Integer
        Dim txtcant As TextBox
        Dim impuesto As DropDownList


        ' manejando los errores con el try
        Try
            'haciendo bucle al gridview

            codprod = cventa.InsertarCab(lblclieCod.Text, txtfecha.Text, "", rbTipo.SelectedValue,
                                           exc, i5, i10, TF, tiva5, tiva10, tivas, rbEntrega.SelectedValue, rblForma.SelectedValue)
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
                    mensaje1 = cventa.InsertarDet(codprod, fila("Codigo"), CDec(txtcant.Text), CDbl(fila("Excenta")),
                                CDbl(fila("5")), CDbl(fila("10")))

                Next

                'falta el tema de stock
                lblResultado.Text = "Movimiento Guardado"
                lblResultado.Visible = True
                lblclieCod.Text = ""
                lblCliente.Text = ""
                txtfecha.Text = ""
                txtFactura.Text = ""
                txtcliente.Text = ""
                lblRuc.Text = ""
                gvDatos.DataSource = Nothing
                gvDatos.DataBind()
                tabla = Nothing
                Session("Tabla") = Nothing
                Session("Tabla") = cventa.cargarTablaVentas
                SetFocus(txtcliente)
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


    Sub btnBuscaCl_Click()
        Dim ds As New cClientes
        Dim er As String
        Try
            gvCliente.DataSourceID = Nothing
            gvCliente.DataSource = ds.dsBuscarClientes(txtcliente.Text)
            gvCliente.DataBind()

        Catch ex As Exception
            er = ex.Message

        End Try
    End Sub
    Sub gvCliente_seleccionar()
        Dim cargar As New cClientes
        Dim grilla As New DataSet
        Dim fila As GridViewRow
        Dim info As String
        Dim nombre As String
        Dim ruc As String
        fila = gvCliente.SelectedRow
        info = fila.Cells(0).Text
        ID = CInt(info)
        ruc = fila.Cells(1).Text
        nombre = fila.Cells(2).Text
        lblclieCod.Text = ID
        lblRuc.Text = ruc
        lblCliente.Text = nombre
        gvCliente.DataSource = Nothing
        gvCliente.DataBind()

    End Sub

    Protected Sub rblSt_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblSt.SelectedIndexChanged
        cargarVerduras()
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
        Dim stockActual As Decimal
        Dim stockcomp As Decimal

        prodCod = rblVerduras.SelectedValue
        precio = traerprecio.traerPrecio(prodCod)
        stockActual = traerprecio.traerStock(prodCod)
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
            fila("Stock") = stockActual
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
                stockcomp = tabla.Rows(i)("Stock")
                If txtCant.Text > stockcomp Then
                    lblResultado.Visible = True
                    lblResultado.Text = "El valor mayor al stock en una de las filas"
                    '  Exit For
                End If
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
            fila("Stock") = stockActual
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
    Sub actualizarGrillaDatos()
        Dim txtCant As TextBox
        Dim txtUnit As TextBox
        Dim ddl As DropDownList
        Dim stockActual As Decimal
        tabla = Session("Tabla")
        For i = 0 To gvDatos.Rows.Count - 1
            stockActual = tabla.Rows(i)("Stock")
            ddl = gvDatos.Rows(i).FindControl("ddlImpuesto")
            txtCant = gvDatos.Rows(i).FindControl("txtgvCant")
            txtUnit = gvDatos.Rows(i).FindControl("txtgvUnit")
            tabla.Rows(i)("Cantidad") = txtCant.Text
            tabla.Rows(i)("Unitario") = txtUnit.Text
            If txtCant.Text > stockActual Then
                lblResultado.Visible = True
                lblResultado.Text = "El valor mayor al stock en una de las filas"
                'Exit For
            End If


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
        Dim txtUnitario As TextBox = CType(sender, TextBox)
        Dim row As GridViewRow = CType(txtUnitario.Parent.Parent, GridViewRow)
        Dim fila As Integer = row.RowIndex
        actualizarGrillaDatos()
        txtUnitario = gvDatos.Rows(fila).FindControl("txtgvUnit")
        SetFocus(txtUnitario)
    End Sub

    Protected Sub txtgvUnit_TextChanged(sender As Object, e As EventArgs)
        Dim txtUnitario As TextBox = CType(sender, TextBox)
        Dim row As GridViewRow = CType(txtUnitario.Parent.Parent, GridViewRow)
        Dim fila As Integer = row.RowIndex
        actualizarGrillaDatos()
        txtUnitario = gvDatos.Rows(fila).FindControl("txtgvUnit")
        SetFocus(txtUnitario)
    End Sub

    Sub btnNuevo_Click()
        Response.Redirect("~/Ventas/ventas.aspx")
    End Sub
    Sub btnBuscaClientes()
        Dim ds As New cClientes
        Dim er As String
        Try
            gvBuscar.DataSourceID = Nothing
            gvBuscar.DataSource = ds.dsBuscarClientes(txtClie.Text)
            gvBuscar.DataBind()

        Catch ex As Exception
            er = ex.Message

        End Try
    End Sub

    Sub TraerVentas()
        Dim cargar As New cClientes
        Dim cobros As New cCobros
        Dim grilla As New DataSet
        Dim fila As GridViewRow
        Dim info As String
        Dim nombre As String
        fila = gvBuscar.SelectedRow
        info = fila.Cells(0).Text
        ID = CInt(info)
        nombre = fila.Cells(2).Text

        'gvCliente.DataSource = Nothing
        'gvCliente.DataBind()

        gvVentas.DataSourceID = Nothing
        gvVentas.DataSource = cobros.dsBuscarClientes(ID)
        gvVentas.DataBind()
        gvVentas.Visible = True
    End Sub


    Protected Sub gvDatos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvBuscar.SelectedIndexChanged
        Dim cargar As New cClientes
        Dim cobros As New cCobros
        Dim grilla As New DataSet
        Dim fila As GridViewRow
        Dim info As String
        Dim nombre As String
        fila = gvBuscar.SelectedRow
        info = fila.Cells(0).Text
        ID = CInt(info)
        nombre = fila.Cells(2).Text

        'gvCliente.DataSource = Nothing
        'gvCliente.DataBind()

        gvVentas.DataSourceID = Nothing
        gvVentas.DataSource = cobros.dsBuscarClientesVentas(ID)
        gvVentas.DataBind()
        gvVentas.Visible = True
    End Sub

    Private Function TraerNroFactura() As Integer
        Dim ds As New DataSet
        Dim nro As New cVentas
        Dim dt As DataTable
        ds = nro.nroFactura
        Dim ultimo As Integer
        dt = ds.Tables(0)

        ultimo = dt.Rows(0)("nroFactura").ToString
        Return ultimo
    End Function

    Sub TraerFacturas()
        Dim row As GridViewRow
        Dim info As String
        Dim id As Integer
        Dim modificar As New cVentas
        Dim dt As DataTable
        Dim tablaBuscar As New DataTable
        row = gvVentas.SelectedRow
        info = row.Cells(0).Text
        id = CInt(info)
        Dim ds As New DataSet
        ds = modificar.Ventas_det(id)
        dt = ds.Tables(0)
        tablaBuscar = Nothing
        tablaBuscar = Session("Tabla")
        gvDatos.DataSource = Nothing
        gvDatos.DataBind()
        rblSt.Enabled = False
        rblVerduras.Enabled = False
        For i = 0 To dt.Rows.Count - 1
            If i = 0 Then

                fila = tablaBuscar.NewRow
                lblCodVenta.Text = dt.Rows(i)("ventaCod").ToString
                txtFactura.Text = dt.Rows(i)("ventaNroFact").ToString
                txtfecha.Text = dt.Rows(i)("ventaFchFact").ToString
                lblCliente.Text = dt.Rows(i)("clieNombre").ToString
                lblclieCod.Text = dt.Rows(i)("clieCod").ToString
                lblRuc.Text = dt.Rows(i)("clieruc").ToString
                rblForma.SelectedValue = dt.Rows(i)("tPagoCod").ToString
                rbEntrega.SelectedValue = dt.Rows(i)("ventaReparto").ToString
                rbTipo.SelectedValue = dt.Rows(i)("ventaTipo").ToString
                fila("Linea") = i + 1
                fila("Codigo") = dt.Rows(i)("prodCod").ToString
                fila("Producto") = dt.Rows(i)("prodNombre").ToString
                fila("Stock") = dt.Rows(i)("prodStock").ToString
                fila("Cantidad") = dt.Rows(i)("ventaDetCant").ToString
                If dt.Rows(i)("ventaPrecio5").ToString <> 0 Then

                    fila("Unitario") = dt.Rows(i)("ventaPrecio5").ToString / dt.Rows(i)("ventaDetCant").ToString
                    fila("Impuesto") = 5
                ElseIf dt.Rows(i)("ventaPrecio10").ToString <> 0 Then

                    fila("Unitario") = dt.Rows(i)("ventaPrecio10").ToString / dt.Rows(i)("ventaDetCant").ToString
                    fila("Impuesto") = 10
                Else
                    fila("Unitario") = dt.Rows(i)("ventaPrecioE").ToString / dt.Rows(i)("ventaDetCant").ToString
                    fila("Impuesto") = 0
                End If
                fila("Excenta") = dt.Rows(i)("ventaPrecioE").ToString
                fila("5") = dt.Rows(i)("ventaPrecio5").ToString
                fila("10") = dt.Rows(i)("ventaPrecio10").ToString
            Else
                fila = tablaBuscar.NewRow
                fila("Linea") = i + 1
                fila("Codigo") = dt.Rows(i)("prodCod").ToString
                fila("Producto") = dt.Rows(i)("prodNombre").ToString
                fila("Stock") = dt.Rows(i)("prodStock").ToString
                fila("Cantidad") = dt.Rows(i)("ventaDetCant").ToString
                If dt.Rows(i)("ventaPrecio5").ToString <> 0 Then

                    fila("Unitario") = dt.Rows(i)("ventaPrecio5").ToString / dt.Rows(i)("ventaDetCant").ToString
                    fila("Impuesto") = 5
                ElseIf dt.Rows(i)("ventaPrecio10").ToString <> 0 Then

                    fila("Unitario") = dt.Rows(i)("ventaPrecio10").ToString / dt.Rows(i)("ventaDetCant").ToString
                    fila("Impuesto") = 10
                Else
                    fila("Unitario") = dt.Rows(i)("ventaPrecioE").ToString / dt.Rows(i)("ventaDetCant").ToString
                    fila("Impuesto") = 0
                End If
                fila("Excenta") = dt.Rows(i)("ventaPrecioE").ToString
                fila("5") = dt.Rows(i)("ventaPrecio5").ToString
                fila("10") = dt.Rows(i)("ventaPrecio10").ToString


            End If

            tablaBuscar.Rows.Add(fila)

        Next


        gvDatos.DataSource = tablaBuscar
        gvDatos.DataBind()
        Session("Tabla") = tablaBuscar
        gvDatos.Enabled = False
        txtfecha.Enabled = False
        rbEntrega.Enabled = False
        rblForma.Enabled = False
        txtcliente.Enabled = False
        rbTipo.Enabled = False
        txtFactura.Enabled = False




    End Sub

    Sub Actualizar()
        rbEntrega.Enabled = True
        rblForma.Enabled = True
        txtFactura.Enabled = True
        rbTipo.Enabled = True
    End Sub
    Sub GuardarActualizar()
        Dim actualizar As New cVentas
        Dim resultado As String
        resultado = actualizar.ActualizarVentasCab(lblCodVenta.Text, txtfecha.Text, txtFactura.Text, rbTipo.SelectedValue, rbEntrega.SelectedValue, rblForma.SelectedValue)
        If resultado = "ok" Then
            lblResultado.Text = "Se actualizó correctamente los datos"
            lblResultado.Visible = True
            rbEntrega.Enabled = False
            rblForma.Enabled = False
            txtFactura.Enabled = False
            rbTipo.Enabled = False
        Else
            lblResultado.Text = "Hubo un problema al tratar de actualizar la venta"
            lblResultado.Visible = True
        End If
    End Sub
    Sub Anular()
        If txtMotivo.Text = "" Then
            lblResultado.Text = "Debe cargar el motivo de Anulación"
            lblResultado.Visible = True
        Else

            Dim eliminar As New cVentas
            Dim resultado As String = ""
            resultado = eliminar.AnularVentas(CInt(lblCodVenta.Text), txtMotivo.Text)
            lblResultado.Text = "Se anuló correctamente la venta"
            lblResultado.Visible = True
        End If
    End Sub

    Protected Sub rbTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbTipo.SelectedIndexChanged
        If rbTipo.SelectedValue = "Crédito" Then
            lblCredito.Enabled = True
            txtDias.Enabled = True
            SetFocus(txtDias)
        Else
            lblCredito.Enabled = False
            txtDias.Enabled = False
        End If

    End Sub

    Sub imprimir(ByVal codVenta As Integer)
        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim traerDs As New cVentas
        Dim letras As String
        Dim numero As Double
        Dim convertirnumero As New nroLetras
        Dim filtro As String = ""
        Dim i As Integer = 0
        ds = traerDs.Ventas_det(codVenta)
        dt = ds.Tables(0)
        Dim precioUnitario As String = ""
        Dim totalPrecios As Double = 0
        Dim Marca As String = ""
        Dim tablaBuscar As New DataTable
        tablaBuscar = Nothing
        tablaBuscar = Session("Tabla")
        Dim dtmDate As DateTime
        dtmDate = DateTime.Now()
        Dim iva10 As Double = 0
        Dim iva5 As Double = 0

        Dim FechaImp As String = "   Fndo de la Mora, " & dtmDate.ToString("dd") & " " & dtmDate.ToString("MMMM") & " de " & dtmDate.ToString("yyyy")
        Dim Cliente As String = dt.Rows(0)("clieNombre")
        Dim rucImp As String = dt.Rows(0)("clieruc")

        Dim OT As String = dt.Rows(0)("ventaCod")
        Dim tipo As String = dt.Rows(0)("ventaTipo")
        Dim cantImp As String
        Dim descpImp As String

        Dim tipoFactCo As String = ""
        Dim tipoFactCr As String = ""

        Dim precioImpFormat As String
        Dim totalPreciosFormat As String
        If tipo = "Contado" Then
            tipoFactCo = "   X"
        Else
            tipoFactCr = "X"
        End If

        Const fic As String = "factura.txt"


        Dim sw As New System.IO.StreamWriter(fic)
        Dim saltos As String = vbCr _
            & vbCr _
               & vbCr

        Dim detalle As String
        Dim pie As String

        Dim cabecera As String = vbCr _
                                  & vbCr _
                                    & vbCr _
                                    & vbCr _
                                    & vbCr _
                                    & vbCr _
                                    & vbCr _
                                    & vbCr _
                                    & vbCr _
                                    & vbCr _
                                    & vbCr _
                                    & vbCr _
                                    & "        " & FechaImp.PadLeft(38, " ") & tipoFactCo.PadLeft(37, " ") & tipoFactCr.PadLeft(16, " ") & vbCr _
                                    & vbCr _
                                    & "     " & vbTab & "  " & rucImp & vbCr _
                                    & "                 " & vbTab & "   " & Cliente & vbCr & vbCr & vbCr & vbCr
        sw.WriteLine(cabecera)



        Dim ventas5 As Double = 0
        Dim ventas10 As Double = 0
        Dim exenta As Double = 0
        Dim totalGral As Double = 0
        Dim espacio As Integer
        Dim unitario As Double
        Dim filaArriba As Integer = 0
        Dim cantidad As Decimal = 0
        Dim espaciototal As Integer = 0
        Dim total5 As Double = 0
        For i = 0 To dt.Rows.Count - 1
            cantidad = dt.Rows(i)("ventaDetCant").ToString
            cantImp = String.Format("{0:N2}", cantidad)
            descpImp = dt.Rows(i)("prodNombre").ToString



            If dt.Rows(i)("ventaPrecio5").ToString <> 0 Then

                unitario = dt.Rows(i)("ventaPrecio5").ToString / dt.Rows(i)("ventaDetCant").ToString

                precioImpFormat = String.Format("{0:N0}", unitario)
                espacio = 45
                espaciototal = 20
                ventas5 = dt.Rows(i)("ventaPrecio5").ToString
                totalPreciosFormat = String.Format("{0:N0}", ventas5)
                total5 = ventas5 + total5
            ElseIf dt.Rows(i)("ventaPrecio10").ToString <> 0 Then

                unitario = dt.Rows(i)("ventaPrecio10").ToString / dt.Rows(i)("ventaDetCant").ToString
                unitario = String.Format("{0:N0}", unitario)
                precioImpFormat = unitario
                espacio = 5
                totalPreciosFormat = dt.Rows(i)("ventaPrecio10").ToString
                totalPreciosFormat = String.Format("{0:N0}", totalPreciosFormat)
                ventas10 = ventas10 + totalPreciosFormat
            Else
                unitario = dt.Rows(i)("ventaPrecioE").ToString / dt.Rows(i)("ventaDetCant").ToString
                unitario = String.Format("{0:N0}", unitario)
                precioImpFormat = String.Format("{0:N0}", unitario)
                espacio = 25
                totalPreciosFormat = dt.Rows(i)("ventaPrecioE").ToString
                totalPreciosFormat = String.Format("{0:N0}", totalPreciosFormat)
                exenta = exenta + totalPreciosFormat
            End If

            detalle = vbCr & cantImp.PadLeft(15, "  ") & vbTab & vbTab & descpImp.PadRight(32, "    ") & precioImpFormat.PadRight(10, "    ") & vbTab & vbTab & totalPreciosFormat.PadRight(15, "    ")


            sw.WriteLine(detalle)

            totalGral = exenta + ventas10 + total5

            filaArriba = filaArriba + 1

        Next


        numero = CDbl(CInt(totalGral))

        Dim totalGralformat As String
        Dim ivaFormat5 As String
        Dim ivaFormat10 As String
        Dim totalIva As Double = 0
        Dim totalIVAImp As String
        letras = convertirnumero.Num2Text(numero)
        iva10 = CDbl(CInt(ventas10 / 11))
        ivaFormat10 = String.Format("{0:N0}", iva10)
        iva5 = CDbl(CInt(total5 / 21))
        totalIva = iva10 + iva5
        totalIVAImp = String.Format("{0:N0}", totalIva)

        ivaFormat5 = String.Format("{0:N0}", iva5)
        Dim numenter As Integer = 51 - filaArriba
        Dim mc As Integer = 0
        Dim x As Integer
        For i = 1 To filaArriba - 1
            If filaArriba = 1 Then
                x = 0
            Else
                x = x + 1
            End If

        Next

        While mc < numenter - 1 - x
            saltos = saltos & vbCr
            mc = mc + 1
        End While
        totalGralformat = String.Format("{0:N0}", totalGral)
        pie = saltos _
                & vbTab & "  " & letras.PadRight(80, "") & vbTab & totalGralformat _
               & vbCr _
                & vbCr _
                & vbCr & ivaFormat5.PadLeft(29, " ") & ivaFormat10.PadLeft(30, " ") & vbTab & vbTab & vbTab & totalIVAImp
        sw.WriteLine(pie)
        Dim marcaImpr As String
        marcaImpr = vbCr _
                 & vbCr _
                  & vbCr _
                 & vbTab & "   " & Marca
        sw.WriteLine(marcaImpr)
        sw.Close()
        saltos = Nothing
        Dim mapa As String
        mapa = Server.MapPath("factura.txt")

        Dim proc As New Process
        ' Dim impresora As New PrintDocument
        '  impresora.PrinterSettings.PrinterName = "ELX350"
        Try
            proc.StartInfo.FileName = "factura.txt"
            proc.StartInfo.Verb = "printto"
            proc.StartInfo.Arguments = "ELX350"

            'Dim st As StreamReader
            'st = New StreamReader("factura.txt")
            proc.StartInfo.CreateNoWindow = False
            proc.Start()
        Catch ex As Exception
            MsgBox(ex)
        End Try

        '  impresora.Print()
        'proc.CloseMainWindow()

    End Sub





    Sub imprimirVentas()


        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim traerDs As New cVentas
        Dim letras As String
        Dim numero As Double
        Dim convertirnumero As New nroLetras
        Dim filtro As String = ""
        Dim i As Integer = 0
        ds = traerDs.Ventas_det(lblCodVenta.Text)
        dt = ds.Tables(0)
        Dim precioUnitario As String = ""
        Dim totalPrecios As Double = 0
        Dim Marca As String = ""
        Dim tablaBuscar As New DataTable
        tablaBuscar = Nothing
        tablaBuscar = Session("Tabla")
        Dim dtmDate As DateTime
        dtmDate = DateTime.Now()
        Dim iva10 As Double = 0
        Dim iva5 As Double = 0


        Dim FechaImp As String = "   Fndo de la Mora, " & dtmDate.ToString("dd") & " " & dtmDate.ToString("MMMM") & " de " & dtmDate.ToString("yyyy")
        Dim Cliente As String = dt.Rows(0)("clieNombre")
        Dim rucImp As String = dt.Rows(0)("clieruc")

        Dim OT As String = dt.Rows(0)("ventaCod")
        Dim tipo As String = dt.Rows(0)("ventaTipo")
        Dim cantImp As String
        Dim descpImp As String

        Dim tipoFactCo As String = ""
        Dim tipoFactCr As String = ""

        Dim precioImpFormat As String
        Dim totalPreciosFormat As String
        If tipo = "Contado" Then
            tipoFactCo = "   X"
        Else
            tipoFactCr = "X"
        End If

        Const fic As String = "factura.txt"

        Dim sw As New System.IO.StreamWriter(fic)
        Dim saltos As String = vbCr _
            & vbCr _
               & vbCr

        Dim detalle As String
        Dim pie As String

        Dim cabecera As String = vbCr _
                                  & vbCr _
                                    & vbCr _
                                    & vbCr _
                                    & vbCr _
                                    & vbCr _
                                    & vbCr _
                                    & vbCr _
                                    & vbCr _
                                    & vbCr _
                                    & vbCr _
                                    & vbCr _
                                    & "        " & FechaImp.PadLeft(38, " ") & tipoFactCo.PadLeft(37, " ") & tipoFactCr.PadLeft(16, " ") & vbCr _
                                    & vbCr _
                                    & "     " & vbTab & "  " & rucImp & vbCr _
                                    & "                 " & vbTab & "   " & Cliente & vbCr & vbCr & vbCr & vbCr

        sw.WriteLine(cabecera)

        Dim ventas5 As Double = 0
        Dim ventas10 As Double = 0
        Dim exenta As Double = 0
        Dim totalGral As Double = 0
        Dim espacio As Integer
        Dim unitario As Double
        Dim filaArriba As Integer = 0
        Dim cantidad As Decimal = 0
        Dim espaciototal As Integer = 0
        Dim total5 As Double = 0
        For i = 0 To dt.Rows.Count - 1
            cantidad = dt.Rows(i)("ventaDetCant").ToString
            cantImp = String.Format("{0:N2}", cantidad)
            descpImp = dt.Rows(i)("prodNombre").ToString



            If dt.Rows(i)("ventaPrecio5").ToString <> 0 Then

                unitario = dt.Rows(i)("ventaPrecio5").ToString / dt.Rows(i)("ventaDetCant").ToString

                precioImpFormat = String.Format("{0:N0}", unitario)
                espacio = 45
                espaciototal = 20
                ventas5 = dt.Rows(i)("ventaPrecio5").ToString
                totalPreciosFormat = String.Format("{0:N0}", ventas5)
                total5 = ventas5 + total5
            ElseIf dt.Rows(i)("ventaPrecio10").ToString <> 0 Then

                unitario = dt.Rows(i)("ventaPrecio10").ToString / dt.Rows(i)("ventaDetCant").ToString
                unitario = String.Format("{0:N0}", unitario)
                precioImpFormat = unitario
                espacio = 5
                totalPreciosFormat = dt.Rows(i)("ventaPrecio10").ToString
                totalPreciosFormat = String.Format("{0:N0}", totalPreciosFormat)
                ventas10 = ventas10 + totalPreciosFormat
            Else
                unitario = dt.Rows(i)("ventaPrecioE").ToString / dt.Rows(i)("ventaDetCant").ToString
                unitario = String.Format("{0:N0}", unitario)
                precioImpFormat = String.Format("{0:N0}", unitario)
                espacio = 25
                totalPreciosFormat = dt.Rows(i)("ventaPrecioE").ToString
                totalPreciosFormat = String.Format("{0:N0}", totalPreciosFormat)
                exenta = exenta + totalPreciosFormat
            End If

            detalle = vbCr & cantImp.PadLeft(15, "  ") & vbTab & vbTab & descpImp.PadRight(32, "    ") & precioImpFormat.PadRight(10, "    ") & vbTab & vbTab & totalPreciosFormat.PadRight(15, "    ")


            sw.WriteLine(detalle)

            totalGral = exenta + ventas10 + total5

            filaArriba = filaArriba + 1

        Next


        numero = CDbl(CInt(totalGral))

        Dim totalGralformat As String
        Dim ivaFormat5 As String
        Dim ivaFormat10 As String
        Dim totalIva As Double = 0
        Dim totalIVAImp As String
        letras = convertirnumero.Num2Text(numero)
        iva10 = CDbl(CInt(ventas10 / 11))
        ivaFormat10 = String.Format("{0:N0}", iva10)
        iva5 = CDbl(CInt(total5 / 21))
        totalIva = iva10 + iva5
        totalIVAImp = String.Format("{0:N0}", totalIva)

        ivaFormat5 = String.Format("{0:N0}", iva5)
        Dim numenter As Integer = 51 - filaArriba
        Dim mc As Integer = 0
        Dim x As Integer
        For i = 1 To filaArriba - 1
            If filaArriba = 1 Then
                x = 0
            Else
                x = x + 1
            End If

        Next

        While mc < numenter - 1 - x
            saltos = saltos & vbCr
            mc = mc + 1
        End While
        totalGralformat = String.Format("{0:N0}", totalGral)
        pie = saltos _
                & vbTab & "  " & letras.PadRight(80, "") & vbTab & totalGralformat _
               & vbCr _
                & vbCr _
                & vbCr & ivaFormat5.PadLeft(29, " ") & ivaFormat10.PadLeft(30, " ") & vbTab & vbTab & vbTab & totalIVAImp
        sw.WriteLine(pie)
        Dim marcaImpr As String
        marcaImpr = vbCr _
                 & vbCr _
                  & vbCr _
                 & vbTab & "   " & Marca
        sw.WriteLine(marcaImpr)
        sw.Close()
        saltos = Nothing

        Dim ultimo As String
        Dim mapa As String
        mapa = Server.MapPath("factura.txt")

        Dim proc As New Process
        ' Dim impresora As New PrintDocument
        '  impresora.PrinterSettings.PrinterName = "ELX350"
        Try
            lblResultado.Text = mapa
            lblResultado.Visible = True
            proc.StartInfo.FileName = "factura.txt"
            proc.StartInfo.Verb = "printto"
            proc.StartInfo.Arguments = "ELX350"

            'Dim st As StreamReader
            'st = New StreamReader("factura.txt")
            proc.StartInfo.CreateNoWindow = False
            proc.Start()
            '  impresora.Print()
            'proc.CloseMainWindow()
            ultimo = txtFactura.Text.Substring(txtFactura.Text.LastIndexOf("-") + 1)
            traerDs.ActualizarNroFactura(CInt(ultimo))
        Catch ex As Exception


        End Try



    End Sub


End Class