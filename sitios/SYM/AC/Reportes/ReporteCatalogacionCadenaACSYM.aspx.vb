Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteCatalogacionCadenaACSYM
    Inherits System.Web.UI.Page

    Dim RegionSQL, EstadoSQL, SupervisorSQL, CiudadSQL, PromotorSQL As String
    Dim Campos(100), Columnas(100), SumaTotal(100) As String
    Dim TituloCamposCad(100), CampoCad(100) As String
    Dim TituloCampos, CamposCad, SumaTiendas(100), SumaTotalTiendas As String
    Dim SeleccionCampos, SeleccionColumna, SeleccionSuma As String
    Dim CantidadPeriodos As Integer

    Sub SQLCombo()
        Combos_SYMAC.SQLsCombo(cmbPeriodo.SelectedValue, cmbRegion.SelectedValue, _
                             cmbEstado.SelectedValue, cmbSupervisor.SelectedValue, _
                             cmbCiudad.SelectedValue, cmbPromotor.SelectedValue, _
                             "", "View_SYM_Catalogacion_Historial")
    End Sub

    Public Function SeleccionPeriodos(ByVal Linea As String) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT * FROM Cadenas_Tiendas WHERE catalogacion <>'' " & _
                                  "ORDER BY catalogacion")
        If Tabla.Rows.Count > 0 Then
            CantidadPeriodos = Tabla.Rows.Count

            For i = 0 To Tabla.Rows.Count - 1
                TituloCamposCad(i) = ",[" & Tabla.Rows(i)("id_cadena") & "]'" & Tabla.Rows(i)("nombre_cadena") & "' "

                If i = 0 Then
                    CampoCad(0) = "[" & Tabla.Rows(i)("id_cadena") & "]" : Else
                    CampoCad(i) = ",[" & Tabla.Rows(i)("id_cadena") & "]" : End If

                Campos(i) = ",ISNULL(CAD" & i & ".Cantidad,0) as '" & Tabla.Rows(i)("nombre_cadena") & "' "

                Columnas(i) = "FULL JOIN (SELECT TI.id_cadena, H.id_producto, COUNT(H.catalogado)as Cantidad " & _
                            "FROM View_SYM_Catalogacion_Historial as H " & _
                            "INNER JOIN AC_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                            "WHERE TI.id_cadena=" & Tabla.Rows(i)("id_cadena") & " AND H.tipo_grupo =" & Linea & " " & _
                            "AND H.catalogado=1 AND H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                            " " + RegionSQL + " " & _
                            " " + EstadoSQL + " " & _
                            " " + SupervisorSQL + " " & _
                            " " + CiudadSQL + " " & _
                            " " + PromotorSQL + " " & _
                            "GROUP BY TI.id_cadena,H.id_producto)CAD" & i & " " & _
                            "ON CAD" & i & ".id_producto= H.id_producto "
            Next i

            For i = 0 To Tabla.Rows.Count - 1
                If i = 0 Then
                    SumaTotal(0) = "ISNULL(CAD0.Cantidad,0)"
                    SumaTiendas(0) = "ISNULL([" & Tabla.Rows(i)("id_cadena") & "],0)"
                Else
                    SumaTotal(i) = "+ISNULL(CAD" & i & ".Cantidad,0)"
                    SumaTiendas(i) = "+ISNULL([" & Tabla.Rows(i)("id_cadena") & "],0)"
                End If

                TituloCampos = TituloCampos + TituloCamposCad(i)
                CamposCad = CamposCad + CampoCad(i)
                SumaTotalTiendas = SumaTotalTiendas + SumaTiendas(i)

                SeleccionSuma = SeleccionSuma + SumaTotal(i)
                SeleccionCampos = SeleccionCampos + Campos(i)
                SeleccionColumna = SeleccionColumna + Columnas(i)
            Next i
        End If

        Tabla.Dispose()
    End Function

    Sub CargarReporte()
        If cmbPeriodo.SelectedValue <> "" Then
            VerGrilla(gridReporte, 101, "DETERGENTE MULTIUSOS")
            VerGrilla(gridReporte2, 103, "JABON DE LAVANDERIA")
            VerGrilla(gridReporte3, 104, "JABON DE TOCADOR")
            VerGrilla(gridReporte4, 110, "LAVANDERÍA LÍQUIDO")
            VerGrilla(gridReporte5, 105, "LAVATRASTES")
            VerGrilla(gridReporte6, 109, "JABON LIQUIDO MANOS")
            VerGrilla(gridReporte7, 108, "JABON LIQUIDO CORPORAL")

        Else
            gridReporte.Visible = False
            gridReporte2.Visible = False
            gridReporte3.Visible = False
            gridReporte4.Visible = False
        End If
    End Sub

    Public Function VerGrilla(ByVal Grilla As GridView, ByVal Linea As String, ByVal NombreLinea As String) As Integer
        RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
        EstadoSQL = Acciones.Slc.cmb("TI.id_estado", cmbEstado.SelectedValue)
        SupervisorSQL = Acciones.Slc.cmb("REL.id_supervisor", cmbSupervisor.SelectedValue)
        CiudadSQL = Acciones.Slc.cmb("TI.ciudad", cmbCiudad.SelectedValue)
        PromotorSQL = Acciones.Slc.cmb("RE.id_usuario", cmbPromotor.SelectedValue)

        TituloCampos = ""
        SumaTotalTiendas = ""
        CamposCad = ""
        SeleccionCampos = ""
        SeleccionSuma = ""
        SeleccionColumna = ""

        SeleccionPeriodos(Linea)

        CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT ' ' as Codigo,'' as '" & NombreLinea & "'" & _
                    "" & TituloCampos & ", (" & SumaTotalTiendas & ") as Total, '' as '% Catalogación' " & _
                    "FROM(SELECT RE.id_periodo, TI.id_cadena, COUNT(RE.id_tienda)tiendas " & _
                    "FROM View_SYM_AC_Tiendas AS TI " & _
                    "INNER JOIN View_SYM_AC_RE AS RE ON RE.id_tienda = TI.id_tienda " & _
                    "WHERE RE.id_periodo =" & cmbPeriodo.SelectedValue & " AND RE.id_usuario<>'' " & _
                    " " + RegionSQL + EstadoSQL + CiudadSQL + " " & _
                    " " + SupervisorSQL + PromotorSQL + " " & _
                    "GROUP BY RE.id_periodo, TI.id_cadena " & _
                    ") AS Datos PIVOT(SUM(tiendas) " & _
                    "FOR id_cadena IN (" & CamposCad & ")) AS PivotTable" & _
                    " UNION ALL " & _
                    "SELECT DISTINCT H.codigo,H.nombre_producto as '" & NombreLinea & "' " & _
                    " " + SeleccionCampos + " " & _
                    ",(" + SeleccionSuma + " ) as Total " & _
                    ",isnull((100*NULLIF(" + SeleccionSuma + ",0)/(SELECT count(RE.id_tienda)FROM AC_Rutas_Eventos as RE " & _
                    "INNER JOIN View_SYM_AC_Tiendas as TI ON RE.id_tienda = TI.id_tienda " & _
                    "WHERE RE.id_periodo=" & cmbPeriodo.SelectedValue & " AND CAD.catalogacion <>'' AND RE.id_usuario<>'' " & _
                    " " + RegionSQL + EstadoSQL + CiudadSQL + " " & _
                    " " + SupervisorSQL + PromotorSQL + ")),0) as '% Catalogación' " & _
                    "FROM View_SYM_Catalogacion_Historial as H " & _
                    " " + SeleccionColumna + " " & _
                    "WHERE H.tipo_grupo = " & Linea & "", Grilla)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PeriodoSQLCmb, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.EstadoSQLcmb, "nombre_estado", "id_estado", cmbEstado)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.SupervisorSQLcmb, "supervisor", "id_supervisor", cmbSupervisor)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CiudadSQLCmb, "ciudad", "ciudad", cmbCiudad)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.EstadoSQLcmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.SupervisorSQLcmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CiudadSQLCmb, "ciudad", "ciudad", cmbCiudad)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.EstadoSQLcmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.SupervisorSQLcmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CiudadSQLCmb, "ciudad", "ciudad", cmbCiudad)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Catalogacion por Cadena " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte2.RowDataBound
        'Dim Dato(50), Suma(50) As Integer

        'Dim cnn As New SqlConnection(ConexionSYM.localSqlServer)
        'cnn.Open()

        'If e.Row.RowType = DataControlRowType.DataRow Then

        '    If e.Row.Cells(0).Text <> " " Then
        '        For iTiendas = 2 To 46
        '            ''Cuenta
        '            If e.Row.Cells(iTiendas).Text <> 0 Then
        '                Dato(iTiendas) = 1
        '                Suma(iTiendas) = Suma(iTiendas) + Dato(iTiendas)
        '            End If
        '        Next iTiendas

        '        Dim Porcentaje As String
        '        Porcentaje = e.Row.Cells(46).Text
        '        e.Row.Cells(46).Text = e.Row.Cells(46).Text & "%"
        '    End If

        '    If e.Row.Cells(0).Text = " " Then
        '        Dim SQL As New SqlCommand("SELECT CAD.id_grupo, CAD.nombre_cadena, GCAD.color FROM Cadenas_Tiendas as CAD " & _
        '                        "INNER JOIN Cadenas_Grupo as GCAD ON GCAD.id_grupo = CAD.id_grupo " & _
        '                        "WHERE CAD.catalogacion <>'' ORDER BY CAD.catalogacion", cnn)
        '        Dim Tabla As New DataTable
        '        Dim Data As New SqlDataAdapter(SQL)
        '        Data.Fill(Tabla)


        '        If Tabla.Rows.Count > 0 Then
        '            Dim iC As Integer
        '            iC = 2

        '            For iT = 0 To Tabla.Rows.Count - 1
        '                e.Row.Cells(iC).BackColor = System.Drawing.Color.FromName(Tabla.Rows(iT)("color"))
        '                e.Row.ForeColor = Drawing.Color.White
        '                e.Row.HorizontalAlign = HorizontalAlign.Center
        '                iC = iC + 1
        '            Next iT
        '        End If

        '        For i = 0 To 1
        '            e.Row.Cells(i).BackColor = Drawing.Color.SteelBlue
        '        Next

        '        e.Row.Cells(45).BackColor = Drawing.Color.SteelBlue
        '        e.Row.Cells(46).BackColor = Drawing.Color.SteelBlue
        '        e.Row.Font.Bold = True

        '        SQL.Dispose()
        '        Tabla.Dispose()
        '    End If
        'End If

        'If e.Row.RowType = DataControlRowType.Header Then
        '    For i = 0 To CInt(gridReporte2.Rows.Count) - 0
        '        Dim SQL As New SqlCommand("SELECT CAD.id_grupo, CAD.nombre_cadena, GCAD.color FROM Cadenas_Tiendas as CAD " & _
        '                        "INNER JOIN Cadenas_Grupo as GCAD ON GCAD.id_grupo = CAD.id_grupo " & _
        '                        "WHERE CAD.catalogacion <>'' ORDER BY CAD.catalogacion", cnn)
        '        Dim Tabla As New DataTable
        '        Dim Data As New SqlDataAdapter(SQL)
        '        Data.Fill(Tabla)

        '        If Tabla.Rows.Count > 0 Then
        '            Dim iC As Integer
        '            iC = 2

        '            For iT = 0 To Tabla.Rows.Count - 1
        '                e.Row.Cells(iC).BackColor = System.Drawing.Color.FromName(Tabla.Rows(iT)("color"))
        '                e.Row.ForeColor = Drawing.Color.White

        '                iC = iC + 1
        '            Next iT
        '        End If

        '        For iV = 0 To 1
        '            e.Row.Cells(iV).BackColor = Drawing.Color.SteelBlue
        '        Next

        '        e.Row.Cells(45).BackColor = Drawing.Color.SteelBlue
        '        e.Row.Cells(46).BackColor = Drawing.Color.SteelBlue

        '        SQL.Dispose()
        '        Tabla.Dispose()
        '    Next i
        'End If

        ''If e.Row.RowType = DataControlRowType.Footer Then
        ''    e.Row.Cells(1).Text = "Cantidad de productos catalogados"

        ''    For iTiendas = 2 To 46
        ''        e.Row.Cells(iTiendas).Text = Suma(iTiendas)
        ''    Next
        ''End If

        'cnn.Close()
        'cnn.Dispose()
    End Sub

    Private Sub cmbEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEstado.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.SupervisorSQLcmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CiudadSQLCmb, "ciudad", "ciudad", cmbCiudad)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Private Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CiudadSQLCmb, "ciudad", "ciudad", cmbCiudad)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Private Sub cmbCiudad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCiudad.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Private Sub gridReporte_RowDataBound1(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        Dim Dato(50), Suma(50) As Integer

        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(0).Text <> " " Then
                For iTiendas = 2 To 46
                    'Cuenta
                    If e.Row.Cells(iTiendas).Text <> 0 Then
                        Dato(iTiendas) = 1
                        Suma(iTiendas) = Suma(iTiendas) + Dato(iTiendas)
                    End If
                Next iTiendas

                Dim Porcentaje As String
                Porcentaje = e.Row.Cells(46).Text
                e.Row.Cells(46).Text = e.Row.Cells(46).Text & "%"
            End If

            If e.Row.Cells(0).Text = " " Then
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                                       "SELECT CAD.id_grupo, CAD.nombre_cadena, GCAD.color FROM Cadenas_Tiendas as CAD " & _
                                "INNER JOIN Cadenas_Grupo as GCAD ON GCAD.id_grupo = CAD.id_grupo " & _
                                "WHERE CAD.catalogacion <>'' ORDER BY CAD.catalogacion")
                If Tabla.Rows.Count > 0 Then
                    Dim iC As Integer
                    iC = 2

                    For iT = 0 To Tabla.Rows.Count - 1
                        e.Row.Cells(iC).BackColor = System.Drawing.Color.FromName(Tabla.Rows(iT)("color"))
                        e.Row.ForeColor = Drawing.Color.White
                        e.Row.HorizontalAlign = HorizontalAlign.Center
                        iC = iC + 1
                    Next iT
                End If

                For i = 0 To 1
                    e.Row.Cells(i).BackColor = Drawing.Color.SteelBlue
                Next

                e.Row.Cells(45).BackColor = Drawing.Color.SteelBlue
                e.Row.Cells(46).BackColor = Drawing.Color.SteelBlue
                e.Row.Font.Bold = True

                Tabla.Dispose()
            End If
        End If

        If e.Row.RowType = DataControlRowType.Header Then
            For i = 0 To CInt(gridReporte2.Rows.Count) - 0
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                                       "SELECT CAD.id_grupo, CAD.nombre_cadena, GCAD.color FROM Cadenas_Tiendas as CAD " & _
                                "INNER JOIN Cadenas_Grupo as GCAD ON GCAD.id_grupo = CAD.id_grupo " & _
                                "WHERE CAD.catalogacion <>'' ORDER BY CAD.catalogacion")
                If Tabla.Rows.Count > 0 Then
                    Dim iC As Integer
                    iC = 2

                    For iT = 0 To Tabla.Rows.Count - 1
                        e.Row.Cells(iC).BackColor = System.Drawing.Color.FromName(Tabla.Rows(iT)("color"))
                        e.Row.ForeColor = Drawing.Color.White

                        iC = iC + 1
                    Next iT
                End If

                For iV = 0 To 1
                    e.Row.Cells(iV).BackColor = Drawing.Color.SteelBlue
                Next

                e.Row.Cells(45).BackColor = Drawing.Color.SteelBlue
                e.Row.Cells(46).BackColor = Drawing.Color.SteelBlue

                Tabla.Dispose()
            Next i
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = "Cantidad de productos catalogados"

            For iTiendas = 2 To 46
                e.Row.Cells(iTiendas).Text = Suma(iTiendas)
            Next
        End If
    End Sub

    Private Sub gridReporte3_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte3.RowDataBound
        Dim Dato(50), Suma(50) As Integer

        Dim cnn As New SqlConnection(ConexionSYM.localSqlServer)
        cnn.Open()

        If e.Row.RowType = DataControlRowType.DataRow Then

            If e.Row.Cells(0).Text <> " " Then
                For iTiendas = 2 To 46
                    'Cuenta
                    If e.Row.Cells(iTiendas).Text <> 0 Then
                        Dato(iTiendas) = 1
                        Suma(iTiendas) = Suma(iTiendas) + Dato(iTiendas)
                    End If
                Next iTiendas

                Dim Porcentaje As String
                Porcentaje = e.Row.Cells(46).Text
                e.Row.Cells(46).Text = e.Row.Cells(46).Text & "%"
            End If

            If e.Row.Cells(0).Text = " " Then
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                                       "SELECT CAD.id_grupo, CAD.nombre_cadena, GCAD.color FROM Cadenas_Tiendas as CAD " & _
                                "INNER JOIN Cadenas_Grupo as GCAD ON GCAD.id_grupo = CAD.id_grupo " & _
                                "WHERE CAD.catalogacion <>'' ORDER BY CAD.catalogacion")
                If Tabla.Rows.Count > 0 Then
                    Dim iC As Integer
                    iC = 2

                    For iT = 0 To Tabla.Rows.Count - 1
                        e.Row.Cells(iC).BackColor = System.Drawing.Color.FromName(Tabla.Rows(iT)("color"))
                        e.Row.ForeColor = Drawing.Color.White
                        e.Row.HorizontalAlign = HorizontalAlign.Center
                        iC = iC + 1
                    Next iT
                End If

                For i = 0 To 1
                    e.Row.Cells(i).BackColor = Drawing.Color.SteelBlue
                Next

                e.Row.Cells(45).BackColor = Drawing.Color.SteelBlue
                e.Row.Cells(46).BackColor = Drawing.Color.SteelBlue
                e.Row.Font.Bold = True

                Tabla.Dispose()
            End If
        End If

        If e.Row.RowType = DataControlRowType.Header Then
            For i = 0 To CInt(gridReporte2.Rows.Count) - 0
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                                       "SELECT CAD.id_grupo, CAD.nombre_cadena, GCAD.color FROM Cadenas_Tiendas as CAD " & _
                                "INNER JOIN Cadenas_Grupo as GCAD ON GCAD.id_grupo = CAD.id_grupo " & _
                                "WHERE CAD.catalogacion <>'' ORDER BY CAD.catalogacion")
                If Tabla.Rows.Count > 0 Then
                    Dim iC As Integer
                    iC = 2

                    For iT = 0 To Tabla.Rows.Count - 1
                        e.Row.Cells(iC).BackColor = System.Drawing.Color.FromName(Tabla.Rows(iT)("color"))
                        e.Row.ForeColor = Drawing.Color.White

                        iC = iC + 1
                    Next iT
                End If

                For iV = 0 To 1
                    e.Row.Cells(iV).BackColor = Drawing.Color.SteelBlue
                Next

                e.Row.Cells(45).BackColor = Drawing.Color.SteelBlue
                e.Row.Cells(46).BackColor = Drawing.Color.SteelBlue

                Tabla.Dispose()
            Next i
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = "Cantidad de productos catalogados"

            For iTiendas = 2 To 46
                e.Row.Cells(iTiendas).Text = Suma(iTiendas)
            Next
        End If
    End Sub

    Private Sub gridReporte4_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte4.RowDataBound
        Dim Dato(50), Suma(50) As Integer

        Dim cnn As New SqlConnection(ConexionSYM.localSqlServer)
        cnn.Open()

        If e.Row.RowType = DataControlRowType.DataRow Then

            If e.Row.Cells(0).Text <> " " Then
                For iTiendas = 2 To 46
                    'Cuenta
                    If e.Row.Cells(iTiendas).Text <> 0 Then
                        Dato(iTiendas) = 1
                        Suma(iTiendas) = Suma(iTiendas) + Dato(iTiendas)
                    End If
                Next iTiendas

                Dim Porcentaje As String
                Porcentaje = e.Row.Cells(46).Text
                e.Row.Cells(46).Text = e.Row.Cells(46).Text & "%"
            End If

            If e.Row.Cells(0).Text = " " Then
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                                       "SELECT CAD.id_grupo, CAD.nombre_cadena, GCAD.color FROM Cadenas_Tiendas as CAD " & _
                                "INNER JOIN Cadenas_Grupo as GCAD ON GCAD.id_grupo = CAD.id_grupo " & _
                                "WHERE CAD.catalogacion <>'' ORDER BY CAD.catalogacion")
                If Tabla.Rows.Count > 0 Then
                    Dim iC As Integer
                    iC = 2

                    For iT = 0 To Tabla.Rows.Count - 1
                        e.Row.Cells(iC).BackColor = System.Drawing.Color.FromName(Tabla.Rows(iT)("color"))
                        e.Row.ForeColor = Drawing.Color.White
                        e.Row.HorizontalAlign = HorizontalAlign.Center
                        iC = iC + 1
                    Next iT
                End If

                For i = 0 To 1
                    e.Row.Cells(i).BackColor = Drawing.Color.SteelBlue
                Next

                e.Row.Cells(45).BackColor = Drawing.Color.SteelBlue
                e.Row.Cells(46).BackColor = Drawing.Color.SteelBlue
                e.Row.Font.Bold = True

                Tabla.Dispose()
            End If
        End If

        If e.Row.RowType = DataControlRowType.Header Then
            For i = 0 To CInt(gridReporte2.Rows.Count) - 0
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                                       "SELECT CAD.id_grupo, CAD.nombre_cadena, GCAD.color FROM Cadenas_Tiendas as CAD " & _
                                "INNER JOIN Cadenas_Grupo as GCAD ON GCAD.id_grupo = CAD.id_grupo " & _
                                "WHERE CAD.catalogacion <>'' ORDER BY CAD.catalogacion")
                If Tabla.Rows.Count > 0 Then
                    Dim iC As Integer
                    iC = 2

                    For iT = 0 To Tabla.Rows.Count - 1
                        e.Row.Cells(iC).BackColor = System.Drawing.Color.FromName(Tabla.Rows(iT)("color"))
                        e.Row.ForeColor = Drawing.Color.White

                        iC = iC + 1
                    Next iT
                End If

                For iV = 0 To 1
                    e.Row.Cells(iV).BackColor = Drawing.Color.SteelBlue
                Next

                e.Row.Cells(45).BackColor = Drawing.Color.SteelBlue
                e.Row.Cells(46).BackColor = Drawing.Color.SteelBlue

                Tabla.Dispose()
            Next i
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = "Cantidad de productos catalogados"

            For iTiendas = 2 To 46
                e.Row.Cells(iTiendas).Text = Suma(iTiendas)
            Next
        End If
    End Sub

    Private Sub gridReporte5_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte5.RowDataBound
        Dim Dato(50), Suma(50) As Integer

        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(0).Text <> " " Then
                For iTiendas = 2 To 46
                    'Cuenta
                    If e.Row.Cells(iTiendas).Text <> 0 Then
                        Dato(iTiendas) = 1
                        Suma(iTiendas) = Suma(iTiendas) + Dato(iTiendas)
                    End If
                Next iTiendas

                Dim Porcentaje As String
                Porcentaje = e.Row.Cells(46).Text
                e.Row.Cells(46).Text = e.Row.Cells(46).Text & "%"
            End If

            If e.Row.Cells(0).Text = " " Then
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                                       "SELECT CAD.id_grupo, CAD.nombre_cadena, GCAD.color FROM Cadenas_Tiendas as CAD " & _
                                "INNER JOIN Cadenas_Grupo as GCAD ON GCAD.id_grupo = CAD.id_grupo " & _
                                "WHERE CAD.catalogacion <>'' ORDER BY CAD.catalogacion")
                If Tabla.Rows.Count > 0 Then
                    Dim iC As Integer
                    iC = 2

                    For iT = 0 To Tabla.Rows.Count - 1
                        e.Row.Cells(iC).BackColor = System.Drawing.Color.FromName(Tabla.Rows(iT)("color"))
                        e.Row.ForeColor = Drawing.Color.White
                        e.Row.HorizontalAlign = HorizontalAlign.Center
                        iC = iC + 1
                    Next iT
                End If

                For i = 0 To 1
                    e.Row.Cells(i).BackColor = Drawing.Color.SteelBlue
                Next

                e.Row.Cells(45).BackColor = Drawing.Color.SteelBlue
                e.Row.Cells(46).BackColor = Drawing.Color.SteelBlue
                e.Row.Font.Bold = True

                Tabla.Dispose()
            End If
        End If

        If e.Row.RowType = DataControlRowType.Header Then
            For i = 0 To CInt(gridReporte2.Rows.Count) - 0
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                                       "SELECT CAD.id_grupo, CAD.nombre_cadena, GCAD.color FROM Cadenas_Tiendas as CAD " & _
                                "INNER JOIN Cadenas_Grupo as GCAD ON GCAD.id_grupo = CAD.id_grupo " & _
                                "WHERE CAD.catalogacion <>'' ORDER BY CAD.catalogacion")
                If Tabla.Rows.Count > 0 Then
                    Dim iC As Integer
                    iC = 2

                    For iT = 0 To Tabla.Rows.Count - 1
                        e.Row.Cells(iC).BackColor = System.Drawing.Color.FromName(Tabla.Rows(iT)("color"))
                        e.Row.ForeColor = Drawing.Color.White

                        iC = iC + 1
                    Next iT
                End If

                For iV = 0 To 1
                    e.Row.Cells(iV).BackColor = Drawing.Color.SteelBlue
                Next

                e.Row.Cells(45).BackColor = Drawing.Color.SteelBlue
                e.Row.Cells(46).BackColor = Drawing.Color.SteelBlue

                Tabla.Dispose()
            Next i
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = "Cantidad de productos catalogados"

            For iTiendas = 2 To 46
                e.Row.Cells(iTiendas).Text = Suma(iTiendas)
            Next
        End If
    End Sub

    Private Sub gridReporte6_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte6.RowDataBound
        Dim Dato(50), Suma(50) As Integer

        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(0).Text <> " " Then
                For iTiendas = 2 To 46
                    'Cuenta
                    If e.Row.Cells(iTiendas).Text <> 0 Then
                        Dato(iTiendas) = 1
                        Suma(iTiendas) = Suma(iTiendas) + Dato(iTiendas)
                    End If
                Next iTiendas

                Dim Porcentaje As String
                Porcentaje = e.Row.Cells(46).Text
                e.Row.Cells(46).Text = e.Row.Cells(46).Text & "%"
            End If

            If e.Row.Cells(0).Text = " " Then
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                                       "SELECT CAD.id_grupo, CAD.nombre_cadena, GCAD.color FROM Cadenas_Tiendas as CAD " & _
                                "INNER JOIN Cadenas_Grupo as GCAD ON GCAD.id_grupo = CAD.id_grupo " & _
                                "WHERE CAD.catalogacion <>'' ORDER BY CAD.catalogacion")
                If Tabla.Rows.Count > 0 Then
                    Dim iC As Integer
                    iC = 2

                    For iT = 0 To Tabla.Rows.Count - 1
                        e.Row.Cells(iC).BackColor = System.Drawing.Color.FromName(Tabla.Rows(iT)("color"))
                        e.Row.ForeColor = Drawing.Color.White
                        e.Row.HorizontalAlign = HorizontalAlign.Center
                        iC = iC + 1
                    Next iT
                End If

                For i = 0 To 1
                    e.Row.Cells(i).BackColor = Drawing.Color.SteelBlue
                Next

                e.Row.Cells(45).BackColor = Drawing.Color.SteelBlue
                e.Row.Cells(46).BackColor = Drawing.Color.SteelBlue
                e.Row.Font.Bold = True

                Tabla.Dispose()
            End If
        End If

        If e.Row.RowType = DataControlRowType.Header Then
            For i = 0 To CInt(gridReporte2.Rows.Count) - 0
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                                       "SELECT CAD.id_grupo, CAD.nombre_cadena, GCAD.color FROM Cadenas_Tiendas as CAD " & _
                                "INNER JOIN Cadenas_Grupo as GCAD ON GCAD.id_grupo = CAD.id_grupo " & _
                                "WHERE CAD.catalogacion <>'' ORDER BY CAD.catalogacion")
                If Tabla.Rows.Count > 0 Then
                    Dim iC As Integer
                    iC = 2

                    For iT = 0 To Tabla.Rows.Count - 1
                        e.Row.Cells(iC).BackColor = System.Drawing.Color.FromName(Tabla.Rows(iT)("color"))
                        e.Row.ForeColor = Drawing.Color.White

                        iC = iC + 1
                    Next iT
                End If

                For iV = 0 To 1
                    e.Row.Cells(iV).BackColor = Drawing.Color.SteelBlue
                Next

                e.Row.Cells(45).BackColor = Drawing.Color.SteelBlue
                e.Row.Cells(46).BackColor = Drawing.Color.SteelBlue

                Tabla.Dispose()
            Next i
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = "Cantidad de productos catalogados"

            For iTiendas = 2 To 46
                e.Row.Cells(iTiendas).Text = Suma(iTiendas)
            Next
        End If
    End Sub

    Private Sub gridReporte7_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte7.RowDataBound
        Dim Dato(50), Suma(50) As Integer

        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(0).Text <> " " Then
                For iTiendas = 2 To 46
                    'Cuenta
                    If e.Row.Cells(iTiendas).Text <> 0 Then
                        Dato(iTiendas) = 1
                        Suma(iTiendas) = Suma(iTiendas) + Dato(iTiendas)
                    End If
                Next iTiendas

                Dim Porcentaje As String
                Porcentaje = e.Row.Cells(46).Text
                e.Row.Cells(46).Text = e.Row.Cells(46).Text & "%"
            End If

            If e.Row.Cells(0).Text = " " Then
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                                       "SELECT CAD.id_grupo, CAD.nombre_cadena, GCAD.color FROM Cadenas_Tiendas as CAD " & _
                                "INNER JOIN Cadenas_Grupo as GCAD ON GCAD.id_grupo = CAD.id_grupo " & _
                                "WHERE CAD.catalogacion <>'' ORDER BY CAD.catalogacion")
                If Tabla.Rows.Count > 0 Then
                    Dim iC As Integer
                    iC = 2

                    For iT = 0 To Tabla.Rows.Count - 1
                        e.Row.Cells(iC).BackColor = System.Drawing.Color.FromName(Tabla.Rows(iT)("color"))
                        e.Row.ForeColor = Drawing.Color.White
                        e.Row.HorizontalAlign = HorizontalAlign.Center
                        iC = iC + 1
                    Next iT
                End If

                For i = 0 To 1
                    e.Row.Cells(i).BackColor = Drawing.Color.SteelBlue
                Next

                e.Row.Cells(45).BackColor = Drawing.Color.SteelBlue
                e.Row.Cells(46).BackColor = Drawing.Color.SteelBlue
                e.Row.Font.Bold = True

                Tabla.Dispose()
            End If
        End If

        If e.Row.RowType = DataControlRowType.Header Then
            For i = 0 To CInt(gridReporte2.Rows.Count) - 0
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                                       "SELECT CAD.id_grupo, CAD.nombre_cadena, GCAD.color FROM Cadenas_Tiendas as CAD " & _
                                "INNER JOIN Cadenas_Grupo as GCAD ON GCAD.id_grupo = CAD.id_grupo " & _
                                "WHERE CAD.catalogacion <>'' ORDER BY CAD.catalogacion")
                If Tabla.Rows.Count > 0 Then
                    Dim iC As Integer
                    iC = 2

                    For iT = 0 To Tabla.Rows.Count - 1
                        e.Row.Cells(iC).BackColor = System.Drawing.Color.FromName(Tabla.Rows(iT)("color"))
                        e.Row.ForeColor = Drawing.Color.White

                        iC = iC + 1
                    Next iT
                End If

                For iV = 0 To 1
                    e.Row.Cells(iV).BackColor = Drawing.Color.SteelBlue
                Next

                e.Row.Cells(45).BackColor = Drawing.Color.SteelBlue
                e.Row.Cells(46).BackColor = Drawing.Color.SteelBlue

                Tabla.Dispose()
            Next i
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = "Cantidad de productos catalogados"

            For iTiendas = 2 To 46
                e.Row.Cells(iTiendas).Text = Suma(iTiendas)
            Next
        End If
    End Sub
End Class