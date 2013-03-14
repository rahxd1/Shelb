Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Partial Public Class evaluar
    Inherits System.Web.UI.Page
    Dim intentos As Integer
    Dim aviAnaquel, aviMascota, aviCaliente, AviES As Integer
    Dim id_formato, id_tipo As Integer
    Dim AnaPouchero, sqlAnaPouchero, AnaLatero, sqlAnaLatero, AnaTira, sqlAnaTira, AnaBalcon, sqlAnaBalcon As Integer
    Dim MasCabecera, sqlMasCabecera, MasIsla, sqlMasIsla, MasBotadero, sqlMasBotadero As Integer
    Dim CalIsla As Integer
    Dim CaliBotadero, sqlCaliBotadero, CaliMixFeeding, sqlCaliMixFeeding, CaliMinirack, sqlCaliMinirack As Integer
    Dim CalBotadero, CalMixFeeding, CalMinirack As Integer
    Dim ESMiniRack, sqlESMiniRack, ESwet, sqlESWet As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        verDATOS()

    End Sub

    Private Sub verDATOS()
        id_formato = Request.Params("id_formato")
        id_tipo = Request.Params("id_tipo")
        Select Case id_formato
            Case 1
                imgFormato.ImageUrl = "imagenes/formatos/BA.png"
                lblFORMATO.Text = "BODEGA"
            Case 2
                imgFormato.ImageUrl = "imagenes/formatos/CASALEY.png"
                lblFORMATO.Text = "CASA LEY"
            Case 3
                imgFormato.ImageUrl = "imagenes/formatos/CHEDRAUI.png"
                lblFORMATO.Text = "CHEDRAUI"
            Case 4
                imgFormato.ImageUrl = "imagenes/formatos/COMEXA.png"
                lblFORMATO.Text = "COMEXA"
            Case 5
                imgFormato.ImageUrl = "imagenes/formatos/HEB.png"
                lblFORMATO.Text = "HEB"
            Case 6
                imgFormato.ImageUrl = "imagenes/formatos/HIPER.png"
                lblFORMATO.Text = "HIPER"
            Case 7
                imgFormato.ImageUrl = "imagenes/formatos/ISSSTE.png"
                lblFORMATO.Text = "ISSSTE"
            Case 8
                imgFormato.ImageUrl = "imagenes/formatos/MERCADO.png"
                lblFORMATO.Text = "MERCADO"
            Case 9
                imgFormato.ImageUrl = "imagenes/formatos/CADENASREGIONALES.png"
                lblFORMATO.Text = "REGIONAL"
            Case 10
                imgFormato.ImageUrl = "imagenes/formatos/SUPER.png"
                lblFORMATO.Text = "SUPER"
            Case 11
                imgFormato.ImageUrl = "imagenes/formatos/SUPERAMA.png"
                lblFORMATO.Text = "SUPERAMA"
            Case 12
                imgFormato.ImageUrl = "imagenes/formatos/SUPERCENTER.png"
                lblFORMATO.Text = "SUPERCENTER"
            Case Else
                imgFormato.ImageUrl = ""
                lblFORMATO.Text = "ERROR-REGRESAR"
                lblTIPO.Text = "error"
        End Select
        Select Case id_tipo
            Case 1
                lblTIPO.Text = "BRONCE"
                imgTipo.ImageUrl = "imagenes/Bronce.png"
            Case 3
                lblTIPO.Text = "DIAMANTE"
                imgTipo.ImageUrl = "imagenes/Diamante.png"
            Case 5
                lblTIPO.Text = "ORO"
                imgTipo.ImageUrl = "imagenes/Oro.png"
            Case 6
                lblTIPO.Text = "PLATA"
                imgTipo.ImageUrl = "imagenes/Plata.png"
        End Select
    End Sub

    Protected Sub btnEvaluar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEvaluar.Click
        asignarVARIABLES()
    End Sub
 
    Private Sub evaluacion()
        Dim OKPouchero, OKlatero, OKTira, OKBalcon, OKanaquel As Integer
        Dim OKMasCabecera, OKMasIsla, OKMasBotadero, OKmascota As Integer
        Dim OKCalIsla, OKCalBotadero, OKCalMixFeeding, OKCalMiniRack, OKcaliente As Integer
        Dim OKESMinirack, OKESWetmovil, OKES As Integer

        intentos = intentos + 1


        'evaluacion anaquel
        If AnaPouchero = sqlAnaPouchero Then
            OKPouchero = 1
            txtAnaquelPouchero.BackColor = Drawing.Color.LightGreen
            txtAnaquelPouchero.Enabled = False

        Else
            txtAnaquelPouchero.BackColor = Drawing.Color.Red
            txtAnaquelPouchero.Focus()
        End If

        If AnaLatero = sqlAnaLatero Then
            OKlatero = 1
            txtAnaquelLatero.BackColor = Drawing.Color.LightGreen
            txtAnaquelLatero.Enabled = False

        Else
            txtAnaquelLatero.BackColor = Drawing.Color.Red
            txtAnaquelLatero.Focus()
        End If

        If AnaTira = sqlAnaTira Then
            OKTira = 1
            txtAnaquelTira.BackColor = Drawing.Color.LightGreen
            txtAnaquelTira.Enabled = False

        Else
            txtAnaquelTira.BackColor = Drawing.Color.Red
            txtAnaquelTira.Focus()

        End If

        If AnaBalcon = sqlAnaBalcon Then
            OKBalcon = 1
            txtAnaquelBalcon.BackColor = Drawing.Color.LightGreen
            txtAnaquelBalcon.Enabled = False
        Else
            txtAnaquelBalcon.BackColor = Drawing.Color.Red
            txtAnaquelBalcon.Focus()
        End If

        'evaluar pasillo mascota

        If MasCabecera = sqlMasCabecera Then
            OKMasCabecera = 1
            txtMascotaCabecera.BackColor = Drawing.Color.LightGreen
            txtMascotaCabecera.Enabled = False
        Else
            txtMascotaCabecera.BackColor = Drawing.Color.Red
            txtMascotaCabecera.Focus()

        End If

        If MasIsla = sqlMasIsla Then
            OKMasIsla = 1
            txtMascotaIsla.BackColor = Drawing.Color.LightGreen
            txtMascotaIsla.Enabled = False
        Else
            txtMascotaIsla.BackColor = Drawing.Color.Red
            txtMascotaIsla.Focus()

        End If

        If MasBotadero = sqlMasBotadero Then
            OKMasBotadero = 1
            txtMascotaBotadero.BackColor = Drawing.Color.LightGreen
            txtMascotaBotadero.Enabled = False
        Else
            txtMascotaBotadero.BackColor = Drawing.Color.Red
            txtMascotaBotadero.Focus()
        End If

        'validar caliente
        If CalIsla = 1 Then
            OKCalIsla = 1
            txtCalienteIsla.Enabled = False
            txtCalienteIsla.BackColor = Drawing.Color.LightGreen
        Else
            txtCalienteIsla.BackColor = Drawing.Color.Red
            txtCalienteIsla.Focus()
        End If

        If CalBotadero = sqlCaliBotadero Then
            OKCalBotadero = 1
            txtCalienteBotadero.BackColor = Drawing.Color.LightGreen
            txtCalienteBotadero.Enabled = False

        Else
            txtCalienteBotadero.BackColor = Drawing.Color.Red
            txtCalienteBotadero.Focus()

        End If

        If CalMixFeeding = sqlCaliMixFeeding Then
            OKCalMixFeeding = 1
            txtCalienteMixfeeding.BackColor = Drawing.Color.LightGreen
            txtCalienteMixfeeding.Enabled = False
        Else
            txtCalienteMixfeeding.BackColor = Drawing.Color.Red
            txtCalienteMixfeeding.Focus()
        End If

        If CalMinirack = sqlCaliMinirack Then
            OKCalMiniRack = 1
            txtCalienteMiniRack.BackColor = Drawing.Color.LightGreen
            txtCalienteMiniRack.Enabled = False
        Else
            txtCalienteMiniRack.BackColor = Drawing.Color.Red
            txtCalienteMiniRack.Focus()
        End If

        'EVALUAR ENTRADA SALIDA
        If ESMiniRack = sqlESMiniRack Then
            OKESMinirack = 1
            txtESMinirack.Enabled = False
            txtESMinirack.BackColor = Drawing.Color.LightGreen
        Else
            txtESMinirack.BackColor = Drawing.Color.Red
            txtESMinirack.Focus()

        End If

        If ESwet = sqlESWet Then
            OKESWetmovil = 1
            txtESWetMovil.BackColor = Drawing.Color.LightGreen
            txtESWetMovil.Enabled = False
        Else
            txtESWetMovil.BackColor = Drawing.Color.Red
            txtESWetMovil.Focus()
        End If


        'validaciones generales

        If OKPouchero = 1 And OKlatero = 1 And OKTira = 1 And OKBalcon = 1 Then
            OKanaquel = 1
            lblAvisoAnaquel.Text = "OK ANAQUEL"
        End If

        If OKMasIsla = 1 And OKMasCabecera = 1 And OKMasBotadero = 1 Then
            OKmascota = 1
            lblAvisoMascota.Text = "OK MASCOTA"
        End If

        If OKCalIsla = 1 And OKCalBotadero = 1 And OKCalMiniRack = 1 And OKCalMixFeeding = 1 Then
            OKcaliente = 1
            lblAvisoCaliente.Text = "OK CALIENTE"
        End If

        If OKESMinirack = 1 And OKESWetmovil = 1 Then
            OKES = 1
            lblAvisoES.Text = "OK ENTRADA SALIDA"
        End If

        If intentos = 5 Then
            lblAvisoGral.Text = "RESPUESTAS ABAJO"
            lblAvisoAnaquel.Text = "POUCHERO = " & sqlAnaPouchero & " LATERO = " & sqlAnaLatero & " TIRAS = " & sqlAnaTira & " BALCON = " & sqlAnaBalcon
            lblAvisoMascota.Text = "CABECERA = " & sqlMasCabecera & " ISLA = " & sqlMasIsla & " Botadero = " & sqlMasBotadero
            lblAvisoES.Text = "MINIRACK = " & sqlESMiniRack & " WETMOVIL = " & sqlESWet
            lblAvisoCaliente.Text = "ISLA = 1" & "BOTADERO = " & sqlCaliBotadero & " MIX FEEDING = " & sqlCaliMixFeeding & "MINI RACK = " & sqlCaliMinirack
        End If

        If OKanaquel = 1 And OKcaliente = 1 And OKmascota = 1 And OKES = 1 Then
            guardar()
        Else
            lblAvisoGral.Text = "ATENCION CON LOS CUADROS EN ROJO, ESTA EQUIVOCADO EL NUMERO DE EXHIBICIONES"
        End If


    End Sub

    Private Sub guardar()
        Dim estatus As Integer = 1
        Dim cnn As New SqlConnection(ConexionMars.localSqlServer)
        cnn.Open()
        Dim SQLEditar As New SqlCommand("execute AS_PI_CrearLogros '" & HttpContext.Current.User.Identity.Name & "' , " & id_formato & ",'" & id_tipo & "','" & estatus & "'", cnn)
        SQLEditar.ExecuteNonQuery()
        SQLEditar.Dispose()
        cnn.Close()
        cnn.Dispose()
        Response.Redirect("menu_logros.aspx")

    End Sub

    Private Sub obtenerPAUTA()
        obtenerAnaquel()
        obtenerMascota()
        obtenerES()
        obtenerCaliente()
    End Sub

    Private Sub obtenerAnaquel()


        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim SQL As New SqlCommand("execute AS_PI_evaAnaquel " & id_tipo & ",'" & id_formato & "'", cnn)
            cnn.Open()
            Dim Data As New SqlDataAdapter(SQL)
            Dim Tabla As New DataTable
            Data.Fill(Tabla)
            cnn.Close()
            cnn.Dispose()
            If Tabla.Rows.Count = 1 Then

                sqlAnaBalcon = Tabla.Rows(0)("Balcon")
                sqlAnaTira = Tabla.Rows(0)("Tiras")
                sqlAnaLatero = Tabla.Rows(0)("Latero")
                sqlAnaPouchero = Tabla.Rows(0)("Pouchero")


            End If
            SQL.Dispose()
            Data.Dispose()
            Tabla.Dispose()
            cnn.Dispose()
            cnn.Close()
        End Using

    End Sub

    Private Sub obtenerMascota()
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim SQL As New SqlCommand("execute AS_PI_evaMascota " & id_tipo & ",'" & id_formato & "'", cnn)
            cnn.Open()
            Dim Data As New SqlDataAdapter(SQL)
            Dim Tabla As New DataTable
            Data.Fill(Tabla)
            cnn.Close()
            cnn.Dispose()
            If Tabla.Rows.Count = 1 Then

                sqlMasCabecera = Tabla.Rows(0)("Cabecera")
                sqlMasIsla = Tabla.Rows(0)("Isla")
                sqlMasBotadero = Tabla.Rows(0)("Botadero")


            End If
            SQL.Dispose()
            Data.Dispose()
            Tabla.Dispose()
            cnn.Dispose()
            cnn.Close()
        End Using
    End Sub

    Private Sub obtenerES()
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)
            Dim SQL As New SqlCommand("execute AS_PI_evaES " & id_tipo & ",'" & id_formato & "'", cnn)
            cnn.Open()
            Dim Data As New SqlDataAdapter(SQL)
            Dim Tabla As New DataTable
            Data.Fill(Tabla)
            cnn.Close()
            cnn.Dispose()
            If Tabla.Rows.Count = 1 Then

                sqlESWet = Tabla.Rows(0)("WetMovil")
                sqlESMiniRack = Tabla.Rows(0)("Minirack")


            End If
            SQL.Dispose()
            Data.Dispose()
            Tabla.Dispose()
            cnn.Dispose()
            cnn.Close()
        End Using
    End Sub

    Private Sub obtenerCaliente()
        Using cnn As New SqlConnection(ConexionMars.localSqlServer)


            Dim SQL As New SqlCommand("execute AS_PI_evaCaliente " & id_tipo & ",'" & id_formato & "'", cnn)
            cnn.Open()
            Dim Data As New SqlDataAdapter(SQL)
            Dim Tabla As New DataTable
            Data.Fill(Tabla)
            cnn.Close()
            cnn.Dispose()
            If Tabla.Rows.Count = 1 Then

                sqlCaliMixFeeding = Tabla.Rows(0)("MixFeeding")
                sqlCaliMinirack = Tabla.Rows(0)("Minirack")
                'Agregar manual si es bodega y/o Mercado el objetivo de botadero es 1
                If id_formato = 1 Or id_formato = 8 Then
                    sqlCaliBotadero = 1
                Else
                    sqlCaliBotadero = Tabla.Rows(0)("Botadero")
                End If




            End If


            SQL.Dispose()
            Data.Dispose()
            Tabla.Dispose()
            cnn.Dispose()
            cnn.Close()
        End Using
    End Sub

    Private Sub asignarVARIABLES()
        'validarEntrada()
        'If aviAnaquel = 0 And aviCaliente = 0 And AviES = 0 And aviMascota = 0 Then
        asignarValores()
        obtenerPAUTA()
        evaluacion()
        lblAvisoGral.Text = ""
        'Else
        '    lblAvisoGral.Text = "TE FALTA ALGUN CAMPO POR CAPTURAR"
        'End If


    End Sub
    

    Private Sub validarEntrada()
        validarEntradaAnaquel()
        validarMascota()
        validarCaliente()
        validarES()
    End Sub


    Private Sub validarEntradaAnaquel()

        If txtAnaquelPouchero.Text = "" Or txtAnaquelPouchero.Text = " " Then
            txtAnaquelPouchero.BackColor = Drawing.Color.Yellow
            aviAnaquel = 1
        Else
            If Not IsNumeric(txtAnaquelPouchero.Text) Then
                Response.Write("SOLO DEBEN DE SER NUMEROS")
            Else

                txtAnaquelPouchero.BackColor = Drawing.Color.White
                aviAnaquel = 0

            End If

        
        End If

        If txtAnaquelLatero.Text = "" Or txtAnaquelLatero.Text = " " Then
            txtAnaquelLatero.BackColor = Drawing.Color.Yellow
            aviAnaquel = 1
        Else
            txtAnaquelLatero.BackColor = Drawing.Color.White
            aviAnaquel = 0
        End If
        If txtAnaquelTira.Text = "" Or txtAnaquelTira.Text = " " Then
            txtAnaquelTira.BackColor = Drawing.Color.Yellow
            aviAnaquel = 1
        Else
            txtAnaquelTira.BackColor = Drawing.Color.White
            aviAnaquel = 0
        End If
        If txtAnaquelBalcon.Text = "" Or txtAnaquelBalcon.Text = " " Then
            txtAnaquelBalcon.BackColor = Drawing.Color.Yellow
            aviAnaquel = 1
        Else
            txtAnaquelBalcon.BackColor = Drawing.Color.White
            aviAnaquel = 0
        End If
    End Sub

    Private Sub validarMascota()

        If txtMascotaCabecera.Text = "" Or txtMascotaCabecera.Text = " " Then
            txtMascotaCabecera.BackColor = Drawing.Color.Yellow
            aviMascota = 1
        Else
            txtMascotaCabecera.BackColor = Drawing.Color.White
            aviMascota = 0
        End If
        If txtMascotaIsla.Text = "" Or txtMascotaIsla.Text = " " Then
            txtMascotaIsla.BackColor = Drawing.Color.Yellow
            aviMascota = 1
        Else
            txtMascotaIsla.BackColor = Drawing.Color.White
            aviMascota = 0
        End If
        If txtMascotaBotadero.Text = "" Or txtMascotaBotadero.Text = " " Then
            txtMascotaBotadero.BackColor = Drawing.Color.Yellow
            aviMascota = 1
        Else
            txtMascotaBotadero.BackColor = Drawing.Color.White
            aviMascota = 0
        End If

    End Sub

    Private Sub validarCaliente()

        If txtCalienteIsla.Text = "" Or txtCalienteIsla.Text = " " Then
            txtCalienteIsla.BackColor = Drawing.Color.Yellow
            aviCaliente = 1
        Else
            txtCalienteIsla.BackColor = Drawing.Color.White
            aviCaliente = 0
        End If
        If txtCalienteBotadero.Text = "" Or txtCalienteBotadero.Text = " " Then
            txtCalienteBotadero.BackColor = Drawing.Color.Yellow
            aviCaliente = 1
        Else
            txtCalienteBotadero.BackColor = Drawing.Color.White
            aviCaliente = 0
        End If
        If txtCalienteMixfeeding.Text = "" Or txtCalienteMixfeeding.Text = " " Then
            txtCalienteMixfeeding.BackColor = Drawing.Color.Yellow
            aviCaliente = 1
        Else
            txtCalienteMixfeeding.BackColor = Drawing.Color.White
            aviCaliente = 0
        End If
        If txtCalienteMiniRack.Text = "" Or txtCalienteMiniRack.Text = " " Then
            txtCalienteMiniRack.BackColor = Drawing.Color.Yellow
            aviCaliente = 1
        Else
            txtCalienteMiniRack.BackColor = Drawing.Color.White
            aviCaliente = 0
        End If

    End Sub

    Private Sub validarES()
        If txtESMinirack.Text = "" Or txtESMinirack.Text = " " Then
            txtESMinirack.BackColor = Drawing.Color.Yellow
            AviES = 1
        Else
            txtESMinirack.BackColor = Drawing.Color.White
            AviES = 0
        End If

        If txtESWetMovil.Text = "" Or txtESWetMovil.Text = " " Then
            AviES = 1
            txtESWetMovil.BackColor = Drawing.Color.Yellow
        Else
            AviES = 0
            txtESWetMovil.BackColor = Drawing.Color.White

        End If

    End Sub

    Private Sub asignarValores()

        AnaPouchero = txtAnaquelPouchero.Text
        AnaLatero = txtAnaquelLatero.Text
        AnaTira = txtAnaquelTira.Text
        AnaBalcon = txtAnaquelBalcon.Text

        MasCabecera = txtMascotaCabecera.Text
        MasIsla = txtMascotaIsla.Text
        MasBotadero = txtMascotaBotadero.Text

        CalIsla = txtCalienteIsla.Text
        CalBotadero = txtCalienteBotadero.Text
        CalMixFeeding = txtCalienteMixfeeding.Text
        CalMinirack = txtCalienteMiniRack.Text

        ESMiniRack = txtESMinirack.Text
        ESwet = txtESWetMovil.Text



    End Sub


End Class