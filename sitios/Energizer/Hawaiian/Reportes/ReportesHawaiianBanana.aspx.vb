Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports procomlcd

Partial Public Class ReportesHawaiianBanana
    Inherits System.Web.UI.Page

    Sub CargarReportes()
        CargaGrilla(ConexionEnergizer.localSqlServer, _
                    "SELECT * FROM Reportes WHERE estatus = 1 AND id_proyecto=21 ORDER BY id_reporte", _
                    Me.gridReportes)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionEnergizer.localSqlServer, HttpContext.Current.User.Identity.Name)

            If Consulta = 1 Then
                CargarReportes() : Else
                Response.Redirect("../menu_HawaiianBanana.aspx") : End If
        End If
    End Sub

End Class