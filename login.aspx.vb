Public Class login
    Inherits System.Web.UI.Page


    Protected Sub btnIngresar_Click(sender As Object, e As EventArgs) Handles btnIngresar.Click
        Dim Rol As String


        Dim loguearse As New acceso
        Rol = loguearse.Login(txtUsuario.Text, txtPass.Text)
        If Rol = "ok" Then
            If Request.Browser("IsMobileDevice") = True Then
                Response.Redirect("~/Gastos/combusMob.aspx")
            Else
                Response.Redirect("default.aspx")
            End If

        Else
            lblLogin.Text = "Error al tratar de acceder"
            lblLogin.Visible = True

        End If


    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblLogin.Visible = False

        End If
    End Sub


End Class