Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteIndexSYMPrecios
    Inherits System.Web.UI.Page

    Dim PeriodoSel, RegionSel As String
    Dim PeriodoSQL, CadenaSQL, RegionSQL As String
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

        CadenaSQL = "SELECT DISTINCT CAD.id_cadena, CAD.nombre_cadena " & _
                    "FROM Precios_Historial as H " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = H.id_cadena " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario = H.id_usuario " & _
                    "WHERE CAD.id_cadena<>0 " & _
                    " " + PeriodoSel + RegionSel + " ORDER BY CAD.nombre_cadena "
    End Sub

    Sub CargarReporte()
        If cmbPeriodo.SelectedValue <> "" Then
            If cmbCadena.SelectedValue <> "" Then
                PnlGrid.Visible = True

                CargarGrilla(gridReporte1, 104)
                CargarGrilla(gridReporte2, 103)
                CargarGrilla(gridReporte3, 100)
                CargarGrilla(gridReporte4, 102)
                CargarGrilla(gridReporte5, 101)
                CargarGrilla(gridReporte6, 105)
                CargarGrilla(gridReporte7, 108)
                CargarGrilla(gridReporte8, 109)
            Else
                PnlGrid.Visible = False
            End If
        Else
            PnlGrid.Visible = False
        End If
    End Sub

    Public Function CargarGrilla(ByVal Grilla As GridView, ByVal Grupo As Integer) As Integer
        RegionSQL = Acciones.Slc.cmb("US.id_region", cmbRegion.SelectedValue)
        CadenaSQL = Acciones.Slc.cmb("RE.id_cadena", cmbCadena.SelectedValue)

        CargaGrilla(ConexionSYM.localSqlServer, _
                    "select PROD.tipo_producto,PROD.nombre_producto, PROD.presentacion, AVG(HDET.precio)precio, " & _
                     "ROUND(AVG(HDET.precio)/PROD.presentacion,5) as PrecioGramo, " & _
                     "convert(nvarchar(10),(100*ROUND((AVG(HDET.precio)/PROD.presentacion)/(select TOP 1 ROUND(AVG(HDET.precio)/PROD.presentacion,5) " & _
                     "FROM Precios_Historial as H " & _
                     "INNER JOIN Precios_Productos_Historial_Det as HDET ON H.folio_historial=HDET.folio_historial " & _
                     "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena=H.id_cadena " & _
                     "INNER JOIN Productos as PROD ON PROD.id_producto = HDET.id_producto " & _
                     "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " and H.id_cadena=" & cmbCadena.SelectedValue & " and PROD.tipo_grupo=" & Grupo & " AND PROD.tipo_producto=1 " & _
                     "GROUP BY PROD.presentacion,HDET.id_producto ORDER BY PROD.presentacion),2)))+'%'PrecioMinimo " & _
                     "FROM Precios_Historial as H " & _
                     "INNER JOIN Precios_Productos_Historial_Det as HDET ON H.folio_historial=HDET.folio_historial " & _
                     "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena=H.id_cadena " & _
                     "INNER JOIN Productos as PROD ON PROD.id_producto = HDET.id_producto " & _
                     "INNER JOIN Grupos as LIN ON LIN.tipo_grupo= PROD.tipo_grupo " & _
                     "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " and H.id_cadena=" & cmbCadena.SelectedValue & " and PROD.tipo_grupo=" & Grupo & " " & _
                     "AND (select TOP 1 ROUND(AVG(HDET.precio)/PROD.presentacion,5) " & _
                     "FROM Precios_Historial as H " & _
                     "INNER JOIN Precios_Productos_Historial_Det as HDET ON H.folio_historial=HDET.folio_historial " & _
                     "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena=H.id_cadena " & _
                     "INNER JOIN Productos as PROD ON PROD.id_producto = HDET.id_producto " & _
                     "WHERE H.id_periodo =" & cmbPeriodo.SelectedValue & " And H.id_cadena =" & cmbCadena.SelectedValue & " And PROD.tipo_grupo =" & Grupo & " And PROD.tipo_producto = 1 " & _
                     "GROUP BY PROD.presentacion,HDET.id_producto ORDER BY PROD.presentacion)<>0 " & _
                     "GROUP BY PROD.tipo_producto,PROD.nombre_producto, PROD.presentacion,PROD.presentacion " & _
                     "order by 100*ROUND((AVG(HDET.precio)/PROD.presentacion)/(select TOP 1 ROUND(AVG(HDET.precio)/PROD.presentacion,5) " & _
                     "FROM Precios_Historial as H " & _
                     "INNER JOIN Precios_Productos_Historial_Det as HDET ON H.folio_historial=HDET.folio_historial " & _
                     "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena=H.id_cadena " & _
                     "INNER JOIN Productos as PROD ON PROD.id_producto = HDET.id_producto " & _
                     "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " and H.id_cadena=" & cmbCadena.SelectedValue & " and PROD.tipo_grupo=" & Grupo & " AND PROD.tipo_producto=1 " & _
                     "GROUP BY PROD.presentacion,HDET.id_producto ORDER BY PROD.presentacion),4) DESC", Grilla)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionSYM.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
        End If
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCadena.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionSYM.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionSYM.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)

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

    Private Sub gridReporte1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte1.RowDataBound
        If gridReporte1.Rows.Count = 0 Then
            gridReporte1.Visible = False : Else
            gridReporte1.Visible = True : End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            If gridReporte1.DataKeys(e.Row.RowIndex).Value.ToString() = 1 Then
                For C = 0 To 4
                    e.Row.Cells(C).BackColor = Drawing.Color.LemonChiffon
                Next C
            End If
        End If
    End Sub

    Private Sub gridReporte2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte2.RowDataBound
        If gridReporte2.Rows.Count = 0 Then
            gridReporte2.Visible = False : Else
            gridReporte2.Visible = True : End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            If gridReporte2.DataKeys(e.Row.RowIndex).Value.ToString() = 1 Then
                For C = 0 To 4
                    e.Row.Cells(C).BackColor = Drawing.Color.LemonChiffon
                Next C
            End If
        End If
    End Sub

    Private Sub gridReporte3_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte3.RowDataBound
        If gridReporte3.Rows.Count = 0 Then
            gridReporte3.Visible = False : Else
            gridReporte3.Visible = True : End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            If gridReporte3.DataKeys(e.Row.RowIndex).Value.ToString() = 1 Then
                For C = 0 To 4
                    e.Row.Cells(C).BackColor = Drawing.Color.LemonChiffon
                Next C
            End If
        End If
    End Sub

    Private Sub gridReporte4_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte4.RowDataBound
        If gridReporte4.Rows.Count = 0 Then
            gridReporte4.Visible = False : Else
            gridReporte4.Visible = True : End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            If gridReporte4.DataKeys(e.Row.RowIndex).Value.ToString() = 1 Then
                For C = 0 To 4
                    e.Row.Cells(C).BackColor = Drawing.Color.LemonChiffon
                Next C
            End If
        End If
    End Sub

    Private Sub gridReporte5_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte5.RowDataBound
        If gridReporte5.Rows.Count = 0 Then
            gridReporte5.Visible = False : Else
            gridReporte5.Visible = True : End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            If gridReporte5.DataKeys(e.Row.RowIndex).Value.ToString() = 1 Then
                For C = 0 To 4
                    e.Row.Cells(C).BackColor = Drawing.Color.LemonChiffon
                Next C
            End If
        End If
    End Sub

    Private Sub gridReporte6_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte6.RowDataBound
        If gridReporte6.Rows.Count = 0 Then
            gridReporte6.Visible = False : Else
            gridReporte6.Visible = True : End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            If gridReporte6.DataKeys(e.Row.RowIndex).Value.ToString() = 1 Then
                For C = 0 To 4
                    e.Row.Cells(C).BackColor = Drawing.Color.LemonChiffon
                Next C
            End If
        End If
    End Sub

    Private Sub gridReporte7_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte7.RowDataBound
        If gridReporte7.Rows.Count = 0 Then
            gridReporte7.Visible = False : Else
            gridReporte7.Visible = True : End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            If gridReporte7.DataKeys(e.Row.RowIndex).Value.ToString() = 1 Then
                For C = 0 To 4
                    e.Row.Cells(C).BackColor = Drawing.Color.LemonChiffon
                Next C
            End If
        End If
    End Sub

    Private Sub gridReporte8_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte8.RowDataBound
        If gridReporte8.Rows.Count = 0 Then
            gridReporte8.Visible = False : Else
            gridReporte8.Visible = True : End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            If gridReporte8.DataKeys(e.Row.RowIndex).Value.ToString() = 1 Then
                For C = 0 To 4
                    e.Row.Cells(C).BackColor = Drawing.Color.LemonChiffon
                Next C
            End If
        End If
    End Sub
End Class