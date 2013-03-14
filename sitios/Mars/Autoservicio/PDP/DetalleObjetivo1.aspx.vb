Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class DetalleObjetivo1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Objetivo()
        End If
    End Sub

    Sub Objetivo()
        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT DISTINCT PER.orden,PER.nombre_periodo,RE.region_mars + '- '+ RE.ejecutivo as EjecutivoMars,RE.nombre_region, RE.id_usuario,   " & _
                    "RE.clasificacion_tienda,RE.codigo,RE.nombre, RE.nombre_cadena, RE.id_tienda, " & _
                    "ISNULL(PVI.Cumplimiento,'NO') as PVI, " & _
                    "RE.[1],RE.[2],RE.[3],RE.[4],RE.[5], " & _
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
                    "CASE when RE.id_clasificacion=1 OR RE.id_clasificacion=6 OR RE.id_clasificacion=4 " & _
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
                    "CASE when RE.id_clasificacion=3 OR RE.id_clasificacion=5  " & _
                    "then convert(nvarchar(5),RE.O_6) else '' end as O_678, " & _
                    "CASE when RE.id_clasificacion=3 OR RE.id_clasificacion=5 " & _
                    "then convert(nvarchar(5),(CASE WHEN(RE.[6])+((RE.[7]))+(RE.[8])>=RE.O_6 then '100' else     " & _
                    "convert(float,(100*(RE.[6])+((RE.[7]))+(RE.[8]))/RE.O_6) end)) " & _
                    "else '' end as Por_678, " & _
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
                    "FROM (select DISTINCT H.folio_historial,H.id_quincena,RE.id_periodo, US.nombre as ejecutivo, RE.id_tienda, REG.nombre_region, RELUS.region_mars,RE.id_usuario,  " & _
                    "TI.id_cadena,TI.id_clasificacion, CTI.clasificacion_tienda,CAD.nombre_cadena,TI.codigo,TI.nombre,  " & _
                    "CTI.O_1, CTI.O_2, CTI.O_3, CTI.O_4, CTI.O_5, CTI.O_6, CTI.O_7, CTI.O_8, CTI.O_9, CTI.O_10, CTI.O_11, CTI.O_12, CTI.O_13, CTI.O_14, " & _
                    "HDET.[1],HDET.[2],HDET.[3],HDET.[4],HDET.[5],HDET.[6],HDET.[7],HDET.[8],HDET.[9],HDET.[10],HDET.[11],HDET.[12],HDET.[13],HDET.[14] " & _
                    "from AS_Rutas_Eventos RE " & _
                    "INNER JOIN AS_Historial as H ON RE.id_tienda=H.id_tienda " & _
                    "AND RE.id_usuario = H.id_usuario AND H.id_periodo=RE.id_periodo AND H.id_quincena=RE.id_quincena " & _
                    "INNER JOIN AS_Exhibidores_Historial_Det as HDET ON H.folio_historial=HDET.folio_historial " & _
                    "INNER JOIN AS_Tiendas as TI ON TI.id_tienda = RE.id_tienda " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena= TI.id_cadena " & _
                    "INNER JOIN AS_Clasificacion_Tiendas AS CTI ON CTI.id_clasificacion = TI.id_clasificacion " & _
                    "INNER JOIN Usuarios_Relacion AS RELUS ON RELUS.id_usuario = RE.id_usuario " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario = RELUS.region_mars " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    "where RE.id_usuario = '" & Request.Params("id_usuario") & "' AND CTI.orden<>0)as RE " & _
                    "INNER JOIN (SELECT DISTINCT orden, id_periodo, nombre_periodo " & _
                    "FROM Periodos)PER ON PER.id_periodo = RE.id_periodo " & _
                    "INNER JOIN (SELECT DISTINCT RE.id_tienda, RE.id_usuario,RE.id_periodo, " & _
                    "CASE WHEN ISNULL(H1.Total,0)>= ISNULL(H2.Total,0) " & _
                    "THEN (CASE when ISNULL(H1.Total,0)=0 then ISNULL(H2.folio_historial,0) ELSE ISNULL(H1.folio_historial,0)END) ELSE " & _
                    "ISNULL(H2.folio_historial,0) END as folio_historial " & _
                    "FROM (SELECT DISTINCT id_periodo, id_tienda, id_usuario " & _
                    "FROM AS_Rutas_Eventos as RE " & _
                    "WHERE RE.id_usuario = '" & Request.Params("id_usuario") & "')RE " & _
                    "FULL JOIN(select H.id_tienda,H.id_usuario,H.folio_historial,H.id_periodo, " & _
                    "(HDET.[1]+HDET.[2]+HDET.[3]+HDET.[4]+HDET.[5]+HDET.[6]+HDET.[7]+ " & _
                    "HDET.[8]+HDET.[9]+HDET.[10]+HDET.[11]+HDET.[12]+HDET.[13]+HDET.[14])Total " & _
                    "FROM AS_Historial as H  " & _
                    "INNER JOIN AS_Exhibidores_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial  " & _
                    "WHERE H.id_quincena='Q1' AND H.id_usuario='" & Request.Params("id_usuario") & "')H1 " & _
                    "ON RE.id_tienda = H1.id_tienda AND RE.id_usuario = H1.id_usuario AND RE.id_periodo=H1.id_periodo " & _
                    "FULL JOIN (select H.id_tienda,H.id_usuario,H.folio_historial,H.id_periodo, " & _
                    "(HDET.[1]+HDET.[2]+HDET.[3]+HDET.[4]+HDET.[5]+HDET.[6]+HDET.[7]+ " & _
                    "HDET.[8]+HDET.[9]+HDET.[10]+HDET.[11]+HDET.[12]+HDET.[13]+HDET.[14])Total " & _
                    "FROM AS_Historial as H  " & _
                    "INNER JOIN AS_Exhibidores_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial  " & _
                    "WHERE H.id_quincena='Q2' AND H.id_usuario='" & Request.Params("id_usuario") & "')H2  " & _
                    "ON RE.id_tienda = H2.id_tienda AND RE.id_usuario = H2.id_usuario AND RE.id_periodo=H2.id_periodo)Folio " & _
                    "ON RE.folio_historial = Folio.folio_historial " & _
                    "FULL JOIN (SELECT DISTINCT RE.id_usuario,RE.id_tienda,HDET.Cumplimiento, RE.id_periodo  " & _
                    "FROM AS_Rutas_Eventos as RE  " & _
                    "INNER JOIN AS_Tiendas as TI ON RE.id_tienda = TI.id_tienda  " & _
                    "INNER JOIN Usuarios_Relacion AS RELUS ON RELUS.id_usuario = RE.id_usuario  " & _
                    "FULL JOIN (select DISTINCT H.id_usuario,H.id_periodo, H.id_tienda,  " & _
                    "(CASE when(CASE when CTI.id_clasificacion=1 OR CTI.id_clasificacion=6 OR CTI.id_clasificacion=4  " & _
                    "then (CASE when((HDET.[1])+(HDET.[2])+(HDET.[3])+(HDET.[4])+(HDET.[5])+(HDET.[6])+((HDET.[7]))+(HDET.[8]))>=CTI.O_1 then 1 else 0 end) " & _
                    "when CTI.id_clasificacion=3 OR CTI.id_clasificacion=5 " & _
                    "then (CASE when((HDET.[1])+(HDET.[2])+(HDET.[3])+(HDET.[4])+(HDET.[5]))>=CTI.O_1 then 1 else 0 end)end)+  " & _
                    "(CASE when CTI.id_clasificacion=3 OR CTI.id_clasificacion=5 " & _
                    "then (CASE when((HDET.[6])+((HDET.[7]))+(HDET.[8]))>=CTI.O_6 then 1 else 0 end)else 0 end)+ " & _
                    "(CASE WHEN CTI.id_clasificacion<>4 then(CASE when TI.id_cadena=7 " & _
                    "then 1 else (CASE when (HDET.[10])>=CTI.O_10 then 1 else 0 end)end)else 0 end)+ " & _
                    "(CASE when (HDET.[12])>=CTI.O_12 then 1 else 0 end)+ " & _
                    "(CASE WHEN CTI.id_clasificacion=4 " & _
                    "then (CASE when ((HDET.[9])+(HDET.[11]))>=CTI.O_9 then 1 else 0 end) " & _
                    "else (CASE WHEN CTI.O_9<>0 then(CASE when (HDET.[9])>=CTI.O_9 then 1 else 0 end)else 0 end)+ " & _
                    "(CASE when CTI.id_clasificacion=3 OR CTI.id_clasificacion=6 " & _
                    "then (CASE WHEN CTI.O_11<>0 then(CASE when (HDET.[11])>=CTI.O_11 then 1 else 0 end)else 0 end)else 0 end)end)>= CTI.Total then 'SI' else '' end)Cumplimiento " & _
                    "FROM AS_Historial H " & _
                    "INNER JOIN Usuarios_Relacion AS RELUS ON RELUS.id_usuario = H.id_usuario " & _
                    "INNER JOIN AS_Tiendas as TI ON H.id_tienda = TI.id_tienda " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    "INNER JOIN AS_Clasificacion_Tiendas AS CTI ON CTI.id_clasificacion = TI.id_clasificacion " & _
                    "INNER JOIN AS_Exhibidores_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial " & _
                    "WHERE H.id_usuario = '" & Request.Params("id_usuario") & "' AND CTI.orden<>0 )HDET " & _
                    "ON HDET.id_tienda = RE.id_tienda AND HDET.id_usuario = RE.id_usuario " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    "WHERE RE.id_usuario = '" & Request.Params("id_usuario") & "' and Cumplimiento='SI')PVI " & _
                    "ON RE.id_tienda = PVI.id_tienda AND PVI.id_periodo = RE.id_periodo and PVI.id_usuario=RE.id_usuario " & _
                    "ORDER BY PER.orden, RE.id_tienda", Me.gridDetalle)
    End Sub

    Protected Sub lnkObjetivo1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkObjetivo1.Click
        Response.Redirect("~/sitios/Mars/Autoservicio/PDP/ReportePDPMarsAS.aspx?id_usuario=" & Request.Params("id_usuario") & "")
    End Sub

    Private Sub lnkExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.gridDetalle.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.gridDetalle)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte PDP Objetivo 1.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class