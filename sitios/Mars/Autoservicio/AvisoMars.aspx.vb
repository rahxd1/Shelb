Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class AvisoMars
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CargaGrilla(ConexionMars.localSqlServer, _
                        "SELECT * FROM AS_Avisos " & _
                        "WHERE id_aviso=" & Request.Params("id") & "", Me.gridDetalle)
        End If
    End Sub

End Class