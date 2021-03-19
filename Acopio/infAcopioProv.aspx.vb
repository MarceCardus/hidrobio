Imports MySql.Data.MySqlClient
Public Class infAcopioProv
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblResultado.Visible = False
            txtDesde.Text = Date.Now.ToString("yyyy-MM-dd")
            txthasta.Text = Date.Now.ToString("yyyy-MM-dd")
            lblCodProv.Visible = False
            lblProv.Visible = False
        End If
    End Sub



    Sub btnBuscar_Click()
        Dim cargar As New cCompras
        Dim ds As New DataSet
        ds = cargar.cInformeComprasFiltro(txtDesde.Text, txthasta.Text, "provCod=" & lblCodProv.Text & " And ")
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
            rvCosecha.LocalReport.DisplayName = "informeacopioProv" + Now().ToString("dd-MM-yy HH:mm")
            rvCosecha.LocalReport.Refresh()
            rvCosecha.Visible = True
            lblResultado.Visible = False
        Catch ex As Exception
            lblResultado.Visible = True
            lblResultado.Text = "No existe datos en ese rango de fecha"
        End Try
    End Sub
    Sub btnBuscaCl_Click()
        Dim ds As New cProveedor
        Dim er As String
        Try
            gvProveedor.DataSourceID = Nothing
            gvProveedor.DataSource = ds.dsBuscarProveedor(txtprov.Text)
            gvProveedor.DataBind()

        Catch ex As Exception
            er = ex.Message

        End Try
    End Sub
    Sub gvProveedor_seleccionar()
        Dim cargar As New cClientes
        Dim grilla As New DataSet
        Dim fila As GridViewRow
        Dim info As String
        Dim nombre As String
        fila = gvProveedor.SelectedRow
        info = fila.Cells(0).Text
        ID = CInt(info)
        nombre = fila.Cells(2).Text
        lblCodProv.Text = ID
        lblProv.Text = nombre
        gvProveedor.DataSource = Nothing
        gvProveedor.DataBind()
        lblCodProv.Visible = True
        lblProv.Visible = True
    End Sub
End Class