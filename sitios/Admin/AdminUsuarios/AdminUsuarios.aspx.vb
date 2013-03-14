Imports System.Data
Imports System.Data.SqlClient
Imports procomlcd
Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports ISODates.Dates
Imports System.Security.Cryptography

Partial Public Class AdminUsuarios
    Inherits System.Web.UI.Page

    Dim Conexion As String

    Private Sub VerDatos(ByVal ID_Usuario As String)
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionAdmin.localSqlServer, _
                                               "select * from Usuarios WHERE id_usuario= '" & ID_Usuario & "'")

        With Tabla
            If .Rows.Count > 0 Then
                lblAviso.Text = ""

                txtIDUsuario.Text = .Rows(0)("id_usuario")
                txtPassword.Enabled = False
                txtNombre.Text = .Rows(0)("nombre")
                cmbTipoUsuario.SelectedValue = .Rows(0)("id_tipo")
                cmbDepartamento.SelectedValue = .Rows(0)("id_departamento")
                txtCorreo.Text = .Rows(0)("correo")
                cmbCliente.SelectedValue = .Rows(0)("id_cliente")
            End If
        End With

        Tabla.Dispose()
    End Sub

    Sub CargarUsuarios()
        VerAccesos(cmbFiltroUsuario.Text)

        Dim SQLCliente, SQLTipoUsuario, SQLDepartamento, SQLUsuario As String
        SQLCliente = Acciones.Slc.cmb("USU.id_cliente", cmbFiltroCliente.SelectedValue)
        SQLTipoUsuario = Acciones.Slc.cmb("USU.id_tipo", cmbFiltroTipoUsuario.SelectedValue)
        SQLDepartamento = Acciones.Slc.cmb("USU.id_departamento", cmbFiltroDepartamento.SelectedValue)
        SQLUsuario = Acciones.Slc.cmb("USU.id_usuario", cmbFiltroUsuario.SelectedValue)

        CargaGrilla(ConexionAdmin.localSqlServer, _
                    "SELECT USU.id_usuario, USU.nombre, TUSU.tipo_usuario " & _
                    "FROM Usuarios as USU " & _
                    "INNER JOIN Tipo_Usuarios TUSU ON USU.id_tipo = TUSU.id_tipo " & _
                    "WHERE USU.id_usuario<>'' " & _
                    " " + SQLCliente + SQLTipoUsuario + " " & _
                    " " + SQLDepartamento + SQLUsuario + " " & _
                    "ORDER BY USU.id_usuario", gridUsuario)
    End Sub

    Protected Sub lnkNuevo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkNuevo.Click
        Borrar()

        txtIDUsuario.Focus()
        pnlNuevo.Visible = True
        lblAviso.Text = ""
        txtIDUsuario.Text = ""
        txtPassword.Text = ""

        txtPassword.Enabled = True
        pnlConsulta.Visible = False
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Borrar()
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Guardar(txtIDUsuario.Text, txtPassword.Text, txtNombre.Text, _
                     cmbTipoUsuario.SelectedValue, cmbDepartamento.SelectedValue, _
                     txtCorreo.Text, cmbCliente.SelectedValue)
        Borrar()
        CargarUsuarios()
    End Sub

    Private Function GuardaAccesos(ByVal id_usuario As String) As Boolean
        Dim Nuevo As Integer, SeleccionProyecto As String
        For i = 0 To CInt(Me.gridAccesos.Rows.Count) - 1
            SeleccionProyecto = Me.gridAccesos.DataKeys(i).Value.ToString()

            If SeleccionProyecto <> "" Then
                Dim chkAcceso As CheckBox = CType(gridAccesos.Rows(i).FindControl("chkAcceso"), CheckBox)
                If chkAcceso.Checked = True Then
                    Nuevo = 1 : Else
                    Nuevo = 0 : End If

                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionAdmin.localSqlServer, _
                                                       "SELECT id_usuario,id_proyecto From Acceso_Proyectos " & _
                                                       "WHERE id_usuario='" & id_usuario & "' " & _
                                                       "AND id_proyecto=" & SeleccionProyecto & "")

                If Tabla.Rows.Count = 0 Then
                    If Nuevo = 1 Then
                        BD.Execute(ConexionAdmin.localSqlServer, _
                                   "INSERT INTO Acceso_Proyectos (id_usuario, id_proyecto) " & _
                                   "VALUES('" & id_usuario & "'," & SeleccionProyecto & ")")

                        Me.lblAviso.Text = "SE GUARDO CORRECTAMENTE"
                    End If
                Else
                    If Nuevo = 0 Then
                        BD.Execute(ConexionAdmin.localSqlServer, _
                                   "DELETE Acceso_Proyectos " & _
                                   "WHERE id_usuario='" & id_usuario & "' AND id_proyecto=" & SeleccionProyecto & "")
                    End If
                End If

                Tabla.Dispose()
            End If
        Next
    End Function

    Public Function Guardar(ByVal id_usuario As String, ByVal password As String, ByVal nombre As String, _
                            ByVal id_tipo As Integer, ByVal id_departamento As Integer, _
                            ByVal correo As String, ByVal id_cliente As Integer) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionAdmin.localSqlServer, _
                                                   "SELECT id_usuario From Usuarios " & _
                                                   "WHERE id_usuario='" & id_usuario & "'")

        If Tabla.Rows.Count = 1 Then
            BD.Execute(ConexionAdmin.localSqlServer, _
                       "UPDATE Usuarios " & _
                       "SET nombre='" & nombre & "', id_tipo=" & id_tipo & ", " & _
                       "id_departamento=" & id_departamento & ", correo='" & correo & "', id_cliente=" & id_cliente & " " & _
                       "WHERE id_usuario='" & id_usuario & "'")

            Me.lblAviso.Text = "Los cambios se realizaron correctamente."
        Else
            Dim clave As String
            clave = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1")

            BD.Execute(ConexionAdmin.localSqlServer, _
                       "INSERT INTO Usuarios" & _
                       "(id_usuario,nombre,id_tipo,id_departamento,correo,id_cliente) " & _
                       "VALUES('" & id_usuario & "','" & nombre & "'," & id_tipo & "," & _
                       "" & id_departamento & ",'" & correo & "'," & id_cliente & ")")

            BD.Execute(ConexionAdmin.localSqlServer, _
                       "INSERT INTO Acceso(id_usuario, password) " & _
                       "VALUES('" & id_usuario & "','" & clave & "')")

            Me.lblAviso.Text = "Se guardo exitosamente la información."
        End If

        pnlNuevo.Visible = False
        Tabla.Dispose()

        GuardaAccesos(id_usuario)
    End Function

    Sub Borrar()
        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
        txtIDUsuario.Text = ""
        txtNombre.Text = ""
        txtPassword.Text = ""
        txtCorreo.Text = ""
        cmbTipoUsuario.SelectedValue = ""
        cmbDepartamento.SelectedValue = ""
        cmbCliente.SelectedValue = ""
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

    Private Sub VerAccesos(ByVal ID_Usuario As String)
        CargaGrilla(ConexionAdmin.localSqlServer, _
                    "SELECT DISTINCT PR.id_proyecto,C.nombre_cliente,C.id_cliente," & _
                    "PR.nombre_proyecto,ISNULL(ACC.Acceso,0)Acceso " & _
                    "FROM Proyectos as PR " & _
                    "INNER JOIN Clientes as C ON PR.id_cliente = C.id_cliente " & _
                    "FULL JOIN (SELECT 1 as Acceso, id_proyecto " & _
                    "FROM Acceso_Proyectos WHERE id_usuario='" & ID_Usuario & "')as ACC " & _
                    "ON ACC.id_proyecto = PR.id_proyecto " & _
                    "ORDER BY PR.nombre_proyecto", Me.gridAccesos)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CargarUsuarios()

            Combo.LlenaDrop(ConexionAdmin.localSqlServer, _
                            "SELECT * FROM Tipo_Usuarios order BY tipo_usuario", "tipo_usuario", "id_tipo", cmbTipoUsuario)
            Combo.LlenaDrop(ConexionAdmin.localSqlServer, _
                            "SELECT * FROM Departamentos ORDER BY nombre_departamento", "nombre_departamento", "id_departamento", cmbDepartamento)
            Combo.LlenaDrop(ConexionAdmin.localSqlServer, _
                            "SELECT * FROM Clientes ORDER BY nombre_cliente", "nombre_cliente", "id_cliente", cmbCliente)
            Combo.LlenaDrop(ConexionAdmin.localSqlServer, _
                            "SELECT * FROM Tipo_Usuarios ORDER BY tipo_usuario", "tipo_usuario", "id_tipo", cmbFiltroTipoUsuario)
            Combo.LlenaDrop(ConexionAdmin.localSqlServer, _
                            "SELECT * FROM Usuarios ORDER BY id_usuario", "id_usuario", "id_usuario", cmbFiltroUsuario)
            Combo.LlenaDrop(ConexionAdmin.localSqlServer, _
                            "SELECT * FROM Departamentos ORDER BY nombre_departamento", "nombre_departamento", "id_departamento", cmbFiltroDepartamento)
            Combo.LlenaDrop(ConexionAdmin.localSqlServer, _
                            "SELECT * FROM Clientes ORDER BY nombre_cliente", "nombre_cliente", "id_cliente", cmbFiltroCliente)
        End If
    End Sub

    Protected Sub lnkConsultas2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkConsultas.Click
        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
        lblAviso.Text = ""
        CargarUsuarios()
        Borrar()
    End Sub

    Private Sub gridUsuario_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridUsuario.RowDeleting
        If gridUsuario.Rows(e.RowIndex).Cells(2).Text = "" Then
            e.Cancel = True
            lblAviso.Text = "Error al intentar editar el usuario."
        Else
            lblAviso.Text = ""

            BD.Execute(ConexionAdmin.localSqlServer, _
                       "DELETE FROM Usuarios WHERE id_usuario = '" & gridUsuario.Rows(e.RowIndex).Cells(2).Text & "'")

            BD.Execute(ConexionAdmin.localSqlServer, _
                       "DELETE FROM Acceso WHERE id_usuario = '" & gridUsuario.Rows(e.RowIndex).Cells(2).Text & "'")

            BD.Execute(ConexionAdmin.localSqlServer, _
                       "DELETE FROM Acceso_Proyectos WHERE id_usuario = '" & gridUsuario.Rows(e.RowIndex).Cells(2).Text & "'")

            CargarUsuarios()
        End If
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
            VerAccesos(gridUsuario.Rows(e.NewEditIndex).Cells(2).Text)
        End If
    End Sub

    Protected Sub cmbFiltroCliente_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFiltroCliente.SelectedIndexChanged
        Combo.LlenaDrop(ConexionAdmin.localSqlServer, "SELECT * FROM Usuarios WHERE id_cliente='" & cmbFiltroCliente.SelectedValue & "'", "id_usuario", "id_usuario", cmbFiltroUsuario)

        If cmbFiltroCliente.Text = "" Then
            Combo.LlenaDrop(ConexionAdmin.localSqlServer, "SELECT * FROM Usuarios", "id_usuario", "id_usuario", cmbFiltroUsuario)
        End If

        lblAviso.Text = ""

        CargarUsuarios()
        gridUsuario.Visible = True
    End Sub

    Protected Sub cmbFiltroTipoUsuario_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFiltroTipoUsuario.SelectedIndexChanged
        Combo.LlenaDrop(ConexionAdmin.localSqlServer, "SELECT * FROM Usuarios WHERE id_tipo='" & cmbFiltroTipoUsuario.SelectedValue & "'", "id_usuario", "id_usuario", cmbFiltroUsuario)

        If Not cmbFiltroCliente.Text = "" Then
            Combo.LlenaDrop(ConexionAdmin.localSqlServer, "SELECT * FROM Usuarios WHERE id_tipo='" & cmbFiltroTipoUsuario.SelectedValue & "' AND id_cliente='" & cmbFiltroCliente.SelectedValue & "'", "id_usuario", "id_usuario", cmbFiltroUsuario)
        End If

        If Not cmbFiltroCliente.Text = "" And Not cmbFiltroDepartamento.Text = "" Then
            Combo.LlenaDrop(ConexionAdmin.localSqlServer, "SELECT * FROM Usuarios WHERE id_tipo='" & cmbFiltroTipoUsuario.SelectedValue & "' AND id_cliente='" & cmbFiltroCliente.SelectedValue & "' AND id_departamento='" & cmbFiltroDepartamento.SelectedValue & "'", "id_usuario", "id_usuario", cmbFiltroUsuario)
        End If

        lblAviso.Text = ""

        CargarUsuarios()
        gridUsuario.Visible = True
    End Sub

    Protected Sub cmbFiltroDepartamento_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFiltroDepartamento.SelectedIndexChanged
        Combo.LlenaDrop(ConexionAdmin.localSqlServer, "SELECT * FROM Usuarios WHERE id_departamento='" & cmbFiltroDepartamento.SelectedValue & "'", "id_usuario", "id_usuario", cmbFiltroUsuario)

        If Not cmbFiltroCliente.Text = "" Then
            Combo.LlenaDrop(ConexionAdmin.localSqlServer, "SELECT * FROM Usuarios WHERE id_departamento='" & cmbFiltroDepartamento.SelectedValue & "' AND id_cliente='" & cmbFiltroCliente.SelectedValue & "'", "id_usuario", "id_usuario", cmbFiltroUsuario)
        End If

        lblAviso.Text = ""

        CargarUsuarios()
        gridUsuario.Visible = True
    End Sub

    Protected Sub cmbFiltroUsuario_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFiltroUsuario.SelectedIndexChanged
        If cmbFiltroUsuario.SelectedValue = "" Then
            'e.Cancel = True
            lblAviso.Text = "Error al intentar editar el usuario."
        Else
            lblAviso.Text = ""
            pnlNuevo.Visible = True
            pnlConsulta.Visible = False
            txtIDUsuario.Focus()
            VerDatos(cmbFiltroUsuario.SelectedValue)
        End If

        lblAviso.Text = ""

        CargarUsuarios()
        gridUsuario.Visible = True
    End Sub

    Protected Sub gridAccesos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridAccesos.RowDataBound
        Dim IDCliente As Integer

        If e.Row.RowType = DataControlRowType.DataRow Then
            If Not e.Row.Cells(3).Text = "&nbsp;" Then
                IDCliente = e.Row.Cells(3).Text : End If

            If IDCliente = 1 Then
                e.Row.Cells(4).Text = "" : End If

            If IDCliente = 2 Then
                Conexion = ConexionMars.localSqlServer : End If

            If IDCliente = 8 Then
                Conexion = ConexionSYM.localSqlServer : End If

            If IDCliente = 2 Or IDCliente = 8 Then
                Dim cmbRegion As DropDownList = e.Row.FindControl("cmbRegion")

                Dim Tabla As DataTable = Tablas.Ver.DT(Conexion, _
                                                       "SELECT * from Usuarios " & _
                                                       "where id_usuario='" & txtIDUsuario.Text & "'")
                If Tabla.Rows.Count <> 0 Then
                    cmbRegion.SelectedValue = Tabla.Rows(0)("id_region") : Else
                    cmbRegion.SelectedValue = 0 : End If
            End If
        End If
    End Sub

    Private Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridUsuario.Columns(3).Visible = False
    End Sub
End Class