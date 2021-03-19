Imports MySql.Data.MySqlClient

Public Class informesNC
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblResultado.Visible = False
            txtFecha.Text = Date.Now.ToString("yyyy-MM-dd")
            txtHasta.Text = Date.Now.ToString("yyyy-MM-dd")
        End If
    End Sub

    Sub btnBuscar_Click()
        Try
            Dim cargar As New cVentas
            Dim ds As New DataSet
            ds = cargar.cReporteNC(txtFecha.Text, txtHasta.Text, rblEstado.SelectedValue)
            Dim rds As Microsoft.Reporting.WebForms.ReportDataSource
            Dim dt As New DataTable
            dt = ds.Tables(0)
            dt.Rows(0)("Usuario") = HttpContext.Current.Session("Usuario")
            dt.Rows(0)("Desde") = txtFecha.Text
            dt.Rows(0)("Hasta") = txtHasta.Text

            rds = New Microsoft.Reporting.WebForms.ReportDataSource("dsInfNC", dt)
            rvCosecha.LocalReport.DataSources.Clear()
            rvCosecha.LocalReport.DataSources.Add(rds)
            rvCosecha.LocalReport.ReportPath = "Informes/NCFecha.rdlc"
            rvCosecha.LocalReport.DisplayName = "informeNC" + Now().ToString("dd-MM-yy HH:mm")
            rvCosecha.LocalReport.Refresh()
            rvCosecha.Visible = True
        Catch ex As Exception
            lblResultado.Text = "No existen datos para la consulta"
            lblResultado.Visible = True


        End Try

    End Sub

End Class