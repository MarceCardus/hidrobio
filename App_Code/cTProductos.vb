Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Imports System.Data
Public Class cTProductos
    Private conn As New conexion
    Private conexion As MySqlConnection = conn.conexionBD
    Public Function InsertarTProd(ByVal nombre As String) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spITProducto"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@tipo", MySqlDbType.VarChar).Value = nombre

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
    Public Function ActualizarTProd(ByVal id As Integer, ByVal nombre As String) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spUTProducto"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@Cod", MySqlDbType.Int32).Value = id
        cmdInsert.Parameters.Add("@tipo", MySqlDbType.VarChar).Value = nombre

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
    Public Function cgTProd() As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vTProd", conexion)
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
            sql = "select * from vTProd where tipoProdCod = '" & id & "'"
            Comandos = New MySqlCommand(sql, conectar.conexionBD)
            Comandos.Connection.Open()

            resultado = Comandos.ExecuteReader

        Catch ex As Exception

        End Try

        Return resultado
        Comandos.Connection.Close()
    End Function
    Public Function eliminarTProd(ByVal id As Integer) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spDTProducto"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@Cod", MySqlDbType.Int32).Value = id

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

    Public Function cgSTProd() As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vstprod", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "SubTipo")
        Return dsRubros
    End Function
    Public Function traerSTProd(ByVal cod As Integer) As DataSet
        'Dim daRubros As New MySqlDataAdapter("select * from vproductos where stpCod=" & cod & " and prodStock>0", conexion)
        Dim daRubros As New MySqlDataAdapter("select * from vproductos where stpCod=" & cod & "", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "SubTipo")
        Return dsRubros
    End Function
End Class
