Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class AdminMars
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LstProyectos(ConexionMars.localSqlServer, Me.gridProyectos)
        End If
    End Sub
End Class