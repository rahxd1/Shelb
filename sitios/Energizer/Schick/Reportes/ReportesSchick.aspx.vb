Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports procomlcd

Partial Public Class ReportesSchick
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionEnergizer.localSqlServer, HttpContext.Current.User.Identity.Name)

            CargarReportes()
        End If
    End Sub

    Sub CargarReportes()
        Using cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
            Dim sql As String = "SELECT * FROM Reportes WHERE estatus = 1 AND id_proyecto=32 ORDER BY id_reporte"

            Dim cmd As New SqlCommand(sql, cnn)
            Dim adapter As New SqlDataAdapter
            adapter.SelectCommand = cmd
            cnn.Open()
            Dim dataset As New DataSet
            adapter.Fill(dataset, "Reporte")
            cnn.Close()
            grid.DataSource = dataset
            grid.DataBind()
            cnn.Dispose()
        End Using
    End Sub

End Class