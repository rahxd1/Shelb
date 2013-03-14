Imports System.Data.SqlClient
Imports System.Security.Cryptography

Partial Public Class PermisosUsuarios
    Inherits System.Web.UI.Page

    Private Sub Permisos_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            Combo.LlenaDrop(ConexionAdmin.localSqlServer, _
                            "SELECT * FROM Tipo_Usuarios", "tipo_usuario", "id_tipo", cmbTipoUsuario)
            Combo.LlenaDrop(ConexionAdmin.localSqlServer, _
                            "SELECT * FROM Usuarios", "id_usuario", "id_usuario", cmbUsuario)
        End If
    End Sub

    Protected Sub cmbTipoUsuario_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbTipoUsuario.SelectedIndexChanged
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionAdmin.localSqlServer, _
                                                   "select * from Permisos where id_tipo=" & cmbTipoUsuario.SelectedValue & " ")

        If Tabla.Rows.Count > 0 Then
            If Tabla.Rows(0)("nuevo") = 1 Then
                chkNuevo.Checked = Acciones.Slc.chk_VF(1) : End If

            If Tabla.Rows(0)("edicion") = 1 Then
                chkEdicion.Checked = Acciones.Slc.chk_VF(1) : End If

            If Tabla.Rows(0)("eliminacion") = 1 Then
                chkEliminacion.Checked = Acciones.Slc.chk_VF(1) : End If

            If Tabla.Rows(0)("consultas") = 1 Then
                chkConsultas.Checked = Acciones.Slc.chk_VF(1) : End If
        End If

        Tabla.Dispose()
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Dim nuevo, edicion, eliminacion, consultas As String

        nuevo = chkNuevo.Checked
        edicion = chkEdicion.Checked
        eliminacion = chkEliminacion.Checked
        consultas = chkConsultas.Checked

        BD.Execute(ConexionAdmin.localSqlServer, _
                   "UPDATE Permisos " & _
                   "SET nuevo=" & nuevo & ", edicion=" & edicion & ", " & _
                   "eliminacion=" & eliminacion & ", consultas=" & consultas & " " & _
                   "WHERE id_tipo=" & cmbTipoUsuario.SelectedValue & "")
    End Sub
End Class