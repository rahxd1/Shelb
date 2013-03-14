Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteEnergizerDPPromPlazas
    Inherits System.Web.UI.Page

    Dim RegionSel,PeriodoSQL, RegionSQL As String
    Dim Dato, Suma, Dato2, Suma2 As Integer

    Sub SQLCombo()
        If cmbRegion.SelectedValue <> "" Then
            If cmbRegion.SelectedValue <> 0 Then
                RegionSel = "AND REG.id_region=" & cmbRegion.SelectedValue & " " : Else
                RegionSel = "" : End If : End If

        PeriodoSQL = "SELECT id_periodo, nombre_periodo " & _
                         "FROM Energizer_DP_Periodos " & _
                         " ORDER BY fecha_inicio DESC"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Regiones as REG " & _
                    "INNER JOIN Energizer_DP_Tiendas AS TI ON TI.id_region = REG.id_region " & _
                    "WHERE REG.id_region<>0 ORDER BY REG.nombre_region"
    End Sub

    Sub CargarReporte()
        If Not cmbPeriodo.SelectedValue = "" Then
            PeriodoSQL = "AND H.id_periodo=" & cmbPeriodo.SelectedValue & " " : Else
            PeriodoSQL = "" : End If

        If cmbRegion.SelectedValue <> 0 Then
            RegionSQL = "AND TI.id_region=" & cmbRegion.SelectedValue & " " : Else
            RegionSQL = "" : End If

        Dim SQL As String = "SELECT EST.nombre_estado, sum(HPROM.cantidad) as Totales " & _
                            "FROM Energizer_DP_Productos_Historial as H " & _
                            "INNER JOIN Energizer_DP_Productos_Historial_Promo as HProm ON H.folio_historial= HPROM.folio_historial " & _
                            "INNER JOIN Energizer_DP_Tiendas as TI ON TI.id_tienda= H.id_tienda  " & _
                            "INNER JOIN Estados as EST ON EST.id_estado= TI.id_estado  " & _
                            "INNER JOIN Energizer_DP_Productos_Promo as PRODP ON PRODP.id_producto = HPROM.id_producto " & _
                            "INNER JOIN Cadenas_Tiendas as CAD ON TI.id_cadena= CAD.id_cadena " & _
                            "WHERE PRODP.activo ='1' " & _
                            " " + PeriodoSQL + " " & _
                            " " + RegionSQL + " " & _
                            " GROUP BY EST.nombre_estado"

        CargaGrilla(ConexionEnergizer.localSqlServer, SQL, Me.gridReporte)

        Using cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
            Dim cmd As New SqlCommand(SQL, cnn)
            cnn.Open()

            ''//CARGA GRAFICA
            Dim tabla As New DataTable
            Dim da2 As New SqlDataAdapter(cmd)
            da2.Fill(tabla)

            'Create FusionCharts XML
            Dim strXML As New StringBuilder()

            'Create chart element
            strXML.Append("<chart caption='CANTIDAD DE PROMOCIONALES ENTREGADOS' showborder='0' bgcolor='FFFFFF' bgalpha='100' subcaption='por Plazas' xAxisName='Promocionales' yAxisName='Cantidad' rotateLabels='1'  placeValuesInside='1' rotateValues='1' >")

            For i = 0 To tabla.Rows.Count - 1
                strXML.Append("<set label='" & tabla.Rows(i)("nombre_estado") & "' value='" & tabla.Rows(i)("totales") & "' />")
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

            CargarReporte()
        End If
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        CargarReporte()
    End Sub


    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        CargarReporte()
    End Sub

    Private Sub gridReporte_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gridReporte.RowEditing
        CargarDetalle(gridReporte.Rows(e.NewEditIndex).Cells(1).Text)
        pnlDetalle.Visible = True
    End Sub

    Sub CargarDetalle(ByVal SeleccionPlaza As String)
        If cmbRegion.SelectedValue <> 0 Then
            RegionSQL = "AND TI.id_region=" & cmbRegion.SelectedValue & " " : Else
            RegionSQL = "" : End If

        CargaGrilla(ConexionEnergizer.localSqlServer, _
                    "SELECT REG.nombre_region, CAD.nombre_cadena, H.id_usuario,Ti.nombre, sum(HPROM.cantidad) as Totales " & _
                    "FROM Energizer_DP_Productos_Historial as H " & _
                    "INNER JOIN Energizer_DP_Productos_Historial_Promo as HProm " & _
                    "ON H.folio_historial= HPROM.folio_historial " & _
                    "INNER JOIN Energizer_DP_Tiendas as TI  " & _
                    "ON TI.id_tienda= H.id_tienda  " & _
                    "INNER JOIN Regiones as REG  " & _
                    "ON REG.id_region= TI.id_region  " & _
                    "INNER JOIN Energizer_DP_Productos_Promo as PRODP " & _
                    "ON PRODP.id_producto = HPROM.id_producto " & _
                    "INNER JOIN Cadenas_Tiendas as CAD " & _
                    "ON TI.id_cadena= CAD.id_cadena " & _
                    "WHERE Ti.ciudad = '" & SeleccionPlaza & "'" & _
                    "AND H.id_periodo = '" & cmbPeriodo.SelectedValue & "'" & _
                    " " + RegionSQL + " " & _
                    " GROUP BY REG.nombre_region, CAD.nombre_cadena, H.id_usuario,Ti.nombre,HPROM.id_producto,PRODP.nombre_producto", _
                    Me.gridDetalle)
    End Sub

    Sub Excel2()
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.gridReporte.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.gridDetalle)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=ReportePrecios " + cmbPeriodo.Text + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Promocionales por Plazas " + cmbPeriodo.SelectedItem.ToString() + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

End Class