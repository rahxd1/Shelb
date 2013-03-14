Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class AdminProcesosMars
    Inherits System.Web.UI.Page

    Dim SeleccionIDProceso As String
    Dim RegionSQL, CadenaSQL As String
    Dim UsuarioSel As String, TiendaSel As String
    Dim Suma, Generados As Integer
    Dim DuplicaRutas As String

    Sub SQLCombo()
        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Regiones as REG " & _
                    "ORDER BY REG.nombre_region"

        CadenaSQL = "SELECT DISTINCT * FROM Cadenas_Tiendas"
    End Sub

    Private Sub VerDatos()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT * FROM AS_Procesos " & _
                                               "WHERE id_proceso=" & SeleccionIDProceso & "")
        If Tabla.Rows.Count > 0 Then
            cmbTipoProceso.SelectedValue = Tabla.Rows(0)("tipo_proceso")
            txtNombreProceso.Text = Tabla.Rows(0)("nombre_proceso")
            txtNotas.Text = Tabla.Rows(0)("notas")
            txtFechaInicio.Text = Tabla.Rows(0)("fecha_inicio")
            txtFechaFin.Text = Tabla.Rows(0)("fecha_fin")
            cmbCadena.SelectedValue = Tabla.Rows(0)("id_cadena")
        End If

        tabla.Dispose()
    End Sub

    Public Function CargarProcesos(ByVal TipoProceso As String) As Integer
        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT PRO.id_proceso,PRO.nombre_proceso, TPRO.nombre_tipoproceso, PRO.fecha_inicio, PRO.fecha_fin " & _
                    "FROM AS_Procesos as PRO " & _
                    "INNER JOIN AS_Tipo_Procesos as TPRO ON TPRO.tipo_proceso= PRO.tipo_proceso " & _
                    "WHERE PRO.tipo_proceso =" & TipoProceso & " " & _
                    "ORDER BY PRO.id_proceso", Me.gridProcesos)

        pnlConsulta.Visible = True
        pnlNuevo.Visible = False
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Dim Cadena, Region As Integer

        If cmbCadena.SelectedValue = "" Then
            Cadena = 0 : Else
            Cadena = cmbCadena.SelectedValue : End If

        If cmbRegion.SelectedValue = "" Then
            Region = cmbRegion.SelectedValue : Else
            Region = 0 : End If

        Guardar(lblIDProceso.Text, cmbTipoProceso.SelectedValue, txtNombreProceso.Text, txtNotas.Text, _
                txtFechaInicio.Text, txtFechaFin.Text, Cadena, Region)
        Borrar()
        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
    End Sub

    Public Function Guardar(ByVal IDProceso As String, ByVal TipoProceso As Integer, _
                            ByVal NombreProceso As String, ByVal Notas As String, ByVal FechaInicio As String, _
                            ByVal FechaFin As String, ByVal Cadena As Integer, ByVal Region As Integer) As Integer
        Dim fecha_inicio As String = ISODates.Dates.SQLServerDate(CDate(FechaInicio))
        Dim fecha_fin As String = ISODates.Dates.SQLServerDate(CDate(FechaFin))

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT * From AS_Procesos " & _
                                               "WHERE id_proceso=" & IDProceso & "")
        If Tabla.Rows.Count = 1 Then
            BD.Execute(ConexionMars.localSqlServer, "UPDATE AS_Procesos " & _
                        "SET tipo_proceso=" & TipoProceso & ", nombre_proceso='" & NombreProceso & "', " & _
                        "notas='" & Notas & "', fecha_inicio='" & fecha_inicio & "', " & _
                        "fecha_fin='" & fecha_fin & "',id_cadena=" & Cadena & ", " & _
                        "id_region=" & Region & " WHERE id_proceso=" & IDProceso & " ")

            lblAviso.Text = "LOS CAMBIOS DEL USUARIO SE REALIZARON CORRECTAMENTE"
        Else
            BD.Execute(ConexionMars.localSqlServer, _
                       "INSERT INTO AS_Procesos" & _
                       "(tipo_proceso, nombre_proceso, notas,fecha_inicio, fecha_fin,id_cadena,id_region) " & _
                       "VALUES(" & TipoProceso & ", '" & NombreProceso & "','" & Notas & "', " & _
                       "'" & fecha_inicio & "', '" & fecha_fin & "'," & Cadena & "," & Region & ")")

            lblAviso.Text = "LA INFORMACIÓN SE GUARDÓ CORRECTAMENTE"
        End If

        pnlNuevo.Visible = False
        CargarProcesos(TipoProceso)
        CrearEventos()

        If Generados >= 1 Then
            lblAviso.Text = "SE CARGO EL PROCESO SELECCIONADO PARA " & Generados & " PROMOTORES EXITOSAMENTE."
        End If

    End Function

    Sub Borrar()
        cmbTipoProceso.SelectedValue = ""
        txtNombreProceso.Text = ""
        txtNotas.Text = ""
        txtFechaInicio.Text = ""
        txtFechaFin.Text = ""
        cmbCadena.SelectedValue = 0
        lblAviso.Text = ""
    End Sub

    Protected Sub lnkNuevo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkNuevo.Click
        pnlNuevo.Visible = True
        pnlConsulta.Visible = False
        cmbTipoProceso.Focus()
        Borrar()
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Borrar()
        pnlNuevo.Visible = False
        pnlConsulta.Visible = False
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionMars.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
            Combo.LlenaDrop(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, "select * FROM AS_Tipo_Procesos", "nombre_tipoproceso", "tipo_proceso", cmbTipoProceso)
        End If
    End Sub

    Private Sub gridProcesos_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gridProcesos.RowEditing
        SeleccionIDProceso = gridProcesos.Rows(e.NewEditIndex).Cells(1).Text
        lblIDProceso.Text = gridProcesos.Rows(e.NewEditIndex).Cells(1).Text

        If SeleccionIDProceso = "" Then
            e.Cancel = True
            lblAviso.Text = "Error al intentar editar"
        Else
            pnlNuevo.Visible = True
            pnlConsulta.Visible = False
            cmbTipoProceso.Focus()
            VerDatos()
        End If
    End Sub

    Private Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridProcesos.Columns(1).Visible = False
    End Sub

    Protected Sub lnkImplementaciones_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkImplementaciones.Click
        CargarProcesos(2)
    End Sub

    Private Sub lnkAlineaciones_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAlineaciones.Click
        CargarProcesos(1)
    End Sub

    Sub CrearEventos()
        Generados = 0
        DuplicaRutas = "DUPLICAR"

        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT RUT.id_usuario, PRO.id_proceso " & _
                    "FROM AS_Procesos as PRO  " & _
                    "INNER JOIN AS_Tipo_Procesos as TPRO ON TPRO.tipo_proceso= PRO.tipo_proceso  " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = PRO.id_cadena  " & _
                    "INNER JOIN (SELECT DISTINCT RUT.id_usuario, 0 as id_cadena  " & _
                    "FROM AS_CatRutas as RUT  " & _
                    "INNER JOIN AS_Tiendas as TI ON TI.id_tienda=RUT.id_tienda  " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena=TI.id_cadena  " & _
                    "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario = RUT.id_usuario)RUT  " & _
                    "ON RUT.id_cadena= PRO.id_cadena  " & _
                    "WHERE PRO.id_proceso=" & lblIDProceso.Text & " AND PRO.id_cadena=0 AND PRO.id_region=0 " & _
                    "UNION ALL " & _
                    "SELECT RUT.id_usuario, PRO.id_proceso " & _
                    "FROM AS_Procesos as PRO  " & _
                    "INNER JOIN AS_Tipo_Procesos as TPRO ON TPRO.tipo_proceso= PRO.tipo_proceso  " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = PRO.id_cadena  " & _
                    "INNER JOIN (SELECT DISTINCT RUT.id_usuario, CAD.id_cadena, TI.id_region  " & _
                    "FROM AS_CatRutas as RUT  " & _
                    "INNER JOIN AS_Tiendas as TI ON TI.id_tienda=RUT.id_tienda  " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena=TI.id_cadena  " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario = RUT.id_usuario)RUT  " & _
                    "ON RUT.id_cadena= PRO.id_cadena  " & _
                    "WHERE PRO.id_proceso=" & lblIDProceso.Text & " AND PRO.id_cadena<>0 AND PRO.id_region<>0 " & _
                    "UNION ALL " & _
                    "SELECT RUT.id_usuario, PRO.id_proceso " & _
                    "FROM AS_Procesos as PRO  " & _
                    "INNER JOIN AS_Tipo_Procesos as TPRO ON TPRO.tipo_proceso= PRO.tipo_proceso  " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = PRO.id_cadena  " & _
                    "INNER JOIN (SELECT DISTINCT RUT.id_usuario, CAD.id_cadena  " & _
                    "FROM AS_CatRutas as RUT  " & _
                    "INNER JOIN AS_Tiendas as TI ON TI.id_tienda=RUT.id_tienda  " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena=TI.id_cadena  " & _
                    "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario = RUT.id_usuario)RUT  " & _
                    "ON RUT.id_cadena= PRO.id_cadena  " & _
                    "WHERE PRO.id_proceso=" & lblIDProceso.Text & " AND PRO.id_cadena<>0 AND PRO.id_region=0 " & _
                    "UNION ALL " & _
                    "SELECT RUT.id_usuario, PRO.id_proceso " & _
                    "FROM AS_Procesos as PRO  " & _
                    "INNER JOIN AS_Tipo_Procesos as TPRO ON TPRO.tipo_proceso= PRO.tipo_proceso  " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = PRO.id_cadena  " & _
                    "INNER JOIN (SELECT DISTINCT RUT.id_usuario, TI.id_region  " & _
                    "FROM AS_CatRutas as RUT  " & _
                    "INNER JOIN AS_Tiendas as TI ON TI.id_tienda=RUT.id_tienda  " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario = RUT.id_usuario)RUT  " & _
                    "ON RUT.id_region= PRO.id_region  " & _
                    "WHERE PRO.id_proceso=" & lblIDProceso.Text & " AND PRO.id_region<>0 AND PRO.id_cadena=0 " & _
                    "ORDER BY id_usuario", Me.gridRuta)

        DuplicaRutas = ""
    End Sub

    Private Sub gridRuta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRuta.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            If DuplicaRutas = "DUPLICAR" Then
                UsuarioSel = e.Row.Cells(0).Text
                TiendaSel = e.Row.Cells(1).Text
                Duplicalo(lblIDProceso.Text, UsuarioSel)
            End If
        End If
    End Sub

    Public Function Duplicalo(ByVal IDProceso As String, ByVal IDUsuario As String) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT * FROM AS_Procesos_Rutas_Eventos " & _
                                               "WHERE id_proceso=" & IDProceso & " " & _
                                               "AND id_usuario='" & IDUsuario & "'")
        If Tabla.Rows.Count = 1 Then
            lblAviso.Text = "NO SE CARGO NINGUN PROCESO PARA LOS PROMOTORES"
            Exit Function
        Else
            BD.Execute(ConexionMars.localSqlServer, _
                       "INSERT INTO AS_Procesos_Rutas_Eventos (id_proceso, id_usuario) " & _
                       "VALUES(" & IDProceso & ",'" & IDUsuario & "')")
            Generados = Generados + 1
        End If

        Tabla.Dispose()
    End Function
End Class