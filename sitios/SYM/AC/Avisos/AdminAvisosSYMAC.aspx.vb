Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class AdminAvisosSYMAC
    Inherits System.Web.UI.Page

    Dim IDAviso As Integer
    Dim RegionSQLCmb, AvisoSQLCmb, SupervisorSQLCmb, PromotorSQLCmb As String
    Dim AvisoSel, RegionSel, SupervisorSel, PromotorSel As String

    Sub SQLCombo()
        AvisoSel = Acciones.Slc.cmb("AV.id_aviso", cmbAviso.SelectedValue)
        RegionSel = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)
        SupervisorSel = Acciones.Slc.cmb("id_supervisor", cmbSupervisor.SelectedValue)

        AvisoSQLCmb = "SELECT DISTINCT AV.nombre_aviso, Av.id_aviso " & _
                        "FROM Avisos as AV " & _
                        "INNER JOIN Avisos_Historial as H ON H.id_aviso=AV.id_aviso " & _
                        "INNER JOIN View_Usuarios as US ON US.id_usuario=H.id_usuario " & _
                        "INNER JOIN Regiones as REG ON REG.id_region=US.id_region " & _
                        "ORDER BY nombre_aviso"

        RegionSQLCmb = "SELECT DISTINCT REG.id_region,REG.nombre_region " & _
                        "FROM Avisos as AV " & _
                        "INNER JOIN Avisos_Historial as H ON H.id_aviso=AV.id_aviso " & _
                        "INNER JOIN View_Usuarios as US ON US.id_usuario=H.id_usuario " & _
                        "INNER JOIN Regiones as REG ON REG.id_region=US.id_region " & _
                        "WHERE H.id_usuario<>'' " & _
                        " " + AvisoSel + " " & _
                        "ORDER BY nombre_region"

        SupervisorSQLCmb = "SELECT DISTINCT US.Supervisor,US.id_supervisor " & _
                        "FROM Avisos as AV " & _
                        "INNER JOIN Avisos_Historial as H ON H.id_aviso=AV.id_aviso " & _
                        "INNER JOIN View_Usuarios as US ON US.id_usuario=H.id_usuario " & _
                        "INNER JOIN Regiones as REG ON REG.id_region=US.id_region " & _
                        "WHERE H.id_usuario<>'' " & _
                        " " + AvisoSel + RegionSel + " " & _
                        "ORDER BY id_supervisor"

        PromotorSQLCmb = "SELECT DISTINCT H.id_usuario " & _
                        "FROM Avisos as AV " & _
                        "INNER JOIN Avisos_Historial as H ON H.id_aviso=AV.id_aviso " & _
                        "INNER JOIN View_Usuarios as US ON US.id_usuario=H.id_usuario " & _
                        "INNER JOIN Regiones as REG ON REG.id_region=US.id_region " & _
                        "WHERE H.id_usuario<>'' " & _
                        " " + AvisoSel + RegionSel + SupervisorSel + " " & _
                        "ORDER BY id_usuario"
    End Sub

    Private Sub VerDatos()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
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
            CargaGrilla(ConexionSYM.localSqlServer, "select * from Avisos ORDER BY fecha_inicio", Me.gridAvisos)
        End If
    End Sub


    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Dim fecha_inicio As String = ISODates.Dates.SQLServerDate(CDate(txtFechaInicio.Text))
        Dim fecha_fin As String = ISODates.Dates.SQLServerDate(CDate(txtFechaFin.Text))

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT id_aviso From Avisos WHERE id_aviso='" & lblIDAviso.Text & "'")

        If Tabla.Rows.Count = 1 Then
            BD.Execute(ConexionSYM.localSqlServer, _
                       "UPDATE Avisos SET nombre_aviso='" & txtTitulo.Text & "', " & _
                        "descripcion='" & txtDescripcion.Text & "', " & _
                        "fecha_inicio='" & fecha_inicio & "',  " & _
                        "fecha_fin='" & fecha_fin & "' " & _
                        "FROM Avisos WHERE id_aviso=" & lblIDAviso.Text & "")

            IDAviso = lblIDAviso.Text
        Else
            IDAviso = BD.RT.Execute(ConexionSYM.localSqlServer, _
                                    "INSERT INTO Avisos" & _
                                    "(nombre_aviso, descripcion, fecha_inicio, fecha_fin) " & _
                                    "VALUES('" & txtTitulo.Text & "','" & txtDescripcion.Text & "'," & _
                                    "'" & fecha_inicio & "','" & fecha_fin & "') " & _
                                    "SELECT @@IDENTITY AS 'id_aviso'")
        End If

        Tabla.Dispose()

        Dim fileExt As String
        fileExt = System.IO.Path.GetExtension(FileUpload1.FileName)

        If (fileExt = ".exe") Then
            lblSubida.Text = "No puedes subir archivos .exe"
        Else
            If FileUpload1.HasFile = True Then
                Dim path As String = Server.MapPath("~/ARCHIVOS/CLIENTES/SYM/AVISOS/")
                FileUpload1.PostedFile.SaveAs(path & IDAviso & ".jpg")
                lblSubida.Text = "El archivo fue subido correctamente"
            End If

        End If

        pnlAvisos.Visible = False
        gridAvisos.Visible = True

        CargaGrilla(ConexionSYM.localSqlServer, "select * from Avisos ORDER BY fecha_inicio", Me.gridAvisos)
    End Sub

    Private Sub gridAvisos_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridAvisos.RowDeleting
        IDAviso = gridAvisos.Rows(e.RowIndex).Cells(2).Text

        BD.Execute(ConexionSYM.localSqlServer, _
                   "DELETE FROM Avisos WHERE id_aviso = '" & IDAviso & "'")

        CargaGrilla(ConexionSYM.localSqlServer, "select * from Avisos ORDER BY fecha_inicio", Me.gridAvisos)
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
        Combo.LlenaDrop(ConexionSYM.localSqlServer, AvisoSQLCmb, "nombre_aviso", "id_aviso", cmbAviso)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
    End Sub

    Protected Sub cmbAviso_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbAviso.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

        VerLecturas()
    End Sub

    Sub VerLecturas()
        AvisoSel = Acciones.Slc.cmb("AV.id_aviso", cmbAviso.SelectedValue)
        RegionSel = Acciones.Slc.cmb("US.id_region", cmbRegion.SelectedValue)
        SupervisorSel = Acciones.Slc.cmb("US.id_supervisor", cmbSupervisor.SelectedValue)
        PromotorSel = Acciones.Slc.cmb("H.id_supervisor", cmbPromotor.SelectedValue)

        CargaGrilla(ConexionSYM.localSqlServer, "select AV.nombre_aviso, REG.nombre_region, H.id_usuario,US.Supervisor,US.id_supervisor,H.fecha_leido " & _
                                    "FROM Avisos as AV " & _
                                    "INNER JOIN Avisos_Historial as H ON H.id_aviso=AV.id_aviso " & _
                                    "INNER JOIN View_Usuarios as US ON US.id_usuario=H.id_usuario " & _
                                    "INNER JOIN Regiones as REG ON REG.id_region=US.id_region " & _
                                    "WHERE H.id_usuario<>'' " & _
                                    " " + AvisoSel + RegionSel + SupervisorSel + PromotorSel + " " & _
                                    "ORDER BY id_usuario", Me.gridLecturas)
    End Sub

    Private Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

        VerLecturas()
    End Sub

    Private Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

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