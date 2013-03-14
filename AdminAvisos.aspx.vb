Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class AdminAvisos
    Inherits System.Web.UI.Page

    Dim IDAviso As Integer
    Dim Nombre, Region, Leido As String
    Dim SQLCLiente, SQLPlataforma, SQLTipoUsuario, SQLUsuario As String
    Dim ClienteSel, PlataformaSel, TipoUsuarioSel As String

    Private Sub VerDatos()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionAdmin.localSqlServer, _
                                               "SELECT * FROM Avisos WHERE id_aviso= '" & IDAviso & "'")

        If Tabla.Rows.Count > 0 Then
            txtTitulo.Text = Tabla.Rows(0)("titulo_aviso")
            cmbUsuario.SelectedValue = Tabla.Rows(0)("id_usuario")
            txtDescripcion.Text = Tabla.Rows(0)("descripcion")
        End If

        Tabla.Dispose()
    End Sub

    Sub SQLCombo()
        ClienteSel = Acciones.Slc.cmb("CLI.id_cliente", cmbCliente.SelectedValue)
        PlataformaSel = Acciones.Slc.cmb("ACC.id_proyecto", cmbPlataforma.SelectedValue)
        TipoUsuarioSel = Acciones.Slc.cmb("US.id_tipo", cmbTipoUsuario.SelectedValue)

        SQLCLiente = "select DISTINCT PRO.id_cliente, CLI.nombre_cliente " & _
                    "FROM Acceso_Proyectos as ACC " & _
                    "INNER JOIN Proyectos as PRO ON PRO.id_proyecto=ACC.id_proyecto " & _
                    "INNER JOIN Clientes as CLI ON CLI.id_cliente =PRO.id_cliente " & _
                    "WHERE id_usuario='" & HttpContext.Current.User.Identity.Name & "' " & _
                    "ORDER BY CLI.nombre_cliente"

        SQLPlataforma = "select ACC.id_proyecto, PRO.nombre_proyecto " & _
                    "FROM Acceso_Proyectos as ACC " & _
                    "INNER JOIN Proyectos as PRO ON PRO.id_proyecto=ACC.id_proyecto " & _
                    "INNER JOIN Clientes as CLI ON CLI.id_cliente =PRO.id_cliente " & _
                    "WHERE id_usuario='" & HttpContext.Current.User.Identity.Name & "' " & _
                     " " + ClienteSel + PlataformaSel + " " & _
                    "ORDER BY PRO.nombre_proyecto"

        SQLTipoUsuario = "SELECT TUS.id_tipo, TUS.tipo_usuario " & _
                    "FROM Usuarios_Mensajes as MEN " & _
                    "INNER JOIN Tipo_Usuarios as TUS ON MEN.id_tipo=TUS.id_tipo " & _
                    "ORDER BY TUS.tipo_usuario"

        SQLUsuario = "SELECT DISTINCT PRO.id_usuario,US.id_tipo " & _
                    "FROM Acceso_Proyectos as PRO " & _
                    "INNER JOIN (select PRO.id_proyecto, PRO.nombre_proyecto " & _
                    "FROM Acceso_Proyectos as ACC " & _
                    "INNER JOIN Proyectos as PRO ON PRO.id_proyecto=ACC.id_proyecto " & _
                    "WHERE id_usuario='" & HttpContext.Current.User.Identity.Name & "')ACC ON PRO.id_proyecto=ACC.id_proyecto " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario=PRO.id_usuario " & _
                    "INNER JOIN Clientes as CLI ON CLI.id_cliente =US.id_cliente " & _
                    "INNER JOIN Usuarios_Mensajes as MEN ON MEN.id_tipo=US.id_tipo " & _
                    "WHERE PRO.id_usuario <>'' " & _
                    " " + PlataformaSel + TipoUsuarioSel + " " & _
                    "ORDER BY PRO.id_usuario"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CargaGrid()

            SQLCombo()
            Combo.LlenaDrop(ConexionAdmin.localSqlServer, SQLCLiente,"nombre_cliente", "id_cliente", cmbCliente)
            Combo.LlenaDrop(ConexionAdmin.localSqlServer, SQLPlataforma, "nombre_proyecto", "id_proyecto", cmbPlataforma)
            Combo.LlenaDrop(ConexionAdmin.localSqlServer, SQLTipoUsuario, "tipo_usuario", "id_tipo", cmbTipoUsuario)
            Combo.LlenaDrop(ConexionAdmin.localSqlServer, SQLUsuario, "id_usuario", "id_usuario", cmbUsuario)
        End If
    End Sub

    Sub CargaGrid()
        CargaGrilla(ConexionAdmin.localSqlServer, _
                    "select id_aviso, titulo_aviso, fecha, " & _
                    "CASE leido when 1 then 'LEIDO' when 0 then 'sin leer' end as leido," & _
                    "CASE estatus when 1 then 'Enterado' when 2 then 'Inconforme' end as estatus " & _
                    "from Avisos ORDER BY fecha DESC", gridAvisos)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionAdmin.localSqlServer, _
                                               "SELECT id_aviso From Avisos WHERE id_aviso='" & lblIDAviso.Text & "'")
        If Tabla.Rows.Count = 1 Then
            BD.Execute(ConexionAdmin.localSqlServer, "UPDATE Avisos " & _
                       "SET titulo_aviso='" & txtTitulo.Text & "', " & _
                        "id_usuario='" & cmbUsuario.SelectedValue & "', " & _
                        "descripcion='" & txtDescripcion.Text & "' FROM Avisos " & _
                        "WHERE id_aviso=" & lblIDAviso.Text & "")
        Else
            IDAviso = BD.RT.Execute(ConexionAdmin.localSqlServer, "INSERT INTO Avisos" & _
                    "(titulo_aviso, id_usuario, descripcion) " & _
                    "VALUES('" & txtTitulo.Text & "','" & cmbUsuario.SelectedValue & "', " & _
                    "'" & txtDescripcion.Text & "') SELECT @@IDENTITY AS 'id_aviso'")
        End If
        Tabla.Dispose()

        pnlAvisos.Visible = False
        gridAvisos.Visible = True
        CargaGrid()
        Borrar()
    End Sub

    Private Sub gridAvisos_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridAvisos.RowDeleting
        IDAviso = gridAvisos.Rows(e.RowIndex).Cells(2).Text
        Leido = gridAvisos.Rows(e.RowIndex).Cells(5).Text

        If Leido <> "LEIDO" Then
            BD.Execute(ConexionAdmin.localSqlServer, _
                       "DELETE FROM Avisos WHERE id_aviso = '" & IDAviso & "'")
        Else
            MsgBox("No puedes eliminar el mensaje, ya ha sido leído por el usuario.")
        End If

        CargaGrid()
    End Sub

    Private Sub gridUsuarios_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gridAvisos.RowEditing
        IDAviso = gridAvisos.Rows(e.NewEditIndex).Cells(2).Text
        lblIDAviso.Text = IDAviso
        pnlAvisos.Visible = True
        gridAvisos.Visible = False

        VerDatos()
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        pnlAvisos.Visible = False
        gridAvisos.Visible = True

        Borrar()
    End Sub

    Protected Sub linkNuevo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles linkNuevo.Click
        pnlAvisos.Visible = True
        gridAvisos.Visible = False

        Borrar()
    End Sub

    Sub Borrar()
        txtTitulo.Text = ""
        txtDescripcion.Text = ""
        cmbCliente.SelectedValue = ""
        cmbUsuario.SelectedValue = ""
    End Sub

    Protected Sub LinkConsultas_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LinkConsultas.Click
        pnlAvisos.Visible = False
        gridAvisos.Visible = True

        Borrar()
    End Sub

    Private Sub CrearAvisoCapacitacion_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridAvisos.Columns(2).Visible = False
        gridConsultas.Columns(1).Visible = False
    End Sub

    Private Sub cmbCliente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCliente.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionAdmin.localSqlServer, SQLPlataforma, "nombre_proyecto", "id_proyecto", cmbPlataforma)
        Combo.LlenaDrop(ConexionAdmin.localSqlServer, SQLUsuario, "id_usuario", "id_usuario", cmbUsuario)
    End Sub

    Public Function CargaAvisos(ByVal Filtro As String) As Integer
        pnlAvisos.Visible = False
        pnlConsultas.Visible = True
        panelGrid.Visible = False

        CargaGrilla(ConexionAdmin.localSqlServer, _
                    "select id_aviso, titulo_aviso, id_usuario, fecha,fecha_leido, respuesta, descripcion, " & _
                    "CASE leido when 1 then 'LEIDO' when 0 then 'sin leer' end as leido," & _
                    "CASE estatus when 1 then 'Enterado' when 2 then 'Inconforme' end as estatus " & _
                    "from Avisos " & _
                    "WHERE id_aviso <>0 " & _
                    " " + Filtro + " " & _
                    "ORDER BY fecha DESC", gridConsultas)
    End Function

    Protected Sub lnkAvisos1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkAvisos1.Click
        CargaAvisos("AND leido =0")
    End Sub

    Protected Sub lnkAvisos2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkAvisos2.Click
        CargaAvisos("AND leido =1")
    End Sub

    Protected Sub lnkAvisos3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkAvisos3.Click
        CargaAvisos("AND estatus =1")
    End Sub

    Protected Sub lnkAvisos4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkAvisos4.Click
        CargaAvisos("AND estatus =2")
    End Sub

    Protected Sub lnkAvisos5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkAvisos5.Click
        CargaAvisos("AND respuesta<>''")
    End Sub

    Private Sub gridConsultas_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gridConsultas.RowEditing
        IDAviso = gridConsultas.Rows(e.NewEditIndex).Cells(1).Text
        pnlAviso.Visible = True

        CargaAviso()
    End Sub

    Sub CargaAviso()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionAdmin.localSqlServer, _
                                               "SELECT * from Avisos WHERE id_aviso=" & IDAviso & "")
        If Tabla.Rows.Count > 0 Then
            lblTitulo.Text = Tabla.Rows(0)("titulo_aviso")
            lblFecha.Text = Tabla.Rows(0)("fecha")
            lblDescripcion.Text = Tabla.Rows(0)("descripcion")
            lblRespuesta.Text = Tabla.Rows(0)("respuesta")
            lblEstatus.Text = Tabla.Rows(0)("estatus")
            lblLeido.Text = Tabla.Rows(0)("leido")
            lblFechaLeido.Text = Tabla.Rows(0)("fecha_leido")
            lblCerrado.Text = Tabla.Rows(0)("cerrado_usuario")
            cmbEstatus.SelectedValue = Tabla.Rows(0)("cerrado_admin")
        End If

        Tabla.Dispose()
    End Sub

    Private Sub cmbPlataforma_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPlataforma.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionAdmin.localSqlServer, SQLUsuario, "id_usuario", "id_usuario", cmbUsuario)
    End Sub

    Private Sub cmbTipoUsuario_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoUsuario.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionAdmin.localSqlServer, SQLUsuario, "id_usuario", "id_usuario", cmbUsuario)
    End Sub
End Class