Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports procomlcd

Partial Public Class ManualesMars
    Inherits System.Web.UI.Page

    Sub CargarReportes()
        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT * FROM Manuales " & _
                        "WHERE estatus = 1 " & _
                        "AND tipo_usuario<>" & Tipo_usuario & " " & _
                        "ORDER BY id_manual", gridManuales)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionMars.localSqlServer, HttpContext.Current.User.Identity.Name)

            CargarReportes()
        End If
    End Sub
End Class