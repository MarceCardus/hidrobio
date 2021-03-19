Imports MySql.Data.MySqlClient
Public Class reparto
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim reparto As Date
            reparto = Date.Now.ToString("yyyy-MM-dd")
            lblResultado.Visible = False
            txtDesde.Text = Date.Now.ToString("yyyy-MM-dd")
            txtHasta.Text = Date.Now.ToString("yyyy-MM-dd")
            txtFchReparto.Text = reparto.AddDays(1).ToString("yyyy-MM-dd")

        End If
    End Sub

    Sub btnBuscar_Click()
        Dim cargar As New cVentas
        Dim grilla As New DataSet
        grilla = cargar.cGrillaRepartoCab(txtDesde.Text, txtHasta.Text)
        gvDatos.DataSource = grilla
        gvDatos.DataBind()
    End Sub
    Protected Sub gvDatos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDatos.SelectedIndexChanged
        Dim cargar As New cVentas
        Dim grilla As New DataSet
        Dim fila As GridViewRow
        Dim info As String
        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
        ID = CInt(info)
        grilla = cargar.cGrillaRepartoDet(ID)
        gvDetalle.DataSource = grilla
        gvDetalle.DataBind()
    End Sub
    Sub CargarDDLChofer(ByVal ddlChofer As DropDownList)
        Dim cargar As New cProductos
        Dim grilla As New DataSet
        grilla = cargar.cDataset("vpersonales where perChofer='Si' and perEstado='Si'", "Chofer")
        ddlChofer.DataSource = grilla
        ddlChofer.DataTextField = "perNombre"
        ddlChofer.DataValueField = "perCod"
        ddlChofer.DataBind()

    End Sub
    Protected Sub gvDatos_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvDatos.RowDataBound

        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim cargar As New cProductos
                Dim ds As New DataSet
                Dim ddlChofer As DropDownList
                ddlChofer = DirectCast(e.Row.FindControl("ddlChoferes"), DropDownList)
                ddlChofer.ClearSelection()

                If ddlChofer IsNot Nothing Then

                    If ddlChofer IsNot DBNull.Value Then
                        CargarDDLChofer(ddlChofer)
                    End If

                End If

            End If
        Catch ex As Exception

            Throw ex

        Finally
        End Try



    End Sub

    Sub btnruteo_Click()
        Dim ventaCod As Integer = 0
        Dim perCod As Integer = 0
        Dim fechaReparto As Date
        Dim insertar As New cReparto
        Dim resultado As String = ""
        Dim orden As String

        For Each row As GridViewRow In gvDatos.Rows
            Dim ordenGv As TextBox = CType(row.FindControl("txtOdern"), TextBox)
            Dim chofer As DropDownList = CType(row.FindControl("ddlChoferes"), DropDownList)
            ventaCod = row.Cells(0).Text
            perCod = chofer.SelectedValue
            fechaReparto = txtFchReparto.Text
            orden = ordenGv.Text
            resultado = insertar.InsertarReparto(ventaCod, perCod, fechaReparto, orden)
        Next



        If resultado = "ok" Then
            lblResultado.Text = "Se guardaron Correctamente los datos"
            lblResultado.Visible = True
            gvDatos.DataSource = Nothing
            gvDatos.DataBind()
        Else
            lblResultado.Text = "No se guardaron los datos. Hubo un problema en la Carga."
            lblResultado.Visible = True
        End If
    End Sub

End Class