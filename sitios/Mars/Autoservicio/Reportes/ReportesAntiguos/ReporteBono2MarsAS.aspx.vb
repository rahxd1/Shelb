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

Partial Public Class ReporteBono2MarsAS
    Inherits System.Web.UI.Page

    Sub SQLCombo()
        MarsAS.SQLsCombo(cmbPeriodo.SelectedValue, "", _
                           cmbRegion.SelectedValue, cmbEjecutivo.SelectedValue, _
                           cmbSupervisor.SelectedValue, cmbPromotor.SelectedValue, _
                           "", "View_Historial_AS")
    End Sub

    Sub CargarReporte()
        Dim EjecutivoSQL, SupervisorSQL, PromotorSQL, RegionSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("H.id_region", cmbRegion.SelectedValue)
            EjecutivoSQL = Acciones.Slc.cmb("US.region_mars", cmbEjecutivo.SelectedValue)
            SupervisorSQL = Acciones.Slc.cmb("US.id_supervisor", cmbSupervisor.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)

            Dim SQL As String = "select H.id_periodo,US.Ejecutivo,H.id_usuario,H.area_nielsen,H.nombre_region,  " & _
                        "H.ASS, H.CS, H.PH, H.PB, H.GS, H.GH,H.GB,  " & _
                        "convert(nvarchar(5),H.Part_Anaquel)+'%'Part_Anaquel, " & _
                        "convert(nvarchar(5),(CASE WHEN H2.ParaBono<=H2.PVI then 50 else 0 end))+'%'Exhibiciones,   " & _
                        "(H.Part_Anaquel)+(CASE WHEN H2.ParaBono<=H2.PVI then 50 else 0 end)Total, " & _
                        "H2.Pautas, H2.PVI, H2.Porcentaje,H2.ParaBono,Capturas.Capturas " & _
                        "FROM Cumplimiento_PVI_Anaquel as H   " & _
                        "INNER JOIN View_Usuario_AS as US ON US.id_usuario = H.id_usuario " & _
                        "INNER JOIN (select RE.region_mars, RE.id_usuario, " & _
                        "COUNT(DISTINCT RE.id_tienda)Tiendas,COUNT(DISTINCT Captura.id_tienda)Pautas,  " & _
                        "ISNULL(SUM(PVI.Cumplimiento),0)PVI,ISNULL((100*ISNULL(SUM(PVI.Cumplimiento),0)/COUNT(DISTINCT Captura.id_tienda)),0)Porcentaje, " & _
                        "round(((75*COUNT(DISTINCT Captura.id_tienda))/100),2)ParaBono " & _
                        "FROM (select DISTINCT id_periodo, US.region_mars, RE.id_tienda, TI.nombre_region, RE.id_usuario  " & _
                        "from AS_Rutas_Eventos as RE  " & _
                        "INNER JOIN View_Tiendas_AS as TI ON TI.id_tienda = RE.id_tienda   " & _
                        "INNER JOIN View_Usuario_AS as US ON US.id_usuario = RE.id_usuario   " & _
                        "where id_periodo ='" & cmbPeriodo.SelectedValue & "' )as RE  " & _
                        "FULL JOIN (select DISTINCT RE.id_periodo, RE.id_tienda  " & _
                        "FROM AS_Rutas_Eventos as RE  " & _
                        "INNER JOIN AS_Tiendas as TI ON TI.id_tienda = RE.id_tienda  " & _
                        "WHERE id_periodo ='" & cmbPeriodo.SelectedValue & "'    AND TI.id_clasificacion<>0 AND TI.id_clasificacion<>2)Captura    " & _
                        "ON Captura.id_tienda = RE.id_tienda AND Captura.id_periodo=RE.id_periodo  " & _
                        "FULL JOIN (SELECT DISTINCT RE.id_tienda,HDET.Cumplimiento, RE.id_periodo, RE.id_usuario  " & _
                        "FROM AS_Rutas_Eventos as RE  " & _
                        "INNER JOIN AS_Tiendas as TI ON RE.id_tienda = TI.id_tienda  " & _
                        "INNER JOIN Usuarios_Relacion AS REL ON REL.id_usuario = RE.id_usuario  " & _
                        "FULL JOIN (select DISTINCT H.id_periodo, H.id_tienda,H.id_usuario,  " & _
                        "(CASE when(CASE when CTI.id_clasificacion=1 OR CTI.id_clasificacion=6 OR CTI.id_clasificacion=4  " & _
                        "then (CASE when((HDET.[1])+(HDET.[2])+(HDET.[3])+(HDET.[4])+(HDET.[5])+(HDET.[6])+((HDET.[7]))+(HDET.[8]))>=CTI.O_1 then 1 else 0 end)  " & _
                        "when CTI.id_clasificacion=3 OR CTI.id_clasificacion=5  " & _
                        "then (CASE when((HDET.[1])+(HDET.[2])+(HDET.[3])+(HDET.[4])+(HDET.[5]))>=CTI.O_1 then 1 else 0 end)end)+  " & _
                        "(CASE when CTI.id_clasificacion=3 OR CTI.id_clasificacion=5  " & _
                        "then (CASE when((HDET.[6])+((HDET.[7]))+(HDET.[8]))>=CTI.O_6 then 1 else 0 end)else 0 end)+  " & _
                        "(CASE WHEN CTI.id_clasificacion<>4 then(CASE when TI.id_cadena=7  " & _
                        "then 1 else (CASE when (HDET.[10])>=CTI.O_10 then 1 else 0 end)end)else 0 end)+  " & _
                        "(CASE when (HDET.[12])>=CTI.O_12 then 1 else 0 end)+  " & _
                        "(CASE WHEN CTI.id_clasificacion=4  " & _
                        "then (CASE when ((HDET.[9])+(HDET.[11]))>=CTI.O_9 then 1 else 0 end)  " & _
                        "else (CASE WHEN CTI.O_9<>0 then(CASE when (HDET.[9])>=CTI.O_9 then 1 else 0 end)else 0 end)+  " & _
                        "(CASE when CTI.id_clasificacion=3 OR CTI.id_clasificacion=6  " & _
                        "then (CASE WHEN CTI.O_11<>0 then(CASE when (HDET.[11])>=CTI.O_11 then 1 else 0 end)else 0 end)else 0 end)end)>= CTI.Total then 1 else 0 end)Cumplimiento  " & _
                        "FROM AS_Historial as H  " & _
                        "INNER JOIN Usuarios_Relacion AS REL ON REL.id_usuario = H.id_usuario  " & _
                        "INNER JOIN AS_Tiendas as TI ON H.id_tienda = TI.id_tienda  " & _
                        "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region  " & _
                        "INNER JOIN AS_Clasificacion_Tiendas AS CTI ON CTI.id_clasificacion = TI.id_clasificacion    " & _
                        "INNER JOIN AS_Exhibidores_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial    " & _
                        "WHERE H.id_periodo ='" & cmbPeriodo.SelectedValue & "'   )HDET  " & _
                        "ON HDET.id_tienda = RE.id_tienda AND HDET.id_usuario = RE.id_usuario  " & _
                        "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region  " & _
                        "WHERE RE.id_periodo ='" & cmbPeriodo.SelectedValue & "'     " & _
                        "AND Cumplimiento=1 AND TI.id_clasificacion<>0 AND TI.id_clasificacion<>2)PVI    " & _
                        "ON RE.id_tienda = PVI.id_tienda AND PVI.id_periodo = RE.id_periodo AND PVI.id_usuario = RE.id_usuario  " & _
                        "WHERE RE.id_periodo ='" & cmbPeriodo.SelectedValue & "'    " & _
                        "GROUP BY RE.region_mars,RE.id_usuario) as H2 ON H.id_usuario=H2.id_usuario AND H.region_mars = H2.region_mars  " & _
                        "INNER JOIN (SELECT id_usuario,CASE WHEN COUNT(id_tienda)=SUM(estatus_anaquel) then 'SI' else 'NO' end Capturas  " & _
                        "FROM AS_Rutas_Eventos  " & _
                        "WHERE id_periodo='" & cmbPeriodo.SelectedValue & "' GROUP BY id_usuario)Capturas ON Capturas.id_usuario = H.id_usuario  " & _
                        "WHERE H.id_periodo='" & cmbPeriodo.SelectedValue & "'" & _
                        " " + RegionSQL + " " & _
                        " " + EjecutivoSQL + " " & _
                        " " + SupervisorSQL + " " & _
                        " " + PromotorSQL + " ORDER BY H.id_usuario"

            CargaGrilla(ConexionMars.localSqlServer, sql, gridReporte)
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

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        CargarReporte()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 3 To 9
                e.Row.Cells(i).Text = e.Row.Cells(i).Text & "%"
            Next i

            e.Row.Cells(11).Text = e.Row.Cells(11).Text & "%"
            e.Row.Cells(16).Text = e.Row.Cells(16).Text & "%"

            If gridReporte.DataKeys(e.Row.RowIndex).Value.ToString() = "NO" Then
                e.Row.Cells(16).BackColor = Drawing.Color.Red
                e.Row.Cells(16).Text = "0%"
            End If
        End If
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte bonos " + cmbPeriodo.SelectedItem.ToString() + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
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
End Class