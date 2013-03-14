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

Partial Public Class RutaSYMAC
    Inherits System.Web.UI.Page

    Dim PeriodoSQL, RegionSQL, RegionSel, PromotorSQL, Usuario As String
    Dim Hoy As Date
    Dim PeriodoActivoSiNo As Integer

    Sub SQLCombo()
        RegionSel = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)

        PromotorSQL = "SELECT DISTINCT id_usuario " & _
                    "FROM AC_CatRutas as RUT " & _
                    "INNER JOIN AC_Tiendas as TI ON TI.id_tienda = RUT.id_tienda " & _
                    "WHERE id_usuario<>'' " + RegionSel + " order by id_usuario"

        PeriodoSQL = "SELECT * FROM AC_Periodos ORDER BY id_periodo DESC"

        RegionSQL = "SELECT DISTINCT REG.id_region, REG.nombre_region " & _
                    "FROM AC_Tiendas as TI " & _
                    "INNER JOIN AC_CatRutas as RUT ON RUT.id_tienda=TI.id_tienda " & _
                    "INNER JOIN Regiones as REG ON REG.id_region=TI.id_region " & _
                    "ORDER BY nombre_region"
    End Sub

    Private Sub PeriodoActual()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT * FROM AC_Periodos ORDER BY id_periodo DESC")
        If Tabla.Rows.Count > 0 Then
            cmbPeriodo.SelectedValue = Tabla.Rows(0)("id_periodo")
        End If

        Tabla.Dispose()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            VerPermisos(ConexionSYM.localSqlServer, HttpContext.Current.User.Identity.Name)
            PeriodoActual()

            If Tipo_usuario <> 1 Then
                pnlMenu.Visible = True
                Combo.LlenaDrop(ConexionSYM.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
                Combo.LlenaDrop(ConexionSYM.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo)
                Combo.LlenaDrop(ConexionSYM.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Else
                ''//Cargar periodo promotor
                Combo.LlenaDrop(ConexionSYM.localSqlServer, "SELECT TOP 4 * FROM AC_Periodos order by id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)
            End If

            CargarRuta()
        End If
    End Sub

    Sub CargarRuta()
        If Tipo_usuario = 1 Then
            Usuario = HttpContext.Current.User.Identity.Name : Else
            Usuario = cmbPromotor.SelectedValue : End If

        CargaGrilla(ConexionSYM.localSqlServer, _
                    "execute AC_Cargar_Ruta " & cmbPeriodo.SelectedValue & ",'" & Usuario & "'", _
                    gridRutas)
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
                                               "SELECT * FROM AC_Periodos " & _
                                               "where id_periodo =" & cmbPeriodo.SelectedValue & "")
        Dim Fecha_Inicio, Fecha_Fin As Date
        If tabla.Rows.Count > 0 Then
            Fecha_Inicio = tabla.Rows(0)("fecha_inicio")
            Fecha_Fin = tabla.Rows(0)("fecha_cierre")
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