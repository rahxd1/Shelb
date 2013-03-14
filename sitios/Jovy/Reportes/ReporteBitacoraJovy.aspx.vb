Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteBitacoraJovy
    Inherits System.Web.UI.Page

    Dim Suma(3) As Integer

    Sub SQLCombo()
        Variables_Jovy.SQLsCombo(cmbPeriodo.SelectedValue, cmbRegion.SelectedValue, _
                     cmbPromotor.SelectedValue, "")
    End Sub

    Sub CargarReporte()
        Dim PromotorSQL, RegionSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("RE.id_region", cmbRegion.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("RE.id_usuario", cmbPromotor.SelectedValue)

            CargaGrilla(ConexionJovy.localSqlServer, _
                        "SELECT RE.nombre_region,SUM(ISNULL(Tiendas.Tiendas,0))Tiendas, " & _
                        "SUM(ISNULL(Capturas.Capturas,0))Capturas, " & _
                        "CASE WHEN SUM(ISNULL(Tiendas.Tiendas,0))=0 then (CASE WHEN SUM(ISNULL(Capturas.Capturas,0))<0 then 100 else 0 end) " & _
                        "else (100*SUM((ISNULL(Capturas.Capturas,0))))/(SUM(ISNULL(Tiendas.Tiendas,0))) end Porcentaje " & _
                        "FROM (SELECT DISTINCT RE.id_usuario, nombre_region, id_region, id_periodo " & _
                        "FROM Jovy_Rutas_Eventos as RE " & _
                        "INNER JOIN (select DISTINCT REG.nombre_region,REG.id_region, RUT.id_usuario " & _
                        "FROM Jovy_CatRutas as RUT " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario=RUT.id_usuario " & _
                        "INNER JOIN Regiones as REG ON REG.id_region=US.id_region)as REG ON REG.id_usuario=RE.id_usuario)as RE  " & _
                        "INNER JOIN (SELECT id_usuario,COUNT(DISTINCT id_tienda)Tiendas " & _
                        "FROM Jovy_Rutas_Eventos as RE  " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " AND id_tienda<>0 GROUP BY id_usuario)Tiendas ON Tiendas.id_usuario = RE.id_usuario " & _
                        "FULL JOIN (SELECT id_usuario, COUNT(RE.estatus) AS Capturas  " & _
                        "FROM Jovy_Rutas_Eventos as RE  " & _
                        "WHERE estatus=1 AND id_periodo=" & cmbPeriodo.SelectedValue & " GROUP BY id_usuario)Capturas ON Capturas.id_usuario = RE.id_usuario  " & _
                        "WHERE RE.id_periodo=" & cmbPeriodo.SelectedValue & "" + RegionSQL + " " & _
                        "GROUP BY RE.nombre_region ", _
                        Me.gridResumen)

            CargaGrilla(ConexionJovy.localSqlServer, _
                        "SELECT RE.nombre_region,RE.id_usuario,ISNULL(Tiendas.Tiendas,0)Tiendas, " & _
                        "ISNULL(Capturas.Capturas,0)Capturas, " & _
                        "CASE WHEN ISNULL(Tiendas.Tiendas,0)=0 then (CASE WHEN ISNULL(Capturas.Capturas,0)<0 then 100 else 0 end) " & _
                        "else (100*(ISNULL(Capturas.Capturas,0)))/(ISNULL(Tiendas.Tiendas,0)) end Porcentaje " & _
                        "FROM (SELECT DISTINCT RE.id_usuario, nombre_region, id_region,id_periodo " & _
                        "FROM Jovy_Rutas_Eventos as RE " & _
                        "INNER JOIN (select DISTINCT REG.nombre_region,REG.id_region, RUT.id_usuario " & _
                        "FROM Jovy_CatRutas as RUT " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario=RUT.id_usuario " & _
                        "INNER JOIN Regiones as REG ON REG.id_region=US.id_region)as REG ON REG.id_usuario=RE.id_usuario)as RE  " & _
                        "FULL JOIN (SELECT id_usuario,COUNT(DISTINCT id_tienda)Tiendas " & _
                        "FROM Jovy_Rutas_Eventos as RE  " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " AND id_tienda<>0 GROUP BY id_usuario)Tiendas ON Tiendas.id_usuario = RE.id_usuario " & _
                        "FULL JOIN (SELECT id_usuario, COUNT(RE.estatus) AS Capturas  " & _
                        "FROM Jovy_Rutas_Eventos as RE  " & _
                        "WHERE estatus=1 AND id_periodo=" & cmbPeriodo.SelectedValue & " GROUP BY id_usuario)Capturas ON Capturas.id_usuario = RE.id_usuario  " & _
                        "WHERE RE.id_periodo=" & cmbPeriodo.SelectedValue & "" + RegionSQL + PromotorSQL + " ", _
                        Me.gridReporte)
        Else
            gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.PeriodoSQLCmb, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

            CargarReporte()
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Protected Sub lnkExportar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.pnlGrid.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.pnlGrid)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Bitácora captura " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Suma(0) = Suma(0) + e.Row.Cells(2).Text ''Suma Tiendas
            Suma(1) = Suma(1) + e.Row.Cells(3).Text ''Suma Capturas

            If e.Row.Cells(4).Text = 0 Then
                e.Row.Cells(4).BackColor = Drawing.Color.Red : Else
                e.Row.Cells(4).BackColor = Drawing.Color.GreenYellow : End If

            If e.Row.Cells(4).Text > 0 And e.Row.Cells(4).Text < 100 Then
                e.Row.Cells(4).BackColor = Drawing.Color.Orange : End If

            e.Row.Cells(4).Text = e.Row.Cells(4).Text + "%"
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.ForeColor = Drawing.Color.White
            e.Row.Cells(2).Text = Suma(0)
            e.Row.Cells(3).Text = Suma(1)
            e.Row.Cells(4).Text = FormatPercent(Suma(1) / Suma(0), 0, 0, 0, 0)
        End If
    End Sub

    Private Sub gridResumen_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridResumen.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(1).Text = 0 Then
                e.Row.Cells(1).BackColor = Drawing.Color.Red : Else
                e.Row.Cells(1).BackColor = Drawing.Color.GreenYellow : End If

            If e.Row.Cells(1).Text > 0 And e.Row.Cells(1).Text < 100 Then
                e.Row.Cells(1).BackColor = Drawing.Color.Orange : End If

            e.Row.Cells(1).Text = e.Row.Cells(1).Text + "%"
        End If

        Dim RegionSQL As String
        If cmbRegion.SelectedValue = "" Then
            RegionSQL = "" : Else
            RegionSQL = "AND RE.id_region=" & cmbRegion.SelectedValue & " " : End If

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                               "SELECT CASE WHEN SUM(ISNULL(Tiendas.Tiendas,0))=0 then (CASE WHEN SUM(ISNULL(Capturas.Capturas,0))<0 then 100 else 0 end) " & _
                        "else (100*SUM((ISNULL(Capturas.Capturas,0))))/(SUM(ISNULL(Tiendas.Tiendas,0))) end Porcentaje " & _
                        "FROM (SELECT DISTINCT RE.id_usuario, nombre_region, id_region, id_periodo " & _
                        "FROM Jovy_Rutas_Eventos as RE " & _
                        "INNER JOIN (select DISTINCT REG.nombre_region,REG.id_region, RUT.id_usuario " & _
                        "FROM Jovy_CatRutas as RUT " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario=RUT.id_usuario " & _
                        "INNER JOIN Regiones as REG ON REG.id_region=US.id_region)as REG ON REG.id_usuario=RE.id_usuario)as RE  " & _
                        "INNER JOIN (SELECT id_usuario,COUNT(DISTINCT id_tienda)Tiendas " & _
                        "FROM Jovy_Rutas_Eventos as RE  " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " AND id_tienda<>0 GROUP BY id_usuario)Tiendas ON Tiendas.id_usuario = RE.id_usuario " & _
                        "FULL JOIN (SELECT id_usuario, COUNT(RE.estatus) AS Capturas  " & _
                        "FROM Jovy_Rutas_Eventos as RE  " & _
                        "WHERE estatus=1 AND id_periodo=" & cmbPeriodo.SelectedValue & " GROUP BY id_usuario)Capturas ON Capturas.id_usuario = RE.id_usuario  " & _
                        "WHERE RE.id_periodo=" & cmbPeriodo.SelectedValue & "" + RegionSQL + " ")

        If e.Row.RowType = DataControlRowType.Footer Then
            If Tabla.Rows.Count > 0 Then
                e.Row.Cells(1).Text = Tabla.Rows(0)("Porcentaje") & "%"
            End If
        End If

        Tabla.Dispose()
    End Sub
End Class