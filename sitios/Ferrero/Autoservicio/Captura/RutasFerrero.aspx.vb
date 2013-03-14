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

Partial Public Class RutasFerrero
    Inherits System.Web.UI.Page

    Dim Usuario As String
    Dim PeriodoActivoSiNo As Integer
    Dim RegionSel, RegionSQL, PromotorSQL As String

    Sub SQLCombo()
        RegionSel = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM AS_Rutas_Eventos AS RE " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario= RE.id_usuario " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = US.id_region " & _
                    "WHERE id_proyecto=27 " & _
                    " ORDER BY REG.nombre_region"

        PromotorSQL = "SELECT DISTINCT RE.id_usuario FROM AS_Rutas_Eventos AS RE " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario= RE.id_usuario " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = US.id_region " & _
                    "WHERE REG.id_region<>0 " & _
                    " " + RegionSel + " " & _
                    "ORDER BY RE.id_usuario"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionFerrero.localSqlServer, HttpContext.Current.User.Identity.Name)

            SQLCombo()
            Combo.LlenaDrop(ConexionFerrero.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionFerrero.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

            If Tipo_usuario = 10 Then
                ''//Cargar periodo promotor
                Combo.LlenaDrop(ConexionFerrero.localSqlServer, "SELECT * FROM AS_Periodos Where fecha_inicio<='" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' AND fecha_cierre>='" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' OR estatus = 1 order by id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)
                CargarRuta()
                Exit Sub
            End If

            If Tipo_usuario = 12 Then
                panelMenu.Visible = True
                ''//Cargar periodo promotor
                Combo.LlenaDrop(ConexionFerrero.localSqlServer, "SELECT * FROM AS_Periodos Where estatus = 1", "nombre_periodo", "id_periodo", cmbPeriodo)
                Exit Sub
            End If

            panelMenu.Visible = True
            Combo.LlenaDrop(ConexionFerrero.localSqlServer, "SELECT * FROM AS_Periodos ORDER BY id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)
        End If
    End Sub

    Sub CargarRuta()
        If cmbPeriodo.SelectedValue <> "" Then
            If Tipo_usuario <> 10 Then
                Usuario = cmbPromotor.SelectedValue : Else
                Usuario = HttpContext.Current.User.Identity.Name : End If

            CargaGrilla(ConexionFerrero.localSqlServer, _
                        "execute AS_Cargar_Ruta " & cmbPeriodo.SelectedValue & ",'" & Usuario & "'", _
                        gridRutas)
        End If
    End Sub

    Private Sub gridRutas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRutas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            PeriodoActivo()

            If PeriodoActivoSiNo = 1 Then
                e.Row.Cells(0).Text = "<img src='../../../../Img/Cerrado.png' />"
                'e.Row.Cells(4).Text = "Tienda Cerrada"
            End If
        End If
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionFerrero.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarRuta()
    End Sub

    Private Sub PeriodoActivo()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                               "SELECT * FROM AS_Periodos " & _
                                               "where id_periodo =" & cmbPeriodo.SelectedValue & "")
        Dim Fecha_Inicio, Fecha_Fin As Date
        If Tabla.Rows.Count > 0 Then
            Fecha_Inicio = Tabla.Rows(0)("fecha_inicio")
            Fecha_Fin = Tabla.Rows(0)("fecha_fin")
        End If

        If Fecha_Inicio <= CDate(Date.Today) _
            And Fecha_Fin >= CDate(Date.Today) Then
            PeriodoActivoSiNo = 0
        Else
            PeriodoActivoSiNo = 1
        End If

        tabla.Dispose()
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarRuta()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        CargarRuta()
    End Sub
End Class