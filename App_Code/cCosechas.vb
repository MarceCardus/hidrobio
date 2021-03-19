Imports Microsoft.VisualBasic
Imports System.Data
Imports MySql.Data.MySqlClient


Public Class cCosechas
    Private conn As New conexion
    Private conexion As MySqlConnection = conn.conexionBD
    Private tabla As New DataTable
    Function cargarTablaCosechas() As DataTable
        tabla = New System.Data.DataTable("Tabla")
        tabla.Columns.Add("Linea", GetType(String))
        tabla.Columns.Add("Codigo", GetType(String))
        tabla.Columns.Add("Producto", GetType(String))
        tabla.Columns.Add("Cantidad", GetType(String))
        Return tabla
    End Function
    Public Function InsertarCab(ByVal prov As Integer, ByVal fecha As Date, ByVal personal As Int32, ByVal movil As Integer) As String
        Dim resultado As Integer
        Dim codOut As MySqlParameter

        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spICosechas"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@prov", MySqlDbType.Int32).Value = prov
        cmdInsert.Parameters.Add("@fecha", MySqlDbType.Date).Value = fecha
        cmdInsert.Parameters.Add("@usuario", MySqlDbType.Int32).Value = HttpContext.Current.Session("accCod")
        cmdInsert.Parameters.Add("@personal", MySqlDbType.Int32).Value = personal
        cmdInsert.Parameters.Add("@movil", MySqlDbType.Int32).Value = movil
        codOut = cmdInsert.Parameters.Add("@cod", MySqlDbType.Int32)
        codOut.Direction = ParameterDirection.Output

        Try

            conexion.Open()
            cmdInsert.ExecuteNonQuery()

            resultado = cmdInsert.Parameters("@cod").Value
            conexion.Close()

        Catch ex As Exception
            conexion.Close()
            resultado = ex.Message
        End Try

        Return resultado
    End Function
    Public Function InsertarDet(ByVal cod As Integer, ByVal producto As Int32, ByVal cant As Decimal) As String
        Dim resultado As String
        Dim cmdInsert As New MySqlCommand

        cmdInsert.CommandText = "spICosechasDet"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@cod", MySqlDbType.Int32).Value = cod
        cmdInsert.Parameters.Add("@producto", MySqlDbType.Int32).Value = producto
        cmdInsert.Parameters.Add("@cant", MySqlDbType.Decimal).Value = cant


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

    Public Function ComandosLectura(ByVal desde As String, ByVal hasta As String) As MySqlDataReader
        Dim sql As String
        Dim conectar As New conexion
        Dim Comandos As MySqlCommand
        Dim resultado As MySqlDataReader = Nothing

        Try
            'Traer el Id, Nombre y Contraseña del usuario
            sql = "select * from vCosecha_Det where producEstado='A' and producFecha BETWEEN '" & desde & "' AND '" & hasta & "'"
            Comandos = New MySqlCommand(sql, conectar.conexionBD)
            Comandos.Connection.Open()

            resultado = Comandos.ExecuteReader

        Catch ex As Exception

        End Try

        Return resultado
        Comandos.Connection.Close()
    End Function
    Public Function cGrilla(ByVal desde As String, ByVal hasta As String) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vCosecha where producEstado='A' and producFecha BETWEEN '" & desde & "' AND '" & hasta & "'", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "Tipo")
        Return dsRubros
    End Function
    Public Function cGrillaDet(ByVal cod As Int32) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vCosecha_Det where  producCod='" & cod & "'", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "Tipo")
        Return dsRubros
    End Function

    Public Function Anular(ByVal id As Integer) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spAnularCosecha"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@Cod", MySqlDbType.Int32).Value = id
        cmdInsert.Parameters.Add("@usuario", MySqlDbType.Int32).Value = HttpContext.Current.Session("accCod")
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
    Public Function cInforme(ByVal desde As String, ByVal hasta As String) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vTraerCosechas where producFecha BETWEEN '" & desde & "' AND '" & hasta & "'", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "Cosechas")
        dsRubros.Tables("Cosechas").Columns.Add("Usuario")
        dsRubros.Tables("Cosechas").Columns.Add("Desde")
        dsRubros.Tables("Cosechas").Columns.Add("Hasta")
        Return dsRubros
    End Function
End Class
