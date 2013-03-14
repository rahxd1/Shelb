Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class AdminAvisoCapacitacionMars
    Inherits System.Web.UI.Page

    Dim IDAviso As Integer
    Dim Nombre, Region As String

    Private Sub VerDatos()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT * FROM Avisos WHERE id_aviso='" & IDAviso & "'")
        If Tabla.Rows.Count > 0 Then
            txtTitulo.Text = Tabla.Rows(0)("titulo_aviso")
            txtDescripcion.Text = Tabla.Rows(0)("descripcion")
            txtFechaInicio.Text = Format(Tabla.Rows(0)("fecha_inicio"), "dd/MM/yyyy")
            txtFechaFin.Text = Format(Tabla.Rows(0)("fecha_fin"), "dd/MM/yyyy")
            Dim Dirigido As String = Tabla.Rows(0)("dirigido_a")
            Select Case Dirigido
                Case Dirigido
                    cmbDirigido.Text = Dirigido
            End Select
        End If

        Tabla.Dispose()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CargaGrid()
        End If
    End Sub

    Sub CargaGrid()
        CargaGrilla(ConexionMars.localSqlServer, "select * from Avisos ORDER BY fecha_inicio", _
                    Me.gridAvisos)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Dim fecha_inicio As String = ISODates.Dates.SQLServerDate(CDate(txtFechaInicio.Text))
        Dim fecha_fin As String = ISODates.Dates.SQLServerDate(CDate(txtFechaFin.Text))

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT id_aviso From Avisos WHERE id_aviso='" & lblIDAviso.Text & "'")
        If Tabla.Rows.Count = 1 Then
            BD.Execute(ConexionMars.localSqlServer, _
                       "UPDATE Avisos SET titulo_aviso='" & txtTitulo.Text & "', " & _
                        "descripcion='" & txtDescripcion.Text & "', " & _
                        "fecha_inicio='" & fecha_inicio & "',  " & _
                        "fecha_fin='" & fecha_fin & ",  " & _
                        "dirigido_a='" & cmbDirigido.Text & "'  " & _
                        "FROM Avisos " & _
                        "WHERE id_aviso=" & lblIDAviso.Text & "")
        Else
            BD.Execute(ConexionMars.localSqlServer, _
                       "INSERT INTO Avisos" & _
                       "(titulo_aviso, descripcion, fecha_inicio, fecha_fin, dirigido_a) " & _
                       "VALUES('" & txtTitulo.Text & "','" & txtDescripcion.Text & "', " & _
                       "'" & fecha_inicio & "', '" & fecha_fin & "','" & cmbDirigido.Text & "')")
        End If

        Tabla.Dispose()

        pnlAvisos.Visible = False
        gridAvisos.Visible = True
        CargaGrid()
    End Sub

    Private Sub gridAvisos_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridAvisos.RowDeleting
        IDAviso = gridAvisos.Rows(e.RowIndex).Cells(2).Text

        BD.Execute(ConexionMars.localSqlServer, _
                   "DELETE FROM Avisos WHERE id_aviso = '" & IDAviso & "'")

        CargaGrid()
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
    End Sub

    Protected Sub linkNuevo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles linkNuevo.Click
        pnlAvisos.Visible = True
        gridAvisos.Visible = False

        txtTitulo.Text = ""
        txtDescripcion.Text = ""
        txtFechaInicio.Text = ""
        txtFechaFin.Text = ""
        cmbDirigido.Text = "TODOS"
    End Sub

    Protected Sub LinkConsultas_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LinkConsultas.Click
        pnlAvisos.Visible = False
        gridAvisos.Visible = True
    End Sub

    Private Sub CrearAvisoCapacitacionSaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridAvisos.Columns(2).Visible = False
    End Sub

    Protected Sub gridAvisos_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gridAvisos.SelectedIndexChanged

    End Sub
End Class