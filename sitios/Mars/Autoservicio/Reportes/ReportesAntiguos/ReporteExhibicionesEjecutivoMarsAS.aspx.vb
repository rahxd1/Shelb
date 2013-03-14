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

Partial Public Class ReporteExhibicionesEjecutivoMarsAS
    Inherits System.Web.UI.Page

    Dim Suma(3) As Integer

    Sub SQLCombo()
        MarsAS.SQLsCombo(cmbPeriodo.SelectedValue, cmbQuincena.SelectedValue, _
                          cmbRegion.SelectedValue, cmbEjecutivo.SelectedValue, "", _
                         "", cmbCadena.SelectedValue, "View_Historial_AS")
    End Sub

    Sub CargarReporte()
        Dim QuincenaSQL, RegionSQL, EjecutivoSQL, CadenaSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            QuincenaSQL = Acciones.Slc.cmb("id_quincena", cmbQuincena.SelectedValue)
            RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
            EjecutivoSQL = Acciones.Slc.cmb("REL.region_mars", cmbEjecutivo.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("TI.id_cadena", cmbCadena.SelectedValue)

            CargaGrilla(ConexionMars.localSqlServer, _
                        "select RE.id_periodo,RE.nombre_region,RE.region_mars,RE.region_mars + '- '+ RE.nombre as EjecutivoMars, " & _
                        "COUNT(DISTINCT RE.id_tienda)Tiendas,COUNT(DISTINCT Captura.id_tienda)Pautas, " & _
                        "ISNULL(SUM(PVI.Cumplimiento),0)PVI,ISNULL((100*ISNULL(SUM(PVI.Cumplimiento),0)/COUNT(RE.id_tienda)),0)Porcentaje " & _
                        "FROM (select DISTINCT id_periodo, US.nombre, RE.id_tienda, REG.nombre_region, REL.region_mars  " & _
                        "from AS_Rutas_Eventos as RE " & _
                        "INNER JOIN AS_Tiendas as TI ON TI.id_tienda = RE.id_tienda  " & _
                        "INNER JOIN Usuarios_Relacion AS REL ON REL.id_usuario = RE.id_usuario " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario = REL.region_mars  " & _
                        "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                        "where id_periodo ='" & cmbPeriodo.SelectedValue & "' " + QuincenaSQL + RegionSQL + EjecutivoSQL + CadenaSQL + Promotor + ")RE " & _
                        "FULL JOIN (select DISTINCT RE.id_periodo, RE.id_tienda " & _
                        "FROM AS_Rutas_Eventos as RE " & _
                        "INNER JOIN AS_Tiendas as TI ON TI.id_tienda = RE.id_tienda " & _
                        "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                        "INNER JOIN Usuarios_Relacion AS REL ON REL.id_usuario = RE.id_usuario " & _
                        "WHERE id_periodo ='" & cmbPeriodo.SelectedValue & "' " + QuincenaSQL + RegionSQL + EjecutivoSQL + CadenaSQL + Promotor + " AND TI.id_clasificacion<>0 AND TI.id_clasificacion<>2)Captura " & _
                        "ON Captura.id_tienda = RE.id_tienda AND Captura.id_periodo=RE.id_periodo " & _
                        "FULL JOIN (SELECT DISTINCT RE.id_tienda,HDET.Cumplimiento, RE.id_periodo " & _
                        "FROM AS_Rutas_Eventos as RE " & _
                        "INNER JOIN AS_Tiendas as TI ON RE.id_tienda = TI.id_tienda " & _
                        "INNER JOIN Usuarios_Relacion AS REL ON REL.id_usuario = RE.id_usuario " & _
                        "FULL JOIN (select DISTINCT H.id_periodo, H.id_tienda, " & _
                        "(CASE when(CASE when CTI.id_clasificacion=1 OR CTI.id_clasificacion=6 OR CTI.id_clasificacion=4 " & _
                        "then (CASE when((HDET.[1])+(HDET.[2])+(HDET.[3])+(HDET.[4])+(HDET.[5])+(HDET.[6])+((HDET.[7]))+(HDET.[8]))>=CTI.O_1 then 1 else 0 end) " & _
                        "when CTI.id_clasificacion=3 OR CTI.id_clasificacion=5 " & _
                        "then (CASE when((HDET.[1])+(HDET.[2])+(HDET.[3])+(HDET.[4])+(HDET.[5]))>=CTI.O_1 then 1 else 0 end)end)+ " & _
                        "(CASE when CTI.id_clasificacion=3 OR CTI.id_clasificacion=5 " & _
                        "then (CASE when((HDET.[6])+((HDET.[7]))+(HDET.[8]))>=CTI.O_6 then 1 else 0 end)else 0 end)+ " & _
                        "(CASE WHEN CTI.id_clasificacion<>4 then(CASE when TI.id_cadena=7 " & _
                        "then 1 else (CASE when (HDET.[10])>=CTI.O_10 then 1 else 0 end)end)else 0 end)+ " & _
                        "(CASE when (HDET.[12])>=CTI.O_12 then 1 else 0 end)+ " & _
                        "(CASE WHEN CTI.id_clasificacion=4 then (CASE when ((HDET.[9])+(HDET.[11]))>=CTI.O_9 then 1 else 0 end) " & _
                        "else (CASE WHEN CTI.O_9<>0 then(CASE when (HDET.[9])>=CTI.O_9 then 1 else 0 end)else 0 end)+ " & _
                        "(CASE when CTI.id_clasificacion=3 OR CTI.id_clasificacion=6 " & _
                        "THEN (CASE WHEN CTI.O_11<>0 then(CASE when (HDET.[11])>=CTI.O_11 then 1 else 0 end)else 0 end)else 0 end)end)>= CTI.Total then 1 else 0 end)Cumplimiento " & _
                        "FROM AS_Historial as H " & _
                        "INNER JOIN Usuarios_Relacion AS REL ON REL.id_usuario = H.id_usuario " & _
                        "INNER JOIN AS_Tiendas as TI ON H.id_tienda = TI.id_tienda " & _
                        "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                        "INNER JOIN AS_Clasificacion_Tiendas AS CTI ON CTI.id_clasificacion = TI.id_clasificacion   " & _
                        "INNER JOIN AS_Exhibidores_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial   " & _
                        "WHERE H.id_periodo ='" & cmbPeriodo.SelectedValue & "' " + QuincenaSQL + RegionSQL + EjecutivoSQL + CadenaSQL + Promotor + ")HDET " & _
                        "ON HDET.id_tienda = RE.id_tienda " & _
                        "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                        "WHERE RE.id_periodo ='" & cmbPeriodo.SelectedValue & "' " + QuincenaSQL + RegionSQL + EjecutivoSQL + CadenaSQL + Promotor + " " & _
                        "AND Cumplimiento=1 AND TI.id_clasificacion<>0 AND TI.id_clasificacion<>2)PVI   " & _
                        "ON RE.id_tienda = PVI.id_tienda AND PVI.id_periodo = RE.id_periodo " & _
                        "WHERE RE.id_periodo ='" & cmbPeriodo.SelectedValue & "' " & _
                        "GROUP BY RE.id_periodo,RE.region_mars,RE.nombre,RE.nombre_region " & _
                        "ORDER BY RE.region_mars ", gridReporte)
        Else
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
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

            cmbPeriodo.SelectedValue = Request.Params("id_periodo")
            cmbEjecutivo.SelectedValue = Request.Params("region_mars")

            If Request.Params("id_periodo") <> "" Then
                SQLCombo()
                Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
                Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
                Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

                CargarReporte()
            End If
        End If
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Private Sub gridReporte_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gridReporte.RowCommand
        If e.CommandName = "update-something" Then
            gridReporte.SelectedIndex = Convert.ToInt32(e.CommandArgument)
        End If
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Suma(0) = Suma(0) + e.Row.Cells(2).Text
            Suma(1) = Suma(1) + e.Row.Cells(3).Text

            If Not e.Row.Cells(4).Text = "&nbsp;" Then
                Suma(2) = Suma(2) + e.Row.Cells(4).Text : End If

            e.Row.Cells(5).Text = e.Row.Cells(5).Text & "%"
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(2).Text = Suma(0)
            e.Row.Cells(3).Text = Suma(1)
            e.Row.Cells(4).Text = Suma(2)
            e.Row.Cells(5).Text = FormatPercent(Suma(2) / Suma(0), 0, 0, 0, 0)
        End If
    End Sub

    Protected Sub cmbQuincena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbQuincena.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Comentarios " + cmbPeriodo.SelectedItem.ToString() + " " + cmbQuincena.SelectedItem.ToString() + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbEjecutivo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEjecutivo.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Protected Sub lnkVerTodo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkVerTodo.Click
        Response.Redirect("~/sitios/Mars/Autoservicio/Reportes/ReportesAntiguos/ReporteDetalleExhibicionesEjecutivoMarsAS.aspx?id_periodo=" & cmbPeriodo.SelectedValue & "&region_mars=" & cmbEjecutivo.SelectedValue & "&id_quincena=" & cmbQuincena.SelectedValue & "&id_region=" & cmbRegion.SelectedValue & "")
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCadena.SelectedIndexChanged
        CargarReporte()
    End Sub
End Class