Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports ISODates.Dates
Imports System.Security.Cryptography
Imports InfoSoftGlobal

Partial Public Class ReporteBandaPreciosM
    Inherits System.Web.UI.Page

    Sub CargarReporte()
        CargaGrilla(ConexionMars.localSqlServer, _
                    "select nombre_tipo as 'Tipo tienda'," & _
                    "'$'+ISNULL(convert(nvarchar(8),cast(round([1],2) as decimal(9,2))),0.00)as'PAL PERRO 1/25 KG', " & _
                    "'$'+ISNULL(convert(nvarchar(8),cast(round([2],2) as decimal(9,2))),0.00)as'PEDIGREE CACHORRO 1/20 KG',  " & _
                    "'$'+ISNULL(convert(nvarchar(8),cast(round([5],2) as decimal(9,2))),0.00)as'PEDIGREE ADULTO NUTRICION COMPLETA 1/25 KG', " & _
                    "'$'+ISNULL(convert(nvarchar(8),cast(round([7],2) as decimal(9,2))),0.00)as'WHISKAS RECETA ORIGINAL / ORIGINAL RECIPE 1/20 KG' " & _
                    "FROM (select SEM.id_producto, TTI.nombre_tipo, precio " & _
                    "from Productos_Mayoreo_Semaforo as SEM " & _
                    "INNER JOIN Tipo_Tiendas_Mayoreo as TTI ON SEM.tipo_tienda= TTI.tipo_tienda " & _
                    "WHERE SEM.id_periodo='')AS Precios " & _
                    "PIVOT(AVG(precio)FOR id_producto IN ([1], [2], [5], [7])) AS H " & _
                    "order by nombre_tipo", gridReporte)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CargarReporte()
        End If
    End Sub

    Protected Sub lnkExportar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.gridReporte.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.gridReporte)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Banda de precios mayoreo mars.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

End Class