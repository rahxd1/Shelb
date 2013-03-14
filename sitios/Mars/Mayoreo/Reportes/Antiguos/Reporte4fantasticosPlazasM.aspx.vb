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

Partial Public Class Reporte4fantasticosPlazasM
    Inherits System.Web.UI.Page

    Dim PeriodoSelA, PeriodoSelB, PromotorSel, RegionSel, CadenaSel As String
    Dim PeriodoSQLA, PeriodoSQLB, PeriodoSQL2A, PeriodoSQL2B, QuincenaSQL, SemanaSQL, PromotorSQL, RegionSQL, CadenaSQL, TiendaSQL As String
    Dim PeriodoActual As String

    Sub SQLCombo()
        PeriodoSelA = Acciones.Slc.cmb("orden", cmbPeriodoA.SelectedValue)
        PeriodoSelB = Acciones.Slc.cmb("orden", cmbPeriodoB.SelectedValue)
        RegionSel = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
        PromotorSel = Acciones.Slc.cmb("RE.id_usuario", cmbPromotor.SelectedValue)
        CadenaSel = Acciones.Slc.cmb("TI.id_cadena", cmbCadena.SelectedValue)

        PeriodoSQLA = "SELECT DISTINCT H.id_periodo, PER.nombre_periodo, PER.orden " & _
                     "FROM May_Historial as H " & _
                     "INNER JOIN Periodos as PER ON H.id_periodo = PER.id_periodo " & _
                     "ORDER BY PER.orden DESC"

        PeriodoSQLB = "SELECT DISTINCT H.id_periodo, PER.nombre_periodo, PER.orden " & _
                     "FROM PM_Productos_Historial as H " & _
                     "INNER JOIN Periodos as PER ON H.id_periodo = PER.id_periodo " & _
                     "ORDER BY PER.orden DESC"

        PeriodoSQL2A = "SELECT DISTINCT H.id_periodo, PER.nombre_periodo, PER.orden " & _
                     "FROM May_Historial as H " & _
                     "INNER JOIN Periodos as PER ON H.id_periodo = PER.id_periodo " & _
                     "ORDER BY PER.orden DESC"

        PeriodoSQL2B = "SELECT DISTINCT H.id_periodo, PER.nombre_periodo, PER.orden " & _
                     "FROM PM_Productos_Historial as H " & _
                     "INNER JOIN Periodos as PER ON H.id_periodo = PER.id_periodo " & _
                     "ORDER BY PER.orden DESC"

        SemanaSQL = "SELECT DISTINCT id_semana, nombre_semana " & _
                    "FROM Periodos ORDER BY id_semana"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Regiones as REG  " & _
                    "WHERE REG.id_region <>0 " & _
                    " ORDER BY REG.nombre_region"

        PromotorSQL = "SELECT DISTINCT RE.id_usuario " & _
                    "FROM PM_Rutas_Eventos AS RE " & _
                    "INNER JOIN PM_Tiendas AS TI ON TI.id_tienda = RE.id_tienda " & _
                    "INNER JOIN Periodos as PER ON RE.id_periodo = PER.id_periodo " & _
                    "WHERE TI.Estatus=1 " & _
                    " " + PeriodoSelB + RegionSel + " " & _
                    "UNION ALL " & _
                    "SELECT DISTINCT RE.id_usuario " & _
                    "FROM May_Rutas_Eventos AS RE " & _
                    "INNER JOIN May_Tiendas AS TI ON TI.id_tienda = RE.id_tienda " & _
                    "INNER JOIN Periodos as PER ON RE.id_periodo = PER.id_periodo " & _
                    "WHERE TI.Estatus=1 " & _
                    " " + PeriodoSelA + RegionSel + " " & _
                    " ORDER BY RE.id_usuario"

        CadenaSQL = "SELECT DISTINCT CAD.id_cadena, CAD.nombre_cadena " & _
                    "FROM Cadenas_Mayoreo as CAD " & _
                    "INNER JOIN PM_Tiendas AS TI ON TI.id_cadena = CAD.id_cadena " & _
                    "INNER JOIN PM_Rutas_Eventos AS RE ON TI.id_tienda = RE.id_tienda " & _
                    "INNER JOIN Periodos as PER ON RE.id_periodo = PER.id_periodo " & _
                    "WHERE TI.Estatus=1 " & _
                    " " + PeriodoSelB + " " & _
                    " " + RegionSel + PromotorSel + " " & _
                    "UNION ALL " & _
                    "SELECT DISTINCT CAD.id_cadena, CAD.nombre_cadena " & _
                    "FROM Cadenas_Mayoreo as CAD " & _
                    "INNER JOIN May_Tiendas AS TI ON TI.id_cadena = CAD.id_cadena " & _
                    "INNER JOIN May_Rutas_Eventos AS RE ON TI.id_tienda = RE.id_tienda " & _
                    "INNER JOIN Periodos as PER ON RE.id_periodo = PER.id_periodo " & _
                    "WHERE TI.Estatus=1 " & _
                    " " + PeriodoSelA + " " & _
                    " " + RegionSel + PromotorSel + " " & _
                    " ORDER BY CAD.nombre_cadena"

        TiendaSQL = "SELECT DISTINCT RE.id_tienda, TI.nombre " & _
                   "FROM PM_Rutas_Eventos AS RE " & _
                   "INNER JOIN PM_Tiendas AS TI ON RE.id_tienda = TI.id_tienda " & _
                   "INNER JOIN Periodos as PER ON RE.id_periodo = PER.id_periodo " & _
                   "WHERE TI.Estatus=1 " & _
                   " " + PeriodoSelB + RegionSel + " " & _
                   " " + PromotorSel + CadenaSel + " " & _
                   "UNION ALL " & _
                   "SELECT DISTINCT RE.id_tienda, TI.nombre " & _
                   "FROM May_Rutas_Eventos AS RE " & _
                   "INNER JOIN May_Tiendas AS TI ON RE.id_tienda = TI.id_tienda " & _
                   "INNER JOIN Periodos as PER ON RE.id_periodo = PER.id_periodo " & _
                   "WHERE TI.Estatus=1 " & _
                   " " + PeriodoSelA + RegionSel + " " & _
                   " " + PromotorSel + CadenaSel + " " & _
                   " ORDER BY TI.nombre"
    End Sub

    Private Function CargarDetalle(ByVal tipo As Integer, ByVal grilla As GridView) As Boolean
        CargaGrilla(ConexionMars.localSqlServer, "SELECT [1],[11],[2],[12],[5],[15],[7],[17] " & _
                     "FROM(SELECT id_producto, precio " & _
                     " FROM Productos_Mayoreo_Semaforo " & _
                     "WHERE tipo_tienda=" & tipo & " and id_periodo='' " & _
                     "union all " & _
                     "SELECT 10+id_producto, round((precio*0.01)+precio,2) precio " & _
                     " FROM Productos_Mayoreo_Semaforo " & _
                     "WHERE tipo_tienda=" & tipo & " and id_periodo='') AS SourceTable " & _
                     "PIVOT(AVG(precio)FOR id_producto IN ([1],[11],[2],[12],[5],[15],[7],[17])) AS PivotTable", _
                     grilla)
    End Function

    Sub CargarReporte()
        If cmbPeriodoA.SelectedValue <> "" And cmbPeriodoB.SelectedValue <> "" Then
            CargarDetalle(2, gridDetalle1)
            CargarDetalle(3, gridDetalle2)

            If Not cmbPeriodoA.SelectedValue = "" Then
                PeriodoSQLA = "AND orden = '" & cmbPeriodoA.SelectedValue & "' " : End If

            If cmbPeriodo2A.SelectedValue = "" Then
                PeriodoSQL2A = "" : Else
                PeriodoSQLA = ""
                PeriodoSQL2A = "AND orden >= '" & cmbPeriodoA.SelectedValue & "' AND orden <= '" & cmbPeriodo2A.SelectedValue & "' " : End If

            If Not cmbPeriodoB.SelectedValue = "" Then
                PeriodoSQLB = "AND orden = '" & cmbPeriodoA.SelectedValue & "' " : End If

            If cmbPeriodo2B.SelectedValue = "" Then
                PeriodoSQL2B = "" : Else
                PeriodoSQLB = ""
                PeriodoSQL2B = "AND orden >= '" & cmbPeriodoB.SelectedValue & "' AND orden <= '" & cmbPeriodo2B.SelectedValue & "' " : End If

            QuincenaSQL = Acciones.Slc.cmb("id_quincena", cmbQuincena.SelectedValue)
            SemanaSQL = Acciones.Slc.cmb("id_semana", cmbSemana.SelectedValue)
            RegionSQL = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("id_usuario", cmbPromotor.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("id_cadena", cmbCadena.SelectedValue)
            TiendaSQL = Acciones.Slc.cmb("id_tienda", cmbTienda.SelectedValue)

            CargaGrilla(ConexionMars.localSqlServer, _
                        "SELECT DISTINCT H.folio_historial,H.orden,H.top_rc,H.nombre_region, H.ejecutivo, " & _
                        "H.id_usuario, H.nombre_cadena, H.descripcion_tienda,H.codigo,H.id_tienda, H.nombre, " & _
                        "H.id_periodo, H.id_semana, H.nombre_periodo, H.nombre_semana, H.id_dia, '' as id_quincena, " & _
                        "ISNULL(PROD1.precio,0)Precio1, ISNULL(PROD1.precio_pieza,0)Producto1, " & _
                        "CASE WHEN ISNULL(PROD1.agotado,0)=0 then(CASE WHEN ISNULL(PROD1.no_catalogado,0)=0 then '' else 'NO CATALOGADO'end)else 'AGOTADO'end Estatus1, " & _
                        "ISNULL(PROD2.precio,0)Precio2, ISNULL(PROD2.precio_pieza,0)Producto2, " & _
                        "CASE WHEN ISNULL(PROD2.agotado,0)=0 then(CASE WHEN ISNULL(PROD2.no_catalogado,0)=0 then '' else 'NO CATALOGADO'end)else 'AGOTADO'end Estatus2, " & _
                        "ISNULL(PROD3.precio,0)Precio3, ISNULL(PROD3.precio_pieza,0)Producto3, " & _
                        "CASE WHEN ISNULL(PROD3.agotado,0)=0 then(CASE WHEN ISNULL(PROD3.no_catalogado,0)=0 then '' else 'NO CATALOGADO'end)else 'AGOTADO'end Estatus3, " & _
                        "ISNULL(PROD4.precio,0)Precio4, ISNULL(PROD4.precio_pieza,0)Producto4, " & _
                        "CASE WHEN ISNULL(PROD4.agotado,0)=0 then(CASE WHEN ISNULL(PROD4.no_catalogado,0)=0 then '' else 'NO CATALOGADO'end)else 'AGOTADO'end Estatus4 " & _
                        "FROM View_Historial_PM as H " & _
                        "FULL JOIN (SELECT id_producto,folio_historial, precio,precio_pieza,no_catalogado,agotado " & _
                        "FROM View_Historial_Prod_PM WHERE id_producto=1 " & _
                        " " + PeriodoSQLB + PeriodoSQL2B + SemanaSQL + " " & _
                        " " + RegionSQL + PromotorSQL + CadenaSQL + TiendaSQL + ")PROD1 " & _
                        "ON H.folio_historial= PROD1.folio_historial " & _
                        "FULL JOIN (SELECT id_producto,folio_historial, precio,precio_pieza,no_catalogado,agotado " & _
                        "FROM View_Historial_Prod_PM WHERE id_producto=2 " & _
                        " " + PeriodoSQLB + PeriodoSQL2B + SemanaSQL + " " & _
                        " " + RegionSQL + PromotorSQL + CadenaSQL + TiendaSQL + ")PROD2 " & _
                        "ON H.folio_historial= PROD2.folio_historial " & _
                        "FULL JOIN (SELECT id_producto,folio_historial, precio,precio_pieza,no_catalogado,agotado " & _
                        "FROM View_Historial_Prod_PM WHERE id_producto=5 " & _
                        " " + PeriodoSQLB + PeriodoSQL2B + SemanaSQL + " " & _
                        " " + RegionSQL + PromotorSQL + CadenaSQL + TiendaSQL + ")PROD3 " & _
                        "ON H.folio_historial= PROD3.folio_historial " & _
                        "FULL JOIN (SELECT id_producto,folio_historial, precio,precio_pieza,no_catalogado,agotado " & _
                        "FROM View_Historial_Prod_PM WHERE id_producto=7 " & _
                        " " + PeriodoSQLB + PeriodoSQL2B + SemanaSQL + " " & _
                        " " + RegionSQL + PromotorSQL + CadenaSQL + TiendaSQL + ")PROD4 " & _
                        "ON H.folio_historial= PROD4.folio_historial " & _
                        "WHERE PROD1.precio_pieza<>0 OR PROD2.precio_pieza<>0 OR PROD3.precio_pieza<>0 OR PROD4.precio_pieza<>0 " & _
                        " " + PeriodoSQLB + PeriodoSQL2B + SemanaSQL + " " & _
                        " " + RegionSQL + PromotorSQL + CadenaSQL + TiendaSQL + " " & _
                        " UNION ALL " & _
                        "SELECT DISTINCT H.folio_historial,H.orden,H.top_rc,H.nombre_region, H.ejecutivo,H.id_usuario, " & _
                        "H.nombre_cadena, H.descripcion_tienda,H.codigo,H.id_tienda, H.nombre, " & _
                        "H.id_periodo, '' as id_semana, H.nombre_periodo, '' as nombre_semana, '' as id_dia, H.id_quincena, " & _
                        "ISNULL(PROD1.precio,0)Precio1, ISNULL(PROD1.precio_pieza,0)Producto1, " & _
                        "CASE WHEN ISNULL(PROD1.agotado,0)=0 then(CASE WHEN ISNULL(PROD1.no_catalogado,0)=0 then '' else 'NO CATALOGADO'end)else 'AGOTADO'end Estatus1, " & _
                        "ISNULL(PROD2.precio,0)Precio2, ISNULL(PROD2.precio_pieza,0)Producto2, " & _
                        "CASE WHEN ISNULL(PROD2.agotado,0)=0 then(CASE WHEN ISNULL(PROD2.no_catalogado,0)=0 then '' else 'NO CATALOGADO'end)else 'AGOTADO'end Estatus2, " & _
                        "ISNULL(PROD3.precio,0)Precio3, ISNULL(PROD3.precio_pieza,0)Producto3, " & _
                        "CASE WHEN ISNULL(PROD3.agotado,0)=0 then(CASE WHEN ISNULL(PROD3.no_catalogado,0)=0 then '' else 'NO CATALOGADO'end)else 'AGOTADO'end Estatus3, " & _
                        "ISNULL(PROD4.precio,0)Precio4, ISNULL(PROD4.precio_pieza,0)Producto4, " & _
                        "CASE WHEN ISNULL(PROD4.agotado,0)=0 then(CASE WHEN ISNULL(PROD4.no_catalogado,0)=0 then '' else 'NO CATALOGADO'end)else 'AGOTADO'end Estatus4 " & _
                        "FROM View_Historial_May as H " & _
                        "FULL JOIN (SELECT id_producto,folio_historial, precio,precio_pieza,no_catalogado,agotado " & _
                        "FROM View_Historial_Prod_May WHERE id_producto=1 " & _
                        " " + PeriodoSQLA + PeriodoSQL2A + QuincenaSQL + " " & _
                        " " + RegionSQL + PromotorSQL + CadenaSQL + TiendaSQL + ")PROD1 " & _
                        "ON H.folio_historial= PROD1.folio_historial " & _
                        "FULL JOIN (SELECT id_producto,folio_historial, precio,precio_pieza,no_catalogado,agotado " & _
                        "FROM View_Historial_Prod_May WHERE id_producto=2 " & _
                        " " + PeriodoSQLA + PeriodoSQL2A + QuincenaSQL + " " & _
                        " " + RegionSQL + PromotorSQL + CadenaSQL + TiendaSQL + ")PROD2 " & _
                        "ON H.folio_historial= PROD2.folio_historial " & _
                        "FULL JOIN (SELECT id_producto,folio_historial, precio,precio_pieza,no_catalogado,agotado " & _
                        "FROM View_Historial_Prod_May WHERE id_producto=5 " & _
                        " " + PeriodoSQLA + PeriodoSQL2A + QuincenaSQL + " " & _
                        " " + RegionSQL + PromotorSQL + CadenaSQL + TiendaSQL + ")PROD3 " & _
                        "ON H.folio_historial= PROD3.folio_historial " & _
                        "FULL JOIN (SELECT id_producto,folio_historial, precio,precio_pieza,no_catalogado,agotado " & _
                        "FROM View_Historial_Prod_May WHERE id_producto=7 " & _
                        " " + PeriodoSQLA + PeriodoSQL2A + QuincenaSQL + " " & _
                        " " + RegionSQL + PromotorSQL + CadenaSQL + TiendaSQL + ")PROD4 " & _
                        "ON H.folio_historial= PROD4.folio_historial " & _
                        "WHERE PROD1.precio_pieza<>0 OR PROD2.precio_pieza<>0 OR PROD3.precio_pieza<>0 OR PROD4.precio_pieza<>0 " & _
                        " " + PeriodoSQLA + PeriodoSQL2A + QuincenaSQL + " " & _
                        " " + RegionSQL + PromotorSQL + CadenaSQL + TiendaSQL + " " & _
                        "ORDER BY H.orden,H.id_semana,H.id_dia,H.nombre_region,H.nombre_cadena,H.nombre", _
                        gridReporte)
        Else
            gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionMars.localSqlServer, PeriodoSQLA, "nombre_periodo", "orden", cmbPeriodoA)
            Combo.LlenaDrop(ConexionMars.localSqlServer, PeriodoSQL2A, "nombre_periodo", "orden", cmbPeriodo2A)
            Combo.LlenaDrop(ConexionMars.localSqlServer, PeriodoSQLB, "nombre_periodo", "orden", cmbPeriodoB)
            Combo.LlenaDrop(ConexionMars.localSqlServer, PeriodoSQL2B, "nombre_periodo", "orden", cmbPeriodo2B)
            cmbPeriodo2A.Items.Insert(0, New ListItem("", ""))
            cmbPeriodo2B.Items.Insert(0, New ListItem("", ""))

            SQLCombo()
            Combo.LlenaDrop(ConexionMars.localSqlServer, SemanaSQL, "nombre_semana", "id_semana", cmbSemana)
            Combo.LlenaDrop(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionMars.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
            Combo.LlenaDrop(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

            BuscaPeriodoActual()
            cmbPeriodoA.SelectedValue = PeriodoActual
            cmbPeriodoB.SelectedValue = PeriodoActual

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

    Protected Sub cmbPeriodoA_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodoA.SelectedIndexChanged
        SQLCombo()
        cmbQuincena.SelectedValue = ""

        Combo.LlenaDrop(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodoB_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodoB.SelectedIndexChanged
        SQLCombo()
        cmbSemana.SelectedValue = ""

        Combo.LlenaDrop(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbTienda_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbTienda.SelectedIndexChanged
        CargarReporte()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        Dim Calculo, PrecioMax, PrecioMin As Double

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim Precio1 As Double, PrecioR1 As Double
            Precio1 = e.Row.Cells(12).Text
            PrecioR1 = e.Row.Cells(13).Text

            If PrecioR1 = 0 Then
                e.Row.Cells(13).Text = "" : End If

            Calculo = Precio1 * 0.01
            PrecioMin = Precio1 - Calculo
            PrecioMax = Precio1 + Calculo

            ''//Color condicional
            If PrecioR1 > PrecioMax Then
                e.Row.Cells(13).ForeColor = Drawing.Color.Green : End If
            If PrecioR1 < PrecioMin Then
                e.Row.Cells(13).ForeColor = Drawing.Color.Red : End If

            Dim Precio2 As Double, PrecioR2 As Double
            Precio2 = e.Row.Cells(14).Text
            PrecioR2 = e.Row.Cells(15).Text

            If PrecioR2 = 0 Then
                e.Row.Cells(15).Text = "" : End If

            Calculo = Precio2 * 0.01
            PrecioMin = Precio2 - Calculo
            PrecioMax = Precio2 + Calculo

            ''//Color condicional
            If PrecioR2 > PrecioMax Then
                e.Row.Cells(15).ForeColor = Drawing.Color.Green : End If
            If PrecioR2 < PrecioMin Then
                e.Row.Cells(15).ForeColor = Drawing.Color.Red : End If

            Dim Precio3 As Double, PrecioR3 As Double
            Precio3 = e.Row.Cells(16).Text
            PrecioR3 = e.Row.Cells(17).Text

            If PrecioR3 = 0 Then
                e.Row.Cells(17).Text = "" : End If

            Calculo = Precio3 * 0.01
            PrecioMin = Precio3 - Calculo
            PrecioMax = Precio3 + Calculo

            ''//Color condicional
            If PrecioR3 > PrecioMax Then
                e.Row.Cells(17).ForeColor = Drawing.Color.Green : End If
            If PrecioR3 < PrecioMin Then
                e.Row.Cells(17).ForeColor = Drawing.Color.Red : End If

            Dim Precio4 As Double, PrecioR4 As Double
            Precio4 = e.Row.Cells(18).Text
            PrecioR4 = e.Row.Cells(19).Text

            If PrecioR4 = 0 Then
                e.Row.Cells(19).Text = "" : End If

            Calculo = Precio4 * 0.01
            PrecioMin = Precio4 - Calculo
            PrecioMax = Precio4 + Calculo

            ''//Color condicional
            If PrecioR4 > PrecioMax Then
                e.Row.Cells(19).ForeColor = Drawing.Color.Green : End If
            If PrecioR4 < PrecioMin Then
                e.Row.Cells(19).ForeColor = Drawing.Color.Red : End If
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
        Me.pnlGrid.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.pnlGrid)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Detalle.xls")
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

        tabla.Dispose()
    End Sub

    Private Sub cmbPeriodo2A_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPeriodo2A.SelectedIndexChanged
        cmbQuincena.SelectedValue = ""

        SQLCombo()
        CargarReporte()
    End Sub

    Private Sub cmbPeriodo2B_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPeriodo2B.SelectedIndexChanged
        cmbSemana.SelectedValue = ""

        SQLCombo()
        CargarReporte()
    End Sub

    Private Sub cmbQuincena_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbQuincena.SelectedIndexChanged
        CargarReporte()
    End Sub

    Private Sub gridDetalle1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridDetalle1.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.Header
                e.Row.Cells(0).ColumnSpan = 2
                e.Row.Cells(1).Visible = False
                e.Row.Cells(2).ColumnSpan = 2
                e.Row.Cells(3).Visible = False
                e.Row.Cells(4).ColumnSpan = 2
                e.Row.Cells(5).Visible = False
                e.Row.Cells(6).ColumnSpan = 2
                e.Row.Cells(7).Visible = False
        End Select
    End Sub

    Private Sub gridDetalle2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridDetalle2.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.Header
                e.Row.Cells(0).ColumnSpan = 2
                e.Row.Cells(1).Visible = False
                e.Row.Cells(2).ColumnSpan = 2
                e.Row.Cells(3).Visible = False
                e.Row.Cells(4).ColumnSpan = 2
                e.Row.Cells(5).Visible = False
                e.Row.Cells(6).ColumnSpan = 2
                e.Row.Cells(7).Visible = False
        End Select
    End Sub
End Class