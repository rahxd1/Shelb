Imports System.Data.SqlClient
Imports System.Data
Imports procomlcd
Imports System.Web.Mail
Imports System.Text
Imports System.Data.Odbc
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class CrearEventosSYMPrecios
    Inherits System.Web.UI.Page

    Dim UsuarioSel, CadenaSel As String
    Dim Suma, Generados, TipoPeriodo As Integer
    Dim DuplicaRutas, Correos, CuerpoMail As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LstProyectos(ConexionSYM.localSqlServer, Me.gridProyectos)

            Combo.LlenaDrop(ConexionSYM.localSqlServer, "SELECT * FROM Precios_Periodos ORDER BY id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, "SELECT distinct id_usuario FROM Precios_CatRutas ORDER BY id_usuario", "id_usuario", "id_usuario", cmbUsuario)
        End If
    End Sub

    Public Function Duplicalo(ByVal IDPeriodo As String, ByVal IDUsuario As String, _
                              ByVal IDCadena As String) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT id_periodo, id_usuario, id_cadena " & _
                                               "FROM Precios_Rutas_Eventos " & _
                                               "WHERE id_periodo=" & IDPeriodo & " " & _
                                               "AND id_usuario='" & IDUsuario & "' " & _
                                               "AND id_cadena=" & IDCadena & " ")

        If Tabla.Rows.Count = 1 Then
            Me.lblAviso.Text = "NO SE CARGO NINGUNA RUTA NUEVA"
            Exit Function
        Else
            BD.Execute(ConexionSYM.localSqlServer, _
                       "INSERT INTO Precios_Rutas_Eventos" & _
                       "(id_periodo,id_usuario, id_cadena) " & _
                       "VALUES(" & IDPeriodo & ",'" & IDUsuario & "'," & IDCadena & ")")

            Generados = Generados + 1
        End If

        Tabla.Dispose()
    End Function

    Sub VerTipo()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT * FROM Precios_Periodos " & _
                                               "WHERE id_periodo=" & cmbPeriodo.SelectedValue & "")
        If Tabla.Rows.Count > 0 Then
            TipoPeriodo = Tabla.Rows(0)("tipo_periodo")
        End If

        Tabla.Dispose()
    End Sub

    Protected Sub btnCargar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCargar.Click
        Generados = 0
        DuplicaRutas = "DUPLICAR"
        VerTipo()

        Dim SQL As String
        If cmbUsuario.Text = "" Then
            SQL = "SELECT * FROM Precios_CatRutas WHERE semanal_mensual=" & TipoPeriodo & " ORDER BY id_usuario"
        Else
            SQL = "SELECT * FROM Precios_CatRutas WHERE semanal_mensual=" & TipoPeriodo & " AND id_usuario ='" & cmbUsuario.Text & "' ORDER BY id_usuario"
        End If

        CargaGrilla(ConexionSYM.localSqlServer,SQL, Me.gridRuta)

        DuplicaRutas = ""
    End Sub

    Private Sub gridRuta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRuta.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If DuplicaRutas = "DUPLICAR" Then
                UsuarioSel = e.Row.Cells(0).Text
                CadenaSel = e.Row.Cells(1).Text
                Duplicalo(cmbPeriodo.Text, UsuarioSel, CadenaSel)

                If Generados >= 1 Then
                    Dim Totales As String
                    Totales = Generados
                    Me.lblAviso.Visible = True
                    Me.lblAviso.Text = "SE CARGARON " + Totales + " RUTAS EXITOSAMENTE."
                    EnviarCorreo()
                End If
            End If
        End If
    End Sub

    Sub EnviarCorreo()
        Dim CorreoTipoCaptura, CorreoProcom(50) As String
        If TipoPeriodo = 1 Then
            CorreoTipoCaptura = "Semanal"
            CuerpoMail = "El portal http://procomlcd.mx/ se encuentra habilitado para su captura semanal de Sánchez y Martín. <br />" & _
                "Se te recuerda que tienes hasta el jueves a las 12 de la noche para tener tu captura al 100% evita descuentos o actas administrativas. <br />" & _
                "NOTA: Cualquier problema con el sistema favor de levantar tu ticket desde el portal y no descuides el correo que diste de alta en el reporte. <br />" & _
                "Ahora también ya puedes tener soporte mediante Messenger de 9 am a 9 pm con el correo soportesshelby@live.com.mx <br />"
        Else
            CorreoTipoCaptura = "Mensual"
            CuerpoMail = "El portal http://procomlcd.mx/ se encuentra habilitado para su captura mensual de Sánchez y Martín. <br />" & _
                "Se te recuerda que tienes hasta el último día del mes en curso para tener tu captura al 100% evita descuentos o actas administrativas.<br />" & _
                "NOTA: Cualquier problema con el sistema favor de levantar tu ticket desde el portal y no descuides el correo que diste de alta en el reporte. <br />" & _
                "Ahora también ya puedes tener soporte mediante Messenger de 9 am a 9 pm con el correo soportesshelby@live.com.mx <br />"
        End If

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "select distinct RUT.id_usuario, correo " & _
                                                "FROM Precios_CatRutas as RUT " & _
                                                "INNER JOIN Usuarios as US ON US.id_usuario=RUT.id_usuario " & _
                                                "WHERE semanal_mensual=" & TipoPeriodo & " AND correo<>''")
        Dim Bandera As Integer

        Bandera=0
        If Tabla.Rows.Count > 1 Then
            For i = 0 To Tabla.Rows.Count - 1

                If Bandera = 0 Then
                    CorreoProcom(i) = Tabla.Rows(i)("correo") & " "
                    Bandera = 1
                End If

                If Bandera = 1 Then
                    CorreoProcom(i) = ", " & Tabla.Rows(i)("correo") & " "
                End If



            Next i

            For i = 0 To Tabla.Rows.Count - 1
                Correos = Correos + CorreoProcom(i) : Next i
        End If

        lblAviso.Text = "Se ha enviado correo"


        ''//Envia correo
        Dim correo As New System.Net.Mail.MailMessage()
        correo.From = New System.Net.Mail.MailAddress("soporte@procomlcd.mx")
        correo.To.Add("inplant.sym@e-shelby.com, " & Correos)
        correo.Subject = "SYM Precios Alta de periodo " & CorreoTipoCaptura & " www.procomlcd.mx"
        correo.Body = CuerpoMail

        correo.IsBodyHtml = True
        correo.Priority = System.Net.Mail.MailPriority.Normal

        Dim smtp As New System.Net.Mail.SmtpClient
        smtp.Host = "mail.procomlcd.mx"
        smtp.Port = "587"
        smtp.Credentials = New System.Net.NetworkCredential("soporte@procomlcd.mx", "sopo1597")
        smtp.EnableSsl = False

        Try
            smtp.Send(correo)
            'MsgBox("Mensaje enviado satisfactoriamente")
        Catch ex As Exception
            'MsgBox("ERROR: " & ex.Message)
        End Try

    End Sub

End Class