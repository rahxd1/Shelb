Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports procomlcd

Partial Public Class ReportesAntiguosMarsAS
    Inherits System.Web.UI.Page

    Sub CargarReportes()
        If Consulta = 1 Then
            CargaGrilla(ConexionMars.localSqlServer, _
                        "SELECT * FROM Reportes_Antiguos " & _
                        "WHERE estatus = 1 AND id_proyecto=7 " & _
                        "AND tipo_usuario<>" & Tipo_usuario & " " & _
                        "ORDER BY id_reporte", gridReportes)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionMars.localSqlServer, HttpContext.Current.User.Identity.Name)

            CargarReportes()
        End If
    End Sub

End Class