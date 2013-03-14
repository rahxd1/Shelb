Imports System.Data.SqlClient
Imports System.Data
Imports procomlcd
Imports System.Web.Mail
Imports System.Text
Imports System.Data.Odbc
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class default_2

    Inherits System.Web.UI.Page

    Dim Periodo, Quincena As String
    Dim PeriodoSYM As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionAdmin.localSqlServer, HttpContext.Current.User.Identity.Name)

            CargarProyectos()
            lblUsuario.Text = HttpContext.Current.User.Identity.Name

            If Tipo_usuario = 10 Then
                pnlProcom.Visible = True : Else
                pnlProcom.Visible = False : End If
        End If
    End Sub

    Public Sub Logout()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionUsuarios.localSqlServer, _
                                               "select * from Bitacora WHERE id_usuario='" & HttpContext.Current.User.Identity.Name & "' ORDER BY folio DESC ")
        If Tabla.Rows.Count > 0 Then
            Dim SQLEditar As New SqlCommand("UPDATE Bitacora " & _
                            "SET salida= @salida " & _
                            "WHERE id_usuario =@id_usuario ")
            SQLEditar.Parameters.AddWithValue("@id_usuario", HttpContext.Current.User.Identity.Name)
            SQLEditar.Parameters.AddWithValue("@salida", ISODates.Dates.SQLServerDate(CDate(DateTime.Now)))
            SQLEditar.ExecuteNonQuery()
            SQLEditar.Dispose()
        End If
        Tabla.Dispose()

        Session.Abandon()
    End Sub

    Sub CargarProyectos()
        CargaGrilla(ConexionUsuarios.localSqlServer, _
                    "SELECT PROY.nombre_proyecto,PROY.URL FROM Proyectos as PROY " & _
                    "INNER JOIN Acceso_Proyectos as ACC ON PROY.id_proyecto = ACC.id_proyecto " & _
                    "WHERE ACC.id_usuario = '" + HttpContext.Current.User.Identity.Name + "' order by PROY.nombre_proyecto", _
                    gridAccesos)
    End Sub

    Protected Sub btnEnviar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEnviar.Click
        If txtCorreo.Text <> txtCorreoConfirma.Text Then
            lblCorreo.Text = "El correo no coincide, por favor reingresalo correctamente."
            Exit Sub
        End If

        Dim Usuario As String
        Usuario = HttpContext.Current.User.Identity.Name

        If Tipo_usuario <> 100 Then
            Dim Tabla As DataTable = Tablas.Ver.DT(ConexionAdmin.localSqlServer, _
                                                   "SELECT * FROM Reportes WHERE resuelto=0 AND id_usuario='" & HttpContext.Current.User.Identity.Name & "'")
            If Tabla.Rows.Count = 1 Then
                lblAviso.Text = "Existe un reporte pendiente, por favor espera la contestacion en tu correo, gracias."
                Exit Sub : End If

            Dim Folio As Integer
            Folio = BD.RT.Execute(ConexionAdmin.localSqlServer, "INSERT INTO Reportes" & _
                    "(id_usuario,dirigido,nombre, correo, lada,telefono, problema, comentario_resuelto) " & _
                    "VALUES('" & HttpContext.Current.User.Identity.Name & "','SISTEMAS'," & _
                    "'" & txtNombre.Text & "','" & txtCorreo.Text & "', '" & txtLada.Text & "', " & _
                    "'" & txtTelefono.Text & "','" & txtComentario.Text & "','') " & _
                    "SELECT @@IDENTITY AS 'id_reporte'")

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

            Tabla.Dispose()
        End If

        txtNombre.Text = ""
        txtCorreo.Text = ""
        txtLada.Text = ""
        txtTelefono.Text = ""
        txtComentario.Text = ""
        txtCorreoConfirma.Text = ""
    End Sub
End Class