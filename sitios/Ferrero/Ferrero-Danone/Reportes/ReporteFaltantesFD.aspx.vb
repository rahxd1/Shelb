Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteFaltantesFD
    Inherits System.Web.UI.Page

    Dim Tiendas As Integer

    Sub SQLCombo()
        Ferrero.SQLsComboFD(cmbPeriodo.SelectedValue, cmbRegion.SelectedValue, _
                            cmbSupervisor.SelectedValue, cmbColonia.SelectedValue)
    End Sub

    Sub CargaGrafica()
        If cmbPeriodo.SelectedValue <> "" Then
            Dim PromotorSQL, RegionSQL, ColoniaSQL As String

            RegionSQL = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbSupervisor.SelectedValue)
            ColoniaSQL = Acciones.Slc.cmb("H.colonia", cmbColonia.SelectedValue)

            Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                                   "select PROD.nombre_producto, sum(HDET.faltante ) as cantidad " & _
                                "FROM Danone_Historial as H  " & _
                                "INNER JOIN Danone_Productos_Historial_Det as HDET ON H.folio_historial = HDET.folio_historial  " & _
                                "INNER JOIN Colonias_Leon as COL ON COL.id_colonia= H.colonia  " & _
                                "INNER JOIN Danone_Productos as PROD ON PROD.id_producto = HDET.id_producto  " & _
                                "INNER JOIN Danone_CatRutas as RUT ON RUT.id_usuario = H.id_usuario  " & _
                                "INNER JOIN Regiones as REG ON RUT.id_region= REG.id_region AND REG.id_proyecto=30  " & _
                                "WHERE H.id_periodo = " & cmbPeriodo.SelectedValue & " And HDET.faltante = 1 " & _
                                " " + RegionSQL + " " & _
                                " " + PromotorSQL + " " & _
                                " " + ColoniaSQL + " " & _
                                "GROUP BY PROD.nombre_producto " & _
                                "ORDER BY PROD.nombre_producto")

            Dim TablaTiendas As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                                   "SELECT count(nombre_tienda) as Cantidad " & _
                                                   "FROM Danone_Historial " & _
                                                   "WHERE id_periodo =" & cmbPeriodo.SelectedValue & "")
            If TablaTiendas.Rows.Count > 0 Then
                Tiendas = TablaTiendas.Rows(0)("Cantidad")
            End If

            Dim strXML As New StringBuilder()
            strXML.Append("<chart caption='Tiendas con faltantes por producto' yAxisMaxValue='" & Tiendas + 5 & "' showborder='0' bgcolor='FFFFFF' bgalpha='100' rotateLabels='1'  placeValuesInside='1' rotateValues='1' >")

            strXML.Append("<trendlines>")
            strXML.Append("<line startvalue='" & Tiendas & "' displayValue='Tiendas " & Tiendas & "' color='FF0000' thickness='3' valueOnRight='1' showOnTop='0' />")
            strXML.Append("</trendlines>")

            For i = 0 To Tabla.Rows.Count - 1
                strXML.Append("<set label='" & Tabla.Rows(i)("nombre_producto") & "' value='" & Tabla.Rows(i)("cantidad") & "' />")
            Next i

            strXML.Append("</chart>")

            Dim outPut As String = ""
            outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/Column3D.swf", "", strXML.ToString(), "chart1", "650", "400", False, False)

            pnlGrafica.Controls.Clear()
            pnlGrafica.Controls.Add(New LiteralControl(outPut))

            Tabla.Dispose()
            TablaTiendas.Dispose()
        End If
    End Sub

    Sub CargarReporte()
        If cmbPeriodo.SelectedValue <> "" Then
            Dim PromotorSQL, RegionSQL, ColoniaSQL As String

            RegionSQL = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbSupervisor.SelectedValue)
            ColoniaSQL = Acciones.Slc.cmb("H.colonia", cmbColonia.SelectedValue)

            CargaGrilla(ConexionFerrero.localSqlServer, _
                        "select REG.nombre_region,COL.nombre_colonia, H.nombre_tienda, H.direccion, H.id_usuario, " & _
                        "PROD.nombre_producto " & _
                        "FROM Danone_Historial as H " & _
                        "INNER JOIN Danone_Productos_Historial_Det as HDET ON H.folio_historial = HDET.folio_historial " & _
                        "INNER JOIN Colonias_Leon as COL ON COL.id_colonia= H.colonia " & _
                        "INNER JOIN Danone_Productos as PROD ON PROD.id_producto = HDET.id_producto " & _
                        "INNER JOIN Danone_CatRutas as RUT ON RUT.id_usuario = H.id_usuario " & _
                        "INNER JOIN Regiones as REG ON RUT.id_region= REG.id_region AND REG.id_proyecto=30 " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        "AND HDET.faltante=1 " & _
                        " " + RegionSQL + " " & _
                        " " + PromotorSQL + " " & _
                        " " + ColoniaSQL + " " & _
                        "ORDER BY REG.nombre_region,H.nombre_tienda ", Me.gridReporte)

            CargaGrafica()
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Productos Faltantes por tienda " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbColonia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbColonia.SelectedIndexChanged
        CargarReporte()
    End Sub

End Class