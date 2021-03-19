Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Imports System.Data
Public Class cProductos
    Private conn As New conexion
    Private conexion As MySqlConnection = conn.conexionBD
    Public Function Insertar(ByVal nombre As String, ByVal tpCod As Integer, ByVal foto As String, ByVal empaque As Integer, ByVal precio As Double,
                            ByVal subtipo As Integer) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spIProducto"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@nombre", MySqlDbType.VarChar).Value = nombre
        cmdInsert.Parameters.Add("@tpCod", MySqlDbType.Int32).Value = tpCod
        cmdInsert.Parameters.Add("@foto", MySqlDbType.VarChar).Value = foto
        cmdInsert.Parameters.Add("@empaque", MySqlDbType.Int32).Value = empaque
        cmdInsert.Parameters.Add("@precio", MySqlDbType.Int32).Value = precio
        cmdInsert.Parameters.Add("@subtipo", MySqlDbType.Int32).Value = subtipo

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
    Public Function Actualizar(ByVal id As Integer, ByVal nombre As String, ByVal tpCod As Integer, ByVal foto As String, ByVal empaque As Integer,
                               ByVal precio As Double, ByVal subtipo As Integer) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spUProducto"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@cod", MySqlDbType.Int32).Value = id
        cmdInsert.Parameters.Add("@nombre", MySqlDbType.VarChar).Value = nombre
        cmdInsert.Parameters.Add("@tpCod", MySqlDbType.Int32).Value = tpCod
        cmdInsert.Parameters.Add("@foto", MySqlDbType.VarChar).Value = foto
        cmdInsert.Parameters.Add("@empaque", MySqlDbType.Int32).Value = empaque
        cmdInsert.Parameters.Add("@precio", MySqlDbType.Int32).Value = precio
        cmdInsert.Parameters.Add("@subtipo", MySqlDbType.Int32).Value = subtipo
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
    Public Function cDataset(ByVal tabla As String, ByVal nombreDato As String) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from " & tabla & "", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, nombreDato)
        Return dsRubros
    End Function
    Public Function ComandosLectura(ByVal id As Int16) As MySqlDataReader
        Dim sql As String
        Dim conectar As New conexion
        Dim Comandos As MySqlCommand
        Dim resultado As MySqlDataReader = Nothing

        Try

            sql = "select * from vproductos where prodCod = '" & id & "'"
            Comandos = New MySqlCommand(sql, conectar.conexionBD)
            Comandos.Connection.Open()

            resultado = Comandos.ExecuteReader

        Catch ex As Exception

        End Try

        Return resultado
        Comandos.Connection.Close()
    End Function

    Public Function dsBuscarProdcutos(ByVal filtro As String) As DataSet

        ' crear un data adapter 

        Dim sql As String = "SELECT DISTINCT * FROM vtraerStock where (cast(prodCod as char) like '%" & filtro & "%'" _
                            & " or cast(prodNombre as char) like '%" & filtro & "%')"
        '   & " order by lista_precio "

        Dim da As MySqlDataAdapter = New MySqlDataAdapter(sql, conexion)
        ' crear un  dataset 
        Dim ds As DataSet = New DataSet
        ' fill dataset 
        da.Fill(ds, "Productos")
        Return ds
        conexion.Close()
    End Function
    Public Function dsBuscarProdcutosss(ByVal filtro As String) As DataSet

        ' crear un data adapter 

        Dim sql As String = "SELECT DISTINCT * FROM vproductos where (cast(prodCod as char) like '%" & filtro & "%'" _
                            & " or cast(prodNombre as char) like '%" & filtro & "%')"
        '   & " order by lista_precio "

        Dim da As MySqlDataAdapter = New MySqlDataAdapter(sql, conexion)
        ' crear un  dataset 
        Dim ds As DataSet = New DataSet
        ' fill dataset 
        da.Fill(ds, "Productos")
        Return ds
        conexion.Close()
    End Function

    Public Function cGrillaStock(ByVal filtro As String) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vtraerStock where stockAnulado='N' and tipoProdCod=" & filtro & " ORDER BY prodNombre", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "Tipo")
        Return dsRubros
    End Function
    Public Function cGrillaStockOtros() As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vtraerStock where stockAnulado='N' and tipoProdCod > 11 ORDER BY prodNombre", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "Tipo")
        Return dsRubros
    End Function
    Public Function cGrillaSalidas(ByVal dato As String) As DataSet
        Dim fecha As String
        fecha = Date.Now.ToString("yyyy-MM-dd")
        Dim daRubros As New MySqlDataAdapter("CALL spTraerVentasDia('" & fecha & "','" & dato & "')", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "Tipo")
        Return dsRubros
    End Function
    Public Function cGrillaSalidasOtros() As DataSet
        Dim fecha As String
        fecha = Date.Now.ToString("yyyy-MM-dd")
        Dim daRubros As New MySqlDataAdapter("CALL spTraerVentasDiaOtros('" & fecha & "')", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "Tipo")
        Return dsRubros
    End Function
    Public Function cdSubgrupo(ByVal tabla As String, ByVal nombreDato As String) As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from " & tabla & "", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, nombreDato)
        Return dsRubros
    End Function
    Public Function cGrillaMovimiento(ByVal dato As String) As DataSet
        Dim fecha As String
        fecha = Date.Now.ToString("yyyy-MM-dd")
        Dim daRubros As New MySqlDataAdapter("CALL spTraerVentasTotales('" & fecha & "','" & dato & "')", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "Tipo")
        Return dsRubros
    End Function
    Public Function cGrillaSubTipoProduc() As DataSet
        Dim daRubros As New MySqlDataAdapter("select * from vsubtipoproduc", conexion)
        Dim dsRubros As New DataSet()

        dsRubros.Clear()
        daRubros.Fill(dsRubros, "SubTipo")
        Return dsRubros
    End Function
    Public Function InsertarSubTipo(ByVal nombre As String, ByVal tipoCod As Integer) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spISubTipoProd"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@subtipo", MySqlDbType.VarChar).Value = nombre
        cmdInsert.Parameters.Add("@tipo", MySqlDbType.Int16).Value = tipoCod
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
    Public Function ActualizarSubTipo(ByVal id As Integer, ByVal nombre As String, ByVal tipo As Integer) As String
        Dim resultado As String = ""
        Dim cmdInsert As New MySqlCommand
        cmdInsert.CommandText = "spUSubTipoProd"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@Cod", MySqlDbType.Int32).Value = id
        cmdInsert.Parameters.Add("@nombre", MySqlDbType.VarChar).Value = nombre
        cmdInsert.Parameters.Add("@tipo", MySqlDbType.Int32).Value = tipo

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
    Public Function ComandosLecturaSubTipo(ByVal id As Int16) As MySqlDataReader
        Dim sql As String
        Dim conectar As New conexion
        Dim Comandos As MySqlCommand
        Dim resultado As MySqlDataReader = Nothing

        Try
            'Traer el Id, Nombre y Contraseña del usuario
            sql = "select * from vsubtipoproduc where stpCod = '" & id & "'"
            Comandos = New MySqlCommand(sql, conectar.conexionBD)
            Comandos.Connection.Open()

            resultado = Comandos.ExecuteReader

        Catch ex As Exception

        End Try

        Return resultado
        Comandos.Connection.Close()
    End Function
End Class
