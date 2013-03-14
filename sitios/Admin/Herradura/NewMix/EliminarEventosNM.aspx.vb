Imports System.Data.SqlClient

Partial Public Class EliminarEventosNM
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LstProyectos(ConexionHerradura.localSqlServer, Me.gridProyectos)

            Combo.LlenaDrop(ConexionHerradura.localSqlServer, "SELECT DISTINCT id_usuario FROM NewMix_CatRutas", "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, "SELECT * FROM NewMix_Periodos ORDER BY id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)
        End If
    End Sub

    Sub CargarRuta()
        If Not cmbPromotor.SelectedValue = "" Then
            If Not cmbPeriodo.SelectedValue = "" Then
                CargaGrilla(ConexionHerradura.localSqlServer, _
                            "SELECT DISTINCT RE.id_usuario, TI.nombre, RE.id_tienda, TI.nombre_cadena, " & _
                            "CASE RE.estatus when 1 then 'CAPTURADO' when 0 then 'sin captura' end as estatus " & _
                            "FROM NewMix_Rutas_Eventos AS RE " & _
                            "INNER JOIN View_Tiendas_NM AS TI ON TI.id_tienda = RE.id_tienda " & _
                            "WHERE RE.id_usuario = '" & cmbPromotor.SelectedValue & "' " & _
                            "AND RE.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                            "ORDER BY TI.nombre_cadena,TI.nombre", Me.gridRuta)
            End If
        End If
    End Sub

    Private Sub gridRuta_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridRuta.RowDeleting
        BD.Execute(ConexionHerradura.localSqlServer, _
                   "DELETE FROM NewMix_Rutas_Eventos " & _
                   "WHERE id_usuario = '" & cmbPromotor.Text & "' " & _
                   "AND id_tienda= '" & gridRuta.Rows(e.RowIndex).Cells(3).Text & "' " & _
                   "AND id_periodo = '" & cmbPeriodo.Text & "'")

        CargarRuta()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        If Not cmbPeriodo.SelectedValue = "" 
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, "SELECT DISTINCT id_usuario FROM NewMix_Rutas_Eventos WHERE id_periodo=" & cmbPeriodo.SelectedValue & " ", "id_usuario", "id_usuario", cmbPromotor)

            CargarRuta()
            pnlGrid.Visible = True
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarRuta()
        pnlGrid.Visible = True
        btnEliminarTodos.Enabled = True
        btnEliminarSeleccion.Enabled = True
    End Sub

    Protected Sub btnEliminarTodos_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEliminarTodos.Click
        BD.Execute(ConexionHerradura.localSqlServer, _
                   "DELETE FROM NewMix_Rutas_Eventos " & _
                   "WHERE id_usuario = '" & cmbPromotor.SelectedValue & "' " & _
                   "AND id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                   "AND estatus=0 ")

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
                BD.Execute(ConexionHerradura.localSqlServer, _
                           "DELETE FROM NewMix_Rutas_Eventos " & _
                           "WHERE id_usuario = '" & cmbPromotor.SelectedValue & "' " & _
                           "AND id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                           "AND id_tienda= '" & IDTienda & "' AND estatus=0")
            End If
        Next

        CargarRuta()
    End Sub
End Class