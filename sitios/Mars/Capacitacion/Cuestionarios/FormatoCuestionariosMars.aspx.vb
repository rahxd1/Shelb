Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports ISODates.Dates
Imports System.Security.Cryptography

Partial Public Class FormatoCuestionariosMars
    Inherits System.Web.UI.Page

    Dim Orden, IDUsuario, IDCuestionario As String
    Dim FolioAct, CantPreguntas, Oportunidad As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionMars.localSqlServer, HttpContext.Current.User.Identity.Name)

            VerDatos()
            lblCuestionario.Text = Request.Params("nombre")

            If Request.Params("Completado") = 0 Then
                btnGuardar.Visible = True
                pnlCompletado.Visible = False
            Else
                btnGuardar.Visible = False
                btnAnterior.Enabled = False
                btnSiguiente.Enabled = False
                pnlCompletado.Visible = True : End If

            If CantPreguntas = 1 Then
                btnAnterior.Visible = False
                btnSiguiente.Visible = False
            Else
                btnAnterior.Visible = True
                btnSiguiente.Visible = True
            End If
        End If
    End Sub

    Private Sub VerDatos()
        Datos()

        Preguntas(FolioAct, IDCuestionario, lblIDSeccion.Text)
    End Sub

    Private Function Preguntas(ByVal Folio As Integer, ByVal Cuestionario As Integer, _
                               ByVal Seccion As Integer) As Boolean
        CargaGrilla(ConexionMars.localSqlServer, _
                    "execute Ver_Cuestionario " & Folio & ", " & Cuestionario & ", " & Seccion & "", gridRespuestas)

        NombreSeccion(Cuestionario, Seccion)

    End Function

    Private Function NombreSeccion(ByVal Cuestionario As Integer, ByVal Seccion As Integer) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * from Cap_Cuestionarios_Seccion_Preguntas as PR " & _
                                  "inner join Cap_Cuestionarios as CT ON CT.id_cuestionario=PR.id_cuestionario " & _
                                  "WHERE CT.id_cuestionario =" & Cuestionario & " AND PR.id_seccion=" & Seccion & "")

        Dim PreguntaAbierta As Integer
        If Tabla.Rows.Count > 0 Then
            lblSeccionPregunta.Text = Tabla.Rows(0)("seccion")
            PreguntaAbierta = Tabla.Rows(0)("pregunta")
            lblComentarios.Text = Tabla.Rows(0)("pregunta_abierta")
        End If

        If PreguntaAbierta = 1 Then
            lblComentarios.Visible = True
            txtComentarios.Visible = True
        Else
            lblComentarios.Visible = False
            txtComentarios.Visible = False
        End If

        Tabla.Dispose()
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        btnGuardar.Enabled = False

        Guardar()

        Me.Dispose()
        Response.Redirect("CuestionariosMars.aspx")
    End Sub

    Sub Guardar()
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * from Cap_Cuestionarios_Historial " & _
                        "WHERE orden =" & Orden & " AND oportunidad =" & Oportunidad & " " & _
                        "AND id_usuario='" & IDUsuario & "' AND id_cuestionario=" & IDCuestionario & " ")
        If Tabla.Rows.Count = 0 Then
            lblFolioAct.Text = BD.RT.Execute(ConexionMars.localSqlServer, _
                       "execute Cap_CrearHistorial_Cuestionario " & _
                       "" & Orden & "," & Oportunidad & ",'" & IDUsuario & "'," & _
                       "" & IDCuestionario & ",'" & txtComentarios.Text & "'")
        Else
            BD.RT.Execute(ConexionMars.localSqlServer, _
                          "execute Cap_EditarHistorial_Cuestionario " & _
                          "" & FolioAct & ",'" & txtComentarios.Text & "'")
        End If

        GuardaGrilla(FolioAct, lblIDSeccion.Text) ''//¿Pregunta actual?
        CambioEstatus(FolioAct)

        Tabla.Dispose()
    End Sub

    Private Function GuardaGrilla(ByVal Folio_Historial As Integer, ByVal IDSeccion As Integer) As Boolean
        For i = 0 To gridRespuestas.Rows.Count - 1
            Dim IDPregunta As Integer = gridRespuestas.DataKeys(i).Value.ToString()
            Dim txtRespuesta As TextBox = CType(gridRespuestas.Rows(i).FindControl("txtRespuesta"), TextBox)

            GuardaRespuestas(Folio_Historial, IDSeccion, IDPregunta, txtRespuesta.Text)
        Next
    End Function

    Private Function GuardaRespuestas(ByVal Folio_Historial As Integer, ByVal IDSeccion As Integer, _
                                      ByVal IDPregunta As Integer, ByVal Respuesta As String) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * from Cap_Cuestionarios_Historial_Det " & _
                                  "WHERE folio_historial =" & Folio_Historial & " " & _
                                  "AND id_seccion=" & IDSeccion & " " & _
                                  "AND id_pregunta=" & IDPregunta & "")
        If Respuesta <> "" Then
            If Tabla.Rows.Count > 0 Then
                BD.Execute(ConexionMars.localSqlServer, _
                           "execute Cap_EditarHistorial_Cuestionario_Det " & _
                           "" & Folio_Historial & "," & IDSeccion & ", " & _
                           "" & IDPregunta & "," & Respuesta & "")
            Else
                BD.Execute(ConexionMars.localSqlServer, _
                           "execute Cap_CrearHistorial_Cuestionario_Det " & _
                           "" & Folio_Historial & "," & IDSeccion & ", " & _
                           "" & IDPregunta & "," & Respuesta & "")
            End If
        Else
            If Tabla.Rows.Count > 0 Then
                BD.Execute(ConexionMars.localSqlServer, _
                           "DELETE FROM Cap_Cuestionarios_Historial_Det " & _
                           "WHERE folio_historial =" & Folio_Historial & " " & _
                           "AND id_seccion=" & IDSeccion & " " & _
                           "AND id_pregunta=" & IDPregunta & "")
            End If
        End If

        Tabla.Dispose()
    End Function

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("CuestionariosMars.aspx")
    End Sub

    Private Function CambioEstatus(ByVal folio As Integer) As Boolean
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * from Cap_Cuestionarios_Historial " & _
                                               "WHERE folio_historial =" & folio & "")
        Dim Estatus As Integer
        If Tabla.Rows.Count > 0 Then
            Estatus = 1 : Else
            Estatus = 2 : End If

        BD.Execute(ConexionMars.localSqlServer, _
                   "UPDATE Cap_Cuestionario_Eventos SET estatus=" & Estatus & " " & _
                   "FROM Cap_Cuestionario_Eventos " & _
                   "WHERE orden=" & Orden & " AND id_usuario='" & IDUsuario & "' " & _
                   "AND id_cuestionario=" & IDCuestionario & "")
        Tabla.Dispose()
    End Function

    Sub Datos()
        Orden = Request.Params("orden")
        IDUsuario = Request.Params("usuario")
        IDCuestionario = Request.Params("cuestionario")
        FolioAct = Request.Params("folio")
        CantPreguntas = Request.Params("preguntas")
        Oportunidad = Request.Params("oportunidad")

        If lblFolioAct.Text <> 0 Then
            FolioAct = lblFolioAct.Text
        End If
    End Sub

    Private Sub btnSiguiente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSiguiente.Click
        Datos()
        Guardar()

        If CantPreguntas = lblIDSeccion.Text Then
            btnSiguiente.Enabled = False
        Else
            lblIDSeccion.Text = lblIDSeccion.Text + 1
            Preguntas(FolioAct, IDCuestionario, lblIDSeccion.Text)

            btnAnterior.Enabled = True

            If lblIDSeccion.Text = CantPreguntas Then
                btnSiguiente.Enabled = False
            End If
        End If
    End Sub

    Protected Sub btnAnterior_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAnterior.Click
        Datos()
        Guardar()

        If lblIDSeccion.Text <> 1 Then
            lblIDSeccion.Text = lblIDSeccion.Text - 1
            Preguntas(FolioAct, IDCuestionario, lblIDSeccion.Text)

            btnAnterior.Enabled = True
            btnSiguiente.Enabled = True
        Else
            btnAnterior.Enabled = False
        End If
    End Sub

    Private Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridRespuestas.Columns(0).Visible = False
    End Sub

    Private Sub gridRespuestas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRespuestas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(0).Text = 1 Then
                gridRespuestas.Columns(3).Visible = True
            Else
                gridRespuestas.Columns(3).Visible = False
            End If
        End If
    End Sub

    Private Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("CuestionariosMars.aspx")
    End Sub
End Class