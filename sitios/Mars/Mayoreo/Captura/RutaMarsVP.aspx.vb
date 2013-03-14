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

Partial Public Class RutaMarsVP
    Inherits System.Web.UI.Page

    Dim Usuario, RegionSQL, RegionSel, PromotorSQL, SemanaSQL As String

    Sub SQLCombo()
        RegionSel = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)

        RegionSQL = "select distinct REG.id_region, REG.nombre_region " & _
                    "FROM PM_Tiendas as TI " & _
                    "INNER JOIN Regiones as REG ON TI.id_region=REG.id_Region " & _
                    "INNER JOIN PM_CatRutas as RUT ON RUT.id_tienda= TI.id_tienda " & _
                    "ORDER BY REG.nombre_region"

        PromotorSQL = "SELECT DISTINCT id_usuario " & _
                    "FROM PM_CatRutas as RUT " & _
                    "INNER JOIN PM_Tiendas as TI ON RUT.id_tienda=TI.id_tienda " & _
                    " " + RegionSel + " ORDER BY id_usuario ASC"

        SemanaSQL = "SELECT DISTINCT id_semana, nombre_semana FROM Periodos order by id_semana DESC"
    End Sub

    Private Sub PeriodoActual()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT * FROM Periodos_Nuevo " & _
                                  "WHERE fecha_inicio_periodo<='" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                                  "AND fecha_fin_periodo>='" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                                  "ORDER BY orden")
        If tabla.Rows.Count > 0 Then
            cmbPeriodo.SelectedValue = tabla.Rows(0)("orden") : End If
        Tabla.Dispose()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionMars.localSqlServer, SemanaSQL, "nombre_semana", "id_semana", cmbSemana)

            VerPermisos(ConexionMars.localSqlServer, HttpContext.Current.User.Identity.Name)

            SQLCombo()
            PeriodoActual()
            SemanaActual()

            If Tipo_usuario <> 7 Then
                panelMENU.Visible = True
                ''//Cargar Periodo Promotor Supervisor
                Combo.LlenaDrop(ConexionMars.localSqlServer, "SELECT * FROM Periodos_Nuevo " & _
                                "where fecha_fin_periodo<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                                "or fecha_inicio_periodo<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' ORDER BY orden DESC", "nombre_periodo", "orden", cmbPeriodo)
                CargarRuta()
            Else
                ''//Cargar Periodo Promotor Supervisor
                Combo.LlenaDrop(ConexionMars.localSqlServer, "SELECT * FROM Periodos_Nuevo " & _
                                "WHERE fecha_inicio_periodo<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                                "AND fecha_fin_periodo>= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' ORDER BY orden DESC", "nombre_periodo", "orden", cmbPeriodo)
                pnlTabla.Visible = True
                CargarRuta()
            End If
        End If
    End Sub

    Sub SemanaActual()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT DISTINCT id_semana, nombre_semana, fecha_inicio_periodo, fecha_fin_periodo " & _
                                      "FROM Periodos where fecha_inicio_semana<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                                      "AND fecha_fin_semana>= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "'")
        If Tabla.Rows.Count = 1 Then
            cmbSemana.SelectedValue = Tabla.Rows(0)("id_semana")
        End If

        Tabla.Dispose()
    End Sub

    Sub CargarRuta()
        If Tipo_usuario <> 7 Then
            Usuario = cmbPromotor.SelectedValue : Else
            Usuario = HttpContext.Current.User.Identity.Name : End If

        CargaGrillaDia("D1", gridDia1)
        CargaGrillaDia("D2", gridDia2)
        CargaGrillaDia("D3", gridDia3)
        CargaGrillaDia("D4", gridDia4)

        pnlTabla.Visible = True
    End Sub

    Public Function CargaGrillaDia(ByVal Dia As String, ByVal Grilla As GridView) As Integer
        CargaGrilla(ConexionMars.localSqlServer, _
                    "EXECUTE Mayoreo_Verificadores_Cargar_Ruta '" & cmbPeriodo.SelectedValue & "', " & _
                    "'" & cmbSemana.SelectedValue & "','" & Dia & "','" & Usuario & "'", Grilla)
    End Function

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarRuta()
    End Sub

    Protected Sub cmbSemana_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbSemana.SelectedIndexChanged
        CargarRuta()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        CargarRuta()
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarRuta()
    End Sub
End Class