Imports System.Data.SqlClient
Imports System.Security.Cryptography

Partial Public Class AdminTiendasFluid
    Inherits System.Web.UI.Page

    Dim RegionSel As String
    Dim RegionSQL, CadenaSQL As String

    Private Sub VerDatos(ByVal SeleccionIDtienda As String)
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                                               "SELECT * FROM View_Tiendas " & _
                                               "WHERE id_tienda=" & SeleccionIDtienda & "")
        If tabla.Rows.Count > 0 Then
            IDtienda.Text = tabla.Rows(0)("id_tienda")
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

        RegionSQL = "SELECT DISTINCT nombre_region, id_region " & _
                    "FROM View_Tiendas " & _
                    "WHERE id_region<>0 " & _
                    "ORDER BY nombre_region"

        CadenaSQL = "SELECT DISTINCT id_cadena, nombre_cadena " & _
                    "FROM View_Tiendas " & _
                    "WHERE id_tienda<>0 " & _
                    " " + RegionSel + " " & _
                    "ORDER BY nombre_cadena"

    End Sub

    Sub CargarTiendas()
        RegionSQL = Acciones.Slc.cmb("id_region", cmbFiltroRegion.SelectedValue)
        CadenaSQL = Acciones.Slc.cmb("id_cadena", cmbFiltroCadena.SelectedValue)

        CargaGrilla(ConexionFluidmaster.localSqlServer, _
                    "SELECT id_tienda, nombre, nombre_cadena, Ciudad, nombre_region, nombre_estado " & _
                    "FROM View_Tiendas " & _
                    "WHERE id_tienda<>0 " & _
                    " " + RegionSQL + CadenaSQL + " ORDER BY nombre", Me.gridTiendas)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Guardar(IDTienda.Text, txtNombreTienda.Text, cmbCadena.SelectedValue, _
                cmbRegion.SelectedValue, txtCiudad.Text, cmbEstado.SelectedValue)
        Borrar()

        pnlNuevo.Visible = False
        pnlConsulta.Visible = Visible
        End Sub

    Public Function Guardar(ByVal id_tienda As String, ByVal Nombre As String, ByVal id_cadena As Integer, _
                            ByVal id_region As Integer, ByVal Ciudad As String, ByVal id_estado As Integer) As Integer

        Me.lblAviso.Visible = True

        If id_tienda = "" Then
            BD.Execute(ConexionFluidmaster.localSqlServer, _
                       "INSERT INTO Tiendas " & _
                       "(nombre, id_cadena, id_region, Ciudad, id_estado) " & _
                       "VALUES('" & Nombre & "', " & id_cadena & ", " & id_region & ", " & _
                       "'" & Ciudad & "', " & id_estado & ")")

            Me.lblAviso.Text = "Los cambios se realizaron correctamente."
        Else
            BD.Execute(ConexionFluidmaster.localSqlServer, _
                       "UPDATE Tiendas " & _
                       "SET nombre='" & Nombre & "', id_cadena=" & id_cadena & ", " & _
                       "id_region=" & id_region & ", id_estado=" & id_estado & ", " & _
                       "ciudad='" & Ciudad & "' " & _
                       "WHERE id_tienda=" & id_tienda & "")

            Me.lblAviso.Text = "Se guardo exitosamente la información."
        End If

        pnlNuevo.Visible = False
        lnkNuevo.Enabled = True

        CargarTiendas()
    End Function

    Sub Borrar()
        txtNombreTienda.Text = ""
        txtCiudad.Text = ""
        cmbEstado.SelectedValue = ""
        cmbRegion.SelectedValue = ""
        cmbCadena.SelectedValue = ""
    End Sub

    Protected Sub lnkNuevo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkNuevo.Click
        pnlNuevo.Visible = True
        pnlConsulta.Visible = False
        txtNombreTienda.Focus()
        lblaviso.Text = ""
        Borrar()
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Borrar()

        pnlNuevo.Visible = False
        pnlConsulta.Visible = False
        lblAviso.Text = ""
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, "SELECT * FROM Cadenas_Tiendas order by nombre_cadena", "nombre_cadena", "id_cadena", cmbCadena)
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, "SELECT * FROM Regiones WHERE id_region <>0", "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, "SELECT * FROM Estados", "nombre_estado", "id_estado", cmbEstado)

            SQLCombo()
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbFiltroCadena)
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbFiltroRegion)

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
            txtNombreTienda.Focus()
            VerDatos(gridTiendas.Rows(e.NewEditIndex).Cells(1).Text)
        End If
    End Sub

    Protected Sub cmbFiltroRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFiltroRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbFiltroCadena)

        CargarTiendas()
    End Sub

    Protected Sub cmbFiltroCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFiltroCadena.SelectedIndexChanged
        CargarTiendas()
    End Sub


    Private Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridTiendas.Columns(1).Visible = False
    End Sub
End Class