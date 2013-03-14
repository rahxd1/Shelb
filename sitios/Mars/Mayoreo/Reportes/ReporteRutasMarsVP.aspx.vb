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

Partial Public Class ReporteRutasMarsVP
    Inherits System.Web.UI.Page

    Dim PromotorSQL, RegionSQL As String

    Sub SQLCombo()
        Dim PromotorSel, RegionSel As String
        RegionSel = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
        PromotorSel = Acciones.Slc.cmb("RE.id_usuario", cmbPromotor.SelectedValue)

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Mayoreo_Verificadores_CatRutas AS RE " & _
                    "INNER JOIN Mayoreo_Tiendas AS TI ON TI.id_tienda = RE.id_tienda " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    "WHERE REG.id_region <>0  " & _
                    " ORDER BY REG.nombre_region"

        PromotorSQL = "SELECT DISTINCT RE.id_usuario " & _
                    "FROM Mayoreo_Verificadores_CatRutas AS RE " & _
                    "INNER JOIN Mayoreo_Tiendas AS TI ON TI.id_tienda = RE.id_tienda " & _
                    "WHERE TI.id_tienda<>0 " & _
                    " " + RegionSel + " " & _
                    " ORDER BY RE.id_usuario"
    End Sub

    Sub CargarReporte()
        RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
        PromotorSQL = Acciones.Slc.cmb("RUT.id_usuario", cmbPromotor.SelectedValue)

        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT DISTINCT REG.nombre_region, RUT.id_usuario, TI.codigo,CAD.nombre_cadena, TI.nombre,TI.ciudad, " & _
                    "ES.nombre_estado,TTI.nombre_tipo,TR.nombre_top_rc,TI.codigo, " & _
                    "RUT.id_tienda, RUT1.dia1, RUT2.dia2, RUT3.dia3, RUT4.dia4,Ti.top_rc,''ejecutivo " & _
                    "From Mayoreo_Tiendas as TI  " & _
                    "INNER JOIN (SELECT id_usuario,id_tienda  " & _
                    "FROM Mayoreo_Verificadores_CatRutas)as RUT ON TI.id_tienda = RUT.id_tienda  " & _
                    "FULL JOIN (SELECT id_tienda, CASE id_dia when 'D1' then 'X' end as dia1  " & _
                    "FROM Mayoreo_Verificadores_CatRutas WHERE id_dia='D1')as RUT1 ON RUT.id_tienda = RUT1.id_tienda " & _
                    "FULL JOIN (SELECT id_tienda, CASE id_dia when 'D2' then 'X' end as dia2  " & _
                    "FROM Mayoreo_Verificadores_CatRutas WHERE id_dia='D2')as RUT2 ON RUT.id_tienda = RUT2.id_tienda  " & _
                    "FULL JOIN (SELECT id_tienda, CASE id_dia when 'D3' then 'X' end as dia3  " & _
                    "FROM Mayoreo_Verificadores_CatRutas WHERE id_dia='D3')as RUT3 ON RUT.id_tienda = RUT3.id_tienda  " & _
                    "FULL JOIN (SELECT id_tienda, CASE id_dia when 'D4' then 'X' end as dia4   " & _
                    "FROM Mayoreo_Verificadores_CatRutas WHERE id_dia='D4')as RUT4 ON RUT.id_tienda = RUT4.id_tienda  " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    "INNER JOIN Cadenas_Mayoreo as CAD ON CAD.id_cadena = TI.id_cadena " & _
                    "INNER JOIN Estados as ES ON TI.id_estado = ES.id_estado " & _
                    "FULL JOIN Tipo_Tiendas_Mayoreo as TTI ON TTI.tipo_tienda = TI.tipo_tienda " & _
                    "INNER JOIN Top_rc as TR ON TR.top_rc=TI.top_rc " & _
                    "WHERE TI.id_tienda<>0 " & _
                    " " + RegionSQL + PromotorSQL + " " & _
                    "ORDER BY REG.nombre_region, CAD.nombre_cadena, Ti.nombre", gridReporte)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

            CargarReporte()
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Rutas.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class