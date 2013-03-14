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

Partial Public Class ReporteMedidasMarsAS2
    Inherits System.Web.UI.Page

    Sub SQLCombo()
        MarsAS.SQLsCombo(cmbPeriodo.SelectedValue, cmbQuincena.SelectedValue, _
                         cmbRegion.SelectedValue, cmbEjecutivo.SelectedValue, _
                         cmbSupervisor.SelectedValue, cmbPromotor.SelectedValue, _
                         cmbCadena.SelectedValue, "View_Historial_AS")
    End Sub

    Sub CargarReporte()
        Dim RegionSQL, EjecutivoSQL, SupervisorSQL, PromotorSQL As String

        RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
        EjecutivoSQL = Acciones.Slc.cmb("REL.region_mars", cmbEjecutivo.SelectedValue)
        SupervisorSQL = Acciones.Slc.cmb("REL.id_supervisor", cmbSupervisor.SelectedValue)
        PromotorSQL = Acciones.Slc.cmb("RUT.id_usuario", cmbPromotor.SelectedValue)

        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT EJE.numero_ruta Ejecutivo,RUT.id_usuario,TI.nombre_region, TI.nombre_cadena, TI.nombre, " & _
                    "TI.nombre_visita,TI.area_nielsen,TI.codigo,TI.nombre_grupo,TI.nombre_formato, TI.clasificacion_tienda,TI.ciudad, TI.nombre_estado, " & _
                    "RUT.W1_1+RUT.W1_2+RUT.W1_3+RUT.W1_4+RUT.W1_5+RUT.W1_6+RUT.W1_7+" & _
                    "RUT.W2_1+RUT.W2_2+RUT.W2_3+RUT.W2_4+RUT.W2_5+RUT.W2_6+RUT.W2_7+" & _
                    "RUT.W3_1+RUT.W3_2+RUT.W3_3+RUT.W3_4+RUT.W3_5+RUT.W3_6+RUT.W3_7+" & _
                    "RUT.W4_1+RUT.W4_2+RUT.W4_3+RUT.W4_4+RUT.W4_5+RUT.W4_6+RUT.W4_7 as frecuencia, AN.tipo_medida, " & _
                    "CASE WHEN RUT.W1_1=1 then 'X' else '' end W1_1,CASE WHEN RUT.W1_2=1 then 'X' else '' end W1_2, " & _
                    "CASE WHEN RUT.W1_3=1 then 'X' else '' end W1_3,CASE WHEN RUT.W1_4=1 then 'X' else '' end W1_4, " & _
                    "CASE WHEN RUT.W1_5=1 then 'X' else '' end W1_5,CASE WHEN RUT.W1_6=1 then 'X' else '' end W1_6, " & _
                    "CASE WHEN RUT.W1_7=1 then 'X' else '' end W1_7, " & _
                    "CASE WHEN RUT.W2_1=1 then 'X' else '' end W2_1,CASE WHEN RUT.W2_2=1 then 'X' else '' end W2_2, " & _
                    "CASE WHEN RUT.W2_3=1 then 'X' else '' end W2_3,CASE WHEN RUT.W2_4=1 then 'X' else '' end W2_4, " & _
                    "CASE WHEN RUT.W2_5=1 then 'X' else '' end W2_5,CASE WHEN RUT.W2_6=1 then 'X' else '' end W2_6, " & _
                    "CASE WHEN RUT.W2_7=1 then 'X' else '' end W2_7, " & _
                    "CASE WHEN RUT.W3_1=1 then 'X' else '' end W3_1,CASE WHEN RUT.W3_2=1 then 'X' else '' end W3_2, " & _
                    "CASE WHEN RUT.W3_3=1 then 'X' else '' end W3_3,CASE WHEN RUT.W3_4=1 then 'X' else '' end W3_4, " & _
                    "CASE WHEN RUT.W3_5=1 then 'X' else '' end W3_5,CASE WHEN RUT.W3_6=1 then 'X' else '' end W3_6, " & _
                    "CASE WHEN RUT.W3_7=1 then 'X' else '' end W3_7, " & _
                    "CASE WHEN RUT.W4_1=1 then 'X' else '' end W4_1,CASE WHEN RUT.W4_2=1 then 'X' else '' end W4_2, " & _
                    "CASE WHEN RUT.W4_3=1 then 'X' else '' end W4_3,CASE WHEN RUT.W4_4=1 then 'X' else '' end W4_4, " & _
                    "CASE WHEN RUT.W4_5=1 then 'X' else '' end W4_5,CASE WHEN RUT.W4_6=1 then 'X' else '' end W4_6, " & _
                    "CASE WHEN RUT.W4_7=1 then 'X' else '' end W4_7 " & _
                    "FROM AS_CatRutas as RUT " & _
                    "INNER JOIN AS_Historial as H ON H.id_tienda = RUT.id_tienda " & _
                    "INNER JOIN View_Tiendas_AS as TI ON TI.id_tienda= RUT.id_tienda " & _
                    "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario = RUT.id_usuario " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario = REL.region_mars  " & _
                    "INNER JOIN AS_Ejecutivos as EJE ON EJE.region_mars = REL.region_mars " & _
                    "INNER JOIN AS_Medida_anaquel as AN ON AN.id_medida = H.id_medida " & _
                    "WHERE H.orden=" & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + EjecutivoSQL + SupervisorSQL + PromotorSQL + " " & _
                    "ORDER BY RUT.id_usuario", Me.gridReporte)
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

    Private Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.QuincenaSQLCmb, "nombre_quincena", "id_quincena", cmbQuincena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Private Sub cmbQuincena_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbQuincena.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.EjecutivoSQLCmb, "ejecutivo", "region_mars", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
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

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Private Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCadena.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_AS.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Private Sub cmbTienda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTienda.SelectedIndexChanged
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

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 13 To 19
                e.Row.Cells(i).BackColor = Drawing.Color.LightGoldenrodYellow : Next i
            For i = 20 To 26
                e.Row.Cells(i).BackColor = Drawing.Color.CadetBlue : Next i
            For i = 27 To 33
                e.Row.Cells(i).BackColor = Drawing.Color.CornflowerBlue : Next i
            For i = 34 To 40
                e.Row.Cells(i).BackColor = Drawing.Color.DeepPink : Next i
        End If

        Select Case e.Row.RowType
            Case DataControlRowType.Header
                For i = 0 To 13
                    e.Row.Cells(i).Visible = True
                Next i

                e.Row.Cells(13).ColumnSpan = 7
                e.Row.Cells(20).ColumnSpan = 7
                e.Row.Cells(27).ColumnSpan = 7
                e.Row.Cells(34).ColumnSpan = 7

                For i = 14 To 19
                    e.Row.Cells(i).Visible = False : Next i
                For i = 21 To 26
                    e.Row.Cells(i).Visible = False : Next i
                For i = 28 To 33
                    e.Row.Cells(i).Visible = False : Next i
                For i = 35 To 40
                    e.Row.Cells(i).Visible = False : Next i

                e.Row.Cells(13).Controls.Clear()
                e.Row.Cells(13).Text = "<table align='center' style='width: 168px; font-weight: 700;'>" & _
                          "<tr><td colspan='7' style='text-align: center;font-weight: bold''>Semana 1</td></tr>" & _
                          "<tr><td style='font-weight: bold; width: 24px; text-align: center;'>D</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>L</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>M</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>X</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>J</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>V</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>S</td>" & _
                          "</tr></table>"

                e.Row.Cells(20).Controls.Clear()
                e.Row.Cells(20).Text = "<table align='center' style='width: 168px; font-weight: 700;'>" & _
                          "<tr><td colspan='7' style='text-align: center;font-weight: bold''>Semana 2</td></tr>" & _
                          "<tr><td style='font-weight: bold; width: 24px; text-align: center;'>D</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>L</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>M</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>X</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>J</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>V</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>S</td>" & _
                          "</tr></table>"

                e.Row.Cells(27).Controls.Clear()
                e.Row.Cells(27).Text = "<table align='center' style='width: 168px; font-weight: 700;'>" & _
                          "<tr><td colspan='7' style='text-align: center;font-weight: bold''>Semana 3</td></tr>" & _
                          "<tr><td style='font-weight: bold; width: 24px; text-align: center;'>D</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>L</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>M</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>X</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>J</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>V</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>S</td>" & _
                          "</tr></table>"

                e.Row.Cells(34).Controls.Clear()
                e.Row.Cells(34).Text = "<table align='center' style='width: 168px; font-weight: 700;'>" & _
                          "<tr><td colspan='7' style='text-align: center;font-weight: bold''>Semana 4</td></tr>" & _
                          "<tr><td style='font-weight: bold; width: 24px; text-align: center;'>D</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>L</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>M</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>X</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>J</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>V</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>S</td>" & _
                          "</tr></table>"
        End Select
    End Sub

End Class