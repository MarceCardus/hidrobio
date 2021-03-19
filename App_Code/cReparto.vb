Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Imports System.Data
Public Class cReparto
    Private conn As New conexion
    Private conexion As MySqlConnection = conn.conexionBD
    Public Function InsertarReparto(ByVal ventaCod As Integer, ByVal perCod As Integer, ByVal reparFecha As Date, ByVal reparOrden As Integer) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spIReparto"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@ventaCod", MySqlDbType.Int32).Value = ventaCod
        cmdInsert.Parameters.Add("@perCod", MySqlDbType.Int32).Value = perCod
        cmdInsert.Parameters.Add("@reparFecha", MySqlDbType.Date).Value = reparFecha
        cmdInsert.Parameters.Add("@reparOrden", MySqlDbType.Int32).Value = reparOrden

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
    Public Function cInforme(ByVal fecha As String) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vRepartoAsignado where reparAnulado='N' and reparFecha = '" & fecha & "' order by reparOrden", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "reparto")
        dsRubros.Tables("reparto").Columns.Add("Usuario")
        dsRubros.Tables("reparto").Columns.Add("fecha")
        dsRubros.Tables("reparto").Columns.Add("codBarra")

        Return dsRubros
    End Function
    Public Function cGVReparto(ByVal desde As String, ByVal hasta As String) As DataSet
        Dim daRubros As MySqlDataAdapter

        daRubros = New MySqlDataAdapter("select  * from vrepartoventacab where reparAnulado='N' and reparFecha BETWEEN '" & desde & "' AND '" & hasta & "' order by reparCod ", conexion)


        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "reparto")
        dsRubros.Tables("reparto").Columns.Add("Usuario")
        dsRubros.Tables("reparto").Columns.Add("Desde")
        dsRubros.Tables("reparto").Columns.Add("Hasta")
        Return dsRubros
    End Function
    Public Function AnularReparto(ByVal id As Integer) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spRepartoAnular"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@cod", MySqlDbType.Int32).Value = id
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


End Class
