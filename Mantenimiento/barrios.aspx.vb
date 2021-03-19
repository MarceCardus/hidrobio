Imports MySql.Data.MySqlClient

Public Class barrios
    Inherits System.Web.UI.Page

    Sub btnAgregar_Click()
        Dim insertar As New cCiudades
        Dim resultado As String = ""

        resultado = insertar.InsertarBarrios(txtNombre.Text, ddlCiudad.SelectedValue)

        CargarGrilla()

        If resultado = "ok" Then
            lblResultado.Text = "Se guardaron Correctamente los datos"
            lblResultado.Visible = True

            ddlCiudad.ClearSelection()
            txtNombre.Text = ""


            CargarGrilla()
            CargarDDLCiudad()
        Else
            lblResultado.Text = "No se guardaron los datos. Hubo un problema en la Carga."
            lblResultado.Visible = True
        End If
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblResultado.Visible = False
            CargarGrilla()
            CargarDDLCiudad()
        End If
    End Sub
    Sub CargarGrilla()
        Dim cargar As New cCiudades
        Dim grilla As New DataSet
        grilla = cargar.cGrillaBarrios()
        gvDatos.DataSource = grilla
        gvDatos.DataBind()
    End Sub

    Sub CargarDDLCiudad()
        Dim cargar As New cCiudades
        Dim grilla As New DataSet
        grilla = cargar.cDataset("vciudades order by cNombre", "ciudades")
        ddlCiudad.DataSource = grilla
        ddlCiudad.DataTextField = "cNombre"
        ddlCiudad.DataValueField = "cCod"
        ddlCiudad.DataBind()


    End Sub
    Sub btnModificar_Click()
        Dim fila As GridViewRow
        Dim info As String
        Dim id As Integer
        Dim modificar As New cCiudades
        Dim resultado As String


        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
        id = CInt(info)


        resultado = modificar.ActualizarBarrios(id, txtNombre.Text, ddlCiudad.SelectedValue)

        If resultado = "ok" Then
            lblResultado.Text = "Se Modificó el dato, Código=" & id & ""
            lblResultado.Visible = True

            ddlCiudad.ClearSelection()
            txtNombre.Text = ""



            CargarGrilla()
            CargarDDLCiudad()

        End If



    End Sub

    Protected Sub gvDatos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDatos.SelectedIndexChanged
        Dim fila As GridViewRow
        Dim info As String
        Dim id As Integer
        Dim modificar As New cCiudades

        Dim leer As MySqlDataReader

        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
        id = CInt(info)


        leer = modificar.ComandosLecturaBarrios(id)
            If leer.Read Then
                txtNombre.Text = leer.Item("bNombre").ToString
                ddlCiudad.ClearSelection()
                ddlCiudad.Items.FindByText(leer.Item("cNombre").ToString).Selected = True
            End If


    End Sub
End Class