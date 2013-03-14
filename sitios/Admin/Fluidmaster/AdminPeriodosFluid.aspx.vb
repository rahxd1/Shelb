Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class AdminPeriodosFluid
    Inherits System.Web.UI.Page

    Dim Correos As String

    Private Sub VerDatos(ByVal SeleccionIDPeriodo As String)
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                                               "SELECT * FROM Periodos " & _
                                               "WHERE id_periodo=" & SeleccionIDPeriodo & "")
        If Tabla.Rows.Count > 0 Then
            lblIDPeriodo.Text = Tabla.Rows(0)("id_periodo")
            txtNombrePeriodo.Text = Tabla.Rows(0)("nombre_periodo")
            txtFechaInicio.Text = Tabla.Rows(0)("fecha_inicio")
            txtFechaFin.Text = Tabla.Rows(0)("fecha_fin")
            txtFechaCierre.Text = Tabla.Rows(0)("fecha_cierre")
        End If

        Tabla.Dispose()
    End Sub

    Sub CargarPeriodo()
        pnlConsulta.Visible = True

        CargaGrilla(ConexionFluidmaster.localSqlServer, _
                    "SELECT * FROM Periodos ORDER BY id_periodo DESC", Me.gridPeriodo)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Guardar(lblIDPeriodo.Text, txtNombrePeriodo.Text, txtFechaInicio.Text, _
                    txtFechaFin.Text, txtFechaCierre.Text)
        Borrar()

        pnlNuevo.Visible = False
        pnlConsulta.Visible = False
        lnkConsultas.Enabled = True

        CargarPeriodo()
    End Sub

    Public Function Guardar(ByVal IDPeriodo As String, ByVal NombrePeriodo As String, _
                            ByVal FechaInicio As String, ByVal FechaFin As String, _
                            ByVal FechaCierre As String) As Integer
        Dim fecha_inicio As String = ISODates.Dates.SQLServerDate(CDate(FechaInicio))
        Dim fecha_fin As String = ISODates.Dates.SQLServerDate(CDate(FechaFin))
        Dim fecha_cierre As String = ISODates.Dates.SQLServerDate(CDate(FechaCierre))

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                                               "SELECT id_periodo From Periodos " & _
                                               "WHERE id_periodo='" & IDPeriodo & "' ORDER BY id_periodo")

        If Tabla.Rows.Count = 1 Then
            BD.Execute(ConexionFluidmaster.localSqlServer, _
                       "UPDATE Periodos " & _
                       "SET nombre_periodo='" & NombrePeriodo & "', fecha_inicio='" & fecha_inicio & "', " & _
                       "fecha_fin='" & fecha_fin & "', fecha_cierre='" & fecha_cierre & "' " & _
                       "WHERE id_periodo=" & IDPeriodo & " ")

            Me.lblAviso.Text = "Los cambios se realizaron correctamente."
        Else
            BD.Execute(ConexionFluidmaster.localSqlServer, _
                       "INSERT INTO Periodos" & _
                        "(nombre_periodo, fecha_inicio, fecha_fin, fecha_cierre) " & _
                        "VALUES('" & NombrePeriodo & "','" & fecha_inicio & "'," & _
                        "'" & fecha_fin & "','" & fecha_cierre & "')")

            Me.lblAviso.Text = "Se guardo exitosamente la información."
        End If

        pnlNuevo.Visible = False
        Tabla.Dispose()

        CargarPeriodo()
    End Function

    Sub Borrar()
        lblIDPeriodo.Text = ""
        txtNombrePeriodo.Text = ""
        txtFechaInicio.Text = ""
        txtFechaFin.Text = ""
        txtFechaCierre.Text = ""
    End Sub

    Protected Sub lnkNuevo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkNuevo.Click
        pnlNuevo.Visible = True
        pnlConsulta.Visible = False
        txtNombrePeriodo.Focus()
        lblaviso.Text = ""
        Borrar()
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Borrar()

        pnlNuevo.Visible = False
        pnlConsulta.Visible = False
        lnkConsultas.Enabled = True

        CargarPeriodo()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CargarPeriodo()
        End If
    End Sub

    Protected Sub lnkConsultas_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkConsultas.Click
        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
        lblaviso.Text = ""
    End Sub

    Private Sub gridPeriodo_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gridPeriodo.RowEditing
        If gridPeriodo.Rows(e.NewEditIndex).Cells(1).Text = "" Then
            e.Cancel = True
            lblAviso.Text = "Error al intentar editar el periodo."
        Else
            lblAviso.Text = ""
            pnlNuevo.Visible = True
            pnlConsulta.Visible = False
            txtNombrePeriodo.Focus()
            VerDatos(gridPeriodo.Rows(e.NewEditIndex).Cells(1).Text)
        End If
    End Sub

    Private Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridPeriodo.Columns(1).Visible = False
    End Sub

    Sub EnviarCorreo()
        Dim CorreosPromotores(50) As String
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "select * FROM Usuarios WHERE correo<>''")
        If Tabla.Rows.Count > 1 Then
            For i = 0 To Tabla.Rows.Count - 1
                CorreosPromotores(i) = ", " & Tabla.Rows(i)("correo") & " "
            Next i

            For i = 0 To Tabla.Rows.Count - 1
                Correos = Correos + CorreosPromotores(i)
            Next i
        End If

        lblaviso.Text = "Se ha enviado correo"

        ''//Envia correo
        Dim correo As New System.Net.Mail.MailMessage()
        correo.From = New System.Net.Mail.MailAddress("soporte@procomlcd.mx")
        correo.To.Add("soporte@procomlcd.mx " + Correos)
        correo.Subject = "Alta de periodo " & txtNombrePeriodo.Text & " para captura en www.procomlcd.mx"
        correo.Body = "El portal http://procomlcd.mx/ se encuentra habilitado para la captura. <br />" & _
                "Se te recuerda que tienes hasta el lunes a las 12 de la noche para tener tu captura al 100% evita actas administrativas. <br />" & _
                "NOTA: Cualquier problema con el sistema favor de levantar tu ticket desde el portal y no descuides el correo que diste de alta en el reporte. <br /><br />" & _
                "Ahora también ya puedes tener soporte mediante Messenger de 9 am a 9 pm con el correo soportesshelby@live.com.mx de Lunes a Viernes y Sábados de 9 a 2 <br />"

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