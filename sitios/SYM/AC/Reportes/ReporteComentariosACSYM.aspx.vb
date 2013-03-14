Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteComentariosACSYM
    Inherits System.Web.UI.Page

    Sub SQLCombo()
        Combos_SYMAC.SQLsCombo(cmbPeriodo.SelectedValue, cmbRegion.SelectedValue, _
                             cmbEstado.SelectedValue, cmbSupervisor.SelectedValue, _
                             cmbCiudad.SelectedValue, cmbPromotor.SelectedValue, _
                             cmbCadena.SelectedValue, "View_SYM_AC_Anaquel_H")
    End Sub

    Sub CargarReporte()
        Dim RegionSQL, CadenaSQL, EstadoSQL, SupervisorSQL, CiudadSQL, PromotorSQL, TiendaSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("CAD.id_cadena", cmbCadena.SelectedValue)
            EstadoSQL = Acciones.Slc.cmb("TI.id_estado", cmbEstado.SelectedValue)
            SupervisorSQL = Acciones.Slc.cmb("REL.id_supervisor", cmbSupervisor.SelectedValue)
            CiudadSQL = Acciones.Slc.cmb("TI.ciudad", cmbCiudad.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("RE.id_usuario", cmbPromotor.SelectedValue)
            TiendaSQL = Acciones.Slc.cmb("TI.id_tienda", cmbTienda.SelectedValue)

            CargaGrilla(ConexionSYM.localSqlServer, _
                        "select DISTINCT TI.nombre_region,TI.nombre_cadena,TI.nombre_estado, REL.id_supervisor, " & _
                        "TI.ciudad, H.id_usuario, TI.nombre,  " & _
                        "H.comentario_general, ISNULL(H2.comentario_general,'')as comentarios2 " & _
                        "FROM View_SYM_AC_Tiendas as TI  " & _
                        "INNER JOIN Anaquel_Historial as H ON TI.id_tienda = H.id_tienda AND H.comentario_general<>'' " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario = H.id_usuario " & _
                        "INNER JOIN AC_Rutas_Eventos as RE ON RE.id_usuario = H.id_usuario AND RE.id_periodo = H.id_periodo AND RE.id_tienda = H.id_tienda " & _
                        "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario=RE.id_usuario  " & _
                        "INNER JOIN Catalogacion_Historial as H2 ON H2.id_periodo = H.id_periodo AND H.id_tienda = H2.id_tienda " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + " " & _
                        " " + CadenaSQL + " " & _
                        " " + EstadoSQL + " " & _
                        " " + SupervisorSQL + " " & _
                        " " + CiudadSQL + " " & _
                        " " + PromotorSQL + " " & _
                        " " + TiendaSQL + " " & _
                        "ORDER BY TI.nombre_region,H.id_usuario,TI.nombre_cadena,TI.nombre ", gridReporte)
        Else
            gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PeriodoSQLCmb, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.EstadoSQLcmb, "nombre_estado", "id_estado", cmbEstado)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.SupervisorSQLcmb, "supervisor", "id_supervisor", cmbSupervisor)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CiudadSQLCmb, "ciudad", "ciudad", cmbCiudad)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.EstadoSQLcmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.SupervisorSQLcmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CiudadSQLCmb, "ciudad", "ciudad", cmbCiudad)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.EstadoSQLcmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.SupervisorSQLcmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CiudadSQLCmb, "ciudad", "ciudad", cmbCiudad)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCadena.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Comentarios " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEstado.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.SupervisorSQLcmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CiudadSQLCmb, "ciudad", "ciudad", cmbCiudad)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub


    Private Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CiudadSQLCmb, "ciudad", "ciudad", cmbCiudad)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Private Sub cmbCiudad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCiudad.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

End Class