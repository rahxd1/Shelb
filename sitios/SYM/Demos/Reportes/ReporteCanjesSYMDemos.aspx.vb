Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteCanjesSYMDemos
    Inherits System.Web.UI.Page

    Dim PeriodoSel, RegionSel, PlazaSel As String
    Dim PeriodoSQL, PromotorSQL, RegionSQL, PlazasSQL, CadenaSQL As String

    Sub SQLCombo()
        PeriodoSel = Acciones.Slc.cmb("H.id_periodo", cmbPeriodo.SelectedValue)
        RegionSel = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)
        PlazaSel = Acciones.Slc.cmb("US.plaza", cmbCiudad.SelectedValue)

        PeriodoSQL = "SELECT id_periodo, nombre_periodo " & _
                         "FROM Demos_Periodos " & _
                         " ORDER BY fecha_inicio DESC"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Regiones as REG " & _
                    "WHERE REG.id_region<>0 " & _
                    " ORDER BY REG.nombre_region"

        PlazasSQL = "SELECT DISTINCT US.plaza " & _
                    "FROM Usuarios as US " & _
                    "INNER JOIN Demos_Historial as H ON H.id_usuario=US.id_usuario " & _
                    "WHERE US.plaza<>'' " & _
                    " " + PeriodoSel + RegionSel + " " & _
                    " ORDER BY US.plaza"

        CadenaSQL = "SELECT DISTINCT CAD.id_cadena, CAD.nombre_cadena " & _
                    "FROM Demos_Historial as H " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = H.id_cadena " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario = H.id_usuario " & _
                    "WHERE CAD.id_cadena<>0 " & _
                    " " + PeriodoSel + RegionSel + PlazaSel + " ORDER BY CAD.nombre_cadena "

        PromotorSQL = "SELECT DISTINCT H.id_usuario " & _
                    "FROM Demos_Historial as H " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario=H.id_usuario " & _
                    "WHERE US.id_usuario<>'' " & _
                    " " + PeriodoSel + RegionSel + PlazaSel + " " & _
                    " ORDER BY H.id_usuario "
    End Sub

    Sub CargarReporte()
        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("US.id_region", cmbRegion.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)
            PlazasSQL = Acciones.Slc.cmb("US.plaza", cmbCiudad.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("H.id_cadena", cmbCadena.SelectedValue)

            CargaGrilla(ConexionSYM.localSqlServer, _
                        "select REG.nombre_region,US.plaza,nombre_tienda, CAD.nombre_cadena,  " & _
                        "HDET.no_ticket, HDET.nombre_cliente, HDET.telefono, HDET.toallas, " & _
                        "PROD.[1],PROD.[2],PROD.[3],PROD.[4] " & _
                        "FROM Demos_Historial as H " & _
                        "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena=H.id_cadena " & _
                        "INNER JOIN Demos_Canjes_Historial_Det as HDET ON H.folio_historial=HDET.folio_historial " & _
                        "INNER JOIN (SELECT * " & _
                        "FROM Demos_Productos_Canjes_Historial_Det ) PVT " & _
                        "PIVOT(SUM(ventas) FOR id_producto " & _
                        "IN([1],[2],[3],[4]))as PROD " & _
                        "ON PROD.folio_historial_det=HDET.folio_historial_det " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario=H.id_usuario " & _
                        "INNER JOIN Regiones as REG ON REG.id_region=US.id_region " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + PlazasSQL + PromotorSQL + CadenaSQL + " " & _
                        "ORDER BY REG.nombre_region,US.plaza,H.id_usuario,CAD.nombre_cadena,H.nombre_tienda ", _
                        gridReporte)
        Else
            gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionSYM.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, PlazasSQL, "plaza", "plaza", cmbCiudad)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
        End If
    End Sub

    Protected Sub cmbCiudad_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCiudad.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, PlazasSQL, "plaza", "plaza", cmbCiudad)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, PlazasSQL, "plaza", "plaza", cmbCiudad)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte canjes " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCadena.SelectedIndexChanged
        CargarReporte()
    End Sub

    Private Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarReporte()
    End Sub
End Class