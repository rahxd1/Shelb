﻿Imports System.Data.SqlClient
Imports System.Data
Imports procomlcd
Imports System.Data.Sql
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class AvisosFluid
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionFluidmaster.localSqlServer, HttpContext.Current.User.Identity.Name)

            CargaAvisos()

            If Tipo_usuario = 1 Then
                Leido()
            End If
        End If
    End Sub

    Sub CargaAvisos()
        CargaGrilla(ConexionFluidmaster.localSqlServer, _
                    "SELECT * from Avisos " & _
                    "WHERE id_aviso=" & Request.Params("ID") & "", _
                    Me.gridAvisos)
    End Sub

    Sub Leido()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                                               "SELECT * From Avisos_Historial  " & _
                                               "WHERE id_usuario='" & HttpContext.Current.User.Identity.Name & "' " & _
                                               "AND id_aviso=" & Request.Params("ID") & "")
        If Tabla.Rows.Count = 0 Then
            Dim fecha_leido As String = ISODates.Dates.SQLServerDate(CDate(Date.Now))
            BD.Execute(ConexionFluidmaster.localSqlServer, _
                       "INSERT INTO Avisos_Historial" & _
                       "(id_aviso, fecha_leido, id_usuario) " & _
                       "VALUES(" & Request.Params("ID") & ", '" & fecha_leido & "', " & _
                       "'" & HttpContext.Current.User.Identity.Name & "')")
        End If

        Tabla.Dispose()
    End Sub

End Class