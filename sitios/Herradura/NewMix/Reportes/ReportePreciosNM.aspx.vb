Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReportePreciosNM
    Inherits System.Web.UI.Page

    Dim RegionSel As String
    Dim PeriodoSQL As String

    Sub SQLCombo()

        PeriodoSQL = "SELECT DISTINCT PER.id_periodo, PER.nombre_periodo " & _
                    "FROM NewMix_Periodos as PER " & _
                    "INNER JOIN NewMix_Historial_Precios as H ON H.id_periodo=PER.id_periodo " & _
                    "ORDER BY PER.id_periodo DESC"
    End Sub

    Sub CargarReporte()
        If cmbPeriodo.SelectedValue <> "" Then
            CargaGrilla(ConexionHerradura.localSqlServer, _
                        "SELECT REG.nombre_region, H.nombre_cadena, " & _
                        "ROUND(ISNULL([100],0),2)[100],ROUND(ISNULL([101],0),2)[101],ROUND(ISNULL([102],0),2)[102], " & _
                        "ROUND(ISNULL([103],0),2)[103],ROUND(ISNULL([104],0),2)[104],ROUND(ISNULL([105],0),2)[105], " & _
                        "ROUND(ISNULL([106],0),2)[106],ROUND(ISNULL([107],0),2)[107],ROUND(ISNULL([108],0),2)[108], " & _
                        "ROUND(ISNULL([109],0),2)[109],ROUND(ISNULL([110],0),2)[110],ROUND(ISNULL([111],0),2)[111], " & _
                        "ROUND(ISNULL([112],0),2)[112],ROUND(ISNULL([113],0),2)[113],ROUND(ISNULL([114],0),2)[114], " & _
                        "ROUND(ISNULL([115],0),2)[115],ROUND(ISNULL([116],0),2)[116],ROUND(ISNULL([117],0),2)[117], " & _
                        "ROUND(ISNULL([118],0),2)[118],ROUND(ISNULL([119],0),2)[119],ROUND(ISNULL([120],0),2)[120], " & _
                        "ROUND(ISNULL([121],0),2)[121],ROUND(ISNULL([122],0),2)[122],ROUND(ISNULL([123],0),2)[123], " & _
                        "ROUND(ISNULL([124],0),2)[124],ROUND(ISNULL([125],0),2)[125],ROUND(ISNULL([126],0),2)[126], " & _
                        "ROUND(ISNULL([127],0),2)[127],ROUND(ISNULL([128],0),2)[128],ROUND(ISNULL([129],0),2)[129], " & _
                        "ROUND(ISNULL([130],0),2)[130],ROUND(ISNULL([131],0),2)[131],ROUND(ISNULL([132],0),2)[132], " & _
                        "ROUND(ISNULL([133],0),2)[133],ROUND(ISNULL([134],0),2)[134],ROUND(ISNULL([135],0),2)[135], " & _
                        "ROUND(ISNULL([136],0),2)[136],ROUND(ISNULL([137],0),2)[137],ROUND(ISNULL([138],0),2)[138], " & _
                        "ROUND(ISNULL([139],0),2)[139],ROUND(ISNULL([140],0),2)[140],ROUND(ISNULL([141],0),2)[141], " & _
                        "ROUND(ISNULL([142],0),2)[142],ROUND(ISNULL([143],0),2)[143],ROUND(ISNULL([144],0),2)[144], " & _
                        "ROUND(ISNULL([145],0),2)[145],ROUND(ISNULL([146],0),2)[146],ROUND(ISNULL([147],0),2)[147], " & _
                        "ROUND(ISNULL([148],0),2)[148],ROUND(ISNULL([149],0),2)[149],ROUND(ISNULL([150],0),2)[150], " & _
                        "ROUND(ISNULL([151],0),2)[151] " & _
                        "FROM (SELECT CAD.nombre_cadena,US.id_region,HDET.id_producto, HDET.precio " & _
                        "FROM NewMix_Historial_Precios as H " & _
                        "INNER JOIN NewMix_Precios_Historial_Det as HDET ON H.folio_historial=HDET.folio_historial " & _
                        "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena=H.id_cadena " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario=H.id_usuario " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & ")PVT " & _
                        "PIVOT(AVG(precio) FOR id_producto  " & _
                        "IN([100],[101],[102],[103],[104],[105],[106],[107],[108],[109],[110],[111],[112],[113],[114],[115], " & _
                        "[116],[117],[118],[119],[120],[121],[122],[123],[124],[125],[126],[127],[128],[129], " & _
                        "[130],[131],[132],[133],[134],[135],[136],[137],[138],[139],[140],[141],[142],[143], " & _
                        "[144],[145],[146],[147],[148],[149],[150],[151])) AS H " & _
                        "INNER JOIN Regiones as REG ON REG.id_region=H.id_region", Me.gridReporte)
        Else
            gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo)

        End If
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()
        CargarReporte()
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Sin pedidos por Tiendas " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

End Class