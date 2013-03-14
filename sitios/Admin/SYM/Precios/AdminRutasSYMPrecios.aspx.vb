Imports System.Data.SqlClient

Partial Public Class AdminRutasSYMPrecios
    Inherits System.Web.UI.Page

    Dim CadenaSQL, EstadoSQL, PromotorSQL, RegionSQL As String
    Dim CadenaSel, EstadoSel, RegionSel, RegionUsuario As String

    Sub CargaSQLLista()
        RegionSel = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)

        CadenaSQL = "SELECT DISTINCT id_cadena, nombre_cadena " & _
                    "FROM Cadenas_Tiendas " & _
                    "WHERE id_cadena NOT IN(SELECT DISTINCT id_cadena FROM Precios_CatRutas " & _
                    "WHERE id_usuario='" & cmbPromotor.SelectedValue & "') " & _
                    "ORDER BY nombre_cadena"

        RegionSQL = "SELECT DISTINCT REG.id_region, REG.nombre_region FROM Usuarios as US " & _
                    "INNER JOIN Regiones AS REG ON US.id_region=REG.id_region " & _
                    "WHERE REG.id_region<>0 "

        PromotorSQL = "SELECT id_usuario FROM Usuarios where id_proyecto=31 AND id_tipo=10 " & _
                    " " + RegionSel + " " & _
                    "ORDER BY id_usuario"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LstProyectos(ConexionSYM.localSqlServer, Me.gridProyectos)

            CargaSQLLista()

            Lista.LlenaBox(ConexionSYM.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", ListCadena)

            Combo.LlenaDrop(ConexionSYM.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, "SELECT * FROM Precios_Tipo_Periodos", "nombre_tipo_periodo", "tipo_periodo", cmbTipoCaptura)
        End If
    End Sub

    Sub CargarRuta()
        CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT DISTINCT RUT.id_usuario, CAD.nombre_cadena, RUT.id_cadena " & _
                    "FROM Precios_CatRutas AS RUT  " & _
                    "INNER JOIN Cadenas_Tiendas AS CAD ON CAD.id_cadena = RUT.id_cadena " & _
                    "WHERE RUT.id_usuario='" & cmbPromotor.Text & "' " & _
                    "AND RUT.semanal_mensual='" & cmbTipoCaptura.Text & "' " & _
                    "ORDER BY CAD.nombre_cadena", Me.gridRuta)
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAgregar.Click
        pnlAgregar.Visible = True

        CargaSQLLista()

        Lista.LlenaBox(ConexionSYM.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", ListCadena)

        CargarRuta()
    End Sub

    Private Sub gridRuta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRuta.RowDataBound
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(2).Text = gridRuta.Rows.Count & " cadenas"
        End If
    End Sub

    Private Sub gridRuta_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridRuta.RowDeleting
        BD.Execute(ConexionSYM.localSqlServer, _
                   "DELETE FROM Precios_CatRutas " & _
                   "WHERE id_usuario = '" & cmbPromotor.Text & "' " & _
                   "AND id_cadena= '" & gridRuta.Rows(e.RowIndex).Cells(3).Text & "'")

        CargarRuta()
        pnlAgregar.Visible = False
    End Sub

    Protected Sub ListCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListCadena.SelectedIndexChanged
        CargarRuta()
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAceptar.Click
        AgregaTienda(cmbPromotor.Text, ListCadena.SelectedValue, cmbTipoCaptura.SelectedValue)
        CargarRuta()

        CargaSQLLista()
    End Sub

    Public Function AgregaTienda(ByVal IDUsuario As String, ByVal IDCadena As String, _
                                 ByVal TipoCaptura As Integer) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT id_usuario, id_cadena " & _
                                               "FROM Precios_CatRutas " & _
                                               "WHERE id_usuario='" & IDUsuario & "' " & _
                                               "AND id_cadena=" & IDCadena & " " & _
                                               "AND semanal_mensual=" & TipoCaptura & "")

        If Tabla.Rows.Count = 1 Then
            Me.lblaviso.Text = "La cadena ya existe en la ruta."
        Else
            BD.Execute(ConexionSYM.localSqlServer, _
                       "INSERT INTO Precios_CatRutas " & _
                       "(id_usuario, id_cadena,semanal_mensual) " & _
                       "VALUES('" & IDUsuario & "'," & IDCadena & "," & TipoCaptura & ")")
        End If

        Tabla.Dispose()
    End Function

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
        Combo.LlenaDrop(ConexionSYM.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        pnlAgregar.Visible = False
    End Sub

    Protected Sub btnTerminar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTerminar.Click
        pnlAgregar.Visible = False
    End Sub

    Private Sub cmbTipoCaptura_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoCaptura.SelectedIndexChanged
        If Not cmbPromotor.SelectedValue = "" Then
            CargarRuta()
            btnAgregar.Enabled = True
        End If

        pnlAgregar.Visible = False
    End Sub
End Class