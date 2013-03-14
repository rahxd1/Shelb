Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Partial Public Class q19q20
    Inherits System.Web.UI.Page
    Dim Q19, Q20, R19, R20 As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnSiguiente_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSiguiente.Click
        asignarValores()
        guardar()
        guardarFINAL()
        Response.Redirect("resumendentastix.aspx")
    End Sub
    Private Sub asignarValores()
        Q19 = Rb19.SelectedValue
        Q20 = Rb20.SelectedValue
        'LA RESPUESTA PREGUNTA 19 ES LA OPCION 2
        If Q19 = 2 Then
            R19 = 1
        Else
            R19 = 0
        End If
        'LA RESPUESTA pregunta 20 ES LA OPCION 3

        If Q20 = 3 Then
            R20 = 1
        Else
            R20 = 0
        End If


    End Sub
    Private Sub guardar()
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim SQLNuevo As New SqlCommand("UPDATE Capacitacion_Certificaciones_Dentastix_tmp " & _
                               "SET q19 = " & R19 & ", q20 = " & R20 & " WHERE id_usuario = '" & HttpContext.Current.User.Identity.Name & "'", cnn)
            cnn.Open()
            SQLNuevo.ExecuteNonQuery()
            SQLNuevo.Dispose()
            cnn.Close()
            cnn.Dispose()

        End Using
    End Sub
    Private Sub guardarFINAL()
        Dim v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20 As Integer
        Using cnn3 As New SqlConnection(ConexionMars.localSqlServer)
            Dim SQL As New SqlCommand("SELECT * FROM Capacitacion_Certificaciones_Dentastix_tmp " & _
                           "WHERE id_usuario = '" & HttpContext.Current.User.Identity.Name & "'", cnn3)
            cnn3.Open()
            Dim Data As New SqlDataAdapter(SQL)
            Dim Tabla As New DataTable
            Data.Fill(Tabla)
            cnn3.Close()
            cnn3.Dispose()
            'ASIGNAR VALORES TEMPORALES AL FINAL
            If Tabla.Rows.Count = 1 Then
                v1 = Tabla.Rows(0)("q1")
                v2 = Tabla.Rows(0)("q2")
                v3 = Tabla.Rows(0)("q3")
                v4 = Tabla.Rows(0)("q4")
                v5 = Tabla.Rows(0)("q5")
                v6 = Tabla.Rows(0)("q6")
                v7 = Tabla.Rows(0)("q7")
                v8 = Tabla.Rows(0)("q8")
                v9 = Tabla.Rows(0)("q9")
                v10 = Tabla.Rows(0)("q10")
                v11 = Tabla.Rows(0)("q11")
                v12 = Tabla.Rows(0)("q12")
                v13 = Tabla.Rows(0)("q13")
                v14 = Tabla.Rows(0)("q14")
                v15 = Tabla.Rows(0)("q15")
                v16 = Tabla.Rows(0)("q16")
                v17 = Tabla.Rows(0)("q17")
                v18 = Tabla.Rows(0)("q18")
                v19 = Tabla.Rows(0)("q19")
                v20 = Tabla.Rows(0)("q20")
                'INSERTAR VALORES
                'SABER SI SE CERTIFICO O NO
                Dim SumCal = (v1 + v2 + v3 + v4 + v5 + v6 + v7 + v8 + v9 + v10 + v11 + v12 + v13 + v14 + v15 + v16 + v17 + v18 + v19 + v20)
                lblAviso.Visible = True
                lblAviso.Text = SumCal
                Dim Certificado As Integer
                If SumCal >= 16 Then
                    Certificado = 1
                Else
                    Certificado = 0
                End If
                Using cnn2 As New SqlConnection(ConexionMars.localSqlServer)
                    Dim SQLNuevo As New SqlCommand("INSERT INTO Capacitacion_Certificaciones_Dentastix " & _
                                       "(id_usuario, fecha,certificado,q1,q2,q3,q4,q5,q6,q7,q8,q9,q10,q11,q12,q13,q14,q15,q16,q17,q18,q19,q20) " & _
                                       "VALUES(@id_usuario, @fecha,@certificado,@q1,@q2,@q3,@q4,@q5,@q6,@q7,@q8,@q9,@q10,@q11,@q12,@q13,@q14,@q15,@q16,@q17,@q18,@q19,@q20)", cnn2)
                    cnn2.Open()
                    SQLNuevo.Parameters.AddWithValue("@id_usuario", HttpContext.Current.User.Identity.Name)
                    SQLNuevo.Parameters.AddWithValue("@fecha", Date.Today)
                    SQLNuevo.Parameters.AddWithValue("@certificado", Certificado)
                    SQLNuevo.Parameters.AddWithValue("@q1", v1)
                    SQLNuevo.Parameters.AddWithValue("@q2", v2)
                    SQLNuevo.Parameters.AddWithValue("@q3", v3)
                    SQLNuevo.Parameters.AddWithValue("@q4", v4)
                    SQLNuevo.Parameters.AddWithValue("@q5", v5)
                    SQLNuevo.Parameters.AddWithValue("@q6", v6)
                    SQLNuevo.Parameters.AddWithValue("@q7", v7)
                    SQLNuevo.Parameters.AddWithValue("@q8", v8)
                    SQLNuevo.Parameters.AddWithValue("@q9", v9)
                    SQLNuevo.Parameters.AddWithValue("@q10", v10)
                    SQLNuevo.Parameters.AddWithValue("@q11", v11)
                    SQLNuevo.Parameters.AddWithValue("@q12", v12)
                    SQLNuevo.Parameters.AddWithValue("@q13", v13)
                    SQLNuevo.Parameters.AddWithValue("@q14", v14)
                    SQLNuevo.Parameters.AddWithValue("@q15", v15)
                    SQLNuevo.Parameters.AddWithValue("@q16", v16)
                    SQLNuevo.Parameters.AddWithValue("@q17", v17)
                    SQLNuevo.Parameters.AddWithValue("@q18", v18)
                    SQLNuevo.Parameters.AddWithValue("@q19", v19)
                    SQLNuevo.Parameters.AddWithValue("@q20", v20)
                    SQLNuevo.ExecuteNonQuery()
                    cnn2.Close()
                    cnn2.Dispose()

                End Using
            End If



        End Using

    End Sub
End Class