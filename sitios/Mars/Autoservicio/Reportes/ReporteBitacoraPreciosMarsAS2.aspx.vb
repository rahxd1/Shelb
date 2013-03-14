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

Partial Public Class ReporteBitacoraPreciosMarsAS2
    Inherits System.Web.UI.Page

    Dim Suma(3) As Integer
    Dim SQLDet, SQLReg, SeleccionIDUsuario As String

    Sub SQLCombo()
        MarsAS.SQLsCombo(cmbPeriodo.SelectedValue, cmbQuincena.SelectedValue, _
                        cmbRegion.SelectedValue, cmbEjecutivo.SelectedValue, _
                        cmbSupervisor.SelectedValue, cmbPromotor.SelectedValue, _
                        "", "View_Historial_AS_Precios")
    End Sub

    Sub CargarReporte()
        Dim RegionSQL, EjecutivoSQL, SupervisorSQL, PromotorSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            pnlDetalle.Visible = False

            RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
            EjecutivoSQL = Acciones.Slc.cmb("REL.region_mars", cmbEjecutivo.SelectedValue)
            SupervisorSQL = Acciones.Slc.cmb("REL.id_supervisor", cmbSupervisor.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("RE.id_usuario", cmbPromotor.SelectedValue)

            If cmbQuincena.SelectedValue = "" Then
                SQLReg = "SELECT REG.nombre_region,COUNT(RE.id_usuario) AS Reportes,  " & _
                            "ISNULL(CapturasP1.Capturas,0) as CapturasP1, " & _
                            "ISNULL(CapturasP2.Capturas,0) as CapturasP2, " & _
                            "((100*(ISNULL(CapturasP1.Capturas,0)+ISNULL(CapturasP2.Capturas,0)))/(COUNT(RE.id_usuario)))Totales " & _
                            "FROM AS_Precios_Rutas_Eventos as RE  " & _
                            "INNER JOIN AS_Tiendas as TI ON TI.id_tienda= RE.id_tienda  " & _
                            "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region  " & _
                            "FULL JOIN (SELECT id_region, COUNT(RE.estatus_precio) AS Capturas  " & _
                            "FROM AS_Precios_Rutas_Eventos as RE " & _
                            "INNER JOIN AS_Tiendas as TI ON TI.id_tienda= RE.id_tienda  " & _
                            "WHERE estatus_precio=1 AND id_quincena='Q1' AND orden=" & cmbPeriodo.SelectedValue & " " & _
                            "GROUP BY id_region)CapturasP1 " & _
                            "ON CapturasP1.id_region = REG.id_region " & _
                            "FULL JOIN (SELECT id_region, COUNT(RE.estatus_precio) AS Capturas  " & _
                            "FROM AS_Precios_Rutas_Eventos as RE " & _
                            "INNER JOIN AS_Tiendas as TI ON TI.id_tienda= RE.id_tienda  " & _
                            "WHERE estatus_precio=1 AND id_quincena='Q2' AND orden=" & cmbPeriodo.SelectedValue & " " & _
                            "GROUP BY id_region)CapturasP2 " & _
                            "ON CapturasP2.id_region = REG.id_region " & _
                            "WHERE RE.orden= " & cmbPeriodo.SelectedValue & " " & _
                            " GROUP BY REG.nombre_region,CapturasP1.Capturas,CapturasP2.Capturas ORDER BY REG.nombre_region"
            Else
                SQLReg = "SELECT REG.nombre_region,COUNT(RE.id_usuario) AS Reportes,  " & _
                            "ISNULL(CapturasP1.Capturas,0) as CapturasP1, " & _
                            "((100*(ISNULL(CapturasP1.Capturas,0)))/(COUNT(RE.id_usuario)))Totales  " & _
                            "FROM AS_Precios_Rutas_Eventos as RE  " & _
                            "INNER JOIN AS_Tiendas as TI ON TI.id_tienda= RE.id_tienda  " & _
                            "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region  " & _
                            "FULL JOIN (SELECT id_region, COUNT(RE.estatus_precio) AS Capturas  " & _
                            "FROM AS_Precios_Rutas_Eventos as RE " & _
                            "INNER JOIN AS_Tiendas as TI ON TI.id_tienda= RE.id_tienda  " & _
                            "WHERE estatus_precio=1 AND id_quincena='" & cmbQuincena.SelectedValue & "' AND orden=" & cmbPeriodo.SelectedValue & " " & _
                            "GROUP BY id_region)CapturasP1 " & _
                            "ON CapturasP1.id_region = REG.id_region " & _
                            "WHERE RE.orden=" & cmbPeriodo.SelectedValue & " AND RE.id_quincena='" & cmbQuincena.SelectedValue & "'" & _
                            " GROUP BY REG.nombre_region,CapturasP1.Capturas ORDER BY REG.nombre_region"
            End If


            CargaGrilla(ConexionMars.localSqlServer, SQLReg, Me.gridTotal)

            If cmbQuincena.SelectedValue = "" Then
                SQLDet = "SELECT RE.id_usuario as'Promotor', COUNT(RE.id_tienda) AS Reportes, " & _
                            "ISNULL(CapturasP1.Capturas,0) as 'Precios Q1', " & _
                            "ISNULL(CapturasP2.Capturas,0) as 'Precios Q2', " & _
                            "((100*(ISNULL(CapturasP1.Capturas,0)+ISNULL(CapturasP2.Capturas,0)))/(COUNT(RE.id_usuario)))'% Captura' " & _
                            "FROM AS_Precios_Rutas_Eventos as RE  " & _
                            "INNER JOIN AS_Tiendas as TI ON TI.id_tienda= RE.id_tienda  " & _
                            "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region  " & _
                            "FULL JOIN (SELECT id_usuario, COUNT(RE.estatus_precio) AS Capturas  " & _
                            "FROM AS_Precios_Rutas_Eventos as RE WHERE estatus_precio=1 AND id_quincena='Q1' AND orden=" & cmbPeriodo.SelectedValue & " " & _
                            "GROUP BY id_usuario)CapturasP1 " & _
                            "ON CapturasP1.id_usuario = RE.id_usuario  " & _
                            "FULL JOIN (SELECT id_usuario, COUNT(RE.estatus_precio) AS Capturas  " & _
                            "FROM AS_Precios_Rutas_Eventos as RE WHERE estatus_precio=1 AND id_quincena='Q2' AND orden=" & cmbPeriodo.SelectedValue & " " & _
                            "GROUP BY id_usuario)CapturasP2 " & _
                            "ON CapturasP2.id_usuario = RE.id_usuario " & _
                            "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario = RE.id_usuario " & _
                            "WHERE RE.orden= " & cmbPeriodo.SelectedValue & " " & _
                            " " + RegionSQL + " " & _
                            " " + EjecutivoSQL + " " & _
                            " " + SupervisorSQL + " " & _
                            " " + PromotorSQL + " " & _
                            " GROUP BY RE.id_usuario,CapturasP1.Capturas,CapturasP2.Capturas,RE.orden, REG.nombre_region "
            Else
                SQLDet = "SELECT RE.id_usuario as'Promotor', COUNT(RE.id_usuario) AS Reportes, " & _
                            "ISNULL(CapturasP1.Capturas,0) as 'Precios " & cmbQuincena.SelectedValue & "', " & _
                            "(100*(ISNULL(CapturasP1.Capturas,0))/(COUNT(RE.id_usuario)))'% Captura' " & _
                            "FROM AS_Precios_Rutas_Eventos as RE  " & _
                            "INNER JOIN AS_Tiendas as TI ON TI.id_tienda= RE.id_tienda  " & _
                            "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region  " & _
                            "FULL JOIN (SELECT id_usuario, COUNT(RE.estatus_precio) AS Capturas  " & _
                            "FROM AS_Precios_Rutas_Eventos as RE WHERE estatus_precio=1 AND id_quincena='" & cmbQuincena.SelectedValue & "' AND orden=" & cmbPeriodo.SelectedValue & " " & _
                            "GROUP BY id_usuario)CapturasP1 " & _
                            "ON CapturasP1.id_usuario = RE.id_usuario " & _
                            "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario = RE.id_usuario " & _
                            "WHERE RE.orden=" & cmbPeriodo.SelectedValue & " AND RE.id_quincena='" & cmbQuincena.SelectedValue & "'" & _
                            " " + RegionSQL + " " & _
                            " " + EjecutivoSQL + " " & _
                            " " + SupervisorSQL + " " & _
                            " " + PromotorSQL + " " & _
                            " GROUP BY RE.id_usuario,CapturasP1.Capturas,RE.orden, REG.nombre_region "
            End If

            CargaGrilla(ConexionMars.localSqlServer, SQLDet, Me.gridReporte)
        Else
            gridTotal.Visible = False
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

                Dim Total As Double = e.Row.Cells(5).Text
                For i = 0 To CInt(gridReporte.Rows.Count) - 0
                    If Total <> 100 Then
                        e.Row.Cells(5).BackColor = Drawing.Color.Red : Else
                        e.Row.Cells(5).BackColor = Drawing.Color.GreenYellow : End If
                Next i

                e.Row.Cells(5).Text = Total & "%"
            End If

            If Not cmbQuincena.SelectedValue = "" Then
                Dim Total As Double = e.Row.Cells(4).Text
                For i = 0 To CInt(gridReporte.Rows.Count) - 0
                    If Total <> 100 Then
                        e.Row.Cells(4).BackColor = Drawing.Color.Red : Else
                        e.Row.Cells(4).BackColor = Drawing.Color.GreenYellow : End If
                Next i

                e.Row.Cells(4).Text = Total & "%"
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

    Sub CargarDetalle()
        Dim QuincenaSQL As String

        pnlDetalle.Visible = True

        QuincenaSQL = Acciones.Slc.cmb("RE.id_quincena", cmbQuincena.SelectedValue)

        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT RE.id_usuario,CAD.nombre_cadena,TI.nombre,RE.id_quincena, " & _
                    "CASE ISNULL(RE.estatus_precio,0) when 1 then 'CAPTURADA' when 2 then 'INCOMPLETA' else 'SIN CAPTURA' end as Estatus " & _
                    "FROM AS_Precios_Rutas_Eventos as RE " & _
                    "INNER JOIN AS_Tiendas as TI ON TI.id_tienda= RE.id_tienda " & _
                    "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena =  TI.id_cadena " & _
                    "WHERE RE.orden = " & cmbPeriodo.SelectedValue & " " & _
                    "AND RE.id_usuario = '" & SeleccionIDUsuario & "' " & _
                    " " + QuincenaSQL + " " & _
                    "ORDER BY RE.id_usuario, TI.nombre", Me.gridDetalle)
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
                        "CASE ISNULL(RE.estatus_precio,0) when 1 then 'CAPTURADA' when 2 then 'INCOMPLETA' else 'SIN CAPTURA' end as Estatus " & _
                        "FROM AS_Precios_Rutas_Eventos as RE " & _
                        "INNER JOIN AS_Tiendas as TI ON TI.id_tienda= RE.id_tienda " & _
                        "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region " & _
                        "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena =  TI.id_cadena " & _
                        "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario = RE.id_usuario " & _
                        "WHERE RE.orden =" & cmbPeriodo.SelectedValue & " " & _
                        " " + QuincenaSQL + " " & _
                        " " + RegionSQL + " " & _
                        " " + EjecutivoSQL + " " & _
                        " " + SupervisorSQL + " " & _
                        " " + PromotorSQL + " " & _
                        "AND RE.estatus_precio<>1" & _
                        "ORDER BY RE.id_usuario, TI.nombre", Me.gridDetalle)
    End Sub

    Private Sub gridReporte_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gridReporte.RowEditing
        SeleccionIDUsuario = gridReporte.Rows(e.NewEditIndex).Cells(1).Text
        CargarDetalle()
        pnlDetalle.Visible = True
    End Sub

    Protected Sub btnDetalles_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDetalles.Click
        CargarDetalleGenerales()
        pnlDetalle.Visible = True
        SeleccionIDUsuario = "General"
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Detalle Bitacora de captura " + SeleccionIDUsuario + ".xls")
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Bitacora captura precios " + cmbPeriodo.SelectedItem.ToString() + ".xls")
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