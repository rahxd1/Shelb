Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class DetalleObjetivo5
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Detalle1()
            Detalle2()
        End If
    End Sub

    Sub Detalle1()
        CargaGrilla(ConexionMars.localSqlServer, _
                    "select Datos.nombre_periodo, COUNT(Datos.id_tienda)Tiendas,SUM(Datos.Q1)Q1, SUM(Datos.Q2)Q2 " & _
                    "FROM(select DISTINCT PER.nombre_periodo,RE.id_tienda, (estatus_anaquel_q1)Q1, (estatus_anaquel_q2)Q2 " & _
                    "FROM AS_Rutas_Eventos as RE " & _
                    "INNER JOIN Periodos as PER ON PER.id_periodo = RE.id_periodo " & _
                    "WHERE id_usuario='" & Request.Params("id_usuario") & "')Datos " & _
                    "GROUP BY Datos.nombre_periodo", Me.gridDetalle)
    End Sub

    Sub Detalle2()
        CargaGrilla(ConexionMars.localSqlServer, _
                    "select DISTINCT PER.orden,PER.nombre_periodo,CAD.nombre_cadena, TI.nombre,  " & _
                    "CASE when RE.estatus_anaquel_q1=1 then 'SI' else 'NO' end as Q1, " & _
                    "CASE when RE.estatus_anaquel_q2=1 then 'SI' else 'NO' end as Q2 " & _
                    "FROM AS_Rutas_Eventos as RE " & _
                    "INNER JOIN Periodos as PER ON PER.id_periodo = RE.id_periodo " & _
                    "INNER JOIN AS_Tiendas as TI ON RE.id_tienda = TI.id_tienda " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = TI.id_cadena " & _
                    "WHERE id_usuario='" & Request.Params("id_usuario") & "' " & _
                    "ORDER BY PER.orden, CAD.nombre_cadena, TI.nombre", Me.gridDetalle2)
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
        Me.gridDetalle.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.pnlGrid)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte PDP Objetivo 5.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class