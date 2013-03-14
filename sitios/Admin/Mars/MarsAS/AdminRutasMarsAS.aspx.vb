Imports System.Data.SqlClient

Partial Public Class AdminRutasMarsAS
    Inherits System.Web.UI.Page

    Dim CadenaSQL, EstadoSQL, TiendaSQL, PromotorSQL, RegionSQL As String
    Dim CadenaSel, EstadoSel, RegionSel, IDTienda As String

    Sub SQLLista()
        CadenaSel = Acciones.Slc.cmb("id_cadena", ListCadena.SelectedValue)
        EstadoSel = Acciones.Slc.cmb("id_estado", ListEstado.SelectedValue)
        RegionSel = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)

        RegionSQL = "SELECT DISTINCT id_region, nombre_region FROM View_Tiendas_AS " & _
                    "WHERE id_region<>0 "

        PromotorSQL = "SELECT id_usuario FROM Usuarios where id_proyecto=7 AND id_tipo=1 " & _
                    " " + RegionSel + " " & _
                    "ORDER BY id_usuario"

        EstadoSQL = "SELECT distinct nombre_estado, id_estado " & _
                    "FROM View_Tiendas_AS " & _
                    "WHERE id_tienda NOT IN(SELECT id_tienda FROM AS_CatRutas) " & _
                    " " + RegionSel + " " & _
                    "ORDER BY nombre_estado"

        CadenaSQL = "SELECT DISTINCT id_cadena, nombre_cadena " & _
                    "FROM View_Tiendas_AS " & _
                    "WHERE id_tienda NOT IN(SELECT id_tienda FROM AS_CatRutas) " & _
                    " " + RegionSel + EstadoSel + " " & _
                    "ORDER BY nombre_cadena"

        TiendaSQL = "SELECT id_tienda, nombre " & _
                    "FROM View_Tiendas_AS as TI " & _
                    "WHERE id_tienda NOT IN(SELECT id_tienda FROM AS_CatRutas) " & _
                    " " + RegionSel + EstadoSel + CadenaSel + " " & _
                    "ORDER BY nombre"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LstProyectos(ConexionMars.localSqlServer, Me.gridProyectos)

            SQLLista()
            CargarRuta()

            Lista.LlenaBox(ConexionMars.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", ListCadena)
            Lista.LlenaBox(ConexionMars.localSqlServer, EstadoSQL, "nombre_estado", "id_estado", ListEstado)
            Lista.LlenaBox(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)

            Combo.LlenaDrop(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
        End If
    End Sub

    Sub CargarRuta()
        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT DISTINCT RUT.id_usuario,TI.Codigo,TI.nombre_cadena,TI.clasificacion_tienda," & _
                    "RUT.id_tienda, TI.nombre, " & _
                    "RUT.W1_1,RUT.W1_2,RUT.W1_3,RUT.W1_4,RUT.W1_5,RUT.W1_6,RUT.W1_7," & _
                    "RUT.W2_1,RUT.W2_2,RUT.W2_3,RUT.W2_4,RUT.W2_5,RUT.W2_6,RUT.W2_7," & _
                    "RUT.W3_1,RUT.W3_2,RUT.W3_3,RUT.W3_4,RUT.W3_5,RUT.W3_6,RUT.W3_7," & _
                    "RUT.W4_1,RUT.W4_2,RUT.W4_3,RUT.W4_4,RUT.W4_5,RUT.W4_6,RUT.W4_7 " & _
                    "FROM AS_CatRutas AS RUT  " & _
                    "INNER JOIN View_Tiendas_AS AS TI ON RUT.id_tienda = TI.id_tienda " & _
                    "WHERE RUT.id_usuario = '" & cmbPromotor.SelectedValue & "' " & _
                    "ORDER BY TI.nombre, TI.nombre_cadena", gridRuta)
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAgregar.Click
        pnlAgregar.Visible = True

        SQLLista()

        Lista.LlenaBox(ConexionMars.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", ListCadena)
        Lista.LlenaBox(ConexionMars.localSqlServer, EstadoSQL, "nombre_estado", "id_estado", ListEstado)
        Lista.LlenaBox(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)

        CargarRuta()
    End Sub

    Private Sub gridRuta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRuta.RowDataBound
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(5).Text = gridRuta.Rows.Count & " tiendas"
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            For i = 7 To 13
                e.Row.Cells(i).BackColor = Drawing.Color.GreenYellow : Next i
            For i = 14 To 20
                e.Row.Cells(i).BackColor = Drawing.Color.CadetBlue : Next i
            For i = 21 To 27
                e.Row.Cells(i).BackColor = Drawing.Color.CornflowerBlue : Next i
            For i = 28 To 34
                e.Row.Cells(i).BackColor = Drawing.Color.DeepPink : Next i
        End If
    End Sub

    Private Sub gridRuta_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridRuta.RowDeleting
        BD.Execute(ConexionMars.localSqlServer, _
                   "DELETE FROM AS_CatRutas " & _
                   "WHERE id_usuario='" & cmbPromotor.SelectedValue & "' " & _
                   "AND id_tienda=" & gridRuta.Rows(e.RowIndex).Cells(4).Text & "")

        CargarRuta()
        pnlAgregar.Visible = False
    End Sub

    Protected Sub ListCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListCadena.SelectedIndexChanged
        SQLLista()

        Lista.LlenaBox(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)

        CargarRuta()
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAceptar.Click
        AgregaTienda(cmbPromotor.Text, ListTienda.SelectedValue, _
                     chkW1_1.Checked, chkW1_2.Checked, chkW1_3.Checked, chkW1_4.Checked, _
                     chkW1_5.Checked, chkW1_6.Checked, chkW1_7.Checked, _
                     chkW2_1.Checked, chkW2_2.Checked, chkW2_3.Checked, chkW2_4.Checked, _
                     chkW2_5.Checked, chkW2_6.Checked, chkW2_7.Checked, _
                     chkW3_1.Checked, chkW3_2.Checked, chkW3_3.Checked, chkW3_4.Checked, _
                     chkW3_5.Checked, chkW3_6.Checked, chkW3_7.Checked, _
                     chkW4_1.Checked, chkW4_2.Checked, chkW4_3.Checked, chkW4_4.Checked, _
                     chkW4_5.Checked, chkW4_6.Checked, chkW4_7.Checked)
        CargarRuta()

        SQLLista()
        Lista.LlenaBox(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)

        pnlAgregar.Visible = False

        chkW1_1.Checked = False
        chkW1_2.Checked = False
        chkW1_3.Checked = False
        chkW1_4.Checked = False
        chkW1_5.Checked = False
        chkW1_6.Checked = False
        chkW1_7.Checked = False
        chkW2_1.Checked = False
        chkW2_2.Checked = False
        chkW2_3.Checked = False
        chkW2_4.Checked = False
        chkW2_5.Checked = False
        chkW2_6.Checked = False
        chkW2_7.Checked = False
        chkW3_1.Checked = False
        chkW3_2.Checked = False
        chkW3_3.Checked = False
        chkW3_4.Checked = False
        chkW3_5.Checked = False
        chkW3_6.Checked = False
        chkW3_7.Checked = False
        chkW4_1.Checked = False
        chkW4_2.Checked = False
        chkW4_3.Checked = False
        chkW4_4.Checked = False
        chkW4_5.Checked = False
        chkW4_6.Checked = False
        chkW4_7.Checked = False

        chkFormatoPrecios.Checked = False
    End Sub

    Public Function AgregaTienda(ByVal IDUsuario As String, ByVal IDTienda As String, _
                                 ByVal W1_1 As Boolean, ByVal W1_2 As Boolean, ByVal W1_3 As Boolean, ByVal W1_4 As Boolean, _
                                 ByVal W1_5 As Boolean, ByVal W1_6 As Boolean, ByVal W1_7 As Boolean, _
                                 ByVal W2_1 As Boolean, ByVal W2_2 As Boolean, ByVal W2_3 As Boolean, ByVal W2_4 As Boolean, _
                                 ByVal W2_5 As Boolean, ByVal W2_6 As Boolean, ByVal W2_7 As Boolean, _
                                 ByVal W3_1 As Boolean, ByVal W3_2 As Boolean, ByVal W3_3 As Boolean, ByVal W3_4 As Boolean, _
                                 ByVal W3_5 As Boolean, ByVal W3_6 As Boolean, ByVal W3_7 As Boolean, _
                                 ByVal W4_1 As Boolean, ByVal W4_2 As Boolean, ByVal W4_3 As Boolean, ByVal W4_4 As Boolean, _
                                 ByVal W4_5 As Boolean, ByVal W4_6 As Boolean, ByVal W4_7 As Boolean) As Integer

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT id_usuario, id_tienda " & _
                                               "FROM AS_CatRutas " & _
                                               "WHERE id_usuario='" & IDUsuario & "' " & _
                                               "AND id_tienda=" & IDTienda & " ")

        If Tabla.Rows.Count = 1 Then
            Me.lblaviso.Text = "LA TIENDA YA EXISTE EN UNA RUTA."
        Else
            Dim cnn As New SqlConnection(ConexionMars.localSqlServer)
            cnn.Open()

            Dim SQLNuevo As New SqlCommand("INSERT INTO AS_CatRutas " & _
                    "(id_usuario, id_tienda," & _
                    "W1_1,W1_2,W1_3,W1_4,W1_5,W1_6,W1_7," & _
                    "W2_1,W2_2,W2_3,W2_4,W2_5,W2_6,W2_7, " & _
                    "W3_1,W3_2,W3_3,W3_4,W3_5,W3_6,W3_7, " & _
                    "W4_1,W4_2,W4_3,W4_4,W4_5,W4_6,W4_7) " & _
                    "VALUES(@id_usuario, @id_tienda," & _
                    "@W1_1,@W1_2,@W1_3,@W1_4,@W1_5,@W1_6,@W1_7, " & _
                    "@W2_1,@W2_2,@W2_3,@W2_4,@W2_5,@W2_6,@W2_7, " & _
                    "@W3_1,@W3_2,@W3_3,@W3_4,@W3_5,@W3_6,@W3_7, " & _
                    "@W4_1,@W4_2,@W4_3,@W4_4,@W4_5,@W4_6,@W4_7)", cnn)
            With SQLNuevo
                .Parameters.AddWithValue("@id_usuario", IDUsuario)
                .Parameters.AddWithValue("@id_tienda", IDTienda)
                .Parameters.AddWithValue("@W1_1", W1_1)
                .Parameters.AddWithValue("@W1_2", W1_2)
                .Parameters.AddWithValue("@W1_3", W1_3)
                .Parameters.AddWithValue("@W1_4", W1_4)
                .Parameters.AddWithValue("@W1_5", W1_5)
                .Parameters.AddWithValue("@W1_6", W1_6)
                .Parameters.AddWithValue("@W1_7", W1_7)
                .Parameters.AddWithValue("@W2_1", W2_1)
                .Parameters.AddWithValue("@W2_2", W2_2)
                .Parameters.AddWithValue("@W2_3", W2_3)
                .Parameters.AddWithValue("@W2_4", W2_4)
                .Parameters.AddWithValue("@W2_5", W2_5)
                .Parameters.AddWithValue("@W2_6", W2_6)
                .Parameters.AddWithValue("@W2_7", W2_7)
                .Parameters.AddWithValue("@W3_1", W3_1)
                .Parameters.AddWithValue("@W3_2", W3_2)
                .Parameters.AddWithValue("@W3_3", W3_3)
                .Parameters.AddWithValue("@W3_4", W3_4)
                .Parameters.AddWithValue("@W3_5", W3_5)
                .Parameters.AddWithValue("@W3_6", W3_6)
                .Parameters.AddWithValue("@W3_7", W3_7)
                .Parameters.AddWithValue("@W4_1", W4_1)
                .Parameters.AddWithValue("@W4_2", W4_2)
                .Parameters.AddWithValue("@W4_3", W4_3)
                .Parameters.AddWithValue("@W4_4", W4_4)
                .Parameters.AddWithValue("@W4_5", W4_5)
                .Parameters.AddWithValue("@W4_6", W4_6)
                .Parameters.AddWithValue("@W4_7", W4_7)
                .ExecuteNonQuery()
                .Dispose()
            End With

            cnn.Close()
            cnn.Dispose()

            If chkFormatoPrecios.Checked = True Then
                Dim Tabla_Pre As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                                       "SELECT id_usuario, id_tienda " & _
                                                     "FROM AS_Precios_CatRutas " & _
                                                     "WHERE id_usuario='" & IDUsuario & "' " & _
                                                     "AND id_tienda=" & IDTienda & " ")

                If Tabla_Pre.Rows.Count = 0 Then
                    BD.Execute(ConexionMars.localSqlServer, _
                               "INSERT INTO AS_Precios_CatRutas " & _
                               "(id_usuario, id_tienda) " & _
                               "VALUES('" & IDUsuario & "'," & IDTienda & ")")
                End If

                Tabla_Pre.Dispose()
            End If

            Tabla.Dispose()
        End If
    End Function

    Protected Sub ListEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListEstado.SelectedIndexChanged
        SQLLista()

        Lista.LlenaBox(ConexionMars.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", ListCadena)
        Lista.LlenaBox(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)

        CargarRuta()
    End Sub

    Private Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridRuta.Columns(4).Visible = False
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        If Not cmbPromotor.SelectedValue = "" Then
            CargarRuta()
            btnAgregar.Enabled = True
        End If

        pnlAgregar.Visible = False
        btnGuardarCambios.Enabled = True
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLLista()
        Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        pnlAgregar.Visible = False
    End Sub

    Protected Sub btnTerminar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTerminar.Click
        pnlAgregar.Visible = False
        chkFormatoPrecios.Checked = False
    End Sub

    Private Sub btnGuardarCambios_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarCambios.Click
        For I = 0 To CInt(Me.gridRuta.Rows.Count) - 1
            IDTienda = gridRuta.DataKeys(I).Value.ToString()
            Dim chkW1_1 As CheckBox = CType(Me.gridRuta.Rows(I).FindControl("chkW1_1"), CheckBox)
            Dim chkW1_2 As CheckBox = CType(Me.gridRuta.Rows(I).FindControl("chkW1_2"), CheckBox)
            Dim chkW1_3 As CheckBox = CType(Me.gridRuta.Rows(I).FindControl("chkW1_3"), CheckBox)
            Dim chkW1_4 As CheckBox = CType(Me.gridRuta.Rows(I).FindControl("chkW1_4"), CheckBox)
            Dim chkW1_5 As CheckBox = CType(Me.gridRuta.Rows(I).FindControl("chkW1_5"), CheckBox)
            Dim chkW1_6 As CheckBox = CType(Me.gridRuta.Rows(I).FindControl("chkW1_6"), CheckBox)
            Dim chkW1_7 As CheckBox = CType(Me.gridRuta.Rows(I).FindControl("chkW1_7"), CheckBox)
            Dim chkW2_1 As CheckBox = CType(Me.gridRuta.Rows(I).FindControl("chkW2_1"), CheckBox)
            Dim chkW2_2 As CheckBox = CType(Me.gridRuta.Rows(I).FindControl("chkW2_2"), CheckBox)
            Dim chkW2_3 As CheckBox = CType(Me.gridRuta.Rows(I).FindControl("chkW2_3"), CheckBox)
            Dim chkW2_4 As CheckBox = CType(Me.gridRuta.Rows(I).FindControl("chkW2_4"), CheckBox)
            Dim chkW2_5 As CheckBox = CType(Me.gridRuta.Rows(I).FindControl("chkW2_5"), CheckBox)
            Dim chkW2_6 As CheckBox = CType(Me.gridRuta.Rows(I).FindControl("chkW2_6"), CheckBox)
            Dim chkW2_7 As CheckBox = CType(Me.gridRuta.Rows(I).FindControl("chkW2_7"), CheckBox)
            Dim chkW3_1 As CheckBox = CType(Me.gridRuta.Rows(I).FindControl("chkW3_1"), CheckBox)
            Dim chkW3_2 As CheckBox = CType(Me.gridRuta.Rows(I).FindControl("chkW3_2"), CheckBox)
            Dim chkW3_3 As CheckBox = CType(Me.gridRuta.Rows(I).FindControl("chkW3_3"), CheckBox)
            Dim chkW3_4 As CheckBox = CType(Me.gridRuta.Rows(I).FindControl("chkW3_4"), CheckBox)
            Dim chkW3_5 As CheckBox = CType(Me.gridRuta.Rows(I).FindControl("chkW3_5"), CheckBox)
            Dim chkW3_6 As CheckBox = CType(Me.gridRuta.Rows(I).FindControl("chkW3_6"), CheckBox)
            Dim chkW3_7 As CheckBox = CType(Me.gridRuta.Rows(I).FindControl("chkW3_7"), CheckBox)
            Dim chkW4_1 As CheckBox = CType(Me.gridRuta.Rows(I).FindControl("chkW4_1"), CheckBox)
            Dim chkW4_2 As CheckBox = CType(Me.gridRuta.Rows(I).FindControl("chkW4_2"), CheckBox)
            Dim chkW4_3 As CheckBox = CType(Me.gridRuta.Rows(I).FindControl("chkW4_3"), CheckBox)
            Dim chkW4_4 As CheckBox = CType(Me.gridRuta.Rows(I).FindControl("chkW4_4"), CheckBox)
            Dim chkW4_5 As CheckBox = CType(Me.gridRuta.Rows(I).FindControl("chkW4_5"), CheckBox)
            Dim chkW4_6 As CheckBox = CType(Me.gridRuta.Rows(I).FindControl("chkW4_6"), CheckBox)
            Dim chkW4_7 As CheckBox = CType(Me.gridRuta.Rows(I).FindControl("chkW4_7"), CheckBox)


            Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                                   "select * from AS_CatRutas " & _
                                                   "WHERE id_usuario='" & cmbPromotor.SelectedValue & "' " & _
                                                   "AND id_tienda=" & IDTienda & "")


            If Tabla.Rows.Count = 1 Then
                Dim cnn As New SqlConnection(ConexionMars.localSqlServer)
                cnn.Open()

                Dim SQLEditar As New SqlCommand("execute AS_Editar_Ruta @id_tienda,@id_usuario, " & _
                                                "@W1_1,@W1_2,@W1_3,@W1_4,@W1_5,@W1_6,@W1_7," & _
                                                "@W2_1,@W2_2,@W2_3,@W2_4,@W2_5,@W2_6,@W2_7," & _
                                                "@W3_1,@W3_2,@W3_3,@W3_4,@W3_5,@W3_6,@W3_7," & _
                                                "@W4_1,@W4_2,@W4_3,@W4_4,@W4_5,@W4_6,@W4_7", cnn)

                With SQLEditar
                    .Parameters.AddWithValue("@id_tienda", IDTienda)
                    .Parameters.AddWithValue("@id_usuario", cmbPromotor.SelectedValue)
                    .Parameters.AddWithValue("@W1_1", chkW1_1.Checked)
                    .Parameters.AddWithValue("@W1_2", chkW1_2.Checked)
                    .Parameters.AddWithValue("@W1_3", chkW1_3.Checked)
                    .Parameters.AddWithValue("@W1_4", chkW1_4.Checked)
                    .Parameters.AddWithValue("@W1_5", chkW1_5.Checked)
                    .Parameters.AddWithValue("@W1_6", chkW1_6.Checked)
                    .Parameters.AddWithValue("@W1_7", chkW1_7.Checked)
                    .Parameters.AddWithValue("@W2_1", chkW2_1.Checked)
                    .Parameters.AddWithValue("@W2_2", chkW2_2.Checked)
                    .Parameters.AddWithValue("@W2_3", chkW2_3.Checked)
                    .Parameters.AddWithValue("@W2_4", chkW2_4.Checked)
                    .Parameters.AddWithValue("@W2_5", chkW2_5.Checked)
                    .Parameters.AddWithValue("@W2_6", chkW2_6.Checked)
                    .Parameters.AddWithValue("@W2_7", chkW2_7.Checked)
                    .Parameters.AddWithValue("@W3_1", chkW3_1.Checked)
                    .Parameters.AddWithValue("@W3_2", chkW3_2.Checked)
                    .Parameters.AddWithValue("@W3_3", chkW3_3.Checked)
                    .Parameters.AddWithValue("@W3_4", chkW3_4.Checked)
                    .Parameters.AddWithValue("@W3_5", chkW3_5.Checked)
                    .Parameters.AddWithValue("@W3_6", chkW3_6.Checked)
                    .Parameters.AddWithValue("@W3_7", chkW3_7.Checked)
                    .Parameters.AddWithValue("@W4_1", chkW4_1.Checked)
                    .Parameters.AddWithValue("@W4_2", chkW4_2.Checked)
                    .Parameters.AddWithValue("@W4_3", chkW4_3.Checked)
                    .Parameters.AddWithValue("@W4_4", chkW4_4.Checked)
                    .Parameters.AddWithValue("@W4_5", chkW4_5.Checked)
                    .Parameters.AddWithValue("@W4_6", chkW4_6.Checked)
                    .Parameters.AddWithValue("@W4_7", chkW4_7.Checked)
                    .ExecuteNonQuery()
                    .Dispose()
                End With

                cnn.Dispose()
                cnn.Close()
            End If

            Tabla.Dispose()
        Next

        btnGuardarCambios.Enabled = False
    End Sub
End Class