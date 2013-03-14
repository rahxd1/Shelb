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

Partial Public Class CapturasFerreroDanone
    Inherits System.Web.UI.Page

    Sub CargarReportes()
        Dim SQL As String

        If Tipo_usuario = 3 Or Tipo_usuario = 12 Then
            SQL = "SELECT * FROM Captura WHERE id_proyecto =27 AND estatus = 1 and tipo_usuario = '2' OR tipo_usuario= '1' ORDER BY id_captura"
        Else
            If Tipo_usuario = 2 Or Tipo_usuario = 16 Then
                SQL = "SELECT * FROM Captura WHERE id_proyecto =27 AND estatus = 1 and tipo_usuario = '" & Tipo_usuario & "' ORDER BY id_captura" : Else
                SQL = "SELECT * FROM Captura WHERE id_proyecto =27 ORDER BY id_captura" : End If
        End If

        CargaGrilla(ConexionFerrero.localSqlServer, SQL, Me.gridCapturas)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionEnergizer.localSqlServer, HttpContext.Current.User.Identity.Name)

            If Tipo_usuario <> 10 Or Tipo_usuario <> 12 Or Tipo_usuario <> 100 Then
                Response.Redirect("../Menu_FerreroDanone.aspx")
            Else
                CargarReportes()
            End If
        End If
    End Sub

End Class