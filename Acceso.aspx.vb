Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports ISODates.Dates
Imports procomlcd

Partial Public Class Acceso
    Inherits System.Web.UI.Page

    Protected Sub btnEntrar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEntrar.Click
        Dim clave As String
        clave = FormsAuthentication.HashPasswordForStoringInConfigFile(txtContraseña.Text, "SHA1")

        Dim password As String = clave
        Validar(txtUsuario.Text, password)
    End Sub

    Public Function Validar(ByVal usuario As String, ByVal password As String) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionAdmin.localSqlServer, _
                                               "SELECT id_usuario, password From Acceso " & _
                                               "WHERE id_usuario='" & usuario & "' " & _
                                               "AND password='" & password & "'")
        If Tabla.Rows.Count = 0 Then
            lblaviso.Text = "Usuario o contraseña incorrecta"
            Me.txtUsuario.Text = ""
            Me.txtContraseña.Text = ""

            Tabla.Dispose()
            Exit Function
        End If

        Tabla.Dispose()

        txtUsuario.Text = usuario.ToUpper
        Bitacora()
        FormsAuthentication.RedirectFromLoginPage(txtUsuario.Text, True)
    End Function

    Sub Bitacora()
        BD.Execute(ConexionAdmin.localSqlServer, _
                   "INSERT INTO Bitacora (id_usuario, entrada) " & _
                   "VALUES('" & txtUsuario.Text & "'," & _
                   "'" & ISODates.Dates.SQLServerDate(CDate(DateTime.Now)) & "')")
    End Sub

    Private Function generarClaveSHA1(ByVal clave As String) As String
        Dim enc As New UTF8Encoding
        Dim data() As Byte = enc.GetBytes(clave)
        Dim result() As Byte

        Dim sha As New SHA1CryptoServiceProvider()
        result = sha.ComputeHash(data)

        Dim sb As New StringBuilder
        For i As Integer = 0 To result.Length - 1
            sb.Append(result(i).ToString("X2"))
        Next

        Return sb.ToString()
    End Function

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

        End If
    End Sub
End Class