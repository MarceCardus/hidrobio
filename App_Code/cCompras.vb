Imports Microsoft.VisualBasic

Imports System.Data
Imports MySql.Data.MySqlClient

Public Class cCompras
    Private conn As New conexion
    Private conexion As MySqlConnection = conn.conexionBD
    Private tabla As New DataTable
    Function cargarTablaCompras() As DataTable
        tabla = New System.Data.DataTable("Tabla")
        tabla.Columns.Add("Linea", GetType(String))
        tabla.Columns.Add("Codigo", GetType(String))
        tabla.Columns.Add("Producto", GetType(String))
        tabla.Columns.Add("Cantidad", GetType(String))
        tabla.Columns.Add("Unitario", GetType(String))
        tabla.Columns.Add("Impuesto", GetType(String))
        tabla.Columns.Add("Excenta", GetType(String))
        tabla.Columns.Add("5", GetType(String))
        tabla.Columns.Add("10", GetType(String))
        tabla.Columns.Add("TotalExcenta", GetType(String))
        tabla.Columns.Add("Total5", GetType(String))
        tabla.Columns.Add("Total10", GetType(String))
        tabla.Columns.Add("TotalFact", GetType(String))
        tabla.Columns.Add("TotalIva", GetType(String))
        Return tabla
    End Function
    Public Function InsertarCab(ByVal prov As Integer, ByVal fecha As Date, ByVal fact As String, ByVal personal As Int32,
                                ByVal movil As Integer, ByVal excenta As Double, ByVal iva5 As Double, ByVal iva10 As Double,
                                ByVal total As Double, ByVal tiva5 As Double, ByVal tiva10 As Double, ByVal tivas As Double) As String
        Dim resultado As Integer
        Dim codOut As MySqlParameter
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spICompras"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@prov", MySqlDbType.Int32).Value = prov
        cmdInsert.Parameters.Add("@fecha", MySqlDbType.Date).Value = fecha
        cmdInsert.Parameters.Add("@fact", MySqlDbType.VarChar).Value = fact
        cmdInsert.Parameters.Add("@Usuario", MySqlDbType.Int32).Value = HttpContext.Current.Session("accCod")
        cmdInsert.Parameters.Add("@personal", MySqlDbType.Int32).Value = personal
        cmdInsert.Parameters.Add("@movil", MySqlDbType.Int32).Value = movil
        cmdInsert.Parameters.Add("@excenta", MySqlDbType.Double).Value = excenta
        cmdInsert.Parameters.Add("@iva5", MySqlDbType.Double).Value = iva5
        cmdInsert.Parameters.Add("@iva10", MySqlDbType.Double).Value = iva10
        cmdInsert.Parameters.Add("@total", MySqlDbType.Double).Value = total
        cmdInsert.Parameters.Add("@tiva5", MySqlDbType.Double).Value = tiva5
        cmdInsert.Parameters.Add("@tiva10", MySqlDbType.Double).Value = tiva10
        cmdInsert.Parameters.Add("@tivas", MySqlDbType.Double).Value = tivas
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
    Public Function InsertarDet(ByVal cod As Integer, ByVal producto As Int32, ByVal cant As Decimal, ByVal excenta As Double,
                               ByVal al5 As Double, ByVal al10 As Double) As String
        Dim resultado As String
        Dim cmdInsert As New MySqlCommand

        cmdInsert.CommandText = "spIComprasDet"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@comprasCod", MySqlDbType.Int32).Value = cod
        cmdInsert.Parameters.Add("@prodCod", MySqlDbType.Int32).Value = producto
        cmdInsert.Parameters.Add("@cantidad", MySqlDbType.Decimal).Value = cant
        cmdInsert.Parameters.Add("@excenta", MySqlDbType.Double).Value = excenta
        cmdInsert.Parameters.Add("@al5", MySqlDbType.Double).Value = al5
        cmdInsert.Parameters.Add("@al10", MySqlDbType.Double).Value = al10

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

    Public Function cGrilla(ByVal desde As String, ByVal hasta As String) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vacopiocab where comprasEstado='G' and comprasFecha BETWEEN '" & desde & "' AND '" & hasta & "'", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "Tipo")
        Return dsRubros
    End Function
    Public Function cGrillaDet(ByVal cod As Integer) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vAcopio where  comprasCod='" & cod & "'", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "Tipo")
        Return dsRubros
    End Function

    Public Function Anular(ByVal id As Integer) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spAnularAcopio"
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
        Dim daRubros As New MySqlDataAdapter("select * from vInfAcopio where comprasEstado='G' and comprasFecha BETWEEN '" & desde & "' AND '" & hasta & "'", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "Acopio")
        dsRubros.Tables("Acopio").Columns.Add("Usuario")
        dsRubros.Tables("Acopio").Columns.Add("Desde")
        dsRubros.Tables("Acopio").Columns.Add("Hasta")
        Return dsRubros
    End Function
    Public Function traerPrecio(ByVal cod As Integer) As String
        Dim resultado As Integer
        Dim codOut As MySqlParameter
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spTraerPrecio"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@cod", MySqlDbType.Int32).Value = cod
        codOut = cmdInsert.Parameters.Add("@precio", MySqlDbType.Double)
        codOut.Direction = ParameterDirection.Output

        Try

            conexion.Open()
            cmdInsert.ExecuteNonQuery()

            resultado = cmdInsert.Parameters("@precio").Value
            conexion.Close()

        Catch ex As Exception
            conexion.Close()
            resultado = ex.Message
        End Try

        Return resultado
    End Function
    Public Function traerStock(ByVal cod As Integer) As String
        Dim resultado As Integer
        Dim codOut As MySqlParameter
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "traerStock"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@cod", MySqlDbType.Int32).Value = cod
        codOut = cmdInsert.Parameters.Add("@stock", MySqlDbType.Decimal)
        codOut.Direction = ParameterDirection.Output

        Try

            conexion.Open()
            cmdInsert.ExecuteNonQuery()

            resultado = cmdInsert.Parameters("@stock").Value
            conexion.Close()

        Catch ex As Exception
            conexion.Close()
            resultado = ex.Message
        End Try

        Return resultado
    End Function
    Public Function dsGrillaPagos() As DataSet
        Dim daRubros As New MySqlDataAdapter("select provCod as provCod, comprasCod as Cod, comprasFecha as Fecha, comprasNFact as factura,provNombre as prov,comprasTotalFac as monto
            from vacopiocab where comprasEstado='G' order by comprasFecha", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "Tipo")
        Return dsRubros
    End Function
    Public Function trarCompras_OP(ByVal cod As Int32) As DataSet
        Dim daRubros As New MySqlDataAdapter("select comprasDetCod AS cod, prodNombre AS item, comprasdetcant AS cantidad from vAcopio where  comprasCod='" & cod & "'", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "Tipo")
        Return dsRubros
    End Function
    Public Function cInformeComprasFiltro(ByVal desde As String, ByVal hasta As String, ByVal filtro As String) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vInfAcopio where " & filtro & " comprasEstado='G' and comprasFecha BETWEEN '" & desde & "' AND '" & hasta & "'", conexion)
        Dim dsRubros As New DataSet()
        dsRubros.Clear()
        daRubros.Fill(dsRubros, "Acopio")
        dsRubros.Tables("Acopio").Columns.Add("Usuario")
        dsRubros.Tables("Acopio").Columns.Add("Desde")
        dsRubros.Tables("Acopio").Columns.Add("Hasta")
        Return dsRubros
    End Function
End Class
