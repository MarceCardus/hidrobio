Imports MySql.Data.MySqlClient
Public Class tipoPago
    Inherits System.Web.UI.Page


    Sub btnAgregar_Click()
        Dim insertar As New cTPagos
        Dim resultado As String = ""

        resultado = insertar.Insertar(txtDesc.Text)

        CargarGrilla()

        If resultado = "ok" Then
            lblResultado.Text = "Se guardaron Correctamente los datos"
            lblResultado.Visible = True
            txtDesc.Text = ""
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
        Dim cargar As New cTPagos
        Dim grilla As New DataSet
        grilla = cargar.cGrilla()
        gvDatos.DataSource = grilla
        gvDatos.DataBind()
    End Sub
    Sub btnModificar_Click()
        Dim fila As GridViewRow
        Dim info As String
        Dim id As Integer
        Dim modificar As New cTPagos
        Dim resultado As String


        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
        id = CInt(info)


        resultado = modificar.Actualizar(id, txtDesc.Text)

        If resultado = "ok" Then
            lblResultado.Text = "Se Modificó el dato, Código=" & id & ""
            lblResultado.Visible = True
            txtDesc.Text = ""


        End If


        CargarGrilla()

    End Sub
    'Protected Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
    '    Dim eliminar As New cTPagos
    '    Dim resultado As String = ""
    '    Dim fila As GridViewRow
    '    Dim info As String
    '    If MsgBox("¿Estas seguro de querer borrar el registro?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirmación") = MsgBoxResult.Yes Then
    '        fila = gvDatos.SelectedRow
    '        info = fila.Cells(0).Text
    '        ID = CInt(info)
    '        resultado = eliminar.eliminar(ID)
    '    End If


    '    CargarGrilla()

    '    If resultado = "ok" Then
    '        lblResultado.Text = "El registro fue eliminado satisfactoriamente"
    '        lblResultado.Visible = True
    '        txtDesc.Text = ""
    '        CargarGrilla()
    '    Else
    '        lblResultado.Text = "No se guardaron los datos. Hubo un problema en la Carga."
    '        lblResultado.Visible = True
    '    End If
    'End Sub

    Protected Sub gvDatos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDatos.SelectedIndexChanged
        Dim fila As GridViewRow
        Dim info As String
        Dim id As Integer
        Dim modificar As New cTPagos

        Dim leer As MySqlDataReader

        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
        id = CInt(info)

        leer = modificar.ComandosLectura(id)
        If leer.Read Then
                txtDesc.Text = leer.Item("tPagoDesc").ToString

            End If
    End Sub
End Class