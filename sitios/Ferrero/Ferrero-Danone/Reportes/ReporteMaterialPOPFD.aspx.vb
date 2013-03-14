Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteMaterialPOPFD
    Inherits System.Web.UI.Page

    Dim Tiendas As Integer

    Sub SQLCombo()
        Ferrero.SQLsComboFD(cmbPeriodo.SelectedValue, cmbRegion.SelectedValue, _
                            cmbSupervisor.SelectedValue, cmbColonia.SelectedValue)
    End Sub

    Sub CargarReporte()
        If cmbPeriodo.SelectedValue <> "" Then
            Dim PromotorSQL, RegionSQL, ColoniaSQL As String

            RegionSQL = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbSupervisor.SelectedValue)
            ColoniaSQL = Acciones.Slc.cmb("H.colonia", cmbColonia.SelectedValue)

            CargaGrilla(ConexionFerrero.localSqlServer, _
                        "select REG.nombre_region,H.nombre_tienda, H.id_usuario, COL.nombre_colonia, H.direccion, HDET.[3], HDET.[3A], HDET.[3B], HDET.[3C] " & _
                        "FROM Danone_Historial as H " & _
                        "INNER JOIN Danone_Encuesta as HDET ON H.folio_historial = HDET.folio_historial " & _
                        "INNER JOIN Colonias_Leon as COL ON COL.id_colonia= H.colonia " & _
                        "INNER JOIN Danone_CatRutas as RUT ON RUT.id_usuario = H.id_usuario " & _
                        "INNER JOIN Regiones as REG ON RUT.id_region= REG.id_region AND REG.id_proyecto=30 " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + " " & _
                        " " + PromotorSQL + " " & _
                        " " + ColoniaSQL + " " & _
                        "ORDER BY REG.nombre_region,H.nombre_tienda,H.id_usuario, H.direccion ", Me.gridReporte)

            CargaGrafica()
            CargaGrafica2()
        End If
    End Sub

    Sub CargaGrafica()
        If cmbPeriodo.SelectedValue <> "" Then
            Dim PromotorSQL, RegionSQL, ColoniaSQL As String

            RegionSQL = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbSupervisor.SelectedValue)
            ColoniaSQL = Acciones.Slc.cmb("H.colonia", cmbColonia.SelectedValue)

            Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                                   "select HDET.[3], count(HDET.[3]) as cantidad " & _
                                "FROM Danone_Historial as H " & _
                                "INNER JOIN Danone_Encuesta as HDET ON H.folio_historial = HDET.folio_historial " & _
                                "INNER JOIN Danone_CatRutas as RUT ON RUT.id_usuario = H.id_usuario " & _
                                "INNER JOIN Regiones as REG ON RUT.id_region= REG.id_region AND REG.id_proyecto=30 " & _
                                "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                                " " + RegionSQL + " " & _
                                " " + PromotorSQL + " " & _
                                " " + ColoniaSQL + " " & _
                                "GROUP BY HDET.[3] " & _
                                "ORDER BY HDET.[3] ")

            Dim strXML As New StringBuilder()
            strXML.Append("<chart caption='MATERIAL POP' showPercentValues='1' showPercentageInLabel='1' pieSliceDepth='25' showBorder='1' showLabels='1'>")

            For i = 0 To Tabla.Rows.Count - 1
                strXML.Append("<set label='" & Tabla.Rows(i)("3") & "' value='" & Tabla.Rows(i)("cantidad") & "'/>")
            Next i

            strXML.Append("</chart>")

            Dim outPut As String = ""
            outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/Pie2D.swf", "", strXML.ToString(), "Frentes", "300", "300", False, False)

            pnlGrafica.Controls.Clear()
            pnlGrafica.Controls.Add(New LiteralControl(outPut))

            Tabla.Dispose()
        End If
    End Sub

    Sub CargaGrafica2()
        If cmbPeriodo.SelectedValue <> "" Then
            Dim PromotorSQL, RegionSQL, ColoniaSQL As String

            RegionSQL = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbSupervisor.SelectedValue)
            ColoniaSQL = Acciones.Slc.cmb("H.colonia", cmbColonia.SelectedValue)

            Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                       "select HDET.[3a] as Material, count(HDET.[3a]) as cantidad " & _
                            "FROM Danone_Historial as H " & _
                            "INNER JOIN Danone_Encuesta as HDET ON H.folio_historial = HDET.folio_historial " & _
                            "INNER JOIN Danone_CatRutas as RUT ON RUT.id_usuario = H.id_usuario " & _
                            "INNER JOIN Regiones as REG ON RUT.id_region= REG.id_region AND REG.id_proyecto=30 " & _
                            "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " AND HDET.[3A]<>'' " & _
                            " " + RegionSQL + " " & _
                            " " + PromotorSQL + " " & _
                            " " + ColoniaSQL + " " & _
                            "GROUP BY HDET.[3a] " & _
                            "union all " & _
                            "select HDET.[3B] as Material, count(HDET.[3B]) as cantidad " & _
                            "FROM Danone_Historial as H " & _
                            "INNER JOIN Danone_Encuesta as HDET ON H.folio_historial = HDET.folio_historial " & _
                            "INNER JOIN Danone_CatRutas as RUT ON RUT.id_usuario = H.id_usuario " & _
                            "INNER JOIN Regiones as REG ON RUT.id_region= REG.id_region AND REG.id_proyecto=30 " & _
                            "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " AND HDET.[3B]<>'' " & _
                            " " + RegionSQL + " " & _
                            " " + PromotorSQL + " " & _
                            " " + ColoniaSQL + " " & _
                            "GROUP BY HDET.[3B] " & _
                            "union all " & _
                            "select HDET.[3C] as Material, count(HDET.[3C]) as cantidad " & _
                            "FROM Danone_Historial as H " & _
                            "INNER JOIN Danone_Encuesta as HDET ON H.folio_historial = HDET.folio_historial " & _
                            "INNER JOIN Danone_CatRutas as RUT ON RUT.id_usuario = H.id_usuario " & _
                            "INNER JOIN Regiones as REG ON RUT.id_region= REG.id_region AND REG.id_proyecto=30 " & _
                            "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " AND HDET.[3C]<>'' " & _
                            " " + RegionSQL + " " & _
                            " " + PromotorSQL + " " & _
                            " " + ColoniaSQL + " " & _
                            "GROUP BY HDET.[3C]")

            Dim TablaTiendas As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                                   "SELECT count(nombre_tienda) as Cantidad FROM Danone_Historial as H " & _
                                             "INNER JOIN Danone_Encuesta as HDET ON H.folio_historial = HDET.folio_historial " & _
                                             "WHERE id_periodo = " & cmbPeriodo.SelectedValue & " AND HDET.[3]='SI' ")
            If TablaTiendas.Rows.Count > 0 Then
                Tiendas = TablaTiendas.Rows(0)("Cantidad")
            End If

            Dim strXML As New StringBuilder()
            strXML.Append("<chart caption='Tipo de material POP' yAxisMaxValue='" & Tiendas + 5 & "' showborder='0' bgcolor='FFFFFF' bgalpha='100' rotateLabels='1'  placeValuesInside='1' rotateValues='1' >")

            strXML.Append("<trendlines>")
            strXML.Append("<line startvalue='" & Tiendas & "' displayValue='Tiendas " & Tiendas & "' color='FF0000' thickness='3' valueOnRight='1' showOnTop='0' />")
            strXML.Append("</trendlines>")

            For i = 0 To Tabla.Rows.Count - 1
                strXML.Append("<set label='" & Tabla.Rows(i)("Material") & "' value='" & Tabla.Rows(i)("cantidad") & "' />")
            Next i

            strXML.Append("</chart>")

            Dim outPut As String = ""
            outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/Column3D.swf", "", strXML.ToString(), "chart1", "300", "300", False, False)

            pnlGrafica2.Controls.Clear()
            pnlGrafica2.Controls.Add(New LiteralControl(outPut))

            Tabla.Dispose()
            TablaTiendas.Dispose()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.PeriodoSQLCmb, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.PromotorSQLCmb, "id_usuario", "id_usuario", cmbSupervisor)
            Combo.LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.ColoniaSQLCmb, "nombre_colonia", "id_colonia", cmbColonia)

            CargarReporte()
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.ColoniaSQLCmb, "nombre_colonia", "id_colonia", cmbColonia)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.PromotorSQLCmb, "id_usuario", "id_usuario", cmbSupervisor)
        Combo.LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.ColoniaSQLCmb, "nombre_colonia", "id_colonia", cmbColonia)

        SQLCombo()

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Material POP " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbColonia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbColonia.SelectedIndexChanged
        CargarReporte()
    End Sub

End Class