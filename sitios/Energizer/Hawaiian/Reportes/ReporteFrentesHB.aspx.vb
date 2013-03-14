Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteFrentesHB
    Inherits System.Web.UI.Page

    Dim PromotorSel, RegionSel, CadenaSel, TiendaSel As String
    Dim PeriodoSQL, PromotorSQL, RegionSQL, CadenaSQL, TiendaSQL As String
    Dim NombreCampos, Campos, Marca, NombreMarca1, NombreMarca2 As String
    Dim Totales,Producto1, Producto2, Producto3, Producto4 As Double
    Dim total1, total2, total3, total4 As Double

    Sub SQLCombo()
        If cmbRegion.SelectedValue <> "" Then
            If cmbRegion.SelectedValue <> 0 Then
                RegionSel = "AND REG.id_region=" & cmbRegion.SelectedValue & " " : Else
                RegionSel = "" : End If : End If

        If Not cmbPromotor.Text = "" Then
            PromotorSel = "AND RUT.id_usuario='" & cmbPromotor.SelectedValue & "'":Else
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
            PromotorSQL = "AND H.id_usuario = '" & cmbPromotor.SelectedValue & "' " : Else
            PromotorSQL = "" : End If

        If Not cmbCadena.SelectedValue = "" Then
            CadenaSQL = "AND TI.id_cadena=" & cmbCadena.SelectedValue & " " : Else
            CadenaSQL = "" : End If

        If Not cmbTienda.SelectedValue = "" Then
            TiendaSQL = "AND TI.id_tienda=" & cmbTienda.SelectedValue & " " : Else
            TiendaSQL = "" : End If


        If cmbMarca.SelectedValue = 1 Then
            NombreMarca1 = "HAWAIIAN TROPIC"
            NombreMarca2 = "BANANA BOAT"
            Marca = 1
            NombreCampos = "ISNULL([1],0)as 'HAWAIIAN TROPIC','' as '%',ISNULL([2],0)as 'BANANA BOAT','' as ' %',ISNULL([3],0)as 'COPPERTONE','' as '% ',ISNULL([4],0) as 'NIVEA','' as '%   ', " & _
                            "(ISNULL([1],0)+ISNULL([2],0)+ISNULL([3],0)+ISNULL([4],0))as 'Totales' "
            Campos = "[1],[2],[3],[4]"
        End If
        If cmbMarca.SelectedValue = 2 Then
            NombreMarca1 = "BANANA BOAT"
            NombreMarca2 = "HAWAIIAN TROPIC"
            Marca = 2
            NombreCampos = "ISNULL([2],0)as 'BANANA BOAT','' as '%',ISNULL([1],0)as 'HAWAIIAN TROPIC','' as ' %',ISNULL([3],0)as 'COPPERTONE','' as '% ',ISNULL([4],0) as 'NIVEA','' as '%   ', " & _
                            "(ISNULL([1],0)+ISNULL([2],0)+ISNULL([3],0)+ISNULL([4],0))as 'Totales' "
            Campos = "[2],[1],[3],[4]"
        End If

        Dim SQL As String = "SELECT DISTINCT nombre_region as 'Región',nombre_cadena as 'Cadena', nombre as 'Tienda', " & _
                    " " + NombreCampos + "  " & _
                    "FROM (SELECT DISTINCT REG.nombre_region, H.id_usuario, CAD.nombre_cadena, TI.nombre, TI.no_tienda, " & _
                    "HDET.id_marca, HDET.cantidad_frentes " & _
                    "From HawaiianBanana_Historial as H  " & _
                    "FULL JOIN HawaiianBanana_Frentes_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial " & _
                    "INNER JOIN HawaiianBanana_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = TI.id_cadena " & _
                    "WHERE H.id_periodo=8 AND H.id_marca = " & Marca & " " & _
                    " " + RegionSQL + " " & _
                    " " + PromotorSQL + " " & _
                    " " + CadenaSQL + " " & _
                    " " + TiendaSQL + ") AS Datos " & _
                    "PIVOT (SUM(cantidad_frentes) " & _
                    "FOR id_marca IN (" + Campos + ")) AS PivotTable"

        CargaGrilla(ConexionEnergizer.localSqlServer, sql, Me.gridReporte)

        Using cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
            Dim cmd As New SqlCommand(SQL, cnn)
            cnn.Open()

            ''//CARGA GRAFICA
            Dim tabla As New DataTable
            Dim da2 As New SqlDataAdapter(cmd)
            da2.Fill(tabla)

            ''//CARGA GRAFICA
            Dim strXML As New StringBuilder()
            strXML.Append("<chart showPercentValues='1' showPercentageInLabel='1' pieSliceDepth='25' showBorder='1' showLabels='1'>")
            strXML.Append("<set label='" & NombreMarca1 & "' value='" & total1 & "' color='ff9966'/>")
            strXML.Append("<set label='" & NombreMarca2 & "' value='" & total2 & "' />")
            strXML.Append("<set label='Coppertone' value='" & total3 & "' />")
            strXML.Append("<set label='Nivea' value='" & total4 & "' />")
            strXML.Append("</chart>")

            Dim outPut As String = ""
            outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/Pie2D.swf", "", strXML.ToString(), "Frentes", "500", "300", False, False)

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
        form.Controls.Add(gridReporte)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Frentes " + cmbMarca.SelectedItem.ToString + " " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Protected Sub cmbMarca_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbMarca.SelectedIndexChanged
        CargarReporte()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Total As Double
            If e.Row.Cells(11).Text = "&nbsp;" Then
                Total = 0 : Else
                Total = e.Row.Cells(11).Text : End If

            Totales = Totales + Total

            If Total <> 0 Then
                e.Row.Cells(4).Text = FormatPercent(e.Row.Cells(3).Text / Total, 2, 0, 0, 0)
                e.Row.Cells(6).Text = FormatPercent(e.Row.Cells(5).Text / Total, 2, 0, 0, 0)
                e.Row.Cells(8).Text = FormatPercent(e.Row.Cells(7).Text / Total, 2, 0, 0, 0)
                e.Row.Cells(10).Text = FormatPercent(e.Row.Cells(9).Text / Total, 2, 0, 0, 0)
            End If

            If Not e.Row.Cells(3).Text = "&nbsp;" Then
                Producto1 = e.Row.Cells(3).Text
                total1 = total1 + Producto1
            End If
            If Not e.Row.Cells(5).Text = "&nbsp;" Then
                Producto2 = e.Row.Cells(5).Text
                total2 = total2 + Producto2
            End If
            If Not e.Row.Cells(7).Text = "&nbsp;" Then
                Producto3 = e.Row.Cells(7).Text
                total3 = total3 + Producto3
            End If
            If Not e.Row.Cells(9).Text = "&nbsp;" Then
                Producto4 = e.Row.Cells(9).Text
                total4 = total4 + Producto4
            End If
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(3).Text = total1
            e.Row.Cells(5).Text = total2
            e.Row.Cells(7).Text = total3
            e.Row.Cells(9).Text = total4
        End If
    End Sub

End Class