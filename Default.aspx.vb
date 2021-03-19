Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

            stockLechugas()
            stockVerdeos()
            stockLocotes()
            stockTomates()
            stockFrutas()
            stockFrescos()
            stockCurcu()
            stockOtros()

        End If
    End Sub


    Sub stockLechugas()
        Dim cargar As New cProductos
        Dim ds As New DataSet
        ds = cargar.cGrillaMovimiento(8)
        gvStockLechugas1.DataSource = ds
        gvStockLechugas1.DataBind()
    End Sub
    Sub stockVerdeos()
        Dim cargar As New cProductos
        Dim ds As New DataSet
        ds = cargar.cGrillaMovimiento(1)
        gvStockVerdeos.DataSource = ds
        gvStockVerdeos.DataBind()
    End Sub
    Sub stockLocotes()
        Dim cargar As New cProductos
        Dim ds As New DataSet
        ds = cargar.cGrillaMovimiento(10)
        gvLocotes.DataSource = ds
        gvLocotes.DataBind()
    End Sub
    Sub stockTomates()
        Dim cargar As New cProductos
        Dim ds As New DataSet
        ds = cargar.cGrillaMovimiento(2)
        gvTomate.DataSource = ds
        gvTomate.DataBind()
    End Sub
    Sub stockFrescos()
        Dim cargar As New cProductos
        Dim ds As New DataSet
        ds = cargar.cGrillaMovimiento(7)
        gvFrescos.DataSource = ds
        gvFrescos.DataBind()
    End Sub
    Sub stockFrutas()
        Dim cargar As New cProductos
        Dim ds As New DataSet
        ds = cargar.cGrillaMovimiento(4)
        gvFrutas.DataSource = ds
        gvFrutas.DataBind()
    End Sub
    Sub stockCurcu()
        Dim cargar As New cProductos
        Dim ds As New DataSet
        ds = cargar.cGrillaMovimiento(11)
        gvCurcu.DataSource = ds
        gvCurcu.DataBind()
    End Sub
    Sub stockOtros()
        Dim cargar As New cProductos
        Dim ds As New DataSet
        ds = cargar.cGrillaSalidasOtros()
        gvOtros.DataSource = ds
        gvOtros.DataBind()
    End Sub
End Class