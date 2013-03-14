Imports System.Data.SqlClient
Imports System.Security.Cryptography

Partial Public Class AdminTiendasJovy
    Inherits System.Web.UI.Page

    Dim RegionSel, CadenaSel As String
    Dim RegionSQL, CadenaSQL, TiendaSQL As String

    Private Sub VerDatos(ByVal SeleccionIDtienda As String)
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                               "SELECT * FROM Jovy_Tiendas " & _
                                               "WHERE id_tienda= '" & SeleccionIDtienda & "'")
        If tabla.Rows.Count > 0 Then
            IDTienda.Text = tabla.Rows(0)("id_tienda")
            txtNoTienda.Text = tabla.Rows(0)("no_tienda")
            txtNombreTienda.Text = tabla.Rows(0)("nombre")
            cmbCadena.SelectedValue = tabla.Rows(0)("id_cadena")
            cmbRegion.SelectedValue = tabla.Rows(0)("id_region")
            cmbEstado.SelectedValue = tabla.Rows(0)("id_estado")
            txtCiudad.Text = tabla.Rows(0)("ciudad")
        End If

        Tabla.Dispose()
    End Sub

    Sub SQLCombo()
        RegionSel = Acciones.Slc.cmb("id_region", cmbFiltroRegion.SelectedValue)
        CadenaSel = Acciones.Slc.cmb("id_cadena", cmbFiltroCadena.SelectedValue)

        RegionSQL = "SELECT DISTINCT nombre_region, id_region " & _
                    "FROM View_Jovy_Tiendas " & _
                    "ORDER BY nombre_region"

        CadenaSQL = "SELECT DISTINCT id_cadena, nombre_cadena " & _
                    "FROM View_Jovy_Tiendas " & _
                    "WHERE id_cadena<>0 " & _
                    " " + RegionSel + " " & _
                    "ORDER BY nombre_cadena"

        TiendaSQL = "SELECT DISTINCT id_tienda, nombre " & _
                   "FROM View_Jovy_Tiendas " & _
                   "WHERE id_tienda<>0 " & _
                   " " + RegionSel + " " & _
                   " " + CadenaSel + " " & _
                   "ORDER BY nombre"
    End Sub

    Sub CargarTiendas()
        RegionSQL = Acciones.Slc.cmb("id_region", cmbFiltroRegion.SelectedValue)
        CadenaSQL = Acciones.Slc.cmb("id_cadena", cmbFiltroCadena.SelectedValue)

        CargaGrilla(ConexionJovy.localSqlServer, _
                    "SELECT id_tienda, nombre, nombre_cadena, Ciudad, nombre_region, nombre_estado " & _
                    "FROM View_Jovy_Tiendas " & _
                    "WHERE id_tienda<>0 " & _
                    " " + RegionSQL + CadenaSQL + " ORDER BY nombre", Me.gridTiendas)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Guardar(IDTienda.Text, txtNoTienda.Text, txtNombreTienda.Text, cmbCadena.SelectedValue, _
                cmbRegion.SelectedValue, txtCiudad.Text, cmbEstado.SelectedValue)
        Borrar()

        pnlNuevo.Visible = False
        pnlConsulta.Visible = False
    End Sub

    Public Function Guardar(ByVal id_tienda As String, ByVal NoTienda As String, _
                            ByVal Nombre As String, ByVal id_cadena As Integer, _
                            ByVal id_region As Integer, ByVal Ciudad As String, _
                            ByVal id_estado As Integer) As Integer
        Me.lblaviso.Visible = True

        If id_tienda = "" Then
            BD.Execute(ConexionJovy.localSqlServer, _
                       "INSERT INTO Jovy_Tiendas " & _
                       "(no_tienda, nombre, id_cadena, id_region, Ciudad, id_estado) " & _
                       "VALUES('" & NoTienda & "','" & Nombre & "'," & id_cadena & ", " & _
                       "" & id_region & ",'" & Ciudad & "'," & id_estado & ")")

            Me.lblaviso.Text = "Se guardo exitosamente la información."
        Else
            BD.Execute(ConexionJovy.localSqlServer, _
                       "UPDATE Jovy_Tiendas " & _
                       "SET nombre='" & Nombre & "', no_tienda='" & NoTienda & "'," & _
                       "id_cadena=" & id_cadena & ", id_region=" & id_region & ", " & _
                       "Ciudad='" & Ciudad & "', id_estado=" & id_estado & " " & _
                       "WHERE id_tienda=" & id_tienda & "")

            Me.lblaviso.Text = "Los cambios se realizaron correctamente."
        End If

        pnlNuevo.Visible = False
        lnkNuevo.Enabled = True

        CargarTiendas()
    End Function

    Sub Borrar()
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
        lblaviso.Text = ""
        Borrar()
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Borrar()

        pnlNuevo.Visible = False
        pnlConsulta.Visible = False
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Combo.LlenaDrop(ConexionJovy.localSqlServer, "SELECT * FROM Cadenas_Tiendas order by nombre_cadena", "nombre_cadena", "id_cadena", cmbCadena)
            Combo.LlenaDrop(ConexionJovy.localSqlServer, "SELECT * FROM Regiones WHERE id_region <>0", "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionJovy.localSqlServer, "SELECT * FROM Estados", "nombre_estado", "id_estado", cmbEstado)

            SQLCombo()
            Combo.LlenaDrop(ConexionJovy.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbFiltroCadena)
            Combo.LlenaDrop(ConexionJovy.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbFiltroRegion)
            Combo.LlenaDrop(ConexionJovy.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbFiltroTienda)

            CargarTiendas()
        End If
    End Sub

    Protected Sub lnkConsultas_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkConsultas.Click
        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
        lblaviso.Text = ""
        Borrar()
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

        Combo.LlenaDrop(ConexionJovy.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbFiltroCadena)
        Combo.LlenaDrop(ConexionJovy.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbFiltroTienda)

        CargarTiendas()
    End Sub

    Protected Sub cmbFiltroCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFiltroCadena.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionJovy.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbFiltroTienda)

        CargarTiendas()
    End Sub

    Protected Sub cmbFiltroTienda_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFiltroTienda.SelectedIndexChanged
        CargarTiendas()
    End Sub

    Private Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridTiendas.Columns(1).Visible = False
    End Sub
End Class