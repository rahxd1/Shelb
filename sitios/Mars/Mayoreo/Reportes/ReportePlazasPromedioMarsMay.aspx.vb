Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports ISODates.Dates
Imports System.Security.Cryptography
Imports InfoSoftGlobal

Partial Public Class ReportePlazasPromedioMarsMay
    Inherits System.Web.UI.Page

    Sub SQLCombo()
        MarsMay.SQLsCombo(cmbPeriodo.SelectedValue, cmbQuincena.SelectedValue, _
                       cmbSemana.SelectedValue, cmbRegion.SelectedValue, "", "", "")
    End Sub

    Sub CargarReporte()
        Dim QuincenaSQL, SemanaSQL, RegionSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            QuincenaSQL = Acciones.Slc.cmb("HDET.id_quincena", cmbQuincena.SelectedValue)
            SemanaSQL = Acciones.Slc.cmb("HDET.id_semana", cmbQuincena.SelectedValue)
            RegionSQL = Acciones.Slc.cmb("HDET.id_region", cmbRegion.SelectedValue)

            CargaGrilla(ConexionMars.localSqlServer, _
                        "SELECT H.nombre_region,H.ciudad,  " & _
                        "(ISNULL([11],0))[11],(ISNULL([12],0))[12],(ISNULL([15],0))[15],(ISNULL([17],0))[17] " & _
                        "FROM (SELECT nombre_region, ciudad, id_producto, AVG(precio)precio " & _
                        "FROM (SELECT orden,id_quincena,id_tienda,id_semana,nombre_region, ciudad, id_producto, precio " & _
                        "FROM(SELECT orden,id_quincena,id_semana,id_tienda,nombre_region,ciudad,id_producto,AVG(precio)precio " & _
                        "FROM View_Historial_Productos_4fantasticos_PM " & _
                        "WHERE precio<>0 AND (id_producto=11 OR id_producto=12 or id_producto=15 OR id_producto=17) " & _
                        "GROUP BY orden,id_quincena,id_tienda,id_semana,id_tienda,nombre_region,ciudad,id_producto " & _
                        "UNION ALL " & _
                        "SELECT orden,id_quincena,id_semana,id_tienda,nombre_region,ciudad,id_producto,AVG(precio)precio " & _
                        "FROM View_Historial_Productos_4fantasticos_May " & _
                        "WHERE precio<>0 AND (id_producto=11 OR id_producto=12 or id_producto=15 OR id_producto=17) " & _
                        "GROUP BY orden,id_quincena,id_tienda,id_semana,id_tienda,nombre_region,ciudad,id_producto)HDET " & _
                        "WHERE HDET.orden=" & cmbPeriodo.SelectedValue & "  " & _
                        " " + QuincenaSQL + SemanaSQL + RegionSQL + ")H " & _
                        "GROUP BY H.nombre_region,H.ciudad, id_producto)H " & _
                        "PIVOT(AVG(precio) FOR id_producto  " & _
                        "IN([11],[12],[15],[17]))H " & _
                        "UNION ALL " & _
                        "select REG.nombre_region,HDET.ciudad, " & _
                        "AVG(ISNULL([11],0))[11],AVG(ISNULL([12],0))[12],AVG(ISNULL([15],0))[15],AVG(ISNULL([17],0))[17]  " & _
                        "FROM (SELECT orden,id_quincena,id_semana,id_region,ciudad, id_producto, precio  " & _
                        "FROM(SELECT orden,id_quincena,id_semana,H.id_region,US.ciudad,id_producto,AVG(precio)precio  " & _
                        "FROM View_Historial_Mayoreo_4fantasticos as H " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario=H.id_usuario " & _
                        "WHERE precio<>0  " & _
                        "GROUP BY orden,id_quincena,id_semana,H.id_region,US.ciudad,id_producto)H)HDET  " & _
                        "PIVOT(AVG(precio) FOR id_producto   " & _
                        "IN([11],[12],[15],[17]))HDET  " & _
                        "INNER JOIN Regiones as REG ON REG.id_region=HDET.id_region " & _
                        "WHERE HDET.orden=" & cmbPeriodo.SelectedValue & " " & _
                        " " + QuincenaSQL + SemanaSQL + RegionSQL + " " & _
                        "GROUP BY nombre_region,ciudad", gridResumen)

            VerGrilla(gridReporteAutoservicio, 3, pnl_1, pnl_2, pnl_3, pnl_4)
            VerGrilla(gridReporteMostrador, 2, pnl_5, pnl_6, pnl_7, pnl_8)
        Else
            gridResumen.Visible = False
            gridReporteAutoservicio.Visible = False
            gridReporteMostrador.Visible = False
        End If
    End Sub

    Private Function VerGrilla(ByVal Grilla As GridView, ByVal Tipo_tienda As Integer, _
                               ByVal pnl1 As Panel, ByVal pnl2 As Panel, ByVal pnl3 As Panel, ByVal pnl4 As Panel) As Boolean
        Dim QuincenaSQL, SemanaSQL, RegionSQL As String

        QuincenaSQL = Acciones.Slc.cmb("HDET.id_quincena", cmbQuincena.SelectedValue)
        SemanaSQL = Acciones.Slc.cmb("HDET.id_semana", cmbQuincena.SelectedValue)
        RegionSQL = Acciones.Slc.cmb("HDET.id_region", cmbRegion.SelectedValue)

        Dim SQL As String = "SELECT HDET.nombre_region,HDET.ciudad,  " & _
                        "AVG(ISNULL([1],0))[1],((AVG(ISNULL([1],0))*.01)+AVG(ISNULL([1],0)))M_1,ROUND(AVG(ISNULL([11],0)),2)[11]," & _
                        "AVG(ISNULL([101],0))[101],AVG(ISNULL([1001],0))[1001], " & _
                        "AVG(ISNULL([2],0))[2],((AVG(ISNULL([2],0))*.01)+AVG(ISNULL([2],0)))M_2,ROUND(AVG(ISNULL([12],0)),2)[12]," & _
                        "AVG(ISNULL([102],0))[102],AVG(ISNULL([1002],0))[1002], " & _
                        "AVG(ISNULL([5],0))[5],((AVG(ISNULL([5],0))*.01)+AVG(ISNULL([5],0)))M_5,ROUND(AVG(ISNULL([15],0)),2)[15]," & _
                        "AVG(ISNULL([105],0))[105],AVG(ISNULL([1005],0))[1005], " & _
                        "AVG(ISNULL([7],0))[7],((AVG(ISNULL([7],0))*.01)+AVG(ISNULL([7],0)))M_7,ROUND(AVG(ISNULL([17],0)),2)[17]," & _
                        "AVG(ISNULL([107],0))[107],AVG(ISNULL([1007],0))[1007], " & _
                        "AVG(ISNULL([24],0))[24],AVG(ISNULL([25],0))[25],AVG(ISNULL([32],0))[32],AVG(ISNULL([34],0))[34], " & _
                        "AVG(ISNULL([39],0))[39],AVG(ISNULL([40],0))[40],AVG(ISNULL([46],0))[46],AVG(ISNULL([47],0))[47],AVG(ISNULL([48],0))[48] " & _
                        "FROM (SELECT nombre_region, ciudad, id_producto, AVG(precio)precio " & _
                        "FROM (SELECT orden,id_quincena,id_tienda,tipo_tienda,id_semana,nombre_region, ciudad, id_producto, precio " & _
                        "FROM(SELECT orden,id_quincena,id_semana,tipo_tienda,id_tienda,nombre_region,ciudad,id_producto,AVG(precio)precio  " & _
                        "FROM View_Historial_Productos_4fantasticos_PM " & _
                        "WHERE precio<>0 " & _
                        "GROUP BY orden,id_quincena,id_semana,tipo_tienda,id_tienda,nombre_region,ciudad,id_producto " & _
                        "UNION ALL " & _
                        "SELECT orden,id_quincena,id_semana,tipo_tienda,id_tienda,nombre_region,ciudad,id_producto,AVG(precio)precio  " & _
                        "FROM View_Historial_Productos_4fantasticos_May " & _
                        "WHERE precio<>0 " & _
                        "GROUP BY orden,id_quincena,id_semana,tipo_tienda,id_tienda,nombre_region,ciudad,id_producto)HDET " & _
                        "WHERE HDET.orden=" & cmbPeriodo.SelectedValue & "  " & _
                        "AND HDET.tipo_tienda=" & Tipo_tienda & " " & _
                        " " + QuincenaSQL + SemanaSQL + RegionSQL + ")H " & _
                        "GROUP BY H.nombre_region,H.ciudad, id_producto)H " & _
                        "PIVOT(AVG(precio) FOR id_producto  " & _
                        "IN([1],[11],[101],[1001],[10001],[2],[12],[102],[1002], " & _
                        "[5],[15],[105],[1005],[7],[17],[107],[1007], " & _
                        "[24],[25],[32],[34],[39],[40],[46],[47],[48]))HDET  " & _
                        "GROUP BY nombre_region,ciudad " & _
                        "UNION ALL " & _
                        "select REG.nombre_region,HDET.ciudad, " & _
                        "AVG(ISNULL([1],0))[1],((AVG(ISNULL([1],0))*.01)+AVG(ISNULL([1],0)))M_1,ROUND(AVG(ISNULL([11],0)),2)[11]," & _
                        "AVG(ISNULL([101],0))[101],AVG(ISNULL([1001],0))[1001], " & _
                        "AVG(ISNULL([2],0))[2],((AVG(ISNULL([2],0))*.01)+AVG(ISNULL([2],0)))M_2,ROUND(AVG(ISNULL([12],0)),2)[12]," & _
                        "AVG(ISNULL([102],0))[102],AVG(ISNULL([1002],0))[1002], " & _
                        "AVG(ISNULL([5],0))[5],((AVG(ISNULL([5],0))*.01)+AVG(ISNULL([5],0)))M_5,ROUND(AVG(ISNULL([15],0)),2)[15]," & _
                        "AVG(ISNULL([105],0))[105],AVG(ISNULL([1005],0))[1005], " & _
                        "AVG(ISNULL([7],0))[7],((AVG(ISNULL([7],0))*.01)+AVG(ISNULL([7],0)))M_7,ROUND(AVG(ISNULL([17],0)),2)[17]," & _
                        "AVG(ISNULL([107],0))[107],AVG(ISNULL([1007],0))[1007], " & _
                        "AVG(ISNULL([24],0))[24],AVG(ISNULL([25],0))[25],AVG(ISNULL([32],0))[32],AVG(ISNULL([34],0))[34], " & _
                        "AVG(ISNULL([39],0))[39],AVG(ISNULL([40],0))[40],AVG(ISNULL([46],0))[46],AVG(ISNULL([47],0))[47],AVG(ISNULL([48],0))[48] " & _
                        "FROM (SELECT orden,id_quincena,id_semana,id_region,tipo_tienda,ciudad, id_producto, precio  " & _
                        "FROM(SELECT orden,id_quincena,id_semana,H.id_region,tipo_tienda,US.ciudad,id_producto,AVG(precio)precio  " & _
                        "FROM View_Historial_Mayoreo_4fantasticos as H " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario=H.id_usuario " & _
                        "WHERE precio<>0  " & _
                        "GROUP BY orden,id_quincena,id_semana,H.id_region,tipo_tienda,US.ciudad,id_producto)H)HDET  " & _
                        "PIVOT(AVG(precio) FOR id_producto   " & _
                        "IN([1],[11],[101],[1001],[10001],[2],[12],[102],[1002], " & _
                        "[5],[15],[105],[1005],[7],[17],[107],[1007], " & _
                        "[24],[25],[32],[34],[39],[40],[46],[47],[48]))HDET  " & _
                        "INNER JOIN Regiones as REG ON REG.id_region=HDET.id_region " & _
                        "INNER JOIN Tipo_Tiendas_Mayoreo as TI ON TI.tipo_tienda=HDET.tipo_tienda  " & _
                        "WHERE HDET.orden=" & cmbPeriodo.SelectedValue & " " & _
                        "AND HDET.tipo_tienda=" & Tipo_tienda & " " & _
                        " " + QuincenaSQL + SemanaSQL + RegionSQL + " " & _
                        "GROUP BY nombre_region,ciudad " & _
                        "ORDER BY nombre_region,ciudad "

        CargaGrilla(ConexionMars.localSqlServer, SQL, Grilla)

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, SQL)

        If tabla.Rows.Count > 0 Then
            ''//Grafica 1
            Dim strXML1 As New StringBuilder()
            strXML1.Append("<chart caption='PAL 25 kg' YAxisMinValue='150' numDivLines='3' bgcolor='FFFFFF' bgalpha='100' rotateLabels='1' numberPrefix='$' placeValuesInside='1' rotateValues='1' >")
            strXML1.Append("<trendlines><line startvalue='" & tabla.Rows(0)("M_1") & "' displayValue='$" & tabla.Rows(0)("M_1") & "' color='FF0000' thickness='3' valueOnRight='1' showOnTop='0' /></trendlines>")

            ''//Grafica 2
            Dim strXML2 As New StringBuilder()
            strXML2.Append("<chart caption='Pedigree cachorro 20 kg' YAxisMinValue='150' numDivLines='3' bgcolor='FFFFFF' bgalpha='100' rotateLabels='1' numberPrefix='$' placeValuesInside='1' rotateValues='1' >")
            strXML2.Append("<trendlines><line startvalue='" & tabla.Rows(0)("M_2") & "' displayValue='$" & tabla.Rows(0)("M_2") & "' color='FF0000' thickness='3' valueOnRight='1' showOnTop='0' /></trendlines>")

            ''//Grafica 3
            Dim strXML3 As New StringBuilder()
            strXML3.Append("<chart caption='Pedigree adulto nutrición completa 25 kg' YAxisMinValue='150' numDivLines='3' bgcolor='FFFFFF' bgalpha='100' rotateLabels='1' numberPrefix='$' placeValuesInside='1' rotateValues='1' >")
            strXML3.Append("<trendlines><line startvalue='" & tabla.Rows(0)("M_5") & "' displayValue='$" & tabla.Rows(0)("M_5") & "' color='FF0000' thickness='3' valueOnRight='1' showOnTop='0' /></trendlines>")

            ''//Grafica 4
            Dim strXML4 As New StringBuilder()
            strXML4.Append("<chart caption='Whiskas receta original 20 kg' YAxisMinValue='150' numDivLines='3' bgcolor='FFFFFF' bgalpha='100' rotateLabels='1' numberPrefix='$' placeValuesInside='1' rotateValues='1' >")
            strXML4.Append("<trendlines><line startvalue='" & tabla.Rows(0)("M_7") & "' displayValue='$" & tabla.Rows(0)("M_7") & "' color='FF0000' thickness='3' valueOnRight='1' showOnTop='0' /></trendlines>")

            For i = 0 To tabla.Rows.Count - 1
                strXML1.Append("<set label='" & tabla.Rows(i)("ciudad") & "' value='" & tabla.Rows(i)("11") & "' />")
                strXML2.Append("<set label='" & tabla.Rows(i)("ciudad") & "' value='" & tabla.Rows(i)("12") & "' />")
                strXML3.Append("<set label='" & tabla.Rows(i)("ciudad") & "' value='" & tabla.Rows(i)("15") & "' />")
                strXML4.Append("<set label='" & tabla.Rows(i)("ciudad") & "' value='" & tabla.Rows(i)("17") & "' />")
            Next i

            strXML1.Append("</chart>")
            strXML2.Append("</chart>")
            strXML3.Append("</chart>")
            strXML4.Append("</chart>")

            Dim outPut1 As String = FusionCharts.RenderChartHTML("../../../../FusionCharts/Column3D.swf", "", strXML1.ToString(), "chart1", "230", "285", False, False)
            pnl1.Controls.Clear()
            pnl1.Controls.Add(New LiteralControl(outPut1))

            Dim outPut2 As String = FusionCharts.RenderChartHTML("../../../../FusionCharts/Column3D.swf", "", strXML2.ToString(), "chart1", "230", "285", False, False)
            pnl2.Controls.Clear()
            pnl2.Controls.Add(New LiteralControl(outPut2))

            Dim outPut3 As String = FusionCharts.RenderChartHTML("../../../../FusionCharts/Column3D.swf", "", strXML3.ToString(), "chart1", "230", "285", False, False)
            pnl3.Controls.Clear()
            pnl3.Controls.Add(New LiteralControl(outPut3))

            Dim outPut4 As String = FusionCharts.RenderChartHTML("../../../../FusionCharts/Column3D.swf", "", strXML4.ToString(), "chart1", "230", "285", False, False)
            pnl4.Controls.Clear()
            pnl4.Controls.Add(New LiteralControl(outPut4))
        End If

        Tabla.Dispose()
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.PeriodoSQLCmb, "nombre_periodo", "orden", cmbPeriodo)
        End If
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.QuincenaSQLCmb, "nombre_quincena", "id_quincena", cmbQuincena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.SemanaSQLCmb, "nombre_semana", "id_semana", cmbSemana)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)

        CargarReporte()
    End Sub

    Protected Sub cmbQuincena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbQuincena.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.SemanaSQLCmb, "nombre_semana", "id_semana", cmbSemana)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)

        CargarReporte()
    End Sub

    Private Sub cmbSemana_SelectedIndexChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSemana.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)

        CargarReporte()
    End Sub

    Private Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbRegion.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub lnkExportar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.pnlGrid.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.pnlGrid)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte por plazas " + cmbPeriodo.SelectedItem.ToString() + " " + cmbQuincena.SelectedItem.ToString() + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub gridReporteAutoservicio_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporteAutoservicio.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 2 To 20 Step 5
                If i = 17 Then
                    i = 18:End If

                Select Case e.Row.Cells(i + 1).Text
                    Case Is = "$0.00"
                        e.Row.Cells(i).Text = ""
                        e.Row.Cells(i + 1).Text = ""
                        e.Row.Cells(i + 2).Text = ""
                    Case Is < e.Row.Cells(i).Text
                        e.Row.Cells(i + 1).BackColor = Drawing.Color.Red
                    Case Is > e.Row.Cells(i + 2).Text
                        e.Row.Cells(i + 1).BackColor = Drawing.Color.Yellow
                    Case Is <> ""
                        If IsNumeric(e.Row.Cells(i + 1).Text) Then
                            e.Row.Cells(i + 1).BackColor = Drawing.Color.Green : End If
                End Select

                If e.Row.Cells(i + 3).Text = "$0.00" Then
                    e.Row.Cells(i + 3).Text = "" : End If
                If e.Row.Cells(i + 4).Text = "$0.00" Then
                    e.Row.Cells(i + 4).Text = "" : End If

                If i = 12 Then
                    If e.Row.Cells(i + 5).Text = "$0.00" Then
                        e.Row.Cells(i + 5).Text = "" : End If
                End If
            Next i
        End If
    End Sub

    Private Sub gridReporteMostrador_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporteMostrador.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 2 To 20 Step 5
                If i = 17 Then
                    i = 18 : End If

                Select Case e.Row.Cells(i + 1).Text
                    Case Is = "$0.00"
                        e.Row.Cells(i).Text = ""
                        e.Row.Cells(i + 1).Text = ""
                        e.Row.Cells(i + 2).Text = ""
                    Case Is < e.Row.Cells(i).Text
                        e.Row.Cells(i + 1).BackColor = Drawing.Color.Red
                    Case Is > e.Row.Cells(i + 2).Text
                        e.Row.Cells(i + 1).BackColor = Drawing.Color.Yellow
                    Case Is <> ""
                        If IsNumeric(e.Row.Cells(i + 1).Text) Then
                            e.Row.Cells(i + 1).BackColor = Drawing.Color.Green : End If
                End Select

                If e.Row.Cells(i + 3).Text = "$0.00" Then
                    e.Row.Cells(i + 3).Text = "" : End If
                If e.Row.Cells(i + 4).Text = "$0.00" Then
                    e.Row.Cells(i + 4).Text = "" : End If

                If i = 12 Then
                    If e.Row.Cells(i + 5).Text = "$0.00" Then
                        e.Row.Cells(i + 5).Text = "" : End If
                End If
            Next i
        End If
    End Sub

End Class