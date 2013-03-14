Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class AdminPromotoresMars
    Inherits System.Web.UI.Page

    Dim RegionSQL, PlazaSQL, SupervisorSQL, NoRegionSQL, EjecutivoSQL As String
    Dim RegionSel, EjecutivoShelbySel As String

    Sub SQLCombo()
        If Tipo_usuario = 12 Then
            EjecutivoShelbySel = "AND Shelby='" & HttpContext.Current.User.Identity.Name & "'" : End If

        RegionSel = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM View_Usuario_AS as REG " & _
                    "WHERE REG.id_region<>0 " & _
                    " " + EjecutivoShelbySel + " " & _
                    "ORDER BY REG.nombre_region"

        PlazaSQL = "SELECT DISTINCT ciudad FROM View_Usuario_AS " & _
                    "WHERE ciudad <>'' " & _
                    " " + EjecutivoShelbySel + " " & _
                    " " + RegionSel + ""

        SupervisorSQL = "SELECT DISTINCT US.id_supervisor, US.Supervisor " & _
                    "FROM View_Usuario_AS as US " & _
                    "WHERE id_supervisor<>'' " & _
                    " " + EjecutivoShelbySel + RegionSel + " " & _
                    "ORDER BY US.Supervisor"

        NoRegionSQL = "SELECT DISTINCT US.region_mars, US.Ejecutivo " & _
                    "FROM View_Usuario_AS as US " & _
                    "WHERE region_mars<>'' " & _
                    " " + EjecutivoShelbySel + RegionSel + " " & _
                    "ORDER BY US.Ejecutivo"

        EjecutivoSQL = "SELECT DISTINCT US.Shelby, US.ShelbyCiudad  " & _
                    "FROM View_Usuario_AS as US " & _
                    "where US.Shelby<>'' " & _
                    " " + EjecutivoShelbySel + RegionSel + " " & _
                    "ORDER BY US.Shelby"
    End Sub

    Public Function EjecutivoShelby(ByVal Plaza As String) As Integer
        If Tipo_usuario = 12 Then
            Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                                   "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                                    "FROM View_Usuario_AS as REG " & _
                                    "WHERE REG.id_region<>0 " & _
                                    "AND Shelby='" + Plaza + "' " & _
                                    "ORDER BY REG.nombre_region")
            If Tabla.Rows.Count > 0 Then
                cmbRegion.SelectedValue = Tabla.Rows(0)("id_region")
            End If

            tabla.Dispose()
        End If
    End Function

    Private Sub VerDatos(ByVal Usuario As String)
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT US.id_usuario, US.nombre, US.fecha_ingreso,US.nombre_region,US.ShelbyCiudad, " & _
                                  "US.id_usuario, US.id_supervisor, US.region_mars, US.shelby, " & _
                                  "CAL.modulo_1, CAl.modulo_2, CAL.modulo_3, CAL.modulo_4 " & _
                                  "FROM View_Usuario_AS as US " & _
                                  "INNER JOIN AS_Calificaciones as CAL ON CAL.id_usuario=US.id_usuario " & _
                                  "WHERE US.id_usuario='" & Usuario & "'")
        If Tabla.Rows.Count > 0 Then
            lblIDUsuario.Text = Tabla.Rows(0)("id_usuario")
            txtNombrePromotor.Text = Tabla.Rows(0)("nombre")

            If Tabla.Rows(0)("fecha_ingreso") IsNot DBNull.Value Then
                txtFechaIngreso.Text = Tabla.Rows(0)("fecha_ingreso") : Else
                txtFechaIngreso.Text = "" : End If

            lblIDRegion.Text = Tabla.Rows(0)("nombre_region")
            cmbSupervisor.SelectedValue = Tabla.Rows(0)("id_supervisor")
            cmbNoRegion.SelectedValue = Tabla.Rows(0)("region_mars")
            cmbEjecutivo.SelectedValue = Tabla.Rows(0)("ShelbyCiudad")

            Dim Modulo1, Modulo2, Modulo3, Modulo4 As Double
            If Tabla.Rows(0)("modulo_1") IsNot DBNull.Value Then
                txtCalificacion1.Text = Tabla.Rows(0)("modulo_1")
                Modulo1 = Tabla.Rows(0)("modulo_1") : Else
                txtCalificacion1.Text = "" : End If
            If Tabla.Rows(0)("modulo_2") IsNot DBNull.Value Then
                txtCalificacion2.Text = Tabla.Rows(0)("modulo_2")
                Modulo2 = Tabla.Rows(0)("modulo_2") : Else
                txtCalificacion2.Text = "" : End If
            If Tabla.Rows(0)("modulo_3") IsNot DBNull.Value Then
                txtCalificacion3.Text = Tabla.Rows(0)("modulo_3")
                Modulo3 = Tabla.Rows(0)("modulo_3") : Else
                txtCalificacion3.Text = "" : End If
            If Tabla.Rows(0)("modulo_4") IsNot DBNull.Value Then
                txtCalificacion4.Text = Tabla.Rows(0)("modulo_4")
                Modulo4 = Tabla.Rows(0)("modulo_4") : Else
                txtCalificacion4.Text = "" : End If

            lblTotal.Text = Modulo1 + Modulo2 + Modulo3 + Modulo4
        End If

        tabla.Dispose()
    End Sub

    Sub CargarUsuarios()
        Dim Shelby As String
        RegionSQL = Acciones.Slc.cmb("US.id_region", cmbRegion.SelectedValue)
        PlazaSQL = Acciones.Slc.cmb("US.ciudad", cmbPlaza.SelectedValue)
        Shelby = Acciones.Slc.cmb("Shelby", HttpContext.Current.User.Identity.Name)

        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT US.nombre_region,US.id_usuario,US.nombre, US.fecha_ingreso  " & _
                    "FROM View_Usuario_AS as US " & _
                    "WHERE US.id_tipo=1  " + RegionSQL + PlazaSQL + "" & _
                    " " + Shelby + " " & _
                    "ORDER BY US.id_usuario ", Me.gridPromotor)

        pnlConsulta.Visible = True
    End Sub

    Public Function Guardar(ByVal IDUsuario As String, ByVal NombrePromotor As String, _
                            ByVal FechaIngreso As String, ByVal RegionMars As String, _
                            ByVal Supervisor As String, ByVal Ejecutivo As String) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT * From Usuarios WHERE id_usuario='" & IDUsuario & "'")
        If Tabla.Rows.Count = 1 Then
            Dim cnn As New SqlConnection(ConexionMars.localSqlServer)
            cnn.Open()

            Dim SQLEditar As New SqlCommand("UPDATE Usuarios " & _
                        "SET nombre= @nombre, fecha_ingreso= @fecha_ingreso " & _
                        "WHERE id_usuario =@id_usuario ", cnn)
            SQLEditar.Parameters.AddWithValue("@id_usuario", IDUsuario)
            SQLEditar.Parameters.AddWithValue("@nombre", NombrePromotor)
            If FechaIngreso = "" Then
                SQLEditar.Parameters.AddWithValue("@fecha_ingreso", DBNull.Value)
            Else
                SQLEditar.Parameters.AddWithValue("@fecha_ingreso", ISODates.Dates.SQLServerDate(CDate(FechaIngreso)))
            End If
            SQLEditar.ExecuteNonQuery()
            SQLEditar.Dispose()

            cnn.Dispose()
            cnn.Close()
        End If
        Tabla.Dispose()

        Dim Tabla2 As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT * From Usuarios_Relacion WHERE id_usuario='" & IDUsuario & "'")
        If Tabla2.Rows.Count = 1 Then
            BD.Execute(ConexionMars.localSqlServer, _
                       "UPDATE Usuarios_Relacion " & _
                        "SET region_mars=" & RegionMars & ", id_supervisor='" & Supervisor & "', " & _
                        "ejecutivo='" & Ejecutivo & "' WHERE id_usuario='" & IDUsuario & "' ")

            lblAviso.Text = "LOS CAMBIOS DEL USUARIO SE REALIZARON CORRECTAMENTE"
        End If
        Tabla2.Dispose()

        Dim Tabla3 As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                                "SELECT * From AS_Calificaciones WHERE id_usuario='" & IDUsuario & "'")

        If Tabla3.Rows.Count = 1 Then
            Dim cnn As New SqlConnection(ConexionMars.localSqlServer)
            cnn.Open()

            Dim SQLEditaCal As New SqlCommand("UPDATE AS_Calificaciones " & _
                                   "SET modulo_1=@modulo_1, modulo_2=@modulo_2, " & _
                                   "modulo_3=@modulo_3, modulo_4=@modulo_4 " & _
                                   "WHERE id_usuario=@id_usuario ", cnn)
            SQLEditaCal.Parameters.AddWithValue("@id_usuario", IDUsuario)

            If txtCalificacion1.Text = "" Then
                SQLEditaCal.Parameters.AddWithValue("@modulo_1", DBNull.Value) : Else
                SQLEditaCal.Parameters.AddWithValue("@modulo_1", txtCalificacion1.Text) : End If
            If txtCalificacion2.Text = "" Then
                SQLEditaCal.Parameters.AddWithValue("@modulo_2", DBNull.Value) : Else
                SQLEditaCal.Parameters.AddWithValue("@modulo_2", txtCalificacion2.Text) : End If
            If txtCalificacion3.Text = "" Then
                SQLEditaCal.Parameters.AddWithValue("@modulo_3", DBNull.Value) : Else
                SQLEditaCal.Parameters.AddWithValue("@modulo_3", txtCalificacion3.Text) : End If
            If txtCalificacion4.Text = "" Then
                SQLEditaCal.Parameters.AddWithValue("@modulo_4", DBNull.Value) : Else
                SQLEditaCal.Parameters.AddWithValue("@modulo_4", txtCalificacion4.Text) : End If

            SQLEditaCal.ExecuteNonQuery()
            SQLEditaCal.Dispose()

            cnn.Dispose()
            cnn.Close()
        End If
        Tabla3.Dispose()

        pnlNuevo.Visible = False
        CargarUsuarios()
    End Function

    Sub Borrar()
        lblIDUsuario.Text = ""
        txtNombrePromotor.Text = ""
        txtFechaIngreso.Text = ""
        lblIDRegion.Text = ""
        cmbSupervisor.SelectedValue = ""
        cmbNoRegion.SelectedValue = ""
        cmbEjecutivo.SelectedValue = ""

        txtCalificacion1.Text = ""
        txtCalificacion2.Text = ""
        txtCalificacion3.Text = ""
        txtCalificacion4.Text = ""

        lblAviso.Text = ""
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Borrar()
        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionMars.localSqlServer, HttpContext.Current.User.Identity.Name)

            SQLCombo()

            Combo.LlenaDrop(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, PlazaSQL, "ciudad", "ciudad", cmbPlaza)

            Combo.LlenaDrop(ConexionMars.localSqlServer, SupervisorSQL, "supervisor", "id_supervisor", cmbSupervisor)
            Combo.LlenaDrop(ConexionMars.localSqlServer, NoRegionSQL, "ejecutivo", "region_mars", cmbNoRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, EjecutivoSQL, "Shelby", "ShelbyCiudad", cmbEjecutivo)

            EjecutivoShelby(HttpContext.Current.User.Identity.Name)

            CargarUsuarios()
        End If
    End Sub

    Protected Sub lnkConsultas_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkConsultas.Click
        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
        Borrar()
    End Sub

    Private Sub gridPeriodo_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gridPromotor.RowEditing
        If gridPromotor.Rows(e.NewEditIndex).Cells(1).Text = "" Then
            e.Cancel = True
            lblAviso.Text = "Error al intentar editar"
        Else
            pnlNuevo.Visible = True
            pnlConsulta.Visible = False
            txtNombrePromotor.Focus()
            VerDatos(gridPromotor.Rows(e.NewEditIndex).Cells(1).Text)
        End If
    End Sub

    Private Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbRegion.SelectedIndexChanged
        'If Tipo_usuario <> 12 Then
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, PlazaSQL, "ciudad", "ciudad", cmbPlaza)
        CargarUsuarios()
        'Else
        'EjecutivoShelby(HttpContext.Current.User.Identity.Name)
        'End If
    End Sub

    Private Sub cmbPlaza_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPlaza.SelectedIndexChanged
        SQLCombo()

        CargarUsuarios()
    End Sub

    Private Sub btnGuardar_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Guardar(lblIDUsuario.Text, txtNombrePromotor.Text, txtFechaIngreso.Text, _
                cmbNoRegion.SelectedValue, cmbSupervisor.SelectedValue, cmbEjecutivo.SelectedValue)
        Borrar()
        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
    End Sub
End Class