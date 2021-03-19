Imports MySql.Data.MySqlClient

Public Class moviles
    Inherits System.Web.UI.Page

    Sub btnAgregar_Click()
        Dim insertar As New cMoviles
        Dim resultado As String = ""

        resultado = insertar.Insertar(ddlMarca.SelectedValue, txtModelo.Text, txtanho.Text, txtChapa.Text, ddlRefri.SelectedValue, ddlActivo.SelectedValue,
                                      ddlTercer.SelectedValue)

        CargarGrilla()

        If resultado = "ok" Then
            lblResultado.Text = "Se guardaron Correctamente los datos"
            lblResultado.Visible = True
            ddlMarca.ClearSelection()
            ddlRefri.ClearSelection()
            ddlActivo.ClearSelection()
            ddlTercer.ClearSelection()
            txtModelo.Text = ""
            txtanho.Text = ""
            txtChapa.Text = ""

            CargarGrilla()
        Else
            lblResultado.Text = "No se guardaron los datos. Hubo un problema en la Carga."
            lblResultado.Visible = True
        End If
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblResultado.Visible = False
            CargarGrilla()
        End If
    End Sub
    Sub CargarGrilla()
        Dim cargar As New cMoviles
        Dim grilla As New DataSet
        grilla = cargar.cGrilla()
        gvDatos.DataSource = grilla
        gvDatos.DataBind()
    End Sub
    Sub btnModificar_Click()
        Dim fila As GridViewRow
        Dim info As String
        Dim id As Integer
        Dim modificar As New cMoviles
        Dim resultado As String

        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
        id = CInt(info)

        resultado = modificar.Actualizar(id, ddlMarca.SelectedValue, txtModelo.Text, txtanho.Text, txtChapa.Text, ddlRefri.SelectedValue, ddlActivo.SelectedValue,
                                      ddlTercer.SelectedValue)

        If resultado = "ok" Then
            lblResultado.Text = "Se Modificó el dato, Código=" & id & ""
            lblResultado.Visible = True
            ddlMarca.ClearSelection()
            ddlRefri.ClearSelection()
            ddlActivo.ClearSelection()
            ddlTercer.ClearSelection()
            txtModelo.Text = ""
            txtanho.Text = ""
            txtChapa.Text = ""



        End If


        CargarGrilla()

    End Sub

    Protected Sub gvDatos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDatos.SelectedIndexChanged
        Dim fila As GridViewRow
        Dim info As String
        Dim id As Integer
        Dim modificar As New cMoviles
        Dim leer As MySqlDataReader

        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
        id = CInt(info)


        leer = modificar.ComandosLectura(id)
            If leer.Read Then
                ddlMarca.ClearSelection()
                ddlMarca.Items.FindByText(leer.Item("movMarca").ToString).Selected = True
                txtModelo.Text = leer.Item("movModelo").ToString
                txtanho.Text = leer.Item("movAnho").ToString
                txtChapa.Text = leer.Item("movChapa").ToString
                ddlRefri.ClearSelection()
                ddlRefri.Items.FindByText(leer.Item("movRefri").ToString).Selected = True
                ddlActivo.ClearSelection()
                ddlActivo.Items.FindByText(leer.Item("movEstado").ToString).Selected = True
                ddlTercer.ClearSelection()
                ddlTercer.Items.FindByText(leer.Item("movTercer").ToString).Selected = True

            End If

    End Sub
End Class