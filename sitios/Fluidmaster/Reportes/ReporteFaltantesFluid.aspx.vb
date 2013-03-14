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

Partial Public Class ReporteFaltantesFluid
    Inherits System.Web.UI.Page

    Sub SQLCombo()
        Fluid.SQLsCombo(cmbPeriodo.SelectedValue, cmbRegion.SelectedValue, cmbEstado.SelectedValue, _
                     cmbPromotor.SelectedValue, cmbDistribuidor.SelectedValue)
    End Sub

    Sub CargarReporte()
        Dim RegionSQL, EstadoSQL, PromotorSQL, CadenaSQL, TiendaSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)
            EstadoSQL = Acciones.Slc.cmb("ES.id_estado", cmbEstado.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("CAD.id_cadena", cmbDistribuidor.SelectedValue)
            TiendaSQL = Acciones.Slc.cmb("H.id_tienda", cmbTienda.SelectedValue)

            CargaGrilla(ConexionFluidmaster.localSqlServer, _
                        "SELECT DISTINCT H.id_usuario,H.ciudad,H.nombre_region,H.nombre_estado,H.nombre_cadena,H.nombre,'Distribución' as tipo_tienda, " & _
                        "[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19] " & _
                        "FROM View_Historial as H " & _
                        "INNER JOIN (select folio_historial, id_producto, faltantes " & _
                        "FROM Distribuidor_Productos_Det WHERE faltantes<>0 ) PVT PIVOT(SUM(faltantes) FOR id_producto " & _
                        "IN([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19]))as HDET " & _
                        "ON HDET.folio_historial=H.folio_historial  " & _
                        "INNER JOIN View_Tiendas as TI ON H.id_tienda= H.id_tienda  " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + EstadoSQL + " " & _
                        " " + PromotorSQL + CadenaSQL + TiendaSQL + " " & _
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
        Me.pnlGrid.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.pnlGrid)
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
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim RegionSQL, EstadoSQL, PromotorSQL, CadenaSQL, TiendaSQL As String

            RegionSQL = Acciones.Slc.cmb("H.id_region", cmbRegion.SelectedValue)
            EstadoSQL = Acciones.Slc.cmb("H.id_estado", cmbEstado.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("H.id_cadena", cmbDistribuidor.SelectedValue)
            TiendaSQL = Acciones.Slc.cmb("H.id_tienda", cmbTienda.SelectedValue)

            Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                                                   "SELECT DISTINCT PROD.orden,PROD.id_producto,PROD.nombre_producto, SUM(ISNULL(H.faltantes,0))cantidad " & _
                        "from Productos as PROD " & _
                        "FULL JOIN (SELECT H.folio_historial, id_producto, faltantes " & _
                        "FROM Distribuidor_Productos_Det as HDET " & _
                        "INNER JOIN View_Historial as H ON HDET.folio_historial=H.folio_historial " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + EstadoSQL + " " & _
                        " " + PromotorSQL + CadenaSQL + TiendaSQL + " " & _
                        ")H ON PROD.id_producto=H.id_producto " & _
                        "GROUP BY PROD.orden,PROD.id_producto,nombre_producto ORDER BY orden ")

            For i = 0 To 18
                e.Row.Cells(i + 6).Text = Tabla.Rows(i)("cantidad")
            Next i

            Tabla.Dispose()
        End If
    End Sub

End Class