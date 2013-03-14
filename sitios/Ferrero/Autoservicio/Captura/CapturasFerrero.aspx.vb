﻿Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports procomlcd
Imports procomlcd.Permisos

Partial Public Class CapturasFerrero
    Inherits System.Web.UI.Page

    Sub CargarReportes()
        Dim SQL As String

        If Tipo_usuario = 1 Then
            SQL = "SELECT * FROM Captura WHERE tipo_usuario = '" & Tipo_usuario & "' ORDER BY Captura" : Else
            SQL = "SELECT * FROM Captura ORDER BY id_captura" : End If

        CargaGrilla(ConexionFerrero.localSqlServer, SQL, Me.gridCapturas)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionFerrero.localSqlServer, HttpContext.Current.User.Identity.Name)

            CargarReportes()
        End If
    End Sub
End Class