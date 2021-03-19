Imports MySql.Data.MySqlClient
Public Class anularPago
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblResultado.Visible = False
            txtDesde.Text = Date.Now.ToString("yyyy-MM-dd")
            txtHasta.Text = Date.Now.ToString("yyyy-MM-dd")

        End If
    End Sub

    Sub btnBuscar_Click()
        Dim cargar As New cPagos
        Dim grilla As New DataSet
        grilla = cargar.cPagos(txtDesde.Text, txtHasta.Text)
        gvDatos.DataSource = grilla
        gvDatos.DataBind()

    End Sub



    Sub btnAceptar_Click()

        Dim eliminar As New cPagos
        Dim resultado As String = ""
        Dim fila As GridViewRow
        Dim info As String

        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
        ID = CInt(info)
        Try
            resultado = eliminar.AnularPagos(ID)
            gvDatos.DataSource = Nothing
            gvDatos.DataBind()
            lblResultado.Visible = True
            lblResultado.Text = "Sé anuló correctamente el Pago"
        Catch ex As Exception
            lblResultado.Text = "Hubo problema para anular el Pago"
        End Try




    End Sub

End Class