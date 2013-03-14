Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteEnergizerDPPrecios
    Inherits System.Web.UI.Page

    Dim PromotorSel, RegionSel, CadenaSel As String
    Dim PeriodoSQL, PromotorSQL, RegionSQL, CadenaSQL, TiendaSQL, TipoPilasSQL As String

    Sub SQLCombo()
        If cmbRegion.SelectedValue <> "" Then
            If cmbRegion.SelectedValue <> 0 Then
                RegionSel = "AND REG.id_region=" & cmbRegion.SelectedValue & " " : Else
                RegionSel = "" : End If : End If

        If cmbPromotor.SelectedValue = "" Then
            PromotorSel = "" : Else
            PromotorSel = " AND RUT.id_usuario = '" & cmbPromotor.SelectedValue & "' " : End If

        If cmbCadena.SelectedValue = "" Then
            CadenaSel = "" : Else
            CadenaSel = " AND CAD.id_cadena=" & cmbCadena.SelectedValue & " " : End If

        PeriodoSQL = "SELECT id_periodo, nombre_periodo " & _
                         "FROM Energizer_DP_Periodos " & _
                         " ORDER BY fecha_inicio DESC"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Regiones as REG " & _
                    "INNER JOIN Energizer_DP_Tiendas AS TI ON TI.id_region = REG.id_region " & _
                    "WHERE REG.id_region<>0 ORDER BY REG.nombre_region"

        PromotorSQL = "SELECT DISTINCT US.id_usuario " & _
                    "FROM Usuarios AS US " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = US.id_region " & _
                    "WHERE US.id_proyecto= 11 " & _
                    " " + RegionSel + " " & _
                    " ORDER BY US.id_usuario"

        CadenaSQL = "SELECT DISTINCT CAD.id_cadena, CAD.nombre_cadena " & _
                    "FROM Cadenas_Tiendas as CAD " & _
                    "INNER JOIN Energizer_DP_Tiendas AS TI ON TI.id_cadena = CAD.id_cadena " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    "INNER JOIN Energizer_DP_CatRutas as RUT ON RUT.id_tienda = TI.id_tienda " & _
                    " " + RegionSel + " " & _
                    " " + PromotorSel + " " & _
                    " ORDER BY CAD.nombre_cadena"

        TiendaSQL = "SELECT DISTINCT TI.id_tienda, TI.nombre " & _
                   "FROM Energizer_DP_Tiendas AS TI " & _
                   "INNER JOIN Cadenas_Tiendas as CAD ON TI.id_cadena = CAD.id_cadena " & _
                   "INNER JOIN Energizer_DP_CatRutas as RUT ON RUT.id_tienda = TI.id_tienda " & _
                   "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                   " " + RegionSel + " " & _
                   " " + PromotorSel + " " & _
                   " " + CadenaSel + " " & _
                   " ORDER BY TI.nombre"

        TipoPilasSQL = "SELECT DISTINCT grupo FROM Energizer_DP_Productos ORDER BY grupo"
    End Sub

    Sub CargarReporte()
        If Not cmbPeriodo.SelectedValue = "" Then
            PeriodoSQL = "AND H.id_periodo=" & cmbPeriodo.SelectedValue & " " : Else
            PeriodoSQL = "" : End If

        If cmbRegion.SelectedValue <> 0 Then
            RegionSQL = "AND TI.id_region=" & cmbRegion.SelectedValue & " " : Else
            RegionSQL = "" : End If

        If Not cmbPromotor.SelectedValue = "" Then
            PromotorSQL = "AND H.id_usuario = '" & cmbPromotor.SelectedValue & "' " : Else
            PromotorSQL = "" : End If

        If Not cmbCadena.SelectedValue = "" Then
            CadenaSQL = "AND TI.id_cadena=" & cmbCadena.SelectedValue & " " : Else
            CadenaSQL = "" : End If

        If Not cmbTienda.SelectedValue = "" Then
            TiendaSQL = "AND TI.id_tienda=" & cmbTienda.SelectedValue & " " : Else
            TiendaSQL = "" : End If

        If Not cmbTipoPilas.SelectedValue = "" Then
            TipoPilasSQL = "AND PROD.grupo=" & cmbTipoPilas.SelectedValue & " " : Else
            TipoPilasSQL = "" : End If

        CargaGrilla(ConexionEnergizer.localSqlServer, _
                    "SELECT PROD.nombre_producto,PER.nombre_periodo,H.id_periodo, HDET.id_producto, " & _
                    "MIN(HDET.precio) as PrecioMin, MAX(HDET.precio) as PrecioMax, Round(AVG(HDET.precio),2) as PrecioProm " & _
                    "FROM Energizer_DP_Productos_Historial as H " & _
                    "INNER JOIN Energizer_DP_Productos_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial " & _
                    "INNER JOIN Energizer_DP_Tiendas as TI ON TI.id_tienda= H.id_tienda  " & _
                    "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region  " & _
                    "INNER JOIN Energizer_DP_Periodos as PER ON H.id_periodo= PER.id_periodo " & _
                    "INNER JOIN Energizer_DP_Productos as PROD ON PROD.id_producto = HDET.id_producto " & _
                    "WHERE HDET.precio <>'0' " & _
                   " " + PeriodoSQL + " " & _
                    " " + RegionSQL + " " & _
                    " " + PromotorSQL + " " & _
                    " " + CadenaSQL + " " & _
                    " " + TiendaSQL + " " & _
                    " " + TipoPilasSQL + " " & _
                    "GROUP BY PROD.nombre_producto,PER.nombre_periodo,H.id_periodo, HDET.id_producto ", _
                    Me.gridReporte)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, TipoPilasSQL, "grupo", "grupo", cmbTipoPilas)

            CargarReporte()
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionEnergizer.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionEnergizer.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionEnergizer.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionEnergizer.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionEnergizer.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCadena.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionEnergizer.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbTienda_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbTienda.SelectedIndexChanged
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Precios " + cmbPeriodo.SelectedItem.ToString() + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Protected Sub cmbTipoPilas_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbTipoPilas.SelectedIndexChanged
        CargarReporte()
    End Sub

End Class