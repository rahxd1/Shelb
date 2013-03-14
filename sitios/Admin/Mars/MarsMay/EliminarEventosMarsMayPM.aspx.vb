Imports System.Data.SqlClient

Partial Public Class EliminarEventosMarsMayPM
    Inherits System.Web.UI.Page

    Dim SQLPeriodo, SQLSemana, SQLRegion, SQLUsuario As String
    Dim PeriodoSel, SemanaSel, RegionSel As String

    Sub SQLCombo()
        PeriodoSel = Acciones.Slc.cmb("orden", cmbPeriodo.SelectedValue)
        SemanaSel = Acciones.Slc.cmb("id_semana", cmbSemana.SelectedValue)
        RegionSel = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)

        SQLPeriodo = "SELECT DISTINCT orden, nombre_periodo " & _
                    "FROM View_Rutas_Eventos_May_VF " & _
                    "ORDER BY orden DESC"

        SQLSemana = "SELECT DISTINCT id_semana, nombre_semana " & _
                    "FROM View_Rutas_Eventos_May_VF " & _
                    "WHERE id_semana<>'' " + PeriodoSel + " " & _
                    "ORDER BY nombre_semana"

        SQLRegion = "SELECT DISTINCT id_region, nombre_region " & _
                    "FROM View_Rutas_Eventos_May_VF " & _
                    "WHERE id_region<>'' " + PeriodoSel + SemanaSel + " " & _
                    "ORDER BY nombre_region"

        SQLUsuario = "SELECT DISTINCT id_usuario " & _
                    "FROM View_Rutas_Eventos_May_VF " & _
                    "WHERE id_usuario<>'' " + PeriodoSel + SemanaSel + RegionSel + " " & _
                    "ORDER BY id_usuario"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LstProyectos(ConexionMars.localSqlServer, Me.gridProyectos)

            SQLCombo()

            Combo.LlenaDrop(ConexionMars.localSqlServer, SQLPeriodo, "nombre_periodo", "orden", cmbPeriodo)
        End If
    End Sub

    Sub CargarRuta()
        If Not cmbPromotor.SelectedValue = "" Then
            If Not cmbPeriodo.SelectedValue = "" Then
                If Not cmbSemana.SelectedValue = "" Then
                    CargaGrilla(ConexionMars.localSqlServer, _
                                "SELECT DISTINCT id_usuario, nombre, id_tienda, nombre_cadena, id_dia, " & _
                                "CASE estatus when 1 then 'CAPTURADO' when 0 then 'sin captura' end as estatus " & _
                                "FROM View_Rutas_Eventos_May_VF " & _
                                "WHERE id_usuario='" & cmbPromotor.SelectedValue & "' " & _
                                "AND id_semana='" & cmbSemana.SelectedValue & "'" & _
                                "AND orden=" & cmbPeriodo.SelectedValue & " ORDER BY nombre_cadena, nombre", _
                                Me.gridRuta)
                    gridRuta.Visible = True
                    btnEliminarTodos.Enabled = True
                    btnEliminarSeleccion.Enabled = True
                End If
            End If
        Else
            gridRuta.Visible = False
            btnEliminarTodos.Enabled = False
            btnEliminarSeleccion.Enabled = False
        End If
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, SQLSemana, "nombre_semana", "id_semana", cmbSemana)
        Combo.LlenaDrop(ConexionMars.localSqlServer, SQLRegion, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionMars.localSqlServer, SQLUsuario, "id_usuario", "id_usuario", cmbPromotor)

        CargarRuta()
    End Sub

    Private Sub cmbSemana_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSemana.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, SQLRegion, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionMars.localSqlServer, SQLUsuario, "id_usuario", "id_usuario", cmbPromotor)

        CargarRuta()
    End Sub

    Private Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, SQLUsuario, "id_usuario", "id_usuario", cmbPromotor)

        CargarRuta()
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarRuta()
    End Sub

    'Sub Habilitar()
    '    pnlGrid.Visible = Not pnlGrid.Visible

    '    btnEliminarTodos.Enabled = Not btnEliminarTodos.Enabled
    '    btnEliminarSeleccion.Enabled = Not btnEliminarSeleccion.Enabled
    'End Sub

    Protected Sub btnEliminarTodos_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEliminarTodos.Click
        BD.Execute(ConexionMars.localSqlServer, _
                   "DELETE FROM Mayoreo_Verificadores_Rutas_Eventos " & _
                   "WHERE id_usuario='" & cmbPromotor.SelectedValue & "' " & _
                   "AND orden=" & cmbPeriodo.SelectedValue & " " & _
                   "AND id_semana='" & cmbSemana.SelectedValue & "' AND estatus=0")


        CargarRuta()
        btnEliminarTodos.Enabled = False
        btnEliminarSeleccion.Enabled = False
    End Sub

    Protected Sub btnEliminarSeleccion_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEliminarSeleccion.Click
        Dim IDTienda As Integer

        For i = 0 To CInt(Me.gridRuta.Rows.Count) - 1
            IDTienda = Me.gridRuta.DataKeys(i).Value.ToString()
            Dim chkEliminar As CheckBox = CType(Me.gridRuta.Rows(i).FindControl("chkEliminar"), CheckBox)

            If chkEliminar.Checked = True Then
                BD.Execute(ConexionMars.localSqlServer, _
                           "DELETE FROM Mayoreo_Verificadores_Rutas_Eventos " & _
                           "WHERE id_usuario='" & cmbPromotor.SelectedValue & "' " & _
                           "AND orden=" & cmbPeriodo.SelectedValue & " " & _
                           "AND id_semana='" & cmbSemana.SelectedValue & "'" & _
                           "AND id_tienda=" & IDTienda & " AND estatus=0")
            End If
        Next

        CargarRuta()
    End Sub

End Class