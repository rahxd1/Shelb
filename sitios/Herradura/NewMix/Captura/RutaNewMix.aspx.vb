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

Partial Public Class RutaNewMix
    Inherits System.Web.UI.Page

    Dim PeriodoActivoSiNo As Integer
    Dim PromotorSQL, RegionSQL, RegionSel As String

    Sub SQLCombo()
        RegionSel = Acciones.Slc.cmb("US.id_region", cmbRegion.SelectedValue)

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Regiones as REG WHERE id_region<>0 " & _
                    " ORDER BY REG.nombre_region"

        PromotorSQL = "SELECT distinct RUT.id_usuario FROM NewMix_CatRutas as RUT " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario = RUT.id_usuario " & _
                    "WHERE RUT.id_usuario<>'' " + RegionSel + " " & _
                    "ORDER BY RUT.id_usuario"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)

            VerPermisos(ConexionHerradura.localSqlServer, HttpContext.Current.User.Identity.Name)

            PeriodoActual()

            If Tipo_usuario <> 1 Then
                panelMenu.Visible = True
                ''//Cargar promotor
                Combo.LlenaDrop(ConexionHerradura.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
                ''//Cargar Periodo Promotor Supervisor
                Combo.LlenaDrop(ConexionHerradura.localSqlServer, "SELECT * FROM NewMix_Periodos ORDER BY id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)
                CargarRuta()
            Else
                ''//Cargar periodo promotor
                Combo.LlenaDrop(ConexionHerradura.localSqlServer, "SELECT TOP 4 * FROM NewMix_Periodos order by id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)

                If cmbPeriodo.Text <> "" Then
                    CargarRuta()
                End If
            End If
        End If
    End Sub

    Private Sub PeriodoActual()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionHerradura.localSqlServer, _
                                               "SELECT * FROM NewMix_Periodos " & _
                                               "ORDER BY id_periodo DESC")
        If Tabla.Rows.Count > 0 Then
            cmbPeriodo.SelectedValue = Tabla.Rows(0)("id_periodo")
        End If

        tabla.Dispose()
    End Sub

    Sub CargarRuta()
        VerPermisos(ConexionHerradura.localSqlServer, HttpContext.Current.User.Identity.Name)

        If Tipo_usuario <> 1 Then
            CargaGrilla(ConexionHerradura.localSqlServer, _
                        "execute NewMix_Cargar_Ruta_2 " & cmbPeriodo.SelectedValue & ",'" & cmbPromotor.SelectedValue & "'", _
                        Me.gridRutas)
        Else
            CargaGrilla(ConexionHerradura.localSqlServer, _
                        "execute NewMix_Cargar_Ruta_2 " & cmbPeriodo.SelectedValue & ",'" & HttpContext.Current.User.Identity.Name & "'", _
                        Me.gridRutas)
        End If
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionHerradura.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarRuta()
    End Sub

    Private Sub gridRutas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRutas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            PeriodoActivo()

            If e.Row.Cells(1).Text = "&nbsp;" Then
                e.Row.Cells(3).Text = ""
            End If

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
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionHerradura.localSqlServer, _
                                               "SELECT * FROM NewMix_Periodos " & _
                                               "where id_periodo =" & cmbPeriodo.Text & "")
        Dim Fecha_Inicio, Fecha_Fin As Date
        If tabla.Rows.Count > 0 Then
            Fecha_Inicio = tabla.Rows(0)("fecha_inicio")
            Fecha_Fin = tabla.Rows(0)("fecha_cierre")
        End If

        If Fecha_Inicio <= CDate(Date.Today) And Fecha_Fin >= CDate(Date.Today) Then
            PeriodoActivoSiNo = 0 : Else
            PeriodoActivoSiNo = 1 : End If

        tabla.Dispose()
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarRuta()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        CargarRuta()
    End Sub
End Class