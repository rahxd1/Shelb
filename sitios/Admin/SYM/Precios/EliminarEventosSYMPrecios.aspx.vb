Imports System.Data.SqlClient

Partial Public Class EliminarEventosSYMPrecios
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LstProyectos(ConexionSYM.localSqlServer, Me.gridProyectos)

            Combo.LlenaDrop(ConexionSYM.localSqlServer, "SELECT DISTINCT id_usuario FROM Precios_CatRutas", "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, "SELECT * FROM Precios_Periodos ORDER BY id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)
        End If
    End Sub

    Sub CargarRuta()
        If Not cmbPromotor.SelectedValue = "" Then
            If Not cmbPeriodo.SelectedValue = "" Then
                CargaGrilla(ConexionSYM.localSqlServer, _
                            "SELECT DISTINCT RE.id_usuario, RE.id_cadena, CAD.nombre_cadena, " & _
                            "CASE RE.estatus when 1 then 'CAPTURADO' when 0 then 'sin captura' end as estatus " & _
                            "FROM Precios_Rutas_Eventos AS RE " & _
                            "INNER JOIN Cadenas_Tiendas AS CAD ON CAD.id_cadena = RE.id_cadena " & _
                            "WHERE RE.id_usuario='" & cmbPromotor.SelectedValue & "' " & _
                            "AND RE.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                            "ORDER BY CAD.nombre_cadena",Me.gridRuta)
            End If
        End If
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        If Not cmbPeriodo.SelectedValue = "" Then
            Combo.LlenaDrop(ConexionSYM.localSqlServer, "SELECT DISTINCT id_usuario FROM Precios_Rutas_Eventos WHERE id_periodo=" & cmbPeriodo.SelectedValue & " ", "id_usuario", "id_usuario", cmbPromotor)

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
                   "DELETE FROM Precios_Rutas_Eventos " & _
                   "WHERE id_usuario='" & cmbPromotor.SelectedValue & "' " & _
                   "AND id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                   "AND estatus=0 ")

        CargarRuta()
        btnEliminarTodos.Enabled = False
        btnEliminarSeleccion.Enabled = False
    End Sub

    Protected Sub btnEliminarSeleccion_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEliminarSeleccion.Click
        Dim IDCadena As Integer

        For i = 0 To CInt(Me.gridRuta.Rows.Count) - 1
            IDCadena = Me.gridRuta.DataKeys(i).Value.ToString()
            Dim chkEliminar As CheckBox = CType(Me.gridRuta.Rows(i).FindControl("chkEliminar"), CheckBox)

            If chkEliminar.Checked = True Then
                BD.Execute(ConexionSYM.localSqlServer, _
                           "DELETE FROM Precios_Rutas_Eventos " & _
                           "WHERE id_usuario = '" & cmbPromotor.SelectedValue & "' " & _
                           "AND id_periodo=" & cmbPeriodo.SelectedValue & "'" & _
                           "AND id_cadena=" & IDCadena & " AND estatus=0")
            End If
        Next

        CargarRuta()
    End Sub
End Class