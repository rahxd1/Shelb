Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class AdminAvisoJovy
    Inherits System.Web.UI.Page

    Dim IDAviso As Integer
    Dim Nombre, Region As String

    Private Sub VerDatos()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                               "SELECT * FROM Avisos WHERE id_aviso=" & IDAviso & "")
        If Tabla.Rows.Count > 0 Then
            txtTitulo.Text = Tabla.Rows(0)("nombre_aviso")
            txtDescripcion.Text = Tabla.Rows(0)("descripcion")
            txtFechaInicio.Text = Format(Tabla.Rows(0)("fecha_inicio"), "dd/MM/yyyy")
            txtFechaFin.Text = Format(Tabla.Rows(0)("fecha_fin"), "dd/MM/yyyy")
            ImagenAviso.ImageUrl = "/ARCHIVOS/CLIENTES/JOVY/AVISOS/" & Tabla.Rows(0)("id_aviso") & ".jpg"
        End If

        Tabla.Dispose()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CargaGrilla(ConexionJovy.localSqlServer, "select * from Avisos ORDER BY fecha_inicio", Me.gridAvisos)
        End If
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Dim fecha_inicio As String = ISODates.Dates.SQLServerDate(CDate(txtFechaInicio.Text))
        Dim fecha_fin As String = ISODates.Dates.SQLServerDate(CDate(txtFechaFin.Text))

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                               "SELECT id_aviso From Avisos " & _
                                               "WHERE id_aviso=" & lblIDAviso.Text & "")
        If Tabla.Rows.Count = 1 Then
            BD.Execute(ConexionJovy.localSqlServer, _
                       "UPDATE Avisos " & _
                        "SET nombre_aviso='" & txtTitulo.Text & "',descripcion='" & txtDescripcion.Text & "', " & _
                        "fecha_inicio='" & fecha_inicio & "',fecha_fin='" & fecha_fin & "'  " & _
                        "FROM Avisos WHERE id_aviso=" & lblIDAviso.Text & "")

            IDAviso = lblIDAviso.Text
        Else
            IDAviso = BD.RT.Execute(ConexionJovy.localSqlServer, _
                       "INSERT INTO Avisos" & _
                       "(nombre_aviso, descripcion, fecha_inicio, fecha_fin) " & _
                       "VALUES('" & txtTitulo.Text & "','" & txtDescripcion.Text & "'," & _
                       "'" & fecha_inicio & "','" & fecha_fin & "') SELECT @@IDENTITY AS 'id_aviso'")
        End If

        Dim fileExt As String
        fileExt = System.IO.Path.GetExtension(FileUpload1.FileName)

        If (fileExt = ".exe") Then
            lblSubida.Text = "No puedes subir archivos .exe"
        Else
            If FileUpload1.HasFile = True Then
                Dim path As String = Server.MapPath("~/ARCHIVOS/CLIENTES/JOVY/AVISOS/")
                FileUpload1.PostedFile.SaveAs(path & IDAviso & ".jpg")
                lblSubida.Text = "El archivo fue subido correctamente"
            End If
        End If

        pnlAvisos.Visible = False
        gridAvisos.Visible = True

        CargaGrilla(ConexionJovy.localSqlServer, "select * from Avisos ORDER BY fecha_inicio", Me.gridAvisos)

        Borrar()
    End Sub

    Private Sub gridAvisos_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridAvisos.RowDeleting
        IDAviso = gridAvisos.Rows(e.RowIndex).Cells(2).Text

        BD.Execute(ConexionJovy.localSqlServer, _
                   "DELETE FROM Avisos WHERE id_aviso = '" & IDAviso & "'")

        CargaGrilla(ConexionJovy.localSqlServer, "select * from Avisos ORDER BY fecha_inicio", Me.gridAvisos)
    End Sub

    Private Sub gridUsuarios_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gridAvisos.RowEditing
        IDAviso = gridAvisos.Rows(e.NewEditIndex).Cells(2).Text
        lblIDAviso.Text = IDAviso
        pnlAvisos.Visible = True
        gridAvisos.Visible = False

        VerDatos()
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        pnlAvisos.Visible = False
        gridAvisos.Visible = True

        Borrar()
    End Sub

    Protected Sub linkNuevo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles linkNuevo.Click
        pnlAvisos.Visible = True
        gridAvisos.Visible = False

        Borrar()
    End Sub

    Sub Borrar()
        txtTitulo.Text = ""
        txtDescripcion.Text = ""
        txtFechaInicio.Text = ""
        txtFechaFin.Text = ""
        ImagenAviso.ImageUrl = ""
    End Sub

    Protected Sub LinkConsultas_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LinkConsultas.Click
        pnlAvisos.Visible = False
        gridAvisos.Visible = True

        Borrar()
    End Sub

    Private Sub CrearAvisoCapacitacionJovy_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridAvisos.Columns(2).Visible = False
    End Sub

End Class