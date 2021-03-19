Imports System.Data
Imports MySql.Data.MySqlClient
Public Class clientes
    Inherits System.Web.UI.Page

    Sub btnAgregar_Click()
        Dim insertar As New cClientes
        Dim resultado As String = ""

        resultado = insertar.Insertar(txtruc.Text, txtNombre.Text, txtResponsable.Text, txtTelef.Text, txtmail.Text, txtCoord.Text, txtDireccion.Text,
ddlBarrio.SelectedValue, txtNroCasa.Text)

        CargarGrilla()

        If resultado = "ok" Then
            lblResultado.Text = "Se guardaron Correctamente los datos"
            lblResultado.Visible = True
            txtruc.Text = ""
            txtNombre.Text = ""
            txtResponsable.Text = ""
            txtTelef.Text = ""
            txtmail.Text = ""
            txtCoord.Text = ""
            txtNroCasa.Text = ""
            CargarGrilla()
            CargarDDLCiudad()
            CargarBarrios(ddlCiudad.SelectedValue)
            RequiredFieldValidator4.Enabled = True
            RequiredFieldValidator3.Enabled = True
        Else
            lblResultado.Text = "No se guardaron los datos. Hubo un problema en la Carga."
            lblResultado.Visible = True
        End If
    End Sub

    'Protected Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click

    'End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblResultado.Visible = False
            CargarGrilla()
            CargarDDLCiudad()
            CargarBarrios(ddlCiudad.SelectedValue)
            RequiredFieldValidator4.Enabled = True
            RequiredFieldValidator3.Enabled = True
        End If
    End Sub
    Sub CargarDDLCiudad()
        Dim cargar As New cCiudades
        Dim grilla As New DataSet
        grilla = cargar.cDataset("vciudades order by cNombre", "ciudades")
        ddlCiudad.DataSource = grilla
        ddlCiudad.DataTextField = "cNombre"
        ddlCiudad.DataValueField = "cCod"
        ddlCiudad.DataBind()
        CargarBarrios(ddlCiudad.SelectedValue)

    End Sub
    Sub CargarBarrios(ByVal cCod As Int16)
        Dim cargar As New cCiudades
        Dim grilla As New DataSet
        grilla = cargar.cDataset("vBarrios where cCod = " & cCod & " order by bNombre", "barrios")
        ddlBarrio.DataSource = grilla
        ddlBarrio.DataTextField = "bNombre"
        ddlBarrio.DataValueField = "bCod"
        ddlBarrio.DataBind()


    End Sub
    Sub CargarGrilla()
        Dim cargar As New cClientes
        Dim grilla As New DataSet
        grilla = cargar.cGrilla()
        gvDatos.DataSource = grilla
        gvDatos.DataBind()
    End Sub

    Sub btnModificar_Click()
        Dim resultado As String
        Dim modificar As New cClientes
        Dim fila As GridViewRow
        Dim info As String
        Dim id As Integer
        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
        ID = CInt(info)
        resultado = modificar.Actualizar(id, txtruc.Text, txtNombre.Text, txtResponsable.Text, txtTelef.Text, txtmail.Text,
txtDireccion.Text, ddlBarrio.SelectedValue, txtNroCasa.Text)

        If resultado = "ok" Then
            lblResultado.Text = "Se Modificó el dato, Código=" & ID & ""
            lblResultado.Visible = True
            txtruc.Text = ""
            txtruc.Text = ""
            txtNombre.Text = ""
            txtResponsable.Text = ""
            txtTelef.Text = ""
            txtmail.Text = ""
            txtCoord.Text = ""
            txtNroCasa.Text = ""
            txtDireccion.Text = ""
            RequiredFieldValidator4.Enabled = True
            RequiredFieldValidator3.Enabled = True
            CargarGrilla()
        End If

    End Sub


    Protected Sub ddlCiudad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCiudad.SelectedIndexChanged
        CargarBarrios(ddlCiudad.SelectedValue)
    End Sub




    Protected Sub gvDatos_RowEditing(sender As Object, e As GridViewEditEventArgs)

    End Sub

    Protected Sub gvDatos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDatos.SelectedIndexChanged
        RequiredFieldValidator4.Enabled = False
        RequiredFieldValidator3.Enabled = False
        Dim fila As GridViewRow
        Dim info As String
        Dim id As Integer
        Dim modificar As New cClientes
        Dim leer As MySqlDataReader
        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
        id = CInt(info)

        Dim ciudad As Int32

        leer = modificar.ComandosLectura(id)
        If leer.Read Then
            txtruc.Text = leer.Item("clieRuc").ToString
            txtNombre.Text = leer.Item("clieNombre").ToString
            txtResponsable.Text = leer.Item("clieResponsable").ToString
            txtTelef.Text = leer.Item("clieTelef").ToString
            txtmail.Text = leer.Item("clieMail").ToString
            '   txtCoord.Text = leer.Item("clieCoord").ToString
            txtDireccion.Text = leer.Item("clieDireccion").ToString
            ddlCiudad.ClearSelection()
            ddlCiudad.Items.FindByText(leer.Item("cNombre").ToString).Selected = True
            ciudad = leer.Item("cCod").ToString
            ddlBarrio.ClearSelection()
            CargarBarrios(ciudad)
            ddlBarrio.Items.FindByText(leer.Item("barrios").ToString).Selected = True
            txtNroCasa.Text = leer.Item("clieNroCasa").ToString
        End If


        'CargarGrilla()
    End Sub
End Class