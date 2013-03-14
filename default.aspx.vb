Imports System.Data.SqlClient
Imports System.Data
Imports procomlcd
Imports System.Web.Mail
Imports System.Text
Imports System.Data.Odbc
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class _default

    Inherits System.Web.UI.Page

    Dim Periodo, Quincena As String
    Dim PeriodoSYM As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionAdmin.localSqlServer, HttpContext.Current.User.Identity.Name)

            If Cliente = 2 Then
                Response.Redirect("default_2.aspx")
            End If

            CargarProyectos()
            lblUsuario.Text = HttpContext.Current.User.Identity.Name

            CargaGrilla(ConexionAdmin.localSqlServer, "SELECT * FROM Reportes " & _
                        "WHERE id_usuario ='" & HttpContext.Current.User.Identity.Name & "' " & _
                        "AND resuelto=0", gridReportes)

            If Tipo_usuario = 10 Then
                CapturaASMars()
                CapturaSYMAC()
                pnlProcom.Visible = True : Else
                pnlProcom.Visible = False : End If
        End If
    End Sub

    Sub CapturaSYMAC()
        Dim TablaPeriodo As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT * FROM AC_Periodos order by fecha_cierre DESC")
        If TablaPeriodo.Rows.Count >= 1 Then
            PeriodoSYM = TablaPeriodo.Rows(0)("id_periodo") : End If

        Dim SQL As String = "SELECT REG.nombre_region,RE.id_usuario, COUNT(RE.id_usuario) AS Tiendas, " & _
            "ISNULL(CapturasAnaq.Capturas,0)CapturasA, ISNULL(IncompletasAnaq.Incompletas,0)IncompletasA,  " & _
            "ISNULL(CapturasCat.Capturas,0)CapturasC,ISNULL(IncompletasCat.Incompletas,0)IncompletasC, " & _
            "convert(nvarchar(10),((100*((ISNULL(CapturasAnaq.Capturas,0)+ISNULL(CapturasCat.Capturas,0))/2))/COUNT(RE.id_usuario)))+'%'Captura " & _
            "FROM AC_Rutas_Eventos as RE  " & _
            "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario=RE.id_usuario  " & _
            "INNER JOIN AC_Tiendas as TI ON TI.id_tienda= RE.id_tienda  " & _
            "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region  " & _
            "FULL JOIN (SELECT id_usuario, COUNT(RE.estatus_anaquel) AS Capturas  " & _
            "FROM AC_Rutas_Eventos as RE WHERE estatus_anaquel=1 AND id_periodo=" & PeriodoSYM & "  GROUP BY id_usuario)CapturasAnaq  " & _
            "ON CapturasAnaq.id_usuario = RE.id_usuario  " & _
            "FULL JOIN (SELECT id_usuario, COUNT(RE.estatus_anaquel) AS Incompletas  " & _
            "FROM AC_Rutas_Eventos as RE WHERE estatus_anaquel=2 AND id_periodo=" & PeriodoSYM & "  GROUP BY id_usuario)IncompletasAnaq  " & _
            "ON IncompletasAnaq.id_usuario = RE.id_usuario  " & _
            "FULL JOIN (SELECT id_usuario, COUNT(RE.estatus_catalogacion) AS Capturas  " & _
            "FROM AC_Rutas_Eventos as RE WHERE estatus_catalogacion=1 AND id_periodo=" & PeriodoSYM & "  GROUP BY id_usuario)CapturasCat  " & _
            "ON CapturasCat.id_usuario = RE.id_usuario  " & _
            "FULL JOIN (SELECT id_usuario, COUNT(RE.estatus_catalogacion) AS Incompletas  " & _
            "FROM AC_Rutas_Eventos as RE WHERE estatus_catalogacion=2 AND id_periodo=" & PeriodoSYM & "  GROUP BY id_usuario)IncompletasCat  " & _
            "ON IncompletasCat.id_usuario = RE.id_usuario  " & _
            "WHERE RE.id_periodo=" & PeriodoSYM & " AND RE.id_usuario<>'&nbsp;' " & _
            "AND REL.id_supervisor='" & HttpContext.Current.User.Identity.Name & "' " & _
            "GROUP BY RE.id_usuario,CapturasAnaq.Capturas,IncompletasAnaq.Incompletas,CapturasCat.Capturas, " & _
            "IncompletasCat.Incompletas,RE.id_periodo, REG.nombre_region "

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, SQL)

        If tabla.Rows.Count > 0 Then
            gridSYMAC.Visible = True : Else
            gridSYMAC.Visible = False : End If

        Tabla.Dispose()

        CargaGrilla(ConexionSYM.localSqlServer, SQL, gridSYMAC)
    End Sub

    Sub CapturaASMars()
        Dim TablaPeriodo As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                                      "SELECT * FROM Periodos_Nuevo where fecha_inicio_periodo<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' AND fecha_fin_periodo>= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "'")
        If TablaPeriodo.Rows.Count = 1 Then
            Periodo = TablaPeriodo.Rows(0)("orden") : End If

        Dim Fecha_Q1, Fecha_Q2 As Date
        Fecha_Q1 = TablaPeriodo.Rows(0)("fecha_inicio_periodo")
        Fecha_Q2 = Fecha_Q1.AddDays(14)

        If Fecha_Q1 <= CDate(Date.Today) And Fecha_Q2 >= CDate(Date.Today) Then
            Quincena = "Q1" : Else
            Quincena = "Q2" : End If

        Dim SQL As String = "SELECT RE.id_usuario, COUNT(RE.id_tienda) AS Reportes, " & _
            "ISNULL(CapturasP1.Capturas,0) as 'Anaquel " & Quincena & "', " & _
            "convert(nvarchar(10),(100*(ISNULL(CapturasP1.Capturas,0))/COUNT(RE.id_tienda)))+'%'Captura  " & _
            "FROM AS_Rutas_Eventos as RE  " & _
            "INNER JOIN AS_Tiendas as TI ON TI.id_tienda= RE.id_tienda  " & _
            "INNER JOIN Regiones as REG ON REG.id_region= TI.id_region  " & _
            "FULL JOIN (SELECT id_usuario, COUNT(RE.estatus_anaquel) AS Capturas  " & _
            "FROM AS_Rutas_Eventos as RE WHERE estatus_anaquel=1 AND id_quincena='" & Quincena & "' AND orden=" & Periodo & " " & _
            "GROUP BY id_usuario)CapturasP1 " & _
            "ON CapturasP1.id_usuario = RE.id_usuario " & _
            "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario = RE.id_usuario " & _
            "WHERE RE.orden=" & Periodo & " AND RE.id_quincena='" & Quincena & "'" & _
            " AND REL.id_supervisor= '" & HttpContext.Current.User.Identity.Name & "' " & _
            " GROUP BY RE.id_usuario,CapturasP1.Capturas,RE.id_periodo, REG.nombre_region "

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, SQL)

        If Tabla.Rows.Count > 0 Then
            gridASMars.Visible = True : Else
            gridASMars.Visible = False : End If

        Tabla.Dispose()

        CargaGrilla(ConexionMars.localSqlServer, SQL, gridASMars)
    End Sub

    Public Sub Logout()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionUsuarios.localSqlServer, _
                                               "select * from Bitacora WHERE id_usuario='" & HttpContext.Current.User.Identity.Name & "' ORDER BY folio DESC ")
        If Tabla.Rows.Count > 0 Then
            BD.Execute(ConexionUsuarios.localSqlServer, "UPDATE Bitacora " & _
                            "SET salida='" & ISODates.Dates.SQLServerDate(CDate(DateTime.Now)) & "' " & _
                            "WHERE id_usuario='" & HttpContext.Current.User.Identity.Name & "' ")
        End If

        Tabla.Dispose()

        Session.Abandon()
    End Sub

    Sub CargarProyectos()
        CargaGrilla(ConexionUsuarios.localSqlServer, _
                    "select PROY.imagen as imagen1, PROY2.imagen as imagen2,  " & _
                    "PROY.url as url1,PROY2.url as url2 " & _
                    "from(SELECT rank() OVER (ORDER BY rank)Fila,imagen, url, id_cliente " & _
                    "FROM(SELECT PROY.id_cliente,ACC.id_usuario,rank() OVER (ORDER BY PROY.nombre_proyecto) as rank,  " & _
                    "PROY.nombre_proyecto,PROY.imagen,PROY.URL FROM Proyectos as PROY   " & _
                    "INNER JOIN Acceso_Proyectos as ACC ON PROY.id_proyecto = ACC.id_proyecto   " & _
                    "WHERE ACC.id_usuario = '" & HttpContext.Current.User.Identity.Name & "' AND PROY.activo=1)H " & _
                    "WHERE rank %2=1)PROY FULL JOIN (SELECT rank() OVER (ORDER BY rank)Fila,imagen, url " & _
                    "FROM(SELECT ACC.id_usuario,rank() OVER (ORDER BY PROY.nombre_proyecto) as rank,  " & _
                    "PROY.nombre_proyecto,PROY.imagen,PROY.URL FROM Proyectos as PROY   " & _
                    "INNER JOIN Acceso_Proyectos as ACC ON PROY.id_proyecto = ACC.id_proyecto   " & _
                    "WHERE ACC.id_usuario = '" & HttpContext.Current.User.Identity.Name & "' AND PROY.activo=1)H " & _
                    "WHERE rank %2=0)PROY2 ON PROY.Fila=PROY2.Fila ORDER BY PROY.id_cliente", gridAccesos)
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

    Private Sub lnkReporte_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkReporte.Click
        pnlReporte.Visible = True
    End Sub

    Private Sub lnkCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCerrar.Click
        pnlReporte.Visible = False
    End Sub
End Class