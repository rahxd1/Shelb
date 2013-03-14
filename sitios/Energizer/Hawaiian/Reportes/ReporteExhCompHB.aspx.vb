Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteExhCompHB
    Inherits System.Web.UI.Page

    Dim PromotorSel, RegionSel, CadenaSel As String
    Dim PeriodoSQL, PromotorSQL, RegionSQL, CadenaSQL, TiendaSQL As String

    Sub SQLCombo()
        If cmbRegion.SelectedValue <> "" Then
            If cmbRegion.SelectedValue <> 0 Then
                RegionSel = "AND REG.id_region=" & cmbRegion.SelectedValue & " " : Else
                RegionSel = "" : End If : End If

        If Not cmbPromotor.Text = "" Then
            PromotorSel = "AND RUT.id_usuario='" & cmbPromotor.SelectedValue & "'" : Else
            PromotorSel = "" : End If

        If Not cmbCadena.Text = "" Then
            CadenaSel = "AND CAD.id_cadena=" & cmbCadena.SelectedValue & "" : Else
            CadenaSel = "" : End If

        PeriodoSQL = "SELECT id_periodo, nombre_periodo " & _
                    "FROM HawaiianBanana_Periodos ORDER BY fecha_inicio DESC"

        RegionSQL = "SELECT DISTINCT nombre_region, id_region " & _
                    "FROM Regiones WHERE id_region<>0 ORDER BY nombre_region"

        PromotorSQL = "SELECT DISTINCT RUT.id_usuario " & _
                    "FROM HawaiianBanana_CatRutas AS RUT " & _
                    "INNER JOIN HawaiianBanana_Tiendas AS TI ON TI.id_tienda = RUT.id_tienda " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    "WHERE TI.estatus = 1 " & _
                    " " + RegionSel + " " & _
                    " ORDER BY RUT.id_usuario"

        CadenaSQL = "SELECT DISTINCT CAD.id_cadena, CAD.nombre_cadena " & _
                    "FROM HawaiianBanana_CatRutas AS RUT " & _
                    "INNER JOIN HawaiianBanana_Tiendas AS TI ON TI.id_tienda = RUT.id_tienda " & _
                    "INNER JOIN Cadenas_Tiendas AS CAD ON TI.id_cadena = CAD.id_cadena " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    "WHERE TI.estatus = 1 " & _
                    " " + RegionSel + " " & _
                    " " + PromotorSel + " " & _
                    " ORDER BY CAD.nombre_cadena"

        TiendaSQL = "SELECT DISTINCT RUT.id_tienda, TI.nombre " & _
                    "FROM HawaiianBanana_CatRutas AS RUT " & _
                    "INNER JOIN HawaiianBanana_Tiendas AS TI ON TI.id_tienda = RUT.id_tienda " & _
                    "INNER JOIN Cadenas_Tiendas AS CAD ON TI.id_cadena = CAD.id_cadena " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    " " + RegionSel + " " & _
                    " " + PromotorSel + " " & _
                    " " + CadenaSel + " " & _
                    " ORDER BY TI.nombre"
    End Sub

    Sub CargarReporte()
        If cmbRegion.SelectedValue <> 0 Then
            RegionSQL = "AND REG.id_region=" & cmbRegion.SelectedValue & " : Else"
            RegionSQL = "" : End If

        If Not cmbPromotor.SelectedValue = "" Then
            PromotorSQL = "AND H.id_usuario='" & cmbPromotor.SelectedValue & "' " : Else
            PromotorSQL = "" : End If

        If Not cmbCadena.SelectedValue = "" Then
            CadenaSQL = "AND TI.id_cadena=" & cmbCadena.SelectedValue & " " : Else
            CadenaSQL = "" : End If

        If Not cmbTienda.SelectedValue = "" Then
            TiendaSQL = "AND TI.id_tienda=" & cmbTienda.SelectedValue & " " : Else
            TiendaSQL = "" : End If

        Dim Marca1, Marca2, NombreMarca1, NombreMarca2 As Integer

        If cmbMarca.SelectedValue = 1 Then
            Marca1 = 1
            Marca2 = 2
            NombreMarca1 = "Hawaiian"
            NombreMarca2 = "Banana"
        End If
        If cmbMarca.SelectedValue = 2 Then
            Marca1 = 2
            Marca2 = 1
            NombreMarca1 = "Banana"
            NombreMarca2 = "Hawaiian"
        End If

        CargaGrilla(ConexionEnergizer.localSqlServer, _
                    "SELECT DISTINCT REG.nombre_region as Region, CAD.nombre_cadena as Cadena, TI.nombre as Tienda, " & _
                    "HDET1.cantidad as " & NombreMarca1 & ", HDET2.cantidad_exhibidor as " & NombreMarca2 & ", " & _
                    "HDET3.cantidad_exhibidor as Coppertone, HDET4.cantidad_exhibidor as Nivea, HDET5.cantidad_exhibidor as Otras " & _
                    "FROM HawaiianBanana_Historial as H  " & _
                    "FULL JOIN (select DISTINCT H.id_tienda, H.folio_historial, sum(HDET.cantidad) as cantidad FROM HawaiianBanana_Historial as H " & _
                    "INNER JOIN HawaiianBanana_Exhibidores_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial " & _
                    "WHERE H.id_periodo = " & cmbPeriodo.SelectedValue & " AND H.id_marca= " & Marca1 & " GROUP BY H.id_tienda, H.folio_historial) as HDET1 " & _
                    "ON H.id_tienda = HDET1.id_tienda " & _
                    "FULL JOIN (select H.id_tienda, H.folio_historial, HDET.cantidad_exhibidor FROM HawaiianBanana_Historial as H " & _
                    "INNER JOIN HawaiianBanana_Competencia_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial " & _
                    "WHERE H.id_periodo = " & cmbPeriodo.SelectedValue & " AND HDET.id_marca =" & Marca2 & " AND H.id_marca= " & Marca1 & ") as HDET2 " & _
                    "ON H.id_tienda = HDET2.id_tienda " & _
                    "FULL JOIN (select H.id_tienda, H.folio_historial, HDET.cantidad_exhibidor FROM HawaiianBanana_Historial as H " & _
                    "INNER JOIN HawaiianBanana_Competencia_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial " & _
                    "WHERE H.id_periodo = " & cmbPeriodo.SelectedValue & " AND HDET.id_marca =3 AND H.id_marca=" & Marca1 & ") as HDET3 " & _
                    "ON H.id_tienda = HDET3.id_tienda " & _
                    "FULL JOIN (select H.id_tienda, H.folio_historial, HDET.cantidad_exhibidor FROM HawaiianBanana_Historial as H " & _
                    "INNER JOIN HawaiianBanana_Competencia_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial " & _
                    "WHERE H.id_periodo = " & cmbPeriodo.SelectedValue & " AND HDET.id_marca =4 AND H.id_marca=" & Marca1 & ") as HDET4 " & _
                    "ON H.id_tienda = HDET4.id_tienda " & _
                    "FULL JOIN (select H.id_tienda, H.folio_historial, HDET.cantidad_exhibidor FROM HawaiianBanana_Historial as H " & _
                    "INNER JOIN HawaiianBanana_Competencia_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial " & _
                    "WHERE H.id_periodo = " & cmbPeriodo.SelectedValue & " AND HDET.id_marca =5 AND H.id_marca=" & Marca1 & ") as HDET5 " & _
                    "ON H.id_tienda = HDET5.id_tienda " & _
                    "INNER JOIN HawaiianBanana_Tiendas as TI ON TI.id_tienda= H.id_tienda " & _
                    "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON TI.id_cadena= CAD.id_cadena " & _
                    "WHERE H.id_periodo = " & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + " " & _
                    " " + PromotorSQL + " " & _
                    " " + CadenaSQL + " " & _
                    " " + TiendaSQL + " " & _
                    "ORDER BY REG.nombre_region,CAD.nombre_cadena, TI.nombre", Me.gridReporte)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

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
        Response.AddHeader("Content-Disposition", "attachment;filename=ReporteExhibidoresCompetencia" + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Protected Sub cmbMarca_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbMarca.SelectedIndexChanged
        CargarReporte()
    End Sub

End Class