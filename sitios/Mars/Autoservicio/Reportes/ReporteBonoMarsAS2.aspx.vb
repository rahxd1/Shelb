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

Partial Public Class ReporteBonoMarsAS2
    Inherits System.Web.UI.Page

    Sub SQLCombo()
        MarsAS.SQLsCombo(cmbPeriodo.SelectedValue, "", _
                         cmbRegion.SelectedValue, cmbEjecutivo.SelectedValue, _
                         cmbSupervisor.SelectedValue, cmbPromotor.SelectedValue, _
                         "", "View_Historial_AS")
    End Sub

    Sub CargarReporte()
        Dim EjecutivoSQL, SupervisorSQL, PromotorSQL, RegionSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("Capturas.id_region", cmbRegion.SelectedValue)
            EjecutivoSQL = Acciones.Slc.cmb("US.region_mars", cmbEjecutivo.SelectedValue)
            SupervisorSQL = Acciones.Slc.cmb("US.id_supervisor", cmbSupervisor.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("Capturas.id_usuario", cmbPromotor.SelectedValue)

            CargaGrilla(ConexionMars.localSqlServer, _
                        "select DISTINCT Capturas.orden,US.Ejecutivo,Capturas.id_usuario,Capturas.area_nielsen, " & _
                        "Capturas.nombre_region,US.id_tipo,  " & _
                        "H.ASS, H.CS, H.PH, H.PB, H.GS, H.GH,H.GB,  " & _
                        "convert(nvarchar(5),H.Part_Anaquel)+'%'Part_Anaquel, " & _
                        "convert(nvarchar(5),(CASE WHEN H2.TiendasBono<=H2.PVI then 50 else 0 end))+'%'Exhibiciones,   " & _
                        "(H.Part_Anaquel)+(CASE WHEN H2.TiendasBono<=H2.PVI then 50 else 0 end)Total, " & _
                        "H2.TiendasPauta, H2.PVI, H2.Porcentaje,H2.TiendasBono,Capturas.Capturas " & _
                        "FROM(SELECT DISTINCT id_usuario,area_nielsen,nombre_region,id_region,RE.orden, " & _
                        "CASE WHEN COUNT(RE.id_tienda)=SUM(estatus_anaquel) then 'SI' else 'NO' end Capturas " & _
                        "FROM AS_Rutas_Eventos as RE " & _
                        "INNER JOIN View_Tiendas_AS as TI ON TI.id_tienda=RE.id_tienda " & _
                        "WHERE RE.orden=" & cmbPeriodo.SelectedValue & " " & _
                        "GROUP BY id_usuario,area_nielsen,nombre_region,id_region,RE.orden)Capturas " & _
                        "INNER JOIN View_Usuario_AS as US ON US.id_usuario = Capturas.id_usuario " & _
                        "LEFT JOIN (SELECT DISTINCT * FROM Cumplimiento_PVI_Anaquel WHERE orden=" & cmbPeriodo.SelectedValue & ")as H " & _
                        "ON Capturas.id_usuario = H.id_usuario " & _
                        "LEFT JOIN (SELECT Tiendas.orden, Tiendas.id_usuario,Tiendas.Tiendas as TiendasPauta, " & _
                        "(95*Tiendas.Tiendas/100)TiendasBono, ISNULL(TiendasPVI.PVI,0)PVI, " & _
                        "100*ISNULL(TiendasPVI.PVI,0)/Tiendas.Tiendas as Porcentaje " & _
                        "FROM(SELECT orden,id_usuario,COUNT(DISTINCT id_tienda)Tiendas " & _
                        "FROM AS_Rutas_Eventos as RE " & _
                        "WHERE orden=" & cmbPeriodo.SelectedValue & " " & _
                        "GROUP BY orden,id_usuario)Tiendas " & _
                        "FULL JOIN (SELECT id_usuario,COUNT(DISTINCT id_tienda)PVI " & _
                        "FROM View_Historial_Cumplimiento_AS " & _
                        "WHERE orden=" & cmbPeriodo.SelectedValue & " AND Cumplimiento=4 " & _
                        "GROUP BY id_usuario)TiendasPVI ON Tiendas.id_usuario=TiendasPVI.id_usuario) as H2 " & _
                        "ON Capturas.id_usuario=H2.id_usuario " & _
                        "WHERE Capturas.id_usuario<>'' " & _
                        "" + RegionSQL + EjecutivoSQL + SupervisorSQL + PromotorSQL + " " & _
                        "ORDER BY Capturas.id_usuario", Me.gridReporte)
        Else
            gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PeriodoSQLCmb, "nombre_periodo", "orden", cmbPeriodo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        CargarReporte()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 3 To 9
                e.Row.Cells(i).Text = e.Row.Cells(i).Text & "%"
            Next

            e.Row.Cells(11).Text = e.Row.Cells(11).Text & "%"
            e.Row.Cells(16).Text = e.Row.Cells(16).Text & "%"

            If gridReporte.DataKeys(e.Row.RowIndex).Value.ToString() = "NO" Then
                e.Row.Cells(16).BackColor = Drawing.Color.Red
                e.Row.Cells(16).Text = "0%"
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte bonos " + cmbPeriodo.SelectedItem.ToString() + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbEjecutivo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEjecutivo.SelectedIndexChanged
        cmbSupervisor.SelectedValue = ""

        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Private Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        cmbEjecutivo.SelectedValue = ""

        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub
End Class