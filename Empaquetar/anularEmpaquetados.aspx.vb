Imports System.Data
Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Public Class anularEmpaquetados
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblResultado.Visible = False
            txtDesde.Text = Date.Now.ToString("yyyy-MM-dd")
            txtHasta.Text = Date.Now.ToString("yyyy-MM-dd")

        End If
    End Sub

    Sub btnBuscar_Click()
        Dim cargar As New cEmpaque
        Dim grilla As New DataSet
        grilla = cargar.cInformeEmpaquetados(txtDesde.Text, txtHasta.Text, "G")
        gvDatos.DataSource = grilla

        gvDatos.DataBind()

    End Sub
    Protected Sub gvDatos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDatos.SelectedIndexChanged
        Dim cargar As New cEmpaque
        Dim grilla As New DataSet
        Dim fila As GridViewRow
        Dim info As String
        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
        ID = CInt(info)

    End Sub

    Sub btnAceptar_Click()

        Dim eliminar As New cEmpaque
        Dim resultado As String = ""
        Dim fila As GridViewRow
        Dim info As String

        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
        ID = CInt(info)
        resultado = eliminar.AnularEmpaquetados(ID)
        gvDatos.DataSource = Nothing

        gvDatos.DataBind()

    End Sub

End Class