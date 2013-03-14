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

Partial Public Class ReporteAnaquelTiendaNR
    Inherits System.Web.UI.Page

    Sub SQLCombo()
        NR.SQLsCombo(cmbPeriodo.SelectedValue, cmbRegion.SelectedValue, cmbEstado.SelectedValue, _
                    cmbSupervisor.SelectedValue, cmbPromotor.SelectedValue, cmbFormato.SelectedValue, _
                    cmbCadena.SelectedValue)
    End Sub

    Sub CargarReporte()
        Dim RegionSQL, EstadoSQL, SupervisorSQL, PromotorSQL, CadenaSQL, FormatoSQL, TiendaSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("H.id_region", cmbRegion.SelectedValue)
            EstadoSQL = Acciones.Slc.cmb("H.id_estado", cmbEstado.SelectedValue)
            SupervisorSQL = Acciones.Slc.cmb("H.id_supervisor", cmbSupervisor.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("H.id_cadena", cmbCadena.SelectedValue)
            FormatoSQL = Acciones.Slc.cmb("H.id_formato", cmbFormato.SelectedValue)
            TiendaSQL = Acciones.Slc.cmb("H.id_tienda", cmbTienda.SelectedValue)

            If Tipo_usuario = 7 Then
                Berol.CuentaClave = "INNER JOIN (select * FROM Usuarios_cadenas WHERE id_usuario='" & HttpContext.Current.User.Identity.Name & "' )as CC " & _
                                "ON CC.id_cadena=H.id_cadena " : Else
                Berol.CuentaClave = "" : End If

            CargaGrilla(ConexionBerol.localSqlServer, _
                        "SELECT DISTINCT H.nombre_region, H.ciudad,H.id_usuario,H.supervisor, " & _
                        "H.nombre_cadena,H.nombre_formato,H.codigo,H.nombre,  " & _
                        "ISNULL([1],0)[1],ISNULL([101],0)[101],ISNULL([2],0)[2],ISNULL([102],0)[102],ISNULL([3],0)[3],ISNULL([103],0)[103], " & _
                        "ISNULL([4],0)[4],ISNULL([104],0)[104],ISNULL([5],0)[5],ISNULL([105],0)[105],ISNULL([6],0)[6],ISNULL([106],0)[106], " & _
                        "ISNULL([7],0)[7],ISNULL([107],0)[107],ISNULL([8],0)[8],ISNULL([108],0)[108],ISNULL([9],0)[9],ISNULL([109],0)[109], " & _
                        "ISNULL([10],0)[10],ISNULL([110],0)[110],ISNULL([11],0)[11],ISNULL([111],0)[111],ISNULL([12],0)[12],ISNULL([112],0)[112], " & _
                        "ISNULL([13],0)[13],ISNULL([113],0)[113],ISNULL([14],0)[14],ISNULL([114],0)[114],ISNULL([15],0)[15],ISNULL([115],0)[115], " & _
                        "ISNULL([16],0)[16],ISNULL([116],0)[116],ISNULL([17],0)[17],ISNULL([117],0)[117],ISNULL([18],0)[18],ISNULL([118],0)[118] " & _
                        "FROM View_Historial_NR as H   " & _
                        "INNER JOIN (SELECT folio_historial,tipo_producto,cantidad  " & _
                        "FROM NR_Historial_Frentes_Det as H  " & _
                        "WHERE H.propia=1 " & _
                        "UNION ALL " & _
                        "SELECT folio_historial,100+tipo_producto,cantidad  " & _
                        "FROM NR_Historial_Frentes_Det as H  " & _
                        "WHERE H.propia=2) PVT  " & _
                        "PIVOT(SUM(cantidad) FOR tipo_producto   " & _
                        "IN ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18], " & _
                        "[101],[102],[103],[104],[105],[106],[107],[108],[109],[110],[111],[112],[113],[114],[115],[116],[117],[118])) AS HDET  " & _
                        "ON HDET.folio_historial=H.folio_historial " & _
                        " " + Berol.CuentaClave + " " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & "   " & _
                        " " + RegionSQL + EstadoSQL + " " & _
                        " " + SupervisorSQL + PromotorSQL + " " & _
                        " " + CadenaSQL + FormatoSQL + TiendaSQL + " " & _
                        " " + TiendaSQL + " " & _
                        "ORDER BY H.nombre_region,H.ciudad,H.nombre_cadena,H.nombre_formato,H.nombre", Me.gridReporte)
        Else
            gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.PeriodoSQLCmb, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.FormatoSQLCmb, "nombre_formato", "id_formato", cmbFormato)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.FormatoSQLCmb, "nombre_formato", "id_formato", cmbFormato)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.FormatoSQLCmb, "nombre_formato", "id_formato", cmbFormato)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCadena.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.FormatoSQLCmb, "nombre_formato", "id_formato", cmbFormato)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.FormatoSQLCmb, "nombre_formato", "id_formato", cmbFormato)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte anaquel por tienda por categoria " + cmbPeriodo.SelectedItem.ToString() + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.FormatoSQLCmb, "nombre_formato", "id_formato", cmbFormato)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Private Sub cmbEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEstado.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.FormatoSQLCmb, "nombre_formato", "id_formato", cmbFormato)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Private Sub cmbFormato_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFormato.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub
End Class