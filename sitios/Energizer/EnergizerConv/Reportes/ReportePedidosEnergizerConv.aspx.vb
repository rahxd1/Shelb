Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReportePedidosEnergizerConv
    Inherits System.Web.UI.Page

    Dim PeriodoSel, PromotorSel, RegionSel, PeriodoActual As String
    Dim PeriodoSQL, PromotorSQL, RegionSQL, TiendaSQL As String

    Sub SQLCombo()
        If cmbRegion.SelectedValue <> "" Then
            If cmbRegion.SelectedValue <> 0 Then
                RegionSel = "AND REG.id_region=" & cmbRegion.SelectedValue & " " : Else
                RegionSel = "" : End If : End If

        If cmbPromotor.SelectedValue = "" Then
            PromotorSel = "":Else
            PromotorSel = " AND RE.id_usuario='" & cmbPromotor.SelectedValue & "' " : End If

        PeriodoSQL = "SELECT id_periodo, nombre_periodo " & _
                         "FROM Energizer_Conv_Periodos " & _
                         " ORDER BY fecha_inicio DESC"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Energizer_Conv_Rutas_Eventos AS RE " & _
                    "INNER JOIN Energizer_Conv_Tiendas AS TI ON TI.id_tienda = RE.id_tienda " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON TI.id_cadena = CAD.id_cadena " & _
                    " ORDER BY REG.nombre_region"

        PromotorSQL = "SELECT DISTINCT RE.id_usuario " & _
                    "FROM Energizer_Conv_Rutas_Eventos AS RE " & _
                    "INNER JOIN Energizer_Conv_Tiendas AS TI ON TI.id_tienda = RE.id_tienda " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    "WHERE TI.estatus =1 " & _
                    " " + PeriodoSel + " " & _
                    " " + RegionSel + " " & _
                    " ORDER BY RE.id_usuario"

        TiendaSQL = "SELECT DISTINCT RE.id_tienda, TI.nombre " & _
                   "FROM Energizer_Conv_Rutas_Eventos AS RE " & _
                   "INNER JOIN Energizer_Conv_Tiendas AS TI ON RE.id_tienda = TI.id_tienda " & _
                   "INNER JOIN Cadenas_Tiendas as CAD ON TI.id_cadena = CAD.id_cadena " & _
                   "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                   "WHERE TI.estatus =1 " & _
                   " " + RegionSel + " " & _
                   " " + PromotorSel + " " & _
                   " ORDER BY TI.nombre"
    End Sub

    Sub CargarReporte()
        If Not cmbPeriodo.SelectedValue = "" Then
            PeriodoSQL = "AND H.id_periodo=" & cmbPeriodo.SelectedValue & " " : Else
            PeriodoSQL = "" : End If

        If cmbRegion.SelectedValue <> 0 Then
            RegionSQL = "AND REG.id_region=" & cmbRegion.SelectedValue & " " : Else
            RegionSQL = "" : End If

        If Not cmbPromotor.SelectedValue = "" Then
            PromotorSQL = "AND H.id_usuario='" & cmbPromotor.SelectedValue & "' " : Else
            PromotorSQL = "" : End If

        If Not cmbTienda.SelectedValue = "" Then
            TiendaSQL = "AND TI.id_tienda=" & cmbTienda.SelectedValue & " " : Else
            TiendaSQL = "" : End If

        CargaGrilla(ConexionEnergizer.localSqlServer, _
                    "SELECT DISTINCT PER.nombre_periodo, REG.nombre_region, H.id_usuario, CAD.nombre_cadena, Ti.nombre, sum(HDET.pedidos) as PedidosTotales " & _
                    "FROM Energizer_Conv_Historial as H " & _
                    "INNER JOIN Energizer_Conv_Productos_Historial_Det as HDET ON H.folio_historial = HDET.folio_historial " & _
                    "INNER JOIN Energizer_Conv_Periodos as PER ON H.id_periodo= PER.id_periodo " & _
                    "INNER JOIN Energizer_Conv_Tiendas as TI ON TI.id_tienda= H.id_tienda " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena= TI.id_cadena " & _
                    "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region " & _
                    "WHERE HDET.pedidos <>'0' " & _
                    " " + PeriodoSQL + " " & _
                    " " + RegionSQL + " " & _
                    " " + PromotorSQL + " " & _
                    " " + TiendaSQL + " " & _
                    "GROUP BY PER.nombre_periodo, REG.nombre_region, H.id_usuario, CAD.nombre_cadena, Ti.nombre " & _
                    "ORDER BY PER.nombre_periodo, REG.nombre_region, H.id_usuario, CAD.nombre_cadena, Ti.nombre ", _
                    gridReporte)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

            BuscaPeriodoActual()
            cmbPeriodo.SelectedValue = PeriodoActual

            CargarReporte()
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionEnergizer.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionEnergizer.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Promocionales Competencia " + cmbPeriodo.SelectedItem.ToString() + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Sub BuscaPeriodoActual()
        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        cnn.Open()
        Dim cmdCap As New SqlCommand("SELECT * FROM Energizer_Conv_Periodos ORDER BY id_periodo DESC", cnn)
        Dim tabla As New DataTable
        Dim da As New SqlDataAdapter(cmdCap)
        da.Fill(tabla)

        If tabla.Rows.Count > 0 Then
            PeriodoActual = tabla.Rows(0)("id_periodo")
        End If

        cmdCap.Dispose()
        tabla.Dispose()
        da.Dispose()
        cnn.Close()
        cnn.Dispose()
    End Sub

End Class