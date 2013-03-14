Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteCaducidadFD
    Inherits System.Web.UI.Page

    Dim FechaCaducidad As Date
    Dim VerificaFecha1, VerificaFecha2, VerificaFecha3 As Date

    Sub SQLCombo()
        Ferrero.SQLsComboFD(cmbPeriodo.SelectedValue, cmbRegion.SelectedValue, _
                            cmbSupervisor.SelectedValue, cmbColonia.SelectedValue)
    End Sub

    Sub CargarReporte()
        If cmbPeriodo.SelectedValue <> "" Then
            Dim PromotorSQL, RegionSQL, ColoniaSQL, CaducidadSQL As String
            CaducidadSQL = ""

            RegionSQL = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbSupervisor.SelectedValue)
            ColoniaSQL = Acciones.Slc.cmb("H.colonia", cmbColonia.SelectedValue)

            If Not cmbCaducidad.SelectedValue = "" Then
                Dim Hoy As Date
                Dim Caducidad1, Caducidad2 As Date
                Hoy = Date.Today

                Caducidad1 = DateAdd(DateInterval.Day, 45, Hoy)
                Caducidad2 = DateAdd(DateInterval.Day, 89, Hoy)

                If cmbCaducidad.SelectedValue = 1 Then
                    CaducidadSQL = "AND HDET.caducidad <= '" & ISODates.Dates.SQLServerDate(CDate(Caducidad1)) & "' " : End If

                If cmbCaducidad.SelectedValue = 2 Then
                    CaducidadSQL = "AND HDET.caducidad >= '" & ISODates.Dates.SQLServerDate(CDate(Caducidad1)) & "' AND HDET.caducidad <= '" & ISODates.Dates.SQLServerDate(CDate(Caducidad2)) & "' " : End If

                If cmbCaducidad.SelectedValue = 3 Then
                    CaducidadSQL = "AND HDET.caducidad > '" & ISODates.Dates.SQLServerDate(CDate(Caducidad2)) & "' " : End If
            Else
                CaducidadSQL = "" : End If


            CargaGrilla(ConexionFerrero.localSqlServer, _
                        "select REG.nombre_region,COL.nombre_colonia, H.nombre_tienda, H.direccion, H.id_usuario, " & _
                        "PROD.nombre_producto, HDET.caducidad " & _
                        "FROM Danone_Historial as H " & _
                        "INNER JOIN Danone_Productos_Historial_Det as HDET ON H.folio_historial = HDET.folio_historial " & _
                        "INNER JOIN Colonias_Leon as COL ON COL.id_colonia= H.colonia " & _
                        "INNER JOIN Danone_Productos as PROD ON PROD.id_producto = HDET.id_producto " & _
                        "INNER JOIN Danone_CatRutas as RUT ON RUT.id_usuario = H.id_usuario " & _
                        "INNER JOIN Regiones as REG ON RUT.id_region= REG.id_region AND REG.id_proyecto=30 " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        "AND HDET.caducidad<>'' " & _
                        " " + RegionSQL + " " & _
                        " " + PromotorSQL + " " & _
                        " " + ColoniaSQL + " " & _
                        " " + CaducidadSQL + " " & _
                        "ORDER BY REG.nombre_region,H.nombre_tienda ", Me.gridReporte)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.PeriodoSQLCmb, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.PromotorSQLCmb, "id_usuario", "id_usuario", cmbSupervisor)
            Combo.LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.ColoniaSQLCmb, "nombre_colonia", "id_colonia", cmbColonia)

            CargarReporte()
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.ColoniaSQLCmb, "nombre_colonia", "id_colonia", cmbColonia)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.PromotorSQLCmb, "id_usuario", "id_usuario", cmbSupervisor)
        Combo.LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.ColoniaSQLCmb, "nombre_colonia", "id_colonia", cmbColonia)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Caducidades " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Hoy As Date
            Hoy = Date.Today

            ''//Fecha caducidad
            If Not e.Row.Cells(6).Text = "&nbsp;" Then
                FechaCaducidad = e.Row.Cells(6).Text

                VerificaFecha1 = DateAdd(DateInterval.Day, 45, Hoy)
                If FechaCaducidad <= VerificaFecha1 Then
                    e.Row.Cells(6).BackColor = Drawing.Color.Red
                End If

                VerificaFecha2 = DateAdd(DateInterval.Day, 89, Hoy)
                If FechaCaducidad >= VerificaFecha1 And FechaCaducidad <= VerificaFecha2 Then
                    e.Row.Cells(6).BackColor = Drawing.Color.Yellow
                End If

                VerificaFecha3 = DateAdd(DateInterval.Day, 90, Hoy)
                If FechaCaducidad >= VerificaFecha3 Then
                    e.Row.Cells(6).BackColor = Drawing.Color.GreenYellow
                End If

            End If
        End If
    End Sub

    Private Sub cmbColonia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbColonia.SelectedIndexChanged
        CargarReporte()
    End Sub

    Private Sub cmbCaducidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCaducidad.SelectedIndexChanged
        CargarReporte()
    End Sub

End Class