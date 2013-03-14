Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteFrentesFerrero
    Inherits System.Web.UI.Page

    Dim NombreProducto, IDProducto As String
    Dim Campo, Campos, NombreCampo, NombreCampos As String
    Dim Suma(73) As Integer

    Sub SQLCombo()
        Ferrero.SQLsComboAS(cmbPeriodo.SelectedValue, cmbRegion.SelectedValue, _
                            cmbPromotor.SelectedValue, cmbCadena.SelectedValue)
    End Sub

    Sub CargarReporte()
        If cmbPeriodo.SelectedValue <> "" Then
            Dim PromotorSQL, RegionSQL, CadenaSQL As String

            RegionSQL = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("H.id_cadena", cmbCadena.SelectedValue)

            Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                        "SELECT * FROM AS_Productos as PROD " & _
                                        "INNER JOIN AS_Categoria as CAT ON CAT.id_categoria = PROD.tipo_producto " & _
                                        "ORDER BY id_marca, tipo_producto, nombre_producto")
            If tabla.Rows.Count > 0 Then
                For i = 0 To tabla.Rows.Count - 1
                    IDProducto = tabla.Rows(i)("id_producto")
                    NombreProducto = tabla.Rows(i)("nombre_producto") + " " + tabla.Rows(i)("presentacion") + " (" + tabla.Rows(i)("nombre_categoria") + ")"

                    If i = 0 Then
                        Campo = "[" + IDProducto + "]" : Else
                        Campo = ",[" + IDProducto + "]" : End If

                    NombreCampo = ",[" + IDProducto + "]as '" & NombreProducto & "'"

                    Campos = Campos + Campo
                    NombreCampos = NombreCampos + NombreCampo
                Next i
            End If

            Tabla.Dispose()

            CargaGrilla(ConexionFerrero.localSqlServer, _
                        "SELECT DISTINCT nombre_region as 'Región',id_usuario as 'Ejecutivo',nombre_cadena as 'Cadena', nombre_tienda as 'Tienda' " & _
                        " " + NombreCampos + " " & _
                        "FROM(SELECT DISTINCT REG.nombre_region, H.id_usuario, CAD.nombre_cadena, H.nombre_tienda, " & _
                        "HDET.id_producto,HDET.frentes " & _
                        "From AS_Historial as H  " & _
                        "FULL JOIN AS_Productos_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial " & _
                        "INNER JOIN AS_Productos as PROD ON PROD.id_producto = HDET.id_producto " & _
                        "INNER JOIN Marcas as MAR ON MAR.id_marca = PROD.id_marca " & _
                        "INNER JOIN AS_Categoria AS CAT ON CAT.id_categoria = PROD.tipo_producto " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario = H.id_usuario " & _
                        "INNER JOIN Regiones as REG ON REG.id_region = US.id_region AND REG.id_proyecto=27 " & _
                        "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = H.id_cadena " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " AND frentes <> 0" & _
                        " " + RegionSQL + " " & _
                        " " + PromotorSQL + " " & _
                        " " + CadenaSQL + ") AS Datos PIVOT(SUM(frentes) " & _
                        "FOR id_producto IN (" + Campos + ")) AS PivotTable " & _
                        "ORDER BY nombre_region,nombre_cadena,nombre_tienda", Me.gridReporte)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.PeriodoSQLCmb, "nombre_periodo", "id_periodo", cmbPeriodo)
            LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
            LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

            CargarReporte()
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()

        LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        LlenaDrop(ConexionFerrero.localSqlServer, FerreroVar.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()
        CargarReporte()
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCadena.SelectedIndexChanged
        SQLCombo()
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Frentes " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 4 To 76
                If e.Row.Cells(i).Text <> "&nbsp;" Then
                    Suma(i - 4) = Suma(i - 4) + e.Row.Cells(i).Text
                End If
            Next i
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            For i = 4 To 77
                e.Row.Cells(i).Text = Suma(i - 4)
            Next i
        End If
    End Sub

End Class