Imports MySql.Data.MySqlClient

Public Class notaCredito
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

    Dim totalG As Double = 0
    Dim total5 As Double = 0
    Dim totalE As Double = 0
    Dim total10 As Double = 0


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

            codprod = cventa.InsertarCabNC(lblventCod.Text, txtfecha.Text, txtFactura.Text, exc, i5, i10, TF, tiva5, tiva10, tivas)
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
                    mensaje1 = cventa.InsertarDetNC(codprod, fila("Codigo"), CDec(txtcant.Text), CDbl(fila("Excenta")),
                                CDbl(fila("5")), CDbl(fila("10")))


                Next

                'falta el tema de stock
                lblResultado.Text = "Movimiento Guardado"
                lblResultado.Visible = True
                txtfecha.Text = ""
                txtFactura.Text = ""
                gvDatos.DataSource = Nothing
                gvDatos.DataBind()
                tabla = Nothing
                Session("Tabla") = Nothing
                Session("Tabla") = cventa.cargarTablaVentas
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


    'Protected Sub gvVentas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvVentas.SelectedIndexChanged

    '    Dim grilla As New DataSet
    '    Dim fila As GridViewRow
    '    Dim info As String
    '    Dim nombre As String
    '    fila = gvVentas.SelectedRow
    '    info = fila.Cells(0).Text
    '    ID = CInt(info)
    '    nombre = fila.Cells(1).Text
    '    lblventCod.Text = ID
    '    lblCliente.Text = nombre
    '    gvVentas.DataSource = Nothing
    '    gvVentas.DataBind()

    'End Sub
    Sub TraerFacturas()
        Dim row As GridViewRow
        Dim info As String
        Dim id As Integer
        Dim modificar As New cVentas
        Dim dt As DataTable

        row = gvVentas.SelectedRow
        info = row.Cells(0).Text
        id = CInt(info)
        Dim ds As New DataSet
        ds = modificar.Ventas_det(id)
        dt = ds.Tables(0)
        tabla = Session("Tabla")

        For i = 0 To dt.Rows.Count - 1

            lblventCod.Text = dt.Rows(i)("ventaCod").ToString

            lblCliente.Text = dt.Rows(i)("clieNombre").ToString

        Next


        gvDatos.DataSource = tabla
        gvDatos.DataBind()
        Session("Tabla") = tabla




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

    Protected Sub gvDatos_RowDataBound_delete(sender As Object, e As GridViewDeleteEventArgs) Handles gvDatos.RowDeleting

        Dim x As Integer = e.RowIndex
        tabla = Session("Tabla")
        tabla.Rows(x).Delete()
        Session("Tabla") = tabla
        gvDatos.DataSource = tabla
        gvDatos.DataBind()
    End Sub
    Protected Sub gvDatos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDatos.RowDataBound

        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                total5 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "5"))
                total10 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "10"))
                totalE += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Excenta"))
                totalG = totalE + total5 + total10

            ElseIf (e.Row.RowType = DataControlRowType.Footer) Then

                e.Row.Cells(3).Text = "Totales:"
                e.Row.Cells(4).Text = totalE.ToString("N0")

                e.Row.Cells(5).Text = total5.ToString("N0")

                e.Row.Cells(6).Text = total10.ToString("N0")
                e.Row.Cells(7).Text = totalG.ToString("N0")
                e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Center
                e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Center
                e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Center
                e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Center
                e.Row.Font.Bold = True

            End If

        Catch ex As Exception
            ex.Message.ToString()
        End Try

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
        gvVentas.DataSource = cobros.dsBuscarClientes(ID)
        gvVentas.DataBind()
        gvVentas.Visible = True
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
    End Sub
End Class