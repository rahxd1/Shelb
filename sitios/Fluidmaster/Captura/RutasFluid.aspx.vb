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

Partial Public Class RutasFluid
    Inherits System.Web.UI.Page

    Dim PeriodoActivoSiNo As Integer
    Dim RegionSel, RegionSQL, PromotorSQL As String

    Private Sub PeriodoActual()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                                               "SELECT * FROM Periodos ORDER BY id_periodo DESC")
        If Tabla.Rows.Count > 0 Then
            cmbPeriodo.SelectedValue = Tabla.Rows(0)("id_periodo")
        End If

        tabla.Dispose()
    End Sub

    Sub SQLCombo()
        PromotorSQL = "SELECT DISTINCT RE.id_usuario FROM CatRutas AS RE " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = RE.id_region " & _
                    " " + RegionSel + " " & _
                    "ORDER BY RE.id_usuario"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            PeriodoActual()

            VerPermisos(ConexionFluidmaster.localSqlServer, HttpContext.Current.User.Identity.Name)

            SQLCombo()
            'Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

            If Tipo_usuario = 1 Then
                ''//Cargar periodo promotor
                Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, _
                                "SELECT TOP 4 * FROM Periodos " & _
                                "order by id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)

                cmbPromotor.SelectedValue = HttpContext.Current.User.Identity.Name
                CargarRuta()
                Exit Sub
            End If

            If Tipo_usuario = 12 Then
                panelMenu.Visible = True
                ''//Cargar periodo promotor
                Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, _
                                "SELECT TOP 4 * FROM Periodos " & _
                                "order by id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)
                Exit Sub
            End If

            panelMenu.Visible = True
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, "SELECT * FROM Periodos ORDER BY id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)
        End If
    End Sub

    Sub CargarRuta()
        VerPermisos(ConexionFluidmaster.localSqlServer, HttpContext.Current.User.Identity.Name)

        If cmbPeriodo.SelectedValue <> "" Then
            If Tipo_usuario = 1 Then
                CargaGrilla(ConexionFluidmaster.localSqlServer, _
                            "execute Cargar_Ruta " & cmbPeriodo.SelectedValue & ",'" & HttpContext.Current.User.Identity.Name & "'", _
                            gridRutas)
            Else
                CargaGrilla(ConexionFluidmaster.localSqlServer, _
                           "execute Cargar_Ruta " & cmbPeriodo.SelectedValue & ",'" & cmbPromotor.SelectedValue & "'", _
                           gridRutas)
            End If

            pnlGrilla.Visible = True
        End If
    End Sub

    Private Sub gridRutas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRutas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            PeriodoActivo()

            If PeriodoActivoSiNo = 1 And Tipo_usuario = 1 Then
                If e.Row.Cells(0).Text <> "" Then
                    e.Row.Cells(0).Text = "<img src='../../../Img/Cerrado.png' />"
                    e.Row.Cells(3).Text = "Captura cerrada"
                    e.Row.Cells(4).Text = "Captura cerrada"
                End If
            End If
        End If
    End Sub

    Private Sub PeriodoActivo()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                                               "SELECT * FROM Periodos where id_periodo =" & cmbPeriodo.SelectedValue & "")

        Dim Fecha_Inicio, Fecha_Fin As Date
        If tabla.Rows.Count > 0 Then
            Fecha_Inicio = tabla.Rows(0)("fecha_inicio")
            Fecha_Fin = tabla.Rows(0)("fecha_cierre")
        End If

        If Fecha_Inicio <= CDate(Date.Today) And Fecha_Fin >= CDate(Date.Today) Then
            PeriodoActivoSiNo = 0:Else
            PeriodoActivoSiNo = 1 : End If

        Tabla.Dispose()
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarRuta()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        CargarRuta()
    End Sub

    Private Sub lnkFotografias_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkFotografias.Click
        Response.Redirect("~/sitios/Fluidmaster/Captura/FormatoCapturaFotosFluid.aspx?periodo=" & cmbPeriodo.SelectedValue & "&usuario=" & cmbPromotor.SelectedValue & "")
    End Sub
End Class