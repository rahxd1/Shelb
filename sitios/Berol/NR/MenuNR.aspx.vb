Imports System.Data.SqlClient

Partial Public Class MenuNR
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionBerol.localSqlServer, HttpContext.Current.User.Identity.Name)

            Avisos()

            If HttpContext.Current.User.Identity.Name = "MELINA" Or _
                HttpContext.Current.User.Identity.Name = "GABRIELA" Then
                lnkAdmin.Visible = True : Else
                If Tipo_usuario = 100 Then
                    lnkAdmin.Visible = True : Else
                    lnkAdmin.Visible = False : End If
            End If
        End If
    End Sub

    Sub Avisos()
        CargaGrilla(ConexionBerol.localSqlServer, _
                    "SELECT * from Avisos " & _
                    "WHERE fecha_inicio<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                    "AND fecha_fin>= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "'", _
                    Me.gridAvisos)
    End Sub
End Class