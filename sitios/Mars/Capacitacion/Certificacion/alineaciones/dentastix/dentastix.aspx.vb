Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Partial Public Class dentastix
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ChecarCertificado()
    End Sub

    Private Sub ChecarCertificado()
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim SQL As New SqlCommand("SELECT * FROM Capacitacion_Certificaciones_Dentastix " & _
                           "WHERE id_usuario = '" & HttpContext.Current.User.Identity.Name & "'", cnn)
            cnn.Open()
            Dim Data As New SqlDataAdapter(SQL)
            Dim Tabla As New DataTable
            Data.Fill(Tabla)
            cnn.Close()
            cnn.Dispose()
            If Tabla.Rows.Count = 1 Then
                lblAviso.Visible = True
                lblAviso.Text = "Tu ya realizaste esta certificacion, si no es asi, por favor comunicate con sistemas."
            Else
                eliminarTemporal()


            End If
        End Using
    End Sub
    Private Sub eliminarTemporal()
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim SQL As New SqlCommand("SELECT * FROM Capacitacion_Certificaciones_Dentastix_tmp " & _
                           "WHERE id_usuario = '" & HttpContext.Current.User.Identity.Name & "'", cnn)
            cnn.Open()
            Dim Data As New SqlDataAdapter(SQL)
            Dim Tabla As New DataTable
            Data.Fill(Tabla)
            cnn.Close()
            cnn.Dispose()
            If Tabla.Rows.Count >= 1 Then
                lblAviso.Visible = True
                lblAviso.Text = "No terminaste tu certificacion, debes de iniciar de nuevo."
                Dim cnn2 As New SqlConnection(ConexionMars.localSqlServer)

                Dim SQLEliminar As New SqlCommand("DELETE FROM Capacitacion_Certificaciones_Dentastix_tmp WHERE id_usuario = '" & HttpContext.Current.User.Identity.Name & "'", cnn2)
                cnn2.Open()
                SQLEliminar.ExecuteNonQuery()
                SQLEliminar.Dispose()
                cnn2.Close()
                cnn2.Dispose()
                btnIniciar.Enabled = True
            Else

                btnIniciar.Enabled = True
                lblAviso.Visible = False
            End If
        End Using
    End Sub

    Protected Sub btnIniciar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnIniciar.Click
        Response.Redirect("q1q2.aspx")
    End Sub
End Class