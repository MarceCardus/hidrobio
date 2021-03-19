Imports MySql.Data.MySqlClient
Imports MySql.Data
Imports System.Data

Public Class cClientes
    Private conn As New conexion
    Private conexion As MySqlConnection = conn.conexionBD
    Public Function Insertar(ByVal ruc As String, ByVal nombre As String, ByVal responsable As String, ByVal telef As String,
                             ByVal mail As String, ByVal coord As String, ByVal direccion As String, ByVal barrios As Integer,
                             ByVal nroCasa As String) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spIClientes"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@ruc", MySqlDbType.VarChar).Value = ruc
        cmdInsert.Parameters.Add("@nombre", MySqlDbType.VarChar).Value = nombre
        cmdInsert.Parameters.Add("@responsable", MySqlDbType.VarChar).Value = responsable
        cmdInsert.Parameters.Add("@telef", MySqlDbType.VarChar).Value = telef
        cmdInsert.Parameters.Add("@mail", MySqlDbType.VarChar).Value = mail
        cmdInsert.Parameters.Add("@coor", MySqlDbType.VarChar).Value = coord
        cmdInsert.Parameters.Add("@direccion", MySqlDbType.VarChar).Value = direccion
        cmdInsert.Parameters.Add("@barrios", MySqlDbType.Int32).Value = barrios
        cmdInsert.Parameters.Add("@nroCasa", MySqlDbType.VarChar).Value = nroCasa

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
    Public Function Actualizar(ByVal id As Integer, ByVal ruc As String, ByVal nombre As String, ByVal responsable As String, ByVal telef As String,
                               ByVal mail As String, ByVal direccion As String, ByVal barrios As Integer,
                             ByVal nroCasa As String) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spUClientes"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@Cod", MySqlDbType.Int32).Value = id
        cmdInsert.Parameters.Add("@ruc", MySqlDbType.VarChar).Value = ruc
        cmdInsert.Parameters.Add("@nombre", MySqlDbType.VarChar).Value = nombre
        cmdInsert.Parameters.Add("@responsable", MySqlDbType.VarChar).Value = responsable
        cmdInsert.Parameters.Add("@telef", MySqlDbType.VarChar).Value = telef
        cmdInsert.Parameters.Add("@mail", MySqlDbType.VarChar).Value = mail

        cmdInsert.Parameters.Add("@direccion", MySqlDbType.VarChar).Value = direccion
        cmdInsert.Parameters.Add("@barrios", MySqlDbType.Int32).Value = barrios
        cmdInsert.Parameters.Add("@nroCasa", MySqlDbType.VarChar).Value = nroCasa
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
        Dim daRubros As New MySqlDataAdapter("select * from vClientesBarrios", conexion)
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
            sql = "select * from vClientesBarrios where clieCod = '" & id & "'"
            Comandos = New MySqlCommand(sql, conectar.conexionBD)
            Comandos.Connection.Open()

            resultado = Comandos.ExecuteReader

        Catch ex As Exception

        End Try

        Return resultado
        Comandos.Connection.Close()
    End Function
    Public Function dsBuscarClientes(ByVal filtro As String) As DataSet

        ' crear un data adapter 

        Dim sql As String = "SELECT DISTINCT * FROM vClientes where (cast(clieCod as char) like '%" & filtro & "%'" _
                            & " or cast(clieNombre as char) like '%" & filtro & "%' or cast(clieRuc as char) like '%" & filtro & "%')"
        '   & " order by lista_precio "

        Dim da As MySqlDataAdapter = New MySqlDataAdapter(sql, conexion)
        ' crear un  dataset 
        Dim ds As DataSet = New DataSet
        ' fill dataset 
        da.Fill(ds, "Clientes")
        Return ds
        conexion.Close()
    End Function
    Public Function cbsClie() As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vClientes", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "Clientes")
        Return dsRubros
    End Function
End Class
