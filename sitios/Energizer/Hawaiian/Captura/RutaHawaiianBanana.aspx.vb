Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports ISODates.Dates
Imports System.Security.Cryptography

Partial Public Class RutaHawaiianBanana
    Inherits System.Web.UI.Page

    Dim Usuario As String
    Dim PeriodoActivoSiNo As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionEnergizer.localSqlServer, HttpContext.Current.User.Identity.Name)

            If tipo_usuario <> 1 Then
                panelMenu.Visible = True
                ''//Cargar promotor
                Combo.LlenaDrop(ConexionEnergizer.localSqlServer, "SELECT DISTINCT id_usuario FROM Usuarios WHERE id_proyecto=21 ORDER BY id_usuario", "id_usuario", "id_usuario", cmbPromotor)
                ''//Cargar Periodo Promotor Supervisor
                Combo.LlenaDrop(ConexionEnergizer.localSqlServer, "SELECT * FROM HawaiianBanana_Periodos ORDER BY id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)
                Region()
                cmbPromotor.Items.Insert(0, New ListItem("", ""))
            Else
                ''//Cargar periodo promotor
                Combo.LlenaDrop(ConexionEnergizer.localSqlServer, "SELECT TOP 4 * FROM HawaiianBanana_Periodos order by id_periodo DESC", "nombre_periodo", "id_periodo", cmbPeriodo)

                If cmbPeriodo.Text <> "" Then
                    CargarRuta()
                End If
            End If
        End If
    End Sub

    Sub CargarRuta()
        If tipo_usuario <> 1 Then
            Usuario = cmbPromotor.SelectedValue : Else
            Usuario = User.Identity.Name : End If

        CargaGrilla(ConexionEnergizer.localSqlServer, _
                    "execute pcdt_HawaiianBanana_Cargar_Ruta " & cmbPeriodo.SelectedValue & ",'" & Usuario & "'", _
                    Me.gridRutas)
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        If cmbRegion.SelectedValue <> 0 Then
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, "SELECT DISTINCT RE.id_usuario FROM HawaiianBanana_Rutas_Eventos AS RE " & _
                        "INNER JOIN HawaiianBanana_Tiendas AS TI ON TI.id_tienda = RE.id_tienda " & _
                        "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                        "INNER JOIN Cadenas_Tiendas as CAD ON TI.id_cadena = CAD.id_cadena " & _
                        "WHERE REG.id_region = '" & cmbRegion.Text & "'" & _
                        " ORDER BY RE.id_usuario", "id_usuario", "id_usuario", cmbPromotor)
        Else
            Combo.LlenaDrop(ConexionEnergizer.localSqlServer, "SELECT DISTINCT RE.id_usuario FROM HawaiianBanana_Rutas_Eventos AS RE " & _
                        "INNER JOIN HawaiianBanana_Tiendas AS TI ON TI.id_tienda = RE.id_tienda " & _
                        "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                        "INNER JOIN Cadenas_Tiendas as CAD ON TI.id_cadena = CAD.id_cadena " & _
                        " ORDER BY RE.id_usuario", "id_usuario", "id_usuario", cmbPromotor)
        End If

        CargarRuta()
    End Sub

    Sub Region()
        Combo.LlenaDrop(ConexionEnergizer.localSqlServer, "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM HawaiianBanana_Rutas_Eventos AS RE " & _
                    "INNER JOIN HawaiianBanana_Tiendas AS TI ON TI.id_tienda = RE.id_tienda " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON TI.id_cadena = CAD.id_cadena " & _
                    " ORDER BY REG.nombre_region", "nombre_region", "id_region", cmbRegion)
    End Sub

    Private Sub gridRUTAS_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRutas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            PeriodoActivo()

            If PeriodoActivoSiNo = 1 Then
                e.Row.Cells(0).Text = "<img src='../../Img/Cerrado.png' />"
                If tipo_usuario = 1 Then
                    e.Row.Cells(4).Text = "Tienda Cerrada"
                End If
            End If
        End If
    End Sub

    Private Sub PeriodoActivo()
        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        cnn.Open()
        Dim SQL As New SqlCommand("SELECT * FROM HawaiianBanana_Periodos where id_periodo =" & cmbPeriodo.Text & "", cnn)
        Dim tabla As New DataTable
        Dim DA As New SqlDataAdapter(SQL)
        DA.Fill(tabla)

        Dim Fecha_Inicio, Fecha_Fin As Date
        If tabla.Rows.Count > 0 Then
            Fecha_Inicio = tabla.Rows(0)("fecha_inicio")
            Fecha_Fin = tabla.Rows(0)("fecha_cierre")
        End If

        If Fecha_Inicio <= CDate(Date.Today) _
            And Fecha_Fin >= CDate(Date.Today) Then
            PeriodoActivoSiNo = 0
        Else
            PeriodoActivoSiNo = 1
        End If

        SQL.Dispose()
        tabla.Dispose()
        DA.Dispose()
        cnn.Close()
        cnn.Dispose()
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        CargarRuta()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        CargarRuta()
    End Sub
End Class