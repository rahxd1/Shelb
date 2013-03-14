Imports System.Data.SqlClient

Partial Public Class EliminarEventosSYMAC
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LstProyectos(ConexionSYM.localSqlServer, Me.gridProyectos)

            Combo.LlenaDrop(ConexionSYM.localSqlServer, "SELECT DISTINCT id_usuario FROM AC_CatRutas", "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, "SELECT * FROM AC_Periodos ORDER BY id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)
        End If
    End Sub

    Sub CargarRuta()
        If Not cmbPromotor.SelectedValue = "" Then
            If Not cmbPeriodo.SelectedValue = "" Then
                CargaGrilla(ConexionSYM.localSqlServer, _
                            "SELECT DISTINCT RE.id_usuario, TI.nombre, RE.id_tienda, TI.nombre_cadena, " & _
                            "CASE RE.estatus_anaquel when 1 then 'CAPTURADO' when 0 then 'sin captura' end as estatus_anaquel, " & _
                            "CASE RE.estatus_catalogacion when 1 then 'CAPTURADO' when 0 then 'sin captura' end as estatus_catalogacion " & _
                            "FROM AC_Rutas_Eventos AS RE " & _
                            "INNER JOIN View_SYM_AC_Tiendas AS TI ON TI.id_tienda = RE.id_tienda " & _
                            "WHERE RE.id_usuario = '" & cmbPromotor.SelectedValue & "' " & _
                            "AND RE.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                            "ORDER BY TI.nombre_cadena, TI.nombre", Me.gridRuta)
            End If
        End If
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        If Not cmbPeriodo.SelectedValue = "" Then
            Combo.LlenaDrop(ConexionSYM.localSqlServer, "SELECT DISTINCT id_usuario FROM AC_Rutas_Eventos WHERE id_periodo=" & cmbPeriodo.SelectedValue & " ", "id_usuario", "id_usuario", cmbPromotor)

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
        BD.Execute(ConexionSYM.localSqlServer, _
                   "DELETE FROM AC_Rutas_Eventos " & _
                   "WHERE id_usuario = '" & cmbPromotor.SelectedValue & "' " & _
                   "AND id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                   "AND estatus_anaquel=0 AND estatus_catalogacion=0 ")

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
                BD.Execute(ConexionSYM.localSqlServer, _
                           "DELETE FROM AC_Rutas_Eventos " & _
                           "WHERE id_usuario = '" & cmbPromotor.SelectedValue & "' " & _
                           "AND id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                           "AND id_tienda=" & IDTienda & " " & _
                           "AND estatus_anaquel=0 AND estatus_catalogacion=0")
            End If
        Next

        CargarRuta()
    End Sub
End Class