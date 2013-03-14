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

Partial Public Class ReporteAnaquelPromotorMarsAS2
    Inherits System.Web.UI.Page

    Dim Filas As Integer, Suma(7) As Double

    Sub SQLCombo()
        MarsAS.SQLsCombo(cmbPeriodo.SelectedValue, cmbQuincena.SelectedValue, _
                        cmbRegion.SelectedValue, cmbEjecutivo.SelectedValue, _
                        cmbSupervisor.SelectedValue, cmbPromotor.SelectedValue, _
                        cmbCadena.SelectedValue, "View_Historial_AS")
    End Sub

    Sub CargarReporte()
        Dim QuincenaSQL, EjecutivoSQL, SupervisorSQL, PromotorSQL, RegionSQL, CadenaSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            QuincenaSQL = Acciones.Slc.cmb("H.id_quincena", cmbQuincena.SelectedValue)
            RegionSQL = Acciones.Slc.cmb("H.id_region", cmbRegion.SelectedValue)
            EjecutivoSQL = Acciones.Slc.cmb("H.region_mars", cmbEjecutivo.SelectedValue)
            SupervisorSQL = Acciones.Slc.cmb("H.id_supervisor", cmbSupervisor.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("H.id_cadena", cmbCadena.SelectedValue)

            CargaGrilla(ConexionMars.localSqlServer, _
                        "select H.orden,H.Ejecutivo,H.id_usuario,H.area_nielsen,H.nombre_region, " & _
                        "H.O_ps,CASE when ((SUM(HDET.[1])+SUM(HDET.[15]))+(SUM(HDET.[5])+SUM(HDET.[16])))=0 then 0 else " & _
                        "cast(round((SUM(HDET.[1])+SUM(HDET.[15]))/cast(((SUM(HDET.[1])+SUM(HDET.[15]))+(SUM(HDET.[5])++SUM(HDET.[16]))) as decimal(9,4)),4,0) * 100 as decimal(9,2))end ASS, " & _
                        "H.O_pc,CASE when (SUM(HDET.[2])+SUM(HDET.[6]))=0 then 0 else " & _
                        "cast(round(SUM(HDET.[2])/cast((SUM(HDET.[2])+SUM(HDET.[6])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end CS, " & _
                        "H.O_ph,CASE when (SUM(HDET.[3])+SUM(HDET.[7]))=0 then 0 else " & _
                        "cast(round(SUM(HDET.[3])/cast((SUM(HDET.[3])+SUM(HDET.[7])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end PH, " & _
                        "H.O_pb,CASE when (SUM(HDET.[8])+SUM(HDET.[4]))=0 then 0 else " & _
                        "cast(round(SUM(HDET.[4])/cast((SUM(HDET.[4])+SUM(HDET.[8])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end PB, " & _
                        "H.O_gs,CASE when (SUM(HDET.[9])+SUM(HDET.[12]))=0 then 0 else " & _
                        "cast(round(SUM(HDET.[9])/cast((SUM(HDET.[9])+SUM(HDET.[12])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end GS, " & _
                        "H.O_gh,CASE when (SUM(HDET.[10])+SUM(HDET.[13]))=0 then 0 else " & _
                        "cast(round(SUM(HDET.[10])/cast((SUM(HDET.[10])+SUM(HDET.[13])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end GH, " & _
                        "H.O_gb,CASE when (SUM(HDET.[11])+SUM(HDET.[14]))=0 then 0 else " & _
                        "cast(round(SUM(HDET.[11])/cast((SUM(HDET.[11])+SUM(HDET.[14])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end GB " & _
                        "FROM AS_Segmentos_Historial_Det as HDET " & _
                        "INNER JOIN View_Historial_AS as H ON HDET.folio_historial = H.folio_historial " & _
                        "WHERE H.orden=" & cmbPeriodo.SelectedValue & " " & _
                        " " + QuincenaSQL + " " & _
                        " " + RegionSQL + " " & _
                        " " + EjecutivoSQL + " " & _
                        " " + SupervisorSQL + " " & _
                        " " + PromotorSQL + CadenaSQL + " " & _
                        "GROUP BY H.orden,H.Ejecutivo,H.id_usuario,H.area_nielsen,H.nombre_region, " & _
                        "H.O_ps,H.O_pc,H.O_ph,H.O_pb,H.O_gs,H.O_gh,H.O_gb " & _
                        "ORDER BY H.id_usuario", Me.gridReporte)
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
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)


            cmbPeriodo.SelectedValue = Request.Params("orden")
            cmbRegion.SelectedValue = Request.Params("id_region")
            cmbEjecutivo.SelectedValue = Request.Params("region_mars")
            cmbSupervisor.SelectedValue = Request.Params("id_supervisor")
            cmbPromotor.SelectedValue = Request.Params("id_usuario")

            If Request.Params("orden") <> "" Then
                SQLCombo()
                Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
                Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
                Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
                Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
                Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

                CargarReporte()
            End If
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        Filas = gridReporte.Rows.Count

        If e.Row.RowType = DataControlRowType.DataRow Then
            Suma(0) = Suma(0) + e.Row.Cells(5).Text
            Suma(1) = Suma(1) + e.Row.Cells(7).Text
            Suma(2) = Suma(2) + e.Row.Cells(9).Text
            Suma(3) = Suma(3) + e.Row.Cells(11).Text
            Suma(4) = Suma(4) + e.Row.Cells(13).Text
            Suma(5) = Suma(5) + e.Row.Cells(15).Text
            Suma(6) = Suma(6) + e.Row.Cells(17).Text

            For i = 4 To 17
                e.Row.Cells(i).Text = e.Row.Cells(i).Text & "%"
            Next i
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(5).Text = FormatNumber(Suma(0) / Filas, 2) & " %"
            e.Row.Cells(7).Text = FormatNumber(Suma(1) / Filas, 2) & " %"
            e.Row.Cells(9).Text = FormatNumber(Suma(2) / Filas, 2) & " %"
            e.Row.Cells(11).Text = FormatNumber(Suma(3) / Filas, 2) & " %"
            e.Row.Cells(13).Text = FormatNumber(Suma(4) / Filas, 2) & " %"
            e.Row.Cells(15).Text = FormatNumber(Suma(5) / Filas, 2) & " %"
            e.Row.Cells(17).Text = FormatNumber(Suma(6) / Filas, 2) & " %"
        End If
    End Sub

    Protected Sub cmbQuincena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbQuincena.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte anaquel por promotor " + cmbPeriodo.SelectedItem.ToString() + " " + cmbQuincena.SelectedItem.ToString() + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbEjecutivo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEjecutivo.SelectedIndexChanged
        cmbSupervisor.SelectedValue = ""

        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Private Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        cmbEjecutivo.SelectedValue = ""

        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Private Sub lnkVerTodo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkVerTodo.Click
        Response.Redirect("~/sitios/Mars/Autoservicio/Reportes/ReporteDetalleAnaquelPromotorMarsAS2.aspx?orden=" & cmbPeriodo.SelectedValue & "&region_mars=" & cmbEjecutivo.SelectedValue & "&id_quincena=" & cmbQuincena.SelectedValue & "&id_region=" & cmbRegion.SelectedValue & "&id_usuario=" & cmbPromotor.SelectedValue & "")
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