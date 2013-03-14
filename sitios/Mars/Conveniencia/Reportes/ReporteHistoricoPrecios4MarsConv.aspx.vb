Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteHistoricoPrecios4MarsConv
    Inherits System.Web.UI.Page

    Dim CamposTodos, ColumnasPeriodos As String

    Sub SQLCombo()
        MarsConv.SQLsCombo(cmbPeriodo1.SelectedValue, "", cmbRegion.SelectedValue, "", _
                            cmbCadena.SelectedValue, "View_Historial_Conv_Pre")
    End Sub

    Sub CargarReporte()
        Dim RegionSQL, CadenaSQL As String

        If cmbPeriodo1.SelectedValue <> "" And cmbPeriodo2.SelectedValue <> "" Then
            If cmbPeriodo2.SelectedValue > cmbPeriodo1.SelectedValue Then
                RegionSQL = Acciones.Slc.cmb("US.id_region", cmbRegion.SelectedValue)
                CadenaSQL = Acciones.Slc.cmb("H.id_cadena", cmbCadena.SelectedValue)

                Periodos()

                Dim SQLGrilla As String = "select nombre_producto as Producto " & _
                        " " + CamposTodos + " " & _
                        "FROM (select DISTINCT H.orden, HDET.id_producto, HDET.precio " & _
                        "FROM Conv_Historial_Precios as H " & _
                        "INNER JOIN Conv_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                        "INNER JOIN Conv_Productos as PROD ON PROD.id_producto=HDET.id_producto " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario = H.id_usuario  " & _
                        "WHERE HDET.precio<>0 AND PROD.reporte=1 " & _
                        " " + RegionSQL + CadenaSQL + " " & _
                        ") AS SourceTable " & _
                        "PIVOT(AVG(precio)FOR orden IN (" + ColumnasPeriodos + ")) AS H " & _
                        "INNER JOIN Conv_Productos as PROD ON PROD.id_producto= H.id_producto"

                CargaGrilla(ConexionMars.localSqlServer, SQLGrilla, gridReporte)

                Dim SQLGrafica As String = "select nombre_periodo, " & _
                        "ISNULL(round([111],2),0)[111], ISNULL(round([119],2),0)[119], " & _
                        "ISNULL(round([126],2),0)[126] " & _
                        "FROM (select DISTINCT H.orden, HDET.id_producto, HDET.precio " & _
                        "FROM Conv_Historial_Precios as H  " & _
                        "INNER JOIN Periodos_Nuevo PER ON H.orden = PER.orden " & _
                        "INNER JOIN Conv_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial  " & _
                        "INNER JOIN Conv_Productos as PROD ON PROD.id_producto=HDET.id_producto  " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario = H.id_usuario  " & _
                        "WHERE HDET.precio<>0 AND PROD.reporte=1  " & _
                        "AND PER.orden >=" & cmbPeriodo1.SelectedValue & " AND PER.orden <=" & cmbPeriodo2.SelectedValue & " " & _
                        " " + RegionSQL + CadenaSQL + ") AS SourceTable " & _
                        "PIVOT(AVG(precio)FOR id_producto IN ([111], [119], [126])) AS H " & _
                        "INNER JOIN (select DISTINCT orden, nombre_periodo FROM Periodos_Nuevo)as PER ON PER.orden = H.orden"

                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, SQLGrafica)

                Dim Producto1, Producto2, Producto3 As New StringBuilder()
                Dim strXML As New StringBuilder()
                Dim strCategories As New StringBuilder()

                strXML.Append("<chart caption='Historico por Precio' rotateLabels='1' formatNumberScale='100' placeValuesInside='150' yAxisMinValue='500' " & _
                              " showValues='100' rotateValues='1' numberPrefix='$' exportEnabled='1' " & _
                              " exportHandler='../../../../FusionCharts/ExportHandlers/ASP_Net/FCExporter.aspx' exportAtClient='0' >")
                strCategories.Append("<categories>")


                Producto1.Append("<dataset seriesName='WHISKAS RECETA ORIGINAL 1/20KG'>")
                Producto2.Append("<dataset seriesName='PEDIGREE ADULTO NUTRICION COMPLETA 1/25k'>")
                Producto3.Append("<dataset seriesName='PEDIGREE CACHORRO ETAPA 1 1/20 KG'>")

                For i = 0 To tabla.Rows.Count - 1
                    strCategories.Append("<category name='" & tabla.Rows(i)("nombre_periodo") & "' />")
                    Producto1.Append("<set value='" & tabla.Rows(i)("111") & "' />")
                    Producto2.Append("<set value='" & tabla.Rows(i)("119") & "' />")
                    Producto3.Append("<set value='" & tabla.Rows(i)("126") & "' />")
                Next

                strCategories.Append("</categories>")
                Producto1.Append("</dataset>")
                Producto2.Append("</dataset>")
                Producto3.Append("</dataset>")

                strXML.Append(strCategories.ToString() & Producto1.ToString() & Producto2.ToString() & Producto3.ToString())
                strXML.Append("</chart>")

                Dim outPut As String = ""
                outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/MSLine.swf", "", strXML.ToString(), "chart1", "670", "350", False, False)

                PanelFS.Controls.Clear()
                PanelFS.Visible = True
                PanelFS.Controls.Add(New LiteralControl(outPut))
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
        Dim PeriodoSQL1, PeriodoSQL2 As String
        Dim Columnas(200), Campos(200) As String

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
        If tabla.Rows.Count > 0 Then
            For i = 0 To tabla.Rows.Count - 1
                If i = 0 Then
                    Columnas(0) = "[" & tabla.Rows(i)("orden") & "]" : Else
                    Columnas(i) = ",[" & tabla.Rows(i)("orden") & "]" : End If

                Campos(i) = ",'$'+ISNULL(convert(nvarchar(8),cast(round([" & tabla.Rows(i)("orden") & "],2) as decimal(9,2))),0.00)[" & tabla.Rows(i)("nombre_periodo") & "] "

                CamposTodos = CamposTodos + Campos(i)
                ColumnasPeriodos = ColumnasPeriodos + Columnas(i)
            Next
        End If

        tabla.Dispose()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_Conv.PeriodoSQLCmb, "nombre_periodo", "orden", cmbPeriodo1)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_Conv.PeriodoSQLCmb, "nombre_periodo", "orden", cmbPeriodo2)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_Conv.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_Conv.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        End If
    End Sub

    Private Sub cmbPeriodo1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPeriodo1.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_Conv.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_Conv.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
    End Sub

    Private Sub cmbPeriodo2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPeriodo2.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_Conv.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Private Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCadena.SelectedIndexChanged
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
End Class