Imports System.Data.SqlClient

Partial Public Class AdminRutasMarsConv
    Inherits System.Web.UI.Page

    Dim CadenaSQL, EstadoSQL, TiendaSQL, PromotorSQL, RegionSQL As String
    Dim CadenaSel, EstadoSel, RegionSel As String

    Sub SQLLista()
        CadenaSel = Acciones.Slc.cmb("id_cadena", ListCadena.SelectedValue)
        EstadoSel = Acciones.Slc.cmb("id_estado", ListEstado.SelectedValue)
        RegionSel = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)

        RegionSQL = "SELECT DISTINCT id_region, nombre_region FROM View_Tiendas_Conv " & _
                    "WHERE id_region<>0 "

        PromotorSQL = "SELECT id_usuario FROM Usuarios where id_proyecto=39 AND id_tipo=1 " & _
                    " " + RegionSel + " " & _
                    "ORDER BY id_usuario"

        EstadoSQL = "SELECT DISTINCT nombre_estado, id_estado " & _
                    "FROM View_Tiendas_Conv " & _
                    "WHERE id_tienda NOT IN(SELECT id_tienda FROM Conv_CatRutas) " & _
                    " " + RegionSel + " " & _
                    "ORDER BY nombre_estado"

        CadenaSQL = "SELECT DISTINCT id_cadena, nombre_cadena " & _
                    "FROM View_Tiendas_Conv " & _
                    "WHERE id_tienda NOT IN(SELECT id_tienda FROM Conv_CatRutas) " & _
                    " " + RegionSel + EstadoSel + " " & _
                    "ORDER BY nombre_cadena"

        TiendaSQL = "SELECT DISTINCT id_tienda, nombre " & _
                    "FROM View_Tiendas_Conv  " & _
                    "WHERE id_tienda NOT IN(SELECT id_tienda FROM Conv_CatRutas) " & _
                    " " + RegionSel + CadenaSel + EstadoSel + " " & _
                    "ORDER BY nombre"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LstProyectos(ConexionMars.localSqlServer, Me.gridProyectos)

            CargarRuta()
            SQLLista()

            Lista.LlenaBox(ConexionMars.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", ListCadena)
            Lista.LlenaBox(ConexionMars.localSqlServer, EstadoSQL, "nombre_estado", "id_estado", ListEstado)
            Lista.LlenaBox(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)

            Combo.LlenaDrop(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
        End If
    End Sub

    Sub CargarRuta()
        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT DISTINCT RUT.id_usuario, TI.nombre_cadena, RUT.id_tienda, TI.nombre " & _
                    "FROM Conv_CatRutas AS RUT  " & _
                    "INNER JOIN View_Tiendas_Conv AS TI ON RUT.id_tienda = TI.id_tienda " & _
                    "WHERE RUT.id_usuario='" & cmbPromotor.Text & "' " & _
                    "ORDER BY TI.nombre, TI.nombre_cadena", Me.gridRuta)
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
            e.Row.Cells(4).Text = gridRuta.Rows.Count & " tiendas"
        End If
    End Sub

    Private Sub gridRuta_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridRuta.RowDeleting
        BD.Execute(ConexionMars.localSqlServer, _
                   "DELETE FROM Conv_CatRutas " & _
                   "WHERE id_usuario = '" & cmbPromotor.SelectedValue & "' " & _
                   "AND id_tienda= '" & gridRuta.Rows(e.RowIndex).Cells(3).Text & "'")

        CargarRuta()
        pnlAgregar.Visible = False
    End Sub

    Protected Sub ListCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListCadena.SelectedIndexChanged
        SQLLista()

        Lista.LlenaBox(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)

        CargarRuta()
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAceptar.Click
        AgregaTienda(cmbPromotor.Text, ListTienda.SelectedValue, ListCadena.SelectedValue)
        CargarRuta()

        SQLLista()
        Lista.LlenaBox(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)
    End Sub

    Public Function AgregaTienda(ByVal IDUsuario As String, ByVal IDTienda As String, ByVal IDCadena As Integer) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT id_usuario, id_tienda " & _
                                               "FROM Conv_CatRutas " & _
                                               "WHERE id_usuario='" & IDUsuario & "' " & _
                                               "AND id_tienda=" & IDTienda & " " & _
                                               "AND id_cadena=" & IDCadena & " ")
        If Tabla.Rows.Count = 1 Then
            Me.lblaviso.Text = "LA TIENDA YA EXISTE EN UNA RUTA."
        Else
            BD.Execute(ConexionMars.localSqlServer, _
                       "INSERT INTO Conv_CatRutas " & _
                       "(id_usuario, id_tienda, id_cadena) " & _
                       "VALUES('" & IDUsuario & "'," & IDTienda & "," & IDCadena & ")")
        End If

        Tabla.Dispose()
    End Function

    Protected Sub ListEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListEstado.SelectedIndexChanged
        SQLLista()

        Lista.LlenaBox(ConexionMars.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", ListCadena)
        Lista.LlenaBox(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)

        CargarRuta()
    End Sub

    Private Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridRuta.Columns(3).Visible = False
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        If Not cmbPromotor.SelectedValue = "" Then
            CargarRuta()
            btnAgregar.Enabled = True
        End If

        pnlAgregar.Visible = False
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLLista()

        Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        pnlAgregar.Visible = False
    End Sub

    Protected Sub btnTerminar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTerminar.Click
        pnlAgregar.Visible = False
    End Sub
End Class