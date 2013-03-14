﻿Imports System
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

Partial Public Class ReporteExhibicionesNR
    Inherits System.Web.UI.Page

    Sub SQLCombo()
        NR.SQLsCombo(cmbPeriodo.SelectedValue, cmbRegion.SelectedValue, cmbEstado.SelectedValue, _
                    cmbSupervisor.SelectedValue, cmbPromotor.SelectedValue, cmbFormato.SelectedValue, _
                    cmbCadena.SelectedValue)
    End Sub

    Sub CargarReporte()
        Dim RegionSQL, EstadoSQL, SupervisorSQL, PromotorSQL, CadenaSQL, FormatoSQL, TiendaSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("H.id_region", cmbRegion.SelectedValue)
            EstadoSQL = Acciones.Slc.cmb("H.id_estado", cmbEstado.SelectedValue)
            SupervisorSQL = Acciones.Slc.cmb("H.id_supervisor", cmbSupervisor.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("H.id_cadena", cmbCadena.SelectedValue)
            FormatoSQL = Acciones.Slc.cmb("H.id_formato", cmbFormato.SelectedValue)
            TiendaSQL = Acciones.Slc.cmb("H.id_tienda", cmbTienda.SelectedValue)

            If Tipo_usuario = 7 Then
                Berol.CuentaClave = "INNER JOIN (select * FROM Usuarios_cadenas WHERE id_usuario='" & HttpContext.Current.User.Identity.Name & "' )as CC " & _
                                "ON CC.id_cadena=H.id_cadena " : Else
                Berol.CuentaClave = "" : End If

            CargaGrilla(ConexionBerol.localSqlServer, _
                        "SELECT H.id_usuario,H.nombre_region,H.ciudad,H.nombre_estado,H.nombre_cadena, " & _
                        "H.supervisor,H.nombre_formato,H.nombre,H.codigo, " & _
                        "ISNULL([1],0)[1],ISNULL([2],0)[2],ISNULL([3],0)[3], " & _
                        "ISNULL([4],0)[4],ISNULL([5],0)[5],ISNULL([6],0)[6],ISNULL([7],0)[7] " & _
                        "FROM View_Historial_NR as H  " & _
                        "INNER JOIN (SELECT folio_historial,id_exhibicion,cantidad " & _
                        "FROM NR_Historial_Exhibiciones_Det as H) PVT " & _
                        "PIVOT(SUM(cantidad) FOR id_exhibicion  " & _
                        "IN([1],[2],[3],[4],[5],[6],[7])) AS HDET  " & _
                        "ON HDET.folio_historial=H.folio_historial  " & _
                        " " + Berol.CuentaClave + " " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + EstadoSQL + " " & _
                        " " + SupervisorSQL + PromotorSQL + " " & _
                        " " + CadenaSQL + FormatoSQL + TiendaSQL + " " & _
                        " " + TiendaSQL + " " & _
                        "ORDER BY H.nombre_region,H.ciudad,H.nombre_cadena,H.nombre_formato,H.nombre ", _
                        Me.gridReporte)
        Else
            gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.PeriodoSQLCmb, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.FormatoSQLCmb, "nombre_formato", "id_formato", cmbFormato)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.FormatoSQLCmb, "nombre_formato", "id_formato", cmbFormato)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.FormatoSQLCmb, "nombre_formato", "id_formato", cmbFormato)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCadena.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.FormatoSQLCmb, "nombre_formato", "id_formato", cmbFormato)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.FormatoSQLCmb, "nombre_formato", "id_formato", cmbFormato)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbTienda_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbTienda.SelectedIndexChanged
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte exhibiciones por tienda " + cmbPeriodo.SelectedItem.ToString() + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.FormatoSQLCmb, "nombre_formato", "id_formato", cmbFormato)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Private Sub cmbEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEstado.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.FormatoSQLCmb, "nombre_formato", "id_formato", cmbFormato)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Private Sub cmbFormato_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFormato.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub
End Class