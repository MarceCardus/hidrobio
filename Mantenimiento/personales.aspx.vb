Imports MySql.Data.MySqlClient
Public Class personales
    Inherits System.Web.UI.Page

    Sub btnAgregar_Click()
        Dim insertar As New cPersonales
        Dim resultado As String = ""

        resultado = insertar.Insertar(txtNombre.Text, txtci.Text, txtTelef.Text, rblChofer.SelectedValue, ddlFunc.SelectedValue,
                                      rblActivo.SelectedValue, rblTercer.SelectedValue, (txtNac.Text))

        CargarGrilla()

        If resultado = "ok" Then
            lblResultado.Text = "Se guardaron Correctamente los datos"
            lblResultado.Visible = True
            rblActivo.ClearSelection()
            ddlFunc.ClearSelection()
            rblTercer.ClearSelection()
            rblChofer.ClearSelection()
            txtNombre.Text = ""
            txtci.Text = ""
            txtTelef.Text = ""
            txtTelef.Text = ""

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
        Dim cargar As New cPersonales
        Dim grilla As New DataSet
        grilla = cargar.cGrilla()
        gvDatos.DataSource = grilla
        gvDatos.DataBind()
    End Sub
    Sub btnModificar_Click(sender As Object, e As EventArgs)
        Dim fila As GridViewRow
        Dim info As String
        Dim id As Integer
        Dim modificar As New cPersonales
        Dim resultado As String
        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
        id = CInt(info)


        resultado = modificar.Actualizar(id, txtNombre.Text, txtci.Text, txtTelef.Text, rblChofer.SelectedValue, ddlFunc.SelectedValue,
                                      rblActivo.SelectedValue, rblTercer.SelectedValue, CDate(txtNac.Text))

        If resultado = "ok" Then
            lblResultado.Text = "Se Modificó el dato, Código=" & id & ""
            lblResultado.Visible = True
            rblActivo.ClearSelection()
            ddlFunc.ClearSelection()
            rblTercer.ClearSelection()
            rblChofer.ClearSelection()
            txtNombre.Text = ""
            txtci.Text = ""
            txtTelef.Text = ""
            txtTelef.Text = ""

        End If


        CargarGrilla()

    End Sub



    Protected Sub gvDatos_SelectedIndexChanged1(sender As Object, e As EventArgs) Handles gvDatos.SelectedIndexChanged
        Dim fila As GridViewRow
        Dim info As String
        Dim id As Integer
        Dim modificar As New cPersonales
        Dim leer As MySqlDataReader

        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
        id = CInt(info)

        leer = modificar.ComandosLectura(id)
        If leer.Read Then
            txtNombre.Text = leer.Item("perNombre").ToString
            txtci.Text = leer.Item("perCi").ToString
            txtTelef.Text = leer.Item("perTelef").ToString
            rblChofer.ClearSelection()
            rblChofer.Items.FindByText(leer.Item("perChofer").ToString).Selected = True
            ddlFunc.ClearSelection()
            ddlFunc.Items.FindByText(leer.Item("perFuncion").ToString).Selected = True
            rblActivo.ClearSelection()
            rblActivo.Items.FindByText(leer.Item("perEstado").ToString).Selected = True
            rblTercer.ClearSelection()
            rblTercer.Items.FindByText(leer.Item("perTercer").ToString).Selected = True
            txtNac.Text = leer.Item("perFchNac").ToString
        End If
    End Sub
End Class