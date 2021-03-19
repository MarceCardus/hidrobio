Imports MySql.Data.MySqlClient
Public Class informeVentasAnuladas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblResultado.Visible = False
            txtFecha.Text = Date.Now.ToString("yyyy-MM-dd")
            txtHasta.Text = Date.Now.ToString("yyyy-MM-dd")
        End If
    End Sub

    Sub btnBuscar_Click()
        Dim cargar As New cVentas
        Dim ds As New DataSet
        ds = cargar.cInformeVentasAnulados(txtFecha.Text, txtHasta.Text)
        Dim rds As Microsoft.Reporting.WebForms.ReportDataSource
        Dim dt As New DataTable
        dt = ds.Tables(0)
        dt.Rows(0)("Usuario") = HttpContext.Current.Session("Usuario")
        dt.Rows(0)("Desde") = txtFecha.Text
        dt.Rows(0)("Hasta") = txtHasta.Text

        rds = New Microsoft.Reporting.WebForms.ReportDataSource("dsInfRepartoChofer", dt)
        rvCosecha.LocalReport.DataSources.Clear()
        rvCosecha.LocalReport.DataSources.Add(rds)
        rvCosecha.LocalReport.ReportPath = "Informes/ventasAnul.rdlc"
        rvCosecha.LocalReport.DisplayName = "Ventas/informeVentasAnuladas" + Now().ToString("dd-MM-yy HH:mm")
        rvCosecha.LocalReport.Refresh()
        rvCosecha.Visible = True
    End Sub

End Class