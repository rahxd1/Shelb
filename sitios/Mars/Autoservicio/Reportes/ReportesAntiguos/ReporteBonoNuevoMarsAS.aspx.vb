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

Partial Public Class ReporteBonoNuevoMarsAS
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
            RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
            EjecutivoSQL = Acciones.Slc.cmb("REL.region_mars", cmbEjecutivo.SelectedValue)
            SupervisorSQL = Acciones.Slc.cmb("REL.id_supervisor", cmbSupervisor.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("RE.id_usuario", cmbPromotor.SelectedValue)

            CargaGrilla(ConexionMars.localSqlServer, _
                        "select RE.id_periodo,REL.region_mars +' - '+US.nombre Ejecutivo,RE.id_usuario,TI.area_nielsen,REG.nombre_region, " & _
                         "CASE WHEN (CASE when ((SUM(HDET.[1])+SUM(HDET.[15]))+SUM(HDET.[5]))=0 then 0 else " & _
                         "cast(round((SUM(HDET.[1])+SUM(HDET.[15]))/cast(((SUM(HDET.[1])+SUM(HDET.[15]))+SUM(HDET.[5])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end)>=NIL.O_ps " & _
                         "then 22.5 else 0 end ASS, " & _
                         "CASE WHEN (CASE when (SUM(HDET.[2])+SUM(HDET.[6]))=0 then 0 else " & _
                         "cast(round(SUM(HDET.[2])/cast((SUM(HDET.[2])+SUM(HDET.[6])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end)>=NIL.O_pc " & _
                         "then 7.5 else 0 end CS, " & _
                         "CASE WHEN (CASE when (SUM(HDET.[3])+SUM(HDET.[7]))=0 then 0 else " & _
                         "cast(round(SUM(HDET.[3])/cast((SUM(HDET.[3])+SUM(HDET.[7])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end)>=NIL.O_ph " & _
                         "then 6.5 else 0 end PH, " & _
                         "CASE WHEN (CASE when (SUM(HDET.[8])+SUM(HDET.[4]))=0 then 0 else " & _
                         "cast(round(SUM(HDET.[4])/cast((SUM(HDET.[4])+SUM(HDET.[8])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end)>=NIL.O_pb " & _
                         "then 1.5 else 0 end PB, " & _
                         "CASE WHEN (CASE when (SUM(HDET.[9])+SUM(HDET.[12]))=0 then 0 else " & _
                         "cast(round(SUM(HDET.[9])/cast((SUM(HDET.[9])+SUM(HDET.[12])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end)>=NIL.O_gs " & _
                         "then 20 else 0 end GS, " & _
                         "CASE WHEN (CASE when (SUM(HDET.[10])+SUM(HDET.[13]))=0 then 0 else " & _
                         "cast(round(SUM(HDET.[10])/cast((SUM(HDET.[10])+SUM(HDET.[13])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end)>=NIL.O_gh " & _
                         "then 1.5 else 0 end GH, " & _
                         "CASE WHEN (CASE when (SUM(HDET.[11])+SUM(HDET.[14]))=0 then 0 else " & _
                         "cast(round(SUM(HDET.[11])/cast((SUM(HDET.[11])+SUM(HDET.[14])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end)>=NIL.O_gb " & _
                         "then 0.5 else 0 end GB, " & _
                         "convert(nvarchar(5),CASE WHEN (CASE when ((SUM(HDET.[1])+SUM(HDET.[15]))+SUM(HDET.[5]))=0 then 0 else cast(round((SUM(HDET.[1])+SUM(HDET.[15]))/cast(((SUM(HDET.[1])+SUM(HDET.[15]))+SUM(HDET.[5])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end)>=NIL.O_ps then 22.5 else 0 end +  " & _
                         "CASE WHEN (CASE when (SUM(HDET.[2])+SUM(HDET.[6]))=0 then 0 else cast(round(SUM(HDET.[2])/cast((SUM(HDET.[2])+SUM(HDET.[6])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end)>=NIL.O_pc then 7.5 else 0 end +  " & _
                         "CASE WHEN (CASE when (SUM(HDET.[3])+SUM(HDET.[7]))=0 then 0 else cast(round(SUM(HDET.[3])/cast((SUM(HDET.[3])+SUM(HDET.[7])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end)>=NIL.O_ph then 6.5 else 0 end +  " & _
                         "CASE WHEN (CASE when (SUM(HDET.[8])+SUM(HDET.[4]))=0 then 0 else cast(round(SUM(HDET.[4])/cast((SUM(HDET.[4])+SUM(HDET.[8])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end)>=NIL.O_pb then 1.5 else 0 end +  " & _
                         "CASE WHEN (CASE when (SUM(HDET.[9])+SUM(HDET.[12]))=0 then 0 else cast(round(SUM(HDET.[9])/cast((SUM(HDET.[9])+SUM(HDET.[12])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end)>=NIL.O_gs then 20 else 0 end +  " & _
                         "CASE WHEN (CASE when (SUM(HDET.[10])+SUM(HDET.[13]))=0 then 0 else cast(round(SUM(HDET.[10])/cast((SUM(HDET.[10])+SUM(HDET.[13])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end)>=NIL.O_gh then 1.5 else 0 end +  " & _
                         "CASE WHEN (CASE when (SUM(HDET.[11])+SUM(HDET.[14]))=0 then 0 else cast(round(SUM(HDET.[11])/cast((SUM(HDET.[11])+SUM(HDET.[14])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end)>=NIL.O_gb then 0.5 else 0 end)+'%' Part_Anaquel, " & _
                         "convert(nvarchar(5),(CASE WHEN H2.ParaBono<=H2.PVI then '40' else '0'end))+'%'Exhibiciones, " & _
                         "convert(nvarchar(5),((CASE WHEN (CASE when ((SUM(HDET.[1])+SUM(HDET.[15]))+SUM(HDET.[5]))=0 then 0 else  " & _
                         "cast(round((SUM(HDET.[1])+SUM(HDET.[15]))/cast(((SUM(HDET.[1])+SUM(HDET.[15]))+SUM(HDET.[5])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end)>=NIL.O_ps  " & _
                         "then 22.5 else 0 end)+ " & _
                         "(CASE WHEN (CASE when (SUM(HDET.[2])+SUM(HDET.[6]))=0 then 0 else  " & _
                         "cast(round(SUM(HDET.[2])/cast((SUM(HDET.[2])+SUM(HDET.[6])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end)>=NIL.O_pc  " & _
                         "then 7.5 else 0 end)+  " & _
                         "(CASE WHEN (CASE when (SUM(HDET.[3])+SUM(HDET.[7]))=0 then 0 else  " & _
                         "cast(round(SUM(HDET.[3])/cast((SUM(HDET.[3])+SUM(HDET.[7])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end)>=NIL.O_ph  " & _
                         "then 6.5 else 0 end)+  " & _
                         "(CASE WHEN (CASE when (SUM(HDET.[8])+SUM(HDET.[4]))=0 then 0 else  " & _
                         "cast(round(SUM(HDET.[4])/cast((SUM(HDET.[4])+SUM(HDET.[8])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end)>=NIL.O_pb  " & _
                         "then 1.5 else 0 end)+  " & _
                         "(CASE WHEN (CASE when (SUM(HDET.[9])+SUM(HDET.[12]))=0 then 0 else  " & _
                         "cast(round(SUM(HDET.[9])/cast((SUM(HDET.[9])+SUM(HDET.[12])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end)>=NIL.O_gs  " & _
                         "then 20 else 0 end)+  " & _
                         "(CASE WHEN (CASE when (SUM(HDET.[10])+SUM(HDET.[13]))=0 then 0 else  " & _
                         "cast(round(SUM(HDET.[10])/cast((SUM(HDET.[10])+SUM(HDET.[13])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end)>=NIL.O_gh  " & _
                         "then 1.5 else 0 end)+ " & _
                         "(CASE WHEN (CASE when (SUM(HDET.[11])+SUM(HDET.[14]))=0 then 0 else  " & _
                         "cast(round(SUM(HDET.[11])/cast((SUM(HDET.[11])+SUM(HDET.[14])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end)>=NIL.O_gb  " & _
                         "then 0.5 else 0 end)+(CASE WHEN H2.ParaBono<=H2.PVI then '40' else '0'end)))+'%'Total, " & _
                         "H2.Pautas, H2.PVI, H2.Porcentaje,H2.ParaBono,Capturas.Capturas " & _
                         "FROM AS_Rutas_Eventos as RE " & _
                         "FULL JOIN AS_Historial as H " & _
                         "ON H.id_periodo= RE.id_periodo AND RE.id_usuario=H.id_usuario AND RE.id_tienda=H.id_tienda AND H.id_quincena=RE.id_quincena " & _
                         "FULL JOIN AS_Segmentos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                         "FULL JOIN Usuarios_Relacion as REL ON REL.id_usuario = RE.id_usuario " & _
                         "FULL JOIN Usuarios as US ON US.id_usuario = REL.region_mars " & _
                         "FULL JOIN AS_Tiendas as TI ON TI.id_tienda = RE.id_tienda " & _
                         "FULL JOIN AS_Area_Nielsen as NIL ON NIL.area_nielsen = TI.area_nielsen " & _
                         "FULL JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                         "FULL JOIN (select RE.region_mars, RE.id_usuario," & _
                         "COUNT(DISTINCT Captura.id_tienda)Pautas,ISNULL(SUM(PVI.Cumplimiento),0)PVI, " & _
                         "ISNULL((100*ISNULL(SUM(PVI.Cumplimiento),0)/COUNT(RE.id_tienda)),0)Porcentaje, " & _
                         "round(((80*COUNT(DISTINCT Captura.id_tienda))/100),2)ParaBono " & _
                         "FROM (select DISTINCT id_periodo, US.nombre, RE.id_tienda,REL.region_mars,RE.id_usuario " & _
                         "from AS_Rutas_Eventos as RE " & _
                         "INNER JOIN AS_Tiendas as TI ON TI.id_tienda = RE.id_tienda  " & _
                         "INNER JOIN Usuarios_Relacion AS REL ON REL.id_usuario = RE.id_usuario " & _
                         "INNER JOIN Usuarios as US ON US.id_usuario = REL.region_mars  " & _
                         "where id_periodo ='" & cmbPeriodo.SelectedValue & "' " + RegionSQL + EjecutivoSQL + SupervisorSQL + ")as RE " & _
                         "FULL JOIN (select DISTINCT RE.id_periodo, RE.id_tienda " & _
                         "FROM AS_Rutas_Eventos as RE " & _
                         "INNER JOIN AS_Tiendas as TI ON TI.id_tienda = RE.id_tienda " & _
                         "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                         "INNER JOIN Usuarios_Relacion AS REL ON REL.id_usuario = RE.id_usuario " & _
                         "WHERE id_periodo ='" & cmbPeriodo.SelectedValue & "' " + RegionSQL + EjecutivoSQL + SupervisorSQL + " AND TI.id_clasificacion<>0 AND TI.id_clasificacion<>2)Captura   " & _
                         "ON Captura.id_tienda = RE.id_tienda AND Captura.id_periodo=RE.id_periodo " & _
                         "FULL JOIN (SELECT DISTINCT RE.id_tienda,HDET.Cumplimiento, RE.id_periodo " & _
                         "FROM AS_Rutas_Eventos as RE " & _
                         "INNER JOIN AS_Tiendas as TI ON RE.id_tienda = TI.id_tienda " & _
                         "INNER JOIN Usuarios_Relacion AS REL ON REL.id_usuario = RE.id_usuario " & _
                         "FULL JOIN (select DISTINCT H.id_periodo, H.id_tienda, " & _
                         "(CASE when(CASE when CTI.id_clasificacion=1 OR CTI.id_clasificacion=6 OR CTI.id_clasificacion=4 " & _
                         "then (CASE when((HDET.[1])+(HDET.[2])+(HDET.[3])+(HDET.[4])+(HDET.[5])+(HDET.[6])+((HDET.[7]))+(HDET.[8]))>=CTI.O_1 then 1 else 0 end) " & _
                         "when CTI.id_clasificacion=3 OR CTI.id_clasificacion=5 " & _
                         "then (CASE when((HDET.[1])+(HDET.[2])+(HDET.[3])+(HDET.[4])+(HDET.[5]))>=CTI.O_1 then 1 else 0 end)end)+ " & _
                         "(CASE when CTI.id_clasificacion=3 OR CTI.id_clasificacion=5 " & _
                         "then (CASE when((HDET.[6])+((HDET.[7]))+(HDET.[8]))>=CTI.O_6 then 1 else 0 end)else 0 end)+ " & _
                         "(CASE WHEN CTI.id_clasificacion<>4 then(CASE when TI.id_cadena=7 " & _
                         "then 1 else (CASE when (HDET.[10])>=CTI.O_10 then 1 else 0 end)end)else 0 end)+ " & _
                         "(CASE when (HDET.[12])>=CTI.O_12 then 1 else 0 end)+ " & _
                         "(CASE WHEN CTI.id_clasificacion=4 " & _
                         "then (CASE when ((HDET.[9])+(HDET.[11]))>=CTI.O_9 then 1 else 0 end) " & _
                         "else (CASE WHEN CTI.O_9<>0 then(CASE when (HDET.[9])>=CTI.O_9 then 1 else 0 end)else 0 end)+ " & _
                         "(CASE when CTI.id_clasificacion=3 OR CTI.id_clasificacion=6 " & _
                         "then (CASE WHEN CTI.O_11<>0 then(CASE when (HDET.[11])>=CTI.O_11 then 1 else 0 end)else 0 end)else 0 end)end)>= CTI.Total then 1 else 0 end)Cumplimiento " & _
                         "FROM AS_Historial as H " & _
                         "INNER JOIN Usuarios_Relacion AS REL ON REL.id_usuario = H.id_usuario " & _
                         "INNER JOIN AS_Tiendas as TI ON H.id_tienda = TI.id_tienda " & _
                         "INNER JOIN AS_Clasificacion_Tiendas AS CTI ON CTI.id_clasificacion = TI.id_clasificacion   " & _
                         "INNER JOIN (select folio_historial,ISNULL([1],0)[1],ISNULL([2],0)[2],ISNULL([3],0)[3],ISNULL([4],0)[4], " & _
                             "ISNULL([5],0)[5],ISNULL([6],0)[6],ISNULL([7],0)[7],ISNULL([8],0)[8],ISNULL([9],0)[9], " & _
                             "ISNULL([10],0)[10],ISNULL([11],0)[11],ISNULL([12],0)[12] " & _
                             "from(select folio_historial, id_exhibidor, cantidad " & _
                             "from AS_Puntos_Interrupcion_Historial_Det) as H " & _
                             "pivot(sum(cantidad)for id_exhibidor in ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12]))HDET ) as HDET ON H.folio_historial= HDET.folio_historial  " & _
                         "WHERE H.id_periodo ='" & cmbPeriodo.SelectedValue & "' AND NOT TI.id_clasificacion=0 " + RegionSQL + EjecutivoSQL + SupervisorSQL + ")HDET " & _
                         "ON HDET.id_tienda = RE.id_tienda " & _
                         "WHERE RE.id_periodo ='" & cmbPeriodo.SelectedValue & "' " + RegionSQL + EjecutivoSQL + SupervisorSQL + " and Cumplimiento=1 AND TI.id_clasificacion<>0 AND TI.id_clasificacion<>2)PVI   " & _
                         "ON RE.id_tienda = PVI.id_tienda AND PVI.id_periodo = RE.id_periodo " & _
                         "WHERE RE.id_periodo ='" & cmbPeriodo.SelectedValue & "' " & _
                         "GROUP BY RE.region_mars, RE.id_usuario) as H2 ON RE.id_usuario=H2.id_usuario AND H2.region_mars = REL.region_mars " & _
                         "FULL JOIN (SELECT id_usuario,CASE WHEN COUNT(id_tienda)=SUM(estatus_anaquel) then 'SI' else 'NO' end Capturas " & _
                         "FROM AS_Rutas_Eventos " & _
                         "WHERE id_periodo='" & cmbPeriodo.SelectedValue & "' GROUP BY id_usuario)Capturas ON Capturas.id_usuario = RE.id_usuario " & _
                         "WHERE RE.id_periodo='" & cmbPeriodo.SelectedValue & "' " & _
                         " " + RegionSQL + " " & _
                         " " + EjecutivoSQL + " " & _
                         " " + SupervisorSQL + " " & _
                         " " + PromotorSQL + " " & _
                         "GROUP BY RE.id_periodo,REL.region_mars,US.nombre,RE.id_usuario,TI.area_nielsen,REG.nombre_region,Capturas.Capturas, " & _
                         "NIL.O_ps,NIL.O_pc,NIL.O_ph,NIL.O_pb,NIL.O_gs,NIL.O_gh,NIL.O_gb,H2.Pautas, H2.PVI, H2.Porcentaje,H2.ParaBono " & _
                         "ORDER BY RE.id_usuario", gridReporte)
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
            Dim Total As Double
            Total = e.Row.Cells(3).Text : e.Row.Cells(3).Text = Total & "%"
            Total = e.Row.Cells(4).Text : e.Row.Cells(4).Text = Total & "%"
            Total = e.Row.Cells(5).Text : e.Row.Cells(5).Text = Total & "%"
            Total = e.Row.Cells(6).Text : e.Row.Cells(6).Text = Total & "%"
            Total = e.Row.Cells(7).Text : e.Row.Cells(7).Text = Total & "%"
            Total = e.Row.Cells(8).Text : e.Row.Cells(8).Text = Total & "%"
            Total = e.Row.Cells(9).Text : e.Row.Cells(9).Text = Total & "%"

            e.Row.Cells(11).Text = e.Row.Cells(11).Text & "%"

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