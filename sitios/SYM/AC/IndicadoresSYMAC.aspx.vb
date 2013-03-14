Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class IndicadoresSYMAC
    Inherits System.Web.UI.Page

    Dim PeriodoAct As Integer
    Dim VerificaFecha1, VerificaFecha2, VerificaFecha3 As String
    Dim CampoTitulo(50), CamposTitulos, Campo(50), Campos As String
    Dim CampoTituloC(50), CamposTitulosC, CampoC(50), CamposC As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionSYM.localSqlServer, HttpContext.Current.User.Identity.Name)

            'If Consulta = 1 Then
            VerPeriodosActivos()

            ''//CARGA GRAFICAS
            CargaGraficas()

            Combo.LlenaDrop(ConexionSYM.localSqlServer, "SELECT id_periodo, nombre_periodo FROM AC_Periodos ORDER BY fecha_inicio DESC", "nombre_periodo", "id_periodo", cmbPeriodo)
            ' Else
            '    Response.Redirect("Menu_Jovy.aspx")
            'End If
        End If
    End Sub

    Sub CargaGraficas()
        'Frentes()
        FrentesRegion(1, pnlFrentesDivision1)
        FrentesRegion(2, pnlFrentesDivision2)
        FrentesRegion(3, pnlFrentesDivision3)
        FrentesRegion(4, pnlFrentesDivision4)
        FrentesRegion(5, pnlFrentesDivision5)

        FrentesCadena()
        FrentesLinea()

        ParticipacionRegion()
        'Participacion(1, pnlParticipacion1)
        'Participacion(2, pnlParticipacion2)
        'Participacion(3, pnlParticipacion3)
        'Participacion(4, pnlParticipacion4)
        'Participacion(5, pnlParticipacion5)
        'Participacion(6, pnlParticipacion6)
        'Participacion(7, pnlParticipacion7)

        'HistoricoLinea()
        'HistoricoCatalogacion()
    End Sub

    Sub VerPeriodosActivos()
        Dim SQL As String
        If cmbPeriodo.Text = "" Then
            SQL = "SELECT * FROM AC_Periodos ORDER BY id_periodo DESC":Else
            SQL = "SELECT * FROM AC_Periodos WHERE id_periodo =" & cmbPeriodo.SelectedValue & "" : End If

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, SQL)

        If Tabla.Rows.Count > 0 Then
            PeriodoAct = Tabla.Rows(0)("id_periodo")
        End If

        cmbPeriodo.SelectedValue = PeriodoAct

        Tabla.Dispose()
    End Sub

    Public Function Participacion(ByVal TipoProducto As Integer, ByVal Panel As Panel) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT TPROD.nombre_tipoproducto, SYM.Frentes, Competencia.FrentesC " & _
                     "FROM Anaquel_Tipo_Productos as TPROD  " & _
                     "FULL JOIN (SELECT TPROD.id_tipo,SUM(HDET.frentes)as Frentes " & _
                     "FROM Anaquel_Historial as H  " & _
                     "INNER JOIN AC_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                     "INNER JOIN Anaquel_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial  " & _
                     "INNER JOIN Anaquel_Productos as PROD ON HDET.id_producto = PROD.id_producto " & _
                     "INNER JOIN Anaquel_Tipo_Productos as TPROD ON TPROD.id_tipo = PROD.id_tipo " & _
                     "WHERE H.id_periodo=" & PeriodoAct & " AND PROD.id_empresa =1 GROUP BY TPROD.id_tipo)SYM ON SYM.id_tipo= TPROD.id_tipo " & _
                     "FULL JOIN (SELECT TPROD.id_tipo,SUM(HDET.frentes)as FrentesC " & _
                     "FROM Anaquel_Historial as H  " & _
                     "INNER JOIN AC_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                     "INNER JOIN Anaquel_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial  " & _
                     "INNER JOIN Anaquel_Productos as PROD ON HDET.id_producto = PROD.id_producto " & _
                     "INNER JOIN Anaquel_Tipo_Productos as TPROD ON TPROD.id_tipo = PROD.id_tipo " & _
                     "WHERE H.id_periodo=" & PeriodoAct & " AND PROD.id_empresa<>1 GROUP BY TPROD.id_tipo)Competencia ON Competencia.id_tipo= TPROD.id_tipo " & _
                     "WHERE TPROD.id_tipo= " & TipoProducto & " " & _
                     "ORDER BY TPROD.nombre_tipoproducto ")
        Dim strXML As New StringBuilder()
        strXML.Append("<chart caption='" & tabla.Rows(0)("nombre_tipoproducto") & "' showPercentValues='1' showPercentageInLabel='1' pieSliceDepth='25' showBorder='1' showLabels='1'>")
        strXML.Append("<set label='SYM' value='" & tabla.Rows(0)("Frentes") & "' />")
        strXML.Append("<set label='Competencia' value='" & tabla.Rows(0)("FrentesC") & "'  />")
        strXML.Append("</chart>")

        Dim outPut As String = ""
        outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/Pie2D.swf", "", strXML.ToString(), "chart1", "270", "115", False, False)

        Panel.Controls.Clear()
        Panel.Controls.Add(New LiteralControl(outPut))

        tabla.Dispose()
    End Function

    Sub ParticipacionRegion()
        ''//CARGA GRAFICA
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT REG.nombre_region,SUM(HDET.frentes)as Frentes  " & _
                            "FROM Anaquel_Historial as H  " & _
                            "INNER JOIN AC_Tiendas as TI ON TI.id_tienda = H.id_tienda  " & _
                            "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region  " & _
                            "INNER JOIN Anaquel_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                            "INNER JOIN Anaquel_Productos as PROD ON HDET.id_producto = PROD.id_producto  " & _
                            "INNER JOIN Anaquel_Tipo_Productos as TPROD ON TPROD.id_tipo = PROD.id_tipo  " & _
                            "WHERE H.id_periodo=" & PeriodoAct & " AND PROD.id_empresa =1  " & _
                            "GROUP BY REG.nombre_region ")
        Dim strXML As New StringBuilder()
        strXML.Append("<chart caption='Porcentaje participación por Región' showPercentValues='1' showPercentageInLabel='1' pieSliceDepth='25' showBorder='1' showLabels='1'>")

        For i = 0 To tabla.Rows.Count - 1
            strXML.Append("<set label='" & tabla.Rows(i)("nombre_region") & "' value='" & tabla.Rows(i)("Frentes") & "'/>")
        Next i

        strXML.Append("</chart>")

        Dim outPut As String = ""
        outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/Pie2D.swf", "", strXML.ToString(), "chart1", "600", "230", False, False)

        pnlParticipacion.Controls.Clear()
        pnlParticipacion.Controls.Add(New LiteralControl(outPut))

        tabla.Dispose()
    End Sub

    Public Function FrentesRegion(ByVal Region As Integer, ByVal Panel As Panel) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT REG.nombre_region,TPROD.nombre_tipoproducto,SUM(HDET.frentes)as Frentes  " & _
                                    "FROM Anaquel_Historial as H   " & _
                                    "INNER JOIN AC_Tiendas as TI ON TI.id_tienda = H.id_tienda  " & _
                                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                                    "INNER JOIN Anaquel_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial   " & _
                                    "INNER JOIN Anaquel_Productos as PROD ON HDET.id_producto = PROD.id_producto  " & _
                                    "INNER JOIN Anaquel_Tipo_Productos as TPROD ON TPROD.id_tipo = PROD.id_tipo  " & _
                                    "WHERE H.id_periodo=" & PeriodoAct & "  AND PROD.id_empresa =1 AND REG.id_region =" & Region & "  " & _
                                    "GROUP BY REG.nombre_region,TPROD.nombre_tipoproducto ORDER BY TPROD.nombre_tipoproducto")
        If Tabla.Rows.Count > 0 Then
            Dim strXML As New StringBuilder()
            strXML.Append("<chart caption='Frentes por linea en " & Tabla.Rows(0)("nombre_region") & "' showborder='0' bgcolor='FFFFFF' formatNumberScale='0' bgalpha='100' rotateLabels='1'  placeValuesInside='1' rotateValues='1' >")

            For i = 0 To Tabla.Rows.Count - 1
                strXML.Append("<set label='" & Tabla.Rows(i)("nombre_tipoproducto") & "' value='" & Tabla.Rows(i)("Frentes") & "' />")
            Next i

            strXML.Append("</chart>")

            Dim outPut As String = ""
            outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/Column3D.swf", "", strXML.ToString(), "chart1", "270", "320", False, False)

            Panel.Controls.Clear()
            Panel.Controls.Add(New LiteralControl(outPut))
        End If

        Tabla.Dispose()
    End Function

    Sub Frentes()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT 'Nacional'as nombre_region,SUM(HDET.frentes)as Frentes " & _
                                "FROM Anaquel_Historial as H  " & _
                                "INNER JOIN AC_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                                "INNER JOIN Anaquel_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                                "WHERE H.id_periodo =" & PeriodoAct & " " & _
                                "UNION ALL " & _
                                "SELECT REG.nombre_region,SUM(HDET.frentes)as Frentes " & _
                                "FROM Anaquel_Historial as H " & _
                                "INNER JOIN AC_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                                "INNER JOIN Regiones as REG ON TI.id_region = REG.id_region " & _
                                "INNER JOIN Anaquel_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                                "WHERE H.id_periodo =" & PeriodoAct & " " & _
                                "GROUP BY REG.nombre_region")
        Dim strXML As New StringBuilder()
        strXML.Append("<chart caption='Frentes por división' showborder='0' bgcolor='FFFFFF' formatNumberScale='0' rotateLabels='1'  placeValuesInside='1' rotateValues='1' >")

        For i = 0 To tabla.Rows.Count - 1
            strXML.Append("<set label='" & tabla.Rows(i)("nombre_region") & "' value='" & tabla.Rows(i)("Frentes") & "' />")
        Next i

        strXML.Append("</chart>")

        Dim outPut As String = ""
        outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/Column3D.swf", "", strXML.ToString(), "chart1", "280", "350", False, False)

        pnlFrentes.Controls.Clear()
        pnlFrentes.Controls.Add(New LiteralControl(outPut))

        tabla.Dispose()
    End Sub

    Sub FrentesLinea()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT TPROD.nombre_tipoproducto,SUM(HDET.frentes)as Frentes " & _
                                    "FROM Anaquel_Historial as H  " & _
                                    "INNER JOIN AC_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                                    "INNER JOIN Anaquel_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial  " & _
                                    "INNER JOIN Anaquel_Productos as PROD ON HDET.id_producto = PROD.id_producto " & _
                                    "INNER JOIN Anaquel_Tipo_Productos as TPROD ON TPROD.id_tipo = PROD.id_tipo " & _
                                    "WHERE H.id_periodo=" & PeriodoAct & " AND PROD.id_empresa =1 " & _
                                    "GROUP BY TPROD.nombre_tipoproducto ORDER BY TPROD.nombre_tipoproducto")

        Dim strXML As New StringBuilder()
        strXML.Append("<chart caption='Frentes por línea Nacional' showborder='0' bgcolor='FFFFFF' formatNumberScale='0' bgalpha='100' rotateLabels='1'  placeValuesInside='1' rotateValues='1' >")

        For i = 0 To tabla.Rows.Count - 1
            strXML.Append("<set label='" & tabla.Rows(i)("nombre_tipoproducto") & "' value='" & tabla.Rows(i)("Frentes") & "' />")
        Next i

        strXML.Append("</chart>")

        Dim outPut As String = ""
        outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/Column3D.swf", "", strXML.ToString(), "chart1", "280", "400", False, False)

        pnlFrentesLinea.Controls.Clear()
        pnlFrentesLinea.Controls.Add(New LiteralControl(outPut))

        tabla.Dispose()
    End Sub

    Sub FrentesCadena()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT GRP.grupo,SUM(HDET.frentes)as Frentes " & _
                                 "FROM Anaquel_Historial as H " & _
                                 "INNER JOIN AC_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                                 "INNER JOIN Cadenas_Tiendas as CAD ON TI.id_cadena = CAD.id_cadena " & _
                                 "INNER JOIN Cadenas_Grupo as GRP ON GRP.id_grupo= CAD.id_grupo " & _
                                 "INNER JOIN Anaquel_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                                 "WHERE H.id_periodo =" & PeriodoAct & " " & _
                                 "GROUP BY GRP.grupo ORDER BY GRP.grupo ")
        Dim strXML As New StringBuilder()
        strXML.Append("<chart caption='Frentes Nacional por Cadena' showborder='0' bgcolor='FFFFFF' formatNumberScale='0' bgalpha='100' rotateLabels='1'  placeValuesInside='1' rotateValues='1' >")

        For i = 0 To tabla.Rows.Count - 1
            strXML.Append("<set label='" & tabla.Rows(i)("grupo") & "' value='" & tabla.Rows(i)("Frentes") & "' />")
        Next i

        strXML.Append("</chart>")

        Dim outPut As String = ""
        outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/Column3D.swf", "", strXML.ToString(), "chart1", "800", "350", False, False)

        pnlCadena.Controls.Clear()
        pnlCadena.Controls.Add(New LiteralControl(outPut))

        tabla.Dispose()
    End Sub

    Sub HistoricoLinea()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT * FROM Anaquel_Tipo_Productos")
        If Tabla.Rows.Count > 0 Then
            For i = 0 To Tabla.Rows.Count - 1
                CampoTitulo(i) = ",ISNULL([" & Tabla.Rows(i)("id_tipo") & "],0)as [" & Tabla.Rows(i)("id_tipo") & "]"
                CamposTitulos = CamposTitulos + CampoTitulo(i)

                If i = 0 Then
                    Campo(i) = "[" & Tabla.Rows(i)("id_tipo") & "]" : Else
                    Campo(i) = ",[" & Tabla.Rows(i)("id_tipo") & "]" : End If

                Campos = Campos + Campo(i)
            Next i : End If

        Dim TablaGraf As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT DISTINCT id_periodo,nombre_periodo " & _
                                " " + CamposTitulos + " " & _
                                "FROM(SELECT TOP 10 H.id_periodo,PER.nombre_periodo,PROD.id_tipo,sum(HDET.frentes)frentes  " & _
                                "FROM Anaquel_Historial as H  " & _
                                "INNER JOIN AC_Tiendas as TI ON TI.id_tienda = H.id_tienda  " & _
                                "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region  " & _
                                "INNER JOIN Anaquel_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial  " & _
                                "INNER JOIN Anaquel_Productos as PROD ON HDET.id_producto = PROD.id_producto  " & _
                                "INNER JOIN Anaquel_Tipo_Productos as TPROD ON TPROD.id_tipo = PROD.id_tipo  " & _
                                "INNER JOIN AC_Periodos as PER ON PER.id_periodo = H.id_periodo " & _
                                "WHERE PROD.id_empresa =1 " & _
                                "GROUP BY  H.id_periodo,PER.nombre_periodo, PROD.id_tipo) AS Datos PIVOT(SUM(frentes) " & _
                                "FOR id_tipo IN (" + Campos + ")) AS PivotTable " & _
                                "ORDER BY id_periodo ")
   
        ''//CARGAR GRAFICA TARIMAS PROMOTOR
        Dim Linea1, Linea2, Linea3, Linea4, _
            Linea5, Linea6, Linea7 As New StringBuilder()
        Dim strXML As New StringBuilder()
        Dim strCategories As New StringBuilder()

        ''//GRAFICA FRENTES
        strXML.Append("<chart caption='Historico frentes por línea' lineThickness='1' showValues='0' formatNumberScale='0'  yAxisMinValue='20' rotateLabels='1' anchorRadius='2' divLineAlpha='20' divLineColor='CC3300' divLineIsDashed='1' showAlternateHGridColor='1' alternateHGridAlpha='5' alternateHGridColor='000000' shadowAlpha='40' numvdivlines='5' chartRightMargin='35' bgColor='FFFFFF,000000' bgAngle='270' bgAlpha='10,10'>")
        strCategories.Append("<categories>")
        Linea1.Append("<dataset seriesName='" & Tabla.Rows(0)("nombre_tipoproducto") & "'>")
        Linea2.Append("<dataset seriesName='" & Tabla.Rows(1)("nombre_tipoproducto") & "'>")
        Linea3.Append("<dataset seriesName='" & Tabla.Rows(2)("nombre_tipoproducto") & "'>")
        Linea4.Append("<dataset seriesName='" & Tabla.Rows(3)("nombre_tipoproducto") & "'>")
        Linea5.Append("<dataset seriesName='" & Tabla.Rows(4)("nombre_tipoproducto") & "'>")
        Linea6.Append("<dataset seriesName='" & Tabla.Rows(5)("nombre_tipoproducto") & "'>")
        Linea7.Append("<dataset seriesName='" & Tabla.Rows(6)("nombre_tipoproducto") & "'>")

        For i = 0 To TablaGraf.Rows.Count - 1
            strCategories.Append("<category name='" & TablaGraf.Rows(i)("nombre_periodo") & "' />")
            Linea1.Append("<set value='" & TablaGraf.Rows(i)("1") & "' />")
            Linea2.Append("<set value='" & TablaGraf.Rows(i)("2") & "' />")
            Linea3.Append("<set value='" & TablaGraf.Rows(i)("3") & "' />")
            Linea4.Append("<set value='" & TablaGraf.Rows(i)("4") & "' />")
            Linea5.Append("<set value='" & TablaGraf.Rows(i)("5") & "' />")
            Linea6.Append("<set value='" & TablaGraf.Rows(i)("6") & "' />")
            Linea7.Append("<set value='" & TablaGraf.Rows(i)("7") & "' />")
        Next

        strCategories.Append("</categories>")
        Linea1.Append("</dataset>")
        Linea2.Append("</dataset>")
        Linea3.Append("</dataset>")
        Linea4.Append("</dataset>")
        Linea5.Append("</dataset>")
        Linea6.Append("</dataset>")
        Linea7.Append("</dataset>")

        strXML.Append(strCategories.ToString() & Linea1.ToString() & Linea2.ToString() & Linea3.ToString() & Linea4.ToString() & Linea5.ToString() & Linea6.ToString() & Linea7.ToString())
        strXML.Append("</chart>")

        Dim outPut As String = ""
        outPut = FusionCharts.RenderChartHTML("../../../FusionCharts/MSLine.swf", "", strXML.ToString(), "chart1", "800", "400", False, False)

        pnlHistorico.Controls.Clear()
        pnlHistorico.Controls.Add(New LiteralControl(outPut))

        Tabla.Dispose()
        TablaGraf.Dispose()
    End Sub

    Sub HistoricoCatalogacion()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT * FROM Catalogacion_Tipo_Productos")
        If Tabla.Rows.Count > 0 Then
            For i = 0 To Tabla.Rows.Count - 1
                CampoTituloC(i) = ",ISNULL([" & Tabla.Rows(i)("id_tipo") & "],0)as [" & Tabla.Rows(i)("id_tipo") & "]"
                CamposTitulosC = CamposTitulosC + CampoTituloC(i)

                If i = 0 Then
                    CampoC(i) = "[" & Tabla.Rows(i)("id_tipo") & "]" : Else
                    CampoC(i) = ",[" & Tabla.Rows(i)("id_tipo") & "]" : End If

                CamposC = CamposC + CampoC(i)
            Next i : End If

        Dim TablaGraf As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT DISTINCT id_periodo,nombre_periodo " & _
                                " " + CamposTitulosC + " " & _
                                "FROM(SELECT TOP 10 H.id_periodo,PER.nombre_periodo,PROD.id_tipo,sum(HDET.catalogado)catalogado  " & _
                                "FROM Catalogacion_Historial as H  " & _
                                "INNER JOIN AC_Tiendas as TI ON TI.id_tienda = H.id_tienda  " & _
                                "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region  " & _
                                "INNER JOIN Catalogacion_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial  " & _
                                "INNER JOIN Catalogacion_Productos as PROD ON PROD.id_producto = HDET.id_producto  " & _
                                "INNER JOIN Catalogacion_Tipo_Productos as TPROD ON TPROD.id_tipo = PROD.id_tipo  " & _
                                "INNER JOIN AC_Periodos as PER ON PER.id_periodo = H.id_periodo " & _
                                "GROUP BY H.id_periodo,PER.nombre_periodo, PROD.id_tipo) AS Datos PIVOT(SUM(catalogado) " & _
                                "FOR id_tipo IN (" + CamposC + ")) AS PivotTable " & _
                                "ORDER BY id_periodo ")
        ''//CARGAR GRAFICA TARIMAS PROMOTOR
        Dim Linea1, Linea2, Linea3, Linea4 As New StringBuilder()
        Dim strXML As New StringBuilder()
        Dim strCategories As New StringBuilder()

        ''//GRAFICA FRENTES
        strXML.Append("<chart caption='Historico catalogación por línea' lineThickness='1' showValues='0' formatNumberScale='0'  yAxisMinValue='20' rotateLabels='1' anchorRadius='2' divLineAlpha='20' divLineColor='CC3300' divLineIsDashed='1' showAlternateHGridColor='1' alternateHGridAlpha='5' alternateHGridColor='000000' shadowAlpha='40' numvdivlines='5' chartRightMargin='35' bgColor='FFFFFF,000000' bgAngle='270' bgAlpha='10,10'>")
        strCategories.Append("<categories>")
        Linea1.Append("<dataset seriesName='" & Tabla.Rows(0)("nombre_tipoproducto") & "'>")
        Linea2.Append("<dataset seriesName='" & Tabla.Rows(1)("nombre_tipoproducto") & "'>")
        Linea3.Append("<dataset seriesName='" & Tabla.Rows(2)("nombre_tipoproducto") & "'>")
        Linea4.Append("<dataset seriesName='" & Tabla.Rows(3)("nombre_tipoproducto") & "'>")

        For i = 0 To TablaGraf.Rows.Count - 1
            strCategories.Append("<category name='" & TablaGraf.Rows(i)("nombre_periodo") & "' />")
            Linea1.Append("<set value='" & TablaGraf.Rows(i)("1") & "' />")
            Linea2.Append("<set value='" & TablaGraf.Rows(i)("2") & "' />")
            Linea3.Append("<set value='" & TablaGraf.Rows(i)("3") & "' />")
            Linea4.Append("<set value='" & TablaGraf.Rows(i)("4") & "' />")
        Next

        strCategories.Append("</categories>")
        Linea1.Append("</dataset>")
        Linea2.Append("</dataset>")
        Linea3.Append("</dataset>")
        Linea4.Append("</dataset>")

        strXML.Append(strCategories.ToString() & Linea1.ToString() & Linea2.ToString() & Linea3.ToString() & Linea4.ToString())
        strXML.Append("</chart>")

        Dim outPut As String = ""
        outPut = FusionCharts.RenderChartHTML("../../../FusionCharts/MSLine.swf", "", strXML.ToString(), "chart1", "800", "400", False, False)

        pnlHistoricoCatalogacion.Controls.Clear()
        pnlHistoricoCatalogacion.Controls.Add(New LiteralControl(outPut))

        Tabla.Dispose()
        TablaGraf.Dispose()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        VerPeriodosActivos()
        CargaGraficas()
    End Sub

End Class