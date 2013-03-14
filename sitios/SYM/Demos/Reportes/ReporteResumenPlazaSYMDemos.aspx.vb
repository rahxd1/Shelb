Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteResumenPlazaSYMDemos
    Inherits System.Web.UI.Page

    Dim PeriodoSel As String
    Dim PeriodoSQL, RegionSQL As String

    Sub SQLCombo()
        PeriodoSel = Acciones.Slc.cmb("H.id_periodo", cmbPeriodo.SelectedValue)

        PeriodoSQL = "SELECT id_periodo, nombre_periodo " & _
                         "FROM Demos_Periodos " & _
                         " ORDER BY fecha_inicio DESC"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Regiones as REG " & _
                    "WHERE REG.id_region<>0 " & _
                    " ORDER BY REG.nombre_region"
    End Sub

    Sub CargarReporte()
        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("US.id_region", cmbRegion.SelectedValue)

            CargaGrilla(ConexionSYM.localSqlServer, _
                        "SELECT plaza,ISNULL([1],0)[1],ISNULL([2],0)[2],ISNULL([3],0)[3], " & _
                        "ISNULL([4],0)[4],ISNULL([101],0)[101],ISNULL([102],0)[102],ISNULL([5],0)[5] " & _
                        "FROM(SELECT 1Dato,US.plaza,SUM(demos)Cantidad  " & _
                        "FROM Demos_Rutas_Eventos as H  " & _
                        "INNER JOIN Usuarios as US ON H.id_usuario=US.id_usuario  " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " " + RegionSQL + " GROUP BY US.plaza " & _
                        "UNION ALL  " & _
                        "SELECT 2Dato,US.plaza,SUM(cantidad)Cantidad  " & _
                        "FROM Demos_Historial as H  " & _
                        "INNER JOIN Usuarios as US ON H.id_usuario=US.id_usuario  " & _
                        "INNER JOIN Demos_Abordos_Historial_Dias_Det as HDET ON H.folio_historial=HDET.folio_historial  " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " " + RegionSQL + " GROUP BY US.plaza " & _
                        "UNION ALL  " & _
                        "SELECT 3Dato,US.plaza,SUM(cantidad)Cantidad  " & _
                        "FROM Demos_Historial as H  " & _
                        "INNER JOIN Usuarios as US ON H.id_usuario=US.id_usuario  " & _
                        "INNER JOIN Demos_Abordos_Historial_Dias_Det as HDET ON H.folio_historial=HDET.folio_historial  " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " " + RegionSQL + " and tipo_abordo=1  GROUP BY US.plaza " & _
                        "UNION ALL  " & _
                        "SELECT 4as Dato,US.plaza,SUM(ventas)Cantidad  " & _
                        "FROM Demos_Historial as H  " & _
                        "INNER JOIN Usuarios as US ON H.id_usuario=US.id_usuario  " & _
                        "INNER JOIN Demos_Productos_Historial_Dias_Det as HDET ON HDET.folio_historial=H.folio_historial  " & _
                        "INNER JOIN Productos_Demos as PROD ON PROD.id_producto=HDET.id_producto  " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " " + RegionSQL + " GROUP BY US.plaza " & _
                        "UNION ALL  " & _
                        "SELECT 100+GRUP.tipo_grupo as Dato,US.plaza,SUM(ventas)Cantidad  " & _
                        "FROM Demos_Historial as H  " & _
                        "INNER JOIN Usuarios as US ON H.id_usuario=US.id_usuario  " & _
                        "INNER JOIN Demos_Productos_Historial_Dias_Det as HDET ON HDET.folio_historial=H.folio_historial  " & _
                        "INNER JOIN Productos_Demos as PROD ON PROD.id_producto=HDET.id_producto  " & _
                        "INNER JOIN Tipo_Grupo_Demos as GRUP ON GRUP.tipo_grupo=PROD.tipo_grupo  " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " " + RegionSQL + " " & _
                        "GROUP BY GRUP.tipo_grupo,US.plaza " & _
                        "UNION ALL  " & _
                        "SELECT 5 as Dato,US.plaza,SUM(toallas)Cantidad  " & _
                        "FROM Demos_Historial as H  " & _
                        "INNER JOIN Usuarios as US ON H.id_usuario=US.id_usuario  " & _
                        "INNER JOIN Demos_Canjes_Historial_Det as HDET ON HDET.folio_historial=H.folio_historial  " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " " + RegionSQL + " GROUP BY US.plaza) PVT " & _
                        "PIVOT(SUM(cantidad) FOR dato  " & _
                        "IN([1],[2],[3],[4],[101],[102],[5])) AS H ", gridReporte)
        Else
            gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionSYM.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
        End If
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte comentarios " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class