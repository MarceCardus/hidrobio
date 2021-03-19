Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports MySql.Data
Imports MySql.Data.MySqlClient


' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class wsClientes
    Inherits System.Web.Services.WebService
    Private conn As New conexion
    Private conexion As MySqlConnection = conn.conexionBD
    <WebMethod()>
    Public Function GetCompletionList(prefixText As String, ByVal count As Integer) As List(Of String)

        Dim result As List(Of String) = New List(Of String)

        Dim cmd As New MySqlCommand
        cmd.CommandText = "spClientesWS"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Connection = conexion
        cmd.Parameters.AddWithValue("@filtro", prefixText)

        conexion.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader()
        While dr.Read()
            result.Add(dr("Clientes").ToString())
        End While
        conexion.Close()
        Return result
    End Function

End Class