Imports MySql.Data.MySqlClient
Public Class anularOP
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblResultado.Visible = False
            txtDesde.Text = Date.Now.ToString("yyyy-MM-dd")
            txtHasta.Text = Date.Now.ToString("yyyy-MM-dd")

        End If
    End Sub

    Sub btnBuscar_Click()
        Dim cargar As New cGastos
        Dim grilla As New DataSet
        grilla = cargar.cOPcab(txtDesde.Text, txtHasta.Text)
        gvDatos.DataSource = grilla
        gvDatos.DataBind()

    End Sub



    Sub btnAceptar_Click()

        Dim eliminar As New cGastos
        Dim resultado As String = ""
        Dim fila As GridViewRow
        Dim info As String

        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
        ID = CInt(info)
        resultado = eliminar.AnularOp(ID)
        gvDatos.DataSource = Nothing
        gvDatos.DataBind()
        gvDetalle.DataSource = Nothing
        gvDetalle.DataBind()


    End Sub

    Protected Sub gvDatos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDatos.SelectedIndexChanged
        Dim cargar As New cGastos
        Dim grilla As New DataSet
        Dim fila As GridViewRow
        Dim info As String
        fila = gvDatos.SelectedRow
        Try
            info = fila.Cells(0).Text
            ID = CInt(info)
            grilla = cargar.cGrillaDetOP(ID)
            gvDetalle.DataSource = grilla
            gvDetalle.DataBind()
        Catch ex As Exception

        End Try
    End Sub
End Class