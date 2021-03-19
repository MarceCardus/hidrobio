Imports MySql.Data.MySqlClient

Public Class informeVentasClientes
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

        Try
            ds = cargar.cInformeVentasFiltro(txtFecha.Text, txtHasta.Text, "clieCod=" & lblclieCod.Text & " And ")
            Dim rds As Microsoft.Reporting.WebForms.ReportDataSource
            Dim dt As New DataTable
            dt = ds.Tables(0)
            dt.Rows(0)("Usuario") = HttpContext.Current.Session("Usuario")
            dt.Rows(0)("Desde") = txtFecha.Text
            dt.Rows(0)("Hasta") = txtHasta.Text

            rds = New Microsoft.Reporting.WebForms.ReportDataSource("dsInfRepartoChofer", dt)
            rvCosecha.LocalReport.DataSources.Clear()
            rvCosecha.LocalReport.DataSources.Add(rds)
            rvCosecha.LocalReport.ReportPath = "Informes/ventasFechaClie.rdlc"
            rvCosecha.LocalReport.DisplayName = "informeVentasClie" + Now().ToString("dd-MM-yy HH:mm")
            rvCosecha.LocalReport.Refresh()
            rvCosecha.Visible = True
        Catch ex As Exception
            lblResultado.Text = "No arrojo ningún resultado"
        End Try

    End Sub
    Sub btnBuscaCl_Click()
        Dim ds As New cClientes
        Dim er As String
        Try
            gvCliente.DataSourceID = Nothing
            gvCliente.DataSource = ds.dsBuscarClientes(txtcliente.Text)
            gvCliente.DataBind()

        Catch ex As Exception
            er = ex.Message

        End Try
    End Sub
    Sub gvCliente_seleccionar()
        Dim cargar As New cClientes
        Dim grilla As New DataSet
        Dim fila As GridViewRow
        Dim info As String
        Dim nombre As String
        fila = gvCliente.SelectedRow
        info = fila.Cells(0).Text
        ID = CInt(info)
        nombre = fila.Cells(2).Text
        lblclieCod.Text = ID
        lblCliente.Text = nombre
        gvCliente.DataSource = Nothing
        gvCliente.DataBind()

    End Sub
End Class