Imports System.Data.SqlClient

Partial Public Class Clientes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CargaGrilla(ConexionAdmin.localSqlServer, _
                    "SELECT * FROM Clientes ORDER BY nombre_cliente ", gridAccesos)
        End If
    End Sub

End Class