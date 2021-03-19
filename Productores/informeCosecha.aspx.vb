Imports MySql.Data.MySqlClient
Public Class informeCosecha
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblResultado.Visible = False
            txtDesde.Text = Date.Now.ToString("yyyy-MM-dd")
            txthasta.Text = Date.Now.ToString("yyyy-MM-dd")
        End If
    End Sub

    Sub btnBuscar_Click()
        Dim cargar As New cCosechas
        Dim ds As New DataSet
        ds = cargar.cInforme(txtDesde.Text, txthasta.Text)
        'gvDatos.DataSource = grilla
        'gvDatos.DataBind()



        Dim rds As Microsoft.Reporting.WebForms.ReportDataSource
        Dim dt As New DataTable
        dt = ds.Tables(0)
        dt.Rows(0)("Usuario") = HttpContext.Current.Session("Usuario")
        dt.Rows(0)("Desde") = txtDesde.Text
        dt.Rows(0)("Hasta") = txthasta.Text
        rds = New Microsoft.Reporting.WebForms.ReportDataSource("dsICosecha", dt)
        rvCosecha.LocalReport.DataSources.Clear()
        rvCosecha.LocalReport.DataSources.Add(rds)
        rvCosecha.LocalReport.ReportPath = "Informes/rptCosechas.rdlc"
        rvCosecha.LocalReport.DisplayName = "informeCosechas" + Now().ToString("dd-MM-yy HH:mm")
        rvCosecha.LocalReport.Refresh()
        rvCosecha.Visible = True
    End Sub




End Class