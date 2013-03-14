Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteDemosHB
    Inherits System.Web.UI.Page

    Dim PromotorSel, RegionSel, CadenaSel As String
    Dim PeriodoSQL, PromotorSQL, RegionSQL, CadenaSQL, TiendaSQL As String

    Sub SQLCombo()
        If cmbRegion.SelectedValue <> "" Then
            If cmbRegion.SelectedValue <> 0 Then
                RegionSel = "AND REG.id_region=" & cmbRegion.SelectedValue & " " : Else
                RegionSel = "" : End If : End If

        If Not cmbPromotor.Text = "" Then
            PromotorSel = "AND RUT.id_usuario='" & cmbPromotor.SelectedValue & "'": Else
            PromotorSel = "" : End If

        If Not cmbCadena.Text = "" Then
            CadenaSel = "AND CAD.id_cadena=" & cmbCadena.SelectedValue & " " : Else
            CadenaSel = "" : End If

        PeriodoSQL = "SELECT id_periodo, nombre_periodo " & _
                    "FROM HawaiianBanana_Periodos ORDER BY fecha_inicio DESC"

        RegionSQL = "SELECT DISTINCT nombre_region, id_region " & _
                    "FROM Regiones WHERE id_region<>0 ORDER BY nombre_region"

        PromotorSQL = "SELECT DISTINCT RUT.id_usuario " & _
                    "FROM HawaiianBanana_CatRutas AS RUT " & _
                    "INNER JOIN HawaiianBanana_Tiendas AS TI ON TI.id_tienda = RUT.id_tienda " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    "WHERE TI.estatus = 1 " & _
                    " " + RegionSel + " " & _
                    " ORDER BY RUT.id_usuario"

        CadenaSQL = "SELECT DISTINCT CAD.id_cadena, CAD.nombre_cadena " & _
                    "FROM HawaiianBanana_CatRutas AS RUT " & _
                    "INNER JOIN HawaiianBanana_Tiendas AS TI ON TI.id_tienda = RUT.id_tienda " & _
                    "INNER JOIN Cadenas_Tiendas AS CAD ON TI.id_cadena = CAD.id_cadena " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    "WHERE TI.estatus = 1 " & _
                    " " + RegionSel + " " & _
                    " " + PromotorSel + " " & _
                    " ORDER BY CAD.nombre_cadena"

        TiendaSQL = "SELECT DISTINCT RUT.id_tienda, TI.nombre " & _
                    "FROM HawaiianBanana_CatRutas AS RUT " & _
                    "INNER JOIN HawaiianBanana_Tiendas AS TI ON TI.id_tienda = RUT.id_tienda " & _
                    "INNER JOIN Cadenas_Tiendas AS CAD ON TI.id_cadena = CAD.id_cadena " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    " " + RegionSel + " " & _
                    " " + PromotorSel + " " & _
                    " " + CadenaSel + " " & _
                    " ORDER BY TI.nombre"
    End Sub

    Sub CargarReporte()
        If cmbRegion.SelectedValue <> 0 Then
            RegionSQL = "AND REG.id_region=" & cmbRegion.SelectedValue & " " : Else
            RegionSQL = "" : End If

        If Not cmbPromotor.SelectedValue = "" Then
            PromotorSQL = "AND H.id_usuario='" & cmbPromotor.SelectedValue & "' " : Else
            PromotorSQL = "" : End If

        If Not cmbCadena.SelectedValue = "" Then
            CadenaSQL = "AND TI.id_cadena=" & cmbCadena.SelectedValue & " " : Else
            CadenaSQL = "" : End If

        If Not cmbTienda.SelectedValue = "" Then
            TiendaSQL = "AND TI.id_tienda=" & cmbTienda.SelectedValue & " " : Else
            TiendaSQL = "" : End If


        Dim SQL As String = "SELECT MAR.nombre_marca, SUM(HDET.cantidad_demos) as Totales " & _
                            "FROM HawaiianBanana_Historial as H " & _
                            "INNER JOIN HawaiianBanana_Competencia_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial " & _
                            "INNER JOIN HawaiianBanana_Tiendas as TI ON TI.id_tienda= H.id_tienda  " & _
                            "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region  " & _
                            "INNER JOIN HawaiianBanana_Marcas as MAR ON MAR.id_marca = HDET.id_marca " & _
                            "INNER JOIN Cadenas_Tiendas as CAD ON TI.id_cadena= CAD.id_cadena " & _
                            "WHERE MAR.id_marca <> " & cmbMarca.SelectedValue.ToString() & " " & _
                            "AND HDET.cantidad_demos <>'0' " & _
                            "AND H.id_periodo = '" & cmbPeriodo.SelectedValue & "'" & _
                            " " + RegionSQL + " " & _
                            " " + PromotorSQL + " " & _
                            " " + CadenaSQL + " " & _
                            " " + TiendaSQL + " " & _
                            " GROUP BY MAR.nombre_marca"

        CargaGrilla(ConexionEnergizer.localSqlServer, SQL, Me.gridReporte)

        Using cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
            Dim cmd As New SqlCommand(sql, cnn)
            cnn.Open()

            ''//CARGA GRAFICA
            Dim tabla As New DataTable
            Dim da2 As New SqlDataAdapter(cmd)
            da2.Fill(tabla)

            'Create FusionCharts XML
            Dim strXML As New StringBuilder()

            'Create chart element
            strXML.Append("<chart caption='CANTIDAD DE DEMOSTRADORAS' showborder='0' bgcolor='FFFFFF' bgalpha='100' subcaption='' xAxisName='Promocionales' yAxisName='Cantidad' rotateLabels='1'  placeValuesInside='1' rotateValues='1' >")

            For i = 0 To tabla.Rows.Count - 1
                strXML.Append("<set label='" & tabla.Rows(i)("nombre_marca") & "' value='" & tabla.Rows(i)("totales") & "' />")
            Next i

            strXML.Append("</chart>")

            Dim outPut As String = ""
            outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/Column3D.swf", "", strXML.ToString(), "chart1", "445", "350", False, False)

            PanelFS.Controls.Clear()
            PanelFS.Controls.Add(New LiteralControl(outPut))

            cnn.Close()
            cnn.Dispose()
        End Using
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

            CargarReporte()
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionEnergizer.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionEnergizer.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionEnergizer.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionEnergizer.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionEnergizer.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCadena.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionEnergizer.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbTienda_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbTienda.SelectedIndexChanged
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
        Response.AddHeader("Content-Disposition", "attachment;filename=ReporteDemosCompetencia" + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Protected Sub cmbMarca_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbMarca.SelectedIndexChanged
        CargarReporte()
    End Sub

End Class