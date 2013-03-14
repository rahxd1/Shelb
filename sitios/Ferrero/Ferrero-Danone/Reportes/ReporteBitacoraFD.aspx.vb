Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteBitacoraFD
    Inherits System.Web.UI.Page

    Dim Suma(2) As Integer

    Sub SQLCombo()
        Ferrero.SQLsComboFD(cmbPeriodo.SelectedValue, cmbRegion.SelectedValue, _
                            cmbSupervisor.SelectedValue, "")
    End Sub

    Sub CargarReporte()
        If cmbPeriodo.SelectedValue <> "" Then
            Dim PromotorSQL, RegionSQL As String

            RegionSQL = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("RE.id_usuario", cmbSupervisor.SelectedValue)

            CargaGrilla(ConexionFerrero.localSqlServer, _
                        "SELECT REG.nombre_region,RE.id_usuario, US.nombre, COUNT(RE.id_usuario) AS Tiendas, SUM(RE.estatus) AS Capturas " & _
                        "FROM Ferrero_Rutas_Eventos as RE " & _
                        "INNER JOIN Cadenas_Tiendas AS CAD ON CAD.id_cadena = RE.id_cadena " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario = RE.id_usuario " & _
                        "INNER JOIN Regiones as REG ON US.id_region= REG.id_region " & _
                        "WHERE RE.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + " " & _
                        " " + PromotorSQL + " " & _
                        "GROUP BY REG.nombre_region,RE.id_usuario, US.nombre " & _
                        "ORDER BY REG.nombre_region,RE.id_usuario, US.nombre", Me.gridReporte)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.PeriodoSQLCmb, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.PromotorSQLCmb, "id_usuario", "id_usuario", cmbSupervisor)

            CargarReporte()
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.PromotorSQLCmb, "id_usuario", "id_usuario", cmbSupervisor)

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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte bitacora captura " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Suma(0) = Suma(0) + e.Row.Cells(3).Text ''Suma Tiendas
            Suma(1) = Suma(1) + e.Row.Cells(4).Text ''Suma Capturas

            e.Row.Cells(5).Text = FormatPercent(e.Row.Cells(4).Text / e.Row.Cells(3).Text, 0, 0, 0, 0)

            If e.Row.Cells(3).Text > e.Row.Cells(4).Text Then
                e.Row.Cells(5).BackColor = Drawing.Color.Red : Else
                e.Row.Cells(5).BackColor = Drawing.Color.GreenYellow : End If
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(3).Text = Suma(0)
            e.Row.Cells(4).Text = Suma(1)
            e.Row.Cells(5).Text = FormatPercent(Suma(1) / Suma(0), 0, 0, 0, 0)
        End If

    End Sub

End Class