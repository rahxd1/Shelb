Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Partial Public Class q17q18
    Inherits System.Web.UI.Page
    Dim Q17, Q18, R17, R18 As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnSiguiente_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSiguiente.Click
        asignarValores()
        guardar()
        Response.Redirect("q19q20.aspx")
    End Sub

    Private Sub asignarValores()
        Q17 = Rb17.SelectedValue
        Q18 = Rb18.SelectedValue
        'LA RESPUESTA PREGUNTA 17 ES LA OPCION 2
        If Q17 = 2 Then
            R17 = 1
        Else
            R17 = 0
        End If
        'LA RESPUESTA pregunta 18 ES LA OPCION 3

        If Q18 = 3 Then
            R18 = 1
        Else
            R18 = 0
        End If


    End Sub
    Private Sub guardar()
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim SQLNuevo As New SqlCommand("UPDATE Capacitacion_Certificaciones_Dentastix_tmp " & _
                               "SET q17 = " & R17 & ", q18 = " & R18 & " WHERE id_usuario = '" & HttpContext.Current.User.Identity.Name & "'", cnn)
            cnn.Open()
            SQLNuevo.ExecuteNonQuery()
            SQLNuevo.Dispose()
            cnn.Close()
            cnn.Dispose()

        End Using
    End Sub

End Class