Imports System.Data.SqlClient
Imports System.Security.Cryptography

Partial Public Class AdminTiendasMarsAS
    Inherits System.Web.UI.Page

    Dim RegionSel, EstadoSel, CadenaSel As String
    Dim RegionSQL, EstadoSQL, CadenaSQL, TiendaSQL, CodigoSQL, NombreSQL As String

    Private Sub VerDatos(ByVal SeleccionIDTienda As String)
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT * FROM AS_Tiendas " & _
                                               "WHERE id_tienda= " & SeleccionIDTienda & "")
        If Tabla.Rows.Count > 0 Then
            IDTienda.Text = Tabla.Rows(0)("id_tienda")
            txtNoTienda.Text = Tabla.Rows(0)("codigo")
            txtNombreTienda.Text = Tabla.Rows(0)("nombre")
            cmbCadena.SelectedValue = Tabla.Rows(0)("id_cadena")
            cmbRegion.SelectedValue = Tabla.Rows(0)("id_region")
            cmbEstado.SelectedValue = Tabla.Rows(0)("id_estado")
            txtCiudad.Text = Tabla.Rows(0)("ciudad")
            cmbAreaNielsen.SelectedValue = Tabla.Rows(0)("area_nielsen")
            cmbClasificacion.SelectedValue = Tabla.Rows(0)("id_clasificacion")
            cmbFormato.SelectedValue = Tabla.Rows(0)("id_formato")
            cmbGrupo.SelectedValue = Tabla.Rows(0)("id_grupo")
            txtRank.Text = Tabla.Rows(0)("rank")
        End If

        Tabla.Dispose()
    End Sub

    Sub SQLCombo()
        RegionSel = Acciones.Slc.cmb("id_region", cmbFiltroRegion.SelectedValue)
        CadenaSel = Acciones.Slc.cmb("id_cadena", cmbFiltroCadena.SelectedValue)
        EstadoSel = Acciones.Slc.cmb("id_estado", cmbFiltroEstado.SelectedValue)

        RegionSQL = "SELECT DISTINCT nombre_region, id_region " & _
                    "FROM View_Tiendas_AS WHERE id_region<>0 " & _
                    " ORDER BY nombre_region"

        EstadoSQL = "SELECT DISTINCT nombre_estado, id_estado " & _
                    "FROM View_Tiendas_AS WHERE id_estado<>0 " & _
                    " " + RegionSel + " " & _
                    " ORDER BY nombre_estado"

        CadenaSQL = "SELECT DISTINCT id_cadena, nombre_cadena " & _
                    "FROM View_Tiendas_AS WHERE id_cadena<>0 " & _
                    " " + EstadoSel + RegionSel + " " & _
                    " ORDER BY nombre_cadena"

        TiendaSQL = "SELECT DISTINCT id_tienda, nombre " & _
                   "FROM View_Tiendas_AS AS TI WHERE id_tienda<>0 " & _
                   " " + RegionSel + EstadoSel + CadenaSel + " " & _
                   " ORDER BY nombre"
    End Sub

    Sub CargarTiendas()
        RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbFiltroRegion.SelectedValue)
        EstadoSQL = Acciones.Slc.cmb("TI.id_estado", cmbFiltroEstado.SelectedValue)
        CadenaSQL = Acciones.Slc.cmb("TI.id_cadena", cmbFiltroCadena.SelectedValue)
        TiendaSQL = Acciones.Slc.cmb("TI.id_tienda", cmbFiltroTienda.SelectedValue)

        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT TI.codigo,TI.id_tienda,TI.nombre,TI.nombre_cadena,TI.clasificacion_tienda, " & _
                    "TI.Ciudad,TI.nombre_region,TI.nombre_estado,ISNULL(RUT.id_usuario,'')id_usuario " & _
                    "FROM View_Tiendas_AS as TI " & _
                    "LEFT JOIN AS_CatRutas as RUT ON RUT.id_tienda = TI.id_tienda " & _
                    "WHERE TI.id_tienda<>0 " & _
                    " " + RegionSQL + EstadoSQL + " " & _
                    " " + CadenaSQL + TiendaSQL + " " & _
                    "ORDER BY TI.nombre", gridTiendas)

        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Guardar(IDTienda.Text, txtNoTienda.Text, txtNombreTienda.Text, cmbCadena.SelectedValue, _
                cmbGrupo.SelectedValue, cmbFormato.SelectedValue, _
                cmbRegion.SelectedValue, txtCiudad.Text, cmbEstado.SelectedValue, _
                cmbAreaNielsen.SelectedValue, cmbClasificacion.SelectedValue, txtRank.Text)
        Borrar()
        lnkNuevo.Enabled = True

        CargarTiendas()
    End Sub

    Public Function Guardar(ByVal id_tienda As String, ByVal NoTienda As String, ByVal Nombre As String, _
                     ByVal id_cadena As String, ByVal id_grupo As Integer, ByVal id_formato As Integer, _
                     ByVal id_region As String, ByVal Ciudad As String, _
                     ByVal id_estado As String, ByVal Area_Nielsen As Integer, _
                     ByVal id_clasificacion As Integer, ByVal Rank As String) As Integer

        If id_tienda = "" Then
            BD.Execute(ConexionMars.localSqlServer, _
                       "INSERT INTO AS_Tiendas " & _
                       "(codigo,nombre,id_cadena,id_grupo,id_formato,id_region,Ciudad,id_estado," & _
                       "area_nielsen,id_clasificacion,Rank) " & _
                       "VALUES('" & NoTienda & "','" & Nombre & "'," & id_cadena & "," & id_grupo & ", " & _
                       "" & id_formato & "," & id_region & ",'" & Ciudad & "'," & id_estado & "," & _
                       "" & Area_Nielsen & "," & id_clasificacion & ",'" & Rank & "')")

            Me.lblAviso.Text = "Los cambios se realizaron correctamente."
        Else
            BD.Execute(ConexionMars.localSqlServer, _
                       "UPDATE AS_Tiendas " & _
                       "SET codigo='" & NoTienda & "',nombre='" & Nombre & "',id_cadena=" & id_cadena & ", " & _
                       "id_grupo=" & id_grupo & ",id_formato=" & id_formato & ",id_region=" & id_region & "," & _
                       "Ciudad='" & Ciudad & "', id_estado=" & id_estado & ", area_nielsen=" & Area_Nielsen & "," & _
                       "id_clasificacion=" & id_clasificacion & ", rank='" & Rank & "' " & _
                       "WHERE id_tienda='" & id_tienda & "'")

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
        cmbEstado.Text = ""
        cmbRegion.Text = ""
        cmbCadena.Text = ""
        cmbGrupo.Text = ""
        cmbFormato.Text = ""
        cmbAreaNielsen.Text = ""
        cmbClasificacion.Text = ""
        txtRank.Text = ""
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

        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LstProyectos(ConexionMars.localSqlServer, Me.gridProyectos)

            SQLCombo()

            Combo.LlenaDrop(ConexionMars.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbFiltroCadena)
            Combo.LlenaDrop(ConexionMars.localSqlServer, EstadoSQL, "nombre_estado", "id_estado", cmbFiltroEstado)
            Combo.LlenaDrop(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbFiltroRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbFiltroTienda)

            Combo.LlenaDrop(ConexionMars.localSqlServer, "SELECT * FROM Cadenas_Tiendas order by nombre_cadena", "nombre_cadena", "id_cadena", cmbCadena)
            Combo.LlenaDrop(ConexionMars.localSqlServer, "SELECT * FROM Cadena_Formato order by nombre_formato", "nombre_formato", "id_formato", cmbFormato)
            Combo.LlenaDrop(ConexionMars.localSqlServer, "SELECT * FROM Cadena_Grupo order by nombre_grupo", "nombre_grupo", "id_grupo", cmbGrupo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, "SELECT * FROM Regiones WHERE id_region<>0", "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, "SELECT * FROM Estados", "nombre_estado", "id_estado", cmbEstado)
            Combo.LlenaDrop(ConexionMars.localSqlServer, "SELECT * FROM AS_Clasificacion_Tiendas", "clasificacion_tienda", "id_clasificacion", cmbClasificacion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, "SELECT * FROM AS_Area_Nielsen", "area_nielsen", "area_nielsen", cmbAreaNielsen)

            CargarTiendas()
        End If
    End Sub

    Protected Sub lnkConsultas_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkConsultas.Click
        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
        lblAviso.Text = ""
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

        Combo.LlenaDrop(ConexionMars.localSqlServer, EstadoSQL, "nombre_estado", "id_estado", cmbFiltroEstado)
        Combo.LlenaDrop(ConexionMars.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbFiltroCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbFiltroTienda)

        CargarTiendas()
    End Sub

    Protected Sub cmbFiltroCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFiltroCadena.SelectedIndexChanged
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
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbFiltroCadena)
        Combo.LlenaDrop(ConexionMars.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbFiltroTienda)

        CargarTiendas()
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscar.Click
        RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbFiltroRegion.SelectedValue)
        EstadoSQL = Acciones.Slc.cmb("TI.id_estado", cmbFiltroEstado.SelectedValue)
        CadenaSQL = Acciones.Slc.cmb("TI.id_cadena", cmbFiltroCadena.SelectedValue)
        TiendaSQL = Acciones.Slc.cmb("TI.id_tienda", cmbFiltroTienda.SelectedValue)

        CodigoSQL = Acciones.Slc.txt("TI.codigo", txtBuscaCodigo.Text)
        NombreSQL = Acciones.Slc.txt("TI.nombre", txtBuscaTienda.Text)

        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT TI.codigo,TI.id_tienda, TI.nombre, TI.nombre_cadena,TI.clasificacion_tienda, " & _
                    "TI.Ciudad, TI.nombre_region, TI.nombre_estado, ISNULL(RUT.id_usuario,'')id_usuario " & _
                    "FROM View_Tiendas_AS as TI " & _
                    "LEFT JOIN AS_CatRutas as RUT ON RUT.id_tienda = TI.id_tienda " & _
                    "WHERE TI.id_tienda<>0 " & _
                    " " + CodigoSQL + NombreSQL + " " & _
                    " " + RegionSQL + EstadoSQL + " " & _
                    " " + CadenaSQL + TiendaSQL + " " & _
                    "ORDER BY TI.nombre", gridTiendas)

        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
    End Sub

    Protected Sub btnBuscarNombre_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscarNombre.Click
        RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbFiltroRegion.SelectedValue)
        EstadoSQL = Acciones.Slc.cmb("TI.id_estado", cmbFiltroEstado.SelectedValue)
        CadenaSQL = Acciones.Slc.cmb("TI.id_cadena", cmbFiltroCadena.SelectedValue)
        TiendaSQL = Acciones.Slc.cmb("TI.id_tienda", cmbFiltroTienda.SelectedValue)

        CodigoSQL = Acciones.Slc.txt("TI.codigo", txtBuscaCodigo.Text)
        NombreSQL = Acciones.Slc.txt("TI.nombre", txtBuscaTienda.Text)

        CargaGrilla(ConexionMars.localSqlServer, _
                     "SELECT TI.codigo,TI.id_tienda,TI.nombre,TI.nombre_cadena,TI.clasificacion_tienda, " & _
                    "TI.Ciudad,TI.nombre_region,TI.nombre_estado,ISNULL(RUT.id_usuario,'')id_usuario " & _
                    "FROM View_Tiendas_AS as TI " & _
                    "LEFT JOIN AS_CatRutas as RUT ON RUT.id_tienda = TI.id_tienda " & _
                    "WHERE TI.id_tienda<>0 " & _
                    " " + CodigoSQL + NombreSQL + " " & _
                    " " + RegionSQL + EstadoSQL + " " & _
                    " " + CadenaSQL + TiendaSQL + " " & _
                    "ORDER BY TI.nombre", gridTiendas)

        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
    End Sub
End Class