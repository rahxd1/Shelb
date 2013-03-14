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

Partial Public Class RutasJovy
    Inherits System.Web.UI.Page

    Dim Usuario, TiendaRuta As String
    Dim PeriodoActivoSiNo As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionJovy.localSqlServer, HttpContext.Current.User.Identity.Name)

            PeriodoActual()

            If Tipo_usuario = 1 Then
                ''//Cargar periodo promotor
                Combo.LlenaDrop(ConexionJovy.localSqlServer, "SELECT TOP 4 * FROM Jovy_Periodos order by id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)
                CargarRuta()
                Exit Sub
            End If

            panelMenu.Visible = True
            CargarRegion()

            ''//Cargar promotor
            Combo.LlenaDrop(ConexionJovy.localSqlServer, "SELECT distinct id_usuario FROM Jovy_CatRutas ORDER BY id_usuario", "id_usuario", "id_usuario", cmbPromotor)
            ''//Cargar Periodo Promotor Supervisor
            Combo.LlenaDrop(ConexionJovy.localSqlServer, "SELECT * FROM Jovy_Periodos ORDER BY id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)

            CargarRuta()
        End If
    End Sub

    Sub CargarRuta()
        If cmbPeriodo.SelectedValue <> "" Then
            Dim SQLCargaRuta As String

            If tipo_usuario <> 1 Then
                Usuario = cmbPromotor.SelectedValue
                SQLCargaRuta = "execute Cargar_Ruta " & cmbPeriodo.SelectedValue & ",'" & Usuario & "'"
            Else
                Usuario = HttpContext.Current.User.Identity.Name
                SQLCargaRuta = "execute cargar_ruta " & cmbPeriodo.SelectedValue & ",'" & Usuario & "'"
            End If

            CargaGrilla(ConexionJovy.localSqlServer, SQLCargaRuta, Me.gridRutas)
        End If
    End Sub

    Private Sub gridRutas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRutas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            PeriodoActivo()

            If PeriodoActivoSiNo = 1 Then
                e.Row.Cells(0).Text = "<img src='../../../Img/Cerrado.png' />"

                If Tipo_usuario = 1 Or Tipo_usuario = 2 Then
                    e.Row.Cells(3).Text = "Tienda Cerrada"
                    e.Row.Cells(4).Text = "Tienda Cerrada"
                End If
            End If
        End If
    End Sub

    Private Sub gridRutas_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gridRutas.RowEditing
        TiendaRuta = gridRutas.Rows(e.NewEditIndex).Cells(1).Text
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        If cmbRegion.SelectedValue <> 0 Then
            Combo.LlenaDrop(ConexionJovy.localSqlServer, "SELECT DISTINCT RE.id_usuario FROM Jovy_Rutas_Eventos AS RE " & _
                             "INNER JOIN Usuarios as US ON US.id_usuario= RE.id_usuario " & _
                             "INNER JOIN Regiones as REG ON REG.id_region = US.id_region " & _
                             "WHERE REG.id_region=" & cmbRegion.Text & " " & _
                             "ORDER BY RE.id_usuario", "id_usuario", "id_usuario", cmbPromotor)
        Else
            Combo.LlenaDrop(ConexionJovy.localSqlServer, "SELECT DISTINCT RE.id_usuario FROM Jovy_Rutas_Eventos AS RE " & _
                             "INNER JOIN Usuarios as US ON US.id_usuario= RE.id_usuario " & _
                             "INNER JOIN Regiones as REG ON REG.id_region = US.id_region " & _
                             "ORDER BY RE.id_usuario", "id_usuario", "id_usuario", cmbPromotor)
        End If

        CargarRuta()
    End Sub

    Sub CargarRegion()
        Combo.LlenaDrop(ConexionJovy.localSqlServer, "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                     "FROM Jovy_Rutas_Eventos AS RE " & _
                     "INNER JOIN Usuarios as US ON US.id_usuario= RE.id_usuario " & _
                     "INNER JOIN Regiones as REG ON REG.id_region = US.id_region " & _
                     " ORDER BY REG.nombre_region", "nombre_region", "id_region", cmbRegion)
    End Sub

    Sub BusquedaRegion()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                               "SELECT * FROM Usuarios " & _
                                "WHERE id_usuario= '" & HttpContext.Current.User.Identity.Name & "'")
        If Tabla.Rows.Count = 1 Then
            Region = Tabla.Rows(0)("id_region")
        End If

        Tabla.Dispose()
    End Sub

    Private Sub PeriodoActual()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                               "SELECT * FROM Jovy_Periodos ORDER BY id_periodo DESC")
        If Tabla.Rows.Count > 0 Then
            cmbPeriodo.SelectedValue = Tabla.Rows(0)("id_periodo")
        End If

        tabla.Dispose()
    End Sub

    Private Sub PeriodoActivo()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                               "SELECT * FROM Jovy_Periodos where id_periodo =" & cmbPeriodo.SelectedValue & "")
        Dim Fecha_Inicio, Fecha_Fin As Date
        If Tabla.Rows.Count > 0 Then
            Fecha_Inicio = Tabla.Rows(0)("fecha_inicio")
            Fecha_Fin = Tabla.Rows(0)("fecha_cierre")
        End If

        If Fecha_Inicio <= CDate(Date.Today) _
            And Fecha_Fin >= CDate(Date.Today) Then
            PeriodoActivoSiNo = 0:Else
            PeriodoActivoSiNo = 1:End If

        tabla.Dispose()
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarRuta()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        CargarRuta()
    End Sub
End Class