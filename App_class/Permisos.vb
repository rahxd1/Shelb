Imports procomlcd
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports System
Imports System.Web
Imports Microsoft.VisualBasic

Public Module Permisos

    Public Tipo_usuario As Integer
    Public Cliente As Integer
    Public Nuevo As Integer
    Public Edicion As Integer
    Public Eliminacion As Integer
    Public Consulta As Integer
    Public Proceso As Integer
    Public Subir As Integer
    Public Region As String
    Public Promotor As String

    Public Sub VerPermisos(ByVal Conexion As String, ByVal Usuario As String)
        Using cnn As New SqlConnection(Conexion)
            Dim SQL As New SqlCommand("SELECT * FROM Usuarios as USU " & _
                                "INNER JOIN Permisos as PER ON USU.id_tipo = PER.id_tipo " & _
                                "WHERE USU.id_usuario= '" & Usuario & "'", cnn)
            cnn.Open()
            Dim Data As New SqlDataAdapter(SQL)
            Dim Tabla As New DataTable
            Data.Fill(Tabla)

            If Tabla.Rows.Count = 1 Then
                tipo_usuario = Tabla.Rows(0)("id_tipo")

                If tipo_usuario = 1 Or tipo_usuario = 6 Then
                    Promotor = Tabla.Rows(0)("id_usuario") : Else
                    Promotor = "" : End If

                Nuevo = Tabla.Rows(0)("nuevo")
                Edicion = Tabla.Rows(0)("edicion")
                Eliminacion = Tabla.Rows(0)("eliminacion")
                Consulta = Tabla.Rows(0)("consultas")
                Subir = Tabla.Rows(0)("subir")
                Cliente = Tabla.Rows(0)("id_cliente")
            End If

                SQL.Dispose()
                Data.Dispose()
                Tabla.Dispose()
                cnn.Dispose()
                cnn.Close()
        End Using
    End Sub
End Module
