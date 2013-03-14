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

Partial Public Class ReporteInventariosFluid
    Inherits System.Web.UI.Page

    Sub SQLCombo()
        Fluid.SQLsCombo(cmbPeriodo.SelectedValue, cmbRegion.SelectedValue, cmbEstado.SelectedValue, _
                     cmbPromotor.SelectedValue, cmbDistribuidor.SelectedValue)
    End Sub

    Sub CargarReporte()
        Dim RegionSQL, EstadoSQL, PromotorSQL, CadenaSQL, TiendaSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
            EstadoSQL = Acciones.Slc.cmb("TI.id_estado", cmbEstado.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("TI.id_cadena", cmbDistribuidor.SelectedValue)
            TiendaSQL = Acciones.Slc.cmb("TI.id_tienda", cmbTienda.SelectedValue)

            CargaGrilla(ConexionFluidmaster.localSqlServer, _
                        "SELECT PROD.nombre_producto, PROD.codigo, SUM(HDET.piso)+SUM(HDET.bodega)cantidad " & _
                        "from Distribuidor_Historial as H " & _
                        "INNER JOIN View_Tiendas as TI ON TI.id_tienda= H.id_tienda  " & _
                        "INNER JOIN Distribuidor_Productos_Det as HDET ON HDET.folio_historial=H.folio_historial " & _
                        "INNER JOIN Productos as PROD ON PROD.id_producto=HDET.id_producto " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + EstadoSQL + " " & _
                        " " + PromotorSQL + CadenaSQL + TiendaSQL + " " & _
                        " " + TiendaSQL + " " & _
                        "GROUP BY PROD.nombre_producto, PROD.codigo " & _
                        "ORDER BY PROD.nombre_producto, PROD.codigo ", _
                        Me.gridResumen)

            CargaGrilla(ConexionFluidmaster.localSqlServer, _
                        "SELECT H.id_usuario,TI.ciudad,TI.nombre_region,TI.nombre_estado,TI.nombre_cadena,TI.nombre, " & _
                        "[6],[106],[1006],[7],[107],[1007],[8],[108],[1008],[16],[116],[1016]," & _
                        "[2],[102],[1002],[3],[103],[1003],[9],[109],[1009], " & _
                        "[1],[101],[1001],[4],[104],[1004],[5],[105],[1005],[17],[117],[1017],  " & _
                        "[10],[110],[1010],[11],[111],[1011],[12],[112],[1012], " & _
                        "[13],[113],[1013],[14],[114],[1014],[15],[115],[1015] " & _
                        "FROM Distribuidor_Historial as H " & _
                        "INNER JOIN (select id_producto, folio_historial, piso FROM Distribuidor_Productos_Det) PVT " & _
                        "PIVOT(SUM(piso) FOR id_producto " & _
                        "IN([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17]))as HDET " & _
                        "ON HDET.folio_historial=H.folio_historial  " & _
                        "INNER JOIN (select 100+id_producto as id_producto, folio_historial, bodega " & _
                        " FROM Distribuidor_Productos_Det) PVT " & _
                        "PIVOT(SUM(bodega) FOR id_producto " & _
                        "IN([101],[102],[103],[104],[105],[106],[107],[108],[109],[110],[111],[112],[113],[114],[115],[116],[117]))as HDET2 " & _
                        "ON HDET2.folio_historial=H.folio_historial  " & _
                        "INNER JOIN (select 1000+id_producto as id_producto, folio_historial, bodega +piso as total " & _
                        " FROM Distribuidor_Productos_Det) PVT " & _
                        "PIVOT(SUM(total) FOR id_producto " & _
                        "IN([1001],[1002],[1003],[1004],[1005],[1006],[1007],[1008],[1009],[1010],[1011],[1012],[1013],[1014],[1015],[1016],[1017]))as HDET3 " & _
                        "ON HDET3.folio_historial=H.folio_historial  " & _
                        "INNER JOIN View_Tiendas as TI ON TI.id_tienda= H.id_tienda  " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + EstadoSQL + " " & _
                        " " + PromotorSQL + CadenaSQL + TiendaSQL + " " & _
                        " " + TiendaSQL + " " & _
                        "ORDER BY TI.nombre_region, TI.nombre_cadena,TI.nombre ", _
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
        Select Case e.Row.RowType
            Case DataControlRowType.Header
                For i = 0 To 5
                    e.Row.Cells(i).Visible = True
                Next i

                e.Row.Cells(5).ColumnSpan = 51

                For i = 6 To 55
                    e.Row.Cells(i).Visible = False : Next i

                e.Row.Cells(5).Controls.Clear()
                e.Row.Cells(5).Text = "<table style='text-align: center; width: 3825px; color: #FFFFFF;' border='0' cellpadding='0' cellspacing='0'>" & _
                 "<tr><td colspan='3' style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 231px; color: #FFFFFF;'>200AM133 Válvula de llenado</td>" & _
                "    <td colspan='3' style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 231px; color: #FFFFFF;'>200CM135 Combo</td>" & _
                "    <td colspan='3' style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 231px; color: #FFFFFF;'>200AK133 Combo</td>" & _
                "    <td colspan='3' style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 231px; color: #FFFFFF;'>400A133 Válvula de llenado</td>" & _
                "    <td colspan='3' style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 231px; color: #FFFFFF;'>400LS133 Válvula de llenado</td>" & _
                "    <td colspan='3' style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 231px; color: #FFFFFF;'>500135 Flapper</td>" & _
                "    <td colspan='3' style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 231px; color: #FFFFFF;'>501135 Flapper</td>" & _
                "    <td colspan='3' style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 231px; color: #FFFFFF;'>502135 Flapper</td>" & _
                "    <td colspan='3' style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 231px; color: #FFFFFF;'>555C135 Combo</td>" & _
                "    <td colspan='3'  style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 231px; color: #FFFFFF;'>507A133 Válvula de descarga</td>" & _
                "    <td colspan='3' style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 231px; color: #FFFFFF;'>681135 Palanca</td>" & _
                "    <td colspan='3' style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 231px; color: #FFFFFF;'>242135 Empaque</td>" & _
                "    <td colspan='3' style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 231px; color: #FFFFFF;'>400LSR133 linea 400LS BLISTER 1</td>" & _
                "    <td colspan='3' style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 231px; color: #FFFFFF;'>400LSCR133 linea 400LS BLISTER 2</td>" & _
                "    <td colspan='3' style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 231px; color: #FFFFFF;'>400LSCLR133 linea 400LS BLISTER 3</td>" & _
                "    <td colspan='3' style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 231px; color: #FFFFFF;'>503 Flapper</td>" & _
                "    <td colspan='3' style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 231px; color: #FFFFFF;'>747UK Válvula de llenado</td></tr>" & _
                "<tr><td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Piso</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Bodega</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Total</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Piso</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Bodega</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Total</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Piso</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Bodega</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Total</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Piso</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Bodega</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Total</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Piso</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Bodega</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Total</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Piso</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Bodega</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Total</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Piso</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Bodega</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Total</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Piso</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Bodega</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Total</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Piso</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Bodega</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Total</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Piso</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Bodega</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Total</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Piso</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Bodega</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Total</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Piso</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Bodega</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Total</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Piso</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Bodega</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Total</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Piso</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Bodega</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Total</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Piso</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Bodega</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Total</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Piso</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Bodega</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Total</td>" & _
                "    <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Piso</td>" & _
                "   <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Bodega</td>" & _
                "   <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 75px; color: #FFFFFF;'>Total</td></tr></table>"
        End Select
    End Sub

End Class