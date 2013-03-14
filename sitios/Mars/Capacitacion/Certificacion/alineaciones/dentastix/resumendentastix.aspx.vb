Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Partial Public Class resumendentastix
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ChecarCertificado()
    End Sub

    Private Sub ChecarCertificado()
        Dim Certificado As Integer
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim SQL As New SqlCommand("SELECT * FROM Capacitacion_Certificaciones_Dentastix " & _
                           "WHERE id_usuario = '" & HttpContext.Current.User.Identity.Name & "'", cnn)
            cnn.Open()
            Dim Data As New SqlDataAdapter(SQL)
            Dim Tabla As New DataTable
            Data.Fill(Tabla)
            cnn.Close()
            cnn.Dispose()
            If Tabla.Rows.Count = 1 Then
                lblAviso.Visible = True
                Certificado = Tabla.Rows(0)("certificado")
                If Certificado = 1 Then
                    lblAviso.Text = "Felicidades,gracias a tu dedicacion ahora Estas Certificado en Dentastix"
                Else
                    lblAviso.Text = "Desafortunadamente no lograste certificarte, volveras a ser capacitado"
                End If
            End If
        End Using
    End Sub
End Class