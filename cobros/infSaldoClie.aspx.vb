Imports MySql.Data.MySqlClient
Public Class infSaldoClie
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblResultado.Visible = False
            txtFecha.Text = Date.Now.ToString("yyyy-MM-dd")
            txtHasta.Text = Date.Now.ToString("yyyy-MM-dd")
        End If
    End Sub

    Sub btnBuscar_Click()
        Dim cargar As New cCobros
        Dim ds As New DataSet
        ds = cargar.cInformeSaldosCliente(txtFecha.Text, txtHasta.Text, lblclieCod.Text)
        Dim rds As Microsoft.Reporting.WebForms.ReportDataSource
        Dim dt As New DataTable
        Try
            dt = ds.Tables(0)
            dt.Rows(0)("Usuario") = HttpContext.Current.Session("Usuario")
            dt.Rows(0)("Desde") = txtFecha.Text
            dt.Rows(0)("Hasta") = txtHasta.Text

            rds = New Microsoft.Reporting.WebForms.ReportDataSource("dsInfSaldos", dt)
            rvCosecha.LocalReport.DataSources.Clear()
            rvCosecha.LocalReport.DataSources.Add(rds)
            rvCosecha.LocalReport.ReportPath = "Informes/saldoFechaCliente.rdlc"
            rvCosecha.LocalReport.DisplayName = "informesaldos" + Now().ToString("dd-MM-yy HH:mm")
            rvCosecha.LocalReport.Refresh()
            rvCosecha.Visible = True
        Catch ex As Exception
            lblResultado.Visible = True
            lblResultado.Text = "No existe datos en ese rango de fecha"
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