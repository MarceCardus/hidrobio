Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Imports System.Data
Public Class cPersonales
    Private conn As New conexion
    Private conexion As MySqlConnection = conn.conexionBD
    Public Function Insertar(ByVal nombre As String, ByVal ci As String, ByVal telef As String, ByVal chofer As String, ByVal funcion As String,
                             ByVal estado As String, ByVal tercer As String, ByVal nac As String) As String

        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spIPersonales"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@nombre", MySqlDbType.VarChar).Value = nombre
        cmdInsert.Parameters.Add("@CI", MySqlDbType.VarChar).Value = ci
        cmdInsert.Parameters.Add("@telef", MySqlDbType.VarChar).Value = telef
        cmdInsert.Parameters.Add("@chofer", MySqlDbType.VarChar).Value = chofer
        cmdInsert.Parameters.Add("@funcion", MySqlDbType.VarChar).Value = funcion
        cmdInsert.Parameters.Add("@estado", MySqlDbType.VarChar).Value = estado
        cmdInsert.Parameters.Add("@tercer", MySqlDbType.VarChar).Value = tercer
        cmdInsert.Parameters.Add("@nacimiento", MySqlDbType.Date).Value = nac
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
    Public Function Actualizar(ByVal id As Integer, ByVal nombre As String, ByVal ci As String, ByVal telef As String, ByVal chofer As String, ByVal funcion As String,
                             ByVal estado As String, ByVal tercer As String, ByVal nac As Date) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spUPersonales"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@cod", MySqlDbType.Int32).Value = id
        cmdInsert.Parameters.Add("@nombre", MySqlDbType.VarChar).Value = nombre
        cmdInsert.Parameters.Add("@ci", MySqlDbType.VarChar).Value = ci
        cmdInsert.Parameters.Add("@telef", MySqlDbType.VarChar).Value = telef
        cmdInsert.Parameters.Add("@chofer", MySqlDbType.VarChar).Value = chofer
        cmdInsert.Parameters.Add("@funcion", MySqlDbType.VarChar).Value = funcion
        cmdInsert.Parameters.Add("@estado", MySqlDbType.VarChar).Value = estado
        cmdInsert.Parameters.Add("@tercer", MySqlDbType.VarChar).Value = tercer
        cmdInsert.Parameters.Add("@nacimiento", MySqlDbType.Date).Value = nac
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
        Dim daRubros As New MySqlDataAdapter("select * from vPersonales", conexion)
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
            sql = "select * from vPersonales where perCod = '" & id & "'"
            Comandos = New MySqlCommand(sql, conectar.conexionBD)
            Comandos.Connection.Open()

            resultado = Comandos.ExecuteReader

        Catch ex As Exception

        End Try

        Return resultado
        Comandos.Connection.Close()
    End Function
End Class
