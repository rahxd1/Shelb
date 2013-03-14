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

Partial Public Class ReporteResumenMarsMay
    Inherits System.Web.UI.Page

    Dim PeriodoActual As String
    Dim QuincenaSQL As String

    Sub SQLCombo()
        MarsMay.SQLsCombo(cmbPeriodo.SelectedValue, cmbQuincena.SelectedValue, _
                        "", "", "", "", "")
    End Sub

    Sub CargarReporte()
        If cmbPeriodo.SelectedValue <> "" Then
            QuincenaSQL = Acciones.Slc.cmb("HDET.id_quincena", cmbQuincena.SelectedValue)

            VerGrillaResumen(gridResumenAutoservicio, 3)
            VerGrillaResumen(gridResumenMostrador, 2)

            VerGrilla(gridReporteAutoservicio, 3)
            VerGrilla(gridReporteMostrador, 2)
        Else
            gridReporteAutoservicio.Visible = False
            gridReporteMostrador.Visible = False
        End If
    End Sub

    Private Function VerGrillaResumen(ByVal Grilla As GridView, ByVal Tipo_tienda As Integer) As Boolean
        CargaGrilla(ConexionMars.localSqlServer, _
               "SELECT nombre_region, " & _
               "SUM(min_1)min_1, SUM(max_1)max_1, " & _
               "CASE WHEN SUM(min_1)<>0 then 100*SUM(min_1)/(SUM(min_1)+SUM(max_1)) else 0 end t_min_1, " & _
               "CASE WHEN SUM(max_1)<>0 then 100*SUM(max_1)/(SUM(min_1)+SUM(max_1)) else 0 end t_max_1, " & _
               "SUM(min_2)min_2,SUM(max_2)max_2, " & _
               "CASE WHEN SUM(min_2)<>0 then 100*SUM(min_2)/(SUM(min_2)+SUM(max_2)) else 0 end t_min_2, " & _
               "CASE WHEN SUM(max_2)<>0 then 100*SUM(max_2)/(SUM(min_2)+SUM(max_2)) else 0 end t_max_2, " & _
               "SUM(min_5)min_5,SUM(max_5)max_5, " & _
               "CASE WHEN SUM(min_5)<>0 then 100*SUM(min_5)/(SUM(min_5)+SUM(max_5)) else 0 end t_min_5, " & _
               "CASE WHEN SUM(max_5)<>0 then 100*SUM(max_5)/(SUM(min_5)+SUM(max_5)) else 0 end t_max_5, " & _
               "SUM(min_7)min_7, SUM(max_7)max_7, " & _
               "CASE WHEN SUM(min_7)<>0 then 100*SUM(min_7)/(SUM(min_7)+SUM(max_7)) else 0 end t_min_7, " & _
               "CASE WHEN SUM(max_7)<>0 then 100*SUM(max_7)/(SUM(min_7)+SUM(max_7)) else 0 end t_max_7 " & _
               "FROM(select HDET.id_tienda, HDET.nombre_region,TI.nombre_tipo,HDET.ciudad,  " & _
               "CASE WHEN (ISNULL([11],0))<>0 THEN " & _
               "(CASE WHEN (ISNULL([11],0))<(ISNULL([1],0)) then 1 else 0 end)else 0 end min_1, " & _
               "CASE WHEN (ISNULL([11],0))<>0 THEN " & _
               "(CASE WHEN (ISNULL([11],0))>(ISNULL([1],0)) then 1 else 0 end)else 0 end max_1, " & _
               "CASE WHEN (ISNULL([12],0))<>0 THEN " & _
               "(CASE WHEN (ISNULL([12],0))<(ISNULL([2],0)) then 1 else 0 end)else 0 end min_2, " & _
               "CASE WHEN (ISNULL([12],0))<>0 THEN " & _
               "(CASE WHEN (ISNULL([12],0))>(ISNULL([2],0)) then 1 else 0 end)else 0 end max_2, " & _
               "CASE WHEN (ISNULL([15],0))<>0 THEN " & _
               "(CASE WHEN (ISNULL([15],0))<(ISNULL([5],0)) then 1 else 0 end)else 0 end min_5, " & _
               "CASE WHEN (ISNULL([15],0))<>0 THEN " & _
               "(CASE WHEN (ISNULL([15],0))>(ISNULL([5],0)) then 1 else 0 end)else 0 end max_5, " & _
               "CASE WHEN (ISNULL([17],0))<>0 THEN " & _
               "(CASE WHEN (ISNULL([17],0))<(ISNULL([7],0)) then 1 else 0 end)else 0 end min_7, " & _
               "CASE WHEN (ISNULL([17],0))<>0 THEN " & _
               "(CASE WHEN (ISNULL([17],0))>(ISNULL([7],0)) then 1 else 0 end)else 0 end max_7 " & _
               "FROM (SELECT orden,id_quincena,id_tienda,tipo_tienda,nombre_region,US.ciudad,id_producto, precio " & _
               "FROM View_Historial_Mayoreo_4fantasticos as H " & _
               "INNER JOIN Usuarios as US ON US.id_usuario=H.id_usuario)HDET " & _
               "PIVOT(AVG(precio) FOR id_producto  " & _
               "IN([1],[11],[2],[12],[5],[15],[7],[17]))HDET " & _
               "INNER JOIN Tipo_Tiendas_Mayoreo as TI ON TI.tipo_tienda=HDET.tipo_tienda  " & _
               "WHERE HDET.orden=" & cmbPeriodo.SelectedValue & " " & _
               "AND HDET.tipo_tienda=" & Tipo_tienda & " " & _
               " " + QuincenaSQL + ")H " & _
               "GROUP BY nombre_region " & _
               "ORDER BY nombre_region", Grilla)
    End Function

    Private Function VerGrilla(ByVal Grilla As GridView, ByVal Tipo_tienda As Integer) As Boolean
        CargaGrilla(ConexionMars.localSqlServer, _
                        "SELECT nombre_region, nombre_tipo, ciudad,  " & _
                        "SUM(min_1)min_1, SUM(max_1)max_1, " & _
                        "CASE WHEN SUM(min_1)<>0 then 100*SUM(min_1)/(SUM(min_1)+SUM(max_1)) else 0 end t_min_1, " & _
                        "CASE WHEN SUM(max_1)<>0 then 100*SUM(max_1)/(SUM(min_1)+SUM(max_1)) else 0 end t_max_1, " & _
                        "SUM(min_2)min_2,SUM(max_2)max_2, " & _
                        "CASE WHEN SUM(min_2)<>0 then 100*SUM(min_2)/(SUM(min_2)+SUM(max_2)) else 0 end t_min_2, " & _
                        "CASE WHEN SUM(max_2)<>0 then 100*SUM(max_2)/(SUM(min_2)+SUM(max_2)) else 0 end t_max_2, " & _
                        "SUM(min_5)min_5,SUM(max_5)max_5, " & _
                        "CASE WHEN SUM(min_5)<>0 then 100*SUM(min_5)/(SUM(min_5)+SUM(max_5)) else 0 end t_min_5, " & _
                        "CASE WHEN SUM(max_5)<>0 then 100*SUM(max_5)/(SUM(min_5)+SUM(max_5)) else 0 end t_max_5, " & _
                        "SUM(min_7)min_7, SUM(max_7)max_7, " & _
                        "CASE WHEN SUM(min_7)<>0 then 100*SUM(min_7)/(SUM(min_7)+SUM(max_7)) else 0 end t_min_7, " & _
                        "CASE WHEN SUM(max_7)<>0 then 100*SUM(max_7)/(SUM(min_7)+SUM(max_7)) else 0 end t_max_7 " & _
                        "FROM(select HDET.id_tienda, HDET.nombre_region,TI.nombre_tipo,HDET.ciudad,  " & _
                        "CASE WHEN (ISNULL([11],0))<>0 THEN " & _
                        "(CASE WHEN (ISNULL([11],0))<(ISNULL([1],0)) then 1 else 0 end)else 0 end min_1, " & _
                        "CASE WHEN (ISNULL([11],0))<>0 THEN " & _
                        "(CASE WHEN (ISNULL([11],0))>(ISNULL([1],0)) then 1 else 0 end)else 0 end max_1, " & _
                        "CASE WHEN (ISNULL([12],0))<>0 THEN " & _
                        "(CASE WHEN (ISNULL([12],0))<(ISNULL([2],0)) then 1 else 0 end)else 0 end min_2, " & _
                        "CASE WHEN (ISNULL([12],0))<>0 THEN " & _
                        "(CASE WHEN (ISNULL([12],0))>(ISNULL([2],0)) then 1 else 0 end)else 0 end max_2, " & _
                        "CASE WHEN (ISNULL([15],0))<>0 THEN " & _
                        "(CASE WHEN (ISNULL([15],0))<(ISNULL([5],0)) then 1 else 0 end)else 0 end min_5, " & _
                        "CASE WHEN (ISNULL([15],0))<>0 THEN " & _
                        "(CASE WHEN (ISNULL([15],0))>(ISNULL([5],0)) then 1 else 0 end)else 0 end max_5, " & _
                        "CASE WHEN (ISNULL([17],0))<>0 THEN " & _
                        "(CASE WHEN (ISNULL([17],0))<(ISNULL([7],0)) then 1 else 0 end)else 0 end min_7, " & _
                        "CASE WHEN (ISNULL([17],0))<>0 THEN " & _
                        "(CASE WHEN (ISNULL([17],0))>(ISNULL([7],0)) then 1 else 0 end)else 0 end max_7 " & _
                        "FROM (SELECT orden,id_quincena,id_tienda,tipo_tienda,nombre_region,US.ciudad,id_producto, precio " & _
                        "FROM View_Historial_Mayoreo_4fantasticos as H " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario=H.id_usuario)HDET " & _
                        "PIVOT(AVG(precio) FOR id_producto  " & _
                        "IN([1],[11],[2],[12],[5],[15],[7],[17]))HDET " & _
                        "INNER JOIN Tipo_Tiendas_Mayoreo as TI ON TI.tipo_tienda=HDET.tipo_tienda  " & _
                        "WHERE HDET.orden=" & cmbPeriodo.SelectedValue & " " & _
                        "AND HDET.tipo_tienda=" & Tipo_tienda & " " & _
                        " " + QuincenaSQL + ")H " & _
                        "GROUP BY nombre_region, nombre_tipo, ciudad " & _
                        "ORDER BY nombre_tipo,nombre_region, ciudad", Grilla)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.PeriodoSQLCmb, "nombre_periodo", "orden", cmbPeriodo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.QuincenaSQLCmb, "nombre_quincena", "id_quincena", cmbQuincena)
        End If
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.QuincenaSQLCmb, "nombre_quincena", "id_quincena", cmbQuincena)

        CargarReporte()
    End Sub

    Private Sub cmbQuincena_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbQuincena.SelectedIndexChanged
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Resumen " + cmbPeriodo.SelectedItem.ToString() + " " + cmbQuincena.SelectedItem.ToString() + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Sub BuscaPeriodoActual()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT * " & _
                                     "FROM Periodos_nuevo where fecha_inicio_periodo<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                                     "AND fecha_fin_periodo>= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "'")
        If Tabla.Rows.Count > 0 Then
            PeriodoActual = Tabla.Rows(0)("orden")
        End If

        Tabla.Dispose()
    End Sub

    Private Function Tabla(ByVal Titulo As String) As String
        If Titulo <> "" Then
            Return "<table border='0' cellpadding='0' cellspacing='0' style='width:332pt' width='424'>" & _
               "<colgroup>" & _
               "<col style='mso-width-source:userset;mso-width-alt:3876;width:80pt' width='106'/>" & _
               "<col style='mso-width-source:userset;mso-width-alt:3876;width:80pt' width='106'/>" & _
               "<col style='mso-width-source:userset;mso-width-alt:3876;width:80pt' width='106'/>" & _
               "<col style='mso-width-source:userset;mso-width-alt:3876;width:80pt' width='106'/></colgroup>" & _
               "<tr height='20'><td colspan='4' height='20' style='padding: 0px; background: #02456F; height: 15.0pt; width: 332pt; color: white; font-size: 10.0pt; font-weight: 700; font-style: normal; text-decoration: none; font-family: Calibri, sans-serif; text-align: center; vertical-align: middle; white-space: normal;'" & _
               "width='424'>" & Titulo & "</td></tr>" & _
               "<tr height='60'>" & _
               "<td style='padding: 0px; background: #02456F; width: 80pt; " & _
               "color: white; font-size: 11.0pt; font-weight: 700; font-style: normal; text-decoration: none; " & _
               "font-family: Calibri, sans-serif; text-align: center; vertical-align: middle; white-space: normal;'" & _
               "width='106'>Tiendas con precio menor mínimo</td>" & _
               "<td style='padding: 0px; background: #02456F; width: 80pt; " & _
               "color: white; font-size: 11.0pt; font-weight: 700; font-style: normal; text-decoration: none; " & _
               "font-family: Calibri, sans-serif; text-align: center; vertical-align: middle; white-space: normal;'" & _
               "width='106'>Tiendas precio limite o mayor</td>" & _
               "<td style='padding: 0px; background: #02456F; width: 80pt; " & _
               "color: white; font-size: 11.0pt; font-weight: 700; font-style: normal; text-decoration: none; " & _
               "font-family: Calibri, sans-serif; text-align: center; vertical-align: middle; white-space: normal;'" & _
               "width='106'>% tiendas con precio menor mínimo</td>" & _
               "<td style='padding: 0px; background: #02456F; width: 80pt; " & _
               "color: white; font-size: 11.0pt; font-weight: 700; font-style: normal; text-decoration: none; " & _
               "font-family: Calibri, sans-serif; text-align: center; vertical-align: middle; white-space: normal;'" & _
               "width='106'>% tiendas precio limite o mayor</td></tr></table>"
        Else
            Return ""
        End If
    End Function

    Private Sub gridReporteAutoservicio_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporteAutoservicio.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.Header
                e.Row.Cells(0).Visible = True
                e.Row.Cells(1).Visible = True

                For i = 2 To 14 Step 4
                    e.Row.Cells(i).ColumnSpan = 4
                    e.Row.Cells(i + 1).Visible = False
                    e.Row.Cells(i + 2).Visible = False
                    e.Row.Cells(i + 3).Visible = False
                    e.Row.Cells(i).Controls.Clear()
                Next

                e.Row.Cells(2).Text = Tabla("PAL ® PERRO 25 KG")
                e.Row.Cells(6).Text = Tabla("PEDIGREE ® CACHORRO 20 KG")
                e.Row.Cells(10).Text = Tabla("PEDIGREE ® ADULTO NUTRICION COMPLETA 25 KG")
                e.Row.Cells(14).Text = Tabla("WHISKAS ® RECETA ORIGINAL 20 KG")
        End Select

        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 4 To 18 Step 4
                Select Case e.Row.Cells(i).Text
                    Case 0 : e.Row.Cells(i).BackColor = Drawing.Color.Green
                    Case Is <> 0 : e.Row.Cells(i).BackColor = Drawing.Color.Red
                End Select
                e.Row.Cells(i).Text = e.Row.Cells(i).Text & "%"

                Select Case e.Row.Cells(i + 1).Text
                    Case 100 : e.Row.Cells(i + 1).BackColor = Drawing.Color.Green
                    Case Is <> 0 : e.Row.Cells(i + 1).BackColor = Drawing.Color.Orange
                    Case 0 : e.Row.Cells(i + 1).BackColor = Drawing.Color.Red
                End Select
                e.Row.Cells(i + 1).Text = e.Row.Cells(i + 1).Text & "%"
            Next i
        End If
    End Sub

    Private Sub gridReporteMostrador_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporteMostrador.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.Header
                e.Row.Cells(0).Visible = True
                e.Row.Cells(1).Visible = True

                For i = 2 To 14 Step 4
                    e.Row.Cells(i).ColumnSpan = 4
                    e.Row.Cells(i + 1).Visible = False
                    e.Row.Cells(i + 2).Visible = False
                    e.Row.Cells(i + 3).Visible = False
                    e.Row.Cells(i).Controls.Clear()
                Next

                e.Row.Cells(2).Text = Tabla("PAL ® PERRO 25 KG")
                e.Row.Cells(6).Text = Tabla("PEDIGREE ® CACHORRO 20 KG")
                e.Row.Cells(10).Text = Tabla("PEDIGREE ® ADULTO NUTRICION COMPLETA 25 KG")
                e.Row.Cells(14).Text = Tabla("WHISKAS ® RECETA ORIGINAL 20 KG")
        End Select

        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 4 To 18 Step 4
                Select Case e.Row.Cells(i).Text
                    Case 0 : e.Row.Cells(i).BackColor = Drawing.Color.Green
                    Case Is <> 0 : e.Row.Cells(i).BackColor = Drawing.Color.Red
                End Select
                e.Row.Cells(i).Text = e.Row.Cells(i).Text & "%"

                Select Case e.Row.Cells(i + 1).Text
                    Case 100 : e.Row.Cells(i + 1).BackColor = Drawing.Color.Green
                    Case Is <> 0 : e.Row.Cells(i + 1).BackColor = Drawing.Color.Orange
                    Case 0 : e.Row.Cells(i + 1).BackColor = Drawing.Color.Red
                End Select
                e.Row.Cells(i + 1).Text = e.Row.Cells(i + 1).Text & "%"
            Next i
        End If
    End Sub

    Private Sub gridResumenAutoservicio_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridResumenAutoservicio.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.Header
                e.Row.Cells(0).Visible = True

                For i = 1 To 13 Step 4
                    e.Row.Cells(i).ColumnSpan = 4
                    e.Row.Cells(i + 1).Visible = False
                    e.Row.Cells(i + 2).Visible = False
                    e.Row.Cells(i + 3).Visible = False
                    e.Row.Cells(i).Controls.Clear()
                Next

                e.Row.Cells(1).Text = Tabla("PAL ® PERRO 25 KG")
                e.Row.Cells(5).Text = Tabla("PEDIGREE ® CACHORRO 20 KG")
                e.Row.Cells(9).Text = Tabla("PEDIGREE ® ADULTO NUTRICION COMPLETA 25 KG")
                e.Row.Cells(13).Text = Tabla("WHISKAS ® RECETA ORIGINAL 20 KG")
        End Select

        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 3 To 17 Step 4
                Select Case e.Row.Cells(i).Text
                    Case 0 : e.Row.Cells(i).BackColor = Drawing.Color.Green
                    Case Is <> 0 : e.Row.Cells(i).BackColor = Drawing.Color.Red
                End Select
                e.Row.Cells(i).Text = e.Row.Cells(i).Text & "%"

                Select Case e.Row.Cells(i + 1).Text
                    Case 100 : e.Row.Cells(i + 1).BackColor = Drawing.Color.Green
                    Case Is <> 0 : e.Row.Cells(i + 1).BackColor = Drawing.Color.Orange
                    Case 0 : e.Row.Cells(i + 1).BackColor = Drawing.Color.Red
                End Select
                e.Row.Cells(i + 1).Text = e.Row.Cells(i + 1).Text & "%"
            Next i
        End If
    End Sub

    Private Sub gridResumenMostrador_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridResumenMostrador.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.Header
                e.Row.Cells(0).Visible = True

                For i = 1 To 13 Step 4
                    e.Row.Cells(i).ColumnSpan = 4
                    e.Row.Cells(i + 1).Visible = False
                    e.Row.Cells(i + 2).Visible = False
                    e.Row.Cells(i + 3).Visible = False
                    e.Row.Cells(i).Controls.Clear()
                Next

                e.Row.Cells(1).Text = Tabla("PAL ® PERRO 25 KG")
                e.Row.Cells(5).Text = Tabla("PEDIGREE ® CACHORRO 20 KG")
                e.Row.Cells(9).Text = Tabla("PEDIGREE ® ADULTO NUTRICION COMPLETA 25 KG")
                e.Row.Cells(13).Text = Tabla("WHISKAS ® RECETA ORIGINAL 20 KG")
        End Select

        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 3 To 17 Step 4
                Select Case e.Row.Cells(i).Text
                    Case 0 : e.Row.Cells(i).BackColor = Drawing.Color.Green
                    Case Is <> 0 : e.Row.Cells(i).BackColor = Drawing.Color.Red
                End Select
                e.Row.Cells(i).Text = e.Row.Cells(i).Text & "%"

                Select Case e.Row.Cells(i + 1).Text
                    Case 100 : e.Row.Cells(i + 1).BackColor = Drawing.Color.Green
                    Case Is <> 0 : e.Row.Cells(i + 1).BackColor = Drawing.Color.Orange
                    Case 0 : e.Row.Cells(i + 1).BackColor = Drawing.Color.Red
                End Select
                e.Row.Cells(i + 1).Text = e.Row.Cells(i + 1).Text & "%"
            Next i
        End If
    End Sub

End Class