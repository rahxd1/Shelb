Public Partial Class DemoPenarandaDinamicaIntro
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        lblAviso.Text = "Esta plataforma es una version Demo, no se puede acceder a este contenido"
    End Sub
End Class