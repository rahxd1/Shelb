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
Imports InfoSoftGlobal

Partial Public Class ReporteTickets
    Inherits System.Web.UI.Page

    Dim PeriodoSel, QuincenaSel, RegionSel, Promotor, SupervisorSel As String
    Dim PeriodoSQL, QuincenaSQL, RegionSQL, SupervisorSQL As String
    Dim SQLDet, SQLReg As String
    Dim FechaInicioPeriodo, FechaFinPeriodo As String

    Sub SQLCombo()
        If cmbRegion.SelectedValue <> "" Then
            If cmbRegion.SelectedValue <> 0 Then
                RegionSel = "AND US.id_region=" & cmbRegion.SelectedValue & " " : Else
                RegionSel = "" : End If : End If

        PeriodoSQL = "SELECT * FROM Periodos_AS ORDER BY orden DESC"

        RegionSQL = "SELECT DISTINCT REG.id_region, REG.nombre_region " & _
                    "FROM View_Tiendas_AS as REG " & _
                    "INNER JOIN View_Usuario_AS as REL ON REL.id_region = REG.id_region " & _
                    "WHERE REG.id_region <>0 " + Promotor + " " & _
                    " ORDER BY REG.nombre_region"

        SupervisorSQL = "SELECT DISTINCT REL.Supervisor, REL.id_supervisor " & _
                    "FROM View_Usuario_AS as REL " & _
                    "WHERE REL.id_usuario<>'' " + RegionSel + " " & _
                    " ORDER BY REL.id_supervisor"
    End Sub

    Sub FechasMars()
        If cmbQuincena.SelectedValue = "" Then
            QuincenaSel = "" : Else
            QuincenaSel = "AND id_quincena='" & cmbQuincena.SelectedValue & "' " : End If

        Dim cnn As New SqlConnection(ConexionMars.localSqlServer)
        cnn.Open()
        Dim SQL As New SqlCommand("select * from Periodos " & _
                                  "WHERE id_periodo= '" & cmbMes.SelectedValue & "' " + QuincenaSel + " ", cnn)
        Dim tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQL)
        Data.Fill(tabla)

        If tabla.Rows.Count > 0 Then
            If cmbQuincena.SelectedValue = "" Then
                FechaInicioPeriodo = tabla.Rows(0)("fecha_inicio_periodo")
                FechaFinPeriodo = tabla.Rows(0)("fecha_fin_periodo")
            Else
                FechaInicioPeriodo = tabla.Rows(0)("fecha_inicio_quincena")
                FechaFinPeriodo = tabla.Rows(0)("fecha_fin_quincena")
            End If
        End If

        SQL.Dispose()
        tabla.Dispose()
        Data.Dispose()
        cnn.Close()
        cnn.Dispose()
    End Sub

    Sub CargarReporte()
        If cmbMes.SelectedValue <> "" Then
            FechasMars()

            Using cnn As New SqlConnection(ConexionAdmin.localSqlServer)
                If cmbRegion.SelectedValue <> 0 Then
                    RegionSQL = "AND US.no_region=" & cmbRegion.SelectedValue & " " : Else
                    RegionSQL = "" : End If

                If cmbSupervisor.SelectedValue = "" Then
                    SupervisorSQL = "" : Else
                    SupervisorSQL = " AND US.id_usuario= '" & cmbSupervisor.SelectedValue & "' " : End If

                SQLReg = ""

                Dim cmdReg As New SqlCommand(SQLReg, cnn)
                Dim adapterReg As New SqlDataAdapter
                adapterReg.SelectCommand = cmdReg
                cnn.Open()
                Dim datasetReg As New DataSet
                adapterReg.Fill(datasetReg, "Tiendas")
                gridTotal.DataSource = datasetReg
                gridTotal.DataBind()

                SQLDet = "select * " & _
                        "FROM Reportes as REP "

                Dim cmd As New SqlCommand(SQLDet, cnn)
                Dim adapter As New SqlDataAdapter
                adapter.SelectCommand = cmd
                Dim dataset As New DataSet
                adapter.Fill(dataset, "Tiendas")

                cnn.Close()
                gridReporte.DataSource = dataset
                gridReporte.DataBind()
                cnn.Dispose()
            End Using
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionMars.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbMes)
            Combo.LlenaDropNum(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, SupervisorSQL, "supervisor", "id_supervisor", cmbSupervisor)
        End If
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionMars.localSqlServer, SupervisorSQL, "supervisor", "id_supervisor", cmbSupervisor)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbMes.SelectedIndexChanged
        FechasMars()

        SQLCombo()
        Combo.LlenaDropNum(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionMars.localSqlServer, SupervisorSQL, "supervisor", "id_supervisor", cmbSupervisor)

        CargarReporte()
    End Sub

    Protected Sub lnkExportar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.gridReporte.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.pnlGrid)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Bitacora captura anaquel y exhibiciones " + cmbMes.SelectedItem.ToString() + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbQuincena_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbQuincena.SelectedIndexChanged
        CargarReporte()
    End Sub

    Private Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        CargarReporte()
    End Sub
End Class