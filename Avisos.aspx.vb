Imports System.Data.SqlClient
Imports System.Data
Imports procomlcd
Imports System.Data.Sql
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class Avisos
    Inherits System.Web.UI.Page

    Sub MensajeLeido()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionAdmin.localSqlServer, _
                                               "SELECT id_aviso From Avisos " & _
                                               "WHERE id_aviso=" & Request.Params("aviso") & "")
        If Tabla.Rows.Count = 1 Then
            BD.Execute(ConexionAdmin.localSqlServer, "UPDATE Avisos " & _
                        "SET leido=1, fecha_leido='" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' FROM Avisos " & _
                        "WHERE id_aviso=" & Request.Params("aviso") & "")
        End If

        Tabla.Dispose()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CargaAvisos()
            Combo.LlenaDrop(ConexionAdmin.localSqlServer, "SELECT * FROM Estatus_avisos ORDER BY id_estatus", "id_estatus", "titulo_estatus", cmbSeguimiento)

            MensajeLeido()
        End If
    End Sub

    Sub CargaAvisos()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionAdmin.localSqlServer, _
                                               "SELECT * from Avisos WHERE id_aviso=" & Request.Params("aviso") & "")
        If Tabla.Rows.Count > 0 Then
            lblTitulo.Text = Tabla.Rows(0)("titulo_aviso")
            lblFecha.Text = Tabla.Rows(0)("fecha")
            lblDescripcion.Text = Tabla.Rows(0)("descripcion")
            txtComentario.Text = Tabla.Rows(0)("respuesta")
            cmbSeguimiento.Text = Tabla.Rows(0)("estatus")

            If Tabla.Rows(0)("estatus") = 2 Then
                pnlSeguimiento.Visible = True
                cmbEstatus.Text = Tabla.Rows(0)("cerrado_usuario")
            End If
        End If

        tabla.Dispose()
    End Sub

    Protected Sub btnEnviar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEnviar.Click
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionAdmin.localSqlServer, _
                                               "SELECT id_aviso From Avisos WHERE id_aviso=@id_aviso")
        If Tabla.Rows.Count = 1 Then
            BD.Execute(ConexionAdmin.localSqlServer, "UPDATE Avisos " & _
                        "SET respuesta='" & txtComentario.Text & "', " & _
                        "estatus='" & cmbSeguimiento.SelectedValue & "', " & _
                        "cerrado_usuario='" & cmbEstatus.SelectedValue & "' FROM Avisos " & _
                        "WHERE id_aviso=" & Request.Params("aviso") & "")
        End If

        Tabla.Dispose()
        Response.Redirect("default.aspx")
    End Sub
End Class