Imports Microsoft.VisualBasic
Imports System.Data
Imports MySql.Data.MySqlClient
Public Class cVentas
    Private conn As New conexion
    Private conexion As MySqlConnection = conn.conexionBD
    Private tabla As New DataTable
    Function cargarTablaVentas() As DataTable
        tabla = New System.Data.DataTable("Tabla")
        tabla.Columns.Add("Linea", GetType(String))
        tabla.Columns.Add("Codigo", GetType(String))
        tabla.Columns.Add("Producto", GetType(String))
        tabla.Columns.Add("Cantidad", GetType(String))
        tabla.Columns.Add("Stock", GetType(String))
        tabla.Columns.Add("Unitario", GetType(String))
        tabla.Columns.Add("Excenta", GetType(String))
        tabla.Columns.Add("5", GetType(String))
        tabla.Columns.Add("10", GetType(String))
        tabla.Columns.Add("TotalExcenta", GetType(String))
        tabla.Columns.Add("Total5", GetType(String))
        tabla.Columns.Add("Total10", GetType(String))
        tabla.Columns.Add("TotalFact", GetType(String))
        tabla.Columns.Add("TotalIva", GetType(String))
        tabla.Columns.Add("Impuesto", GetType(String))
        Return tabla
    End Function
    Public Function InsertarCab(ByVal cliente As Integer, ByVal fecha As Date, ByVal fact As String,
                               ByVal tipo As String, ByVal excenta As Double, ByVal iva5 As Double, ByVal iva10 As Double,
                               ByVal total As Double, ByVal tiva5 As Double, ByVal tiva10 As Double, ByVal tivas As Double,
                               ByVal reparto As String, ByVal tPagoCod As Integer) As String
        Dim resultado As Integer
        Dim codOut As MySqlParameter
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spIVentas"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@cliente", MySqlDbType.Int32).Value = cliente
        cmdInsert.Parameters.Add("@fecha", MySqlDbType.Date).Value = fecha
        cmdInsert.Parameters.Add("@fact", MySqlDbType.VarChar).Value = fact
        cmdInsert.Parameters.Add("@Usuario", MySqlDbType.Int32).Value = HttpContext.Current.Session("accCod")
        cmdInsert.Parameters.Add("@tipo", MySqlDbType.VarChar).Value = tipo
        cmdInsert.Parameters.Add("@excenta", MySqlDbType.Double).Value = excenta
        cmdInsert.Parameters.Add("@iva5", MySqlDbType.Double).Value = iva5
        cmdInsert.Parameters.Add("@iva10", MySqlDbType.Double).Value = iva10
        cmdInsert.Parameters.Add("@total", MySqlDbType.Double).Value = total
        cmdInsert.Parameters.Add("@tiva5", MySqlDbType.Double).Value = tiva5
        cmdInsert.Parameters.Add("@tiva10", MySqlDbType.Double).Value = tiva10
        cmdInsert.Parameters.Add("@tivas", MySqlDbType.Double).Value = tivas
        cmdInsert.Parameters.Add("@reparto", MySqlDbType.VarChar).Value = reparto
        cmdInsert.Parameters.Add("@tPagoCod", MySqlDbType.Int32).Value = tPagoCod
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
    Public Function InsertarDet(ByVal cod As Integer, ByVal prodCod As Int32, ByVal cantidad As Decimal, ByVal excenta As Double,
                               ByVal al5 As Double, ByVal al10 As Double) As String
        Dim resultado As String
        Dim cmdInsert As New MySqlCommand

        cmdInsert.CommandText = "spIVentasDet"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@cod", MySqlDbType.Int32).Value = cod
        cmdInsert.Parameters.Add("@prodCod", MySqlDbType.Int32).Value = prodCod
        cmdInsert.Parameters.Add("@cantidad", MySqlDbType.Decimal).Value = cantidad
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
    Public Function cGrillaRepartoCab(ByVal desde As String, ByVal hasta As String) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vVentasReparto where ventaReparto='S' and ventaEstado<>'A' and ventaFchFact BETWEEN '" & desde & "' AND '" & hasta & "' ORDER BY cCod, bCod", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "RepartoCab")
        Return dsRubros
    End Function
    Public Function cGrillaRepartoDet(ByVal cod As Int32) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vVentasRepartoDet where  ventaCod='" & cod & "'", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "RepartoDet")
        Return dsRubros
    End Function
    Public Function cInformeRuteo(ByVal desde As String, ByVal hasta As String) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vTraerCosechas where producEstado='A' and producFecha BETWEEN '" & desde & "' AND '" & hasta & "'", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "Cosechas")
        dsRubros.Tables("Cosechas").Columns.Add("Usuario")
        dsRubros.Tables("Cosechas").Columns.Add("Desde")
        dsRubros.Tables("Cosechas").Columns.Add("Hasta")
        Return dsRubros
    End Function
    Public Function cInformeVentas(ByVal desde As String, ByVal hasta As String) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vinfventas where ventaEstado <>'A' and ventaFchFact BETWEEN '" & desde & "' AND '" & hasta & "' order by ventaCod ", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "reparto")
        dsRubros.Tables("reparto").Columns.Add("Usuario")
        dsRubros.Tables("reparto").Columns.Add("Desde")
        dsRubros.Tables("reparto").Columns.Add("Hasta")
        Return dsRubros
    End Function
    Public Function cInformeVentasFiltro(ByVal desde As String, ByVal hasta As String, ByVal filtro As String) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vinfventas where " & filtro & " ventaEstado <>'A' and ventaFchFact BETWEEN '" & desde & "' AND '" & hasta & "' order by ventaCod ", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "reparto")
        dsRubros.Tables("reparto").Columns.Add("Usuario")
        dsRubros.Tables("reparto").Columns.Add("Desde")
        dsRubros.Tables("reparto").Columns.Add("Hasta")
        Return dsRubros
    End Function
    Public Function cInformeVentasAnulados(ByVal desde As String, ByVal hasta As String) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vinfventas where ventaEstado='A' and ventaFchFact BETWEEN '" & desde & "' AND '" & hasta & "' order by ventaCod ", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "reparto")
        dsRubros.Tables("reparto").Columns.Add("Usuario")
        dsRubros.Tables("reparto").Columns.Add("Desde")
        dsRubros.Tables("reparto").Columns.Add("Hasta")
        Return dsRubros
    End Function
    Public Function cInformeVentasAnular(ByVal desde As String, ByVal hasta As String, ByVal cliente As String) As DataSet
        Dim daRubros As MySqlDataAdapter
        If cliente = "" Then
            daRubros = New MySqlDataAdapter("select  * from vVentasCab where ventaEstado='G' and ventaFchFact BETWEEN '" & desde & "' AND '" & hasta & "' order by ventaCod ", conexion)
        Else
            daRubros = New MySqlDataAdapter("select  * from vVentasCab where clieCod =" & cliente & " and ventaEstado='G' and ventaFchFact BETWEEN '" & desde & "' AND '" & hasta & "' order by ventaCod ", conexion)
        End If

        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "ventasCab")
        dsRubros.Tables("ventasCab").Columns.Add("Usuario")
        dsRubros.Tables("ventasCab").Columns.Add("Desde")
        dsRubros.Tables("ventasCab").Columns.Add("Hasta")
        Return dsRubros
    End Function
    Public Function cGrillaDet(ByVal cod As Int32) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vinfventas where  ventaCod=" & cod & "", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "Ventas")
        Return dsRubros
    End Function
    Public Function AnularVentas(ByVal id As Integer, ByVal motivo As String) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spUVentasAnular"
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
    Public Function dsBuscarVentas(ByVal filtro As String) As DataSet

        ' crear un data adapter 

        Dim sql As String = "SELECT * FROM vventascab where (cast(ventaCod as char) like '%" & filtro & "%'" _
                            & " or cast(clieNombre as char) like '%" & filtro & "%' or cast(clieRuc as char) like '%" & filtro & "%' or cast(ventaNroFact as char) like '%" & filtro & "%')"
        '   & " order by lista_precio "

        Dim da As MySqlDataAdapter = New MySqlDataAdapter(sql, conexion)
        ' crear un  dataset 
        Dim ds As DataSet = New DataSet
        ' fill dataset 
        da.Fill(ds, "Clientes")
        Return ds
        conexion.Close()
    End Function
    Public Function InsertarCabNC(ByVal cliecod As Integer, ByVal fecha As Date, ByVal fact As String,
                           ByVal excenta As Double, ByVal iva5 As Double, ByVal iva10 As Double,
                             ByVal total As Double, ByVal tiva5 As Double, ByVal tiva10 As Double, ByVal tivas As Double) As String
        Dim resultado As Integer
        Dim codOut As MySqlParameter
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spINC"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@cliecod", MySqlDbType.Int32).Value = cliecod
        cmdInsert.Parameters.Add("@fecha", MySqlDbType.Date).Value = fecha
        cmdInsert.Parameters.Add("@fact", MySqlDbType.VarChar).Value = fact
        cmdInsert.Parameters.Add("@Usuario", MySqlDbType.Int32).Value = HttpContext.Current.Session("accCod")

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
    Public Function InsertarDetNC(ByVal cod As Integer, ByVal prodCod As Int32, ByVal cantidad As Decimal, ByVal excenta As Double,
                               ByVal al5 As Double, ByVal al10 As Double) As String
        Dim resultado As String
        Dim cmdInsert As New MySqlCommand

        cmdInsert.CommandText = "spINCDet"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@cod", MySqlDbType.Int32).Value = cod
        cmdInsert.Parameters.Add("@prodCod", MySqlDbType.Int32).Value = prodCod
        cmdInsert.Parameters.Add("@cantidad", MySqlDbType.Decimal).Value = cantidad
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

    Public Function InsertarCabMerma(ByVal fecha As Date, ByVal motivo As String) As String
        Dim resultado As Integer
        Dim codOut As MySqlParameter
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spImermasCab"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion

        cmdInsert.Parameters.Add("@fecha", MySqlDbType.Date).Value = fecha
        cmdInsert.Parameters.Add("@motivo", MySqlDbType.VarChar).Value = motivo
        cmdInsert.Parameters.Add("@Usuario", MySqlDbType.Int32).Value = HttpContext.Current.Session("accCod")

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
    Public Function InsertarDetMerma(ByVal cod As Integer, ByVal prodCod As Integer, ByVal cantidad As Decimal) As String
        Dim resultado As String
        Dim cmdInsert As New MySqlCommand

        cmdInsert.CommandText = "spImermasDet"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@cod", MySqlDbType.Int32).Value = cod
        cmdInsert.Parameters.Add("@prodCod", MySqlDbType.Int32).Value = prodCod
        cmdInsert.Parameters.Add("@cantidad", MySqlDbType.Decimal).Value = cantidad
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

    Public Function cInformeMermas(ByVal desde As String, ByVal hasta As String) As DataSet
        Dim daRubros As MySqlDataAdapter

        daRubros = New MySqlDataAdapter("select  * from vmermascab where mermaEstado='G' and mermaFecha BETWEEN '" & desde & "' AND '" & hasta & "' order by mermacod ", conexion)


        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "mermas")
        dsRubros.Tables("mermas").Columns.Add("Usuario")
        dsRubros.Tables("mermas").Columns.Add("Desde")
        dsRubros.Tables("mermas").Columns.Add("Hasta")
        Return dsRubros
    End Function
    Public Function cGrillaDetMerma(ByVal cod As Int32) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vmermadet where  mermaCod=" & cod & "", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "Ventas")
        Return dsRubros
    End Function
    Public Function AnularMermas(ByVal id As Integer, ByVal motivo As String) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spUMermas"
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
    Public Function cInformeNC(ByVal desde As String, ByVal hasta As String) As DataSet
        Dim daRubros As MySqlDataAdapter

        daRubros = New MySqlDataAdapter("select  * from vnccab where ncEstado='G' and ncFchFact BETWEEN '" & desde & "' AND '" & hasta & "' order by ncCod ", conexion)


        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "NC")
        dsRubros.Tables("NC").Columns.Add("Usuario")
        dsRubros.Tables("NC").Columns.Add("Desde")
        dsRubros.Tables("NC").Columns.Add("Hasta")
        Return dsRubros
    End Function

    Public Function cGrillaNcAnul(ByVal cod As Int32) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vNCDet where  ncCod=" & cod & "", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        Try
            daRubros.Fill(dsRubros, "NC")
        Catch ex As Exception

        End Try

        Return dsRubros
    End Function
    Public Function cInformeVentasProd(ByVal desde As String, ByVal hasta As String) As DataSet
        Dim daRubros As New MySqlDataAdapter(" SELECT sum(`ventaDetCant`) AS `ventaDetCant`, fecha,sum(`ventaPrecio5`) AS `ventaPrecio5`,`prodCod` ,`prodNombre`,
sum(`mermaCant`) AS `mermaCant`,  sum(comprasCant) AS comprasCant, sum(compras5) AS compras5, sum(producCant) AS ProducCant, sum(stockCantidad) as stockCantidad
from vInfVentasProd where  fecha BETWEEN '" & desde & "' AND '" & hasta & "' OR fecha=0 GROUP BY `prodCod` ", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "informes")
        dsRubros.Tables("informes").Columns.Add("Usuario")
        dsRubros.Tables("informes").Columns.Add("Desde")
        dsRubros.Tables("informes").Columns.Add("Hasta")
        Return dsRubros
    End Function
    Public Function cReporteNC(ByVal desde As String, ByVal hasta As String, ByVal estado As String) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vinformenc where ncEstado = '" & estado & "' and ncFchFact BETWEEN '" & desde & "' AND '" & hasta & "' order by ncCod ", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "NC")
        dsRubros.Tables("NC").Columns.Add("Usuario")
        dsRubros.Tables("NC").Columns.Add("Desde")
        dsRubros.Tables("NC").Columns.Add("Hasta")
        Return dsRubros
    End Function
    Public Function cInformeMermas(ByVal desde As String, ByVal hasta As String, ByVal estado As String) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vInfMermas where mermaEstado = '" & estado & "' and mermaFecha BETWEEN '" & desde & "' AND '" & hasta & "' order by mermaCod ", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "Mermas")
        dsRubros.Tables("Mermas").Columns.Add("Usuario")
        dsRubros.Tables("Mermas").Columns.Add("Desde")
        dsRubros.Tables("Mermas").Columns.Add("Hasta")
        Return dsRubros
    End Function

    Public Function Ventas_det(ByVal id As Integer) As DataSet

        Dim daRubros As New MySqlDataAdapter("select * from vInformeVentas_Det where ventacod = '" & id & "'", conexion)
        Dim dsRubros As New DataSet()

        Try
            'Traer el Id, Nombre y Contraseña del usuario

            dsRubros.Clear()
            daRubros.Fill(dsRubros, "Ventas")


        Catch ex As Exception

        End Try

        Return dsRubros

    End Function

    Public Function ActualizarVentasCab(ByVal cod As Integer, ByVal fecha As Date, ByVal fact As String, ByVal tipo As String, ByVal reparto As String,
                                        ByVal tPagoCod As Integer) As String
        Dim resultado As String
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spUVentasCab"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@cod", MySqlDbType.Int32).Value = cod
        cmdInsert.Parameters.Add("@fecha", MySqlDbType.Date).Value = fecha
        cmdInsert.Parameters.Add("@fact", MySqlDbType.VarChar).Value = fact
        cmdInsert.Parameters.Add("@usuario", MySqlDbType.Int32).Value = HttpContext.Current.Session("accCod")
        cmdInsert.Parameters.Add("@tipo", MySqlDbType.VarChar).Value = tipo
        cmdInsert.Parameters.Add("@reparto", MySqlDbType.VarChar).Value = reparto
        cmdInsert.Parameters.Add("@tPagoCod", MySqlDbType.Int32).Value = tPagoCod

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


    Public Function AnularNC(ByVal id As Integer, ByVal motivo As String) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spUNC"
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
    Public Function cInformeVentasActualizar(ByVal desde As String, ByVal hasta As String) As DataSet
        Dim daRubros As MySqlDataAdapter

        daRubros = New MySqlDataAdapter("select  * from vVentasCab where ventaEstado<>'A' and ventaFchFact BETWEEN '" & desde & "' AND '" & hasta & "' order by ventaCod ", conexion)


        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "ventasCab")
        dsRubros.Tables("ventasCab").Columns.Add("Usuario")
        dsRubros.Tables("ventasCab").Columns.Add("Desde")
        dsRubros.Tables("ventasCab").Columns.Add("Hasta")
        Return dsRubros
    End Function
    Public Function ActualizarVentasFact(ByVal cod As Integer, ByVal fecha As Date, ByVal fact As String) As String
        Dim resultado As String
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spUVentasCabFact"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@cod", MySqlDbType.Int32).Value = cod
        cmdInsert.Parameters.Add("@fact", MySqlDbType.VarChar).Value = fact
        cmdInsert.Parameters.Add("@usuario", MySqlDbType.Int32).Value = HttpContext.Current.Session("accCod")

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

    Public Function nroFactura() As DataSet
        Dim daRubros As New MySqlDataAdapter("select max(nroFactura) as nroFactura from factura", conexion)
        Dim dsRubros As New DataSet()

        Try
            'Traer el Id, Nombre y Contraseña del usuario

            dsRubros.Clear()
            daRubros.Fill(dsRubros, "factura")


        Catch ex As Exception

        End Try

        Return dsRubros

    End Function
    Public Function ActualizarNroFactura(ByVal nro As Integer) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spUFacturas"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@nro", MySqlDbType.Int32).Value = nro

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
