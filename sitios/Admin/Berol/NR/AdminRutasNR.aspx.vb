﻿Imports System.Data.SqlClient

Partial Public Class AdminRutasNR
    Inherits System.Web.UI.Page

    Dim CadenaSQL, EstadoSQL, TiendaSQL, PromotorSQL, RegionSQL As String
    Dim CadenaSel, EstadoSel, RegionSel As String

    Sub CargaSQLLista()
        CadenaSel = Acciones.Slc.cmb("id_cadena", ListCadena.SelectedValue)
        EstadoSel = Acciones.Slc.cmb("id_estado", ListEstado.SelectedValue)
        RegionSel = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)

        EstadoSQL = "SELECT distinct nombre_estado, id_estado " & _
                    "FROM View_Tiendas_NR " & _
                    "WHERE id_tienda NOT IN(SELECT id_tienda FROM NR_CatRutas) " & _
                    "ORDER BY nombre_estado"

        CadenaSQL = "SELECT DISTINCT id_cadena, nombre_cadena " & _
                    "FROM View_Tiendas_NR " & _
                    "WHERE id_tienda NOT IN(SELECT id_tienda FROM NR_CatRutas) " & _
                    " " + EstadoSel + " " & _
                    "ORDER BY nombre_cadena"

        TiendaSQL = "SELECT id_tienda, nombre " & _
                    "FROM View_Tiendas_NR as TI " & _
                    "WHERE id_tienda NOT IN(SELECT id_tienda FROM NR_CatRutas) " & _
                    " " + CadenaSel + EstadoSel + " " & _
                    "ORDER BY nombre"

        RegionSQL = "SELECT DISTINCT id_region, nombre_region " & _
                    "FROM View_Tiendas_NR "

        PromotorSQL = "SELECT id_usuario FROM Usuarios where id_proyecto=5 AND id_tipo=1 " & _
                    " " + RegionSel + " " & _
                    "ORDER BY id_usuario"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LstProyectos(ConexionBerol.localSqlServer, Me.gridProyectos)

            CargaSQLLista()
            CargarRuta()

            Lista.LlenaBox(ConexionBerol.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", ListCadena)
            Lista.LlenaBox(ConexionBerol.localSqlServer, EstadoSQL, "nombre_estado", "id_estado", ListEstado)
            Lista.LlenaBox(ConexionBerol.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)

            Combo.LlenaDrop(ConexionBerol.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
        End If
    End Sub

    Sub CargarRuta()
        CargaGrilla(ConexionBerol.localSqlServer, _
                    "SELECT DISTINCT RUT.id_usuario, TI.nombre_cadena, RUT.id_tienda, TI.nombre " & _
                    "FROM NR_CatRutas AS RUT  " & _
                    "INNER JOIN View_Tiendas_NR AS TI ON RUT.id_tienda = TI.id_tienda " & _
                    "WHERE RUT.id_usuario = '" & cmbPromotor.SelectedValue & "' " & _
                    "ORDER BY TI.nombre, TI.nombre_cadena", Me.gridRuta)
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAgregar.Click
        pnlAgregar.Visible = True

        CargaSQLLista()

        Lista.LlenaBox(ConexionBerol.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", ListCadena)
        Lista.LlenaBox(ConexionBerol.localSqlServer, EstadoSQL, "nombre_estado", "id_estado", ListEstado)
        Lista.LlenaBox(ConexionBerol.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)

        CargarRuta()
    End Sub

    Private Sub gridRuta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRuta.RowDataBound
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(4).Text = gridRuta.Rows.Count & " tiendas"
        End If
    End Sub

    Private Sub gridRuta_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridRuta.RowDeleting
        BD.Execute(ConexionBerol.localSqlServer, _
                   "DELETE FROM NR_CatRutas " & _
                   "WHERE id_usuario = '" & cmbPromotor.Text & "' " & _
                   "AND id_tienda= '" & gridRuta.Rows(e.RowIndex).Cells(3).Text & "'")

        CargarRuta()
        pnlAgregar.Visible = False
    End Sub

    Protected Sub ListCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListCadena.SelectedIndexChanged
        CargaSQLLista()

        Lista.LlenaBox(ConexionBerol.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)

        CargarRuta()
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAceptar.Click
        AgregaTienda(cmbPromotor.Text, ListTienda.SelectedValue)
        CargarRuta()

        CargaSQLLista()
        Lista.LlenaBox(ConexionBerol.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)
    End Sub

    Public Function AgregaTienda(ByVal IDUsuario As String, ByVal IDTienda As String) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                               "SELECT id_usuario, id_tienda " & _
                                               "FROM NR_CatRutas " & _
                                               "WHERE id_usuario='" & IDUsuario & "' " & _
                                               "AND id_tienda =" & IDTienda & " ")
        If Tabla.Rows.Count = 1 Then
            Me.lblaviso.Text = "LA TIENDA YA EXISTE EN UNA RUTA."
        Else
            BD.Execute(ConexionBerol.localSqlServer, _
                       "INSERT INTO NR_CatRutas " & _
                       "(id_usuario, id_tienda) " & _
                       "VALUES('" & IDUsuario & "', " & IDTienda & ")")
        End If
    End Function

    Protected Sub ListEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListEstado.SelectedIndexChanged
        CargaSQLLista()

        Lista.LlenaBox(ConexionBerol.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", ListCadena)
        Lista.LlenaBox(ConexionBerol.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)

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
        Combo.LlenaDrop(ConexionBerol.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        pnlAgregar.Visible = False
    End Sub

    Protected Sub btnTerminar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTerminar.Click
        pnlAgregar.Visible = False
    End Sub
End Class