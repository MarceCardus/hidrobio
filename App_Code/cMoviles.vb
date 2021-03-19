Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Imports System.Data
Public Class cMoviles
    Private conn As New conexion
    Private conexion As MySqlConnection = conn.conexionBD
    Public Function Insertar(ByVal Marca As String, ByVal Modelo As String, ByVal Anho As String, ByVal chapa As String, ByVal refri As String,
                             ByVal estado As String, ByVal tercer As String) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spIMoviles"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@Marca", MySqlDbType.VarChar).Value = Marca
        cmdInsert.Parameters.Add("@Modelo", MySqlDbType.VarChar).Value = Modelo
        cmdInsert.Parameters.Add("@Anho", MySqlDbType.Year).Value = Anho
        cmdInsert.Parameters.Add("@chapa", MySqlDbType.VarChar).Value = chapa
        cmdInsert.Parameters.Add("@refri", MySqlDbType.VarChar).Value = refri
        cmdInsert.Parameters.Add("@estado", MySqlDbType.VarChar).Value = estado
        cmdInsert.Parameters.Add("@tercer", MySqlDbType.VarChar).Value = tercer
        Try
            conexion.Open()
            cmdInsert.ExecuteNonQuery()
            conexion.Close()
            resultado = "ok"
        Catch ex As Exception
            conexion.Close()
            resultado = ex.Message
        End Try

        Return resultado
    End Function
    Public Function Actualizar(ByVal id As Integer, ByVal Marca As String, ByVal Modelo As String, ByVal Anho As String, ByVal chapa As String, ByVal refri As String,
                             ByVal estado As String, ByVal tercer As String) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spUMoviles"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@Cod", MySqlDbType.Int32).Value = id
        cmdInsert.Parameters.Add("@Marca", MySqlDbType.VarChar).Value = Marca
        cmdInsert.Parameters.Add("@Modelo", MySqlDbType.VarChar).Value = Modelo
        cmdInsert.Parameters.Add("@Anho", MySqlDbType.Year).Value = Anho
        cmdInsert.Parameters.Add("@chapa", MySqlDbType.VarChar).Value = chapa
        cmdInsert.Parameters.Add("@refri", MySqlDbType.VarChar).Value = refri
        cmdInsert.Parameters.Add("@estado", MySqlDbType.VarChar).Value = estado
        cmdInsert.Parameters.Add("@tercer", MySqlDbType.VarChar).Value = tercer
        Try
            conexion.Open()
            cmdInsert.ExecuteNonQuery()
            conexion.Close()
            resultado = "ok"
        Catch ex As Exception
            conexion.Close()
            resultado = ex.Message
        End Try

        Return resultado
    End Function
    Public Function cGrilla() As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vMoviles", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "Tipo")
        Return dsRubros
    End Function

    Public Function ComandosLectura(ByVal id As Int16) As MySqlDataReader
        Dim sql As String
        Dim conectar As New conexion
        Dim Comandos As MySqlCommand
        Dim resultado As MySqlDataReader = Nothing

        Try
            'Traer el Id, Nombre y Contraseña del usuario
            sql = "select * from vMoviles where movCod = '" & id & "'"
            Comandos = New MySqlCommand(sql, conectar.conexionBD)
            Comandos.Connection.Open()

            resultado = Comandos.ExecuteReader

        Catch ex As Exception

        End Try

        Return resultado
        Comandos.Connection.Close()
    End Function

End Class
