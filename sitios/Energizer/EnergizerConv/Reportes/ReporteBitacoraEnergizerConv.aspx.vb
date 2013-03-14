Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteBitacoraEnergizerConv
    Inherits System.Web.UI.Page

    Dim PeriodoSel, PromotorSel, RegionSel As String
    Dim PeriodoSQL, PromotorSQL, RegionSQL As String
    Dim Suma(2) As Integer

    Sub SQLCombo()
        If cmbRegion.SelectedValue <> "" Then
            If cmbRegion.SelectedValue <> 0 Then
                RegionSel = "AND REG.id_region=" & cmbRegion.SelectedValue & " " : Else
                RegionSel = "" : End If : End If

        PeriodoSQL = "SELECT id_periodo, nombre_periodo " & _
                     "FROM Energizer_Conv_Periodos " & _
                     " ORDER BY fecha_inicio DESC"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM  Energizer_Conv_Tiendas AS TI " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON TI.id_cadena = CAD.id_cadena " & _
                    " ORDER BY REG.nombre_region"

        PromotorSQL = "SELECT DISTINCT RE.id_usuario " & _
                    "FROM Energizer_Conv_Rutas_Eventos AS RE " & _
                    "INNER JOIN Energizer_Conv_Tiendas AS TI ON TI.id_tienda = RE.id_tienda " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON TI.id_cadena = CAD.id_cadena " & _
                    "WHERE TI.estatus=1 " & _
                    " " + PeriodoSel + " " & _
                    " " + RegionSel + " " & _
                    " ORDER BY RE.id_usuario"
    End Sub

    Sub CargarReporte()
        If cmbRegion.SelectedValue <> 0 Then
            RegionSQL = "AND REG.id_region=" & cmbRegion.SelectedValue & " " : Else
            RegionSQL = "" : End If

        If Not cmbPromotor.SelectedValue = "" Then
            PromotorSQL = "AND RE.id_usuario = '" & cmbPromotor.SelectedValue & "' " : Else
            PromotorSQL = "" : End If

        CargaGrilla(ConexionEnergizer.localSqlServer, _
                    "SELECT REG.nombre_region,RE.id_usuario, COUNT(RE.id_usuario) AS Tiendas, SUM(RE.estatus) AS Capturas " & _
                    "FROM Energizer_Conv_Rutas_Eventos as RE " & _
                    "INNER JOIN Energizer_Conv_Tiendas as TI " & _
                    "ON TI.id_tienda= RE.id_tienda " & _
                    "INNER JOIN Regiones as REG " & _
                    "ON REG.id_region= TI.id_region " & _
                    "WHERE RE.id_periodo = '" & cmbPeriodo.SelectedValue & "'" & _
                    " " + RegionSQL + " " & _
                    " " + PromotorSQL + " " & _
                    " GROUP BY RE.id_usuario, RE.id_periodo, REG.nombre_region", Me.gridReporte)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

            CargarReporte()
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionEnergizer.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Suma(0) = Suma(0) + e.Row.Cells(2).Text ''Suma Tiendas
            Suma(1) = Suma(1) + e.Row.Cells(3).Text ''Suma Capturas

            If e.Row.Cells(2).Text > e.Row.Cells(3).Text Then
                e.Row.Cells(4).BackColor = Drawing.Color.Red : Else
                e.Row.Cells(4).BackColor = Drawing.Color.GreenYellow : End If

            e.Row.Cells(4).Text = FormatPercent(e.Row.Cells(3).Text / e.Row.Cells(2).Text, 0, 0, 0, 0)
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.ForeColor = Drawing.Color.White
            e.Row.Cells(2).Text = Suma(0)
            e.Row.Cells(3).Text = Suma(1)
            e.Row.Cells(4).Text = FormatPercent(Suma(1) / Suma(0), 0, 0, 0, 0)
        End If

    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        CargarReporte()
    End Sub

    Sub CargarDetalle()
        Using cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
            Dim sql As String = "SELECT RE.id_usuario,CAD.nombre_cadena,TI.nombre, RE.estatus " & _
                                "FROM Energizer_Conv_Rutas_Eventos as RE " & _
                                "INNER JOIN Energizer_Conv_Tiendas as TI ON TI.id_tienda= RE.id_tienda " & _
                                "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region " & _
                                "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena =  TI.id_cadena " & _
                                "WHERE RE.id_periodo = '" & cmbPeriodo.SelectedValue & "'" & _
                                "AND RE.id_usuario = '" & cmbPromotor.SelectedValue & "' " & _
                                " ORDER BY RE.id_usuario, TI.nombre"

            Dim cmd As New SqlCommand(sql, cnn)
            Dim adapter As New SqlDataAdapter
            adapter.SelectCommand = cmd
            cnn.Open()
            Dim dataset As New DataSet
            adapter.Fill(dataset, "id_usuario")

            cnn.Close()
            gridDetalle.DataSource = dataset
            gridDetalle.DataBind()
            cnn.Dispose()
        End Using
    End Sub

    Sub CargarDetalleGenerales()
        Using cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
            Dim sql As String = "SELECT RE.id_usuario,CAD.nombre_cadena,TI.nombre, RE.estatus " & _
                                "FROM Energizer_Conv_Rutas_Eventos as RE " & _
                                "INNER JOIN Energizer_Conv_Tiendas as TI ON TI.id_tienda= RE.id_tienda " & _
                                "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region " & _
                                "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena =  TI.id_cadena " & _
                                "WHERE RE.id_periodo = '" & cmbPeriodo.Text & "'" & _
                                "AND RE.estatus = 0 " & _
                                " ORDER BY RE.id_usuario, TI.nombre"

            Dim cmd As New SqlCommand(sql, cnn)
            Dim adapter As New SqlDataAdapter
            adapter.SelectCommand = cmd
            cnn.Open()
            Dim dataset As New DataSet
            adapter.Fill(dataset, "id_usuario")

            cnn.Close()
            gridDetalle.DataSource = dataset
            gridDetalle.DataBind()
            cnn.Dispose()
        End Using
    End Sub

    Protected Sub btnDetalles_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDetalles.Click
        CargarDetalleGenerales()
        pnlDetalle.Visible = True
    End Sub

    Private Sub gridDetalle_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridDetalle.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 0 To CInt(gridReporte.Rows.Count) - 0
                If e.Row.Cells(3).Text = 0 Then
                    e.Row.Cells(0).ForeColor = Drawing.Color.Red
                    e.Row.Cells(1).ForeColor = Drawing.Color.Red
                    e.Row.Cells(2).ForeColor = Drawing.Color.Red
                    e.Row.Cells(3).ForeColor = Drawing.Color.Red
                End If
            Next i
        End If
    End Sub

    Sub Excel2()
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.gridReporte.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.gridDetalle)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Detalle Bitacora de captura " + cmbPromotor.SelectedValue + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Bitacora Captura " + cmbPeriodo.SelectedItem.ToString() + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

End Class