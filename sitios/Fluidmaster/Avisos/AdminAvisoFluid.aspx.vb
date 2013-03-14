Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class AdminAvisoFluid
    Inherits System.Web.UI.Page

    Dim IDAviso As Integer
    Dim RegionSQLCmb, AvisoSQLCmb, SupervisorSQLCmb, PromotorSQLCmb As String
    Dim AvisoSel, PromotorSel As String

    Sub SQLCombo()
        AvisoSel = Acciones.Slc.cmb("AV.id_aviso", cmbAviso.SelectedValue)

        AvisoSQLCmb = "SELECT DISTINCT AV.nombre_aviso, Av.id_aviso " & _
                        "FROM Avisos as AV " & _
                        "INNER JOIN Avisos_Historial as H ON H.id_aviso=AV.id_aviso " & _
                        "INNER JOIN (select distinct id_usuario " & _
                        "FROM CatRutas) as US ON US.id_usuario=H.id_usuario " & _
                        "ORDER BY nombre_aviso"

        PromotorSQLCmb = "SELECT DISTINCT H.id_usuario " & _
                        "FROM Avisos as AV " & _
                        "INNER JOIN Avisos_Historial as H ON H.id_aviso=AV.id_aviso " & _
                        "INNER JOIN (select distinct id_usuario " & _
                        "FROM CatRutas) as US ON US.id_usuario=H.id_usuario " & _
                        "WHERE H.id_usuario<>'' " & _
                        " " + AvisoSel + " " & _
                        "ORDER BY id_usuario"
    End Sub

    Private Sub VerDatos()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                                                    "SELECT * FROM Avisos " & _
                                                    "WHERE id_aviso=" & IDAviso & "")
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
            CargaGrilla(ConexionFluidmaster.localSqlServer, "select * from Avisos ORDER BY fecha_inicio", Me.gridAvisos)
        End If
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Dim fecha_inicio As String = ISODates.Dates.SQLServerDate(CDate(txtFechaInicio.Text))
        Dim fecha_fin As String = ISODates.Dates.SQLServerDate(CDate(txtFechaFin.Text))

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                                                    "SELECT id_aviso From Avisos " & _
                                                    "WHERE id_aviso=" & lblIDAviso.Text & "")
        If Tabla.Rows.Count = 1 Then
            BD.Execute(ConexionFluidmaster.localSqlServer, _
                       "UPDATE Avisos " & _
                       "SET nombre_aviso='" & txtTitulo.Text & "',descripcion='" & txtDescripcion.Text & "', " & _
                       "fecha_inicio='" & fecha_inicio & "',fecha_fin='" & fecha_fin & "'" & _
                       "FROM Avisos WHERE id_aviso=" & lblIDAviso.Text & "")
        Else
            IDAviso = BD.RT.Execute(ConexionFluidmaster.localSqlServer, _
                       "INSERT INTO Avisos" & _
                       "(nombre_aviso, descripcion, fecha_inicio, fecha_fin) " & _
                       "VALUES('" & txtTitulo.Text & "','" & txtDescripcion.Text & "'," & _
                       "'" & fecha_inicio & "','" & fecha_fin & "') " & _
                       "SELECT @@IDENTITY AS 'id_aviso'")
        End If

        Tabla.Dispose()

        pnlAvisos.Visible = False
        gridAvisos.Visible = True

        CargaGrilla(ConexionFluidmaster.localSqlServer, "select * from Avisos ORDER BY fecha_inicio", Me.gridAvisos)
    End Sub

    Private Sub gridAvisos_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridAvisos.RowDeleting
        IDAviso = gridAvisos.Rows(e.RowIndex).Cells(2).Text

        BD.Execute(ConexionFluidmaster.localSqlServer, _
                   "DELETE FROM Avisos WHERE id_aviso = '" & IDAviso & "'")

        CargaGrilla(ConexionFluidmaster.localSqlServer, "select * from Avisos ORDER BY fecha_inicio", Me.gridAvisos)
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
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, AvisoSQLCmb, "nombre_aviso", "id_aviso", cmbAviso)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
    End Sub

    Protected Sub cmbAviso_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbAviso.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)

        VerLecturas()
    End Sub

    Sub VerLecturas()
        AvisoSel = Acciones.Slc.cmb("AV.id_aviso", cmbAviso.SelectedValue)
        PromotorSel = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)

        CargaGrilla(ConexionFluidmaster.localSqlServer, "select AV.nombre_aviso, H.id_usuario,H.fecha_leido " & _
                                    "FROM Avisos as AV " & _
                                    "INNER JOIN Avisos_Historial as H ON H.id_aviso=AV.id_aviso " & _
                                    "INNER JOIN (select distinct id_usuario " & _
                                    "FROM CatRutas) as US ON US.id_usuario=H.id_usuario " & _
                                    "WHERE H.id_usuario<>'' " & _
                                    " " + AvisoSel + PromotorSel + " " & _
                                    "ORDER BY id_usuario", Me.gridLecturas)
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