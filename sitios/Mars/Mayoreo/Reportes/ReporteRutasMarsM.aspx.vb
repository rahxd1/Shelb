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

Partial Public Class ReporteRutasMarsM
    Inherits System.Web.UI.Page

    Dim RegionSel, EjecutivoSel As String
    Dim RegionSQL, EjecutivoSQL, PromotorSQL As String

    Sub SQLCombo()
        RegionSel = Acciones.Slc.cmb("US.id_region", cmbRegion.SelectedValue)
        EjecutivoSel = Acciones.Slc.cmb("REL.ruta_ejecutivo", cmbEjecutivo.SelectedValue)

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Mayoreo_CatRutas AS RE " & _
                    "INNER JOIN Mayoreo_Tiendas AS TI ON TI.id_tienda = RE.id_tienda " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    "WHERE REG.id_region <>0  " & _
                    " ORDER BY REG.nombre_region"

        EjecutivoSQL = "SELECT DISTINCT REL.ruta_ejecutivo, US.nombre " & _
                    "FROM Mayoreo_CatRutas AS RE  " & _
                    "INNER JOIN Mayoreo_Tiendas AS TI ON TI.id_tienda = RE.id_tienda  " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region  " & _
                    "INNER JOIN Usuarios_Relacion_Mayoreo as REL ON REL.id_usuario=RE.id_usuario " & _
                    "INNER JOIN Usuarios as US ON REL.ruta_ejecutivo=US.id_usuario " & _
                    "WHERE REL.ruta_ejecutivo <>''   " & _
                    " " + RegionSel + " " & _
                    "ORDER BY US.nombre "

        PromotorSQL = "SELECT DISTINCT RE.id_usuario " & _
                    "FROM Mayoreo_CatRutas AS RE  " & _
                    "INNER JOIN Mayoreo_Tiendas AS TI ON TI.id_tienda = RE.id_tienda  " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region  " & _
                    "INNER JOIN Usuarios_Relacion_Mayoreo as REL ON REL.id_usuario=RE.id_usuario " & _
                    "INNER JOIN Usuarios as US ON REL.ruta_ejecutivo=US.id_usuario " & _
                    "WHERE REL.ruta_ejecutivo <>''   " & _
                    " " + RegionSel + EjecutivoSel + " " & _
                    "ORDER BY RE.id_usuario "
    End Sub

    Sub CargarReporte()
        RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
        EjecutivoSQL = Acciones.Slc.cmb("REL.ruta_ejecutivo", cmbEjecutivo.SelectedValue)
        PromotorSQL = Acciones.Slc.cmb("RUT.id_usuario", cmbPromotor.SelectedValue)

        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT DISTINCT REG.nombre_region, RUT.id_usuario, TI.codigo,CAD.nombre_cadena, TI.nombre,TI.ciudad, " & _
                    "ES.nombre_estado, TTI.nombre_tipo,RUT.id_tienda,TR.nombre_top_rc,REL.ruta_ejecutivo, " & _
                    "CASE WHEN RUT.W1_1=1 then 1 else 0 end +CASE WHEN RUT.W1_2=1 then 1 else 0 end +" & _
                    "CASE WHEN RUT.W1_3=1 then 1 else 0 end+CASE WHEN RUT.W1_4=1 then 1 else 0 end +" & _
                    "CASE WHEN RUT.W1_5=1 then 1 else 0 end+CASE WHEN RUT.W1_6=1 then 1 else 0 end +" & _
                    "CASE WHEN RUT.W1_7=1 then 1 else 0 end+" & _
                    "CASE WHEN RUT.W2_1=1 then 1 else 0 end+CASE WHEN RUT.W2_2=1 then 1 else 0 end +" & _
                    "CASE WHEN RUT.W2_3=1 then 1 else 0 end+CASE WHEN RUT.W2_4=1 then 1 else 0 end +" & _
                    "CASE WHEN RUT.W2_5=1 then 1 else 0 end+CASE WHEN RUT.W2_6=1 then 1 else 0 end +" & _
                    "CASE WHEN RUT.W2_7=1 then 1 else 0 end+" & _
                    "CASE WHEN RUT.W3_1=1 then 1 else 0 end+CASE WHEN RUT.W3_2=1 then 1 else 0 end +" & _
                    "CASE WHEN RUT.W3_3=1 then 1 else 0 end+CASE WHEN RUT.W3_4=1 then 1 else 0 end +" & _
                    "CASE WHEN RUT.W3_5=1 then 1 else 0 end+CASE WHEN RUT.W3_6=1 then 1 else 0 end +" & _
                    "CASE WHEN RUT.W3_7=1 then 1 else 0 end+" & _
                    "CASE WHEN RUT.W4_1=1 then 1 else 0 end+CASE WHEN RUT.W4_2=1 then 1 else 0 end +" & _
                    "CASE WHEN RUT.W4_3=1 then 1 else 0 end+CASE WHEN RUT.W4_4=1 then 1 else 0 end +" & _
                    "CASE WHEN RUT.W4_5=1 then 1 else 0 end+CASE WHEN RUT.W4_6=1 then 1 else 0 end +" & _
                    "CASE WHEN RUT.W4_7=1 then 1 else 0 end Frecuencia," & _
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
                    "From Mayoreo_Tiendas as TI " & _
                    "INNER JOIN Mayoreo_CatRutas as RUT ON RUT.id_tienda = TI.id_tienda " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    "INNER JOIN Cadenas_Mayoreo as CAD ON CAD.id_cadena = TI.id_cadena " & _
                    "INNER JOIN Estados as ES ON TI.id_estado = ES.id_estado " & _
                    "FULL JOIN Tipo_Tiendas_Mayoreo as TTI ON TTI.tipo_tienda = TI.tipo_tienda " & _
                    "INNER JOIN Top_rc as TR ON TR.top_rc=TI.top_rc " & _
                    "INNER JOIN Usuarios_Relacion_Mayoreo as REL ON REL.id_usuario=RUT.id_usuario " & _
                    "WHERE TI.id_tienda<>0 " & _
                    " " + RegionSQL + " " & _
                    " " + EjecutivoSQL + " " & _
                    " " + PromotorSQL + " " & _
                    "ORDER BY REG.nombre_region, CAD.nombre_cadena, Ti.nombre", gridReporte)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, EjecutivoSQL, "nombre", "ruta_ejecutivo", cmbEjecutivo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

            CargarReporte()
        End If
    End Sub

    Protected Sub cmbEjecutivo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbEjecutivo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, EjecutivoSQL, "nombre", "ruta_ejecutivo", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Rutas.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 12 To 18
                e.Row.Cells(i).BackColor = Drawing.Color.LightGoldenrodYellow : Next i
            For i = 19 To 25
                e.Row.Cells(i).BackColor = Drawing.Color.CadetBlue : Next i
            For i = 26 To 32
                e.Row.Cells(i).BackColor = Drawing.Color.CornflowerBlue : Next i
            For i = 33 To 39
                e.Row.Cells(i).BackColor = Drawing.Color.DeepPink : Next i
        End If

        Select Case e.Row.RowType
            Case DataControlRowType.Header
                For i = 0 To 12
                    e.Row.Cells(i).Visible = True
                Next i

                For i = 12 To 35 Step 7
                    e.Row.Cells(i).ColumnSpan = 7
                    e.Row.Cells(i).Controls.Clear()
                Next i

                For i = 13 To 18
                    e.Row.Cells(i).Visible = False : Next i
                For i = 20 To 25
                    e.Row.Cells(i).Visible = False : Next i
                For i = 27 To 32
                    e.Row.Cells(i).Visible = False : Next i
                For i = 34 To 39
                    e.Row.Cells(i).Visible = False : Next i

                e.Row.Cells(12).Text = Tabla("Semana 1")
                e.Row.Cells(19).Text = Tabla("Semana 2")
                e.Row.Cells(26).Text = Tabla("Semana 3")
                e.Row.Cells(33).Text = Tabla("Semana 4")
        End Select
    End Sub

    Private Function Tabla(ByVal Titulo As String) As String
        If Titulo <> "" Then
            Return "<table align='center' style='width: 168px; font-weight: 700;'>" & _
                          "<tr><td colspan='7' style='text-align: center;font-weight: bold''>" & Titulo & "</td></tr>" & _
                          "<tr><td style='font-weight: bold; width: 24px; text-align: center;'>D</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>L</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>M</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>X</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>J</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>V</td>" & _
                          "<td style='font-weight: bold; width: 24px; text-align: center;'>S</td>" & _
                          "</tr></table>"
        Else
            Return ""
        End If
    End Function
End Class