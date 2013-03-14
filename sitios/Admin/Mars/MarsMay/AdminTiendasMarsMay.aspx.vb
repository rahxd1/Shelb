Imports System.Data.SqlClient
Imports System.Security.Cryptography

Partial Public Class AdminTiendasMarsMay
    Inherits System.Web.UI.Page

    Dim RegionSel, EstadoSel, CadenaSel, TiendaSel As String
    Dim RegionSQL, EstadoSQL, CadenaSQL, TiendaSQL As String

    Private Sub VerDatos(ByVal SeleccionIDtienda As String)
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT * FROM Mayoreo_Tiendas " & _
                                               "WHERE id_tienda= " & SeleccionIDtienda & "")
        If Tabla.Rows.Count > 0 Then
            IDTienda.Text = Tabla.Rows(0)("id_tienda")
            txtNoTienda.Text = Tabla.Rows(0)("codigo")
            txtNombreTienda.Text = Tabla.Rows(0)("nombre")
            cmbCadena.SelectedValue = Tabla.Rows(0)("id_cadena")
            cmbTipoTienda.SelectedValue = Tabla.Rows(0)("tipo_tienda")
            cmbTop.SelectedValue = Tabla.Rows(0)("top_rc")
            cmbRegion.SelectedValue = Tabla.Rows(0)("id_region")
            cmbEstado.SelectedValue = Tabla.Rows(0)("id_estado")
            txtCiudad.Text = Tabla.Rows(0)("ciudad")
        End If

        Tabla.Dispose()
    End Sub

    Sub SQLCombo()
        RegionSel = Acciones.Slc.cmb("id_region", cmbFiltroRegion.SelectedValue)
        CadenaSel = Acciones.Slc.cmb("id_cadena", cmbFiltroCadena.SelectedValue)
        EstadoSel = Acciones.Slc.cmb("id_estado", cmbFiltroEstado.SelectedValue)

        RegionSQL = "SELECT DISTINCT nombre_region, id_region " & _
                    "FROM View_Tiendas_Mayoreo " & _
                    "WHERE id_region <>0 ORDER BY nombre_region"

        EstadoSQL = "SELECT DISTINCT nombre_estado, id_estado " & _
                    "FROM View_Tiendas_Mayoreo WHERE id_estado<>0 " & _
                    " " + RegionSel + " " & _
                    "ORDER BY nombre_estado"

        CadenaSQL = "SELECT DISTINCT id_cadena,nombre_cadena " & _
                    "FROM View_Tiendas_Mayoreo WHERE id_cadena<>0 " & _
                    " " + EstadoSel + RegionSel + " " & _
                    " ORDER BY nombre_cadena"

        TiendaSQL = "SELECT DISTINCT id_tienda, nombre " & _
                   "FROM View_Tiendas_Mayoreo WHERE id_tienda<>0 " & _
                   " " + RegionSel + EstadoSel + CadenaSel + " " & _
                   " ORDER BY nombre"
    End Sub

    Sub CargarTiendas()
        RegionSQL = Acciones.Slc.cmb("id_region", cmbFiltroRegion.SelectedValue)
        EstadoSQL = Acciones.Slc.cmb("id_estado", cmbFiltroEstado.SelectedValue)
        CadenaSQL = Acciones.Slc.cmb("id_cadena", cmbFiltroCadena.SelectedValue)
        TiendaSQL = Acciones.Slc.cmb("id_tienda", cmbFiltroTienda.SelectedValue)

        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT * FROM View_Tiendas_Mayoreo WHERE id_tienda<>0 " & _
                    " " + RegionSQL + EstadoSQL + CadenaSQL + TiendaSQL + " " & _
                    "ORDER BY nombre", _
                    gridTiendas)

        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
    End Sub


    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Guardar(IDTienda.Text, txtNoTienda.Text, txtNombreTienda.Text, cmbCadena.SelectedValue, _
                cmbTipoTienda.SelectedValue, cmbTop.SelectedValue, cmbRegion.SelectedValue, _
                txtCiudad.Text, cmbEstado.SelectedValue)
        Borrar()
        lnkNuevo.Enabled = True

        CargarTiendas()
    End Sub

    Public Function Guardar(ByVal id_tienda As String, ByVal NoTienda As String, ByVal Nombre As String, _
                            ByVal id_cadena As Integer, ByVal TipoTienda As Integer, ByVal Top As String, _
                            ByVal id_region As Integer, ByVal Ciudad As String, ByVal id_estado As Integer) As Integer
        If id_tienda = "" Then
            BD.Execute(ConexionMars.localSqlServer, _
                       "INSERT INTO Mayoreo_Tiendas " & _
                       "(codigo,nombre,id_cadena,tipo_tienda,top_rc,id_region,ciudad,id_estado) " & _
                       "VALUES('" & NoTienda & "','" & Nombre & "'," & id_cadena & "," & TipoTienda & ", " & _
                       "'" & Top & "'," & id_region & ",'" & Ciudad & "'," & id_estado & ")")

            Me.lblAviso.Text = "Los cambios se realizaron correctamente."

        Else
            BD.Execute(ConexionMars.localSqlServer, _
                       "UPDATE Mayoreo_Tiendas " & _
                       "SET nombre='" & Nombre & "',codigo='" & NoTienda & "',id_cadena=" & id_cadena & ", " & _
                       "tipo_tienda=" & TipoTienda & ",top_rc='" & Top & "',id_region=" & id_region & ", " & _
                       "ciudad='" & Ciudad & "',id_estado=" & id_estado & " " & _
                       "WHERE id_tienda=" & id_tienda & "")

            Me.lblAviso.Text = "Se guardo exitosamente la información."
        End If

        pnlNuevo.Visible = False

        cmbFiltroCadena.Items.Clear()
        cmbFiltroTienda.Items.Clear()
        cmbFiltroEstado.Items.Clear()

        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, EstadoSQL, "nombre_estado", "id_estado", cmbFiltroEstado)
        Combo.LlenaDrop(ConexionMars.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbFiltroCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbFiltroTienda)

        CargarTiendas()
    End Function

    Sub Borrar()
        IDTienda.Text = ""
        txtNoTienda.Text = ""
        txtNombreTienda.Text = ""
        txtCiudad.Text = ""
        cmbEstado.SelectedValue = ""
        cmbRegion.SelectedValue = ""
        cmbCadena.SelectedValue = ""
        cmbTipoTienda.SelectedValue = ""
    End Sub

    Protected Sub lnkNuevo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkNuevo.Click
        pnlNuevo.Visible = True
        pnlConsulta.Visible = False
        txtNoTienda.Focus()
        lblAviso.Text = ""
        Borrar()
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Borrar()

        CargarTiendas()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LstProyectos(ConexionMars.localSqlServer, Me.gridProyectos)

            Combo.LlenaDrop(ConexionMars.localSqlServer, "SELECT * FROM Cadenas_Mayoreo order by nombre_cadena", "nombre_cadena", "id_cadena", cmbCadena)
            Combo.LlenaDrop(ConexionMars.localSqlServer, "SELECT * FROM Regiones WHERE id_region<>0", "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, "SELECT * FROM Estados", "nombre_estado", "id_estado", cmbEstado)
            Combo.LlenaDrop(ConexionMars.localSqlServer, "SELECT * FROM Tipo_Tiendas_Mayoreo", "nombre_tipo", "tipo_tienda", cmbTipoTienda)
            Combo.LlenaDrop(ConexionMars.localSqlServer, "SELECT * FROM Top_rc", "nombre_top_rc", "top_rc", cmbTop)

            SQLCombo()
            Combo.LlenaDrop(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbFiltroRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, EstadoSQL, "nombre_estado", "id_estado", cmbFiltroEstado)
            Combo.LlenaDrop(ConexionMars.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbFiltroCadena)
            Combo.LlenaDrop(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbFiltroTienda)

            CargarTiendas()
        End If
    End Sub

    Protected Sub lnkConsultas_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkConsultas.Click
        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
        lblAviso.Text = ""
    End Sub

    Private Sub gridTiendas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridTiendas.RowDataBound
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(2).Text = gridTiendas.Rows.Count & " tienda(s)"
        End If
    End Sub

    Private Sub gridTiendas_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gridTiendas.RowEditing
        If gridTiendas.Rows(e.NewEditIndex).Cells(1).Text = "" Then
            e.Cancel = True
            lblAviso.Text = "Error al intentar editar la tienda."
        Else
            lblAviso.Text = ""
            pnlNuevo.Visible = True
            pnlConsulta.Visible = False
            txtNoTienda.Focus()
            VerDatos(gridTiendas.Rows(e.NewEditIndex).Cells(1).Text)
        End If
    End Sub

    Protected Sub cmbFiltroRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFiltroRegion.SelectedIndexChanged
        cmbFiltroCadena.Items.Clear()
        cmbFiltroTienda.Items.Clear()
        cmbFiltroEstado.Items.Clear()

        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, EstadoSQL, "nombre_estado", "id_estado", cmbFiltroEstado)
        Combo.LlenaDrop(ConexionMars.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbFiltroCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbFiltroTienda)

        CargarTiendas()
    End Sub

    Protected Sub cmbFiltroCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFiltroCadena.SelectedIndexChanged
        cmbFiltroTienda.Items.Clear()

        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbFiltroTienda)

        CargarTiendas()
    End Sub

    Protected Sub cmbFiltroTienda_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFiltroTienda.SelectedIndexChanged
        CargarTiendas()
    End Sub

    Private Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridTiendas.Columns(1).Visible = False
    End Sub

    Private Sub cmbFiltroEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFiltroEstado.SelectedIndexChanged
        cmbFiltroCadena.Items.Clear()
        cmbFiltroTienda.Items.Clear()

        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbFiltroCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbFiltroTienda)

        CargarTiendas()
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscar.Click
        RegionSQL = Acciones.Slc.cmb("id_region", cmbFiltroRegion.SelectedValue)
        EstadoSQL = Acciones.Slc.cmb("id_estado", cmbFiltroEstado.SelectedValue)
        CadenaSQL = Acciones.Slc.cmb("id_cadena", cmbFiltroCadena.SelectedValue)
        TiendaSQL = Acciones.Slc.cmb("id_tienda", cmbFiltroTienda.SelectedValue)

        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT * FROM View_Tiendas_Mayoreo " & _
                    "WHERE nombre like '%" & txtBuscaNombreTienda.Text & "%' " & _
                    " " + RegionSQL + EstadoSQL + CadenaSQL + TiendaSQL + " " & _
                    "ORDER BY nombre", gridTiendas)

        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
    End Sub
End Class