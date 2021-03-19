Imports MySql.Data.MySqlClient
Imports MySql.Data
Imports System.Data

Public Class cCobros
    Private conn As New conexion
    Private conexion As MySqlConnection = conn.conexionBD
    Public Function dsBuscarClientes(ByVal cod As String) As DataSet

        ' crear un data adapter 

        Dim sql As String = "select * from vcobrosventas where ventaEstado ='G' and  clieCod = '" & cod & "' order by ventaFchFact DESC"
        '   & " order by lista_precio "

        Dim da As MySqlDataAdapter = New MySqlDataAdapter(sql, conexion)
        ' crear un  dataset 
        Dim ds As DataSet = New DataSet
        ' fill dataset 
        da.Fill(ds, "Clientes")
        Return ds
        conexion.Close()
    End Function
    Public Function dsBuscarClientesVentas(ByVal cod As String) As DataSet

        ' crear un data adapter 

        Dim sql As String = "select * from vcobrosventas where ventaEstado <>'A' and  clieCod = '" & cod & "' order by ventaCod DESC"
        '   & " order by lista_precio "

        Dim da As MySqlDataAdapter = New MySqlDataAdapter(sql, conexion)
        ' crear un  dataset 
        Dim ds As DataSet = New DataSet
        ' fill dataset 
        da.Fill(ds, "Clientes")
        Return ds
        conexion.Close()
    End Function
    Public Function clVentas(ByVal id As Integer) As MySqlDataReader
        Dim sql As String
        Dim conectar As New conexion
        Dim Comandos As MySqlCommand
        Dim resultado As MySqlDataReader = Nothing

        Try
            'Traer el Id, Nombre y Contraseña del usuario
            sql = "select * from vcobrosventas where  ventacod = '" & id & "'"
            Comandos = New MySqlCommand(sql, conectar.conexionBD)
            Comandos.Connection.Open()

            resultado = Comandos.ExecuteReader

        Catch ex As Exception

        End Try

        Return resultado
        Comandos.Connection.Close()
    End Function
    Public Function cDatasetBancos(ByVal tabla As String, ByVal nombreDato As String) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from " & tabla & "", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, nombreDato)
        Return dsRubros
    End Function

    Public Function dsBuscarCobros(ByVal codVenta As Integer) As DataSet

        ' crear un data adapter 

        Dim sql As String = "select * from vCobros where ventaCod=" & codVenta & " order by cobroCod"
        '   & " order by lista_precio "

        Dim da As MySqlDataAdapter = New MySqlDataAdapter(sql, conexion)
        ' crear un  dataset 
        Dim ds As DataSet = New DataSet
        ' fill dataset 
        da.Fill(ds, "Clientes")
        Return ds
        conexion.Close()
    End Function
    Public Function Insertar(ByVal ventaCod As Integer, ByVal fecha As Date, ByVal tipo As Integer, ByVal chqnro As Integer,
                            ByVal chqfch As Date, ByVal saldo As Double, ByVal recibo As Integer, ByVal estado As String,
                            ByVal banco As Integer, ByVal monto As Double) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spCobros"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@ventaCod", MySqlDbType.Int32).Value = ventaCod
        cmdInsert.Parameters.Add("@fecha", MySqlDbType.Date).Value = fecha
        cmdInsert.Parameters.Add("@tipo", MySqlDbType.Int32).Value = tipo
        cmdInsert.Parameters.Add("@chqnro", MySqlDbType.Int32).Value = chqnro
        cmdInsert.Parameters.Add("@chqfch", MySqlDbType.Date).Value = chqfch
        cmdInsert.Parameters.Add("@saldo", MySqlDbType.Double).Value = saldo
        cmdInsert.Parameters.Add("@recibo", MySqlDbType.Int32).Value = recibo
        cmdInsert.Parameters.Add("@estado", MySqlDbType.VarChar).Value = estado
        cmdInsert.Parameters.Add("@usuario", MySqlDbType.Int32).Value = HttpContext.Current.Session("accCod")
        cmdInsert.Parameters.Add("@banco", MySqlDbType.Int32).Value = banco
        cmdInsert.Parameters.Add("@monto", MySqlDbType.Double).Value = monto
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
    Public Function InsertarContado(ByVal ventaCod As Integer, ByVal fecha As Date, ByVal tipo As Integer, ByVal saldo As Double,
                                    ByVal estado As String,
                             ByVal monto As Double, ByVal recibo As Integer) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spCobrosContado"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@ventaCod", MySqlDbType.Int32).Value = ventaCod
        cmdInsert.Parameters.Add("@fecha", MySqlDbType.Date).Value = fecha
        cmdInsert.Parameters.Add("@tipo", MySqlDbType.Int32).Value = tipo

        cmdInsert.Parameters.Add("@saldo", MySqlDbType.Double).Value = saldo

        cmdInsert.Parameters.Add("@estado", MySqlDbType.VarChar).Value = estado
        cmdInsert.Parameters.Add("@usuario", MySqlDbType.Int32).Value = HttpContext.Current.Session("accCod")
        cmdInsert.Parameters.Add("@recibo", MySqlDbType.Int32).Value = recibo
        cmdInsert.Parameters.Add("@monto", MySqlDbType.Double).Value = monto
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
    Public Function cInformeCobros(ByVal desde As String, ByVal hasta As String, ByVal estado As String) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vCobrosInforme where " & estado & " cobroFecha BETWEEN '" & desde & "' AND '" & hasta & "' order by cobroCod ", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "cobros")
        dsRubros.Tables("cobros").Columns.Add("Usuario")
        dsRubros.Tables("cobros").Columns.Add("Desde")
        dsRubros.Tables("cobros").Columns.Add("Hasta")
        Return dsRubros
    End Function
    Public Function cInformeSaldos(ByVal desde As String, ByVal hasta As String) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vventascab where ventaEstado='G' and ventaSaldo <> 0 and ventaFchFact BETWEEN '" & desde & "' AND '" & hasta & "' order by ventaCod ", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "reparto")
        dsRubros.Tables("reparto").Columns.Add("Usuario")
        dsRubros.Tables("reparto").Columns.Add("Desde")
        dsRubros.Tables("reparto").Columns.Add("Hasta")
        Return dsRubros
    End Function
    Public Function AnularCobros(ByVal id As Integer, ByVal motivo As String) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spUCobros"
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

    Public Function cInformeCobrosAul(ByVal desde As String, ByVal hasta As String) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vCobrosInforme where cobroEstado  <> 'A' and cobroFecha BETWEEN '" & desde & "' AND '" & hasta & "' order by cobroCod ", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "cobros")
        dsRubros.Tables("cobros").Columns.Add("Usuario")
        dsRubros.Tables("cobros").Columns.Add("Desde")
        dsRubros.Tables("cobros").Columns.Add("Hasta")
        Return dsRubros
    End Function
End Class
