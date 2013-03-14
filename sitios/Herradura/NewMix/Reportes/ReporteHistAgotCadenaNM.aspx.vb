Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteHistAgotCadenaNM
    Inherits System.Web.UI.Page

    Dim PeriodoSel1, PeriodoSel2, PromotorSel, RegionSel, CadenaSel, TiendaSel As String
    Dim PeriodoSQL1, PeriodoSQL2, PromotorSQL, RegionSQL, CadenaSQL, TiendaSQL As String
    Dim ListaRegion, ListaPromotor, ListaCadena As String
    Dim CamposTodos, ColumnasPeriodos As String

    Sub SQLCombo()
        RegionSel = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)

        PeriodoSQL1 = "SELECT id_periodo, nombre_periodo " & _
                         "FROM NewMix_Periodos " & _
                         " ORDER BY id_periodo DESC"

        PeriodoSQL2 = "SELECT id_periodo, nombre_periodo " & _
                         "FROM NewMix_Periodos " & _
                         " ORDER BY id_periodo DESC"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Regiones as REG " & _
                    "WHERE REG.id_region<>0 ORDER BY REG.nombre_region"

        PromotorSQL = "SELECT DISTINCT id_usuario " & _
                    "FROM NewMix_Rutas_Eventos as RE " & _
                    "INNER JOIN NewMix_Tiendas as TI ON RE.id_tienda = TI.id_tienda " & _
                    "WHERE id_usuario<>'' " & _
                    " " + RegionSel + " " & _
                    " ORDER BY id_usuario "

        CadenaSQL = "SELECT DISTINCT CAD.id_cadena, CAD.nombre_cadena " & _
                    "FROM Cadenas_tiendas as CAD " & _
                    " ORDER BY CAD.nombre_cadena"

        TiendaSQL = "SELECT DISTINCT TI.id_tienda, TI.nombre " & _
                    "FROM NewMix_Tiendas as TI " & _
                    " ORDER BY TI.nombre"
    End Sub

    Sub CargarReporte()
        If cmbPeriodo1.SelectedValue <> "" And cmbPeriodo2.SelectedValue <> "" Then
            PeriodoSQL1 = Acciones.Slc.cmb("H.id_periodo", cmbPeriodo1.SelectedValue)
            RegionSQL = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)
            TiendaSQL = Acciones.Slc.cmb("TI.id_tienda", cmbTienda.SelectedValue)

            Periodos()

            CargaGrilla(ConexionHerradura.localSqlServer, _
                        "select DISTINCT CAD.nombre_cadena as Cadena " & _
                        " " + CamposTodos + " " & _
                        "FROM  NewMix_Historial as H " & _
                        "INNER JOIN NewMix_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                        "INNER JOIN Cadenas_tiendas as CAD ON CAD.id_cadena = TI.id_cadena " & _
                        "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                        " " + ColumnasPeriodos + " ", Me.gridReporte)

            Dim SQLGrafica As String = "select DISTINCT PER.id_periodo,PER.nombre_periodo as Periodo, ISNULL(Cad1.Tiendas1,0) as Tiendas1,  " & _
                            "ISNULL(Cad2.Tiendas2,0) as Tiendas2, ISNULL(Cad3.Tiendas3,0) as Tiendas3,ISNULL(Cad4.Tiendas4,0) as Tiendas4 " & _
                            "FROM NewMix_Historial as H  " & _
                            "INNER JOIN NewMix_Periodos as PER ON H.id_periodo = PER.id_periodo " & _
                            "FULL JOIN (select DISTINCT H.id_periodo, TI.id_cadena, COUNT(distinct H.id_tienda) as Tiendas1  " & _
                            "FROM NewMix_Historial as H  " & _
                            "INNER JOIN NewMix_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial  " & _
                            "INNER JOIN NewMix_Tiendas as TI ON TI.id_tienda = H.id_tienda  " & _
                            "WHERE HDET.agotado = 1 And TI.id_cadena = 1  " & _
                            "AND H.id_periodo >= " & cmbPeriodo1.SelectedValue & " AND H.id_periodo<=" & cmbPeriodo2.SelectedValue & " " & _
                            "GROUP BY H.id_periodo, TI.id_cadena) as Cad1 ON H.id_periodo = Cad1.id_periodo  " & _
                            "FULL JOIN (select DISTINCT H.id_periodo, TI.id_cadena, COUNT(distinct H.id_tienda) as Tiendas2  " & _
                            "FROM NewMix_Historial as H  " & _
                            "INNER JOIN NewMix_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial  " & _
                            "INNER JOIN NewMix_Tiendas as TI ON TI.id_tienda = H.id_tienda  " & _
                            "WHERE HDET.agotado = 1 And TI.id_cadena = 2  " & _
                            "AND H.id_periodo >= " & cmbPeriodo1.SelectedValue & " AND H.id_periodo<=" & cmbPeriodo2.SelectedValue & " " & _
                            "GROUP BY H.id_periodo, TI.id_cadena) as Cad2 ON H.id_periodo = Cad2.id_periodo  " & _
                            "FULL JOIN (select DISTINCT H.id_periodo, TI.id_cadena, COUNT(distinct H.id_tienda) as Tiendas3  " & _
                            "FROM NewMix_Historial as H  " & _
                            "INNER JOIN NewMix_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial  " & _
                            "INNER JOIN NewMix_Tiendas as TI ON TI.id_tienda = H.id_tienda  " & _
                            "WHERE HDET.agotado = 1 And TI.id_cadena = 3  " & _
                            "AND H.id_periodo >= " & cmbPeriodo1.SelectedValue & " AND H.id_periodo<=" & cmbPeriodo2.SelectedValue & " " & _
                            "GROUP BY H.id_periodo, TI.id_cadena) as Cad3 ON H.id_periodo = Cad3.id_periodo  " & _
                            "FULL JOIN (select DISTINCT H.id_periodo, TI.id_cadena, COUNT(distinct H.id_tienda) as Tiendas4  " & _
                            "FROM NewMix_Historial as H  " & _
                            "INNER JOIN NewMix_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial  " & _
                            "INNER JOIN NewMix_Tiendas as TI ON TI.id_tienda = H.id_tienda  " & _
                            "WHERE HDET.agotado = 1 And TI.id_cadena = 4  " & _
                            "AND H.id_periodo >= " & cmbPeriodo1.SelectedValue & " AND H.id_periodo<=" & cmbPeriodo2.SelectedValue & " " & _
                            "GROUP BY H.id_periodo, TI.id_cadena) as Cad4 ON H.id_periodo = Cad4.id_periodo " & _
                            "WHERE H.id_periodo >= " & cmbPeriodo1.SelectedValue & " AND H.id_periodo<=" & cmbPeriodo2.SelectedValue & " " & _
                            "ORDER BY PER.id_periodo "

            ''//CARGA GRAFICA 
            Dim Tabla As DataTable = Tablas.Ver.DT(ConexionHerradura.localSqlServer, SQLGrafica)

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
                strCategories.Append("<category name='" & Tabla.Rows(i)("periodo") & "' />")
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

            Dim outPut As String = ""
            outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/MSLine.swf", "", strXML.ToString(), "chart1", "800", "350", False, False)

            PanelFS.Controls.Clear()
            PanelFS.Visible = True
            PanelFS.Controls.Add(New LiteralControl(outPut))
        Else
            gridReporte.Visible = False
            PanelFS.Visible = False
        End If
    End Sub

    Sub Periodos()
        Dim P As Integer
        Dim ColumnaDia(200), CamposDia(200) As String
        Dim Columnas(200), Campos(200) As String

        ''//busca los periodos
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionHerradura.localSqlServer, _
                                               "SELECT * from NewMix_Periodos as H " & _
                                    "WHERE H.id_periodo >= " & cmbPeriodo1.SelectedValue & " AND H.id_periodo<= " & cmbPeriodo2.SelectedValue & "")

        If tabla.Rows.Count > 0 Then
            For i = 0 To tabla.Rows.Count - 1
                P = tabla.Rows(i)("id_periodo")

                Columnas(i) = " FULL JOIN (select DISTINCT H.id_periodo, TI.id_cadena, COUNT(distinct H.id_tienda) as Tiendas" & P & " FROM NewMix_Historial as H " & _
                            "INNER JOIN NewMix_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                            "INNER JOIN NewMix_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                            "WHERE HDET.agotado = 1 And H.id_periodo = " & P & " " & _
                            "GROUP BY H.id_periodo, TI.id_cadena) as Per" & P & " " & _
                            "ON TI.id_cadena = Per" & P & ".id_cadena "

                Campos(i) = ", Per" & P & ".Tiendas" & P & " as '" & tabla.Rows(i)("nombre_periodo") & "' "

                CamposTodos = CamposTodos + Campos(i)
                ColumnasPeriodos = ColumnasPeriodos + Columnas(i)
            Next
        End If

        tabla.Dispose()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionHerradura.localSqlServer, PeriodoSQL1, "nombre_periodo", "id_periodo", cmbPeriodo1)
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, PeriodoSQL2, "nombre_periodo", "id_periodo", cmbPeriodo2)
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionHerradura.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionHerradura.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionHerradura.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

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