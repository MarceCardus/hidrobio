Imports MySql.Data.MySqlClient
Public Class anularReparto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblResultado.Visible = False
            txtDesde.Text = Date.Now.ToString("yyyy-MM-dd")
            txtHasta.Text = Date.Now.ToString("yyyy-MM-dd")

        End If
    End Sub


    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim cargar As New cReparto
        Dim grilla As New DataSet
        grilla = cargar.cGVReparto(txtDesde.Text, txtHasta.Text)
        gvDatos.DataSource = grilla
        gvDatos.DataBind()
        gvDetalle.DataSource = Nothing
        gvDetalle.DataBind()

    End Sub
    Protected Sub gvDatos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDatos.SelectedIndexChanged
        Dim cargar As New cVentas
        Dim grilla As New DataSet
        Dim fila As GridViewRow
        Dim info As String
        fila = gvDatos.SelectedRow
        Try
            info = fila.Cells(1).Text
            ID = CInt(info)
            grilla = cargar.cGrillaDet(ID)
            gvDetalle.DataSource = grilla
            gvDetalle.DataBind()
        Catch ex As Exception

        End Try

    End Sub



    Protected Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim eliminar As New cReparto
        Dim resultado As String = ""
        Dim fila As GridViewRow
        Dim info As String

        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
        ID = CInt(info)
        resultado = eliminar.AnularReparto(ID)
        gvDatos.DataSource = Nothing
        gvDetalle.DataSource = Nothing
        gvDatos.DataBind()
        gvDetalle.DataBind()

        gvDetalle.DataBind()
    End Sub
End Class