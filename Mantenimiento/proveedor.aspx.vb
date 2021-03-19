Imports MySql.Data.MySqlClient
Public Class proveedor
    Inherits System.Web.UI.Page

    Sub btnAgregar_Click()
        Dim insertar As New cProveedor
        Dim resultado As String = ""

        resultado = insertar.Insertar(txtNombre.Text, ddlAcopio.SelectedValue, txtruc.Text, txtResponsable.Text, txtTelef.Text, txtCoord.Text)

        CargarGrilla()

        If resultado = "ok" Then
            lblResultado.Text = "Se guardaron Correctamente los datos"
            lblResultado.Visible = True
            txtruc.Text = ""
            txtNombre.Text = ""
            txtResponsable.Text = ""
            txtTelef.Text = ""
            txtCoord.Text = ""
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
        Dim cargar As New cProveedor
        Dim grilla As New DataSet
        grilla = cargar.cGrilla()
        gvDatos.DataSource = grilla
        gvDatos.DataBind()
    End Sub
    Sub btnModificar_Click()
        Dim fila As GridViewRow
        Dim info As String
        Dim id As Integer
        Dim modificar As New cProveedor
        Dim resultado As String

        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
        id = CInt(info)


        resultado = modificar.Actualizar(id, txtNombre.Text, ddlAcopio.SelectedValue, txtruc.Text, txtResponsable.Text, txtTelef.Text, txtCoord.Text)

            If resultado = "ok" Then
                lblResultado.Text = "Se Modificó el dato, Código=" & id & ""
                lblResultado.Visible = True
                txtruc.Text = ""
                txtruc.Text = ""
                txtNombre.Text = ""
                txtResponsable.Text = ""
                txtTelef.Text = ""
                txtCoord.Text = ""

        End If

        CargarGrilla()

    End Sub
    Protected Sub gvDatos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDatos.SelectedIndexChanged
        Dim fila As GridViewRow
        Dim info As String
        Dim id As Integer
        Dim modificar As New cProveedor

        Dim leer As MySqlDataReader

        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
        id = CInt(info)


        leer = modificar.ComandosLectura(id)
        If leer.Read Then
            txtNombre.Text = leer.Item("provNombre").ToString
            ddlAcopio.ClearSelection()
            ddlAcopio.Items.FindByText(leer.Item("provAcopio").ToString).Selected = True
            txtruc.Text = leer.Item("provRuc").ToString
            txtResponsable.Text = leer.Item("provResponsable").ToString
            txtTelef.Text = leer.Item("provTelef").ToString
            txtCoord.Text = leer.Item("provCoord").ToString

        End If

    End Sub
End Class