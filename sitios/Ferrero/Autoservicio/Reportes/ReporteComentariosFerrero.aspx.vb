Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteComentariosFerrero
    Inherits System.Web.UI.Page

    Sub SQLCombo()
        Ferrero.SQLsComboAS(cmbPeriodo.SelectedValue, cmbRegion.SelectedValue, _
                            cmbPromotor.SelectedValue, cmbCadena.SelectedValue)
    End Sub

    Sub CargarReporte()
        If cmbPeriodo.SelectedValue <> "" Then
            Dim PromotorSQL, RegionSQL, CadenaSQL As String
            RegionSQL = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("H.id_cadena", cmbCadena.SelectedValue)

            CargaGrilla(ConexionFerrero.localSqlServer, _
                        "select REG.nombre_region,CAD.nombre_cadena,H.nombre_tienda, H.id_usuario,H.comentarios " & _
                        "FROM AS_Historial as H " & _
                        "INNER JOIN Cadenas_Tiendas AS CAD ON CAD.id_cadena = H.id_cadena " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario = H.id_usuario " & _
                        "INNER JOIN Regiones as REG ON US.id_region= REG.id_region AND REG.id_proyecto=27 " & _
                        "WHERE H.comentarios <>'' " & _
                        "AND H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + " " & _
                        " " + PromotorSQL + " " & _
                        " " + CadenaSQL + " " & _
                        "ORDER BY REG.nombre_region,CAD.nombre_cadena,H.nombre_tienda ", Me.gridReporte)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.PeriodoSQLCmb, "nombre_periodo", "id_periodo", cmbPeriodo)
            LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
            LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

            CargarReporte()
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()

        LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()
        CargarReporte()
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCadena.SelectedIndexChanged
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Comentarios " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

End Class