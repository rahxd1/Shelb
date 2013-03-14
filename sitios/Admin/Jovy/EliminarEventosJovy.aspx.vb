Imports System.Data.SqlClient

Partial Public Class EliminarEventosJovy
    Inherits System.Web.UI.Page

    Dim PromotorSQL, RegionSQL, PeriodoSQL, RegionSel As String

    Sub CargaSQL()
        RegionSel = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)

        PeriodoSQL = "SELECT * FROM Jovy_Periodos ORDER BY id_periodo DESC"

        RegionSQL = "SELECT id_region, nombre_region FROM Regiones WHERE id_region<>0 "

        PromotorSQL = "SELECT id_usuario FROM Usuarios where id_tipo=1 " + RegionSel + " ORDER BY id_usuario"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CargaSQL()

            Combo.LlenaDrop(ConexionJovy.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionJovy.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionJovy.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo)

        End If
    End Sub

    Sub CargarRuta()
        If Not cmbPromotor.SelectedValue = "" Then
            If Not cmbPeriodo.SelectedValue = "" Then
                CargaGrilla(ConexionJovy.localSqlServer, _
                            "SELECT DISTINCT RUT.id_usuario, TI.nombre_cadena, ISNULL(TI.id_tienda,0)id_tienda, TI.nombre,RUT.id_cadena,  " & _
                            "CASE RUT.estatus when 1 then 'CAPTURADA' when 0 then 'SIN CAPTURA' end as estatus " & _
                            "FROM Jovy_Rutas_Eventos AS RUT " & _
                            "FULL JOIN View_Jovy_Tiendas AS TI ON RUT.id_tienda = TI.id_tienda " & _
                            "WHERE RUT.id_usuario='" & cmbPromotor.SelectedValue & "' " & _
                            "AND RUT.id_periodo=" & cmbPeriodo.SelectedValue & "", Me.gridRuta)

                pnlGrid.Visible = True
            End If
        End If
    End Sub

    Public Function AgregaTienda(ByVal IDUsuario As String, ByVal IDTienda As String) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                               "SELECT id_usuario, id_tienda " & _
                                                "FROM Jovy_CatRutas " & _
                                                "AND id_usuario='" & IDUsuario & "' " & _
                                                "AND id_tienda=" & IDTienda & " ")

        If Tabla.Rows.Count = 1 Then
            Me.lblaviso.Text = "LA TIENDA YA EXISTE EN UNA RUTA."
        Else
            BD.Execute(ConexionJovy.localSqlServer, _
                       "INSERT INTO Jovy_CatRutas " & _
                       "(id_usuario, id_tienda)VALUES('" & IDUsuario & "', " & IDTienda & ")")
        End If
    End Function

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        CargaSQL()
        Combo.LlenaDrop(ConexionJovy.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarRuta()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        If Not cmbPeriodo.SelectedValue = "" Then
            CargarRuta()
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarRuta()

        btnEliminarTodos.Enabled = True
        btnEliminarSeleccion.Enabled = True
    End Sub

    Protected Sub btnEliminarTodos_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEliminarTodos.Click
        BD.Execute(ConexionJovy.localSqlServer, _
                   "DELETE FROM Jovy_Rutas_Eventos " & _
                   "WHERE id_usuario = '" & cmbPromotor.Text & "' " & _
                   "AND id_periodo=" & cmbPeriodo.SelectedValue & " AND estatus=0")

        CargarRuta()
        btnEliminarTodos.Enabled = False
        btnEliminarSeleccion.Enabled = False
    End Sub

    Protected Sub btnEliminarSeleccion_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEliminarSeleccion.Click
        Dim IDTienda As Integer

        For i = 0 To CInt(Me.gridRuta.Rows.Count) - 1
            IDTienda = Me.gridRuta.DataKeys(i).Value.ToString()
            Dim chkEliminar As CheckBox = CType(Me.gridRuta.Rows(i).FindControl("chkEliminar"), CheckBox)
            Dim lblIDCadena As Label = CType(Me.gridRuta.Rows(i).FindControl("lblIDCadena"), Label)

            If chkEliminar.Checked = True Then
                BD.Execute(ConexionJovy.localSqlServer, _
                           "DELETE FROM Jovy_Rutas_Eventos " & _
                           "WHERE id_usuario = '" & cmbPromotor.Text & "' " & _
                           "AND id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                           "AND id_tienda=" & IDTienda & " " & _
                           "AND id_cadena=" & lblIDCadena.Text & " AND estatus=0")
            End If
        Next

        CargarRuta()
    End Sub

    Private Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridRuta.Columns(1).Visible = False
    End Sub
End Class