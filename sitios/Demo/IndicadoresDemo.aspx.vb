Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class IndicadoresDemo
    Inherits System.Web.UI.Page

    Dim PeriodoAct, PeriodoActSYM, PeriodoActJovy, SemanaAct, PeriodoObj, SemanaObj As Integer
    Dim Semana, RegionSQL As String
    Dim Hoy As Date

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Consulta = 1 Then
                VerPeriodosActivos()
                VerPeriodosActivosSYM()
                VerPeriodosActivosJovy()

                ''//CARGA GRAFICAS
                Objetivo()
                Faltantes()
                FrentesRegion(1, pnlFrentesDivision1)
                FrentesRegion(2, pnlFrentesDivision2)
                FrentesRegion(3, pnlFrentesDivision3)
                FrentesRegion(4, pnlFrentesDivision4)
                FrentesRegion(5, pnlFrentesDivision5)
                ParticipacionRegion()
            Else
                Response.Redirect("Menu_Cloe.aspx")
            End If
        End If
    End Sub


    Sub VerPeriodosActivosSYM()
        Dim cnn As New SqlConnection(ConexionSYM.localSqlServer)
        cnn.Open()

        Dim SQL As String
        If cmbPeriodo.Text = "" Then
            SQL = "SELECT * FROM SYM_AC_Periodos ORDER BY id_periodo DESC"
        Else
            SQL = "SELECT * FROM SYM_AC_Periodos WHERE id_periodo =" & cmbPeriodo.SelectedValue & ""
        End If

        Dim cmd As New SqlCommand(SQL, cnn)
        Dim tabla As New DataTable
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(tabla)

        If tabla.Rows.Count > 0 Then
            PeriodoActSYM = tabla.Rows(0)("id_periodo")
        End If

        cmbPeriodo.SelectedValue = PeriodoAct


        cmd.Dispose()
        tabla.Dispose()
        da.Dispose()
        cnn.Close()
        cnn.Dispose()
    End Sub

    Sub VerPeriodosActivosJovy()
        Dim cnn As New SqlConnection(ConexionJovy.localSqlServer)
        cnn.Open()

        Dim SQL As String
        If cmbPeriodo.Text = "" Then
            SQL = "SELECT * FROM C_Jovy_Periodos ORDER BY id_periodo DESC"
        Else
            SQL = "SELECT * FROM C_Jovy_Periodos WHERE id_periodo =" & cmbPeriodo.SelectedValue & ""
        End If

        Dim cmd As New SqlCommand(SQL, cnn)
        Dim tabla As New DataTable
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(tabla)

        If tabla.Rows.Count > 0 Then
            PeriodoActJovy = tabla.Rows(0)("id_periodo")
        End If

        cmbPeriodo.SelectedValue = PeriodoAct


        cmd.Dispose()
        tabla.Dispose()
        da.Dispose()
        cnn.Close()
        cnn.Dispose()
    End Sub

    Sub ParticipacionRegion()
        Using cnn As New SqlConnection(ConexionSYM.localSqlServer)
            ''//CARGA GRAFICA
            Dim SQL As New SqlCommand("SELECT REG.nombre_region,SUM(HDET.frentes)as Frentes  " & _
                                "FROM SYM_Anaquel_Historial as H  " & _
                                "INNER JOIN SYM_AC_Tiendas as TI ON TI.id_tienda = H.id_tienda  " & _
                                "INNER JOIN SYM_Regiones as REG ON REG.id_region = TI.id_region  " & _
                                "INNER JOIN SYM_Anaquel_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                                "INNER JOIN SYM_Anaquel_Productos as PROD ON HDET.id_producto = PROD.id_producto  " & _
                                "INNER JOIN SYM_Anaquel_Tipo_Productos as TPROD ON TPROD.id_tipo = PROD.id_tipo  " & _
                                "WHERE H.id_periodo=" & PeriodoActSYM & " AND PROD.id_empresa =1  " & _
                                "GROUP BY REG.nombre_region ", cnn)
            Dim adapter2 As New SqlDataAdapter
            adapter2.SelectCommand = SQL
            Dim dataset2 As New DataSet
            adapter2.Fill(dataset2, "total")

            ''//CARGA GRAFICA
            Dim tabla As New DataTable
            Dim data As New SqlDataAdapter(SQL)
            data.Fill(tabla)

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

            SQL.Dispose()
            tabla.Dispose()

            cnn.Close()
            cnn.Dispose()
        End Using
    End Sub

    Public Function FrentesRegion(ByVal Region As Integer, ByVal Panel As Panel) As Integer
        Using cnn As New SqlConnection(ConexionSYM.localSqlServer)
            Dim SQL As New SqlCommand("SELECT REG.nombre_region,TPROD.nombre,SUM(HDET.frentes)as Frentes  " & _
                                    "FROM SYM_Anaquel_Historial as H   " & _
                                    "INNER JOIN SYM_AC_Tiendas as TI ON TI.id_tienda = H.id_tienda  " & _
                                    "INNER JOIN SYM_Regiones as REG ON REG.id_region = TI.id_region " & _
                                    "INNER JOIN SYM_Anaquel_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial   " & _
                                    "INNER JOIN SYM_Anaquel_Productos as PROD ON HDET.id_producto = PROD.id_producto  " & _
                                    "INNER JOIN SYM_Anaquel_Tipo_Productos as TPROD ON TPROD.id_tipo = PROD.id_tipo  " & _
                                    "WHERE H.id_periodo=" & PeriodoActSYM & "  AND PROD.id_empresa =1 AND REG.id_region =" & Region & "  " & _
                                    "GROUP BY REG.nombre_region,TPROD.nombre ORDER BY TPROD.nombre", cnn)
            Dim tabla As New DataTable
            Dim Data As New SqlDataAdapter(SQL)
            Data.Fill(tabla)

            If tabla.Rows.Count > 0 Then
                Dim strXML As New StringBuilder()
                strXML.Append("<chart caption='Frentes por linea en " & tabla.Rows(0)("nombre_region") & "' showborder='0' bgcolor='FFFFFF' formatNumberScale='0' bgalpha='100' rotateLabels='1'  placeValuesInside='1' rotateValues='1' >")

                For i = 0 To tabla.Rows.Count - 1
                    strXML.Append("<set label='" & tabla.Rows(i)("nombre") & "' value='" & tabla.Rows(i)("Frentes") & "' />")
                Next i

                strXML.Append("</chart>")

                Dim outPut As String = ""
                outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/Column3D.swf", "", strXML.ToString(), "chart1", "270", "320", False, False)

                Panel.Controls.Clear()
                Panel.Controls.Add(New LiteralControl(outPut))
            End If

            SQL.Dispose()
            tabla.Dispose()

            cnn.Close()
            cnn.Dispose()
        End Using
    End Function

    Sub VerPeriodosActivos()
        Dim cnn As New SqlConnection(ConexionCloe.localSqlServer)
        cnn.Open()

        Dim SQL As String
        If cmbPeriodo.Text = "" Then
            SQL = "SELECT * FROM Cloe_Periodos ORDER BY id_periodo DESC"
        Else
            SQL = "SELECT * FROM Cloe_Periodos WHERE id_periodo =" & cmbPeriodo.SelectedValue & ""
        End If

        Dim cmd As New SqlCommand(SQL, cnn)
        Dim tabla As New DataTable
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(tabla)

        If tabla.Rows.Count > 0 Then
            PeriodoAct = tabla.Rows(0)("id_periodo")
        End If

        cmbPeriodo.SelectedValue = PeriodoAct

        Dim SQL3 As String
        SQL3 = "SELECT * FROM Cloe_Periodos_Objetivos WHERE id_periodo_captura=" & PeriodoAct & " ORDER BY id_semana DESC"

        Dim cmd3 As New SqlCommand(SQL3, cnn)

        Dim tabla3 As New DataTable
        Dim da3 As New SqlDataAdapter(cmd3)
        da3.Fill(tabla3)

        If tabla3.Rows.Count > 0 Then
            PeriodoObj = tabla3.Rows(0)("id_periodo")
            SemanaObj = tabla3.Rows(0)("id_semana")
        End If

        cmd.Dispose()
        cmd3.Dispose()
        tabla.Dispose()
        tabla3.Dispose()
        da.Dispose()
        da3.Dispose()
        cnn.Close()
        cnn.Dispose()
    End Sub

    Sub Objetivo()
        Using cnn As New SqlConnection(ConexionCloe.localSqlServer)

            If cmbRegion.SelectedValue <> 0 Then
                RegionSQL = "AND TI.id_region = " & cmbRegion.SelectedValue & "" : Else
                RegionSQL = "" : End If

            Dim sql As String = "select PROD.nombre, round((100* NULLIF(sum(venta),0)/NULLIF(sum(Objetivo),0)),2)PorcentajeVenta " & _
                        "FROM Cloe_Objetivos as OBJ " & _
                        "INNER JOIN Cloe_Productos as PROD ON PROD.id_producto = OBJ.id_producto " & _
                        "INNER JOIN Cloe_Tiendas as TI ON TI.id_tienda = OBJ.id_tienda " & _
                        "WHERE id_periodo=8 AND id_semana=1 " & _
                        "" + RegionSQL + " GROUP BY PROD.nombre"

            Dim cmd As New SqlCommand(sql, cnn)
            cnn.Open()

            ''//CARGA GRAFICA
            Dim tabla As New DataTable
            Dim da2 As New SqlDataAdapter(cmd)
            da2.Fill(tabla)

            Dim strXML As New StringBuilder()
            strXML.Append("<chart caption='Porcentaje de Avance de Objetivo Venta' showborder='0' bgcolor='FFFFFF' bgalpha='100' rotateLabels='1'  placeValuesInside='1' numberSuffix='%25' rotateValues='1' >")

            strXML.Append("<trendlines>")
            strXML.Append("<line startvalue='100' displayValue='100' color='FF0000' thickness='3' valueOnRight='1' showOnTop='0' />")
            strXML.Append("</trendlines>")

            For i = 0 To tabla.Rows.Count - 1
                strXML.Append("<set label='" & tabla.Rows(i)("nombre") & "' value='" & tabla.Rows(i)("PorcentajeVenta") & "' />")
            Next i

            strXML.Append("</chart>")

            Dim outPut As String = ""
            outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/Column3D.swf", "", strXML.ToString(), "chart1", "500", "350", False, False)

            pnlObjetivo.Controls.Clear()
            pnlObjetivo.Controls.Add(New LiteralControl(outPut))

            cnn.Dispose()
            cnn.Close()
        End Using
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        VerPeriodosActivos()

        Objetivo()
        Faltantes()
    End Sub

    Private Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbRegion.SelectedIndexChanged
        VerPeriodosActivos()

        Objetivo()
        Faltantes()
    End Sub

    Sub Faltantes()
        Using cnn As New SqlConnection(ConexionJovy.localSqlServer)

            Dim sql As String = "select CAT.nombre_categoria, PROD.nombre,PROD.bolsa,PROD.piezas, sum(HDET.faltante) as Faltante " & _
                                "FROM C_Jovy_Historial as H " & _
                                "FULL JOIN C_Jovy_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                                "FULL JOIN C_Jovy_Productos AS PROD ON PROD.id_producto = HDET.id_producto " & _
                                "INNER JOIN C_Jovy_Marcas as MAR ON MAR.id_marca = PROD.id_marca " & _
                                "INNER JOIN C_Jovy_Categoria AS CAT ON CAT.id_categoria = PROD.tipo_producto " & _
                                "WHERE H.id_periodo = '" & PeriodoActJovy & "' " & _
                                "AND HDET.faltante<>0 " & _
                                "AND MAR.id_marca= 1 " & _
                                "GROUP BY CAT.nombre_categoria, PROD.nombre,PROD.bolsa,PROD.piezas " & _
                                "ORDER BY PROD.nombre "

            Dim cmd As New SqlCommand(sql, cnn)
            cnn.Open()

            ''//CARGA GRAFICA
            Dim tabla As New DataTable
            Dim da2 As New SqlDataAdapter(cmd)
            da2.Fill(tabla)

            Dim strXML As New StringBuilder()
            strXML.Append("<chart caption='Cantidad de tiendas con faltantes por Productos' showborder='0' bgcolor='FFFFFF' bgalpha='100' rotateLabels='1'  placeValuesInside='1' rotateValues='1' >")

            For i = 0 To tabla.Rows.Count - 1
                strXML.Append("<set label='" & tabla.Rows(i)("nombre") & "' value='" & tabla.Rows(i)("Faltante") & "' />")
            Next i

            strXML.Append("</chart>")

            Dim outPut As String = ""
            outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/Column3D.swf", "", strXML.ToString(), "chart1", "700", "500", False, False)

            pnlFaltantes.Controls.Clear()
            pnlFaltantes.Controls.Add(New LiteralControl(outPut))

            cnn.Dispose()
            cnn.Close()
        End Using
    End Sub
End Class