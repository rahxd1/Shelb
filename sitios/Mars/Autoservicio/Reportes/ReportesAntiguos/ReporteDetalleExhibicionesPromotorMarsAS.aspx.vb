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

Partial Public Class ReporteDetalleExhibicionesPromotorMarsAS
    Inherits System.Web.UI.Page

    Dim Suma(50), Total(50) As Integer
    Dim Filas1, Filas2 As Integer

    Sub SQLCombo()
        MarsAS.SQLsCombo(cmbPeriodo.SelectedValue, cmbQuincena.SelectedValue, _
                          cmbRegion.SelectedValue, cmbEjecutivo.SelectedValue, _
                          cmbSupervisor.SelectedValue, cmbPromotor.SelectedValue, _
                          cmbCadena.SelectedValue, "View_Historial_AS")
    End Sub

    Sub CargarReporte()
        Dim QuincenaSQL, QuincenaHSQL, RegionSQL, EjecutivoSQL, SupervisorSQL, PromotorSQL, PromotorHSQL, CadenaSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            Dim Q1, Q2 As String

            If cmbQuincena.SelectedValue = "" Then
                QuincenaSQL = ""
                QuincenaHSQL = ""
                Q1 = "Q1" : Q2 = "Q2" : Else
                QuincenaSQL = "AND id_quincena='" & cmbQuincena.SelectedValue & "' "
                QuincenaHSQL = "AND H.id_quincena='" & cmbQuincena.SelectedValue & "' "
                Q1 = cmbQuincena.SelectedValue : Q2 = cmbQuincena.SelectedValue : End If

            RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
            EjecutivoSQL = Acciones.Slc.cmb("REL.region_mars", cmbEjecutivo.SelectedValue)
            SupervisorSQL = Acciones.Slc.cmb("REL.id_supervisor", cmbSupervisor.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("RE.id_usuario", cmbPromotor.SelectedValue)
            PromotorHSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("TI.id_cadena", cmbCadena.SelectedValue)

            CargaGrilla(ConexionMars.localSqlServer, _
                        "select DISTINCT RE.region_mars + '- '+ RE.ejecutivo as EjecutivoMars,RE.nombre_region, RE.id_usuario, " & _
                        "RE.clasificacion_tienda,RE.codigo,RE.nombre, RE.nombre_cadena, RE.id_tienda, " & _
                        "ISNULL(PVI.Cumplimiento,'NO') as PVI,RE.[1],RE.[2],RE.[3],RE.[4],RE.[5], " & _
                        "CASE when RE.id_clasificacion<>3 AND RE.id_clasificacion<>5 " & _
                        "then convert(nvarchar(5),RE.[6]) else '' end as [6], " & _
                        "CASE when RE.id_clasificacion<>3 AND RE.id_clasificacion<>5 " & _
                        "then convert(nvarchar(5),RE.[7]) else '' end as [7], " & _
                        "CASE when RE.id_clasificacion<>3 AND RE.id_clasificacion<>5 " & _
                        "then convert(nvarchar(5),RE.[8]) else '' end as [8], " & _
                        "CASE when RE.id_clasificacion=1 OR RE.id_clasificacion=6 OR RE.id_clasificacion=4 " & _
                        "then ((RE.[1])+(RE.[2])+(RE.[3])+(RE.[4])+(RE.[5])+(RE.[6])+((RE.[7]))+(RE.[8])) " & _
                        "when RE.id_clasificacion=3 OR RE.id_clasificacion=5 " & _
                        "then ((RE.[1])+(RE.[2])+(RE.[3])+(RE.[4])+(RE.[5])) end as T_12345, RE.O_1 as O_12345, " & _
                        "CASE when RE.id_clasificacion=1 OR RE.id_clasificacion=6 OR RE.id_clasificacion=4  " & _
                        "then (CASE WHEN ((RE.[1])+(RE.[2])+(RE.[3])+(RE.[4])+(RE.[5])+(RE.[6])+((RE.[7]))+(RE.[8]))>=RE.O_1 then '100' " & _
                        "else (100*((RE.[1])+(RE.[2])+(RE.[3])+(RE.[4])+(RE.[5])+(RE.[6])+((RE.[7]))+(RE.[8])))/ RE.O_1 end) " & _
                        "when RE.id_clasificacion=3 OR RE.id_clasificacion=5 " & _
                        "then (CASE WHEN ((RE.[1])+(RE.[2])+(RE.[3])+(RE.[4])+(RE.[5]))>=RE.O_1 then '100' " & _
                        "else (100*((RE.[1])+(RE.[2])+(RE.[3])+(RE.[4])+(RE.[5])))/ RE.O_1 end) end as Por_12345, " & _
                        "CASE when RE.id_clasificacion=3 OR RE.id_clasificacion=5 " & _
                        "then convert(nvarchar(5),RE.[6]) else '' end as [6_B], " & _
                        "CASE when RE.id_clasificacion=3 OR RE.id_clasificacion=5 " & _
                        "then convert(nvarchar(5),RE.[7]) else '' end as [7_B], " & _
                        "CASE when RE.id_clasificacion=3 OR RE.id_clasificacion=5 " & _
                        "then convert(nvarchar(5),RE.[8]) else '' end as [8_B], " & _
                        "CASE when RE.id_clasificacion=3 OR RE.id_clasificacion=5 " & _
                        "then convert(nvarchar(5),(RE.[6])+((RE.[7]))+(RE.[8])) else '' end as T_678, " & _
                        "CASE when RE.id_clasificacion=3 OR RE.id_clasificacion=5 " & _
                        "then convert(nvarchar(5),RE.O_6) else '' end as O_678, " & _
                        "CASE when RE.id_clasificacion=3 OR RE.id_clasificacion=5 " & _
                        "then convert(nvarchar(5),(CASE WHEN(RE.[6])+((RE.[7]))+(RE.[8])>=RE.O_6 then '100' else " & _
                        "convert(float,(100*(RE.[6])+((RE.[7]))+(RE.[8]))/RE.O_6) end)) else '' end as Por_678, " & _
                        "RE.[9],RE.O_9,CASE when RE.id_clasificacion=1 OR RE.id_clasificacion=5 " & _
                        "then '' else(CASE WHEN RE.O_9<>0 then(CASE WHEN(RE.[9])>=RE.O_9 then '100' else " & _
                        "convert(nvarchar(5),(100*(RE.[9]))/RE.O_9)end)else 0 end)end as Por_9, " & _
                        "CASE when RE.id_cadena=7 then '' else convert(nvarchar(5),RE.[10]) end as [10], " & _
                        "CASE when RE.id_cadena=7 then '' else convert(nvarchar(5),RE.O_10) end as O_10, " & _
                        "CASE when RE.id_clasificacion=4 then '' " & _
                        "else(CASE when RE.id_cadena<>7 then convert(nvarchar(5),(CASE WHEN(RE.[10])>=RE.O_10 then '100' else " & _
                        "convert(nvarchar(5),(100*(RE.[10]))/RE.O_10) end)) else '' end)end as Por_10, " & _
                        "RE.[11],RE.O_11,CASE when RE.id_clasificacion=1 OR RE.id_clasificacion=5 " & _
                        "then '' else(CASE WHEN(RE.[11])>=RE.O_11 then '100' else " & _
                        "convert(nvarchar(5),(100*(RE.[11]))/RE.O_11) end)end as Por_11, " & _
                        "RE.[12],RE.O_12,CASE WHEN(RE.[12])>=RE.O_12 then '100' else " & _
                        "convert(float,(100*(RE.[12]))/RE.O_12) end as Por_12, " & _
                        "RE.[13],RE.O_13,CASE WHEN(RE.[13])>=RE.O_13 then '100' else " & _
                        "convert(float,(100*(RE.[13]))/RE.O_13) end as Por_13, " & _
                        "RE.[14],RE.O_14,CASE WHEN(RE.[14])>=RE.O_14 then '100' else " & _
                        "convert(float,(100*(RE.[14]))/RE.O_14) end as Por_14 " & _
                        "FROM (select DISTINCT H.folio_historial,H.id_quincena,RE.id_periodo, US.nombre as ejecutivo, RE.id_tienda, REG.nombre_region, REL.region_mars,RE.id_usuario, " & _
                        "TI.id_cadena,TI.id_clasificacion, CTI.clasificacion_tienda,CAD.nombre_cadena,TI.codigo,TI.nombre, " & _
                        "CTI.O_1, CTI.O_2, CTI.O_3, CTI.O_4, CTI.O_5, CTI.O_6, CTI.O_7, CTI.O_8, CTI.O_9, CTI.O_10, CTI.O_11, CTI.O_12, CTI.O_13, CTI.O_14, " & _
                        "HDET.[1],HDET.[2],HDET.[3],HDET.[4],HDET.[5],HDET.[6],HDET.[7],HDET.[8],HDET.[9],HDET.[10],HDET.[11],HDET.[12],HDET.[13],HDET.[14] " & _
                        "from AS_Rutas_Eventos as RE " & _
                        "INNER JOIN AS_Historial as H ON RE.id_tienda=H.id_tienda " & _
                        "AND RE.id_usuario = H.id_usuario AND H.id_periodo=RE.id_periodo AND H.id_quincena=RE.id_quincena " & _
                        "INNER JOIN AS_Exhibidores_Historial_Det as HDET ON H.folio_historial=HDET.folio_historial " & _
                        "INNER JOIN AS_Tiendas as TI ON TI.id_tienda = RE.id_tienda " & _
                        "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena= TI.id_cadena " & _
                        "INNER JOIN AS_Clasificacion_Tiendas AS CTI ON CTI.id_clasificacion = TI.id_clasificacion " & _
                        "INNER JOIN Usuarios_Relacion AS REL ON REL.id_usuario = RE.id_usuario " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario = REL.region_mars " & _
                        "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                        "where RE.id_periodo ='" & cmbPeriodo.SelectedValue & "' " + QuincenaHSQL + RegionSQL + EjecutivoSQL + SupervisorSQL + PromotorSQL + CadenaSQL + " AND CTI.orden<>0)as RE  " & _
                        "INNER JOIN (SELECT DISTINCT RE.id_tienda, RE.id_usuario,RE.id_periodo, " & _
                        "CASE WHEN ISNULL(H1.Total,0)>= ISNULL(H2.Total,0) " & _
                        "THEN (CASE when ISNULL(H1.Total,0)=0 then ISNULL(H2.folio_historial,0) ELSE ISNULL(H1.folio_historial,0)END) ELSE " & _
                        "ISNULL(H2.folio_historial,0) END as folio_historial " & _
                        "FROM (SELECT DISTINCT id_periodo, id_tienda, id_usuario " & _
                        "FROM AS_Rutas_Eventos as RE " & _
                        "WHERE id_periodo='" & cmbPeriodo.SelectedValue & "' " + QuincenaSQL + PromotorSQL + ")RE " & _
                        "FULL JOIN(select H.id_tienda,H.id_usuario,H.folio_historial,H.id_periodo, " & _
                        "(HDET.[1]+HDET.[2]+HDET.[3]+HDET.[4]+HDET.[5]+HDET.[6]+HDET.[7]+ " & _
                        "HDET.[8]+HDET.[9]+HDET.[10]+HDET.[11]+HDET.[12]+HDET.[13]+HDET.[14])Total " & _
                        "FROM AS_Historial as H  " & _
                        "INNER JOIN AS_Exhibidores_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial  " & _
                        "WHERE H.id_periodo='" & cmbPeriodo.SelectedValue & "' AND H.id_quincena='" & Q1 & "')H1 " & _
                        "ON RE.id_tienda = H1.id_tienda AND RE.id_usuario = H1.id_usuario AND RE.id_periodo=H1.id_periodo " & _
                        "FULL JOIN (select H.id_tienda,H.id_usuario,H.folio_historial,H.id_periodo, " & _
                        "(HDET.[1]+HDET.[2]+HDET.[3]+HDET.[4]+HDET.[5]+HDET.[6]+HDET.[7]+ " & _
                        "HDET.[8]+HDET.[9]+HDET.[10]+HDET.[11]+HDET.[12]+HDET.[13]+HDET.[14])Total " & _
                        "FROM AS_Historial as H  " & _
                        "INNER JOIN AS_Exhibidores_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial  " & _
                        "WHERE H.id_periodo='" & cmbPeriodo.SelectedValue & "' AND H.id_quincena='" & Q2 & "')H2  " & _
                        "ON RE.id_tienda = H2.id_tienda AND RE.id_usuario = H2.id_usuario AND RE.id_periodo=H2.id_periodo)Folio " & _
                        "ON RE.folio_historial = Folio.folio_historial  " & _
                        "FULL JOIN (SELECT DISTINCT RE.id_usuario,RE.id_tienda,HDET.Cumplimiento, RE.id_periodo  " & _
                        "FROM AS_Rutas_Eventos as RE  " & _
                        "INNER JOIN AS_Tiendas as TI ON RE.id_tienda = TI.id_tienda  " & _
                        "INNER JOIN Usuarios_Relacion AS REL ON REL.id_usuario = RE.id_usuario  " & _
                        "FULL JOIN (select DISTINCT H.id_usuario,H.id_periodo, H.id_tienda,  " & _
                        "(CASE when(CASE when CTI.id_clasificacion=1 OR CTI.id_clasificacion=6 OR CTI.id_clasificacion=4  " & _
                        "then (CASE when((HDET.[1])+(HDET.[2])+(HDET.[3])+(HDET.[4])+(HDET.[5])+(HDET.[6])+((HDET.[7]))+(HDET.[8]))>=CTI.O_1 then 1 else 0 end)  " & _
                        "when CTI.id_clasificacion=3 OR CTI.id_clasificacion=5  " & _
                        "then (CASE when((HDET.[1])+(HDET.[2])+(HDET.[3])+(HDET.[4])+(HDET.[5]))>=CTI.O_1 then 1 else 0 end)end)+  " & _
                        "(CASE when CTI.id_clasificacion=3 OR CTI.id_clasificacion=5 " & _
                        "then (CASE when((HDET.[6])+((HDET.[7]))+(HDET.[8]))>=CTI.O_6 then 1 else 0 end)else 0 end)+ " & _
                        "(CASE WHEN CTI.id_clasificacion<>4 then(CASE when TI.id_cadena=7 " & _
                        "then 1 else (CASE when (HDET.[10])>=CTI.O_10 then 1 else 0 end)end)else 0 end)+ " & _
                        "(CASE when (HDET.[12])>=CTI.O_12 then 1 else 0 end)+ " & _
                        "(CASE WHEN CTI.id_clasificacion=4 " & _
                        "then (CASE when ((HDET.[9])+(HDET.[11]))>=CTI.O_9 then 1 else 0 end) " & _
                        "else (CASE WHEN CTI.O_9<>0 then(CASE when (HDET.[9])>=CTI.O_9 then 1 else 0 end)else 0 end)+ " & _
                        "(CASE when CTI.id_clasificacion=3 OR CTI.id_clasificacion=6  " & _
                        "then (CASE WHEN CTI.O_11<>0 then(CASE when (HDET.[11])>=CTI.O_11 then 1 else 0 end)else 0 end)else 0 end)end)>= CTI.Total then 'SI' else '' end)Cumplimiento " & _
                        "FROM AS_Historial as H " & _
                        "INNER JOIN Usuarios_Relacion AS REL ON REL.id_usuario = H.id_usuario " & _
                        "INNER JOIN AS_Tiendas as TI ON H.id_tienda = TI.id_tienda " & _
                        "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                        "INNER JOIN AS_Clasificacion_Tiendas AS CTI ON CTI.id_clasificacion = TI.id_clasificacion    " & _
                        "INNER JOIN AS_Exhibidores_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial    " & _
                        "WHERE H.id_periodo ='" & cmbPeriodo.SelectedValue & "' " + QuincenaSQL + RegionSQL + EjecutivoSQL + SupervisorSQL + PromotorHSQL + CadenaSQL + " AND CTI.orden<>0 )HDET " & _
                        "ON HDET.id_tienda = RE.id_tienda AND HDET.id_usuario = RE.id_usuario " & _
                        "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                        "WHERE RE.id_periodo ='" & cmbPeriodo.SelectedValue & "' " + QuincenaSQL + RegionSQL + EjecutivoSQL + SupervisorSQL + PromotorSQL + CadenaSQL + " and Cumplimiento='SI')PVI " & _
                        "ON RE.id_tienda = PVI.id_tienda AND PVI.id_periodo = RE.id_periodo and PVI.id_usuario=RE.id_usuario " & _
                        "ORDER BY RE.id_tienda", gridReporte)
        Else
            gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PeriodoSQLCmb, "nombre_periodo", "orden", cmbPeriodo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.QuincenaSQLCmb, "nombre_quincena", "id_quincena", cmbQuincena)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

            cmbPeriodo.SelectedValue = Request.Params("id_periodo")
            cmbQuincena.SelectedValue = Request.Params("id_quincena")
            cmbRegion.SelectedValue = Request.Params("id_region")
            cmbEjecutivo.SelectedValue = Request.Params("region_mars")
            cmbSupervisor.SelectedValue = Request.Params("id_supervisor")
            cmbPromotor.SelectedValue = Request.Params("id_usuario")

            If Request.Params("id_periodo") <> "" Then
                CargarReporte()
            End If
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCadena.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Protected Sub cmbQuincena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbQuincena.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Comentarios precios " + cmbPeriodo.SelectedItem.ToString() + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbEjecutivo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEjecutivo.SelectedIndexChanged
        cmbSupervisor.SelectedValue = ""

        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Private Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        cmbEjecutivo.SelectedValue = ""

        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 8 To 42
                If e.Row.Cells(i).Text <> "&nbsp;" Then
                    Suma(i) = Suma(i) + e.Row.Cells(i).Text : End If : Next i

            ''//Formato Porcentaje
            If e.Row.Cells(18).Text <> "&nbsp;" Then
                Total(18) = e.Row.Cells(18).Text
                e.Row.Cells(18).Text = Total(18) & "%" : End If

            For i = 24 To 42 Step 3
                If e.Row.Cells(i).Text <> "&nbsp;" Then
                    Total(i) = e.Row.Cells(i).Text
                    e.Row.Cells(i).Text = Total(i) & "%" : End If : Next i

            If e.Row.Cells(24).Text <> "&nbsp;" Then
                Filas1 = Filas1 + 1 : End If

            If e.Row.Cells(30).Text <> "&nbsp;" Then
                Filas2 = Filas2 + 1 : End If

            For i = 0 To CInt(gridReporte.Rows.Count) - 0
                If e.Row.Cells(7).Text = "NO" Then
                    For iC = 0 To 42
                        e.Row.Cells(iC).BackColor = Drawing.Color.Red : Next iC
                End If
            Next i
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            Dim Filas As Integer = gridReporte.Rows.Count
            For i = 8 To 42
                e.Row.Cells(i).Text = Suma(i) : Next i

            ''Formato porcentaje
            e.Row.Cells(18).Text = FormatNumber(Suma(18) / Filas, 2) & " %"
            e.Row.Cells(27).Text = FormatNumber(Suma(27) / Filas, 2) & " %"
            e.Row.Cells(33).Text = FormatNumber(Suma(33) / Filas, 2) & " %"
            e.Row.Cells(36).Text = FormatNumber(Suma(36) / Filas, 2) & " %"
            e.Row.Cells(39).Text = FormatNumber(Suma(39) / Filas, 2) & " %"
            e.Row.Cells(42).Text = FormatNumber(Suma(42) / Filas, 2) & " %"
            e.Row.Cells(24).Text = FormatNumber(Suma(24) / Filas1, 2) & " %"
            e.Row.Cells(30).Text = FormatNumber(Suma(30) / Filas2, 2) & " %"
        End If
    End Sub
End Class