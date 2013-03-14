Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteBitacoraNR
    Inherits System.Web.UI.Page

    Dim RegionSel, PeriodoSQL, RegionSQL, SupervisorSQL As String
    Dim Suma(6) As Integer

    Sub SQLCombo()
        RegionSel = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)

        PeriodoSQL = "SELECT id_periodo, nombre_periodo " & _
                         "FROM NR_Periodos " & _
                         " ORDER BY id_periodo DESC"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Regiones as REG WHERE REG.id_region<>0 " & _
                    " ORDER BY REG.nombre_region"

        SupervisorSQL = "SELECT DISTINCT REL.id_supervisor, REL.id_supervisor+' - '+US.nombre as Supervisor " & _
                    "FROM Usuarios_Relacion as REL " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario=REL.id_supervisor " & _
                    "WHERE REL.id_supervisor<>'' " + RegionSel + " " & _
                    "ORDER BY REL.id_supervisor"
    End Sub

    Sub CargarReporte()
        pnlDetalle.Visible = False

        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("RE.id_region", cmbRegion.SelectedValue)
            SupervisorSQL = Acciones.Slc.cmb("RE.id_supervisor", cmbSupervisor.SelectedValue)

            If Tipo_usuario = 7 Then
                Berol.CuentaClave = "INNER JOIN (select * FROM Usuarios_cadenas WHERE id_usuario='" & HttpContext.Current.User.Identity.Name & "' )as CC " & _
                                "ON CC.id_cadena=TI.id_cadena " : Else
                Berol.CuentaClave = "" : End If

            CargaGrilla(ConexionBerol.localSqlServer, _
                        "select RE.nombre_region,RE.id_usuario, RE.Tiendas,Frentes.PorFrentes, Inventarios.PorInventarios, " & _
                        "Fotos.PorFotos " & _
                        "FROM (select id_periodo,nombre_region,id_region,id_usuario,id_supervisor,COUNT(id_usuario)Tiendas " & _
                        "FROM View_NR_Rutas_Eventos " & _
                        " " + Berol.CuentaClave + " " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & "  " & _
                        "GROUP BY id_periodo,nombre_region,id_region,id_supervisor,id_usuario) as RE  " & _
                        "INNER JOIN (SELECT id_usuario, COUNT(id_tienda)Tiendas,  " & _
                        "CASE WHEN SUM(estatus_frentes)>COUNT(id_tienda) then '-' " & _
                        "else convert(nvarchar(5),SUM(estatus_frentes)) end Capturas, " & _
                        "CASE WHEN SUM(estatus_frentes)>COUNT(id_tienda) then '-' " & _
                        "else convert(nvarchar(5),100*SUM(estatus_frentes)/COUNT(id_tienda)) end PorFrentes " & _
                        "FROM NR_Rutas_Eventos  " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " GROUP BY id_usuario)Frentes ON Frentes.id_usuario=RE.id_usuario " & _
                        "INNER JOIN (SELECT id_usuario, COUNT(id_tienda)Tiendas,  " & _
                        "CASE WHEN SUM(estatus_inventarios)>COUNT(id_tienda) then '-' " & _
                        "else convert(nvarchar(5),SUM(estatus_inventarios)) end Capturas, " & _
                        "CASE WHEN SUM(estatus_inventarios)>COUNT(id_tienda) then '-' " & _
                        "else convert(nvarchar(5),100*SUM(estatus_inventarios)/COUNT(id_tienda)) end PorInventarios " & _
                        "FROM NR_Rutas_Eventos  " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " GROUP BY id_usuario)Inventarios ON Inventarios.id_usuario=RE.id_usuario " & _
                        "INNER JOIN (SELECT id_usuario, COUNT(id_tienda)Tiendas,  " & _
                        "CASE WHEN SUM(estatus_fotos)>COUNT(id_tienda) then '-' " & _
                        "else convert(nvarchar(5),SUM(estatus_fotos)) end Capturas, " & _
                        "CASE WHEN SUM(estatus_fotos)>COUNT(id_tienda) then '-' " & _
                        "else convert(nvarchar(5),100*SUM(estatus_fotos)/COUNT(id_tienda)) end PorFotos " & _
                        "FROM NR_Rutas_Eventos  " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " GROUP BY id_usuario)Fotos ON Fotos.id_usuario=RE.id_usuario " & _
                        "WHERE RE.id_usuario<>'' " & _
                        " " + RegionSQL + SupervisorSQL + " " & _
                        "ORDER BY RE.id_usuario ", gridReporte)

            CargaGrilla(ConexionBerol.localSqlServer, _
                        "select RE.nombre_region, " & _
                        "CASE WHEN 100*SUM(Frentes.Capturas)/SUM(Frentes.Tiendas)>=400 then '-' " & _
                        "else CONVERT(nvarchar(5),100*SUM(Frentes.Capturas)/SUM(Frentes.Tiendas))end Frentes, " & _
                        "CASE WHEN 100*SUM(Inventarios.Capturas)/SUM(Inventarios.Tiendas)>=400 then '-' " & _
                        "else CONVERT(nvarchar(5),100*SUM(Inventarios.Capturas)/SUM(Inventarios.Tiendas))end Inventarios, " & _
                        "CASE WHEN 100*SUM(Fotos.Capturas)/SUM(Fotos.Tiendas)>=400 then '-' " & _
                        "else CONVERT(nvarchar(10),(100*SUM(Fotos.Capturas)/SUM(Fotos.Tiendas)))end Fotos " & _
                        "FROM (select RE.id_periodo,RE.nombre_region,RE.id_region,RE.id_usuario, COUNT(RE.id_usuario)Tiendas " & _
                        "FROM View_NR_Rutas_Eventos as RE " & _
                        " " + Berol.CuentaClave + " " & _
                        "WHERE RE.id_periodo=" & cmbPeriodo.SelectedValue & "  " & _
                        "GROUP BY RE.id_periodo,RE.nombre_region,RE.id_region,RE.id_usuario)RE  " & _
                        "INNER JOIN (SELECT id_region, COUNT(id_tienda)Tiendas, SUM(estatus_frentes)Capturas " & _
                        "FROM View_NR_Rutas_Eventos " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " GROUP BY id_region)Frentes ON Frentes.id_region=RE.id_region " & _
                        "INNER JOIN (SELECT id_region, COUNT(id_tienda)Tiendas,SUM(estatus_inventarios)Capturas " & _
                        "FROM View_NR_Rutas_Eventos as RE " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " GROUP BY id_region)Inventarios ON Inventarios.id_region=RE.id_region " & _
                        "INNER JOIN (SELECT id_region, COUNT(id_tienda)Tiendas,SUM(estatus_fotos)Capturas " & _
                        "FROM View_NR_Rutas_Eventos " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " GROUP BY id_region)Fotos ON Fotos.id_region=RE.id_region " & _
                        "GROUP BY RE.nombre_region ", gridResumen)

        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionBerol.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, SupervisorSQL, "supervisor", "id_supervisor", cmbSupervisor)
        End If
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, SupervisorSQL, "supervisor", "id_supervisor", cmbSupervisor)

        CargarReporte()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Suma(0) = Suma(0) + 1 ''Usuarios
            Suma(1) = Suma(1) + e.Row.Cells(2).Text ''Suma Tiendas

            If e.Row.Cells(3).Text <> "-" Then
                Suma(2) = Suma(2) + e.Row.Cells(3).Text

                If e.Row.Cells(3).Text = "100" Then
                    e.Row.Cells(3).BackColor = Drawing.Color.GreenYellow : Else
                    e.Row.Cells(3).BackColor = Drawing.Color.Orange : End If

                If e.Row.Cells(3).Text = "0" Then
                    e.Row.Cells(3).BackColor = Drawing.Color.Red : End If

                e.Row.Cells(3).Text = e.Row.Cells(3).Text + "%"
            End If

            If e.Row.Cells(4).Text <> "-" Then
                Suma(3) = Suma(3) + e.Row.Cells(4).Text

                If e.Row.Cells(4).Text = "100" Then
                    e.Row.Cells(4).BackColor = Drawing.Color.GreenYellow : Else
                    e.Row.Cells(4).BackColor = Drawing.Color.Orange : End If

                If e.Row.Cells(4).Text = "0" Then
                    e.Row.Cells(4).BackColor = Drawing.Color.Red : End If

                e.Row.Cells(4).Text = e.Row.Cells(4).Text + "%"
            End If

            If e.Row.Cells(5).Text <> "-" Then
                Suma(4) = Suma(4) + e.Row.Cells(5).Text

                If e.Row.Cells(5).Text = "100" Then
                    e.Row.Cells(5).BackColor = Drawing.Color.GreenYellow : Else
                    e.Row.Cells(5).BackColor = Drawing.Color.Orange : End If

                If e.Row.Cells(5).Text = "0" Then
                    e.Row.Cells(5).BackColor = Drawing.Color.Red : End If

                e.Row.Cells(5).Text = e.Row.Cells(5).Text + "%"
            End If
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = Suma(0) & " usuarios"
            e.Row.Cells(2).Text = Suma(1) & " tiendas"
            e.Row.Cells(3).Text = FormatNumber((Suma(2) / Suma(0)), 2) & "%"
            e.Row.Cells(4).Text = FormatNumber((Suma(3) / Suma(0)), 2) & "%"
            e.Row.Cells(5).Text = FormatNumber((Suma(4) / Suma(0)), 2) & "%"
        End If
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, SupervisorSQL, "supervisor", "id_supervisor", cmbSupervisor)

        CargarReporte()
    End Sub

    Sub CargarDetalle(ByVal SeleccionIDUsuario As String)
        CargaGrilla(ConexionBerol.localSqlServer, _
                    "SELECT id_usuario,nombre_cadena,nombre,nombre, " & _
                    "CASE estatus_fotos when 1 then 'CAPTURA' when 0 then 'SIN CAPTURA' end as estatus_fotos, " & _
                    "CASE estatus_frentes when 1 then 'CAPTURA' when 0 then 'SIN CAPTURA' end as estatus_frentes, " & _
                    "CASE estatus_inventarios when 1 then 'CAPTURA' when 0 then 'SIN CAPTURA' end as estatus_inventarios " & _
                    "FROM View_NR_Rutas_Eventos " & _
                    "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                    "AND id_usuario = '" & SeleccionIDUsuario & "' " & _
                    "ORDER BY id_usuario,nombre", Me.gridDetalle)
    End Sub

    Private Sub gridReporte_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gridReporte.RowEditing
        CargarDetalle(gridReporte.Rows(e.NewEditIndex).Cells(1).Text)
        pnlDetalle.Visible = True
    End Sub

    Private Sub gridDetalle_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridDetalle.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(3).Text = "SIN CAPTURA" Then
                e.Row.Cells(0).ForeColor = Drawing.Color.Red
                e.Row.Cells(1).ForeColor = Drawing.Color.Red
                e.Row.Cells(2).ForeColor = Drawing.Color.Red
                e.Row.Cells(3).ForeColor = Drawing.Color.Red
                e.Row.Cells(4).ForeColor = Drawing.Color.Red
            End If

            If e.Row.Cells(3).Text = "INCOMPLETA" Then
                e.Row.Cells(0).ForeColor = Drawing.Color.OrangeRed
                e.Row.Cells(1).ForeColor = Drawing.Color.OrangeRed
                e.Row.Cells(2).ForeColor = Drawing.Color.OrangeRed
                e.Row.Cells(3).ForeColor = Drawing.Color.OrangeRed
                e.Row.Cells(4).ForeColor = Drawing.Color.OrangeRed
            End If
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte bitácora captura promotores " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        CargarReporte()
    End Sub

    Private Sub gridResumen_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridResumen.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(1).Text <> "-" Then
                If e.Row.Cells(1).Text = "100" Then
                    e.Row.Cells(1).BackColor = Drawing.Color.GreenYellow : Else
                    e.Row.Cells(1).BackColor = Drawing.Color.Orange : End If

                If e.Row.Cells(1).Text = "0" Then
                    e.Row.Cells(1).BackColor = Drawing.Color.Red : End If

                e.Row.Cells(1).Text = e.Row.Cells(1).Text + "%"
            End If

            If e.Row.Cells(2).Text <> "-" Then
                If e.Row.Cells(2).Text = "100" Then
                    e.Row.Cells(2).BackColor = Drawing.Color.GreenYellow : Else
                    e.Row.Cells(2).BackColor = Drawing.Color.Orange : End If

                If e.Row.Cells(2).Text = "0" Then
                    e.Row.Cells(2).BackColor = Drawing.Color.Red : End If

                e.Row.Cells(2).Text = e.Row.Cells(2).Text + "%"
            End If

            If e.Row.Cells(3).Text <> "-" Then
                If e.Row.Cells(3).Text = "100" Then
                    e.Row.Cells(3).BackColor = Drawing.Color.GreenYellow : Else
                    e.Row.Cells(3).BackColor = Drawing.Color.Orange : End If

                If e.Row.Cells(3).Text = "0" Then
                    e.Row.Cells(3).BackColor = Drawing.Color.Red : End If

                e.Row.Cells(3).Text = e.Row.Cells(3).Text + "%"
            End If
        End If
    End Sub
End Class