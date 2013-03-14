Imports System.Data.SqlClient
Imports System.Data
Imports ISODates.Dates

Partial Public Class AdminCapacitacionMars
    Inherits System.Web.UI.Page

    Dim IDModulo As String
    Dim varPromotor, FolioAct, Nombre, Region As String
    Dim RegionSel, RegionSQL, EstadoSel, EstadoSQL, EjecutivoCuentaSel, EjecutivoCuentaSQL, EjecutivoSel, EjecutivoSQL, SupervisorSel, SupervisorSQL As String
    Dim SeleccionIDPromotor, SeleccionIDSupervisor, SeleccionIDEjecutivo, IDEjecutivo As String
    Dim FiltroRegion As String

    Sub FiltrosCombos()
        FiltroRegion = Acciones.Slc.cmb("UMARS.id_ejecutivo", Region)

        RegionSQL = "select distinct UMARS.id_region,REG.nombre_region, UMARS.id_ejecutivo  from " & _
                    "Usuarios as UMARS " & _
                    "INNER JOIN Regiones as REG " & _
                    "ON UMARS.id_region = REG.id_region " & _
                    " " + FiltroRegion + " " & _
                    "ORDER BY REG.nombre_region"

        EstadoSQL = "select distinct UMARS.id_estado,EST.nombre_estado, UMARS.id_ejecutivo  from " & _
                    "Usuarios as UMARS " & _
                    "INNER JOIN Estados as EST " & _
                    "ON UMARS.id_estado = EST.id_estado " & _
                    " " + FiltroRegion + " " & _
                    "ORDER BY EST.nombre_estado"

        EjecutivoCuentaSQL = "select distinct UMARS.id_ejecutivo, U.nombre, U.nombre as NombreEjecutivoCuenta " & _
                    "from Usuarios as UMARS " & _
                    "INNER JOIN Usuarios as U ON UMARS.id_ejecutivo= U.no_region " & _
                    " " + FiltroRegion + " " & _
                    "ORDER BY UMARS.id_ejecutivo"

        EjecutivoSQL = "select distinct UMARS.no_region, U.nombre, UMARS.no_region + '- ' + U.nombre as NombreEjecutivo, id_ejecutivo " & _
                    "from Usuarios as UMARS  " & _
                    "INNER JOIN Usuarios as U ON UMARS.no_region= U.no_region " & _
                    " " + FiltroRegion + " " & _
                    "ORDER BY UMARS.no_region"

        SupervisorSQL = "select distinct UMARS.id_supervisor, U.nombre, UMARS.id_supervisor + '-' + U.nombre as NombreSupervisor, UMARS.id_ejecutivo " & _
                    "from Usuarios as UMARS " & _
                    "INNER JOIN Usuarios as U ON UMARS.id_supervisor= U.id_usuario " & _
                    " " + FiltroRegion + " " & _
                    "ORDER BY UMARS.id_supervisor"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            FiltrosCombos()

            Combo.LlenaDrop(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, EstadoSQL, "nombre_estado", "id_estado", cmbEstado)
            Combo.LlenaDrop(ConexionMars.localSqlServer, EjecutivoCuentaSQL, "NombreEjecutivoCuenta", "id_ejecutivo", cmbEjecutivoCuenta)
            Combo.LlenaDrop(ConexionMars.localSqlServer, EjecutivoSQL, "NombreEjecutivo", "no_region", cmbEjecutivo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, SupervisorSQL, "NombreSupervisor", "id_supervisor", cmbSupervisor)

            Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct UM.no_region, U.nombre from Usuarios as UM INNER JOIN Usuarios as U ON UM.no_region= U.no_region order by UM.no_region", "no_region", "no_region", cmbLoginEjecutivo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct UM.id_supervisor, U.nombre, UM.id_supervisor + '-' + U.nombre as NombreSupervisor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_supervisor= U.id_usuario order by UM.id_supervisor", "id_supervisor", "id_supervisor", cmbLoginSupervisor)

            Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct * from Estados ", "nombre_estado", "id_estado", cmbEstadoPromotor)
            Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct * from Regiones ", "nombre_region", "id_region", cmbRegionPromotor)
            Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct id_ejecutivo from Usuarios", "id_ejecutivo", "id_ejecutivo", cmbLoginEjecutivoCuenta)
        End If
    End Sub

    Protected Sub cmbEjecutivo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbEjecutivo.SelectedIndexChanged
        Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct UM.id_supervisor, U.nombre, UM.id_supervisor + '-' + U.nombre as NombreSupervisor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_supervisor= U.id_usuario WHERE UM.no_region ='" & cmbEjecutivo.SelectedValue & "' order by UM.id_supervisor", "NombreSupervisor", "id_supervisor", cmbSupervisor)

        If cmbEjecutivo.Text = "" Then
            FiltrosCombos()
            Combo.LlenaDrop(ConexionMars.localSqlServer, SupervisorSQL, "NombreSupervisor", "id_supervisor", cmbSupervisor)
        End If

        btnFiltrar.Enabled = True
    End Sub

    Protected Sub cmbEjecutivoCuenta_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbEjecutivoCuenta.SelectedIndexChanged
        Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct UM.no_region, U.nombre, UM.no_region + '- ' + U.nombre as NombreEjecutivo from Usuarios as UM INNER JOIN Usuarios as U ON UM.no_region= U.no_region WHERE UM.id_ejecutivo ='" & cmbEjecutivoCuenta.SelectedValue & "' order by U.nombre", "NombreEjecutivo", "no_region", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct UM.id_supervisor, U.nombre, UM.id_supervisor + '-' + U.nombre as NombreSupervisor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_supervisor= U.id_usuario WHERE UM.id_ejecutivo ='" & cmbEjecutivoCuenta.SelectedValue & "' order by UM.id_supervisor", "NombreSupervisor", "id_supervisor", cmbSupervisor)

        If cmbEjecutivoCuenta.Text = "" Then
            FiltrosCombos()
            Combo.LlenaDrop(ConexionMars.localSqlServer, EjecutivoSQL, "NombreEjecutivo", "no_region", cmbEjecutivo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, SupervisorSQL, "NombreSupervisor", "id_supervisor", cmbSupervisor)
        End If

        btnFiltrar.Enabled = True
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct UM.id_estado, ES.nombre_estado from Estados as ES INNER JOIN Usuarios as UM ON UM.id_estado = ES.id_estado WHERE UM.id_region ='" & cmbRegion.SelectedValue & "' order by nombre_estado", "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct UM.id_ejecutivo, U.nombre, U.nombre as NombreEjecutivoCuenta from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_ejecutivo= U.no_region WHERE UM.id_region ='" & cmbRegion.SelectedValue & "' order by UM.id_ejecutivo", "NombreEjecutivoCuenta", "id_ejecutivo", cmbEjecutivoCuenta)
        Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct UM.no_region, U.nombre, UM.no_region + '- ' + U.nombre as NombreEjecutivo from Usuarios as UM INNER JOIN Usuarios as U ON UM.no_region= U.no_region WHERE UM.id_region ='" & cmbRegion.SelectedValue & "' order by U.nombre", "NombreEjecutivo", "no_region", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct UM.id_supervisor, U.nombre, UM.id_supervisor + '-' + U.nombre as NombreSupervisor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_supervisor= U.id_usuario WHERE UM.id_region ='" & cmbRegion.SelectedValue & "' order by UM.id_supervisor", "NombreSupervisor", "id_supervisor", cmbSupervisor)

        If cmbRegion.SelectedValue = "" Then
            FiltrosCombos()
            Combo.LlenaDrop(ConexionMars.localSqlServer, EstadoSQL, "nombre_estado", "id_estado", cmbEstado)
            Combo.LlenaDrop(ConexionMars.localSqlServer, EjecutivoCuentaSQL, "NombreEjecutivoCuenta", "id_ejecutivo", cmbEjecutivoCuenta)
            Combo.LlenaDrop(ConexionMars.localSqlServer, EjecutivoSQL, "NombreEjecutivo", "no_region", cmbEjecutivo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, SupervisorSQL, "NombreSupervisor", "id_supervisor", cmbSupervisor)
        End If

        btnFiltrar.Enabled = True
    End Sub

    Protected Sub cmbEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbEstado.SelectedIndexChanged
        Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct UM.id_ejecutivo, U.nombre, U.nombre as NombreEjecutivoCuenta from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_ejecutivo= U.no_region WHERE UM.id_estado ='" & cmbEstado.SelectedValue & "' order by UM.id_ejecutivo", "NombreEjecutivoCuenta", "id_ejecutivo", cmbEjecutivoCuenta)
        Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct UM.no_region, U.nombre, UM.no_region + '- ' + U.nombre as NombreEjecutivo from Usuarios as UM INNER JOIN Usuarios as U ON UM.no_region= U.no_region WHERE UM.id_estado ='" & cmbEstado.SelectedValue & "' order by U.nombre", "NombreEjecutivo", "no_region", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct UM.id_supervisor, U.nombre, UM.id_supervisor + '-' + U.nombre as NombreSupervisor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_supervisor= U.id_usuario WHERE UM.id_estado ='" & cmbEstado.SelectedValue & "' order by UM.id_supervisor", "NombreSupervisor", "id_supervisor", cmbSupervisor)

        If cmbRegion.SelectedValue = "" And cmbEstado.Text = "" Then
            Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct UM.id_ejecutivo, U.nombre, U.nombre as NombreEjecutivoCuenta from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_ejecutivo= U.no_region WHERE UM.id_estado ='" & cmbEstado.SelectedValue & "' AND UM.id_region ='" & cmbRegion.SelectedValue & "' order by UM.id_ejecutivo", "NombreEjecutivoCuenta", "id_ejecutivo", cmbEjecutivoCuenta)
            Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct UM.no_region, U.nombre, UM.no_region + '- ' + U.nombre as NombreEjecutivo from Usuarios as UM INNER JOIN Usuarios as U ON UM.no_region= U.no_region WHERE UM.id_estado ='" & cmbEstado.SelectedValue & "' AND UM.id_region ='" & cmbRegion.SelectedValue & "' order by U.nombre", "NombreEjecutivo", "no_region", cmbEjecutivo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct UM.id_supervisor, U.nombre, UM.id_supervisor + '-' + U.nombre as NombreSupervisor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_supervisor= U.id_usuario WHERE UM.id_estado ='" & cmbEstado.SelectedValue & "' AND UM.id_region ='" & cmbRegion.SelectedValue & "' order by UM.id_supervisor", "NombreSupervisor", "id_supervisor", cmbSupervisor)
        End If

        If cmbEstado.Text = "" Then
            FiltrosCombos()
            Combo.LlenaDrop(ConexionMars.localSqlServer, EjecutivoCuentaSQL, "NombreEjecutivoCuenta", "id_ejecutivo", cmbEjecutivoCuenta)
            Combo.LlenaDrop(ConexionMars.localSqlServer, EjecutivoSQL, "NombreEjecutivo", "no_region", cmbEjecutivo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, SupervisorSQL, "NombreSupervisor", "id_supervisor", cmbSupervisor)

            If cmbRegion.SelectedValue = "" Then
                Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct UM.id_ejecutivo, U.nombre, U.nombre as NombreEjecutivoCuenta from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_ejecutivo= U.no_region WHERE UM.id_region ='" & cmbRegion.SelectedValue & "' order by UM.id_ejecutivo", "NombreEjecutivoCuenta", "id_ejecutivo", cmbEjecutivoCuenta)
                Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct UM.no_region, U.nombre, UM.no_region + '- ' + U.nombre as NombreEjecutivo from Usuarios as UM INNER JOIN Usuarios as U ON UM.no_region= U.no_region WHERE UM.id_region ='" & cmbRegion.SelectedValue & "' order by U.nombre", "NombreEjecutivo", "no_region", cmbEjecutivo)
                Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct UM.id_supervisor, U.nombre, UM.id_supervisor + '-' + U.nombre as NombreSupervisor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_supervisor= U.id_usuario WHERE UM.id_region ='" & cmbRegion.SelectedValue & "' order by UM.id_supervisor", "NombreSupervisor", "id_supervisor", cmbSupervisor)
            End If
        End If

        btnFiltrar.Enabled = True
    End Sub

    Sub ValoresCombos()
        RegionSel = cmbRegion.Text
        EstadoSel = cmbEstado.Text
        EjecutivoCuentaSel = cmbEjecutivoCuenta.Text
        EjecutivoSel = cmbEjecutivo.Text
        SupervisorSel = cmbSupervisor.Text
    End Sub

    Protected Sub btnFiltrar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFiltrar.Click
        ValoresCombos()

        RegionSQL = Acciones.Slc.cmb("Promotor.id_region", RegionSel)
        EstadoSQL = Acciones.Slc.cmb("Promotor.id_estado", EstadoSel)
        EjecutivoCuentaSQL = Acciones.Slc.cmb("Promotor.id_ejecutivo", EjecutivoCuentaSel)
        EjecutivoSQL = Acciones.Slc.cmb("Promotor.no_region", EjecutivoSel)
        SupervisorSQL = Acciones.Slc.cmb("Promotor.id_supervisor", SupervisorSel)

        CargaGrilla(ConexionMars.localSqlServer, "select distinct * from " & _
                            "(select distinct US.id_usuario, US.nombre as NombrePromotor, USM.id_supervisor,USM.id_ejecutivo, USM.no_region, USM.id_region, USM.id_estado from Usuarios as US " & _
                            "INNER JOIN Usuarios as USM " & _
                            "ON US.id_usuario=USM.id_promotor) as Promotor, " & _
                            "(select distinct US.id_usuario, CASE USM.activo when 1 then 'Si' when 0 then 'No' end as Activo, US.nombre as NombreSupervisor, USM.id_supervisor from Usuarios as US " & _
                            "INNER JOIN Usuarios as USM " & _
                            "ON US.id_usuario=USM.id_supervisor)as Supervisor, " & _
                            "(select distinct US.id_usuario as id_ejecutivo, US.nombre as NombreEjecutivo, USM.no_region from Usuarios as US " & _
                            "INNER JOIN Usuarios as USM " & _
                            "ON US.no_region=USM.no_region) as Ejecutivo, " & _
                            "(select * from Regiones) as Region, " & _
                            "(select * from Estados) as Estado " & _
                            "WHERE(Promotor.id_supervisor = Supervisor.id_supervisor) " & _
                            "AND Promotor.no_region= Ejecutivo.no_region " & _
                            "AND Promotor.id_region= Region.id_region " & _
                            "AND Promotor.id_estado= Estado.id_estado " & _
                            " " + RegionSQL + " " & _
                            " " + EstadoSQL + " " & _
                            " " + EjecutivoCuentaSQL + " " & _
                            " " + EjecutivoSQL + " " & _
                            " " + SupervisorSQL + " " & _
                            "ORDER BY Promotor.id_usuario", Me.gridUsuarios)
    End Sub

    Private Sub gridUsuarios_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gridUsuarios.RowEditing
        SeleccionIDPromotor = gridUsuarios.Rows(e.NewEditIndex).Cells(1).Text
        Busca()

        pnlPromotor.Visible = True
        pnlFiltro.Visible = False
    End Sub

    Private Sub Busca()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select DISTINCT MARSUS.activo, MARSUS.id_promotor,MARSUS.id_region, MARSUS.id_estado, Promo.nombre as Promotor, CAL.fecha_ingreso, Promo.fecha_nacimiento, " & _
                            "MARSUS.id_supervisor, Supe.nombre as Supervisor, MARSUS.id_ejecutivo, Region.nombre as Ejecutivo, MARSUS.no_region from " & _
                            "Usuarios as MARSUS " & _
                            "INNER JOIN Cap_Calificaciones as CAL " & _
                            "ON CAL.id_usuario = MARSUS.id_promotor " & _
                            "INNER JOIN (select * from Usuarios) AS Promo " & _
                            "ON MARSUS.id_promotor= Promo.id_usuario " & _
                            "INNER JOIN (select * from Usuarios) AS Supe " & _
                            "ON MARSUS.id_supervisor= Supe.id_usuario " & _
                            "INNER JOIN (select * from Usuarios) AS Region " & _
                            "ON MARSUS.no_region= Region.no_region " & _
                            "WHERE MARSUS.id_promotor= '" & SeleccionIDPromotor & "'")

        If tabla.Rows.Count > 0 Then
            txtLoginPromotor.Text = tabla.Rows(0)("id_promotor")
            txtNombrePromotor.Text = tabla.Rows(0)("promotor")
            If tabla.Rows(0)("fecha_ingreso") Is DBNull.Value Then
                txtFechaIngresoPromotor.Text = ""
            Else
                txtFechaIngresoPromotor.Text = Format(tabla.Rows(0)("fecha_ingreso"), "dd/MMM/yyyy")
            End If
            If tabla.Rows(0)("fecha_nacimiento") Is DBNull.Value Then
                txtCumpleaños.Text = ""
            Else
                txtCumpleaños.Text = Format(tabla.Rows(0)("fecha_nacimiento"), "dd/MMM/yyyy")
            End If
            cmbLoginSupervisor.Text = tabla.Rows(0)("id_supervisor")
            txtNombreSupervisor.Text = tabla.Rows(0)("Supervisor")
            cmbLoginEjecutivo.Text = tabla.Rows(0)("no_region")
            txtNombreEjecutivo.Text = tabla.Rows(0)("Ejecutivo")
            cmbLoginEjecutivoCuenta.Text = tabla.Rows(0)("id_ejecutivo")

            Dim estado As Integer = tabla.Rows(0)("id_estado")
            cmbEstadoPromotor.SelectedValue = estado

            Dim Region As Integer = tabla.Rows(0)("id_region")
            cmbRegionPromotor.SelectedValue = Region
        Else
            pnlPromotor.Visible = False
            pnlFiltro.Visible = True
        End If

        tabla.Dispose()
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        OcultaPaneles()
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        GuardaCambios()
    End Sub

    Sub GuardaCambios()
        OcultaPaneles()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT * From Usuarios WHERE id_usuario=@id_usuario")

        If tabla.Rows.Count = 0 Then
            Me.lblAviso.Text = "HUBO UN ERROR, CIERRA LA PAGINA Y VUELVE A INTENTAR"
            Me.lblAviso.Visible = True
        Else
            BD.Execute(ConexionMars.localSqlServer, _
                       "UPDATE Usuarios SET id_region=" & cmbRegionPromotor.SelectedValue & ", " & _
                       "id_estado=" & cmbEstadoPromotor.Text & ", activo=" & cmbActivo.SelectedValue & ", " & _
                       "no_region=" & cmbLoginEjecutivo.Text & ", id_supervisor=" & cmbLoginSupervisor.Text & ", " & _
                       "id_ejecutivo=" & cmbLoginEjecutivoCuenta.Text & " " & _
                       "WHERE id_promotor='" & txtLoginPromotor.Text & "' ")

            BD.Execute(ConexionMars.localSqlServer, _
                       "UPDATE Usuarios SET nombre='" & txtNombrePromotor.Text & "', " & _
                       "fecha_nacimiento='" & ISODates.Dates.SQLServerDate(CDate(txtCumpleaños.Text)) & "' " & _
                       "WHERE id_usuario='" & txtLoginPromotor.Text & "' ")

            BD.Execute(ConexionMars.localSqlServer, _
                       "UPDATE Usuarios SET nombre='" & txtNombreSupervisor.Text & "' " & _
                       "WHERE id_usuario='" & cmbLoginSupervisor.SelectedValue & "' ")

            BD.Execute(ConexionMars.localSqlServer, _
                       "UPDATE Cap_Calificaciones " & _
                       "SET fecha_ingreso='" & ISODates.Dates.SQLServerDate(CDate(txtFechaIngresoPromotor.Text)) & "' " & _
                       "WHERE id_usuario='" & txtLoginPromotor.Text & "' ")

            Me.lblAviso.Text = "LOS CAMBIOS DEL PROMOTOR " + txtLoginPromotor.Text + " SE REALIZARON CORRECTAMENTE"
        End If

        txtFechaIngresoPromotor.Text = ""
        txtCumpleaños.Text = ""
    End Sub

    Sub OcultaPaneles()
        pnlPromotor.Visible = False
        pnlFiltro.Visible = True

        SeleccionIDPromotor = ""
        SeleccionIDSupervisor = ""
        SeleccionIDEjecutivo = ""
    End Sub

    Protected Sub cmbLoginSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbLoginSupervisor.SelectedIndexChanged
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * from Usuarios " & _
                                               "WHERE id_usuario= '" & cmbLoginSupervisor.SelectedValue & "'")
        If Tabla.Rows.Count > 0 Then
            txtNombreSupervisor.Text = Tabla.Rows(0)("nombre")
        End If

        Tabla.Dispose()
    End Sub

    Protected Sub cmbLoginEjecutivo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbLoginEjecutivo.SelectedIndexChanged
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * from Usuarios " & _
                                               "WHERE no_region= '" & cmbLoginEjecutivo.SelectedValue & "'")
        If tabla.Rows.Count > 0 Then
            txtNombreEjecutivo.Text = tabla.Rows(0)("nombre")
        End If

        tabla.Dispose()
    End Sub

    Protected Sub linkBorrar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles linkBorrar.Click
        txtCumpleaños.Text = ""
        txtFechaIngresoPromotor.Text = ""
        txtNombrePromotor.Text = ""
        GuardaCambios()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT * From Cap_Calificaciones " & _
                                               "WHERE id_usuario='" & txtLoginPromotor.Text & "'")

        If Not Tabla.Rows.Count = 0 Then
            Dim cnn As New SqlConnection(ConexionMars.localSqlServer)
            cnn.Open()

            Dim SQLBorra As New SqlCommand("UPDATE Cap_Calificaciones " & _
                    "SET fecha_ingreso= @fecha_ingreso," & _
                    "fecha_material_p=@fecha_material_p, fecha_capacitacion_p=@fecha_capacitacion_p, fecha_certificacion_p = @fecha_certificacion_p, " & _
                    "fecha_material_s=@fecha_material_s, fecha_capacitacion_s=@fecha_capacitacion_s, fecha_certificacion_s =@fecha_certificacion_s , " & _
                    "fecha_material_e='0', fecha_capacitacion_e='0', fecha_certificacion_e ='0' " & _
                    "WHERE id_usuario =@id_usuario ", cnn)
            With SQLBorra
                .Parameters.AddWithValue("@fecha_ingreso", DBNull.Value)
                .Parameters.AddWithValue("@fecha_material_p", DBNull.Value)
                .Parameters.AddWithValue("@fecha_capacitacion_p", DBNull.Value)
                .Parameters.AddWithValue("@fecha_certificacion_p", DBNull.Value)
                .Parameters.AddWithValue("@fecha_material_s", DBNull.Value)
                .Parameters.AddWithValue("@fecha_capacitacion_s", DBNull.Value)
                .Parameters.AddWithValue("@fecha_certificacion_s", DBNull.Value)
                .Parameters.AddWithValue("@fecha_material_e", DBNull.Value)
                .Parameters.AddWithValue("@fecha_capacitacion_e", DBNull.Value)
                .Parameters.AddWithValue("@fecha_certificacion_e", DBNull.Value)
                .Parameters.AddWithValue("@id_usuario", txtLoginPromotor.Text)
                .ExecuteNonQuery()
                .Dispose()
            End With

            cnn.Close()
            cnn.Dispose()
        End If

        BD.Execute(ConexionMars.localSqlServer, _
                   "DELETE FROM Blog WHERE id_usuario = '" & txtLoginPromotor.Text & "' ")
        BD.Execute(ConexionMars.localSqlServer, _
                   "DELETE FROM Blog WHERE de_id_usuario = '" & txtLoginPromotor.Text & "' ")
        BD.Execute(ConexionMars.localSqlServer, _
                   "DELETE FROM Familia WHERE id_usuario = '" & txtLoginPromotor.Text & "' ")
        BD.Execute(ConexionMars.localSqlServer, _
                   "DELETE FROM Mascota WHERE id_usuario = '" & txtLoginPromotor.Text & "' ")
        BD.Execute(ConexionMars.localSqlServer, _
                   "DELETE FROM Recetas WHERE id_usuario = '" & txtLoginPromotor.Text & "' ")
        BD.Execute(ConexionMars.localSqlServer, _
                   "DELETE FROM Comentarios WHERE id_usuario = '" & txtLoginPromotor.Text & "' ")
        BD.Execute(ConexionMars.localSqlServer, _
                   "DELETE FROM Comentarios WHERE de_id_usuario = '" & txtLoginPromotor.Text & "' ")

        MsgBox("Los datos del usuario con Login" + txtLoginPromotor.Text + " ha sido eliminado. ", MsgBoxStyle.Information)
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Protected Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        btnFiltrar.Enabled = True
    End Sub
End Class