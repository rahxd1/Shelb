Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports ISODates.Dates
Imports procomlcd

Partial Public Class MenuMayoreo
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

        Dim cnn As New SqlConnection(ConexionMars.localSqlServer)
        cnn.Open()


        Dim SQL As New SqlCommand("execute Ver_Calendario '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "'", cnn)
        Dim Tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQL)
        Data.Fill(Tabla)

        If Tabla.Rows.Count > 0 Then
            lblPeriodo.Text = Tabla.Rows(0)("periodo")
        End If

        cnn.Close()
        cnn.Dispose()
    End Sub

End Class