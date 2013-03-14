Imports System.Text
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class MenuSchick
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionEnergizer.localSqlServer, HttpContext.Current.User.Identity.Name)
        End If
    End Sub

End Class