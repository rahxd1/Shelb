Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteBitacoraAC
    Inherits System.Web.UI.Page

    Dim Dato(10), Suma(10) As Integer
    Dim SeleccionIDUsuario As String

    Sub SQLCombo()
        Combos_SYMAC.SQLsCombo(cmbPeriodo.SelectedValue, cmbRegion.SelectedValue, _
                               cmbEstado.SelectedValue, "", "", cmbPromotor.SelectedValue, _
                              "", "View_SYM_AC_RE")
    End Sub

    Sub CargarReporte()
        If cmbPeriodo.SelectedValue <> "" Then
            Dim PromotorSQL, RegionSQL, EstadoSQL As String
            RegionSQL = Acciones.Slc.cmb("RE.id_region", cmbRegion.SelectedValue)
            EstadoSQL = Acciones.Slc.cmb("RE.id_estado", cmbEstado.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("RE.id_usuario", cmbPromotor.SelectedValue)

            CargaGrilla(ConexionSYM.localSqlServer, _
                        "SELECT RE.nombre_region,RE.id_usuario, COUNT(RE.id_usuario) AS Tiendas, " & _
                        "ISNULL(CapturasAnaq.Capturas,0)CapturasA, ISNULL(IncompletasAnaq.Incompletas,0)IncompletasA,  " & _
                        "ISNULL(CapturasCat.Capturas,0)CapturasC,ISNULL(IncompletasCat.Incompletas,0)IncompletasC, " & _
                        "(100*((ISNULL(CapturasAnaq.Capturas,0)+ISNULL(CapturasCat.Capturas,0))/2))/COUNT(RE.id_usuario) as Total " & _
                        "FROM View_SYM_AC_RE as RE  " & _
                        "FULL JOIN (SELECT id_usuario, COUNT(RE.estatus_anaquel) AS Capturas  " & _
                        "FROM AC_Rutas_Eventos as RE WHERE estatus_anaquel=1 AND id_periodo=" & cmbPeriodo.SelectedValue & "  GROUP BY id_usuario)CapturasAnaq  " & _
                        "ON CapturasAnaq.id_usuario = RE.id_usuario  " & _
                        "FULL JOIN (SELECT id_usuario, COUNT(RE.estatus_anaquel) AS Incompletas  " & _
                        "FROM AC_Rutas_Eventos as RE WHERE estatus_anaquel=2 AND id_periodo=" & cmbPeriodo.SelectedValue & "  GROUP BY id_usuario)IncompletasAnaq  " & _
                        "ON IncompletasAnaq.id_usuario = RE.id_usuario  " & _
                        "FULL JOIN (SELECT id_usuario, COUNT(RE.estatus_catalogacion) AS Capturas  " & _
                        "FROM AC_Rutas_Eventos as RE WHERE estatus_catalogacion=1 AND id_periodo=" & cmbPeriodo.SelectedValue & "  GROUP BY id_usuario)CapturasCat  " & _
                        "ON CapturasCat.id_usuario = RE.id_usuario  " & _
                        "FULL JOIN (SELECT id_usuario, COUNT(RE.estatus_catalogacion) AS Incompletas  " & _
                        "FROM AC_Rutas_Eventos as RE WHERE estatus_catalogacion=2 AND id_periodo=" & cmbPeriodo.SelectedValue & "  GROUP BY id_usuario)IncompletasCat  " & _
                        "ON IncompletasCat.id_usuario = RE.id_usuario  " & _
                        "WHERE RE.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + " " & _
                        " " + EstadoSQL + " " & _
                        " " + PromotorSQL + " " & _
                        "GROUP BY RE.id_usuario,CapturasAnaq.Capturas,IncompletasAnaq.Incompletas,CapturasCat.Capturas, " & _
                        "IncompletasCat.Incompletas,RE.id_periodo, RE.nombre_region ", gridReporte)
        Else
            gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PeriodoSQLCmb, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.EstadoSQLcmb, "nombre_estado", "id_estado", cmbEstado)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.EstadoSQLcmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.EstadoSQLcmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

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
            For i = 3 To 7
                Dato(i) = e.Row.Cells(i).Text
                Suma(i) = Suma(i) + Dato(i)
            Next i

            For i = 0 To CInt(gridReporte.Rows.Count) - 0
                If e.Row.Cells(8).Text < 100 Then
                    e.Row.Cells(8).BackColor = Drawing.Color.Red : Else
                    e.Row.Cells(8).BackColor = Drawing.Color.GreenYellow : End If
            Next i

            Dim Porcentaje As String
            Porcentaje = e.Row.Cells(8).Text
            e.Row.Cells(8).Text = Porcentaje & "%"
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            Dim SumaTotal As Double
            SumaTotal = (Suma(4) + Suma(6)) / 2

            e.Row.Cells(3).Text = Suma(3)
            e.Row.Cells(4).Text = Suma(4)
            e.Row.Cells(5).Text = Suma(5)
            e.Row.Cells(6).Text = Suma(6)
            e.Row.Cells(7).Text = Suma(7)
            e.Row.Cells(8).Text = FormatPercent(SumaTotal / Suma(3), 0, 0, 0, 0)
        End If

    End Sub

    Private Sub gridReporte_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gridReporte.RowEditing
        SeleccionIDUsuario = gridReporte.Rows(e.NewEditIndex).Cells(2).Text
        CargarDetalle()
        pnlDetalle.Visible = True
    End Sub

    Sub CargarDetalle()
        CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT RE.id_usuario,CAD.nombre_cadena,TI.nombre, " & _
                            "CASE RE.estatus_anaquel when 2 then 'INCOMPLETA' when 1 then 'CAPTURADA' when 0 then 'SIN CAPTURA' end as estatus_anaquel, " & _
                            "CASE RE.estatus_catalogacion when 2 then 'INCOMPLETA' when 1 then 'CAPTURADA' when 0 then 'SIN CAPTURA' end as estatus_catalogacion " & _
                            "FROM AC_Rutas_Eventos as RE " & _
                            "INNER JOIN AC_Tiendas as TI ON TI.id_tienda= RE.id_tienda " & _
                            "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region " & _
                            "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena =  TI.id_cadena " & _
                            "WHERE RE.id_periodo = " & cmbPeriodo.SelectedValue & "" & _
                            "AND RE.id_usuario = '" & SeleccionIDUsuario & "' " & _
                            " ORDER BY RE.id_usuario, TI.nombre", Me.gridDetalle)
    End Sub

    Private Sub gridDetalle_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridDetalle.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 0 To CInt(gridReporte.Rows.Count) - 0
                If e.Row.Cells(3).Text = "SIN CAPTURA" Then
                    e.Row.Cells(3).ForeColor = Drawing.Color.Red : End If
                If e.Row.Cells(3).Text = "INCOMPLETA" Then
                    e.Row.Cells(3).ForeColor = Drawing.Color.Red : End If
                If e.Row.Cells(4).Text = "SIN CAPTURA" Then
                    e.Row.Cells(4).ForeColor = Drawing.Color.Red : End If
                If e.Row.Cells(4).Text = "INCOMPLETA" Then
                    e.Row.Cells(4).ForeColor = Drawing.Color.Red : End If
            Next i
        End If
    End Sub

    Private Sub cmbEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEstado.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

End Class