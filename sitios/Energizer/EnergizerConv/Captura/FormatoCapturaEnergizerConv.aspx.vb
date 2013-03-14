Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports ISODates.Dates
Imports System.Security.Cryptography

Partial Public Class FormatoCapturaEnergizerConv
    Inherits System.Web.UI.Page

    Dim IDTienda, IDUsuario, IDPeriodo, FolioAct As String
    Dim MaterialEnergizer, MaterialOtro As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionEnergizer.localSqlServer, HttpContext.Current.User.Identity.Name)

            VerDatos()

            If Edicion = 1 Then
                btnGuardar.Visible = True
                btnSubir.Visible = True
            Else
                btnGuardar.Visible = False
                btnSubir.Visible = False
            End If
        End If
    End Sub

    Private Sub VerDatos()
        Datos()

        ''//SI LA TIENDA YA ESTA CAPTURADA, SE ACTUALIZARA
        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        cnn.Open()

        Dim SQLVer As New SqlCommand("SELECT * FROM Energizer_Conv_Productos_Historial " & _
                            "WHERE id_periodo = @id_periodoAND id_usuario = @id_usuario AND id_tienda = @id_tienda", cnn)
        SQLVer.Parameters.AddWithValue("@id_periodo", IDPeriodo)
        SQLVer.Parameters.AddWithValue("@id_usuario", IDUsuario)
        SQLVer.Parameters.AddWithValue("@id_tienda", IDTienda)

        Dim Tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQLVer)
        Data.Fill(Tabla)

        If Tabla.Rows.Count > 0 Then
            CargaImagenes()

            txtComentarios.Text = Tabla.Rows(0)("comentarios")
            txtFecha.Text = Format(Tabla.Rows(0)("fecha_visita"), "dd/MM/yyyy")
            txtEncargado.Text = Tabla.Rows(0)("encargado_tienda")
            txtCantidadEnergizer.Text = Tabla.Rows(0)("cantidad_energizer")
            txtCantidadOtro.Text = Tabla.Rows(0)("cantidad_otros")
            txtPromoE.Text = Tabla.Rows(0)("promo_e")
            txtPromoD.Text = Tabla.Rows(0)("promo_d")
            MaterialEnergizer = Tabla.Rows(0)("material_pop_energizer")
            If MaterialEnergizer = 1 Then
                chkMaterialEnergizer.Checked = True:Else
                chkMaterialEnergizer.Checked = False:End If

            MaterialOtro = Tabla.Rows(0)("material_pop_otros")
            If MaterialOtro = 1 Then
                chkMaterialOtro.Checked = True:Else
                chkMaterialOtro.Checked = False: End If
        End If

        SQLVer.Dispose()
        Tabla.Dispose()
        Data.Dispose()
        cnn.Close()
        cnn.Dispose()

        CargaGrilla(ConexionEnergizer.localSqlServer, _
                    "SELECT PROD.codigo, PROD.id_producto, PROD.nombre_producto, HDET.existencia as existencia, HDET.pedidos as pedidos " & _
                    "FROM Energizer_Productos AS PROD " & _
                    "INNER JOIN Energizer_Conv_Productos AS PRODENE " & _
                    "ON PRODENE.id_producto = PROD.id_producto " & _
                    "FULL JOIN (SELECT * FROM Energizer_Conv_Productos_Historial_Det " & _
                    "WHERE folio_historial=" & FolioAct & ")AS HDET " & _
                    "ON HDET.id_producto = PROD.id_producto " & _
                    "WHERE PROD.tipo_producto= 1", Me.gridProductosCliente)

        ''Carga competencia
        CargaGrilla(ConexionEnergizer.localSqlServer, _
                    "SELECT PROD.codigo, PROD.id_producto, PROD.nombre_producto, HDET.existencia as existencia, HDET.pedidos as pedidos " & _
                    "FROM Energizer_Productos AS PROD " & _
                    "INNER JOIN Energizer_Conv_Productos AS PRODENE " & _
                    "ON PRODENE.id_producto = PROD.id_producto " & _
                    "FULL JOIN (SELECT * FROM Energizer_Conv_Productos_Historial_Det " & _
                    "WHERE folio_historial=" & FolioAct & ")AS HDET " & _
                    "ON HDET.id_producto = PROD.id_producto " & _
                    "WHERE PROD.tipo_producto= 2", Me.gridCompetencia)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        If txtFecha.Text = "" Then
            lblFecha.Text = "LA FECHA DE VISITA ESTA VACIA"
            Exit Sub
        End If

        btnGuardar.Enabled = False

        Guardar()
    End Sub

    Sub Guardar()
        If chkMaterialEnergizer.Checked = True Then
            MaterialEnergizer = 1 : Else
            MaterialEnergizer = 0
            txtCantidadEnergizer.Text = "0" : End If

        If chkMaterialOtro.Checked = True Then
            MaterialOtro = 1 : Else
            MaterialOtro = 0
            txtCantidadOtro.Text = "0" : End If

        If txtCantidadEnergizer.Text = "" Then
            txtCantidadEnergizer.Text = "0" : End If
        If txtCantidadOtro.Text = "" Then
            txtCantidadOtro.Text = "0" : End If

        Datos()

        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        cnn.Open()

        Dim SQL As New SqlCommand("select * from Energizer_Conv_Productos_Historial WHERE folio_historial = " & FolioAct & "", cnn)
        Dim Tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQL)
        Data.Fill(Tabla)

        If Tabla.Rows.Count = 1 Then
            Dim SQLEditar As New SqlCommand("execute pcdt_Energizer_Conv_EditarHistorial " & FolioAct & ",'" & IDPeriodo & "','" & IDUsuario & "','" & IDTienda & "','" & txtEncargado.Text & "','" & txtComentarios.Text & "','" & txtPromoE.Text & "','" & txtPromoD.Text & "','" & ISODates.Dates.SQLServerDate(CDate(txtFecha.Text)) & "'," & MaterialEnergizer & "," & txtCantidadEnergizer.Text & "," & MaterialOtro & ", " & txtCantidadOtro.Text & "", cnn)
            SQLEditar.ExecuteNonQuery()
            SQLEditar.Dispose()

            GuardaDetalles(FolioAct)
            CambioEstatus(FolioAct)
        Else
            Dim SQLNuevo As New SqlCommand("execute pcdt_Energizer_Conv_CrearHistorial '" & IDPeriodo & "','" & IDUsuario & "','" & IDTienda & "','" & txtEncargado.Text & "','" & txtComentarios.Text & "','" & txtPromoE.Text & "','" & txtPromoD.Text & "','" & ISODates.Dates.SQLServerDate(CDate(txtFecha.Text)) & "'," & MaterialEnergizer & "," & txtCantidadEnergizer.Text & "," & MaterialOtro & ", " & txtCantidadOtro.Text & "", cnn)
            FolioAct = Convert.ToInt32(SQLNuevo.ExecuteScalar())
            SQLNuevo.Dispose()

            GuardaDetalles(FolioAct)
            CambioEstatus(FolioAct)
        End If

        cnn.Dispose()
        cnn.Close()

        Response.Redirect("RutaEnergizerConv.aspx")
    End Sub

    Private Function GuardaDetalles(ByVal folio As Integer) As Boolean
        Dim id_articulo As Integer
        For I As Integer = 0 To gridProductosCliente.Rows.Count - 1
            id_articulo = gridProductosCliente.DataKeys(I).Value.ToString()
            Dim txtExistencia As TextBox = CType(gridProductosCliente.Rows(I).FindControl("txtExistencia"), TextBox)
            Dim txtPedidos As TextBox = CType(gridProductosCliente.Rows(I).FindControl("txtPedidos"), TextBox)

            If txtExistencia.Text = "" Or txtExistencia.Text = " " Then
                txtExistencia.Text = "0" : End If

            If txtPedidos.Text = "" Or txtPedidos.Text = " " Then
                txtPedidos.Text = "0" : End If

            GuardaHistorial(folio, id_articulo, txtExistencia.Text, txtPedidos.Text)
        Next

        ''//COMPETENCIA
        For I As Integer = 0 To gridCompetencia.Rows.Count - 1
            id_articulo = gridCompetencia.DataKeys(I).Value.ToString()
            Dim txtExistencia As TextBox = CType(gridCompetencia.Rows(I).FindControl("txtExistencia"), TextBox)

            If txtExistencia.Text = "" Or txtExistencia.Text = " " Then
                txtExistencia.Text = "0" : End If

            GuardaHistorial(folio, id_articulo, txtExistencia.Text, 0)
        Next

    End Function

    Private Function GuardaHistorial(ByVal folio_historial As Integer, ByVal id_producto As Integer, ByVal Existencia As Double, ByVal Pedidos As Double) As Boolean
        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        cnn.Open()

        Dim SQL As New SqlCommand("select * from Energizer_Conv_Productos_Historial_Det WHERE folio_historial = " & folio_historial & " AND id_producto ='" & id_producto & "'", cnn)
        Dim Tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQL)
        Data.Fill(Tabla)

        If Existencia <> 0 Or Pedidos <> 0 Then
            If Tabla.Rows.Count = 1 Then
                Dim SQLEditar As New SqlCommand("execute pcdt_Energizer_Conv_EditarHistorial_Det " & folio_historial & ",'" & id_producto & "'," & Existencia & "," & Pedidos & "", cnn)
                SQLEditar.ExecuteNonQuery()
                SQLEditar.Dispose()
            Else
                Dim SQLNuevo As New SqlCommand("execute pcdt_Energizer_Conv_CrearHistorial_Det " & folio_historial & ",'" & id_producto & "'," & Existencia & "," & Pedidos & "", cnn)
                SQLNuevo.ExecuteNonQuery()
                SQLNuevo.Dispose()
            End If
        Else
            If Tabla.Rows.Count = 1 Then
                Dim SQLEliminar As New SqlCommand("DELETE FROM Energizer_Conv_Productos_Historial_Det WHERE folio_historial = " & folio_historial & " AND id_producto= '" & id_producto & "'", cnn)
                SQLEliminar.ExecuteNonQuery()
                SQLEliminar.Dispose()
            End If
        End If

        cnn.Close()
        cnn.Dispose()
    End Function

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("javascript:history.go(-1)")
    End Sub

    Private Function CambioEstatus(ByVal folio As Integer) As Boolean
        Datos()

        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        cnn.Open()

        Dim SQL As New SqlCommand("select * from Energizer_Conv_Productos_Historial_Det WHERE folio_historial =" & folio & "", cnn)
        Dim Tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQL)
        Data.Fill(Tabla)

        Dim Estatus As Integer
        If Tabla.Rows.Count > 0 Then
            Estatus = 1 : Else
            Estatus = 2 : End If

        Dim SQLEstatus As New SqlCommand("UPDATE Energizer_Conv_Rutas_Eventos SET estatus =@estatus " & _
                "FROM Energizer_Conv_Rutas_Eventos " & _
                "WHERE id_periodo = @id_periodo AND id_usuario = @id_usuario AND id_tienda = @id_tienda", cnn)
        SQLEstatus.Parameters.AddWithValue("@id_periodo", IDPeriodo)
        SQLEstatus.Parameters.AddWithValue("@id_usuario", IDUsuario)
        SQLEstatus.Parameters.AddWithValue("@id_tienda", IDTienda)
        SQLEstatus.Parameters.AddWithValue("@estatus", Estatus)
        SQLEstatus.ExecuteNonQuery()
        SQLEstatus.Dispose()

        cnn.Dispose()
        cnn.Close()
    End Function

    Sub Datos()
        FolioAct = Request.Params("folio")
        IDTienda = Request.Params("tienda")
        IDUsuario = Request.Params("usuario")
        IDPeriodo = Request.Params("periodo")
    End Sub

    Protected Sub btnSubir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubir.Click
        Dim NombreFoto As String

        Datos()

        Using cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
            Dim cmdval As New SqlCommand("SELECT * From Energizer_Conv_Galerias WHERE id_periodo='" & IDPeriodo & "' AND id_tienda= '" & IDTienda & "' ORDER BY no_foto DESC", cnn)
            cnn.Open()
            Dim tabla As New DataTable
            Dim da As New SqlDataAdapter(cmdval)
            da.Fill(tabla)

            Dim NoFoto As String
            If tabla.Rows.Count > 0 Then
                NoFoto = tabla.Rows(0)("no_foto") + 1
            Else
                NoFoto = "1"
            End If

            Dim sqlGuardar As String
            sqlGuardar = "INSERT INTO Energizer_Conv_Galerias " & _
                         "(id_periodo,id_usuario, id_tienda, descripcion, ruta, foto, no_foto) " & _
                         "VALUES(@id_periodo,@id_usuario, @id_tienda, @descripcion, @ruta, @foto, @no_foto)"

            NombreFoto = (IDPeriodo + "-" + IDTienda + "-" + NoFoto + ".JPG")
            Dim cmd As New SqlCommand(sqlGuardar, cnn)
            cmd.Parameters.AddWithValue("@id_periodo", IDPeriodo)
            cmd.Parameters.AddWithValue("@id_usuario", IDUsuario)
            cmd.Parameters.AddWithValue("@id_tienda", IDTienda)
            cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text)
            cmd.Parameters.AddWithValue("@ruta", "/ARCHIVOS/CLIENTES/ENERGIZER/CONVENIENCIA/IMAGENES/")
            cmd.Parameters.AddWithValue("@foto", NombreFoto)
            cmd.Parameters.AddWithValue("@no_foto", NoFoto)
            cmd.ExecuteNonQuery()

            cmd.Dispose()
            cnn.Close()
            cnn.Dispose()
        End Using

        'FileUpload1.SaveAs(Server.MapPath("/ARCHIVOS/CLIENTES/CLOE/EQUIPAJE/IMAGENES/" + NombreFoto))
        'lblSubida.Text = "El archivo fue subido correctamente"
        Dim fileExt As String
        fileExt = System.IO.Path.GetExtension(FileUpload1.FileName)


        If (fileExt = ".exe") Then
            lblSubida.Text = "No puedes subir archivos .exe"
        Else
            If FileUpload1.HasFile = True Then
                Dim path As String = Server.MapPath("~/ARCHIVOS/CLIENTES/ENERGIZER/CONVENIENCIA/IMAGENES/")
                FileUpload1.PostedFile.SaveAs(path & NombreFoto)
            End If
            lblSubida.Text = "El archivo fue subido correctamente"
        End If

        ''//SUBIR
        ''Subir Archivo por FTP
        '' Configurar el Request
        'Dim ElRequest As FtpWebRequest = DirectCast(WebRequest.Create("ftp.procomlcd.mx" + "/Quia.jpg"), FtpWebRequest)
        'ElRequest.Credentials = New NetworkCredential("procomlc", "1210156")
        'ElRequest.Method = WebRequestMethods.Ftp.UploadFile
        ''Leer(archivo)

        'Dim BufferArchivo() As Byte = File.ReadAllBytes(FileUpload1.PostedFile.FileName)

        ''Subir(archivo)
        'Dim ElStream As System.IO.Stream = ElRequest.GetRequestStream()
        'ElStream.Write(BufferArchivo, 0, BufferArchivo.Length)
        'ElStream.Close()
        'ElStream.Dispose()
        CargaImagenes()
    End Sub

    Sub CargaImagenes()
        Datos()

        CargaGrilla(ConexionEnergizer.localSqlServer, _
                    "SELECT * FROM Energizer_Conv_Galerias " & _
                    "WHERE id_usuario='" & IDUsuario & "' " & _
                    "AND id_periodo=" & IDPeriodo & " " & _
                    "AND id_tienda=" & IDTienda & " ORDER BY no_foto", Me.gridImagenes)
    End Sub
End Class