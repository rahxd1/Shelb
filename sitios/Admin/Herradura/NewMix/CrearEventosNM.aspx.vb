Imports System.Data.SqlClient

Partial Public Class CrearEventosNM
    Inherits System.Web.UI.Page

    Dim UsuarioSel, TiendaSel, CadenaSel As String
    Dim Generados, GeneradosCad As Integer
    Dim DuplicaRutas As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LstProyectos(ConexionHerradura.localSqlServer, Me.gridProyectos)

            Combo.LlenaDrop(ConexionHerradura.localSqlServer, "SELECT * FROM NewMix_Periodos ORDER BY id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, "SELECT distinct id_usuario FROM NewMix_CatRutas ORDER BY id_usuario", "id_usuario", "id_usuario", cmbUsuario)
        End If
    End Sub

    Public Function Duplicalo(ByVal IDPeriodo As Integer, ByVal IDUsuario As String, _
                              ByVal IDTienda As Integer) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionHerradura.localSqlServer, _
                                               "SELECT id_periodo, id_usuario, id_tienda " & _
                                               "FROM NewMix_Rutas_Eventos " & _
                                               "WHERE id_periodo=" & IDPeriodo & " " & _
                                               "AND id_usuario='" & IDUsuario & "' " & _
                                               "AND id_tienda=" & IDTienda & " ")

        If Tabla.Rows.Count = 1 Then
            Me.lblAviso.Text = "NO SE CARGO NINGUNA RUTA NUEVA"
        Else
            BD.Execute(ConexionHerradura.localSqlServer, _
                       "INSERT INTO NewMix_Rutas_Eventos" & _
                       "(id_periodo, id_usuario, id_tienda) " & _
                       "VALUES(" & IDPeriodo & ", '" & IDUsuario & "', " & IDTienda & ")")

            Generados = Generados + 1
        End If

        Tabla.Dispose()
    End Function

    Public Function DuplicaloCadenas(ByVal IDPeriodo As Integer, ByVal IDUsuario As String, _
                                     ByVal IDCadena As Integer) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionHerradura.localSqlServer, _
                                               "SELECT id_periodo, id_usuario, id_cadena " & _
                                               "FROM NewMix_Rutas_Eventos_Cadenas " & _
                                               "WHERE id_periodo=" & IDPeriodo & " " & _
                                               "AND id_usuario='" & IDUsuario & "' " & _
                                               "AND id_cadena=" & IDCadena & " ")

        If Tabla.Rows.Count = 1 Then
            Me.lblAviso.Text = "NO SE CARGO NINGUNA RUTA NUEVA"
        Else
            BD.Execute(ConexionHerradura.localSqlServer, _
                       "INSERT INTO NewMix_Rutas_Eventos_Cadenas" & _
                       "(id_periodo, id_usuario, id_cadena) " & _
                       "VALUES(" & IDPeriodo & ", '" & IDUsuario & "', " & IDCadena & ")")
        End If
    End Function

    Protected Sub btnCargar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCargar.Click
        Generados = 0
        GeneradosCad = 0
        DuplicaRutas = "DUPLICAR"

        Dim SQL, SQLCad As String

        ''//tiendas
        If cmbUsuario.Text = "" Then
            SQL = "SELECT * FROM NewMix_CatRutas as RUT " & _
                    "ORDER BY id_usuario"
        Else
            SQL = "SELECT * FROM NewMix_CatRutas as RUT " & _
                    "WHERE id_usuario ='" & cmbUsuario.Text & "' " & _
                    "ORDER BY id_usuario"
        End If

        CargaGrilla(ConexionHerradura.localSqlServer, SQL, Me.gridRuta)
        Me.gridRuta.Visible = False

        ''Cadenas
        If cmbUsuario.Text = "" Then
            SQLCad = "SELECT DISTINCT id_usuario, id_cadena " & _
                    "FROM NewMix_CatRutas as RUT " & _
                    "INNER JOIN NewMix_Tiendas as TI ON RUT.id_tienda=TI.id_tienda " & _
                    "ORDER BY id_usuario"
        Else
            SQLCad = "SELECT DISTINCT id_usuario, id_cadena " & _
                    "FROM NewMix_CatRutas as RUT " & _
                    "INNER JOIN NewMix_Tiendas as TI ON RUT.id_tienda=TI.id_tienda " & _
                    "WHERE id_usuario ='" & cmbUsuario.Text & "' " & _
                    "ORDER BY id_usuario"
        End If
        CargaGrilla(ConexionHerradura.localSqlServer, SQLCad, Me.gridRutaCadenas)
        Me.gridRutaCadenas.Visible = False

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

    Private Sub gridRutaCadenas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRutaCadenas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            If DuplicaRutas = "DUPLICAR" Then
                UsuarioSel = e.Row.Cells(0).Text
                CadenaSel = e.Row.Cells(1).Text
                DuplicaloCadenas(cmbPeriodo.Text, UsuarioSel, CadenaSel)
            End If
        End If
    End Sub
End Class