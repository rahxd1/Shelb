Imports System.Data.SqlClient

Partial Public Class AdminClientes
    Inherits System.Web.UI.Page

    Private Sub VerDatos(ByVal ID_Cliente As String)
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionAdmin.localSqlServer, _
                                                   "SELECT * FROM Clientes " & _
                                                   "WHERE id_cliente=" & ID_Cliente & "")
        If Tabla.Rows.Count > 0 Then
            IDCliente.Text = Tabla.Rows(0)("id_cliente")
            txtNombreCliente.Text = Tabla.Rows(0)("nombre_cliente")
            txtGiro.Text = Tabla.Rows(0)("giro")
            txtAdmin.Text = Tabla.Rows(0)("admin")
            cmbActivo.SelectedValue = Tabla.Rows(0)("activo")
        End If

        Tabla.Dispose()
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Dim id_cliente As Integer
        If IDCliente.Text = "" Then
            id_cliente = 0 : Else
            id_cliente = IDCliente.Text : End If

        ''//Guarda
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionAdmin.localSqlServer, _
                                               "SELECT nombre_cliente From Clientes " & _
                                               "WHERE id_cliente=" & id_cliente & "")

        If Tabla.Rows.Count = 1 Then
            BD.Execute(ConexionAdmin.localSqlServer, _
                       "UPDATE Clientes SET nombre_cliente ='" & txtNombreCliente.Text & "'," & _
                       "giro='" & txtGiro.Text & "',admin='" & txtAdmin.Text & "'," & _
                       "activo=" & cmbActivo.SelectedValue & " FROM Clientes " & _
                       "WHERE id_cliente=" & id_cliente & "")

            Me.lblAviso.Text = "Los cambios se realizaron correctamente."
        Else
            BD.Execute(ConexionAdmin.localSqlServer, _
                       "INSERT into Clientes(nombre_cliente, giro, admin, activo)" & _
                       "VALUES('" & txtNombreCliente.Text & "', '" & txtGiro.Text & "'," & _
                       "'" & txtAdmin.Text & "', " & cmbActivo.SelectedValue & ")")

            Me.lblAviso.Text = "Se guardo exitosamente la información."
        End If

        Tabla.Dispose()

        pnlNuevo.Visible = False
        pnlConsulta.Visible = False
        lnkNuevo.Enabled = True
    End Sub

    Protected Sub lnkNuevo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkNuevo.Click
        pnlNuevo.Visible = True
        pnlConsulta.Visible = False
        txtNombreCliente.Focus()

        lblAviso.Text = ""
        txtNombreCliente.Text = ""
        txtGiro.Text = ""
        txtAdmin.Text = ""
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        pnlNuevo.Visible = False
        pnlConsulta.Visible = False

        lblAviso.Text = ""
        txtNombreCliente.Text = ""
        txtGiro.Text = ""
        txtAdmin.Text = ""
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CargaGrilla(ConexionAdmin.localSqlServer, _
                        "SELECT * FROM Clientes ORDER BY nombre_cliente", gridClientes)
        End If
    End Sub

    Protected Sub lnkConsultas_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkConsultas.Click
        pnlNuevo.Visible = False
        pnlConsulta.Visible = True

        lblAviso.Text = ""
    End Sub

    Private Sub gridClientes_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gridClientes.RowEditing
        If gridClientes.Rows(e.NewEditIndex).Cells(1).Text = "" Then
            e.Cancel = True
            lblAviso.Text = "Error al intentar editar el cliente."
        Else
            lblAviso.Text = ""
            pnlNuevo.Visible = True
            pnlConsulta.Visible = False
            txtNombreCliente.Focus()
            VerDatos(gridClientes.Rows(e.NewEditIndex).Cells(1).Text)
        End If
    End Sub

    Private Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridClientes.Columns(1).Visible = False
    End Sub
End Class