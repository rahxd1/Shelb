Imports System.Data.SqlClient

Partial Public Class CrearEventosJovy
    Inherits System.Web.UI.Page

    Dim UsuarioSel, CadenaSel, TiendaSel, DuplicaRutas As String
    Dim Generados As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Combo.LlenaDrop(ConexionJovy.localSqlServer, "SELECT * FROM Jovy_Periodos ORDER BY id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionJovy.localSqlServer, "SELECT DISTINCT id_usuario FROM Jovy_CatRutas", "id_usuario", "id_usuario", cmbdemo)
        End If
    End Sub

    Public Function Duplicalo(ByVal IDPeriodo As String, ByVal IDUsuario As String, _
                              ByVal IDCadena As String, ByVal IDTienda As Integer) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                               "SELECT * FROM Jovy_Rutas_Eventos " & _
                                                 "WHERE id_periodo=" & IDPeriodo & " " & _
                                                 "AND id_usuario='" & IDUsuario & "' " & _
                                                 "AND id_tienda=" & IDTienda & " " & _
                                                 "AND id_cadena=" & IDCadena & " ")

        If Tabla.Rows.Count = 1 Then
            Me.lblaviso.Text = "NO SE CARGO NINGUNA RUTA NUEVA"
            Exit Function
        Else
            BD.Execute(ConexionJovy.localSqlServer, _
                       "INSERT INTO Jovy_Rutas_Eventos" & _
                       "(id_periodo, id_usuario, id_cadena, id_tienda) " & _
                       "VALUES(" & IDPeriodo & ",'" & IDUsuario & "'," & IDCadena & "," & IDTienda & ")")

            Generados = Generados + 1
        End If

        Tabla.Dispose()
    End Function

    Protected Sub btnCargar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCargar.Click
        Generados = 0
        DuplicaRutas = "DUPLICAR"

        Dim SQL As String
        If cmbdemo.Text = "" Then
            SQL = "SELECT * FROM Jovy_CatRutas ORDER BY id_usuario"
        Else
            SQL = "SELECT * FROM Jovy_CatRutas WHERE id_usuario= '" & cmbdemo.SelectedValue & "'"
        End If

        CargaGrilla(ConexionJovy.localSqlServer,SQL,Me.gridRuta)

        DuplicaRutas = ""

    End Sub

    Private Sub gridRuta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRuta.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            If DuplicaRutas = "DUPLICAR" Then
                UsuarioSel = e.Row.Cells(0).Text
                CadenaSel = e.Row.Cells(1).Text
                TiendaSel = e.Row.Cells(2).Text

                Duplicalo(cmbPeriodo.Text, UsuarioSel, CadenaSel, TiendaSel)

                If Generados >= 1 Then
                    Dim Totales As String
                    Totales = Generados
                    Me.lblaviso.Text = "SE CARGARON " + Totales + " RUTAS DE DEMOSTRADORAS EXITOSAMENTE."
                End If
            End If
        End If
    End Sub

End Class