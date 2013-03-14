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
Imports procomlcd.Permisos

Partial Public Class RutaMarsAS
    Inherits System.Web.UI.Page

    Dim fecha_extra As Date
    Dim Periodo, Quincena As String
    Dim PeriodoActivoSiNo, PeriodoActivoSiNo1, PeriodoActivoSiNo2 As Integer
    Dim TiendasCapturadasQ1, TiendasCapturadasQ2 As Integer
    Dim EjecutivoSQL, PromotorSQL, RegionSQL As String
    Dim RegionSel, EjecutivoSel, Supervisor As String

    Sub SQLCombo()
        If Tipo_usuario = 10 Then
            Supervisor = "AND REL.id_supervisor='" & HttpContext.Current.User.Identity.Name & "'" : End If

        If Tipo_usuario = 3 Then
            Supervisor = "AND REL.region_mars='" & HttpContext.Current.User.Identity.Name & "'" : End If

        RegionSel = Acciones.Slc.cmb("REL.id_region", cmbRegion.SelectedValue)
        EjecutivoSel = Acciones.Slc.cmb("REL.region_mars", cmbEjecutivo.SelectedValue)

        EjecutivoSQL = "SELECT distinct region_mars + ' ' + nombre as ejecutivo, region_mars " & _
                    "FROM Usuarios_Relacion as REL " & _
                    "INNER JOIN Usuarios as US ON REL.region_mars = US.ciudad " & _
                    "WHERE REL.id_region<>0 " + RegionSel + Supervisor + ""

        PromotorSQL = "SELECT distinct RUT.id_usuario FROM AS_CatRutas as RUT " & _
                    "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario = RUT.id_usuario " & _
                    "WHERE RUT.id_usuario<>'' " + RegionSel + EjecutivoSel + Supervisor + " " & _
                    "ORDER BY RUT.id_usuario"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Regiones as REG WHERE id_region<>0 " & _
                    " ORDER BY REG.nombre_region"
    End Sub

    Private Sub PeriodoActual()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT * FROM Periodos_Nuevo " & _
                                  "WHERE fecha_inicio_periodo<='" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                                  "AND fecha_fin_periodo>='" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                                  "ORDER BY orden DESC")
        If tabla.Rows.Count > 0 Then
            cmbPeriodo.SelectedValue = tabla.Rows(0)("orden")
        End If

        tabla.Dispose()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionMars.localSqlServer, HttpContext.Current.User.Identity.Name)

            PeriodoActual()

            gridAreaNielsen.Visible = False

            If Tipo_usuario = 1 Then
                gridAreaNielsen.Visible = True
                CargaGrilla(ConexionMars.localSqlServer, "select DISTINCT area, " & _
                                "O_gb, O_gh, O_gs, O_pb, O_pc, O_ph, O_ps " & _
                                "FROM AS_CatRutas as RUT " & _
                                "INNER JOIN AS_Tiendas as TI ON RUT.id_tienda=TI.id_tienda " & _
                                "INNER JOIN Ver_Area_nielsen as AN ON AN.area_nielsen=TI.area_nielsen " & _
                                "WHERE RUT.id_usuario='" & HttpContext.Current.User.Identity.Name & "'", gridAreaNielsen)

                SQLCombo()
                Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
                cmbPromotor.SelectedValue = HttpContext.Current.User.Identity.Name

                ''//Cargar Periodo Promotor 
                Combo.LlenaDrop(ConexionMars.localSqlServer, "SELECT TOP 4 * FROM Periodos_Nuevo " & _
                                "WHERE fecha_fin_periodo<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                                "OR fecha_inicio_periodo<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                                "ORDER BY orden DESC", "nombre_periodo", "orden", cmbPeriodo)
                CargarRuta()
                Exit Sub : End If

            If Tipo_usuario = 10 Then
                panelMenu.Visible = True

                SQLCombo()
                Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

                ''//Cargar Periodo  Supervisor
                Combo.LlenaDrop(ConexionMars.localSqlServer, "SELECT * FROM Periodos_Nuevo " & _
                                "WHERE fecha_fin_periodo<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                                "OR fecha_inicio_periodo<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                                "ORDER BY orden DESC", "nombre_periodo", "orden", cmbPeriodo)

                CargarRuta()
                Exit Sub : End If

            If Tipo_usuario = 3 Then
                panelMenu.Visible = True

                SQLCombo()
                Combo.LlenaDrop(ConexionMars.localSqlServer, EjecutivoSQL, "ejecutivo", "region_mars", cmbEjecutivo)
                Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

                ''//Cargar Periodo Promotor Supervisor
                Combo.LlenaDrop(ConexionMars.localSqlServer, "SELECT * FROM Periodos_Nuevo " & _
                                "WHERE fecha_fin_periodo<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                                "OR fecha_inicio_periodo<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                                "ORDER BY orden DESC", "nombre_periodo", "orden", cmbPeriodo)

                CargarRuta()
                Exit Sub : End If

            If Consulta = 1 Or Tipo_usuario = 12 Then
                panelMenu.Visible = True

                SQLCombo()
                Combo.LlenaDrop(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
                Combo.LlenaDrop(ConexionMars.localSqlServer, EjecutivoSQL, "ejecutivo", "region_mars", cmbEjecutivo)
                Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

                cmbEjecutivo.Items.Insert(0, New ListItem("", ""))

                ''//Cargar Periodo Promotor Supervisor
                Combo.LlenaDrop(ConexionMars.localSqlServer, "SELECT * FROM Periodos_Nuevo " & _
                                "WHERE fecha_fin_periodo<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                                "OR fecha_inicio_periodo<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                                "ORDER BY orden DESC", "nombre_periodo", "orden", cmbPeriodo)

                CargarRuta()
            Else
                Response.Redirect("../MenuMarsAS.aspx")
            End If

        End If
    End Sub

    Sub CargarRuta()
        VerPermisos(ConexionMars.localSqlServer, HttpContext.Current.User.Identity.Name)

        If Tipo_usuario <> 1 Then
            CargaGrilla(ConexionMars.localSqlServer, "execute AS_Cargar_Ruta " & cmbPeriodo.SelectedValue & ",'" & cmbPromotor.SelectedValue & "'", Me.gridRutas)
        Else
            CargaGrilla(ConexionMars.localSqlServer, "execute AS_Cargar_Ruta " & cmbPeriodo.SelectedValue & ",'" & HttpContext.Current.User.Identity.Name & "'", Me.gridRutas)
        End If
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, EjecutivoSQL, "ejecutivo", "region_mars", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarRuta()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        CargarRuta()
        PeriodoActivo()
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()
        CargarRuta()
    End Sub

    Private Sub PeriodoActivo()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT * FROM Periodos_Nuevo " & _
                                  "WHERE orden=" & cmbPeriodo.SelectedValue & "")
        Dim Fecha_Inicio, Fecha_Fin As Date
        If Tabla.Rows.Count > 0 Then
            Fecha_Inicio = Tabla.Rows(0)("fecha_inicio_periodo")
            Fecha_Fin = Tabla.Rows(0)("fecha_fin_periodo")
            Fecha_Fin = Fecha_Fin.AddDays(1)
        End If

        Dim Fecha_InicioQ1, Fecha_FinQ1 As Date
        If Tabla.Rows.Count > 0 Then
            Fecha_InicioQ1 = Tabla.Rows(0)("fecha_inicio_periodo")
            Fecha_FinQ1 = Fecha_InicioQ1.AddDays(14)
            Fecha_FinQ1 = Fecha_FinQ1.AddDays(1)
        End If

        Dim Fecha_InicioQ2, Fecha_FinQ2 As Date
        If Tabla.Rows.Count > 0 Then
            Fecha_InicioQ2 = Fecha_InicioQ1.AddDays(14)
            Fecha_FinQ2 = Tabla.Rows(0)("fecha_fin_periodo")
            Fecha_FinQ2 = Fecha_FinQ2.AddDays(1)
        End If

        If Fecha_Inicio <= CDate(Date.Today) And Fecha_Fin >= CDate(Date.Today) Then
            PeriodoActivoSiNo = 0 : Else
            PeriodoActivoSiNo = 1 : End If

        If Fecha_InicioQ1 <= CDate(Date.Today) And Fecha_FinQ1 >= CDate(Date.Today) Then
            PeriodoActivoSiNo1 = 0 : Else
            PeriodoActivoSiNo1 = 1 : End If

        If Fecha_InicioQ2 <= CDate(Date.Today) And Fecha_FinQ2 >= CDate(Date.Today) Then
            PeriodoActivoSiNo2 = 0 : Else
            PeriodoActivoSiNo2 = 1 : End If

        Tabla.Dispose()
    End Sub

    Private Sub gridRutas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRutas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            PeriodoActivo()

            If PeriodoActivoSiNo = 1 Then
                e.Row.Cells(0).Text = "<img src='../../../../Img/Cerrado.png' />" : End If

            If PeriodoActivoSiNo1 = 1 Then
                If Tipo_usuario = 1 Then
                    If Periodo = cmbPeriodo.SelectedValue And Quincena = "Q1" Then
                        If fecha_extra >= CDate(Date.Today) Then
                        Else
                            e.Row.Cells(5).Text = "Tienda Cerrada"
                            e.Row.Cells(6).Text = "Tienda Cerrada"
                        End If
                    Else
                        e.Row.Cells(5).Text = "Tienda Cerrada"
                        e.Row.Cells(6).Text = "Tienda Cerrada"
                    End If
                End If
            End If

            If PeriodoActivoSiNo2 = 1 Or Quincena = "Q2" Then
                If Tipo_usuario = 1 Then
                    If Periodo = cmbPeriodo.SelectedValue And Quincena = "Q2" Then
                        If fecha_extra >= CDate(Date.Today) Then
                        Else
                            e.Row.Cells(7).Text = "Tienda Cerrada"
                            e.Row.Cells(8).Text = "Tienda Cerrada"
                        End If
                    Else
                        e.Row.Cells(7).Text = "Tienda Cerrada"
                        e.Row.Cells(8).Text = "Tienda Cerrada"
                    End If
                End If
            End If

            If e.Row.Cells(0).Text <> "0" Then
                TiendasCapturadasQ1 = TiendasCapturadasQ1 + 1 : End If

            If e.Row.Cells(1).Text <> "0" Then
                TiendasCapturadasQ2 = TiendasCapturadasQ2 + 1 : End If

            If TiendasCapturadasQ1 = gridRutas.Rows.Count + 1 Then
                lnkReporteFocoQ1.Visible = True : Else
                lnkReporteFocoQ1.Visible = False : End If

            If TiendasCapturadasQ2 = gridRutas.Rows.Count + 1 Then
                lnkReporteFocoQ1.Visible = True : Else
                lnkReporteFocoQ2.Visible = False : End If
        End If
    End Sub

    Private Function ReporteFoco(ByVal Quincena As String) As Boolean
        pnlDetalle2.Visible = True

        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT * FROM Foco_Nuevo " & _
                    "WHERE id_usuario='" & cmbPromotor.SelectedValue & "' " & _
                    "AND orden='" & cmbPeriodo.SelectedValue & "' " & _
                    "AND id_quincena ='" & Quincena & "'", gridDetalle2)
    End Function

    Private Sub lnkReporteFocoQ1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkReporteFocoQ1.Click
        ReporteFoco("Q1")
    End Sub

    Private Sub lnkReporteFocoQ2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkReporteFocoQ2.Click
        ReporteFoco("Q2")
    End Sub

    Private Sub cmbEjecutivo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEjecutivo.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
    End Sub

    Private Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        pnlDetalle2.Visible = False
    End Sub

    Private Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton2.Click
        pnlDetalle2.Visible = False
    End Sub

    Private Sub gridDetalle2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridDetalle2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblPS As Label = CType(e.Row.FindControl("lblPS"), Label)
            Dim lblPC As Label = CType(e.Row.FindControl("lblPC"), Label)
            Dim lblPH As Label = CType(e.Row.FindControl("lblPH"), Label)
            Dim lblPB As Label = CType(e.Row.FindControl("lblPB"), Label)
            Dim lblGS As Label = CType(e.Row.FindControl("lblGS"), Label)
            Dim lblGH As Label = CType(e.Row.FindControl("lblGH"), Label)
            Dim lblGB As Label = CType(e.Row.FindControl("lblGB"), Label)

            If lblPS.Text = "BIEN" Then
                lblPS.BackColor = Drawing.Color.Green : Else
                If lblPS.Text <> "" Then
                    lblPS.BackColor = Drawing.Color.Red : End If : End If
            If lblPC.Text = "BIEN" Then
                lblPC.BackColor = Drawing.Color.Green : Else
                If lblPC.Text <> "" Then
                    lblPC.BackColor = Drawing.Color.Red : End If : End If
            If lblPH.Text = "BIEN" Then
                lblPH.BackColor = Drawing.Color.Green : Else
                If lblPH.Text <> "" Then
                    lblPH.BackColor = Drawing.Color.Red : End If : End If
            If lblPB.Text = "BIEN" Then
                lblPB.BackColor = Drawing.Color.Green : Else
                If lblPB.Text <> "" Then
                    lblPB.BackColor = Drawing.Color.Red : End If : End If
            If lblGS.Text = "BIEN" Then
                lblGS.BackColor = Drawing.Color.Green : Else
                If lblGS.Text <> "" Then
                    lblGS.BackColor = Drawing.Color.Red : End If : End If
            If lblGH.Text = "BIEN" Then
                lblGH.BackColor = Drawing.Color.Green : Else
                If lblGH.Text <> "" Then
                    lblGH.BackColor = Drawing.Color.Red : End If : End If
            If lblGB.Text = "BIEN" Then
                lblGB.BackColor = Drawing.Color.Green : Else
                If lblGB.Text <> "" Then
                    lblGB.BackColor = Drawing.Color.Red : End If : End If

            Dim lblAnaquel1 As Label = CType(e.Row.FindControl("lblAnaquel1"), Label)
            Dim lblAnaquel2 As Label = CType(e.Row.FindControl("lblAnaquel2"), Label)
            Dim lblAnaquel3 As Label = CType(e.Row.FindControl("lblAnaquel3"), Label)
            Dim lblAnaquel4 As Label = CType(e.Row.FindControl("lblAnaquel4"), Label)
            Dim lblPasillo1 As Label = CType(e.Row.FindControl("lblPasillo1"), Label)
            Dim lblPasillo2 As Label = CType(e.Row.FindControl("lblPasillo2"), Label)
            Dim lblPasillo3 As Label = CType(e.Row.FindControl("lblPasillo3"), Label)
            Dim lblCaliente1 As Label = CType(e.Row.FindControl("lblCaliente1"), Label)
            Dim lblCaliente2 As Label = CType(e.Row.FindControl("lblCaliente2"), Label)
            Dim lblCaliente3 As Label = CType(e.Row.FindControl("lblCaliente3"), Label)
            Dim lblCaliente4 As Label = CType(e.Row.FindControl("lblCaliente4"), Label)
            Dim lblES1 As Label = CType(e.Row.FindControl("lblES1"), Label)
            Dim lblES2 As Label = CType(e.Row.FindControl("lblES2"), Label)

            ColorLbl(lblAnaquel1)
            ColorLbl(lblAnaquel2)
            ColorLbl(lblAnaquel3)
            ColorLbl(lblAnaquel4)
            ColorLbl(lblPasillo1)
            ColorLbl(lblPasillo2)
            ColorLbl(lblPasillo3)
            ColorLbl(lblCaliente1)
            ColorLbl(lblCaliente2)
            ColorLbl(lblCaliente3)
            ColorLbl(lblCaliente4)
            ColorLbl(lblES1)
            ColorLbl(lblES2)
        End If
    End Sub

    Private Function ColorLbl(ByVal Etiqueta As Label) As Boolean
        If Etiqueta.Text <> "" Then
            If Etiqueta.Text = "BIEN" Then
                Etiqueta.BackColor = Drawing.Color.Green : Else
                Etiqueta.BackColor = Drawing.Color.Red : End If
        End If
    End Function

    Private Sub gridAreaNielsen_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridAreaNielsen.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.Header
                e.Row.Cells(0).ColumnSpan = 8
                e.Row.Cells(1).Visible = False
                e.Row.Cells(2).Visible = False
                e.Row.Cells(3).Visible = False
                e.Row.Cells(4).Visible = False
                e.Row.Cells(5).Visible = False
                e.Row.Cells(6).Visible = False
                e.Row.Cells(7).Visible = False

                e.Row.Cells(0).Controls.Clear()
                e.Row.Cells(0).Text = Mars_AS.TablaAreaNielsen
        End Select
    End Sub

End Class