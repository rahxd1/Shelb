Imports System.Data.SqlClient
Imports System.Security.Cryptography

Partial Public Class AdminRegionesSYM
    Inherits System.Web.UI.Page

    Private Sub VerDatos(ByVal SeleccionIDRegion As String)
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT * FROM Regiones " & _
                                               "WHERE id_region = " & SeleccionIDRegion & "")
        If Tabla.Rows.Count > 0 Then
            lblIDRegion.Text = Tabla.Rows(0)("id_region")
            txtNombreRegion.Text = Tabla.Rows(0)("nombre_region")
        End If

        Tabla.Dispose()
    End Sub

    Sub CargarRegiones()
        pnlNuevo.Visible = False
        pnlConsulta.Visible = True

        CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT id_region, nombre_region FROM Regiones WHERE id_region<>0 " & _
                    "ORDER BY nombre_region", me.gridRegiones)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Guardar(lblIDRegion.Text, txtNombreRegion.Text)
        Borrar()

        CargarRegiones()
    End Sub

    Public Function Guardar(ByVal IDRegion As Integer, ByVal NombreRegion As String) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT id_region From Regiones " & _
                                               "WHERE id_region=" & IDRegion & "")
        If Tabla.Rows.Count = 1 Then
            BD.Execute(ConexionSYM.localSqlServer, _
                       "UPDATE Regiones " & _
                       "SET nombre_region='" & NombreRegion & "' " & _
                       "WHERE id_region=" & IDRegion & "")

            Me.lblaviso.Text = "Los cambios se realizaron correctamente."
        Else
            BD.Execute(ConexionSYM.localSqlServer, _
                       "INSERT INTO Regiones" & _
                       "(nombre_region)VALUES('" & NombreRegion & "')")

            Me.lblaviso.Text = "Se guardo exitosamente la información."
        End If

        Tabla.Dispose()
        pnlNuevo.Visible = False

        CargarRegiones()
    End Function

    Sub Borrar()
        lblIDRegion.Text = ""
        txtNombreRegion.Text = ""
        lblaviso.Text = ""
    End Sub

    Protected Sub lnkNuevo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkNuevo.Click
        pnlNuevo.Visible = True
        pnlConsulta.Visible = False
        txtNombreRegion.Focus()

        Borrar()
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Borrar()

        CargarRegiones()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LstProyectos(ConexionSYM.localSqlServer, Me.gridProyectos)

            CargarRegiones()
        End If
    End Sub

    Protected Sub lnkConsultas_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkConsultas.Click
        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
        lblaviso.Text = ""
    End Sub

    Private Sub gridRegiones_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gridRegiones.RowEditing
        If gridRegiones.Rows(e.NewEditIndex).Cells(1).Text = "" Then
            e.Cancel = True
            lblAviso.Text = "Error al intentar editar la región."
        Else
            lblAviso.Text = ""
            pnlNuevo.Visible = True
            pnlConsulta.Visible = False
            txtNombreRegion.Focus()
            VerDatos(gridRegiones.Rows(e.NewEditIndex).Cells(1).Text)
        End If
    End Sub

    Private Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridRegiones.Columns(1).Visible = False
    End Sub
End Class