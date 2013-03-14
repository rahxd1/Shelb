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

Partial Public Class ReporteMaterialPOPFluid
    Inherits System.Web.UI.Page

    Sub SQLCombo()
        Fluid.SQLsCombo(cmbPeriodo.SelectedValue, cmbRegion.SelectedValue, cmbEstado.SelectedValue, _
                     cmbPromotor.SelectedValue, cmbDistribuidor.SelectedValue)
    End Sub

    Sub CargarReporte()
        Dim RegionSQL, EstadoSQL, PromotorSQL, CadenaSQL, TiendaSQL, TiendaSQL2 As String

        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("H.id_region", cmbRegion.SelectedValue)
            EstadoSQL = Acciones.Slc.cmb("H.id_estado", cmbEstado.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("H.id_cadena", cmbDistribuidor.SelectedValue)
            TiendaSQL = Acciones.Slc.cmb("H.id_tienda", cmbTienda.SelectedValue)
            TiendaSQL2 = Acciones.Slc.cmb("H.id_tienda", cmbTienda.SelectedValue)

            CargaGrilla(ConexionFluidmaster.localSqlServer, _
                        "SELECT H.id_usuario,H.ciudad,H.nombre_region,H.nombre_estado,H.nombre_cadena,H.nombre,'Distribución' as tipo_tienda, " & _
                        "[1],[2],[3],[4],[5],[6],[7]" & _
                        "FROM View_Historial as H " & _
                        "INNER JOIN (select id_material, folio_historial, cantidad FROM Distribuidor_MaterialPOP_Det) PVT " & _
                        "PIVOT(SUM(cantidad) FOR id_material " & _
                        "IN([1],[2],[3],[4],[5],[6],[7]))as HDET ON HDET.folio_historial=H.folio_historial  " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + EstadoSQL + " " & _
                        " " + PromotorSQL + CadenaSQL + TiendaSQL + " " & _
                        "UNION ALL " & _
                        "SELECT H.id_usuario,H.ciudad,H.nombre_region,H.nombre_estado,H.nombre_cadena,H.nombre,'Subistribución' as tipo_tienda, " & _
                        "[1],[2],[3],[4],[5],[6],[7]" & _
                        "FROM View_Historial_Sub as H " & _
                        "INNER JOIN (select id_material, folio_historial, cantidad FROM Subdistribuidor_MaterialPOP_Det) PVT " & _
                        "PIVOT(SUM(cantidad) FOR id_material " & _
                        "IN([1],[2],[3],[4],[5],[6],[7]))as HDET ON HDET.folio_historial=H.folio_historial  " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + EstadoSQL + " " & _
                        " " + PromotorSQL + CadenaSQL + TiendaSQL2 + " " & _
                        "ORDER BY H.nombre_region, H.nombre_cadena,H.nombre ", _
                        Me.gridReporte)
        Else
            gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.PeriodoSQLCmb, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbDistribuidor)
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbDistribuidor)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbDistribuidor)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbDistribuidor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbDistribuidor)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte faltantes " + cmbPeriodo.SelectedItem.ToString() + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEstado.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbDistribuidor)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        Dim RegionSQL, EstadoSQL, PromotorSQL, CadenaSQL, TiendaSQL, TiendaSQL2 As String

        RegionSQL = Acciones.Slc.cmb("H.id_region", cmbRegion.SelectedValue)
        EstadoSQL = Acciones.Slc.cmb("H.id_estado", cmbEstado.SelectedValue)
        PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)
        CadenaSQL = Acciones.Slc.cmb("H.id_cadena", cmbDistribuidor.SelectedValue)
        TiendaSQL = Acciones.Slc.cmb("H.id_tienda", cmbTienda.SelectedValue)
        TiendaSQL2 = Acciones.Slc.cmb("H.id_tienda", cmbTienda.SelectedValue)

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                    "select id_material,nombre_material, SUM(cantidad) as cantidad " & _
                    "FROM(SELECT DISTINCT PROD.id_material,PROD.nombre_material, SUM(ISNULL(H.cantidad,0))cantidad " & _
                    "from Material_POP as PROD " & _
                    "FULL JOIN (SELECT H.folio_historial, id_material, cantidad " & _
                    "FROM Distribuidor_MaterialPOP_Det as HDET " & _
                    "INNER JOIN View_Historial as H ON HDET.folio_historial=H.folio_historial " & _
                    "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + EstadoSQL + " " & _
                    " " + PromotorSQL + CadenaSQL + TiendaSQL + " " & _
                    ")H ON PROD.id_material=H.id_material " & _
                    "GROUP BY PROD.nombre_material,PROD.id_material " & _
                    "UNION ALL " & _
                    "SELECT DISTINCT PROD.id_material,PROD.nombre_material, SUM(ISNULL(H.cantidad,0))cantidad " & _
                    "from Material_POP as PROD " & _
                    "FULL JOIN (SELECT H.folio_historial, id_material, cantidad " & _
                    "FROM Distribuidor_MaterialPOP_Det as HDET " & _
                    "INNER JOIN View_Historial_Sub as H ON HDET.folio_historial=H.folio_historial " & _
                    "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + EstadoSQL + " " & _
                    " " + PromotorSQL + CadenaSQL + TiendaSQL2 + " " & _
                    ")H ON PROD.id_material=H.id_material " & _
                    "GROUP BY PROD.nombre_material,PROD.id_material) as H " & _
                    "GROUP BY id_material,nombre_material ORDER BY id_material ")

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(6).Text = Tabla.Rows(0)("cantidad")
            e.Row.Cells(7).Text = Tabla.Rows(1)("cantidad")
            e.Row.Cells(8).Text = Tabla.Rows(2)("cantidad")
            e.Row.Cells(9).Text = Tabla.Rows(3)("cantidad")
            e.Row.Cells(10).Text = Tabla.Rows(4)("cantidad")
            e.Row.Cells(11).Text = Tabla.Rows(5)("cantidad")
            e.Row.Cells(12).Text = Tabla.Rows(6)("cantidad")
        End If

        Tabla.Dispose()
    End Sub

End Class