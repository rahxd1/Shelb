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

Partial Public Class RutasSupervisorNR
    Inherits System.Web.UI.Page

    Dim Usuario As String
    Dim PeriodoActivoSiNo As Integer
    Dim RegionSel, RegionSQL, PromotorSQL As String

    Private Sub PeriodoActual()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                               "SELECT * FROM NR_Periodos_Sup ORDER BY id_periodo DESC")
        If Tabla.Rows.Count > 0 Then
            cmbPeriodo.SelectedValue = Tabla.Rows(0)("id_periodo")
        End If

        tabla.Dispose()
    End Sub

    Sub SQLCombo()
        RegionSel = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)

        RegionSQL = "SELECT DISTINCT nombre_region, id_region " & _
                    "FROM View_NR_Rutas_Eventos_Sup " & _
                    " ORDER BY nombre_region"

        PromotorSQL = "SELECT DISTINCT id_usuario FROM View_NR_Rutas_Eventos_Sup " & _
                    "WHERE id_usuario<>'' " & _
                    " " + RegionSel + " " & _
                    "ORDER BY id_usuario"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            PeriodoActual()
            VerPermisos(ConexionBerol.localSqlServer, HttpContext.Current.User.Identity.Name)

            SQLCombo()
            Combo.LlenaDrop(ConexionBerol.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

            If Tipo_usuario = 2 Then
                ''//Cargar periodo promotor
                Combo.LlenaDrop(ConexionBerol.localSqlServer, "SELECT TOP 4 * FROM NR_Periodos_Sup order by id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)
                CargarRuta()
                Exit Sub
            End If

            If Tipo_usuario = 12 Then
                panelMenu.Visible = True
                ''//Cargar periodo promotor
                Combo.LlenaDrop(ConexionBerol.localSqlServer, "SELECT TOP 4 * FROM NR_Periodos_Sup order by id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)
                Exit Sub
            End If

            panelMenu.Visible = True
            Combo.LlenaDrop(ConexionBerol.localSqlServer, "SELECT * FROM NR_Periodos_Sup ORDER BY id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)
        End If
    End Sub

    Sub CargarRuta()
        If cmbPeriodo.SelectedValue <> "" Then
            If Tipo_usuario <> 2 Then
                Usuario = cmbPromotor.SelectedValue : Else
                Usuario = HttpContext.Current.User.Identity.Name : End If

            CargaGrilla(ConexionBerol.localSqlServer, _
                        "execute NR_Cargar_Ruta_Supervisor " & cmbPeriodo.SelectedValue & ",'" & Usuario & "'", _
                        gridRutas)
        End If
    End Sub

    Private Sub gridRutas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRutas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            PeriodoActivo()

            If PeriodoActivoSiNo = 1 Then
                e.Row.Cells(0).Text = "<img src='../../../../Img/Cerrado.png' />"
                e.Row.Cells(3).Text = "Tienda Cerrada"
                e.Row.Cells(4).Text = "Tienda Cerrada"
            End If
        End If
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionBerol.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarRuta()
    End Sub

    Private Sub PeriodoActivo()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                               "SELECT * FROM NR_Periodos_Sup " & _
                                               "where id_periodo =" & cmbPeriodo.Text & "")
        Dim Fecha_Inicio, Fecha_Fin As Date
        If Tabla.Rows.Count > 0 Then
            Fecha_Inicio = Tabla.Rows(0)("fecha_inicio")
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

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarRuta()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        CargarRuta()
    End Sub
End Class