Imports System.Data
Imports System.Data.SqlClient
Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls

Partial Public Class Procompreguntas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim usuario, VLORFES As String
        Dim numero As Integer
        usuario = User.Identity.Name




        Dim cnn As New SqlConnection(ConexionShelby.localSqlServer)
        cnn.Open()

        Dim SQLEditar As New SqlCommand("SELECT intentos FROM Examen_Procom where usuario =@usuario  ", cnn)
        SQLEditar.Parameters.AddWithValue("@usuario", usuario)
        SQLEditar.ExecuteNonQuery()
       
        'numero = System.Convert.ToInt32(SQLEditar.ExecuteScalar())
        VLORFES = System.Convert.ToString(SQLEditar.ExecuteScalar())

        SQLEditar.Dispose()
        cnn.Close()
        cnn.Dispose()


        numero = Val(VLORFES)
        If numero >= 3 Then
            Response.Redirect("Completo.aspx")
        End If

        mundo.intento = numero


    End Sub


    Public Class mundo

        Public Shared intento As Integer


    End Class



    Protected Sub btnSiguiente_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSiguiente.Click
        Dim a, b, c, d, f, Contas, peri As Integer
        a = Rb1.SelectedValue
        b = Rb2.SelectedValue
        c = Rb3.SelectedValue
        d = Rb4.SelectedValue
        f = Rb5.SelectedValue


        If a = 2 Then
            a = 1
        Else
            a = 0
        End If

        If b = 1 Then
            b = 1
        Else
            b = 0
        End If

        If c = 1 Then
            c = 1
        Else
            c = 0
        End If

        If d = 3 Then
            d = 1
        Else
            d = 0
        End If

        If f = 5 Then
            f = 1
        Else
            f = 0
        End If

        Contas = a + b + c + d + f
        peri = mundo.intento

        Response.Redirect("Procompreguntas2.aspx?Cont=" & Contas & "&ten=" & peri)


    End Sub
End Class