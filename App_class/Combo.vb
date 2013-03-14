Imports procomlcd
Imports System.Data
Imports System.Data.SqlClient

Module Combo
    Public Sub LlenaDrop(ByVal Conexion As String, ByVal SQL As String, _
                     ByVal Campo As String, ByVal Valor As String, ByVal Combo As DropDownList)
        Combo.Items.Clear()

        Using cnn As New SqlConnection(Conexion)
            Dim da As New SqlDataAdapter(SQL, cnn)
            Dim datos As New DataTable
            da.Fill(datos)
            Combo.DataSource = datos
            Combo.DataMember = "Tabla"
            Combo.DataValueField = Valor
            Combo.DataTextField = Campo
            Combo.DataBind()
            da.Dispose()
            datos.Dispose()

            cnn.Dispose()
            cnn.Close()
        End Using

        Combo.Items.Insert(0, New ListItem("", ""))
    End Sub

    Public Sub LlenaDropSin(ByVal Conexion As String, ByVal SQL As String, _
                 ByVal Campo As String, ByVal Valor As String, ByVal Combo As DropDownList)
        Combo.Items.Clear()

        Using cnn As New SqlConnection(Conexion)
            Dim da As New SqlDataAdapter(SQL, cnn)
            Dim datos As New DataTable
            da.Fill(datos)
            Combo.DataSource = datos
            Combo.DataMember = "Tabla"
            Combo.DataValueField = Valor
            Combo.DataTextField = Campo
            Combo.DataBind()
            da.Dispose()
            datos.Dispose()

            cnn.Dispose()
            cnn.Close()
        End Using
    End Sub
End Module

