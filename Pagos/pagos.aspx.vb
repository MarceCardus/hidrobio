Public Class pagos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtFecha.Text = Date.Now.ToString("yyyy-MM-dd")
            txtFechaCheque.Text = Date.Now.ToString("yyyy-MM-dd")
            lblCod.Text = ""
            lblMonto.Text = ""
            lblTipo.Text = ""
        End If
    End Sub
    Sub BuscarOP()
        Dim ds As New cPagos
        Dim er As String
        Try
            gvOp.DataSourceID = Nothing
            gvOp.DataSource = ds.buscarOP(txtOP.Text)
            gvOp.DataBind()

        Catch ex As Exception
            er = ex.Message

        End Try
    End Sub

    Sub gvOp_seleccionar()
        Dim cargar As New cClientes
        Dim grilla As New DataSet
        Dim fila As GridViewRow
        Dim info As String
        Dim tipo As String
        Dim total As Double

        fila = gvOp.SelectedRow
        info = fila.Cells(0).Text
        ID = CInt(info)
        tipo = fila.Cells(1).Text
        total = CInt(fila.Cells(2).Text)

        lblCod.Text = ID
        lblTipo.Text = tipo
        lblMonto.Text = total

        gvOp.DataSource = Nothing
        gvOp.DataBind()

    End Sub
    Sub btnGuardar_Click()
        Dim insertar As New cPagos
        Dim resultado As String = ""
        Dim fechaVenc As String
        fechaVenc = txtFecha.Text
        If txtNroRecibo.Text = "" Then
            txtNroRecibo.Text = 0
        End If
        If txtNroCheque.Text = "" Then
            txtNroCheque.Text = 0
        End If
        resultado = insertar.InsertarPagos(lblCod.Text, txtFecha.Text, ddlTipo.SelectedValue, txtNroCheque.Text,
                                          txtFechaCheque.Text, txtNroRecibo.Text, ddlBanco.SelectedValue)

        If resultado = "ok" Then
                lblResultado.Text = "Se guardaron Correctamente los datos"
                lblResultado.Visible = True


            lblCod.Text = ""
            lblMonto.Text = ""
            lblTipo.Text = ""
            txtNroCheque.Text = ""
            txtNroRecibo.Text = ""

            SetFocus(txtOP)
            txtFecha.Text = Date.Now.ToString("yyyy-MM-dd")
            txtFechaCheque.Text = Date.Now.ToString("yyyy-MM-dd")
            gvOp.DataSource = Nothing

            gvOp.DataBind()
        Else
                lblResultado.Text = "No se guardaron los datos. Hubo un problema en la Carga."
                lblResultado.Visible = True
            End If

    End Sub
End Class