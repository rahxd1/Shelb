﻿Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports procomlcd

Partial Public Class AdminUsuariosFluid
    Inherits System.Web.UI.Page

    Dim FiltroRegion, FiltroTipoUsuario, FiltroUsuario As String
    Dim RegionSQL, UsuarioSQL, TipoUsuarioSQL As String
    Dim RegionSel, TipoSel As String

    Sub SQLCombos()
        RegionSel = Acciones.Slc.cmb("id_region", cmbFiltroRegion.SelectedValue)
        TipoSel = Acciones.Slc.cmb("id_tipo", cmbFiltroTipoUsuario.SelectedValue)

        RegionSQL = "SELECT DISTINCT REG.id_region, REG.nombre_region " & _
                    "FROM Regiones as REG " & _
                    "ORDER BY REG.nombre_region "

        UsuarioSQL = "SELECT id_usuario FROM Usuarios " & _
                    "WHERE id_usuario <>'' " & _
                    " " + RegionSel + " " & _
                    " " + TipoSel + " " & _
                    "ORDER BY nombre"

        TipoUsuarioSQL = "SELECT * FROM Tipo_Usuarios ORDER BY tipo_usuario"
    End Sub

    Private Sub VerDatos(ByVal SeleccionIDUsuario As String)
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                                               "select * from Usuarios " & _
                                               "WHERE id_usuario='" & SeleccionIDUsuario & "'")
        If Tabla.Rows.Count > 0 Then
            lblAviso.Text = ""

            txtIDUsuario.Text = Tabla.Rows(0)("id_usuario")
            txtNombre.Text = Tabla.Rows(0)("nombre")
            cmbRegion.SelectedValue = Tabla.Rows(0)("id_region")
            cmbTipoUsuario.SelectedValue = Tabla.Rows(0)("id_tipo")
        End If

        Tabla.Dispose()
    End Sub

    Sub CargarUsuarios()
        FiltroTipoUsuario = Acciones.Slc.cmb("USU.id_tipo", cmbFiltroTipoUsuario.SelectedValue)
        FiltroRegion = Acciones.Slc.cmb("USU.id_region", cmbFiltroRegion.SelectedValue)
        FiltroUsuario = Acciones.Slc.cmb("USU.id_usuario", cmbFiltroUsuario.SelectedValue)

        CargaGrilla(ConexionFluidmaster.localSqlServer, _
                    "SELECT * FROM Usuarios as USU " & _
                    "INNER JOIN Tipo_Usuarios as TUSU ON TUSU.id_tipo = USU.id_tipo " & _
                    "WHERE id_usuario<>'' " & _
                    " " + FiltroRegion + " " & _
                    " " + FiltroTipoUsuario + " " & _
                    " " + FiltroUsuario + " " & _
                    "ORDER BY id_usuario", Me.gridUsuario)
    End Sub

    Protected Sub lnkNuevo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkNuevo.Click
        Borrar()

        pnlNuevo.Visible = True
        pnlConsulta.Visible = False
        txtIDUsuario.Focus()
        lblAviso.Text = ""
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Borrar()
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Guardar(txtIDUsuario.Text, txtNombre.Text, cmbRegion.Text, _
                     cmbTipoUsuario.SelectedValue)
        Borrar()
        CargarUsuarios()
    End Sub

    Public Function Guardar(ByVal id_usuario As String, ByVal nombre As String, _
                            ByVal id_region As Integer, ByVal tipousuario As Integer) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                                               "SELECT id_usuario From Usuarios " & _
                                               "WHERE id_usuario='" & id_usuario & "'")
        If Tabla.Rows.Count = 1 Then
            BD.Execute(ConexionFluidmaster.localSqlServer, _
                       "UPDATE Usuarios " & _
                       "SET nombre='" & nombre & "', id_region=" & id_region & ", id_tipo=" & tipousuario & " " & _
                       "WHERE id_usuario='" & id_usuario & "'")

            Me.lblAviso.Text = "Los cambios se realizaron correctamente."
        Else
            BD.Execute(ConexionFluidmaster.localSqlServer, _
                       "INSERT INTO Usuarios" & _
                       "(id_usuario, nombre, id_region, id_tipo) " & _
                       "VALUES('" & id_usuario & "', '" & nombre & "', " & id_region & ", " & tipousuario & ")")

            Me.lblAviso.Text = "Se guardo exitosamente la información."
        End If

        Tabla.Dispose()
        pnlNuevo.Visible = False
    End Function

    Sub Borrar()
        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
        txtIDUsuario.Text = ""
        txtNombre.Text = ""
        cmbTipoUsuario.SelectedValue = ""
        cmbRegion.SelectedValue = 0
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombos()

            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, TipoUsuarioSQL, "tipo_usuario", "id_tipo", cmbTipoUsuario)

            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, TipoUsuarioSQL, "tipo_usuario", "id_tipo", cmbFiltroTipoUsuario)
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, UsuarioSQL, "id_usuario", "id_usuario", cmbFiltroUsuario)
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbFiltroRegion)

            CargarUsuarios()
        End If
    End Sub

    Protected Sub lnkConsultas_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkConsultas.Click
        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
        lblAviso.Text = ""
        CargarUsuarios()
    End Sub

    Private Sub gridUsuario_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridUsuario.RowDeleting
        BD.Execute(ConexionFluidmaster.localSqlServer, _
                   "DELETE FROM Usuarios " & _
                   "WHERE id_usuario = '" & gridUsuario.Rows(e.RowIndex).Cells(2).Text & "'")
        CargarUsuarios()
    End Sub

    Private Sub gridUsuario_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gridUsuario.RowEditing
        If gridUsuario.Rows(e.NewEditIndex).Cells(2).Text = "" Then
            e.Cancel = True
            lblAviso.Text = "Error al intentar editar el usuario."
        Else
            lblAviso.Text = ""
            pnlNuevo.Visible = True
            pnlConsulta.Visible = False
            txtIDUsuario.Focus()
            VerDatos(gridUsuario.Rows(e.NewEditIndex).Cells(2).Text)
        End If
    End Sub

    Sub Filtrar()
        CargarUsuarios()
        gridUsuario.Visible = True
    End Sub

    Protected Sub cmbFiltroTipoUsuario_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFiltroTipoUsuario.SelectedIndexChanged
        SQLCombos()
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, UsuarioSQL, "id_usuario", "id_usuario", cmbFiltroUsuario)

        lblAviso.Text = ""
        Filtrar()
    End Sub

    Protected Sub cmbFiltroRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFiltroRegion.SelectedIndexChanged
        SQLCombos()
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, UsuarioSQL, "id_usuario", "id_usuario", cmbFiltroUsuario)

        lblAviso.Text = ""
        Filtrar()
    End Sub

    Protected Sub cmbFiltroUsuario_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFiltroUsuario.SelectedIndexChanged
        If cmbFiltroUsuario.SelectedValue = "" Then
            lblAviso.Text = "Error al intentar editar el usuario."
        Else
            lblAviso.Text = ""
            pnlNuevo.Visible = True
            pnlConsulta.Visible = False
            txtIDUsuario.Focus()
            VerDatos(cmbFiltroUsuario.SelectedValue)
        End If

        lblAviso.Text = ""
        Filtrar()
    End Sub
End Class