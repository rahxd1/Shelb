Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteAgotamientosCadenaNM
    Inherits System.Web.UI.Page

    Dim RegionSel As String
    Dim PeriodoSQL, PeriodoSQL2, PromotorSQL, RegionSQL As String
    Dim NombreProducto, IDProducto As String
    Dim Campo, Campos, NombreCampo, NombreCampos As String
    Dim Dato(80), Suma(80) As Integer

    Sub SQLCombo()
        RegionSel = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)

        PeriodoSQL = "SELECT id_periodo, nombre_periodo " & _
                         "FROM NewMix_Periodos " & _
                         " ORDER BY id_periodo DESC"

        PeriodoSQL2 = "SELECT id_periodo, nombre_periodo " & _
                         "FROM NewMix_Periodos " & _
                         " ORDER BY id_periodo DESC"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Regiones as REG " & _
                    "WHERE REG.id_region<>0 ORDER BY REG.nombre_region"

        PromotorSQL = "SELECT DISTINCT id_usuario " & _
                    "FROM NewMix_Historial as RE " & _
                    "INNER JOIN NewMix_Tiendas as TI ON RE.id_tienda = TI.id_tienda " & _
                    "WHERE id_usuario<>'' " & _
                    " " + RegionSel + " " & _
                    " ORDER BY id_usuario "
    End Sub

    Sub CargarReporte()
        If cmbPeriodo1.SelectedValue <> "" Then
            If cmbPeriodo1.SelectedValue = "" Then
                Exit Sub : End If

            If cmbPeriodo2.SelectedValue = "" Then
                PeriodoSQL = " AND H.id_periodo=" & cmbPeriodo1.SelectedValue & " "
                PeriodoSQL2 = "" : Else
                PeriodoSQL = " AND H.id_periodo >= " & cmbPeriodo1.SelectedValue & ""
                PeriodoSQL2 = " AND H.id_periodo <= " & cmbPeriodo2.SelectedValue & "" : End If

            RegionSQL = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)

            Dim Tabla As DataTable = Tablas.Ver.DT(ConexionHerradura.localSqlServer, _
                                                   "SELECT * FROM NewMix_Productos as PROD " & _
                                                    "WHERE tipo_producto=1 AND activo=1 " & _
                                                    "ORDER BY id_producto")
            If Tabla.Rows.Count > 0 Then
                For i = 0 To Tabla.Rows.Count - 1
                    IDProducto = Tabla.Rows(i)("id_producto")
                    NombreProducto = Tabla.Rows(i)("nombre_producto")

                    If i = 0 Then
                        Campo = "[" + IDProducto + "]" : Else
                        Campo = ",[" + IDProducto + "]" : End If

                    NombreCampo = ",[" + IDProducto + "]as '" & NombreProducto & "'"

                    Campos = Campos + Campo
                    NombreCampos = NombreCampos + NombreCampo
                Next i
            End If

            Tabla.Dispose()

            CargaGrilla(ConexionHerradura.localSqlServer, _
                        "SELECT DISTINCT nombre_cadena as 'Cadena' " & _
                        " " + NombreCampos + " " & _
                        "FROM(SELECT DISTINCT TI.nombre_cadena, H.id_producto,COUNT(H.id_producto)as agotado " & _
                        "From View_Historial_NM_Agotados as H  " & _
                        "INNER JOIN View_Tiendas_NM as TI ON TI.id_tienda = H.id_tienda " & _
                        "WHERE H.agotado <>0 " & _
                        " " + PeriodoSQL + " " & _
                        " " + PeriodoSQL2 + " " & _
                        " " + RegionSQL + " " & _
                        " " + PromotorSQL + " GROUP BY TI.nombre_cadena, H.id_producto)AS Productos PIVOT(SUM(agotado) " & _
                        "FOR id_producto IN (" + Campos + ")) AS PivotTable " & _
                        "ORDER BY nombre_cadena", Me.gridReporte)
        Else
            Me.gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionHerradura.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo1)
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, PeriodoSQL2, "nombre_periodo", "id_periodo", cmbPeriodo2)
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionHerradura.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo1.SelectedIndexChanged
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Agotamientos por Cadena " + cmbPeriodo1.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        Dim Columnas As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionHerradura.localSqlServer, _
                                               "SELECT * FROM NewMix_Productos " & _
                                               "WHERE tipo_producto=1 AND activo=1")
        Columnas = Tabla.Rows.Count

        Tabla.Dispose()

        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 1 To Columnas
                If e.Row.Cells(i).Text <> "&nbsp;" Then
                    Dato(i) = e.Row.Cells(i).Text
                    Suma(i) = Suma(i) + Dato(i)
                End If
            Next i
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            For i = 1 To Columnas
                e.Row.Cells(i).Text = Suma(i)
            Next i
        End If
    End Sub

    Private Sub cmbPeriodo2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPeriodo2.SelectedIndexChanged
        SQLCombo()
        CargarReporte()
    End Sub
End Class