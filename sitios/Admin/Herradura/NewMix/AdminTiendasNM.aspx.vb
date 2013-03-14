Imports System.Data.SqlClient
Imports System.Security.Cryptography

Partial Public Class AdminTiendasNM
    Inherits System.Web.UI.Page

    Dim RegionSel, EstadoSel, CadenaSel, TiendaSel As String
    Dim RegionSQL, EstadoSQL, CadenaSQL, TiendaSQL As String

    Private Sub VerDatos(ByVal SeleccionIDtienda As String)
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionHerradura.localSqlServer, _
                                               "SELECT * FROM NewMix_Tiendas " & _
                                               "WHERE id_tienda= " & SeleccionIDtienda & "")
        If Tabla.Rows.Count > 0 Then
            IDTienda.Text = Tabla.Rows(0)("id_tienda")
            txtNoTienda.Text = Tabla.Rows(0)("no_tienda")
            txtNombreTienda.Text = Tabla.Rows(0)("nombre")
            cmbCadena.SelectedValue = Tabla.Rows(0)("id_cadena")
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
                    "FROM View_Tiendas_NM " & _
                    "ORDER BY nombre_region"

        EstadoSQL = "SELECT DISTINCT nombre_estado, id_estado " & _
                    "FROM View_Tiendas_NM " & _
                    "WHERE id_tienda<>0 " & _
                    " " + RegionSel + " " & _
                    " ORDER BY nombre_estado"

        CadenaSQL = "SELECT DISTINCT id_cadena, nombre_cadena " & _
                    "FROM View_Tiendas_NM " & _
                    "WHERE id_cadena<>0 " & _
                    " " + EstadoSel + " " & _
                    " " + RegionSel + " " & _
                    " ORDER BY nombre_cadena"

        TiendaSQL = "SELECT DISTINCT id_tienda, nombre " & _
                   "FROM View_Tiendas_NM " & _
                   " " + RegionSel + " " & _
                   " " + EstadoSel + " " & _
                   " " + CadenaSel + " " & _
                   " ORDER BY nombre"
    End Sub

    Sub CargarTiendas()
        RegionSQL = Acciones.Slc.cmb("REG.id_region", cmbFiltroRegion.SelectedValue)
        EstadoSQL = Acciones.Slc.cmb("TI.id_estado", cmbFiltroEstado.SelectedValue)
        CadenaSQL = Acciones.Slc.cmb("CAD.id_cadena", cmbFiltroCadena.SelectedValue)
        TiendaSQL = Acciones.Slc.cmb("TI.id_tienda", cmbFiltroTienda.SelectedValue)

        CargaGrilla(ConexionHerradura.localSqlServer, _
                    "SELECT id_tienda, nombre, nombre_cadena, Ciudad, nombre_region, nombre_estado " & _
                    "FROM View_Tiendas_NM " & _
                    " " + RegionSQL + EstadoSQL + CadenaSQL + TiendaSQL + " " & _
                    "ORDER BY nombre", Me.gridTiendas)

        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
    End Sub

    Sub CargarCombos()
        Combo.LlenaDrop(ConexionHerradura.localSqlServer, "SELECT * FROM Cadenas_tiendas order by nombre_cadena", "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionHerradura.localSqlServer, "SELECT * FROM Regiones WHERE id_region<>0", "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionHerradura.localSqlServer, "SELECT * FROM Estados", "nombre_estado", "id_estado", cmbEstado)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Guardar(IDTienda.Text, txtNoTienda.Text, txtNombreTienda.Text, cmbCadena.SelectedValue, _
                cmbRegion.SelectedValue, txtCiudad.Text, cmbEstado.SelectedValue)
        Borrar()
        lnkNuevo.Enabled = True

        CargarTiendas()
    End Sub

    Public Function Guardar(ByVal id_tienda As String, ByVal NoTienda As String, _
                            ByVal Nombre As String, ByVal id_cadena As Integer, _
                            ByVal id_region As Integer, ByVal Ciudad As String, _
                            ByVal id_estado As Integer) As Integer

        If id_tienda = "" Then
            BD.Execute(ConexionHerradura.localSqlServer, _
                       "INSERT INTO NewMix_Tiendas " & _
                       "(no_tienda, nombre, id_cadena, id_region, Ciudad, id_estado) " & _
                       "VALUES('" & NoTienda & "', '" & Nombre & "', " & id_cadena & "," & _
                       "" & id_region & ", '" & Ciudad & "', " & id_estado & ")")

            Me.lblAviso.Text = "Los cambios se realizaron correctamente."
        Else
            BD.Execute(ConexionHerradura.localSqlServer, _
                       "UPDATE NewMix_Tiendas " & _
                       "SET nombre='" & Nombre & "', id_cadena=" & id_cadena & ", " & _
                       "id_region=" & id_region & ", Ciudad='" & Ciudad & "', id_estado=" & id_estado & " " & _
                       "WHERE id_tienda=" & id_tienda & "")

            Me.lblAviso.Text = "Se guardo exitosamente la información."
        End If

        pnlNuevo.Visible = False

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
    End Sub

    Protected Sub lnkNuevo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkNuevo.Click
        pnlNuevo.Visible = True
        pnlConsulta.Visible = False
        txtNoTienda.Focus()
        CargarCombos()
        lblAviso.Text = ""
        Borrar()
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Borrar()

        CargarTiendas()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LstProyectos(ConexionHerradura.localSqlServer, Me.gridProyectos)

            CargarTiendas()
            CargarCombos()
            SQLCombo()

            Combo.LlenaDrop(ConexionHerradura.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbFiltroCadena)
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, EstadoSQL, "nombre_estado", "id_estado", cmbFiltroEstado)
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbFiltroRegion)
            Combo.LlenaDrop(ConexionHerradura.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbFiltroTienda)
        End If
    End Sub

    Protected Sub lnkConsultas_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkConsultas.Click
        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
        lblAviso.Text = ""
    End Sub

    Private Sub gridTiendas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridTiendas.RowDataBound
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(2).Text = gridTiendas.Rows.Count & " tiendas"
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
        SQLCombo()

        Combo.LlenaDrop(ConexionHerradura.localSqlServer, EstadoSQL, "nombre_estado", "id_estado", cmbFiltroEstado)
        Combo.LlenaDrop(ConexionHerradura.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbFiltroCadena)
        Combo.LlenaDrop(ConexionHerradura.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbFiltroTienda)

        CargarTiendas()
    End Sub

    Protected Sub cmbFiltroCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFiltroCadena.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionHerradura.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbFiltroTienda)

        CargarTiendas()
    End Sub

    Protected Sub cmbFiltroTienda_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFiltroTienda.SelectedIndexChanged
        CargarTiendas()
    End Sub

    Private Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridTiendas.Columns(1).Visible = False
    End Sub

    Private Sub cmbFiltroEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFiltroEstado.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionHerradura.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbFiltroCadena)
        Combo.LlenaDrop(ConexionHerradura.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbFiltroTienda)

        CargarTiendas()
    End Sub
End Class