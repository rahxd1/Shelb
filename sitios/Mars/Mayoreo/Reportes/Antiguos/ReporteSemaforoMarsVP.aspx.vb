Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports ISODates.Dates
Imports System.Security.Cryptography
Imports InfoSoftGlobal

Partial Public Class ReporteSemaforoMarsVP
    Inherits System.Web.UI.Page

    Dim PeriodoSel, SemanaSel, PromotorSel, RegionSel, CadenaSel, TiendaSel As String
    Dim PeriodoSQL, SemanaSQL, PromotorSQL, RegionSQL, CadenaSQL, TiendaSQL As String
    Dim PeriodoActual As String

    Sub SQLCombo()
        PeriodoSel = Acciones.Slc.cmb("PER.orden", cmbPeriodo.SelectedValue)
        RegionSel = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
        PromotorSel = Acciones.Slc.cmb("RE.id_usuario", cmbPromotor.SelectedValue)
        CadenaSel = Acciones.Slc.cmb("TI.id_cadena", cmbCadena.SelectedValue)

        PeriodoSQL = "SELECT DISTINCT H.id_periodo, PER.nombre_periodo, PER.fecha_fin_periodo " & _
                     "FROM May_Productos_Historial_Det as H " & _
                     "INNER JOIN Periodos as PER ON H.id_periodo = PER.id_periodo " & _
                     "ORDER BY PER.fecha_fin_periodo DESC"

        SemanaSQL = "SELECT DISTINCT id_semana, nombre_semana " & _
                    "FROM Periodos ORDER BY id_semana"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM PM_Rutas_Eventos AS RE " & _
                    "INNER JOIN PM_Tiendas AS TI ON TI.id_tienda = RE.id_tienda " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    "WHERE REG.id_region <>0 " + PeriodoSel + " " & _
                    " ORDER BY REG.nombre_region"

        PromotorSQL = "SELECT DISTINCT RE.id_usuario " & _
                    "FROM PM_Rutas_Eventos AS RE " & _
                    "INNER JOIN PM_Tiendas AS TI ON TI.id_tienda = RE.id_tienda " & _
                    "WHERE TI.Estatus=1 " & _
                    " " + PeriodoSel + RegionSel + " " & _
                    " ORDER BY RE.id_usuario"

        CadenaSQL = "SELECT DISTINCT CAD.id_cadena, CAD.nombre_cadena " & _
                    "FROM Cadenas_Mayoreo as CAD " & _
                    "INNER JOIN PM_Tiendas AS TI ON TI.id_cadena = CAD.id_cadena " & _
                    "INNER JOIN PM_Rutas_Eventos AS RE ON TI.id_tienda = RE.id_tienda " & _
                    "WHERE TI.Estatus=1 " & _
                    " " + PeriodoSel + " " & _
                    " " + RegionSel + PromotorSel + " " & _
                    " ORDER BY CAD.nombre_cadena"

        TiendaSQL = "SELECT DISTINCT RE.id_tienda, TI.nombre " & _
                   "FROM PM_Rutas_Eventos AS RE " & _
                   "INNER JOIN PM_Tiendas AS TI ON RE.id_tienda = TI.id_tienda " & _
                   "WHERE TI.Estatus=1 " & _
                   " " + PeriodoSel + RegionSel + " " & _
                   " " + PromotorSel + CadenaSel + " " & _
                   " ORDER BY TI.nombre"
    End Sub

    Sub CargarReporte()
        If cmbPeriodo.SelectedValue <> "" Then
            SemanaSQL = Acciones.Slc.cmb("H.id_semana", cmbSemana.SelectedValue)
            RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("TI.id_cadena", cmbCadena.SelectedValue)
            TiendaSQL = Acciones.Slc.cmb("TI.id_tienda", cmbTienda.SelectedValue)

            CargaGrilla(ConexionMars.localSqlServer, _
                        "select H.folio_historial, REG.nombre_region, H.id_usuario, CAD.nombre_cadena, TTI.descripcion_tienda,TI.codigo,H.id_tienda, TI.nombre, " & _
                        "CASE H.call_center " & _
                        "when 0 then '<img src=../../Img/Cerrado.gif />' " & _
                        "when 1 then '<img src=../../Img/Capturado.gif />'  " & _
                        "when 2 then '<img src=../../Img/Falta.gif />' end as img_status, " & _
                        "H.id_periodo, H.id_semana, PER.nombre_periodo, PER.nombre_semana, H.id_dia, " & _
                        "ISNULL(PROD1.id_producto,0)id_producto, ISNULL(PROD1.precio,0)Precio1, ISNULL(PROD1.precio_pieza,0)Producto1, " & _
                        "ISNULL(PROD2.id_producto,0)id_producto, ISNULL(PROD2.precio,0)Precio2, ISNULL(PROD2.precio_pieza,0)Producto2, " & _
                        "ISNULL(PROD3.id_producto,0)id_producto, ISNULL(PROD3.precio,0)Precio3, ISNULL(PROD3.precio_pieza,0)Producto3, " & _
                        "ISNULL(PROD4.id_producto,0)id_producto, ISNULL(PROD4.precio,0)Precio4, ISNULL(PROD4.precio_pieza,0)Producto4 " & _
                        "FROM May_Productos_Historial_Det as H " & _
                        "INNER JOIN (SELECT DISTINCT HDET.folio_historial " & _
                        "FROM May_Productos_Historial_Det as HDET  " & _
                        "INNER JOIN  May_Productos_Historial_Det as H ON HDET.folio_historial = H.folio_historial " & _
                        "INNER JOIN PM_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                        "INNER JOIN PM_Tipo_Tiendas as TTI ON TI.tipo_tienda = TTI.tipo_tienda " & _
                        "INNER JOIN Productos_Mayoreo_Semaforo as SEM ON SEM.id_producto= HDET.id_producto  " & _
                        "AND SEM.tipo_tienda =TI.tipo_tienda WHERE HDET.precio_pieza < SEM.precio AND HDET.precio_pieza>0)as HDET  " & _
                        "ON H.folio_historial = HDET.folio_historial " & _
                        "INNER JOIN PM_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                        "INNER JOIN Cadenas_Mayoreo as CAD ON TI.id_cadena = CAD.id_cadena " & _
                        "INNER JOIN PM_Tipo_Tiendas as TTI ON TI.tipo_tienda = TTI.tipo_tienda " & _
                        "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                        "INNER JOIN Periodos as PER ON PER.id_periodo = H.id_periodo AND PER.id_semana = H.id_semana " & _
                        "FULL JOIN (SELECT HDET.id_producto,HDET.folio_historial, SEM.precio, HDET.precio_pieza  " & _
                        "FROM May_Productos_Historial_Det as HDET  " & _
                        "INNER JOIN  May_Productos_Historial_Det as H ON HDET.folio_historial = H.folio_historial " & _
                        "INNER JOIN PM_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                        "INNER JOIN PM_Tipo_Tiendas as TTI ON TI.tipo_tienda = TTI.tipo_tienda " & _
                        "INNER JOIN Productos_Mayoreo_Semaforo as SEM ON SEM.id_producto= HDET.id_producto  " & _
                        "AND SEM.tipo_tienda =TI.tipo_tienda WHERE HDET.id_producto=1 " & _
                        "AND HDET.precio_pieza < SEM.precio AND HDET.precio_pieza>0)PROD1 " & _
                        "ON H.folio_historial= PROD1.folio_historial " & _
                        "FULL JOIN (SELECT HDET.id_producto,HDET.folio_historial, SEM.precio, HDET.precio_pieza  " & _
                        "FROM May_Productos_Historial_Det as HDET  " & _
                        "INNER JOIN May_Productos_Historial_Det as H ON HDET.folio_historial = H.folio_historial " & _
                        "INNER JOIN PM_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                        "INNER JOIN PM_Tipo_Tiendas as TTI ON TI.tipo_tienda = TTI.tipo_tienda " & _
                        "INNER JOIN Productos_Mayoreo_Semaforo as SEM ON SEM.id_producto= HDET.id_producto  " & _
                        "AND SEM.tipo_tienda =TI.tipo_tienda WHERE HDET.id_producto=2 " & _
                        "AND HDET.precio_pieza < SEM.precio AND HDET.precio_pieza>0)PROD2 " & _
                        "ON H.folio_historial= PROD2.folio_historial " & _
                        "FULL JOIN (SELECT HDET.id_producto,HDET.folio_historial, SEM.precio, HDET.precio_pieza  " & _
                        "FROM May_Productos_Historial_Det as HDET  " & _
                        "INNER JOIN  May_Productos_Historial_Det as H ON HDET.folio_historial = H.folio_historial " & _
                        "INNER JOIN PM_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                        "INNER JOIN PM_Tipo_Tiendas as TTI ON TI.tipo_tienda = TTI.tipo_tienda " & _
                        "INNER JOIN Productos_Mayoreo_Semaforo as SEM ON SEM.id_producto= HDET.id_producto  " & _
                        "AND SEM.tipo_tienda =TI.tipo_tienda WHERE HDET.id_producto=5 " & _
                        "AND HDET.precio_pieza < SEM.precio AND HDET.precio_pieza>0)PROD3 " & _
                        "ON H.folio_historial= PROD3.folio_historial " & _
                        "FULL JOIN (SELECT HDET.id_producto,HDET.folio_historial, SEM.precio, HDET.precio_pieza  " & _
                        "FROM May_Productos_Historial_Det as HDET  " & _
                        "INNER JOIN  May_Productos_Historial_Det as H ON HDET.folio_historial = H.folio_historial " & _
                        "INNER JOIN PM_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                        "INNER JOIN PM_Tipo_Tiendas as TTI ON TI.tipo_tienda = TTI.tipo_tienda " & _
                        "INNER JOIN Productos_Mayoreo_Semaforo as SEM ON SEM.id_producto= HDET.id_producto  " & _
                        "AND SEM.tipo_tienda =TI.tipo_tienda WHERE HDET.id_producto=7 " & _
                        "AND HDET.precio_pieza < SEM.precio AND HDET.precio_pieza>0)PROD4 " & _
                        "ON H.folio_historial= PROD4.folio_historial " & _
                        "WHERE H.id_periodo = '" & cmbPeriodo.SelectedValue & "'" & _
                        " " + SemanaSQL + " " & _
                        " " + RegionSQL + " " & _
                        " " + PromotorSQL + " " & _
                        " " + CadenaSQL + " " & _
                        " " + TiendaSQL + " " & _
                        "ORDER BY REG.nombre_region,CAD.nombre_cadena,TI.nombre, H.id_periodo, H.id_semana, H.id_dia", _
                        gridReporte)
        Else
            gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionMars.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, SemanaSQL, "nombre_semana", "id_semana", cmbSemana)
            Combo.LlenaDrop(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionMars.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
            Combo.LlenaDrop(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

            BuscaPeriodoActual()
            cmbPeriodo.SelectedValue = PeriodoActual

            CargarReporte()
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCadena.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbTienda_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbTienda.SelectedIndexChanged
        CargarReporte()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim Precio1 As Double, PrecioR1 As Double
            Precio1 = e.Row.Cells(10).Text
            PrecioR1 = e.Row.Cells(11).Text

            If PrecioR1 = 0 Then
                e.Row.Cells(11).Text = "" : End If

            For i = 0 To CInt(gridReporte.Rows.Count) - 0
                If Precio1 > PrecioR1 Then
                    e.Row.Cells(11).ForeColor = Drawing.Color.Red : End If
            Next i

            Dim Precio2 As Double, PrecioR2 As Double
            Precio2 = e.Row.Cells(12).Text
            PrecioR2 = e.Row.Cells(13).Text

            If PrecioR2 = 0 Then
                e.Row.Cells(13).Text = ""
            End If

            For i = 0 To CInt(gridReporte.Rows.Count) - 0
                If Precio2 > PrecioR2 Then
                    e.Row.Cells(13).ForeColor = Drawing.Color.Red
                End If
            Next i

            Dim Precio3 As Double, PrecioR3 As Double
            Precio3 = e.Row.Cells(14).Text
            PrecioR3 = e.Row.Cells(15).Text

            If PrecioR3 = 0 Then
                e.Row.Cells(15).Text = ""
            End If

            For i = 0 To CInt(gridReporte.Rows.Count) - 0
                If Precio3 > PrecioR3 Then
                    e.Row.Cells(15).ForeColor = Drawing.Color.Red
                End If
            Next i

            Dim Precio4 As Double, PrecioR4 As Double
            Precio4 = e.Row.Cells(16).Text
            PrecioR4 = e.Row.Cells(17).Text

            If PrecioR4 = 0 Then
                e.Row.Cells(17).Text = ""
            End If

            For i = 0 To CInt(gridReporte.Rows.Count) - 0
                If Precio4 > PrecioR4 Then
                    e.Row.Cells(17).ForeColor = Drawing.Color.Red
                End If
            Next i
        End If
    End Sub

    Protected Sub cmbSemana_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbSemana.SelectedIndexChanged
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Semaforo " + cmbPeriodo.SelectedItem.ToString() + " " + cmbSemana.SelectedItem.ToString() + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Sub BuscaPeriodoActual()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT DISTINCT id_periodo, nombre_periodo, fecha_inicio_periodo, fecha_fin_periodo " & _
                                     "FROM Periodos where fecha_inicio_periodo<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                                     "AND fecha_fin_periodo>= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "'")
        If Tabla.Rows.Count > 0 Then
            PeriodoActual = Tabla.Rows(0)("id_periodo")
        End If

        Tabla.Dispose()
    End Sub

End Class