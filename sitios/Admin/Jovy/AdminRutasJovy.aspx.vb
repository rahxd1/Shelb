Imports System.Data.SqlClient

Partial Public Class AdminRutasJovy
    Inherits System.Web.UI.Page

    Dim SQLCadena, SQLEstado, SQLTienda As String
    Dim SelCadena, SelEstado As String

    Sub CargaSQL()
        SelCadena = Acciones.Slc.cmb("id_cadena", ListCadena.SelectedValue)
        SelEstado = Acciones.Slc.cmb("id_estado", ListEstado.SelectedValue)

        SQLEstado = "SELECT DISTINCT id_estado, nombre_estado " & _
                    "FROM View_Jovy_Tiendas " & _
                    "WHERE id_tienda NOT IN(SELECT id_tienda FROM Jovy_CatRutas) "

        SQLCadena = "SELECT distinct nombre_cadena, id_cadena " & _
                    "FROM View_Jovy_Tiendas " & _
                    "WHERE id_tienda NOT IN(SELECT id_tienda FROM Jovy_CatRutas) " & _
                    " " + SelEstado + " " & _
                    "ORDER BY nombre_cadena"

        SQLTienda = "SELECT id_tienda, nombre " & _
                    "FROM View_Jovy_Tiendas " & _
                    "WHERE id_tienda NOT IN(SELECT id_tienda FROM Jovy_CatRutas) " & _
                    " " + SelCadena + SelEstado + " " & _
                    "ORDER BY nombre"

        CargarRuta()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CargaSQL()

            Lista.LlenaBox(ConexionJovy.localSqlServer, SQLCadena, "nombre_cadena", "id_cadena", ListCadena)
            Lista.LlenaBox(ConexionJovy.localSqlServer, SQLEstado, "nombre_estado", "id_estado", ListEstado)
            Lista.LlenaBox(ConexionJovy.localSqlServer, SQLTienda, "nombre", "id_tienda", ListTienda)

            Combo.LlenaDrop(ConexionJovy.localSqlServer, "SELECT distinct id_usuario FROM Usuarios WHERE id_tipo=1", "id_usuario", "id_usuario", cmbPromotor)
        End If
    End Sub

    Sub CargarRuta()
        CargaGrilla(ConexionJovy.localSqlServer, _
                    "SELECT DISTINCT RUT.id_usuario,RUT.id_cadena,CAD.nombre_cadena," & _
                    "RUT.id_tienda,ISNULL(TI.nombre ,'')nombre " & _
                    "FROM Jovy_CatRutas AS RUT " & _
                    "INNER JOIN Cadenas_Tiendas AS CAD ON CAD.id_cadena = RUT.id_cadena " & _
                    "FULL JOIN View_Jovy_Tiendas AS TI ON RUT.id_tienda = TI.id_tienda " & _
                    "WHERE RUT.id_usuario = '" & cmbPromotor.SelectedValue & "' " & _
                    "order by CAD.nombre_cadena", Me.gridRuta)
    End Sub

    Sub Cargar()
        CargarRuta()
        pnlRuta.Visible = True
        pnlAgregar.Visible = False
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAgregar.Click
        pnlAgregar.Visible = True
    End Sub

    Private Sub gridRuta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRuta.RowDataBound
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.ForeColor = Drawing.Color.White
            e.Row.Cells(4).Text = gridRuta.Rows.Count & " tiendas"
        End If
    End Sub

    Private Sub gridRuta_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridRuta.RowDeleting
        BD.Execute(ConexionJovy.localSqlServer, _
                   "DELETE FROM Jovy_CatRutas WHERE id_usuario='" & cmbPromotor.Text & "' " & _
                   "AND id_tienda=" & gridRuta.Rows(e.RowIndex).Cells(4).Text & " " & _
                   "AND id_cadena=" & gridRuta.Rows(e.RowIndex).Cells(2).Text & "")

        CargarRuta()
    End Sub

    Protected Sub ListCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListCadena.SelectedIndexChanged
        CargaSQL()

        Lista.LlenaBox(ConexionJovy.localSqlServer, SQLTienda, "nombre", "id_tienda", ListTienda)

        lblAgregada.Text = ""
    End Sub

    Protected Sub ListEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListEstado.SelectedIndexChanged
        CargaSQL()

        Lista.LlenaBox(ConexionJovy.localSqlServer, SQLCadena, "nombre_cadena", "id_cadena", ListCadena)
        Lista.LlenaBox(ConexionJovy.localSqlServer, SQLTienda, "nombre", "id_tienda", ListTienda)

        lblAgregada.Text = ""
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAceptar.Click
        Dim Tienda, Cadena As Integer
        If ListTienda.SelectedValue = "" Then
            Tienda = 0 : Else
            Tienda = ListTienda.SelectedValue

            Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                                   "SELECT * FROM Jovy_Tiendas " & _
                                                   "WHERE id_tienda=" & Tienda & "")
            If tabla.Rows.Count = 1 Then
                Cadena = tabla.Rows(0)("id_cadena") : End If
        End If

        If ListCadena.SelectedValue = "" Then
            If ListTienda.SelectedValue = "" Then
                lblaviso.Text = "Selecciona Tienda o Cadena"
                Exit Sub : End If
        Else
            Cadena = ListCadena.SelectedValue
        End If

        AgregaTienda(cmbPromotor.Text, Cadena, Tienda)
        CargarRuta()

        Dim ItemTienda As Integer
        ItemTienda = ListTienda.SelectedIndex.ToString - 1
        Lista.LlenaBox(ConexionJovy.localSqlServer, "SELECT id_tienda, nombre FROM Jovy_Tiendas WHERE id_tienda NOT IN(SELECT id_tienda FROM Jovy_CatRutas) ORDER BY nombre", "nombre", "id_tienda", ListTienda)
    End Sub

    Public Function AgregaTienda(ByVal IDUsuario As String, ByVal IDCadena As Integer, _
                                 ByVal IDTienda As Integer) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                               "SELECT id_usuario, id_tienda " & _
                                             "FROM Jovy_CatRutas " & _
                                             "WHERE id_usuario='" & IDUsuario & "' " & _
                                             "AND id_tienda=" & IDTienda & " " & _
                                             "AND id_cadena=" & IDCadena & "")
        If Tabla.Rows.Count = 1 Then
            Me.lblAgregada.Text = "La tienda ya existe en una Ruta."
        Else
            BD.Execute(ConexionJovy.localSqlServer, _
                                   "INSERT INTO Jovy_CatRutas " & _
                                   "(id_usuario,id_cadena,id_tienda) " & _
                                   "VALUES('" & IDUsuario & "'," & IDCadena & "," & IDTienda & ")")

            Dim Agregado As String
            If Not ListTienda.SelectedValue = "" Then
                Agregado = "Se agrego la tienda '" & ListTienda.SelectedItem.ToString & "' a la Ruta actual." : Else
                Agregado = "Se agrego la cadena '" & ListCadena.SelectedItem.ToString & "' a la Ruta actual." : End If

            lblAgregada.Text = Agregado
        End If

        Tabla.Dispose()

        CargarRuta()
    End Function

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        Cargar()
    End Sub

    Protected Sub btnTerminar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTerminar.Click
        pnlAgregar.Visible = False
        pnlRuta.Visible = True

        lblAgregada.Text = ""
    End Sub

    Private Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridRuta.Columns(2).Visible = False
        gridRuta.Columns(4).Visible = False
    End Sub

    Private Sub ListTienda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListTienda.SelectedIndexChanged
        lblAgregada.Text = ""
    End Sub
End Class