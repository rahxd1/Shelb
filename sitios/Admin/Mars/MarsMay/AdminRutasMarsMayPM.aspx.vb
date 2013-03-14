Imports System.Data.SqlClient

Partial Public Class AdminRutasMarsMayPM
    Inherits System.Web.UI.Page

    Dim CadenaSQL, EstadoSQL, TiendaSQL, PromotorSQL, RegionSQL As String
    Dim CadenaSel, EstadoSel, RegionSel As String

    Sub CargaSQLLista()
        CadenaSel = Acciones.Slc.cmb("id_cadena", ListCadena.SelectedValue)
        EstadoSel = Acciones.Slc.cmb("id_estado", ListEstado.SelectedValue)
        RegionSel = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)

        RegionSQL = "SELECT DISTINCT id_region, nombre_region " & _
                    "FROM View_Tiendas_Mayoreo  " & _
                    "WHERE id_region<>0 ORDER BY nombre_region"

        PromotorSQL = "SELECT id_usuario FROM Usuarios where id_proyecto=10 AND id_tipo=7 AND captura=1 " & _
                    " " + RegionSel + " " & _
                    "ORDER BY id_usuario"

        EstadoSQL = "SELECT distinct id_estado,nombre_estado " & _
                    "FROM View_Tiendas_Mayoreo " & _
                    "WHERE id_tienda NOT IN(SELECT id_tienda FROM Mayoreo_Verificadores_CatRutas) " & _
                    " " + RegionSel + " " & _
                    "ORDER BY nombre_estado"

        CadenaSQL = "SELECT DISTINCT id_cadena, nombre_cadena " & _
                    "FROM View_Tiendas_Mayoreo " & _
                    "WHERE id_tienda NOT IN(SELECT id_tienda FROM Mayoreo_Verificadores_CatRutas) " & _
                    " " + RegionSel + EstadoSel + " " & _
                    "ORDER BY nombre_cadena"

        TiendaSQL = "SELECT id_tienda, nombre " & _
                    "FROM View_Tiendas_Mayoreo as TI " & _
                    "WHERE id_tienda NOT IN(SELECT id_tienda FROM Mayoreo_Verificadores_CatRutas) " & _
                    " " + RegionSel + CadenaSel + EstadoSel + " " & _
                    "ORDER BY nombre"

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LstProyectos(ConexionMars.localSqlServer, Me.gridProyectos)

            CargaSQLLista()
            CargarRuta()

            Lista.LlenaBox(ConexionMars.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", ListCadena)
            Lista.LlenaBox(ConexionMars.localSqlServer, EstadoSQL, "nombre_estado", "id_estado", ListEstado)
            Lista.LlenaBox(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)

            Combo.LlenaDrop(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

            Combo.LlenaDrop(ConexionMars.localSqlServer, "SELECT * FROM Mayoreo_Verificadores_Dias", "dia", "id_dia", cmbDia)
        End If
    End Sub

    Sub CargarRuta()
        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT DISTINCT RUT.id_usuario,TI.nombre_cadena,RUT.id_tienda, TI.nombre,RUT.id_dia " & _
                    "FROM Mayoreo_Verificadores_CatRutas AS RUT  " & _
                    "INNER JOIN View_Tiendas_Mayoreo AS TI ON RUT.id_tienda = TI.id_tienda " & _
                    "WHERE RUT.id_usuario='" & cmbPromotor.Text & "' " & _
                    "ORDER BY TI.nombre,TI.nombre_cadena", Me.gridRuta)
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAgregar.Click
        pnlAgregar.Visible = True

        CargaSQLLista()

        Lista.LlenaBox(ConexionMars.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", ListCadena)
        Lista.LlenaBox(ConexionMars.localSqlServer, EstadoSQL, "nombre_estado", "id_estado", ListEstado)
        Lista.LlenaBox(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)

        CargarRuta()
    End Sub

    Private Sub gridRuta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRuta.RowDataBound
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(4).Text = gridRuta.Rows.Count & " tiendas"
        End If
    End Sub

    Private Sub gridRuta_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridRuta.RowDeleting
        BD.Execute(ConexionMars.localSqlServer, _
                   "DELETE FROM Mayoreo_Verificadores_CatRutas " & _
                   "WHERE id_usuario = '" & cmbPromotor.Text & "' " & _
                   "AND id_tienda=" & gridRuta.Rows(e.RowIndex).Cells(3).Text & "")

        CargarRuta()
        pnlAgregar.Visible = False
    End Sub

    Protected Sub ListCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListCadena.SelectedIndexChanged
        lblGuardado.Text = ""

        CargaSQLLista()

        Lista.LlenaBox(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)

        CargarRuta()
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAceptar.Click
        AgregaTienda(cmbPromotor.Text, ListTienda.SelectedValue, cmbDia.SelectedValue)
        lblGuardado.Text = "Se guardo la tienda '" + ListTienda.SelectedItem.ToString() + "' correctamente"

        CargarRuta()
        CargaSQLLista()

        Lista.LlenaBox(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)
        cmbDia.SelectedValue = ""
    End Sub

    Public Function AgregaTienda(ByVal IDUsuario As String, ByVal IDTienda As Integer, _
                                 ByVal Dia As String) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT id_usuario, id_tienda " & _
                                               "FROM Mayoreo_Verificadores_CatRutas " & _
                                               "WHERE id_usuario='" & IDUsuario & "' " & _
                                               "AND id_tienda=" & IDTienda & " " & _
                                               "AND dia='" & Dia & "'")
        If Tabla.Rows.Count = 1 Then
            Me.lblaviso.Text = "LA TIENDA YA EXISTE EN UNA RUTA."
        Else
            BD.Execute(ConexionMars.localSqlServer, _
                       "INSERT INTO Mayoreo_Verificadores_CatRutas " & _
                       "(id_usuario,id_tienda,dia) " & _
                       "VALUES('" & IDUsuario & "'," & IDTienda & ",'" & Dia & "')")
        End If

        Tabla.Dispose()
    End Function

    Protected Sub ListEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListEstado.SelectedIndexChanged
        lblGuardado.Text = ""

        CargaSQLLista()

        Lista.LlenaBox(ConexionMars.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", ListTienda)
        Lista.LlenaBox(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", ListTienda)

        CargarRuta()
    End Sub

    Private Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridRuta.Columns(3).Visible = False
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        If Not cmbPromotor.SelectedValue = "" Then
            CargarRuta()
            btnAgregar.Enabled = True
        End If

        pnlAgregar.Visible = False
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        CargaSQLLista()
        Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        pnlAgregar.Visible = False
    End Sub

    Protected Sub btnTerminar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTerminar.Click
        pnlAgregar.Visible = False
    End Sub

    Protected Sub btnGuardaCambios_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardaCambios.Click
        For i = 0 To gridRuta.Rows.Count - 1
            Dim IDTienda As Integer
            IDTienda = gridRuta.Rows(i).Cells(3).Text

            Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                                   "SELECT * From Mayoreo_Verificadores_CatRutas " & _
                                                   "WHERE id_usuario='" & cmbPromotor.SelectedValue & "' " & _
                                                   "AND id_tienda=" & IDTienda & "")

            If Tabla.Rows.Count <> 0 Then
                Dim cmbDia As DropDownList = CType(Me.gridRuta.Rows(i).FindControl("cmbDia"), DropDownList)

                BD.Execute(ConexionMars.localSqlServer, _
                           "UPDATE Mayoreo_Verificadores_CatRutas " & _
                           "set dia='" & cmbDia.SelectedValue & "' FROM Mayoreo_Verificadores_CatRutas " & _
                           "where id_tienda=" & IDTienda & "")
            End If

            Tabla.Dispose()
        Next i
    End Sub

    Private Sub ListTienda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListTienda.SelectedIndexChanged
        lblGuardado.Text = ""
    End Sub
End Class