Imports MySql.Data.MySqlClient

Public Class conexion
    Public Function conexionBD() As MySqlConnection
        Dim usuario As String = HttpContext.Current.Session("Usuario")
        Dim password As String = HttpContext.Current.Session("password")
        'Dim usuario As String = "Cardus"
        'Dim password As String = "sanguines"
        'Dim ConexionBaseDatos As String = "Database=alianza;Data Source=192.168.100.31;persistsecurityinfo=True;Allow Zero Datetime=True;Convert Zero Datetime=True;User Id='" & usuario & "';Password='" & password & "'"
        Dim ConexionBaseDatos As String = "Database=alianza;Data Source=192.168.0.38;persistsecurityinfo=True;Allow Zero Datetime=True;Convert Zero Datetime=True;User Id='" & usuario & "';Password='" & password & "'"
        Dim conect As MySqlConnection = New MySqlConnection(ConexionBaseDatos)
        Return conect
    End Function
End Class
