Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports procomlcd

Partial Public Class CambioContrasena
    Inherits System.Web.UI.Page

    Dim FiltroDepartamento, FiltroTipoUsuario, FiltroCliente As String
    Dim SeleccionIDUsuario As String

    Private Sub VerDatos(ByVal ID_Usuario As String)
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionAdmin.localSqlServer, _
                                                   "SELECT * FROM Usuarios WHERE id_usuario= '" & ID_Usuario & "'")
        If Tabla.Rows.Count > 0 Then
            lblUsuario.Text = Tabla.Rows(0)("id_usuario")
        End If

        Tabla.Dispose()
    End Sub

    Sub CargarUsuarios()
        FiltroCliente = Acciones.Slc.cmb("USU.id_cliente", cmbFiltroCliente.SelectedValue)
        FiltroTipoUsuario = Acciones.Slc.cmb("USU.id_tipo", cmbFiltroTipoUsuario.SelectedValue)
        FiltroDepartamento = Acciones.Slc.cmb("USU.id_departamento", cmbFiltroDepartamento.SelectedValue)

        CargaGrilla(ConexionAdmin.localSqlServer, "SELECT USU.id_usuario, USU.nombre, TUSU.tipo_usuario " & _
                    "FROM Usuarios as USU " & _
                    "INNER JOIN Tipo_Usuarios TUSU ON USU.id_tipo = TUSU.id_tipo " & _
                    "WHERE USU.id_usuario<>'' " & _
                    " " + FiltroCliente + " " & _
                    " " + FiltroTipoUsuario + " " & _
                    " " + FiltroDepartamento + " " & _
                    "ORDER BY USU.id_usuario", gridUsuario)
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Borrar()
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        If txtPassword.Text = txtPasswordConfirma.Text Then
            Guardar(lblUsuario.Text, txtPassword.Text)
            Borrar()
        Else
            lblAviso.Text = "La contraseña no coincide con la comprobación."
        End If
    End Sub

    Public Function Guardar(ByVal id_usuario As String, ByVal password As String) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionAdmin.localSqlServer, _
                                                   "SELECT id_usuario From Usuarios " & _
                                                   "WHERE id_usuario='" & id_usuario & "'")

        If Tabla.Rows.Count = 1 Then
            Dim clave As String
            clave = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1")

            BD.Execute(ConexionAdmin.localSqlServer, _
                       "UPDATE Acceso " & _
                       "SET password='" & clave & "' " & _
                       "WHERE id_usuario='" & id_usuario & "' ")

            Me.lblAviso.Text = "SE CAMBIO LA CONTRASEÑA"
        End If

        Tabla.Dispose()
    End Function

    Sub Borrar()
        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
        txtPassword.Text = ""
        txtPasswordConfirma.Text = ""
        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
    End Sub

    Private Function generarClaveSHA1(ByVal clave As String) As String
        Dim enc As New UTF8Encoding
        Dim data() As Byte = enc.GetBytes(clave)
        Dim result() As Byte

        Dim sha As New SHA1CryptoServiceProvider()
        result = sha.ComputeHash(data)

        Dim sb As New StringBuilder
        For i As Integer = 0 To result.Length - 1
            sb.Append(result(i).ToString("X2"))
        Next

        Return sb.ToString()
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Combo.LlenaDrop(ConexionAdmin.localSqlServer, _
                            "SELECT * FROM Tipo_Usuarios order by tipo_usuario", "tipo_usuario", "id_tipo", cmbFiltroTipoUsuario)
            Combo.LlenaDrop(ConexionAdmin.localSqlServer, _
                            "SELECT * FROM Departamentos order by nombre_departamento", "nombre_departamento", "id_departamento", cmbFiltroDepartamento)
            Combo.LlenaDrop(ConexionAdmin.localSqlServer, _
                            "SELECT * FROM Clientes ORDER BY nombre_cliente", "nombre_cliente", "id_cliente", cmbFiltroCliente)
        End If
    End Sub

    Private Sub gridUsuario_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gridUsuario.RowEditing
        If gridUsuario.Rows(e.NewEditIndex).Cells(1).Text = "" Then
            e.Cancel = True
            lblAviso.Text = "Error al intentar editar el usuario."
        Else
            lblAviso.Text = ""
            pnlNuevo.Visible = True
            pnlConsulta.Visible = False
            txtPassword.Focus()
            VerDatos(gridUsuario.Rows(e.NewEditIndex).Cells(1).Text)
        End If
    End Sub

    Protected Sub cmbFiltroCliente_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFiltroCliente.SelectedIndexChanged
        CargarUsuarios()
    End Sub

    Protected Sub cmbFiltroDepartamento_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFiltroDepartamento.SelectedIndexChanged
        CargarUsuarios()
    End Sub

    Protected Sub cmbFiltroTipoUsuario_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFiltroTipoUsuario.SelectedIndexChanged
        CargarUsuarios()
    End Sub
End Class