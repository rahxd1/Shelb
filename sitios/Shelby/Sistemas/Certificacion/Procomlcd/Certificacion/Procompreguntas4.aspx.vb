Imports System.Data
Imports System.Data.SqlClient
Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls


Partial Public Class Procompreguntas4
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        mundo.Conta = Request.QueryString("cons")
        mundo.inten = Request.QueryString("ten")

        mundo.usuario = User.Identity.Name


    End Sub


    Public Class mundo

        Public Shared Conta As String
        Public Shared inten As String
        Public Shared usuario As String


    End Class






    Protected Sub btnSiguiente_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSiguiente.Click
        Dim a, b, c, d, f, g, total, intento As Integer
        Dim usuario As String



        a = Rb1.SelectedValue
        b = Rb2.SelectedValue
        c = Rb3.SelectedValue
        d = Rb4.SelectedValue
        f = Rb5.SelectedValue


        If a = 1 Then
            a = 1
        Else
            a = 0
        End If

        If b = 5 Then
            b = 1
        Else
            b = 0
        End If

        If c = 2 Then
            c = 1
        Else
            c = 0
        End If

        If d = 2 Then
            d = 1
        Else
            d = 0
        End If

        If f = 5 Then
            f = 1
        Else
            f = 0
        End If

        g = Val(mundo.Conta)

        total = a + b + c + d + f + g
        usuario = mundo.usuario
        intento = Val(mundo.inten)




        '----------------------------------------------------------------------------------------------
        '        Conexion SQL Y GUARDAR
        '----------------------------------------------------------------------------------------------

        If intento = 0 Then
            intento = intento + 1
            Dim cnn As New SqlConnection(ConexionShelby.localSqlServer)
            cnn.Open()

            Dim SQLEditar As New SqlCommand("insert into Examen_Procom (usuario,resultado,intentos) values (@usuario,@resultado,@intentos)", cnn)
            SQLEditar.Parameters.AddWithValue("@usuario", usuario)
            SQLEditar.Parameters.AddWithValue("@resultado", total)
            SQLEditar.Parameters.AddWithValue("@intentos", intento)


            SQLEditar.ExecuteNonQuery()
            SQLEditar.Dispose()
            cnn.Close()
            cnn.Dispose()
            Response.Redirect("Terminado.aspx")

        ElseIf intento <> 0 Then
            intento = intento + 1
            Dim cnn As New SqlConnection(ConexionShelby.localSqlServer)
            cnn.Open()

            Dim SQLEditar As New SqlCommand("update Examen_Procom set usuario=@usuario,resultado=@resultado,intentos=@intentos where usuario=@usuario", cnn)
            SQLEditar.Parameters.AddWithValue("@usuario", usuario)
            SQLEditar.Parameters.AddWithValue("@resultado", total)
            SQLEditar.Parameters.AddWithValue("@intentos", intento)


            SQLEditar.ExecuteNonQuery()
            SQLEditar.Dispose()

            cnn.Close()
            cnn.Dispose()

            Response.Redirect("Terminado.aspx")
        End If

      

        '----------------------------------------------------------------------------------------------
        '        CERRAR Conexion SQL Y GUARDAR
        '----------------------------------------------------------------------------------------------





    End Sub
End Class