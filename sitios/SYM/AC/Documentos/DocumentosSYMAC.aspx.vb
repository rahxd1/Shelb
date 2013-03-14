Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports procomlcd

Partial Public Class DocumentosSYMAC
    Inherits System.Web.UI.Page

    Sub CargarReportes()
        If Consulta = 1 Then
            CargaGrilla(ConexionSYM.localSqlServer, _
                        "SELECT * FROM Documentos WHERE id_proyecto = 31 AND estatus= 1 " & _
                        "ORDER BY id_documento", gridDocumentos)
        Else
            Response.Redirect("../menu_SYMAC.aspx")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionSYM.localSqlServer, HttpContext.Current.User.Identity.Name)

            CargarReportes()
        End If
    End Sub
End Class