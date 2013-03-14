Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports ISODates.Dates
Imports procomlcd
Partial Public Class Capacitacion_Mars
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Protected Sub lnkLogOff_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkCerrarSesion.Click
        FormsAuthentication.SignOut()
        Response.Redirect("~/acceso.aspx")
    End Sub
End Class