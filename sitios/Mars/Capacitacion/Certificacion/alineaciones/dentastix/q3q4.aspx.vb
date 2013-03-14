Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Partial Public Class q3q4
    Inherits System.Web.UI.Page
    Dim Q3, Q4, R3, R4 As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnSiguiente_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSiguiente.Click
        asignarValores()
        guardar()
        Response.Redirect("q5q6.aspx")
    End Sub
    Private Sub asignarValores()
        Q3 = Rb3.SelectedValue
        Q4 = Rb4.SelectedValue
        'LA RESPUESTA PREGUNTA 1 ES LA OPCION 3
        If Q3 = 3 Then
            R3 = 1
        Else
            R3 = 0
        End If
        'LA RESPUESTA Q2 ES LA OPCION 1

        If Q4 = 1 Then
            R4 = 1
        Else
            R4 = 0
        End If


    End Sub
    Private Sub guardar()
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim SQLNuevo As New SqlCommand("UPDATE Capacitacion_Certificaciones_Dentastix_tmp " & _
                               "SET q3 = " & R3 & ", q4 = " & R4 & " WHERE id_usuario = '" & HttpContext.Current.User.Identity.Name & "'", cnn)
            cnn.Open()
            SQLNuevo.ExecuteNonQuery()
            SQLNuevo.Dispose()
            cnn.Close()
            cnn.Dispose()

        End Using
    End Sub
End Class