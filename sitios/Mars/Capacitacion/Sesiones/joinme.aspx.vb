Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Partial Public Class joinme
    Inherits System.Web.UI.Page
    Dim tipo_usuario As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Permiso()
        sesiones()

    End Sub

    Private Sub sesiones()
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim SQL As String
            If tipo_usuario <> 1 Then
                SQL = "select * from Capacitacion_Control where status = 1"
            Else
                SQL = "SELECT CO.fecha as fecha, CO.id_supervisor as id_supervisor, CO.id_joinme as id_joinme, CO.status " & _
                       "FROM Capacitacion_Control as CO " & _
                       "INNER JOIN Usuarios_Relacion_tmp_capacitacion as US ON US.id_supervisor = CO.id_supervisor and CO.status = 1 " & _
                       "WHERE US.id_usuario = '" & HttpContext.Current.User.Identity.Name & "'"

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
                If tipo_usuario <> 1 Then
                    lnkAdmin.Visible = True
                End If

            End If
        End Using
    End Sub

End Class