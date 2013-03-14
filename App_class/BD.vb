Imports procomlcd
Imports System.Data
Imports System.Data.SqlClient

Module BD
    Public Sub Execute(ByVal Conexion As String, ByVal SQL As String)
        Dim cnn As New SqlConnection(Conexion)
        cnn.Open()

        Dim SQLEditar As New SqlCommand(SQL, cnn)
        SQLEditar.ExecuteNonQuery()
        SQLEditar.Dispose()

        cnn.Dispose()
        cnn.Close()
    End Sub

    Partial Public Class RT
        Public Shared Function Execute(ByVal Conexion As String, ByVal SQL As String) As Integer
            Dim Folio As Integer

            Dim cnn As New SqlConnection(Conexion)
            cnn.Open()

            Dim SQLEditar As New SqlCommand(SQL, cnn)
            Folio = Convert.ToInt32(SQLEditar.ExecuteScalar())
            SQLEditar.Dispose()

            cnn.Dispose()
            cnn.Close()

            Return Folio
        End Function
    End Class
End Module



