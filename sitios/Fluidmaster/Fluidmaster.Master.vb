
Partial Public Class Fluidmaster
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub lnkLogOffFluid_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkCerrarSesionFluid.Click
        FormsAuthentication.SignOut()
        Response.Redirect("~/acceso.aspx")
    End Sub
End Class