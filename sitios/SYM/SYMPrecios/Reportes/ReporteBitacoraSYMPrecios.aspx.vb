Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteBitacoraSYMPrecios
    Inherits System.Web.UI.Page

    Dim RegionSel As String
    Dim PeriodoSQL, PromotorSQL, RegionSQL As String
    Dim Dato(10), Suma(10) As Integer

    Sub SQLCombo()
        RegionSel = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)

        PeriodoSQL = "SELECT id_periodo, nombre_periodo " & _
                         "FROM Precios_Periodos " & _
                         " ORDER BY fecha_inicio DESC"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Regiones as REG " & _
                    "WHERE REG.id_region<>0 " & _
                    " ORDER BY REG.nombre_region"

        PromotorSQL = "SELECT DISTINCT RE.id_usuario " & _
                    "FROM Precios_Rutas_Eventos as RE " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario=RE.id_usuario " & _
                    "WHERE US.id_usuario<>'' " & _
                    " " + RegionSel + " " & _
                    " ORDER BY RE.id_usuario "
    End Sub

    Sub CargarReporte()
        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("US.id_region", cmbRegion.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("RE.id_usuario", cmbPromotor.SelectedValue)

            CargaGrilla(ConexionSYM.localSqlServer, _
                        "SELECT REG.nombre_region,RE.id_usuario, COUNT(DISTINCT RE.id_cadena) AS Cadenas," & _
                        "ISNULL(Capturas.Capturas,0) as Capturas, ISNULL(H.Productos,0)Productos, " & _
                        "ISNULL(Incompletas.Incompletas,0) as Incompletas, " & _
                        "(100* ISNULL(Capturas.Capturas,0)/COUNT(DISTINCT RE.id_cadena)) as Total " & _
                        "FROM Precios_Rutas_Eventos as RE " & _
                        "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena= RE.id_cadena " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario= RE.id_usuario " & _
                        "INNER JOIN Precios_CatRutas as RUT ON RUT.id_cadena = RE.id_cadena " & _
                        "INNER JOIN Regiones as REG ON REG.id_region= US.id_region " & _
                        "FULL JOIN (select H.id_periodo,H.id_usuario,COUNT(HDET.id_producto)Productos " & _
                        "FROM Precios_Historial as H " & _
                        "INNER JOIN Precios_Productos_Historial_Det as HDET ON HDET.folio_historial=H.folio_historial " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " GROUP BY H.id_periodo, H.id_usuario)H " & _
                        "ON H.id_periodo= RE.id_periodo AND H.id_usuario=RE.id_usuario " & _
                        "FULL JOIN (SELECT id_usuario, COUNT(RE.estatus) AS Capturas " & _
                        "FROM Precios_Rutas_Eventos as RE WHERE estatus=1 AND id_periodo=" & cmbPeriodo.SelectedValue & " GROUP BY id_usuario)Capturas " & _
                        "ON Capturas.id_usuario = RE.id_usuario " & _
                        "FULL JOIN (SELECT id_usuario, COUNT(RE.estatus) AS Incompletas " & _
                        "FROM Precios_Rutas_Eventos as RE WHERE estatus=2 AND id_periodo=" & cmbPeriodo.SelectedValue & " GROUP BY id_usuario)Incompletas " & _
                        "ON Incompletas.id_usuario = RE.id_usuario " & _
                        "WHERE RE.id_periodo=" & cmbPeriodo.SelectedValue & "" & _
                        " " + RegionSQL + PromotorSQL + " " & _
                        " GROUP BY RE.id_usuario,Capturas,Incompletas,RE.id_periodo, REG.nombre_region, Productos", _
                        gridReporte)
        Else
            gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionSYM.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionSYM.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionSYM.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Bitácora captura " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            For i = 2 To 5
                Dato(i) = e.Row.Cells(i).Text
                Suma(i) = Suma(i) + Dato(i)
            Next i

            For i = 0 To CInt(gridReporte.Rows.Count) - 0
                If e.Row.Cells(6).Text < 100 Then
                    e.Row.Cells(6).BackColor = Drawing.Color.Red : Else
                    e.Row.Cells(6).BackColor = Drawing.Color.GreenYellow : End If
            Next i

            Dim Porcentaje As String
            Porcentaje = e.Row.Cells(6).Text
            e.Row.Cells(6).Text = Porcentaje & "%"
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(2).Text = Suma(2)
            e.Row.Cells(3).Text = Suma(3)
            e.Row.Cells(4).Text = Suma(4)
            e.Row.Cells(5).Text = Suma(5)
            e.Row.Cells(6).Text = FormatPercent(Suma(3) / Suma(2), 0, 0, 0, 0)
        End If

    End Sub

End Class