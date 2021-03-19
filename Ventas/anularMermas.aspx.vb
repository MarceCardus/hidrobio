Imports MySql.Data.MySqlClient
Public Class anularMermas
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblResultado.Visible = False
            txtDesde.Text = Date.Now.ToString("yyyy-MM-dd")
            txtHasta.Text = Date.Now.ToString("yyyy-MM-dd")

        End If
    End Sub

    Sub btnBuscar_Click()
        Dim cargar As New cVentas
        Dim grilla As New DataSet
        grilla = cargar.cInformeMermas(txtDesde.Text, txtHasta.Text)
        gvDatos.DataSource = grilla
        gvDetalle.DataSource = Nothing
        gvDetalle.DataBind()
        gvDatos.DataBind()

    End Sub
    Protected Sub gvDatos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDatos.SelectedIndexChanged
        Dim cargar As New cVentas
        Dim grilla As New DataSet
        Dim fila As GridViewRow
        Dim info As String
        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
        ID = CInt(info)
        grilla = cargar.cGrillaDetMerma(ID)
        gvDetalle.DataSource = grilla
        gvDetalle.DataBind()
    End Sub

    Sub btnAceptar_Click()

        Dim eliminar As New cVentas
        Dim resultado As String = ""
        Dim fila As GridViewRow
        Dim info As String

        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
        ID = CInt(info)
        resultado = eliminar.AnularMermas(ID, "")
        gvDatos.DataSource = Nothing
        gvDetalle.DataSource = Nothing
        gvDatos.DataBind()
        gvDetalle.DataBind()

        gvDetalle.DataBind()



    End Sub


End Class