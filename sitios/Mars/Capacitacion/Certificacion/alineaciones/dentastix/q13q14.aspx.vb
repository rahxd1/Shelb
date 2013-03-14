Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Partial Public Class q13q14
    Inherits System.Web.UI.Page
    Dim Q13, Q14, R13, R14 As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnSiguiente_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSiguiente.Click
        asignarValores()
        guardar()
        Response.Redirect("q15q16.aspx")
    End Sub
    Private Sub asignarValores()
        Q13 = Rb13.SelectedValue
        Q14 = Rb14.SelectedValue
        'LA RESPUESTA PREGUNTA 13 ES LA OPCION 2
        If Q13 = 2 Then
            R13 = 1
        Else
            R13 = 0
        End If
        'LA RESPUESTA pregunta 14 ES LA OPCION 3

        If Q14 = 3 Then
            R14 = 1
        Else
            R14 = 0
        End If


    End Sub
    Private Sub guardar()
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim SQLNuevo As New SqlCommand("UPDATE Capacitacion_Certificaciones_Dentastix_tmp " & _
                               "SET q13 = " & R13 & ", q14 = " & R14 & " WHERE id_usuario = '" & HttpContext.Current.User.Identity.Name & "'", cnn)
            cnn.Open()
            SQLNuevo.ExecuteNonQuery()
            SQLNuevo.Dispose()
            cnn.Close()
            cnn.Dispose()

        End Using
    End Sub
End Class