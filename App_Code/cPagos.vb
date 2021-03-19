Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Imports System.Data
Public Class cPagos
    Private conn As New conexion
    Private conexion As MySqlConnection = conn.conexionBD
    Function cargarTablaPagos() As DataTable
        Dim tablaGastos As New DataTable
        tablaGastos = New System.Data.DataTable("TablaOP")
        tablaGastos.Columns.Add("Codigo", GetType(String))
        tablaGastos.Columns.Add("NroFactura", GetType(String))
        tablaGastos.Columns.Add("provCod", GetType(String))
        tablaGastos.Columns.Add("Proveedor", GetType(String))
        tablaGastos.Columns.Add("Monto", GetType(String))
        tablaGastos.Columns.Add("tipo", GetType(String))

        Return tablaGastos
    End Function
    Public Function InsertarOPCab(ByVal Fecha As Date, ByVal aut As String, ByVal elaborador As String,
                               ByVal total As Double) As String
        Dim resultado As Integer
        Dim codOut As MySqlParameter
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spIOpCab"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@Fecha", MySqlDbType.Date).Value = Fecha
        cmdInsert.Parameters.Add("@aut", MySqlDbType.VarChar).Value = aut
        cmdInsert.Parameters.Add("@elaborador", MySqlDbType.VarChar).Value = elaborador
        cmdInsert.Parameters.Add("@usuario", MySqlDbType.Int32).Value = HttpContext.Current.Session("accCod")
        cmdInsert.Parameters.Add("@total", MySqlDbType.Double).Value = total

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
    Public Function InsertarDetOPCompras(ByVal cod As Integer, ByVal opTipo As String, ByVal comprasCod As Integer) As String
        Dim resultado As String
        Dim cmdInsert As New MySqlCommand

        cmdInsert.CommandText = "spIOpDetCompras"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@opCod", MySqlDbType.Int32).Value = cod
        cmdInsert.Parameters.Add("@opTipo", MySqlDbType.String).Value = opTipo
        cmdInsert.Parameters.Add("@comprasCod", MySqlDbType.Decimal).Value = comprasCod


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
    Public Function InsertarDetOPGastos(ByVal cod As Integer, ByVal opTipo As String, ByVal gastosCod As Integer) As String
        Dim resultado As String
        Dim cmdInsert As New MySqlCommand

        cmdInsert.CommandText = "spIOpDetGastos"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@opCod", MySqlDbType.Int32).Value = cod
        cmdInsert.Parameters.Add("@opTipo", MySqlDbType.String).Value = opTipo
        cmdInsert.Parameters.Add("@gastosCod", MySqlDbType.Decimal).Value = gastosCod


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
    Public Function cReporteOP(ByVal cod As Integer) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vOP where opCod=" & cod & " and  opEstado = 'G' order by opFecha ", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "OP")
        dsRubros.Tables("OP").Columns.Add("Usuario")

        Return dsRubros
    End Function
    Public Function buscarOP(ByVal nroOP As String) As DataSet
        Dim daop As MySqlDataAdapter
        If nroOP = "" Then
            daop = New MySqlDataAdapter("select * from vop where opEstado='G' order by opCod ", conexion)

        Else
            daop = New MySqlDataAdapter("select * from vop where opEstado='G' AND opCod = " & nroOP & " LIMIT 1 ", conexion)

        End If

        Dim dsOP As New DataSet()

        dsOP.Clear()
        daop.Fill(dsOP, "op")
        Return dsOP
    End Function
    Public Function InsertarPagos(ByVal opCod As Integer, ByVal fecha As Date, ByVal tipoPago As Integer, ByVal chqnro As String,
                           ByVal chqfch As Date, ByVal reciboNro As Integer, ByVal cc As Integer) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spInsertarPago"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@op", MySqlDbType.Int32).Value = opCod
        cmdInsert.Parameters.Add("@fecha", MySqlDbType.Date).Value = fecha
        cmdInsert.Parameters.Add("@tipoP", MySqlDbType.Int32).Value = tipoPago
        cmdInsert.Parameters.Add("@chqnro", MySqlDbType.VarChar).Value = chqnro
        cmdInsert.Parameters.Add("@chqfch", MySqlDbType.Date).Value = chqfch
        cmdInsert.Parameters.Add("@recibo", MySqlDbType.Int32).Value = reciboNro
        cmdInsert.Parameters.Add("@usuario", MySqlDbType.Int32).Value = HttpContext.Current.Session("accCod")
        cmdInsert.Parameters.Add("@cc", MySqlDbType.Int32).Value = cc
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
    Public Function cPagos(ByVal desde As String, ByVal hasta As String) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vpagos where pagEstado  <> 'A' and pagFecha BETWEEN '" & desde & "' AND '" & hasta & "' order by pagCod ", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "op")
        dsRubros.Tables("op").Columns.Add("Usuario")
        dsRubros.Tables("op").Columns.Add("Desde")
        dsRubros.Tables("op").Columns.Add("Hasta")
        Return dsRubros
    End Function
    Public Function AnularPagos(ByVal id As Integer) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spuPagos"
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
    Public Function cReportePago(ByVal desde As String, ByVal hasta As String, ByVal estado As String) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vInformePagos where pagEstado  " & estado & " and pagFecha BETWEEN '" & desde & "' AND '" & hasta & "' order by pagCod ", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "Pagos")
        dsRubros.Tables("Pagos").Columns.Add("Usuario")
        dsRubros.Tables("Pagos").Columns.Add("Desde")
        dsRubros.Tables("Pagos").Columns.Add("Hasta")
        Return dsRubros
    End Function
End Class
