Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Partial Public Class q15q16
    Inherits System.Web.UI.Page
    Dim Q15, Q16, R15, R16 As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnSiguiente_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSiguiente.Click
        asignarValores()
        guardar()
        Response.Redirect("q17q18.aspx")
    End Sub
    Private Sub asignarValores()
        Q15 = Rb15.SelectedValue
        Q16 = Rb16.SelectedValue
        'LA RESPUESTA PREGUNTA 15 ES LA OPCION 1
        If Q15 = 1 Then
            R15 = 1
        Else
            R15 = 0
        End If
        'LA RESPUESTA pregunta 16 ES LA OPCION 1

        If Q16 = 1 Then
            R16 = 1
        Else
            R16 = 0
        End If


    End Sub
    Private Sub guardar()
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim SQLNuevo As New SqlCommand("UPDATE Capacitacion_Certificaciones_Dentastix_tmp " & _
                               "SET q15 = " & R15 & ", q16 = " & R16 & " WHERE id_usuario = '" & HttpContext.Current.User.Identity.Name & "'", cnn)
            cnn.Open()
            SQLNuevo.ExecuteNonQuery()
            SQLNuevo.Dispose()
            cnn.Close()
            cnn.Dispose()

        End Using
    End Sub
End Class