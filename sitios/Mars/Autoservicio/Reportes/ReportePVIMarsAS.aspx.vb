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

Partial Public Class ReportePVIMarsAS
    Inherits System.Web.UI.Page

    Sub SQLCombo()
        MarsAS.SQLsCombo(cmbPeriodo.SelectedValue, "", _
                         cmbRegion.SelectedValue, cmbEjecutivo.SelectedValue, _
                         "", cmbPromotor.SelectedValue, _
                         "", "View_Historial_AS")
    End Sub

    Sub CargarReporte()
        Dim RegionSQL, SupervisorSQL, PromotorSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            If cmbEjecutivo.SelectedValue <> "" Then
                RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
                SupervisorSQL = Acciones.Slc.cmb("REL.id_supervisor", cmbSupervisor.SelectedValue)
                PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)

                CargaGrilla(ConexionMars.localSqlServer, _
                           "select distinct REL.Ejecutivo, COUNT(RE.id_tienda)Tiendas, SUM(PVI.PVI)PVI, " & _
                            "COUNT(RE.id_tienda)-SUM(PVI.PVI)NoPVI " & _
                            "FROM (select distinct id_tienda,orden,id_usuario " & _
                            "FROM AS_Rutas_Eventos WHERE orden=" & cmbPeriodo.SelectedValue & ") as RE " & _
                            "INNER JOIN (SELECT orden, id_tienda, CASE WHEN PVI<>0 then 1 else 0 end PVI " & _
                            "FROM(select distinct orden, id_tienda, SUM(PVI)PVI " & _
                            "FROM Cumplimiento_PVI_Nuevo WHERE orden=" & cmbPeriodo.SelectedValue & " " & _
                            "GROUP BY orden, id_tienda)PVI)as PVI " & _
                            "ON PVI.id_tienda=RE.id_tienda AND RE.orden=PVI.orden " & _
                            "INNER JOIN View_Tiendas_AS as TI ON RE.id_tienda=TI.id_tienda " & _
                            "INNER JOIN View_Usuario_AS as REL ON RE.id_usuario=REL.id_usuario " & _
                            "WHERE RE.orden=" & cmbPeriodo.SelectedValue & " " & _
                            "AND REL.region_mars= '" & cmbEjecutivo.SelectedValue & "' " & _
                            " " + RegionSQL + SupervisorSQL + PromotorSQL + " " & _
                            "GROUP BY REL.Ejecutivo", _
                            Me.gridResumen)

                CargaGrilla(ConexionMars.localSqlServer, _
                            "select TI.nombre_region,REL.Ejecutivo, PVI.id_usuario, PVI.orden, PVI.id_quincena, " & _
                            "TI.clasificacion_tienda, TI.nombre_grupo, TI.nombre_cadena,TI.nombre_formato, TI.codigo, TI.nombre, " & _
                            "PVI.Cumplimiento " & _
                            "FROM  Cumplimiento_PVI_Nuevo as PVI " & _
                            "INNER JOIN View_Tiendas_AS as TI ON TI.id_tienda=PVI.id_tienda " & _
                            "INNER JOIN View_Usuario_AS as REL ON REL.id_usuario=PVI.id_usuario " & _
                            "WHERE PVI.orden=" & cmbPeriodo.SelectedValue & " " & _
                            "AND REL.region_mars= '" & cmbEjecutivo.SelectedValue & "' " & _
                            " " + RegionSQL + SupervisorSQL + PromotorSQL + " " & _
                            "ORDER BY PVI.Cumplimiento DESC,TI.nombre_cadena ASC,TI.nombre ASC", _
                            Me.gridDetalle)

                CargaGrilla(ConexionMars.localSqlServer, _
                            "SELECT REL.Ejecutivo,H.id_usuario,TI.nombre_region, TI.nombre_cadena,TI.nombre_grupo, TI.nombre, TI.nombre_formato, " & _
                            "[Sobran_A_9],[Faltan_A_9],[Sobran_A_10],[Faltan_A_10],[Sobran_A_11],[Faltan_A_11],[Sobran_A_12],[Faltan_A_12], " & _
                            "[Sobran_M_1],[Faltan_M_1],[Sobran_M_6],[Faltan_M_6],[Sobran_M_15],[Faltan_M_15]," & _
                            "[Sobran_C_1],[Faltan_C_1],[Sobran_C_3],[Faltan_C_3],[Sobran_C_5],[Faltan_C_5],[Sobran_C_15],[Faltan_C_15]," & _
                            "[Sobran_ES_5], [Faltan_ES_5], [Sobran_ES_16], [Faltan_ES_16]" & _
                            "FROM Detalle_PVI_No_Logrado as H " & _
                            "INNER JOIN View_Tiendas_AS as TI ON TI.id_tienda= H.id_tienda  " & _
                            "INNER JOIN View_Usuario_AS as REL ON REL.id_usuario = H.id_usuario " & _
                            "WHERE H.orden=" & cmbPeriodo.SelectedValue & " " & _
                            "AND REL.region_mars= '" & cmbEjecutivo.SelectedValue & "' " & _
                            " " + RegionSQL + SupervisorSQL + PromotorSQL + " " & _
                            "ORDER BY TI.nombre_region, TI.nombre_cadena, TI.nombre ", _
                            Me.gridReporte)
            Else
                gridReporte.Visible = False
            End If
        Else
            gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PeriodoSQLCmb, "nombre_periodo", "orden", cmbPeriodo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        End If
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Private Sub cmbEjecutivo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEjecutivo.SelectedIndexChanged
        cmbSupervisor.SelectedValue = ""

        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Private Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        cmbEjecutivo.SelectedValue = ""

        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte comentarios anaquel y exhibiciones " + cmbPeriodo.SelectedItem.ToString() + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.Header
                For i = 0 To 7
                    e.Row.Cells(i).Visible = True
                Next i

                e.Row.Cells(7).ColumnSpan = 13
                e.Row.Cells(24).ColumnSpan = 13

                For i = 8 To 23
                    e.Row.Cells(i).Visible = False : Next i

                For i = 25 To 32
                    e.Row.Cells(i).Visible = False : Next i

                e.Row.Cells(7).Controls.Clear()
                e.Row.Cells(7).Text = "<table style='width:1170px; font-weight: 700; color: #FFFFFF;' border='0' cellpadding='0' cellspacing='0'>" & _
                "<tr><td colspan='13' style='border: thin solid #FFFFFF; padding: inherit; text-align: center; color: #FFFFFF;'>Faltan</td></tr>" & _
                "<tr><td colspan='4' style='border: thin solid #FFFFFF; padding: inherit; text-align: center; color: #FFFFFF;'>Anaquel</td>" & _
                "    <td colspan='3' style='border: thin solid #FFFFFF; padding: inherit; text-align: center; color: #FFFFFF;'>Pasillo mascota</td>" & _
                "    <td colspan='4' style='border: thin solid #FFFFFF; padding: inherit; text-align: center; color: #FFFFFF;'>Zona caliente</td>" & _
                "    <td colspan='2' style='border: thin solid #FFFFFF; padding: inherit; text-align: center; color: #FFFFFF;'>Entrada o salida</td></tr>" & _
                "<tr><td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>Balcón</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>Tiras</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>Latero</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>Pouchero</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>Isla</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>Cabecera</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>Botadero</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>Isla</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>Mix feeding</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>Mini rack</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>Botadero</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>Mini rack</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>Wet móvil</td></tr></table>"

                e.Row.Cells(24).Controls.Clear()
                e.Row.Cells(24).Text = "<table style='width:1170px; font-weight: 700; color: #FFFFFF;' border='0' cellpadding='0' cellspacing='0'>" & _
                "<tr><td colspan='13' style='border: thin solid #FFFFFF; padding: inherit; text-align: center; color: #FFFFFF;'>Sobran</td></tr>" & _
                "<tr><td colspan='4' style='border: thin solid #FFFFFF; padding: inherit; text-align: center; color: #FFFFFF;'>Anaquel</td>" & _
                "    <td colspan='3' style='border: thin solid #FFFFFF; padding: inherit; text-align: center; color: #FFFFFF;'>Pasillo mascota</td>" & _
                "    <td colspan='4' style='border: thin solid #FFFFFF; padding: inherit; text-align: center; color: #FFFFFF;'>Zona caliente</td>" & _
                "    <td colspan='2' style='border: thin solid #FFFFFF; padding: inherit; text-align: center; color: #FFFFFF;'>Entrada o salida</td></tr>" & _
                "<tr><td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>Balcón</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>Tiras</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>Latero</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>Pouchero</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>Isla</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>Cabecera</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>Botadero</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>Isla</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>Mix feeding</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>Mini rack</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>Botadero</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>Mini rack</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>Wet móvil</td></tr></table>"
        End Select
    End Sub
End Class