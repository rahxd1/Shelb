Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteComentariosNM
    Inherits System.Web.UI.Page

    Dim RegionSel As String
    Dim PeriodoSQL, PromotorSQL, RegionSQL, CadenaSQL As String

    Sub SQLCombo()
        RegionSQL = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)

        PeriodoSQL = "SELECT id_periodo, nombre_periodo " & _
                         "FROM NewMix_Periodos " & _
                         " ORDER BY id_periodo DESC"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Regiones as REG " & _
                    "WHERE REG.id_region<>0 ORDER BY REG.nombre_region"

        PromotorSQL = "SELECT DISTINCT id_usuario " & _
                    "FROM NewMix_Historial as RE " & _
                    "INNER JOIN NewMix_Tiendas as TI ON RE.id_tienda = TI.id_tienda " & _
                    "WHERE id_usuario<>'' " & _
                    " " + RegionSel + " " & _
                    " ORDER BY id_usuario "

        CadenaSQL = "SELECT DISTINCT CAD.id_cadena, CAD.nombre_cadena " & _
                    "FROM Cadenas_tiendas as CAD " & _
                    " ORDER BY CAD.nombre_cadena"
    End Sub

    Sub CargarReporte()
        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("TI.id_cadena", cmbCadena.SelectedValue)

            CargaGrilla(ConexionHerradura.localSqlServer, _
                        "SELECT DISTINCT REG.nombre_region, H.id_usuario, CAD.nombre_cadena, TI.nombre, TI.no_tienda, " & _
                        "H.comentario_general " & _
                        "From NewMix_Historial as H  " & _
                        "INNER JOIN NewMix_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                        "INNER JOIN Cadenas_tiendas as CAD ON CAD.id_cadena = TI.id_cadena " & _
                        "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                        "WHERE H.id_periodo = " & cmbPeriodo.SelectedValue & " AND H.comentario_general <>'' " & _
                        " " + RegionSQL + " " & _
                        " " + PromotorSQL + " " & _
                        " " + CadenaSQL + " " & _
                        "ORDER BY nombre_region,nombre_cadena,nombre", Me.gridReporte)
        Else
            gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)

        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionHerradura.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionHerradura.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionHerradura.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)

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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Sin pedidos por Tiendas " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

End Class