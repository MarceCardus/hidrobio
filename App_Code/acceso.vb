Imports MySql.Data.MySqlClient
Public Class acceso
    Public Function Login(ByVal Usuario As String, ByVal password As String) As String
        Dim sql As String
        Dim DRUsuario As MySqlDataReader
        Dim conectar As New conexion
        Dim Comandos As MySqlCommand
        HttpContext.Current.Session("Usuario") = Usuario
        HttpContext.Current.Session("password") = password
        Dim resultado As String
        Try
            'Traer el Id, Nombre y Contraseña del usuario
            sql = "Select accCod,accNombre from acceso " _
             & " where accNombre = '" & Usuario & "'"
            Comandos = New MySqlCommand(sql, conectar.conexionBD)
            Comandos.Connection.Open()

            DRUsuario = Comandos.ExecuteReader

            If DRUsuario.Read Then

                HttpContext.Current.Session("Usuario") = Usuario
                HttpContext.Current.Session("password") = password
                HttpContext.Current.Session("accCod") = DRUsuario.Item("accCod").ToString()
            End If
            resultado = "ok"
        Catch ex As Exception
            resultado = ex.Message
        End Try

        Return resultado
        Comandos.Connection.Close()
    End Function
End Class
