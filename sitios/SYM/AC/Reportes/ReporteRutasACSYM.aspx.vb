Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteRutasACSYM
    Inherits System.Web.UI.Page

    Dim RegionSQL, SupervisorSQL, PromotorSQL As String

    Sub SQLCombo()
        Dim RegionSel, SupervisorSel As String
        RegionSel = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
        SupervisorSel = Acciones.Slc.cmb("REL.id_supervisor", cmbSupervisor.SelectedValue)

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Regiones as REG WHERE REG.id_region<>0 " & _
                    " ORDER BY REG.nombre_region"

        SupervisorSQL = "SELECT DISTINCT REL.id_supervisor, US.nombre, US.nombre + ' ('+REL.id_supervisor+')'as supervisor " & _
                    "FROM AC_Tiendas as TI " & _
                    "INNER JOIN AC_Rutas_Eventos as RE ON RE.id_tienda=TI.id_tienda " & _
                    "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario=RE.id_usuario  " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario=REL.id_supervisor " & _
                    "WHERE TI.estatus=1 AND RE.id_usuario<>'' " & _
                    " " + RegionSel + " " & _
                    "ORDER BY US.nombre "

        PromotorSQL = "SELECT DISTINCT US.id_usuario " & _
                    "FROM Usuarios as US " & _
                    "INNER JOIN AC_CatRutas as RUT ON RUT.id_usuario = US.id_usuario " & _
                    "INNER JOIN AC_Tiendas as TI ON TI.id_tienda=RUT.id_tienda " & _
                    "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario=RUT.id_usuario  " & _
                    "WHERE US.id_tipo = 1 " & _
                    " " + SupervisorSel + " " & _
                    " " + RegionSel + " " & _
                    " ORDER BY US.id_usuario "
    End Sub

    Sub CargarReporte()
        RegionSQL = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)
        SupervisorSQL = Acciones.Slc.cmb("REL.id_supervisor", cmbSupervisor.SelectedValue)
        PromotorSQL = Acciones.Slc.cmb("RUT.id_usuario", cmbPromotor.SelectedValue)

        CargaGrilla(ConexionSYM.localSqlServer, _
                    "select DISTINCT TI.id_tienda,TI.nombre_region,REL.id_supervisor, " & _
                    "US.nombre as nombre_sup, RUT.id_usuario,TI.nombre_cadena,TI.nombre, " & _
                     "TI.ciudad, TI.nombre_estado " & _
                     "FROM AC_CatRutas as RUT  " & _
                     "INNER JOIN View_SYM_AC_Tiendas as TI ON TI.id_tienda = RUT.id_tienda " & _
                     "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario=RUT.id_usuario  " & _
                     "INNER JOIN Usuarios as US ON US.id_usuario = REL.id_supervisor " & _
                     "WHERE estatus=1 " & _
                     " " + RegionSQL + " " & _
                     " " + PromotorSQL + " " & _
                     " " + SupervisorSQL + " " & _
                     "ORDER BY TI.nombre_region,TI.nombre_cadena,TI.nombre", gridReporte)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionSYM.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SupervisorSQL, "supervisor", "id_supervisor", cmbSupervisor)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

            CargarReporte()
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SupervisorSQL, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Protected Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

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
        Response.AddHeader("Content-Disposition", "attachment;filename=Ruta Anaquel y catalogacion.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

End Class