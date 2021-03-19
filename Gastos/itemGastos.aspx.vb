Imports MySql.Data.MySqlClient
Public Class itemGastos
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblResultado.Visible = False
            CargarGrilla()
            CargarDDLEmpaque()
            CargarDDLRubro()
            CargarDDLImpuesto()
        End If
    End Sub
    Sub btnAgregar_Click()
        Dim insertarItem As New cGastos
        Dim resultado As String = ""

        resultado = insertarItem.InsertarItem(txtNombre.Text, txtPrecio.Text, ddlEmpaque.SelectedValue, ddlRubros.SelectedValue, ddlImpuesto.SelectedValue)

        CargarGrilla()

        If resultado = "ok" Then
            lblResultado.Text = "Se guardaron Correctamente los datos"
            lblResultado.Visible = True
            ddlEmpaque.ClearSelection()
            ddlImpuesto.ClearSelection()
            ddlRubros.ClearSelection()
            txtNombre.Text = ""
            txtPrecio.Text = ""
            CargarGrilla()
            CargarDDLEmpaque()
            CargarDDLRubro()
            CargarDDLImpuesto()
        Else
            lblResultado.Text = "No se guardaron los datos. Hubo un problema en la Carga."
            lblResultado.Visible = True
        End If
    End Sub

    Sub CargarGrilla()
        Dim cargar As New cProductos
        Dim grilla As New DataSet
        grilla = cargar.cDataset("vItem", "Grilla")
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
    Sub CargarDDLImpuesto()
        Dim cargar As New cProductos
        Dim grilla As New DataSet
        grilla = cargar.cDataset("vImpuesto", "Impuesto")
        ddlImpuesto.DataSource = grilla
        ddlImpuesto.DataTextField = "impNombre"
        ddlImpuesto.DataValueField = "impCod"
        ddlImpuesto.DataBind()
    End Sub
    Sub CargarDDLRubro()
        Dim cargar As New cProductos
        Dim grilla As New DataSet
        grilla = cargar.cDataset("vRubros", "Rubros")
        ddlRubros.DataSource = grilla
        ddlRubros.DataTextField = "rubroNombre"
        ddlRubros.DataValueField = "rubroCod"
        ddlRubros.DataBind()

    End Sub
    Sub btnModificar_Click()
        Dim fila As GridViewRow
        Dim info As String
        Dim id As Integer
        Dim modificar As New cGastos
        Dim resultado As String
        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
        id = CInt(info)


        resultado = modificar.ActualizarItem(id, txtNombre.Text, txtPrecio.Text, ddlEmpaque.SelectedValue, ddlRubros.SelectedValue, ddlImpuesto.SelectedValue)

        If resultado = "ok" Then
            lblResultado.Text = "Se Modificó el dato, Código=" & id & ""
            lblResultado.Visible = True
            ddlEmpaque.ClearSelection()
            ddlRubros.ClearSelection()
            ddlImpuesto.ClearSelection()
            txtNombre.Text = ""
            txtPrecio.Text = ""


            CargarGrilla()
            CargarDDLEmpaque()
            CargarGrilla()
            CargarDDLEmpaque()
            CargarDDLRubro()
            CargarDDLImpuesto()
        End If



    End Sub

    Protected Sub gvDatos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDatos.SelectedIndexChanged
        Dim fila As GridViewRow
        Dim info As String
        Dim id As Integer
        Dim modificar As New cGastos
        Dim leer As MySqlDataReader
        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
        id = CInt(info)
        leer = modificar.ComandosLecturaItem(id)

        If leer.Read Then
            txtNombre.Text = leer.Item("itemNombre").ToString
            txtPrecio.Text = leer.Item("itemPrecio").ToString
            ddlEmpaque.ClearSelection()
            ddlEmpaque.Items.FindByText(leer.Item("empaNombre").ToString).Selected = True
            ddlRubros.ClearSelection()
            ddlRubros.Items.FindByText(leer.Item("rubroNombre").ToString).Selected = True
            ddlImpuesto.ClearSelection()
            ddlImpuesto.Items.FindByText(leer.Item("impNombre").ToString).Selected = True

        End If

    End Sub
End Class