Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports procomlcd

Partial Public Class ReportesNR
    Inherits System.Web.UI.Page

    Sub CargarReportes()
        If Consulta = 1 Then
            CargaGrilla(ConexionBerol.localSqlServer, _
                        "SELECT * FROM Reportes WHERE estatus = 1 AND id_proyecto=5 " & _
                        "ORDER BY id_reporte", Me.gridReportes) : Else
            Response.Redirect("../MenuNR.aspx") : End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionBerol.localSqlServer, HttpContext.Current.User.Identity.Name)

            CargarReportes()
        End If
    End Sub

End Class