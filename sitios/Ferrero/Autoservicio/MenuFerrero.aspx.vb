Imports System.Data.SqlClient

Partial Public Class MenuFerrero
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionFerrero.localSqlServer, HttpContext.Current.User.Identity.Name)
        End If
    End Sub

End Class