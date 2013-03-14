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

Partial Public Class CuestionariosMars
    Inherits System.Web.UI.Page

    Dim PeriodoActivoSiNo, Online As Integer
    Dim PromotorSQL, RegionSQL, IDUsuario As String
    Dim RegionSel As String

    Sub SQLCombo()
        RegionSel = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)

        PromotorSQL = "SELECT id_usuario FROM Usuarios " & _
                    "WHERE id_tipo=1 " + RegionSel + " " & _
                    "ORDER BY id_usuario"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Regiones as REG WHERE id_region<>0 " & _
                    " ORDER BY REG.nombre_region"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, "select * from Cap_Cuestionarios WHERE tipo_cuestionario=1 " & _
                            "and fecha_cierre>='" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "'", "nombre_cuestionario", "id_cuestionario", cmbPrograma)

            VerPermisos(ConexionMars.localSqlServer, HttpContext.Current.User.Identity.Name)

            PeriodoActual()

            If Tipo_usuario <> 1 Then
                panelMenu.Visible = True
                SQLCombo()
                Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

                ''//Cargar Periodo  Supervisor
                Combo.LlenaDrop(ConexionMars.localSqlServer, "SELECT DISTINCT id_periodo, orden, nombre_periodo, fecha_inicio_periodo, fecha_fin_periodo FROM Periodos_Nuevo where fecha_fin_periodo<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' or fecha_inicio_periodo<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' ORDER BY orden DESC", "nombre_periodo", "orden", cmbPeriodo)

                CargarRuta()
            Else
                SQLCombo()
                Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
                cmbPromotor.SelectedValue = HttpContext.Current.User.Identity.Name

                ''//Cargar Periodo Promotor 
                Combo.LlenaDrop(ConexionMars.localSqlServer, "SELECT DISTINCT TOP 4 id_periodo, orden, nombre_periodo, fecha_inicio_periodo, fecha_fin_periodo FROM Periodos_Nuevo where fecha_fin_periodo<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' or fecha_inicio_periodo<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' ORDER BY orden DESC", "nombre_periodo", "orden", cmbPeriodo)
                CargarRuta()
            End If

            DatosCompletados()
        End If
    End Sub

    Private Sub PeriodoActual()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT DISTINCT orden,id_periodo FROM Periodos_Nuevo " & _
                                  "WHERE fecha_inicio_periodo<='" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                                  "AND fecha_fin_periodo>='" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                                  "ORDER BY orden DESC")
        If Tabla.Rows.Count > 0 Then
            cmbPeriodo.SelectedValue = Tabla.Rows(0)("orden")
        End If

        tabla.Dispose()
    End Sub

    Private Sub TipoPrograma()
        If Tipo_usuario <> 1 Then
            IDUsuario= cmbPromotor.SelectedValue: Else
            IDUsuario = HttpContext.Current.User.Identity.Name:End If

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT * from Cap_Datos " & _
                                               "WHERE orden=" & cmbPeriodo.SelectedValue & " " & _
                                               "AND id_usuario='" & IDUsuario & "' ")
        If tabla.Rows.Count > 0 Then
            Online = tabla.Rows(0)("online")
        End If

        tabla.Dispose()
    End Sub

    Sub CargarRuta()
        VerPermisos(ConexionMars.localSqlServer, HttpContext.Current.User.Identity.Name)

        TipoPrograma()

        If Tipo_usuario <> 1 Then
            IDUsuario = cmbPromotor.SelectedValue : Else
            IDUsuario = HttpContext.Current.User.Identity.Name : End If

        CargaGrilla(ConexionMars.localSqlServer, _
                    "execute Cargar_Cuestionarios " & cmbPeriodo.SelectedValue & ",'" & IDUsuario & "'," & Online & "", _
                    Me.gridRutas)

    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarRuta()
    End Sub

    Private Sub gridRutas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRutas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
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

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarRuta()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        CargarRuta()
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        BD.Execute(ConexionMars.localSqlServer, _
                   "INSERT INTO Cap_Datos " & _
                   "(orden, id_usuario, id_cuestionario, canal, ciudad, nombre_promotor, nombre_jefe,online) " & _
                   "VALUES(" & cmbPeriodo.SelectedValue & ",'" & HttpContext.Current.User.Identity.Name & "', " & _
                   "" & cmbPrograma.SelectedValue & "," & cmbCanal.SelectedValue & "," & txtCiudad.Text & ", " & _
                   "" & txtNombre.Text & "," & txtJefe.Text & "," & cmbOnline.SelectedValue & ")")

        pnlDatos.Visible = False
    End Sub

    Sub DatosCompletados()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT * FROM Cap_Datos " & _
                                  "WHERE orden=" & cmbPeriodo.SelectedValue & " " & _
                                  "AND id_usuario='" & HttpContext.Current.User.Identity.Name & "'")
        If Tabla.Rows.Count > 0 Then
            pnlDatos.Visible = False : Else
            pnlDatos.Visible = True : End If

        Tabla.Dispose()
    End Sub

End Class