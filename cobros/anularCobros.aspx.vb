﻿Imports MySql.Data.MySqlClient
Public Class anularCobros
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblResultado.Visible = False
            txtDesde.Text = Date.Now.ToString("yyyy-MM-dd")
            txtHasta.Text = Date.Now.ToString("yyyy-MM-dd")

        End If
    End Sub

    Sub btnBuscar_Click()
        Dim cargar As New cCobros
        Dim grilla As New DataSet
        grilla = cargar.cInformeCobrosAul(txtDesde.Text, txtHasta.Text)
        gvDatos.DataSource = grilla
        gvDatos.DataBind()

    End Sub



    Sub btnAceptar_Click()

        Dim eliminar As New cCobros
        Dim resultado As String = ""
        Dim fila As GridViewRow
        Dim info As String

        fila = gvDatos.SelectedRow
        info = fila.Cells(0).Text
        ID = CInt(info)
        resultado = eliminar.AnularCobros(ID, txtMotivo.Text)
        gvDatos.DataSource = Nothing
        gvDatos.DataBind()



    End Sub



End Class