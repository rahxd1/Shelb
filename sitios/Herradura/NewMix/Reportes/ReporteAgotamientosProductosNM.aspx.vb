Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class ReporteAgotamientosProductosNM
    Inherits System.Web.UI.Page

    Dim RegionSel As String
    Dim PeriodoSQL, PeriodoSQL2, PromotorSQL, RegionSQL As String
    Dim Porcentaje As String

    Sub SQLCombo()
        RegionSel = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)

        PeriodoSQL = "SELECT id_periodo, nombre_periodo " & _
                         "FROM NewMix_Periodos " & _
                         " ORDER BY id_periodo DESC"

        PeriodoSQL2 = "SELECT id_periodo, nombre_periodo " & _
                         "FROM NewMix_Periodos " & _
                         " ORDER BY id_periodo DESC"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Regiones as REG " & _
                    "WHERE REG.id_region<>0 ORDER BY REG.nombre_region"

        PromotorSQL = "SELECT DISTINCT id_usuario " & _
                    "FROM NewMix_Historial as RE " & _
                    "INNER JOIN NewMix_Tiendas as TI ON RE.id_tienda = TI.id_tienda " & _
                    "WHERE id_usuario<>'' " & _
                    " " + RegionSel + " ORDER BY id_usuario "
    End Sub

    Sub CargarReporte()
        If cmbPeriodo1.SelectedValue <> "" Then
            If cmbPeriodo1.SelectedValue = "" Then
                Exit Sub : End If

            If cmbPeriodo2.SelectedValue = "" Then
                PeriodoSQL = " AND H.id_periodo=" & cmbPeriodo1.SelectedValue & " "
                PeriodoSQL2 = "" : Else
                PeriodoSQL = " AND H.id_periodo >= " & cmbPeriodo1.SelectedValue & ""
                PeriodoSQL2 = " AND H.id_periodo <= " & cmbPeriodo2.SelectedValue & "" : End If

            RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)

            Dim SQL As String = "SELECT DISTINCT PROD.orden, " & _
                                "PROD.nombre_producto, ISNULL(Agotamientos.tiendas,0) as Agotamientos, " & _
                                "ISNULL(Catalagados.tiendas,0) as Catalagados, " & _
                                "ISNULL(Agotamientos.tiendas*100/Catalagados.tiendas,0) as Porcentaje  " & _
                                "FROM NewMix_Productos as PROD " & _
                                "FULL JOIN(SELECT HDET.id_producto, COUNT(H.id_tienda) tiendas " & _
                                "FROM NewMix_Historial as H  " & _
                                "INNER JOIN NewMix_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                                "INNER JOIN NewMix_Productos_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial " & _
                                "WHERE HDET.agotado=1 " + PeriodoSQL + PeriodoSQL2 + RegionSQL + PromotorSQL + " " & _
                                "GROUP BY HDET.id_producto) as Agotamientos " & _
                                "ON Agotamientos.id_producto = PROD.id_producto " & _
                                "FULL JOIN(SELECT HDET.id_producto, COUNT(H.id_tienda) tiendas " & _
                                "FROM NewMix_Historial as H  " & _
                                "INNER JOIN NewMix_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                                "INNER JOIN NewMix_Productos_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial " & _
                                "WHERE HDET.catalogado=1 " + PeriodoSQL + PeriodoSQL2 + RegionSQL + PromotorSQL + " " & _
                                "GROUP BY HDET.id_producto) as Catalagados " & _
                                "ON Catalagados.id_producto = PROD.id_producto " & _
                                "WHERE PROD.tipo_producto = 1 And PROD.activo = 1 " & _
                                "GROUP BY PROD.orden,PROD.nombre_producto, Agotamientos.tiendas, Catalagados.tiendas ORDER BY PROD.orden"

            CargaGrilla(ConexionHerradura.localSqlServer, SQL, Me.gridReporte)

            Dim Tabla As DataTable = Tablas.Ver.DT(ConexionHerradura.localSqlServer, SQL)

            Dim strXML As New StringBuilder()
            strXML.Append("<chart caption='Agotados' showborder='0' bgcolor='FFFFFF' bgalpha='100' rotateLabels='1'  placeValuesInside='1' rotateValues='1' >")

            For i = 0 To Tabla.Rows.Count - 1
                strXML.Append("<set label='" & Tabla.Rows(i)("nombre_producto") & "' value='" & Tabla.Rows(i)("Agotamientos") & "' />")
            Next i

            strXML.Append("</chart>")

            Dim outPut As String = ""
            outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/Column3D.swf", "", strXML.ToString(), "chart1", "600", "400", False, False)

            PanelFS.Controls.Clear()
            PanelFS.Controls.Add(New LiteralControl(outPut))

            Tabla.Dispose()
        Else
            Me.gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo1)
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, PeriodoSQL2, "nombre_periodo", "id_periodo", cmbPeriodo2)
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)


        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionHerradura.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo1.SelectedIndexChanged
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Agotamientos por Productos " + cmbPeriodo1.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 0 To CInt(gridReporte.Rows.Count) - 0
                Porcentaje = FormatPercent(e.Row.Cells(1).Text / e.Row.Cells(2).Text, 0)
                If Porcentaje = "NeuN" Then
                    Porcentaje = "0%" : End If
                e.Row.Cells(3).Text = Porcentaje : Next i
        End If
    End Sub

    Private Sub cmbPeriodo2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPeriodo2.SelectedIndexChanged
        SQLCombo()
        CargarReporte()
    End Sub
End Class