Imports MySql.Data.MySqlClient

Public Class imprimirOp
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
        dt = ds.Tables(0)
        dt.Rows(0)("Usuario") = HttpContext.Current.Session("Usuario")
        dt.Rows(0)("Desde") = txtDesde.Text
        dt.Rows(0)("Hasta") = txtHasta.Text

        rds = New Microsoft.Reporting.WebForms.ReportDataSource("dsInfOp", dt)

    End Sub
    'Protected Sub gvDetalle_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvDetalle.RowCommand

    '    If e.CommandName = "verDetalle" Then
    '        Dim row As GridViewRow = CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)
    '        Dim id As String = CType(row.Cells(0), DataControlFieldCell).Text
    '        If rblTipo.SelectedValue = "Acopio" Then
    '            Dim cargar As New cCompras
    '            Dim ds As New DataSet
    '            ds = cargar.trarCompras_OP(CInt(id))
    '            gvDet.DataSource = ds
    '            gvDet.DataBind()
    '        ElseIf rblTipo.SelectedValue = "Gastos" Then
    '            Dim cargarGastos As New cGastos
    '            Dim dsgastos As New DataSet
    '            Try

    '                dsgastos = cargarGastos.cGrillaDetGastos_OP(CInt(id))
    '                gvDet.DataSource = dsgastos
    '                gvDet.DataBind()
    '            Catch ex As Exception

    '            End Try


    '        End If
    '    End If
    'End Sub

End Class