Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports ISODates.Dates
Imports procomlcd

Partial Class SYMPDemos
    Inherits System.Web.UI.MasterPage

    Protected Sub lnkCerrarSesionSYMDemos_Click(ByVal sender As Object, ByVal e As EventArgs)
        FormsAuthentication.SignOut()
        Response.Redirect("~/acceso.aspx")
    End Sub

    Public Sub Logout()
        Dim cnn As New SqlConnection(ConexionUsuarios.localSqlServer)
        cnn.Open()
        Dim SQL As New SqlCommand("select * from Bitacora WHERE id_usuario='" & HttpContext.Current.User.Identity.Name & "' ORDER BY folio DESC ", cnn)
        Dim tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQL)
        Data.Fill(tabla)

        If tabla.Rows.Count > 0 Then
            Dim SQLEditar As New SqlCommand("UPDATE Bitacora " & _
                            "SET salida= @salida " & _
                            "WHERE folio =@folio ", cnn)
            SQLEditar.Parameters.AddWithValue("@folio", tabla.Rows(0)("folio"))
            SQLEditar.Parameters.AddWithValue("@salida", ISODates.Dates.SQLServerDate(CDate(DateTime.Now)))
            SQLEditar.ExecuteNonQuery()
            SQLEditar.Dispose()
        End If

        cnn.Close()
        cnn.Dispose()
    End Sub
End Class

