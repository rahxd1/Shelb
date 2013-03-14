
Module Acciones
    Partial Public Class Slc
        Public Shared Function chk_VF(ByVal Valor As String) As Boolean
            Dim Resultado As Integer
            If Valor = 1 Then
                Resultado = True : Else
                Resultado = False : End If

            Return Resultado
        End Function

        Public Shared Function chk(ByVal check As CheckBox) As Integer
            Dim Resultado As Integer
            If check.Checked = True Then
                Resultado = 1 : Else
                Resultado = 0 : End If

            Return Resultado
        End Function

        Public Shared Function cmb(ByVal Campo As String, ByVal Seleccion As String) As String
            Dim SQL As String
            If Seleccion = "" Then
                SQL = "" : Else
                SQL = "AND " & Campo & "='" & Seleccion & "' " : End If

            Return SQL
        End Function

        Public Shared Function txt(ByVal Campo As String, ByVal Seleccion As String) As String
            Dim SQL As String
            If Seleccion = "" Then
                SQL = "" : Else
                SQL = "AND " & Campo & " like '%" & Seleccion & "%' " : End If

            Return SQL
        End Function

    End Class
End Module



