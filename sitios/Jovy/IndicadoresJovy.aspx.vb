Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class IndicadoresJovy
    Inherits System.Web.UI.Page

    Dim PeriodoAct, SemanaAct As Integer
    Dim Semana As String
    Dim Hoy As Date
    Dim VerificaFecha1, VerificaFecha2, VerificaFecha3 As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionJovy.localSqlServer, HttpContext.Current.User.Identity.Name)

            If Consulta = 1 Then
                VerPeriodosActivos()

                ''//CARGA GRAFICAS
                CargaGraficas()

                Combo.LlenaDrop(ConexionJovy.localSqlServer, "SELECT id_periodo, nombre_periodo FROM Jovy_Periodos ORDER BY fecha_inicio DESC", "nombre_periodo", "id_periodo", cmbPeriodo)
            Else
                Response.Redirect("Menu_Jovy.aspx")
            End If
        End If
    End Sub

    Sub CargaGraficas()
        Exhibiciones()
        Inventario()
        InventarioTienda()
        CargaPrecios()
        Caducidad()
        Faltantes()
    End Sub

    Sub VerPeriodosActivos()
        Dim SQL As String
        If cmbPeriodo.Text = "" Then
            SQL = "SELECT * FROM Jovy_Periodos ORDER BY id_periodo DESC":Else
            SQL = "SELECT * FROM Jovy_Periodos WHERE id_periodo =" & cmbPeriodo.SelectedValue & "" : End If

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, SQL)

        If Tabla.Rows.Count > 0 Then
            PeriodoAct = Tabla.Rows(0)("id_periodo")
        End If

        cmbPeriodo.SelectedValue = PeriodoAct

        Tabla.Dispose()
    End Sub

    Sub Faltantes()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                               "select CAT.nombre_categoria, PROD.nombre_producto,PROD.bolsa,PROD.piezas, sum(HDET.faltante) as Faltante " & _
                                "FROM Jovy_Historial as H " & _
                                "FULL JOIN Jovy_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                                "FULL JOIN Jovy_Productos AS PROD ON PROD.id_producto = HDET.id_producto " & _
                                "INNER JOIN Jovy_Marcas as MAR ON MAR.id_marca = PROD.id_marca " & _
                                "INNER JOIN Jovy_Categoria AS CAT ON CAT.id_categoria = PROD.tipo_producto " & _
                                "WHERE H.id_periodo = '" & PeriodoAct & "' " & _
                                "AND HDET.faltante<>0 " & _
                                "AND MAR.id_marca= 1 " & _
                                "GROUP BY CAT.nombre_categoria, PROD.nombre_producto,PROD.bolsa,PROD.piezas " & _
                                "ORDER BY PROD.nombre_producto ")

        Dim strXML As New StringBuilder()
        strXML.Append("<chart caption='Cantidad de tiendas con faltantes por Productos' showborder='0' bgcolor='FFFFFF' bgalpha='100' rotateLabels='1'  placeValuesInside='1' rotateValues='1' >")

        For i = 0 To tabla.Rows.Count - 1
            strXML.Append("<set label='" & tabla.Rows(i)("nombre_producto") & "' value='" & tabla.Rows(i)("Faltante") & "' />")
        Next i

        strXML.Append("</chart>")

        Dim outPut As String = ""
        outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/Column3D.swf", "", strXML.ToString(), "chart1", "700", "500", False, False)

        pnlFaltantes.Controls.Clear()
        pnlFaltantes.Controls.Add(New LiteralControl(outPut))

        Tabla.Dispose()
    End Sub

    Sub Inventario()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                               "select CAT.nombre_categoria, PROD.nombre_producto,PROD.bolsa,PROD.piezas, sum(HDET.inventarios) as inventarios " & _
                                "FROM Jovy_Historial as H " & _
                                "FULL JOIN Jovy_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                                "FULL JOIN Jovy_Productos AS PROD ON PROD.id_producto = HDET.id_producto " & _
                                "INNER JOIN Jovy_Marcas as MAR ON MAR.id_marca = PROD.id_marca " & _
                                "INNER JOIN Jovy_Categoria AS CAT ON CAT.id_categoria = PROD.tipo_producto " & _
                                "WHERE H.id_periodo = '" & PeriodoAct & "' " & _
                                "AND MAR.id_marca= 1 " & _
                                "GROUP BY CAT.nombre_categoria, PROD.nombre_producto,PROD.bolsa, PROD.piezas " & _
                                "ORDER BY PROD.nombre_producto ")

        Dim strXML As New StringBuilder()
        strXML.Append("<chart caption='Inventario por Productos' showborder='0' bgcolor='FFFFFF' bgalpha='100' rotateLabels='1'  placeValuesInside='1' rotateValues='1' >")

        For i = 0 To tabla.Rows.Count - 1
            strXML.Append("<set label='" & tabla.Rows(i)("nombre_producto") & "' value='" & tabla.Rows(i)("inventarios") & "' />")
        Next i

        strXML.Append("</chart>")

        Dim outPut As String = ""
        outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/Column3D.swf", "", strXML.ToString(), "chart1", "700", "700", False, False)

        pnlInventarios.Controls.Clear()
        pnlInventarios.Controls.Add(New LiteralControl(outPut))

        Tabla.Dispose()
    End Sub

    Sub InventarioTienda()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                               "select TI.nombre, sum(HDET.inventarios) as inventarios " & _
                                "FROM Jovy_Historial as H " & _
                                "FULL JOIN Jovy_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                                "INNER JOIN Jovy_Tiendas as TI ON H.id_tienda = TI.id_tienda " & _
                                "WHERE H.id_periodo = '" & PeriodoAct & "' " & _
                                "GROUP BY TI.nombre " & _
                                "ORDER BY TI.nombre ")

        Dim strXML As New StringBuilder()
        strXML.Append("<chart caption='Inventario por Tienda' showborder='0' bgcolor='FFFFFF' bgalpha='100' rotateLabels='1'  placeValuesInside='1' rotateValues='1' >")

        For i = 0 To tabla.Rows.Count - 1
            strXML.Append("<set label='" & tabla.Rows(i)("nombre") & "' value='" & tabla.Rows(i)("inventarios") & "' />")
        Next i

        strXML.Append("</chart>")

        Dim outPut As String = ""
        outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/Column3D.swf", "", strXML.ToString(), "chart1", "700", "500", False, False)

        pnlInventariosTienda.Controls.Clear()
        pnlInventariosTienda.Controls.Add(New LiteralControl(outPut))

        Tabla.Dispose()
    End Sub

    Sub Exhibiciones()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                               "select sum(H.exhibidores) as exhibidores, sum(H.exhibidores_competencia) as exhibidores_competencia " & _
                                "FROM Jovy_Historial_Exhibidores as H " & _
                                "WHERE H.id_periodo = '" & PeriodoAct & "' ")

        Dim strXML As New StringBuilder()
        strXML.Append("<chart caption='Exhibiciones' showPercentValues='1' showPercentageInLabel='1' pieSliceDepth='25' showBorder='1' showLabels='1'>")

        strXML.Append("<set label='JOVY' value='" & tabla.Rows(0)("exhibidores") & "'/>")
        strXML.Append("<set label='COMPETENCIA' value='" & tabla.Rows(0)("exhibidores_competencia") & "'/>")

        strXML.Append("</chart>")

        Dim outPut As String = ""
        outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/Pie2D.swf", "", strXML.ToString(), "Frentes", "350", "250", False, False)

        pnlExhibidores.Controls.Clear()
        pnlExhibidores.Controls.Add(New LiteralControl(outPut))

        Tabla.Dispose()
    End Sub

    Sub Caducidad()
        Dim Hoy As Date
        Hoy = Date.Today

        VerificaFecha1 = DateAdd(DateInterval.Day, 60, Hoy)
        VerificaFecha2 = DateAdd(DateInterval.Day, 90, Hoy)
        VerificaFecha3 = DateAdd(DateInterval.Day, 91, Hoy)

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                               "select 'Un mes' as Mes, count(distinct H.id_tienda) as Tiendas " & _
                    "FROM Jovy_Historial as H " & _
                    "INNER JOIN Jovy_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                    "WHERE H.id_periodo = '" & PeriodoAct & "' AND caducidad<='" & ISODates.Dates.SQLServerDate(CDate(VerificaFecha1)) & "' " & _
                    "union all " & _
                    "select '2 meses' as Mes, count(distinct H.id_tienda) as Tiendas " & _
                    "FROM Jovy_Historial as H " & _
                    "INNER JOIN Jovy_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                    "WHERE H.id_periodo = '" & PeriodoAct & "' AND caducidad>'" & ISODates.Dates.SQLServerDate(CDate(VerificaFecha1)) & "' AND caducidad <='" & ISODates.Dates.SQLServerDate(CDate(VerificaFecha2)) & "' " & _
                    "union all " & _
                    "select '3 o mas' as Mes, count(distinct H.id_tienda) as Tiendas " & _
                    "FROM Jovy_Historial as H " & _
                    "INNER JOIN Jovy_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                    "WHERE H.id_periodo = '" & PeriodoAct & "' AND caducidad>='" & ISODates.Dates.SQLServerDate(CDate(VerificaFecha3)) & "'")

        Dim strXML As New StringBuilder()
        strXML.Append("<chart caption='Cantidad de tiendas con productos por caducar' showborder='0' bgcolor='FFFFFF' bgalpha='100' rotateLabels='1'  placeValuesInside='1' rotateValues='1' >")

        For i = 0 To tabla.Rows.Count - 1
            strXML.Append("<set label='" & tabla.Rows(i)("Mes") & "' value='" & tabla.Rows(i)("Tiendas") & "' />")
        Next i

        strXML.Append("</chart>")

        Dim outPut As String = ""
        outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/Column3D.swf", "", strXML.ToString(), "chart1", "250", "400", False, False)

        pnlCaducidad.Controls.Clear()
        pnlCaducidad.Controls.Add(New LiteralControl(outPut))

        Tabla.Dispose()
    End Sub

    Sub CargaPrecios()
        PrecioPromedio(pnlPrecioPromedio, "Precio promedio por Productos", "", "450", "650")
    End Sub

    Public Function PrecioPromedio(ByVal panel As Panel, ByVal Titulo As String, ByVal Cadena As String, ByVal Espacio1 As Integer, ByVal Espacio2 As Integer) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                               "select CAT.nombre_categoria, PROD.nombre_producto,PROD.bolsa,PROD.piezas, round(avg(HDET.precio),2) as Precio " & _
                                "FROM Jovy_Historial as H " & _
                                "FULL JOIN Jovy_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                                "FULL JOIN Jovy_Productos AS PROD ON PROD.id_producto = HDET.id_producto " & _
                                "INNER JOIN Jovy_Marcas as MAR ON MAR.id_marca = PROD.id_marca " & _
                                "INNER JOIN Jovy_Categoria AS CAT ON CAT.id_categoria = PROD.tipo_producto " & _
                                "WHERE H.id_periodo = '" & PeriodoAct & "' " & _
                                "AND MAR.id_marca= 1 " + Cadena + " " & _
                                "AND HDET.precio<>0 " & _
                                "GROUP BY CAT.nombre_categoria, PROD.nombre_producto,PROD.bolsa,PROD.piezas " & _
                                "ORDER BY PROD.nombre_producto ")

        Dim strXML As New StringBuilder()
        strXML.Append("<chart caption='" & Titulo & "' showborder='0' bgcolor='FFFFFF' bgalpha='100' rotateLabels='1'  placeValuesInside='1' numberPrefix='$' rotateValues='1' >")

        For i = 0 To tabla.Rows.Count - 1
            strXML.Append("<set label='" & tabla.Rows(i)("nombre_producto") & "' value='" & tabla.Rows(i)("Precio") & "' />")
        Next i

        strXML.Append("</chart>")

        Dim outPut As String = ""
        outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/Column3D.swf", "", strXML.ToString(), "chart1", "700", "500", False, False)

        panel.Controls.Clear()
        panel.Controls.Add(New LiteralControl(outPut))

        Tabla.Dispose()
    End Function

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        VerPeriodosActivos()
        CargaGraficas()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class