Imports System.Data.SqlClient

Partial Public Class Menu_Jovy
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionJovy.localSqlServer, HttpContext.Current.User.Identity.Name)

            Avisos()
        End If
    End Sub

    Sub Avisos()
        CargaGrilla(ConexionJovy.localSqlServer, _
                    "SELECT * from Avisos " & _
                    "WHERE fecha_inicio<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                    "AND fecha_fin>= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "'", _
                    Me.gridAvisos)
    End Sub

End Class