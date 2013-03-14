﻿Imports System
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

Partial Public Class ReporteBitacoraMarsAS
    Inherits System.Web.UI.Page

    Dim Suma(3) As Integer

    Sub SQLCombo()
        MarsAS.SQLsCombo(cmbPeriodo.SelectedValue, cmbQuincena.SelectedValue, _
                          cmbRegion.SelectedValue, cmbEjecutivo.SelectedValue, _
                          cmbSupervisor.SelectedValue, cmbPromotor.SelectedValue, _
                          "", "View_Historial_AS")
    End Sub

    Sub CargarReporte()
        Dim RegionSQL, EjecutivoSQL, SupervisorSQL, PromotorSQL As String
        Dim SQLReg, SQLDet As String

        If cmbPeriodo.SelectedValue <> "" Then
            pnlDetalle.Visible = False

            RegionSQL = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)
            EjecutivoSQL = Acciones.Slc.cmb("REL.region_mars", cmbEjecutivo.SelectedValue)
            SupervisorSQL = Acciones.Slc.cmb("REL.id_supervisor", cmbSupervisor.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("RE.id_usuario", cmbPromotor.SelectedValue)

            If cmbQuincena.SelectedValue = "" Then
                SQLReg = "SELECT REG.nombre_region,REG.Reportes,  " & _
                            "ISNULL(CapturasP1.Capturas,0) as CapturasP1, " & _
                            "ISNULL(CapturasP2.Capturas,0) as CapturasP2, " & _
                            "((100*(ISNULL(CapturasP1.Capturas,0)+ISNULL(CapturasP2.Capturas,0)))/(REG.Reportes))Totales " & _
                            "FROM (select TI.id_region,REG.nombre_region,RE.id_periodo,COUNT(RE.id_tienda)Reportes " & _
                            "FROM AS_Rutas_Eventos as RE " & _
                            "INNER JOIN AS_Tiendas as TI ON TI.id_tienda= RE.id_tienda " & _
                            "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region " & _
                            "WHERE RE.id_periodo='" & cmbPeriodo.SelectedValue & "' " & _
                            "GROUP BY  TI.id_region,REG.nombre_region,RE.id_periodo)REG " & _
                            "FULL JOIN (SELECT id_region, COUNT(RE.estatus_anaquel) AS Capturas  " & _
                            "FROM AS_Rutas_Eventos as RE " & _
                            "INNER JOIN AS_Tiendas as TI ON TI.id_tienda= RE.id_tienda  " & _
                            "WHERE estatus_anaquel=1 AND id_quincena='Q1' AND id_periodo='" & cmbPeriodo.SelectedValue & "' " & _
                            "GROUP BY id_region)CapturasP1 " & _
                            "ON CapturasP1.id_region = REG.id_region " & _
                            "FULL JOIN (SELECT id_region, COUNT(RE.estatus_anaquel) AS Capturas  " & _
                            "FROM AS_Rutas_Eventos as RE " & _
                            "INNER JOIN AS_Tiendas as TI ON TI.id_tienda= RE.id_tienda  " & _
                            "WHERE estatus_anaquel=1 AND id_quincena='Q2' AND id_periodo='" & cmbPeriodo.SelectedValue & "' " & _
                            "GROUP BY id_region)CapturasP2 " & _
                            "ON CapturasP2.id_region = REG.id_region " & _
                            "WHERE REG.id_periodo= '" & cmbPeriodo.SelectedValue & "' " & _
                            "ORDER BY REG.nombre_region"
            Else
                SQLReg = "SELECT REG.nombre_region,REG.Reportes, " & _
                            "ISNULL(CapturasP2.Capturas,0) as CapturasP2, " & _
                            "((100*(ISNULL(CapturasP2.Capturas,0)))/(REG.Reportes))Totales " & _
                            "FROM (select TI.id_region,REG.nombre_region,RE.id_periodo,RE.id_quincena,COUNT(RE.id_tienda)Reportes " & _
                            "FROM AS_Rutas_Eventos as RE " & _
                            "INNER JOIN AS_Tiendas as TI ON TI.id_tienda= RE.id_tienda " & _
                            "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region " & _
                            "WHERE RE.id_periodo='" & cmbPeriodo.SelectedValue & "' " & _
                            "GROUP BY TI.id_region,REG.nombre_region,RE.id_periodo,RE.id_quincena)REG " & _
                            "FULL JOIN (SELECT id_region, COUNT(RE.estatus_anaquel) AS Capturas  " & _
                            "FROM AS_Rutas_Eventos as RE " & _
                            "INNER JOIN AS_Tiendas as TI ON TI.id_tienda= RE.id_tienda  " & _
                            "WHERE estatus_anaquel=1  AND id_quincena='" & cmbQuincena.SelectedValue & "' AND id_periodo='" & cmbPeriodo.SelectedValue & "' " & _
                            "GROUP BY id_region)CapturasP2 " & _
                            "ON CapturasP2.id_region = REG.id_region " & _
                            "WHERE REG.id_periodo= '" & cmbPeriodo.SelectedValue & "' AND REG.id_quincena='" & cmbQuincena.SelectedValue & "'" & _
                            "ORDER BY REG.nombre_region"
            End If


            CargaGrilla(ConexionMars.localSqlServer, SQLReg, gridTotal)

            If cmbQuincena.SelectedValue = "" Then
                SQLDet = "SELECT RE.id_usuario as'Promotor', COUNT(RE.id_tienda) AS Reportes, " & _
                            "ISNULL(CapturasP1.Capturas,0) as 'Anaquel Q1', " & _
                            "ISNULL(CapturasP2.Capturas,0) as 'Anaquel Q2', " & _
                            "100*(ISNULL(CapturasP1.Capturas,0)+ISNULL(CapturasP2.Capturas,0))/COUNT(RE.id_tienda)'% Captura'  " & _
                            "FROM AS_Rutas_Eventos as RE  " & _
                            "INNER JOIN AS_Tiendas as TI ON TI.id_tienda= RE.id_tienda  " & _
                            "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region  " & _
                            "FULL JOIN (SELECT id_usuario, COUNT(RE.estatus_anaquel) AS Capturas  " & _
                            "FROM AS_Rutas_Eventos as RE WHERE estatus_anaquel=1 AND id_quincena='Q1' AND id_periodo='" & cmbPeriodo.SelectedValue & "' " & _
                            "GROUP BY id_usuario)CapturasP1 " & _
                            "ON CapturasP1.id_usuario = RE.id_usuario  " & _
                            "FULL JOIN (SELECT id_usuario, COUNT(RE.estatus_anaquel) AS Capturas  " & _
                            "FROM AS_Rutas_Eventos as RE WHERE estatus_anaquel=1 AND id_quincena='Q2' AND id_periodo='" & cmbPeriodo.SelectedValue & "' " & _
                            "GROUP BY id_usuario)CapturasP2 " & _
                            "ON CapturasP2.id_usuario = RE.id_usuario " & _
                            "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario = RE.id_usuario " & _
                            "WHERE RE.id_periodo= '" & cmbPeriodo.SelectedValue & "' " & _
                            " " + RegionSQL + " " & _
                            " " + EjecutivoSQL + " " & _
                            " " + SupervisorSQL + " " & _
                            " " + PromotorSQL + " " & _
                            " GROUP BY RE.id_usuario,CapturasP1.Capturas,CapturasP2.Capturas,RE.id_periodo, REG.nombre_region "
            Else
                SQLDet = "SELECT RE.id_usuario as'Promotor', COUNT(RE.id_tienda) AS Reportes, " & _
                            "ISNULL(CapturasP1.Capturas,0) as 'Anaquel " & cmbQuincena.SelectedValue & "', " & _
                            "100*(ISNULL(CapturasP1.Capturas,0))/COUNT(RE.id_tienda)'% Captura'  " & _
                            "FROM AS_Rutas_Eventos as RE  " & _
                            "INNER JOIN AS_Tiendas as TI ON TI.id_tienda= RE.id_tienda  " & _
                            "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region  " & _
                            "FULL JOIN (SELECT id_usuario, COUNT(RE.estatus_anaquel) AS Capturas  " & _
                            "FROM AS_Rutas_Eventos as RE WHERE estatus_anaquel=1 AND id_quincena='" & cmbQuincena.SelectedValue & "' AND id_periodo='" & cmbPeriodo.SelectedValue & "' " & _
                            "GROUP BY id_usuario)CapturasP1 " & _
                            "ON CapturasP1.id_usuario = RE.id_usuario " & _
                            "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario = RE.id_usuario " & _
                            "WHERE RE.id_periodo= '" & cmbPeriodo.SelectedValue & "' AND RE.id_quincena='" & cmbQuincena.SelectedValue & "'" & _
                            " " + RegionSQL + " " & _
                            " " + EjecutivoSQL + " " & _
                            " " + SupervisorSQL + " " & _
                             " " + PromotorSQL + " " & _
                            " GROUP BY RE.id_usuario,CapturasP1.Capturas,RE.id_periodo, REG.nombre_region "
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
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PeriodoSQLCmb, "nombre_periodo", "orden", cmbPeriodo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.QuincenaSQLCmb, "nombre_quincena", "id_quincena", cmbQuincena)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        End If
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Suma(0) = Suma(0) + e.Row.Cells(2).Text
            Suma(1) = Suma(1) + e.Row.Cells(3).Text

            If cmbQuincena.SelectedValue = "" Then
                Suma(2) = Suma(2) + e.Row.Cells(4).Text

                For i = 0 To CInt(gridReporte.Rows.Count) - 0
                    If e.Row.Cells(5).Text <> 100 Then
                        e.Row.Cells(5).BackColor = Drawing.Color.Red : Else
                        e.Row.Cells(5).BackColor = Drawing.Color.GreenYellow : End If
                Next i

                e.Row.Cells(5).Text = e.Row.Cells(5).Text & "%"
            End If

            If Not cmbQuincena.SelectedValue = "" Then
                For i = 0 To CInt(gridReporte.Rows.Count) - 0
                    If e.Row.Cells(4).Text <> 100 Then
                        e.Row.Cells(4).BackColor = Drawing.Color.Red : Else
                        e.Row.Cells(4).BackColor = Drawing.Color.GreenYellow : End If
                Next i

                e.Row.Cells(4).Text = e.Row.Cells(4).Text & "%"
            End If
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(2).Text = Suma(0)
            e.Row.Cells(3).Text = Suma(1)

            If cmbQuincena.SelectedValue = "" Then
                e.Row.Cells(4).Text = Suma(2)
                e.Row.Cells(5).Text = FormatPercent((Suma(1) + Suma(2)) / Suma(0), 0, 0, 0, 0)
            Else
                e.Row.Cells(4).Text = FormatPercent(Suma(1) / Suma(0), 0, 0, 0, 0)
            End If
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

    Sub CargarDetalle(ByVal SeleccionIDUsuario As String)
        Dim QuincenaSQL As String
        pnlDetalle.Visible = True

        QuincenaSQL = Acciones.Slc.cmb("RE.id_quincena", cmbQuincena.SelectedValue)

        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT RE.id_usuario,CAD.nombre_cadena,TI.nombre, RE.id_quincena, " & _
                    "CASE ISNULL(RE.estatus_anaquel,0) when 1 then 'CAPTURADA' when 2 then 'INCOMPLETA' else 'SIN CAPTURA' end as Estatus " & _
                    "FROM AS_Rutas_Eventos as RE " & _
                    "INNER JOIN AS_Tiendas as TI ON TI.id_tienda= RE.id_tienda " & _
                    "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena =  TI.id_cadena " & _
                    "WHERE RE.id_periodo = '" & cmbPeriodo.SelectedValue & "'" & _
                    "AND RE.id_usuario = '" & SeleccionIDUsuario & "' " & _
                    " " + QuincenaSQL + " " & _
                    "ORDER BY RE.id_usuario, TI.nombre, RE.id_quincena ", Me. gridDetalle)
    End Sub

    Sub CargarDetalleGenerales()
        Dim QuincenaSQL, RegionSQL, EjecutivoSQL, SupervisorSQL, PromotorSQL As String

        pnlDetalle.Visible = True

        QuincenaSQL = Acciones.Slc.cmb("RE.id_quincena", cmbQuincena.SelectedValue)
        RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
        EjecutivoSQL = Acciones.Slc.cmb("REL.region_mars", cmbEjecutivo.SelectedValue)
        SupervisorSQL = Acciones.Slc.cmb("REL.id_supervisor", cmbSupervisor.SelectedValue)
        PromotorSQL = Acciones.Slc.cmb("RE.id_usuario", cmbPromotor.SelectedValue)

        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT RE.id_usuario,CAD.nombre_cadena,TI.nombre,RE.id_quincena, " & _
                    "CASE ISNULL(RE.estatus_anaquel,0) when 1 then 'CAPTURADA' when 2 then 'INCOMPLETA' else 'SIN CAPTURA' end as Estatus " & _
                    "FROM AS_Rutas_Eventos as RE " & _
                    "INNER JOIN AS_Tiendas as TI ON TI.id_tienda= RE.id_tienda " & _
                    "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena =  TI.id_cadena " & _
                    "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario = RE.id_usuario " & _
                    "WHERE RE.id_periodo = '" & cmbPeriodo.SelectedValue & "'" & _
                    " " + QuincenaSQL + " " & _
                    " " + RegionSQL + " " & _
                    " " + EjecutivoSQL + " " & _
                    " " + SupervisorSQL + " " & _
                    " " + PromotorSQL + " " & _
                    "AND RE.estatus_anaquel<>1 " & _
                    "ORDER BY RE.id_usuario, TI.nombre, RE.id_quincena", Me.gridDetalle)
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
                If e.Row.Cells(4).Text = "SIN CAPTURA" Then
                    e.Row.Cells(4).ForeColor = Drawing.Color.Red : End If
                If e.Row.Cells(4).Text = "INCOMPLETA" Then
                    e.Row.Cells(4).ForeColor = Drawing.Color.Orange : End If
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Bitacora captura anaquel y exhibiciones " + cmbPeriodo.SelectedItem.ToString() + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub gridTotal_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridTotal.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim Total As Integer
            Total = e.Row.Cells(1).Text

            For i = 0 To CInt(gridTotal.Rows.Count) - 0
                If Total <> 100 Then
                    e.Row.Cells(1).BackColor = Drawing.Color.Red : Else
                    e.Row.Cells(1).BackColor = Drawing.Color.GreenYellow : End If
            Next i

            e.Row.Cells(1).Text = e.Row.Cells(1).Text + "%"
        End If
    End Sub

    Private Sub cmbQuincena_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbQuincena.SelectedIndexChanged
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

    Private Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarReporte()
    End Sub
End Class