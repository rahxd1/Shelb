Imports System.Data.SqlClient
Imports System.Data
Imports procomlcd
Imports System.Web.Mail
Imports System.Text
Imports System.Data.Odbc
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class PaginaError

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnEnviar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEnviar.Click
        If txtCorreo.Text <> txtCorreoConfirma.Text Then
            lblCorreo.Text = "El correo no coincide, por favor reingresalo correctamente."
            Exit Sub
        End If

        Dim Usuario As String
        Usuario = HttpContext.Current.User.Identity.Name

        If Tipo_usuario <> 100 Then
            Using cnn As New SqlConnection(ConexionAdmin.localSqlServer)
                cnn.Open()

                Dim SQLBusca As New SqlCommand("SELECT * FROM Reportes WHERE resuelto=0 AND id_usuario='" & HttpContext.Current.User.Identity.Name & "'", cnn)
                Dim Data As New SqlDataAdapter(SQLBusca)
                Dim Tabla As New DataTable
                Data.Fill(Tabla)

                If Tabla.Rows.Count = 1 Then
                    lblAviso.Text = "Existe un reporte pendiente, por favor espera la contestacion en tu correo, gracias."

                    cnn.Close()
                    cnn.Dispose()
                    Exit Sub : End If

                Dim Folio As Integer
                Dim sqlGuardar As New SqlCommand("INSERT INTO Reportes" & _
                        "(id_usuario,dirigido,nombre, correo, lada,telefono, problema, comentario_resuelto) " & _
                        "VALUES(@id_usuario,'SISTEMAS',@nombre, @correo, @lada, @telefono, @problema, @comentario_resuelto) " & _
                        "SELECT @@IDENTITY AS 'id_reporte'", cnn)
                sqlGuardar.Parameters.AddWithValue("@id_usuario", HttpContext.Current.User.Identity.Name)
                sqlGuardar.Parameters.AddWithValue("@nombre", txtNombre.Text)
                sqlGuardar.Parameters.AddWithValue("@correo", txtCorreo.Text)
                sqlGuardar.Parameters.AddWithValue("@lada", txtLada.Text)
                sqlGuardar.Parameters.AddWithValue("@telefono", txtTelefono.Text)
                sqlGuardar.Parameters.AddWithValue("@problema", txtComentario.Text)
                sqlGuardar.Parameters.AddWithValue("@comentario_resuelto", "")
                Folio = Convert.ToInt32(sqlGuardar.ExecuteScalar())
                sqlGuardar.Dispose()

                lblAviso.Text = "Tu reporte ha sido enviado, el número de ticket es: " & Folio & ", por favor esta al pendiente de tu correo para el seguimiento."

                ''//Envia correo
                Dim correo As New System.Net.Mail.MailMessage()
                correo.From = New System.Net.Mail.MailAddress(txtCorreo.Text)
                correo.To.Add("soporte@procomlcd.mx")
                correo.Subject = "Reporte No." & Folio & " www.procomlcd.mx"
                correo.Body = "Usuario:" & HttpContext.Current.User.Identity.Name & " <br />" & _
                                "Nombre:" & txtNombre.Text & "  <br />" & _
                                "Teléfono:(" & txtLada.Text & ")-" & txtTelefono.Text & " <br />" & _
                                "Problema: " & txtComentario.Text & " <br />"

                correo.IsBodyHtml = True
                correo.Priority = System.Net.Mail.MailPriority.Normal

                ''//Envia correo
                Dim correo2 As New System.Net.Mail.MailMessage()
                correo2.From = New System.Net.Mail.MailAddress("soporte@procomlcd.mx")
                correo2.To.Add(txtCorreo.Text)
                correo2.Subject = "Reporte No." & Folio & " www.procomlcd.mx"
                correo2.Body = "Hola " & txtNombre.Text & " <br />" & _
                                "Tu reporte ha sido enviado, espera en breve la contestación. <br />" & _
                                "Gracias <br />" & _
                                "<br /> Atte. Soporte Procomlcd <br />"

                correo2.IsBodyHtml = True
                correo2.Priority = System.Net.Mail.MailPriority.Normal

                Dim smtp As New System.Net.Mail.SmtpClient
                smtp.Host = "mail.procomlcd.mx"
                smtp.Port = "587"
                smtp.Credentials = New System.Net.NetworkCredential("soporte@procomlcd.mx", "sopo1597")
                smtp.EnableSsl = False

                Try
                    smtp.Send(correo)
                    smtp.Send(correo2)
                    'MsgBox("Mensaje enviado satisfactoriamente")
                Catch ex As Exception
                    'MsgBox("ERROR: " & ex.Message)
                End Try

                cnn.Close()
                cnn.Dispose()
            End Using
        End If

        txtNombre.Text = ""
        txtCorreo.Text = ""
        txtLada.Text = ""
        txtTelefono.Text = ""
        txtComentario.Text = ""
        txtCorreoConfirma.Text = ""
    End Sub
End Class