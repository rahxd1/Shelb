Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteHistAgotCadenaMensualNM
    Inherits System.Web.UI.Page

    Dim PeriodoSQL, RegionSQL As String
    Dim Seleccion, ListaRegion, ListaPromotor, ListaCadena As String
    Dim CamposTodos, ColumnasPeriodos As String

    Sub SQLCombo()
        PeriodoSQL = "SELECT DISTINCT orden,anio,id_mes,nombre_mes " & _
                        "FROM View_Periodos_NM " & _
                        "ORDER BY anio DESC,id_mes DESC"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Regiones as REG " & _
                    "WHERE id_region <>0 " & _
                    " ORDER BY REG.nombre_region"
    End Sub

    Sub CargarReporte()
        If cmbPeriodo1.SelectedValue <> "" And cmbPeriodo2.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)

            Periodos()

            Dim SQLGrilla As String = "select DISTINCT CAD.nombre_cadena as Cadena " & _
                                " " + CamposTodos + " " & _
                                "FROM  NewMix_Historial as H " & _
                                "INNER JOIN NewMix_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                                "INNER JOIN Cadenas_tiendas as CAD ON CAD.id_cadena = TI.id_cadena " & _
                                "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                                " " + ColumnasPeriodos + " " & _
                                "WHERE TI.id_tienda <>0 " + RegionSQL + ""

            CargaGrilla(ConexionHerradura.localSqlServer, SQLGrilla, Me.gridReporte)

            ''//GRAFICA
            Dim SQLGrafica As String = "select DISTINCT PER.orden,PER.nombre_mes as Mes, ISNULL(Cad1.Tiendas1,0) as Tiendas1,  " & _
                            "ISNULL(Cad2.Tiendas2,0) as Tiendas2, ISNULL(Cad3.Tiendas3,0) as Tiendas3,ISNULL(Cad4.Tiendas4,0) as Tiendas4 " & _
                            "FROM NewMix_Historial as H  " & _
                            "INNER JOIN NewMix_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                            "INNER JOIN View_Periodos_NM as PER ON H.id_periodo = PER.id_periodo " & _
                            "FULL JOIN (select DISTINCT PER.orden, TI.id_cadena, COUNT(distinct H.id_tienda) as Tiendas1  " & _
                            "FROM NewMix_Historial as H  " & _
                            "INNER JOIN NewMix_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial  " & _
                            "INNER JOIN NewMix_Tiendas as TI ON TI.id_tienda = H.id_tienda  " & _
                            "INNER JOIN View_Periodos_NM as PER ON PER.id_periodo = H.id_periodo  " & _
                            "WHERE HDET.agotado = 1 And TI.id_cadena = 1 " + RegionSQL + " " & _
                            "AND PER.orden >= " & cmbPeriodo1.SelectedValue & " AND PER.orden<=" & cmbPeriodo2.SelectedValue & " " & _
                            "GROUP BY PER.orden, TI.id_cadena) as Cad1 ON PER.orden = Cad1.orden  " & _
                            "FULL JOIN (select DISTINCT PER.orden, TI.id_cadena, COUNT(distinct H.id_tienda) as Tiendas2  " & _
                            "FROM NewMix_Historial as H  " & _
                            "INNER JOIN NewMix_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial  " & _
                            "INNER JOIN NewMix_Tiendas as TI ON TI.id_tienda = H.id_tienda  " & _
                            "INNER JOIN View_Periodos_NM as PER ON PER.id_periodo = H.id_periodo  " & _
                            "WHERE HDET.agotado = 1 And TI.id_cadena = 2 " + RegionSQL + " " & _
                            "AND PER.orden >= " & cmbPeriodo1.SelectedValue & " AND PER.orden<=" & cmbPeriodo2.SelectedValue & " " & _
                            "GROUP BY PER.orden, TI.id_cadena) as Cad2 ON PER.orden = Cad2.orden  " & _
                            "FULL JOIN (select DISTINCT PER.orden, TI.id_cadena, COUNT(distinct H.id_tienda) as Tiendas3  " & _
                            "FROM NewMix_Historial as H  " & _
                            "INNER JOIN NewMix_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial  " & _
                            "INNER JOIN NewMix_Tiendas as TI ON TI.id_tienda = H.id_tienda  " & _
                            "INNER JOIN View_Periodos_NM as PER ON PER.id_periodo = H.id_periodo  " & _
                            "WHERE HDET.agotado = 1 And TI.id_cadena = 3 " + RegionSQL + " " & _
                            "AND PER.orden >= " & cmbPeriodo1.SelectedValue & " AND PER.orden<=" & cmbPeriodo2.SelectedValue & " " & _
                            "GROUP BY PER.orden, TI.id_cadena) as Cad3 ON PER.orden = Cad3.orden  " & _
                            "FULL JOIN (select DISTINCT PER.orden, TI.id_cadena, COUNT(distinct H.id_tienda) as Tiendas4  " & _
                            "FROM NewMix_Historial as H  " & _
                            "INNER JOIN NewMix_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial  " & _
                            "INNER JOIN NewMix_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                            "INNER JOIN View_Periodos_NM as PER ON PER.id_periodo = H.id_periodo " & _
                            "WHERE HDET.agotado = 1 And TI.id_cadena = 4 " + RegionSQL + " " & _
                            "AND PER.orden >= " & cmbPeriodo1.SelectedValue & " AND PER.orden<=" & cmbPeriodo2.SelectedValue & " " & _
                            "GROUP BY PER.orden, TI.id_cadena) as Cad4 ON PER.orden = Cad4.orden " & _
                            "WHERE PER.orden >= " & cmbPeriodo1.SelectedValue & " AND PER.orden<=" & cmbPeriodo2.SelectedValue & " " & _
                            " " + RegionSQL + " " & _
                            "ORDER BY PER.orden "

            ''//CARGA GRAFICA 
            Dim Tabla As DataTable = Tablas.Ver.DT(ConexionHerradura.localSqlServer, _
                                                   SQLGrafica)

            Dim Linea1, Linea2, Linea3, Linea4 As New StringBuilder()
            Dim strXML As New StringBuilder()
            Dim strCategories As New StringBuilder()

            strXML.Append("<chart palette='4' caption='Historico Agotamiento por Cadena' rotateLabels='1' formatNumberScale='0' placeValuesInside='1' rotateValues='1' >")
            strCategories.Append("<categories>")
            Linea1.Append("<dataset seriesName='OXXO'>")
            Linea2.Append("<dataset seriesName='FARMACIAS GUADALAJARA'>")
            Linea3.Append("<dataset seriesName='7 ELEVEN'>")
            Linea4.Append("<dataset seriesName='EXTRA'>")

            For i = 0 To tabla.Rows.Count - 1
                strCategories.Append("<category name='" & tabla.Rows(i)("mes") & "' />")
                Linea1.Append("<set value='" & tabla.Rows(i)("Tiendas1") & "' />")
                Linea2.Append("<set value='" & tabla.Rows(i)("Tiendas2") & "' />")
                Linea3.Append("<set value='" & tabla.Rows(i)("Tiendas3") & "' />")
                Linea4.Append("<set value='" & tabla.Rows(i)("Tiendas4") & "' />")
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

            PanelFS.Controls.Clear()
            PanelFS.Controls.Add(New LiteralControl(outPut))

            Tabla.Dispose()
        Else
            gridReporte.Visible = False
        End If
    End Sub

    Sub Periodos()
        Dim P As Integer
        Dim ColumnaDia(200), CamposDia(200) As String
        Dim Columnas(200), Campos(200) As String

        ''//busca los periodos
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionHerradura.localSqlServer, _
                                               "SELECT DISTINCT orden,anio,id_mes,nombre_mes " & _
                                               "FROM View_Periodos_NM " & _
                                               "WHERE orden>= " & cmbPeriodo1.SelectedValue & " " & _
                                               "AND orden<= " & cmbPeriodo2.SelectedValue & " " & _
                                               "ORDER BY anio DESC,id_mes DESC")

        If tabla.Rows.Count > 0 Then
            For i = 0 To tabla.Rows.Count - 1
                P = Tabla.Rows(i)("orden")

                Columnas(i) = " FULL JOIN (select DISTINCT PER.orden, TI.id_cadena, COUNT(distinct H.id_tienda) as Tiendas" & P & " FROM NewMix_Historial as H " & _
                            "INNER JOIN NewMix_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                            "INNER JOIN NewMix_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                            "INNER JOIN View_Periodos_NM as PER ON PER.id_periodo = H.id_periodo  " & _
                            "WHERE HDET.agotado = 1 And PER.orden = " & P & " " + RegionSQL + "" & _
                            "GROUP BY PER.orden, TI.id_cadena) as Per" & P & " " & _
                            "ON TI.id_cadena = Per" & P & ".id_cadena "

                Campos(i) = ", Per" & P & ".Tiendas" & P & " as '" & Tabla.Rows(i)("nombre_mes") & "' "

                CamposTodos = CamposTodos + Campos(i)
                ColumnasPeriodos = ColumnasPeriodos + Columnas(i)
            Next
        End If

        tabla.Dispose()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionHerradura.localSqlServer, PeriodoSQL, "nombre_mes", "orden", cmbPeriodo1)
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, PeriodoSQL, "nombre_mes", "orden", cmbPeriodo2)
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
        End If
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Agotamiento Historico por Cadena del " + cmbPeriodo1.SelectedItem.ToString + " al " + cmbPeriodo2.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbPeriodo2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPeriodo2.SelectedIndexChanged
        If Not cmbPeriodo1.SelectedValue = "" And Not cmbPeriodo2.SelectedValue = "" Then
            SQLCombo()
            CargarReporte()
        End If
    End Sub

    Private Sub cmbPeriodo1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPeriodo1.SelectedIndexChanged
        If Not cmbPeriodo2.SelectedValue = "" And Not cmbPeriodo1.SelectedValue = "" Then
            SQLCombo()
            CargarReporte()
        End If
    End Sub
End Class