Public Partial Class inicio
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnENTRAR_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnENTRAR.Click
        Response.Redirect("intro.aspx")
    End Sub
End Class