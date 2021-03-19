Imports MySql.Data.MySqlClient

Public Class informeOp
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
        Dim ds As New DataSet
        Dim estado As String
        If ddlEstado.SelectedValue = "Activos" Then
            estado = "<>'A'"
        Else
            estado = "='" & ddlEstado.SelectedValue & "'"
        End If

        ds = cargar.cReporteOp(txtDesde.Text, txtHasta.Text, estado)
        Dim rds As Microsoft.Reporting.WebForms.ReportDataSource
        Dim dt As New DataTable
        Try
            dt = ds.Tables(0)
            dt.Rows(0)("Usuario") = HttpContext.Current.Session("Usuario")
            dt.Rows(0)("Desde") = txtDesde.Text
            dt.Rows(0)("Hasta") = txtHasta.Text

            rds = New Microsoft.Reporting.WebForms.ReportDataSource("dsInfOp", dt)
            rvCosecha.LocalReport.DataSources.Clear()
            rvCosecha.LocalReport.DataSources.Add(rds)
            rvCosecha.LocalReport.ReportPath = "informes/infOp.rdlc"
            rvCosecha.LocalReport.DisplayName = "informeOp" + Now().ToString("dd-MM-yy HH:mm")
            rvCosecha.LocalReport.Refresh()
            rvCosecha.Visible = True

            lblResultado.Visible = False
        Catch ex As Exception
            lblResultado.Visible = True
            lblResultado.Text = "No existe datos en ese rango de fecha"

        End Try

    End Sub

End Class