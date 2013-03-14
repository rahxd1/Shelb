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

Partial Public Class RutaSYMDemos
    Inherits System.Web.UI.Page

    Dim Hoy As Date, PeriodoActivoSiNo As Integer
    Dim PromotorSQL, RegionSQL, RegionSel As String

    Sub SQLCombo()
        RegionSel = Acciones.Slc.cmb("US.id_region", cmbRegion.SelectedValue)

        PromotorSQL = "SELECT DISTINCT RUT.id_usuario " & _
                    "FROM Demos_CatRutas as RUT " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario =RUT.id_usuario " & _
                    " " + RegionSel + " " & _
                    "order by RUT.id_usuario"

        RegionSQL = "SELECT DISTINCT REG.id_region, REG.nombre_region " & _
                    "FROM Demos_CatRutas as RUT " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario =RUT.id_usuario " & _
                    "INNER JOIN Regiones as REG ON REG.id_region=US.id_region " & _
                    "ORDER BY nombre_region"
    End Sub

    Private Sub PeriodoActual()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT * FROM Demos_Periodos ORDER BY id_periodo DESC")
        If Tabla.Rows.Count > 0 Then
            cmbPeriodo.SelectedValue = Tabla.Rows(0)("id_periodo")
        End If

        tabla.Dispose()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            VerPermisos(ConexionSYM.localSqlServer, HttpContext.Current.User.Identity.Name)

            PeriodoActual()

            If Consulta <> 1 Then
                Response.Redirect("../menu_SYMDemos.aspx")
            End If

            If Tipo_usuario = 10 Or Tipo_usuario = 12 Or Tipo_usuario = 2 Then
                ''//Cargar periodo promotor
                Combo.LlenaDrop(ConexionSYM.localSqlServer, "SELECT * FROM Demos_Periodos order by id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)
            Else

                pnlMenu.Visible = True
                ''//Cargar promotor
                Combo.LlenaDrop(ConexionSYM.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
                ''//Cargar Periodo Promotor Supervisor
                Combo.LlenaDrop(ConexionSYM.localSqlServer, "SELECT * FROM Demos_Periodos ORDER BY id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)
                Combo.LlenaDrop(ConexionSYM.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            End If

            CargarRuta()
        End If
    End Sub

    Sub CargarRuta()
        If cmbPeriodo.SelectedValue <> "" Then
            Dim SQLCargaRuta As String

            If Tipo_usuario = 10 Or Tipo_usuario = 12 Or Tipo_usuario = 2 Then
                SQLCargaRuta = "execute Demos_Cargar_Ruta " & cmbPeriodo.SelectedValue & ",'" & HttpContext.Current.User.Identity.Name & "'"
            Else
                SQLCargaRuta = "execute Demos_Cargar_Ruta " & cmbPeriodo.SelectedValue & ",'" & cmbPromotor.SelectedValue & "'"
            End If

            CargaGrilla(ConexionSYM.localSqlServer, SQLCargaRuta, gridRutas)
        End If
    End Sub

    Private Sub gridRUTAS_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRutas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            PeriodoActivo()

            If e.Row.Cells(1).Text = "&nbsp;" Then
                e.Row.Cells(3).Text = ""
            End If

            If PeriodoActivoSiNo = 1 Then
                e.Row.Cells(0).Text = "<img src='../../../../Img/Cerrado.png' />"

                If Tipo_usuario = 10 Then
                    e.Row.Cells(3).Text = "Tienda Cerrada"
                End If
            End If
        End If
    End Sub

    Private Sub PeriodoActivo()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT * FROM Demos_Periodos where id_periodo =" & cmbPeriodo.SelectedValue & "")
        Dim Fecha_Inicio, Fecha_Fin As Date
        If tabla.Rows.Count > 0 Then
            Fecha_Inicio = tabla.Rows(0)("fecha_inicio")
            Fecha_Fin = tabla.Rows(0)("fecha_cierre")
        End If

        If Fecha_Inicio <= CDate(Date.Today)And Fecha_Fin >= CDate(Date.Today) Then
            PeriodoActivoSiNo = 0
        Else
            PeriodoActivoSiNo = 1
        End If

        tabla.Dispose()
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