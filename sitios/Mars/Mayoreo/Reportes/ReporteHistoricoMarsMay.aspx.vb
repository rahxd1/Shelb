Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteHistoricoMarsMay
    Inherits System.Web.UI.Page

    Dim CamposPeriodos, ColumnasPeriodos As String
    Dim CamposPeriodos2, ColumnasPeriodos2 As String

    Sub SQLCombo()
        MarsMay.SQLsCombo(cmbPeriodo1.SelectedValue, "", "", _
                         cmbRegion.SelectedValue, cmbEjecutivo.SelectedValue, _
                         cmbPromotor.SelectedValue, cmbCadena.SelectedValue)
    End Sub

    Sub CargarReporte()
        If cmbPeriodo1.SelectedValue <> "" And cmbPeriodo2.SelectedValue <> "" Then
            VerGrilla(3, gridReporte1)
            VerGrilla(2, gridReporte2)
            VerGrafica(3, "Autoservicio", GraficaAS)
            VerGrafica(2, "Mostrador", GraficaMos)
        Else
            gridReporte1.Visible = False
            gridReporte2.Visible = False
            GraficaAS.Visible = False
            GraficaMos.Visible = False
        End If
    End Sub

    Public Function VerGrilla(ByVal Tipo_tienda As String, ByVal Grilla As GridView) As Integer
        Dim RegionSQL, EjecutivoSQL, PromotorSQL, CadenaSQL, TiendaSQL, ProductoSQL As String
        ProductoSQL = ""

        ColumnasPeriodos = ""
        CamposPeriodos = ""
        ColumnasPeriodos2 = ""
        CamposPeriodos2 = ""
        Periodos()

        RegionSQL = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)
        EjecutivoSQL = Acciones.Slc.cmb("ruta_ejecutivo", cmbEjecutivo.SelectedValue)
        PromotorSQL = Acciones.Slc.cmb("id_usuario", cmbPromotor.SelectedValue)
        CadenaSQL = Acciones.Slc.cmb("id_cadena", cmbCadena.SelectedValue)
        TiendaSQL = Acciones.Slc.cmb("id_tienda", cmbTienda.SelectedValue)

        CargaGrilla(ConexionMars.localSqlServer, "SELECT PROD.nombre_producto as Producto, " & _
                    " " + ColumnasPeriodos + "  " & _
                    "FROM (SELECT tipo_tienda,id_producto,orden, precio_pieza " & _
                    "FROM View_Historial_Mayoreo WHERE precio_pieza<>0 " & _
                    " " + RegionSQL + EjecutivoSQL + PromotorSQL + CadenaSQL + TiendaSQL + " " & _
                    "UNION ALL " & _
                    "SELECT tipo_tienda,id_producto,orden, precio_pieza " & _
                    "FROM View_Historial_May_Ant WHERE precio_pieza<>0 " & _
                    " " + RegionSQL + EjecutivoSQL + PromotorSQL + CadenaSQL + TiendaSQL + " " & _
                    "UNION ALL " & _
                    "SELECT tipo_tienda,id_producto,orden, precio_pieza " & _
                    "FROM View_Historial_PM_Ant WHERE precio_pieza<>0 " & _
                    " " + RegionSQL + EjecutivoSQL + PromotorSQL + CadenaSQL + TiendaSQL + ") PVT " & _
                    "PIVOT(AVG(precio_pieza) FOR orden " & _
                    "IN(" + CamposPeriodos + ")) AS H " & _
                    "INNER JOIN Productos_Mayoreo as PROD ON H.id_producto=PROD.id_producto " & _
                    "WHERE H.tipo_tienda=" & Tipo_tienda & " " & _
                    "ORDER BY nombre_producto ", Grilla)
    End Function

    Public Function VerGrafica(ByVal Tipo_tienda As Integer, ByVal Tipo As String, ByVal Pnl As Panel) As Integer
        Dim RegionSQL, EjecutivoSQL, PromotorSQL, CadenaSQL, TiendaSQL As String
        RegionSQL = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)
        EjecutivoSQL = Acciones.Slc.cmb("ruta_ejecutivo", cmbEjecutivo.SelectedValue)
        PromotorSQL = Acciones.Slc.cmb("id_usuario", cmbPromotor.SelectedValue)
        CadenaSQL = Acciones.Slc.cmb("id_cadena", cmbCadena.SelectedValue)
        TiendaSQL = Acciones.Slc.cmb("id_tienda", cmbTienda.SelectedValue)

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT H.orden, PER.nombre_periodo," & _
                                    "ROUND([1],2)[1] ,ROUND([2],2)[2] ,ROUND([5],2)[5] ,ROUND([7],2)[7]   " & _
                                    "FROM (SELECT tipo_tienda,id_producto,orden, precio_pieza  " & _
                                    "FROM View_Historial_Mayoreo  " & _
                                    "WHERE precio_pieza<>0  " & _
                                    "AND orden Between " & cmbPeriodo1.SelectedValue & " and " & cmbPeriodo2.SelectedValue & " " & _
                                    " " + RegionSQL + EjecutivoSQL + PromotorSQL + CadenaSQL + TiendaSQL + " " & _
                                    "UNION ALL  " & _
                                    "SELECT tipo_tienda,id_producto,orden, precio_pieza  " & _
                                    "FROM View_Historial_May_Ant  " & _
                                    "WHERE precio_pieza<>0  " & _
                                    "AND orden Between " & cmbPeriodo1.SelectedValue & " and " & cmbPeriodo2.SelectedValue & " " & _
                                    " " + RegionSQL + EjecutivoSQL + PromotorSQL + CadenaSQL + TiendaSQL + " " & _
                                    "UNION ALL  " & _
                                    "SELECT tipo_tienda,id_producto,orden, precio_pieza  " & _
                                    "FROM View_Historial_PM_Ant  " & _
                                    "WHERE precio_pieza<>0 " & _
                                    "AND orden Between " & cmbPeriodo1.SelectedValue & " and " & cmbPeriodo2.SelectedValue & " " & _
                                    " " + RegionSQL + EjecutivoSQL + PromotorSQL + CadenaSQL + TiendaSQL + ") PVT  " & _
                                    "PIVOT(AVG(precio_pieza) FOR id_producto IN([1],[2],[5],[7])) AS H  " & _
                                    "INNER join Periodos_Nuevo as PER ON PER.orden=H.orden " & _
                                    "WHERE H.tipo_tienda=" & Tipo_tienda & " ORDER BY orden")

        Dim Linea1, Linea2, Linea3, Linea4 As New StringBuilder()
        Dim strXML As New StringBuilder()
        Dim strCategories As New StringBuilder()

        strXML.Append("<chart caption='Precio historico " & Tipo & "' YAxisMinValue='200' numDivLines='0' rotateLabels='1' formatNumberScale='0' placeValuesInside='1' " & _
                      " showValues='0' rotateValues='1' numberPrefix='$' exportEnabled='1' " & _
                      " exportHandler='../../../../FusionCharts/ExportHandlers/ASP_Net/FCExporter.aspx' exportAtClient='0' >")
        strCategories.Append("<categories>")

        Linea1.Append("<dataset seriesName='PAL ® PERRO 25 kg'><br /> ")
        Linea2.Append("<dataset seriesName='PEDIGREE ® CACHORRO 20 kg'><br /> ")
        Linea3.Append("<dataset seriesName='PEDIGREE ® ADULTO NUTRICION COMPLETA 25 kg'><br /> ")
        Linea4.Append("<dataset seriesName='WHISKAS ® RECETA ORIGINAL / ORIGINAL RECIPE 20 kg'><br /> ")

        For i = 0 To tabla.Rows.Count - 1
            strCategories.Append("<category name='" & tabla.Rows(i)("nombre_periodo") & "' />")
            Linea1.Append("<set value='" & tabla.Rows(i)("1") & "' />")
            Linea2.Append("<set value='" & tabla.Rows(i)("2") & "' />")
            Linea3.Append("<set value='" & tabla.Rows(i)("5") & "' />")
            Linea4.Append("<set value='" & tabla.Rows(i)("7") & "' />")
        Next

        strCategories.Append("</categories>")
        Linea1.Append("</dataset>")
        Linea2.Append("</dataset>")
        Linea3.Append("</dataset>")
        Linea4.Append("</dataset>")

        strXML.Append(strCategories.ToString() & Linea1.ToString() & Linea2.ToString() & Linea3.ToString() & Linea4.ToString())
        strXML.Append("</chart>")

        Dim outPut As String = ""
        outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/MSLine.swf", "", strXML.ToString(), "chart1", "670", "350", False, False)

        Pnl.Controls.Clear()
        Pnl.Visible = True
        Pnl.Controls.Add(New LiteralControl(outPut))
    End Function

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
                                               "SELECT * FROM Periodos_Nuevo " & _
                                               " " + PeriodoSQL1 + PeriodoSQL2 + " order by orden")

        Dim Columnas(tabla.Rows.Count), Campos(tabla.Rows.Count) As String
        Dim Columnas2(tabla.Rows.Count), Campos2(tabla.Rows.Count) As String

        If tabla.Rows.Count > 0 Then
            For i = 0 To tabla.Rows.Count - 1
                If i = 0 Then
                    Campos(0) = "[" & tabla.Rows(0)("orden") & "]"
                    Campos2(0) = "[" & tabla.Rows(0)("orden") & "]" : Else
                    Campos(i) = ",[" & tabla.Rows(i)("orden") & "]"
                    Campos2(i) = ",[" & tabla.Rows(i)("orden") & "]"
                End If

                If i = 0 Then
                    Columnas(0) = "'$'+convert(nvarchar(10),ROUND([" & tabla.Rows(0)("orden") & "],2))'" & tabla.Rows(0)("nombre_periodo") & "'"
                    Columnas2(0) = "ROUND([" & tabla.Rows(0)("orden") & "],2)[" & tabla.Rows(0)("orden") & "] " : Else
                    Columnas(i) = ",'$'+convert(nvarchar(10),ROUND([" & tabla.Rows(i)("orden") & "],2))'" & tabla.Rows(i)("nombre_periodo") & "'"
                    Columnas2(i) = ",ROUND([" & tabla.Rows(i)("orden") & "],2)[" & tabla.Rows(i)("orden") & "] "
                End If

                CamposPeriodos = CamposPeriodos + Campos(i)
                CamposPeriodos2 = CamposPeriodos2 + Campos2(i)
                ColumnasPeriodos = ColumnasPeriodos + Columnas(i)
                ColumnasPeriodos2 = ColumnasPeriodos2 + Columnas2(i)
            Next
        End If

        tabla.Dispose()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.PeriodoSQLCmb, "nombre_periodo", "orden", cmbPeriodo1)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.PeriodoSQLCmb, "nombre_periodo", "orden", cmbPeriodo2)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.EjecutivoSQLCmb, "ejecutivo", "ruta_ejecutivo", cmbEjecutivo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)
        End If
    End Sub

    Private Sub cmbPeriodo1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPeriodo1.SelectedIndexChanged
        SQLCombo()
        CargarReporte()
    End Sub

    Private Sub cmbPeriodo2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPeriodo2.SelectedIndexChanged
        SQLCombo()
        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.EjecutivoSQLCmb, "ejecutivo", "ruta_ejecutivo", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Private Sub cmbEjecutivo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEjecutivo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Private Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCadena.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

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
        Me.pnlGrid.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.pnlGrid)
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

End Class