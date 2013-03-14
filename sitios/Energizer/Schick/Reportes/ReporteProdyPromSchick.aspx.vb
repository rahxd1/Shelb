﻿Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteProdyPromSchick
    Inherits System.Web.UI.Page

    Dim PromotorSel, RegionSel, CadenaSel As String
    Dim PeriodoSQL, PromotorSQL, RegionSQL, CadenaSQL, TiendaSQL As String

    Sub SQLCombo()
        If cmbRegion.SelectedValue <> "" Then
            If cmbRegion.SelectedValue <> 0 Then
                RegionSel = "AND REG.id_region=" & cmbRegion.SelectedValue & " " : Else
                RegionSel = "" : End If : End If

        If Not cmbPromotor.Text = "" Then
            PromotorSel = "AND RUT.id_usuario='" & cmbPromotor.SelectedValue & "'": Else
            PromotorSel = "":End If

        If Not cmbCadena.Text = "" Then
            CadenaSel = "AND CAD.id_cadena=" & cmbCadena.SelectedValue & " " : Else
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

            Dim sql As String = "SELECT DISTINCT nombre_region,id_usuario,nombre_cadena, nombre, no_tienda, " & _
                                "[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15], " & _
                                "[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[100],[200],[300],[400],[500],[600] " & _
                                "FROM (SELECT DISTINCT REG.nombre_region, H.id_usuario, CAD.nombre_cadena, TI.nombre, TI.no_tienda, " & _
                                "HDET.id_producto, HDET.cantidad " & _
                                "From Schick_Historial as H  " & _
                                "FULL JOIN Schick_Productos_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial " & _
                                "INNER JOIN Schick_Tiendas as TI ON TI.id_tienda = H.id_tienda  " & _
                                "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                                "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = TI.id_cadena " & _
                                "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                                "UNION ALL " & _
                                "SELECT DISTINCT REG.nombre_region, H.id_usuario, CAD.nombre_cadena, TI.nombre, TI.no_tienda, " & _
                                "id_promo as id_producto, cantidad " & _
                                "From Schick_Promos_Historial as H  " & _
                                "FULL JOIN Schick_Promos_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial " & _
                                "INNER JOIN Schick_Tiendas as TI ON TI.id_tienda = H.id_tienda  " & _
                                "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                                "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = TI.id_cadena " & _
                                "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                                ")AS Datos PIVOT(SUM(cantidad) " & _
                                "FOR id_producto IN ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15], " & _
                                "[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[100],[200],[300],[400],[500],[600])) AS PivotTable " & _
                                " " + RegionSQL + " " & _
                                " " + PromotorSQL + " " & _
                                " " + CadenaSQL + " " & _
                                " " + TiendaSQL + " "

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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Productos y promocionales por tienda " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

End Class