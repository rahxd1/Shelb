Imports System.Data.SqlClient
Imports System.Data

Partial Public Class CapacitacionMars
    Inherits System.Web.UI.Page
    Dim IDModulo As String
    Dim tipo_usuario, Nuevo, Edicion, Eliminacion, Consulta As Integer
    Dim Supervisor, varPromotor, FolioAct, Nombre, Region As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BusquedaPermisos()
            If tipo_usuario = 12 Then
                cmbEjecutivoCuenta.Items.Insert(0, New ListItem(HttpContext.Current.User.Identity.Name, Region))
                ListaCombo("select distinct UM.no_region, U.nombre, UM.no_region + ' ' + U.nombre as NombreEjecutivo from Usuarios as UM INNER JOIN Usuarios as U ON UM.no_region= U.no_region WHERE UM.id_ejecutivo='" & cmbEjecutivoCuenta.SelectedValue & "' order by UM.no_region", "Usuarios", "NombreEjecutivo", "no_region", cmbEjecutivo)
                ListaCombo("select distinct UM.id_supervisor, U.nombre, UM.id_supervisor + ' ' + U.nombre as NombreSupervisor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_supervisor= U.id_usuario WHERE UM.id_ejecutivo ='" & cmbEjecutivoCuenta.SelectedValue & "' order by UM.id_supervisor", "Usuarios", "NombreSupervisor", "id_supervisor", cmbSupervisor)
                ListaCombo("select distinct UM.id_promotor, U.nombre, UM.id_promotor + ' ' + U.nombre as NombrePromotor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_promotor= U.id_usuario WHERE UM.id_ejecutivo ='" & cmbEjecutivoCuenta.SelectedValue & "' order by UM.id_promotor", "Usuarios", "NombrePromotor", "id_promotor", cmbPromotor)
                Exit Sub
            End If

            If tipo_usuario = 3 Then
                pnlEjecutivoCuenta.Visible = False
                pnlEjecutivo.Visible = False
                cmbEjecutivo.Items.Insert(0, New ListItem(HttpContext.Current.User.Identity.Name, Region))
                ListaCombo("select distinct UM.id_supervisor, U.nombre, UM.id_supervisor + ' ' + U.nombre as NombreSupervisor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_supervisor= U.id_usuario WHERE UM.no_region ='" & cmbEjecutivo.SelectedValue & "' order by UM.id_supervisor", "Usuarios", "NombreSupervisor", "id_supervisor", cmbSupervisor)
                ListaCombo("select distinct UM.id_promotor, U.nombre, UM.id_promotor + ' ' + U.nombre as NombrePromotor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_promotor= U.id_usuario WHERE UM.no_region ='" & cmbEjecutivo.SelectedValue & "' order by UM.id_promotor", "Usuarios", "NombrePromotor", "id_promotor", cmbPromotor)
                Exit Sub
            End If

            If tipo_usuario = 2 Then
                pnlEjecutivoCuenta.Visible = False
                pnlEjecutivo.Visible = False
                pnlSupervisor.Visible = False
                cmbSupervisor.Items.Insert(0, New ListItem(Nombre, HttpContext.Current.User.Identity.Name))
                ListaCombo("select distinct UM.id_promotor, U.nombre, UM.id_promotor + ' ' + U.nombre as NombrePromotor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_promotor= U.id_usuario WHERE UM.id_supervisor ='" & HttpContext.Current.User.Identity.Name & "' order by UM.id_promotor", "Usuarios", "NombrePromotor", "id_promotor", cmbPromotor)
                Exit Sub
            End If

            If tipo_usuario = 1 Then
                pnlEjecutivoCuenta.Visible = False
                pnlEjecutivo.Visible = False
                pnlSupervisor.Visible = False
                cmbPromotor.Items.Insert(0, New ListItem(Nombre, HttpContext.Current.User.Identity.Name))
                Exit Sub
            End If

            ListaCombo("select distinct UM.id_ejecutivo from Usuarios as UM order by UM.id_ejecutivo", "Usuarios", "id_ejecutivo", "id_ejecutivo", cmbEjecutivoCuenta)
            ListaCombo("select distinct UM.no_region, U.nombre, UM.no_region + ' ' + U.nombre as NombreEjecutivo from Usuarios as UM INNER JOIN Usuarios as U ON UM.no_region= U.id_usuario order by UM.no_region", "Usuarios", "NombreEjecutivo", "no_region", cmbEjecutivo)
            ListaCombo("select distinct UM.id_supervisor, U.nombre, UM.id_supervisor + ' ' + U.nombre as NombreSupervisor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_supervisor= U.id_usuario order by UM.id_supervisor", "Usuarios", "NombreSupervisor", "id_supervisor", cmbSupervisor)
            ListaCombo("select distinct UM.id_promotor, U.nombre, UM.id_promotor + ' ' + U.nombre as NombrePromotor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_promotor= U.id_usuario order by UM.id_promotor", "Usuarios", "NombrePromotor", "id_promotor", cmbPromotor)
        End If
    End Sub

    Public Function ListaCombo(ByVal SQL As String, ByVal Tabla As String, ByVal Campo As String, ByVal Valor As String, _
                                ByVal Combo As DropDownList) As Integer
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)

            Dim SQLCadena As New SqlDataAdapter(SQL, cnn)
            Dim DatosCadenas As New DataTable
            SQLCadena.Fill(DatosCadenas)
            Combo.DataSource = DatosCadenas
            Combo.DataMember = Tabla
            Combo.DataValueField = Valor
            Combo.DataTextField = Campo
            Combo.DataBind()
            SQLCadena.Dispose()
            DatosCadenas.Dispose()
            cnn.Close()
            cnn.Dispose()

            Combo.Items.Insert(0, New ListItem("", ""))
        End Using
    End Function

    Sub BusquedaPermisos()
        Try
            Using cnn As New SqlConnection(ConexionMars.localSqlServer)
                Dim squs As String = "SELECT * FROM Usuarios as USU INNER JOIN Permisos as PER ON USU.id_tipo = PER.id_tipo WHERE USU.id_usuario= '" + HttpContext.Current.User.Identity.Name + "'"
                Dim cmdus As New SqlCommand(squs, cnn)
                cnn.Open()
                Dim da As New SqlDataAdapter(cmdus)
                Dim tabla As New DataTable
                da.Fill(tabla)
                cnn.Close()
                cnn.Dispose()
                If tabla.Rows.Count = 1 Then
                    tipo_usuario = tabla.Rows(0)("id_tipo")
                    Nombre = tabla.Rows(0)("nombre")
                    Nuevo = tabla.Rows(0)("nuevo")
                    Edicion = tabla.Rows(0)("edicion")
                    Eliminacion = tabla.Rows(0)("eliminacion")
                    Consulta = tabla.Rows(0)("consultas")
                    Region = tabla.Rows(0)("no_region")
                End If
                da.Dispose()
                tabla.Dispose()
                cmdus.Dispose()
            End Using
        Catch ex As Exception
            'XERROR(ex.Message)
        End Try
    End Sub

    Protected Sub cmbEjecutivo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbEjecutivo.SelectedIndexChanged
        BusquedaPermisos()

        If tipo_usuario = 12 Then
            'ListaCombo("select distinct UM.no_region, U.nombre, UM.no_region + '- ' + U.nombre as NombreEjecutivo from Usuarios as UM INNER JOIN Usuarios as U ON UM.no_region= U.no_region WHERE UM.id_ejecutivo='" & cmbEjecutivoCuenta.SelectedValue & "' order by UM.no_region", "Usuarios", "NombreEjecutivo", "no_region", cmbEjecutivo)
            ListaCombo("select distinct UM.id_supervisor, U.nombre, UM.id_supervisor + ' ' + U.nombre as NombreSupervisor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_supervisor= U.id_usuario WHERE UM.id_ejecutivo ='" & cmbEjecutivoCuenta.SelectedValue & "' order by UM.id_supervisor", "Usuarios", "NombreSupervisor", "id_supervisor", cmbSupervisor)
            ListaCombo("select distinct UM.id_promotor, U.nombre, UM.id_promotor + ' ' + U.nombre as NombrePromotor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_promotor= U.id_usuario WHERE UM.id_ejecutivo ='" & cmbEjecutivoCuenta.SelectedValue & "' order by UM.id_promotor", "Usuarios", "NombrePromotor", "id_promotor", cmbPromotor)

            If Not cmbEjecutivo.Text = "" Then
                ListaCombo("select distinct UM.id_supervisor, U.nombre, UM.id_supervisor + ' ' + U.nombre as NombreSupervisor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_supervisor= U.id_usuario WHERE UM.id_ejecutivo='" & cmbEjecutivoCuenta.SelectedValue & "' AND UM.no_region ='" & cmbEjecutivo.SelectedValue & "' order by UM.id_supervisor", "Usuarios", "NombreSupervisor", "id_supervisor", cmbSupervisor)
                ListaCombo("select distinct UM.id_promotor, U.nombre, UM.id_promotor + ' ' + U.nombre as NombrePromotor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_promotor= U.id_usuario WHERE UM.id_ejecutivo='" & cmbEjecutivoCuenta.SelectedValue & "' AND UM.no_region ='" & cmbEjecutivo.SelectedValue & "' order by UM.id_promotor", "Usuarios", "NombrePromotor", "id_promotor", cmbPromotor)
            End If

            If Not cmbSupervisor.Text = "" Then
                ListaCombo("select distinct UM.id_promotor, U.nombre, UM.id_promotor + ' ' + U.nombre as NombrePromotor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_promotor= U.id_usuario WHERE UM.no_region ='" & cmbEjecutivo.SelectedValue & "' AND id_supervisor ='" & cmbSupervisor.SelectedValue & "' AND UM.id_ejecutivo='" & cmbEjecutivoCuenta.SelectedValue & "' order by UM.id_promotor", "Usuarios", "NombrePromotor", "id_promotor", cmbPromotor)
            End If

            Exit Sub
        End If

        If tipo_usuario = 3 Then
            ListaCombo("select distinct UM.id_supervisor, U.nombre, UM.id_supervisor + ' ' + U.nombre as NombreSupervisor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_supervisor= U.id_usuario WHERE UM.id_ejecutivo ='" & cmbEjecutivoCuenta.SelectedValue & "' order by UM.id_supervisor", "Usuarios", "NombreSupervisor", "id_supervisor", cmbSupervisor)
            ListaCombo("select distinct UM.id_promotor, U.nombre, UM.id_promotor + ' ' + U.nombre as NombrePromotor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_promotor= U.id_usuario WHERE UM.id_ejecutivo ='" & cmbEjecutivoCuenta.SelectedValue & "' order by UM.id_promotor", "Usuarios", "NombrePromotor", "id_promotor", cmbPromotor)

            If Not cmbEjecutivo.Text = "" Then
                ListaCombo("select distinct UM.id_supervisor, U.nombre, UM.id_supervisor + ' ' + U.nombre as NombreSupervisor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_supervisor= U.id_usuario WHERE UM.id_ejecutivo='" & cmbEjecutivoCuenta.SelectedValue & "' AND UM.no_region ='" & cmbEjecutivo.SelectedValue & "' order by UM.id_supervisor", "Usuarios", "NombreSupervisor", "id_supervisor", cmbSupervisor)
                ListaCombo("select distinct UM.id_promotor, U.nombre, UM.id_promotor + ' ' + U.nombre as NombrePromotor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_promotor= U.id_usuario WHERE UM.id_ejecutivo='" & cmbEjecutivoCuenta.SelectedValue & "' AND UM.no_region ='" & cmbEjecutivo.SelectedValue & "' order by UM.id_promotor", "Usuarios", "NombrePromotor", "id_promotor", cmbPromotor)
            End If

            If Not cmbSupervisor.Text = "" Then
                ListaCombo("select distinct UM.id_promotor, U.nombre, UM.id_promotor + ' ' + U.nombre as NombrePromotor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_promotor= U.id_usuario WHERE UM.no_region ='" & cmbEjecutivo.SelectedValue & "' AND id_supervisor ='" & cmbSupervisor.SelectedValue & "' AND UM.id_ejecutivo='" & cmbEjecutivoCuenta.SelectedValue & "' order by UM.id_promotor", "Usuarios", "NombrePromotor", "id_promotor", cmbPromotor)
            End If

            Exit Sub
            Exit Sub
        End If

        If tipo_usuario = 2 Then
            pnlEjecutivo.Visible = True
            cmbSupervisor.Items.Insert(0, New ListItem(Nombre, HttpContext.Current.User.Identity.Name))
            ListaCombo("select distinct UM.id_promotor, U.nombre, UM.id_promotor + ' ' + U.nombre as NombrePromotor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_promotor= U.id_usuario WHERE UM.id_supervisor ='" & HttpContext.Current.User.Identity.Name & "' order by U.nombre", "Usuarios", "NombrePromotor", "id_promotor", cmbPromotor)

            ListaCombo("select distinct UM.id_supervisor, U.nombre, UM.id_supervisor + ' ' + U.nombre as NombreSupervisor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_supervisor= U.id_usuario WHERE UM.no_region ='" & cmbEjecutivo.SelectedValue & "' order by UM.id_supervisor", "Usuarios", "NombreSupervisor", "id_supervisor", cmbSupervisor)
            ListaCombo("select distinct UM.id_promotor, U.nombre, UM.id_promotor + ' ' + U.nombre as NombrePromotor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_promotor= U.id_usuario WHERE UM.no_region ='" & cmbEjecutivo.SelectedValue & "' order by UM.id_promotor", "Usuarios", "NombrePromotor", "id_promotor", cmbPromotor)

            If Not cmbSupervisor.Text = "" Then
                ListaCombo("select distinct UM.id_promotor, U.nombre, UM.id_promotor + ' ' + U.nombre as NombrePromotor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_promotor= U.id_usuario WHERE UM.no_region ='" & cmbEjecutivo.SelectedValue & "' AND id_supervisor ='" & cmbSupervisor.SelectedValue & "' order by UM.id_promotor", "Usuarios", "NombrePromotor", "id_promotor", cmbPromotor)
            End If

            If cmbEjecutivo.Text = "" Then
                ListaCombo("select distinct UM.id_supervisor, U.nombre, UM.id_supervisor + ' ' + U.nombre as NombreSupervisor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_supervisor= U.id_usuario order by U.nombre", "Usuarios", "NombreSupervisor", "id_supervisor", cmbSupervisor)
                ListaCombo("select distinct UM.id_promotor, U.nombre, UM.id_promotor + ' ' + U.nombre as NombrePromotor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_promotor= U.id_usuario order by U.id_promotor", "Usuarios", "NombrePromotor", "id_promotor", cmbPromotor)
            End If

            Exit Sub
        End If


        ListaCombo("select distinct UM.id_supervisor, U.nombre, UM.id_supervisor + ' ' + U.nombre as NombreSupervisor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_supervisor= U.id_usuario WHERE UM.no_region ='" & cmbEjecutivo.SelectedValue & "' order by UM.id_supervisor", "Usuarios", "NombreSupervisor", "id_supervisor", cmbSupervisor)
        ListaCombo("select distinct UM.id_promotor, U.nombre, UM.id_promotor + ' ' + U.nombre as NombrePromotor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_promotor= U.id_usuario WHERE UM.no_region ='" & cmbEjecutivo.SelectedValue & "' order by UM.id_promotor", "Usuarios", "NombrePromotor", "id_promotor", cmbPromotor)

        If Not cmbSupervisor.Text = "" Then
            ListaCombo("select distinct UM.id_promotor, U.nombre, UM.id_promotor + ' ' + U.nombre as NombrePromotor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_promotor= U.id_usuario WHERE UM.no_region ='" & cmbEjecutivo.SelectedValue & "' AND id_supervisor ='" & cmbSupervisor.SelectedValue & "' order by UM.id_promotor", "Usuarios", "NombrePromotor", "id_promotor", cmbPromotor)
        End If

        If cmbEjecutivo.Text = "" Then
            ListaCombo("select distinct UM.id_supervisor, U.nombre, UM.id_supervisor + ' ' + U.nombre as NombreSupervisor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_supervisor= U.id_usuario order by UM.id_supervisor", "Usuarios", "NombreSupervisor", "id_supervisor", cmbSupervisor)
            ListaCombo("select distinct UM.id_promotor, U.nombre, UM.id_promotor + ' ' + U.nombre as NombrePromotor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_promotor= U.id_usuario order by UM.id_promotor", "Usuarios", "NombrePromotor", "id_promotor", cmbPromotor)
        End If
    End Sub

    Protected Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        ListaCombo("select distinct UM.id_promotor, U.nombre, UM.id_promotor + ' ' + U.nombre as NombrePromotor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_promotor= U.id_usuario WHERE UM.id_supervisor ='" & cmbSupervisor.SelectedValue & "' order by UM.id_promotor", "Usuarios", "NombrePromotor", "id_promotor", cmbPromotor)

        If Not cmbEjecutivo.Text = "" Then
            ListaCombo("select distinct UM.id_promotor, U.nombre, UM.id_promotor + ' ' + U.nombre as NombrePromotor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_promotor= U.id_usuario WHERE UM.no_region ='" & cmbEjecutivo.SelectedValue & "' AND UM.id_supervisor ='" & cmbSupervisor.SelectedValue & "' order by UM.id_promotor", "Usuarios", "NombrePromotor", "id_promotor", cmbPromotor)
        End If

        If cmbSupervisor.Text = "" Then
            If Not cmbEjecutivo.Text = "" Then
                ListaCombo("select distinct UM.id_promotor, U.nombre, UM.id_promotor + ' ' + U.nombre as NombrePromotor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_promotor= U.id_usuario WHERE UM.no_region ='" & cmbEjecutivo.SelectedValue & "'  order by UM.id_promotor", "Usuarios", "NombrePromotor", "id_promotor", cmbPromotor)
            Else
                ListaCombo("select distinct UM.id_promotor, U.nombre, UM.id_promotor + ' ' + U.nombre as NombrePromotor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_promotor= U.id_usuario order by UM.id_promotor", "Usuarios", "NombrePromotor", "id_promotor", cmbPromotor)

            End If
        End If
    End Sub

    Protected Sub cmbEjecutivoCuenta_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbEjecutivoCuenta.SelectedIndexChanged
        ListaCombo("select distinct UM.no_region, U.nombre, UM.no_region + ' ' + U.nombre as NombreEjecutivo from Usuarios as UM INNER JOIN Usuarios as U ON UM.no_region= U.id_usuario WHERE UM.id_ejecutivo ='" & cmbEjecutivoCuenta.SelectedValue & "' order by U.nombre", "Usuarios", "NombreEjecutivo", "no_region", cmbEjecutivo)
        ListaCombo("select distinct UM.id_supervisor, U.nombre, UM.id_supervisor + ' ' + U.nombre as NombreSupervisor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_supervisor= U.id_usuario WHERE UM.id_ejecutivo ='" & cmbEjecutivoCuenta.SelectedValue & "' order by UM.id_supervisor", "Usuarios", "NombreSupervisor", "id_supervisor", cmbSupervisor)
        ListaCombo("select distinct UM.id_promotor, U.nombre, UM.id_promotor + ' ' + U.nombre as NombrePromotor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_promotor= U.id_usuario WHERE UM.id_ejecutivo ='" & cmbEjecutivoCuenta.SelectedValue & "' order by UM.id_promotor", "Usuarios", "NombrePromotor", "id_promotor", cmbPromotor)

        If Not cmbSupervisor.Text = "" Then
            ListaCombo("select distinct UM.id_promotor, U.nombre, UM.id_promotor + ' ' + U.nombre as NombrePromotor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_promotor= U.id_usuario WHERE UM.id_ejecutivo ='" & cmbEjecutivoCuenta.SelectedValue & "' AND id_supervisor ='" & cmbSupervisor.SelectedValue & "' order by UM.id_promotor", "Usuarios", "NombrePromotor", "id_promotor", cmbPromotor)
        End If

        If cmbEjecutivoCuenta.Text = "" Then
            ListaCombo("select distinct UM.no_region, U.nombre, UM.no_region + ' ' + U.nombre as NombreEjecutivo from Usuarios as UM INNER JOIN Usuarios as U ON UM.no_region= U.id_usuario order by U.nombre", "Usuarios", "NombreEjecutivo", "no_region", cmbEjecutivo)
            ListaCombo("select distinct UM.id_supervisor, U.nombre, UM.id_supervisor + ' ' + U.nombre as NombreSupervisor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_supervisor= U.id_usuario order by U.nombre", "Usuarios", "NombreSupervisor", "id_supervisor", cmbSupervisor)
            ListaCombo("select distinct UM.id_promotor, U.nombre, UM.id_promotor + ' ' + U.nombre as NombrePromotor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_promotor= U.id_usuario order by U.nombre", "Usuarios", "NombrePromotor", "id_promotor", cmbPromotor)
        End If
    End Sub

    Sub CargaGrillasPromotor()
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim sql As String = "select CAL.id_usuario,MOD.id_modulo, MOD.nombre_modulo, CAL.fecha_material_p, CAL.fecha_capacitacion_p, CAL.fecha_certificacion_p, CAL.calificacion from Cap_Calificaciones as CAL INNER JOIN Cap_Modulos as MOD on MOD.id_modulo= CAL.id_modulo WHERE CAL.id_usuario ='" & cmbPromotor.SelectedValue & "' ORDER by MOD.id_modulo "

            Dim cmd As New SqlCommand(sql, cnn)
            Dim adapter As New SqlDataAdapter
            adapter.SelectCommand = cmd
            cnn.Open()
            Dim dataset As New DataSet
            adapter.Fill(dataset, "nombre_modulo")
            cnn.Close()
            gridModulosPromotor.DataSource = dataset
            gridModulosPromotor.DataBind()
            cnn.Dispose()
        End Using
    End Sub

    Sub CargaGrillasSupervisor()
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim sql As String = "select CAL.id_usuario,MOD.id_modulo, MOD.nombre_modulo, CAL.fecha_material_p, CAL.fecha_capacitacion_p, CAL.fecha_certificacion_p, CAL.fecha_material_s, CAL.fecha_capacitacion_s, CAL.fecha_certificacion_s, CAL.calificacion from Cap_Calificaciones as CAL INNER JOIN Cap_Modulos as MOD on MOD.id_modulo= CAL.id_modulo WHERE CAL.id_usuario ='" & cmbPromotor.SelectedValue & "' ORDER by MOD.id_modulo "

            Dim cmd As New SqlCommand(sql, cnn)
            Dim adapter As New SqlDataAdapter
            adapter.SelectCommand = cmd
            cnn.Open()
            Dim dataset As New DataSet
            adapter.Fill(dataset, "nombre_modulo")
            cnn.Close()
            gridModulosSupervisor.DataSource = dataset
            gridModulosSupervisor.DataBind()
            cnn.Dispose()
        End Using
    End Sub

    Sub CargaGrillasEjecutivo()
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim sql As String = "select CAL.id_usuario,MOD.id_modulo, MOD.nombre_modulo, CAL.fecha_material_p, CAL.fecha_material_s, CAL.fecha_material_e,CAL.fecha_capacitacion_p,CAL.fecha_capacitacion_s,CAL.fecha_capacitacion_e, CAL.fecha_certificacion_p,CAL.fecha_certificacion_s, CAL.fecha_certificacion_e,CAL.calificacion from Cap_Calificaciones as CAL INNER JOIN Cap_Modulos as MOD on MOD.id_modulo= CAL.id_modulo WHERE CAL.id_usuario ='" & cmbPromotor.SelectedValue & "' ORDER by MOD.id_modulo "

            Dim cmd As New SqlCommand(sql, cnn)
            Dim adapter As New SqlDataAdapter
            adapter.SelectCommand = cmd
            cnn.Open()
            Dim dataset As New DataSet
            adapter.Fill(dataset, "nombre_modulo")
            cnn.Close()
            gridModulosEjecutivo.DataSource = dataset
            gridModulosEjecutivo.DataBind()
            cnn.Dispose()
        End Using
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardarPromotor.Click
        For I As Integer = 0 To gridModulosPromotor.Rows.Count - 1
            IDModulo = gridModulosPromotor.DataKeys(I).Value.ToString()
            Dim txtMaterial As TextBox = CType(gridModulosPromotor.Rows(I).FindControl("txtMaterial"), TextBox)
            Dim txtCapacitacion As TextBox = CType(gridModulosPromotor.Rows(I).FindControl("txtCapacitacion"), TextBox)
            Dim txtCertificacion As TextBox = CType(gridModulosPromotor.Rows(I).FindControl("txtCertificacion"), TextBox)

            ''//Guarda
            Dim cnn As New SqlConnection(ConexionMars.localSqlServer)
            cnn.Open()

            Dim cmd As New SqlCommand("UPDATE Cap_Calificaciones " & _
                        "SET  fecha_material_p = @material, " & _
                        "fecha_capacitacion_p = @capacitacion, " & _
                        "fecha_certificacion_p = @certificacion  " & _
                        "FROM Cap_Calificaciones " & _
                        "WHERE id_usuario= @id_usuario AND id_modulo = @id_modulo", cnn)

            If Not txtMaterial.Text = "" Then
                cmd.Parameters.AddWithValue("@material", ISODates.Dates.SQLServerDate(CDate(txtMaterial.Text)))
            Else
                cmd.Parameters.AddWithValue("@material", DBNull.Value)
            End If

            If Not txtCapacitacion.Text = "" Then
                cmd.Parameters.AddWithValue("@capacitacion", ISODates.Dates.SQLServerDate(CDate(txtCapacitacion.Text)))
            Else
                cmd.Parameters.AddWithValue("@capacitacion", DBNull.Value)
            End If

            If Not txtCertificacion.Text = "" Then
                cmd.Parameters.AddWithValue("@certificacion", ISODates.Dates.SQLServerDate(CDate(txtCertificacion.Text)))
            Else
                cmd.Parameters.AddWithValue("@certificacion", DBNull.Value)
            End If

            cmd.Parameters.AddWithValue("@id_usuario", cmbPromotor.SelectedValue)
            cmd.Parameters.AddWithValue("@id_modulo", IDModulo)
            Dim T As Integer = CInt(cmd.ExecuteScalar())

            cmd.Dispose()
            cnn.Close()
            cnn.Dispose()
        Next
    End Sub

    Protected Sub btnGuardarSupervisor_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardarSupervisor.Click
        For I As Integer = 0 To gridModulosSupervisor.Rows.Count - 1
            IDModulo = gridModulosSupervisor.DataKeys(I).Value.ToString()
            Dim txtMaterial As TextBox = CType(gridModulosSupervisor.Rows(I).FindControl("txtMaterial"), TextBox)
            Dim txtCapacitacion As TextBox = CType(gridModulosSupervisor.Rows(I).FindControl("txtCapacitacion"), TextBox)
            Dim txtCertificacion As TextBox = CType(gridModulosSupervisor.Rows(I).FindControl("txtCertificacion"), TextBox)

            ''//Guarda
            Dim cnn As New SqlConnection(ConexionMars.localSqlServer)
            cnn.Open()

            Dim cmd As New SqlCommand("UPDATE Cap_Calificaciones " & _
                        "SET  fecha_material_s = @material, " & _
                        "fecha_capacitacion_s = @capacitacion, " & _
                        "fecha_certificacion_s = @certificacion  " & _
                        "FROM Cap_Calificaciones " & _
                        "WHERE id_usuario= @id_usuario AND id_modulo = @id_modulo", cnn)

            If Not txtMaterial.Text = "" Then
                cmd.Parameters.AddWithValue("@material", ISODates.Dates.SQLServerDate(CDate(txtMaterial.Text)))
            Else
                cmd.Parameters.AddWithValue("@material", DBNull.Value)
            End If

            If Not txtCapacitacion.Text = "" Then
                cmd.Parameters.AddWithValue("@capacitacion", ISODates.Dates.SQLServerDate(CDate(txtCapacitacion.Text)))
            Else
                cmd.Parameters.AddWithValue("@capacitacion", DBNull.Value)
            End If

            If Not txtCertificacion.Text = "" Then
                cmd.Parameters.AddWithValue("@certificacion", ISODates.Dates.SQLServerDate(CDate(txtCertificacion.Text)))
            Else
                cmd.Parameters.AddWithValue("@certificacion", DBNull.Value)
            End If

            cmd.Parameters.AddWithValue("@id_usuario", cmbPromotor.SelectedValue)
            cmd.Parameters.AddWithValue("@id_modulo", IDModulo)
            Dim T As Integer = CInt(cmd.ExecuteScalar())

            cmd.Dispose()
            cnn.Close()
            cnn.Dispose()
        Next
    End Sub

    Protected Sub btnGuardarEjecutivo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardarEjecutivo.Click
        For I As Integer = 0 To gridModulosEjecutivo.Rows.Count - 1
            IDModulo = gridModulosEjecutivo.DataKeys(I).Value.ToString()
            Dim chkMaterial As CheckBox = CType(gridModulosEjecutivo.Rows(I).FindControl("chkMaterial"), CheckBox)
            Dim chkCapacitacion As CheckBox = CType(gridModulosEjecutivo.Rows(I).FindControl("chkCapacitacion"), CheckBox)
            Dim chkCertificacion As CheckBox = CType(gridModulosEjecutivo.Rows(I).FindControl("chkCertificacion"), CheckBox)

            ''//Guarda
            Dim cnn As New SqlConnection(ConexionMars.localSqlServer)
            cnn.Open()

            Dim Material, Capacitacion, Certificacion As Integer
            If chkMaterial.Checked = True Then
                Material = 1
            Else
                Material = 0
            End If

            If chkCapacitacion.Checked = True Then
                Capacitacion = 1
            Else
                Capacitacion = 0
            End If

            If chkCertificacion.Checked = True Then
                Certificacion = 1
            Else
                Certificacion = 0
            End If

            Dim cmd As New SqlCommand("UPDATE Cap_Calificaciones " & _
                        "SET  fecha_material_e = @material, " & _
                        "fecha_capacitacion_e = @capacitacion, " & _
                        "fecha_certificacion_e = @certificacion  " & _
                        "FROM Cap_Calificaciones " & _
                        "WHERE id_usuario= @id_usuario AND id_modulo = @id_modulo", cnn)

            cmd.Parameters.AddWithValue("@material", Material)
            cmd.Parameters.AddWithValue("@capacitacion", Capacitacion)
            cmd.Parameters.AddWithValue("@certificacion", Certificacion)
            cmd.Parameters.AddWithValue("@id_usuario", cmbPromotor.SelectedValue)
            cmd.Parameters.AddWithValue("@id_modulo", IDModulo)
            Dim T As Integer = CInt(cmd.ExecuteScalar())

            cmd.Dispose()
            cnn.Close()
            cnn.Dispose()
        Next
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        pnlFechasPromotor.Visible = False
        pnlFechasSupervisor.Visible = False
        pnlFechasEjecutivo.Visible = False
    End Sub

    Protected Sub btnVer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnVer.Click
        BusquedaPermisos()

        If tipo_usuario = 1 Then
            pnlFechasPromotor.Visible = True
            CargaGrillasPromotor()
            Exit Sub
        End If

        If tipo_usuario = 2 Then
            pnlFechasSupervisor.Visible = True
            CargaGrillasSupervisor()
            Exit Sub
        End If

        If tipo_usuario = 3 Then
            pnlFechasPromotor.Visible = True

            pnlFechasSupervisor.Visible = True

            CargaGrillasPromotor()
            CargaGrillasSupervisor()
            Exit Sub
        End If

        If tipo_usuario = 12 Or tipo_usuario = 100 Then
            pnlFechasEjecutivo.Visible = True

            Using cnn As New SqlConnection(ConexionMars.localSqlServer)
                Dim SQLPromotor As String = "SELECT * FROM Usuarios WHERE id_promotor= '" & cmbPromotor.SelectedValue.ToString() & "'"
                Dim cmdPromotor As New SqlCommand(SQLPromotor, cnn)
                cnn.Open()
                Dim da As New SqlDataAdapter(cmdPromotor)
                Dim tabla As New DataTable
                da.Fill(tabla)
                cnn.Close()
                cnn.Dispose()
                If tabla.Rows.Count > 0 Then
                    Supervisor = tabla.Rows(0)("id_supervisor")
                End If
                da.Dispose()
                tabla.Dispose()
                cmdPromotor.Dispose()
            End Using

            If Supervisor = "SIN SUPERVISOR" Then
                pnlFechasSupervisor.Visible = True
                CargaGrillasSupervisor()
            End If

            CargaGrillasEjecutivo()
            Exit Sub
        End If
    End Sub

    Protected Sub lnkValidaFechas_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkValidaFechas.Click
        pnlUsuarios.Visible = True
        pnlImagen.Visible = False
    End Sub

    Protected Sub lnkPromotor_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkPromotor.Click
        pnlImagen.Visible = False
        pnlUsuarios.Visible = False
        pnlFechasPromotor.Visible = False
        pnlFechasEjecutivo.Visible = False
        pnlFechasEjecutivoMars.Visible = False
        pnlFechasSupervisor.Visible = False
    End Sub
End Class