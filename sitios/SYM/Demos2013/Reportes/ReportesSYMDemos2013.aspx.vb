Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports procomlcd
Partial Public Class ReportesSYMDemos2013
    Inherits System.Web.UI.Page
    Sub CargarReportes()
        If Consulta = 1 Then
            CargaGrilla(ConexionSYM.localSqlServer, _
                        "SELECT * FROM Reportes WHERE estatus = 1 AND id_proyecto=43 " & _
                        "ORDER BY id_reporte", gridReportes)
        Else
            Response.Redirect("../menu_SYMDemos2013.aspx")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionSYM.localSqlServer, HttpContext.Current.User.Identity.Name)

            CargarReportes()
        End If
    End Sub

End Class