Imports System.Data.SqlClient

Partial Public Class ReportePreciosCadenasNR
    Inherits System.Web.UI.Page

    Dim ProductoPropio, GramosPropio As Double
    Dim Campos(30), Columnas(30), PromedioCampos(30), SumaCampos(30), _
        Campos2(30), Columnas2(30), PromedioCampos2(30), SumaCampos2(30), _
        Campos3(30), Columnas3(30), PromedioCampos3(30), SumaCampos3(30), _
        SeleccionColumna, _
        SeleccionCampos, SeleccionCampos2, SeleccionCampos3, _
        SeleccionCamposPromedio, SeleccionCamposSuma As String
    Dim ColumnasGrilla As Integer
    Dim PrecioGramoSYM, PrecioGramo As Double
    Dim PeriodoSQLCmb, RegionSQLCmb, EstadoSQLCmb, SupervisorSQLCmb, CadenaSQLCmb As String

    Sub SQLCombo()
        Dim PeriodoSel, RegionSel, EstadoSel, SupervisorSel, CadenaSel As String
        RegionSel = ""

        PeriodoSel = Acciones.Slc.cmb("H.id_periodo", cmbPeriodo.SelectedValue)
        RegionSel = Acciones.Slc.cmb("H.id_region", cmbRegion.SelectedValue)
        EstadoSel = Acciones.Slc.cmb("H.id_estado", cmbEstado.SelectedValue)
        SupervisorSel = Acciones.Slc.cmb("H.id_supervisor", cmbSupervisor.SelectedValue)
        CadenaSel = Acciones.Slc.cmb("H.id_cadena", cmbCadena.SelectedValue)

        PeriodoSQLCmb = "SELECT * FROM NR_Periodos_Sup ORDER BY id_periodo DESC"

        RegionSQLCmb = "SELECT DISTINCT H.id_region, H.nombre_region " & _
                    "FROM View_Historial_Precios_NR as H " & _
                    "WHERE H.id_region <>0  " & _
                    "" + PeriodoSel + " " & _
                    " ORDER BY H.nombre_region"

        EstadoSQLCmb = "SELECT DISTINCT H.id_estado, H.nombre_estado " & _
                    "FROM View_Historial_Precios_NR as H " & _
                    "WHERE H.id_estado<>0  " & _
                    "" + PeriodoSel + RegionSel + " " & _
                    " ORDER BY H.nombre_estado"

        SupervisorSQLCmb = "SELECT DISTINCT H.id_supervisor, H.supervisor " & _
                     "FROM View_Historial_Precios_NR as H " & _
                     "WHERE H.id_usuario<>'' " & _
                     " " + PeriodoSel + RegionSel + EstadoSel + " ORDER BY H.id_supervisor"

        If Tipo_usuario = 7 Then
            Berol.CuentaClave = "INNER JOIN (select * FROM Usuarios_cadenas WHERE id_usuario='" & HttpContext.Current.User.Identity.Name & "' )as CC " & _
                            "ON CC.id_cadena=H.id_cadena " : Else
            Berol.CuentaClave = "" : End If

        CadenaSQLCmb = "SELECT DISTINCT H.id_cadena, H.nombre_cadena " & _
                    "FROM View_Historial_Precios_NR AS H " & _
                    " " + Berol.CuentaClave + " " & _
                    "WHERE H.id_cadena<>0 " & _
                    " " + PeriodoSel + RegionSel + EstadoSel + " " & _
                    " " + SupervisorSel + " " & _
                    " ORDER BY H.nombre_cadena"
    End Sub

    Public Function SeleccionFormatos(ByVal Tipo_producto As Integer, ByVal TipoMarca As String) As Integer
        Dim RegionSQL, EstadoSQL, SupervisorSQL, CadenaSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
            EstadoSQL = Acciones.Slc.cmb("TI.id_estado", cmbEstado.SelectedValue)
            SupervisorSQL = Acciones.Slc.cmb("H.id_usuario", cmbSupervisor.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("TI.id_cadena", cmbCadena.SelectedValue)

            If Tipo_usuario = 7 Then
                Berol.CuentaClave = "INNER JOIN (select * FROM Usuarios_cadenas WHERE id_usuario='" & HttpContext.Current.User.Identity.Name & "' )as CC " & _
                                "ON CC.id_cadena=TI.id_cadena " : Else
                Berol.CuentaClave = "" : End If

            Using cnn As New SqlConnection(ConexionBerol.localSqlServer)
                Dim SQL As New SqlCommand("SELECT DISTINCT TI.id_formato, TI.nombre_formato " & _
                            "FROM NR_Historial_Precios as H " & _
                            "INNER JOIN NR_Historial_Precios_Det as HDET on h.folio_historial=HDET.folio_historial " & _
                            "INNER JOIN View_Productos_NR as PROD ON PROD.id_producto=HDET.id_producto " & _
                            "INNER JOIN View_Tiendas_NR as TI on TI.id_tienda=h.id_tienda " & _
                            " " + Berol.CuentaClave + " " & _
                            "INNER JOIN (select distinct id_supervisor, supervisor " & _
                            "FROM View_Usuario_NR) as US ON US.id_supervisor=H.id_usuario " & _
                            "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                            "AND PROD.tipo_producto =" & Tipo_producto & " AND PROD.TipoMarca='" & TipoMarca & "' " & _
                            " " + RegionSQL + EstadoSQL + " " & _
                            " " + SupervisorSQL + CadenaSQL + " ", cnn)
                cnn.Open()
                Dim Data As New SqlDataAdapter(SQL)
                Dim Tabla As New DataTable
                Data.Fill(Tabla)

                If Tabla.Rows.Count > 0 Then
                    For i = 0 To Tabla.Rows.Count - 1
                        Columnas(i) = ",'$'+convert(nvarchar(10),CAST(ISNULL([" & Tabla.Rows(i)("id_formato") & "],0)as decimal(10,3)))'Promedio " & Tabla.Rows(i)("nombre_formato") & "' "
                        Columnas2(i) = ",'$'+convert(nvarchar(10),CAST(ISNULL([" & 100 + Tabla.Rows(i)("id_formato") & "],0)as decimal(10,3)))'Mínimo " & Tabla.Rows(i)("nombre_formato") & "' "
                        Columnas3(i) = ",'$'+convert(nvarchar(10),CAST(ISNULL([" & 200 + Tabla.Rows(i)("id_formato") & "],0)as decimal(10,3)))'Máximo " & Tabla.Rows(i)("nombre_formato") & "' "
                    Next i

                    For i = 0 To Tabla.Rows.Count - 1
                        If i = 0 Then
                            Campos(0) = "[" & Tabla.Rows(0)("id_formato") & "]"
                            SumaCampos(0) = "ISNULL([" & Tabla.Rows(0)("id_formato") & "],0)"
                            PromedioCampos(0) = "CASE WHEN ISNULL([" & Tabla.Rows(0)("id_formato") & "],0)=0 then 0 else 1 end "

                            Campos2(0) = "[" & 100 + Tabla.Rows(0)("id_formato") & "]"
                            SumaCampos2(0) = "ISNULL([" & 100 + Tabla.Rows(0)("id_formato") & "],0)"
                            PromedioCampos2(0) = "CASE WHEN ISNULL([" & 100 + Tabla.Rows(0)("id_formato") & "],0)=0 then 0 else 1 end "

                            Campos3(0) = "[" & 200 + Tabla.Rows(0)("id_formato") & "]"
                            SumaCampos3(0) = "ISNULL([" & 200 + Tabla.Rows(0)("id_formato") & "],0)"
                            PromedioCampos3(0) = "CASE WHEN ISNULL([" & 200 + Tabla.Rows(0)("id_formato") & "],0)=0 then 0 else 1 end "
                        Else
                            Campos(i) = ",[" & Tabla.Rows(i)("id_formato") & "]"
                            SumaCampos(i) = "+ISNULL([" & Tabla.Rows(i)("id_formato") & "],0)"
                            PromedioCampos(i) = "+ CASE WHEN ISNULL([" & Tabla.Rows(i)("id_formato") & "],0)=0 then 0 else 1 end "

                            Campos2(i) = ",[" & 100 + Tabla.Rows(i)("id_formato") & "]"
                            SumaCampos2(i) = "+ISNULL([" & 100 + Tabla.Rows(i)("id_formato") & "],0)"
                            PromedioCampos2(i) = "+ CASE WHEN ISNULL([" & 100 + Tabla.Rows(i)("id_formato") & "],0)=0 then 0 else 1 end "

                            Campos3(i) = ",[" & 200 + Tabla.Rows(i)("id_formato") & "]"
                            SumaCampos3(i) = "+ISNULL([" & 200 + Tabla.Rows(i)("id_formato") & "],0)"
                            PromedioCampos3(i) = "+ CASE WHEN ISNULL([" & 200 + Tabla.Rows(i)("id_formato") & "],0)=0 then 0 else 1 end "
                        End If
                    Next i

                    For i = 0 To Tabla.Rows.Count - 1
                        SeleccionColumna = SeleccionColumna + Columnas(i) + Columnas2(i) + Columnas3(i)

                        SeleccionCampos = SeleccionCampos + Campos(i)
                        SeleccionCampos2 = SeleccionCampos2 + Campos2(i)
                        SeleccionCampos3 = SeleccionCampos3 + Campos3(i)

                        SeleccionCamposPromedio = SeleccionCamposPromedio + PromedioCampos(i)
                        SeleccionCamposSuma = SeleccionCamposSuma + SumaCampos(i)
                    Next i
                End If

                SQL.Dispose()
                Data.Dispose()
                Tabla.Dispose()
                cnn.Dispose()
                cnn.Close()
            End Using
        End If
    End Function

    Public Function VerGrilla(ByVal Grilla As GridView, ByVal TipoProducto As Integer, ByVal Marca As String) As Integer
        Dim RegionSQL, EstadoSQL, SupervisorSQL, CadenaSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
            EstadoSQL = Acciones.Slc.cmb("TI.id_estado", cmbEstado.SelectedValue)
            SupervisorSQL = Acciones.Slc.cmb("H.id_usuario", cmbSupervisor.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("TI.id_cadena", cmbCadena.SelectedValue)

            If Tipo_usuario = 7 Then
                Berol.CuentaClave = "INNER JOIN (select * FROM Usuarios_cadenas WHERE id_usuario='" & HttpContext.Current.User.Identity.Name & "' )as CC " & _
                                "ON CC.id_cadena=TI.id_cadena " : Else
                Berol.CuentaClave = "" : End If

            SeleccionColumna = ""

            SeleccionCampos = ""
            SeleccionCampos2 = ""
            SeleccionCampos3 = ""

            SeleccionCamposPromedio = ""
            SeleccionCamposSuma = ""
            SeleccionFormatos(TipoProducto, Marca)

            If SeleccionColumna <> "" Then
                CargaGrilla(ConexionBerol.localSqlServer, "select PROD.nombre_producto as 'Producto', PROD.nombre_marca as 'Marca', " & _
                        "PROD.nombre_presentacion as 'Presentación', PROD.codigo as 'UPC' " & _
                        " " + SeleccionColumna + ", " + SeleccionCamposPromedio + "as Cantidad, " & _
                        " '$'+convert(nvarchar(10),CAST((CASE WHEN (" + SeleccionCamposPromedio + ")<>0 then " & _
                        " (" + SeleccionCamposSuma + ") /(" + SeleccionCamposPromedio + ") ELSE 0 end)as decimal(10,3))) Promedio, " & _
                        " '$'+convert(nvarchar(10),Hmin.precio) as 'Precio minimo', " & _
                        " '$'+convert(nvarchar(10),Hmax.precio) as 'Precio máximo' " & _
                        "FROM View_Productos_NR as PROD " & _
                        "RIGHT JOIN (select TI.id_formato, HDET.id_producto, cast((HDET.precio)as decimal(10,3))Precio " & _
                        "FROM NR_Historial_Precios as H " & _
                        "INNER JOIN NR_Historial_Precios_Det as hdet on hdet.folio_historial=h.folio_historial " & _
                        "INNER JOIN View_Tiendas_NR as TI on ti.id_tienda=h.id_tienda " & _
                        " " + Berol.CuentaClave + " " & _
                        "INNER JOIN (select distinct id_supervisor, supervisor " & _
                        "FROM View_Usuario_NR) as US ON US.id_supervisor=H.id_usuario " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + EstadoSQL + " " & _
                        " " + SupervisorSQL + CadenaSQL + ") PVT " & _
                        "PIVOT(avg(precio) FOR id_formato " & _
                        "IN (" + SeleccionCampos + "))HDET ON HDET.id_producto=PROD.id_producto " & _
                        "RIGHT JOIN (select 100+TI.id_formato as id_formato, HDET.id_producto, cast((HDET.precio)as decimal(10,3))Precio " & _
                        "FROM NR_Historial_Precios as H " & _
                        "INNER JOIN NR_Historial_Precios_Det as hdet on hdet.folio_historial=h.folio_historial " & _
                        "INNER JOIN View_Tiendas_NR as TI on ti.id_tienda=h.id_tienda " & _
                        " " + Berol.CuentaClave + " " & _
                        "INNER JOIN (select distinct id_supervisor, supervisor " & _
                        "FROM View_Usuario_NR) as US ON US.id_supervisor=H.id_usuario " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + EstadoSQL + " " & _
                        " " + SupervisorSQL + CadenaSQL + ") PVT " & _
                        "PIVOT(MIN(precio) FOR id_formato " & _
                        "IN (" + SeleccionCampos2 + "))HDET2 ON HDET2.id_producto=PROD.id_producto " & _
                        "RIGHT JOIN (select 200+TI.id_formato as id_formato, HDET.id_producto, cast((HDET.precio)as decimal(10,3))Precio " & _
                        "FROM NR_Historial_Precios as H " & _
                        "INNER JOIN NR_Historial_Precios_Det as hdet on hdet.folio_historial=h.folio_historial " & _
                        "INNER JOIN View_Tiendas_NR as TI on ti.id_tienda=h.id_tienda " & _
                        " " + Berol.CuentaClave + " " & _
                        "INNER JOIN (select distinct id_supervisor, supervisor " & _
                        "FROM View_Usuario_NR) as US ON US.id_supervisor=H.id_usuario " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + EstadoSQL + " " & _
                        " " + SupervisorSQL + CadenaSQL + ") PVT " & _
                        "PIVOT(MAX(precio) FOR id_formato " & _
                        "IN (" + SeleccionCampos3 + "))HDET3 ON HDET3.id_producto=PROD.id_producto " & _
                        "RIGHT JOIN (select id_producto,cast(min(precio)as decimal(10,3))Precio " & _
                        "from (select HDET.id_producto,TI.id_formato,(avg(HDET.precio))Precio  " & _
                        "FROM NR_Historial_Precios as H   " & _
                        "INNER JOIN NR_Historial_Precios_Det as hdet on hdet.folio_historial=h.folio_historial   " & _
                        "INNER JOIN View_Tiendas_NR as TI on ti.id_tienda=h.id_tienda   " & _
                        " " + Berol.CuentaClave + " " & _
                        "INNER JOIN (select distinct id_supervisor, supervisor   " & _
                        "FROM View_Usuario_NR) as US ON US.id_supervisor=H.id_usuario   " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + EstadoSQL + " " & _
                        " " + SupervisorSQL + CadenaSQL + "GROUP BY HDET.id_producto,TI.id_formato " & _
                        ")H group by id_producto) AS Hmin on Hmin.id_producto=PROD.id_producto " & _
                        "RIGHT JOIN (select id_producto,cast(max(precio)as decimal(10,3))Precio " & _
                        "from (select HDET.id_producto,TI.id_formato,(avg(HDET.precio))Precio  " & _
                        "FROM NR_Historial_Precios as H   " & _
                        "INNER JOIN NR_Historial_Precios_Det as hdet on hdet.folio_historial=h.folio_historial   " & _
                        "INNER JOIN View_Tiendas_NR as TI on ti.id_tienda=h.id_tienda   " & _
                        " " + Berol.CuentaClave + " " & _
                        "INNER JOIN (select distinct id_supervisor, supervisor   " & _
                        "FROM View_Usuario_NR) as US ON US.id_supervisor=H.id_usuario   " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + EstadoSQL + " " & _
                        " " + SupervisorSQL + CadenaSQL + "GROUP BY HDET.id_producto,TI.id_formato " & _
                        ")H group by id_producto)AS Hmax on Hmax.id_producto=PROD.id_producto " & _
                        "WHERE PROD.tipo_producto =" & TipoProducto & " AND PROD.TipoMarca='" & Marca & "' " & _
                        "ORDER BY PROD.nombre_producto", Grilla)
            End If
        End If
    End Function

    Sub CargarReporte()
        If cmbPeriodo.SelectedValue <> "" Then
            VerGrilla(gridReporte, 1, "PROPIA")
            VerGrilla(gridReporteB, 1, "COMPETENCIA")
            VerGrilla(gridReporte2, 2, "PROPIA")
            VerGrilla(gridReporte2B, 2, "COMPETENCIA")
            VerGrilla(gridReporte3, 3, "PROPIA")
            VerGrilla(gridReporte3B, 3, "COMPETENCIA")
            'VerGrilla(gridReporte4, 4, "PROPIA")
            'VerGrilla(gridReporte4B, 4, "COMPETENCIA")
            VerGrilla(gridReporte5, 5, "PROPIA")
            VerGrilla(gridReporte5B, 5, "COMPETENCIA")
            VerGrilla(gridReporte6, 6, "PROPIA")
            VerGrilla(gridReporte6B, 6, "COMPETENCIA")
        Else
            gridReporte.Visible = False
            gridReporteB.Visible = False
            gridReporte2.Visible = False
            gridReporte2B.Visible = False
            gridReporte3.Visible = False
            gridReporte3B.Visible = False
            'gridReporte4.Visible = False
            'gridReporte4B.Visible = False
            gridReporte5.Visible = False
            gridReporte5B.Visible = False
            gridReporte6.Visible = False
            gridReporte6B.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionBerol.localSqlServer, PeriodoSQLCmb, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        End If
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte precios por cadena " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEstado.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Private Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)

        CargarReporte()
    End Sub

    Private Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCadena.SelectedIndexChanged
        CargarReporte()
    End Sub

End Class