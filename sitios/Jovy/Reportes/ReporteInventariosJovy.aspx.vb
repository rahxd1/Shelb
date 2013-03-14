Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteInventariosJovy
    Inherits System.Web.UI.Page

    Dim Suma(3) As Integer

    Sub SQLCombo()
        Variables_Jovy.SQLsCombo(cmbPeriodo.SelectedValue, cmbRegion.SelectedValue, _
                     cmbPromotor.SelectedValue, cmbCadena.SelectedValue)
    End Sub

    Sub CargarReporte()
        Dim PromotorSQL, RegionSQL, CadenaSQL, TiendaSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("TI.id_cadena", cmbCadena.SelectedValue)
            TiendaSQL = Acciones.Slc.cmb("TI.id_tienda", cmbTienda.SelectedValue)

            CargaGrilla(ConexionJovy.localSqlServer, _
                        "select TI.nombre_region,H.id_usuario,TI.nombre_cadena,TI.nombre, PROD.id_marca, " & _
                        "PROD.nombre_marca, PROD.nombre_categoria, PROD.piezas, PROD.nombre_producto, " & _
                        "HDET.inventarios, ISNULL(HDET.inventarios_bodega, '0')inventarios_bodega, " & _
                        "ISNULL(HDET.inventarios,0)+ISNULL(HDET.inventarios_bodega, '0')total " & _
                        "FROM Jovy_Historial as H " & _
                        "INNER JOIN Jovy_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                        "INNER JOIN View_Jovy_Productos AS PROD ON PROD.id_producto = HDET.id_producto " & _
                        "INNER JOIN View_Jovy_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario = H.id_usuario " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        "AND HDET.inventarios <>0 AND PROD.id_marca=1 " & _
                        " " + RegionSQL + PromotorSQL + " " & _
                        " " + CadenaSQL + TiendaSQL + " " & _
                        "ORDER BY TI.nombre_region,TI.nombre_cadena,TI.nombre,PROD.nombre_categoria,PROD.id_marca", _
                        Me.gridReporte)
            Grafica()

        Else
            Me.pnlGrafica.Visible = True
            Me.gridReporte.Visible = False
        End If
    End Sub

    Sub Grafica()
        Dim PromotorSQL, RegionSQL, CadenaSQL, TiendaSQL As String

        RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
        PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)
        CadenaSQL = Acciones.Slc.cmb("TI.id_cadena", cmbCadena.SelectedValue)
        TiendaSQL = Acciones.Slc.cmb("TI.id_tienda", cmbTienda.SelectedValue)

        Dim SQLGrafica As String = "select PROD.nombre_marca, PROD.nombre_categoria, PROD.piezas, PROD.nombre_producto, " & _
                    "SUM(ISNULL(HDET.inventarios,0))+SUM(ISNULL(HDET.inventarios_bodega, '0')) as total " & _
                    "FROM Jovy_Historial as H " & _
                    "INNER JOIN Jovy_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                    "INNER JOIN View_Jovy_Productos AS PROD ON PROD.id_producto = HDET.id_producto " & _
                    "INNER JOIN View_Jovy_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario = H.id_usuario " & _
                    "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                    "AND HDET.inventarios <>0 AND PROD.id_marca=1 " & _
                    " " + RegionSQL + PromotorSQL + " " & _
                    " " + CadenaSQL + TiendaSQL + " " & _
                    "GROUP BY PROD.nombre_marca, PROD.nombre_categoria, PROD.piezas, PROD.nombre_producto " & _
                    "ORDER BY PROD.nombre_producto"


        CargaGrilla(ConexionJovy.localSqlServer, SQLGrafica, Me.gridResumen)

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, SQLGrafica)

        ''//CARGA GRAFICA
        Dim strXML As New StringBuilder()
        strXML.Append("<chart caption='Cantidad de inventarios por Productos' showborder='0' bgcolor='FFFFFF' bgalpha='100' rotateLabels='1'  placeValuesInside='1' rotateValues='1' >")

        For i = 0 To tabla.Rows.Count - 1
            strXML.Append("<set label='" & tabla.Rows(i)("nombre_producto") & "' value='" & tabla.Rows(i)("total") & "' />")
        Next i

        strXML.Append("</chart>")

        Dim outPut As String = ""
        outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/Column3D.swf", "", strXML.ToString(), "chart1", "800", "600", False, False)

        pnlGrafica.Visible = True
        pnlGrafica.Controls.Clear()
        pnlGrafica.Controls.Add(New LiteralControl(outPut))
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.PeriodoSQLCmb, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
            Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

            CargarReporte()
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCadena.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Inventarios " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Suma(0) = Suma(0) + e.Row.Cells(6).Text ''Suma inventario piso
            Suma(1) = Suma(1) + e.Row.Cells(7).Text ''Suma inventario bodega
            Suma(2) = Suma(2) + e.Row.Cells(8).Text ''Suma inventario totales
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(6).Text = Suma(0)
            e.Row.Cells(7).Text = Suma(1)
            e.Row.Cells(8).Text = Suma(2)
        End If
    End Sub
End Class