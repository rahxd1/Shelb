Imports System.Data.SqlClient

Partial Public Class ReportePreciosPromedioSYM
    Inherits System.Web.UI.Page

    Dim PeriodoSel, RegionSel, PeriodoSQL, RegionSQL, PlazasSQL, SeleccionCampos, SeleccionColumna As String
    Dim ProductoPropio, GramosPropio As Double
    Dim Campos(100), Columnas(100) As String
    Dim ColumnasGrilla As Integer
    Dim PrecioGramoSYM, PrecioGramo As Double

    Sub SQLCombo()
        PeriodoSel = Acciones.Slc.cmb("H.id_periodo", cmbPeriodo.SelectedValue)
        RegionSel = Acciones.Slc.cmb("US.id_region", cmbRegion.SelectedValue)

        PeriodoSQL = "SELECT id_periodo, nombre_periodo " & _
                         "FROM Precios_Periodos " & _
                         " ORDER BY fecha_inicio DESC"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Precios_Historial as H " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = H.id_cadena " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario = H.id_usuario " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = US.id_region " & _
                    "WHERE REG.id_region<>0 " & _
                    " " + PeriodoSel + " " & _
                    " ORDER BY REG.nombre_region"

        PlazasSQL = "SELECT DISTINCT US.plaza " & _
                    "FROM Usuarios as US " & _
                    "INNER JOIN Precios_Historial as H ON H.id_usuario=US.id_usuario " & _
                    "WHERE US.plaza<>'' " & _
                    " " + PeriodoSel + RegionSel + " " & _
                    " ORDER BY US.plaza"

    End Sub

    Public Function SeleccionCadenas(ByVal Grupo As Integer) As Integer
        RegionSQL = Acciones.Slc.cmb("US.id_region", cmbRegion.SelectedValue)
        PlazasSQL = Acciones.Slc.cmb("US.plaza", cmbCiudad.SelectedValue)

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT DISTINCT CAD.id_cadena, CAD.nombre_cadena " & _
                                      "FROM Precios_Historial as H " & _
                                      "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena= H.id_cadena " & _
                                      "INNER JOIN Usuarios as US ON US.id_usuario = H.id_usuario  " & _
                                      "INNER JOIN Regiones as REG ON REG.id_region = US.id_region  " & _
                                      "WHERE id_periodo= " & cmbPeriodo.SelectedValue & " " & _
                                      " " + RegionSQL + PlazasSQL + " ")
        If Tabla.Rows.Count > 1 Then
            For i = 0 To Tabla.Rows.Count - 1
                If i = 0 Then
                    Campos(0) = "[" & Tabla.Rows(0)("id_cadena") & "]" : Else
                    Campos(i) = ",[" & Tabla.Rows(i)("id_cadena") & "]" : End If
            Next i

            For i = 0 To Tabla.Rows.Count - 1
                If i = 0 Then
                    Columnas(0) = "'$'+CONVERT(NVARCHAR(10),CAST(ISNULL([" & Tabla.Rows(0)("id_cadena") & "],0)as decimal(9,2)))'" & Tabla.Rows(0)("nombre_cadena") & "'" : Else
                    Columnas(i) = ",'$'+CONVERT(NVARCHAR(10),CAST(ISNULL([" & Tabla.Rows(i)("id_cadena") & "],0)as decimal(9,2)))'" & Tabla.Rows(i)("nombre_cadena") & "'" : End If
            Next i

            For i = 0 To Tabla.Rows.Count - 1
                SeleccionCampos = SeleccionCampos + Campos(i)
                SeleccionColumna = SeleccionColumna + Columnas(i)
            Next i
        End If

        Tabla.Dispose()
    End Function

    Public Function VerGrilla(ByVal Grilla As GridView, ByVal TipoGrupo As Integer) As Integer
        Dim SQL As String
        RegionSQL = Acciones.Slc.cmb("US.id_region", cmbRegion.SelectedValue)
        PlazasSQL = Acciones.Slc.cmb("US.plaza", cmbCiudad.SelectedValue)

        SeleccionCampos = ""
        SeleccionColumna = ""
        SeleccionCadenas(TipoGrupo)

        If SeleccionColumna = "" Then
            SQL = ""
            Grilla.Visible = False
            Exit Function
        Else
            SQL = "SELECT PROD.nombre_producto as Producto,PROD.presentacion as 'Presentación', " & _
                    " " + SeleccionColumna + ", " & _
                    "'$'+CONVERT(NVARCHAR(10),CAST((ISNULL(PROM.PromedioGeneral,0))as decimal(9,2)))'Promedio General', " & _
                    "CASE WHEN (PROD.tipo_producto=1) THEN '' " & _
                    "ELSE '$'+CONVERT(NVARCHAR(10),CAST(ROUND(ISNULL((PROM.PromedioGeneral-DIF.precio),0),4)as decimal(9,3))) end AS 'Dif. Precio', " & _
                    "CASE WHEN (PROD.tipo_producto=1) THEN '' " & _
                    "ELSE CONVERT(NVARCHAR(10),CAST(ROUND(ISNULL((100*(PROM.PromedioGeneral-DIF.precio))/DIF.precio,0),2)as decimal(9,2)))+'%' end AS 'Dif. %', " & _
                    "'$'+CONVERT(NVARCHAR(10),CAST(ROUND(ISNULL((PROM.PromedioGeneral/PROD.presentacion),0),4)as decimal(9,4)))'Precio por gramo', " & _
                    "CASE WHEN (PROD.tipo_producto=1) THEN '' " & _
                    "ELSE '$'+CONVERT(NVARCHAR(10),CAST(ROUND(ISNULL(((PROM.PromedioGeneral/PROD.presentacion)-DIF.PrecioGramo),0),4)as decimal(9,4))) end AS 'Dif. Precio gramo', " & _
                    "CASE WHEN (PROD.tipo_producto=1) THEN '' " & _
                    "else CONVERT(NVARCHAR(10),ISNULL(CAST(ROUND((100*cast(ROUND(((PROM.PromedioGeneral/PROD.presentacion)-DIF.PrecioGramo),4)as decimal(9,4)))/ " & _
                    "cast(ROUND(DIF.PrecioGramo,4)as decimal(9,4)),2)as decimal(9,2)),0))+'%' end AS 'Dif. % gramo' " & _
                    "FROM Productos as PROD " & _
                    "INNER JOIN Grupos as TIPO ON TIPO.tipo_grupo= PROD.tipo_grupo " & _
                    "FULL JOIN(SELECT HDET.id_producto,HDET.id_periodo, HDET.id_cadena, HDET.precio " & _
                    "FROM Precios_Hdet as HDET  " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario = HDET.id_usuario  " & _
                    "WHERE HDET.tipo_grupo=" & TipoGrupo & " AND HDET.precio <>0 AND HDET.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + PlazasSQL + ")Productos " & _
                    "PIVOT (AVG(precio) FOR id_cadena IN (" + SeleccionCampos + ")) AS H ON PROD.id_producto = H.id_producto " & _
                    "FULL JOIN (SELECT id_producto, AVG(precio)PromedioGeneral " & _
                    "FROM(SELECT HDET.id_producto,avg(HDET.precio)precio " & _
                    "FROM Precios_Hdet as HDET  " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario = HDET.id_usuario  " & _
                    "WHERE HDET.tipo_grupo=" & TipoGrupo & " AND HDET.precio <>0 and HDET.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + PlazasSQL + " " & _
                    "GROUP BY HDET.id_producto)as PROD " & _
                    "GROUP BY id_producto)as PROM ON PROM.id_producto = PROD.id_producto " & _
                    "FULL JOIN (SELECT PROD.grupo_comparativo,AVG(precio)precio,AVG(PROD.PrecioGramo)PrecioGramo  " & _
                    "FROM(SELECT HDET.id_producto,HDET.grupo_comparativo,HDET.presentacion, " & _
                    "AVG(HDET.precio)precio,(AVG(precio)/HDET.presentacion)PrecioGramo " & _
                    "FROM Precios_Hdet as HDET  " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario = HDET.id_usuario " & _
                    "WHERE HDET.tipo_grupo=" & TipoGrupo & " AND HDET.precio <>0 and HDET.id_periodo=" & cmbPeriodo.SelectedValue & " and HDET.tipo_producto=1 " & _
                    " " + RegionSQL + PlazasSQL + " " & _
                    "GROUP BY HDET.grupo_comparativo,HDET.id_producto,HDET.presentacion)as PROD " & _
                    "GROUP BY PROD.grupo_comparativo)as DIF ON DIF.grupo_comparativo = PROD.grupo_comparativo " & _
                    "WHERE PROD.tipo_grupo=" & TipoGrupo & " AND PROD.orden<>0 ORDER BY PROD.orden"

            CargaGrilla(ConexionSYM.localSqlServer, SQL, Grilla)
        End If


    End Function

    Sub CargarReporte()
        If cmbPeriodo.SelectedValue <> "" Then
            VerGrilla(gridReporte, 104)
            VerGrilla(gridReporte2, 103)
            VerGrilla(gridReporte3, 100)
            VerGrilla(gridReporte4, 102)
            VerGrilla(gridReporte5, 101)
            VerGrilla(gridReporte6, 105)
            VerGrilla(gridReporte7, 108)
            VerGrilla(gridReporte8, 109)
        Else
            gridReporte.Visible = False
            gridReporte2.Visible = False
            gridReporte3.Visible = False
            gridReporte4.Visible = False
            gridReporte5.Visible = False
            gridReporte6.Visible = False
            gridReporte7.Visible = False
            gridReporte8.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionSYM.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, PlazasSQL, "plaza", "plaza", cmbCiudad)
        End If
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, PlazasSQL, "plaza", "plaza", cmbCiudad)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionSYM.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, PlazasSQL, "plaza", "plaza", cmbCiudad)

        CargarReporte()
    End Sub

    Protected Sub lnkExportar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.PnlGrid.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.PnlGrid)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte cadena por linea " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbPlazas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCiudad.SelectedIndexChanged
        CargarReporte()
    End Sub

    Sub CantidadColumnas()
        RegionSQL = Acciones.Slc.cmb("US.id_region", cmbRegion.SelectedValue)
        PlazasSQL = Acciones.Slc.cmb("US.plaza", cmbCiudad.SelectedValue)

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT DISTINCT CAD.id_cadena, CAD.nombre_cadena " & _
                                      "FROM Precios_Historial as H " & _
                                      "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena= H.id_cadena " & _
                                      "INNER JOIN Usuarios_Precios as US ON US.id_usuario = H.id_usuario " & _
                                      "WHERE id_periodo= " & cmbPeriodo.SelectedValue & " " & _
                                      " " + RegionSQL + PlazasSQL + " ")
        ColumnasGrilla = Tabla.Rows.Count - 1

        Tabla.Dispose()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        CantidadColumnas()
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 2 To (4 + ColumnasGrilla)
                If e.Row.Cells(i).Text = "&nbsp;" Then
                    For C = 0 To (8 + ColumnasGrilla)
                        e.Row.Cells(C).BackColor = Drawing.Color.LemonChiffon : Next C
                End If
            Next i

            If e.Row.Cells(5 + ColumnasGrilla).Text = "&nbsp;" Then
                PrecioGramoSYM = e.Row.Cells(6 + ColumnasGrilla).Text
            End If

            If e.Row.Cells(3 + ColumnasGrilla).Text = "$0.00" Then
                e.Row.Cells(3 + ColumnasGrilla).Text = ""
                e.Row.Cells(4 + ColumnasGrilla).Text = ""
                e.Row.Cells(5 + ColumnasGrilla).Text = ""
                e.Row.Cells(6 + ColumnasGrilla).Text = ""
                e.Row.Cells(7 + ColumnasGrilla).Text = ""
                e.Row.Cells(8 + ColumnasGrilla).Text = ""
            Else
                PrecioGramo = e.Row.Cells(6 + ColumnasGrilla).Text

                If PrecioGramo < PrecioGramoSYM Then
                    For C = (4 + ColumnasGrilla) To (8 + ColumnasGrilla)
                        e.Row.Cells(C).ForeColor = Drawing.Color.Red
                    Next C
                End If
            End If
        End If
    End Sub

    Private Sub gridReporte2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte2.RowDataBound
        CantidadColumnas()
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 2 To (4 + ColumnasGrilla)
                If e.Row.Cells(i).Text = "&nbsp;" Then
                    For C = 0 To (8 + ColumnasGrilla)
                        e.Row.Cells(C).BackColor = Drawing.Color.LemonChiffon : Next C
                End If
            Next i

            If e.Row.Cells(5 + ColumnasGrilla).Text = "&nbsp;" Then
                PrecioGramoSYM = e.Row.Cells(6 + ColumnasGrilla).Text
            End If

            If e.Row.Cells(3 + ColumnasGrilla).Text = "$0.00" Then
                e.Row.Cells(3 + ColumnasGrilla).Text = ""
                e.Row.Cells(4 + ColumnasGrilla).Text = ""
                e.Row.Cells(5 + ColumnasGrilla).Text = ""
                e.Row.Cells(6 + ColumnasGrilla).Text = ""
                e.Row.Cells(7 + ColumnasGrilla).Text = ""
                e.Row.Cells(8 + ColumnasGrilla).Text = ""
            Else
                PrecioGramo = e.Row.Cells(6 + ColumnasGrilla).Text

                If PrecioGramo < PrecioGramoSYM Then
                    For C = (4 + ColumnasGrilla) To (8 + ColumnasGrilla)
                        e.Row.Cells(C).ForeColor = Drawing.Color.Red
                    Next C
                End If
            End If
        End If
    End Sub

    Private Sub gridReporte3_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte3.RowDataBound
        CantidadColumnas()
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 2 To (4 + ColumnasGrilla)
                If e.Row.Cells(i).Text = "&nbsp;" Then
                    For C = 0 To (8 + ColumnasGrilla)
                        e.Row.Cells(C).BackColor = Drawing.Color.LemonChiffon : Next C
                End If
            Next i

            If e.Row.Cells(5 + ColumnasGrilla).Text = "&nbsp;" Then
                PrecioGramoSYM = e.Row.Cells(6 + ColumnasGrilla).Text
            End If

            If e.Row.Cells(3 + ColumnasGrilla).Text = "$0.00" Then
                e.Row.Cells(3 + ColumnasGrilla).Text = ""
                e.Row.Cells(4 + ColumnasGrilla).Text = ""
                e.Row.Cells(5 + ColumnasGrilla).Text = ""
                e.Row.Cells(6 + ColumnasGrilla).Text = ""
                e.Row.Cells(7 + ColumnasGrilla).Text = ""
                e.Row.Cells(8 + ColumnasGrilla).Text = ""
            Else
                PrecioGramo = e.Row.Cells(6 + ColumnasGrilla).Text

                If PrecioGramo < PrecioGramoSYM Then
                    For C = (4 + ColumnasGrilla) To (8 + ColumnasGrilla)
                        e.Row.Cells(C).ForeColor = Drawing.Color.Red
                    Next C
                End If
            End If
        End If
    End Sub

    Private Sub gridReporte4_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte4.RowDataBound
        CantidadColumnas()
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 2 To (4 + ColumnasGrilla)
                If e.Row.Cells(i).Text = "&nbsp;" Then
                    For C = 0 To (8 + ColumnasGrilla)
                        e.Row.Cells(C).BackColor = Drawing.Color.LemonChiffon : Next C
                End If
            Next i

            If e.Row.Cells(5 + ColumnasGrilla).Text = "&nbsp;" Then
                PrecioGramoSYM = e.Row.Cells(6 + ColumnasGrilla).Text
            End If

            If e.Row.Cells(3 + ColumnasGrilla).Text = "$0.00" Then
                e.Row.Cells(3 + ColumnasGrilla).Text = ""
                e.Row.Cells(4 + ColumnasGrilla).Text = ""
                e.Row.Cells(5 + ColumnasGrilla).Text = ""
                e.Row.Cells(6 + ColumnasGrilla).Text = ""
                e.Row.Cells(7 + ColumnasGrilla).Text = ""
                e.Row.Cells(8 + ColumnasGrilla).Text = ""
            Else
                PrecioGramo = e.Row.Cells(6 + ColumnasGrilla).Text

                If PrecioGramo < PrecioGramoSYM Then
                    For C = (4 + ColumnasGrilla) To (8 + ColumnasGrilla)
                        e.Row.Cells(C).ForeColor = Drawing.Color.Red
                    Next C
                End If
            End If
        End If
    End Sub

    Private Sub gridReporte5_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte5.RowDataBound
        CantidadColumnas()
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 2 To (4 + ColumnasGrilla)
                If e.Row.Cells(i).Text = "&nbsp;" Then
                    For C = 0 To (8 + ColumnasGrilla)
                        e.Row.Cells(C).BackColor = Drawing.Color.LemonChiffon : Next C
                End If
            Next i

            If e.Row.Cells(5 + ColumnasGrilla).Text = "&nbsp;" Then
                PrecioGramoSYM = e.Row.Cells(6 + ColumnasGrilla).Text
            End If

            If e.Row.Cells(3 + ColumnasGrilla).Text = "$0.00" Then
                e.Row.Cells(3 + ColumnasGrilla).Text = ""
                e.Row.Cells(4 + ColumnasGrilla).Text = ""
                e.Row.Cells(5 + ColumnasGrilla).Text = ""
                e.Row.Cells(6 + ColumnasGrilla).Text = ""
                e.Row.Cells(7 + ColumnasGrilla).Text = ""
                e.Row.Cells(8 + ColumnasGrilla).Text = ""
            Else
                PrecioGramo = e.Row.Cells(6 + ColumnasGrilla).Text

                If PrecioGramo < PrecioGramoSYM Then
                    For C = (4 + ColumnasGrilla) To (8 + ColumnasGrilla)
                        e.Row.Cells(C).ForeColor = Drawing.Color.Red
                    Next C
                End If
            End If
        End If
    End Sub

    Private Sub gridReporte6_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte6.RowDataBound
        CantidadColumnas()
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 2 To (4 + ColumnasGrilla)
                If e.Row.Cells(i).Text = "&nbsp;" Then
                    For C = 0 To (8 + ColumnasGrilla)
                        e.Row.Cells(C).BackColor = Drawing.Color.LemonChiffon : Next C
                End If
            Next i

            If e.Row.Cells(5 + ColumnasGrilla).Text = "&nbsp;" Then
                PrecioGramoSYM = e.Row.Cells(6 + ColumnasGrilla).Text
            End If

            If e.Row.Cells(3 + ColumnasGrilla).Text = "$0.00" Then
                e.Row.Cells(3 + ColumnasGrilla).Text = ""
                e.Row.Cells(4 + ColumnasGrilla).Text = ""
                e.Row.Cells(5 + ColumnasGrilla).Text = ""
                e.Row.Cells(6 + ColumnasGrilla).Text = ""
                e.Row.Cells(7 + ColumnasGrilla).Text = ""
                e.Row.Cells(8 + ColumnasGrilla).Text = ""
            Else
                PrecioGramo = e.Row.Cells(6 + ColumnasGrilla).Text

                If PrecioGramo < PrecioGramoSYM Then
                    For C = (4 + ColumnasGrilla) To (8 + ColumnasGrilla)
                        e.Row.Cells(C).ForeColor = Drawing.Color.Red
                    Next C
                End If
            End If
        End If
    End Sub

    Private Sub gridReporte7_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte7.RowDataBound
        CantidadColumnas()
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 2 To (4 + ColumnasGrilla)
                If e.Row.Cells(i).Text = "&nbsp;" Then
                    For C = 0 To (8 + ColumnasGrilla)
                        e.Row.Cells(C).BackColor = Drawing.Color.LemonChiffon : Next C
                End If
            Next i

            If e.Row.Cells(5 + ColumnasGrilla).Text = "&nbsp;" Then
                PrecioGramoSYM = e.Row.Cells(6 + ColumnasGrilla).Text
            End If

            If e.Row.Cells(3 + ColumnasGrilla).Text = "$0.00" Then
                e.Row.Cells(3 + ColumnasGrilla).Text = ""
                e.Row.Cells(4 + ColumnasGrilla).Text = ""
                e.Row.Cells(5 + ColumnasGrilla).Text = ""
                e.Row.Cells(6 + ColumnasGrilla).Text = ""
                e.Row.Cells(7 + ColumnasGrilla).Text = ""
                e.Row.Cells(8 + ColumnasGrilla).Text = ""
            Else
                PrecioGramo = e.Row.Cells(6 + ColumnasGrilla).Text

                If PrecioGramo < PrecioGramoSYM Then
                    For C = (4 + ColumnasGrilla) To (8 + ColumnasGrilla)
                        e.Row.Cells(C).ForeColor = Drawing.Color.Red
                    Next C
                End If
            End If
        End If
    End Sub

    Private Sub gridReporte8_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte8.RowDataBound
        CantidadColumnas()
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 2 To (4 + ColumnasGrilla)
                If e.Row.Cells(i).Text = "&nbsp;" Then
                    For C = 0 To (8 + ColumnasGrilla)
                        e.Row.Cells(C).BackColor = Drawing.Color.LemonChiffon : Next C
                End If
            Next i

            If e.Row.Cells(5 + ColumnasGrilla).Text = "&nbsp;" Then
                PrecioGramoSYM = e.Row.Cells(6 + ColumnasGrilla).Text
            End If

            If e.Row.Cells(3 + ColumnasGrilla).Text = "$0.00" Then
                e.Row.Cells(3 + ColumnasGrilla).Text = ""
                e.Row.Cells(4 + ColumnasGrilla).Text = ""
                e.Row.Cells(5 + ColumnasGrilla).Text = ""
                e.Row.Cells(6 + ColumnasGrilla).Text = ""
                e.Row.Cells(7 + ColumnasGrilla).Text = ""
                e.Row.Cells(8 + ColumnasGrilla).Text = ""
            Else
                PrecioGramo = e.Row.Cells(6 + ColumnasGrilla).Text

                If PrecioGramo < PrecioGramoSYM Then
                    For C = (4 + ColumnasGrilla) To (8 + ColumnasGrilla)
                        e.Row.Cells(C).ForeColor = Drawing.Color.Red
                    Next C
                End If
            End If
        End If
    End Sub
End Class