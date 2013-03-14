Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteFrentesLineaACSYM
    Inherits System.Web.UI.Page

    Dim Campo(50), Campos, CampoTitulo(50), CamposTitulos As String
    Dim Dato(100), Suma(100) As Integer

    Sub SQLCombo()
        Combos_SYMAC.SQLsCombo(cmbPeriodo.SelectedValue, cmbRegion.SelectedValue, _
                             cmbEstado.SelectedValue, cmbSupervisor.SelectedValue, _
                             cmbCiudad.SelectedValue, cmbPromotor.SelectedValue, _
                             cmbCadena.SelectedValue, "View_SYM_Anaquel_Historial")
    End Sub

    Sub VerProductos()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "select * FROM Anaquel_Tipo_Productos")
        If Tabla.Rows.Count > 0 Then
            For i = 0 To Tabla.Rows.Count - 1
                If i = 0 Then
                    Campo(i) = "[" & Tabla.Rows(i)("id_tipo") & "]" : Else
                    Campo(i) = ",[" & Tabla.Rows(i)("id_tipo") & "]" : End If

                CampoTitulo(i) = ",ISNULL([" & Tabla.Rows(i)("id_tipo") & "],0) as '" & Tabla.Rows(i)("nombre_tipoproducto") & "'"

                Campos = Campos + Campo(i)
                CamposTitulos = CamposTitulos + CampoTitulo(i)
            Next i
        End If

        tabla.Dispose()
    End Sub

    Sub CargarReporte()
        Dim RegionSQL, CadenaSQL, EstadoSQL, SupervisorSQL, CiudadSQL, PromotorSQL, TiendaSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("TI.id_cadena", cmbCadena.SelectedValue)
            EstadoSQL = Acciones.Slc.cmb("TI.id_estado", cmbEstado.SelectedValue)
            SupervisorSQL = Acciones.Slc.cmb("REL.id_supervisor", cmbSupervisor.SelectedValue)
            CiudadSQL = Acciones.Slc.cmb("TI.ciudad", cmbCiudad.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("RE.id_usuario", cmbPromotor.SelectedValue)
            TiendaSQL = Acciones.Slc.cmb("TI.id_tienda", cmbTienda.SelectedValue)

            VerProductos()

            CargaGrilla(ConexionSYM.localSqlServer, _
                        "SELECT DISTINCT nombre_region as 'División',nombre_cadena as 'Cadena', nombre_estado as 'Estado', " & _
                         "id_supervisor as 'Supervisor',Ciudad,id_usuario as 'Promotor',nombre as 'Tienda'" & _
                         " " + CamposTitulos + " " & _
                         "FROM(select DISTINCT TI.nombre_region,TI.nombre_cadena,TI.nombre_estado, RE.id_ejecutivo, RE.id_supervisor, " & _
                         " TI.ciudad, H.id_usuario, TI.nombre, PROD.id_tipo, HDET.frentes " & _
                         "FROM View_SYM_AC_Tiendas as TI " & _
                         "INNER JOIN Anaquel_Historial as H  ON TI.id_tienda = H.id_tienda  " & _
                         "INNER JOIN Anaquel_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                         "INNER JOIN Productos_Nuevos as PROD ON PROD.tipo_grupo = TPROD.tipo_grupo   " & _
                         "INNER JOIN Linea_Productos_Nuevo as LIN ON PROD.id_linea = LIN.id_linea  " & _
                         "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario=H.id_usuario " & _
                         "INNER JOIN AC_Rutas_Eventos as RE ON RE.id_usuario = H.id_usuario " & _
                         "AND RE.id_periodo = H.id_periodo AND RE.id_tienda = H.id_tienda " & _
                         "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                         " " + RegionSQL + " " & _
                         " " + CadenaSQL + " " & _
                         " " + EstadoSQL + " " & _
                         " " + SupervisorSQL + " " & _
                         " " + CiudadSQL + " " & _
                         " " + PromotorSQL + " " & _
                         " " + TiendaSQL + ") AS Datos PIVOT(SUM(frentes) " & _
                         "FOR id_tipo IN (" + Campos + ")) AS PivotTable " & _
                         "ORDER BY nombre_region,nombre_cadena,nombre_estado, id_ejecutivo, id_supervisor, ciudad, id_usuario, nombre", _
                         gridReporte)
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
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PromotorSQLCmb, "nombre", "id_tienda", cmbTienda)
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PromotorSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PromotorSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.EstadoSQLcmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.SupervisorSQLcmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CiudadSQLCmb, "ciudad", "ciudad", cmbCiudad)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PromotorSQLCmb, "nombre", "id_tienda", cmbTienda)

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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Frentes por linea " + cmbPeriodo.SelectedItem.ToString + ".xls")
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

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            For iTiendas = 8 To 14
                ''Cuenta
                If e.Row.Cells(iTiendas).Text <> 0 Then
                    Dato(iTiendas) = 1
                    Suma(iTiendas) = Suma(iTiendas) + Dato(iTiendas)
                End If
            Next iTiendas
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            For iTiendas = 8 To 14
                e.Row.Cells(iTiendas).Text = Suma(iTiendas)
            Next
        End If
    End Sub

    Private Sub cmbTienda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTienda.SelectedIndexChanged
        CargarReporte()
    End Sub
End Class