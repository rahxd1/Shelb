Imports System.Data.SqlClient

Partial Public Class AdminRutasFluid
    Inherits System.Web.UI.Page

    Dim SQLRegion, SQLEstado As String
    Dim RegionSel As String

    Sub CargaSQL()
        RegionSel = Acciones.Slc.cmb("id_region", ListRegion.SelectedValue)

        SQLRegion = "SELECT DISTINCT id_region, nombre_region " & _
                    "FROM Regiones WHERE id_region <>0 " & _
                    "ORDER BY nombre_region"

        SQLEstado = "SELECT DISTINCT TI.id_estado, ES.nombre_estado " & _
                    "FROM Tiendas as TI " & _
                    "INNER JOIN Estados as ES ON TI.id_estado= ES.id_estado " & _
                    "WHERE TI.id_tienda <>0 " & _
                    " " + RegionSel + " " & _
                    "ORDER BY ES.nombre_estado "

        CargarRuta()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CargaSQL()

            Lista.LlenaBox(ConexionFluidmaster.localSqlServer, SQLRegion, "nombre_region", "id_region", ListRegion)
            Lista.LlenaBox(ConexionFluidmaster.localSqlServer, SQLEstado, "nombre_estado", "id_estado", ListEstado)

            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, "SELECT distinct id_usuario FROM Usuarios WHERE id_tipo=1", "id_usuario", "id_usuario", cmbPromotor)
        End If
    End Sub

    Sub CargarRuta()
        CargaGrilla(ConexionFluidmaster.localSqlServer, _
                    "SELECT DISTINCT RUT.id_usuario,RUT.id_region,REG.nombre_region, " & _
                    "RUT.id_estado, ES.nombre_estado " & _
                    "FROM CatRutas AS RUT " & _
                    "INNER JOIN Regiones AS REG ON RUT.id_region= REG.id_region " & _
                    "INNER JOIN Estados as ES ON ES.id_estado=RUT.id_estado " & _
                    "WHERE RUT.id_usuario = '" & cmbPromotor.SelectedValue & "' " & _
                    "order by REG.nombre_region",  Me.gridRuta)
    End Sub

    Sub Cargar()
        CargarRuta()
        pnlRuta.Visible = True
        pnlAgregar.Visible = False
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAgregar.Click
        pnlAgregar.Visible = True
    End Sub

    Private Sub gridRuta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRuta.RowDataBound
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.ForeColor = Drawing.Color.White
            e.Row.Cells(4).Text = gridRuta.Rows.Count & " tiendas"
        End If
    End Sub

    Private Sub gridRuta_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridRuta.RowDeleting
        BD.Execute(ConexionFluidmaster.localSqlServer, _
                   "DELETE FROM CatRutas WHERE id_usuario='" & cmbPromotor.Text & "' " & _
                   "AND id_region=" & gridRuta.Rows(e.RowIndex).Cells(4).Text & " " & _
                   "AND id_estado=" & gridRuta.Rows(e.RowIndex).Cells(2).Text & "")

        CargarRuta()
    End Sub

    Protected Sub ListEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListEstado.SelectedIndexChanged
        lblAgregada.Text = ""
    End Sub

    Protected Sub ListRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListRegion.SelectedIndexChanged
        CargaSQL()

        Lista.LlenaBox(ConexionFluidmaster.localSqlServer, SQLEstado, "nombre_estado", "id_estado", ListEstado)

        lblAgregada.Text = ""
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAceptar.Click
        'Dim Cadena As Integer

        'Using cnn As New SqlConnection(ConexionFluidmaster.localSqlServer)
        '    cnn.Open()
        '    Dim SQLBusca As New SqlCommand("SELECT * FROM Tiendas WHERE id_tienda=" & Tienda & "", cnn)
        '    Dim tabla As New DataTable
        '    Dim da As New SqlDataAdapter(SQLBusca)
        '    da.Fill(tabla)
        '    If tabla.Rows.Count = 1 Then
        '        Cadena = tabla.Rows(0)("id_cadena") : End If
        'End Using
        'End If

        'If ListCadena.SelectedValue = "" Then
        '    If ListTienda.SelectedValue = "" Then
        '        lblaviso.Text = "Selecciona Tienda o Cadena"
        '        Exit Sub : End If
        'Else
        '    Cadena = ListCadena.SelectedValue
        'End If

        'AgregaTienda(cmbPromotor.Text, Cadena, Tienda)
        'CargarRuta()

        'SQLLista("SELECT id_tienda, nombre FROM Tiendas WHERE id_tienda NOT IN(SELECT id_tienda FROM CatRutas) ORDER BY nombre", "nombre", "id_tienda", ListTienda)

    End Sub

    Public Function AgregaTienda(ByVal IDUsuario As String, ByVal IDCadena As Integer, ByVal IDTienda As Integer) As Integer
        'Using cnn As New SqlConnection(ConexionFluidmaster.localSqlServer)
        '    cnn.Open()

        '    Dim SQLBusca As New SqlCommand("SELECT id_usuario, id_tienda " & _
        '                                 "FROM CatRutas " & _
        '                                 "WHERE id_usuario=@id_usuario " & _
        '                                 "AND id_tienda =@id_tienda AND id_cadena=@id_cadena", cnn)
        '    SQLBusca.Parameters.AddWithValue("@id_usuario", IDUsuario)
        '    SQLBusca.Parameters.AddWithValue("@id_tienda", IDTienda)
        '    SQLBusca.Parameters.AddWithValue("@id_cadena", IDCadena)

        '    Dim tabla As New DataTable
        '    Dim da As New SqlDataAdapter(SQLBusca)
        '    da.Fill(tabla)

        '    If tabla.Rows.Count = 1 Then
        '        Me.lblAgregada.Text = "La tienda ya existe en una Ruta."
        '    Else
        '        Dim SQLGuardar As New SqlCommand("INSERT INTO CatRutas " & _
        '                "(id_usuario,id_cadena,id_tienda) VALUES(@id_usuario,@id_cadena,@id_tienda)", cnn)
        '        SQLGuardar.Parameters.AddWithValue("@id_usuario", IDUsuario)
        '        SQLGuardar.Parameters.AddWithValue("@id_tienda", IDTienda)
        '        SQLGuardar.Parameters.AddWithValue("@id_cadena", IDCadena)
        '        SQLGuardar.ExecuteNonQuery()
        '        SQLGuardar.Dispose()

        '        Dim Agregado As String
        '        If Not ListTienda.SelectedValue = "" Then
        '            Agregado = "Se agrego la tienda '" & ListTienda.SelectedItem.ToString & "' a la Ruta actual." : Else
        '            Agregado = "Se agrego la cadena '" & ListCadena.SelectedItem.ToString & "' a la Ruta actual." : End If

        '        lblAgregada.Text = Agregado
        '    End If

        '    cnn.Close()
        '    cnn.Dispose()
        'End Using

        'CargarRuta()
    End Function

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        Cargar()
    End Sub

    Protected Sub btnTerminar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTerminar.Click
        pnlAgregar.Visible = False
        pnlRuta.Visible = True

        lblAgregada.Text = ""
    End Sub

    Private Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridRuta.Columns(2).Visible = False
        gridRuta.Columns(4).Visible = False
    End Sub

End Class