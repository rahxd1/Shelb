Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteRutasJovy
    Inherits System.Web.UI.Page

    Dim PromotorSel, RegionSel As String
    Dim PromotorSQL, RegionSQL As String

    Sub SQLCombo()
        RegionSel = Acciones.Slc.cmb("US.id_region", cmbRegion.SelectedValue)

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Regiones as REG " & _
                    " ORDER BY REG.nombre_region"

        PromotorSQL = "select DISTINCT RUT.id_usuario " & _
                    "FROM Jovy_CatRutas as RUT " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario = RUT.id_usuario " & _
                    "WHERE RUT.id_usuario<>'' " + RegionSel + " " & _
                    "ORDER BY RUT.id_usuario"
    End Sub

    Sub CargarReporte()
        RegionSQL = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)
        PromotorSQL = Acciones.Slc.cmb("RUT.id_usuario", cmbPromotor.SelectedValue)

        CargaGrilla(ConexionJovy.localSqlServer, _
                    "select * FROM Jovy_CatRutas as RUT " & _
                    "FULL JOIN Jovy_Tiendas as TI ON TI.id_tienda = RUT.id_tienda " & _
                    "FULL JOIN Estados as ES ON ES.id_estado = TI.id_estado " & _
                    "FULL JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = RUT.id_cadena " & _
                    "WHERE RUT.id_usuario<>'' " + RegionSQL + PromotorSQL + " " & _
                    "ORDER BY RUT.id_usuario", Me.gridReporte)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionJovy.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionJovy.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

            CargarReporte()
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionJovy.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Rutas Jovy.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

End Class