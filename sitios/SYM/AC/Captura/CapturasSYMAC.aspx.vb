Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports ISODates.Dates
Imports System.Security.Cryptography

Partial Public Class CapturasSYMAC
    Inherits System.Web.UI.Page

    Sub CargarReportes()
        Dim SQL As String
        If Tipo_usuario = 100 Or Tipo_usuario = 12 Then
            SQL = "SELECT * FROM Captura WHERE id_proyecto=31 ORDER BY id_captura" : Else
            SQL = "SELECT * FROM Captura WHERE id_proyecto=31 AND estatus=1 and tipo_usuario=" & Tipo_usuario & " ORDER BY id_captura" : End If

        CargaGrilla(ConexionSYM.localSqlServer, SQL, gridCapturas)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionSYM.localSqlServer, HttpContext.Current.User.Identity.Name)

            CargarReportes()
        End If
    End Sub

End Class