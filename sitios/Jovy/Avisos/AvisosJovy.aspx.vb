Imports System.Data.SqlClient
Imports System.Data
Imports procomlcd
Imports System.Data.Sql
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class AvisosJovy
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CargaAvisos()
        End If
    End Sub

    Sub CargaAvisos()
        CargaGrilla(ConexionJovy.localSqlServer, _
                    "SELECT * from Avisos " & _
                    "WHERE fecha_inicio<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                    "AND fecha_fin>= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "'", _
                    Me.gridAvisos)
    End Sub

End Class