Imports System.Data.SqlClient

Partial Public Class CrearEventosMarsConv
    Inherits System.Web.UI.Page

    Dim UsuarioSel, CadenaSel, TiendaSel As String
    Dim Suma, Generados, Generados2 As Integer
    Dim DuplicaRutas As String

    Sub PeriodoActual()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT * FROM Periodos_Nuevo " & _
                                               "WHERE fecha_inicio_periodo<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                                               "AND fecha_fin_periodo>= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "'")
        If Tabla.Rows.Count = 1 Then
            cmbPeriodo.SelectedValue = Tabla.Rows(0)("orden")
        End If

        Tabla.Dispose()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LstProyectos(ConexionMars.localSqlServer, Me.gridProyectos)

            Combo.LlenaDrop(ConexionMars.localSqlServer, "SELECT * FROM Periodos_Nuevo ORDER BY orden DESC", "nombre_periodo", "orden", cmbPeriodo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, "SELECT DISTINCT id_usuario FROM Conv_CatRutas ORDER BY id_usuario", "id_usuario", "id_usuario", cmbUsuario)

            Combo.LlenaDrop(ConexionMars.localSqlServer, _
                            "SELECT distinct id_quincena, nombre_quincena FROM Semanas ORDER BY id_quincena", _
                            "nombre_quincena", "id_quincena", cmbQuincena)

            PeriodoActual()
        End If
    End Sub

    Public Function DuplicaloPre(ByVal IDPeriodo As String, ByVal IDQuincena As String, _
                                 ByVal IDUsuario As String, ByVal IDCadena As String) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT * FROM Conv_Rutas_Eventos_Precios " & _
                                               "WHERE orden=" & IDPeriodo & " " & _
                                               "AND id_quincena='" & IDQuincena & "' " & _
                                               "AND id_usuario='" & IDUsuario & "' " & _
                                               "AND id_cadena =" & IDCadena & " ")
        If Tabla.Rows.Count = 1 Then
            Me.lblAviso.Text = "NO SE CARGO NINGUNA RUTA NUEVA"
        Else
            BD.Execute(ConexionMars.localSqlServer, _
                       "INSERT INTO Conv_Rutas_Eventos_Precios" & _
                       "(orden,id_quincena,id_usuario, id_cadena) " & _
                       "VALUES(" & IDPeriodo & ",'" & IDQuincena & "','" & IDUsuario & "'," & IDCadena & ")")

            Generados = Generados + 1
        End If
    End Function

    Public Function DuplicaloExh(ByVal IDPeriodo As String, ByVal IDQuincena As String, _
                                 ByVal IDUsuario As String, ByVal IDTienda As String) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT orden, id_usuario, id_tienda " & _
                                               "FROM Conv_Rutas_Eventos_Exhibiciones " & _
                                               "WHERE orden=" & IDPeriodo & " " & _
                                               "AND id_quincena='" & IDQuincena & "' " & _
                                               "AND id_usuario='" & IDUsuario & "' " & _
                                               "AND id_tienda=" & IDTienda & " ")
        If Tabla.Rows.Count = 1 Then
            Me.lblAviso.Text = "NO SE CARGO NINGUNA RUTA NUEVA"
        Else
            BD.Execute(ConexionMars.localSqlServer, _
                       "INSERT INTO Conv_Rutas_Eventos_Exhibiciones" & _
                       "(orden,id_quincena,id_usuario, id_tienda) " & _
                       "VALUES(" & IDPeriodo & ",'" & IDQuincena & "','" & IDUsuario & "'," & IDTienda & ")")

            Generados2 = Generados2 + 1
        End If
    End Function

    Protected Sub btnCargar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCargar.Click
        Generados = 0
        Generados2 = 0
        DuplicaRutas = "DUPLICAR"

        Dim SQLPrecios, SQLExhibiciones As String
        If cmbUsuario.Text = "" Then
            SQLExhibiciones = "SELECT * FROM Conv_CatRutas ORDER BY id_usuario"
            SQLPrecios = "SELECT DISTINCT id_cadena, id_usuario FROM Conv_CatRutas ORDER BY id_usuario" : Else
            SQLExhibiciones = "SELECT * FROM Conv_CatRutas WHERE id_usuario ='" & cmbUsuario.SelectedValue & "' ORDER BY id_usuario"
            SQLPrecios = "SELECT DISTINCT id_cadena, id_usuario FROM Conv_CatRutas WHERE id_usuario ='" & cmbUsuario.SelectedValue & "' ORDER BY id_usuario" : End If

        CargaGrilla(ConexionMars.localSqlServer, SQLPrecios, Me.gridRutaPrecios)
        CargaGrilla(ConexionMars.localSqlServer, SQLExhibiciones, Me.gridRutaExh)

        DuplicaRutas = ""

        Me.gridRutaPrecios.Visible = False
        Me.gridRutaExh.Visible = False
    End Sub

    Private Sub gridRutaPrecios_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRutaPrecios.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            If DuplicaRutas = "DUPLICAR" Then
                UsuarioSel = e.Row.Cells(0).Text
                CadenaSel = e.Row.Cells(1).Text

                DuplicaloPre(cmbPeriodo.SelectedValue, cmbQuincena.SelectedValue, UsuarioSel, CadenaSel)

                If Generados >= 1 Then
                    Dim Totales As String
                    Totales = Generados
                    Me.lblAviso.Visible = True
                    Me.lblAviso.Text = "SE CARGARON " + Totales + " RUTAS EXITOSAMENTE."
                End If
            End If
        End If
    End Sub

    Private Sub gridRutaExh_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRutaExh.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            If DuplicaRutas = "DUPLICAR" Then
                UsuarioSel = e.Row.Cells(0).Text
                TiendaSel = e.Row.Cells(1).Text

                If cmbQuincena.SelectedValue = "Q2" Then
                    DuplicaloExh(cmbPeriodo.SelectedValue, "Q2", UsuarioSel, TiendaSel)
                Else
                    Exit Sub
                End If

                If Generados2 >= 1 Then
                    Dim Totales As String
                    Totales = Generados2
                    Me.lblAviso.Visible = True
                    Me.lblAviso.Text = "SE CARGARON " + Totales + " RUTAS EXITOSAMENTE."
                End If
            End If
        End If
    End Sub
End Class