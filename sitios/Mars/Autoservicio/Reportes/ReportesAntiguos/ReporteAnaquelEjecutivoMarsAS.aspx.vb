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

Partial Public Class ReporteAnaquelEjecutivoMarsAS
    Inherits System.Web.UI.Page

    Dim Suma(7) As Integer

    Sub SQLCombo()
        MarsAS.SQLsCombo(cmbPeriodo.SelectedValue, cmbQuincena.SelectedValue, _
                         cmbRegion.SelectedValue, cmbEjecutivo.SelectedValue, "", _
                         "", cmbCadena.SelectedValue, "View_Historial_AS")
    End Sub

    Sub CargarReporte()
        Dim QuincenaSQL, EjecutivoSQL, RegionSQL, CadenaSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            QuincenaSQL = Acciones.Slc.cmb("H.id_quincena", cmbQuincena.SelectedValue)
            RegionSQL = Acciones.Slc.cmb("H.id_region", cmbRegion.SelectedValue)
            EjecutivoSQL = Acciones.Slc.cmb("H.region_mars", cmbEjecutivo.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("H.id_cadena", cmbCadena.SelectedValue)

            CargaGrilla(ConexionMars.localSqlServer, _
                        "select H.orden,H.region_mars,H.Ejecutivo,H.region_mars,H.nombre_region, " & _
                        "CASE when ((SUM(HDET.[1])+SUM(HDET.[15]))+SUM(HDET.[5]))=0 then 0 else " & _
                        "cast(round((SUM(HDET.[1])+SUM(HDET.[15]))/cast(((SUM(HDET.[1])+SUM(HDET.[15]))+SUM(HDET.[5])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end ASS, " & _
                        "CASE when (SUM(HDET.[2])+SUM(HDET.[6]))=0 then 0 else " & _
                        "cast(round(SUM(HDET.[2])/cast((SUM(HDET.[2])+SUM(HDET.[6])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end CS, " & _
                        "CASE when (SUM(HDET.[3])+SUM(HDET.[7]))=0 then 0 else " & _
                        "cast(round(SUM(HDET.[3])/cast((SUM(HDET.[3])+SUM(HDET.[7])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end PH, " & _
                        "CASE when (SUM(HDET.[8])+SUM(HDET.[4]))=0 then 0 else " & _
                        "cast(round(SUM(HDET.[4])/cast((SUM(HDET.[4])+SUM(HDET.[8])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end PB, " & _
                        "CASE when (SUM(HDET.[9])+SUM(HDET.[12]))=0 then 0 else " & _
                        "cast(round(SUM(HDET.[9])/cast((SUM(HDET.[9])+SUM(HDET.[12])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end GS, " & _
                        "CASE when (SUM(HDET.[10])+SUM(HDET.[13]))=0 then 0 else " & _
                        "cast(round(SUM(HDET.[10])/cast((SUM(HDET.[10])+SUM(HDET.[13])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end GH, " & _
                        "CASE when (SUM(HDET.[11])+SUM(HDET.[14]))=0 then 0 else " & _
                        "cast(round(SUM(HDET.[11])/cast((SUM(HDET.[11])+SUM(HDET.[14])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end GB " & _
                        "FROM AS_Segmentos_Historial_Det as HDET " & _
                        "INNER JOIN View_Historial_AS as H ON HDET.folio_historial = H.folio_historial " & _
                        "WHERE H.orden=" & cmbPeriodo.SelectedValue & " " & _
                        " " + QuincenaSQL + " " & _
                        " " + RegionSQL + CadenaSQL + EjecutivoSQL + " " & _
                        "GROUP BY H.orden,H.region_mars,H.Ejecutivo,H.nombre_region " & _
                        "ORDER BY H.region_mars ", gridReporte)
        Else
            gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CargaGrilla(ConexionMars.localSqlServer, "select * from Ver_Area_nielsen", gridAreaNielsen)

            SQLCombo()
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PeriodoSQLCmb, "nombre_periodo", "orden", cmbPeriodo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.QuincenaSQLCmb, "nombre_quincena", "id_quincena", cmbQuincena)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
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

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 0 To 6
                Suma(i) = Suma(i) + e.Row.Cells(i + 2).Text
            Next i

            For i = 2 To 8
                e.Row.Cells(i).Text = e.Row.Cells(i).Text & "%"
            Next i
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            Dim Filas As Integer = gridReporte.Rows.Count
            For i = 0 To 6
                e.Row.Cells(i + 2).Text = FormatNumber(Suma(i) / Filas, 2) & " %"
            Next i
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte anaquel por ejecutivo " + cmbPeriodo.SelectedItem.ToString() + " " + cmbQuincena.SelectedItem.ToString() + ".xls")
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

    Private Sub lnkVerTodo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkVerTodo.Click
        Response.Redirect("~/sitios/Mars/Autoservicio/Reportes/ReportesAntiguos/ReporteDetalleAnaquelEjecutivoMarsAS.aspx?orden=" & cmbPeriodo.SelectedValue & "&region_mars=" & cmbEjecutivo.SelectedValue & "&id_quincena=" & cmbQuincena.SelectedValue & "&id_region=" & cmbRegion.SelectedValue & "")
    End Sub

    Private Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCadena.SelectedIndexChanged
        CargarReporte()
    End Sub

    Private Sub gridAreaNielsen_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridAreaNielsen.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.Header
                e.Row.Cells(0).ColumnSpan = 8
                e.Row.Cells(1).Visible = False
                e.Row.Cells(2).Visible = False
                e.Row.Cells(3).Visible = False
                e.Row.Cells(4).Visible = False
                e.Row.Cells(5).Visible = False
                e.Row.Cells(6).Visible = False
                e.Row.Cells(7).Visible = False

                e.Row.Cells(0).Controls.Clear()
                e.Row.Cells(0).Text = Mars_AS.TablaAreaNielsen
        End Select
    End Sub
End Class