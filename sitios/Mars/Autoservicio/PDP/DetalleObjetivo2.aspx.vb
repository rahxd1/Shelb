Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class DetalleObjetivo2
    Inherits System.Web.UI.Page

    Dim Anaquel(30), Objetivo(30) As Integer
    Dim Cumplimiento(30) As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerObjetivo()
        End If
    End Sub

    Sub VerObjetivo()
        CargaGrilla(ConexionMars.localSqlServer, _
                    "select DISTINCT PER.orden,PER.nombre_periodo, RELUS.region_mars+' '+ US.nombre as EjecutivoMars, RE.id_usuario,  " & _
                    "TI.codigo, CTI.clasificacion_tienda,TI.area_nielsen,CAD.nombre_cadena,TI.nombre, " & _
                    "CASE WHEN (SUM(HDET.[1])+SUM(HDET.[5])) = 0 THEN 0 ELSE (100*SUM(HDET.[1]))/(SUM(HDET.[1])+SUM(HDET.[5]))END PS,AN.O_ps, " & _
                    "CASE WHEN (SUM(HDET.[2])+SUM(HDET.[6])) = 0 THEN 0 ELSE (100*SUM(HDET.[2]))/(SUM(HDET.[2])+SUM(HDET.[6]))END PC,AN.O_pc, " & _
                    "CASE WHEN (SUM(HDET.[3])+SUM(HDET.[7])) = 0 THEN 0 ELSE (100*SUM(HDET.[3]))/(SUM(HDET.[3])+SUM(HDET.[7]))END PH, AN.O_ph, " & _
                    "CASE WHEN (SUM(HDET.[4])+SUM(HDET.[8])) = 0 THEN 0 ELSE (100*SUM(HDET.[4]))/(SUM(HDET.[4])+SUM(HDET.[8]))END PB, AN.O_pb, " & _
                    "CASE WHEN (SUM(HDET.[9])+SUM(HDET.[12])) = 0 THEN 0 ELSE (100*SUM(HDET.[9]))/(SUM(HDET.[9])+SUM(HDET.[12]))END GS,AN.O_gs, " & _
                    "CASE WHEN (SUM(HDET.[10])+SUM(HDET.[13])) = 0 THEN 0 ELSE (100*SUM(HDET.[10]))/(SUM(HDET.[10])+SUM(HDET.[13]))END GH,AN.O_gh, " & _
                    "CASE WHEN (SUM(HDET.[11])+SUM(HDET.[14])) = 0 THEN 0 ELSE (100*SUM(HDET.[11]))/(SUM(HDET.[11])+SUM(HDET.[14]))END GB,AN.O_gb " & _
                    "FROM AS_Rutas_Eventos as RE " & _
                    "INNER JOIN Usuarios_Relacion as RELUS ON RELUS.id_usuario = RE.id_usuario " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario = RELUS.region_mars " & _
                    "INNER JOIN AS_Tiendas as TI ON RE.id_tienda = TI.id_tienda  " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = TI.id_cadena  " & _
                    "FULL JOIN AS_Historial as H ON TI.id_tienda = H.id_tienda  " & _
                    "INNER JOIN AS_Segmentos_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial  " & _
                    "INNER JOIN Periodos as PER ON PER.id_periodo = H.id_periodo AND RE.id_periodo = PER.id_periodo " & _
                    "INNER JOIN AS_Area_Nielsen AS AN ON AN.area_nielsen = TI.area_nielsen  " & _
                    "INNER JOIN AS_Clasificacion_Tiendas as CTI ON CTI.id_clasificacion= TI.id_clasificacion " & _
                    "WHERE RE.id_usuario='" & Request.Params("id_usuario") & "'  " & _
                    "GROUP BY PER.orden,PER.nombre_periodo, RELUS.region_mars,US.nombre,RE.id_usuario,  " & _
                    "TI.codigo, CTI.clasificacion_tienda,TI.area_nielsen,CAD.nombre_cadena,TI.nombre, " & _
                    "AN.O_ps,AN.O_pc,AN.O_ph,AN.O_pb,AN.O_gs,AN.O_gh,AN.O_gb " & _
                    "ORDER BY PER.orden,PER.nombre_periodo, TI.nombre, CAD.nombre_cadena ", _
                    Me.gridDetalle)
    End Sub

    Private Sub gridDetalle_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridDetalle.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 8 To 20 Step 2
                Anaquel(i) = e.Row.Cells(i).Text
                Objetivo(i) = e.Row.Cells(i + 1).Text

                If Anaquel(i) >= Objetivo(i) Then
                    Cumplimiento(i) = Cumplimiento(i) + 1
                End If

                If Anaquel(i) >= Objetivo(i) Then
                    e.Row.Cells(i).BackColor = Drawing.Color.GreenYellow : Else
                    e.Row.Cells(i).BackColor = Drawing.Color.Red : End If

                e.Row.Cells(i).Text = Anaquel(i) & "%"
                e.Row.Cells(i + 1).Text = Objetivo(i) & "%"
            Next i
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.ForeColor = Drawing.Color.Black
            For i = 8 To 20 Step 2
                e.Row.Cells(i).Text = Cumplimiento(i)
            Next i
        End If
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
        form.Controls.Add(Me.gridDetalle)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte PDP Objetivo 2.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class