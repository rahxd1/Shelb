Imports System.Data.SqlClient

Partial Public Class AdminRutasNM
    Inherits System.Web.UI.Page

    Dim CadenaSQL, EstadoSQL, TiendaSQL, PromotorSQL, RegionSQL As String
    Dim CadenaSel, EstadoSel, RegionSel As String

    Sub CargaSQLLista()
        CadenaSel = Acciones.Slc.cmb("id_cadena", ListCadena.SelectedValue)
        EstadoSel = Acciones.Slc.cmb("id_estado", ListEstado.SelectedValue)
        RegionSel = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)

        EstadoSQL = "SELECT DISTINCT nombre_estado, id_estado " & _
                    "FROM View_Tiendas_NM " & _
                    "WHERE id_tienda NOT IN(SELECT id_tienda FROM NewMix_CatRutas) " & _
                    "ORDER BY nombre_estado"

        CadenaSQL = "SELECT DISTINCT id_cadena, nombre_cadena " & _
                    "FROM View_Tiendas_NM as TI " & _
                    "WHERE id_tienda NOT IN(SELECT id_tienda FROM NewMix_CatRutas) " & _
                    " " + EstadoSel + " " & _
                    "ORDER BY nombre_cadena"

        TiendaSQL = "SELECT id_tienda, nombre " & _
                    "FROM View_Tiendas_NM " & _
                    "WHERE id_tienda NOT IN(SELECT id_tienda FROM NewMix_CatRutas) " & _
                    " " + CadenaSel + " " & _
                    " " + EstadoSel + " " & _
                    "ORDER BY nombre"

        RegionSQL = "SELECT DISTINCT REG.id_region, REG.nombre_region FROM Usuarios as US " & _
                    "INNER JOIN Regiones AS REG ON US.id_region=REG.id_region " & _
                    "WHERE id_proyecto=25 AND REG.id_region<>0"

        PromotorSQL = "SELECT id_usuario FROM Usuarios where id_proyecto=25 AND id_tipo=1 " & _
                    " " + RegionSel + " " & _
                    "ORDER BY id_usuario"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LstProyectos(ConexionHerradura.localSqlServer, Me.gridProyectos)

            CargaSQLLista()
            CargarRuta()

            Lista.LlenaBox(ConexionHerradura.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", ListCadena)
            Lista.LlenaBox(ConexionHerradura.localSqlServer, EstadoSQL, "nombre_estado", "id_estado", ListEstado)
            Lista.LlenaBox(ConexionHerradura.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)

            Combo.LlenaDrop(ConexionHerradura.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
        End If
    End Sub

    Sub CargarRuta()
        CargaGrilla(ConexionHerradura.localSqlServer, _
                    "SELECT DISTINCT RUT.id_usuario, TI.nombre_cadena, RUT.id_tienda, TI.nombre " & _
                    "FROM NewMix_CatRutas AS RUT  " & _
                    "INNER JOIN View_Tiendas_NM AS TI ON RUT.id_tienda = TI.id_tienda " & _
                    "WHERE RUT.id_usuario = '" & cmbPromotor.SelectedValue & "' " & _
                    "ORDER BY TI.nombre, TI.nombre_cadena", Me.gridRuta)
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAgregar.Click
        pnlAgregar.Visible = True

        CargaSQLLista()

        Lista.LlenaBox(ConexionHerradura.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", ListCadena)
        Lista.LlenaBox(ConexionHerradura.localSqlServer, EstadoSQL, "nombre_estado", "id_estado", ListEstado)
        Lista.LlenaBox(ConexionHerradura.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)

        CargarRuta()
    End Sub

    Private Sub gridRuta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRuta.RowDataBound
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(4).Text = gridRuta.Rows.Count & " tiendas"
        End If
    End Sub

    Private Sub gridRuta_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridRuta.RowDeleting
        BD.Execute(ConexionHerradura.localSqlServer, _
                   "DELETE FROM NewMix_CatRutas WHERE id_usuario = '" & cmbPromotor.Text & "' " & _
                   "AND id_tienda= '" & gridRuta.Rows(e.RowIndex).Cells(3).Text & "'")


        CargarRuta()
        pnlAgregar.Visible = False
    End Sub

    Protected Sub ListCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListCadena.SelectedIndexChanged
        CargaSQLLista()

        Lista.LlenaBox(ConexionHerradura.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)

        CargarRuta()
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAceptar.Click
        AgregaTienda(cmbPromotor.Text, ListTienda.SelectedValue)
        CargarRuta()

        CargaSQLLista()
        Lista.LlenaBox(ConexionHerradura.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)
    End Sub

    Public Function AgregaTienda(ByVal IDUsuario As String, ByVal IDTienda As Integer) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionHerradura.localSqlServer, _
                                               "SELECT id_usuario, id_tienda " & _
                                                "FROM NewMix_CatRutas " & _
                                                "WHERE id_usuario='" & IDUsuario & "' " & _
                                                "AND id_tienda=" & IDTienda & " ")
        If Tabla.Rows.Count = 1 Then
            Me.lblaviso.Text = "LA TIENDA YA EXISTE EN UNA RUTA."
        Else
            BD.Execute(ConexionHerradura.localSqlServer, _
                       "INSERT INTO NewMix_CatRutas " & _
                       "(id_usuario, id_tienda) " & _
                       "VALUES('" & IDUsuario & "', " & IDTienda & ")")
        End If

        Tabla.Dispose()
    End Function

    Protected Sub ListEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListEstado.SelectedIndexChanged
        CargaSQLLista()

        Lista.LlenaBox(ConexionHerradura.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", ListCadena)
        Lista.LlenaBox(ConexionHerradura.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)

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
        Combo.LlenaDrop(ConexionHerradura.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        pnlAgregar.Visible = False
    End Sub

    Protected Sub btnTerminar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTerminar.Click
        pnlAgregar.Visible = False
    End Sub
End Class