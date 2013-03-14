Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Partial Public Class q1q2
    Inherits System.Web.UI.Page
    Dim Q1, Q2, R1, R2 As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnSiguiente_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSiguiente.Click
        asignarValores()
        guardar()
        Response.Redirect("q3q4.aspx")
    End Sub
    Private Sub asignarValores()
        Q1 = Rb1.SelectedValue
        Q2 = Rb2.SelectedValue
        'LA RESPUESTA PREGUNTA 1 ES LA OPCION 1
        If Q1 = 1 Then
            R1 = 1
        Else
            R1 = 0
        End If
        'LA RESPUESTA Q2 ES LA OPCION 2

        If Q2 = 2 Then
            R2 = 1
        Else
            R2 = 0
        End If


    End Sub
    Private Sub guardar()
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim SQLNuevo As New SqlCommand("INSERT INTO Capacitacion_Certificaciones_Dentastix_tmp" & _
                               "(id_usuario, fecha,q1,q2) " & _
                               "VALUES(@id_usuario, @fecha,@q1,@q2)", cnn)
            cnn.Open()
            SQLNuevo.Parameters.AddWithValue("@id_usuario", HttpContext.Current.User.Identity.Name)
            SQLNuevo.Parameters.AddWithValue("@fecha", Date.Today)
            SQLNuevo.Parameters.AddWithValue("@q1", R1)
            SQLNuevo.Parameters.AddWithValue("@q2", R2)
            SQLNuevo.ExecuteNonQuery()
            cnn.Close()
            cnn.Dispose()

        End Using
    End Sub
End Class