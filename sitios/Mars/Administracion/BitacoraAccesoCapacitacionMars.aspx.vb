Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class BitacoraAccesoCapacitacionMars
    Inherits System.Web.UI.Page

    Dim Filtro, FiltroFecha As String
    Dim Inicio, Fin As String

    Sub BuscaPeriodo()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT * FROM Periodos " & _
                                               "where id_periodo = '" & cmbPeriodo.SelectedValue.ToString() & "'")
        If Tabla.Rows.Count > 0 Then
            Inicio = ISODates.Dates.SQLServerDate(CDate(Tabla.Rows(0)("fecha_inicio_periodo")))
            Fin = ISODates.Dates.SQLServerDate(CDate(Tabla.Rows(0)("fecha_fin_periodo")))
        End If

        tabla.Dispose()
    End Sub

    Sub CargarReporte()
        BuscaPeriodo()

        If cmbPromotor.Text <> "" Then
            Filtro = "AND BITA.id_usuario= '" & cmbPromotor.Text & "'"
        End If
            If cmbSupervisor.Text <> "" Then
                Filtro = "AND BITA.id_usuario= '" & cmbPromotor.Text & "'"
            End If
            If cmbEjecutivoMars.Text <> "" Then
                Filtro = "AND BITA.id_usuario= '" & cmbPromotor.Text & "'"
            End If
            If cmbEjecutivoCuenta.Text <> "" Then
                Filtro = "AND BITA.id_usuario= '" & cmbPromotor.Text & "'"
        End If

        CargaGrilla(ConexionMars.localSqlServer, _
                    "select distinct MARSUS.id_promotor,BITA.id_usuario, BITA.entrada " & _
                    "FROM Usuarios as MARSUS " & _
                    "INNER JOIN Bitacoratmp as BITA " & _
                    "ON BITA.id_usuario= MARSUS.id_promotor " & _
                    "WHERE MARSUS.activo ='1' " & _
                    "AND BITA.entrada>='" & Inicio & "' AND BITA.entrada<='" & Fin & "' " & _
                    " " + Filtro + " ", Me.gridReporte)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct UM.id_ejecutivo, U.nombre, U.nombre as NombreEjecutivoCuenta from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_ejecutivo= U.no_region order by UM.id_ejecutivo", "NombreEjecutivoCuenta", "id_ejecutivo", cmbEjecutivoCuenta)
            Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct UM.no_region, U.nombre, UM.no_region + '- ' + U.nombre as NombreEjecutivo from Usuarios as UM INNER JOIN Usuarios as U ON UM.no_region= U.no_region order by UM.no_region", "NombreEjecutivo", "no_region", cmbEjecutivoMars)
            Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct UM.id_supervisor, U.nombre, UM.id_supervisor + '-' + U.nombre as NombreSupervisor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_supervisor= U.id_usuario order by UM.id_supervisor", "NombreSupervisor", "id_supervisor", cmbSupervisor)
            Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct UM.id_promotor, U.nombre, UM.id_promotor + '-' + U.nombre as NombrePromotor from Usuarios as UM INNER JOIN Usuarios as U ON UM.id_promotor= U.id_usuario order by UM.id_promotor", "NombrePromotor", "id_promotor", cmbPromotor)
            Combo.LlenaDrop(ConexionMars.localSqlServer, "select distinct id_periodo, nombre_periodo,fecha_inicio_periodo FROM Periodos ORDER BY fecha_inicio_periodo", "nombre_periodo", "id_periodo", cmbPeriodo)

            Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                                   "SELECT DISTINCT id_periodo, nombre_periodo, fecha_inicio_periodo, fecha_fin_periodo FROM Periodos where fecha_inicio_periodo<= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' AND fecha_fin_periodo>= '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "'")
            If tabla.Rows.Count > 0 Then
                cmbPeriodo.Text = tabla.Rows(0)("id_periodo")
            End If

            Tabla.Dispose()
        End If
    End Sub

    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGenerar.Click
        PnlGrid.Visible = True

        CargarReporte()
        btnExcel.Enabled = True
    End Sub

    Protected Sub btnExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExcel.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.gridReporte.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.gridReporte)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Bitácora de Accesos.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        cmbSupervisor.Text = ""
        cmbEjecutivoCuenta.Text = ""
        cmbEjecutivoMars.Text = ""
    End Sub

    Protected Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        cmbPromotor.Text = ""
        cmbEjecutivoCuenta.Text = ""
        cmbEjecutivoMars.Text = ""
    End Sub

    Protected Sub cmbEjecutivoSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbEjecutivoMars.SelectedIndexChanged
        cmbPromotor.Text = ""
        cmbSupervisor.Text = ""
        cmbEjecutivoCuenta.Text = ""
    End Sub

    Protected Sub cmbEjecutivoCuenta_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbEjecutivoCuenta.SelectedIndexChanged
        cmbPromotor.Text = ""
        cmbSupervisor.Text = ""
        cmbEjecutivoMars.Text = ""
    End Sub
End Class