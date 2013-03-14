Imports System.Data.SqlClient

Partial Public Class CrearEventosMarsMay
    Inherits System.Web.UI.Page

    Dim UsuarioSel, TiendaSel, EjecutivoSel As String
    Dim Suma, Generados As Integer
    Dim DuplicaRutas As String

    Sub PeriodoActual()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT * FROM Periodos_Nuevo  " & _
                                               "where fecha_inicio_periodo<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                                               "AND fecha_fin_periodo>= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "'")
        If Tabla.Rows.Count = 1 Then
            cmbPeriodo.SelectedValue = Tabla.Rows(0)("orden")
        End If

        Tabla.Dispose()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LstProyectos(ConexionMars.localSqlServer, Me.gridProyectos)

            Combo.LlenaDrop(ConexionMars.localSqlServer, "SELECT * FROM Periodos_Nuevo  ORDER BY orden DESC", _
                            "nombre_periodo", "orden", cmbPeriodo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, _
                            "SELECT DISTINCT id_quincena, nombre_quincena FROM Semanas ORDER BY id_quincena", _
                            "nombre_quincena", "id_quincena", cmbQuincena)
            Combo.LlenaDrop(ConexionMars.localSqlServer, "SELECT distinct id_usuario " & _
                            "FROM Mayoreo_CatRutas ORDER BY id_usuario", _
                            "id_usuario", "id_usuario", cmbUsuario)

            PeriodoActual()
        End If
    End Sub

    Public Function Duplicalo(ByVal Orden As Integer, ByVal IDQuincena As String, _
                              ByVal IDUsuario As String, ByVal IDTienda As String) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT orden, id_usuario, id_tienda " & _
                                               "FROM Mayoreo_Rutas_Eventos " & _
                                               "WHERE orden=" & Orden & " " & _
                                               "AND id_quincena='" & IDQuincena & "' " & _
                                               "AND id_usuario='" & IDUsuario & "' " & _
                                               "AND id_tienda=" & IDTienda & " ")

        If tabla.Rows.Count = 1 Then
            Me.lblAviso.Text = "NO SE CARGO NINGUNA RUTA NUEVA"
        Else
            BD.Execute(ConexionMars.localSqlServer, _
                       "INSERT INTO Mayoreo_Rutas_Eventos" & _
                       "(orden, id_quincena, id_usuario, id_tienda) " & _
                       "VALUES(" & Orden & ", '" & IDQuincena & "', '" & IDUsuario & "', " & IDTienda & ")")

            Generados = Generados + 1
        End If

        Tabla.Dispose()
    End Function

    Protected Sub btnCargar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCargar.Click
        Generados = 0
        DuplicaRutas = "DUPLICAR"

        Dim SQL As String
        If cmbUsuario.Text = "" Then
            SQL = "SELECT * FROM Mayoreo_CatRutas ORDER BY id_usuario" : Else
            SQL = "SELECT * FROM Mayoreo_CatRutas WHERE id_usuario ='" & cmbUsuario.SelectedValue & "' " & _
                    "ORDER BY id_usuario"
        End If

        CargaGrilla(ConexionMars.localSqlServer, SQL, Me.gridRuta)

        DuplicaRutas = ""

        gridRuta.Visible = False
    End Sub

    Private Sub gridRuta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRuta.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            If DuplicaRutas = "DUPLICAR" Then
                UsuarioSel = e.Row.Cells(0).Text
                TiendaSel = e.Row.Cells(1).Text

                Duplicalo(cmbPeriodo.SelectedValue, cmbQuincena.SelectedValue, _
                          UsuarioSel, TiendaSel)

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