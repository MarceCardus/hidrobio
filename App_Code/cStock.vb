Imports Microsoft.VisualBasic
Imports System.Data
Imports MySql.Data.MySqlClient
Public Class cStock
    Private conn As New conexion
    Private conexion As MySqlConnection = conn.conexionBD
    Private tabla As New DataTable
    Function cargarTablaEmpaque() As DataTable
        tabla = New System.Data.DataTable("Tabla")
        tabla.Columns.Add("Codigo", GetType(String))
        tabla.Columns.Add("Producto", GetType(String))
        tabla.Columns.Add("Anterior", GetType(String))
        tabla.Columns.Add("empaCod", GetType(String))
        tabla.Columns.Add("Empaque", GetType(String))
        tabla.Columns.Add("Empaquetado", GetType(String))

        Return tabla
    End Function
    Public Function InsertarEmp(ByVal empaCod As Integer, ByVal prodCod As Integer, ByVal cantidadA As Decimal, ByVal eCantEmp As Decimal, ByVal fecha As Date) As String
        Dim resultado As String
        Dim cmdInsert As New MySqlCommand

        cmdInsert.CommandText = "spIEmpaquetados"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Connection = conexion
        cmdInsert.Parameters.Add("@prodCodA", MySqlDbType.Int32).Value = empaCod
        cmdInsert.Parameters.Add("@prodCodN", MySqlDbType.Int32).Value = prodCod
        cmdInsert.Parameters.Add("@cantidadA", MySqlDbType.Decimal).Value = cantidadA
        cmdInsert.Parameters.Add("@eCantEmp", MySqlDbType.Decimal).Value = eCantEmp
        cmdInsert.Parameters.Add("@usuario", MySqlDbType.Int32).Value = HttpContext.Current.Session("accCod")
        cmdInsert.Parameters.Add("@fecha", MySqlDbType.Date).Value = fecha
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
