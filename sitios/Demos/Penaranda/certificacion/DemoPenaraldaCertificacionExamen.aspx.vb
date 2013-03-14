Public Partial Class DemoPenaraldaCertificacionExamen
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnIniciar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnIniciar.Click
        Response.Redirect("DemoPenaraldaCertificacionPreguntas.aspx")
    End Sub
End Class