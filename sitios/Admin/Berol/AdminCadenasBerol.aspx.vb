Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports procomlcd

Partial Public Class AdminCadenasBerol
    Inherits System.Web.UI.Page

    Private Sub VerDatos(ByVal SeleccionIDCadena As String)
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                               "SELECT * FROM Cadenas_tiendas " & _
                                               "WHERE id_cadena= '" & SeleccionIDCadena & "'")
        If tabla.Rows.Count > 0 Then
            lblIDCadena.Text = tabla.Rows(0)("id_cadena")
            txtNombreCadena.Text = tabla.Rows(0)("nombre_cadena")
            cmbTipoCadena.SelectedValue = tabla.Rows(0)("tipo_cadena")
        End If

        Tabla.Dispose()
    End Sub

    Sub CargarCadena()
        CargaGrilla(ConexionBerol.localSqlServer, _
                    "SELECT CAD.id_cadena, CAD.nombre_cadena, TCAD.nombre_tipocadena " & _
                    "FROM Cadenas_tiendas as CAD " & _
                    "INNER JOIN Cadenas_Tipo as TCAD ON CAD.tipo_cadena = TCAD.id_tipocadena " & _
                    "ORDER BY CAD.nombre_cadena", Me.gridCadena)

        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Dim id_cadena As Integer
        If lblIDCadena.Text = "" Then
            id_cadena = 0 : Else
            id_cadena = lblIDCadena.Text : End If

        Guardar(id_cadena, txtNombreCadena.Text, cmbTipoCadena.SelectedValue)
        Borrar()
        lnkNuevo.Enabled = True

        CargarCadena()
    End Sub

    Public Function Guardar(ByVal IDCadena As Integer, ByVal Nombre As String, ByVal TipoCadena As String) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                               "SELECT id_cadena From Cadenas_tiendas " & _
                                               "WHERE id_cadena=" & IDCadena & "")
        If Tabla.Rows.Count = 1 Then
            BD.Execute(ConexionBerol.localSqlServer, _
                       "UPDATE Cadenas_tiendas " & _
                       "SET nombre_cadena='" & Nombre & "', tipo_cadena=" & TipoCadena & " " & _
                       "WHERE id_cadena=" & IDCadena & "")

            Me.lblaviso.Text = "Los cambios se realizaron correctamente."
        Else
            BD.Execute(ConexionBerol.localSqlServer, _
                      "INSERT INTO Cadenas_tiendas" & _
                      "(nombre_cadena, tipo_cadena) " & _
                      "VALUES('" & Nombre & "', " & TipoCadena & ")")

            Me.lblaviso.Text = "Se guardo exitosamente la información."
        End If

        Tabla.Dispose()

        pnlNuevo.Visible = False
        CargarCadena()
    End Function

    Sub Borrar()
        txtNombreCadena.Text = ""
    End Sub

    Protected Sub lnkNuevo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkNuevo.Click
        lblIDCadena.Text = ""
        txtNombreCadena.Text = ""
        cmbTipoCadena.SelectedIndex = 0

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
            LstProyectos(ConexionBerol.localSqlServer, Me.gridProyectos)

            CargarCadena()
            Combo.LlenaDrop(ConexionBerol.localSqlServer, "SELECT id_tipocadena, nombre_tipocadena FROM Cadenas_Tipo ORDER BY id_tipocadena", "nombre_tipocadena", "id_tipocadena", cmbTipoCadena)
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
            lblAviso.Text = "Error al intentar editar la cadena."
        Else
            lblAviso.Text = ""
            pnlNuevo.Visible = True
            pnlConsulta.Visible = False
            txtNombreCadena.Focus()
            VerDatos(gridCadena.Rows(e.NewEditIndex).Cells(1).Text)
        End If
    End Sub

    Private Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridCadena.Columns(1).Visible = False
    End Sub
End Class