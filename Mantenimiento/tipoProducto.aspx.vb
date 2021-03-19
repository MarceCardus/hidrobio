Imports MySql.Data.MySqlClient

Public Class tipoProducto
    Inherits System.Web.UI.Page


    Sub btnAgregar_Click()
        Dim insertar As New cTProductos
        Dim resultado As String = ""

        resultado = insertar.InsertarTProd(txtRubro.Text)

        CargarGrilla()

        If resultado = "ok" Then
            lblResultado.Text = "Se guardaron Correctamente los datos"
            lblResultado.Visible = True
            txtRubro.Text = ""
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
        Dim cargar As New cTProductos
        Dim grilla As New DataSet
        grilla = cargar.cgTProd()
        gvRubros.DataSource = grilla
        gvRubros.DataBind()
    End Sub
    Sub btnModificar_Click()
        Dim fila As GridViewRow
        Dim info As String
        Dim id As Integer
        Dim modificar As New cTProductos
        Dim resultado As String

        fila = gvRubros.SelectedRow
        info = fila.Cells(0).Text
        id = CInt(info)


        resultado = modificar.ActualizarTProd(id, txtRubro.Text)

        If resultado = "ok" Then
            lblResultado.Text = "Se Modifico el dato, Código=" & id & ""
            lblResultado.Visible = True
            txtRubro.Text = ""


        End If


        CargarGrilla()

    End Sub
    'Protected Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
    '    Dim eliminar As New cTProductos
    '    Dim resultado As String = ""
    '    Dim fila As GridViewRow
    '    Dim info As String
    '    If MsgBox("¿Estas seguro de querer borrar el registro?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirmación") = MsgBoxResult.Yes Then
    '        fila = gvRubros.SelectedRow
    '        info = fila.Cells(0).Text
    '        ID = CInt(info)
    '        resultado = eliminar.eliminarTProd(ID)
    '    End If


    '    CargarGrilla()

    '    If resultado = "ok" Then
    '        lblResultado.Text = "El registro fue eliminado satisfactoriamente"
    '        lblResultado.Visible = True
    '        txtRubro.Text = ""
    '        CargarGrilla()
    '    Else
    '        lblResultado.Text = "No se guardaron los datos. Hubo un problema en la Carga."
    '        lblResultado.Visible = True
    '    End If
    'End Sub

    Protected Sub gvRubros_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvRubros.SelectedIndexChanged
        Dim fila As GridViewRow
        Dim info As String
        Dim id As Integer
        Dim modificar As New cTProductos

        Dim leer As MySqlDataReader

        fila = gvRubros.SelectedRow
        info = fila.Cells(0).Text
        id = CInt(info)


        leer = modificar.ComandosLectura(id)
            If leer.Read Then
                txtRubro.Text = leer.Item("tipoProdDesc").ToString

            End If

    End Sub
End Class