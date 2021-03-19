Imports MySql.Data.MySqlClient
Public Class informeMermas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblResultado.Visible = False
            txtDesde.Text = Date.Now.ToString("yyyy-MM-dd")
            txthasta.Text = Date.Now.ToString("yyyy-MM-dd")
        End If
    End Sub

    Sub btnBuscar_Click()
        Dim cargar As New cVentas
        Dim ds As New DataSet
        ds = cargar.cInformeMermas(txtDesde.Text, txthasta.Text, rblEstado.SelectedValue)
        'gvDatos.DataSource = grilla
        'gvDatos.DataBind()



        Dim rds As Microsoft.Reporting.WebForms.ReportDataSource
        Dim dt As New DataTable
        dt = ds.Tables(0)
        dt.Rows(0)("Usuario") = HttpContext.Current.Session("Usuario")
        dt.Rows(0)("Desde") = txtDesde.Text
        dt.Rows(0)("Hasta") = txthasta.Text
        rds = New Microsoft.Reporting.WebForms.ReportDataSource("dsMermas", dt)
        rvCosecha.LocalReport.DataSources.Clear()
        rvCosecha.LocalReport.DataSources.Add(rds)
        rvCosecha.LocalReport.ReportPath = "Informes/mermasFecha.rdlc"
        rvCosecha.LocalReport.DisplayName = "informemermas" + Now().ToString("dd-MM-yy HH:mm")
        rvCosecha.LocalReport.Refresh()
        rvCosecha.Visible = True
    End Sub



End Class