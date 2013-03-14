Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class IndicadoresFerreroDanone
    Inherits System.Web.UI.Page

    Dim PeriodoAct, SemanaAct, Tiendas As Integer
    Dim Semana As String
    Dim Hoy As Date
    Dim VerificaFecha1, VerificaFecha2 As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            VerPeriodosActivos()
            CantidadTiendas()

            ''//CARGA GRAFICAS
            CargaGraficas()

            Combo.LlenaDrop(ConexionFerrero.localSqlServer, "SELECT id_periodo, nombre_periodo FROM Danone_Periodos ORDER BY fecha_inicio DESC", "nombre_periodo", "id_periodo", cmbPeriodo)
        End If
    End Sub

    Sub CargaGraficas()
        FaltantesCatalogados()
        GraficaEncuestas("1", pnl1, "¿QUIEN SURTE LOS PRODUCTOS DE FERRERO?")
        GraficaEncuestas("2", pnl2, "¿TIENE EXHIBIDO EL PRODUCTO?")
        GraficaEncuestas("3", pnl3, "¿HAY MATERIAL POP?")
        GraficaEncuestas("4", pnl4, "¿CUAL ES LA FRECUENCIA DE VISITA DEL PREVENTISTA DE FERRERO PARA LEVANTAR PEDIDO?")
        GraficaEncuestas("5", pnl5, "¿EN CUANTO TIEMPO TE ENTREGAN EL PEDIDO REALIZADO?")
        Caducidad()
    End Sub

    Sub VerPeriodosActivos()
        Dim SQL As String
        If cmbPeriodo.Text = "" Then
            SQL = "SELECT * FROM Danone_Periodos ORDER BY id_periodo DESC":Else
            SQL = "SELECT * FROM Danone_Periodos WHERE id_periodo =" & cmbPeriodo.SelectedValue & "":End If

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer,SQL)

        If Tabla.Rows.Count > 0 Then
            PeriodoAct = Tabla.Rows(0)("id_periodo")
        End If

        cmbPeriodo.SelectedValue = PeriodoAct

        Tabla.Dispose()
    End Sub

    Sub CantidadTiendas()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                               "SELECT count(nombre_tienda) as Cantidad " & _
                                               "FROM Danone_Historial " & _
                                               "WHERE id_periodo =" & PeriodoAct & "")
        If Tabla.Rows.Count > 0 Then
            lblTiendas.Text = Tabla.Rows(0)("Cantidad")
            Tiendas = Tabla.Rows(0)("Cantidad")
        End If

        Tabla.Dispose()
    End Sub

    Sub FaltantesCatalogados()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                               "select PROD.nombre_producto,sum(HDET.faltante) as faltante, sum(HDET.catalogado) as catalogado " & _
                                "FROM Danone_Historial as H " & _
                                "INNER JOIN Danone_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                                "INNER JOIN Danone_Productos AS PROD ON PROD.id_producto = HDET.id_producto " & _
                                "WHERE H.id_periodo=" & PeriodoAct & " " & _
                                "GROUP BY PROD.nombre_producto " & _
                                "ORDER BY PROD.nombre_producto ")

        Dim strDataQty As New StringBuilder()
        Dim strDataQty2 As New StringBuilder()
        Dim strXML As New StringBuilder()
        Dim strCategories As New StringBuilder()
        Dim strDataRev As New StringBuilder()

        strXML.Append("<chart caption='Catalogados vs. Faltantes' yAxisMaxValue='" & Tiendas + 20 & "' rotateLabels='1'  placeValuesInside='1' rotateValues='1' >")
        strCategories.Append("<categories>")

        strDataRev.Append("<dataset seriesName='Catalogados'>")
        strDataQty.Append("<dataset seriesName='Faltantes'>")

        strXML.Append("<trendlines>")
        strXML.Append("<line startvalue='" & Tiendas & "' displayValue='Tiendas " & Tiendas & "' color='FF0000' thickness='3' valueOnRight='1' showOnTop='0' />")
        strXML.Append("</trendlines>")

        For i = 0 To tabla.Rows.Count - 1
            strCategories.Append("<category name='" & tabla.Rows(i)("nombre_producto") & "' />")
            strDataRev.Append("<set value='" & tabla.Rows(i)("catalogado") & "' />")
            strDataQty.Append("<set value='" & tabla.Rows(i)("faltante") & "' />")
        Next i

        strCategories.Append("</categories>")
        strDataRev.Append("</dataset>")
        strDataQty.Append("</dataset>")

        strXML.Append(strCategories.ToString() & strDataRev.ToString() & strDataQty.ToString())
        strXML.Append("</chart>")

        Dim outPut As String = ""
        If IsPostBack = True Then
            outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/MSColumnLine3D.swf", "", strXML.ToString(), "chart1", "540", "520", False, False)
        Else
            outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/MSColumnLine3D.swf", "", strXML.ToString(), "chart1", "540", "520", False, False)
        End If

        pnlFaltantesCatalogados.Controls.Clear()
        pnlFaltantesCatalogados.Controls.Add(New LiteralControl(outPut))

        Tabla.Dispose()
    End Sub

    Sub Caducidad()
        Dim Hoy As Date
        Hoy = Date.Today

        VerificaFecha1 = DateAdd(DateInterval.Day, 45, Hoy)
        VerificaFecha2 = DateAdd(DateInterval.Day, 89, Hoy)

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                               "select 'Tiendas' as Mes, count(distinct HDET.caducidad) as Tiendas " & _
                    "FROM Danone_Historial as H  " & _
                    "INNER JOIN Danone_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                    "WHERE H.id_periodo=" & PeriodoAct & " AND caducidad<>'' " & _
                    "union all " & _
                    "select 'en 45 días' as Mes, count(distinct HDET.caducidad) as Tiendas " & _
                    "FROM Danone_Historial as H " & _
                    "INNER JOIN Danone_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                    "WHERE H.id_periodo=" & PeriodoAct & " AND caducidad<='" & ISODates.Dates.SQLServerDate(CDate(VerificaFecha1)) & "' " & _
                    "union all " & _
                    "select 'menos de 90 días' as Mes, count(distinct HDET.caducidad) as Tiendas " & _
                    "FROM Danone_Historial as H " & _
                    "INNER JOIN Danone_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                    "WHERE H.id_periodo=" & PeriodoAct & " AND caducidad>='" & ISODates.Dates.SQLServerDate(CDate(VerificaFecha1)) & "' AND caducidad <='" & ISODates.Dates.SQLServerDate(CDate(VerificaFecha2)) & "' " & _
                    "union all " & _
                    "select 'más de 90 días' as Mes, count(distinct HDET.caducidad) as Tiendas " & _
                    "FROM Danone_Historial as H " & _
                    "INNER JOIN Danone_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                    "WHERE H.id_periodo=" & PeriodoAct & " AND caducidad>'" & ISODates.Dates.SQLServerDate(CDate(VerificaFecha2)) & "'")

        Dim strXML As New StringBuilder()
        strXML.Append("<chart caption='Tiendas con productos por caducar' yAxisMaxValue='" & tabla.Rows(0)("Tiendas") + 5 & "' showborder='0' bgcolor='FFFFFF' bgalpha='100' rotateLabels='1'  placeValuesInside='1' rotateValues='1' >")

        strXML.Append("<trendlines>")
        strXML.Append("<line startvalue='" & tabla.Rows(0)("Tiendas") & "' displayValue='" & tabla.Rows(0)("Tiendas") & " tiendas con caducidad' color='FF0000' thickness='3' valueOnRight='1' showOnTop='0' />")
        strXML.Append("</trendlines>")

        For i = 1 To tabla.Rows.Count - 1
            strXML.Append("<set label='" & tabla.Rows(i)("Mes") & "' value='" & tabla.Rows(i)("Tiendas") & "' />")
        Next i

        strXML.Append("</chart>")

        Dim outPut As String = ""
        outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/Column3D.swf", "", strXML.ToString(), "chart1", "400", "400", False, False)

        pnlCaducidad.Controls.Clear()
        pnlCaducidad.Controls.Add(New LiteralControl(outPut))

        Tabla.Dispose()
    End Sub

    Public Function GraficaEncuestas(ByVal Campo As String, ByVal panel As Panel, ByVal titulo As String) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                               "select HDET.[" & Campo & "], count(HDET.[" & Campo & "]) as cantidad " & _
                                "FROM Danone_Historial as H " & _
                                "INNER JOIN Danone_Encuesta as HDET ON H.folio_historial = HDET.folio_historial " & _
                                "WHERE H.id_periodo=" & PeriodoAct & " " & _
                                "GROUP BY HDET.[" & Campo & "] " & _
                                "ORDER BY HDET.[" & Campo & "] ")

        Dim strXML As New StringBuilder()
        strXML.Append("<chart caption='" & titulo & "' showPercentValues='1' showPercentageInLabel='1' pieSliceDepth='25' showBorder='1' showLabels='1'>")

        For i = 0 To tabla.Rows.Count - 1
            strXML.Append("<set label='" & tabla.Rows(i)("" & Campo & "") & "' value='" & tabla.Rows(i)("cantidad") & "'/>")
        Next i

        strXML.Append("</chart>")

        Dim outPut As String = ""
        outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/Pie2D.swf", "", strXML.ToString(), "Frentes", "450", "200", False, False)

        panel.Controls.Clear()
        panel.Controls.Add(New LiteralControl(outPut))

        tabla.Dispose()
    End Function

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        VerPeriodosActivos()
        CargaGraficas()
    End Sub

End Class