Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteTicketsSchick
    Inherits System.Web.UI.Page

    Dim PromotorSel, RegionSel, CadenaSel, TiendaSel As String
    Dim PeriodoSQL, PromotorSQL, RegionSQL, CadenaSQL, TiendaSQL As String
    Dim Dato(6), Suma(6) As Integer
    Dim TodasFilas, TodosCampos As String
    Dim PromosTotales As Integer

    Sub SQLCombo()
        If cmbRegion.SelectedValue <> "" Then
            If cmbRegion.SelectedValue <> 0 Then
                RegionSel = "AND REG.id_region=" & cmbRegion.SelectedValue & " " : Else
                RegionSel = "" : End If : End If

        If Not cmbPromotor.SelectedValue = "" Then
            PromotorSel = "AND RUT.id_usuario='" & cmbPromotor.SelectedValue & "'" : Else
            PromotorSel = "" : End If

        If Not cmbCadena.SelectedValue = "" Then
            CadenaSel = "AND CAD.id_cadena=" & cmbCadena.SelectedValue & "" : Else
            CadenaSel = "" : End If

        PeriodoSQL = "SELECT id_periodo, nombre_periodo " & _
                    "FROM Schick_Periodos ORDER BY fecha_inicio DESC"

        RegionSQL = "SELECT DISTINCT nombre_region, id_region " & _
                    "FROM Regiones WHERE id_region<>0 ORDER BY nombre_region"

        PromotorSQL = "SELECT DISTINCT RUT.id_usuario " & _
                    "FROM Schick_CatRutas AS RUT " & _
                    "INNER JOIN Schick_Tiendas AS TI ON TI.id_tienda = RUT.id_tienda " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    "WHERE TI.estatus = 1 " & _
                    " " + RegionSel + " " & _
                    " ORDER BY RUT.id_usuario"

        CadenaSQL = "SELECT DISTINCT CAD.id_cadena, CAD.nombre_cadena " & _
                    "FROM Schick_CatRutas AS RUT " & _
                    "INNER JOIN Schick_Tiendas AS TI ON TI.id_tienda = RUT.id_tienda " & _
                    "INNER JOIN Cadenas_Tiendas AS CAD ON TI.id_cadena = CAD.id_cadena " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    "WHERE TI.estatus = 1 " & _
                    " " + RegionSel + " " & _
                    " " + PromotorSel + " " & _
                    " ORDER BY CAD.nombre_cadena"

        TiendaSQL = "SELECT DISTINCT RUT.id_tienda, TI.nombre " & _
                    "FROM Schick_CatRutas AS RUT " & _
                    "INNER JOIN Schick_Tiendas AS TI ON TI.id_tienda = RUT.id_tienda " & _
                    "INNER JOIN Cadenas_Tiendas AS CAD ON TI.id_cadena = CAD.id_cadena " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    " " + RegionSel + " " & _
                    " " + PromotorSel + " " & _
                    " " + CadenaSel + " " & _
                    " ORDER BY TI.nombre"
    End Sub

    Sub CargarReporte()
        Using cnn As New SqlConnection(ConexionEnergizer.localSqlServer)

            If cmbRegion.SelectedValue <> 0 Then
                RegionSQL = "AND REG.id_region=" & cmbRegion.SelectedValue & " " : Else
                RegionSQL = "" : End If

            If Not cmbPromotor.SelectedValue = "" Then
                PromotorSQL = "AND H.id_usuario='" & cmbPromotor.SelectedValue & "' " : Else
                PromotorSQL = "" : End If

            If Not cmbCadena.SelectedValue = "" Then
                CadenaSQL = "AND TI.id_cadena=" & cmbCadena.SelectedValue & " " : Else
                CadenaSQL = "" : End If

            If Not cmbTienda.SelectedValue = "" Then
                TiendaSQL = "AND TI.id_tienda=" & cmbTienda.SelectedValue & " " : Else
                TiendaSQL = "" : End If

            VerPromocionales()

            Dim sql As String = "SELECT DISTINCT REG.nombre_region as Region, CAD.nombre_cadena as Cadena, " & _
                        "TI.nombre as Tienda, H.ticket as Ticket, H.nombre_cliente as 'Nombre Cliente'" & _
                        " " + TodosCampos + " " & _
                        "FROM Schick_Promos_Historial as H  " & _
                        " " + TodasFilas + " " & _
                        "INNER JOIN Schick_Tiendas as TI ON TI.id_tienda= H.id_tienda " & _
                        "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region " & _
                        "INNER JOIN Cadenas_Tiendas as CAD ON TI.id_cadena= CAD.id_cadena " & _
                        "WHERE H.id_periodo = " & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + " " & _
                        " " + PromotorSQL + " " & _
                        " " + CadenaSQL + " " & _
                        " " + TiendaSQL + " " & _
                        "ORDER BY REG.nombre_region,CAD.nombre_cadena, TI.nombre"

            Dim cmd As New SqlCommand(sql, cnn)
            Dim adapter As New SqlDataAdapter
            adapter.SelectCommand = cmd
            cnn.Open()
            Dim dataset As New DataSet
            adapter.Fill(dataset, "nombre_region")

            cnn.Close()
            gridReporte.DataSource = dataset
            gridReporte.DataBind()
            cnn.Dispose()
        End Using
    End Sub

    Sub VerPromocionales()
        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        cnn.Open()
        Dim SQL As New SqlCommand("SELECT * FROM Schick_Promos", cnn)
        Dim tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQL)
        Data.Fill(tabla)
        If tabla.Rows.Count > 0 Then
            PromosTotales = tabla.Rows.Count


            Dim Producto(20), Campos(20) As String
            For i = 0 To PromosTotales - 1
                Dim Producto1 As String
                Producto1 = tabla.Rows(i)("id_promo")
                Campos(i) = ", ISNULL(HDET" & (i) & ".cantidad,0) as '" & tabla.Rows(i)("nombre_promo") & "'"
                Producto(i) = "FULL JOIN (SELECT DISTINCT H.id_tienda, H.folio_historial, HDET.cantidad FROM Schick_Promos_Historial as H " & _
                        "INNER JOIN Schick_Promos_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial " & _
                        "WHERE HDET.id_promo=" & Producto1 & ") as HDET" & (i) & " " & _
                        "ON H.id_tienda= HDET" & (i) & ".id_tienda AND H.folio_historial=HDET" & (i) & ".folio_historial "
            Next

            For i = 0 To PromosTotales
                TodosCampos = TodosCampos + Campos(i)
                TodasFilas = TodasFilas + Producto(i)
            Next
        End If

        SQL.Dispose()
        tabla.Dispose()
        Data.Dispose()
        cnn.Close()
        cnn.Dispose()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)
            CargarReporte()
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionEnergizer.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionEnergizer.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionEnergizer.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionEnergizer.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionEnergizer.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCadena.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionEnergizer.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbTienda_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbTienda.SelectedIndexChanged
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
        Response.AddHeader("Content-Disposition", "attachment;filename=ReporteEntregaPromocionales" + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 5 To 10
                Dato(i) = e.Row.Cells(i).Text
                Suma(i) = Suma(i) + Dato(i)
            Next i
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            For i = 5 To 10
                e.Row.Cells(i).Text = Suma(i)
            Next i
        End If
    End Sub

End Class