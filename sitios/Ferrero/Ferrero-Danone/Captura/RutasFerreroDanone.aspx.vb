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

Partial Public Class RutasFerreroDanone
    Inherits System.Web.UI.Page

    Dim Usuario As String
    Dim PeriodoActivoSiNo As Integer
    Dim RegionSel, RegionSQL, PromotorSQL As String

    Sub SQLCombo()
        RegionSel = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)

        If cmbRegion.SelectedValue <> "" Then
            RegionSel = "WHERE REG.id_region=" & cmbRegion.SelectedValue & " " : Else
            RegionSel = "" : End If

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Danone_CatRutas AS RE " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario= RE.id_usuario " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = US.id_region " & _
                    "WHERE id_proyecto=30 " & _
                    " ORDER BY REG.nombre_region"

        PromotorSQL = "SELECT DISTINCT RE.id_usuario " & _
                    "FROM Danone_CatRutas AS RE " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario= RE.id_usuario " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = US.id_region " & _
                    "WHERE id_usuario<>'' " & _
                    " " + RegionSel + " " & _
                    "ORDER BY RE.id_usuario"

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionEnergizer.localSqlServer, HttpContext.Current.User.Identity.Name)

            SQLCombo()

            If Tipo_usuario = 12 Or Tipo_usuario = 2 Then
                ''//Cargar periodo promotor
                Combo.LlenaDrop(ConexionFerrero.localSqlServer, "SELECT * FROM Danone_Periodos Where fecha_inicio<='" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' AND fecha_cierre>='" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' OR estatus = 1 order by id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)
                CargarRuta()
                Exit Sub
            End If

            panelMenu.Visible = True

            Combo.LlenaDrop(ConexionFerrero.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionFerrero.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionFerrero.localSqlServer, "SELECT * FROM Danone_Periodos ORDER BY id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)

            CargarRuta()
        End If
    End Sub

    Sub CargarRuta()
        If cmbPeriodo.SelectedValue <> "" Then
            If Tipo_usuario = 12 Or Tipo_usuario = 2 Then
                Usuario = HttpContext.Current.User.Identity.Name : Else
                Usuario = cmbPromotor.SelectedValue : End If


            CargaGrilla(ConexionFerrero.localSqlServer, _
                        "execute Danone_cargar_ruta " & cmbPeriodo.SelectedValue & "," & _
                        "'" & Usuario & "'", Me.gridRutas)
        End If
    End Sub

    Private Sub gridRUTAS_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRutas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            PeriodoActivo()

            If PeriodoActivoSiNo = 1 Then
                e.Row.Cells(0).Text = "<img src='../../../../Img/Cerrado.png' />"
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
                                               "SELECT * FROM Danone_Periodos " & _
                                               "WHERE id_periodo =" & cmbPeriodo.SelectedValue & "")
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

        Tabla.Dispose()
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarRuta()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        CargarRuta()
    End Sub

    Protected Sub btnAgregaTienda_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAgregaTienda.Click
        Response.Redirect("FormatoCapturaFerreroDanone.aspx")
    End Sub
End Class