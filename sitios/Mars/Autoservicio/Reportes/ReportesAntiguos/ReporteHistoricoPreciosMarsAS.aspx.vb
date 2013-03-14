Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports procomlcd

Partial Public Class ReporteHistoricoPreciosMarsAS
    Inherits System.Web.UI.Page

    Dim RegionSel, CadenaSel, SupervisorSel, EjecutivoSel, Promotor As String
    Dim PeriodoSQL1, PeriodoSQL2, RegionSQL, SupervisorSQL, EjecutivoSQL, CadenaSQL, TiendaSQL, ProductoSQL As String
    Dim CamposTodos, ColumnasPeriodos As String
    Dim CamposProductosTodos, ColumnasProductos As String

    Sub SQLCombo()
        RegionSel = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)
        EjecutivoSel = Acciones.Slc.cmb("region_mars", cmbEjecutivo.SelectedValue)
        SupervisorSel = Acciones.Slc.cmb("id_supervisor", cmbSupervisor.SelectedValue)
        CadenaSel = Acciones.Slc.cmb("id_cadena", cmbCadena.SelectedValue)

        PeriodoSQL1 = "SELECT * FROM View_Historial_AS_Precios ORDER BY orden DESC"

        PeriodoSQL2 = "SELECT DISTINCT orden, nombre_periodo " & _
                     "FROM View_Historial_AS_Precios " & _
                     "ORDER BY orden DESC"

        RegionSQL = "SELECT DISTINCT id_region, nombre_region " & _
                    "FROM View_Historial_AS_Precios " & _
                    "WHERE id_region <>0 " + Promotor + " " & _
                    "ORDER BY nombre_region"

        EjecutivoSQL = "SELECT DISTINCT Ejecutivo, id_supervisor " & _
                    "FROM View_Historial_AS_Precios " & _
                    "WHERE Ejecutivo<>'' " & _
                    " " + RegionSel + Promotor + " ORDER BY id_supervisor"

        SupervisorSQL = "SELECT DISTINCT Supervisor, id_supervisor " & _
                     "FROM View_Historial_AS_Precios " & _
                     "WHERE id_usuario<>'' " & _
                     " " + RegionSel + Promotor + " ORDER BY id_supervisor"

        CadenaSQL = "SELECT DISTINCT id_cadena,nombre_cadena " & _
                    "FROM View_Historial_AS_Precios " & _
                    "WHERE id_cadena<>'' " & _
                    " " + RegionSel + EjecutivoSel + SupervisorSel + Promotor + " " & _
                    " RDER BY nombre_cadena"

        TiendaSQL = "SELECT DISTINCT id_tienda, nombre " & _
                   "FROM View_Historial_AS_Precios " & _
                   "WHERE id_tienda <>'' " & _
                   " " + RegionSel + CadenaSel + EjecutivoSel + SupervisorSel + Promotor + " " & _
                   " ORDER BY nombre"

        ProductoSQL = "SELECT * FROM AS_Precios_Productos WHERE tipo_producto=1"
    End Sub

    Sub CargarReporte()
        If cmbPeriodo1.SelectedValue <> "" And cmbPeriodo2.SelectedValue <> "" Then

            If Not cmbPeriodo1.SelectedValue = "" Then
                PeriodoSQL1 = "WHERE PER.orden >=" & cmbPeriodo1.SelectedValue & "" : Else
                Exit Sub : End If

            If Not cmbPeriodo2.SelectedValue = "" Then
                PeriodoSQL2 = "AND PER.orden <=" & cmbPeriodo2.SelectedValue & "" : Else
                Exit Sub : End If

            Periodos()

            Dim SQLGrilla As String = "select PROD.nombre_producto as Producto " & _
                                " " + CamposTodos + " " & _
                                "FROM AS_Precios_Productos as PROD " & _
                                " " + ColumnasPeriodos + " " & _
                                "ORDER BY PROD.id_producto"

            CargaGrilla(ConexionMars.localSqlServer, SQLGrilla, gridReporte)
            If cmbProducto.SelectedValue <> "" Then
                Productos(cmbProducto.SelectedValue)

                Dim TablaProductos As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                                                "SELECT REL.id_producto_comp, PROD.nombre_producto FROM AS_Precios_Productos_Relacion as REL " & _
                                                               "INNER JOIN AS_Precios_Productos as PROD ON PROD.id_producto= REL.id_producto_comp " & _
                                                               "WHERE REL.id_producto = " & cmbProducto.SelectedValue & " ORDER BY id_producto_comp")

                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                                       "SELECT DISTINCT PER.orden,PER.id_periodo,PER.nombre_periodo as Periodo " & _
                                                       " " + CamposProductosTodos + " " & _
                                                       "FROM AS_Precios_Historial as H " & _
                                                       "INNER JOIN Periodos_Nuevo as PER ON H.id_periodo = PER.id_periodo " & _
                                                       " " + ColumnasProductos + " " & _
                                                       " " + PeriodoSQL1 + PeriodoSQL2 + " " & _
                                                       "ORDER BY PER.orden")

                Dim Linea1, Linea2, Linea3 As New StringBuilder()
                Dim strXML As New StringBuilder()
                Dim strCategories As New StringBuilder()

                strXML.Append("<chart caption='Historico por Precio' rotateLabels='1' formatNumberScale='0' placeValuesInside='1' yAxisMinValue='50' " & _
                              " showValues='0' rotateValues='1' numberPrefix='$' exportEnabled='1' " & _
                              " exportHandler='../../../../FusionCharts/ExportHandlers/ASP_Net/FCExporter.aspx' exportAtClient='0' >")
                strCategories.Append("<categories>")

                If TablaProductos.Rows.Count = 1 Then
                    Linea1.Append("<dataset seriesName='" & TablaProductos.Rows(0)("nombre_producto") & "'>")

                    For i = 0 To tabla.Rows.Count - 1
                        strCategories.Append("<category name='" & tabla.Rows(i)("periodo") & "' />")
                        Linea1.Append("<set value='" & tabla.Rows(i)("Precio0") & "' />")
                    Next

                    strCategories.Append("</categories>")
                    Linea1.Append("</dataset>")

                    strXML.Append(strCategories.ToString() & Linea1.ToString())
                    strXML.Append("</chart>")
                End If

                If TablaProductos.Rows.Count = 2 Then
                    Linea1.Append("<dataset seriesName='" & TablaProductos.Rows(0)("nombre_producto") & "'>")
                    Linea2.Append("<dataset seriesName='" & TablaProductos.Rows(1)("nombre_producto") & "'>")

                    For i = 0 To tabla.Rows.Count - 1
                        strCategories.Append("<category name='" & tabla.Rows(i)("periodo") & "' />")
                        Linea1.Append("<set value='" & tabla.Rows(i)("Precio0") & "' />")
                        Linea2.Append("<set value='" & tabla.Rows(i)("Precio1") & "' />")
                    Next

                    strCategories.Append("</categories>")
                    Linea1.Append("</dataset>")
                    Linea2.Append("</dataset>")

                    strXML.Append(strCategories.ToString() & Linea1.ToString() & Linea2.ToString())
                    strXML.Append("</chart>")
                End If

                If TablaProductos.Rows.Count = 3 Then
                    Linea1.Append("<dataset seriesName='" & TablaProductos.Rows(0)("nombre_producto") & "'>")
                    Linea2.Append("<dataset seriesName='" & TablaProductos.Rows(1)("nombre_producto") & "'>")
                    Linea3.Append("<dataset seriesName='" & TablaProductos.Rows(2)("nombre_producto") & "'>")

                    For i = 0 To tabla.Rows.Count - 1
                        strCategories.Append("<category name='" & tabla.Rows(i)("periodo") & "' />")
                        Linea1.Append("<set value='" & tabla.Rows(i)("Precio0") & "' />")
                        Linea2.Append("<set value='" & tabla.Rows(i)("Precio1") & "' />")
                        Linea3.Append("<set value='" & tabla.Rows(i)("Precio2") & "' />")
                    Next

                    strCategories.Append("</categories>")
                    Linea1.Append("</dataset>")
                    Linea2.Append("</dataset>")
                    Linea3.Append("</dataset>")

                    strXML.Append(strCategories.ToString() & Linea1.ToString() & Linea2.ToString() & Linea3.ToString())
                    strXML.Append("</chart>")
                End If

                Dim outPut As String = ""
                outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/MSLine.swf", "", strXML.ToString(), "chart1", "670", "350", False, False)

                PanelFS.Controls.Clear()
                PanelFS.Visible = True
                PanelFS.Controls.Add(New LiteralControl(outPut))

            End If
        Else
            gridReporte.Visible = False
            PanelFS.Visible = False
        End If
    End Sub

    Sub Periodos()
        Dim P As String
        Dim Columnas(200), Campos(200) As String

        If Not cmbPeriodo1.SelectedValue = "" Then
            PeriodoSQL1 = "WHERE PER.orden >=" & cmbPeriodo1.SelectedValue & "" : Else
            Exit Sub : End If

        If Not cmbPeriodo2.SelectedValue = "" Then
            PeriodoSQL2 = "AND PER.orden <=" & cmbPeriodo2.SelectedValue & "" : Else
            Exit Sub : End If

        RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
        EjecutivoSQL = Acciones.Slc.cmb("REL.region_mars", cmbEjecutivo.SelectedValue)
        SupervisorSQL = Acciones.Slc.cmb("REL.id_supervisor", cmbSupervisor.SelectedValue)
        CadenaSQL = Acciones.Slc.cmb("TI.id_cadena", cmbCadena.SelectedValue)
        TiendaSQL = Acciones.Slc.cmb("TI.id_tienda", cmbTienda.SelectedValue)

        ''//busca los periodos
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT DISTINCT id_periodo, orden, nombre_periodo " & _
                                               "FROM Periodos_Nuevo PER " & _
                                               " " + PeriodoSQL1 + PeriodoSQL2 + " order by orden")
        If Tabla.Rows.Count > 0 Then
            For i = 0 To Tabla.Rows.Count - 1
                P = Tabla.Rows(i)("id_periodo")

                Columnas(i) = " FULL JOIN (select DISTINCT H.id_periodo, HDET.id_producto, '$' + CONVERT(varchar, CONVERT(money,ROUND(AVG(precio),2))) as Precio" & P & " FROM AS_Precios_Historial as H " & _
                            "INNER JOIN AS_Precios_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                            "INNER JOIN AS_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                            "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario = H.id_usuario " & _
                            "WHERE HDET.precio<>0 And H.id_periodo = '" & P & "' " & _
                            " " + RegionSQL + " " & _
                            " " + EjecutivoSQL + " " & _
                            " " + SupervisorSQL + " " & _
                            " " + CadenaSQL + " " & _
                            " " + TiendaSQL + " " & _
                            "GROUP BY H.id_periodo, HDET.id_producto) as Per" & P & " " & _
                            "ON PROD.id_producto = Per" & P & ".id_producto "

                Campos(i) = ", Per" & P & ".Precio" & P & " as '" & Tabla.Rows(i)("nombre_periodo") & "' "

                CamposTodos = CamposTodos + Campos(i)
                ColumnasPeriodos = ColumnasPeriodos + Columnas(i)
            Next
        End If

        tabla.Dispose()
    End Sub

    Public Function Productos(ByVal Producto As String) As Integer
        Dim ProdComp As String
        Dim ProductoNo(200), CamposProductos(200) As String

        RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
        EjecutivoSQL = Acciones.Slc.cmb("REL.region_mars", cmbEjecutivo.SelectedValue)
        SupervisorSQL = Acciones.Slc.cmb("REL.id_supervisor", cmbSupervisor.SelectedValue)
        CadenaSQL = Acciones.Slc.cmb("TI.id_cadena", cmbCadena.SelectedValue)
        TiendaSQL = Acciones.Slc.cmb("TI.id_tienda", cmbTienda.SelectedValue)

        ''//busca los periodos
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT * FROM AS_Precios_Productos_Relacion WHERE id_producto = " & Producto & " ORDER BY id_producto_comp")
        If Tabla.Rows.Count > 0 Then
            For i = 0 To Tabla.Rows.Count - 1
                ProdComp = Tabla.Rows(i)("id_producto_comp")

                ProductoNo(i) = " FULL JOIN (select DISTINCT H.id_periodo,PROD.nombre_producto, PROD.id_producto, ROUND(AVG(HDET.precio),2) as Precio " & _
                            "FROM AS_Precios_Historial as H " & _
                            "INNER JOIN Periodos as PER ON H.id_periodo = PER.id_periodo " & _
                            "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario = H.id_usuario " & _
                            "INNER JOIN AS_Precios_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                            "INNER JOIN AS_Precios_Productos as PROD ON PROD.id_producto = HDET.id_producto " & _
                            "INNER JOIN AS_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                            "WHERE PROD.id_producto = " & ProdComp & " " & _
                            " " + RegionSQL + " " & _
                            " " + EjecutivoSQL + " " & _
                            " " + SupervisorSQL + " " & _
                            " " + CadenaSQL + " " & _
                            " " + TiendaSQL + " " & _
                            "GROUP BY H.id_periodo,PROD.nombre_producto,PROD.id_producto)as Prod" & i & " ON Prod" & i & ".id_periodo = PER.id_periodo"

                CamposProductos(i) = ",Prod" & i & ".precio as Precio" & i & " "

                CamposProductosTodos = CamposProductosTodos + CamposProductos(i)
                ColumnasProductos = ColumnasProductos + ProductoNo(i)
            Next
        End If

        Tabla.Dispose()
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionMars.localSqlServer, PeriodoSQL1, "nombre_periodo", "orden", cmbPeriodo1)
            Combo.LlenaDrop(ConexionMars.localSqlServer, PeriodoSQL2, "nombre_periodo", "orden", cmbPeriodo2)
            Combo.LlenaDrop(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, EjecutivoSQL, "ejecutivo", "id_supervisor", cmbEjecutivo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, SupervisorSQL, "supervisor", "id_supervisor", cmbSupervisor)
            Combo.LlenaDrop(ConexionMars.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
            Combo.LlenaDrop(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)
            Combo.LlenaDrop(ConexionMars.localSqlServer, ProductoSQL, "nombre_producto", "id_producto", cmbProducto)

        End If
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, EjecutivoSQL, "ejecutivo", "id_supervisor", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, SupervisorSQL, "supervisor", "id_supervisor", cmbSupervisor)
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

    Private Sub cmbProducto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProducto.SelectedIndexChanged
        CargarReporte()
    End Sub

    Private Sub cmbPeriodo1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPeriodo1.SelectedIndexChanged
        SQLCombo()
        CargarReporte()
    End Sub

    Private Sub cmbEjecutivo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEjecutivo.SelectedIndexChanged
        cmbSupervisor.SelectedValue = ""

        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Private Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        cmbEjecutivo.SelectedValue = ""

        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub
End Class