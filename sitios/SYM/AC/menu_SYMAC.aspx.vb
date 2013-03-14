Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class menu_SYMAC
    Inherits System.Web.UI.Page

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionSYM.localSqlServer, HttpContext.Current.User.Identity.Name)

            Avisos()

            If HttpContext.Current.User.Identity.Name = "CYNTHIA" Then
                lnkAdmin.Visible = True : Else
                If Tipo_usuario = 100 Then
                    lnkAdmin.Visible = True : Else
                    lnkAdmin.Visible = False : End If
            End If
        End If
    End Sub

    Sub Avisos()
        CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT * from Avisos " & _
                    "WHERE fecha_inicio<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                    "AND fecha_fin>= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "'", _
                    Me.gridAvisos)
    End Sub

End Class