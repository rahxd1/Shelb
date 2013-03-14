Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteCaducidadesJovy
    Inherits System.Web.UI.Page

    Dim FechaCaducidad, FechaPeriodoInicio, FechaPeriodoFin As Date
    Dim VerificaFecha1, VerificaFecha2, VerificaFecha3, CaducidadSQL As String

    Sub SQLCombo()
        Variables_Jovy.SQLsCombo(cmbPeriodo.SelectedValue, cmbRegion.SelectedValue, _
                     cmbPromotor.SelectedValue, cmbCadena.SelectedValue)
    End Sub

    Sub CargarReporte()
        Dim PromotorSQL, RegionSQL, CadenaSQL, TiendaSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("TI.id_cadena", cmbCadena.SelectedValue)
            TiendaSQL = Acciones.Slc.cmb("TI.id_tienda", cmbTienda.SelectedValue)

            If cmbCaducidad.Text = "" Then
                CaducidadSQL = ""
            Else
                Dim Hoy As Date
                Dim Caducidad1, Caducidad2 As Date
                Hoy = Date.Today

                Caducidad1 = DateAdd(DateInterval.Day, 30, FechaPeriodoInicio)
                Caducidad2 = DateAdd(DateInterval.Day, 90, FechaPeriodoInicio)

                If cmbCaducidad.SelectedValue = 1 Then
                    CaducidadSQL = "AND HDET.caducidad <= '" & ISODates.Dates.SQLServerDate(CDate(Caducidad1)) & "' " : End If

                If cmbCaducidad.SelectedValue = 2 Then
                    CaducidadSQL = "AND HDET.caducidad >= '" & ISODates.Dates.SQLServerDate(CDate(Caducidad1)) & "' AND HDET.caducidad <= '" & ISODates.Dates.SQLServerDate(CDate(Caducidad2)) & "' " : End If

                If cmbCaducidad.SelectedValue = 3 Then
                    CaducidadSQL = "AND HDET.caducidad > '" & ISODates.Dates.SQLServerDate(CDate(Caducidad2)) & "' " : End If
            End If

            CargaGrilla(ConexionJovy.localSqlServer, _
                        "select TI.nombre_region,H.id_usuario,TI.nombre_cadena,TI.nombre, PROD.id_marca, " & _
                        "PROD.nombre_marca, PROD.nombre_categoria, PROD.piezas, PROD.nombre_producto, " & _
                        "HDET.caducidad, ISNULL(HDET.cantidad_caducada, '0') as cantidad_caducada " & _
                        "FROM Jovy_Historial as H " & _
                        "INNER JOIN Jovy_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                        "INNER JOIN View_Jovy_Productos AS PROD ON PROD.id_producto = HDET.id_producto " & _
                        "INNER JOIN View_Jovy_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario = H.id_usuario " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        "AND HDET.caducidad <>'' " & _
                        " " + RegionSQL + PromotorSQL + " " & _
                        " " + CadenaSQL + TiendaSQL + " " & _
                        " " + CaducidadSQL + " " & _
                        "ORDER BY HDET.caducidad, TI.nombre_region,TI.nombre_cadena,TI.nombre,PROD.nombre_categoria,PROD.id_marca", _
                        Me.gridReporte)

            Grafica()
        Else
            gridReporte.Visible = False
            pnlGrafica.Visible = False
        End If
    End Sub

    Sub Grafica()
        Dim PromotorSQL, RegionSQL, CadenaSQL, TiendaSQL As String

        pnlGrafica.Visible = True

        VerificaFecha1 = DateAdd(DateInterval.Day, 30, FechaPeriodoFin)
        VerificaFecha2 = DateAdd(DateInterval.Day, 60, FechaPeriodoFin)
        VerificaFecha3 = DateAdd(DateInterval.Day, 90, FechaPeriodoFin)

        RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
        PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)
        CadenaSQL = Acciones.Slc.cmb("TI.id_cadena", cmbCadena.SelectedValue)
        TiendaSQL = Acciones.Slc.cmb("TI.id_tienda", cmbTienda.SelectedValue)

        Dim SQL As String
        SQL = "select 'Caducados' as Mes, count(distinct H.id_tienda) as Tiendas " & _
                "FROM Jovy_Historial as H " & _
                "INNER JOIN View_Jovy_Tiendas as TI ON H.id_tienda=TI.id_tienda " & _
                "INNER JOIN Jovy_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                "WHERE H.id_periodo = " & cmbPeriodo.SelectedValue & " AND caducidad<='" & ISODates.Dates.SQLServerDate(CDate(FechaPeriodoFin)) & "' " & _
                " " + RegionSQL + PromotorSQL + " " & _
                " " + CadenaSQL + TiendaSQL + " " & _
                "union all " & _
                "select '1 mes' as Mes, count(distinct H.id_tienda) as Tiendas " & _
                "FROM Jovy_Historial as H " & _
                "INNER JOIN View_Jovy_Tiendas as TI ON H.id_tienda=TI.id_tienda " & _
                "INNER JOIN Jovy_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                "WHERE H.id_periodo = " & cmbPeriodo.SelectedValue & " AND caducidad>'" & ISODates.Dates.SQLServerDate(CDate(FechaPeriodoFin)) & "' AND caducidad <='" & ISODates.Dates.SQLServerDate(CDate(VerificaFecha1)) & "' " & _
                " " + RegionSQL + PromotorSQL + " " & _
                " " + CadenaSQL + TiendaSQL + " " & _
                "union all " & _
                "select '2 meses' as Mes, count(distinct H.id_tienda) as Tiendas " & _
                "FROM Jovy_Historial as H " & _
                "INNER JOIN View_Jovy_Tiendas as TI ON H.id_tienda=TI.id_tienda " & _
                "INNER JOIN Jovy_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                "WHERE H.id_periodo = " & cmbPeriodo.SelectedValue & " AND caducidad>'" & ISODates.Dates.SQLServerDate(CDate(VerificaFecha1)) & "' AND caducidad <='" & ISODates.Dates.SQLServerDate(CDate(VerificaFecha2)) & "' " & _
                " " + RegionSQL + PromotorSQL + " " & _
                " " + CadenaSQL + TiendaSQL + " " & _
                "union all " & _
                "select '3 meses' as Mes, count(distinct H.id_tienda) as Tiendas " & _
                "FROM Jovy_Historial as H " & _
                "INNER JOIN View_Jovy_Tiendas as TI ON H.id_tienda=TI.id_tienda " & _
                "INNER JOIN Jovy_Productos_Historial_Det as HDET ON HDET.folio_historial = H.folio_historial " & _
                "WHERE H.id_periodo = " & cmbPeriodo.SelectedValue & " AND caducidad>'" & ISODates.Dates.SQLServerDate(CDate(VerificaFecha2)) & "' AND caducidad <='" & ISODates.Dates.SQLServerDate(CDate(VerificaFecha3)) & "' " & _
                " " + RegionSQL + PromotorSQL + " " & _
                " " + CadenaSQL + TiendaSQL + " "

        CargaGrilla(ConexionJovy.localSqlServer, SQL, Me.gridResumen)

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, SQL)

        Dim strXML As New StringBuilder()
        strXML.Append("<chart caption='Cantidad de tiendas con productos por caducar' showborder='0' bgcolor='FFFFFF' bgalpha='100' rotateLabels='1'  placeValuesInside='1' rotateValues='1' >")

        For i = 0 To tabla.Rows.Count - 1
            strXML.Append("<set label='" & tabla.Rows(i)("Mes") & "' value='" & tabla.Rows(i)("Tiendas") & "' />")
        Next i

        strXML.Append("</chart>")

        Dim outPut As String = ""
        outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/Column3D.swf", "", strXML.ToString(), "chart1", "350", "300", False, False)

        pnlGrafica.Controls.Clear()
        pnlGrafica.Controls.Add(New LiteralControl(outPut))
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.PeriodoSQLCmb, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
            Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

            CargarReporte()
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        VerPeriodo()

        Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCadena.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionJovy.localSqlServer, Jovy.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Caducidad " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        VerPeriodo()

        VerificaFecha1 = DateAdd(DateInterval.Day, 30, FechaPeriodoFin)
        VerificaFecha2 = DateAdd(DateInterval.Day, 60, FechaPeriodoFin)
        VerificaFecha3 = DateAdd(DateInterval.Day, 90, FechaPeriodoFin)

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Hoy As Date
            Hoy = Date.Today

            ''//Fecha caducidad
            If Not e.Row.Cells(7).Text = "&nbsp;" Then
                FechaCaducidad = e.Row.Cells(7).Text

                If FechaCaducidad <= VerificaFecha1 Then
                    e.Row.Cells(7).BackColor = Drawing.Color.Red
                End If

                If FechaCaducidad >= VerificaFecha1 And FechaCaducidad <= VerificaFecha3 Then
                    e.Row.Cells(7).BackColor = Drawing.Color.Yellow
                End If

                If FechaCaducidad > VerificaFecha3 Then
                    e.Row.Cells(7).BackColor = Drawing.Color.GreenYellow
                End If

            End If
        End If
    End Sub

    Protected Sub cmbCaducidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCaducidad.SelectedIndexChanged
        CargarReporte()
    End Sub

    Sub VerPeriodo()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                               "SELECT * FROM Jovy_Periodos WHERE id_periodo=" & cmbPeriodo.SelectedValue & " ORDER BY id_periodo")
        If Tabla.Rows.Count > 0 Then
            FechaPeriodoInicio = Tabla.Rows(0)("fecha_inicio")
            FechaPeriodoFin = Tabla.Rows(0)("fecha_fin")
        End If

        tabla.Dispose()
    End Sub

End Class