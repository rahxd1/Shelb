Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class AdminPeriodosSYMPrecios
    Inherits System.Web.UI.Page

    Private Sub VerDatos(ByVal SeleccionIDPeriodo As String)
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT * FROM Precios_Periodos " & _
                                               "WHERE id_periodo=" & SeleccionIDPeriodo & "")
        If Tabla.Rows.Count > 0 Then
            lblIDPeriodo.Text = Tabla.Rows(0)("id_periodo")
            txtNombrePeriodo.Text = Tabla.Rows(0)("nombre_periodo")
            cmbTipoPeriodo.SelectedValue = Tabla.Rows(0)("tipo_periodo")
            txtFechaInicio.Text = Tabla.Rows(0)("fecha_inicio")
            txtFechaFin.Text = Tabla.Rows(0)("fecha_fin")
            txtFechaCierre.Text = Tabla.Rows(0)("fecha_cierre")
        End If

        Tabla.Dispose()
    End Sub

    Sub CargarPeriodo()
        pnlNuevo.Visible = False
        pnlConsulta.Visible = True

        CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT * FROM Precios_Periodos ORDER BY id_periodo DESC", _
                    me.gridPeriodo)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Guardar(lblIDPeriodo.Text, txtNombrePeriodo.Text, cmbTipoPeriodo.SelectedValue, txtFechaInicio.Text, _
                    txtFechaFin.Text, txtFechaCierre.Text)
        Borrar()

        CargarPeriodo()
    End Sub

    Public Function Guardar(ByVal IDPeriodo As String, ByVal NombrePeriodo As String, _
                            ByVal TipoPeriodo As Integer, ByVal FechaInicio As String, _
                            ByVal FechaFin As String, ByVal FechaCierre As String) As Integer
        Dim fecha_inicio As String = ISODates.Dates.SQLServerDate(CDate(FechaInicio))
        Dim fecha_fin As String = ISODates.Dates.SQLServerDate(CDate(FechaFin))
        Dim fecha_cierre As String = ISODates.Dates.SQLServerDate(CDate(FechaCierre))

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT id_periodo From Precios_Periodos " & _
                                               "WHERE id_periodo='" & IDPeriodo & "' ORDER BY id_periodo")
        If Tabla.Rows.Count = 1 Then
            BD.Execute(ConexionSYM.localSqlServer, _
                       "UPDATE Precios_Periodos " & _
                       "SET nombre_periodo='" & NombrePeriodo & "', tipo_periodo=" & TipoPeriodo & "," & _
                       "fecha_inicio='" & fecha_inicio & "', " & _
                       "fecha_fin='" & fecha_fin & "', fecha_cierre='" & fecha_cierre & "' " & _
                       "WHERE id_periodo=" & IDPeriodo & " ")

            Me.lblAviso.Text = "Los cambios se realizaron correctamente."
        Else
            BD.Execute(ConexionSYM.localSqlServer, _
                       "INSERT INTO Precios_Periodos" & _
                       "(nombre_periodo,tipo_periodo,fecha_inicio,fecha_fin,fecha_cierre) " & _
                       "VALUES('" & NombrePeriodo & "'," & TipoPeriodo & ", " & _
                       "'" & fecha_inicio & "','" & fecha_fin & "','" & fecha_cierre & "')")

            Me.lblAviso.Text = "Se guardo exitosamente la información."
        End If

        Tabla.Dispose()

        pnlNuevo.Visible = False
        CargarPeriodo()
    End Function

    Sub Borrar()
        lblIDPeriodo.Text = ""
        txtNombrePeriodo.Text = ""
        cmbTipoPeriodo.SelectedValue = ""
        txtFechaInicio.Text = ""
        txtFechaFin.Text = ""
        txtFechaCierre.Text = ""
        lblAviso.Text = ""
    End Sub

    Protected Sub lnkNuevo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkNuevo.Click
        pnlNuevo.Visible = True
        pnlConsulta.Visible = False
        txtNombrePeriodo.Focus()
        Borrar()
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Borrar()
        pnlNuevo.Visible = False
        pnlConsulta.Visible = False

        CargarPeriodo()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LstProyectos(ConexionSYM.localSqlServer, Me.gridProyectos)

            Combo.LlenaDrop(ConexionSYM.localSqlServer, "SELECT * FROM Precios_Tipo_Periodos", "nombre_tipo_periodo", "tipo_periodo", cmbTipoPeriodo)

            CargarPeriodo()
        End If
    End Sub

    Protected Sub lnkConsultas_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkConsultas.Click
        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
        Borrar()
    End Sub

    Private Sub gridPeriodo_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gridPeriodo.RowEditing
        lblAviso.Text = ""

        If gridPeriodo.Rows(e.NewEditIndex).Cells(1).Text = "" Then
            e.Cancel = True
            lblAviso.Text = "Error al intentar editar el periodo."
        Else
            lblAviso.Text = ""
            pnlNuevo.Visible = True
            pnlConsulta.Visible = False
            txtNombrePeriodo.Focus()
            VerDatos(gridPeriodo.Rows(e.NewEditIndex).Cells(1).Text)
        End If
    End Sub

    Private Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridPeriodo.Columns(1).Visible = False
    End Sub
End Class