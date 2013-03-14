Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports procomlcd

Partial Public Class ReportesShelby
    Inherits System.Web.UI.Page

    Sub CargarReportes()
        If Consulta = 1 Then
            CargaGrilla(ConexionAdmin.localSqlServer, _
                        "SELECT * FROM Lista_Reportes ORDER BY id_reporte", Me.grid)
        Else
            Response.Redirect("../MenuShelby.aspx")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CargarReportes()
        End If
    End Sub

End Class