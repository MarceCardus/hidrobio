Imports MySql.Data.MySqlClient
Public Class AnularVentas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblResultado.Visible = False
            txtDesde.Text = Date.Now.ToString("yyyy-MM-dd")
            txtHasta.Text = Date.Now.ToString("yyyy-MM-dd")

        End If
    End Sub
    Sub btnBuscar_Click()
        Dim cargar As New cVentas
        Dim grilla As New DataSet
        grilla = cargar.cInformeVentasAnular(txtDesde.Text, txtHasta.Text, lblclieCod.Text)
        gvDatos.DataSource = grilla
        gvDatos.DataBind()
        gvDetalle.DataSource = Nothing
        gvDetalle.DataBind()

        lblclieCod.Text = ""
        lblCliente.Text = ""
    End Sub
    Protected Sub gvDatos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDatos.SelectedIndexChanged
        Dim cargar As New cVentas
        Dim grilla As New DataSet
        Dim fila As GridViewRow
        Dim info As String
        fila = gvDatos.SelectedRow
        Try
            info = fila.Cells(0).Text
            ID = CInt(info)
            grilla = cargar.cGrillaDet(ID)
            gvDetalle.DataSource = grilla
            gvDetalle.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Sub btnCliente_Click()
        Dim ds As New cClientes
        Dim er As String
        Try
            gvCliente.DataSourceID = Nothing
            gvCliente.DataSource = ds.dsBuscarClientes(txtcliente.Text)
            gvCliente.DataBind()
            gvCliente.Visible = True
        Catch ex As Exception
            er = ex.Message

        End Try
    End Sub
    Sub gvCliente_SelectedIndexChanged()
        Dim fila As GridViewRow
        Dim cod As String
        Dim desc As String

        fila = gvCliente.SelectedRow
        cod = fila.Cells(0).Text
        desc = fila.Cells(2).Text
        lblclieCod.Text = cod
        lblCliente.Text = desc
        lblclieCod.Visible = True
        lblCliente.Visible = True
        gvCliente.Visible = False
    End Sub
    Sub btnAceptar_Click()
        If txtMotivo.Text = "" Then
            lblResultado.Text = "Debe cargar el motivo de Anulación"
            lblResultado.Visible = True
        Else

            Dim eliminar As New cVentas
            Dim resultado As String = ""
            Dim fila As GridViewRow
            Dim info As String

            fila = gvDatos.SelectedRow
            info = fila.Cells(0).Text
            ID = CInt(info)
            resultado = eliminar.AnularVentas(ID, txtMotivo.Text)
            gvDatos.DataSource = Nothing
            gvDetalle.DataSource = Nothing
            gvDatos.DataBind()
            gvDetalle.DataBind()
            gvCliente.DataSource = Nothing
            gvDetalle.DataBind()
            lblResultado.Visible = False
            txtcliente.Text = ""
        End If

    End Sub


End Class