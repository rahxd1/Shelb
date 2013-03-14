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

Partial Public Class FormatoCapturaComentariosJovy
    Inherits System.Web.UI.Page

    Dim IDTienda, IDUsuario, IDPeriodo, IDRegion, FolioAct As String
    Dim Faltante As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionJovy.localSqlServer, HttpContext.Current.User.Identity.Name)

            VerDatos()

            If Edicion = 1 Then
                btnGuardar.Visible = True : Else
                btnGuardar.Visible = False : End If
        End If
    End Sub

    Private Sub VerDatos()
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                               "select * from Jovy_Historial " & _
                                               "WHERE folio_historial= " & FolioAct & "")
        If Tabla.Rows.Count > 0 Then
            txtComentarios.Text = Tabla.Rows(0)("comentarios")
            txtComentariosCompetencia.Text = Tabla.Rows(0)("comentarios_competencia")
        End If
        Tabla.Dispose()
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        btnGuardar.Enabled = False

        Guardar()
    End Sub

    Sub Guardar()
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                               "select * from Jovy_Historial " & _
                                               "WHERE folio_historial=" & FolioAct & "")
        If Tabla.Rows.Count > 0 Then
            BD.Execute(ConexionJovy.localSqlServer, _
                       "UPDATE Jovy_Historial SET comentarios='" & txtComentarios.Text & "', " & _
                       "comentarios_competencia='" & txtComentariosCompetencia.Text & "' " & _
                       "WHERE folio_historial=" & FolioAct & " ")
        End If

        Tabla.Dispose()

        CambioEstatus(FolioAct)
        Response.Redirect("RutasJovy.aspx")
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("javascript:history.go(-1)")
    End Sub

    Private Function CambioEstatus(ByVal folio As Integer) As Boolean
        Datos()

        BD.Execute(ConexionJovy.localSqlServer, _
                   "UPDATE Jovy_Rutas_Eventos SET estatus_comentarios=1 " & _
                   "FROM Jovy_Rutas_Eventos " & _
                   "WHERE id_periodo=" & IDPeriodo & " AND id_usuario='" & IDUsuario & "' " & _
                   "AND id_tienda=" & IDTienda & "")
    End Function

    Sub Datos()
        FolioAct = Request.Params("folio")
        IDUsuario = Request.Params("usuario")
        IDPeriodo = Request.Params("periodo")
        IDTienda = Request.Params("tienda")

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                               "select H.id_tienda,TI.nombre, TI.nombre_cadena " & _
                                  "from Jovy_Historial as H " & _
                                  "INNER JOIN View_Jovy_Tiendas as TI ON TI.id_tienda=H.id_tienda " & _
                                  "WHERE H.folio_historial=" & FolioAct & " ")
        If tabla.Rows.Count > 0 Then
            lblTienda.Text = tabla.Rows(0)("nombre")
            lblCadena.Text = tabla.Rows(0)("nombre_cadena")
        End If

        Tabla.Dispose()
    End Sub

End Class