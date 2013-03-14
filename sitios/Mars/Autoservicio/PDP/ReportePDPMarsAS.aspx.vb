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
Imports System.Text
Imports System.Data.Odbc
Imports System.IO.StringWriter
Imports System.Web.UI.WebControls.Style

Partial Public Class ReportePDPMarsAS
    Inherits System.Web.UI.Page

    Dim PromotorSel, RegionSel, EjecutivoSel, SupervisorSel, Region As String
    Dim PeriodoSQLA, PeriodoSQLB, RegionSQL, EjecutivoSQL, SupervisorSQL, PromotorSQL As String
    Dim PeriodoActual, Detalle As String
    Dim DatoObj1A, SumaObj1A, DatoObj1B, SumaObj1B, DatoObj1C, SumaObj1C As Integer
    Dim DatoObj2A, SumaObj2A, DatoObj2B, SumaObj2B, DatoObj2C, SumaObj2C As Integer
    Dim DatoObj3, SumaObj3 As Integer
    Dim DatoObj4A, SumaObj4A, DatoObj4B, SumaObj4B, DatoObj4C, SumaObj4C As Integer
    Dim Alcance4A, Alcance4B, Alcance4C As Double
    Dim DatoObj5A, SumaObj5A, DatoObj5B, SumaObj5B, DatoObj5C, SumaObj5C As Integer
    Dim Alcance1, Ponderacion1, Alcance2, Ponderacion2, _
        Alcance3, Ponderacion3, Alcance4, Ponderacion4, Alcance5, Ponderacion5 As Double
    Dim Dato(50), Suma(50) As Double
    Dim PrimerPeriodo As Integer

    Sub SQLCombo()
        RegionSel = Acciones.Slc.cmb("REL.id_region", cmbRegion.SelectedValue)
        EjecutivoSel = Acciones.Slc.cmb("REL.region_mars", cmbEjecutivo.SelectedValue)
        SupervisorSel = Acciones.Slc.cmb("REL.id_supervisor", cmbSupervisor.SelectedValue)

        PeriodoSQLA = "SELECT DISTINCT PER.orden,RE.id_periodo, PER.nombre_periodo, PER.fecha_fin_periodo " & _
                         "FROM AS_Rutas_Eventos as RE " & _
                         "INNER JOIN Periodos as PER ON RE.id_periodo = PER.id_periodo " & _
                         "ORDER BY PER.orden ASC"

        PeriodoSQLB = "SELECT DISTINCT PER.orden,RE.id_periodo, PER.nombre_periodo, PER.fecha_fin_periodo " & _
                         "FROM AS_Rutas_Eventos as RE " & _
                         "INNER JOIN Periodos as PER ON RE.id_periodo = PER.id_periodo " & _
                         "ORDER BY PER.orden DESC"

        RegionSQL = "SELECT * FROM Regiones"

        EjecutivoSQL = "SELECT DISTINCT REL.region_mars, REL.region_mars + ' - ' + US.nombre as Supervisor " & _
                        "FROM Usuarios_Relacion as REL " & _
                        "INNER JOIN Usuarios as US ON US.ciudad = REL.region_mars " & _
                        "WHERE REL.region_mars<>'' " & _
                        " " + RegionSel + PromotorSel + " "

        SupervisorSQL = "SELECT DISTINCT id_supervisor, id_supervisor + ' - ' + nombre as Supervisor " & _
                        "FROM Usuarios_Relacion as REL " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario = REL.id_supervisor " & _
                        "WHERE REL.id_supervisor<>'' " & _
                        " " + RegionSel + EjecutivoSel + PromotorSel + " "

        PromotorSQL = "SELECT DISTINCT REL.id_usuario " & _
                        "FROM Usuarios_Relacion as REL " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario = REL.id_usuario " & _
                        "WHERE REL.id_usuario<>'' " & _
                        " " + RegionSel + EjecutivoSel + SupervisorSel + PromotorSel + " "
    End Sub

    Sub PeriodoInicial()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select DISTINCT PER.id_periodo,PER.orden " & _
                    "FROM Periodos as PER " & _
                    "WHERE '" & lblFechaIngreso.Text & "' between PER.fecha_inicio_periodo " & _
                    "AND PER.fecha_fin_periodo")
        If Tabla.Rows.Count > 0 Then
            PrimerPeriodo = Tabla.Rows(0)("PER.orden")
        End If

        Tabla.Dispose()
    End Sub

    Sub VerDatos()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * FROM Usuarios " & _
                                               "WHERE id_usuario ='" & cmbPromotor.SelectedValue & "'")
        If Tabla.Rows.Count > 0 Then
            lblNombrePromotor.Text = Tabla.Rows(0)("nombre")
            lblFechaIngreso.Text = Tabla.Rows(0)("fecha_ingreso")
        End If

        Tabla.Dispose()
    End Sub

    Sub CargarReporte()
        CargarObjetivos("Select DISTINCT CTI.orden,CTI.clasificacion_tienda,isnull(Datos.id_usuario,'')id_usuario, " & _
                        "COUNT(Datos.Cumplimiento)Tiendas,ISNULL(SUM(Datos.Cumplimiento),0)Si " & _
                        "FROM(SELECT DISTINCT CTI.id_clasificacion,CTI.clasificacion_tienda,RE.id_periodo, RE.id_tienda,RE.id_usuario,  " & _
                        "CASE when HDET1.Folio<>0 then 1 else (CASE WHEN HDET2.Folio<>0 then 1 else 0 end) end Cumplimiento " & _
                        "FROM AS_Rutas_Eventos as RE  " & _
                        "INNER JOIN (SELECT DISTINCT orden,id_periodo,nombre_periodo FROM Periodos)PER ON RE.id_periodo=PER.id_periodo " & _
                        "FULL JOIN (select H.folio_historial,H.id_periodo, H.id_tienda, H.id_usuario, H.id_quincena, " & _
                        "(CASE when(CASE when CTI.id_clasificacion=1 OR CTI.id_clasificacion=6  " & _
                        "then (CASE when((HDET.[1])+(HDET.[2])+(HDET.[3])+(HDET.[6])+(HDET.[7]))>=CTI.O_1 then 1 else 0 end)  " & _
                        "when CTI.id_clasificacion=3 OR CTI.id_clasificacion=5  " & _
                        "then (CASE when((HDET.[1])+(HDET.[2])+(HDET.[3]))>=CTI.O_1 then 1 else 0 end)end)+  " & _
                        "(CASE when CTI.id_clasificacion=3 OR CTI.id_clasificacion=5  " & _
                        "then (CASE when((HDET.[6])+(HDET.[7]))>=CTI.O_6 then 1 else 0 end)else 0 end)+  " & _
                        "(CASE when (HDET.[9])>=CTI.O_9 then 1 else 0 end)+  " & _
                        "(CASE when TI.id_cadena=7 then 1 else (CASE when (HDET.[10])>=CTI.O_10 then 1 else 0 end)end)+  " & _
                        "(CASE when (HDET.[12])>=CTI.O_12 then 1 else 0 end)+  " & _
                        "(CASE when CTI.id_clasificacion=3 OR CTI.id_clasificacion=6  " & _
                        "then (CASE when (HDET.[11])>=CTI.O_11 then 1 else 0 end)else 0 end)>= CTI.Total then HDET.folio_historial else 0 end) Folio " & _
                        "FROM AS_Historial as H  " & _
                        "INNER JOIN (SELECT DISTINCT orden,id_periodo,nombre_periodo FROM Periodos)PER ON H.id_periodo=PER.id_periodo " & _
                        "INNER JOIN AS_Tiendas as TI ON H.id_tienda = TI.id_tienda  " & _
                        "INNER JOIN AS_Clasificacion_Tiendas AS CTI ON CTI.id_clasificacion = TI.id_clasificacion  " & _
                        "INNER JOIN AS_Exhibidores_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial  " & _
                        "WHERE H.id_quincena='Q1' AND H.id_usuario='" & cmbPromotor.SelectedValue & "')HDET1  " & _
                        "ON HDET1.id_tienda = RE.id_tienda AND HDET1.id_periodo = RE.id_periodo " & _
                        "FULL JOIN (select H.folio_historial,H.id_periodo, H.id_tienda, H.id_usuario, H.id_quincena, " & _
                        "(CASE when(CASE when CTI.id_clasificacion=1 OR CTI.id_clasificacion=6  " & _
                        "then (CASE when((HDET.[1])+(HDET.[2])+(HDET.[3])+(HDET.[6])+(HDET.[7]))>=CTI.O_1 then 1 else 0 end)  " & _
                        "when CTI.id_clasificacion=3 OR CTI.id_clasificacion=5  " & _
                        "then (CASE when((HDET.[1])+(HDET.[2])+(HDET.[3]))>=CTI.O_1 then 1 else 0 end)end)+  " & _
                        "(CASE when CTI.id_clasificacion=3 OR CTI.id_clasificacion=5  " & _
                        "then (CASE when((HDET.[6])+(HDET.[7]))>=CTI.O_6 then 1 else 0 end)else 0 end)+  " & _
                        "(CASE when (HDET.[9])>=CTI.O_9 then 1 else 0 end)+  " & _
                        "(CASE when TI.id_cadena=7 then 1 else (CASE when (HDET.[10])>=CTI.O_10 then 1 else 0 end)end)+  " & _
                        "(CASE when (HDET.[12])>=CTI.O_12 then 1 else 0 end)+  " & _
                        "(CASE when CTI.id_clasificacion=3 OR CTI.id_clasificacion=6  " & _
                        "then (CASE when (HDET.[11])>=CTI.O_11 then 1 else 0 end)else 0 end)>= CTI.Total then HDET.folio_historial else 0 end) Folio " & _
                        "FROM AS_Historial as H  " & _
                        "INNER JOIN AS_Tiendas as TI ON H.id_tienda = TI.id_tienda  " & _
                        "INNER JOIN AS_Clasificacion_Tiendas AS CTI ON CTI.id_clasificacion = TI.id_clasificacion  " & _
                        "INNER JOIN AS_Exhibidores_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial  " & _
                        "WHERE H.id_quincena='Q2' AND H.id_usuario='" & cmbPromotor.SelectedValue & "')HDET2 " & _
                        "ON HDET2.id_tienda = RE.id_tienda AND HDET2.id_periodo = RE.id_periodo " & _
                        "INNER JOIN AS_Tiendas as TI ON RE.id_tienda = TI.id_tienda  " & _
                        "INNER JOIN AS_Clasificacion_Tiendas AS CTI ON CTI.id_clasificacion = TI.id_clasificacion  " & _
                        "WHERE RE.id_usuario='" & cmbPromotor.SelectedValue & "' AND PER.orden BETWEEN " & cmbPeriodoA.SelectedValue & " AND " & cmbPeriodoB.SelectedValue & ")Datos " & _
                        "FULL JOIN AS_Clasificacion_Tiendas as CTI ON CTI.id_clasificacion =Datos.id_clasificacion " & _
                        "WHERE(CTI.orden <> 0) " & _
                        "GROUP BY CTI.orden,CTI.clasificacion_tienda,Datos.id_usuario ORDER BY CTI.orden", gridObjetivo1)

        CargarObjetivos("select 'Adulto Seco'as Segmento,sum(CASE WHEN HDET.Porcentaje>=HDET.Objetivo THEN 1 else 0 end)Cumplimiento, " & _
                        "COUNT(HDET.id_usuario)Tiendas, COUNT(HDET.id_usuario)-sum(CASE WHEN HDET.Porcentaje>=HDET.Objetivo THEN 1 else 0 end)Falta " & _
                        "FROM (SELECT H.id_periodo,H.id_usuario,H.id_tienda,AN.O_ps Objetivo, " & _
                        "CASE WHEN SUM(HDET.[1])+SUM(HDET.[5])<>0 then " & _
                        "((100*SUM(HDET.[1]))/(SUM(HDET.[1])+SUM(HDET.[5]))) else 0 end as Porcentaje " & _
                        "FROM AS_Historial as H  " & _
                        "INNER JOIN (SELECT DISTINCT orden,id_periodo,nombre_periodo FROM Periodos)PER ON H.id_periodo=PER.id_periodo " & _
                        "INNER JOIN AS_Tiendas as TI ON H.id_tienda= TI.id_tienda  " & _
                        "INNER JOIN AS_Segmentos_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial  " & _
                        "INNER JOIN AS_Area_Nielsen AS AN ON AN.area_nielsen = TI.area_nielsen  " & _
                        "WHERE id_usuario='" & cmbPromotor.SelectedValue & "' AND PER.orden BETWEEN " & cmbPeriodoA.SelectedValue & " AND " & cmbPeriodoB.SelectedValue & " " & _
                        "GROUP BY H.id_periodo,H.id_usuario,H.id_tienda,AN.O_ps)as HDET  " & _
                        "GROUP BY HDET.id_usuario UNION ALL " & _
                        "select 'Cachorro Seco'as Segmento,sum(CASE WHEN HDET.Porcentaje>=HDET.Objetivo THEN 1 else 0 end)Cumplimiento, " & _
                        "COUNT(HDET.id_usuario)Tiendas, COUNT(HDET.id_usuario)-sum(CASE WHEN HDET.Porcentaje>=HDET.Objetivo THEN 1 else 0 end)Falta  " & _
                        "FROM (SELECT H.id_periodo,H.id_usuario,H.id_tienda,AN.O_pc Objetivo, " & _
                        "CASE WHEN SUM(HDET.[2])+SUM(HDET.[6])<>0 then " & _
                        "((100*SUM(HDET.[2]))/(SUM(HDET.[2])+SUM(HDET.[6]))) else 0 end as Porcentaje " & _
                        "FROM AS_Historial as H  " & _
                        "INNER JOIN (SELECT DISTINCT orden,id_periodo,nombre_periodo FROM Periodos)PER ON H.id_periodo=PER.id_periodo " & _
                        "INNER JOIN AS_Tiendas as TI ON H.id_tienda= TI.id_tienda  " & _
                        "INNER JOIN AS_Segmentos_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial  " & _
                        "INNER JOIN AS_Area_Nielsen AS AN ON AN.area_nielsen = TI.area_nielsen  " & _
                        "WHERE id_usuario='" & cmbPromotor.SelectedValue & "' AND PER.orden BETWEEN " & cmbPeriodoA.SelectedValue & " AND " & cmbPeriodoB.SelectedValue & " " & _
                        "GROUP BY H.id_periodo,H.id_usuario,H.id_tienda,AN.O_pc)as HDET  " & _
                        "GROUP BY HDET.id_usuario UNION ALL " & _
                        "select 'Perro húmedo'as Segmento,sum(CASE WHEN HDET.Porcentaje>=HDET.Objetivo THEN 1 else 0 end)Cumplimiento, " & _
                        "COUNT(HDET.id_usuario)Tiendas, COUNT(HDET.id_usuario)-sum(CASE WHEN HDET.Porcentaje>=HDET.Objetivo THEN 1 else 0 end)Falta  " & _
                        "FROM (SELECT H.id_periodo,H.id_usuario,H.id_tienda,AN.O_ph Objetivo, " & _
                        "CASE WHEN SUM(HDET.[3])+SUM(HDET.[7])<>0 then " & _
                        "((100*SUM(HDET.[3]))/(SUM(HDET.[3])+SUM(HDET.[7]))) else 0 end as Porcentaje " & _
                        "FROM AS_Historial as H  " & _
                        "INNER JOIN (SELECT DISTINCT orden,id_periodo,nombre_periodo FROM Periodos)PER ON H.id_periodo=PER.id_periodo " & _
                        "INNER JOIN AS_Tiendas as TI ON H.id_tienda= TI.id_tienda  " & _
                        "INNER JOIN AS_Segmentos_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial  " & _
                        "INNER JOIN AS_Area_Nielsen AS AN ON AN.area_nielsen = TI.area_nielsen  " & _
                        "WHERE id_usuario='" & cmbPromotor.SelectedValue & "' AND PER.orden BETWEEN " & cmbPeriodoA.SelectedValue & " AND " & cmbPeriodoB.SelectedValue & " " & _
                        "GROUP BY H.id_periodo,H.id_usuario,H.id_tienda,AN.O_ph)as HDET  " & _
                        "GROUP BY HDET.id_usuario UNION ALL " & _
                        "select 'Perro botana'as Segmento,sum(CASE WHEN HDET.Porcentaje>=HDET.Objetivo THEN 1 else 0 end)Cumplimiento, " & _
                        "COUNT(HDET.id_usuario)Tiendas, COUNT(HDET.id_usuario)-sum(CASE WHEN HDET.Porcentaje>=HDET.Objetivo THEN 1 else 0 end)Falta  " & _
                        "FROM (SELECT H.id_periodo,H.id_usuario,H.id_tienda,AN.O_pb Objetivo, " & _
                        "CASE WHEN SUM(HDET.[4])+SUM(HDET.[8])<>0 then " & _
                        "((100*SUM(HDET.[4]))/(SUM(HDET.[4])+SUM(HDET.[8]))) else 0 end as Porcentaje " & _
                        "FROM AS_Historial as H  " & _
                        "INNER JOIN (SELECT DISTINCT orden,id_periodo,nombre_periodo FROM Periodos)PER ON H.id_periodo=PER.id_periodo " & _
                        "INNER JOIN AS_Tiendas as TI ON H.id_tienda= TI.id_tienda  " & _
                        "INNER JOIN AS_Segmentos_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial  " & _
                        "INNER JOIN AS_Area_Nielsen AS AN ON AN.area_nielsen = TI.area_nielsen  " & _
                        "WHERE id_usuario='" & cmbPromotor.SelectedValue & "' AND PER.orden BETWEEN " & cmbPeriodoA.SelectedValue & " AND " & cmbPeriodoB.SelectedValue & " " & _
                        "GROUP BY H.id_periodo,H.id_usuario,H.id_tienda,AN.O_pb)as HDET  " & _
                        "GROUP BY HDET.id_usuario UNION ALL " & _
                        "select 'Gato Seco'as Segmento,sum(CASE WHEN HDET.Porcentaje>=HDET.Objetivo THEN 1 else 0 end)Cumplimiento, " & _
                        "COUNT(HDET.id_usuario)Tiendas, COUNT(HDET.id_usuario)-sum(CASE WHEN HDET.Porcentaje>=HDET.Objetivo THEN 1 else 0 end)Falta  " & _
                        "FROM (SELECT H.id_periodo,H.id_usuario,H.id_tienda,AN.O_gs Objetivo, " & _
                        "CASE WHEN SUM(HDET.[9])+SUM(HDET.[12])<>0 then " & _
                        "((100*SUM(HDET.[9]))/(SUM(HDET.[9])+SUM(HDET.[12]))) else 0 end as Porcentaje " & _
                        "FROM AS_Historial as H  " & _
                        "INNER JOIN (SELECT DISTINCT orden,id_periodo,nombre_periodo FROM Periodos)PER ON H.id_periodo=PER.id_periodo " & _
                        "INNER JOIN AS_Tiendas as TI ON H.id_tienda= TI.id_tienda  " & _
                        "INNER JOIN AS_Segmentos_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial  " & _
                        "INNER JOIN AS_Area_Nielsen AS AN ON AN.area_nielsen = TI.area_nielsen  " & _
                        "WHERE id_usuario='" & cmbPromotor.SelectedValue & "' AND PER.orden BETWEEN " & cmbPeriodoA.SelectedValue & " AND " & cmbPeriodoB.SelectedValue & " " & _
                        "GROUP BY H.id_periodo,H.id_usuario,H.id_tienda,AN.O_gs)as HDET  " & _
                        "GROUP BY HDET.id_usuario UNION ALL " & _
                        "select 'Gato húmedo'as Segmento,sum(CASE WHEN HDET.Porcentaje>=HDET.Objetivo THEN 1 else 0 end)Cumplimiento, " & _
                        "COUNT(HDET.id_usuario)Tiendas, COUNT(HDET.id_usuario)-sum(CASE WHEN HDET.Porcentaje>=HDET.Objetivo THEN 1 else 0 end)Falta  " & _
                        "FROM (SELECT H.id_periodo,H.id_usuario,H.id_tienda,AN.O_gh Objetivo, " & _
                        "CASE WHEN SUM(HDET.[10])+SUM(HDET.[13])<>0 then " & _
                        "((100*SUM(HDET.[10]))/(SUM(HDET.[10])+SUM(HDET.[13]))) else 0 end as Porcentaje " & _
                        "FROM AS_Historial as H  " & _
                        "INNER JOIN (SELECT DISTINCT orden,id_periodo,nombre_periodo FROM Periodos)PER ON H.id_periodo=PER.id_periodo " & _
                        "INNER JOIN AS_Tiendas as TI ON H.id_tienda= TI.id_tienda  " & _
                        "INNER JOIN AS_Segmentos_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial  " & _
                        "INNER JOIN AS_Area_Nielsen AS AN ON AN.area_nielsen = TI.area_nielsen  " & _
                        "WHERE id_usuario='" & cmbPromotor.SelectedValue & "' AND PER.orden BETWEEN " & cmbPeriodoA.SelectedValue & " AND " & cmbPeriodoB.SelectedValue & " " & _
                        "GROUP BY H.id_periodo,H.id_usuario,H.id_tienda,AN.O_gh)as HDET  " & _
                        "GROUP BY HDET.id_usuario UNION ALL " & _
                        "select 'Gato botanas'as Segmento,sum(CASE WHEN HDET.Porcentaje>=HDET.Objetivo THEN 1 else 0 end)Cumplimiento, " & _
                        "COUNT(HDET.id_usuario)Tiendas, COUNT(HDET.id_usuario)-sum(CASE WHEN HDET.Porcentaje>=HDET.Objetivo THEN 1 else 0 end)Falta  " & _
                        "FROM (SELECT H.id_periodo,H.id_usuario,H.id_tienda,AN.O_gb Objetivo, " & _
                        "CASE WHEN SUM(HDET.[11])+SUM(HDET.[14])<>0 then " & _
                        "((100*SUM(HDET.[11]))/(SUM(HDET.[11])+SUM(HDET.[14]))) else 0 end as Porcentaje " & _
                        "FROM AS_Historial as H  " & _
                        "INNER JOIN (SELECT DISTINCT orden,id_periodo,nombre_periodo FROM Periodos)PER ON H.id_periodo=PER.id_periodo " & _
                        "INNER JOIN AS_Tiendas as TI ON H.id_tienda= TI.id_tienda  " & _
                        "INNER JOIN AS_Segmentos_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial  " & _
                        "INNER JOIN AS_Area_Nielsen AS AN ON AN.area_nielsen = TI.area_nielsen  " & _
                        "WHERE id_usuario='" & cmbPromotor.SelectedValue & "' AND PER.orden BETWEEN " & cmbPeriodoA.SelectedValue & " AND " & cmbPeriodoB.SelectedValue & " " & _
                        "GROUP BY H.id_periodo,H.id_usuario,H.id_tienda,AN.O_gb)as HDET  " & _
                        "GROUP BY HDET.id_usuario", gridObjetivo2)

        CargarObjetivos("SELECT PRO.nombre_proceso,RE.id_usuario, ISNULL(RE.SiNo,0)SiNo, " & _
                        "CASE when ISNULL(RE.SiNo,0)=1 then 'Si' else 'No' end as Cumplimiento " & _
                        "FROM AS_Procesos as PRO " & _
                        "INNER JOIN (select RE.id_proceso,RE.id_usuario,Re.estatus as SiNo " & _
                        "FROM AS_Procesos_Rutas_Eventos as RE " & _
                        "INNER JOIN Usuarios as US ON RE.id_usuario=US.id_usuario " & _
                        "WHERE RE.fecha>=US.fecha_ingreso AND RE.id_usuario='" & cmbPromotor.SelectedValue & "')RE ON RE.id_proceso = PRO.id_proceso " & _
                        "WHERE PRO.tipo_proceso = 2", gridObjetivo3)

        CargarObjetivos("select 'Nivel 1'Certifiacion, Modulos.id_usuario, " & _
                        "(CASE when SUM(Modulos.Modulo)>=80 then 1 else 0 end)Sino, " & _
                        "(CASE when SUM(Modulos.Modulo)>=80 then 'Si' else 'No' end)Cumplimiento " & _
                        "FROM(SELECT US.id_usuario,CAL.modulo_1 as Modulo FROM Usuarios as US  " & _
                        "INNER JOIN AS_Calificaciones as CAL ON CAL.id_usuario=US.id_usuario  " & _
                        "WHERE US.id_usuario='" & cmbPromotor.SelectedValue & "' UNION ALL  " & _
                        "SELECT US.id_usuario,CAL.modulo_2 as Modulo FROM Usuarios as US  " & _
                        "INNER JOIN AS_Calificaciones as CAL ON CAL.id_usuario=US.id_usuario  " & _
                        "WHERE US.id_usuario='" & cmbPromotor.SelectedValue & "' UNION ALL  " & _
                        "SELECT US.id_usuario,CAL.modulo_3 as Modulo FROM Usuarios as US  " & _
                        "INNER JOIN AS_Calificaciones as CAL ON CAL.id_usuario=US.id_usuario  " & _
                        "WHERE US.id_usuario='" & cmbPromotor.SelectedValue & "' UNION ALL  " & _
                        "SELECT US.id_usuario,CAL.modulo_4 as Modulo FROM Usuarios as US  " & _
                        "INNER JOIN AS_Calificaciones as CAL ON CAL.id_usuario=US.id_usuario  " & _
                        "WHERE US.id_usuario='" & cmbPromotor.SelectedValue & "')Modulos GROUP BY Modulos.id_usuario", gridObjetivo4A)

        CargarObjetivos("SELECT PRO.nombre_proceso,RE.id_usuario,ISNULL(RE.SiNo,0)SiNo, " & _
                        "CASE when ISNULL(RE.SiNo,0)=1 then 'Si' else 'No' end Cumplimiento " & _
                        "FROM AS_Procesos as PRO " & _
                        "INNER JOIN (select RE.id_proceso,RE.id_usuario,Re.estatus as SiNo " & _
                        "FROM AS_Procesos_Rutas_Eventos as RE " & _
                        "INNER JOIN Usuarios as US ON RE.id_usuario=US.id_usuario " & _
                        "WHERE RE.fecha>=US.fecha_ingreso AND RE.id_usuario='" & cmbPromotor.SelectedValue & "')RE ON RE.id_proceso = PRO.id_proceso " & _
                        "WHERE PRO.tipo_proceso = 1", gridObjetivo4B)

        CargarObjetivos("SELECT PRO.nombre_entrenamiento,isnull(RE.id_usuario,'')id_usuario,ISNULL(RE.SiNo,0)SiNo, " & _
                        "CASE WHEN ISNULL(RE.SiNo,0)=1 then 'Si' else 'No' end Cumplimiento " & _
                        "FROM AS_Entrenamientos_tecnicos as PRO " & _
                        "INNER JOIN (select RE.id_entrenamiento,RE.id_usuario,1 as SiNo " & _
                        "FROM AS_Entrenamientos_Rutas_Eventos as RE " & _
                        "INNER JOIN Usuarios as US ON RE.id_usuario=US.id_usuario " & _
                        "WHERE RE.fecha>=US.fecha_ingreso AND RE.id_usuario='" & cmbPromotor.SelectedValue & "')RE " & _
                        "ON RE.id_entrenamiento = PRO.id_entrenamiento", gridObjetivo4C)

        Dim A1, A2, A3 As Double
        A1 = lblAlcance4A.Text
        A2 = lblAlcance4B.Text
        A3 = lblAlcance4C.Text

        lblAlcance4.Text = FormatNumber(((A1 + A2 + A3) / 3), 2) & "%"
        lblPonderacion4.Text = FormatNumber(((15 * ((A1 + A2 + A3) / 3)) / 100), 2) & "%"
        lblResultado4.Text = FormatNumber(((15 * ((A1 + A2 + A3) / 3)) / 100), 2) & "%"

        CargarObjetivos("select 'Cumplimiento de captura'Dato,COUNT(Capturas)Periodos, SUM(Capturas)Cumplimiento,Capturas.id_usuario " & _
                        "FROM(SELECT RE.id_periodo,RE.id_usuario, " & _
                        "(CASE when COUNT(RE.id_tienda)=SUM(RE.estatus_anaquel) then 1 " & _
                        "else 0 end) as Capturas " & _
                        "FROM AS_Rutas_Eventos as RE " & _
                        "INNER JOIN Periodos as PER ON PER.id_periodo= RE.id_periodo " & _
                        "INNER JOIN Usuarios as US ON RE.id_usuario = US.id_usuario " & _
                        "WHERE RE.id_usuario='" & cmbPromotor.SelectedValue & "' AND " & _
                        "PER.fecha_fin_periodo>=US.fecha_ingreso " & _
                        "AND PER.orden BETWEEN " & cmbPeriodoA.SelectedValue & " AND " & cmbPeriodoB.SelectedValue & " " & _
                        "GROUP BY  RE.id_periodo,RE.id_usuario)Capturas GROUP BY Capturas.id_usuario", gridObjetivo5)

        Dim R1, R2, R3, R4, R5, Total As Double
        R1 = Ponderacion1
        R2 = Ponderacion2
        R3 = Ponderacion3
        R4 = FormatNumber(((15 * ((A1 + A2 + A3) / 3)) / 100), 2)
        R5 = Ponderacion5
        Total = (R1 + R2 + R3 + R4 + R5)

        lblResultadoTotal.Text = Total & "%"
    End Sub

    Public Function CargarObjetivos(ByVal SQL As String, ByVal grilla As GridView) As Integer
        CargaGrilla(ConexionMars.localSqlServer, SQL, grilla)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDropSin(ConexionMars.localSqlServer, PeriodoSQLA, "nombre_periodo", "orden", cmbPeriodoA)
            Combo.LlenaDropSin(ConexionMars.localSqlServer, PeriodoSQLB, "nombre_periodo", "orden", cmbPeriodoB)

            If tipo_usuario <> 1 Then

                SQLCombo()
                Combo.LlenaDrop(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
                Combo.LlenaDrop(ConexionMars.localSqlServer, EjecutivoSQL, "supervisor", "region_mars", cmbEjecutivo)
                Combo.LlenaDrop(ConexionMars.localSqlServer, SupervisorSQL, "supervisor", "id_supervisor", cmbSupervisor)
                Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
            Else
                PromotorSel = "AND REL.id_usuario='" & HttpContext.Current.User.Identity.Name & "'"
                SQLCombo()
                Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
            End If

            BuscaPeriodoActual()

            If Not Request.Params("id_usuario") = "" Then
                cmbPromotor.SelectedValue = Request.Params("id_usuario")
                SQLCombo()

                VerDatos()
                CargarReporte()

                pnlPDP.Visible = True
            End If
        End If
    End Sub

    Sub BuscaPeriodoActual()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT DISTINCT id_periodo, nombre_periodo, fecha_inicio_periodo, fecha_fin_periodo " & _
                                     "FROM Periodos where fecha_inicio_periodo<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                                     "AND fecha_fin_periodo>= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "'")
        If Tabla.Rows.Count > 0 Then
            PeriodoActual = Tabla.Rows(0)("id_periodo")
            cmbPeriodoA.SelectedValue = Tabla.Rows(0)("id_periodo")
            cmbPeriodoB.SelectedValue = Tabla.Rows(0)("id_periodo")
        End If

        Tabla.Dispose()
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()

        VerDatos()
        CargarReporte()

        pnlPDP.Visible = True
    End Sub

    Private Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, EjecutivoSQL, "supervisor", "region_mars", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, SupervisorSQL, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        pnlPDP.Visible = False
    End Sub

    Private Sub cmbEjecutivo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEjecutivo.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, SupervisorSQL, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        pnlPDP.Visible = False
    End Sub

    Private Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        pnlPDP.Visible = False
    End Sub

    Private Sub gridObjetivo1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridObjetivo1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If Not e.Row.Cells(1).Text = "&nbsp;" Then
                DatoObj1A = e.Row.Cells(1).Text
                SumaObj1A = SumaObj1A + DatoObj1A : End If

            If Not e.Row.Cells(2).Text = "&nbsp;" Then
                DatoObj1B = e.Row.Cells(2).Text
                SumaObj1B = SumaObj1B + DatoObj1B : End If

            e.Row.Cells(3).Text = DatoObj1A - DatoObj1B

            DatoObj1C = e.Row.Cells(3).Text
            SumaObj1C = SumaObj1C + DatoObj1C
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.Cells(1).Text = SumaObj1A
            e.Row.Cells(2).Text = SumaObj1B
            e.Row.Cells(3).Text = SumaObj1C
            e.Row.Cells(4).Text = SumaObj1B & " de " & SumaObj1A

            Alcance1 = FormatNumber(((SumaObj1B * 100) / SumaObj1A), 2)
            Ponderacion1 = FormatNumber(((35 * Alcance1) / 100), 2)

            lblAlcance1.Text = Alcance1 & "%"
            lblPonderacion1.Text = Ponderacion1 & "%"
            lblResultado1.Text = Ponderacion1 & "%"
        End If
    End Sub

    Private Sub gridObjetivo2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridObjetivo2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If Not e.Row.Cells(1).Text = "&nbsp;" Then
                DatoObj2A = e.Row.Cells(1).Text
                SumaObj2A = SumaObj2A + DatoObj2A : End If

            If Not e.Row.Cells(2).Text = "&nbsp;" Then
                DatoObj2B = e.Row.Cells(2).Text
                SumaObj2B = SumaObj2B + DatoObj2B : End If

            e.Row.Cells(3).Text = DatoObj2A - DatoObj2B

            DatoObj2C = e.Row.Cells(3).Text
            SumaObj2C = SumaObj2C + DatoObj2C
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.Cells(1).Text = SumaObj2A
            e.Row.Cells(2).Text = SumaObj2B
            e.Row.Cells(3).Text = SumaObj2C
            e.Row.Cells(4).Text = SumaObj2B & " de " & SumaObj2A

            Alcance2 = FormatNumber(((SumaObj2B * 100) / SumaObj2A), 2)
            Ponderacion2 = FormatNumber(((35 * Alcance2) / 100), 2)

            lblAlcance2.Text = Alcance2 & "%"
            lblPonderacion2.Text = Ponderacion2 & "%"
            lblResultado2.Text = Ponderacion2 & "%"
        End If
    End Sub

    Private Sub gridObjetivo3_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridObjetivo3.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If Not e.Row.Cells(1).Text = "&nbsp;" Then
                DatoObj3 = e.Row.Cells(1).Text
                SumaObj3 = SumaObj3 + DatoObj3 : End If
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.Cells(2).Text = SumaObj3 & " de " & gridObjetivo3.Rows.Count

            Alcance3 = FormatNumber(((SumaObj3 * 100) / (gridObjetivo3.Rows.Count)), 2)
            Ponderacion3 = FormatNumber(((10 * Alcance3) / 100), 2)

            lblAlcance3.Text = Alcance3 & "%"
            lblPonderacion3.Text = Ponderacion3 & "%"
            lblResultado3.Text = Ponderacion3 & "%"
        End If

        If gridObjetivo3.Rows.Count = 0 Then
            lblAlcance3.Text = "100%"
            lblPonderacion3.Text = "10%"
            lblResultado3.Text = "10%"
        End If
    End Sub

    Private Sub gridObjetivo4A_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridObjetivo4A.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If Not e.Row.Cells(1).Text = "&nbsp;" Then
                DatoObj4A = e.Row.Cells(1).Text
                SumaObj4A = SumaObj4A + DatoObj4A : End If
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.Cells(2).Text = SumaObj4A & " de " & gridObjetivo4A.Rows.Count

            Alcance4A = FormatNumber(((SumaObj4A * 100) / (gridObjetivo4A.Rows.Count)), 2)
            lblAlcance4A.Text = Alcance4A
        End If

        If gridObjetivo4A.Rows.Count = 0 Then
            lblAlcance4A.Text = "100"
        End If
    End Sub

    Private Sub gridObjetivo4B_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridObjetivo4B.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If Not e.Row.Cells(1).Text = "&nbsp;" Then
                DatoObj4B = e.Row.Cells(1).Text
                SumaObj4B = SumaObj4B + DatoObj4B : End If
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.Cells(2).Text = SumaObj4B & " de " & gridObjetivo4B.Rows.Count

            Alcance4B = FormatNumber(((SumaObj4B * 100) / (gridObjetivo4B.Rows.Count)), 2)
            lblAlcance4B.Text = Alcance4B
        End If

        If gridObjetivo4B.Rows.Count = 0 Then
            lblAlcance4B.Text = "100"
        End If
    End Sub

    Private Sub gridObjetivo4C_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridObjetivo4C.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If Not e.Row.Cells(1).Text = "&nbsp;" Then
                DatoObj4C = e.Row.Cells(1).Text
                SumaObj4C = SumaObj4C + DatoObj4C : End If
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.ForeColor = Drawing.Color.Black
            e.Row.Cells(2).Text = SumaObj4C & " de " & gridObjetivo4C.Rows.Count

            Alcance4C = FormatNumber(((SumaObj4C * 100) / (gridObjetivo4C.Rows.Count)), 2)
            lblAlcance4C.Text = Alcance4C
        End If

        If gridObjetivo4C.Rows.Count = 0 Then
            lblAlcance4C.Text = "100"
        End If
    End Sub

    Private Sub gridObjetivo5_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridObjetivo5.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If Not e.Row.Cells(1).Text = "&nbsp;" Then
                DatoObj5A = e.Row.Cells(1).Text
                SumaObj5A = SumaObj5A + DatoObj5A : End If

            If Not e.Row.Cells(3).Text = "&nbsp;" Then
                DatoObj5B = e.Row.Cells(3).Text
                SumaObj5B = SumaObj5B + DatoObj5B : End If
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.ForeColor = Drawing.Color.Black
            Alcance5 = FormatNumber(((SumaObj5A * 100) / SumaObj5B), 2)
            Ponderacion5 = FormatNumber(((5 * Alcance5) / 100), 2)

            lblAlcance5.Text = Alcance5 & "%"
            lblPonderacion5.Text = Ponderacion5 & "%"
            lblResultado5.Text = Ponderacion5 & "%"
        End If
    End Sub

    Protected Sub lnkObjetivo1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkObjetivo1.Click
        Response.Redirect("~/sitios/Mars/Autoservicio/PDP/DetalleObjetivo1.aspx?id_usuario=" & cmbPromotor.SelectedValue & "")
    End Sub

    Private Sub lnkObjetivo2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkObjetivo2.Click
        Response.Redirect("~/sitios/Mars/Autoservicio/PDP/DetalleObjetivo2.aspx?id_usuario=" & cmbPromotor.SelectedValue & "")
    End Sub

    Private Sub lnkObjetivo3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkObjetivo3.Click
        Response.Redirect("~/sitios/Mars/Autoservicio/PDP/DetalleObjetivo3.aspx?id_usuario=" & cmbPromotor.SelectedValue & "")
    End Sub

    Private Sub lnkObjetivo4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkObjetivo4.Click
        Response.Redirect("~/sitios/Mars/Autoservicio/PDP/DetalleObjetivo4.aspx?id_usuario=" & cmbPromotor.SelectedValue & "")
    End Sub

    Private Sub lnkObjetivo5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkObjetivo5.Click
        Response.Redirect("~/sitios/Mars/Autoservicio/PDP/DetalleObjetivo5.aspx?id_usuario=" & cmbPromotor.SelectedValue & "")
    End Sub

    Private Sub cmbPeriodoB_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPeriodoB.SelectedIndexChanged
        SQLCombo()

        VerDatos()
        CargarReporte()

        pnlPDP.Visible = True
    End Sub

    Private Sub cmbPeriodoA_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPeriodoA.SelectedIndexChanged
        SQLCombo()

        VerDatos()
        CargarReporte()

        pnlPDP.Visible = True
    End Sub
End Class