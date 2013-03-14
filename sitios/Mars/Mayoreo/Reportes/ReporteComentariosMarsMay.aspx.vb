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

Partial Public Class ReporteComentariosMarsMay
    Inherits System.Web.UI.Page

    Dim PeriodoActual As String

    Sub SQLCombo()
        MarsMay.SQLsCombo(cmbPeriodo.SelectedValue, cmbQuincena.SelectedValue, _
                       cmbSemana.SelectedValue, cmbRegion.SelectedValue, "", cmbPromotor.SelectedValue, _
                       cmbCadena.SelectedValue)
    End Sub

    Sub CargarReporte()
        Dim PeriodoSQL, QuincenaSQL, SemanaSQL, PromotorSQL, RegionSQL, CadenaSQL, TiendaSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            PeriodoSQL = Acciones.Slc.cmb("orden", cmbPeriodo.SelectedValue)
            QuincenaSQL = Acciones.Slc.cmb("id_quincena", cmbQuincena.SelectedValue)
            SemanaSQL = Acciones.Slc.cmb("id_semana", cmbQuincena.SelectedValue)
            RegionSQL = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("id_usuario", cmbPromotor.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("id_cadena", cmbCadena.SelectedValue)
            TiendaSQL = Acciones.Slc.cmb("id_tienda", cmbTienda.SelectedValue)

            CargaGrilla(ConexionMars.localSqlServer, _
                        "SELECT DISTINCT nombre_region,id_usuario,nombre_cadena, nombre," & _
                        "nombre_quincena,nombre_semana,id_dia, " & _
                        "comentarios,comentarios_competencia " & _
                        "FROM View_Historial_May_Ant as H " & _
                        "WHERE (comentarios<>'' OR comentarios_competencia<>'') " & _
                        " " + PeriodoSQL + QuincenaSQL + SemanaSQL + " " & _
                        " " + RegionSQL + PromotorSQL + " " & _
                        " " + CadenaSQL + TiendaSQL + " " & _
                        "UNION ALL " & _
                        "SELECT DISTINCT nombre_region,id_usuario,nombre_cadena, nombre," & _
                        "nombre_quincena,nombre_semana,id_dia, " & _
                        "comentarios,comentarios_competencia " & _
                        "FROM View_Historial_PM_Ant as H " & _
                        "WHERE (comentarios<>'' OR comentarios_competencia<>'') " & _
                        " " + PeriodoSQL + QuincenaSQL + SemanaSQL + " " & _
                        " " + RegionSQL + PromotorSQL + " " & _
                        " " + CadenaSQL + TiendaSQL + " " & _
                        "UNION ALL " & _
                        "SELECT DISTINCT nombre_region,id_usuario,nombre_cadena, nombre," & _
                        "nombre_quincena,nombre_semana,id_dia, " & _
                        "comentarios,comentarios_competencia " & _
                        "FROM View_Historial_Mayoreo as H " & _
                        "WHERE (comentarios<>'' OR comentarios_competencia<>'') " & _
                        " " + PeriodoSQL + QuincenaSQL + SemanaSQL + " " & _
                        " " + RegionSQL + PromotorSQL + " " & _
                        " " + CadenaSQL + TiendaSQL + " " & _
                        "ORDER BY nombre_quincena,nombre_semana,id_dia, nombre_region, nombre_cadena, nombre ", _
                        gridReporte)
        Else
            gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.PeriodoSQLCmb, "nombre_periodo", "orden", cmbPeriodo)
            'Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.QuincenaSQLCmb, "nombre_quincena", "id_quincena", cmbQuincena)
            'Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.SemanaSQLCmb, "nombre_semana", "id_semana", cmbSemana)
            'Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            'Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
            'Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
            'Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)
        End If
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.QuincenaSQLCmb, "nombre_quincena", "id_quincena", cmbQuincena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.SemanaSQLCmb, "nombre_semana", "id_semana", cmbSemana)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Private Sub cmbQuincena_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbQuincena.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.SemanaSQLCmb, "nombre_semana", "id_semana", cmbSemana)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbSemana_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbSemana.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCadena.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Comentarios " + cmbPeriodo.SelectedItem.ToString() + " " + cmbSemana.SelectedItem.ToString() + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class