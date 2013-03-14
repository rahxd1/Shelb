Imports System.Data.SqlClient

Partial Public Class AdminRutasSYMAC
    Inherits System.Web.UI.Page

    Dim CadenaSQL, EstadoSQL, TiendaSQL, PromotorSQL, RegionSQL As String
    Dim CadenaSel, EstadoSel, RegionSel As String

    Sub CargaSQLLista()
        CadenaSel = Acciones.Slc.cmb("id_cadena", ListCadena.SelectedValue)
        EstadoSel = Acciones.Slc.cmb("id_estado", ListEstado.SelectedValue)
        RegionSel = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)

        RegionSQL = "SELECT DISTINCT id_region, nombre_region " & _
                    "FROM View_SYM_AC_Tiendas " & _
                    "WHERE id_region<>0 "

        PromotorSQL = "SELECT id_usuario FROM Usuarios where id_proyecto=31 AND id_tipo=1 " & _
                    " " + RegionSel + " " & _
                    "ORDER BY id_usuario"

        EstadoSQL = "SELECT DISTINCT nombre_estado, id_estado " & _
                    "FROM View_SYM_AC_Tiendas " & _
                    "WHERE id_tienda NOT IN(SELECT id_tienda FROM AC_CatRutas) " & _
                    " " + RegionSel + " " & _
                    "ORDER BY nombre_estado"

        CadenaSQL = "SELECT DISTINCT id_cadena, nombre_cadena " & _
                    "FROM View_SYM_AC_Tiendas " & _
                    "WHERE id_tienda NOT IN(SELECT DISTINCT id_cadena " & _
                    "FROM AC_CatRutas as RUT INNER JOIN AC_Tiendas as TI ON TI.id_tienda = RUT.id_tienda) " & _
                    " " + RegionSel + EstadoSel + " " & _
                    "ORDER BY nombre_cadena"

        TiendaSQL = "SELECT id_tienda, no_tienda+' '+nombre + ' ('+ciudad+')' nombre " & _
                    "FROM View_SYM_AC_Tiendas as TI " & _
                    "WHERE id_tienda NOT IN(SELECT id_tienda FROM AC_CatRutas) " & _
                    " " + RegionSel + CadenaSel + EstadoSel + " " & _
                    "ORDER BY nombre"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LstProyectos(ConexionSYM.localSqlServer, Me.gridProyectos)

            CargaSQLLista()

            Lista.LlenaBox(ConexionSYM.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", ListCadena)
            Lista.LlenaBox(ConexionSYM.localSqlServer, EstadoSQL, "nombre_estado", "id_estado", ListEstado)
            Lista.LlenaBox(ConexionSYM.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)

            Combo.LlenaDrop(ConexionSYM.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        End If
    End Sub

    Sub CargarRuta()
        CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT DISTINCT RUT.id_usuario, TI.nombre_cadena, RUT.id_tienda, TI.nombre " & _
                    "FROM AC_CatRutas AS RUT  " & _
                    "INNER JOIN View_SYM_AC_Tiendas AS TI ON RUT.id_tienda = TI.id_tienda " & _
                    "WHERE RUT.id_usuario = '" & cmbPromotor.SelectedValue & "' " & _
                    "ORDER BY TI.nombre, TI.nombre_cadena", Me.gridRuta)
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAgregar.Click
        pnlAgregar.Visible = True

        CargaSQLLista()

        Lista.LlenaBox(ConexionSYM.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", ListCadena)
        Lista.LlenaBox(ConexionSYM.localSqlServer, EstadoSQL, "nombre_estado", "id_estado", ListEstado)
        Lista.LlenaBox(ConexionSYM.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)

        CargarRuta()
    End Sub

    Private Sub gridRuta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRuta.RowDataBound
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(4).Text = gridRuta.Rows.Count & " tiendas"
        End If
    End Sub

    Private Sub gridRuta_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridRuta.RowDeleting
        BD.Execute(ConexionSYM.localSqlServer, _
                   "DELETE FROM AC_CatRutas WHERE id_usuario = '" & cmbPromotor.SelectedValue & "' " & _
                   "AND id_tienda= '" & gridRuta.Rows(e.RowIndex).Cells(3).Text & "'")

        CargarRuta()
        pnlAgregar.Visible = False
    End Sub

    Protected Sub ListCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListCadena.SelectedIndexChanged
        CargaSQLLista()

        Lista.LlenaBox(ConexionSYM.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)

        CargarRuta()
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAceptar.Click
        AgregaTienda(cmbPromotor.Text, ListTienda.SelectedValue)
        CargarRuta()

        CargaSQLLista()
        Lista.LlenaBox(ConexionSYM.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)
    End Sub

    Public Function AgregaTienda(ByVal IDUsuario As String, ByVal IDTienda As String) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT id_usuario, id_tienda " & _
                                               "FROM AC_CatRutas " & _
                                               "WHERE id_usuario='" & IDUsuario & "' " & _
                                               "AND id_tienda=" & IDTienda & " ")
        If Tabla.Rows.Count = 1 Then
            Me.lblaviso.Text = "LA TIENDA YA EXISTE EN UNA RUTA."
        Else
            BD.Execute(ConexionSYM.localSqlServer, _
                       "INSERT INTO AC_CatRutas " & _
                       "(id_usuario, id_tienda) " & _
                       "VALUES('" & IDUsuario & "'," & IDTienda & ")")
        End If

        Tabla.Dispose()
    End Function

    Protected Sub ListEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListEstado.SelectedIndexChanged
        CargaSQLLista()

        Lista.LlenaBox(ConexionSYM.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", ListCadena)
        Lista.LlenaBox(ConexionSYM.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)

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
        CargaSQLLista()
        Combo.LlenaDrop(ConexionSYM.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        pnlAgregar.Visible = False
    End Sub

    Protected Sub btnTerminar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTerminar.Click
        pnlAgregar.Visible = False
    End Sub
End Class