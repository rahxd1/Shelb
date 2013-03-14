Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteResumenSYMDemos
    Inherits System.Web.UI.Page

    Dim RegionSel, PlazaSel As String
    Dim PeriodoSQL, PromotorSQL, RegionSQL, PlazaSQL As String
    Dim Dato(4), Suma(4) As Integer

    Sub SQLCombo()
        RegionSel = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)
        PlazaSel = Acciones.Slc.cmb("plaza", cmbCiudad.SelectedValue)

        PeriodoSQL = "SELECT id_periodo, nombre_periodo " & _
                         "FROM Demos_Periodos " & _
                         " ORDER BY fecha_inicio DESC"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Regiones as REG " & _
                    "WHERE REG.id_region<>0 " & _
                    " ORDER BY REG.nombre_region"

        PlazaSQL = "select DISTINCT plaza " & _
                    "from Usuarios as US " & _
                    "INNER JOIN Demos_Historial as H ON H.id_usuario=US.id_usuario " & _
                    "WHERE US.id_usuario<>'' " & _
                    " " + RegionSel + " " & _
                    " ORDER BY plaza "

        PromotorSQL = "SELECT DISTINCT RE.id_usuario " & _
                    "FROM Demos_Rutas_Eventos as RE " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario=RE.id_usuario " & _
                    "WHERE US.id_usuario<>'' " & _
                    " " + RegionSel + PlazaSel + " " & _
                    " ORDER BY RE.id_usuario "
    End Sub

    Sub CargarReporte()
        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("US.id_region", cmbRegion.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)
            PlazaSQL = Acciones.Slc.cmb("US.plaza", cmbCiudad.SelectedValue)

            CargaGrilla(ConexionSYM.localSqlServer, _
                        "SELECT 'Tiendas'Dato,SUM(demos)Cantidad " & _
                        "FROM Demos_Rutas_Eventos as H " & _
                        "INNER JOIN Usuarios as US ON H.id_usuario=US.id_usuario " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " " + RegionSQL + PlazaSQL + PromotorSQL + " " & _
                        "UNION ALL " & _
                        "SELECT 'Contactos'Dato,SUM(cantidad)Cantidad " & _
                        "FROM Demos_Historial as H " & _
                        "INNER JOIN Usuarios as US ON H.id_usuario=US.id_usuario " & _
                        "INNER JOIN Demos_Abordos_Historial_Dias_Det as HDET ON H.folio_historial=HDET.folio_historial " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " " + RegionSQL + PlazaSQL + PromotorSQL + " " & _
                        "UNION ALL " & _
                        "SELECT 'Abordo efectivo'Dato,SUM(cantidad)Cantidad " & _
                        "FROM Demos_Historial as H " & _
                        "INNER JOIN Usuarios as US ON H.id_usuario=US.id_usuario " & _
                        "INNER JOIN Demos_Abordos_Historial_Dias_Det as HDET ON H.folio_historial=HDET.folio_historial " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " " + RegionSQL + PlazaSQL + PromotorSQL + " and tipo_abordo=1 " & _
                        "UNION ALL " & _
                        "SELECT 'Ventas'as Dato,SUM(ventas)Cantidad " & _
                        "FROM Demos_Historial as H " & _
                        "INNER JOIN Usuarios as US ON H.id_usuario=US.id_usuario " & _
                        "INNER JOIN Demos_Productos_Historial_Dias_Det as HDET ON HDET.folio_historial=H.folio_historial " & _
                        "INNER JOIN Productos_Demos as PROD ON PROD.id_producto=HDET.id_producto " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " " + RegionSQL + PlazaSQL + PromotorSQL + " " & _
                        "UNION ALL " & _
                        "SELECT 'Venta '+nombre_grupo as Dato,SUM(ventas)Cantidad " & _
                        "FROM Demos_Historial as H " & _
                        "INNER JOIN Usuarios as US ON H.id_usuario=US.id_usuario " & _
                        "INNER JOIN Demos_Productos_Historial_Dias_Det as HDET ON HDET.folio_historial=H.folio_historial " & _
                        "INNER JOIN Productos_Demos as PROD ON PROD.id_producto=HDET.id_producto " & _
                        "INNER JOIN Tipo_Grupo_Demos as GRUP ON GRUP.tipo_grupo=PROD.tipo_grupo " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " " + RegionSQL + PlazaSQL + PromotorSQL + " " & _
                        "GROUP BY nombre_grupo " & _
                        "UNION ALL " & _
                        "SELECT 'Canjes' as Dato,SUM(toallas)Cantidad " & _
                        "FROM Demos_Historial as H " & _
                        "INNER JOIN Usuarios as US ON H.id_usuario=US.id_usuario " & _
                        "INNER JOIN Demos_Canjes_Historial_Det as HDET ON HDET.folio_historial=H.folio_historial " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " " + RegionSQL + PlazaSQL + PromotorSQL + " ", _
                        gridReporte)
        Else
            gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionSYM.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, PlazaSQL, "plaza", "plaza", cmbCiudad)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionSYM.localSqlServer, PlazaSQL, "plaza", "plaza", cmbCiudad)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionSYM.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Resumen " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbCiudad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCiudad.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionSYM.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub
End Class