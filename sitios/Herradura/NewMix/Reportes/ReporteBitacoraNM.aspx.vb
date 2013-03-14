Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteBitacoraNM
    Inherits System.Web.UI.Page

    Dim PeriodoSQL, RegionSQL As String
    Dim Suma(3) As Integer

    Sub SQLCombo()
        PeriodoSQL = "SELECT id_periodo, nombre_periodo " & _
                         "FROM NewMix_Periodos " & _
                         " ORDER BY id_periodo DESC"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Regiones as REG WHERE REG.id_region<>0 " & _
                    " ORDER BY REG.nombre_region"
    End Sub

    Sub CargarReporte()
        If cmbPeriodo.SelectedValue <> "" Then

            RegionSQL = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)

            CargaGrilla(ConexionHerradura.localSqlServer, _
                        "SELECT REG.nombre_region,RE.id_usuario, COUNT(RE.id_usuario) AS Tiendas," & _
                        "ISNULL(Capturas.Capturas,0) as Capturas, " & _
                        "ISNULL(Incompletas.Incompletas,0) as Incompletas, " & _
                        "CASE WHEN ISNULL(Capturas.Capturas,0)=0 then 0 else " & _
                        "(100*ISNULL(Capturas.Capturas,0))/COUNT(RE.id_usuario)end Porcentaje " & _
                        "FROM NewMix_Rutas_Eventos as RE " & _
                        "INNER JOIN NewMix_Tiendas as TI ON TI.id_tienda= RE.id_tienda " & _
                        "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region " & _
                        "FULL JOIN (SELECT id_usuario, COUNT(RE.estatus) AS Capturas " & _
                        "FROM NewMix_Rutas_Eventos as RE WHERE estatus=1 AND id_periodo=" & cmbPeriodo.SelectedValue & " GROUP BY id_usuario)Capturas " & _
                        "ON Capturas.id_usuario = RE.id_usuario " & _
                        "FULL JOIN (SELECT id_usuario, COUNT(RE.estatus) AS Incompletas " & _
                        "FROM NewMix_Rutas_Eventos as RE WHERE estatus=2 AND id_periodo=" & cmbPeriodo.SelectedValue & " GROUP BY id_usuario)Incompletas " & _
                        "ON Incompletas.id_usuario = RE.id_usuario " & _
                        "WHERE RE.id_periodo=" & cmbPeriodo.SelectedValue & "" & _
                        " " + RegionSQL + " " & _
                        " GROUP BY RE.id_usuario,Capturas,Incompletas,RE.id_periodo, REG.nombre_region", Me.gridReporte)
        Else
            gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)

        End If
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        CargarReporte()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Suma(0) = Suma(0) + e.Row.Cells(2).Text ''Suma Tiendas
            Suma(1) = Suma(1) + e.Row.Cells(3).Text ''Suma Capturas
            Suma(2) = Suma(2) + e.Row.Cells(4).Text ''Suma Incompletas

            If e.Row.Cells(2).Text > e.Row.Cells(3).Text Then
                e.Row.Cells(5).BackColor = Drawing.Color.Red : Else
                e.Row.Cells(5).BackColor = Drawing.Color.GreenYellow : End If

            If e.Row.Cells(5).Text > 0 And e.Row.Cells(5).Text < 100 Then
                e.Row.Cells(5).BackColor = Drawing.Color.Orange : End If

            e.Row.Cells(5).Text = e.Row.Cells(5).Text + "%"
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(2).Text = Suma(0)
            e.Row.Cells(3).Text = Suma(1)
            e.Row.Cells(4).Text = Suma(2)
            e.Row.Cells(5).Text = FormatPercent(Suma(1) / Suma(0), 0, 0, 0, 0)
        End If

    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionHerradura.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)

        CargarReporte()
    End Sub

    Sub CargarDetalle(ByVal SeleccionIDUsuario As String)
        CargaGrilla(ConexionHerradura.localSqlServer, "SELECT RE.id_usuario,CAD.nombre_cadena,TI.nombre, U.nombre, " & _
                                "CASE RE.estatus when 2 then 'Incompleta' when 1 then 'Completa' when 0 then 'Sin captura' end as Estatus " & _
                                "FROM NewMix_Rutas_Eventos as RE " & _
                                "INNER JOIN NewMix_Tiendas as TI ON TI.id_tienda= RE.id_tienda " & _
                                "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region " & _
                                "INNER JOIN Cadenas_tiendas as CAD ON CAD.id_cadena =  TI.id_cadena " & _
                                "INNER JOIN Usuarios as U ON U.id_usuario = RE.id_usuario " & _
                                "WHERE RE.id_periodo = '" & cmbPeriodo.SelectedValue & "'" & _
                                "AND RE.id_usuario = '" & SeleccionIDUsuario & "' " & _
                                " ORDER BY RE.id_usuario, TI.nombre", Me.gridDetalle)
    End Sub

    Sub CargarDetalleGenerales()
        CargaGrilla(ConexionHerradura.localSqlServer,"SELECT RE.id_usuario,CAD.nombre_cadena,TI.nombre, " & _
                                "CASE RE.estatus when 2 then 'Incompleta' when 1 then 'Completa' when 0 then 'Sin Captura' end as Estatus " & _
                                "FROM NewMix_Rutas_Eventos as RE " & _
                                "INNER JOIN NewMix_Tiendas as TI ON TI.id_tienda= RE.id_tienda " & _
                                "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region " & _
                                "INNER JOIN Cadenas_tiendas as CAD ON CAD.id_cadena =  TI.id_cadena " & _
                                "WHERE RE.id_periodo=" & cmbPeriodo.Text & " " & _
                                "ORDER BY RE.id_usuario, TI.nombre",Me.gridDetalle)
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
                If e.Row.Cells(4).Text = "Sin captura" Then
                    e.Row.Cells(0).ForeColor = Drawing.Color.Red
                    e.Row.Cells(1).ForeColor = Drawing.Color.Red
                    e.Row.Cells(2).ForeColor = Drawing.Color.Red
                    e.Row.Cells(3).ForeColor = Drawing.Color.Red
                    e.Row.Cells(4).ForeColor = Drawing.Color.Red
                End If

                If e.Row.Cells(4).Text = "Completa" Then
                    e.Row.Cells(0).ForeColor = Drawing.Color.Green
                    e.Row.Cells(1).ForeColor = Drawing.Color.Green
                    e.Row.Cells(2).ForeColor = Drawing.Color.Green
                    e.Row.Cells(3).ForeColor = Drawing.Color.Green
                    e.Row.Cells(4).ForeColor = Drawing.Color.Green
                End If

                If e.Row.Cells(4).Text = "Incompleta" Then
                    e.Row.Cells(0).ForeColor = Drawing.Color.OrangeRed
                    e.Row.Cells(1).ForeColor = Drawing.Color.OrangeRed
                    e.Row.Cells(2).ForeColor = Drawing.Color.OrangeRed
                    e.Row.Cells(3).ForeColor = Drawing.Color.OrangeRed
                    e.Row.Cells(4).ForeColor = Drawing.Color.OrangeRed
                End If
            Next i
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Bitácora Captura Promotores " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

End Class