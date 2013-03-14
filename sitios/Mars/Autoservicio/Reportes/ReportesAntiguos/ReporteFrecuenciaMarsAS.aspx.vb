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

Partial Public Class ReporteFrecuenciaMarsAS
    Inherits System.Web.UI.Page

    Dim RegionSel, EjecutivoSel, SupervisorSel, PromotorSel, Promotor As String
    Dim RegionSQL, EjecutivoSQL, SupervisorSQL, PromotorSQL As String
    Dim PeriodoActual As String

    Sub SQLCombo()
        RegionSel = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
        EjecutivoSel = Acciones.Slc.cmb("REL.region_mars", cmbEjecutivo.SelectedValue)
        SupervisorSel = Acciones.Slc.cmb("REL.id_supervisor", cmbSupervisor.SelectedValue)
        PromotorSel = Acciones.Slc.cmb("RUT.id_usuario", cmbPromotor.SelectedValue)

        RegionSQL = "SELECT DISTINCT REG.id_region, REG.nombre_region " & _
                    "FROM View_Tiendas_AS as REG " & _
                    "INNER JOIN View_Usuario_AS as REL ON REL.id_region = REG.id_region " & _
                    "WHERE REG.id_region <>0 " + Promotor + " " & _
                    " ORDER BY REG.nombre_region"

        EjecutivoSQL = "SELECT DISTINCT REL.Ejecutivo, REL.region_mars " & _
                    "FROM AS_CatRutas AS RUT " & _
                    "INNER JOIN View_Tiendas_AS AS TI ON TI.id_tienda = RUT.id_tienda " & _
                    "INNER JOIN View_Usuario_AS as REL ON REL.id_usuario = RUT.id_usuario " & _
                    "WHERE TI.id_tienda<>0 " & _
                    " " + RegionSel + Promotor + " " & _
                    " ORDER BY REL.region_mars"

        SupervisorSQL = "SELECT DISTINCT REL.Supervisor, REL.id_supervisor " & _
                    "FROM View_Usuario_AS as REL " & _
                    "INNER JOIN AS_Rutas_Eventos AS RE ON RE.id_usuario=REL.id_usuario " & _
                    "INNER JOIN View_Tiendas_AS AS TI ON RE.id_tienda = TI.id_tienda " & _
                    "WHERE REL.id_usuario<>'' " + RegionSel + Promotor + " " & _
                    " ORDER BY REL.id_supervisor"

        PromotorSQL = "SELECT DISTINCT RUT.id_usuario " & _
                    "FROM AS_CatRutas AS RUT " & _
                    "INNER JOIN View_Tiendas_AS AS TI ON TI.id_tienda = RUT.id_tienda " & _
                    "INNER JOIN View_Usuario_AS as REL ON REL.id_usuario = RUT.id_usuario " & _
                    "WHERE TI.id_tienda<>0 " & _
                    " " + RegionSel + EjecutivoSel + SupervisorSel + Promotor + " " & _
                    " ORDER BY RUT.id_usuario"
    End Sub

    Sub CargarReporte()
        RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
        EjecutivoSQL = Acciones.Slc.cmb("REL.region_mars", cmbEjecutivo.SelectedValue)
        SupervisorSQL = Acciones.Slc.cmb("REL.id_supervisor", cmbSupervisor.SelectedValue)
        PromotorSQL = Acciones.Slc.cmb("RUT.id_usuario", cmbPromotor.SelectedValue)

        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT REL.region_mars +' - '+US.nombre Ejecutivo,RUT.id_usuario,REG.nombre_region, CAD.nombre_cadena, TI.nombre,TI.id_tienda, " & _
                    "RUT.tipo,RUT.frecuencia,TI.area_nielsen,TI.codigo, CTI.clasificacion_tienda,Ti.ciudad, ES.nombre_estado, " & _
                    "CASE WHEN RUT.W1_1=1 then 'X' else '' end W1_1,CASE WHEN RUT.W1_2=1 then 'X' else '' end W1_2, " & _
                    "CASE WHEN RUT.W1_1=1 then 'X' else '' end W1_3,CASE WHEN RUT.W1_4=1 then 'X' else '' end W1_4, " & _
                    "CASE WHEN RUT.W1_5=1 then 'X' else '' end W1_5,CASE WHEN RUT.W1_6=1 then 'X' else '' end W1_6, " & _
                    "CASE WHEN RUT.W1_7=1 then 'X' else '' end W1_7, " & _
                    "CASE WHEN RUT.W2_1=1 then 'X' else '' end W2_1,CASE WHEN RUT.W2_2=1 then 'X' else '' end W2_2, " & _
                    "CASE WHEN RUT.W2_1=1 then 'X' else '' end W2_3,CASE WHEN RUT.W2_4=1 then 'X' else '' end W2_4, " & _
                    "CASE WHEN RUT.W2_5=1 then 'X' else '' end W2_5,CASE WHEN RUT.W2_6=1 then 'X' else '' end W2_6, " & _
                    "CASE WHEN RUT.W2_7=1 then 'X' else '' end W2_7, " & _
                    "CASE WHEN RUT.W3_1=1 then 'X' else '' end W3_1,CASE WHEN RUT.W3_2=1 then 'X' else '' end W3_2, " & _
                    "CASE WHEN RUT.W3_1=1 then 'X' else '' end W3_3,CASE WHEN RUT.W3_4=1 then 'X' else '' end W3_4, " & _
                    "CASE WHEN RUT.W3_5=1 then 'X' else '' end W3_5,CASE WHEN RUT.W3_6=1 then 'X' else '' end W3_6, " & _
                    "CASE WHEN RUT.W3_7=1 then 'X' else '' end W3_7, " & _
                    "CASE WHEN RUT.W4_1=1 then 'X' else '' end W4_1,CASE WHEN RUT.W4_2=1 then 'X' else '' end W4_2, " & _
                    "CASE WHEN RUT.W4_1=1 then 'X' else '' end W4_3,CASE WHEN RUT.W4_4=1 then 'X' else '' end W4_4, " & _
                    "CASE WHEN RUT.W4_5=1 then 'X' else '' end W4_5,CASE WHEN RUT.W4_6=1 then 'X' else '' end W4_6, " & _
                    "CASE WHEN RUT.W4_7=1 then 'X' else '' end W4_7 " & _
                    "FROM AS_CatRutas as RUT " & _
                    "INNER JOIN AS_Tiendas as TI ON TI.id_tienda= RUT.id_tienda " & _
                    "INNER JOIN AS_Clasificacion_Tiendas as CTI ON CTI.id_clasificacion=TI.id_clasificacion " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena= TI.id_cadena " & _
                    "INNER JOIN Estados as ES ON ES.id_estado=TI.id_estado " & _
                    "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region " & _
                    "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario = RUT.id_usuario " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario = REL.region_mars  " & _
                    "WHERE RUT.id_usuario<>'' " & _
                    " " + RegionSQL + " " & _
                    " " + EjecutivoSQL + " " & _
                    " " + SupervisorSQL + " " & _
                    " " + PromotorSQL + " " & _
                    "ORDER BY RUT.id_usuario", gridReporte)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, EjecutivoSQL, "ejecutivo", "region_mars", cmbEjecutivo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, SupervisorSQL, "supervisor", "id_supervisor", cmbSupervisor)
            Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

            CargarReporte()
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, EjecutivoSQL, "ejecutivo", "region_mars", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, SupervisorSQL, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte frecuencia.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbEjecutivo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEjecutivo.SelectedIndexChanged
        cmbSupervisor.SelectedValue = ""

        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Private Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        cmbEjecutivo.SelectedValue = ""

        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 12 To 18
                e.Row.Cells(i).BackColor = Drawing.Color.LightGoldenrodYellow : Next i
            For i = 19 To 25
                e.Row.Cells(i).BackColor = Drawing.Color.CadetBlue : Next i
            For i = 26 To 32
                e.Row.Cells(i).BackColor = Drawing.Color.CornflowerBlue : Next i
            For i = 33 To 38
                e.Row.Cells(i).BackColor = Drawing.Color.DeepPink : Next i
        End If

        Select Case e.Row.RowType
            Case DataControlRowType.Header
                e.Row.Cells(0).Visible = True
                e.Row.Cells(1).Visible = True
                e.Row.Cells(2).Visible = True
                e.Row.Cells(3).Visible = True
                e.Row.Cells(4).Visible = True
                e.Row.Cells(5).Visible = True
                e.Row.Cells(6).Visible = True
                e.Row.Cells(7).Visible = True
                e.Row.Cells(8).Visible = True
                e.Row.Cells(9).Visible = True
                e.Row.Cells(10).Visible = True
                e.Row.Cells(11).Visible = True
                e.Row.Cells(12).Visible = True

                e.Row.Cells(12).ColumnSpan = 7
                e.Row.Cells(19).ColumnSpan = 7
                e.Row.Cells(26).ColumnSpan = 7
                e.Row.Cells(33).ColumnSpan = 7
                For i = 13 To 18
                    e.Row.Cells(i).Visible = False : Next i
                For i = 20 To 25
                    e.Row.Cells(i).Visible = False : Next i
                For i = 27 To 32
                    e.Row.Cells(i).Visible = False : Next i
                For i = 34 To 37
                    e.Row.Cells(i).Visible = False : Next i

                e.Row.Cells(12).Controls.Clear()
                e.Row.Cells(12).Text = "<table align='center' style='width: 100%; font-weight: 700;'>" & _
                          "<tr><td colspan='7' style='text-align: center;font-weight: bold''>Semana 1</td></tr>" & _
                          "<tr><td style='font-weight: bold; width: 50px'>Dom</td>" & _
                          "<td style='font-weight: bold; width: 180px'>Lun</td>" & _
                          "<td style='font-weight: bold; width: 180px'>Mar</td>" & _
                          "<td style='font-weight: bold; width: 180px'>Mié</td>" & _
                          "<td style='font-weight: bold; width: 180px'>Jue</td>" & _
                          "<td style='font-weight: bold; width: 180px'>Vie</td>" & _
                          "<td style='font-weight: bold; width: 180px'>Sáb</td>" & _
                          "</tr></table>"

                e.Row.Cells(19).Controls.Clear()
                e.Row.Cells(19).Text = "<table align='center' style='width: 100%; font-weight: 700;'>" & _
                          "<tr><td colspan='7' style='text-align: center;font-weight: bold''>Semana 2</td></tr>" & _
                          "<tr><td style='font-weight: bold'>Dom</td>" & _
                          "<td style='font-weight: bold'>Lun</td>" & _
                          "<td style='font-weight: bold'>Mar</td>" & _
                          "<td style='font-weight: bold'>Mié</td>" & _
                          "<td style='font-weight: bold'>Jue</td>" & _
                          "<td style='font-weight: bold'>Vie</td>" & _
                          "<td style='font-weight: bold'>Sáb</td>" & _
                          "</tr></table>"

                e.Row.Cells(26).Controls.Clear()
                e.Row.Cells(26).Text = "<table align='center' style='width: 100%; font-weight: 700;'>" & _
                          "<tr><td colspan='7' style='text-align: center;font-weight: bold''>Semana 3</td></tr>" & _
                          "<tr><td style='font-weight: bold'>Dom</td>" & _
                          "<td style='font-weight: bold'>Lun</td>" & _
                          "<td style='font-weight: bold'>Mar</td>" & _
                          "<td style='font-weight: bold'>Mié</td>" & _
                          "<td style='font-weight: bold'>Jue</td>" & _
                          "<td style='font-weight: bold'>Vie</td>" & _
                          "<td style='font-weight: bold'>Sáb</td>" & _
                          "</tr></table>"

                e.Row.Cells(33).Controls.Clear()
                e.Row.Cells(33).Text = "<table align='center' style='width: 100%; font-weight: 700;'>" & _
                          "<tr><td colspan='7' style='text-align: center;font-weight: bold''>Semana 4</td></tr>" & _
                          "<tr><td style='font-weight: bold'>Dom</td>" & _
                          "<td style='font-weight: bold'>Lun</td>" & _
                          "<td style='font-weight: bold'>Mar</td>" & _
                          "<td style='font-weight: bold'>Mié</td>" & _
                          "<td style='font-weight: bold'>Jue</td>" & _
                          "<td style='font-weight: bold'>Vie</td>" & _
                          "<td style='font-weight: bold'>Sáb</td>" & _
                          "</tr></table>"
        End Select
    End Sub
End Class