Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls



Partial Public Class Material_Cap
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Material()
    End Sub

    Private Sub Material()
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim SQL As String
            SQL = "select * from AS_Capacitacion_Material"
            Dim cmd As New SqlCommand(SQL, cnn)
            Dim adapter As New SqlDataAdapter
            adapter.SelectCommand = cmd
            cnn.Open()
            Dim dataset As New DataSet
            adapter.Fill(dataset, "Formato")
            cnn.Close()
            gridCap.DataSource = dataset
            gridCap.DataBind()
            cnn.Dispose()
        End Using
    End Sub
End Class