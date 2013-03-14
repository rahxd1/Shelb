Imports System.Data.SqlClient

Partial Public Class CrearEventosSupNR
    Inherits System.Web.UI.Page

    Dim UsuarioSel, TiendaSel As String
    Dim Generados As Integer
    Dim DuplicaRutas As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LstProyectos(ConexionBerol.localSqlServer, Me.gridProyectos)

            Combo.LlenaDrop(ConexionBerol.localSqlServer, "SELECT * FROM NR_Periodos_Sup ORDER BY id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, "SELECT distinct id_supervisor FROM View_Usuario_NR WHERE Captura=1 ORDER BY id_supervisor", "id_supervisor", "id_supervisor", cmbUsuario)
        End If
    End Sub

    Public Function Duplicalo(ByVal IDPeriodo As String, ByVal IDUsuario As String, ByVal IDTienda As String) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                               "SELECT id_periodo, id_usuario, id_tienda " & _
                                                "FROM NR_Rutas_Eventos_Sup " & _
                                                "WHERE id_periodo=" & IDPeriodo & " " & _
                                                "AND id_usuario='" & IDUsuario & "' " & _
                                                "AND id_tienda=" & IDTienda & "")

        If Tabla.Rows.Count = 1 Then
            Me.lblAviso.Text = "NO SE CARGO NINGUNA RUTA NUEVA"
        Else
            BD.Execute(ConexionBerol.localSqlServer, _
                       "INSERT INTO NR_Rutas_Eventos_Sup" & _
                       "(id_periodo, id_usuario, id_tienda, " & _
                       "estatus_precios,estatus_fotos) " & _
                       "VALUES(" & IDPeriodo & ", '" & IDUsuario & "', " & IDTienda & ",0,0)")

            Generados = Generados + 1
        End If

        Tabla.Dispose()
    End Function

    Protected Sub btnCargar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCargar.Click
        Generados = 0
        DuplicaRutas = "DUPLICAR"

        Dim SQL As String
        If cmbUsuario.Text = "" Then
            SQL = "SELECT US.id_supervisor, RUT.id_tienda FROM NR_CatRutas as RUT " & _
                    "INNER JOIN View_Usuario_NR as US ON US.id_usuario=RUT.id_usuario " & _
                    "WHERE US.captura=1 ORDER BY US.id_supervisor" : Else
            SQL = "SELECT US.id_supervisor, RUT.id_tienda FROM NR_CatRutas as RUT " & _
                    "INNER JOIN View_Usuario_NR as US ON US.id_usuario=RUT.id_usuario " & _
                    "WHERE US.captura=1 AND US.id_supervisor ='" & cmbUsuario.Text & "' ORDER BY US.id_supervisor" : End If

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
End Class