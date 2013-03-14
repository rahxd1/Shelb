Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class DetalleEnergizerProducto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim IDProducto, Periodo As String
            IDProducto = Request.Params("id")
            Periodo = Request.Params("periodo")

            Dim Filtro As String
            Filtro = "AND HDET.id_producto=" + IDProducto

            CargaGrilla(ConexionEnergizer.localSqlServer, _
                        "SELECT REG.nombre_region, CAD.nombre_cadena,H.id_periodo,  H.id_usuario,Ti.nombre,HDET.id_producto,PROD.nombre_producto, HDET.precio " & _
                        "FROM Energizer_DP_Productos_Historial as H " & _
                        "INNER JOIN Energizer_DP_Productos_Historial_Det as HDET " & _
                        "ON H.folio_historial= HDET.folio_historial " & _
                        "INNER JOIN Energizer_DP_Tiendas as TI  " & _
                        "ON TI.id_tienda= H.id_tienda  " & _
                        "INNER JOIN Regiones as REG  " & _
                        "ON REG.id_region= TI.id_region  " & _
                        "INNER JOIN Energizer_DP_Productos as PROD " & _
                        "ON PROD.id_producto = HDET.id_producto " & _
                        "INNER JOIN Cadenas_Tiendas as CAD " & _
                        "ON TI.id_cadena= CAD.id_cadena " & _
                        "WHERE HDET.precio<>'0' " & _
                        "AND H.id_periodo='" & Periodo & "' " & _
                        " " + Filtro + " " & _
                        "ORDER BY REG.id_region, H.id_usuario, Ti.nombre", Me.gridDetalle)
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Detalle Productos.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class