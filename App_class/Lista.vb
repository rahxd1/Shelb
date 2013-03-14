Imports procomlcd
Imports System.Data
Imports System.Data.SqlClient

Module Lista

    Public Sub LlenaBox(ByVal Conexion As String, ByVal SQL As String, _
                     ByVal Campo As String, ByVal Valor As String, ByVal Lista As ListBox)
        Lista.Items.Clear()

        Using cnn As New SqlConnection(Conexion)
            Dim da As New SqlDataAdapter(SQL, cnn)
            Dim datos As New DataTable
            da.Fill(datos)
            Lista.DataSource = datos
            Lista.DataMember = "Tabla"
            Lista.DataValueField = Valor
            Lista.DataTextField = Campo
            Lista.DataBind()
            da.Dispose()
            datos.Dispose()

            cnn.Dispose()
            cnn.Close()
        End Using

        Lista.Items.Insert(0, New ListItem("", ""))
    End Sub

End Module

