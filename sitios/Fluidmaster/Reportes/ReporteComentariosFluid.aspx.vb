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

Partial Public Class ReporteComentariosFluid
    Inherits System.Web.UI.Page

    Sub SQLCombo()
        Fluid.SQLsCombo(cmbPeriodo.SelectedValue, cmbRegion.SelectedValue, cmbEstado.SelectedValue, _
                     cmbPromotor.SelectedValue, cmbDistribuidor.SelectedValue)
    End Sub

    Sub CargarReporte()
        Dim RegionSQL, EstadoSQL, PromotorSQL, CadenaSQL, TiendaSQL, TiendaSQL2, TipoComentarioSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)
            EstadoSQL = Acciones.Slc.cmb("ES.id_estado", cmbEstado.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("CAD.id_cadena", cmbDistribuidor.SelectedValue)
            TipoComentarioSQL = Acciones.Slc.cmb("H.tipo_comentarios", cmbTipoComentario.SelectedValue)
            TiendaSQL = Acciones.Slc.cmb("H.id_tienda", cmbTienda.SelectedValue)
            TiendaSQL2 = Acciones.Slc.cmb("H.tienda", cmbTienda.Text.ToString())

            CargaGrilla(ConexionFluidmaster.localSqlServer, _
                        "SELECT H.id_usuario,TI.ciudad,TI.nombre_region,TI.nombre_estado, 'Distribución' as tipo_tienda," & _
                        "TI.nombre_cadena,TI.nombre, TCOM.descripcion_comentario, H.comentarios " & _
                        "FROM Distribuidor_Historial as H " & _
                        "RIGHT JOIN Tipo_Comentarios TCOM ON TCOM.tipo_comentario=H.tipo_comentarios " & _
                        "INNER JOIN View_Tiendas as TI ON TI.id_tienda= H.id_tienda  " & _
                        "INNER JOIN Regiones as REG ON REG.id_region=TI.id_region " & _
                        "INNER JOIN Estados as ES ON ES.id_estado=TI.id_estado " & _
                        "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena= TI.id_cadena " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + EstadoSQL + " " & _
                        " " + PromotorSQL + CadenaSQL + TiendaSQL + " " & _
                        " " + TipoComentarioSQL + " " & _
                        "UNION ALL " & _
                        "SELECT H.id_usuario,H.ciudad,REG.nombre_region,ES.nombre_estado,'Subdistribución' as tipo_tienda, " & _
                        "CAD.nombre_cadena,H.tienda,TCOM.descripcion_comentario, H.comentarios " & _
                        "FROM Subdistribuidor_Historial as H " & _
                        "RIGHT JOIN Tipo_Comentarios TCOM ON TCOM.tipo_comentario=H.tipo_comentarios " & _
                        "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena= H.id_cadena " & _
                        "INNER JOIN Regiones as REG ON REG.id_region=H.id_region " & _
                        "INNER JOIN Estados as ES ON ES.id_estado=H.id_estado " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + EstadoSQL + " " & _
                        " " + PromotorSQL + CadenaSQL + TiendaSQL2 + " " & _
                        " " + TipoComentarioSQL + " " & _
                        "ORDER BY TI.nombre_region, TI.nombre_cadena,TI.nombre, H.comentarios ", _
                        Me.gridReporte)
        Else
            gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.PeriodoSQLCmb, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbDistribuidor)
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.TipoComentarioSQLCmb, "descripcion_comentario", "tipo_comentario", cmbTipoComentario)
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbDistribuidor)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbDistribuidor)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbDistribuidor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbDistribuidor)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte comentarios " + cmbPeriodo.SelectedItem.ToString() + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbTipoComentario_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoComentario.SelectedIndexChanged
        CargarReporte()
    End Sub

    Private Sub cmbEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEstado.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbDistribuidor)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

End Class