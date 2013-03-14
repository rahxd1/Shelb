Public Partial Class DemoPenaraldaCertificacionPreguntas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnSiguiente_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSiguiente.Click
        Response.Redirect("DemoPenaraldaCertificacionResumen.aspx")
    End Sub
End Class