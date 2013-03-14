
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class AdminReporte
    Inherits System.Web.UI.Page

    Dim IDReporte As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Combo.LlenaDrop(ConexionAdmin.localSqlServer, "SELECT DISTINCT id_usuario FROM Reportes ORDER BY id_usuario", "id_usuario", "id_usuario", cmbUsuario)
            Combo.LlenaDrop(ConexionAdmin.localSqlServer, "select id_usuario FROM Usuarios as US " & _
                                                    "WHERE id_cliente=1 and no_region=0 and id_tipo<>100 and id_tipo<>5 UNION ALL " & _
                                                    "select distinct 'SISTEMAS' as id_usuario from Usuarios ORDER BY id_usuario", "id_usuario", "id_usuario", cmbDirigido)
        End If
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionAdmin.localSqlServer, _
                                                   "SELECT * From Reportes " & _
                                                   "WHERE id_reporte=" & lblIDReporte.Text & "")

        If Not lblIDReporte.Text = "" Then
            If Tabla.Rows.Count > 0 Then
                If Tabla.Rows(0)("resuelto") = 0 Then
                    Dim cnn As New SqlConnection(ConexionAdmin.localSqlServer)
                    cnn.Open()

                    Dim SQLEditar As New SqlCommand("UPDATE Reportes " & _
                            "SET resuelto = @resuelto, comentario_resuelto=@comentario_resuelto, " & _
                            " fecha_resuelto=@fecha_resuelto " & _
                            "FROM Reportes WHERE id_reporte= @id_reporte", cnn)

                    With SQLEditar
                        .Parameters.AddWithValue("@id_reporte", lblIDReporte.Text)
                        .Parameters.AddWithValue("@resuelto", cmbEstatus.SelectedValue)

                        If cmbEstatus.SelectedValue = 1 Then
                            .Parameters.AddWithValue("@fecha_resuelto", ISODates.Dates.SQLServerDate(CDate(Now))) : Else
                            .Parameters.AddWithValue("@fecha_resuelto", DBNull.Value) : End If

                        .Parameters.AddWithValue("@comentario_resuelto", txtResuelto.Text)
                        .ExecuteNonQuery()
                        .Dispose()
                    End With

                    cnn.Close()
                    cnn.Dispose()
                Else
                    BD.Execute(ConexionAdmin.localSqlServer, _
                               "UPDATE Reportes " & _
                               "SET comentario_resuelto='" & txtResuelto.Text & "' " & _
                               "FROM Reportes WHERE id_reporte=" & lblIDReporte.Text & "")
                End If
            End If
        Else
            Dim cnn As New SqlConnection(ConexionAdmin.localSqlServer)
            cnn.Open()

            Dim SQLNuevo As New SqlCommand("INSERT INTO Reportes " & _
                            "(id_usuario, nombre, correo, lada, telefono, problema, leido, " & _
                            "fecha_leido,soporte,resuelto, fecha_resuelto,comentario_resuelto, " & _
                            "dirigido) " & _
                            "VALUES(@id_usuario, @nombre, @correo, @lada, @telefono, @problema, '1', " & _
                            "@fecha_leido,@soporte,@resuelto,@fecha_resuelto,@comentario_resuelto, " & _
                            "@dirigido ) " & _
                            "SELECT @@IDENTITY AS 'id_reporte'", cnn)

            With SQLNuevo
                .Parameters.AddWithValue("@id_usuario", txtUsuario.Text)
                .Parameters.AddWithValue("@nombre", txtNombre.Text)
                .Parameters.AddWithValue("@correo", txtCorreo.Text)
                .Parameters.AddWithValue("@lada", txtLada.Text)
                .Parameters.AddWithValue("@telefono", txtTelefono.Text)
                .Parameters.AddWithValue("@problema", txtProblema.Text)
                .Parameters.AddWithValue("@fecha_leido", ISODates.Dates.SQLServerDate(CDate(Now)))
                .Parameters.AddWithValue("@soporte", HttpContext.Current.User.Identity.Name)
                .Parameters.AddWithValue("@resuelto", cmbEstatus.SelectedValue)
                .Parameters.AddWithValue("@dirigido", cmbDirigido.SelectedValue)

                If cmbEstatus.SelectedValue = 1 Then
                    .Parameters.AddWithValue("@fecha_resuelto", ISODates.Dates.SQLServerDate(CDate(Now))) : Else
                    .Parameters.AddWithValue("@fecha_resuelto", DBNull.Value) : End If

                SQLNuevo.Parameters.AddWithValue("@comentario_resuelto", txtResuelto.Text)
                Dim ID As Integer = CInt(SQLNuevo.ExecuteScalar())
                SQLNuevo.Dispose()

                cnn.Close()
                cnn.Dispose()
            End With

            lblAviso.Text = "Reporte No." & ID & "."
        End If

        Tabla.Dispose()

        Borrar()
    End Sub

    Protected Sub lnkNuevo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkNuevo.Click
        pnlConsultas.Visible = True
        pnlNuevo.Visible = True
        pnlDatos.Visible = False
        pnlConsulta.Visible = False

        lblAviso.Text = ""
        lblIDReporte.Text = ""
        lblFecha.Text = ""
        lblIDUsuario.Text = ""
        lblNombre.Text = ""
        lnkCorreo.Text = ""
        lblLada.Text = ""
        lblTelefono.Text = ""
        lblProblema.Text = ""
        lblFechaLeido.Text = ""
        lblFechaResuelto.Text = ""
        txtResuelto.Text = ""
        lnkCorreoDirigido.Text = ""
        cmbDirigido.SelectedValue = ""
    End Sub

    Sub Borrar()
        pnlConsultas.Visible = False
    End Sub

    Protected Sub lnkConsultas_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkConsultas.Click
        Dim FiltroUsuario As String
        If cmbUsuario.SelectedValue <> "" Then
            FiltroUsuario = "AND id_usuario='" & cmbUsuario.SelectedValue & "' " : Else
            FiltroUsuario = "" : End If

        CargaReportes(FiltroUsuario)
        lblAviso.Text = ""

        pnlConsulta.Visible = True
    End Sub

    Public Function CargaReportes(ByVal Filtro As String) As Integer
        Borrar()

        CargaGrilla(ConexionAdmin.localSqlServer, _
                    "SELECT id_reporte, nombre, id_usuario, fecha,fecha_leido, fecha_resuelto,soporte " & _
                    "FROM Reportes WHERE id_reporte <>0 " & _
                    " " + Filtro + " " & _
                    "ORDER BY id_reporte DESC", Me.gridConsultas)

        pnlConsulta.Visible = True
    End Function

    Protected Sub lnkAvisos1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkAvisos1.Click
        Dim FiltroUsuario As String
        lblAviso.Text = ""

        If cmbUsuario.SelectedValue <> "" Then
            FiltroUsuario = "AND id_usuario='" & cmbUsuario.SelectedValue & "' " : Else
            FiltroUsuario = "" : End If

        CargaReportes("AND leido =0" + FiltroUsuario)
    End Sub

    Protected Sub lnkAvisos2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkAvisos2.Click
        Dim FiltroUsuario As String
        lblAviso.Text = ""

        If cmbUsuario.SelectedValue <> "" Then
            FiltroUsuario = "AND id_usuario='" & cmbUsuario.SelectedValue & "' " : Else
            FiltroUsuario = "" : End If

        CargaReportes("AND leido =1 AND resuelto=0" + FiltroUsuario)
    End Sub

    Protected Sub lnkAvisos3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkAvisos3.Click
        Dim FiltroUsuario As String
        lblAviso.Text = ""

        If cmbUsuario.SelectedValue <> "" Then
            FiltroUsuario = "AND id_usuario='" & cmbUsuario.SelectedValue & "' " : Else
            FiltroUsuario = "" : End If

        CargaReportes("AND resuelto =1" + FiltroUsuario)
    End Sub

    Private Sub gridConsultas_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gridConsultas.RowEditing
        IDReporte = gridConsultas.Rows(e.NewEditIndex).Cells(1).Text

        CargaReporte()
    End Sub

    Sub CargaReporte()
        pnlConsulta.Visible = False
        pnlNuevo.Visible = False

        pnlConsultas.Visible = True
        pnlDatos.Visible = True

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionAdmin.localSqlServer, _
                                               "SELECT *, ISNULL(fecha_leido,0) as FLeido, ISNULL(fecha_resuelto,0) as FResuelto " & _
                                               "FROM Reportes WHERE id_reporte=" & IDReporte & "")
        If Tabla.Rows.Count > 0 Then
            If Tabla.Rows(0)("leido") = 0 Then
                Dim cnn As New SqlConnection(ConexionAdmin.localSqlServer)
                cnn.Open()

                Dim SQLEditar As New SqlCommand("UPDATE Reportes " & _
                            "SET fecha_leido = @fecha_leido, soporte =@soporte, leido=1 FROM Reportes " & _
                            "WHERE id_reporte= @id_reporte", cnn)

                With SQLEditar
                    .Parameters.AddWithValue("@id_reporte", IDReporte)
                    .Parameters.AddWithValue("@fecha_leido", ISODates.Dates.SQLServerDate(CDate(Now)))
                    .Parameters.AddWithValue("@soporte", HttpContext.Current.User.Identity.Name)
                    .ExecuteNonQuery()
                    .Dispose()
                End With

                cnn.Close()
                cnn.Dispose()
            End If
        End If

        Tabla.Dispose()

        Dim Tabla2 As DataTable = Tablas.Ver.DT(ConexionAdmin.localSqlServer, _
                                               "SELECT *, ISNULL(fecha_leido,0) as FLeido, ISNULL(fecha_resuelto,0) as FResuelto " & _
                                               "FROM Reportes WHERE id_reporte=" & IDReporte & "")

        If Tabla2.Rows.Count > 0 Then
            lblIDReporte.Text = Tabla2.Rows(0)("id_reporte")
            lblFecha.Text = Tabla2.Rows(0)("fecha")
            lblIDUsuario.Text = Tabla2.Rows(0)("id_usuario")
            lblNombre.Text = Tabla2.Rows(0)("nombre")
            lnkCorreo.Text = Tabla2.Rows(0)("correo")
            lblLada.Text = Tabla2.Rows(0)("lada")
            lblTelefono.Text = Tabla2.Rows(0)("telefono")
            lblProblema.Text = Tabla2.Rows(0)("problema")
            lblFechaLeido.Text = Tabla2.Rows(0)("FLeido")
            lblFechaResuelto.Text = Tabla2.Rows(0)("FResuelto")
            txtResuelto.Text = Tabla2.Rows(0)("Comentario_resuelto")
            cmbEstatus.SelectedValue = Tabla2.Rows(0)("resuelto")
            cmbDirigido.SelectedValue = Tabla2.Rows(0)("dirigido")
        End If

        Tabla2.Dispose()
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Borrar()
    End Sub

    Private Sub cmbUsuario_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbUsuario.SelectedIndexChanged
        lblAviso.Text = ""
        CargaReportes("AND id_usuario='" & cmbUsuario.SelectedValue & "'")
    End Sub

    Protected Sub cmbDirigido_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbDirigido.SelectedIndexChanged
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionAdmin.localSqlServer, _
                                                    "SELECT * FROM Usuarios WHERE id_usuario='" & cmbDirigido.SelectedValue & "'")

        If Tabla.Rows.Count > 0 Then
            lnkCorreoDirigido.Text = Tabla.Rows(0)("correo")
        End If

        Tabla.Dispose()
    End Sub

    Protected Sub lnkCorreoDirigido_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkCorreoDirigido.Click
        Dim correo As New System.Net.Mail.MailMessage()
        correo.From = New System.Net.Mail.MailAddress("soporte@procomlcd.mx")
        correo.To.Add(lnkCorreoDirigido.Text)
        correo.Subject = "Reporte No. " & lblIDReporte.Text & " www.procomlcd.mx"
        correo.Body = "Usuario:" & HttpContext.Current.User.Identity.Name & " <br />" & _
                    "Nombre:" & txtNombre.Text & "  <br />" & _
                    "Teléfono:(" & txtLada.Text & ")-" & txtTelefono.Text & " <br />" & _
                    "Problema: " & lblProblema.Text & " <br />"

        correo.IsBodyHtml = True
        correo.Priority = System.Net.Mail.MailPriority.Normal

        Dim smtp As New System.Net.Mail.SmtpClient
        smtp.Host = "mail.procomlcd.mx"
        smtp.Port = "587"
        smtp.Credentials = New System.Net.NetworkCredential("soporte@procomlcd.mx", "sopo1597")
        smtp.EnableSsl = False

        Try
            smtp.Send(correo)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        lblAviso.Text = "Se ha enviado correo"
    End Sub
End Class