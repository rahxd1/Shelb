Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class AdminParticipacionMars
    Inherits System.Web.UI.Page

    Dim Filtro, FiltroFecha As String
    Dim Inicio, Fin As String

    Sub BuscaPeriodo()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT * FROM Periodos where id_periodo = '" & cmbPeriodo.SelectedValue.ToString() & "'")
        If Tabla.Rows.Count > 0 Then
            Inicio = ISODates.Dates.SQLServerDate(CDate(Tabla.Rows(0)("fecha_inicio_periodo")))
            Fin = ISODates.Dates.SQLServerDate(CDate(Tabla.Rows(0)("fecha_fin_periodo")))
        End If

        tabla.Dispose()
    End Sub

    Sub CargarReporte()
        BuscaPeriodo()

            If cmbPromotor.Text <> "" Then
                Filtro = "AND BITA.id_usuario= '" & cmbPromotor.Text & "'":End If
            If cmbSupervisor.Text <> "" Then
                Filtro = "AND BITA.id_usuario= '" & cmbPromotor.Text & "'"
            End If
            If cmbEjecutivoMars.Text <> "" Then
                Filtro = "AND BITA.id_usuario= '" & cmbPromotor.Text & "'"
            End If
            If cmbEjecutivoCuenta.Text <> "" Then
                Filtro = "AND BITA.id_usuario= '" & cmbPromotor.Text & "'"
            End If

        CargaGrilla(ConexionMars.localSqlServer, _
                    "select distinct MUS.id_promotor, " & _
                    "BLOG.ParticipacionBlog, " & _
                    "COMEN.ParticipacionComentarios, " & _
                    "EVEN.ParticipacionFotosEventos " & _
                    "FROM Usuarios as MUS " & _
                    "FULL JOIN (select de_id_usuario,count(de_id_usuario) as ParticipacionComentarios " & _
                    "FROM Comentarios " & _
                    "WHERE fecha_registro>='" & Inicio & "' AND fecha_registro<='" & Fin & "' " & _
                    "GROUP BY de_id_usuario) AS COMEN " & _
                    "ON MUS.id_promotor = COMEN.de_id_usuario " & _
                    "FULL JOIN (select de_id_usuario, count(de_id_usuario) as ParticipacionBlog " & _
                    "FROM Blog " & _
                    "WHERE fecha_registro>='" & Inicio & "' AND fecha_registro<='" & Fin & "' " & _
                    "GROUP BY de_id_usuario) AS BLOG " & _
                    "ON MUS.id_promotor = BLOG.de_id_usuario " & _
                    "FULL JOIN (select id_usuario,count(id_usuario) as ParticipacionFotosEventos " & _
                    "FROM Eventos " & _
                    "WHERE fecha_registro>='" & Inicio & "' AND fecha_registro<='" & Fin & "' " & _
                    "GROUP BY id_usuario) AS EVEN " & _
                    "ON MUS.id_promotor = EVEN.id_usuario " & _
                    "WHERE MUS.activo='1' " & _
                    "ORDER BY MUS.id_promotor ", Me.gridReporte)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct UM.id_ejecutivo, U.nombre, U.nombre as NombreEjecutivoCuenta from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_ejecutivo= U.no_region order by UM.id_ejecutivo", "NombreEjecutivoCuenta", "id_ejecutivo", cmbEjecutivoCuenta)
            Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct UM.no_region, U.nombre, UM.no_region + '- ' + U.nombre as NombreEjecutivo from Usuarios as UM INNER JOIN Usuarios as U ON UM.no_region= U.no_region order by UM.no_region", "NombreEjecutivo", "no_region", cmbEjecutivoMars)
            Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct UM.id_supervisor, U.nombre, UM.id_supervisor + '-' + U.nombre as NombreSupervisor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_supervisor= U.id_usuario order by UM.id_supervisor", "NombreSupervisor", "id_supervisor", cmbSupervisor)
            Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct UM.id_promotor, U.nombre, UM.id_promotor + '-' + U.nombre as NombrePromotor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_promotor= U.id_usuario order by UM.id_promotor", "NombrePromotor", "id_promotor", cmbPromotor)
            Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct id_periodo, nombre_periodo,fecha_inicio_periodo FROM Periodos ORDER BY fecha_inicio_periodo", "nombre_periodo", "id_periodo", cmbPeriodo)

            Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                                   "SELECT DISTINCT id_periodo, nombre_periodo, fecha_inicio_periodo, fecha_fin_periodo " & _
                                                   "FROM Periodos where fecha_inicio_periodo<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                                                   "AND fecha_fin_periodo>= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "'")
            If Tabla.Rows.Count > 0 Then
                cmbPeriodo.SelectedValue = Tabla.Rows(0)("id_periodo")
            End If

            Tabla.Dispose()
        End If
    End Sub

    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGenerar.Click
        PnlGrid.Visible = True


        CargarReporte()
        btnExcel.Enabled = True
    End Sub

    Protected Sub btnExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExcel.Click
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Bitácora de Accesos.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        cmbSupervisor.Text = ""
        cmbEjecutivoCuenta.Text = ""
        cmbEjecutivoMars.Text = ""
    End Sub

    Protected Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        cmbPromotor.Text = ""
        cmbEjecutivoCuenta.Text = ""
        cmbEjecutivoMars.Text = ""
    End Sub

    Protected Sub cmbEjecutivoSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbEjecutivoMars.SelectedIndexChanged
        cmbPromotor.Text = ""
        cmbSupervisor.Text = ""
        cmbEjecutivoCuenta.Text = ""
    End Sub

    Protected Sub cmbEjecutivoCuenta_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbEjecutivoCuenta.SelectedIndexChanged
        cmbPromotor.Text = ""
        cmbSupervisor.Text = ""
        cmbEjecutivoMars.Text = ""
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        BuscaPeriodo()
    End Sub
End Class