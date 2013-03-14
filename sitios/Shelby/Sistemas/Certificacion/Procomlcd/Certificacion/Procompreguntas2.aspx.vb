Public Partial Class Procompreguntas2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        mundo.Conta = Request.QueryString("Cont")
        mundo.inten = Request.QueryString("ten")


    End Sub

    Public Class mundo

        Public Shared Conta As String
        Public Shared inten As String

    End Class

    Protected Sub btnSiguiente_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSiguiente.Click

        Dim a, b, c, d, f, g, consuelo, peri As Integer
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

        If b = 3 Then
            b = 1
        Else
            b = 0
        End If

        If c = 4 Then
            c = 1
        Else
            c = 0
        End If

        If d = 3 Then
            d = 1
        Else
            d = 0
        End If

        If f = 2 Then
            f = 1
        Else
            f = 0
        End If
        g = Val(mundo.Conta)
        peri = Val(mundo.inten)
        consuelo = a + b + c + d + f + g
        Response.Redirect("Procompreguntas3.aspx?Cons=" & consuelo & "&ten=" & peri)

    End Sub
End Class