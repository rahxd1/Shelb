Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls

Partial Public Class admin_joinme
    Inherits System.Web.UI.Page
    Dim tipo_usuario As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Permiso()
        sesiones()
    End Sub
    Private Sub Permiso()

        Using cnn As New SqlConnection(ConexionAdmin.localSqlServer)
            Dim SQL As New SqlCommand("SELECT * FROM Usuarios " & _
                           "WHERE id_usuario = '" & HttpContext.Current.User.Identity.Name & "'", cnn)
            cnn.Open()
            Dim Data As New SqlDataAdapter(SQL)
            Dim Tabla As New DataTable
            Data.Fill(Tabla)
            cnn.Close()
            cnn.Dispose()
            If Tabla.Rows.Count = 1 Then
                tipo_usuario = Tabla.Rows(0)("id_tipo")

            End If
        End Using
    End Sub
    Private Sub sesiones()
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim SQL As String
            If tipo_usuario <> 100 Then
                SQL = "select * from Capacitacion_Control WHERE id_supervisor ='" & HttpContext.Current.User.Identity.Name & "' AND status = 1 "
            Else
                SQL = "select * from Capacitacion_Control WHERE status = 1 "
            End If

            Dim cmd As New SqlCommand(SQL, cnn)
            Dim adapter As New SqlDataAdapter
            adapter.SelectCommand = cmd
            cnn.Open()
            Dim dataset As New DataSet
            adapter.Fill(dataset, "Formato")
            cnn.Close()
            gridSesiones.DataSource = dataset
            gridSesiones.DataBind()
            cnn.Dispose()
        End Using
    End Sub

    Private Sub gridSesiones_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridSesiones.RowDeleting
        Dim Sesion As Integer
        Sesion = gridSesiones.Rows(e.RowIndex).Cells(1).Text

        Dim cnn As New SqlConnection(ConexionMars.localSqlServer)
        cnn.Open()
        Dim SQLEliminar As New SqlCommand("UPDATE Capacitacion_Control SET status = 0 WHERE id_sesion = '" & Sesion & "'", cnn)
        SQLEliminar.ExecuteNonQuery()
        SQLEliminar.Dispose()
        cnn.Close()
        cnn.Dispose()
        sesiones()
        Label1.Text = Sesion
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim SQL As New SqlCommand("SELECT * FROM Capacitacion_Control " & _
                           "WHERE id_supervisor = '" & HttpContext.Current.User.Identity.Name & "' AND status = 1 ", cnn)
            cnn.Open()
            Dim Data As New SqlDataAdapter(SQL)
            Dim Tabla As New DataTable
            Data.Fill(Tabla)

            If Tabla.Rows.Count = 1 Then
                Label1.Text = "NO SE PUEDE CREAR LA SESION PORQUE TIENES UNA ACTIVA"
                sesiones()
            Else
                Dim estatus As Integer = 1
                Dim SQLNuevo As New SqlCommand("INSERT INTO Capacitacion_Control" & _
                                   "(id_supervisor, id_joinme,fecha,status) " & _
                                   "VALUES(@id_usuario, @id_joinme,@fecha,@status)", cnn)
                SQLNuevo.Parameters.AddWithValue("@id_usuario", HttpContext.Current.User.Identity.Name)
                SQLNuevo.Parameters.AddWithValue("@id_joinme", txtID.Text)
                SQLNuevo.Parameters.AddWithValue("@fecha", Date.Today)
                SQLNuevo.Parameters.AddWithValue("@status", estatus)
                SQLNuevo.ExecuteNonQuery()
                sesiones()
            End If
            cnn.Close()
            cnn.Dispose()
        End Using

    End Sub
End Class