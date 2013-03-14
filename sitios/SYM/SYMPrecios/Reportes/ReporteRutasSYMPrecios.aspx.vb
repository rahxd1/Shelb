Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteRutasSYMPrecios
    Inherits System.Web.UI.Page

    Dim RegionSel, PlazaSel As String
    Dim PromotorSQL, RegionSQL, PlazasSQL, TipoCapturaSQL As String

    Sub SQLCombo()
        RegionSel = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)
        PlazaSel = Acciones.Slc.cmb("US.plaza", cmbCiudad.SelectedValue)

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Regiones as REG " & _
                    "WHERE REG.id_region<>0 " & _
                    " ORDER BY REG.nombre_region"

        PlazasSQL = "SELECT DISTINCT US.plaza " & _
                    "FROM Usuarios as US " & _
                    "INNER JOIN Precios_Historial as H ON H.id_usuario=US.id_usuario " & _
                    "WHERE US.plaza<>'' " & _
                    " " + RegionSel + " " & _
                    " ORDER BY US.plaza"

        PromotorSQL = "SELECT DISTINCT RE.id_usuario " & _
                    "FROM Precios_Rutas_Eventos as RE " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario=RE.id_usuario " & _
                    "WHERE US.id_usuario<>'' " & _
                    " " + RegionSel + PlazaSel + " " & _
                    " ORDER BY RE.id_usuario "
    End Sub

    Sub CargarReporte()
        RegionSQL = Acciones.Slc.cmb("US.id_region", cmbRegion.SelectedValue)
        PromotorSQL = Acciones.Slc.cmb("RUT.id_usuario", cmbPromotor.SelectedValue)
        PlazasSQL = Acciones.Slc.cmb("US.plaza", cmbCiudad.SelectedValue)
        TipoCapturaSQL = Acciones.Slc.cmb("RUT.semanal_mensual", cmbTipoCaptura.SelectedValue)

        CargaGrilla(ConexionSYM.localSqlServer, _
                    "select REG.nombre_region, US.plaza, RUT.id_usuario, CAD.nombre_cadena, " & _
                    "CASE WHEN RUT.semanal_mensual=1 then 'SEMANAL' else 'MENSUAL' end as Tipo " & _
                    "FROM Precios_CatRutas as RUT " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = RUT.id_cadena " & _
                    "INNER JOIN Usuarios as US ON RUT.id_usuario = US.id_usuario " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = US.id_region " & _
                    "WHERE REG.id_region<>0 " + RegionSQL + PlazasSQL + PromotorSQL + TipoCapturaSQL + " " & _
                    "ORDER BY REG.nombre_region, US.id_usuario, CAD.nombre_cadena, Tipo ", _
                    gridReporte)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionSYM.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, PlazasSQL, "plaza", "plaza", cmbCiudad)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, "select * from Precios_Tipo_Periodos", _
                            "nombre_tipo_periodo", "tipo_periodo", cmbTipoCaptura)

            CargarReporte()
        End If
    End Sub

    Protected Sub cmbPlazas_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCiudad.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, PlazasSQL, "plaza", "plaza", cmbCiudad)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

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
        Response.AddHeader("Content-Disposition", "attachment;filename=Ruta SYM Precios.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarReporte()
    End Sub

    Private Sub cmbTipoCaptura_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoCaptura.SelectedIndexChanged
        CargarReporte()
    End Sub
End Class