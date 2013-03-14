Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports procomlcd

Partial Public Class AdminCadenasSYM
    Inherits System.Web.UI.Page

    Private Sub VerDatos(ByVal SeleccionIDCadena As String)
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT * FROM Cadenas_Tiendas " & _
                                               "WHERE id_cadena=" & SeleccionIDCadena & " ")
        If Tabla.Rows.Count > 0 Then
            lblIDCadena.Text = Tabla.Rows(0)("id_cadena")
            txtNombreCadena.Text = Tabla.Rows(0)("nombre_cadena")
            cmbGrupoCadena.SelectedValue = Tabla.Rows(0)("id_grupo")
        End If

        Tabla.Dispose()
    End Sub

    Sub CargarCadena()
        CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT CAD.id_cadena, CAD.nombre_cadena, TCAD.grupo " & _
                    "FROM Cadenas_Tiendas as CAD " & _
                    "INNER JOIN Cadenas_Grupo as TCAD ON CAD.id_grupo = TCAD.id_grupo " & _
                    "ORDER BY CAD.nombre_cadena",gridCadena)

        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Dim id_cadena As Integer
        If lblIDCadena.Text = "" Then
            id_cadena = 0 : Else
            id_cadena = lblIDCadena.Text : End If

        Guardar(id_cadena, txtNombreCadena.Text, cmbGrupoCadena.SelectedValue)
        Borrar()
        lnkNuevo.Enabled = True

        CargarCadena()
    End Sub

    Public Function Guardar(ByVal id_cadena As Integer, ByVal Nombre As String, _
                            ByVal id_grupo As Integer) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT id_cadena From Cadenas_Tiendas " & _
                                               "WHERE id_cadena=" & id_cadena & "")

        If Tabla.Rows.Count = 1 Then
            BD.Execute(ConexionSYM.localSqlServer, _
                       "UPDATE Cadenas_Tiendas " & _
                       "SET nombre_cadena='" & Nombre & "', id_grupo=" & id_grupo & " " & _
                       "WHERE id_cadena=" & id_cadena & "")

            Me.lblaviso.Text = "Los cambios se realizaron correctamente."
        Else
            BD.Execute(ConexionSYM.localSqlServer, _
                       "INSERT INTO Cadenas_Tiendas" & _
                       "(nombre_cadena, id_grupo) " & _
                       "VALUES('" & Nombre & "'," & id_grupo & ")")

            Me.lblaviso.Text = "Se guardo exitosamente la información."
        End If

        pnlNuevo.Visible = False
        Tabla.Dispose()

        CargarCadena()
    End Function

    Sub Borrar()
        txtNombreCadena.Text = ""
    End Sub

    Protected Sub lnkNuevo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkNuevo.Click
        lblIDCadena.Text = ""
        txtNombreCadena.Text = ""
        cmbGrupoCadena.SelectedIndex = 0

        pnlNuevo.Visible = True
        pnlConsulta.Visible = False
        txtNombreCadena.Focus()
        lblaviso.Text = ""
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Borrar()

        CargarCadena()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LstProyectos(ConexionSYM.localSqlServer, Me.gridProyectos)

            Combo.LlenaDrop(ConexionSYM.localSqlServer, "SELECT * FROM Cadenas_Grupo ORDER BY grupo", "grupo", "id_grupo", cmbGrupoCadena)

            CargarCadena()
        End If
    End Sub

    Protected Sub lnkConsultas_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkConsultas.Click
        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
        lblaviso.Text = ""
    End Sub

    Private Sub gridCadena_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gridCadena.RowEditing
        If gridCadena.Rows(e.NewEditIndex).Cells(1).Text = "" Then
            e.Cancel = True
            lblaviso.Text = "Error al intentar editar la cadena."
        Else
            lblAviso.Text = ""
            VerDatos(gridCadena.Rows(e.NewEditIndex).Cells(1).Text)
            pnlNuevo.Visible = True
            pnlConsulta.Visible = False
            txtNombreCadena.Focus()
        End If
    End Sub

    Private Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridCadena.Columns(1).Visible = False
    End Sub
End Class