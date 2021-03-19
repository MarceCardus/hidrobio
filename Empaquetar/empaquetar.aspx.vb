Imports MySql.Data.MySqlClient
Public Class empaquetar
    Inherits System.Web.UI.Page

    Dim tabla As New DataTable
    Dim fila As DataRow

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim cmdTabla As New cStock
            lblResultado.Visible = False
            lblCod.Visible = False
            lblNombre.Visible = False
            lblCantidad.Visible = False
            lblCod1.Visible = False
            lblNombre1.Visible = False
            Session("Tabla") = cmdTabla.cargarTablaEmpaque
            txtFecha.Text = Date.Now.ToString("yyyy-MM-dd")
        End If
    End Sub

    Protected Sub btnAgregar_Click1(sender As Object, e As EventArgs) Handles btnAgregar.Click


        If txtCantidad.Text = "" Or txtCantNueva.Text = "" Then
            lblCantidad.Text = "Debe Cargar una cantidad y precio"
            lblCantidad.Visible = True
        Else

            tabla = Session("Tabla")
            fila = tabla.NewRow
            fila("Codigo") = lblCod.Text
            fila("Producto") = HttpUtility.HtmlDecode(lblNombre.Text)
            fila("Anterior") = txtCantidad.Text
            fila("empaCod") = lblCod1.Text
            fila("Empaque") = HttpUtility.HtmlDecode(lblNombre1.Text)
            fila("Empaquetado") = txtCantNueva.Text
            tabla.Rows.Add(fila)
            gvDatos.DataSource = tabla
            gvDatos.DataBind()
            Session("Tabla") = tabla
            txtCantidad.Text = ""
            txtCantNueva.Text = ""
            txtProducto.Text = ""
            txtProducto1.Text = ""
            SetFocus(txtProducto1)
            lblCantidad.Visible = False
        End If

    End Sub
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim ds As New cProductos
        Dim er As String
        Try
            gvProductos.DataSourceID = Nothing
            gvProductos.DataSource = ds.dsBuscarProdcutos(txtProducto.Text)
            gvProductos.DataBind()
            gvProductos.Visible = True
        Catch ex As Exception
            er = ex.Message

        End Try
    End Sub
    Protected Sub gvProductos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvProductos.SelectedIndexChanged
        Dim fila As GridViewRow
        Dim cod As String
        Dim desc As String

        fila = gvProductos.SelectedRow
        cod = fila.Cells(0).Text
        desc = fila.Cells(1).Text
        lblCod.Text = cod
        lblNombre.Text = desc
        lblCod.Visible = True
        lblNombre.Visible = True
        gvProductos.Visible = False
        SetFocus(txtCantidad)
    End Sub

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
        Dim codprod As Integer = 0
        tabla = Session("Tabla")
        Dim mensaje1 As String = ""
        Dim FechaVencimiento As String = ""
        Dim cCosecha As New cStock
        Dim anterior As Decimal

        'manejando los errores con el try
        Try
            'haciendo bucle al gridview




            For i = 0 To gvDatos.Rows.Count - 1
                row = gvDatos.Rows(i)
                fila = tabla.Rows(i)
                anterior = CDec(fila("Anterior") * -1)
                mensaje1 = cCosecha.InsertarEmp(fila("Codigo"), fila("empaCod"), anterior, fila("Empaquetado"), txtFecha.Text)

            Next
            If mensaje1 = "ok" Then
                lblResultado.Text = "Movimiento Guardado"
                lblResultado.Visible = True
                txtCantidad.Text = ""
                txtCantNueva.Text = ""
                txtProducto.Text = ""
                gvDatos.DataSource = Nothing
                gvDatos.DataBind()
                tabla = Nothing
                Session("Tabla") = Nothing
                Session("Tabla") = cCosecha.cargarTablaEmpaque
                SetFocus(txtProducto)
                lblCod.Text = ""
                lblNombre.Text = ""
                lblCod1.Text = ""
                lblNombre1.Text = ""
            Else
                lblResultado.Text = mensaje1
                lblResultado.Visible = True
            End If

        Catch ex As Exception
            lblResultado.Text = ex.Message
            lblResultado.Visible = True
        End Try

    End Sub


    Protected Sub btnBuscar1_Click(sender As Object, e As EventArgs) Handles btnBuscar1.Click
        Dim ds As New cProductos
        Dim er As String
        Try
            gvProducto1.DataSourceID = Nothing
            gvProducto1.DataSource = ds.dsBuscarProdcutos(txtProducto1.Text)
            gvProducto1.DataBind()
            gvProducto1.Visible = True
        Catch ex As Exception
            er = ex.Message

        End Try
    End Sub
    Protected Sub gvProducto1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvProducto1.SelectedIndexChanged
        Dim fila As GridViewRow
        Dim cod As String
        Dim desc As String

        fila = gvProducto1.SelectedRow
        cod = fila.Cells(0).Text
        desc = fila.Cells(1).Text
        lblCod1.Text = cod
        lblNombre1.Text = desc
        lblCod1.Visible = True
        lblNombre1.Visible = True
        gvProducto1.Visible = False
        SetFocus(txtCantNueva)
    End Sub

End Class