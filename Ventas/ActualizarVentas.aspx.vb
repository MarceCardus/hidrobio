Imports MySql.Data.MySqlClient
Public Class ActualizarVentas
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
        grilla = cargar.cInformeVentasActualizar(txtDesde.Text, txtHasta.Text)
        gvDatos.DataSource = grilla
        gvDatos.DataBind()

    End Sub

    Protected Sub gvDatos_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvDatos.RowEditing
        gvDatos.EditIndex = e.NewEditIndex
        btnBuscar_Click()

        gvDatos.DataBind()

    End Sub

    Protected Sub gvDatos_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvDatos.RowCancelingEdit
        gvDatos.EditIndex = -1
        btnBuscar_Click()
        gvDatos.DataBind()
    End Sub

    Protected Sub gvDatos_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvDatos.RowUpdating
        Dim txtNFac As TextBox
        Dim dllPagadado As DropDownList
        Dim lblCod As TextBox
        Dim insertar As New cCobros
        Dim actualizar As New cVentas
        Dim resultado As String
        Dim nroFactura As String
        Dim lblTotal As TextBox
        Dim txtEstado As TextBox
        txtNFac = gvDatos.Rows(e.RowIndex).FindControl("txtNroFact")
        dllPagadado = gvDatos.Rows(e.RowIndex).FindControl("ddlPagadoE")
        lblCod = gvDatos.Rows(e.RowIndex).FindControl("txtventaCod")
        lblTotal = gvDatos.Rows(e.RowIndex).FindControl("txtTotal")
        txtEstado = gvDatos.Rows(e.RowIndex).FindControl("txtEstado")
        nroFactura = txtNFac.Text
        resultado = actualizar.ActualizarVentasFact(lblCod.Text, Date.Now.ToString("yyyy-MM-dd"), nroFactura)
        If dllPagadado.SelectedValue = "Si" Then
            If txtEstado.Text = "G" Then
                resultado = insertar.InsertarContado(lblCod.Text, Date.Now.ToString("yyyy-MM-dd"), 1, 0, "P", lblTotal.Text, 0)
                lblResultado.Text = ""
            Else

                lblResultado.Text = "Ya tiene pago"
                lblResultado.Visible = True
            End If


        End If

        btnBuscar_Click()
        gvDatos.EditIndex = -1
        gvDatos.DataBind()
    End Sub
End Class