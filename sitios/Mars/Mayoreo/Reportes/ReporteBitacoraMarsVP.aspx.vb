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

Partial Public Class ReporteBitacoraMarsVP
    Inherits System.Web.UI.Page

    Dim PeriodoSel, SemanaSel As String
    Dim PeriodoSQL, SemanaSQL, RegionSQL As String
    Dim Suma(3) As Integer

    Sub SQLCombo()
        PeriodoSel = Acciones.Slc.cmb("RE.orden", cmbPeriodo.SelectedValue)

        PeriodoSQL = "SELECT DISTINCT H.orden, PER.nombre_periodo, PER.fecha_fin_periodo " & _
                     "FROM Mayoreo_Verificadores_Rutas_Eventos as H " & _
                     "INNER JOIN Periodos_Nuevo as PER ON H.orden = PER.orden " & _
                     "ORDER BY PER.fecha_fin_periodo DESC"

        SemanaSQL = "SELECT DISTINCT id_semana, nombre_semana " & _
                    "FROM Semanas ORDER BY id_semana"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Mayoreo_Verificadores_Rutas_Eventos AS RE " & _
                    "INNER JOIN Mayoreo_Tiendas AS TI ON TI.id_tienda = RE.id_tienda " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    "WHERE TI.id_tienda<>0 " + PeriodoSel + " " & _
                    " ORDER BY REG.nombre_region"

    End Sub

    Sub CargarReporte()
        If cmbPeriodo.SelectedValue <> "" Then
            SemanaSQL = Acciones.Slc.cmb("RE.id_semana", cmbSemana.SelectedValue)
            RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)

            CargaGrilla(ConexionMars.localSqlServer, _
                        "SELECT REG.nombre_region,RE.id_usuario, COUNT(RE.id_usuario)Tiendas," & _
                        "ISNULL(Capturas.Capturas,0) as Capturas, " & _
                        "ISNULL(Incompletas.Incompletas,0) as Incompletas,  " & _
                        "(100*ISNULL(Capturas.Capturas,0))/COUNT(RE.id_usuario)Porcentaje " & _
                        "FROM Mayoreo_Verificadores_Rutas_Eventos as RE " & _
                        "INNER JOIN Mayoreo_Tiendas as TI ON TI.id_tienda= RE.id_tienda " & _
                        "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region " & _
                        "FULL JOIN (SELECT id_usuario, COUNT(RE.estatus) AS Capturas " & _
                        "FROM Mayoreo_Verificadores_Rutas_Eventos as RE WHERE estatus=1 AND orden=" & cmbPeriodo.SelectedValue & " " + SemanaSQL + " GROUP BY id_usuario)Capturas " & _
                        "ON Capturas.id_usuario = RE.id_usuario " & _
                        "FULL JOIN (SELECT id_usuario, COUNT(RE.estatus) AS Incompletas " & _
                        "FROM Mayoreo_Verificadores_Rutas_Eventos as RE WHERE estatus=2 AND orden=" & cmbPeriodo.SelectedValue & " " + SemanaSQL + " GROUP BY id_usuario)Incompletas " & _
                        "ON Incompletas.id_usuario = RE.id_usuario " & _
                        "WHERE RE.orden=" & cmbPeriodo.SelectedValue & " " & _
                        " " + SemanaSQL + RegionSQL + " " & _
                        " GROUP BY RE.id_usuario,Capturas,Incompletas,RE.orden, REG.nombre_region", _
                        gridReporte)
        Else
            gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionMars.localSqlServer, PeriodoSQL, "nombre_periodo", "orden", cmbPeriodo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, SemanaSQL, "nombre_semana", "id_semana", cmbSemana)
            Combo.LlenaDrop(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)

            CargarReporte()
        End If
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbSemana_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbSemana.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        CargarReporte()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Suma(0) = Suma(0) + e.Row.Cells(2).Text ''Suma Tiendas
            Suma(1) = Suma(1) + e.Row.Cells(3).Text ''Suma Capturas
            Suma(2) = Suma(2) + e.Row.Cells(4).Text ''Suma Incompletas

            Select Case e.Row.Cells(5).Text
                Case Is = 0
                    e.Row.Cells(5).BackColor = Drawing.Color.Red
                Case Is = 100
                    e.Row.Cells(5).BackColor = Drawing.Color.GreenYellow
                Case Is < 99
                    e.Row.Cells(5).BackColor = Drawing.Color.Orange
            End Select

            e.Row.Cells(5).Text = e.Row.Cells(5).Text & "%"
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(2).Text = Suma(0)
            e.Row.Cells(3).Text = Suma(1)
            e.Row.Cells(4).Text = Suma(2)
            e.Row.Cells(5).Text = FormatPercent(Suma(1) / Suma(0), 0, 0, 0, 0)
        End If
    End Sub

    Sub CargarDetalle(ByVal SeleccionIDUsuario As String)
        SemanaSQL = Acciones.Slc.cmb("RE.id_semana", cmbSemana.SelectedValue)

        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT RE.id_usuario,CAD.nombre_cadena,TI.nombre, " & _
                    "CASE RE.estatus when 2 then 'INCOMPLETA' when 1 then 'CAPTURADA' " & _
                    "when 0 then 'SIN CAPTURA' end as estatus " & _
                    "FROM Mayoreo_Verificadores_Rutas_Eventos as RE " & _
                    "INNER JOIN Mayoreo_Tiendas as TI ON TI.id_tienda= RE.id_tienda " & _
                    "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region " & _
                    "INNER JOIN Cadenas_Mayoreo as CAD ON CAD.id_cadena =  TI.id_cadena " & _
                    "WHERE RE.orden=" & cmbPeriodo.SelectedValue & " " & _
                    " " + SemanaSQL + " " & _
                    "AND RE.id_usuario = '" & SeleccionIDUsuario & "' " & _
                    " ORDER BY RE.id_usuario, TI.nombre", gridDetalle)
    End Sub

    Sub CargarDetalleGenerales()
        SemanaSQL = Acciones.Slc.cmb("id_semana", cmbSemana.SelectedValue)
        RegionSQL = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)

        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT RE.id_usuario,CAD.nombre_cadena,TI.nombre, " & _
                    "CASE RE.estatus when 2 then 'INCOMPLETA' when 1 then 'CAPTURADA'" & _
                    "when 0 then 'SIN CAPTURA' end as estatus " & _
                    "FROM Mayoreo_Verificadores_Rutas_Eventos as RE " & _
                    "INNER JOIN Mayoreo_Tiendas as TI ON TI.id_tienda= RE.id_tienda " & _
                    "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region " & _
                    "INNER JOIN Cadenas_Mayoreo as CAD ON CAD.id_cadena =  TI.id_cadena " & _
                    "WHERE RE.orden=" & cmbPeriodo.SelectedValue & " " & _
                    " " + SemanaSQL + " " & _
                    " " + RegionSQL + " " & _
                    "AND RE.estatus = 0 " & _
                    " ORDER BY RE.id_usuario, TI.nombre", gridDetalle)
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
            Select Case e.Row.Cells(3).Text
                Case Is = "SIN CAPTURA"
                    e.Row.Cells(0).ForeColor = Drawing.Color.Red
                    e.Row.Cells(1).ForeColor = Drawing.Color.Red
                    e.Row.Cells(2).ForeColor = Drawing.Color.Red
                    e.Row.Cells(3).ForeColor = Drawing.Color.Red

                Case Is = "INCOMPLETA"
                    e.Row.Cells(0).ForeColor = Drawing.Color.OrangeRed
                    e.Row.Cells(1).ForeColor = Drawing.Color.OrangeRed
                    e.Row.Cells(2).ForeColor = Drawing.Color.OrangeRed
                    e.Row.Cells(3).ForeColor = Drawing.Color.OrangeRed
            End Select
        End If
    End Sub

    Sub Excel2()
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Detalle Bitacora de captura Verificadores.xls")
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Bitacora Captura Verificadores " + cmbPeriodo.SelectedItem.ToString() + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub


End Class