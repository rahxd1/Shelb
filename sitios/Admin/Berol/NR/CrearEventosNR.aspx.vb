Imports System.Data.SqlClient

Partial Public Class CrearEventosNR
    Inherits System.Web.UI.Page

    Dim UsuarioSel, TiendaSel As String
    Dim Generados As Integer
    Dim DuplicaRutas As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LstProyectos(ConexionBerol.localSqlServer, Me.gridProyectos)

            Combo.LlenaDrop(ConexionBerol.localSqlServer, _
                            "SELECT * FROM NR_Periodos ORDER BY id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, _
                            "SELECT distinct id_usuario FROM NR_CatRutas ORDER BY id_usuario", "id_usuario", "id_usuario", cmbUsuario)
        End If
    End Sub

    Public Function Duplicalo(ByVal IDPeriodo As String, ByVal IDUsuario As String, ByVal IDTienda As String) As Integer
        Dim Frentes, Inventarios, Comentarios, Fotografias As Integer
        If chkFrentes.Checked = True Then
            Frentes = 0 : Else
            Frentes = 4 : End If

        If chkInventarios.Checked = True Then
            Inventarios = 0 : Else
            Inventarios = 4 : End If

        If chkComentarios.Checked = True Then
            Comentarios = 0 : Else
            Comentarios = 4 : End If

        If chkFotografias.Checked = True Then
            Fotografias = 0 : Else
            Fotografias = 4 : End If

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                               "SELECT id_periodo, id_usuario, id_tienda " & _
                                               "FROM NR_Rutas_Eventos " & _
                                               "WHERE id_periodo=" & IDPeriodo & " " & _
                                               "AND id_usuario='" & IDUsuario & "' " & _
                                               "AND id_tienda=" & IDTienda & "")

        If Tabla.Rows.Count = 1 Then
            Me.lblAviso.Text = "NO SE CARGO NINGUNA RUTA NUEVA"
        Else
            BD.Execute(ConexionBerol.localSqlServer, _
                       "INSERT INTO NR_Rutas_Eventos" & _
                       "(id_periodo, id_usuario, id_tienda, " & _
                       "estatus_frentes, estatus_inventarios, estatus_comentarios, estatus_fotos) " & _
                       "VALUES(" & IDPeriodo & ",'" & IDUsuario & "'," & IDTienda & ", " & _
                       "" & Frentes & "," & Inventarios & "," & Comentarios & "," & Fotografias & ")")

            Generados = Generados + 1
        End If

        Tabla.Dispose()
    End Function

    Protected Sub btnCargar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCargar.Click
        Generados = 0
        DuplicaRutas = "DUPLICAR"

        Dim SQL As String
        If cmbUsuario.Text = "" Then
            SQL = "SELECT * FROM NR_CatRutas ORDER BY id_usuario" : Else
            SQL = "SELECT * FROM NR_CatRutas WHERE id_usuario ='" & cmbUsuario.Text & "' ORDER BY id_usuario" : End If

        CargaGrilla(ConexionBerol.localSqlServer,SQL,Me.gridRuta)

        DuplicaRutas = ""

    End Sub

    Private Sub gridRuta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRuta.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            If DuplicaRutas = "DUPLICAR" Then
                UsuarioSel = e.Row.Cells(0).Text
                TiendaSel = e.Row.Cells(1).Text
                Duplicalo(cmbPeriodo.Text, UsuarioSel, TiendaSel)

                If Generados >= 1 Then
                    Dim Totales As String
                    Totales = Generados
                    Me.lblAviso.Visible = True
                    Me.lblAviso.Text = "SE CARGARON " + Totales + " RUTAS EXITOSAMENTE."
                End If
            End If
        End If
    End Sub

    Protected Sub cmbUsuario_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbUsuario.SelectedIndexChanged

    End Sub
End Class