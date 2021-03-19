Imports MySql.Data.MySqlClient
Public Class informeEmpaquetados
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblResultado.Visible = False
            txtFecha.Text = Date.Now.ToString("yyyy-MM-dd")
            txtHasta.Text = Date.Now.ToString("yyyy-MM-dd")
        End If
    End Sub

    Sub btnBuscar_Click()
        Dim cargar As New cEmpaque
        Dim ds As New DataSet
        ds = cargar.cInformeEmpaquetados(txtFecha.Text, txtHasta.Text, rblEstado.SelectedValue)
        Dim rds As Microsoft.Reporting.WebForms.ReportDataSource
        Dim dt As New DataTable
        dt = ds.Tables(0)
        dt.Rows(0)("Usuario") = HttpContext.Current.Session("Usuario")
        dt.Rows(0)("Desde") = txtFecha.Text
        dt.Rows(0)("Hasta") = txtHasta.Text

        rds = New Microsoft.Reporting.WebForms.ReportDataSource("dsInfEmpaquetados", dt)
        rvCosecha.LocalReport.DataSources.Clear()
        rvCosecha.LocalReport.DataSources.Add(rds)
        rvCosecha.LocalReport.ReportPath = "Informes/empaFecha.rdlc"
        rvCosecha.LocalReport.DisplayName = "informeEmpaquetados" + Now().ToString("dd-MM-yy HH:mm")
        rvCosecha.LocalReport.Refresh()
        rvCosecha.Visible = True
    End Sub




End Class