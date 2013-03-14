Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class DetallePrecioJovy
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim IDProducto, Precio, Periodo As String
            IDProducto = Request.Params("id")
            Precio = Request.Params("precio")
            Periodo = Request.Params("periodo")

            Dim Filtro As String
            Filtro = "AND HDET.id_producto =" + IDProducto + " AND HDET.precio=" + Precio

            CargaGrilla(ConexionJovy.localSqlServer, _
                        "SELECT REG.nombre_region, CAD.nombre_cadena,H.id_periodo, H.id_usuario,TI.nombre,HDET.id_producto,PROD.nombre_producto, HDET.precio " & _
                        "FROM Jovy_Historial as H " & _
                        "INNER JOIN Jovy_Productos_Historial_Det as HDET " & _
                        "ON H.folio_historial= HDET.folio_historial " & _
                        "INNER JOIN Jovy_Productos as PROD ON PROD.id_producto = HDET.id_producto " & _
                        "INNER JOIN Jovy_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                        "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena= TI.id_cadena " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario = H.id_usuario " & _
                        "INNER JOIN Regiones as REG ON REG.id_region= US.id_region " & _
                        "WHERE HDET.precio <>'0' " & _
                        "AND H.id_periodo='" & Periodo & "' " & _
                        " " + Filtro + " " & _
                        "ORDER BY REG.id_region, H.id_usuario, TI.nombre", Me.gridDetalle)
        End If
    End Sub

    Protected Sub btnExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExcel.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.gridDetalle.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.gridDetalle)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Detalle Precios.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class