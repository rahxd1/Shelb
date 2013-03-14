﻿Imports System.Data.SqlClient
Imports System.Security.Cryptography

Partial Public Class AdminRegionesJovy
    Inherits System.Web.UI.Page

    Private Sub VerDatos(ByVal SeleccionIDRegion As String)
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                               "SELECT * FROM Regiones " & _
                                               "WHERE id_region='" & SeleccionIDRegion & "'")
        If Tabla.Rows.Count > 0 Then
            lblIDRegion.Text = Tabla.Rows(0)("id_region")
            txtNombreRegion.Text = Tabla.Rows(0)("nombre_region")
        End If

        Tabla.Dispose()
    End Sub

    Sub CargarRegiones()
        CargaGrilla(ConexionJovy.localSqlServer, _
                    "SELECT id_region, nombre_region FROM Regiones WHERE id_region <>0 " & _
                    "ORDER BY nombre_region", Me.gridRegiones)

        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Guardar(lblIDRegion.Text, txtNombreRegion.Text)
        Borrar()

        CargarRegiones()
    End Sub

    Public Function Guardar(ByVal IDRegion As String, ByVal NombreRegion As String) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                               "SELECT id_region From Regiones " & _
                                               "WHERE id_region=" & IDRegion & "")

        If Tabla.Rows.Count = 1 Then
            BD.Execute(ConexionJovy.localSqlServer, _
                       "UPDATE Regiones SET nombre_region='" & NombreRegion & "' " & _
                       "WHERE id_region=" & IDRegion & "")

            Me.lblaviso.Text = "Los cambios se realizaron correctamente."
        Else
            BD.Execute(ConexionJovy.localSqlServer, _
                       "INSERT INTO Regiones" & _
                       "(nombre_region)VALUES('" & NombreRegion & "')")

            Me.lblaviso.Text = "Se guardo exitosamente la información."
        End If

        pnlNuevo.Visible = False
        Tabla.Dispose()
        CargarRegiones()
    End Function

    Sub Borrar()
        lblIDRegion.Text = ""
        txtNombreRegion.Text = ""
    End Sub

    Protected Sub lnkNuevo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkNuevo.Click
        pnlNuevo.Visible = True
        pnlConsulta.Visible = False
        lblIDRegion.Focus()
        lblaviso.Text = ""
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Borrar()

        CargarRegiones()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CargarRegiones()
        End If
    End Sub

    Protected Sub lnkConsultas_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkConsultas.Click
        lblaviso.Text = ""

        CargarRegiones()
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