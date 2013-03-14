Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Partial Public Class q7q8
    Inherits System.Web.UI.Page
    Dim Q7, Q8, R7, R8 As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnSiguiente_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSiguiente.Click
        asignarValores()
        guardar()
        Response.Redirect("q9q10.aspx")
    End Sub
    Private Sub asignarValores()
        Q7 = Rb7.SelectedValue
        Q8 = Rb8.SelectedValue
        'LA RESPUESTA PREGUNTA 7 ES LA OPCION 3
        If Q7 = 3 Then
            R7 = 1
        Else
            R7 = 0
        End If
        'LA RESPUESTA pregunta 8 ES LA OPCION 1

        If Q8 = 1 Then
            R8 = 1
        Else
            R8 = 0
        End If


    End Sub
    Private Sub guardar()
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim SQLNuevo As New SqlCommand("UPDATE Capacitacion_Certificaciones_Dentastix_tmp " & _
                               "SET q7 = " & R7 & ", q8 = " & R8 & " WHERE id_usuario = '" & HttpContext.Current.User.Identity.Name & "'", cnn)
            cnn.Open()
            SQLNuevo.ExecuteNonQuery()
            SQLNuevo.Dispose()
            cnn.Close()
            cnn.Dispose()

        End Using
    End Sub
End Class