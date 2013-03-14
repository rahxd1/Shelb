Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Partial Public Class q9q10
    Inherits System.Web.UI.Page
    Dim Q9, Q10, R9, R10 As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnSiguiente_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSiguiente.Click
        asignarValores()
        guardar()
        Response.Redirect("q11q12.aspx")
    End Sub
    Private Sub asignarValores()
        Q9 = Rb9.SelectedValue
        Q10 = Rb10.SelectedValue
        'LA RESPUESTA PREGUNTA 9 ES LA OPCION 2
        If Q9 = 2 Then
            R9 = 1
        Else
            R9 = 0
        End If
        'LA RESPUESTA pregunta 10 ES LA OPCION 1

        If Q10 = 1 Then
            R10 = 1
        Else
            R10 = 0
        End If


    End Sub
    Private Sub guardar()
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim SQLNuevo As New SqlCommand("UPDATE Capacitacion_Certificaciones_Dentastix_tmp " & _
                               "SET q9 = " & R9 & ", q10 = " & R10 & " WHERE id_usuario = '" & HttpContext.Current.User.Identity.Name & "'", cnn)
            cnn.Open()
            SQLNuevo.ExecuteNonQuery()
            SQLNuevo.Dispose()
            cnn.Close()
            cnn.Dispose()

        End Using
    End Sub
End Class