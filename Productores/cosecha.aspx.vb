Imports MySql.Data.MySqlClient
Public Class cosecha
    Inherits System.Web.UI.Page

    Dim tabla As New DataTable
    Dim fila As DataRow

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim cmdTabla As New cCosechas
            lblResultado.Visible = False

            CargarDDLProductor()
            CargarDDLChofer()
            CargarDDLMovil()

            Session("Tabla") = cmdTabla.cargarTablaCosechas
            txtfecha.Text = Date.Now.ToString("yyyy-MM-dd")
            cargarSubTipo()
        End If
    End Sub
    Sub CargarDDLProductor()
        Dim cargar As New cProductos
        Dim grilla As New DataSet
        grilla = cargar.cDataset("vproveedores where provAcopio='NO'", "Productor")
        ddlProductor.DataSource = grilla
        ddlProductor.DataTextField = "provNombre"
        ddlProductor.DataValueField = "provCod"
        ddlProductor.DataBind()


    End Sub
    Sub CargarDDLChofer()
        Dim cargar As New cProductos
        Dim grilla As New DataSet
        grilla = cargar.cDataset("vpersonales where perChofer='Si' and perEstado='Si'", "Chofer")
        ddlChofer.DataSource = grilla
        ddlChofer.DataTextField = "perNombre"
        ddlChofer.DataValueField = "perCod"
        ddlChofer.DataBind()

    End Sub
    Sub CargarDDLMovil()
        Dim cargar As New cProductos
        Dim grilla As New DataSet
        grilla = cargar.cDataset("vTraerMovil", "Movil")
        ddlMovil.DataSource = grilla
        ddlMovil.DataTextField = "movil"
        ddlMovil.DataValueField = "movCod"
        ddlMovil.DataBind()

    End Sub


    Protected Sub gvDatos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDatos.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim ctrlEliminar As ImageButton = CType(e.Row.FindControl("Eliminar"), ImageButton)
                ctrlEliminar.OnClientClick = "return confirm('¿Esta seguro de eliminar este registro?');"
        End Select
    End Sub

    Protected Sub gvDatos_RowDataBound_delete(sender As Object, e As GridViewDeleteEventArgs) Handles gvDatos.RowDeleting
        Dim x As Integer = e.RowIndex
        tabla = Session("Tabla")
        tabla.Rows(x).Delete()
        Session("Tabla") = tabla
        gvDatos.DataSource = tabla
        gvDatos.DataBind()
    End Sub
    Sub btnGuardar_Click()

        Dim i As Integer
        'Creacion de variables
        Dim mensaje2 As String = ""
        Dim codprod As Integer
        tabla = Session("Tabla")
        Dim mensaje1 As String = ""
        Dim FechaVencimiento As String = ""
        Dim cCosecha As New cCosechas
        'manejando los errores con el try
        Dim txt As TextBox
        Try
            'haciendo bucle al gridview

            codprod = cCosecha.InsertarCab(ddlProductor.SelectedValue, txtfecha.Text, ddlChofer.SelectedValue, ddlMovil.SelectedValue)
            If IsNumeric(codprod) Then

                For i = 0 To gvDatos.Rows.Count - 1


                    fila = tabla.Rows(i)
                    txt = gvDatos.Rows(i).FindControl("txtCantidades2")
                    mensaje1 = cCosecha.InsertarDet(codprod, fila("Codigo"), CDec(txt.Text))

                Next

                'falta el tema de stock
                lblResultado.Text = "Movimiento Guardado"
                lblResultado.Visible = True

                txtfecha.Text = ""
                gvDatos.DataSource = Nothing
                gvDatos.DataBind()
                tabla = Nothing
                Session("Tabla") = Nothing
                Session("Tabla") = cCosecha.cargarTablaCosechas

                txtfecha.Text = Date.Now.ToString("yyyy-MM-dd")
            Else
                lblResultado.Text = mensaje1
                lblResultado.Visible = True
            End If
        Catch ex As Exception
            lblResultado.Text = ex.Message
            lblResultado.Visible = True
        End Try

    End Sub
    Sub cargarSubTipo()
        Dim cargar As New cTProductos
        Dim grilla As New DataSet
        grilla = cargar.cgSTProd()
        rblSt.DataSource = grilla
        rblSt.DataValueField = "stpCod"
        rblSt.DataTextField = "stpDesc"
        rblSt.DataBind()

    End Sub
    Sub cargarVerduras()
        Dim cargar As New cTProductos
        Dim grilla As New DataSet
        grilla = cargar.traerSTProd(rblSt.SelectedValue)
        rblVerduras.DataSource = grilla
        rblVerduras.DataValueField = "prodCod"
        rblVerduras.DataTextField = "prodNombre"
        rblVerduras.DataBind()

    End Sub

    Protected Sub rblSt_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblSt.SelectedIndexChanged
        cargarVerduras()

    End Sub



    Protected Sub rblVerduras_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblVerduras.SelectedIndexChanged
        '
        Dim prodCod As Integer
        prodCod = rblVerduras.SelectedValue
        Dim nrofila As Integer

        Dim txtCant As TextBox


        tabla = Session("Tabla")
        fila = tabla.NewRow
        If gvDatos.Rows.Count = 0 Then

            fila("Linea") = 0
            fila("Codigo") = prodCod
            fila("Producto") = rblVerduras.SelectedItem
            fila("Cantidad") = 1

        Else
            For i = 0 To gvDatos.Rows.Count - 1
                txtCant = gvDatos.Rows(i).FindControl("txtCantidades2")
                tabla.Rows(i)("Cantidad") = txtCant.Text
            Next

            nrofila = tabla.Rows.Count
            fila("Codigo") = prodCod
            fila("Producto") = rblVerduras.SelectedItem
            fila("Cantidad") = 1


        End If
        fila("Linea") = nrofila + 1




        tabla.Rows.Add(fila)
        gvDatos.DataSource = tabla
        gvDatos.DataBind()
        Session("Tabla") = tabla



    End Sub

End Class