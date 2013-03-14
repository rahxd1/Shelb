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

Partial Public Class ReporteBitacoraMarsConv
    Inherits System.Web.UI.Page

    Dim PeriodoSQL, QuincenaSQL, PromotorSQL, RegionSQL As String
    Dim SQLDet, SQLReg As String

    Sub SQLCombo()
        Dim PeriodoSel, RegionSel As String
        PeriodoSel = Acciones.Slc.cmb("RE.orden", cmbPeriodo.SelectedValue)
        RegionSel = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)

        PeriodoSQL = "SELECT DISTINCT PER.orden, PER.nombre_periodo, PER.fecha_fin_periodo " & _
                     "FROM Conv_Rutas_Eventos_Precios as H " & _
                     "INNER JOIN Periodos_Nuevo PER ON H.orden = PER.orden " & _
                     "ORDER BY PER.orden DESC"

        QuincenaSQL = "SELECT DISTINCT PER.id_quincena, PER.nombre_quincena " & _
                    "FROM Conv_Rutas_Eventos_Precios as H " & _
                    "INNER JOIN Semanas PER ON H.id_quincena = PER.id_quincena " & _
                    "ORDER BY PER.id_quincena"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Conv_Rutas_Eventos_Precios AS RE " & _
                    "INNER JOIN Usuarios AS US ON US.id_usuario = RE.id_usuario " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = US.id_region " & _
                    "WHERE REG.id_region <>0 " + PeriodoSel + " " & _
                    " ORDER BY REG.nombre_region"

        PromotorSQL = "SELECT DISTINCT RE.id_usuario " & _
                    "FROM Conv_Rutas_Eventos_Precios AS RE " & _
                    "INNER JOIN Usuarios AS US ON US.id_usuario = RE.id_usuario " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = US.id_region " & _
                    "WHERE RE.id_usuario<>'' " & _
                    " " + PeriodoSel + RegionSel + " " & _
                    " ORDER BY RE.id_usuario"
    End Sub

    Sub CargarReporte()
        If cmbPeriodo.SelectedValue <> "" Then
            pnlDetalle.Visible = False

            RegionSQL = Acciones.Slc.cmb("US.id_region", cmbRegion.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("RE.id_usuario", cmbPromotor.SelectedValue)

            If cmbQuincena.SelectedValue = "" Then
                SQLReg = "SELECT DISTINCT REG.nombre_region 'Región',  " & _
                    "(ISNULL(CapturasP1.Captura,0)+ISNULL(CapturasP2.Captura,0)+ISNULL(ReExh.Capturas,0))/3 as'% Captura' " & _
                    "FROM Regiones as REG " & _
                    "INNER JOIN Usuarios as US ON REG.id_region = US.id_region " & _
                    "FULL JOIN (SELECT US.id_region,100*Capturas.Capturas/COUNT(RE.estatus_precio)Captura " & _
                    "FROM Conv_Rutas_Eventos_Precios as RE  " & _
                    "INNER JOIN Usuarios as US ON RE.id_usuario = US.id_usuario " & _
                    "FULL JOIN (SELECT US.id_region, COUNT(RE.estatus_precio)Capturas   " & _
                    "FROM Conv_Rutas_Eventos_Precios as RE  " & _
                    "INNER JOIN Usuarios as US ON RE.id_usuario = US.id_usuario " & _
                    "WHERE estatus_precio=1 AND id_quincena='Q1' AND orden=" & cmbPeriodo.SelectedValue & "  " & _
                    " " + RegionSQL + " " & _
                    "GROUP BY US.id_region)as Capturas ON Capturas.id_region = US.id_region " & _
                    "WHERE id_quincena='Q1' AND orden=" & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + " " & _
                    "GROUP BY US.id_region, Capturas.Capturas)CapturasP1 ON CapturasP1.id_region = REG.id_region   " & _
                    "FULL JOIN (SELECT US.id_region,100*Capturas.Capturas/COUNT(RE.estatus_precio)Captura " & _
                    "FROM Conv_Rutas_Eventos_Precios as RE  " & _
                    "INNER JOIN Usuarios as US ON RE.id_usuario = US.id_usuario " & _
                    "FULL JOIN (SELECT US.id_region, COUNT(RE.estatus_precio)Capturas   " & _
                    "FROM Conv_Rutas_Eventos_Precios as RE  " & _
                    "INNER JOIN Usuarios as US ON RE.id_usuario = US.id_usuario " & _
                    "WHERE estatus_precio=1 AND id_quincena='Q2' AND orden=" & cmbPeriodo.SelectedValue & "  " & _
                    " " + RegionSQL + " " & _
                    "GROUP BY US.id_region)as Capturas ON Capturas.id_region = US.id_region " & _
                    "WHERE id_quincena='Q2' AND orden=" & cmbPeriodo.SelectedValue & "  " & _
                    " " + RegionSQL + " " & _
                    "GROUP BY US.id_region, Capturas.Capturas)CapturasP2 ON CapturasP2.id_region = REG.id_region   " & _
                    "FULL JOIN (SELECT REG.id_region, COUNT(RE.id_usuario) AS Reportes, " & _
                    "100*ISNULL(Capturas.Capturas,0)/COUNT(RE.id_usuario)Capturas " & _
                    "FROM Conv_Rutas_Eventos_Exhibiciones as RE   " & _
                    "INNER JOIN Usuarios as US ON RE.id_usuario = US.id_usuario " & _
                    "INNER JOIN Regiones as REG ON US.id_region= REG.id_region " & _
                    "INNER JOIN (SELECT REG.id_region, COUNT(RE.estatus_exhibiciones) AS Capturas   " & _
                    "FROM Conv_Rutas_Eventos_Exhibiciones as RE  " & _
                    "INNER JOIN Usuarios as US ON RE.id_usuario = US.id_usuario " & _
                    "INNER JOIN Regiones as REG ON US.id_region= REG.id_region " & _
                    "WHERE estatus_exhibiciones=1 AND orden=" & cmbPeriodo.SelectedValue & "  " & _
                    " " + RegionSQL + " " & _
                    "GROUP BY REG.id_region)Capturas ON Capturas.id_region = REG.id_region   " & _
                    "WHERE RE.orden=" & cmbPeriodo.SelectedValue & "  " & _
                    " " + RegionSQL + " " & _
                    "GROUP BY REG.id_region,Capturas.Capturas)ReExh " & _
                    "ON ReExh.id_region = REG.id_region " & _
                    "WHERE REG.id_region<>0 ORDER BY REG.nombre_region"
            End If

            If cmbQuincena.SelectedValue = "Q1" Then
                SQLReg = "SELECT DISTINCT REG.nombre_region 'Región',  " & _
                    "ISNULL(CapturasP1.Captura,0)'% Captura' " & _
                    "FROM Regiones as REG " & _
                    "INNER JOIN Usuarios as US ON REG.id_region = US.id_region " & _
                    "FULL JOIN (SELECT US.id_region,100*Capturas.Capturas/COUNT(RE.estatus_precio)Captura " & _
                    "FROM Conv_Rutas_Eventos_Precios as RE " & _
                    "INNER JOIN Usuarios as US ON RE.id_usuario = US.id_usuario " & _
                    "FULL JOIN (SELECT US.id_region, COUNT(RE.estatus_precio)Capturas   " & _
                    "FROM Conv_Rutas_Eventos_Precios as RE  " & _
                    "INNER JOIN Usuarios as US ON RE.id_usuario = US.id_usuario " & _
                    "WHERE estatus_precio=1 AND id_quincena='Q1' AND orden=" & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + " " & _
                    "GROUP BY US.id_region)as Capturas ON Capturas.id_region = US.id_region " & _
                    "WHERE id_quincena='Q1' AND orden=" & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + " " & _
                    "GROUP BY US.id_region, Capturas.Capturas)CapturasP1 ON CapturasP1.id_region = REG.id_region   " & _
                    "WHERE REG.id_region<>0 ORDER BY REG.nombre_region"
            End If

            If cmbQuincena.SelectedValue = "Q2" Then
                SQLReg = "SELECT DISTINCT REG.nombre_region 'Región',  " & _
                    "(ISNULL(CapturasP2.Captura,0)+ISNULL(ReExh.Capturas,0))/2 as'% Captura' " & _
                    "FROM Regiones as REG " & _
                    "INNER JOIN Usuarios as US ON REG.id_region = US.id_region " & _
                    "FULL JOIN (SELECT US.id_region,100*Capturas.Capturas/COUNT(RE.estatus_precio)Captura " & _
                    "FROM Conv_Rutas_Eventos_Precios as RE  " & _
                    "INNER JOIN Usuarios as US ON RE.id_usuario = US.id_usuario " & _
                    "FULL JOIN (SELECT US.id_region, COUNT(RE.estatus_precio)Capturas   " & _
                    "FROM Conv_Rutas_Eventos_Precios as RE  " & _
                    "INNER JOIN Usuarios as US ON RE.id_usuario = US.id_usuario " & _
                    "WHERE estatus_precio=1 AND id_quincena='Q1' AND orden=" & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + " " & _
                    "GROUP BY US.id_region)as Capturas ON Capturas.id_region = US.id_region " & _
                    "WHERE id_quincena='Q1' AND orden=" & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + " " & _
                    "GROUP BY US.id_region, Capturas.Capturas)CapturasP1 ON CapturasP1.id_region = REG.id_region   " & _
                    "FULL JOIN (SELECT US.id_region,100*Capturas.Capturas/COUNT(RE.estatus_precio)Captura " & _
                    "FROM Conv_Rutas_Eventos_Precios as RE  " & _
                    "INNER JOIN Usuarios as US ON RE.id_usuario = US.id_usuario " & _
                    "FULL JOIN (SELECT US.id_region, COUNT(RE.estatus_precio)Capturas   " & _
                    "FROM Conv_Rutas_Eventos_Precios as RE  " & _
                    "INNER JOIN Usuarios as US ON RE.id_usuario = US.id_usuario " & _
                    "WHERE estatus_precio=1 AND id_quincena='Q2' AND orden=" & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + " " & _
                    "GROUP BY US.id_region)as Capturas ON Capturas.id_region = US.id_region " & _
                    "WHERE id_quincena='Q2' AND orden=" & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + " " & _
                    "GROUP BY US.id_region, Capturas.Capturas)CapturasP2 ON CapturasP2.id_region = REG.id_region   " & _
                    "FULL JOIN (SELECT REG.id_region, COUNT(RE.id_usuario) AS Reportes, " & _
                    "100*ISNULL(Capturas.Capturas,0)/COUNT(RE.id_usuario)Capturas " & _
                    "FROM Conv_Rutas_Eventos_Exhibiciones as RE   " & _
                    "INNER JOIN Usuarios as US ON RE.id_usuario = US.id_usuario " & _
                    "INNER JOIN Regiones as REG ON US.id_region= REG.id_region " & _
                    "INNER JOIN (SELECT REG.id_region, COUNT(RE.estatus_exhibiciones) AS Capturas   " & _
                    "FROM Conv_Rutas_Eventos_Exhibiciones as RE  " & _
                    "INNER JOIN Usuarios as US ON RE.id_usuario = US.id_usuario " & _
                    "INNER JOIN Regiones as REG ON US.id_region= REG.id_region " & _
                    "WHERE estatus_exhibiciones=1 AND orden=" & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + " " & _
                    "GROUP BY REG.id_region)Capturas ON Capturas.id_region = REG.id_region   " & _
                    "WHERE RE.orden=" & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + " " & _
                    "GROUP BY REG.id_region,Capturas.Capturas)ReExh " & _
                    "ON ReExh.id_region = REG.id_region " & _
                    "WHERE REG.id_region<>0 ORDER BY REG.nombre_region"
            End If

            CargaGrilla(ConexionMars.localSqlServer, SQLReg, gridTotal)

            If cmbQuincena.SelectedValue = "" Then
                SQLDet = "SELECT RE.id_usuario as'Promotor', COUNT(RE.id_usuario)'Reportes Precios', " & _
                    "ISNULL(CapturasP1.Capturas,0) as 'Precios Q1',  " & _
                    "ISNULL(CapturasP2.Capturas,0) as 'Precios Q2', " & _
                    "ISNULL(ReExh.Reportes,0) 'Reportes Exhibiciones',ISNULL(ReExh.Exhibiciones,0) 'Exhibiciones Q2', " & _
                    "((100*(ISNULL(CapturasP1.Capturas,0)+ISNULL(CapturasP2.Capturas,0))/COUNT(RE.id_usuario)+ISNULL(ReExh.Captura,0))/2)'% Captura' " & _
                    "FROM Conv_Rutas_Eventos_Precios as RE " & _
                    "INNER JOIN Usuarios as US ON RE.id_usuario = US.id_usuario " & _
                    "FULL JOIN (SELECT RE.id_usuario, COUNT(RE.estatus_precio) AS Capturas   " & _
                    "FROM Conv_Rutas_Eventos_Precios as RE " & _
                    "INNER JOIN Usuarios as US ON RE.id_usuario = US.id_usuario " & _
                    "WHERE estatus_precio=1 " & _
                    "AND id_quincena='Q1' AND orden=" & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + PromotorSQL + " " & _
                    "GROUP BY RE.id_usuario)CapturasP1  " & _
                    "ON CapturasP1.id_usuario = RE.id_usuario   " & _
                    "FULL JOIN (SELECT RE.id_usuario, COUNT(RE.estatus_precio) AS Capturas   " & _
                    "FROM Conv_Rutas_Eventos_Precios as RE " & _
                    "INNER JOIN Usuarios as US ON RE.id_usuario = US.id_usuario " & _
                    "WHERE estatus_precio=1 " & _
                    "AND id_quincena='Q2' AND orden=" & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + PromotorSQL + " " & _
                    "GROUP BY RE.id_usuario)CapturasP2 ON CapturasP2.id_usuario = RE.id_usuario   " & _
                    "FULL JOIN (SELECT RE.id_usuario, COUNT(RE.id_usuario) AS Reportes, " & _
                    "ISNULL(Capturas.Capturas,0) as Exhibiciones,  " & _
                    "100*ISNULL(Capturas.Capturas,0)/COUNT(RE.id_usuario)Captura " & _
                    "FROM Conv_Rutas_Eventos_Exhibiciones as RE " & _
                    "INNER JOIN Usuarios as US ON RE.id_usuario = US.id_usuario " & _
                    "FULL JOIN (SELECT RE.id_usuario, COUNT(RE.estatus_exhibiciones) AS Capturas   " & _
                    "FROM Conv_Rutas_Eventos_Exhibiciones as RE " & _
                    "INNER JOIN Usuarios as US ON RE.id_usuario = US.id_usuario " & _
                    "WHERE estatus_exhibiciones=1 AND orden=" & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + PromotorSQL + " " & _
                    "GROUP BY RE.id_usuario)Capturas " & _
                    "ON Capturas.id_usuario = RE.id_usuario   " & _
                    "WHERE RE.orden=" & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + PromotorSQL + " " & _
                    "GROUP BY RE.id_usuario,Capturas.Capturas,RE.orden)ReExh " & _
                    "ON ReExh.id_usuario = RE.id_usuario " & _
                    "WHERE RE.orden=" & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + PromotorSQL + " " & _
                    "GROUP BY RE.id_usuario,CapturasP1.Capturas,CapturasP2.Capturas,RE.orden,ReExh.Reportes,ReExh.Exhibiciones,ReExh.Captura " & _
                    "ORDER BY RE.id_usuario"
            End If

            If cmbQuincena.SelectedValue = "Q1" Then
                SQLDet = "SELECT RE.id_usuario as'Promotor', COUNT(RE.id_usuario)'Reportes Precios', " & _
                    "ISNULL(CapturasP1.Capturas,0) as 'Precios Q1',  " & _
                    "((100*(ISNULL(CapturasP1.Capturas,0))/COUNT(RE.id_usuario))) '% Captura' " & _
                    "FROM (select * from Conv_Rutas_Eventos_Precios WHERE id_quincena='Q1')as RE " & _
                    "INNER JOIN Usuarios as US ON RE.id_usuario = US.id_usuario " & _
                    "FULL JOIN (SELECT RE.id_usuario, COUNT(RE.estatus_precio) AS Capturas   " & _
                    "FROM Conv_Rutas_Eventos_Precios as RE " & _
                    "INNER JOIN Usuarios as US ON RE.id_usuario = US.id_usuario " & _
                    "WHERE estatus_precio=1 " & _
                    "AND id_quincena='Q1' AND orden=" & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + PromotorSQL + " " & _
                    "GROUP BY RE.id_usuario)CapturasP1  " & _
                    "ON CapturasP1.id_usuario = RE.id_usuario   " & _
                    "WHERE RE.orden=" & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + PromotorSQL + " " & _
                    "GROUP BY RE.id_usuario,CapturasP1.Capturas,RE.orden " & _
                    "ORDER BY RE.id_usuario"
            End If

            If cmbQuincena.SelectedValue = "Q2" Then
                SQLDet = "SELECT RE.id_usuario as'Promotor', COUNT(RE.id_usuario)'Reportes Precios', " & _
                    "ISNULL(CapturasP2.Capturas,0) as 'Precios Q2', " & _
                    "ISNULL(ReExh.Reportes,0) 'Reportes Exhibiciones',ISNULL(ReExh.Exhibiciones,0) 'Exhibiciones Q2', " & _
                    "((100*ISNULL(CapturasP2.Capturas,0)/COUNT(RE.id_usuario)+ISNULL(ReExh.Captura,0))/2)'% Captura' " & _
                    "FROM (select * from Conv_Rutas_Eventos_Precios WHERE id_quincena='Q2') as RE " & _
                    "INNER JOIN Usuarios as US ON RE.id_usuario = US.id_usuario " & _
                    "FULL JOIN (SELECT RE.id_usuario, COUNT(RE.estatus_precio) AS Capturas   " & _
                    "FROM Conv_Rutas_Eventos_Precios as RE " & _
                    "INNER JOIN Usuarios as US ON RE.id_usuario = US.id_usuario " & _
                    "WHERE estatus_precio=1 " & _
                    "AND id_quincena='Q1' AND orden=" & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + PromotorSQL + " " & _
                    "GROUP BY RE.id_usuario)CapturasP1  " & _
                    "ON CapturasP1.id_usuario = RE.id_usuario   " & _
                    "FULL JOIN (SELECT RE.id_usuario, COUNT(RE.estatus_precio) AS Capturas   " & _
                    "FROM Conv_Rutas_Eventos_Precios as RE " & _
                    "INNER JOIN Usuarios as US ON RE.id_usuario = US.id_usuario " & _
                    "WHERE estatus_precio=1 " & _
                    "AND id_quincena='Q2' AND orden=" & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + PromotorSQL + " " & _
                    "GROUP BY RE.id_usuario)CapturasP2 ON CapturasP2.id_usuario = RE.id_usuario   " & _
                    "FULL JOIN (SELECT RE.id_usuario, COUNT(RE.id_usuario) AS Reportes, " & _
                    "ISNULL(Capturas.Capturas,0) as Exhibiciones,  " & _
                    "100*ISNULL(Capturas.Capturas,0)/COUNT(RE.id_usuario)Captura " & _
                    "FROM Conv_Rutas_Eventos_Exhibiciones as RE " & _
                    "INNER JOIN Usuarios as US ON RE.id_usuario = US.id_usuario " & _
                    "FULL JOIN (SELECT RE.id_usuario, COUNT(RE.estatus_exhibiciones) AS Capturas   " & _
                    "FROM Conv_Rutas_Eventos_Exhibiciones as RE " & _
                    "INNER JOIN Usuarios as US ON RE.id_usuario = US.id_usuario " & _
                    "WHERE estatus_exhibiciones=1 AND orden=" & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + PromotorSQL + " " & _
                    "GROUP BY RE.id_usuario)Capturas " & _
                    "ON Capturas.id_usuario = RE.id_usuario " & _
                    "WHERE RE.orden=" & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + PromotorSQL + " " & _
                    "GROUP BY RE.id_usuario,Capturas.Capturas,RE.orden)ReExh " & _
                    "ON ReExh.id_usuario = RE.id_usuario " & _
                    "WHERE RE.orden=" & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + PromotorSQL + " " & _
                    "GROUP BY RE.id_usuario,CapturasP1.Capturas,CapturasP2.Capturas,RE.orden,ReExh.Reportes,ReExh.Exhibiciones,ReExh.Captura " & _
                    "ORDER BY RE.id_usuario"
            End If

            CargaGrilla(ConexionMars.localSqlServer, SQLDet, gridReporte)
        Else
            gridReporte.Visible = False
            gridTotal.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionMars.localSqlServer, PeriodoSQL, "nombre_periodo", "orden", cmbPeriodo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, QuincenaSQL, "nombre_quincena", "id_quincena", cmbQuincena)
            Combo.LlenaDrop(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

            CargarReporte()
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            If cmbQuincena.SelectedValue = "" Then
                Dim TotalPor As Double = e.Row.Cells(7).Text
                For i = 0 To CInt(gridReporte.Rows.Count) - 0
                    If TotalPor <> 100 Then
                        e.Row.Cells(7).BackColor = Drawing.Color.Red : Else
                        e.Row.Cells(7).BackColor = Drawing.Color.GreenYellow : End If : Next i

                e.Row.Cells(7).Text = TotalPor & "%"
            End If

            If cmbQuincena.SelectedValue = "Q1" Then
                Dim TotalT1 As Double = e.Row.Cells(4).Text
                For i = 0 To CInt(gridReporte.Rows.Count) - 0
                    If TotalT1 <> 100 Then
                        e.Row.Cells(4).BackColor = Drawing.Color.Red : Else
                        e.Row.Cells(4).BackColor = Drawing.Color.GreenYellow : End If : Next i

                e.Row.Cells(4).Text = TotalT1 & "%"
            End If

            If cmbQuincena.SelectedValue = "Q2" Then
                Dim TotalPor As Double = e.Row.Cells(6).Text
                For i = 0 To CInt(gridReporte.Rows.Count) - 0
                    If TotalPor <> 100 Then
                        e.Row.Cells(6).BackColor = Drawing.Color.Red : Else
                        e.Row.Cells(6).BackColor = Drawing.Color.GreenYellow : End If : Next i

                e.Row.Cells(6).Text = TotalPor & "%"
            End If
        End If
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        CargarReporte()
    End Sub

    Sub CargarDetalle(ByVal SeleccionIDUsuario As String)
        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT RE.id_usuario,'Precio' as Tipo,CAD.nombre_cadena, ''nombre,RE.id_quincena, " & _
                    "CASE ISNULL(RE.estatus_precio,0) when 1 then 'CAPTURADA' when 2 then 'INCOMPLETA' else 'SIN CAPTURA' end as Estatus " & _
                    "FROM Conv_Rutas_Eventos_Precios as RE " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena=RE.id_cadena " & _
                    "WHERE RE.orden=" & cmbPeriodo.SelectedValue & " " & _
                    "AND RE.id_usuario = '" & SeleccionIDUsuario & "' " & _
                    "UNION ALL " & _
                    "SELECT RE.id_usuario,'Exhibiciones' as Tipo,CAD.nombre_cadena,TI.nombre,'' as id_quincena, " & _
                    "CASE ISNULL(RE.estatus_exhibiciones,0) when 1 then 'CAPTURADA' when 2 then 'INCOMPLETA' else 'SIN CAPTURA' end as Estatus " & _
                    "FROM Conv_Rutas_Eventos_Exhibiciones as RE " & _
                    "INNER JOIN Conv_Tiendas as TI ON TI.id_tienda= RE.id_tienda " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena =  TI.id_cadena " & _
                    "WHERE RE.orden=" & cmbPeriodo.SelectedValue & " " & _
                    "AND RE.id_usuario = '" & SeleccionIDUsuario & "' " & _
                    "ORDER BY RE.id_usuario, nombre", Me.gridDetalle)
    End Sub

    Sub CargarDetalleGenerales()
        RegionSQL = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)
        PromotorSQL = Acciones.Slc.cmb("RE.id_usuario", cmbPromotor.SelectedValue)

        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT RE.id_usuario,'Precio' as Tipo,CAD.nombre_cadena, ''nombre,RE.id_quincena, " & _
                    "CASE ISNULL(RE.estatus_precio,0) when 1 then 'CAPTURADA' when 2 then 'INCOMPLETA' else 'SIN CAPTURA' end as Estatus " & _
                    "FROM Conv_Rutas_Eventos_Precios as RE " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena=RE.id_cadena " & _
                    "WHERE RE.orden=" & cmbPeriodo.SelectedValue & " " & _
                    "AND RE.estatus_precio<>1 " & _
                    " " + RegionSQL + " " & _
                    " " + PromotorSQL + " " & _
                    "UNION ALL " & _
                    "SELECT RE.id_usuario,'Exhibiciones' as Tipo,CAD.nombre_cadena,TI.nombre,'' as id_quincena, " & _
                    "CASE ISNULL(RE.estatus_exhibiciones,0) when 1 then 'CAPTURADA' when 2 then 'INCOMPLETA' else 'SIN CAPTURA' end as Estatus " & _
                    "FROM Conv_Rutas_Eventos_Exhibiciones as RE " & _
                    "INNER JOIN Conv_Tiendas as TI ON TI.id_tienda= RE.id_tienda " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena =  TI.id_cadena " & _
                    "WHERE RE.orden=" & cmbPeriodo.SelectedValue & " " & _
                    "AND RE.estatus_exhibiciones<>1 " & _
                    " " + RegionSQL + " " & _
                    " " + PromotorSQL + " " & _
                    "ORDER BY RE.id_usuario, nombre", Me.gridDetalle)
    End Sub

    Private Sub gridReporte_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gridReporte.RowEditing
        CargarDetalle(gridReporte.Rows(e.NewEditIndex).Cells(1).Text)
        pnlDetalle.Visible = True
    End Sub

    Protected Sub btnDetalles_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDetalles.Click
        CargarDetalleGenerales()
        pnlDetalle.Visible = True
    End Sub

    Private Sub gridDetalle_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridDetalle.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 0 To CInt(gridReporte.Rows.Count) - 0
                If e.Row.Cells(5).Text = "SIN CAPTURA" Then
                    e.Row.Cells(5).ForeColor = Drawing.Color.Red : End If
                If e.Row.Cells(5).Text = "INCOMPLETA" Then
                    e.Row.Cells(5).ForeColor = Drawing.Color.Orange : End If
            Next i
        End If
    End Sub

    Sub Excel2()
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.gridReporte.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.gridDetalle)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Detalle Bitacora de captura.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
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
        form.Controls.Add(Me.pnlGrid)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Bitacora Captura " + cmbPeriodo.SelectedItem.ToString() + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub gridTotal_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridTotal.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim Total As Integer
            If e.Row.Cells(1).Text <> "&nbsp;" Then
                Total = e.Row.Cells(1).Text

                For i = 0 To CInt(gridTotal.Rows.Count) - 0
                    If Total <> 100 Then
                        e.Row.Cells(1).BackColor = Drawing.Color.Red : Else
                        e.Row.Cells(1).BackColor = Drawing.Color.GreenYellow : End If
                Next i

                e.Row.Cells(1).Text = e.Row.Cells(1).Text + "%"
            End If
        End If
    End Sub

    Private Sub cmbQuincena_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbQuincena.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub gridTotal_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gridTotal.SelectedIndexChanged

    End Sub
End Class