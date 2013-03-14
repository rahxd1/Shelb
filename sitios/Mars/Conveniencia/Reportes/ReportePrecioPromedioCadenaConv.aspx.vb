Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReportePrecioPromedioCadenaConv
    Inherits System.Web.UI.Page

    Dim Campo(50), Campos, CampoTitulo(50), CamposTitulos As String

    Sub SQLCombo()
        MarsConv.SQLsCombo(cmbPeriodo.SelectedValue, "", _
                           cmbRegion.SelectedValue, cmbPromotor.SelectedValue, _
                            cmbCadena.SelectedValue, "View_Historial_Conv_Pre")
    End Sub

    Sub VerProductos()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * FROM Conv_Productos")
        If tabla.Rows.Count > 0 Then
            For i = 0 To tabla.Rows.Count - 1
                If i = 0 Then
                    Campo(i) = "[" & tabla.Rows(i)("id_producto") & "]" : Else
                    Campo(i) = ",[" & tabla.Rows(i)("id_producto") & "]" : End If

                CampoTitulo(i) = ",'$' + CONVERT(varchar, CONVERT(money, ISNULL([" & tabla.Rows(i)("id_producto") & "],0)), 1) as '" & tabla.Rows(i)("nombre_producto") & "' "

                Campos = Campos + Campo(i)
                CamposTitulos = CamposTitulos + CampoTitulo(i)
            Next i
        End If

        tabla.Dispose()
    End Sub

    Sub CargarReporte()
        Dim PromotorSQL, RegionSQL, CadenaSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("US.id_region", cmbRegion.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("CAD.id_cadena", cmbCadena.SelectedValue)

            VerProductos()

            CargaGrilla(ConexionMars.localSqlServer, _
                        "SELECT DISTINCT nombre_cadena as Cadena " + CamposTitulos + " " & _
                        " FROM (SELECT CAD.nombre_cadena,HDET.id_producto, HDET.precio " & _
                        "From Conv_Historial_Precios as H " & _
                        "INNER JOIN Usuarios as US ON H.id_usuario = US.id_usuario " & _
                        "INNER JOIN Conv_Productos_Historial_Det AS HDET on HDET.folio_historial = H.folio_historial " & _
                        "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = H.id_cadena " & _
                        "WHERE H.orden=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + PromotorSQL + CadenaSQL + ")AS Datos " & _
                        "PIVOT(AVG(precio) FOR id_producto IN (" + Campos + " )) AS PivotTable", _
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Precios por Cadena " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class