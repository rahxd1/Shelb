Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports procomlcd

Partial Public Class ReporteHistoricoPreciosMarsM
    Inherits System.Web.UI.Page

    Dim RegionSel, CadenaSel, Promotor As String
    Dim PeriodoSQL1, PeriodoSQL2, RegionSQL, CadenaSQL, TiendaSQL As String
    Dim CamposTodos, ColumnasPeriodos As String

    Sub SQLCombo()
        RegionSel = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)
        CadenaSel = Acciones.Slc.cmb("id_cadena", cmbCadena.SelectedValue)

        PeriodoSQL1 = "SELECT * FROM View_Historial_AS_Precios ORDER BY orden DESC"

        PeriodoSQL2 = "SELECT DISTINCT orden, nombre_periodo " & _
                     "FROM View_Historial_AS_Precios " & _
                     "ORDER BY orden DESC"

        RegionSQL = "SELECT DISTINCT id_region, nombre_region " & _
                    "FROM View_Historial_AS_Precios " & _
                    "WHERE id_region <>0 " + Promotor + " " & _
                    "ORDER BY nombre_region"

        CadenaSQL = "SELECT DISTINCT id_cadena,nombre_cadena " & _
                    "FROM View_Historial_AS_Precios " & _
                    "WHERE id_cadena<>'' " & _
                    " " + RegionSel + Promotor + " " & _
                    " RDER BY nombre_cadena"

        TiendaSQL = "SELECT DISTINCT id_tienda, nombre " & _
                   "FROM View_Historial_AS_Precios " & _
                   "WHERE id_tienda <>'' " & _
                   " " + RegionSel + CadenaSel + Promotor + " " & _
                   " ORDER BY nombre"
    End Sub

    Sub CargarReporte()
        If cmbPeriodo1.SelectedValue <> "" And cmbPeriodo2.SelectedValue <> "" Then
            If cmbPeriodo2.SelectedValue > cmbPeriodo1.SelectedValue Then
                RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
                CadenaSQL = Acciones.Slc.cmb("TI.id_cadena", cmbCadena.SelectedValue)
                TiendaSQL = Acciones.Slc.cmb("TI.id_tienda", cmbTienda.SelectedValue)

                Periodos()

                Dim SQLGrilla As String = "select nombre_producto as Producto " & _
                        " " + CamposTodos + " " & _
                        "FROM (select DISTINCT H.id_periodo, HDET.id_producto, HDET.precio_pieza " & _
                        "FROM May_Historial as H " & _
                        "INNER JOIN May_Tiendas as TI ON H.id_tienda = TI.id_tienda " & _
                        "INNER JOIN May_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                        "INNER JOIN Productos_Mayoreo as PROD ON PROD.id_producto=HDET.id_producto " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario = H.id_usuario  " & _
                        "WHERE HDET.precio_pieza<>0 AND PROD.reporte=1 " & _
                        " " + RegionSQL + CadenaSQL + TiendaSQL + " " & _
                        "UNION ALL " & _
                        "select DISTINCT H.id_periodo, CASE WHEN HDET.id_producto=1 then 1 else (CASE WHEN HDET.id_producto=2 then 2 else " & _
                        "(CASE WHEN HDET.id_producto=5 then 5 else(CASE WHEN HDET.id_producto=7 then 7 else 0 end)end)end)end as id_producto, HDET.precio_pieza " & _
                        "FROM May_Productos_Historial_Det as H  " & _
                        "INNER JOIN Periodos as PER ON H.id_periodo = PER.id_periodo " & _
                        "INNER JOIN May_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial  " & _
                        "INNER JOIN Productos_Mayoreo as PROD ON PROD.id_producto=HDET.id_producto  " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario = H.id_usuario  " & _
                        "WHERE HDET.precio_pieza<>0 AND PROD.reporte=1  " & _
                        " " + RegionSQL + CadenaSQL + TiendaSQL + " " & _
                        ") AS SourceTable " & _
                        "PIVOT(AVG(precio_pieza)FOR id_periodo IN (" + ColumnasPeriodos + ")) AS H " & _
                        "INNER JOIN Productos_Mayoreo as PROD ON PROD.id_producto= H.id_producto"

                CargaGrilla(ConexionMars.localSqlServer, SQLGrilla, gridReporte)

                Dim SQLGrafica As String = "select nombre_periodo, " & _
                        "ISNULL(round([1],2),0)[1], ISNULL(round([2],2),0)[2], " & _
                        "ISNULL(round([5],2),0)[5], ISNULL(round([7],2),0)[7] " & _
                        "FROM (select DISTINCT H.id_periodo, HDET.id_producto, HDET.precio_pieza " & _
                        "FROM May_Historial as H  " & _
                        "INNER JOIN Periodos as PER ON H.id_periodo = PER.id_periodo " & _
                        "INNER JOIN May_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial  " & _
                        "INNER JOIN Productos_Mayoreo as PROD ON PROD.id_producto=HDET.id_producto  " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario = H.id_usuario  " & _
                        "WHERE HDET.precio_pieza<>0 AND PROD.reporte=1  " & _
                        "AND PER.orden >=" & cmbPeriodo1.SelectedValue & " AND PER.orden <=" & cmbPeriodo2.SelectedValue & " " & _
                        " " + RegionSQL + CadenaSQL + TiendaSQL + " " & _
                        "UNION ALL " & _
                        "select DISTINCT H.id_periodo,  " & _
                        "CASE WHEN HDET.id_producto=1 then 1 else (CASE WHEN HDET.id_producto=2 then 2 else " & _
                        "(CASE WHEN HDET.id_producto=5 then 5 else(CASE WHEN HDET.id_producto=7 then 7 else 0 end)end)end)end as id_producto,  " & _
                        "HDET.precio_pieza " & _
                        "FROM May_Productos_Historial_Det as H  " & _
                        "INNER JOIN Periodos as PER ON H.id_periodo = PER.id_periodo " & _
                        "INNER JOIN May_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial  " & _
                        "INNER JOIN Productos_Mayoreo as PROD ON PROD.id_producto=HDET.id_producto  " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario = H.id_usuario  " & _
                        "WHERE HDET.precio_pieza<>0 AND PROD.reporte=1  " & _
                        "AND PER.orden >=" & cmbPeriodo1.SelectedValue & " AND PER.orden <=" & cmbPeriodo2.SelectedValue & " " & _
                        " " + RegionSQL + CadenaSQL + TiendaSQL + ") AS SourceTable " & _
                        "PIVOT(AVG(precio_pieza)FOR id_producto IN ([1], [2], [5], [7])) AS H " & _
                        "INNER JOIN (select DISTINCT id_periodo, nombre_periodo FROM Periodos)as PER ON PER.id_periodo = H.id_periodo"

                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, SQLGrafica)

                Dim Producto1, Producto2, Producto3, Producto4 As New StringBuilder()
                Dim strXML As New StringBuilder()
                Dim strCategories As New StringBuilder()

                strXML.Append("<chart caption='Historico por Precio' rotateLabels='1' formatNumberScale='1' placeValuesInside='150' yAxisMinValue='500' " & _
                              " showValues='1' rotateValues='1' numberPrefix='$' exportEnabled='1' " & _
                              " exportHandler='../../../../FusionCharts/ExportHandlers/ASP_Net/FCExporter.aspx' exportAtClient='0' >")
                strCategories.Append("<categories>")


                Producto1.Append("<dataset seriesName='PAL PERRO 1/25 KG'>")
                Producto2.Append("<dataset seriesName='PEDIGREE CACHORRO 1/20 KG'>")
                Producto3.Append("<dataset seriesName='PEDIGREE ADULTO NUTRICION COMPLETA 1/25 KG'>")
                Producto4.Append("<dataset seriesName='WHISKAS RECETA ORIGINAL / ORIGINAL RECIPE 1/20 KG'>")

                For i = 0 To Tabla.Rows.Count - 1
                    strCategories.Append("<category name='" & Tabla.Rows(i)("nombre_periodo") & "' />")
                    Producto1.Append("<set value='" & Tabla.Rows(i)("1") & "' />")
                    Producto2.Append("<set value='" & Tabla.Rows(i)("2") & "' />")
                    Producto3.Append("<set value='" & Tabla.Rows(i)("5") & "' />")
                    Producto4.Append("<set value='" & Tabla.Rows(i)("7") & "' />")
                Next

                strCategories.Append("</categories>")
                Producto1.Append("</dataset>")
                Producto2.Append("</dataset>")
                Producto3.Append("</dataset>")
                Producto4.Append("</dataset>")

                strXML.Append(strCategories.ToString() & Producto1.ToString() & Producto2.ToString() & Producto3.ToString() & Producto4.ToString())
                strXML.Append("</chart>")

                Dim outPut As String = ""
                outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/MSLine.swf", "", strXML.ToString(), "chart1", "670", "350", False, False)

                PanelFS.Controls.Clear()
                PanelFS.Visible = True
                PanelFS.Controls.Add(New LiteralControl(outPut))

                Tabla.Dispose()
            Else
                PanelFS.Visible = False
                gridReporte.Visible = False
            End If
        Else
            PanelFS.Visible = False
            gridReporte.Visible = False
        End If
    End Sub

    Sub Periodos()
        Dim Columnas(200), Campos(200) As String

        If Not cmbPeriodo1.SelectedValue = "" Then
            PeriodoSQL1 = "WHERE PER.orden >=" & cmbPeriodo1.SelectedValue & "" : Else
            Exit Sub : End If

        If Not cmbPeriodo2.SelectedValue = "" Then
            PeriodoSQL2 = "AND PER.orden <=" & cmbPeriodo2.SelectedValue & "" : Else
            Exit Sub : End If

        ''//busca los periodos
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT DISTINCT PER.id_periodo, PER.orden, PER.nombre_periodo " & _
                                     "FROM Periodos as H " & _
                                     "INNER JOIN Periodos as PER ON H.id_periodo = PER.id_periodo " & _
                                    " " + PeriodoSQL1 + PeriodoSQL2 + " order by PER.orden")
        If Tabla.Rows.Count > 0 Then
            For i = 0 To Tabla.Rows.Count - 1
                If i = 0 Then
                    Columnas(0) = "[" & Tabla.Rows(i)("id_periodo") & "]" : Else
                    Columnas(i) = ",[" & Tabla.Rows(i)("id_periodo") & "]" : End If

                Campos(i) = ",'$'+ISNULL(convert(nvarchar(8),cast(round([" & Tabla.Rows(i)("id_periodo") & "],2) as decimal(9,2))),0.00)[" & Tabla.Rows(i)("nombre_periodo") & "] "

                CamposTodos = CamposTodos + Campos(i)
                ColumnasPeriodos = ColumnasPeriodos + Columnas(i)
            Next
        End If

        Tabla.Dispose()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionMars.localSqlServer, PeriodoSQL1, "nombre_periodo", "orden", cmbPeriodo1)
            Combo.LlenaDrop(ConexionMars.localSqlServer, PeriodoSQL2, "nombre_periodo", "orden", cmbPeriodo2)
            Combo.LlenaDrop(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
            Combo.LlenaDrop(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)
        End If
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbTienda_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbTienda.SelectedIndexChanged
        SQLCombo()
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte historico precios del " + cmbPeriodo1.SelectedItem.ToString + " al " + cmbPeriodo2.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbPeriodo2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPeriodo2.SelectedIndexChanged
        SQLCombo()
        CargarReporte()
    End Sub

    Private Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCadena.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Private Sub cmbPeriodo1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPeriodo1.SelectedIndexChanged
        SQLCombo()
        CargarReporte()
    End Sub

End Class