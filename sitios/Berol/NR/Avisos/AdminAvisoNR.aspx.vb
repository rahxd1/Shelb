Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class AdminAvisoNR
    Inherits System.Web.UI.Page

    Dim IDAviso As Integer
    Dim RegionSQLCmb, AvisoSQLCmb, SupervisorSQLCmb, PromotorSQLCmb As String
    Dim AvisoSel, RegionSel, SupervisorSel, PromotorSel As String

    Sub SQLCombo()
        AvisoSel = Acciones.Slc.cmb("id_aviso", cmbAviso.SelectedValue)
        RegionSel = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)
        SupervisorSel = Acciones.Slc.cmb("id_supervisor", cmbSupervisor.SelectedValue)

        AvisoSQLCmb = "SELECT DISTINCT nombre_aviso, id_aviso " & _
                        "FROM View_Avisos " & _
                        "ORDER BY nombre_aviso"

        RegionSQLCmb = "SELECT DISTINCT id_region,nombre_region " & _
                        "FROM View_Avisos " & _
                        "WHERE id_usuario<>'' " & _
                        " " + AvisoSel + " " & _
                        "ORDER BY nombre_region"

        SupervisorSQLCmb = "SELECT DISTINCT Supervisor,id_supervisor " & _
                        "FROM View_Avisos " & _
                        "WHERE id_usuario<>'' " & _
                        " " + AvisoSel + RegionSel + " " & _
                        "ORDER BY id_supervisor"

        PromotorSQLCmb = "SELECT DISTINCT id_usuario " & _
                        "FROM View_Avisos " & _
                        "WHERE id_usuario<>'' " & _
                        " " + AvisoSel + RegionSel + SupervisorSel + " " & _
                        "ORDER BY id_usuario"
    End Sub

    Private Sub VerDatos()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                               "SELECT * FROM Avisos WHERE id_aviso=" & IDAviso & "")

        If Tabla.Rows.Count > 0 Then
            txtTitulo.Text = Tabla.Rows(0)("nombre_aviso")
            txtDescripcion.Text = Tabla.Rows(0)("descripcion")
            txtFechaInicio.Text = Format(Tabla.Rows(0)("fecha_inicio"), "dd/MM/yyyy")
            txtFechaFin.Text = Format(Tabla.Rows(0)("fecha_fin"), "dd/MM/yyyy")
        End If

        Tabla.Dispose()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CargaGrilla(ConexionBerol.localSqlServer, "select * from Avisos ORDER BY fecha_inicio", Me.gridAvisos)
        End If
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Dim fecha_inicio As String = ISODates.Dates.SQLServerDate(CDate(txtFechaInicio.Text))
        Dim fecha_fin As String = ISODates.Dates.SQLServerDate(CDate(txtFechaFin.Text))

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                               "SELECT id_aviso From Avisos WHERE id_aviso='" & lblIDAviso.Text & "'")

        If Tabla.Rows.Count = 1 Then
            BD.Execute(ConexionBerol.localSqlServer, _
                       "UPDATE Avisos " & _
                        "SET nombre_aviso='" & txtTitulo.Text & "', " & _
                        "descripcion='" & txtDescripcion.Text & "', " & _
                        "fecha_inicio='" & fecha_inicio & "',  " & _
                        "fecha_fin='" & fecha_fin & "'  " & _
                        "FROM Avisos " & _
                        "WHERE id_aviso=" & lblIDAviso.Text & "")
            IDAviso = lblIDAviso.Text
        Else
            BD.Execute(ConexionBerol.localSqlServer, _
                       "INSERT INTO Avisos" & _
                       "(nombre_aviso, descripcion, fecha_inicio, fecha_fin) " & _
                       "VALUES('" & txtTitulo.Text & "','" & txtDescripcion.Text & "', " & _
                       "'" & fecha_inicio & "','" & fecha_fin & "')")
        End If

        pnlAvisos.Visible = False
        gridAvisos.Visible = True

        CargaGrilla(ConexionBerol.localSqlServer, "select * from Avisos ORDER BY fecha_inicio", Me.gridAvisos)
    End Sub

    Private Sub gridAvisos_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridAvisos.RowDeleting
        IDAviso = gridAvisos.Rows(e.RowIndex).Cells(2).Text

        BD.Execute(ConexionBerol.localSqlServer, _
                   "DELETE FROM Avisos WHERE id_aviso=" & IDAviso & "")

        CargaGrilla(ConexionBerol.localSqlServer, "select * from Avisos ORDER BY fecha_inicio", Me.gridAvisos)
    End Sub

    Private Sub gridUsuarios_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gridAvisos.RowEditing
        IDAviso = gridAvisos.Rows(e.NewEditIndex).Cells(2).Text
        lblIDAviso.Text = IDAviso
        pnlAvisos.Visible = True
        gridAvisos.Visible = False

        VerDatos()
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        pnlAvisos.Visible = False
        gridAvisos.Visible = True
    End Sub

    Private Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridAvisos.Columns(2).Visible = False
    End Sub

    Protected Sub LinkVerLecturas_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LinkVerLecturas.Click
        pnlAvisos.Visible = False
        pnlVerAvisos.Visible = False
        pnlLecturas.Visible = True

        SQLCombo()
        Combo.LlenaDrop(ConexionBerol.localSqlServer, AvisoSQLCmb, "nombre_aviso", "id_aviso", cmbAviso)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
    End Sub

    Protected Sub cmbAviso_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbAviso.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

        VerLecturas()
    End Sub

    Sub VerLecturas()
        AvisoSel = Acciones.Slc.cmb("id_aviso", cmbAviso.SelectedValue)
        RegionSel = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)
        SupervisorSel = Acciones.Slc.cmb("id_supervisor", cmbSupervisor.SelectedValue)
        PromotorSel = Acciones.Slc.cmb("id_usuario", cmbPromotor.SelectedValue)

        CargaGrilla(ConexionBerol.localSqlServer, _
                    "SELECT * FROM View_Avisos " & _
                    "WHERE id_usuario<>'' " & _
                    " " + AvisoSel + RegionSel + SupervisorSel + PromotorSel + " " & _
                    "ORDER BY id_usuario", Me.gridLecturas)
    End Sub

    Private Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

        VerLecturas()
    End Sub

    Private Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

        VerLecturas()
    End Sub

    Private Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPromotor.SelectedIndexChanged
        VerLecturas()
    End Sub

    Protected Sub LinkVerAvisos_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LinkVerAvisos.Click
        pnlAvisos.Visible = False
        pnlVerAvisos.Visible = True
        pnlLecturas.Visible = False
    End Sub

    Protected Sub LinkAviso_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LinkAviso.Click
        pnlAvisos.Visible = True
        pnlVerAvisos.Visible = False
        pnlLecturas.Visible = False

        txtTitulo.Text = ""
        txtDescripcion.Text = ""
        txtFechaInicio.Text = ""
        txtFechaFin.Text = ""
    End Sub
End Class