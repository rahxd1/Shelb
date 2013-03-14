Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteEntregaFD
    Inherits System.Web.UI.Page

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

            Dim TablaTiendas As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                                   "SELECT count(nombre_tienda) as Cantidad " & _
                                                   "FROM Danone_Historial " & _
                                                   "WHERE id_periodo =" & cmbPeriodo.SelectedValue & "")
            If TablaTiendas.Rows.Count > 0 Then
                lblTiendas.Text = TablaTiendas.Rows(0)("Cantidad")
            End If

            TablaTiendas.Dispose()

            CargaGrilla(ConexionFerrero.localSqlServer, _
                        "select REG.nombre_region,H.nombre_tienda, H.id_usuario, COL.nombre_colonia, H.direccion, HDET.[5] " & _
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
        End If
    End Sub

    Sub CargaGrafica()
        If cmbPeriodo.SelectedValue <> "" Then
            Dim PromotorSQL, RegionSQL, ColoniaSQL As String

            RegionSQL = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbSupervisor.SelectedValue)
            ColoniaSQL = Acciones.Slc.cmb("H.colonia", cmbColonia.SelectedValue)

            Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                                          "select HDET.[5], count(HDET.[5]) as cantidad " & _
                                    "FROM Danone_Historial as H " & _
                                    "INNER JOIN Danone_Encuesta as HDET ON H.folio_historial = HDET.folio_historial " & _
                                    "INNER JOIN Danone_CatRutas as RUT ON RUT.id_usuario = H.id_usuario " & _
                                    "INNER JOIN Regiones as REG ON RUT.id_region= REG.id_region AND REG.id_proyecto=30 " & _
                                    "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                                    " " + RegionSQL + " " & _
                                    " " + PromotorSQL + " " & _
                                    " " + ColoniaSQL + " " & _
                                    "GROUP BY HDET.[5] " & _
                                    "ORDER BY HDET.[5] ")

            Dim strXML As New StringBuilder()
            strXML.Append("<chart caption='Tiempo de entrega de pedido' showPercentValues='1' showPercentageInLabel='1' pieSliceDepth='25' showBorder='1' showLabels='1'>")

            For i = 0 To Tabla.Rows.Count - 1
                strXML.Append("<set label='" & Tabla.Rows(i)("5") & "' value='" & Tabla.Rows(i)("cantidad") & "'/>")
            Next i

            strXML.Append("</chart>")

            Dim outPut As String = ""
            outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/Pie2D.swf", "", strXML.ToString(), "Frentes", "500", "300", False, False)

            pnlGrafica.Controls.Clear()
            pnlGrafica.Controls.Add(New LiteralControl(outPut))

            Tabla.Dispose()
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
            CargaGrafica()
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Entregas " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbColonia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbColonia.SelectedIndexChanged
        CargarReporte()
    End Sub

End Class