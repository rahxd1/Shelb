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

Partial Public Class ReporteRutasNR
    Inherits System.Web.UI.Page

    Dim RegionSQLCmb, EstadoSQLCmb, SupervisorSQLCmb, PromotorSQLCmb As String

    Sub SQLCombo()
        Dim RegionSel, EstadoSel, SupervisorSel As String

        RegionSel = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
        EstadoSel = Acciones.Slc.cmb("TI.id_estado", cmbEstado.SelectedValue)
        SupervisorSel = Acciones.Slc.cmb("US.id_supervisor", cmbSupervisor.SelectedValue)

        RegionSQLCmb = "SELECT DISTINCT TI.id_region, TI.nombre_region " & _
                    "FROM NR_CatRutas as RUT " & _
                    "INNER JOIN View_Tiendas_NR as TI on TI.id_tienda=RUT.id_tienda " & _
                    "INNER JOIN  View_Usuario_NR as US ON RUT.id_usuario=US.id_usuario " & _
                    "WHERE TI.id_region <>0  " & _
                    " ORDER BY TI.nombre_region"

        EstadoSQLCmb = "SELECT DISTINCT TI.id_estado, TI.nombre_estado " & _
                    "FROM NR_CatRutas as RUT " & _
                    "INNER JOIN View_Tiendas_NR as TI on TI.id_tienda=RUT.id_tienda " & _
                    "INNER JOIN  View_Usuario_NR as US ON RUT.id_usuario=US.id_usuario " & _
                    "WHERE TI.id_estado<>0  " & _
                    "" + RegionSel + " " & _
                    " ORDER BY TI.nombre_estado"

        SupervisorSQLCmb = "SELECT DISTINCT US.id_supervisor, US.supervisor " & _
                     "FROM NR_CatRutas as RUT " & _
                    "INNER JOIN View_Tiendas_NR as TI on TI.id_tienda=RUT.id_tienda " & _
                    "INNER JOIN  View_Usuario_NR as US ON RUT.id_usuario=US.id_usuario " & _
                     "WHERE US.id_usuario<>'' " & _
                     " " + RegionSel + EstadoSel + " ORDER BY US.id_supervisor"

        PromotorSQLCmb = "SELECT DISTINCT RUT.id_usuario " & _
                    "FROM NR_CatRutas as RUT " & _
                    "INNER JOIN View_Tiendas_NR as TI on TI.id_tienda=RUT.id_tienda " & _
                    "INNER JOIN  View_Usuario_NR as US ON RUT.id_usuario=US.id_usuario " & _
                    "WHERE RUT.id_usuario<>'' " & _
                    " " + RegionSel + EstadoSel + "  " & _
                    " " + SupervisorSel + " ORDER BY RUT.id_usuario"
    End Sub

    Sub CargarReporte()
        Dim RegionSQL, EstadoSQL, SupervisorSQL, PromotorSQL As String

        RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
        EstadoSQL = Acciones.Slc.cmb("TI.id_estado", cmbEstado.SelectedValue)
        SupervisorSQL = Acciones.Slc.cmb("US.id_supervisor", cmbSupervisor.SelectedValue)
        PromotorSQL = Acciones.Slc.cmb("RUT.id_usuario", cmbPromotor.SelectedValue)

        If Tipo_usuario = 7 Then
            Berol.CuentaClave = "INNER JOIN (select * FROM Usuarios_cadenas WHERE id_usuario='" & HttpContext.Current.User.Identity.Name & "' )as CC " & _
                            "ON CC.id_cadena=TI.id_cadena " : Else
            Berol.CuentaClave = "" : End If

        CargaGrilla(ConexionBerol.localSqlServer, _
                        "select RUT.id_tienda, TI.codigo, TI.nombre, TI.ciudad, TI.nombre_estado, TI.nombre_region, Ti.nombre_formato, " & _
                        "TI.nombre_cadena, RUT.id_usuario, US.supervisor " & _
                        "FROM NR_CatRutas as RUT " & _
                        "INNER JOIN View_Tiendas_NR as TI on TI.id_tienda=RUT.id_tienda " & _
                        "INNER JOIN View_Usuario_NR as US ON US.id_usuario=RUT.id_usuario " & _
                        " " + Berol.CuentaClave + " " & _
                        "WHERE RUT.id_tienda<>0 " & _
                        " " + RegionSQL + EstadoSQL + " " & _
                        " " + SupervisorSQL + PromotorSQL + " " & _
                        "ORDER BY RUT.id_usuario, TI.nombre_region, TI.nombre, TI.nombre_formato, TI.nombre_cadena ", _
                        Me.gridReporte)

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionBerol.localSqlServer, RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

            CargarReporte()
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte rutas.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Private Sub cmbEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEstado.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

End Class