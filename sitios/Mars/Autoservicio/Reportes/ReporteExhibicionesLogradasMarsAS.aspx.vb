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

Partial Public Class ReporteExhibicionesLogradasMarsAS
    Inherits System.Web.UI.Page

    Dim Tiendas, SumaTiendas, Pautas, SumaPautas, PVI, SumaPVI As Integer

    Sub SQLCombo()
        MarsAS.SQLsCombo(cmbPeriodo.SelectedValue, cmbQuincena.SelectedValue, _
                         cmbRegion.SelectedValue, cmbEjecutivo.SelectedValue, _
                         cmbSupervisor.SelectedValue, cmbPromotor.SelectedValue, _
                         cmbCadena.SelectedValue, "View_Historial_AS")
    End Sub

    Sub CargarReporte()
        Dim QuincenaSQL, RegionSQL, EjecutivoSQL, SupervisorSQL, PromotorSQL, CadenaSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            QuincenaSQL = Acciones.Slc.cmb("RE.id_quincena", cmbQuincena.SelectedValue)
            RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
            EjecutivoSQL = Acciones.Slc.cmb("REL.region_mars", cmbEjecutivo.SelectedValue)
            SupervisorSQL = Acciones.Slc.cmb("REL.id_supervisor", cmbSupervisor.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("REL.id_usuario", cmbPromotor.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("TI.id_cadena", cmbCadena.SelectedValue)

            CargaGrilla(ConexionMars.localSqlServer, _
                        "SELECT Tiendas.orden, Tiendas.nombre_region,REL.id_tipo,Tiendas.region_mars, Tiendas.region_mars as id_usuario," & _
                        "Tiendas.region_mars + ' - ' + REL.nombre Usuario, Tiendas.Tiendas, TiendasPauta.TiendasPauta, ISNULL(TiendasPVI.PVI,0)PVI, " & _
                        "CASE WHEN ISNULL(TiendasPVI.PVI,0)=0 THEN '0%'  " & _
                        "ELSE convert(nvarchar(5),(100*ISNULL(TiendasPVI.PVI,0)/TiendasPauta.TiendasPauta))+'%' END AS Porcentaje  " & _
                        "FROM(SELECT orden,nombre_region,region_mars,id_tipo,COUNT(id_tienda)Tiendas " & _
                        "FROM (SELECT DISTINCT RE.orden,REL.region_mars, RE.id_tienda, REL.id_tipo, TI.nombre_region " & _
                        "FROM AS_Rutas_Eventos as RE " & _
                        "INNER JOIN View_Tiendas_AS as TI ON TI.id_tienda = RE.id_tienda " & _
                        "INNER JOIN View_Usuario_AS as REL ON RE.id_usuario=REL.id_usuario " & _
                        "WHERE RE.orden=" & cmbPeriodo.SelectedValue & " " & _
                        "" + QuincenaSQL + RegionSQL + EjecutivoSQL + SupervisorSQL + PromotorSQL + CadenaSQL + ")RE " & _
                        "GROUP BY orden,nombre_region,region_mars,id_tipo)Tiendas " & _
                        "INNER JOIN Usuarios as REL ON Tiendas.region_mars=REL.id_usuario " & _
                        "INNER JOIN(SELECT region_mars, COUNT(id_tienda)TiendasPauta " & _
                        "FROM (SELECT DISTINCT REL.region_mars, RE.id_tienda " & _
                        "FROM AS_Rutas_Eventos as RE " & _
                        "INNER JOIN View_Usuario_AS as REL ON RE.id_usuario=REL.id_usuario " & _
                        "INNER JOIN AS_Tiendas as TI ON TI.id_tienda = RE.id_tienda " & _
                        "INNER JOIN AS_Tipo_Tiendas_PVI as TPVI ON TPVI.id_clasificacion=TI.id_clasificacion " & _
                        "WHERE RE.orden=" & cmbPeriodo.SelectedValue & " " & _
                        "" + QuincenaSQL + RegionSQL + EjecutivoSQL + SupervisorSQL + PromotorSQL + CadenaSQL + ")H " & _
                        "GROUP BY region_mars)TiendasPauta ON Tiendas.region_mars=TiendasPauta.region_mars " & _
                        "RIGHT JOIN (SELECT region_mars, COUNT(Cumplimiento)PVI " & _
                        "FROM(SELECT DISTINCT RE.orden,region_mars,RE.id_tienda,Cumplimiento " & _
                        "FROM View_Historial_Cumplimiento_AS as RE " & _
                        "INNER JOIN AS_Tiendas as TI ON TI.id_tienda = RE.id_tienda " & _
                        "INNER JOIN Usuarios_Relacion as REL ON RE.id_usuario=REL.id_usuario  " & _
                        "WHERE RE.orden=" & cmbPeriodo.SelectedValue & " AND Cumplimiento=4 " & _
                        "" + QuincenaSQL + RegionSQL + EjecutivoSQL + SupervisorSQL + PromotorSQL + CadenaSQL + ")H " & _
                        "GROUP BY region_mars)TiendasPVI ON Tiendas.region_mars=TiendasPVI.region_mars " & _
                        "UNION ALL " & _
                        "SELECT Tiendas.orden,Tiendas.nombre_region,REL.id_tipo,REL.region_mars, Tiendas.id_usuario, Tiendas.id_usuario as Usuario, Tiendas.Tiendas, TiendasPauta.TiendasPauta, ISNULL(TiendasPVI.PVI,0)PVI, " & _
                        "CASE WHEN ISNULL(TiendasPVI.PVI,0)=0 THEN '0%'  " & _
                        "ELSE convert(nvarchar(5),(100*ISNULL(TiendasPVI.PVI,0)/TiendasPauta.TiendasPauta))+'%' END AS Porcentaje  " & _
                        "FROM(SELECT orden,id_usuario,nombre_region,COUNT(id_tienda)Tiendas " & _
                        "FROM (SELECT DISTINCT RE.orden,RE.id_usuario, RE.id_tienda,TI.nombre_region " & _
                        "FROM AS_Rutas_Eventos as RE " & _
                        "INNER JOIN View_Tiendas_AS as TI ON TI.id_tienda = RE.id_tienda " & _
                        "INNER JOIN View_Usuario_AS as REL ON RE.id_usuario=REL.id_usuario " & _
                        "WHERE RE.orden=" & cmbPeriodo.SelectedValue & " " & _
                        "" + QuincenaSQL + RegionSQL + EjecutivoSQL + SupervisorSQL + PromotorSQL + CadenaSQL + ")RE " & _
                        "GROUP BY orden,nombre_region,id_usuario)Tiendas " & _
                        "INNER JOIN View_Usuario_AS as REL ON Tiendas.id_usuario=REL.id_usuario " & _
                        "INNER JOIN(SELECT id_usuario, COUNT(id_tienda)TiendasPauta " & _
                        "FROM (SELECT DISTINCT RE.id_usuario, RE.id_tienda " & _
                        "FROM AS_Rutas_Eventos as RE " & _
                        "INNER JOIN AS_Tiendas as TI ON TI.id_tienda = RE.id_tienda " & _
                        "INNER JOIN View_Usuario_AS as REL ON RE.id_usuario=REL.id_usuario " & _
                        "INNER JOIN AS_Tipo_Tiendas_PVI as TPVI ON TPVI.id_clasificacion=TI.id_clasificacion " & _
                        "WHERE RE.orden=" & cmbPeriodo.SelectedValue & " " & _
                        "" + QuincenaSQL + RegionSQL + EjecutivoSQL + SupervisorSQL + PromotorSQL + CadenaSQL + "" & _
                        ")H GROUP BY id_usuario)TiendasPauta " & _
                        "ON Tiendas.id_usuario=TiendasPauta.id_usuario " & _
                        "RIGHT JOIN (SELECT id_usuario, COUNT(Cumplimiento)PVI " & _
                        "FROM(SELECT DISTINCT orden,RE.id_usuario,RE.id_tienda,Cumplimiento " & _
                        "FROM View_Historial_Cumplimiento_AS as RE " & _
                        "INNER JOIN AS_Tiendas as TI ON TI.id_tienda = RE.id_tienda " & _
                        "WHERE RE.orden=" & cmbPeriodo.SelectedValue & " AND Cumplimiento=4 " & _
                        "" + QuincenaSQL + RegionSQL + EjecutivoSQL + SupervisorSQL + PromotorSQL + CadenaSQL + ")H " & _
                        "GROUP BY id_usuario)TiendasPVI ON Tiendas.id_usuario=TiendasPVI.id_usuario " & _
                        "ORDER BY region_mars ASC, id_tipo DESC", Me.gridReporte)
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


            cmbPeriodo.SelectedValue = Request.Params("orden")
            cmbQuincena.SelectedValue = Request.Params("id_quincena")
            cmbRegion.SelectedValue = Request.Params("id_region")
            cmbEjecutivo.SelectedValue = Request.Params("region_mars")
            cmbSupervisor.SelectedValue = Request.Params("id_supervisor")
            cmbPromotor.SelectedValue = Request.Params("id_usuario")

            If Request.Params("orden") <> "" Then
                SQLCombo()
                Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
                Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
                Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
                Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
                Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

                CargarReporte()
            End If
        End If
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub


    Protected Sub cmbQuincena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbQuincena.SelectedIndexChanged
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte detalle exhibiciones por promotor " + cmbPeriodo.SelectedItem.ToString() + " " + cmbQuincena.SelectedItem.ToString() + ".xls")
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

        CargarReporte()
    End Sub

    Private Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        cmbEjecutivo.SelectedValue = ""

        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Protected Sub lnkVerTodo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkVerTodo.Click
        Response.Redirect("~/sitios/Mars/Autoservicio/Reportes/ReporteDetalleExhibicionesMarsAS.aspx?orden=" & cmbPeriodo.SelectedValue & "&region_mars=" & cmbEjecutivo.SelectedValue & "&id_quincena=" & cmbQuincena.SelectedValue & "&id_region=" & cmbRegion.SelectedValue & "&id_supervisor=" & cmbSupervisor.SelectedValue & "&id_usuario=" & cmbPromotor.SelectedValue & "")
    End Sub

    Private Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCadena.SelectedIndexChanged
        CargarReporte()
    End Sub
End Class