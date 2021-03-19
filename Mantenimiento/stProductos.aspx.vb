Imports MySql.Data.MySqlClient

Public Class stProductos
    Inherits System.Web.UI.Page

    Sub btnAgregar_Click()
        Dim insertar As New cProductos
        Dim resultado As String = ""

        resultado = insertar.InsertarSubTipo(txtNombre.Text, ddlTipoPro.SelectedValue)

        CargarGrilla()

        If resultado = "ok" Then
            lblResultado.Text = "Se guardaron Correctamente los datos"
            lblResultado.Visible = True

            ddlTipoPro.ClearSelection()
            txtNombre.Text = ""


            CargarGrilla()
            CargarDDLTipo()
        Else
            lblResultado.Text = "No se guardaron los datos. Hubo un problema en la Carga."
            lblResultado.Visible = True
        End If
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblResultado.Visible = False
            CargarGrilla()
            CargarDDLTipo()
        End If
    End Sub
    Sub CargarGrilla()
        Dim cargar As New cProductos
        Dim grilla As New DataSet
        grilla = cargar.cGrillaSubTipoProduc()
        gvDatos.DataSource = grilla
        gvDatos.DataBind()
    End Sub

    Sub CargarDDLTipo()
        Dim cargar As New cProductos
        Dim grilla As New DataSet
        grilla = cargar.cDataset("vtprod order by tipoProdDesc", "tipoProductos")
        ddlTipoPro.DataSource = grilla
        ddlTipoPro.DataTextField = "tipoProdDesc"
        ddlTipoPro.DataValueField = "tipoProdCod"
        ddlTipoPro.DataBind()


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


        resultado = modificar.ActualizarSubTipo(id, txtNombre.Text, ddlTipoPro.SelectedValue)

        If resultado = "ok" Then
            lblResultado.Text = "Se Modificó el dato, Código=" & id & ""
            lblResultado.Visible = True

            ddlTipoPro.ClearSelection()
            txtNombre.Text = ""



            CargarGrilla()
            CargarDDLTipo()

        End If



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


        leer = modificar.ComandosLecturaSubTipo(id)
        If leer.Read Then
            txtNombre.Text = leer.Item("stpDesc").ToString
            ddlTipoPro.ClearSelection()
            ddlTipoPro.Items.FindByText(leer.Item("tipoProdDesc").ToString).Selected = True
        End If


    End Sub
End Class