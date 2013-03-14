Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteHistoricoPreciosMarsAS2
    Inherits System.Web.UI.Page

    Dim CamposPeriodos, ColumnasPeriodos As String
    Dim ProductosGrilla, ProductoGrilla(4) As String

    Sub SQLCombo()
        MarsAS.SQLsCombo(cmbPeriodo1.SelectedValue, "", _
                         cmbRegion.SelectedValue, cmbEjecutivo.SelectedValue, _
                         cmbSupervisor.SelectedValue, "", _
                         cmbCadena.SelectedValue, "View_Historial_AS")
    End Sub

    Sub CargarReporte()
        Dim SQLDatos As String
        Dim RegionSQL, SupervisorSQL, EjecutivoSQL, CadenaSQL, TiendaSQL, ProductoSQL As String
        ProductoSQL = ""

        If cmbPeriodo1.SelectedValue <> "" And cmbPeriodo2.SelectedValue <> "" Then
            Periodos()

            RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
            EjecutivoSQL = Acciones.Slc.cmb("US.region_mars", cmbEjecutivo.SelectedValue)
            SupervisorSQL = Acciones.Slc.cmb("US.id_supervisor", cmbSupervisor.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("TI.id_cadena", cmbCadena.SelectedValue)
            TiendaSQL = Acciones.Slc.cmb("TI.id_tienda", cmbTienda.SelectedValue)

            If cmbProducto.SelectedValue <> "" Then
                Dim TablaProductos As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                                       "SELECT REL.id_producto_comp, PROD.nombre_producto FROM AS_Precios_Productos_Relacion as REL " & _
                                                       "INNER JOIN AS_Precios_Productos as PROD ON PROD.id_producto= REL.id_producto_comp " & _
                                                       "WHERE REL.id_producto = " & cmbProducto.SelectedValue & " ORDER BY id_producto_comp")
                Dim CompProd(TablaProductos.Rows.Count) As String
                If TablaProductos.Rows.Count > 0 Then
                    For i = 0 To TablaProductos.Rows.Count - 1
                        If i = 0 Then
                            CompProd(0) = " PROD.id_producto =" & TablaProductos.Rows(0)("id_producto_comp") & " "
                            ProductoGrilla(0) = ",[" & TablaProductos.Rows(0)("id_producto_comp") & "]"
                        Else
                            CompProd(i) = "OR PROD.id_producto =" & TablaProductos.Rows(i)("id_producto_comp") & " "
                            ProductoGrilla(i) = ",[" & TablaProductos.Rows(i)("id_producto_comp") & "]"
                        End If

                        ProductoSQL = ProductoSQL + CompProd(i)

                        If ProductoGrilla(i) <> ",[0]" Then
                            ProductosGrilla = ProductosGrilla + ProductoGrilla(i)
                        End If
                    Next

                    ProductoSQL = "WHERE (" + ProductoSQL + ")"
                End If

                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                                                "SELECT nombre_periodo" + ProductosGrilla + "" & _
                    "FROM (SELECT H.orden, HDET.id_producto, HDET.precio  " & _
                    "FROM AS_Precios_Historial as H  " & _
                    "INNER JOIN AS_Precios_Historial_Det as HDET ON H.folio_historial=HDET.folio_historial  " & _
                    "INNER JOIN AS_Tiendas as TI ON TI.id_tienda=H.id_tienda  " & _
                    "INNER JOIN View_Usuario_AS as US ON US.id_usuario=H.id_usuario  " & _
                    "WHERE TI.id_tienda<>0 ) PVT PIVOT(AVG(precio)  " & _
                    "FOR id_producto IN([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12], " & _
                    "[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26], " & _
                    "[27],[28],[29],[30],[31],[32],[33],[34],[35],[36],[37])) AS H  " & _
                    "INNER JOIN Periodos_Nuevo as PER ON PER.orden= H.orden " & _
                    "WHERE H.orden between " & cmbPeriodo1.SelectedValue & " and " & cmbPeriodo2.SelectedValue & "  " & _
                    "ORDER BY H.orden ")

                Dim Linea1, Linea2, Linea3, Linea4 As New StringBuilder()
                Dim strXML As New StringBuilder()
                Dim strCategories As New StringBuilder()

                strXML.Append("<chart caption='Historico por precio' rotateLabels='1' formatNumberScale='0' placeValuesInside='1' yAxisMinValue='50' " & _
                              " showValues='0' rotateValues='1' numberPrefix='$' exportEnabled='1' " & _
                              " exportHandler='../../../../FusionCharts/ExportHandlers/ASP_Net/FCExporter.aspx' exportAtClient='0' >")
                strCategories.Append("<categories>")

                Select Case TablaProductos.Rows.Count
                    Case Is = 1
                        Linea1.Append("<dataset seriesName='" & TablaProductos.Rows(0)("nombre_producto") & "'>")

                        For i = 0 To tabla.Rows.Count - 1
                            strCategories.Append("<category name='" & tabla.Rows(i)("nombre_periodo") & "' />")
                            Linea1.Append("<set value='" & tabla.Rows(i)("" & TablaProductos.Rows(0)("id_producto_comp") & "") & "' />")
                        Next

                        strCategories.Append("</categories>")
                        Linea1.Append("</dataset>")

                        strXML.Append(strCategories.ToString() + Linea1.ToString())
                        strXML.Append("</chart>")

                    Case Is = 2
                        Linea1.Append("<dataset seriesName='" & TablaProductos.Rows(0)("nombre_producto") & "'>")
                        Linea2.Append("<dataset seriesName='" & TablaProductos.Rows(1)("nombre_producto") & "'>")

                        For i = 0 To tabla.Rows.Count - 1
                            strCategories.Append("<category name='" & tabla.Rows(i)("nombre_periodo") & "' />")
                            Linea1.Append("<set value='" & tabla.Rows(i)("" & TablaProductos.Rows(0)("id_producto_comp") & "") & "' />")
                            Linea2.Append("<set value='" & tabla.Rows(i)("" & TablaProductos.Rows(1)("id_producto_comp") & "") & "' />")
                        Next

                        strCategories.Append("</categories>")
                        Linea1.Append("</dataset>")
                        Linea2.Append("</dataset>")

                        strXML.Append(strCategories.ToString() + Linea1.ToString() + Linea2.ToString())
                        strXML.Append("</chart>")

                    Case Is = 3
                        Linea1.Append("<dataset seriesName='" & TablaProductos.Rows(0)("nombre_producto") & "'>")
                        Linea2.Append("<dataset seriesName='" & TablaProductos.Rows(1)("nombre_producto") & "'>")
                        Linea3.Append("<dataset seriesName='" & TablaProductos.Rows(2)("nombre_producto") & "'>")

                        For i = 0 To tabla.Rows.Count - 1
                            strCategories.Append("<category name='" & tabla.Rows(i)("nombre_periodo") & "' />")
                            Linea1.Append("<set value='" & tabla.Rows(i)("" & TablaProductos.Rows(0)("id_producto_comp") & "") & "' />")
                            Linea2.Append("<set value='" & tabla.Rows(i)("" & TablaProductos.Rows(1)("id_producto_comp") & "") & "' />")
                            Linea3.Append("<set value='" & tabla.Rows(i)("" & TablaProductos.Rows(2)("id_producto_comp") & "") & "' />")
                        Next

                        strCategories.Append("</categories>")
                        Linea1.Append("</dataset>")
                        Linea2.Append("</dataset>")
                        Linea3.Append("</dataset>")

                        strXML.Append(strCategories.ToString() + Linea1.ToString() + Linea2.ToString() + Linea3.ToString())
                        strXML.Append("</chart>")

                    Case Is = 4
                        Linea1.Append("<dataset seriesName='" & TablaProductos.Rows(0)("nombre_producto") & "'>")
                        Linea2.Append("<dataset seriesName='" & TablaProductos.Rows(1)("nombre_producto") & "'>")
                        Linea3.Append("<dataset seriesName='" & TablaProductos.Rows(2)("nombre_producto") & "'>")
                        Linea4.Append("<dataset seriesName='" & TablaProductos.Rows(3)("nombre_producto") & "'>")

                        For i = 0 To tabla.Rows.Count - 1
                            strCategories.Append("<category name='" & tabla.Rows(i)("nombre_periodo") & "' />")
                            Linea1.Append("<set value='" & tabla.Rows(i)("" & TablaProductos.Rows(0)("id_producto_comp") & "") & "' />")
                            Linea2.Append("<set value='" & tabla.Rows(i)("" & TablaProductos.Rows(1)("id_producto_comp") & "") & "' />")
                            Linea3.Append("<set value='" & tabla.Rows(i)("" & TablaProductos.Rows(2)("id_producto_comp") & "") & "' />")
                            Linea4.Append("<set value='" & tabla.Rows(i)("" & TablaProductos.Rows(3)("id_producto_comp") & "") & "' />")
                        Next

                        strCategories.Append("</categories>")
                        Linea1.Append("</dataset>")
                        Linea2.Append("</dataset>")
                        Linea3.Append("</dataset>")
                        Linea4.Append("</dataset>")

                        strXML.Append(strCategories.ToString() + Linea1.ToString() + Linea2.ToString() + Linea3.ToString() + Linea4.ToString())
                        strXML.Append("</chart>")
                End Select

                Dim outPut As String = ""
                outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/MSLine.swf", "", strXML.ToString(), "chart1", "670", "350", False, False)

                PanelFS.Controls.Clear()
                PanelFS.Visible = True
                PanelFS.Controls.Add(New LiteralControl(outPut))
            Else
                ProductoSQL = "WHERE PROD.id_producto<>0 " : End If

            SQLDatos = "SELECT PROD.nombre_producto as Producto," + ColumnasPeriodos + " " & _
                        "FROM (SELECT H.orden, HDET.id_producto, HDET.precio " & _
                        "FROM AS_Precios_Historial as H " & _
                        "INNER JOIN AS_Precios_Historial_Det as HDET ON H.folio_historial=HDET.folio_historial " & _
                        "INNER JOIN AS_Tiendas as TI ON TI.id_tienda=H.id_tienda " & _
                        "INNER JOIN View_Usuario_AS as US ON US.id_usuario=H.id_usuario " & _
                        "WHERE TI.id_tienda<>0   " & _
                        " " + RegionSQL + EjecutivoSQL + SupervisorSQL + CadenaSQL + TiendaSQL + " " & _
                        ") PVT " & _
                        "PIVOT(AVG(precio) FOR orden  " & _
                        "IN(" + CamposPeriodos + ")) AS H " & _
                        "INNER JOIN AS_Precios_Productos as PROD ON PROD.id_producto = H.id_producto " & _
                        "" + ProductoSQL + " " & _
                        "ORDER BY PROD.id_producto "

            CargaGrilla(ConexionMars.localSqlServer, SQLDatos, gridReporte)

            If cmbProducto.SelectedValue <> "" Then
            End If
        Else
            gridReporte.Visible = False
            PanelFS.Visible = False
        End If
    End Sub

    Sub Periodos()
        Dim PeriodoSQL1, PeriodoSQL2 As String

        If Not cmbPeriodo1.SelectedValue = "" Then
            PeriodoSQL1 = "WHERE orden >=" & cmbPeriodo1.SelectedValue & "" : Else
            Exit Sub : End If

        If Not cmbPeriodo2.SelectedValue = "" Then
            PeriodoSQL2 = "AND orden <=" & cmbPeriodo2.SelectedValue & "" : Else
            Exit Sub : End If

        ''//busca los periodos
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                        "SELECT DISTINCT orden, nombre_periodo " & _
                                     "FROM Periodos_Nuevo " & _
                                    " " + PeriodoSQL1 + PeriodoSQL2 + " order by orden")

        Dim Columnas(tabla.Rows.Count), Campos(tabla.Rows.Count) As String
        If tabla.Rows.Count > 0 Then
            For i = 0 To tabla.Rows.Count - 1
                If i = 0 Then
                    Campos(0) = "[" & tabla.Rows(0)("orden") & "]" : Else
                    Campos(i) = ",[" & tabla.Rows(i)("orden") & "]"
                End If

                If i = 0 Then
                    Columnas(0) = "'$'+convert(nvarchar(10),ROUND([" & tabla.Rows(0)("orden") & "],2))'" & tabla.Rows(0)("nombre_periodo") & "'" : Else
                    Columnas(i) = ",'$'+convert(nvarchar(10),ROUND([" & tabla.Rows(i)("orden") & "],2))'" & tabla.Rows(i)("nombre_periodo") & "'"
                End If

                CamposPeriodos = CamposPeriodos + Campos(i)
                ColumnasPeriodos = ColumnasPeriodos + Columnas(i)
            Next
        End If

        tabla.Dispose()
    End Sub

    Public Function Productos(ByVal Producto As String) As Integer
        'Dim RegionSQL, SupervisorSQL, EjecutivoSQL, CadenaSQL, TiendaSQL, ProductoSQL As String

        'Dim ProdComp As String
        'Dim ProductoNo(200), CamposProductos(200) As String

        'If cmbRegion.SelectedValue ="" Then
        '    RegionSQL = "AND TI.id_region=" & cmbRegion.SelectedValue & " " : Else
        '    RegionSQL = "" : End If

        'If cmbEjecutivo.SelectedValue = "" Then
        '    EjecutivoSQL = "" : Else
        '    EjecutivoSQL = " AND REL.region_mars= '" & cmbEjecutivo.SelectedValue & "' " : End If

        'If cmbSupervisor.SelectedValue = "" Then
        '    SupervisorSQL = "" : Else
        '    SupervisorSQL = " AND REL.id_supervisor= '" & cmbSupervisor.SelectedValue & "' " : End If

        'If Not cmbCadena.SelectedValue = "" Then
        '    CadenaSQL = "AND TI.id_cadena = '" & cmbCadena.SelectedValue & "' " : Else
        '    CadenaSQL = "" : End If

        'If Not cmbTienda.SelectedValue = "" Then
        '    TiendaSQL = "AND TI.id_tienda=" & cmbTienda.SelectedValue & " " : Else
        '    TiendaSQL = "" : End If

        ' ''//busca los periodos
        'Dim cnn As New SqlConnection(ConexionMars.localSqlServer)
        'cnn.Open()
        'Dim cmdCap As New SqlCommand("SELECT * FROM AS_Precios_Productos_Relacion WHERE id_producto = " & Producto & " ORDER BY id_producto_comp", cnn)
        'Dim tabla As New DataTable
        'Dim da As New SqlDataAdapter(cmdCap)
        'da.Fill(tabla)

        'If tabla.Rows.Count > 0 Then
        '    For i = 0 To tabla.Rows.Count - 1
        '        ProdComp = tabla.Rows(i)("id_producto_comp")

        '        ProductoNo(i) = " FULL JOIN (select DISTINCT H.id_periodo,PROD.nombre_producto, PROD.id_producto, ROUND(AVG(HDET.precio),2) as Precio " & _
        '                    "FROM AS_Precios_Historial as H " & _
        '                    "INNER JOIN Periodos as PER ON H.id_periodo = PER.id_periodo " & _
        '                    "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario = H.id_usuario " & _
        '                    "INNER JOIN AS_Precios_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
        '                    "INNER JOIN AS_Precios_Productos as PROD ON PROD.id_producto = HDET.id_producto " & _
        '                    "INNER JOIN AS_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
        '                    "WHERE PROD.id_producto = " & ProdComp & " " & _
        '                    " " + RegionSQL + " " & _
        '                    " " + EjecutivoSQL + " " & _
        '                    " " + SupervisorSQL + " " & _
        '                    " " + CadenaSQL + " " & _
        '                    " " + TiendaSQL + " " & _
        '                    "GROUP BY H.id_periodo,PROD.nombre_producto,PROD.id_producto)as Prod" & i & " ON Prod" & i & ".id_periodo = PER.id_periodo"

        '        CamposProductos(i) = ",Prod" & i & ".precio as Precio" & i & " "

        '        CamposProductosTodos = CamposProductosTodos + CamposProductos(i)
        '        ColumnasProductos = ColumnasProductos + ProductoNo(i)
        '    Next
        'End If

        'cmdCap.Dispose()
        'tabla.Dispose()
        'da.Dispose()
        'cnn.Close()
        'cnn.Dispose()
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PeriodoSQLCmb, "nombre_periodo", "orden", cmbPeriodo1)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PeriodoSQLCmb, "nombre_periodo", "orden", cmbPeriodo2)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "id_supervisor", "id_supervisor", cmbSupervisor)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.ProductoPre_SQLCmb, "nombre_producto", "id_producto", cmbProducto)


        End If
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "id_supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

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

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

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
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Private Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        cmbEjecutivo.SelectedValue = ""

        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub
End Class