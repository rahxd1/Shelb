Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReportePreciosJovy
    Inherits System.Web.UI.Page

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
                        "select H.id_periodo, PROD.id_marca, PROD.nombre_marca, PROD.nombre_categoria, " & _
                        "PROD.piezas, PROD.id_producto, PROD.nombre_producto, " & _
                        "MIN(HDET.precio)PrecioMin, MAX(HDET.precio) PrecioMax, " & _
                        "Round(AVG(HDET.precio),2) as PrecioProm " & _
                        "FROM Jovy_Historial as H " & _
                        "INNER JOIN Jovy_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                        "INNER JOIN View_Jovy_Productos AS PROD ON PROD.id_producto = HDET.id_producto " & _
                        "INNER JOIN View_Jovy_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario = H.id_usuario " & _
                        "WHERE H.id_periodo = '" & cmbPeriodo.SelectedValue & "' " & _
                        "AND HDET.precio <>'0' " & _
                        " " + RegionSQL + PromotorSQL + " " & _
                        " " + CadenaSQL + TiendaSQL + " " & _
                        "GROUP BY H.id_periodo,PROD.id_marca, PROD.nombre_marca, PROD.nombre_categoria, PROD.piezas, PROD.id_producto, PROD.nombre_producto " & _
                        "ORDER BY PROD.id_marca, PROD.nombre_categoria, PROD.nombre_producto ", _
                        Me.gridReporte)

            'GraficaCaptura()
        Else
            gridReporte.Visible = False
            pnlGrafica.Visible = False
        End If
    End Sub

    Sub GraficaCaptura()
        Dim SQLGrafica As String = "select id_producto,ROUND([57],2)[57],ROUND([58],2)[58],ROUND([59],2)[59],ROUND([60],2)[60],ROUND([61],2)[61],ROUND([62],2)[62] " & _
                        "from(SELECT DISTINCT H.id_periodo,HDET.id_producto,HDET.precio " & _
                        "FROM Jovy_Historial as H " & _
                        "INNER JOIN Jovy_Productos_Historial_Det as HDET ON HDET.folio_historial=H.folio_historial " & _
                        "INNER JOIN View_Jovy_Productos as PROD ON PROD.id_producto=HDET.id_producto " & _
                        "WHERE H.id_periodo between 57 and 62 AND PROD.id_marca=1)H " & _
                        "PIVOT(AVG(precio) FOR id_periodo " & _
                        "IN([57],[58],[59],[60],[61],[62]))as SEG"

        ''//CARGA GRAFICA 
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, SQLGrafica)

        Dim Linea1, Linea2, Linea3, Linea4 As New StringBuilder()
        Dim strXML As New StringBuilder()
        Dim strCategories As New StringBuilder()

        strXML.Append("<chart palette='4' caption='Historico bitacora captura' rotateLabels='1' formatNumberScale='0' placeValuesInside='1' rotateValues='1' >")
        strCategories.Append("<categories>")
        Linea1.Append("<dataset seriesName='Periodo 1'>")
        Linea2.Append("<dataset seriesName='Periodo 2'>")
        Linea3.Append("<dataset seriesName='Periodo 3'>")
        Linea4.Append("<dataset seriesName='Periodo 4'>")

        For i = 0 To Tabla.Rows.Count - 1
            strCategories.Append("<category name='" & Tabla.Rows(i)("id_producto") & "' />")
            Linea1.Append("<set value='" & Tabla.Rows(i)("57") & "' />")
            Linea2.Append("<set value='" & Tabla.Rows(i)("58") & "' />")
            Linea3.Append("<set value='" & Tabla.Rows(i)("59") & "' />")
            Linea4.Append("<set value='" & Tabla.Rows(i)("60") & "' />")
        Next

        strCategories.Append("</categories>")
        Linea1.Append("</dataset>")
        Linea2.Append("</dataset>")
        Linea3.Append("</dataset>")
        Linea4.Append("</dataset>")

        strXML.Append(strCategories.ToString() & Linea1.ToString() & Linea2.ToString() & Linea3.ToString() & Linea4.ToString())
        strXML.Append("</chart>")

        Dim outPut As String = ""
        outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/MSLine.swf", "", strXML.ToString(), "chart1", "800", "350", False, False)

        pnlGrafica.Controls.Clear()
        pnlGrafica.Visible = True
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Precios " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

End Class