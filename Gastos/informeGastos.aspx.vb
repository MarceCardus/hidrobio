Imports MySql.Data.MySqlClient

Public Class informeGastos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblResultado.Visible = False
            txtDesde.Text = Date.Now.ToString("yyyy-MM-dd")
            txtHasta.Text = Date.Now.ToString("yyyy-MM-dd")
            CargarDDLRubro()
        End If
    End Sub

    Sub btnBuscar_Click()
        Dim cargar As New cGastos
        Dim ds As New DataSet
        Dim rubros As String
        rubros = ddlRubros.SelectedValue
        Dim estado As String
        If ddlEstado.SelectedValue = "Activos" Then
            estado = "<>'A'"
        Else
            estado = "='" & ddlEstado.SelectedValue & "'"
        End If
        If rubros = "" Then

        Else
            rubros = "rubroCod=" & rubros & " and "
        End If
        ds = cargar.cReporteGastosNuevo(txtDesde.Text, txtHasta.Text, estado, rubros)
        Dim rds As Microsoft.Reporting.WebForms.ReportDataSource
        Dim dt As New DataTable
        dt = ds.Tables(0)
        dt.Rows(0)("Usuario") = HttpContext.Current.Session("Usuario")
        dt.Rows(0)("Desde") = txtDesde.Text
        dt.Rows(0)("Hasta") = txtHasta.Text

        rds = New Microsoft.Reporting.WebForms.ReportDataSource("dsInfGastos", dt)
        rvCosecha.LocalReport.DataSources.Clear()
        rvCosecha.LocalReport.DataSources.Add(rds)
        rvCosecha.LocalReport.ReportPath = "Informes/infgastos.rdlc"
        rvCosecha.LocalReport.DisplayName = "informeGastos" + Now().ToString("dd-MM-yy HH:mm")
        rvCosecha.LocalReport.Refresh()
        rvCosecha.Visible = True
    End Sub
    Sub CargarDDLRubro()
        Dim cargar As New cProductos
        Dim grilla As New DataSet
        grilla = cargar.cDataset("vRubros", "Rubros")
        ddlRubros.DataSource = grilla

        ddlRubros.DataTextField = "rubroNombre"
        ddlRubros.DataValueField = "rubroCod"
        ddlRubros.DataBind()
        ddlRubros.Items.Insert(0, New ListItem("Todos", ""))
    End Sub
End Class