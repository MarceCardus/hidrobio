Imports MySql.Data.MySqlClient
Public Class cobros
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load


        If Not IsPostBack Then
            txtFecha.Text = Date.Now.ToString("yyyy-MM-dd")
            txtFechaCobro.Text = Date.Now.ToString("yyyy-MM-dd")
            CargarDDLbancos()
        End If
    End Sub

    Sub cargarCobros(ByVal ventaCod As Integer)
        Dim ds As New cCobros
        Dim er As String
        Try
            gvCobros.DataSourceID = Nothing
            gvCobros.DataSource = ds.dsBuscarCobros(ventaCod)
            gvCobros.DataBind()
            gvCobros.Visible = True

        Catch ex As Exception
            er = ex.Message

        End Try


    End Sub



    Sub TraerFacturas()
        Dim fila As GridViewRow
        Dim info As String
        Dim id As Integer
        Dim modificar As New cCobros
        Dim leer As MySqlDataReader

        fila = gvVentas.SelectedRow
        info = fila.Cells(0).Text
        id = CInt(info)


        leer = modificar.clVentas(id)
        If leer.Read Then
            txtVenta.Text = leer.Item("ventaNroFact").ToString
            lblFechaFact.Text = leer.Item("ventaFchFact").ToString
            lblCliente.Text = leer.Item("clieNombre").ToString
            lblTotalFact.Text = FormatNumber(leer.Item("ventaTotal").ToString, 0)
            lblTipopago.Text = leer.Item("ventaTipo").ToString
            lblCodVenta.Text = leer.Item("ventaCod").ToString
            txtVenta.Text = leer.Item("ventaNroFact").ToString
            If leer.Item("ventaTipo").ToString = "Contado" Then
                txtRecibo.Enabled = False
            End If

            If leer.Item("ventaReparto").ToString = "N" Then
                lblReparto.Text = "Depósito"
            Else
                lblReparto.Text = "Reparto"
            End If
            lblSaldo.Text = FormatNumber(leer.Item("ventaSaldo").ToString, 0)
            txtMonto.Text = FormatNumber(leer.Item("ventaSaldo").ToString, 0)
            lblTotalIVa.Text = FormatNumber(leer.Item("ventaTiva").ToString, 0)

            If leer.Item("ventaEstado").ToString = "G" Then
                lblEstado.Text = "Generado"
            ElseIf leer.Item("ventaEstado").ToString = "A" Then
                lblEstado.Text = "Anulado"
            Else
                lblEstado.Text = "Pagado"
            End If

            gvVentas.DataSource = Nothing
            gvVentas.DataBind()


            SetFocus(ddlTipo)
            cargarCobros(lblCodVenta.Text)

        End If
    End Sub
    Sub CargarDDLbancos()
        Dim cargar As New cCobros
        Dim grilla As New DataSet
        grilla = cargar.cDatasetBancos("bancos WHERE bancod > 0 order by banNombre", "bancos")
        ddlBanco.DataSource = grilla
        ddlBanco.DataTextField = "banNombre"
        ddlBanco.DataValueField = "banCod"
        ddlBanco.DataBind()


    End Sub

    Dim banco As Integer
    Protected Sub ddlTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipo.SelectedIndexChanged

        If ddlTipo.SelectedValue = 1 Or ddlTipo.SelectedValue = 5 Then
            ddlBanco.Enabled = False
            txtCheque.Enabled = False
            txtFecha.Enabled = False
            SetFocus(txtMonto)

        Else
            ddlBanco.Enabled = True
            txtCheque.Enabled = True
            txtFecha.Enabled = True
            SetFocus(ddlBanco)
            banco = ddlBanco.SelectedValue
        End If
    End Sub
    Sub btnGuardar_Click()
        Dim insertar As New cCobros
        Dim resultado As String = ""
        Dim saldo As Double
        Dim estado As String
        Dim fechaVenc As String
        fechaVenc = txtFecha.Text
        saldo = CDbl(lblSaldo.Text) - CDbl(txtMonto.Text)
        If txtRecibo.Text = "" Then
            txtRecibo.Text = 0
        End If
        If txtCheque.Text = "" Then
            txtCheque.Text = 0
        End If
        If saldo < 0 Then
            lblResultado.Text = "El valor no puede ser mayor al saldo"

        Else
            If saldo > 0 Then
                estado = "G"
            Else
                estado = "P"

            End If
            If ddlBanco.Enabled = True Then
                banco = ddlBanco.SelectedValue
                resultado = insertar.Insertar(lblCodVenta.Text, txtFechaCobro.Text, ddlTipo.SelectedValue, txtCheque.Text, fechaVenc, saldo, txtRecibo.Text,
           estado, banco, txtMonto.Text)
            Else
                resultado = insertar.InsertarContado(lblCodVenta.Text, txtFechaCobro.Text, ddlTipo.SelectedValue, saldo, estado, txtMonto.Text, txtRecibo.Text)
            End If

            If resultado = "ok" Then
                lblResultado.Text = "Se guardaron Correctamente los datos"
                lblResultado.Visible = True
                cargarCobros(lblCodVenta.Text)
                txtVenta.Text = ""
                lblFechaFact.Text = ""
                lblCliente.Text = ""
                lblTotalFact.Text = ""
                lblTipopago.Text = ""
                lblCodVenta.Text = ""
                lblReparto.Text = ""
                lblSaldo.Text = ""
                lblTotalIVa.Text = ""
                lblEstado.Text = ""
                txtMonto.Text = ""
                txtRecibo.Text = ""
                SetFocus(txtVenta)
                txtFecha.Text = Date.Now.ToString("yyyy-MM-dd")
                txtFechaCobro.Text = Date.Now.ToString("yyyy-MM-dd")
                gvVentas.DataSource = Nothing

                gvVentas.DataBind()
            Else
                lblResultado.Text = "No se guardaron los datos. Hubo un problema en la Carga."
                lblResultado.Visible = True
            End If
        End If


    End Sub




    Sub btnBuscaClientes()
        Dim ds As New cClientes
        Dim er As String
        Try
            gvBuscar.DataSourceID = Nothing
            gvBuscar.DataSource = ds.dsBuscarClientes(txtClie.Text)
            gvBuscar.DataBind()

        Catch ex As Exception
            er = ex.Message

        End Try
    End Sub
    Protected Sub gvBuscar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvBuscar.SelectedIndexChanged
        Try
            Dim cargar As New cClientes
            Dim cobros As New cCobros
            Dim grilla As New DataSet
            Dim fila As GridViewRow
            Dim info As String
            Dim nombre As String
            fila = gvBuscar.SelectedRow
            info = fila.Cells(0).Text
            ID = CInt(info)
            nombre = fila.Cells(2).Text
            gvVentas.DataSourceID = Nothing
            gvVentas.DataSource = cobros.dsBuscarClientes(ID)
            gvVentas.DataBind()
            gvVentas.Visible = True
            lblResultado.Visible = False
        Catch ex As Exception
            lblResultado.Text = "No existe datos de la consulta"
            lblResultado.Visible = True
        End Try

    End Sub
End Class