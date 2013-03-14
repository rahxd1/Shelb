Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteBitacoraSYMDemos
    Inherits System.Web.UI.Page

    Dim RegionSel As String
    Dim PeriodoSQL, PromotorSQL, RegionSQL As String
    Dim Dato(4), Suma(4) As Integer

    Sub SQLCombo()
        RegionSel = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)

        PeriodoSQL = "SELECT id_periodo, nombre_periodo " & _
                         "FROM Demos_Periodos " & _
                         " ORDER BY fecha_inicio DESC"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Regiones as REG " & _
                    "WHERE REG.id_region<>0 " & _
                    " ORDER BY REG.nombre_region"

        PromotorSQL = "SELECT DISTINCT RE.id_usuario " & _
                    "FROM Demos_Rutas_Eventos as RE " & _
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
                        "SELECT REG.nombre_region, RE.id_usuario, RE.demos, COUNT(H.nombre_tienda)Tiendas," & _
                        "CASE WHEN COUNT(H.nombre_tienda)=0 then 0 else " & _
                        "(100*COUNT(H.nombre_tienda))/RE.demos end Porcentaje " & _
                        "FROM Demos_Rutas_Eventos as RE " & _
                        "INNER JOIN Usuarios as US ON RE.id_usuario=US.id_usuario " & _
                        "INNER JOIN Regiones as REG ON REG.id_region=US.id_region " & _
                        "FULL JOIN Demos_Historial as H  " & _
                        "ON H.id_periodo= RE.id_periodo AND H.id_usuario=RE.id_usuario " & _
                        "WHERE RE.id_periodo =" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + PromotorSQL + " " & _
                        "GROUP BY REG.nombre_region,RE.id_usuario, RE.demos", _
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

            For i = 2 To 3
                Dato(i) = e.Row.Cells(i).Text
                Suma(i) = Suma(i) + Dato(i)
            Next i

            For i = 0 To CInt(gridReporte.Rows.Count) - 0
                If e.Row.Cells(4).Text < 100 Then
                    e.Row.Cells(4).BackColor = Drawing.Color.Red : Else
                    e.Row.Cells(4).BackColor = Drawing.Color.GreenYellow : End If

                If e.Row.Cells(4).Text > 0 And e.Row.Cells(4).Text < 100 Then
                    e.Row.Cells(4).BackColor = Drawing.Color.Orange : End If
            Next i

            Dim Porcentaje As String
            Porcentaje = e.Row.Cells(4).Text
            e.Row.Cells(4).Text = Porcentaje & "%"
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(2).Text = Suma(2)
            e.Row.Cells(3).Text = Suma(3)
            e.Row.Cells(4).Text = FormatPercent(Suma(3) / Suma(2), 0, 0, 0, 0)
        End If

    End Sub

End Class