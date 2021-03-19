Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Imports System.Data
Public Class cProveedor
    Private conn As New conexion
    Private conexion As MySqlConnection = conn.conexionBD
    Public Function Insertar(ByVal nombre As String, ByVal acopio As String, ByVal ruc As String, ByVal responsable As String, ByVal telef As String, ByVal coord As String) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spIProveedores"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@nombre", MySqlDbType.VarChar).Value = nombre
        cmdInsert.Parameters.Add("@acopio", MySqlDbType.VarChar).Value = acopio
        cmdInsert.Parameters.Add("@ruc", MySqlDbType.VarChar).Value = ruc
        cmdInsert.Parameters.Add("@responsable", MySqlDbType.VarChar).Value = responsable
        cmdInsert.Parameters.Add("@telef", MySqlDbType.VarChar).Value = telef
        cmdInsert.Parameters.Add("@coor", MySqlDbType.VarChar).Value = coord

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
    Public Function Actualizar(ByVal id As Integer, ByVal nombre As String, ByVal acopio As String, ByVal ruc As String, ByVal responsable As String, ByVal telef As String, ByVal coord As String) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spUProveedores"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@Cod", MySqlDbType.Int32).Value = id
        cmdInsert.Parameters.Add("@nombre", MySqlDbType.VarChar).Value = nombre
        cmdInsert.Parameters.Add("@acopio", MySqlDbType.VarChar).Value = acopio
        cmdInsert.Parameters.Add("@ruc", MySqlDbType.VarChar).Value = ruc
        cmdInsert.Parameters.Add("@responsable", MySqlDbType.VarChar).Value = responsable
        cmdInsert.Parameters.Add("@telef", MySqlDbType.VarChar).Value = telef
        cmdInsert.Parameters.Add("@coor", MySqlDbType.VarChar).Value = coord
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
        Dim daRubros As New MySqlDataAdapter("select * from vproveedores", conexion)
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
            sql = "select * from vproveedores where provCod = '" & id & "'"
            Comandos = New MySqlCommand(sql, conectar.conexionBD)
            Comandos.Connection.Open()

            resultado = Comandos.ExecuteReader

        Catch ex As Exception

        End Try

        Return resultado
        Comandos.Connection.Close()
    End Function
    Public Function dsBuscarProveedor(ByVal filtro As String) As DataSet

        ' crear un data adapter 

        Dim sql As String = "SELECT DISTINCT * FROM vproveedores where (cast(provCod as char) like '%" & filtro & "%'" _
                            & " or cast(provNombre as char) like '%" & filtro & "%' or cast(provRuc as char) like '%" & filtro & "%')"
        '   & " order by lista_precio "

        Dim da As MySqlDataAdapter = New MySqlDataAdapter(sql, conexion)
        ' crear un  dataset 
        Dim ds As DataSet = New DataSet
        ' fill dataset 
        da.Fill(ds, "Proveedores")
        Return ds
        conexion.Close()
    End Function
End Class
