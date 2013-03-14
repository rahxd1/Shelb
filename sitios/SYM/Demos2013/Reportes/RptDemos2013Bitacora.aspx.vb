Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc
Partial Public Class RptDemos2013Bitacora
    Inherits System.Web.UI.Page
    Dim RegionSel As String
    Dim PeriodoSQL, PromotorSQL, RegionSQL As String
    Dim Dato(4), Suma(4) As Integer

    Sub SQLCombo()
        If cmbRegion.SelectedValue = "" Then
            RegionSel = "" : Else
            RegionSel = "AND id_region=" & cmbRegion.SelectedValue & " " : End If

        PeriodoSQL = "SELECT id_periodo, nombre_periodo " & _
                         "FROM SYM_D_Periodos " & _
                         " ORDER BY fecha_de_inicio DESC"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Regiones as REG " & _
                    "WHERE REG.id_region<>0 " & _
                    " ORDER BY REG.nombre_region"

        PromotorSQL = "SELECT DISTINCT RE.id_usuario " & _
                    "FROM SYM_D_Rutas_Eventos as RE " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario=RE.id_usuario " & _
                    "WHERE US.id_usuario<>'' " & _
                    " " + RegionSel + " " & _
                    " ORDER BY RE.id_usuario "
    End Sub

    Sub CargarReporte()
        If cmbPeriodo.SelectedValue <> "" Then
            If cmbRegion.SelectedValue = "" Then
                RegionSQL = "" : Else
                RegionSQL = "AND US.id_region=" & cmbRegion.SelectedValue & " " : End If

            If cmbPromotor.SelectedValue = "" Then
                PromotorSQL = "" : Else
                PromotorSQL = "AND RU.id_usuario = '" & cmbPromotor.SelectedValue & "' " : End If

            CargaGrilla(ConexionSYM.localSqlServer, _
                       "SELECT REG.nombre_region AS REGION, RU.id_usuario, COUNT(RU.id_tienda)as TIENDAS, SUM(RU.status_ventas) AS CAPTURA_VENTAS, " & _
                       "SUM(RU.status_imagenes) as CAPTURA_IMAGENES, (((100*(SUM(RU.status_ventas))/COUNT(RU.id_tienda) + (100*(SUM(RU.status_imagenes)))/COUNT(RU.id_tienda)))/COUNT(RU.id_tienda))PORCENTAJE " & _
                       "FROM SYM_D_Rutas_Eventos AS RU " & _
                       "INNER JOIN Usuarios AS US on US.id_usuario = RU.id_usuario " & _
                        "INNER JOIN Regiones as REG on REG.id_region = US.id_region " & _
                        "WHERE RU.id_periodo =" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + PromotorSQL + " " & _
                        "GROUP BY REG.nombre_region,RU.id_usuario", _
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

            For i = 2 To 4
                Dato(i) = e.Row.Cells(i).Text
                Suma(i) = Suma(i) + Dato(i)
            Next i

            For i = 0 To CInt(gridReporte.Rows.Count) - 0
                If e.Row.Cells(5).Text < 100 Then
                    e.Row.Cells(5).BackColor = Drawing.Color.Red : Else
                    e.Row.Cells(5).BackColor = Drawing.Color.GreenYellow : End If

                If e.Row.Cells(5).Text > 0 And e.Row.Cells(5).Text < 100 Then
                    e.Row.Cells(5).BackColor = Drawing.Color.Orange : End If
            Next i

            Dim Porcentaje As String
            Porcentaje = e.Row.Cells(5).Text
            e.Row.Cells(5).Text = Porcentaje & "%"
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(2).Text = Suma(2)
            e.Row.Cells(3).Text = Suma(3)
            e.Row.Cells(4).Text = Suma(4)
            e.Row.Cells(5).Text = FormatPercent(Suma(3) / Suma(4), 0, 0, 0, 0)
        End If

    End Sub

End Class