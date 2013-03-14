Imports System.Data.SqlClient
Imports System.Data

Partial Public Class ResultadosMars
    Inherits System.Web.UI.Page
    Dim IDModulo As String
    Dim tipo_usuario, Nuevo, Edicion, Eliminacion, Consulta As Integer
    Dim Supervisor, varPromotor, FolioAct, Nombre, Region As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BusquedaPermisos()
            If tipo_usuario = 12 Then
                cmbEjecutivoCuenta.Items.Insert(0, New ListItem(HttpContext.Current.User.Identity.Name, Region))
                ListaCombo("select distinct UM.no_region, U.nombre, UM.no_region + ' ' + U.nombre as NombreEjecutivo from Usuarios as UM INNER JOIN Usuarios as U ON UM.no_region= U.id_usuario WHERE UM.id_ejecutivo='" & cmbEjecutivoCuenta.SelectedValue & "' order by UM.no_region", "Usuarios", "NombreEjecutivo", "no_region", cmbEjecutivo)
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

    Sub CargaModulos()
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim sql As String = "select CAL.id_modulo, MOD.nombre_modulo, CAL.calificacion from Cap_Calificaciones as CAL INNER JOIN Cap_Modulos as MOD on MOD.id_modulo= CAL.id_modulo WHERE CAL.id_usuario ='" & cmbPromotor.SelectedValue & "' ORDER by MOD.id_modulo "

            Dim cmd As New SqlCommand(sql, cnn)
            Dim adapter As New SqlDataAdapter
            adapter.SelectCommand = cmd
            cnn.Open()
            Dim dataset As New DataSet
            adapter.Fill(dataset, "nombre_modulo")
            cnn.Close()
            gridCalificaciones.DataSource = dataset
            gridCalificaciones.DataBind()
            cnn.Dispose()
        End Using
    End Sub

    Protected Sub btnVer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnVer.Click
        CargaModulos()
    End Sub

    Protected Sub lnkCalificaciones_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkCalificaciones.Click
        pnlUsuarios.Visible = True
        pnlResultados.Visible = False
        pnlImagen.Visible = False
    End Sub

    Protected Sub lnkDetalle_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkDetalle.Click
        pnlUsuarios.Visible = False
        pnlResultados.Visible = True
        pnlImagen.Visible = False
    End Sub
End Class