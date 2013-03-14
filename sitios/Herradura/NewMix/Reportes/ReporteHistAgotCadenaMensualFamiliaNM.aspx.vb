Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteHistAgotCadenaMensualFamiliaNM
    Inherits System.Web.UI.Page

    Dim PeriodoSQL1, PeriodoSQL2, RegionSQL As String
    Dim Seleccion, ListaRegion, ListaPromotor, ListaCadena As String
    Dim CamposTodos, ColumnasPeriodos As String

    Sub SQLCombo()
        PeriodoSQL1 = "SELECT DISTINCT no_mes, mes " & _
                         "FROM NewMix_Periodos " & _
                         " ORDER BY no_mes DESC"

        PeriodoSQL2 = "SELECT DISTINCT no_mes, mes " & _
                         "FROM NewMix_Periodos " & _
                         " ORDER BY no_mes DESC"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Regiones as REG " & _
                    "WHERE REG.id_region<>0 ORDER BY REG.nombre_region"
    End Sub

    Sub CargarReporte()
        If cmbPeriodo1.SelectedValue <> "" And cmbPeriodo2.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)

            Periodos()

            CargaGrilla(ConexionHerradura.localSqlServer, _
                        "select DISTINCT CAD.nombre_cadena as Cadena " & _
                        " " + CamposTodos + " " & _
                        "FROM  NewMix_Historial as H " & _
                        "INNER JOIN NewMix_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                        "INNER JOIN Cadenas_tiendas as CAD ON CAD.id_cadena = TI.id_cadena " & _
                        "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                        " " + ColumnasPeriodos + " " & _
                        "WHERE TI.id_tienda <>0 " + RegionSQL + "", Me.gridReporte)

            Dim SQLGrafica(5) As String
            Dim outPut(5) As String

            For iG = 1 To 5
                SQLGrafica(iG) = "select DISTINCT PER.no_mes,PER.mes as Mes, ISNULL(Cad1.Tiendas1,0) as Tiendas1,  " & _
                                "ISNULL(Cad2.Tiendas2,0) as Tiendas2, ISNULL(Cad3.Tiendas3,0) as Tiendas3,ISNULL(Cad4.Tiendas4,0) as Tiendas4 " & _
                                "FROM NewMix_Historial as H  " & _
                                "INNER JOIN NewMix_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                                "INNER JOIN NewMix_Periodos as PER ON H.id_periodo = PER.id_periodo " & _
                                "FULL JOIN (select DISTINCT PER.no_mes, TI.id_cadena, COUNT(distinct H.id_tienda) as Tiendas1  " & _
                                "FROM NewMix_Historial as H  " & _
                                "INNER JOIN NewMix_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial  " & _
                                "INNER JOIN NewMix_Tiendas as TI ON TI.id_tienda = H.id_tienda  " & _
                                "INNER JOIN NewMix_Periodos as PER ON PER.id_periodo = H.id_periodo  " & _
                                "INNER JOIN NewMix_Productos as PROD ON PROD.id_producto = HDET.id_producto " & _
                                "WHERE PROD.id_familia=" & iG & " AND HDET.agotado = 1 And TI.id_cadena = 1 " + RegionSQL + " " & _
                                "AND PER.no_mes >= " & cmbPeriodo1.SelectedValue & " AND PER.no_mes<=" & cmbPeriodo2.SelectedValue & " " & _
                                "GROUP BY PER.no_mes, TI.id_cadena) as Cad1 ON PER.no_mes = Cad1.no_mes  " & _
                                "FULL JOIN (select DISTINCT PER.no_mes, TI.id_cadena, COUNT(distinct H.id_tienda) as Tiendas2  " & _
                                "FROM NewMix_Historial as H  " & _
                                "INNER JOIN NewMix_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial  " & _
                                "INNER JOIN NewMix_Tiendas as TI ON TI.id_tienda = H.id_tienda  " & _
                                "INNER JOIN NewMix_Periodos as PER ON PER.id_periodo = H.id_periodo  " & _
                                "INNER JOIN NewMix_Productos as PROD ON PROD.id_producto = HDET.id_producto " & _
                                "WHERE PROD.id_familia=" & iG & " AND HDET.agotado = 1 And TI.id_cadena = 2 " + RegionSQL + " " & _
                                "AND PER.no_mes >= " & cmbPeriodo1.SelectedValue & " AND PER.no_mes<=" & cmbPeriodo2.SelectedValue & " " & _
                                "GROUP BY PER.no_mes, TI.id_cadena) as Cad2 ON PER.no_mes = Cad2.no_mes  " & _
                                "FULL JOIN (select DISTINCT PER.no_mes, TI.id_cadena, COUNT(distinct H.id_tienda) as Tiendas3  " & _
                                "FROM NewMix_Historial as H  " & _
                                "INNER JOIN NewMix_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial  " & _
                                "INNER JOIN NewMix_Tiendas as TI ON TI.id_tienda = H.id_tienda  " & _
                                "INNER JOIN NewMix_Periodos as PER ON PER.id_periodo = H.id_periodo  " & _
                                "INNER JOIN NewMix_Productos as PROD ON PROD.id_producto = HDET.id_producto " & _
                                "WHERE PROD.id_familia=" & iG & " AND HDET.agotado = 1 And TI.id_cadena = 3 " + RegionSQL + " " & _
                                "AND PER.no_mes >= " & cmbPeriodo1.SelectedValue & " AND PER.no_mes<=" & cmbPeriodo2.SelectedValue & " " & _
                                "GROUP BY PER.no_mes, TI.id_cadena) as Cad3 ON PER.no_mes = Cad3.no_mes  " & _
                                "FULL JOIN (select DISTINCT PER.no_mes, TI.id_cadena, COUNT(distinct H.id_tienda) as Tiendas4  " & _
                                "FROM NewMix_Historial as H  " & _
                                "INNER JOIN NewMix_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial  " & _
                                "INNER JOIN NewMix_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                                "INNER JOIN NewMix_Periodos as PER ON PER.id_periodo = H.id_periodo  " & _
                                "INNER JOIN NewMix_Productos as PROD ON PROD.id_producto = HDET.id_producto " & _
                                "WHERE PROD.id_familia=" & iG & " AND HDET.agotado = 1 And TI.id_cadena = 4 " + RegionSQL + " " & _
                                "AND PER.no_mes >= " & cmbPeriodo1.SelectedValue & " AND PER.no_mes<=" & cmbPeriodo2.SelectedValue & " " & _
                                "GROUP BY PER.no_mes, TI.id_cadena) as Cad4 ON PER.no_mes = Cad4.no_mes " & _
                                "WHERE PER.no_mes >= " & cmbPeriodo1.SelectedValue & " AND PER.no_mes<=" & cmbPeriodo2.SelectedValue & " " & _
                                " " + RegionSQL + " " & _
                                "ORDER BY PER.no_mes "

                ''//CARGA GRAFICA 
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionHerradura.localSqlServer, _
                                                       SQLGrafica(iG))

                Dim Linea1, Linea2, Linea3, Linea4 As New StringBuilder()
                Dim strXML As New StringBuilder()
                Dim strCategories As New StringBuilder()

                strXML.Append("<chart palette='4' caption='Historico Agotamiento por Cadena' rotateLabels='1' formatNumberScale='0' placeValuesInside='1' rotateValues='1' >")
                strCategories.Append("<categories>")
                Linea1.Append("<dataset seriesName='OXXO'>")
                Linea2.Append("<dataset seriesName='FARMACIAS GUADALAJARA'>")
                Linea3.Append("<dataset seriesName='7 ELEVEN'>")
                Linea4.Append("<dataset seriesName='EXTRA'>")

                For i = 0 To Tabla.Rows.Count - 1
                    strCategories.Append("<category name='" & Tabla.Rows(i)("mes") & "' />")
                    Linea1.Append("<set value='" & Tabla.Rows(i)("Tiendas1") & "' />")
                    Linea2.Append("<set value='" & Tabla.Rows(i)("Tiendas2") & "' />")
                    Linea3.Append("<set value='" & Tabla.Rows(i)("Tiendas3") & "' />")
                    Linea4.Append("<set value='" & Tabla.Rows(i)("Tiendas4") & "' />")
                Next

                strCategories.Append("</categories>")
                Linea1.Append("</dataset>")
                Linea2.Append("</dataset>")
                Linea3.Append("</dataset>")
                Linea4.Append("</dataset>")

                strXML.Append(strCategories.ToString() & Linea1.ToString() & Linea2.ToString() & Linea3.ToString() & Linea4.ToString())
                strXML.Append("</chart>")

                outPut(iG) = FusionCharts.RenderChartHTML("../../../../FusionCharts/MSLine.swf", "", strXML.ToString(), "chart1", "800", "350", False, False)
            Next iG

            PanelFS1.Controls.Clear()
            PanelFS1.Visible = True
            PanelFS1.Controls.Add(New LiteralControl(outPut(1)))

            PanelFS2.Controls.Clear()
            PanelFS2.Visible = True
            PanelFS2.Controls.Add(New LiteralControl(outPut(2)))

            PanelFS3.Controls.Clear()
            PanelFS3.Visible = True
            PanelFS3.Controls.Add(New LiteralControl(outPut(3)))

            PanelFS4.Controls.Clear()
            PanelFS4.Visible = True
            PanelFS4.Controls.Add(New LiteralControl(outPut(4)))

            PanelFS5.Controls.Clear()
            PanelFS5.Visible = True
            PanelFS5.Controls.Add(New LiteralControl(outPut(5)))
        Else
            gridReporte.Visible = False

            PanelFS1.Visible = False
            PanelFS2.Visible = False
            PanelFS3.Visible = False
            PanelFS4.Visible = False
            PanelFS5.Visible = False
        End If
    End Sub

    Sub Periodos()
        Dim P As Integer
        Dim ColumnaDia(200), CamposDia(200) As String
        Dim Columnas(200), Campos(200) As String

        ''//busca los periodos
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionHerradura.localSqlServer, _
                                               "SELECT DISTINCT no_mes, mes FROM NewMix_Periodos " & _
                                   "WHERE no_mes >= " & cmbPeriodo1.SelectedValue & " AND no_mes<= " & cmbPeriodo2.SelectedValue & "")

        If Tabla.Rows.Count > 0 Then
            For i = 0 To Tabla.Rows.Count - 1
                P = Tabla.Rows(i)("no_mes")

                Columnas(i) = " FULL JOIN (select DISTINCT PER.no_mes, TI.id_cadena, COUNT(distinct H.id_tienda) as Tiendas" & P & " FROM NewMix_Historial as H " & _
                            "INNER JOIN NewMix_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                            "INNER JOIN NewMix_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                            "INNER JOIN NewMix_Periodos as PER ON PER.id_periodo = H.id_periodo  " & _
                            "WHERE HDET.agotado = 1 And PER.no_mes = " & P & " " + RegionSQL + "" & _
                            "GROUP BY PER.no_mes, TI.id_cadena) as Per" & P & " " & _
                            "ON TI.id_cadena = Per" & P & ".id_cadena "

                Campos(i) = ", Per" & P & ".Tiendas" & P & " as '" & Tabla.Rows(i)("mes") & "' "

                CamposTodos = CamposTodos + Campos(i)
                ColumnasPeriodos = ColumnasPeriodos + Columnas(i)
            Next
        End If

        Tabla.Dispose()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionHerradura.localSqlServer, PeriodoSQL1, "mes", "no_mes", cmbPeriodo1)
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, PeriodoSQL2, "mes", "no_mes", cmbPeriodo2)
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
        SQLCombo()
        CargarReporte()
    End Sub

End Class