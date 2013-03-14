Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Partial Public Class q5q6
    Inherits System.Web.UI.Page
    Dim Q5, Q6, R5, R6 As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnSiguiente_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSiguiente.Click
        asignarValores()
        guardar()
        Response.Redirect("q7q8.aspx")
    End Sub
    Private Sub asignarValores()
        Q5 = Rb5.SelectedValue
        Q6 = Rb6.SelectedValue
        'LA RESPUESTA PREGUNTA 5 ES LA OPCION 3
        If Q5 = 3 Then
            R5 = 1
        Else
            R5 = 0
        End If
        'LA RESPUESTA pregunta 6 ES LA OPCION 3

        If Q6 = 3 Then
            R6 = 1
        Else
            R6 = 0
        End If


    End Sub
    Private Sub guardar()
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim SQLNuevo As New SqlCommand("UPDATE Capacitacion_Certificaciones_Dentastix_tmp " & _
                               "SET q5 = " & R5 & ", q6 = " & R6 & " WHERE id_usuario = '" & HttpContext.Current.User.Identity.Name & "'", cnn)
            cnn.Open()
            SQLNuevo.ExecuteNonQuery()
            SQLNuevo.Dispose()
            cnn.Close()
            cnn.Dispose()

        End Using
    End Sub
End Class