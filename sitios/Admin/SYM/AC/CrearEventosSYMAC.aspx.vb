Imports System.Data.SqlClient

Partial Public Class CrearEventosSYMAC
    Inherits System.Web.UI.Page

    Dim UsuarioSel, TiendaSel As String
    Dim Suma, Generados As Integer
    Dim DuplicaRutas As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LstProyectos(ConexionSYM.localSqlServer, Me.gridProyectos)

            Combo.LlenaDrop(ConexionSYM.localSqlServer, "SELECT * FROM AC_Periodos ORDER BY id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, "SELECT distinct id_usuario FROM AC_CatRutas ORDER BY id_usuario", "id_usuario", "id_usuario", cmbUsuario)
        End If
    End Sub

    Public Function Duplicalo(ByVal IDPeriodo As String, ByVal IDUsuario As String, _
                              ByVal IDTienda As Integer) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT id_periodo, id_usuario, id_tienda " & _
                                               "FROM AC_Rutas_Eventos " & _
                                               "WHERE id_periodo=" & IDPeriodo & " " & _
                                               "AND id_usuario='" & IDUsuario & "' " & _
                                               "AND id_tienda=" & IDTienda & " ")
        If Tabla.Rows.Count = 1 Then
            Me.lblAviso.Text = "NO SE CARGO NINGUNA RUTA NUEVA"
            Exit Function
        Else
            BD.Execute(ConexionSYM.localSqlServer, _
                       "INSERT INTO AC_Rutas_Eventos" & _
                       "(id_periodo, id_usuario, id_tienda) " & _
                       "VALUES(" & IDPeriodo & ",'" & IDUsuario & "'," & IDTienda & ")")

            Generados = Generados + 1
        End If

        Tabla.Dispose()
    End Function

    Protected Sub btnCargar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCargar.Click
        Generados = 0
        DuplicaRutas = "DUPLICAR"

        Dim SQL As String
        If cmbUsuario.Text = "" Then
            SQL = "SELECT * FROM AC_CatRutas ORDER BY id_usuario"
        Else
            SQL = "SELECT * FROM AC_CatRutas WHERE id_usuario ='" & cmbUsuario.Text & "' ORDER BY id_usuario"
        End If

        CargaGrilla(ConexionSYM.localSqlServer,SQL,Me.gridRuta)

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
                    Me.lblAviso.Text = "SE CARGARON " + Totales + " RUTA(S) EXITOSAMENTE."
                End If
            End If
        End If
    End Sub
End Class