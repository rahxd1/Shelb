Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class MenuAdmin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionAdmin.localSqlServer, HttpContext.Current.User.Identity.Name)

            If Not tipo_usuario = 100 Then
                Response.Redirect("../default.aspx") : End If
        End If
    End Sub

End Class