Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Partial Public Class Certificacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Certificaciones()
    End Sub
    Private Sub Certificaciones()
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim SQL As String
            SQL = "select * from Capacitacion_Certificaciones where status = 1"
            Dim cmd As New SqlCommand(SQL, cnn)
            Dim adapter As New SqlDataAdapter
            adapter.SelectCommand = cmd
            cnn.Open()
            Dim dataset As New DataSet
            adapter.Fill(dataset, "Formato")
            cnn.Close()
            gridCer.DataSource = dataset
            gridCer.DataBind()
            cnn.Dispose()
        End Using
    End Sub

End Class