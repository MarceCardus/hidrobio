Imports MySql.Data.MySqlClient
Public Class AnularGastos
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
        Dim grilla As New DataSet
        grilla = cargar.cInformeGastosAnular(txtDesde.Text, txtHasta.Text, lblProvCod.Text)
        gvDatos.DataSource = grilla
        gvDatos.DataBind()
        gvDetalle.DataSource = Nothing
        gvDetalle.DataBind()

        lblProvCod.Text = ""
        lblProv.Text = ""
    End Sub
    Protected Sub gvDatos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDatos.SelectedIndexChanged
        Dim cargar As New cGastos
        Dim grilla As New DataSet
        Dim fila As GridViewRow
        Dim info As String
        fila = gvDatos.SelectedRow
        Try
            info = fila.Cells(0).Text
            ID = CInt(info)
            grilla = cargar.cGrillaDetGastos(ID)
            gvDetalle.DataSource = grilla
            gvDetalle.DataBind()
        Catch ex As Exception

        End Try

    End Sub

   Sub btnBuscaProv_Click()
        Dim ds As New cProveedor
        Dim er As String
        Try
            gvProveedor.DataSourceID = Nothing
            gvProveedor.DataSource = ds.dsBuscarProveedor(txtProv.Text)
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
        lblProvCod.Text = ID
        lblProv.Text = nombre
        gvProveedor.DataSource = Nothing
        gvProveedor.DataBind()
        lblProvCod.Visible = True
        lblProv.Visible = True
    End Sub
    Sub btnAceptar_Click()
        If txtMotivo.Text = "" Then
            lblResultado.Text = "Debe cargar el motivo de Anulación"
            lblResultado.Visible = True
        Else

            Dim eliminar As New cGastos
            Dim resultado As String = ""
            Dim fila As GridViewRow
            Dim info As String

            fila = gvDatos.SelectedRow
            info = fila.Cells(0).Text
            ID = CInt(info)
            resultado = eliminar.AnularGastos(ID, txtMotivo.Text)
            gvDatos.DataSource = Nothing
            gvDetalle.DataSource = Nothing
            gvDatos.DataBind()
            gvDetalle.DataBind()
            gvProveedor.DataSource = Nothing
            gvDetalle.DataBind()
            lblResultado.Visible = False
            txtProv.Text = ""
        End If

    End Sub


End Class