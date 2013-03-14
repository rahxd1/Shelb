Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteIndexSYMPrecios2
    Inherits System.Web.UI.Page

    Dim PeriodoSel, RegionSel As String
    Dim PeriodoSQL, RegionSQL As String
    Dim Dato(10), Suma(10) As Integer

    Sub SQLCombo()
        PeriodoSel = Acciones.Slc.cmb("H.id_periodo", cmbPeriodo.SelectedValue)
        RegionSel = Acciones.Slc.cmb("US.id_region", cmbRegion.SelectedValue)

        PeriodoSQL = "SELECT id_periodo, nombre_periodo " & _
                         "FROM Precios_Periodos " & _
                         " ORDER BY fecha_inicio DESC"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Precios_Historial as H " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = H.id_cadena " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario = H.id_usuario " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = US.id_region " & _
                    "WHERE REG.id_region<>0 " & _
                    " " + PeriodoSel + " " & _
                    " ORDER BY REG.nombre_region"
    End Sub

    Sub CargarReporte()
        RegionSQL = Acciones.Slc.cmb("US.id_region", cmbRegion.SelectedValue)

        CargaGrilla(ConexionSYM.localSqlServer, _
                    "select CAD.nombre_cadena,LIN.nombre_grupos,PROD.tipo_producto," & _
                    "CASE WHEN PROD.tipo_producto=1 then 'SYM' else 'Competencia' end as Marca, " & _
                    "PROD.nombre_producto, PROD.presentacion, AVG(HDET.precio)precio " & _
                    "FROM Precios_Historial as H " & _
                    "INNER JOIN Precios_Productos_Historial_Det as HDET ON H.folio_historial=HDET.folio_historial " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena=H.id_cadena " & _
                    "INNER JOIN Productos as PROD ON PROD.id_producto = HDET.id_producto " & _
                    "INNER JOIN Grupos as LIN ON LIN.tipo_grupo= PROD.tipo_grupo " & _
                    "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & "  " & _
                    "AND (select TOP 1 ROUND(AVG(HDET.precio)/PROD.presentacion,5) " & _
                    "FROM Precios_Historial as H " & _
                    "INNER JOIN Precios_Productos_Historial_Det as HDET ON H.folio_historial=HDET.folio_historial " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena=H.id_cadena " & _
                    "INNER JOIN Productos as PROD ON PROD.id_producto = HDET.id_producto " & _
                    "WHERE H.id_periodo =" & cmbPeriodo.SelectedValue & "  And PROD.tipo_producto = 1 " & _
                    "GROUP BY PROD.presentacion,HDET.id_producto ORDER BY PROD.presentacion)<>0 " & _
                    "GROUP BY CAD.nombre_cadena,LIN.nombre_grupos,PROD.tipo_producto,PROD.nombre_producto, PROD.presentacion,PROD.presentacion ", _
                    gridReporte)
        PnlGrid.Visible = True
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionSYM.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)

        End If
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionSYM.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)

        CargarReporte()
    End Sub

    Protected Sub lnkExportar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.PnlGrid.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.PnlGrid)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Price index por cadena  " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If gridReporte.Rows.Count = 0 Then
            gridReporte.Visible = False : Else
            gridReporte.Visible = True : End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            If gridReporte.DataKeys(e.Row.RowIndex).Value.ToString() = 1 Then
                For C = 0 To 3
                    e.Row.Cells(C).BackColor = Drawing.Color.LemonChiffon
                Next C
            End If
        End If
    End Sub

End Class