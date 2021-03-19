Imports MySql.Data.MySqlClient
Public Class anularNC
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
        grilla = cargar.cInformeNC(txtDesde.Text, txtHasta.Text)
        gvDatos.DataSource = grilla
        gvNCDet.DataSource = Nothing
        gvNCDet.DataBind()
        gvDatos.DataBind()

    End Sub



    Sub btnAceptar_Click()

        Dim eliminar As New cVentas
        Dim resultado As String = ""
        Dim fila As GridViewRow
        Dim info As String

        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
        ID = CInt(info)
        resultado = eliminar.AnularNC(ID, txtMotivo.Text)
        gvDatos.DataSource = Nothing
        gvNCDet.DataSource = Nothing
        gvDatos.DataBind()
        gvNCDet.DataBind()

        gvNCDet.DataBind()



    End Sub


    Protected Sub gvDatos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDatos.SelectedIndexChanged
        Dim cargar As New cVentas
        Dim grilla As New DataSet
        Dim fila As GridViewRow
        Dim info As String
        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
        ID = CInt(info)
        grilla = cargar.cGrillaNcAnul(ID)
        gvNCDet.DataSource = grilla
        gvNCDet.DataBind()
    End Sub


End Class