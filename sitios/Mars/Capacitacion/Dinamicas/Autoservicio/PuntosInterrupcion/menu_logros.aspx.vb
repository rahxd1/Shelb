Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Partial Public Class menu_logros
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        logros()
        nivel()

    End Sub
    Private Sub logros()
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim SQL As String

            SQL = "execute AS_PI_Logros2 '" & HttpContext.Current.User.Identity.Name & "'"
            Dim cmd As New SqlCommand(SQL, cnn)
            Dim adapter As New SqlDataAdapter
            adapter.SelectCommand = cmd
            cnn.Open()
            Dim dataset As New DataSet
            adapter.Fill(dataset, "Formato")
            cnn.Close()
            gridLogros.DataSource = dataset
            gridLogros.DataBind()
            cnn.Dispose()
        End Using
    End Sub

    Private Sub nivel()
        Dim avance As Integer
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim SQL As New SqlCommand("SELECT COUNT(id_usuario) as logros " & _
                           "FROM AS_PI_Logros " & _
                           "WHERE estatus = 1 and id_usuario = '" & HttpContext.Current.User.Identity.Name & "'", cnn)
            cnn.Open()
            Dim Data As New SqlDataAdapter(SQL)
            Dim Tabla As New DataTable
            Data.Fill(Tabla)
            cnn.Close()
            cnn.Dispose()
            If Tabla.Rows.Count = 1 Then
                avance = Tabla.Rows(0)("logros")

            End If

            If avance > 1 Then
                Dim calcular As Integer
                calcular = ((avance * 100) / 48)
                If calcular <= 25 Then
                    imgNIVEL.ImageUrl = "imagenes/NivelAmarillo.png"
                End If
                If (calcular > 25) And (calcular <= 99) Then
                    imgNIVEL.ImageUrl = "imagenes/NivelNaranja.png"
                End If
                If calcular >= 100 Then
                    imgNIVEL.ImageUrl = "imagenes/NivelVerde.png"
                End If
                lblnivel.Text = calcular
            Else
                lblnivel.Text = "NINGUN OBJETIVO LOGRADO"
                imgNIVEL.ImageUrl = "imagenes/NivelVacio.png"
            End If

            SQL.Dispose()
            Data.Dispose()
            Tabla.Dispose()
            cnn.Dispose()
            cnn.Close()
        End Using
    End Sub
End Class