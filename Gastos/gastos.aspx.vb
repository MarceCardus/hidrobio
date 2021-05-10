Public Class gastos
    Inherits System.Web.UI.Page
    Private tabla As New DataTable
    Private fila As DataRow
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim cmdTabla As New cGastos
        If Not IsPostBack Then
            cargarRubros()
            txtFecha.Text = Date.Now.ToString("yyyy-MM-dd")
            Session("TablaItem") = cmdTabla.cargarTablaGastos
            lblCodProv.Visible = False
            lblProv.Visible = False

            txtTotal.Text = 0
        End If
    End Sub
    Sub cargarRubros()
        Dim cargar As New cGastos
        Dim grilla As New DataSet
        Dim filtro As String = " WHERE rubroNombre <> 'Combustible' and  rubroNombre <> 'Insumos' "
        grilla = cargar.cgRubro(filtro)
        rblRubros.DataSource = grilla
        rblRubros.DataValueField = "rubroCod"
        rblRubros.DataTextField = "rubroNombre"
        rblRubros.DataBind()
    End Sub

    Sub btnBuscaProv_Click()
        Dim ds As New cProveedor
        Dim er As String
        Try
            gvProveedor.DataSourceID = Nothing
            gvProveedor.DataSource = ds.dsBuscarProveedor(txtProveedor.Text)
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
    Sub btnGuardar_Click()

        'Creacion de variables
        Dim mensaje2 As String = ""
        tabla = Session("TablaItem")
        Dim mensaje1 As String = ""
        Dim FechaVencimiento As String = ""
        Dim cgastos As New cGastos
        Dim i5 As Double = 0
        Dim i10 As Double = 0
        Dim exc As Double
        Dim tiva5 As Double
        exc = CInt(txtE.Text)
        i5 = CInt(txt5.Text)
        i10 = CInt(txt10.Text)
        tiva5 = CInt(txt5.Text / 21)
        Dim tiva10 As Double
        tiva10 = CInt(txt10.Text / 11)
        Dim tf As Double
        tf = CInt(txtTotal.Text)
        Dim tivas As Double = tiva5 + tiva10
        Dim coditem As Integer

        ' manejando los errores con el try
        Try
            'haciendo bucle al gridview

            coditem = cgastos.InsertarGastosCab(txtNroFact.Text, txtFecha.Text, rblTipo.SelectedValue, tf, exc, i5, i10, tiva5, tiva10, lblCodProv.Text, txtObs.Text)
            If IsNumeric(coditem) Then
                If txt10.Text <> 0 Then
                    mensaje1 = cgastos.InsertarGastosDet(coditem, 3, i10, 0, 0, rblRubros.SelectedValue)
                End If
                If txt5.Text <> 0 Then
                    mensaje1 = cgastos.InsertarGastosDet(coditem, 2, 0, i5, 0, rblRubros.SelectedValue)
                End If
                If txtE.Text <> 0 Then
                    mensaje1 = cgastos.InsertarGastosDet(coditem, 1, 0, 0, exc, rblRubros.SelectedValue)
                End If


                'falta el tema de stock
                lblResultado.Text = "Movimiento Guardado"
                lblResultado.Visible = True
                lblCodProv.Text = ""
                lblProv.Text = ""
                txtFecha.Text = ""
                txtNroFact.Text = ""
                txt10.Text = ""
                txt5.Text = ""
                txtE.Text = ""
                txtTotal.Text = ""
                lbl10.Visible = False
                lbl5.Visible = False
                lblTotIva.Visible = False
                txtObs.Text = ""
                txtProveedor.Text = ""
                tabla = Nothing
                Session("Tabla") = Nothing
                Session("Tabla") = cgastos.cargarTablaGastos
                SetFocus(txtNroFact)
                txtFecha.Text = Date.Now.ToString("yyyy-MM-dd")
                SetFocus(txtNroFact)
            Else
                lblResultado.Text = mensaje1
                lblResultado.Visible = True
            End If
        Catch ex As Exception
            lblResultado.Text = ex.Message
            lblResultado.Visible = True
        End Try

    End Sub

    Protected Sub txt10_TextChanged(sender As Object, e As EventArgs) Handles txt10.TextChanged
        sumarTotal()
        totalIVa()
        SetFocus(txt5)
    End Sub

    Protected Sub txt5_TextChanged(sender As Object, e As EventArgs) Handles txt5.TextChanged
        sumarTotal()
        totalIVa()
        SetFocus(txtObs)
    End Sub

    Protected Sub txtE_TextChanged(sender As Object, e As EventArgs) Handles txtE.TextChanged
        sumarTotal()
        SetFocus(txt10)
    End Sub
    Sub sumarTotal()
        Dim total As Integer = 0
        If txt10.Text = "" Then
            txt10.Text = 0
        End If
        If txt5.Text = "" Then
            txt5.Text = 0
        End If
        If txtE.Text = "" Then
            txtE.Text = 0
        End If
        total = CInt(txt10.Text) + CInt(txtE.Text) + CInt(txt5.Text)
        txtTotal.Text = total.ToString("N0")
    End Sub
    Sub totalIVa()
        Dim iva5 As Integer = 0
        Dim iva10 As Integer = 0
        Dim tiva As Integer
        If txt5.Text > 0 Then

            iva5 = txt5.Text / 21
            lbl5.Text = iva5.ToString("N0")
            lbl5.Visible = True

        End If
        If txt10.Text > 0 Then

            iva10 = txt10.Text / 11
            lbl10.Text = iva10.ToString("N0")
            lbl10.Visible = True


        End If
        tiva = iva10 + iva5
        lblTotIva.Text = tiva.ToString("N0")
        lblTotIva.Visible = True
    End Sub
End Class