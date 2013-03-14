Public Partial Class RutasSYMDemos2013
    Inherits System.Web.UI.Page
    Dim PeriodoSQL, RegionSQL, RegionSel, PromotorSQL, Usuario As String
    Dim Hoy As Date
    Dim PeriodoActivoSiNo As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            VerPermisos(ConexionSYM.localSqlServer, HttpContext.Current.User.Identity.Name)
            PeriodoActual()

            If Tipo_usuario = 10 Or Tipo_usuario = 12 Or Tipo_usuario = 2 Then
                Combo.LlenaDrop(ConexionSYM.localSqlServer, "SELECT TOP 4 * FROM SYM_D_Periodos order by id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)

               
            Else
                pnlMenu.Visible = True
                Combo.LlenaDrop(ConexionSYM.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
                Combo.LlenaDrop(ConexionSYM.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo)
                Combo.LlenaDrop(ConexionSYM.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)


            End If

            CargarRuta()
        End If
    End Sub
    Sub SQLCombo()
        'RegionSel = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)

        PromotorSQL = "SELECT DISTINCT id_usuario " & _
                    "FROM SYM_D_Cat_Rutas as RUT " & _
                    "INNER JOIN SYM_D_Tiendas as TI ON TI.id_tienda = RUT.id_tienda " & _
                    "WHERE id_usuario<>'' " + RegionSel + " order by id_usuario"

        PeriodoSQL = "SELECT * FROM SYM_D_Periodos ORDER BY id_periodo DESC"

        RegionSQL = "SELECT DISTINCT REG.id_region, REG.nombre_region " & _
                    "FROM SYM_D_Tiendas as TI " & _
                    "INNER JOIN SYM_D_CAT_RUTAS as RUT ON RUT.id_tienda=TI.id_tienda " & _
                    "INNER JOIN Regiones as REG ON REG.id_region=TI.id_region " & _
                    "ORDER BY nombre_region"
    End Sub

    Private Sub PeriodoActual()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT * FROM SYM_D_Periodos ORDER BY id_periodo DESC")
        If Tabla.Rows.Count > 0 Then
            cmbPeriodo.SelectedValue = Tabla.Rows(0)("id_periodo")
        End If

        Tabla.Dispose()
    End Sub


    Sub CargarRuta()
        If Not cmbPeriodo.SelectedValue = "" Or cmbPeriodo.SelectedValue = " " Then
            If Tipo_usuario = 10 Then
                Usuario = HttpContext.Current.User.Identity.Name : Else
                Usuario = cmbPromotor.SelectedValue : End If

            CargaGrilla(ConexionSYM.localSqlServer, _
                        "execute SYM_D_Cargar_Ruta " & cmbPeriodo.SelectedValue & ",'" & Usuario & "'", _
                        gridRutas)
        Else

            lblAviso.Text = "<--selecciona"
        End If

    End Sub

    Private Sub gridRUTAS_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRutas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            PeriodoActivo()

            If PeriodoActivoSiNo = 1 Then
                e.Row.Cells(0).Text = "<img src='../../../../Img/Cerrado.png' />"

                If Tipo_usuario = 1 Then
                    e.Row.Cells(3).Text = "Tienda Cerrada"
                    e.Row.Cells(4).Text = "Tienda Cerrada"
                End If
            End If
        End If
    End Sub

    Private Sub PeriodoActivo()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT * FROM SYM_D_Periodos " & _
                                               "where id_periodo =" & cmbPeriodo.Text & "")
        Dim Fecha_Inicio, Fecha_Fin As Date
        If Tabla.Rows.Count > 0 Then
            Fecha_Inicio = Tabla.Rows(0)("fecha_de_inicio")
            Fecha_Fin = Tabla.Rows(0)("fecha_cierre")
        End If

        If Fecha_Inicio <= CDate(Date.Today) _
            And Fecha_Fin >= CDate(Date.Today) Then
            PeriodoActivoSiNo = 0
        Else
            PeriodoActivoSiNo = 1
        End If

        Tabla.Dispose()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        CargarRuta()
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarRuta()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionSYM.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarRuta()
    End Sub

End Class