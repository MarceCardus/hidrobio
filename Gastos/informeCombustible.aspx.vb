Imports MySql.Data.MySqlClient

Public Class informeCombustible
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblResultado.Visible = False
            txtFecha.Text = Date.Now.ToString("yyyy-MM-dd")
            txtHasta.Text = Date.Now.ToString("yyyy-MM-dd")

        End If
    End Sub

    Sub btnBuscar_Click()
        Dim cargar As New cGastos
        Dim ds As New DataSet
        Dim rubros As String = "rubroNombre='Combustible' and "
        Dim estado As String
        If ddlEstado.SelectedValue = "Activos" Then
            estado = "<>'A'"
        Else
            estado = "='" & ddlEstado.SelectedValue & "'"
        End If

        ds = cargar.cReporteGastosNuevo(txtFecha.Text, txtHasta.Text, estado, rubros)
        Dim rds As Microsoft.Reporting.WebForms.ReportDataSource
        Dim dt As New DataTable
        dt = ds.Tables(0)
        dt.Rows(0)("Usuario") = HttpContext.Current.Session("Usuario")
        dt.Rows(0)("Desde") = txtFecha.Text
        dt.Rows(0)("Hasta") = txtHasta.Text

        rds = New Microsoft.Reporting.WebForms.ReportDataSource("dsInfGastos", dt)
        rvCosecha.LocalReport.DataSources.Clear()
        rvCosecha.LocalReport.DataSources.Add(rds)
        rvCosecha.LocalReport.ReportPath = "Informes/infCombustible.rdlc"
        rvCosecha.LocalReport.DisplayName = "informeCombustible" + Now().ToString("dd-MM-yy HH:mm")
        rvCosecha.LocalReport.Refresh()
        rvCosecha.Visible = True
    End Sub

End Class