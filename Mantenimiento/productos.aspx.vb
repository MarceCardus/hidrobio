Imports MySql.Data.MySqlClient
Public Class productos
    Inherits System.Web.UI.Page

    Sub btnAgregar_Click()
        Dim insertar As New cProductos
        Dim resultado As String = ""

        resultado = insertar.Insertar(txtNombre.Text, ddlTipo.SelectedValue, txtFoto.Text, ddlEmpaque.SelectedValue, txtPrecio.Text, ddlSubGrupo.SelectedValue)

        CargarGrilla()

        If resultado = "ok" Then
            lblResultado.Text = "Se guardaron Correctamente los datos"
            lblResultado.Visible = True
            ddlEmpaque.ClearSelection()
            ddlTipo.ClearSelection()
            txtNombre.Text = ""
            txtFoto.Text = ""
            txtFoto.Text = ""
            txtFoto.Text = ""
            txtPrecio.Text = ""
            CargarGrilla()
            CargarDDLEmpaque()
            CargarDDLTipo()
            CargarDDLSubGrupo(ddlTipo.SelectedValue)
        Else
            lblResultado.Text = "No se guardaron los datos. Hubo un problema en la Carga."
            lblResultado.Visible = True
        End If
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblResultado.Visible = False
            CargarGrilla()
            CargarDDLEmpaque()
            CargarDDLTipo()
            CargarDDLSubGrupo(ddlTipo.SelectedValue)
        End If
    End Sub
    Sub CargarGrilla()
        Dim cargar As New cProductos
        Dim grilla As New DataSet
        grilla = cargar.cDataset("vproductos", "Grilla")
        gvDatos.DataSource = grilla
        gvDatos.DataBind()
    End Sub
    Sub CargarDDLEmpaque()
        Dim cargar As New cProductos
        Dim grilla As New DataSet
        grilla = cargar.cDataset("vempaque", "Empaque")
        ddlEmpaque.DataSource = grilla
        ddlEmpaque.DataTextField = "empaNombre"
        ddlEmpaque.DataValueField = "empaCod"
        ddlEmpaque.DataBind()
    End Sub
    Sub CargarDDLTipo()
        Dim cargar As New cProductos
        Dim grilla As New DataSet
        grilla = cargar.cDataset("vtprod", "tipo")
        ddlTipo.DataSource = grilla
        ddlTipo.DataTextField = "tipoProdDesc"
        ddlTipo.DataValueField = "tipoProdCod"
        ddlTipo.DataBind()


    End Sub
    Sub btnModificar_Click()
        Dim fila As GridViewRow
        Dim info As String
        Dim id As Integer
        Dim modificar As New cProductos
        Dim resultado As String
        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
        id = CInt(info)


        resultado = modificar.Actualizar(id, txtNombre.Text, ddlTipo.SelectedValue, txtFoto.Text, ddlEmpaque.SelectedValue, txtPrecio.Text, ddlSubGrupo.SelectedValue)

        If resultado = "ok" Then
            lblResultado.Text = "Se Modificó el dato, Código=" & id & ""
            lblResultado.Visible = True
            ddlEmpaque.ClearSelection()
            ddlTipo.ClearSelection()
            txtNombre.Text = ""
            txtFoto.Text = ""
            txtFoto.Text = ""
            txtFoto.Text = ""
            txtPrecio.Text = ""


            CargarGrilla()
            CargarDDLEmpaque()
            CargarDDLTipo()
            CargarDDLSubGrupo(ddlTipo.SelectedValue)
        End If



    End Sub

    Protected Sub ddlTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipo.SelectedIndexChanged
        CargarDDLSubGrupo(ddlTipo.SelectedValue)
    End Sub
    Sub CargarDDLSubGrupo(ByVal tipo As Integer)
        Dim cargar As New cProductos
        Dim grilla As New DataSet
        grilla = cargar.cdSubgrupo("vSubTipoProduc where tipoProdCod=" & tipo & "", "subtipo")
        ddlSubGrupo.DataSource = grilla
        ddlSubGrupo.DataTextField = "stpDesc"
        ddlSubGrupo.DataValueField = "stpCod"
        ddlSubGrupo.DataBind()


    End Sub

    Protected Sub gvDatos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDatos.SelectedIndexChanged
        Dim fila As GridViewRow
        Dim info As String
        Dim id As Integer
        Dim modificar As New cProductos
        Dim leer As MySqlDataReader
        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
        id = CInt(info)
        leer = modificar.ComandosLectura(id)

        If leer.Read Then
            txtNombre.Text = leer.Item("prodNombre").ToString
            txtFoto.Text = leer.Item("prodFoto").ToString
            txtPrecio.Text = leer.Item("prodPrecio").ToString
            ddlTipo.ClearSelection()
            ddlTipo.Items.FindByText(leer.Item("tipoProdDesc").ToString).Selected = True
            ddlSubGrupo.ClearSelection()
            CargarDDLSubGrupo(CInt(leer.Item("tipoProdCod").ToString))
            ddlSubGrupo.DataBind()
            ddlSubGrupo.Items.FindByText(leer.Item("stpDesc").ToString).Selected = True
            ddlEmpaque.ClearSelection()
            ddlEmpaque.Items.FindByText(leer.Item("empaNombre").ToString).Selected = True
        End If

    End Sub
End Class