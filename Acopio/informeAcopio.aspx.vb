Imports MySql.Data.MySqlClient
Public Class informeAcopio
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblResultado.Visible = False
            txtDesde.Text = Date.Now.ToString("yyyy-MM-dd")
            txthasta.Text = Date.Now.ToString("yyyy-MM-dd")
        End If
    End Sub

    Sub btnBuscar_Click()
        Dim cargar As New cCompras
        Dim ds As New DataSet
        ds = cargar.cInforme(txtDesde.Text, txthasta.Text)
        'gvDatos.DataSource = grilla
        'gvDatos.DataBind()



        Dim rds As Microsoft.Reporting.WebForms.ReportDataSource
        Dim dt As New DataTable
        Try
            dt = ds.Tables(0)
            dt.Rows(0)("Usuario") = HttpContext.Current.Session("Usuario")
            dt.Rows(0)("Desde") = txtDesde.Text
            dt.Rows(0)("Hasta") = txthasta.Text
            rds = New Microsoft.Reporting.WebForms.ReportDataSource("dsAcopio", dt)
            rvCosecha.LocalReport.DataSources.Clear()
            rvCosecha.LocalReport.DataSources.Add(rds)
            rvCosecha.LocalReport.ReportPath = "Informes/rptAcopio.rdlc"
            rvCosecha.LocalReport.DisplayName = "informeacopio" + Now().ToString("dd-MM-yy HH:mm")
            rvCosecha.LocalReport.Refresh()
            rvCosecha.Visible = True
            lblResultado.Visible = False
        Catch ex As Exception
            lblResultado.Visible = True
            lblResultado.Text = "No existe datos en ese rango de fecha"
        End Try

    End Sub



End Class