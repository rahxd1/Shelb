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

Partial Public Class ReporteDetalleExhibicionesMarsAS
    Inherits System.Web.UI.Page

    Sub SQLCombo()
        MarsAS.SQLsCombo(cmbPeriodo.SelectedValue, cmbQuincena.SelectedValue, _
                         cmbRegion.SelectedValue, cmbEjecutivo.SelectedValue, _
                         cmbSupervisor.SelectedValue, cmbPromotor.SelectedValue, _
                         cmbCadena.SelectedValue, "View_Historial_AS")
    End Sub

    Sub CargarReporte()
        Dim QuincenaSQL, RegionSQL, EjecutivoSQL, SupervisorSQL, PromotorSQL, CadenaSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            QuincenaSQL = Acciones.Slc.cmb("H.id_quincena", cmbQuincena.SelectedValue)
            RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
            EjecutivoSQL = Acciones.Slc.cmb("REL.region_mars", cmbEjecutivo.SelectedValue)
            SupervisorSQL = Acciones.Slc.cmb("REL.id_supervisor", cmbSupervisor.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("TI.id_cadena", cmbCadena.SelectedValue)

            CargaGrilla(ConexionMars.localSqlServer, _
                        "select TI.nombre_region,REL.Ejecutivo, H.id_usuario, H.orden, H.id_quincena, H.nombre_punto," & _
                        "TI.clasificacion_tienda, TI.nombre_grupo, TI.nombre_cadena,TI.nombre_formato, TI.codigo, TI.nombre, " & _
                        "PVI.Cumplimiento,H.[12], H.Obj_12, H.Por_12,H.[11], H.Obj_11, H.Por_11,H.[10], H.Obj_10,H.Por_10, " & _
                        "H.[9], H.Obj_9, H.Por_9,H.[6], H.Obj_6, H.Por_6, H.[1], H.Obj_1, H.Por_1,H.[15], H.Obj_15, H.Por_15, " & _
                        "H.[3], H.Obj_3, H.Por_3,H.[5], H.Obj_5, H.Por_5,H.[16], H.Obj_16, H.Por_16 " & _
                        "FROM View_Historial_Puntos_AS as H " & _
                        "INNER JOIN View_Tiendas_AS as TI ON TI.id_tienda=H.id_tienda " & _
                        "INNER JOIN View_Usuario_AS as REL ON REL.id_usuario=H.id_usuario " & _
                        "INNER JOIN Cumplimiento_PVI_Nuevo as PVI  " & _
                        "ON PVI.orden=H.orden AND PVI.id_quincena=H.id_quincena " & _
                        "AND PVI.id_tienda=H.id_tienda AND PVI.id_usuario=H.id_usuario " & _
                        "WHERE H.orden=" & cmbPeriodo.SelectedValue & " " & _
                        "" + QuincenaSQL + RegionSQL + EjecutivoSQL + SupervisorSQL + PromotorSQL + CadenaSQL + " " & _
                        "ORDER BY H.id_usuario", Me.gridReporte)
        Else
            Me.gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PeriodoSQLCmb, "nombre_periodo", "orden", cmbPeriodo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.QuincenaSQLCmb, "nombre_quincena", "id_quincena", cmbQuincena)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)


            cmbPeriodo.SelectedValue = Request.Params("orden")
            cmbQuincena.SelectedValue = Request.Params("id_quincena")
            cmbRegion.SelectedValue = Request.Params("id_region")
            cmbSupervisor.SelectedValue = Request.Params("id_supervisor")

            If Request.Params("tipo") = 1 Then
                cmbPromotor.SelectedValue = Request.Params("id_usuario") : Else
                cmbEjecutivo.SelectedValue = Request.Params("region_mars") : End If

            If Request.Params("orden") <> "" Then
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

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCadena.SelectedIndexChanged
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Comentarios precios " + cmbPeriodo.SelectedItem.ToString() + ".xls")
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

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(10).Text = "NO" Then
                For iC = 0 To 40
                    e.Row.Cells(iC).BackColor = Drawing.Color.Salmon : Next iC
            End If

            'If e.Row.Cells(10).Text = "Anaquel" Then
            '    e.Row.Cells(10).BackColor = Drawing.Color.DarkOrange : End If
            'If e.Row.Cells(10).Text = "Pasillo mascota" Then
            '    e.Row.Cells(10).BackColor = Drawing.Color.CornflowerBlue : End If
            'If e.Row.Cells(10).Text = "Zona caliente" Then
            '    e.Row.Cells(10).BackColor = Drawing.Color.Red : End If
            'If e.Row.Cells(10).Text = "Entrada o salida" Then
            '    e.Row.Cells(10).BackColor = Drawing.Color.LimeGreen : End If
        End If
    End Sub
End Class