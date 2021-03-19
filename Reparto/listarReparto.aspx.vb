Imports MySql.Data.MySqlClient
Public Class listarReparto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblResultado.Visible = False
            txtFecha.Text = Date.Now.ToString("yyyy-MM-dd")
        End If
    End Sub

    Sub btnBuscar_Click()
        Dim cargar As New cReparto
        Dim ds As New DataSet
        ds = cargar.cInforme(txtFecha.Text)
        Dim rds As Microsoft.Reporting.WebForms.ReportDataSource
        Dim dt As New DataTable
        dt = ds.Tables(0)
        dt.Rows(0)("Usuario") = HttpContext.Current.Session("Usuario")
        dt.Rows(0)("fecha") = txtFecha.Text

        rds = New Microsoft.Reporting.WebForms.ReportDataSource("dsInfRepartoChofer", dt)
        rvCosecha.LocalReport.DataSources.Clear()
        rvCosecha.LocalReport.DataSources.Add(rds)
        rvCosecha.LocalReport.ReportPath = "Reparto/repartoChoferes.rdlc"
        rvCosecha.LocalReport.DisplayName = "Reparto/repartoChoferes" + Now().ToString("dd-MM-yy HH:mm")
        rvCosecha.LocalReport.Refresh()
        rvCosecha.Visible = True
    End Sub




End Class