Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteTiendaxProductos
    Inherits System.Web.UI.Page

    Dim PromotorSel, RegionSel, CadenaSel As String
    Dim PeriodoSQL, PromotorSQL, RegionSQL, CadenaSQL, TiendaSQL As String
    Dim CamposTitulos, Campos As String

    Sub SQLCombo()
        If cmbRegion.SelectedValue <> "" Then
            If cmbRegion.SelectedValue <> 0 Then
                RegionSel = "AND REG.id_region=" & cmbRegion.SelectedValue & " " : Else
                RegionSel = "" : End If : End If

        If Not cmbPromotor.SelectedValue = "" Then
            PromotorSel = "AND RUT.id_usuario='" & cmbPromotor.SelectedValue & "'" : Else
            PromotorSel = "" : End If

        If Not cmbCadena.SelectedValue = "" Then
            CadenaSel = "AND CAD.id_cadena=" & cmbCadena.SelectedValue & " " : Else
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
            RegionSQL = "AND REG.id_region=" & cmbRegion.SelectedValue & " " : Else
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

        If cmbMarca.SelectedValue = 1 Then
            CamposTitulos = "[100]as'HT FP 70 OZ 240ML',[101]as 'HT FP 80 OZ 120ML',[102]as 'HT FP 60 PG 240ML', " & _
                        "[103]as 'HT FP 45 PG 240ML',[104]as 'HT FP30 LIP GLOSS ',[105]as 'HT FP 4 BR/SPRAY 240ML', " & _
                        "[106]as 'HT FP 15 BR/SPRAY 240ML',[107]as 'HT FP 10 BR/ZAN 240ML',[108]as 'HT FP80 BABY 240ML', " & _
                        "[109]as 'HT AFTS ALOE/GL 240ML',[110]as 'HT AFTS ICE/GL 240ML',[111]as 'HT FP 60 PG 120ML',[112]as 'HT FP 45 PG 120ML',[113]as 'HT FP80 BABY 120ML',[1]as 'COLGUIJES',[2]as 'ESPEJOS', " & _
                        "[3]as 'GORRAS',[4]as 'MORRALES',[5]as 'VASITOS PLAYTEX',[6]as 'HT FP45 BABY 120ML BARBI',[7]as 'HT FP30 BABY 120ML BARBI', " & _
                        "[8]as 'HT AUTOBRON GLOW 240ML',[9]as 'HT FP4 ZANAHO/GEL 240ML',[10]as 'HT FP15 ZANAHO/GEL 240ML',[11]as 'HT FP30 FACIAL 109 ML', " & _
                        "[12]as 'HT AFTS TROP 240ML',[13]as 'HT FP30 FACIALCOLOR 109ML',[14]as 'HT AFTS SOFT 240ML',[15] as 'HT FP30 FACIALMEN 109 ML', " & _
                        "[16]as 'HT FP50 OZONO/STIK 90ML',[31]as 'MORRAL NYLON',[32]as 'MORRAL MANTA', [33]as 'MORRAL CAFE', [34]as 'HT FP45 BABY  70 120ML BARBI', [35]as 'HT TAN 8 240 ML', [36]as 'HT KIDS SPRAY 45 240 ML'"
            Campos = "[100],[101],[102],[103],[104],[105],[106],[107],[108],[109],[110],[111],[112],[113],[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[31],[32],[33],[34],[35],[36]"
        Else
            CamposTitulos = "[200]as 'FPS 30 236ML',[201]as 'ULTRA MIST FPS 30 DE 177ML',[202]as 'UD FPS 50 UM 177ML', " & _
                    "[203]as 'FPS 50 236ml',[204] as 'ULTRA DEFENSE FPS 100 118 ML',[205]as 'UD FPS 110 UM 177ML', " & _
                    "[206]as 'NINOS FPS 50 118 ML',[207]as 'NINOS CON FPS 50 236ML',[208] as 'NIÑOS ULTRA MIST FPS50 177ML', " & _
                    "[209]as 'KIDS FPS UM 60 177 ML',[210]as 'KIDS SPRAY FPS 50 180 ML TF',[211]as 'KIDS UM FPS 110', " & _
                    "[212]as 'KIDS FPS 100 118ML',[213]as 'BB BABY SPF 80 120 ML',[214]as 'BB BABY SPRAY TF SPF 50 180 ML', " & _
                    "[215]as 'BB BABY SPRAY TF SPF 60 180 ML',[216]as 'BABY FPS 100',[217]as 'SPORT UltraMist SPF 30 177 ml', " & _
                    "[218]as 'SPORT CON SPF 50 118ML',[219] as 'SPORT ULTRA MIST FPS 50 ',[220]as 'ACEITE FPS 4 236ML'," & _
                    "[221]as 'ACEITE SPF8 236ML ',[222]as 'ACEITE FPS 15 236ML',[223]as 'FPS 15 236ML',[224]as 'ACEITE CONTINIUS SPRAY SPF 4 117ML', " & _
                    "[225]as 'UM FPS 6',[226]as 'GEL DE ALOE VERA 453GR',[227]as 'GEL DE VITAMINA E 453GR',[228]as 'FPS 30 118ML',[229]as 'UD FPS 50 60 ML', " & _
                    "[230]as 'UD FPS 50 60 ML',[231]as 'BEBES FPS 50 118ML',[232]as 'BEBES FPS 60 118ML',[233]as 'GEL DE ALOE VERA 230 gr',[234]as 'GEL DE ALIVIO RAPIDO SOOTH-A-CAINE 230GM', " & _
                    "[17]as 'MOCHILA SIN FORRO',[18]as 'FOAMI EN ESTUCHE DE(PLÁSTICO)',[19]as 'ESTUCHE PLASTICO',[20]as 'TAPETE NYLON ',[21]as 'BOLSA DE PLASTICO CON LOGOTIPO', " & _
                    "[22]as 'PISTOLAS DE AGUA',[23]as 'CUADERNOS',[24]as 'VASITOS PLAYTEX',[25]as 'BB FACES 88ML SPF 30',[26]as 'BB FP4 BRON LOT 236ML', " & _
                    "[27]as 'BB FP 30 KID 236 ML',[28]as 'PBBT PROT TNG SPF8 80Z',[29]as 'PBBT KIDS FPS30 236 SINLA',[30]as 'BB FPS 15 118 ML', [37]as 'VASO CON POPOTE'"
            Campos = "[200],[201],[202],[203],[204],[205],[206],[207],[208],[209],[210],[211],[212],[213], " & _
                    "[214],[215],[216],[217],[218],[219],[220],[221],[222],[223],[224],[225],[226],[227],[228],[229],[230],[231],[232],[233],[234],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[37]"
        End If

        Dim IDPeriodo As Integer = cmbPeriodo.SelectedValue
        'ya que no estaba la estructura antes de la semana 3 del periodo se genero una modificacion agregando una tabla, asi que para nueva 
        'temporada, solo tomar en cuenta la condicion SQL en el ELSE
        Dim SQL As String
        If IDPeriodo <= 12 Then
            SQL = "SELECT DISTINCT nombre_region AS 'Región',id_usuario as Promotor,nombre_cadena as Cadena, nombre as Tienda,  " & _
                                " " + CamposTitulos + " " & _
                                "FROM (SELECT REG.nombre_region, H.id_usuario, CAD.nombre_cadena, TI.nombre, " & _
                                "H.id_producto, COUNT(H.id_producto) as Cantidad " & _
                                "From HawaiianBanana_Promos_Historial as H  " & _
                                "INNER JOIN HawaiianBanana_Tiendas as TI ON TI.id_tienda = H.id_tienda  " & _
                                "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                                "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = TI.id_cadena " & _
                                "WHERE H.id_periodo = " & cmbPeriodo.SelectedValue & " " & _
                                " " + RegionSQL + " " & _
                                " " + PromotorSQL + " " & _
                                " " + CadenaSQL + " " & _
                                " " + TiendaSQL + " " & _
                                "GROUP BY REG.nombre_region, H.id_usuario, CAD.nombre_cadena, TI.nombre, H.id_producto " & _
                                "UNION ALL " & _
                                "SELECT DISTINCT REG.nombre_region, H.id_usuario, CAD.nombre_cadena, TI.nombre, " & _
                                "HDET.id_promo as id_producto, SUM(HDET.cantidad) as cantidad " & _
                                "From HawaiianBanana_Promos_Historial as H " & _
                                "FULL JOIN HawaiianBanana_Promos_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial " & _
                                "INNER JOIN HawaiianBanana_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                                "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                                "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = TI.id_cadena " & _
                                "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                                " " + RegionSQL + " " & _
                                " " + PromotorSQL + " " & _
                                " " + CadenaSQL + " " & _
                                " " + TiendaSQL + " " & _
                                "GROUP BY REG.nombre_region, H.id_usuario, CAD.nombre_cadena, TI.nombre, HDET.id_promo" & _
                                ")AS Datos PIVOT(SUM(cantidad) " & _
                                "FOR id_producto IN (" + Campos + " )) AS PivotTable "
        Else

            SQL = "SELECT DISTINCT nombre_region AS 'Región',id_usuario as Promotor,nombre_cadena as Cadena, nombre as Tienda,   " & _
                                " " + CamposTitulos + " " & _
                                "FROM (SELECT REG.nombre_region, HT.id_usuario, CAD.nombre_cadena, TI.nombre, " & _
                                "H.id_producto, H.cantidad as cantidad " & _
                                "From HawaiianBanana_Productos_Historial_Det as H  " & _
                                "INNER JOIN HawaiianBanana_Historial AS HT on HT.folio_historial = H.folio_historial " & _
                                "INNER JOIN HawaiianBanana_Tiendas as TI ON TI.id_tienda = HT.id_tienda  " & _
                                "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                                "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = TI.id_cadena " & _
                                "WHERE HT.id_periodo = " & cmbPeriodo.SelectedValue & " " & _
                                " " + RegionSQL + " " & _
                                " " + PromotorSQL + " " & _
                                " " + CadenaSQL + " " & _
                                " " + TiendaSQL + " " & _
                                "GROUP BY REG.nombre_region, HT.id_usuario, CAD.nombre_cadena, TI.nombre, H.id_producto, H.cantidad " & _
                                "UNION ALL " & _
                                "SELECT DISTINCT REG.nombre_region, HP.id_usuario, CAD.nombre_cadena, TI.nombre, " & _
                                "HDET.id_promo as id_producto, SUM(HDET.cantidad) AS cantidad " & _
                                "From HawaiianBanana_Promos_Historial as HP " & _
                                "FULL JOIN HawaiianBanana_Promos_Historial_Det as HDET ON HP.folio_historial= HDET.folio_historial " & _
                                "INNER JOIN HawaiianBanana_Tiendas as TI ON TI.id_tienda = HP.id_tienda " & _
                                "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                                "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = TI.id_cadena " & _
                                "WHERE HP.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                                " " + RegionSQL + " " & _
                                " " + PromotorSQL + " " & _
                                " " + CadenaSQL + " " & _
                                " " + TiendaSQL + " " & _
                                "GROUP BY REG.nombre_region, HP.id_usuario, CAD.nombre_cadena, TI.nombre, HDET.id_promo, HDET.cantidad " & _
                                ")AS Datos PIVOT(SUM(cantidad) " & _
                                "FOR id_producto IN (" + Campos + " )) AS PivotTable "
        End If

        CargaGrilla(ConexionEnergizer.localSqlServer, SQL, Me.gridReporte)
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Productos y promocionales por tienda " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbMarca_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMarca.SelectedIndexChanged
        CargarReporte()
    End Sub

End Class