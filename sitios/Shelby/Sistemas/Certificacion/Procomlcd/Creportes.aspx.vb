Public Partial Class Creportes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub



    Protected Sub Lista_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Lista.SelectedIndexChanged
        Dim selec As Integer

        selec = Lista.SelectedValue

        If selec = 1 Then
            llenar()
        ElseIf selec = 2 Then
            llenar2()
        End If

    End Sub

    Sub llenar()
        Dim SQLReg As String
        SQLReg = "select usuario,resultado from Examen_Procom where resultado >=17 and intentos =1 or resultado >=18 and intentos =2 or resultado >=20 and intentos =3 "
        CargaGrilla(ConexionShelby.localSqlServer, SQLReg, aprobados)
    End Sub

    Sub llenar2()
        Dim SQLReg As String
        SQLReg = "select usuario,resultado from Examen_Procom where resultado <=16 and intentos =1 or resultado <=17 and intentos =2 or resultado <20 and intentos =3 "
        CargaGrilla(ConexionShelby.localSqlServer, SQLReg, aprobados)
    End Sub




End Class