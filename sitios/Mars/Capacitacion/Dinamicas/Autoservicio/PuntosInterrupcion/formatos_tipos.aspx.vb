Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Partial Public Class formatos_tipos
    Inherits System.Web.UI.Page
    Dim id_formato As Integer
    Dim imgDiamante, imgOro, imgPlata, imgBronce As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        verDAT0S()
        verNIVEL()
        validarAccesos()

    End Sub
    Private Sub verNIVEL()
        Dim avance As Integer
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim SQL As New SqlCommand("SELECT COUNT(id_usuario) as logros " & _
                           "FROM AS_PI_Logros " & _
                           "WHERE id_formato = " & id_formato & " and id_usuario = '" & HttpContext.Current.User.Identity.Name & "'", cnn)
            cnn.Open()
            Dim Data As New SqlDataAdapter(Sql)
            Dim Tabla As New DataTable
            Data.Fill(Tabla)
            cnn.Close()
            cnn.Dispose()
            If Tabla.Rows.Count = 1 Then
                avance = Tabla.Rows(0)("logros")
                lblnivel.Text = avance
            End If

            If avance > 0 Then
                Dim calcular As Integer
                calcular = ((avance * 100) / 4)
                If calcular < 25 Then
                    imgNIVEL.ImageUrl = "imagenes/NivelVacio.png"
                    lblAVISO.Text = "¡Aviso! te hace falta pasar algún nivel"
                    lblAVISO.ForeColor = Drawing.Color.Red


                End If
                If (calcular >= 25) And (calcular < 50) Then
                    imgNIVEL.ImageUrl = "imagenes/NivelAmarillo.png"
                    lblAVISO.Text = "¡Aviso! te hace falta pasar algún nivel"
                End If
                If (calcular >= 50) And (calcular <= 75) Then
                    imgNIVEL.ImageUrl = "imagenes/NivelNaranja.png"
                    lblAVISO.Text = "¡Aviso! te hace falta pasar algún nivel"
                    lblAVISO.ForeColor = Drawing.Color.Red
                End If
                If calcular = 100 Then
                    imgNIVEL.ImageUrl = "imagenes/NivelVerde.png"
                    lblAVISO.Text = "¡Felicidades!! ya tienes todos tus logros de este formato"
                    lblAVISO.ForeColor = Drawing.Color.Green
                    btnDiamante.Enabled = False
                End If
                lblnivel.Text = calcular
            Else
                imgNIVEL.ImageUrl = "imagenes/NivelVacio.png"
                lblAVISO.Text = "¡Aviso! No tienes ningun logro en este formato"
                lblAVISO.ForeColor = Drawing.Color.Red

            End If

            SQL.Dispose()
            Data.Dispose()
            Tabla.Dispose()
            cnn.Dispose()
            cnn.Close()
        End Using
    End Sub

    'Private Sub verLOGROS()
    '    Dim sqlLogros As String
    '    sqlLogros = "execute AS_PI_LogrosFormatos " & HttpContext.Current.User.Identity.Name & ",'" & id_formato & "'"
    '    Using cnn As New SqlConnection(ConexionMars.localSqlServer)
    '        cnn.Open()
    '        Dim cmd As New SqlCommand(sqlLogros, cnn)
    '        Dim adapter As New SqlDataAdapter
    '        adapter.SelectCommand = cmd
    '        Dim dataset As New DataSet
    '        adapter.Fill(dataset, "AS_PI_Logros")
    '        gridLogros.DataSource = dataset
    '        gridLogros.DataBind()
    '        dataset.Dispose()
    '        cmd.Dispose()
    '        adapter.Dispose()
    '        cnn.Close()
    '        cnn.Dispose()
    '    End Using

    'End Sub

    Private Sub validarAccesos()
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim SQL As New SqlCommand("execute AS_PI_LogrosFormatos2 '" & HttpContext.Current.User.Identity.Name & "','" & id_formato & "'", cnn)
            cnn.Open()
            Dim Data As New SqlDataAdapter(SQL)
            Dim Tabla As New DataTable
            Data.Fill(Tabla)
            cnn.Close()
            cnn.Dispose()
            If Tabla.Rows.Count = 1 Then

                imgDiamante = Tabla.Rows(0)("DIAMANTE")
                imgOro = Tabla.Rows(0)("ORO")
                imgPlata = Tabla.Rows(0)("PLATA")
                imgBronce = Tabla.Rows(0)("BRONCE")
            End If
            SQL.Dispose()
            Data.Dispose()
            Tabla.Dispose()
            cnn.Dispose()
            cnn.Close()
        End Using
        If imgDiamante = 1 Then
            btnDiamante.Enabled = False
            lblDiamante.Text = "Logrado"
            lblDiamante.ForeColor = Drawing.Color.Green
            lblDiamante0.Text = "¡Felicidades!"
            lblDiamante0.ForeColor = Drawing.Color.Green
            ImagenDiamante.ImageUrl = "imagenes/ok.png"
            imagenLogroD.ImageUrl = "imagenes/estrellita.png"

        Else
            btnDiamante.Enabled = True
            lblDiamante.Text = "¡Te falta!"
            lblDiamante.ForeColor = Drawing.Color.Red
            lblDiamante0.Text = "Tu puedes"
            lblDiamante0.ForeColor = Drawing.Color.Red
            ImagenDiamante.ImageUrl = "imagenes/no.png"

        End If
        If imgOro = 1 Then
            btnOro.Enabled = False
            lblOro.Text = "Logrado"
            lblOro.ForeColor = Drawing.Color.Green
            lblOro0.Text = "Muy bien"
            lblOro0.ForeColor = Drawing.Color.Green
            ImagenOro.ImageUrl = "imagenes/ok.png"
            ImagenLogroO.ImageUrl = "imagenes/estrellita.png"
        Else
            btnOro.Enabled = True
            lblOro.Text = "Te falta"
            lblOro.ForeColor = Drawing.Color.Red
            lblOro0.Text = "¡Vamos!"
            lblOro0.ForeColor = Drawing.Color.Red
            ImagenOro.ImageUrl = "imagenes/no.png"

        End If
        If imgPlata = 1 Then
            btnPlata.Enabled = False
            lblPlata.Text = "Logrado"
            lblPlata.ForeColor = Drawing.Color.Green
            lblPlata0.Text = "Sigue asi"
            lblPlata0.ForeColor = Drawing.Color.Green
            ImagenPlata.ImageUrl = "imagenes/ok.png"
            ImagenLogroP.ImageUrl = "imagenes/estrellita.png"
        Else
            btnPlata.Enabled = True
            lblPlata.Text = "Te falta"
            lblPlata.ForeColor = Drawing.Color.Red
            lblPlata0.Text = "Ya casi"
            lblPlata0.ForeColor = Drawing.Color.Red
            ImagenPlata.ImageUrl = "imagenes/no.png"
        End If
        If imgBronce = 1 Then
            btnBronce.Enabled = False
            lblBronce.Text = "Logrado"
            lblBronce.ForeColor = Drawing.Color.Green
            lblBronce0.Text = "¡Eso es!"
            lblBronce0.ForeColor = Drawing.Color.Green
            ImagenBronce.ImageUrl = "imagenes/ok.png"
            ImagenLogroB.ImageUrl = "imagenes/estrellita.png"
        Else
            btnBronce.Enabled = True
            lblBronce.Text = "Te falta"
            lblBronce.ForeColor = Drawing.Color.Red
            lblBronce0.Text = "Falta poco"
            lblBronce0.ForeColor = Drawing.Color.Red
            ImagenBronce.ImageUrl = "imagenes/no.png"
        End If

    End Sub

    Private Sub verDAT0S()
        id_formato = Request.Params("id_formato")

        Select Case id_formato
            Case 1
                imgFormato.ImageUrl = "imagenes/formatos/BA.png"
            Case 2
                imgFormato.ImageUrl = "imagenes/formatos/CASALEY.png"
            Case 3
                imgFormato.ImageUrl = "imagenes/formatos/CHEDRAUI.png"
            Case 4
                imgFormato.ImageUrl = "imagenes/formatos/COMEXA.png"
            Case 5
                imgFormato.ImageUrl = "imagenes/formatos/HEB.png"
            Case 6
                imgFormato.ImageUrl = "imagenes/formatos/HIPER.png"
            Case 7
                imgFormato.ImageUrl = "imagenes/formatos/ISSSTE.png"
            Case 8
                imgFormato.ImageUrl = "imagenes/formatos/MERCADO.png"
            Case 9
                imgFormato.ImageUrl = "imagenes/formatos/CADENASREGIONALES.png"
            Case 10
                imgFormato.ImageUrl = "imagenes/formatos/SUPER.png"
            Case 11
                imgFormato.ImageUrl = "imagenes/formatos/SUPERAMA.png"
            Case 12
                imgFormato.ImageUrl = "imagenes/formatos/SUPERCENTER.png"
            Case Else
                imgFormato.ImageUrl = "imagenes/NoMedalla.jpg"

        End Select
    End Sub

    Protected Sub btnDiamante_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDiamante.Click
        Dim id_tipo As Integer = 3
        Response.Redirect(String.Format("evaluar.aspx?id_formato={0}&id_tipo={1}", id_formato, id_tipo))

    End Sub

    Protected Sub btnOro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnOro.Click
        Dim id_tipo As Integer = 5
        Response.Redirect(String.Format("evaluar.aspx?id_formato={0}&id_tipo={1}", id_formato, id_tipo))
    End Sub

    Protected Sub btnPlata_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnPlata.Click
        Dim id_tipo As Integer = 6
        Response.Redirect(String.Format("evaluar.aspx?id_formato={0}&id_tipo={1}", id_formato, id_tipo))
    End Sub

    Protected Sub btnBronce_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBronce.Click
        Dim id_tipo As Integer = 1
        Response.Redirect(String.Format("evaluar.aspx?id_formato={0}&id_tipo={1}", id_formato, id_tipo))
    End Sub
End Class