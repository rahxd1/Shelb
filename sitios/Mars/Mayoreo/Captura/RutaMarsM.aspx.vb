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

Partial Public Class RutaMarsM
    Inherits System.Web.UI.Page

    Dim no_region As String
    Dim PeriodoActivoSiNo, PeriodoActivoSiNo1, PeriodoActivoSiNo2, CapturaAbierta As Integer

    Private Sub PeriodoActual()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT * FROM Periodos_Nuevo " & _
                                  "WHERE  fecha_inicio_periodo<='" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                                  "AND fecha_fin_periodo>='" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                                  "ORDER BY orden")
        If Tabla.Rows.Count > 0 Then
            cmbPeriodo.SelectedValue = Tabla.Rows(0)("orden")
        End If

        Tabla.Dispose()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionMars.localSqlServer, HttpContext.Current.User.Identity.Name)

            PeriodoActual()

            If tipo_usuario = 1 Or tipo_usuario = 2 Then
                ''//Cargar Periodo Promotor Supervisor
                Combo.LlenaDrop(ConexionMars.localSqlServer, "SELECT TOP 4 * FROM Periodos_Nuevo " & _
                                "where fecha_fin_periodo<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                                "or fecha_inicio_periodo<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' ORDER BY orden DESC", "nombre_periodo", "orden", cmbPeriodo)
                CargarRuta()
                Exit Sub
            End If

            If Consulta = 1 Then
                panelMenu.Visible = True
                CargarRegion()

                ''//Cargar promotor
                Combo.LlenaDrop(ConexionMars.localSqlServer, "SELECT distinct id_usuario FROM Mayoreo_CatRutas ORDER BY id_usuario", "id_usuario", "id_usuario", cmbPromotor)
                ''//Cargar Periodo Promotor Supervisor
                Combo.LlenaDrop(ConexionMars.localSqlServer, "SELECT * FROM Periodos_Nuevo " & _
                                "where fecha_fin_periodo<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                                "or fecha_inicio_periodo<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' ORDER BY orden DESC", "nombre_periodo", "orden", cmbPeriodo)

                CargarRuta()
            Else
                Response.Redirect("../MenuMayoreo.aspx")
            End If
        End If
    End Sub

    Sub CargarRegion()
        Combo.LlenaDrop(ConexionMars.localSqlServer, _
                           "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                           "FROM Mayoreo_Rutas_Eventos AS RE " & _
                           "INNER JOIN Usuarios as US ON US.id_usuario= RE.id_usuario " & _
                           "INNER JOIN Regiones as REG ON REG.id_region = US.id_region " & _
                           " ORDER BY REG.nombre_region", "nombre_region", "id_region", cmbRegion)


    End Sub

    Sub CargarRuta()
        Dim SQLCargaRuta As String

        If tipo_usuario <> 1 Then
            SQLCargaRuta = "execute Mayoreo_Cargar_Ruta '" & cmbPeriodo.SelectedValue & "','" & cmbPromotor.SelectedValue & "'" : Else
            SQLCargaRuta = "execute Mayoreo_cargar_ruta '" & cmbPeriodo.SelectedValue & "','" & HttpContext.Current.User.Identity.Name & "'" : End If

        CargaGrilla(ConexionMars.localSqlServer,SQLCargaRuta, gridRutas)
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        If cmbRegion.SelectedValue <> 0 Then
            Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct REG.nombre_region, RUT.id_usuario " & _
                        "FROM Mayoreo_Tiendas as TI " & _
                        "INNER JOIN Regiones as REG ON TI.id_region=REG.id_Region " & _
                        "INNER JOIN Mayoreo_CatRutas as RUT ON RUT.id_tienda= TI.id_tienda " & _
                        "WHERE REG.id_region = '" & cmbRegion.Text & "'" & _
                        " ORDER BY RUT.id_usuario", "id_usuario", "id_usuario", cmbPromotor)
        Else
            Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct REG.nombre_region, RUT.id_usuario " & _
                        "FROM Mayoreo_Tiendas as TI " & _
                        "INNER JOIN Regiones as REG ON TI.id_region=REG.id_Region " & _
                        "INNER JOIN Mayoreo_CatRutas as RUT ON RUT.id_tienda= TI.id_tienda " & _
                        "ORDER BY RUT.id_usuario", "id_usuario", "id_usuario", cmbPromotor)
        End If
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

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        CargarRuta()
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
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
            End If

            If PeriodoActivoSiNo1 = 1 And CapturaAbierta = 0 Then
                If tipo_usuario = 1 Then
                    e.Row.Cells(3).Text = "Tienda Cerrada"
                End If
            End If

            If PeriodoActivoSiNo2 = 1 And CapturaAbierta = 0 Then
                If tipo_usuario = 1 Then
                    e.Row.Cells(4).Text = "Tienda Cerrada"
                End If
            End If
        End If
    End Sub
End Class