Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports ISODates.Dates
Imports procomlcd

Partial Public Class MenuMarsConveniencia
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionMars.localSqlServer, HttpContext.Current.User.Identity.Name)

            Calendario()
        End If
    End Sub

    Sub Calendario()
        CargaGrilla(ConexionMars.localSqlServer, _
                    "execute Ver_Calendario '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "'", _
                    gridCalendario)

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "execute Ver_Calendario '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "'")
        If Tabla.Rows.Count > 0 Then
            lblPeriodo.Text = Tabla.Rows(0)("periodo")
        End If
        Tabla.Dispose()
    End Sub

End Class