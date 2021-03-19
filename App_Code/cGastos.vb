Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Imports System.Data
Public Class cGastos
    Private conn As New conexion
    Private conexion As MySqlConnection = conn.conexionBD
    Public Function InsertarItem(ByVal nombre As String, ByVal precio As Double, ByVal empaque As Integer, ByVal rubroCod As Integer,
                           ByVal impuesto As Integer) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spIItem"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@nombre", MySqlDbType.VarChar).Value = nombre
        cmdInsert.Parameters.Add("@precio", MySqlDbType.Double).Value = precio
        cmdInsert.Parameters.Add("@empaque", MySqlDbType.Int32).Value = empaque
        cmdInsert.Parameters.Add("@rubroCod", MySqlDbType.Int32).Value = rubroCod
        cmdInsert.Parameters.Add("@impuesto", MySqlDbType.Int32).Value = impuesto


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
    Public Function ActualizarItem(ByVal cod As Integer, ByVal nombre As String, ByVal precio As Double, ByVal empaque As Integer, ByVal rubroCod As Integer,
                           ByVal impuesto As Integer) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spUItem"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@cod", MySqlDbType.Int32).Value = cod
        cmdInsert.Parameters.Add("@nombre", MySqlDbType.VarChar).Value = nombre
        cmdInsert.Parameters.Add("@precio", MySqlDbType.Double).Value = precio
        cmdInsert.Parameters.Add("@empaque", MySqlDbType.Int32).Value = empaque
        cmdInsert.Parameters.Add("@rubroCod", MySqlDbType.Int32).Value = rubroCod
        cmdInsert.Parameters.Add("@impuesto", MySqlDbType.Int32).Value = impuesto
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
    Public Function ComandosLecturaItem(ByVal id As Int16) As MySqlDataReader
        Dim sql As String
        Dim conectar As New conexion
        Dim Comandos As MySqlCommand
        Dim resultado As MySqlDataReader = Nothing

        Try

            sql = "select * from vItem where itemCod = '" & id & "'"
            Comandos = New MySqlCommand(sql, conectar.conexionBD)
            Comandos.Connection.Open()

            resultado = Comandos.ExecuteReader

        Catch ex As Exception

        End Try

        Return resultado
        Comandos.Connection.Close()
    End Function
    Public Function cgRubro(ByVal filtro As String) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vRubros " & filtro & "", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "Rubros")
        Return dsRubros
    End Function
    Public Function traerItem(ByVal cod As Integer) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vItem where rubroCod=" & cod & "", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "Item")
        Return dsRubros
    End Function

    Function cargarTablaGastos() As DataTable
        Dim tablaGastos As New DataTable
        tablaGastos = New System.Data.DataTable("Tabla")
        tablaGastos.Columns.Add("Linea", GetType(String))
        tablaGastos.Columns.Add("Codigo", GetType(String))
        tablaGastos.Columns.Add("Item", GetType(String))
        tablaGastos.Columns.Add("Cantidad", GetType(String))
        tablaGastos.Columns.Add("Unitario", GetType(String))
        tablaGastos.Columns.Add("Excenta", GetType(String))
        tablaGastos.Columns.Add("5", GetType(String))
        tablaGastos.Columns.Add("10", GetType(String))
        tablaGastos.Columns.Add("TotalExcenta", GetType(String))
        tablaGastos.Columns.Add("Total5", GetType(String))
        tablaGastos.Columns.Add("Total10", GetType(String))
        tablaGastos.Columns.Add("TotalFact", GetType(String))
        tablaGastos.Columns.Add("TotalIva", GetType(String))
        tablaGastos.Columns.Add("Impuesto", GetType(String))
        tablaGastos.Columns.Add("Movil", GetType(String))
        tablaGastos.Columns.Add("Chofer", GetType(String))
        tablaGastos.Columns.Add("movCod", GetType(String))
        tablaGastos.Columns.Add("perCod", GetType(String))
        Return tablaGastos
    End Function


    Public Function traerPrecioItem(ByVal cod As Integer) As Tuple(Of Double, Integer)

        Dim precio As Double
        Dim impuesto As Integer
        Dim error23 As String
        Dim codOut As MySqlParameter
        Dim codOut1 As MySqlParameter
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spTraerItem"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@cod", MySqlDbType.Int32).Value = cod
        codOut = cmdInsert.Parameters.Add("@precio", MySqlDbType.Double)
        codOut1 = cmdInsert.Parameters.Add("@impuesto", MySqlDbType.Int32)
        codOut.Direction = ParameterDirection.Output
        codOut1.Direction = ParameterDirection.Output
        Dim resultado
        Try

            conexion.Open()
            cmdInsert.ExecuteNonQuery()
            precio = cmdInsert.Parameters("@precio").Value
            impuesto = cmdInsert.Parameters("@impuesto").Value
            resultado = New Tuple(Of Double, Integer)(precio, impuesto)
            conexion.Close()

        Catch ex As Exception
            conexion.Close()
            error23 = ex.Message
        End Try

        Return resultado
    End Function

    Public Function InsertarGastosCab(ByVal NroFact As String, ByVal FechaFact As Date, ByVal Tipo As String, ByVal total As Double,
                          ByVal exenta As Double, ByVal g5 As Double, ByVal g10 As Double, ByVal i5 As Double, ByVal i10 As Double, ByVal provCod As Integer, ByVal gastosObs As String) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        Dim codOut As MySqlParameter
        cmdInsert.CommandText = "spIGastos"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@NroFact", MySqlDbType.VarChar).Value = NroFact
        cmdInsert.Parameters.Add("@FechaFact", MySqlDbType.Date).Value = FechaFact
        cmdInsert.Parameters.Add("@Tipo", MySqlDbType.VarChar).Value = Tipo
        cmdInsert.Parameters.Add("@usuario", MySqlDbType.Int32).Value = HttpContext.Current.Session("accCod")
        cmdInsert.Parameters.Add("@total", MySqlDbType.Double).Value = total
        cmdInsert.Parameters.Add("@exenta", MySqlDbType.Double).Value = exenta
        cmdInsert.Parameters.Add("@g5", MySqlDbType.Double).Value = g5
        cmdInsert.Parameters.Add("@g10", MySqlDbType.Double).Value = g10
        cmdInsert.Parameters.Add("@i5", MySqlDbType.Double).Value = i5
        cmdInsert.Parameters.Add("@i10", MySqlDbType.Double).Value = i10
        cmdInsert.Parameters.Add("@provCod", MySqlDbType.Int32).Value = provCod
        cmdInsert.Parameters.Add("@gastosObs", MySqlDbType.VarChar).Value = gastosObs
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
    Public Function InsertarGastosDet(ByVal gCod As Integer, ByVal impCod As Integer,
                         ByVal d10 As Double, ByVal d5 As Double, ByVal dEx As Double, ByVal rubroCod As Integer) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand

        cmdInsert.CommandText = "spGastosDet"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@gCod", MySqlDbType.Int32).Value = gCod
        cmdInsert.Parameters.Add("@impCod", MySqlDbType.Int32).Value = impCod
        cmdInsert.Parameters.Add("@d10", MySqlDbType.Double).Value = d10
        cmdInsert.Parameters.Add("@d5", MySqlDbType.Double).Value = d5
        cmdInsert.Parameters.Add("@dEx", MySqlDbType.Double).Value = dEx

        cmdInsert.Parameters.Add("@rubroCod", MySqlDbType.Int32).Value = rubroCod

        Try

            conexion.Open()
            cmdInsert.ExecuteNonQuery()

            resultado = "ok"
            conexion.Close()

        Catch ex As Exception
            conexion.Close()
            resultado = ex.Message
        End Try

        Return resultado
    End Function
    Public Function InsertarGastosDetCombus(ByVal gCod As Integer, ByVal itemCod As Integer, ByVal impCod As Integer,
                       ByVal cant As Decimal, ByVal d10 As Double, ByVal d5 As Double, ByVal dEx As Double, ByVal movCod As Integer, ByVal perCod As Integer) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand

        cmdInsert.CommandText = "spGastosDetCombus"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@gCod", MySqlDbType.Int32).Value = gCod
        cmdInsert.Parameters.Add("@itemCod", MySqlDbType.Int32).Value = itemCod
        cmdInsert.Parameters.Add("@impCod", MySqlDbType.Int32).Value = impCod
        cmdInsert.Parameters.Add("@cant", MySqlDbType.Decimal).Value = cant
        cmdInsert.Parameters.Add("@d10", MySqlDbType.Double).Value = d10
        cmdInsert.Parameters.Add("@d5", MySqlDbType.Double).Value = d5
        cmdInsert.Parameters.Add("@dEx", MySqlDbType.Double).Value = dEx
        cmdInsert.Parameters.Add("@movCod", MySqlDbType.Int32).Value = movCod
        cmdInsert.Parameters.Add("@perCod", MySqlDbType.Int32).Value = perCod


        Try

            conexion.Open()
            cmdInsert.ExecuteNonQuery()

            resultado = "ok"
            conexion.Close()

        Catch ex As Exception
            conexion.Close()
            resultado = ex.Message
        End Try

        Return resultado
    End Function
    Public Function cInformeGastosAnular(ByVal desde As String, ByVal hasta As String, ByVal proveedor As String) As DataSet
        Dim daRubros As MySqlDataAdapter
        If proveedor = "" Then
            daRubros = New MySqlDataAdapter("select  * from vgastoscab where gastosEstado='G' and gastosFechaFact BETWEEN '" & desde & "' AND '" & hasta & "' order by gastosCod ", conexion)
        Else
            daRubros = New MySqlDataAdapter("select  * from vgastoscab where provCod =" & proveedor & " and gastosEstado='G' and gastosFechaFact BETWEEN '" & desde & "' AND '" & hasta & "' order by gastosCod ", conexion)
        End If

        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "gastosCab")
        dsRubros.Tables("gastosCab").Columns.Add("Usuario")
        dsRubros.Tables("gastosCab").Columns.Add("Desde")
        dsRubros.Tables("gastosCab").Columns.Add("Hasta")
        Return dsRubros
    End Function

    Public Function cGrillaDetGastos(ByVal cod As Int32) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vGastosDet where  gastoCod=" & cod & "", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "Ventas")
        Return dsRubros
    End Function
    Public Function AnularGastos(ByVal id As Integer, ByVal motivo As String) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spAnularGastos"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@Cod", MySqlDbType.Int32).Value = id
        cmdInsert.Parameters.Add("@usuario", MySqlDbType.Int32).Value = HttpContext.Current.Session("accCod")
        cmdInsert.Parameters.Add("@motivo", MySqlDbType.VarChar).Value = motivo
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


    Public Function cReporteGastosNuevo(ByVal desde As String, ByVal hasta As String, ByVal estado As String, ByVal rubros As String) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vreporteGastos where " & rubros & " gastosEstado  " & estado & " and gastosFechaFact BETWEEN '" & desde & "' AND '" & hasta & "' order by gastosCod ", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "Gastos")
        dsRubros.Tables("Gastos").Columns.Add("Usuario")
        dsRubros.Tables("Gastos").Columns.Add("Desde")
        dsRubros.Tables("Gastos").Columns.Add("Hasta")
        Return dsRubros
    End Function
    Public Function dsGastosPago() As DataSet
        Dim daRubros As MySqlDataAdapter

        daRubros = New MySqlDataAdapter("select provCod as provCod, gastosCod as Cod, gastosFechaFact as Fecha, gastosNroFact as factura,provNombre as prov,gastosTotal as monto 
                                    from vgastoscab where gastosEstado='G' order by gastosFechaFact ", conexion)


        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "Tipo")
        Return dsRubros
    End Function
    Public Function cGrillaDetGastos_OP(ByVal cod As Int32) As DataSet
        Dim daRubros As New MySqlDataAdapter("SELECT gDetCod AS cod, itemNombre AS item, gDetCant AS cantidad  from vGastosDet where  gastoCod=" & cod & "", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "Gastos")
        Return dsRubros
    End Function
    Public Function cOPcab(ByVal desde As String, ByVal hasta As String) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vOpcab where opEstado  <> 'A' and opFecha BETWEEN '" & desde & "' AND '" & hasta & "' order by opCod ", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "op")
        dsRubros.Tables("op").Columns.Add("Usuario")
        dsRubros.Tables("op").Columns.Add("Desde")
        dsRubros.Tables("op").Columns.Add("Hasta")
        Return dsRubros
    End Function
    Public Function AnularOp(ByVal id As Integer) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spUOp"
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
    Public Function cGrillaDetOP(ByVal cod As Int32) As DataSet
        Dim daRubros As New MySqlDataAdapter("select opTipo, provNombre, comprasTotalFac,gastosTotal from vop where  opCod=" & cod & "", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "Ventas")
        Return dsRubros
    End Function
    Public Function cReporteOp(ByVal desde As String, ByVal hasta As String, ByVal estado As String) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vop where opEstado  " & estado & " and opFecha BETWEEN '" & desde & "' AND '" & hasta & "' order by opCod ", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "op")
        dsRubros.Tables("op").Columns.Add("Usuario")
        dsRubros.Tables("op").Columns.Add("Desde")
        dsRubros.Tables("op").Columns.Add("Hasta")
        Return dsRubros
    End Function
    Public Function InsertarGastosDetInsumos(ByVal gCod As Integer, ByVal impCod As Integer,
                        ByVal d10 As Double, ByVal d5 As Double, ByVal dEx As Double, ByVal rubroCod As Integer, ByVal gDetCant As Decimal,
                        ByVal itemCod As Integer) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand

        cmdInsert.CommandText = "spGastosDetIns"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@gCod", MySqlDbType.Int32).Value = gCod
        cmdInsert.Parameters.Add("@impCod", MySqlDbType.Int32).Value = impCod
        cmdInsert.Parameters.Add("@d10", MySqlDbType.Double).Value = d10
        cmdInsert.Parameters.Add("@d5", MySqlDbType.Double).Value = d5
        cmdInsert.Parameters.Add("@dEx", MySqlDbType.Double).Value = dEx
        cmdInsert.Parameters.Add("@gDetCant", MySqlDbType.Decimal).Value = gDetCant
        cmdInsert.Parameters.Add("@rubroCod", MySqlDbType.Int32).Value = rubroCod
        cmdInsert.Parameters.Add("@itemCod", MySqlDbType.Int32).Value = itemCod
        Try

            conexion.Open()
            cmdInsert.ExecuteNonQuery()

            resultado = "ok"
            conexion.Close()

        Catch ex As Exception
            conexion.Close()
            resultado = ex.Message
        End Try

        Return resultado
    End Function

End Class
