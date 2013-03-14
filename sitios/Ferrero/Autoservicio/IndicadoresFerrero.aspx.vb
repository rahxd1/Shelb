Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class IndicadoresFerrero
    Inherits System.Web.UI.Page

    Dim PeriodoAct, SemanaAct As Integer
    Dim Semana As String
    Dim Hoy As Date

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            VerPeriodosActivos()

            ''//CARGA GRAFICAS
            CargaGraficas()

            Combo.LlenaDrop(ConexionFerrero.localSqlServer, "SELECT id_periodo, nombre_periodo FROM AS_Periodos ORDER BY fecha_inicio DESC", "nombre_periodo", "id_periodo", cmbPeriodo)
        End If
    End Sub

    Sub CargaGraficas()
        FrentesPromedio()
        CargaPrecios()
        CargaFrentes()
        Faltantes()
    End Sub

    Sub VerPeriodosActivos()

        Dim SQL As String
        If cmbPeriodo.Text = "" Then
            SQL = "SELECT * FROM AS_Periodos ORDER BY id_periodo DESC" : Else
            SQL = "SELECT * FROM AS_Periodos WHERE id_periodo =" & cmbPeriodo.SelectedValue & "" : End If

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, SQL)
        If Tabla.Rows.Count > 0 Then
            PeriodoAct = Tabla.Rows(0)("id_periodo")
        End If
        Tabla.Dispose()

        cmbPeriodo.SelectedValue = PeriodoAct
    End Sub

    Public Function FrentesCategoria(ByVal panel As Panel, ByVal Categoria As String) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                               "select PROD.id_marca,MAR.nombre_marca,CAT.nombre_categoria, sum(HDET.frentes) as Frentes " & _
                                "FROM AS_Historial as H " & _
                                "INNER JOIN AS_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                                "INNER JOIN AS_Productos AS PROD ON PROD.id_producto = HDET.id_producto " & _
                                "INNER JOIN Marcas as MAR ON MAR.id_marca = PROD.id_marca " & _
                                "INNER JOIN AS_Categoria AS CAT ON CAT.id_categoria = PROD.tipo_producto " & _
                                "WHERE H.id_periodo=" & PeriodoAct & " " & _
                                "AND PROD.tipo_producto=" & Categoria & " " & _
                                "GROUP BY PROD.id_marca,MAR.nombre_marca,CAT.nombre_categoria " & _
                                "ORDER BY PROD.id_marca ")

        Dim strXML As New StringBuilder()
        strXML.Append("<chart caption='Participación de frentes " & tabla.Rows(0)("nombre_categoria") & "' showPercentValues='1' showPercentageInLabel='1' pieSliceDepth='25' showBorder='1' showLabels='1'>")

        For i = 0 To tabla.Rows.Count - 1
            strXML.Append("<set label='" & tabla.Rows(i)("nombre_marca") & "' value='" & tabla.Rows(i)("Frentes") & "'/>")
        Next i

        strXML.Append("</chart>")

        Dim outPut As String = ""
        outPut = FusionCharts.RenderChartHTML("../../../../../FusionCharts/Pie2D.swf", "", strXML.ToString(), "Frentes", "350", "250", False, False)

        panel.Controls.Clear()
        panel.Controls.Add(New LiteralControl(outPut))

        tabla.Dispose()
    End Function

    Sub CargaFrentes()
        FrentesCategoria(pnlFrentes1, "1")
        FrentesCategoria(pnlFrentes2, "2")
        FrentesCategoria(pnlFrentes3, "3")
        FrentesCategoria(pnlFrentes4, "4")
    End Sub

    Sub Faltantes()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                               "select CAT.nombre_categoria, PROD.nombre_producto,PROD.presentacion,PROD.gramaje, sum(HDET.faltante) as Faltante " & _
                                "FROM AS_Historial as H " & _
                                "FULL JOIN AS_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                                "FULL JOIN AS_Productos AS PROD ON PROD.id_producto = HDET.id_producto " & _
                                "INNER JOIN Marcas as MAR ON MAR.id_marca = PROD.id_marca " & _
                                "INNER JOIN AS_Categoria AS CAT ON CAT.id_categoria = PROD.tipo_producto " & _
                                "WHERE H.id_periodo=" & PeriodoAct & " " & _
                                "AND HDET.faltante<>0 " & _
                                "AND MAR.id_marca= 1 " & _
                                "GROUP BY CAT.nombre_categoria, PROD.nombre_producto,PROD.presentacion,PROD.gramaje " & _
                                "ORDER BY PROD.nombre_producto ")

        Dim strXML As New StringBuilder()
        strXML.Append("<chart caption='Cantidad de tiendas con faltantes por Productos' showborder='0' bgcolor='FFFFFF' bgalpha='100' rotateLabels='1'  placeValuesInside='1' rotateValues='1' >")

        For i = 1 To tabla.Rows.Count - 1
            strXML.Append("<set label='" & tabla.Rows(i)("nombre_producto") + " " + tabla.Rows(i)("presentacion") + " (" + tabla.Rows(i)("gramaje") + ")" & "' value='" & tabla.Rows(i)("Faltante") & "' />")
        Next i

        strXML.Append("</chart>")

        Dim outPut As String = ""
        outPut = FusionCharts.RenderChartHTML("../../../../../FusionCharts/Column3D.swf", "", strXML.ToString(), "chart1", "450", "600", False, False)

        pnlFaltantes.Controls.Clear()
        pnlFaltantes.Controls.Add(New LiteralControl(outPut))

        Tabla.Dispose()
    End Sub

    Sub FrentesPromedio()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                               "select CAT.nombre_categoria, PROD.nombre_producto,PROD.presentacion,PROD.gramaje, ROUND(avg(HDET.frentes),2) as Frentes " & _
                                "FROM AS_Historial as H " & _
                                "FULL JOIN AS_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                                "FULL JOIN AS_Productos AS PROD ON PROD.id_producto = HDET.id_producto " & _
                                "INNER JOIN Marcas as MAR ON MAR.id_marca = PROD.id_marca " & _
                                "INNER JOIN AS_Categoria AS CAT ON CAT.id_categoria = PROD.tipo_producto " & _
                                "WHERE H.id_periodo=" & PeriodoAct & " " & _
                                "AND MAR.id_marca=1 " & _
                                "GROUP BY CAT.nombre_categoria, PROD.nombre_producto,PROD.presentacion, PROD.gramaje " & _
                                "ORDER BY PROD.nombre_producto ")

        Dim strXML As New StringBuilder()
        strXML.Append("<chart caption='Cantidad promedio de frentes por Productos' showborder='0' bgcolor='FFFFFF' bgalpha='100' rotateLabels='1'  placeValuesInside='1' rotateValues='1' >")

        For i = 0 To tabla.Rows.Count - 1
            strXML.Append("<set label='" & tabla.Rows(i)("nombre_producto") + " " + tabla.Rows(i)("presentacion") + " (" + tabla.Rows(i)("gramaje") + ")" & "' value='" & tabla.Rows(i)("Frentes") & "' />")
        Next i

        strXML.Append("</chart>")

        Dim outPut As String = ""
        outPut = FusionCharts.RenderChartHTML("../../../../../FusionCharts/Column3D.swf", "", strXML.ToString(), "chart1", "450", "650", False, False)

        pnlFrentesPromedio.Controls.Clear()
        pnlFrentesPromedio.Controls.Add(New LiteralControl(outPut))

        Tabla.Dispose()
    End Sub

    Sub CargaPrecios()
        PrecioPromedio(pnlPrecioPromedio, "Precio promedio por Productos", "", "450", "650")
        PrecioPromedio(pnlPrecioWalmart, "Precio promedio por Productos en Walmart", "AND H.id_cadena=1", "400", "450")
        PrecioPromedio(pnlPrecioSoriana, "Precio promedio por Productos en Soriana", "AND H.id_cadena=2", "400", "450")
    End Sub

    Public Function PrecioPromedio(ByVal panel As Panel, ByVal Titulo As String, ByVal Cadena As String, ByVal Espacio1 As Integer, ByVal Espacio2 As Integer) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                               "select CAT.nombre_categoria, PROD.nombre_producto,PROD.presentacion,PROD.gramaje, round(avg(HDET.precio),2) as Precio " & _
                                "FROM AS_Historial as H " & _
                                "FULL JOIN AS_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                                "FULL JOIN AS_Productos AS PROD ON PROD.id_producto = HDET.id_producto " & _
                                "INNER JOIN Marcas as MAR ON MAR.id_marca = PROD.id_marca " & _
                                "INNER JOIN AS_Categoria AS CAT ON CAT.id_categoria = PROD.tipo_producto " & _
                                "WHERE H.id_periodo=" & PeriodoAct & " " & _
                                "AND MAR.id_marca= 1 " + Cadena + " " & _
                                "AND HDET.precio<>0 " & _
                                "GROUP BY CAT.nombre_categoria, PROD.nombre_producto,PROD.presentacion,PROD.gramaje " & _
                                "ORDER BY PROD.nombre_producto ")

        Dim strXML As New StringBuilder()
        strXML.Append("<chart caption='" & Titulo & "' showborder='0' bgcolor='FFFFFF' bgalpha='100' rotateLabels='1'  placeValuesInside='1' numberPrefix='$' rotateValues='1' >")

        For i = 0 To tabla.Rows.Count - 1
            strXML.Append("<set label='" & tabla.Rows(i)("nombre_producto") + " " + tabla.Rows(i)("presentacion") + " (" + tabla.Rows(i)("gramaje") + ")" & "' value='" & tabla.Rows(i)("Precio") & "' />")
        Next i

        strXML.Append("</chart>")

        Dim outPut As String = ""
        outPut = FusionCharts.RenderChartHTML("../../../../../FusionCharts/Column3D.swf", "", strXML.ToString(), "chart1", Espacio1, Espacio2, False, False)

        panel.Controls.Clear()
        panel.Controls.Add(New LiteralControl(outPut))

        Tabla.Dispose()
    End Function

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        VerPeriodosActivos()
        CargaGraficas()
    End Sub

End Class