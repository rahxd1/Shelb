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

Partial Public Class ReporteBonosMarsConv
    Inherits System.Web.UI.Page

    Sub SQLCombo()
        MarsConv.SQLsCombo(cmbPeriodo.SelectedValue, "", _
                        cmbRegion.SelectedValue, cmbPromotor.SelectedValue, _
                        "", "View_Historial_Conv_Exh")
    End Sub

    Sub CargarReporte()
        Dim PromotorSQL, RegionSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("US.id_region", cmbRegion.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("RE.id_usuario", cmbPromotor.SelectedValue)

            CargaGrilla(ConexionMars.localSqlServer, _
                        "select RE.id_usuario,(RE.id_tienda)tiendas,Precios.Cadenas," & _
                        "ISNULL(((ISNULL(100*CapExh.Capturas/(RE.id_tienda),0)+100*Precios.Capturas/Precios.Cadenas)/2),0)Capturas, " & _
                        "ISNULL((H.material_pop),0)MaterialPop,ISNULL(ROUND(100*(H.material_pop)/(RE.id_tienda),2),0)ObjMaterialPop, " & _
                        "ISNULL((HDET2.[1]),0)Centro, " & _
                        "ISNULL((HDET2.[2]),0)Cesar,ISNULL(ROUND(100*(HDET2.[2])/(RE.id_tienda),2),0)ObjCesar, " & _
                        "ISNULL((HDET2.[3]),0)Plastival,ISNULL(ROUND(100*(HDET2.[3])/(RE.id_tienda),2),0)ObjPlastival, " & _
                        "ISNULL((HDET2.[4]),0)Tiras,ISNULL(ROUND(100*(HDET2.[4])/(RE.id_tienda),2),0)ObjTiras, " & _
                        "ISNULL((HDET2.[5]),0)Wet,ISNULL(ROUND(100*(HDET2.[5])/(RE.id_tienda),2),0)ObjWet " & _
                        "FROM (select RE.id_usuario,COUNT(id_tienda)id_tienda " & _
                        "FROM Conv_Rutas_Eventos_Exhibiciones as RE " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario=RE.id_usuario " & _
                        "WHERE orden=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + PromotorSQL + " " & _
                        "GROUP BY RE.id_usuario) as RE " & _
                        "LEFT JOIN (select id_usuario,SUM(estatus_exhibiciones)Capturas " & _
                        "FROM Conv_Rutas_Eventos_Exhibiciones " & _
                        "WHERE orden=" & cmbPeriodo.SelectedValue & " AND estatus_exhibiciones=1 GROUP BY id_usuario)CapExh " & _
                        "ON CapExh.id_usuario = RE.id_usuario " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario= RE.id_usuario " & _
                        "INNER JOIN Regiones as REG ON REG.id_region = US.id_region " & _
                        "LEFT JOIN (select id_usuario, SUM(material_pop)material_pop " & _
                        "FROM Conv_Historial_Exhibiciones " & _
                        "WHERE orden=" & cmbPeriodo.SelectedValue & " GROUP BY id_usuario)H " & _
                        "ON RE.id_usuario=H.id_usuario " & _
                        "LEFT JOIN(select id_usuario,ISNULL([1],0)[1],ISNULL([2],0)[2],ISNULL([3],0)[3],ISNULL([4],0)[4],ISNULL([5] ,0)[5] " & _
                        "FROM (SELECT H.id_usuario, HDET.id_exhibidor, SUM(HDET.cantidad)cantidad " & _
                        "FROM Conv_Historial_Exhibiciones as H " & _
                        "INNER JOIN Conv_Exhibiciones_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial " & _
                        "WHERE H.orden=" & cmbPeriodo.SelectedValue & " " & _
                        "GROUP BY H.id_usuario, HDET.id_exhibidor)Tabla " & _
                        "PIVOT(SUM(cantidad)FOR id_exhibidor IN ([1], [2], [3], [4], [5])) AS PivotTable)as HDET2  " & _
                        "ON H.id_usuario = HDET2.id_usuario " & _
                        "LEFT JOIN (select id_usuario, count(id_cadena)Cadenas, sum(estatus_precio)Capturas " & _
                        "FROM Conv_Rutas_Eventos_Precios " & _
                        "where orden=" & cmbPeriodo.SelectedValue & " group by id_usuario)as Precios ON RE.id_usuario=Precios.id_usuario " & _
                        "ORDER BY RE.id_usuario", _
                        gridReporte)
        Else
            gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_Conv.PeriodoSQLCmb, "nombre_periodo", "orden", cmbPeriodo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_Conv.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_Conv.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        End If
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_Conv.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Bonos.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Total As Double = e.Row.Cells(2).Text
            e.Row.Cells(2).Text = Total & "%"
            Total = e.Row.Cells(4).Text
            e.Row.Cells(4).Text = Total & "%"
            Total = e.Row.Cells(6).Text
            e.Row.Cells(6).Text = Total & "%"
            Total = e.Row.Cells(8).Text
            e.Row.Cells(8).Text = Total & "%"
            Total = e.Row.Cells(10).Text
            e.Row.Cells(10).Text = Total & "%"
            Total = e.Row.Cells(12).Text
            e.Row.Cells(12).Text = Total & "%"
        End If
    End Sub
End Class