Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class DetalleObjetivo4
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Detalle1()
            Detalle2()
        End If
    End Sub

    Sub Detalle1()
        CargaGrilla(ConexionMars.localSqlServer, _
                    "select 'Modulo 1' as Modulo, modulo_1 as Cal " & _
                    "FROM AS_Calificaciones as CAL WHERE id_usuario='" & Request.Params("id_usuario") & "' " & _
                    "UNION ALL " & _
                    "select 'Modulo 2' as Modulo, modulo_2 as Cal " & _
                    "FROM AS_Calificaciones as CAL WHERE id_usuario='" & Request.Params("id_usuario") & "' " & _
                    "UNION ALL " & _
                    "select 'Modulo 3' as Modulo, modulo_3 as Cal " & _
                    "FROM AS_Calificaciones as CAL WHERE id_usuario='" & Request.Params("id_usuario") & "' " & _
                    "UNION ALL " & _
                    "select 'Modulo 4' as Modulo, modulo_4 as Cal " & _
                    "FROM AS_Calificaciones as CAL WHERE id_usuario='" & Request.Params("id_usuario") & "'", _
                    Me.gridDetalle)
    End Sub

    Sub Detalle2()
        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT PRO.nombre_proceso,PRO.notas,PRO.fecha_inicio,PRO.fecha_fin, " & _
                    "CASE when RE.estatus=1 then 'Si' else 'No' end as SiNo, RE.fecha " & _
                    "FROM AS_Procesos as PRO  " & _
                    "INNER JOIN (select RE.id_proceso,RE.id_usuario,Re.estatus, RE.fecha " & _
                    "FROM AS_Procesos_Rutas_Eventos as RE  " & _
                    "INNER JOIN Usuarios as US ON RE.id_usuario=US.id_usuario  " & _
                    "WHERE RE.fecha>=US.fecha_ingreso AND RE.id_usuario='" & Request.Params("id_usuario") & "')RE ON RE.id_proceso = PRO.id_proceso  " & _
                    "WHERE PRO.tipo_proceso = 1", Me.gridDetalle2)
    End Sub

    Sub Detalle3()
        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT PRO.nombre_proceso,PRO.notas,PRO.fecha_inicio,PRO.fecha_fin, " & _
                    "CASE when RE.estatus=1 then 'Si' else 'No' end as SiNo, RE.fecha " & _
                    "FROM AS_Procesos as PRO  " & _
                    "INNER JOIN (select RE.id_proceso,RE.id_usuario,Re.estatus, RE.fecha " & _
                    "FROM AS_Procesos_Rutas_Eventos as RE  " & _
                    "INNER JOIN Usuarios as US ON RE.id_usuario=US.id_usuario  " & _
                    "WHERE RE.fecha>=US.fecha_ingreso AND RE.id_usuario='" & Request.Params("id_usuario") & "')RE ON RE.id_proceso = PRO.id_proceso  " & _
                    "WHERE PRO.tipo_proceso = 1", Me.gridDetalle3)
    End Sub

    Protected Sub lnkObjetivo1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkObjetivo1.Click
        Response.Redirect("~/sitios/Mars/Autoservicio/PDP/ReportePDPMarsAS.aspx?id_usuario=" & Request.Params("id_usuario") & "")
    End Sub

    Private Sub lnkExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.pnlGrid.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.pnlGrid)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte PDP Objetivo 4.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class