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

Partial Public Class ReporteDetalleMarsMay
    Inherits System.Web.UI.Page

    Dim PeriodoActual As String

    Sub SQLCombo()
        MarsMay.SQLsCombo(cmbPeriodo.SelectedValue, cmbQuincena.SelectedValue, _
                       cmbSemana.SelectedValue, cmbRegion.SelectedValue, "", "", "")
    End Sub

    Sub CargarReporte()
        Dim QuincenaSQL, SemanaSQL, RegionSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            QuincenaSQL = Acciones.Slc.cmb("HDET.id_quincena", cmbQuincena.SelectedValue)
            SemanaSQL = Acciones.Slc.cmb("HDET.id_semana", cmbQuincena.SelectedValue)
            RegionSQL = Acciones.Slc.cmb("HDET.id_region", cmbRegion.SelectedValue)

            Dim SQL As String = "select REG.nombre_region,TI.nombre_tipo,HDET.nombre_cadena,HDET.top_rc,HDET.nombre,HDET.id_usuario, " & _
                        "HDET.ciudad, HDET.id_quincena, HDET.id_semana, HDET.id_dia, " & _
                        "CASE WHEN ISNULL([1001],0)=0 then (CASE WHEN ISNULL([101],0)=0 then(CASE WHEN ISNULL([11],0)<>0 then '$'+convert(nvarchar(10),ROUND([1],2))else '' end)else '' end) else '' end[1], " & _
                        "CASE WHEN ISNULL([1001],0)=0 then (CASE WHEN ISNULL([101],0)=0 then(CASE WHEN ISNULL([11],0)<>0 then '$'+convert(nvarchar(10),[11])else '' end)else 'NO CATALOGADO' end) else 'AGOTADO' end[11], " & _
                        "CASE WHEN ISNULL([1001],0)=0 then (CASE WHEN ISNULL([101],0)=0 then(CASE WHEN ISNULL([11],0)<>0 then '$'+convert(nvarchar(10),((ISNULL([1],0)*.01)+ISNULL([1],0)))else '' end)else '' end) else '' end M_1, " & _
                        "CASE WHEN ISNULL([1002],0)=0 then (CASE WHEN ISNULL([102],0)=0 then(CASE WHEN ISNULL([12],0)<>0 then '$'+convert(nvarchar(10),[2])else '' end)else '' end) else '' end[2], " & _
                        "CASE WHEN ISNULL([1002],0)=0 then (CASE WHEN ISNULL([102],0)=0 then(CASE WHEN ISNULL([12],0)<>0 then '$'+convert(nvarchar(10),[12])else '' end)else 'NO CATALOGADO' end) else 'AGOTADO' end[12], " & _
                        "CASE WHEN ISNULL([1002],0)=0 then (CASE WHEN ISNULL([102],0)=0 then(CASE WHEN ISNULL([12],0)<>0 then '$'+convert(nvarchar(10),((ISNULL([2],0)*.01)+ISNULL([2],0)))else '' end)else '' end) else '' end M_2, " & _
                        "CASE WHEN ISNULL([1005],0)=0 then (CASE WHEN ISNULL([105],0)=0 then(CASE WHEN ISNULL([15],0)<>0 then '$'+convert(nvarchar(10),[5])else '' end)else '' end) else '' end[5], " & _
                        "CASE WHEN ISNULL([1005],0)=0 then (CASE WHEN ISNULL([105],0)=0 then(CASE WHEN ISNULL([15],0)<>0 then '$'+convert(nvarchar(10),[15])else '' end)else 'NO CATALOGADO' end) else 'AGOTADO' end[15], " & _
                        "CASE WHEN ISNULL([1005],0)=0 then (CASE WHEN ISNULL([105],0)=0 then(CASE WHEN ISNULL([15],0)<>0 then '$'+convert(nvarchar(10),((ISNULL([5],0)*.01)+ISNULL([5],0)))else '' end)else '' end) else '' end M_5, " & _
                        "CASE WHEN ISNULL([1007],0)=0 then (CASE WHEN ISNULL([107],0)=0 then(CASE WHEN ISNULL([17],0)<>0 then '$'+convert(nvarchar(10),[7])else '' end)else '' end) else '' end[7], " & _
                        "CASE WHEN ISNULL([1007],0)=0 then (CASE WHEN ISNULL([107],0)=0 then(CASE WHEN ISNULL([17],0)<>0 then '$'+convert(nvarchar(10),[17])else '' end)else 'NO CATALOGADO' end) else 'AGOTADO' end[17], " & _
                        "CASE WHEN ISNULL([1007],0)=0 then (CASE WHEN ISNULL([107],0)=0 then(CASE WHEN ISNULL([17],0)<>0 then '$'+convert(nvarchar(10),((ISNULL([7],0)*.01)+ISNULL([7],0)))else '' end)else '' end) else '' end M_7, " & _
                        "(ISNULL([24],0))[24],(ISNULL([25],0))[25],(ISNULL([32],0))[32],(ISNULL([34],0))[34],  " & _
                        "(ISNULL([39],0))[39],(ISNULL([40],0))[40],(ISNULL([46],0))[46],(ISNULL([47],0))[47],(ISNULL([48],0))[48]  " & _
                        "FROM(SELECT folio_historial, orden,id_quincena,id_semana,id_dia,H.id_region,tipo_tienda,id_tienda, " & _
                        "US.ciudad,nombre_cadena, H.nombre,H.top_rc, " & _
                        "id_producto,US.id_usuario,precio " & _
                        "FROM View_Historial_Mayoreo_4fantasticos  as H " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario=H.id_usuario " & _
                        "UNION ALL " & _
                        "SELECT folio_historial, orden,id_quincena,id_semana,id_dia,H.id_region,tipo_tienda,id_tienda, " & _
                        "US.ciudad,nombre_cadena, H.nombre,H.top_rc, " & _
                        "id_producto,US.id_usuario,precio " & _
                        "FROM View_Historial_Productos_4fantasticos_May as H " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario=H.id_usuario " & _
                        "UNION ALL " & _
                        "SELECT folio_historial, orden,id_quincena,id_semana,id_dia,H.id_region,tipo_tienda,id_tienda, " & _
                        "US.ciudad,nombre_cadena, H.nombre,H.top_rc, " & _
                        "id_producto,US.id_usuario,precio " & _
                        "FROM View_Historial_Productos_4fantasticos_PM as H " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario=H.id_usuario)H   " & _
                        "PIVOT(AVG(precio) FOR id_producto    " & _
                        "IN([1],[11],[101],[1001],[10001],[2],[12],[102],[1002],  " & _
                        "[5],[15],[105],[1005],[7],[17],[107],[1007],  " & _
                        "[24],[25],[32],[34],[39],[40],[46],[47],[48]))HDET   " & _
                        "INNER JOIN Regiones as REG ON REG.id_region=HDET.id_region  " & _
                        "INNER JOIN Tipo_Tiendas_Mayoreo as TI ON TI.tipo_tienda=HDET.tipo_tienda   " & _
                        "WHERE HDET.orden=" & cmbPeriodo.SelectedValue & " " & _
                        " " + QuincenaSQL + SemanaSQL + RegionSQL + " " & _
                        "ORDER BY nombre_region,TI.nombre_tipo,ciudad"

            CargaGrilla(ConexionMars.localSqlServer, SQL, gridReporte)
        Else
            gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.PeriodoSQLCmb, "nombre_periodo", "orden", cmbPeriodo)
        End If
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.QuincenaSQLCmb, "nombre_quincena", "id_quincena", cmbQuincena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.SemanaSQLCmb, "nombre_semana", "id_semana", cmbSemana)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)

        CargarReporte()
    End Sub

    Protected Sub cmbQuincena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbQuincena.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.SemanaSQLCmb, "nombre_semana", "id_semana", cmbSemana)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)

        CargarReporte()
    End Sub

    Private Sub cmbSemana_SelectedIndexChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSemana.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_May.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)

        CargarReporte()
    End Sub

    Private Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbRegion.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub lnkExportar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkExportar.Click
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte por plazas " + cmbPeriodo.SelectedItem.ToString() + " " + cmbQuincena.SelectedItem.ToString() + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 10 To 27 Step 5
                If i = 25 Then
                    i = 26 : End If

                Select Case e.Row.Cells(i + 1).Text
                    Case "NO CATALOGADO"
                        e.Row.Cells(i + 1).BackColor = Drawing.Color.Orange
                    Case "AGOTADO"
                        e.Row.Cells(i + 1).BackColor = Drawing.Color.Orange
                    Case Is < e.Row.Cells(i).Text
                        e.Row.Cells(i + 1).BackColor = Drawing.Color.Red
                    Case Is > e.Row.Cells(i + 2).Text
                        e.Row.Cells(i + 1).BackColor = Drawing.Color.Yellow
                    Case Is <> ""
                        If IsNumeric(e.Row.Cells(i + 1).Text) Then
                            e.Row.Cells(i + 1).BackColor = Drawing.Color.Green
                        End If
                End Select
            Next i
        End If
    End Sub
End Class