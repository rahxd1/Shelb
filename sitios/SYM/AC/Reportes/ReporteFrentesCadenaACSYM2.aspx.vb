Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteFrentesCadenaACSYM2
    Inherits System.Web.UI.Page

    Dim Campos(100), Columnas(100), SumaTotal(100), SumaCampo(100) As String
    Dim TituloCamposCad(100), CampoCad(100) As String
    Dim TituloCampos, CamposCad, SumaTiendas(100), SumaTotalTiendas, SumaCampos, CampoPorcentaje As String
    Dim SeleccionCampos, SeleccionColumna, SeleccionSuma As String
    Dim CantidadPeriodos As Integer
    Dim Dato(50), Suma(50) As Integer

    Sub SQLCombo()
        Combos_SYMAC.SQLsCombo(cmbPeriodo.SelectedValue, cmbRegion.SelectedValue, _
                                     cmbEstado.SelectedValue, cmbSupervisor.SelectedValue, _
                                     cmbCiudad.SelectedValue, cmbPromotor.SelectedValue, _
                                     "", "View_SYM_Anaquel_Historial")
    End Sub

    Public Function SeleccionPeriodos(ByVal Producto As Integer) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT * FROM Cadenas_Tiendas WHERE catalogacion <>'' " & _
                                  "ORDER BY catalogacion")
        If Tabla.Rows.Count > 0 Then
            CantidadPeriodos = Tabla.Rows.Count

            For i = 0 To Tabla.Rows.Count - 1
                Columnas(i) = ",round(ISNULL((([" & Tabla.Rows(i)("id_cadena") & "])),0),2)'" & Tabla.Rows(i)("nombre_cadena") & "'"
                If i = 0 Then
                    Campos(0) = "[" & Tabla.Rows(0)("id_cadena") & "]" : Else
                    Campos(i) = ",[" & Tabla.Rows(i)("id_cadena") & "]" : End If

                SeleccionColumna = SeleccionColumna + Columnas(i)
                SeleccionCampos = SeleccionCampos + Campos(i)
            Next
        End If

        Tabla.Dispose()
    End Function

    Public Function VerGrilla(ByVal Grilla As GridView, ByVal Producto As Integer, ByVal NombreProducto As String) As Integer
        Dim RegionSQL, EstadoSQL, SupervisorSQL, CiudadSQL, PromotorSQL As String

        SeleccionCampos = ""
        SeleccionColumna = ""
        SumaCampos = ""

        RegionSQL = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)
        EstadoSQL = Acciones.Slc.cmb("id_estado", cmbEstado.SelectedValue)
        SupervisorSQL = Acciones.Slc.cmb("id_supervisor", cmbSupervisor.SelectedValue)
        CiudadSQL = Acciones.Slc.cmb("ciudad", cmbCiudad.SelectedValue)
        PromotorSQL = Acciones.Slc.cmb("id_usuario", cmbPromotor.SelectedValue)

        SeleccionPeriodos(Producto)

        CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT nombre_region as 'Región', nombre_linea as '" & NombreProducto & "' " & _
                    " " + SeleccionColumna + " " & _
                    "FROM (SELECT id_cadena,nombre_region,id_linea,nombre_linea,frentes " & _
                    "FROM View_SYM_Anaquel_Historial  " & _
                    "WHERE tipo_grupo=" & Producto & " AND id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                    " " + RegionSQL + " " & _
                    " " + EstadoSQL + " " & _
                    " " + SupervisorSQL + " " & _
                    " " + CiudadSQL + " " & _
                    " " + PromotorSQL + ")as HDET " & _
                    "PIVOT (SUM(frentes)FOR id_cadena IN " & _
                    "(" & SeleccionCampos & ")) AS H " & _
                    " ORDER BY nombre_linea ", Grilla)
    End Function

    Sub CargarReporte()
        If cmbPeriodo.SelectedValue <> "" Then
            VerGrilla(gridReporte, 101, "DETERGENTES MULTIUSOS")
            VerGrilla(gridReporte2, 103, "JABON DE LAVANDERIA")
            VerGrilla(gridReporte3, 104, "JABONES DE TOCADOR")
            VerGrilla(gridReporte4, 110, "LAVANDERÍA LÍQUIDO")
            VerGrilla(gridReporte5, 105, "LAVATRASTES")
            VerGrilla(gridReporte6, 111, "LIQUIDO TOCADOR")
        Else
            gridReporte.Visible = False
            gridReporte2.Visible = False
            gridReporte3.Visible = False
            gridReporte4.Visible = False
            gridReporte5.Visible = False
            gridReporte6.Visible = False
        End If
    End Sub

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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Porcentaje participacion por producto por Cadena " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(0).Text = " " Then
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                                       "SELECT CAD.id_grupo, CAD.nombre_cadena, GCAD.color FROM Cadenas_Tiendas as CAD " & _
                                "INNER JOIN Cadenas_Grupo as GCAD ON GCAD.id_grupo = CAD.id_grupo " & _
                                "WHERE CAD.catalogacion <>'' ORDER BY CAD.catalogacion")
                If Tabla.Rows.Count > 0 Then
                    For i = 2 To 44
                        e.Row.Cells(i).HorizontalAlign = HorizontalAlign.Center : Next i

                    Dim iC As Integer
                    iC = 1

                    For iT = 0 To Tabla.Rows.Count - 1
                        e.Row.Cells(iC).BackColor = System.Drawing.Color.FromName(Tabla.Rows(iT)("color"))
                        e.Row.ForeColor = Drawing.Color.White
                        e.Row.HorizontalAlign = HorizontalAlign.Center
                        iC = iC + 1
                    Next iT
                End If

                e.Row.Cells(1).BackColor = Drawing.Color.SteelBlue
                e.Row.Cells(44).BackColor = Drawing.Color.SteelBlue
                e.Row.Font.Bold = True

                Tabla.Dispose()
            End If
        End If

        If e.Row.RowType = DataControlRowType.Header Then
            For i = 0 To CInt(gridReporte.Rows.Count) - 0
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                                       "SELECT CAD.id_grupo, CAD.nombre_cadena, GCAD.color FROM Cadenas_Tiendas as CAD " & _
                                "INNER JOIN Cadenas_Grupo as GCAD ON GCAD.id_grupo = CAD.id_grupo " & _
                                "WHERE CAD.catalogacion <>'' ORDER BY CAD.catalogacion")
                If Tabla.Rows.Count > 0 Then
                    Dim iC As Integer
                    iC = 1

                    For iT = 0 To Tabla.Rows.Count - 1
                        e.Row.Cells(iC).BackColor = System.Drawing.Color.FromName(Tabla.Rows(iT)("color"))
                        e.Row.ForeColor = Drawing.Color.White
                        e.Row.HorizontalAlign = HorizontalAlign.Center
                        iC = iC + 1
                    Next iT
                End If

                e.Row.Cells(1).BackColor = Drawing.Color.SteelBlue
                e.Row.Cells(44).BackColor = Drawing.Color.SteelBlue

                Tabla.Dispose()
            Next i
        End If
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

    Private Sub gridReporte2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            If e.Row.Cells(0).Text = " " Then
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                                       "SELECT CAD.id_grupo, CAD.nombre_cadena, GCAD.color FROM Cadenas_Tiendas as CAD " & _
                                "INNER JOIN Cadenas_Grupo as GCAD ON GCAD.id_grupo = CAD.id_grupo " & _
                                "WHERE CAD.catalogacion <>'' ORDER BY CAD.catalogacion")
                If Tabla.Rows.Count > 0 Then
                    For i = 2 To 44
                        e.Row.Cells(i).HorizontalAlign = HorizontalAlign.Center : Next i

                    Dim iC As Integer
                    iC = 1

                    For iT = 0 To Tabla.Rows.Count - 1
                        e.Row.Cells(iC).BackColor = System.Drawing.Color.FromName(Tabla.Rows(iT)("color"))
                        e.Row.ForeColor = Drawing.Color.White
                        e.Row.HorizontalAlign = HorizontalAlign.Center
                        iC = iC + 1
                    Next iT
                End If

                e.Row.Cells(1).BackColor = Drawing.Color.SteelBlue
                e.Row.Cells(44).BackColor = Drawing.Color.SteelBlue
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
                    iC = 1

                    For iT = 0 To Tabla.Rows.Count - 1
                        e.Row.Cells(iC).BackColor = System.Drawing.Color.FromName(Tabla.Rows(iT)("color"))
                        e.Row.ForeColor = Drawing.Color.White

                        iC = iC + 1
                    Next iT
                End If

                e.Row.Cells(1).BackColor = Drawing.Color.SteelBlue
                e.Row.Cells(44).BackColor = Drawing.Color.SteelBlue

                Tabla.Dispose()
            Next i
        End If
    End Sub

    Private Sub gridReporte3_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte3.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(0).Text = " " Then
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                                       "SELECT CAD.id_grupo, CAD.nombre_cadena, GCAD.color FROM Cadenas_Tiendas as CAD " & _
                                "INNER JOIN Cadenas_Grupo as GCAD ON GCAD.id_grupo = CAD.id_grupo " & _
                                "WHERE CAD.catalogacion <>'' ORDER BY CAD.catalogacion")
                If Tabla.Rows.Count > 0 Then
                    For i = 2 To 44
                        e.Row.Cells(i).HorizontalAlign = HorizontalAlign.Center : Next i

                    Dim iC As Integer
                    iC = 1

                    For iT = 0 To Tabla.Rows.Count - 1
                        e.Row.Cells(iC).BackColor = System.Drawing.Color.FromName(Tabla.Rows(iT)("color"))
                        e.Row.ForeColor = Drawing.Color.White
                        e.Row.HorizontalAlign = HorizontalAlign.Center
                        iC = iC + 1
                    Next iT
                End If

                e.Row.Cells(1).BackColor = Drawing.Color.SteelBlue
                e.Row.Cells(44).BackColor = Drawing.Color.SteelBlue
                e.Row.Font.Bold = True

                Tabla.Dispose()
            End If
        End If

        If e.Row.RowType = DataControlRowType.Header Then
            For i = 0 To CInt(gridReporte3.Rows.Count) - 0
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                                       "SELECT CAD.id_grupo, CAD.nombre_cadena, GCAD.color FROM Cadenas_Tiendas as CAD " & _
                                "INNER JOIN Cadenas_Grupo as GCAD ON GCAD.id_grupo = CAD.id_grupo " & _
                                "WHERE CAD.catalogacion <>'' ORDER BY CAD.catalogacion")
                If Tabla.Rows.Count > 0 Then
                    Dim iC As Integer
                    iC = 1

                    For iT = 0 To Tabla.Rows.Count - 1
                        e.Row.Cells(iC).BackColor = System.Drawing.Color.FromName(Tabla.Rows(iT)("color"))
                        e.Row.ForeColor = Drawing.Color.White

                        iC = iC + 1
                    Next iT
                End If

                e.Row.Cells(1).BackColor = Drawing.Color.SteelBlue
                e.Row.Cells(44).BackColor = Drawing.Color.SteelBlue

                Tabla.Dispose()
            Next i
        End If
    End Sub

    Private Sub gridReporte4_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte4.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(0).Text = " " Then
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                                       "SELECT CAD.id_grupo, CAD.nombre_cadena, GCAD.color FROM Cadenas_Tiendas as CAD " & _
                                "INNER JOIN Cadenas_Grupo as GCAD ON GCAD.id_grupo = CAD.id_grupo " & _
                                "WHERE CAD.catalogacion <>'' ORDER BY CAD.catalogacion")
                If Tabla.Rows.Count > 0 Then
                    For i = 2 To 44
                        e.Row.Cells(i).HorizontalAlign = HorizontalAlign.Center : Next i

                    Dim iC As Integer
                    iC = 1

                    For iT = 0 To Tabla.Rows.Count - 1
                        e.Row.Cells(iC).BackColor = System.Drawing.Color.FromName(Tabla.Rows(iT)("color"))
                        e.Row.ForeColor = Drawing.Color.White
                        e.Row.HorizontalAlign = HorizontalAlign.Center
                        iC = iC + 1
                    Next iT
                End If

                e.Row.Cells(1).BackColor = Drawing.Color.SteelBlue
                e.Row.Cells(44).BackColor = Drawing.Color.SteelBlue
                e.Row.Font.Bold = True

                Tabla.Dispose()
            End If
        End If

        If e.Row.RowType = DataControlRowType.Header Then
            For i = 0 To CInt(gridReporte4.Rows.Count) - 0
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                                       "SELECT CAD.id_grupo, CAD.nombre_cadena, GCAD.color FROM Cadenas_Tiendas as CAD " & _
                                "INNER JOIN Cadenas_Grupo as GCAD ON GCAD.id_grupo = CAD.id_grupo " & _
                                "WHERE CAD.catalogacion <>'' ORDER BY CAD.catalogacion")
                If Tabla.Rows.Count > 0 Then
                    Dim iC As Integer
                    iC = 1

                    For iT = 0 To Tabla.Rows.Count - 1
                        e.Row.Cells(iC).BackColor = System.Drawing.Color.FromName(Tabla.Rows(iT)("color"))
                        e.Row.ForeColor = Drawing.Color.White

                        iC = iC + 1
                    Next iT
                End If

                e.Row.Cells(1).BackColor = Drawing.Color.SteelBlue
                e.Row.Cells(44).BackColor = Drawing.Color.SteelBlue

                Tabla.Dispose()
            Next i
        End If
    End Sub

    Private Sub gridReporte5_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte5.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(0).Text = " " Then
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                                       "SELECT CAD.id_grupo, CAD.nombre_cadena, GCAD.color FROM Cadenas_Tiendas as CAD " & _
                                "INNER JOIN Cadenas_Grupo as GCAD ON GCAD.id_grupo = CAD.id_grupo " & _
                                "WHERE CAD.catalogacion <>'' ORDER BY CAD.catalogacion")
                If Tabla.Rows.Count > 0 Then
                    For i = 2 To 44
                        e.Row.Cells(i).HorizontalAlign = HorizontalAlign.Center : Next i

                    Dim iC As Integer
                    iC = 1

                    For iT = 0 To Tabla.Rows.Count - 1
                        e.Row.Cells(iC).BackColor = System.Drawing.Color.FromName(Tabla.Rows(iT)("color"))
                        e.Row.ForeColor = Drawing.Color.White
                        e.Row.HorizontalAlign = HorizontalAlign.Center
                        iC = iC + 1
                    Next iT
                End If

                e.Row.Cells(1).BackColor = Drawing.Color.SteelBlue
                e.Row.Cells(44).BackColor = Drawing.Color.SteelBlue
                e.Row.Font.Bold = True

                Tabla.Dispose()
            End If
        End If

        If e.Row.RowType = DataControlRowType.Header Then
            For i = 0 To CInt(gridReporte5.Rows.Count) - 0
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                                       "SELECT CAD.id_grupo, CAD.nombre_cadena, GCAD.color FROM Cadenas_Tiendas as CAD " & _
                                "INNER JOIN Cadenas_Grupo as GCAD ON GCAD.id_grupo = CAD.id_grupo " & _
                                "WHERE CAD.catalogacion <>'' ORDER BY CAD.catalogacion")
                If Tabla.Rows.Count > 0 Then
                    Dim iC As Integer
                    iC = 1

                    For iT = 0 To Tabla.Rows.Count - 1
                        e.Row.Cells(iC).BackColor = System.Drawing.Color.FromName(Tabla.Rows(iT)("color"))
                        e.Row.ForeColor = Drawing.Color.White

                        iC = iC + 1
                    Next iT
                End If

                e.Row.Cells(1).BackColor = Drawing.Color.SteelBlue
                e.Row.Cells(44).BackColor = Drawing.Color.SteelBlue

                Tabla.Dispose()
            Next i
        End If
    End Sub

    Private Sub gridReporte6_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte6.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(0).Text = " " Then
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                                       "SELECT CAD.id_grupo, CAD.nombre_cadena, GCAD.color FROM Cadenas_Tiendas as CAD " & _
                                "INNER JOIN Cadenas_Grupo as GCAD ON GCAD.id_grupo = CAD.id_grupo " & _
                                "WHERE CAD.catalogacion <>'' ORDER BY CAD.catalogacion")
                If Tabla.Rows.Count > 0 Then
                    For i = 2 To 44
                        e.Row.Cells(i).HorizontalAlign = HorizontalAlign.Center : Next i

                    Dim iC As Integer
                    iC = 1

                    For iT = 0 To Tabla.Rows.Count - 1
                        e.Row.Cells(iC).BackColor = System.Drawing.Color.FromName(Tabla.Rows(iT)("color"))
                        e.Row.ForeColor = Drawing.Color.White
                        e.Row.HorizontalAlign = HorizontalAlign.Center
                        iC = iC + 1
                    Next iT
                End If

                e.Row.Cells(1).BackColor = Drawing.Color.SteelBlue
                e.Row.Cells(44).BackColor = Drawing.Color.SteelBlue
                e.Row.Font.Bold = True

                Tabla.Dispose()
            End If
        End If

        If e.Row.RowType = DataControlRowType.Header Then
            For i = 0 To CInt(gridReporte6.Rows.Count) - 0
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                                       "SELECT CAD.id_grupo, CAD.nombre_cadena, GCAD.color FROM Cadenas_Tiendas as CAD " & _
                                "INNER JOIN Cadenas_Grupo as GCAD ON GCAD.id_grupo = CAD.id_grupo " & _
                                "WHERE CAD.catalogacion <>'' ORDER BY CAD.catalogacion")
                If Tabla.Rows.Count > 0 Then
                    Dim iC As Integer
                    iC = 1

                    For iT = 0 To Tabla.Rows.Count - 1
                        e.Row.Cells(iC).BackColor = System.Drawing.Color.FromName(Tabla.Rows(iT)("color"))
                        e.Row.ForeColor = Drawing.Color.White

                        iC = iC + 1
                    Next iT
                End If

                e.Row.Cells(1).BackColor = Drawing.Color.SteelBlue
                e.Row.Cells(44).BackColor = Drawing.Color.SteelBlue

                Tabla.Dispose()
            Next i
        End If
    End Sub

    Private Sub gridReporte7_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte7.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(0).Text = " " Then
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                                       "SELECT CAD.id_grupo, CAD.nombre_cadena, GCAD.color FROM Cadenas_Tiendas as CAD " & _
                                "INNER JOIN Cadenas_Grupo as GCAD ON GCAD.id_grupo = CAD.id_grupo " & _
                                "WHERE CAD.catalogacion <>'' ORDER BY CAD.catalogacion")
                If Tabla.Rows.Count > 0 Then
                    For i = 2 To 44
                        e.Row.Cells(i).HorizontalAlign = HorizontalAlign.Center : Next i

                    Dim iC As Integer
                    iC = 1

                    For iT = 0 To Tabla.Rows.Count - 1
                        e.Row.Cells(iC).BackColor = System.Drawing.Color.FromName(Tabla.Rows(iT)("color"))
                        e.Row.ForeColor = Drawing.Color.White
                        e.Row.HorizontalAlign = HorizontalAlign.Center
                        iC = iC + 1
                    Next iT
                End If

                e.Row.Cells(1).BackColor = Drawing.Color.SteelBlue
                e.Row.Cells(44).BackColor = Drawing.Color.SteelBlue
                e.Row.Font.Bold = True

                Tabla.Dispose()
            End If
        End If

        If e.Row.RowType = DataControlRowType.Header Then
            For i = 0 To CInt(gridReporte7.Rows.Count) - 0
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                                       "SELECT CAD.id_grupo, CAD.nombre_cadena, GCAD.color FROM Cadenas_Tiendas as CAD " & _
                                "INNER JOIN Cadenas_Grupo as GCAD ON GCAD.id_grupo = CAD.id_grupo " & _
                                "WHERE CAD.catalogacion <>'' ORDER BY CAD.catalogacion")
                If Tabla.Rows.Count > 0 Then
                    Dim iC As Integer
                    iC = 1

                    For iT = 0 To Tabla.Rows.Count - 1
                        e.Row.Cells(iC).BackColor = System.Drawing.Color.FromName(Tabla.Rows(iT)("color"))
                        e.Row.ForeColor = Drawing.Color.White

                        iC = iC + 1
                    Next iT
                End If

                e.Row.Cells(1).BackColor = Drawing.Color.SteelBlue
                e.Row.Cells(44).BackColor = Drawing.Color.SteelBlue

                Tabla.Dispose()
            Next i
        End If
    End Sub
End Class