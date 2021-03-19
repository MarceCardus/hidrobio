Imports MySql.Data.MySqlClient

Public Class anularAcopio
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblResultado.Visible = False
            txtDesde.Text = Date.Now.ToString("yyyy-MM-dd")
            txtHasta.Text = Date.Now.ToString("yyyy-MM-dd")
        End If
    End Sub
    Sub btnEliminar_Click()
        Dim eliminar As New cCompras
        Dim resultado As String = ""
        Dim fila As GridViewRow
        Dim info As String
        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
            ID = CInt(info)
            resultado = eliminar.Anular(ID)
        If resultado = "ok" Then
            lblResultado.Text = "El registro fue eliminado satisfactoriamente"
            lblResultado.Visible = True
            txtHasta.Text = ""
            txtDesde.Text = ""
            gvDatos.DataBind()
            gvDetalle.DataBind()
            gvDetalle.DataBind()
            txtDesde.Text = Date.Now.ToString("yyyy-MM-dd")
            txtHasta.Text = Date.Now.ToString("yyyy-MM-dd")

        Else
            lblResultado.Text = "No se guardaron los datos. Hubo un problema en la Carga."
            lblResultado.Visible = True
        End If
    End Sub
    Sub btnBuscar_Click()
        Dim cargar As New cCompras
        Dim grilla As New DataSet
        grilla = cargar.cGrilla(txtDesde.Text, txtHasta.Text)
        gvDatos.DataSource = grilla
        gvDatos.DataBind()
    End Sub
    Protected Sub gvDatos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDatos.SelectedIndexChanged
        Dim cargar As New cCompras
        Dim grilla As New DataSet
        Dim fila As GridViewRow
        Dim info As String
        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
        ID = CInt(info)
        grilla = cargar.cGrillaDet(ID)
        gvDetalle.DataSource = grilla
        gvDetalle.DataBind()
    End Sub

End Class