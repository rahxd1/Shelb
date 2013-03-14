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

Partial Public Class ReporteDetalleAnaquelEjecutivoMarsAS2
    Inherits System.Web.UI.Page

    Dim Dato(50), Suma(50) As Double
    Dim Cumplimiento(50), Objetivo(50) As Double

    Sub SQLCombo()
        MarsAS.SQLsCombo(cmbPeriodo.SelectedValue, cmbQuincena.SelectedValue, _
                         cmbRegion.SelectedValue, cmbEjecutivo.SelectedValue, _
                         cmbSupervisor.SelectedValue, cmbPromotor.SelectedValue, _
                         cmbCadena.SelectedValue, "View_Historial_AS")
    End Sub

    Sub CargarReporte()
        Dim QuincenaSQL, RegionSQL, EjecutivoSQL, SupervisorSQL, PromotorSQL, CadenaSQL, TiendaSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            QuincenaSQL = Acciones.Slc.cmb("H.id_quincena", cmbQuincena.SelectedValue)
            RegionSQL = Acciones.Slc.cmb("H.id_region", cmbRegion.SelectedValue)
            EjecutivoSQL = Acciones.Slc.cmb("H.region_mars", cmbEjecutivo.SelectedValue)
            SupervisorSQL = Acciones.Slc.cmb("H.id_supervisor", cmbSupervisor.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("H.id_cadena", cmbCadena.SelectedValue)
            TiendaSQL = Acciones.Slc.cmb("H.id_tienda", cmbTienda.SelectedValue)

            CargaGrilla(ConexionMars.localSqlServer, _
                        "select H.Ejecutivo,H.id_usuario,H.area_nielsen,H.nombre_region, " & _
                        "H.codigo,H.clasificacion_tienda,H.nombre_cadena,H.nombre_grupo,H.nombre_formato,H.nombre,  " & _
                        "(cast(AVG(cast((HDET.[1]+HDET.[15])as decimal(9,4)))as decimal(9,2)))[1],(cast(AVG(cast((HDET.[5]+HDET.[16])as decimal(9,4)))as decimal(9,2)))[5], " & _
                        "CASE when ((AVG(HDET.[1])+AVG(HDET.[15]))+(AVG(HDET.[5])+AVG(HDET.[16])))=0 then 0 else   " & _
                        "cast(round((AVG(HDET.[1])+AVG(HDET.[15]))/cast(((AVG(HDET.[1])+AVG(HDET.[15]))+(AVG(HDET.[5])+AVG(HDET.[16]))) as decimal(9,4)),4,0) * 100 as decimal(9,2))end Porcentaje_PS, " & _
                        "H.O_ps Objetivo_PS, " & _
                        "(cast(AVG(cast(HDET.[2]as decimal(9,4)))as decimal(9,2)))[2],(cast(AVG(cast(HDET.[6]as decimal(9,4)))as decimal(9,2)))[6], " & _
                        "CASE when (AVG(HDET.[2])+AVG(HDET.[6]))=0 then 0 else   " & _
                        "cast(round(AVG(HDET.[2])/cast((AVG(HDET.[2])+AVG(HDET.[6])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end Porcentaje_PC, " & _
                        "H.O_pc Objetivo_PC, " & _
                        "(cast(AVG(cast(HDET.[3]as decimal(9,4)))as decimal(9,2)))[3],(cast(AVG(cast(HDET.[7]as decimal(9,4)))as decimal(9,2)))[7], " & _
                        "CASE when (AVG(HDET.[3])+AVG(HDET.[7]))=0 then 0 else   " & _
                        "cast(round(AVG(HDET.[3])/cast((AVG(HDET.[3])+AVG(HDET.[7])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end Porcentaje_PH, " & _
                        "H.O_ph Objetivo_PH, " & _
                        "(cast(AVG(cast(HDET.[4]as decimal(9,4)))as decimal(9,2)))[4],(cast(AVG(cast(HDET.[8]as decimal(9,4)))as decimal(9,2)))[8], " & _
                        "CASE when (AVG(HDET.[4])+AVG(HDET.[8]))=0 then 0 else   " & _
                        "cast(round(AVG(HDET.[4])/cast((AVG(HDET.[4])+AVG(HDET.[8])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end Porcentaje_PB, " & _
                        "H.O_pb Objetivo_PB, " & _
                        "(cast(AVG(cast(HDET.[9]as decimal(9,4)))as decimal(9,2)))[9],(cast(AVG(cast(HDET.[12]as decimal(9,4)))as decimal(9,2)))[12], " & _
                        "CASE when (AVG(HDET.[9])+AVG(HDET.[12]))=0 then 0 else   " & _
                        "cast(round(AVG(HDET.[9])/cast((AVG(HDET.[9])+AVG(HDET.[12])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end Porcentaje_GS, " & _
                        "H.O_gs Objetivo_GS, " & _
                        "(cast(AVG(cast(HDET.[10]as decimal(9,4)))as decimal(9,2)))[10],(cast(AVG(cast(HDET.[13]as decimal(9,4)))as decimal(9,2)))[13], " & _
                        "CASE when (AVG(HDET.[10])+AVG(HDET.[13]))=0 then 0 else   " & _
                        "cast(round(AVG(HDET.[10])/cast((AVG(HDET.[10])+AVG(HDET.[13])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end Porcentaje_GH,  " & _
                        "H.O_gh Objetivo_GH, " & _
                        "(cast(AVG(cast(HDET.[11]as decimal(9,4)))as decimal(9,2)))[11],(cast(AVG(cast(HDET.[14]as decimal(9,4)))as decimal(9,2)))[14], " & _
                        "CASE when (AVG(HDET.[11])+AVG(HDET.[14]))=0 then 0 else   " & _
                        "cast(round(AVG(HDET.[11])/cast((AVG(HDET.[11])+AVG(HDET.[14])) as decimal(9,4)),4,0) * 100 as decimal(9,2))end Porcentaje_GB, " & _
                        "H.O_gb Objetivo_GB " & _
                        "FROM AS_Segmentos_Historial_Det as HDET " & _
                        "INNER JOIN View_Historial_AS as H ON HDET.folio_historial = H.folio_historial " & _
                        "WHERE H.orden=" & cmbPeriodo.SelectedValue & " " & _
                        " " + QuincenaSQL + RegionSQL + " " & _
                        " " + EjecutivoSQL + SupervisorSQL + PromotorSQL + " " & _
                        " " + CadenaSQL + TiendaSQL + " " & _
                        "GROUP BY H.ejecutivo,H.id_usuario,H.area_nielsen,H.nombre_region,  " & _
                        "H.codigo,H.clasificacion_tienda,H.nombre_cadena,H.nombre,H.nombre_grupo, " & _
                        "H.O_ps,H.O_pc,H.O_ph,H.O_pb,H.O_gs,H.O_gh,H.O_gb, H.nombre_formato " & _
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
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)


            cmbPeriodo.SelectedValue = Request.Params("orden")
            cmbQuincena.SelectedValue = Request.Params("id_quincena")
            cmbRegion.SelectedValue = Request.Params("id_region")
            cmbEjecutivo.SelectedValue = Request.Params("region_mars")
            cmbSupervisor.SelectedValue = Request.Params("id_supervisor")

            If Request.Params("orden") <> "" Then
                CargarReporte()
            End If
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCadena.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbTienda_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbTienda.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbQuincena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbQuincena.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte detalle anaquel por ejecutivo " + cmbPeriodo.SelectedItem.ToString() + " " + cmbQuincena.SelectedItem.ToString() + ".xls")
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
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Private Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        cmbEjecutivo.SelectedValue = ""

        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 10 To 37
                Dato(i) = e.Row.Cells(i).Text
                Suma(i) = Suma(i) + Dato(i)
            Next i

            For i = 12 To 36 Step 4
                Cumplimiento(i) = e.Row.Cells(i).Text
                Objetivo(i + 1) = e.Row.Cells(i + 1).Text

                If Cumplimiento(i) < Objetivo(i + 1) Then
                    e.Row.Cells(i).BackColor = Drawing.Color.Red
                    e.Row.Cells(i + 1).BackColor = Drawing.Color.Red : Else
                    e.Row.Cells(i).BackColor = Drawing.Color.GreenYellow
                    e.Row.Cells(i + 1).BackColor = Drawing.Color.GreenYellow : End If

                e.Row.Cells(i).Text = Cumplimiento(i) & "%"
                e.Row.Cells(i + 1).Text = Objetivo(i + 1) & "%"
            Next i
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            For i = 10 To 37
                e.Row.Cells(i).Text = FormatNumber(Suma(i), 2)

                For iPor = 12 To 36 Step 4
                    e.Row.Cells(iPor).Text = FormatNumber((Suma(iPor) / (gridReporte.Rows.Count)), 2) & "%"
                    e.Row.Cells(iPor + 1).Text = FormatNumber((Suma(iPor + 1) / (gridReporte.Rows.Count)), 2) & "%"
                Next iPor

            Next i
        End If
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