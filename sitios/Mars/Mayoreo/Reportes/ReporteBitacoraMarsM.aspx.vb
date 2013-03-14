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

Partial Public Class ReporteBitacoraMarsM
    Inherits System.Web.UI.Page

    Dim PeriodoSel, QuincenaSel, PromotorSel, RegionSel As String
    Dim PeriodoSQL, QuincenaSQL, PromotorSQL, RegionSQL As String
    Dim Suma(3) As Integer
    Dim SQLDet, SQLReg As String

    Sub SQLCombo()
        PeriodoSel = Acciones.Slc.cmb("orden", cmbPeriodo.SelectedValue)
        QuincenaSel = Acciones.Slc.cmb("id_quincena", cmbQuincena.SelectedValue)
        RegionSel = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)
        PromotorSel = Acciones.Slc.cmb("id_usuario", cmbPromotor.SelectedValue)

        PeriodoSQL = "SELECT DISTINCT orden,nombre_periodo " & _
                     "from View_RE_Mayoreo ORDER BY orden DESC"

        QuincenaSQL = "SELECT DISTINCT id_quincena, nombre_quincena " & _
                    "from View_RE_Mayoreo " & _
                    "WHERE id_quincena<>'' " + PeriodoSel + " " & _
                    "ORDER BY nombre_quincena"

        RegionSQL = "SELECT DISTINCT id_region, nombre_region " & _
                    "FROM View_RE_Mayoreo " & _
                    "WHERE nombre_region<>'' " + PeriodoSel + QuincenaSel + " " & _
                    "ORDER BY nombre_region"

        PromotorSQL = "SELECT DISTINCT id_usuario " & _
                    "FROM View_RE_Mayoreo " & _
                    "WHERE id_usuario<>'' " + PeriodoSel + QuincenaSel + " " & _
                    " " + RegionSel + " " & _
                    "ORDER BY id_usuario"
    End Sub

    Sub CargarReporte()
        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("RE.id_usuario", cmbPromotor.SelectedValue)

            Select Case cmbQuincena.SelectedValue
                Case Is = ""
                    SQLReg = "SELECT nombre_region,COUNT(RE.id_usuario) AS Tiendas, " & _
                        "ISNULL(CapturasP1.Capturas,0) as CapturasP1, " & _
                        "ISNULL(CapturasP2.Capturas,0) as CapturasP2, " & _
                        "((100*(ISNULL(CapturasP1.Capturas,0)+ISNULL(CapturasP2.Capturas,0)))/COUNT(RE.id_usuario))Totales " & _
                        "FROM View_RE_Mayoreo as RE  " & _
                        "FULL JOIN (SELECT id_region, COUNT(estatus) AS Capturas  " & _
                        "FROM View_RE_Mayoreo as RE " & _
                        "WHERE estatus=1 AND id_quincena='Q1' AND orden=" & cmbPeriodo.SelectedValue & " " & _
                        "GROUP BY id_region)CapturasP1 ON CapturasP1.id_region = RE.id_region " & _
                        "FULL JOIN (SELECT id_region, COUNT(estatus) AS Capturas  " & _
                        "FROM View_RE_Mayoreo as RE " & _
                        "WHERE estatus=1 AND id_quincena='Q2' AND orden=" & cmbPeriodo.SelectedValue & " " & _
                        "GROUP BY id_region)CapturasP2 ON CapturasP2.id_region = RE.id_region " & _
                        "WHERE orden= '" & cmbPeriodo.SelectedValue & "' " & _
                        " GROUP BY nombre_region,CapturasP1.Capturas,CapturasP2.Capturas "

                    SQLDet = "SELECT nombre_region as 'Región',RE.id_usuario as'Promotor', COUNT(RE.id_usuario) AS Tiendas, " & _
                        "ISNULL(CapturasP1.Capturas,0) as 'Q1', " & _
                        "ISNULL(CapturasP2.Capturas,0) as 'Q2', " & _
                        "100*(ISNULL(CapturasP1.Capturas,0)+ISNULL(CapturasP2.Capturas,0))/(COUNT(RE.id_usuario)*2) as '% Captura' " & _
                        "FROM View_RE_Mayoreo as RE  " & _
                        "FULL JOIN (SELECT id_usuario, COUNT(estatus) AS Capturas  " & _
                        "FROM View_RE_Mayoreo WHERE estatus=1 AND id_quincena='Q1' AND orden=" & cmbPeriodo.SelectedValue & " " & _
                        "GROUP BY id_usuario)CapturasP1 ON CapturasP1.id_usuario = RE.id_usuario  " & _
                        "FULL JOIN (SELECT id_usuario, COUNT(estatus) AS Capturas  " & _
                        "FROM View_RE_Mayoreo as RE WHERE estatus=1 AND id_quincena='Q2' AND orden=" & cmbPeriodo.SelectedValue & " " & _
                        "GROUP BY id_usuario)CapturasP2 ON CapturasP2.id_usuario = RE.id_usuario  " & _
                        "WHERE orden= '" & cmbPeriodo.SelectedValue & "' " & _
                        " " + RegionSQL + PromotorSQL + " " & _
                        " GROUP BY RE.id_usuario,CapturasP1.Capturas,CapturasP2.Capturas,RE.orden,nombre_region "

                Case Is = "Q1"
                    SQLReg = "SELECT nombre_region,COUNT(RE.id_usuario) AS Tiendas, " & _
                        "ISNULL(CapturasP1.Capturas,0) as CapturasP1, " & _
                        "((100*(ISNULL(CapturasP1.Capturas,0)))/COUNT(RE.id_usuario))Totales " & _
                        "FROM View_RE_Mayoreo as RE  " & _
                        "FULL JOIN (SELECT id_region, COUNT(estatus) AS Capturas  " & _
                        "FROM View_RE_Mayoreo as RE " & _
                        "WHERE estatus=1 AND id_quincena='Q1' AND orden=" & cmbPeriodo.SelectedValue & " " & _
                        "GROUP BY id_region)CapturasP1 ON CapturasP1.id_region = RE.id_region " & _
                        "WHERE orden= '" & cmbPeriodo.SelectedValue & "' " & _
                        " GROUP BY nombre_region,CapturasP1.Capturas "
                    SQLDet = "SELECT nombre_region as 'Región',RE.id_usuario as'Promotor', COUNT(RE.id_usuario) AS Tiendas, " & _
                        "ISNULL(CapturasP1.Capturas,0) as 'Q1', " & _
                        "100*(ISNULL(CapturasP1.Capturas,0))/(COUNT(RE.id_usuario)) as '% Captura' " & _
                        "FROM View_RE_Mayoreo as RE  " & _
                        "FULL JOIN (SELECT id_usuario, COUNT(estatus) AS Capturas  " & _
                        "FROM View_RE_Mayoreo WHERE estatus=1 AND id_quincena='Q1' AND orden=" & cmbPeriodo.SelectedValue & " " & _
                        "GROUP BY id_usuario)CapturasP1 ON CapturasP1.id_usuario = RE.id_usuario  " & _
                        "WHERE orden= '" & cmbPeriodo.SelectedValue & "' " & _
                        " " + RegionSQL + PromotorSQL + " " & _
                        " GROUP BY RE.id_usuario,CapturasP1.Capturas,RE.orden, nombre_region "

                Case Is = "Q2"
                    SQLReg = "SELECT nombre_region,COUNT(RE.id_usuario) AS Tiendas, " & _
                        "ISNULL(CapturasP1.Capturas,0) as CapturasP1, " & _
                        "((100*(ISNULL(CapturasP1.Capturas,0)))/COUNT(RE.id_usuario))Totales " & _
                        "FROM View_RE_Mayoreo as RE  " & _
                        "FULL JOIN (SELECT id_region, COUNT(estatus) AS Capturas  " & _
                        "FROM View_RE_Mayoreo as RE " & _
                        "WHERE estatus=1 AND id_quincena='Q2' AND orden=" & cmbPeriodo.SelectedValue & " " & _
                        "GROUP BY id_region)CapturasP1 ON CapturasP1.id_region = RE.id_region " & _
                        "WHERE orden= '" & cmbPeriodo.SelectedValue & "' " & _
                        " GROUP BY nombre_region,CapturasP1.Capturas "
                    SQLDet = "SELECT nombre_region as 'Región',RE.id_usuario as'Promotor', COUNT(RE.id_usuario) AS Tiendas, " & _
                        "ISNULL(CapturasP1.Capturas,0) as 'Q2', " & _
                        "100*(ISNULL(CapturasP1.Capturas,0))/(COUNT(RE.id_usuario)) as '% Captura' " & _
                        "FROM View_RE_Mayoreo as RE  " & _
                        "FULL JOIN (SELECT id_usuario, COUNT(estatus) AS Capturas  " & _
                        "FROM View_RE_Mayoreo WHERE estatus=1 AND id_quincena='Q2' AND orden=" & cmbPeriodo.SelectedValue & " " & _
                        "GROUP BY id_usuario)CapturasP1 ON CapturasP1.id_usuario = RE.id_usuario  " & _
                        "WHERE orden= '" & cmbPeriodo.SelectedValue & "' " & _
                        " " + RegionSQL + PromotorSQL + " " & _
                        " GROUP BY RE.id_usuario,CapturasP1.Capturas,RE.orden, nombre_region "

            End Select

            CargaGrilla(ConexionMars.localSqlServer, SQLReg, gridTotal)
            CargaGrilla(ConexionMars.localSqlServer, SQLDet, gridReporte)
        Else
            gridTotal.Visible = False
            gridReporte.Visible = False
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

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Suma(0) = Suma(0) + e.Row.Cells(3).Text ''Suma Tiendas
            Suma(1) = Suma(1) + e.Row.Cells(4).Text ''Suma Q1

            Select cmbQuincena.SelectedValue
                Case Is = ""
                    Suma(2) = Suma(2) + e.Row.Cells(5).Text ''Suma Q2
                    Select Case e.Row.Cells(6).Text
                        Case Is = 0
                            e.Row.Cells(6).BackColor = Drawing.Color.Red
                        Case Is = 100
                            e.Row.Cells(6).BackColor = Drawing.Color.GreenYellow
                        Case Is < 99
                            e.Row.Cells(6).BackColor = Drawing.Color.Orange
                    End Select
                    e.Row.Cells(6).Text = e.Row.Cells(6).Text & "%"

                Case Is <> ""
                    Select Case e.Row.Cells(5).Text
                        Case Is = 0
                            e.Row.Cells(5).BackColor = Drawing.Color.Red
                        Case Is = 100
                            e.Row.Cells(5).BackColor = Drawing.Color.GreenYellow
                        Case Is < 99
                            e.Row.Cells(5).BackColor = Drawing.Color.Orange
                    End Select
                    e.Row.Cells(5).Text = e.Row.Cells(5).Text & "%"
            End Select
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(3).Text = Suma(0)
            e.Row.Cells(4).Text = Suma(1)

            Select cmbQuincena.SelectedValue
                Case Is = ""
                    e.Row.Cells(5).Text = Suma(2)
                    e.Row.Cells(6).Text = FormatPercent((Suma(1) + Suma(2)) / Suma(0), 2, 0, 0, 0)
                Case Is <> ""
                    e.Row.Cells(5).Text = FormatPercent(Suma(1) / Suma(0), 2, 0, 0, 0)
            End Select
        End If
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, QuincenaSQL, "nombre_quincena", "id_quincena", cmbQuincena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Private Sub cmbQuincena_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbQuincena.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarReporte()
    End Sub

    Sub CargarDetalle(ByVal SeleccionIDUsuario As String)
        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT RE.id_usuario,CAD.nombre_cadena,TI.nombre,RE.id_quincena, " & _
                    "CASE ISNULL(RE.estatus,0) when 1 then 'Capturada' when 2 then 'Incompleta' else 'Sin captura' end as Estatus " & _
                    "FROM Mayoreo_Rutas_Eventos as RE " & _
                    "INNER JOIN Mayoreo_Tiendas as TI ON TI.id_tienda= RE.id_tienda " & _
                    "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region " & _
                    "INNER JOIN Cadenas_Mayoreo as CAD ON CAD.id_cadena =  TI.id_cadena " & _
                    "WHERE RE.orden=" & cmbPeriodo.SelectedValue & " " & _
                    "AND RE.id_usuario = '" & SeleccionIDUsuario & "' " & _
                    "ORDER BY RE.id_usuario, TI.nombre", Me.gridDetalle)
    End Sub

    Sub CargarDetalleGenerales()
        RegionSQL = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)
        PromotorSQL = Acciones.Slc.cmb("RE.id_usuario", cmbPromotor.SelectedValue)

        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT RE.id_usuario,CAD.nombre_cadena,TI.nombre,RE.id_quincena," & _
                    "CASE ISNULL(RE.estatus,0) when 1 then 'Capturada' else 'Sin captura' end as Estatus " & _
                    "FROM Mayoreo_Rutas_Eventos as RE " & _
                    "INNER JOIN Mayoreo_Tiendas as TI ON TI.id_tienda= RE.id_tienda " & _
                    "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region " & _
                    "INNER JOIN Cadenas_Mayoreo as CAD ON CAD.id_cadena =  TI.id_cadena " & _
                    "WHERE RE.orden=" & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + PromotorSQL + " " & _
                    "AND RE.estatus<>1  " & _
                    "ORDER BY RE.id_usuario, TI.nombre", Me.gridDetalle)
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
            Select Case e.Row.Cells(4).Text
                Case "Sin captura":e.Row.Cells(4).ForeColor = Drawing.Color.Red
                Case "Capturada":e.Row.Cells(4).ForeColor = Drawing.Color.Green
            End Select
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
            Select e.Row.Cells(1).Text
                Case Is = 0 : e.Row.Cells(1).BackColor = Drawing.Color.Red
                Case Is = 100 : e.Row.Cells(1).BackColor = Drawing.Color.GreenYellow
                Case Is <> 100 : e.Row.Cells(1).BackColor = Drawing.Color.Orange
            End Select
            e.Row.Cells(1).Text = e.Row.Cells(1).Text + "%"
        End If
    End Sub
End Class