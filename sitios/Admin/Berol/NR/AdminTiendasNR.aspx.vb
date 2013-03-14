Imports System.Data.SqlClient
Imports System.Security.Cryptography

Partial Public Class AdminTiendasNR
    Inherits System.Web.UI.Page

    Dim RegionSel, EstadoSel, FormatoSel, TiendaSel As String
    Dim RegionSQL, EstadoSQL, CadenaSQL, FormatoSQL, TiendaSQL As String

    Private Sub VerDatos(ByVal SeleccionIDtienda As String)
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                               "SELECT * FROM NR_Tiendas " & _
                                               "WHERE id_tienda= " & SeleccionIDtienda & "")

        If Tabla.Rows.Count > 0 Then
            IDTienda.Text = Tabla.Rows(0)("id_tienda")
            txtNoTienda.Text = Tabla.Rows(0)("codigo")
            txtNombreTienda.Text = Tabla.Rows(0)("nombre")
            cmbCadena.SelectedValue = Tabla.Rows(0)("id_cadena")
            cmbFormato.SelectedValue = Tabla.Rows(0)("id_formato")
            cmbRegion.SelectedValue = Tabla.Rows(0)("id_region")
            cmbEstado.SelectedValue = Tabla.Rows(0)("id_estado")
            txtCiudad.Text = Tabla.Rows(0)("ciudad")
        End If

        Tabla.Dispose()
    End Sub

    Sub SQLCombo()
        RegionSel = Acciones.Slc.cmb("id_region", cmbFiltroRegion.SelectedValue)
        FormatoSel = Acciones.Slc.cmb("id_formato", cmbFiltroFormato.SelectedValue)
        EstadoSel = Acciones.Slc.cmb("id_estado", cmbFiltroEstado.SelectedValue)

        RegionSQL = "SELECT DISTINCT nombre_region, id_region " & _
                    "FROM View_Tiendas_NR WHERE id_region <>0 " & _
                    "ORDER BY nombre_region"

        EstadoSQL = "SELECT DISTINCT nombre_estado, id_estado " & _
                    "FROM View_Tiendas_NR WHERE id_tienda<>0 " & _
                    " " + RegionSel + " " & _
                    " ORDER BY nombre_estado"

        FormatoSQL = "SELECT DISTINCT id_formato, nombre_formato " & _
                    "FROM View_Tiendas_NR WHERE id_tienda<>0 " & _
                    " " + EstadoSel + RegionSel + " " & _
                    " ORDER BY nombre_formato"

        CadenaSQL = "SELECT DISTINCT id_cadena, nombre_cadena " & _
                    "FROM View_Tiendas_NR WHERE id_tienda<>0 " & _
                    " " + EstadoSel + RegionSel + FormatoSel + " " & _
                    " ORDER BY nombre_cadena"

        TiendaSQL = "SELECT DISTINCT id_tienda, nombre " & _
                    "FROM View_Tiendas_NR WHERE id_tienda<>0 " & _
                    " " + RegionSel + EstadoSel + FormatoSel + " " & _
                    "ORDER BY nombre"
    End Sub

    Sub CargarTiendas()
        RegionSQL = Acciones.Slc.cmb("id_region", cmbFiltroRegion.SelectedValue)
        EstadoSQL = Acciones.Slc.cmb("id_estado", cmbFiltroEstado.SelectedValue)
        CadenaSQL = Acciones.Slc.cmb("id_formato", cmbFiltroFormato.SelectedValue)
        TiendaSQL = Acciones.Slc.cmb("id_tienda", cmbFiltroTienda.SelectedValue)

        CargaGrilla(ConexionBerol.localSqlServer, _
                    "SELECT * FROM View_Tiendas_NR WHERE id_tienda<>0 " & _
                    " " + RegionSQL + EstadoSQL + " " & _
                    " " + CadenaSQL + TiendaSQL + " ORDER BY nombre", Me.gridTiendas)

        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Guardar(IDTienda.Text, txtNoTienda.Text, txtNombreTienda.Text, cmbCadena.SelectedValue, _
                cmbFormato.SelectedValue, cmbRegion.SelectedValue, txtCiudad.Text, cmbEstado.SelectedValue)
        Borrar()
        lnkNuevo.Enabled = True

        CargarTiendas()
    End Sub

    Public Function Guardar(ByVal id_tienda As String, ByVal NoTienda As String, _
                            ByVal Nombre As String, ByVal id_cadena As Integer, _
                            ByVal id_formato As Integer, ByVal id_region As Integer, _
                            ByVal Ciudad As String, ByVal id_estado As Integer) As Integer

        If id_tienda = "" Then
            BD.Execute(ConexionBerol.localSqlServer, _
                       "INSERT INTO NR_Tiendas " & _
                       "(codigo, nombre, id_cadena, id_formato, id_region, Ciudad, id_estado) " & _
                       "VALUES('" & NoTienda & "','" & Nombre & "'," & id_cadena & "," & id_formato & ", " & _
                       "" & id_region & ",'" & Ciudad & "'," & id_estado & ")")

            lblAviso.Text = "La información se guardo satisfactoriamente."
            pnlNuevo.Visible = False
        Else
            BD.Execute(ConexionBerol.localSqlServer, _
                       "UPDATE NR_Tiendas " & _
                       "SET codigo='" & NoTienda & "', nombre='" & Nombre & "', id_cadena=" & id_cadena & ", " & _
                       "id_formato=" & id_formato & ",id_region=" & id_region & ", Ciudad='" & Ciudad & "', " & _
                       "id_estado=" & id_estado & " WHERE id_tienda=" & id_tienda & "")
 
            lblAviso.Text = "Los cambios de la tienda se realizaron correctamente."
            pnlNuevo.Visible = False
        End If

        CargarTiendas()
    End Function

    Sub Borrar()
        IDTienda.Text = ""
        txtNoTienda.Text = ""
        txtNombreTienda.Text = ""
        txtCiudad.Text = ""
        cmbEstado.SelectedValue = ""
        cmbFormato.SelectedValue = ""
        cmbRegion.SelectedValue = ""
        cmbCadena.SelectedValue = ""
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
            LstProyectos(ConexionBerol.localSqlServer, Me.gridProyectos)

            CargarTiendas()

            SQLCombo()
            Combo.LlenaDrop(ConexionBerol.localSqlServer, FormatoSQL, "nombre_formato", "id_formato", cmbFiltroFormato)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, FormatoSQL, "nombre_formato", "id_formato", cmbFormato)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, EstadoSQL, "nombre_estado", "id_estado", cmbFiltroEstado)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, EstadoSQL, "nombre_estado", "id_estado", cmbEstado)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbFiltroRegion)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbFiltroTienda)
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

        Combo.LlenaDrop(ConexionBerol.localSqlServer, EstadoSQL, "nombre_estado", "id_estado", cmbFiltroEstado)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, FormatoSQL, "nombre_formato", "id_formato", cmbFiltroFormato)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbFiltroTienda)

        CargarTiendas()
    End Sub

    Private Sub cmbFiltroEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFiltroEstado.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, FormatoSQL, "nombre_formato", "id_formato", cmbFiltroFormato)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbFiltroTienda)

        CargarTiendas()
    End Sub

    Protected Sub cmbFiltroFormato_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFiltroFormato.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbFiltroTienda)

        CargarTiendas()
    End Sub

    Protected Sub cmbFiltroTienda_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFiltroTienda.SelectedIndexChanged
        CargarTiendas()
    End Sub

    Private Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridTiendas.Columns(1).Visible = False
    End Sub

End Class