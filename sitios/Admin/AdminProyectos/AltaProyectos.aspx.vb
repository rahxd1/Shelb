Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports procomlcd

Partial Public Class AltaProyectos
    Inherits System.Web.UI.Page

    Private Sub VerDatos(ByVal ID_Proyecto As String)
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionAdmin.localSqlServer, _
                                                   "SELECT * FROM Proyectos " & _
                                                   "WHERE id_proyecto= " & ID_Proyecto & "")
        If Tabla.Rows.Count > 0 Then
            txtIDProyecto.Text = Tabla.Rows(0)("id_proyecto")
            txtNombreProyecto.Text = Tabla.Rows(0)("nombre_proyecto")
            cmbCliente.SelectedValue = Tabla.Rows(0)("id_cliente")
        End If

        Tabla.Dispose()
    End Sub

    Sub CargarProyecto()
        CargaGrilla(ConexionAdmin.localSqlServer, _
                    "SELECT GP.id_proyecto, C.nombre_cliente, GP.nombre_proyecto " & _
                    "FROM Proyectos as GP " & _
                    "INNER JOIN Clientes as C ON GP.id_cliente = C.id_cliente " & _
                    "ORDER BY GP.id_proyecto", gridProyectos)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Guardar(txtIDProyecto.Text, txtNombreProyecto.Text, cmbCliente.SelectedValue)
        Borrar()
        lnkNuevo.Enabled = True
    End Sub

    Public Function Guardar(ByVal IDProyecto As Integer, ByVal Proyecto As String, ByVal Cliente As String) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionAdmin.localSqlServer, _
                                                   "SELECT nombre_proyecto From Proyectos " & _
                                                   "WHERE nombre_proyecto='" & Proyecto & "'")

        If Tabla.Rows.Count = 1 Then
            BD.Execute(ConexionAdmin.localSqlServer, _
                       "execute Proyectos_Editar " & IDProyecto & ",'" & Proyecto & "'," & _
                       "" & Cliente & "")

            Me.lblAviso.Text = "Los cambios se realizaron correctamente."
        Else
            BD.Execute(ConexionAdmin.localSqlServer, _
                       "execute Proyectos_Crear " & IDProyecto & ",'" & Proyecto & "'," & _
                       "" & Cliente & "")

            Me.lblAviso.Text = "Se guardo exitosamente la información."
        End If

        pnlNuevo.Visible = False
        Tabla.Dispose()

        CargarProyecto()
    End Function

    Sub Borrar()
        txtIDProyecto.Text = ""
        txtNombreProyecto.Text = ""

        pnlNuevo.Visible = False
        pnlConsulta.Visible = False
        lnkNuevo.Enabled = True
        lnkConsultas.Enabled = True
    End Sub

    Protected Sub lnkNuevo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkNuevo.Click
        pnlNuevo.Visible = True
        pnlConsulta.Visible = False
        lnkNuevo.Enabled = False
        txtIDProyecto.Focus()
        lblAviso.Text = ""
        cmbCliente.SelectedValue = ""
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Borrar()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CargarProyecto()

            Combo.LlenaDrop(ConexionAdmin.localSqlServer, "SELECT * FROM Clientes", "nombre_cliente", "id_cliente", cmbCliente)
        End If
    End Sub

    Protected Sub lnkConsultas_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkConsultas.Click
        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
        lnkNuevo.Enabled = True
        lblAviso.Text = ""
    End Sub

    Private Sub gridProyectos_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gridProyectos.RowEditing
        If gridProyectos.Rows(e.NewEditIndex).Cells(1).Text = "" Then
            e.Cancel = True
            lblAviso.Text = "Error al intentar editar el proyecto."
        Else
            lblAviso.Text = ""
            pnlNuevo.Visible = True
            pnlConsulta.Visible = False
            lnkNuevo.Enabled = False
            txtIDProyecto.Focus()
            VerDatos(gridProyectos.Rows(e.NewEditIndex).Cells(1).Text)
        End If
    End Sub
End Class