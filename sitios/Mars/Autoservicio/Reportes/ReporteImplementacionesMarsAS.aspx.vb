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

Partial Public Class ReporteImplementacionesMarsAS
    Inherits System.Web.UI.Page

    Sub SQLCombo()
        MarsAS.SQLsCombo(cmbPeriodo.SelectedValue, cmbQuincena.SelectedValue, _
                         cmbRegion.SelectedValue, cmbEjecutivo.SelectedValue, _
                         cmbSupervisor.SelectedValue, cmbPromotor.SelectedValue, _
                         cmbCadena.SelectedValue, "View_Historial_AS")
    End Sub

    Sub CargarReporte()
        Dim PeriodoSQL, QuincenaSQL, RegionSQL, EjecutivoSQL, SupervisorSQL, PromotorSQL, CadenaSQL, TiendaSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            If cmbPeriodo.SelectedValue = "" Then
                PeriodoSQL = "" : Else
                PeriodoSQL = " " : End If

            QuincenaSQL = Acciones.Slc.cmb("H.id_quincena", cmbQuincena.SelectedValue)
            RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
            EjecutivoSQL = Acciones.Slc.cmb("REL.region_mars", cmbEjecutivo.SelectedValue)
            SupervisorSQL = Acciones.Slc.cmb("REL.id_supervisor", cmbSupervisor.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("TI.id_cadena", cmbCadena.SelectedValue)
            TiendaSQL = Acciones.Slc.cmb("TI.id_tienda", cmbTienda.SelectedValue)

            CargaGrilla(ConexionMars.localSqlServer, _
                        "SELECT H.id_quincena,H.id_usuario,REG.nombre_region, CAD.nombre_cadena, Ti.nombre, " & _
                        "REL.region_mars +' - '+US.nombre Ejecutivo, FORM.nombre_formato,GRUP.nombre_grupo, " & _
                        "PRO.nombre_proceso,H.fecha_implementacion," & _
                        "CASE WHEN [1]=1 then 'Si' else 'No' end as [1], " & _
                        "CASE WHEN [2]=1 then 'Si' else 'No' end as [2], " & _
                        "CASE WHEN [3]=1 then 'Si' else 'No' end as [3], " & _
                        "CASE WHEN [4]=1 then 'Si' else 'No' end as [4], " & _
                        "CASE WHEN [5]=1 then 'Si' else 'No' end as [5], " & _
                        "CASE WHEN [6]=1 then 'Si' else 'No' end as [6], " & _
                        "CASE WHEN [7]=1 then 'Si' else 'No' end as [7], " & _
                        "CASE WHEN [8]=1 then 'Si' else 'No' end as [8], " & _
                        "CASE WHEN [9]=1 then 'Si' else 'No' end as [9],Otros " & _
                        "FROM AS_Procesos_Historial_Det as H " & _
                        "INNER JOIN AS_Procesos as PRO ON PRO.id_proceso= H.id_proceso " & _
                        "INNER JOIN Periodos_Nuevo as PER ON H.orden= PER.orden " & _
                        "INNER JOIN AS_Tiendas as TI ON TI.id_tienda= H.id_tienda  " & _
                        "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena= TI.id_cadena  " & _
                        "INNER JOIN Cadena_Grupo as GRUP ON TI.id_grupo = GRUP.id_grupo " & _
                        "INNER JOIN Cadena_Formato as FORM ON FORM.id_formato=TI.id_formato " & _
                        "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region  " & _
                        "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario = H.id_usuario " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario = REL.region_mars " & _
                        "WHERE H.id_quincena<>'' AND H.orden =" & cmbPeriodo.SelectedValue & " " & _
                        " " + QuincenaSQL + RegionSQL + " " & _
                        " " + EjecutivoSQL + SupervisorSQL + PromotorSQL + " " & _
                        " " + CadenaSQL + TiendaSQL + " " & _
                        "ORDER BY H.id_quincena, REG.nombre_region, CAD.nombre_cadena, Ti.nombre ", _
                        gridReporte)
        Else
            gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PeriodoSQLCmb, "nombre_periodo", "orden", cmbPeriodo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.QuincenaSQLCmb, "nombre_quincena", "id_quincena", cmbQuincena)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCadena.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbTienda_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbTienda.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbQuincena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbQuincena.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte comentarios anaquel y exhibiciones " + cmbPeriodo.SelectedItem.ToString() + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbEjecutivo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEjecutivo.SelectedIndexChanged
        cmbSupervisor.SelectedValue = ""

        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Private Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        cmbEjecutivo.SelectedValue = ""

        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

End Class