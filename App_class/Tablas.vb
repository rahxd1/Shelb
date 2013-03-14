Imports procomlcd
Imports System.Data
Imports System.Data.SqlClient

Module Tablas
    Partial Public Class Ver
        Public Shared Function DT(ByVal Conexion As String, ByVal SQL As String) As DataTable
            Dim cnn As New SqlConnection(Conexion)
            cnn.Open()

            Dim DataSQL As New SqlCommand(SQL, cnn)
            Dim Tabla As New DataTable
            Dim Data As New SqlDataAdapter(DataSQL)
            Data.Fill(Tabla)

            DataSQL.Dispose()
            Tabla.Dispose()
            Data.Dispose()

            cnn.Dispose()
            cnn.Close()

            Return Tabla
        End Function
    End Class
End Module



