Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class AdminFotgrafiasNR
    Inherits System.Web.UI.Page

    Dim Folio As Integer

    Private Sub VerDatos(ByVal SeleccionIDPeriodo As String)
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                               "SELECT * FROM NR_Historial_Galeria_Promotor_Det " & _
                                               "WHERE folio_foto=" & SeleccionIDPeriodo & "")
        If Tabla.Rows.Count > 0 Then
            lblIDPeriodo.Text = Tabla.Rows(0)("descripcion")
            cmbFamilia.SelectedValue = Tabla.Rows(0)("id_grupo")
            cmbUbicacion.SelectedValue = Tabla.Rows(0)("id_ubicacion")
            Image2.ImageUrl = Tabla.Rows(0)("ruta") & Tabla.Rows(0)("foto")
        End If

        Tabla.Dispose()
    End Sub

    Sub CargarPeriodo()
        pnlNuevo.Visible = False
        pnlConsulta.Visible = True

        CargaGrilla(ConexionBerol.localSqlServer, _
                    "SELECT * FROM NR_Historial_Galeria_Promotor_Det " & _
                    "WHERE id_ubicacion=0 and id_periodo=2" & _
                    "ORDER BY folio_foto DESC", Me.gridPeriodo)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Guardar()
        CargarPeriodo()
    End Sub

    Sub Guardar()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                               "SELECT id_periodo From NR_Historial_Galeria_Promotor_Det " & _
                                               "WHERE folio_foto=" & lblfolio.Text & "")

        If Tabla.Rows.Count = 1 Then
            BD.Execute(ConexionBerol.localSqlServer, _
                       "UPDATE NR_Historial_Galeria_Promotor_Det " & _
                       "SET id_ubicacion=" & cmbUbicacion.SelectedValue & ", id_grupo=" & cmbFamilia.SelectedValue & " " & _
                       "WHERE folio_foto =" & lblfolio.Text & " ")

            lblAviso.Text = "LOS CAMBIOS DEL PERIODO SE REALIZARON CORRECTAMENTE"
        End If

        pnlNuevo.Visible = False
        CargarPeriodo()
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        pnlNuevo.Visible = False
        pnlConsulta.Visible = False

        CargarPeriodo()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LstProyectos(ConexionBerol.localSqlServer, Me.gridProyectos)

            CargarPeriodo()

            Combo.LlenaDrop(ConexionBerol.localSqlServer, "select * from Ubicacion where activo=1 order by nombre_ubicacion", "nombre_ubicacion", "id_ubicacion", cmbUbicacion)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, "select * from Tipo_Productos where activo=1 order by nombre_tipo", "nombre_tipo", "tipo_producto", cmbFamilia)
        End If
    End Sub

    Protected Sub lnkConsultas_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkConsultas.Click
        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
    End Sub

    Private Sub gridPeriodo_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gridPeriodo.RowEditing
        If gridPeriodo.Rows(e.NewEditIndex).Cells(1).Text = "" Then
            e.Cancel = True
            lblAviso.Text = "Error al intentar editar el periodo."
        Else
            lblAviso.Text = ""
            pnlNuevo.Visible = True
            pnlConsulta.Visible = False
            VerDatos(gridPeriodo.Rows(e.NewEditIndex).Cells(1).Text)
            lblFolio.Text = gridPeriodo.Rows(e.NewEditIndex).Cells(1).Text
        End If
    End Sub

    Private Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridPeriodo.Columns(1).Visible = False
    End Sub
End Class