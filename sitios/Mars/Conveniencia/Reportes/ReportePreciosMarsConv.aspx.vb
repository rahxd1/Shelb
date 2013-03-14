
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

Partial Public Class ReportePreciosMarsConv
    Inherits System.Web.UI.Page

    Dim PeriodoSQL, PromotorSQL, RegionSQL, CadenaSQL As String
    Dim PeriodoActual As String

    Sub SQLCombo()
        MarsConv.SQLsCombo(cmbPeriodo.SelectedValue, "", _
                           cmbRegion.SelectedValue, cmbPromotor.SelectedValue, _
                            cmbCadena.SelectedValue, "View_Historial_Conv_Pre")
    End Sub

    Sub CargarReporte()
        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("TI.id_cadena", cmbCadena.SelectedValue)

            CargaGrilla(ConexionMars.localSqlServer, _
                        "SELECT PROD.nombre_producto, Q1.precio as PrecioQ1, Q2.precio as PrecioQ2, ((Q1.precio+Q2.precio)/2)PrecioMes " & _
                        "FROM Conv_Productos as PROD " & _
                        "FULL JOIN (select HDET.id_producto, Round(AVG(HDET.precio),2) as precio " & _
                        "FROM Conv_Historial_Precios as H " & _
                        "INNER JOIN Conv_Productos_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial " & _
                        "WHERE H.orden=" & cmbPeriodo.SelectedValue & " AND H.quincena='q1' " & _
                        "GROUP BY HDET.id_producto) as Q1 ON Q1.id_producto = PROD.id_producto " & _
                        "FULL JOIN (select HDET.id_producto, Round(AVG(HDET.precio),2) as precio " & _
                        "FROM Conv_Historial_Precios as H " & _
                        "INNER JOIN Conv_Productos_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial " & _
                        "WHERE H.orden=" & cmbPeriodo.SelectedValue & " AND H.quincena='q2' " & _
                        "GROUP BY HDET.id_producto) as Q2 ON Q2.id_producto = PROD.id_producto " & _
                        "WHERE PROD.activo=1 " & _
                        " " + RegionSQL + PromotorSQL + CadenaSQL + " " & _
                        "ORDER BY PROD.nombre_producto", gridReporte)
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
            Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_Conv.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        End If
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_Conv.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_Conv.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_Conv.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_Conv.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_Conv.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, Mars_Conv.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCadena.SelectedIndexChanged
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Precios " + cmbPeriodo.SelectedItem.ToString() + " " + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class