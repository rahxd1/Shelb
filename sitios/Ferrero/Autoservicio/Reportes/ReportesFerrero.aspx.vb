Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports procomlcd

Partial Public Class ReportesFerrero
    Inherits System.Web.UI.Page

    Sub CargarReportes()
        If Consulta = 1 Then
            CargaGrilla(ConexionFerrero.localSqlServer, "SELECT * FROM Reportes WHERE estatus = 1 " & _
                        "AND id_proyecto=27 ORDER BY id_reporte", Me.gridReportes)
        Else
            Response.Redirect("../MenuFerrero.aspx")
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionFerrero.localSqlServer, HttpContext.Current.User.Identity.Name)
            CargarReportes()
        End If
    End Sub

End Class