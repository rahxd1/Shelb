Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteBitacoraHB
    Inherits System.Web.UI.Page

    Dim PromotorSel, RegionSel, TiendaSel As String
    Dim PeriodoSQL, PromotorSQL, RegionSQL, TiendaSQL As String

    Sub SQLCombo()
        If cmbRegion.SelectedValue <> "" Then
            If cmbRegion.SelectedValue <> 0 Then
                RegionSel = "AND REG.id_region=" & cmbRegion.SelectedValue & " " : Else
                RegionSel = "" : End If : End If

        If Not cmbPromotor.Text = "" Then
            PromotorSel = "AND RUT.id_usuario='" & cmbPromotor.SelectedValue & "'":Else
            PromotorSel = "":End If

        PeriodoSQL = "SELECT id_periodo, nombre_periodo " & _
                    "FROM HawaiianBanana_Periodos ORDER BY fecha_inicio DESC"

        RegionSQL = "SELECT DISTINCT nombre_region, id_region " & _
                    "FROM Regiones WHERE id_region<>0 ORDER BY nombre_region"

        PromotorSQL = "SELECT DISTINCT RUT.id_usuario " & _
                    "FROM HawaiianBanana_CatRutas AS RUT " & _
                    "INNER JOIN HawaiianBanana_Tiendas AS TI ON TI.id_tienda = RUT.id_tienda " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    "WHERE TI.estatus = 1 " & _
                    " " + RegionSel + " " & _
                    " ORDER BY RUT.id_usuario"

        TiendaSQL = "SELECT DISTINCT RUT.id_tienda, TI.nombre " & _
                    "FROM HawaiianBanana_CatRutas AS RUT " & _
                    "INNER JOIN HawaiianBanana_Tiendas AS TI ON TI.id_tienda = RUT.id_tienda " & _
                    "INNER JOIN Cadenas_Tiendas AS CAD ON TI.id_cadena = CAD.id_cadena " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    " " + RegionSel + " " & _
                    " " + PromotorSel + " " & _
                    " ORDER BY TI.nombre"
    End Sub

    Sub CargarReporte()
        If cmbRegion.SelectedValue <> 0 Then
            RegionSQL = "AND REG.id_region=" & cmbRegion.SelectedValue & " " : Else
            RegionSQL = "" : End If

        If Not cmbPromotor.SelectedValue = "" Then
            PromotorSQL = "AND RE.id_usuario = '" & cmbPromotor.SelectedValue & "' " : Else
            PromotorSQL = "" : End If

        If Not cmbTienda.SelectedValue = "" Then
            TiendaSQL = "AND TI.id_tienda = '" & cmbTienda.SelectedValue & "' " : Else
            TiendaSQL = "" : End If

        CargaGrilla(ConexionEnergizer.localSqlServer, _
                    "SELECT REG.nombre_region,RE.id_usuario, COUNT(RE.id_usuario) AS Tiendas, " & _
                    "SUM(RE.estatus1) AS Capturas1, SUM(RE.estatus2) AS Capturas2, " & _
                    "SUM(RE.estatus1)+SUM(RE.estatus2)CapturasTotales, " & _
                    "CASE WHEN (2/COUNT(RE.id_usuario))<>0 then 100*(SUM(RE.estatus1)+SUM(RE.estatus2))/(2/COUNT(RE.id_usuario))else 0 end Porcentaje " & _
                    "FROM HawaiianBanana_Rutas_Eventos as RE " & _
                    "INNER JOIN HawaiianBanana_Tiendas as TI ON TI.id_tienda= RE.id_tienda " & _
                    "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region " & _
                    "WHERE RE.id_periodo = '" & cmbPeriodo.SelectedValue & "'" & _
                    " " + RegionSQL + " " & _
                    " " + PromotorSQL + " " & _
                    " " + TiendaSQL + " " & _
                    " GROUP BY RE.id_usuario, RE.id_periodo, REG.nombre_region", Me.gridReporte)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

            CargarReporte()
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionEnergizer.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionEnergizer.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionEnergizer.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(6).Text = e.Row.Cells(6).Text & "%"
        End If
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbTienda_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbTienda.SelectedIndexChanged
        CargarReporte()
    End Sub

    Sub CargarDetalle(ByVal SeleccionIDUsuario As String)
        CargaGrilla(ConexionEnergizer.localSqlServer, _
                    "SELECT RE.id_usuario,CAD.nombre_cadena,TI.nombre, RE.estatus1, RE.estatus2 " & _
                    "FROM HawaiianBanana_Rutas_Eventos as RE " & _
                    "INNER JOIN HawaiianBanana_Tiendas as TI " & _
                    "ON TI.id_tienda= RE.id_tienda " & _
                    "INNER JOIN Regiones as REG " & _
                    "ON REG.id_region= TI.id_region " & _
                    "INNER JOIN Cadenas_Tiendas as CAD " & _
                    "ON CAD.id_cadena =  TI.id_cadena " & _
                    "WHERE RE.id_periodo = '" & cmbPeriodo.Text & "'" & _
                    "AND RE.id_usuario = '" & SeleccionIDUsuario & "' " & _
                    " ORDER BY RE.id_usuario, TI.nombre", Me.gridDetalle)
    End Sub

    Sub CargarDetalleGenerales()
        CargaGrilla(ConexionEnergizer.localSqlServer, _
                    "SELECT RE.id_usuario,CAD.nombre_cadena,TI.nombre, RE.estatus1, RE.estatus2 " & _
                    "FROM HawaiianBanana_Rutas_Eventos as RE " & _
                    "INNER JOIN HawaiianBanana_Tiendas as TI " & _
                    "ON TI.id_tienda= RE.id_tienda " & _
                    "INNER JOIN Regiones as REG " & _
                    "ON REG.id_region= TI.id_region " & _
                    "INNER JOIN Cadenas_Tiendas as CAD " & _
                    "ON CAD.id_cadena =  TI.id_cadena " & _
                    "WHERE RE.id_periodo = '" & cmbPeriodo.Text & "'" & _
                    "AND RE.estatus1 = 0 AND RE.estatus2 =0 " & _
                    " ORDER BY RE.id_usuario, TI.nombre", Me.gridDetalle)
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
                If e.Row.Cells(3).Text = 0 Then
                    e.Row.Cells(0).ForeColor = Drawing.Color.Red
                    e.Row.Cells(1).ForeColor = Drawing.Color.Red
                    e.Row.Cells(2).ForeColor = Drawing.Color.Red
                    e.Row.Cells(3).ForeColor = Drawing.Color.Red
                    e.Row.Cells(4).ForeColor = Drawing.Color.Red
                End If
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
        Response.AddHeader("Content-Disposition", "attachment;filename=DetalleBitacoraCaptura.xls")
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
        form.Controls.Add(Me.gridReporte)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=BitacoraCaptura" + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

End Class