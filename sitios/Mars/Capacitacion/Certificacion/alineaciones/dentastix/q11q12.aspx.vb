Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Partial Public Class q11q12
    Inherits System.Web.UI.Page
    Dim Q11, Q12, R11, R12 As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnSiguiente_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSiguiente.Click
        asignarValores()
        guardar()
        Response.Redirect("q13q14.aspx")
    End Sub
    Private Sub asignarValores()
        Q11 = Rb11.SelectedValue
        Q12 = Rb12.SelectedValue
        'LA RESPUESTA PREGUNTA 11 ES LA OPCION 1
        If Q11 = 1 Then
            R11 = 1
        Else
            R11 = 0
        End If
        'LA RESPUESTA pregunta 10 ES LA OPCION 2

        If Q12 = 2 Then
            R12 = 1
        Else
            R12 = 0
        End If


    End Sub
    Private Sub guardar()
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim SQLNuevo As New SqlCommand("UPDATE Capacitacion_Certificaciones_Dentastix_tmp " & _
                               "SET q11 = " & R11 & ", q12 = " & R12 & " WHERE id_usuario = '" & HttpContext.Current.User.Identity.Name & "'", cnn)
            cnn.Open()
            SQLNuevo.ExecuteNonQuery()
            SQLNuevo.Dispose()
            cnn.Close()
            cnn.Dispose()

        End Using
    End Sub
End Class