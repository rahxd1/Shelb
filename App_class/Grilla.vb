Imports procomlcd
Imports System.Data
Imports System.Data.SqlClient

Module Grilla
    Public Sub CargaGrilla(ByVal Conexion As String, ByVal SQL As String, _
                     ByVal Grilla As GridView)
        Using cnn As New SqlConnection(Conexion)

            Dim cmd As New SqlCommand(SQL, cnn)
            Dim adapter As New SqlDataAdapter
            adapter.SelectCommand = cmd
            cnn.Open()

            Dim dataset As New DataSet
            adapter.Fill(dataset, "nombre")

            Grilla.DataSource = dataset
            Grilla.DataBind()

            cnn.Dispose()
            cnn.Close()

            Grilla.Visible = True
        End Using
    End Sub
End Module

