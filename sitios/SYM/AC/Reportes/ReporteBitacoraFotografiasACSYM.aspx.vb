Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteBitacoraFotografiasACSYM
    Inherits System.Web.UI.Page


    Sub SQLCombo()
        Combos_SYMAC.SQLsCombo(cmbPeriodo.SelectedValue, cmbRegion.SelectedValue, _
                                     cmbEstado.SelectedValue, cmbSupervisor.SelectedValue, _
                                     "", "", "", "View_SYM_AC_Galeria")
    End Sub

    Sub CargarReporte()
        Dim RegionSQL, EstadoSQL, SupervisorSQL As String
        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)
            EstadoSQL = Acciones.Slc.cmb("id_estado", cmbEstado.SelectedValue)
            SupervisorSQL = Acciones.Slc.cmb("id_usuario", cmbSupervisor.SelectedValue)

            CargaGrilla(ConexionSYM.localSqlServer, _
                        "select DISTINCT nombre_region,nombre_estado,id_usuario,COUNT(DISTINCT id_tienda)Tiendas " & _
                         "FROM View_SYM_AC_Galeria " & _
                         "WHERE H.id_periodo = " & cmbPeriodo.SelectedValue & " " & _
                         " " + RegionSQL + " " & _
                         " " + EstadoSQL + " " & _
                         " " + SupervisorSQL + " " & _
                         "GROUP BY nombre_region,nombre_estado,id_usuario " & _
                         "ORDER BY nombre_region ", gridReporte)
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
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.SupervisorSQLcmb, "supervisor", "id_supervisor", cmbSupervisor)
        End If
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.EstadoSQLcmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.SupervisorSQLcmb, "supervisor", "id_supervisor", cmbSupervisor)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.EstadoSQLcmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.SupervisorSQLcmb, "supervisor", "id_supervisor", cmbSupervisor)

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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Comentarios " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEstado.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.SupervisorSQLcmb, "supervisor", "id_supervisor", cmbSupervisor)

        CargarReporte()
    End Sub

    Private Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        SQLCombo()

        CargarReporte()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class