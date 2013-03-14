Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.TreeNode
Imports System.Drawing
Imports System.Data.OleDb
Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Net.FtpWebRequest
Imports System.IO.Path
Imports System.Web.HttpException
Imports ISODates.Dates
Imports System.Security.Cryptography

Partial Public Class FormatoCapturaHawaiianBanana
    Inherits System.Web.UI.Page

    Dim IDTienda, IDUsuario, IDPeriodo, Marca As String
    Dim FolioAct, FolioPromo As String
    Dim fileExt As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionEnergizer.localSqlServer, HttpContext.Current.User.Identity.Name)

            VerDatos()
            CargaImagenes()

            If Edicion = 1 Then
                btnGuardar.Visible = True : Else
                btnGuardar.Visible = False : End If
        End If
    End Sub

    Private Sub VerDatos()
        Datos()

        ''//SI LA TIENDA YA ESTA CAPTURADA, SE ACTUALIZARA
        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        cnn.Open()
        Dim SQLBusca As New SqlCommand("SELECT * FROM HawaiianBanana_Historial WHERE folio_historial=" & FolioAct & "", cnn)
        Dim Tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQLBusca)
        Data.Fill(Tabla)

        If Tabla.Rows.Count > 0 Then
            FolioAct = Tabla.Rows(0)("folio_historial")
            txtComentarios.Text = Tabla.Rows(0)("comentarios_competencia")
        End If

        Tabla.Dispose()
        SQLBusca.Dispose()
        Data.Dispose()

        cnn.Close()
        cnn.Dispose()

        ''//DEMOS
        CargaGrilla(ConexionEnergizer.localSqlServer, _
                    "select MAR.id_marca, MAR.nombre_marca, ISNULL(HDET.cantidad_demos,0)cantidad_demos " & _
                    "from HawaiianBanana_Marcas as MAR " & _
                    "FULL JOIN (select * FROM HawaiianBanana_Competencia_Historial_Det " & _
                    "WHERE folio_historial=" & FolioAct & ") as HDET " & _
                    "ON MAR.id_marca= HDET.id_marca " & _
                    "WHERE MAR.id_marca<> " & Marca & " ", Me.gridDemos)

        ''//FRENTES
        CargaGrilla(ConexionEnergizer.localSqlServer, _
                    "select MAR.id_marca, MAR.nombre_marca, ISNULL(HDET.cantidad_frentes,0)cantidad_frentes " & _
                    "from HawaiianBanana_Marcas as MAR " & _
                    "FULL JOIN (SELECT * FROM HawaiianBanana_Frentes_Historial_Det " & _
                    "WHERE folio_historial=" & FolioAct & ")as HDET ON MAR.id_marca= HDET.id_marca " & _
                    "WHERE MAR.activo=1", Me.gridFrentes)

        ''//EXHIBIDORES PROPIOS
        CargaGrilla(ConexionEnergizer.localSqlServer, _
                    "select EXH.id_exhibidor, EXH.imagen, EXH.nombre_exhibidor, ISNULL(HDET.cantidad,0)cantidad " & _
                    "from HawaiianBanana_Exhibidores as EXH " & _
                    "FULL JOIN (SELECT * FROM HawaiianBanana_Exhibidores_Historial_Det " & _
                    "WHERE folio_historial=" & FolioAct & ") as HDET " & _
                    "ON EXH.id_exhibidor= HDET.id_exhibidor " & _
                    "WHERE EXH.activo=1 AND EXH.id_marca=" & Marca & "", Me.gridExhibidores)

        ''//EXHIBIDORES COMPETENCIA
        CargaGrilla(ConexionEnergizer.localSqlServer, _
                    "select MAR.id_marca, MAR.nombre_marca, ISNULL(HDET.cantidad_exhibidor,0)cantidad_exhibidor " & _
                    "from HawaiianBanana_Marcas as MAR " & _
                    "FULL JOIN (SELECT * FROM HawaiianBanana_Competencia_Historial_Det " & _
                    "WHERE folio_historial=" & FolioAct & ")as HDET ON MAR.id_marca= HDET.id_marca " & _
                    "WHERE MAR.id_marca<>" & Marca & "", Me.gridCompetencia)

        ''//CARGAR PRODUCTOS
        CargaGrilla(ConexionEnergizer.localSqlServer, _
                    "select PRO.id_marca, PRO.id_producto, PRO.nombre_producto, ISNULL(HDET.cantidad,0)cantidad " & _
                        "from HawaiianBanana_Productos as PRO " & _
                        "FULL JOIN (select * FROM HawaiianBanana_Productos_Historial_Det " & _
                        "WHERE folio_historial=" & FolioAct & ") as HDET " & _
                        "ON PRO.id_producto= HDET.id_producto WHERE PRO.id_marca = " & Marca & "",mE.gridProductosVendidos)

        ''//PROMOCIONALES
        CargaGridPromocionales()

        ''//PROMOCIONALES
        CargaGridVerPromos()
    End Sub

    Sub CargaGridPromocionales()
        Datos()

        CargaGrilla(ConexionEnergizer.localSqlServer, _
                    "select H.folio_historial, H.nombre_cliente, H.ticket, SUM(HDET.cantidad) as subtotales " & _
                    "From HawaiianBanana_Promos_Historial as H " & _
                    "INNER JOIN HawaiianBanana_Promos_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial " & _
                    "WHERE H.id_periodo = '" & IDPeriodo & "' AND H.id_usuario='" & IDUsuario & "' AND H.id_tienda ='" & IDTienda & "' AND H.id_marca =" & Marca & " " & _
                    "GROUP BY H.folio_historial,H.nombre_cliente, H.ticket", mE.gridPromocionales)
    End Sub

    Sub CargaGridVerPromos()
        CargaGrilla(ConexionEnergizer.localSqlServer, _
                    "select PR.id_promo, PR.nombre_promo, 0 as cantidad, 0 as subtotales " & _
                     "from HawaiianBanana_Promos as PR WHERE PR.activo =1 AND PR.id_marca=" & Marca & " " & _
                     "ORDER BY PR.nombre_promo ", Me.gridPromos)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        btnGuardar.Enabled = False

        Guardar()
    End Sub

    Sub Guardar()
        Datos()

        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        cnn.Open()

        Dim SQL As New SqlCommand("select * from HawaiianBanana_Historial WHERE folio_historial = " & FolioAct & "", cnn)
        Dim Tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQL)
        Data.Fill(Tabla)

        If Tabla.Rows.Count = 1 Then
            Dim SQLEditar As New SqlCommand("execute pcdt_HawaiianBanana_EditarHistorial " & FolioAct & ",'" & IDPeriodo & "','" & IDUsuario & "','" & IDTienda & "','" & txtComentarios.Text & "', " & Marca & "", cnn)
            SQLEditar.ExecuteNonQuery()
            SQLEditar.Dispose()

            GuardaDetalles(FolioAct)
            CambioEstatus(FolioAct)
        Else
            Dim SQLNuevo As New SqlCommand("execute pcdt_HawaiianBanana_CrearHistorial '" & IDPeriodo & "','" & IDUsuario & "','" & IDTienda & "','" & txtComentarios.Text & "', " & Marca & "", cnn)
            FolioAct = Convert.ToInt32(SQLNuevo.ExecuteScalar())
            SQLNuevo.Dispose()

            GuardaDetalles(FolioAct)
            CambioEstatus(FolioAct)
        End If

        cnn.Close()
        cnn.Dispose()

        Response.Redirect("RutaHawaiianBanana.aspx")
    End Sub

    Private Function GuardaDetalles(ByVal folio As Integer) As Boolean
        Dim id_marca As Integer
        For I As Integer = 0 To gridDemos.Rows.Count - 1
            id_marca = gridDemos.DataKeys(I).Value.ToString()
            Dim txtDemos As TextBox = CType(gridDemos.Rows(I).FindControl("txtDemos"), TextBox)
            Dim txtExhibidor As TextBox = CType(gridCompetencia.Rows(I).FindControl("txtExhibidor"), TextBox)

            If txtDemos.Text = "" Or txtDemos.Text = " " Then
                txtDemos.Text = "0" : End If
            If txtExhibidor.Text = "" Or txtExhibidor.Text = " " Then
                txtExhibidor.Text = "0" : End If

            GuardaHistorial(folio, id_marca, txtDemos.Text, txtExhibidor.Text)
        Next

        For I As Integer = 0 To gridFrentes.Rows.Count - 1
            id_marca = gridFrentes.DataKeys(I).Value.ToString()
            Dim txtFrentes As TextBox = CType(gridFrentes.Rows(I).FindControl("txtFrentes"), TextBox)

            If txtFrentes.Text = "" Or txtFrentes.Text = " " Then
                txtFrentes.Text = "0" : End If

            GuardaHistorialFrentes(folio, id_marca, txtFrentes.Text)
        Next

        Dim id_exhibidor As Integer
        For I As Integer = 0 To gridExhibidores.Rows.Count - 1
            id_exhibidor = gridExhibidores.DataKeys(I).Value.ToString()
            Dim txtExhibidor As TextBox = CType(gridExhibidores.Rows(I).FindControl("txtExhibidor"), TextBox)

            If txtExhibidor.Text = "" Or txtExhibidor.Text = " " Then
                txtExhibidor.Text = "0" : End If

            GuardaHistorialExhihidor(folio, id_exhibidor, txtExhibidor.Text)
        Next

        ''//GUARDAR PRODUCTOS
        Dim id_producto As Integer
        For I As Integer = 0 To gridProductosVendidos.Rows.Count - 1
            id_producto = gridProductosVendidos.DataKeys(I).Value.ToString()
            Dim txtCantidad As TextBox = CType(gridProductosVendidos.Rows(I).FindControl("txtCantidad"), TextBox)
            If txtCantidad.Text = "" Or txtCantidad.Text = " " Then
                txtCantidad.Text = "0" : End If

            GuardaHistorialProductos(folio, id_producto, txtCantidad.Text)
        Next

    End Function

    Private Function GuardaHistorial(ByVal folio_historial As Integer, ByVal id_marca As Integer, ByVal Demos As Double, ByVal Exhibidor As Double) As Boolean
        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        cnn.Open()

        Dim SQL As New SqlCommand("select * from HawaiianBanana_Competencia_Historial_Det WHERE folio_historial = " & folio_historial & " AND id_marca ='" & id_marca & "'", cnn)
        Dim Tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQL)
        Data.Fill(Tabla)

        If Demos <> 0 Or Exhibidor <> 0 Then
            If Tabla.Rows.Count = 1 Then
                Dim SQLEditar As New SqlCommand("execute pcdt_HawaiianBanana_EditarHistorial_Comp_Det " & folio_historial & ",'" & id_marca & "'," & Demos & "," & Exhibidor & "", cnn)
                SQLEditar.ExecuteNonQuery()
                SQLEditar.Dispose()
            Else
                Dim SQLNuevo As New SqlCommand("execute pcdt_HawaiianBanana_CrearHistorial_Comp_Det " & folio_historial & ",'" & id_marca & "'," & Demos & "," & Exhibidor & "", cnn)
                SQLNuevo.ExecuteNonQuery()
                SQLNuevo.Dispose()
            End If
        Else
            If Tabla.Rows.Count = 1 Then
                Dim SQLEliminar As New SqlCommand("DELETE FROM HawaiianBanana_Competencia_Historial_Det WHERE folio_historial = " & folio_historial & " AND id_marca= '" & id_marca & "'", cnn)
                SQLEliminar.ExecuteNonQuery()
                SQLEliminar.Dispose()
            End If
        End If

        cnn.Close()
        cnn.Dispose()
    End Function

    Private Function GuardaHistorialFrentes(ByVal folio_historial As Integer, ByVal id_marca As Integer, ByVal Frentes As Double) As Boolean
        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        cnn.Open()

        Dim SQL As New SqlCommand("select * from HawaiianBanana_Frentes_Historial_Det WHERE folio_historial = " & folio_historial & " AND id_marca ='" & id_marca & "'", cnn)
        Dim Tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQL)
        Data.Fill(Tabla)

        If Frentes <> 0 Then
            If Tabla.Rows.Count = 1 Then
                Dim SQLEditar As New SqlCommand("execute pcdt_HawaiianBanana_EditarHistorial_Fr_Det " & folio_historial & ",'" & id_marca & "'," & Frentes & "", cnn)
                SQLEditar.ExecuteNonQuery()
                SQLEditar.Dispose()
            Else
                Dim SQLNuevo As New SqlCommand("execute pcdt_HawaiianBanana_CrearHistorial_Fr_Det " & folio_historial & ",'" & id_marca & "'," & Frentes & "", cnn)
                SQLNuevo.ExecuteNonQuery()
                SQLNuevo.Dispose()
            End If
        Else
            If Tabla.Rows.Count = 1 Then
                Dim SQLEliminar As New SqlCommand("DELETE FROM HawaiianBanana_Frentes_Historial_Det WHERE folio_historial = " & folio_historial & " AND id_marca= '" & id_marca & "'", cnn)
                SQLEliminar.ExecuteNonQuery()
                SQLEliminar.Dispose()
            End If
        End If

        cnn.Close()
        cnn.Dispose()
    End Function

    Private Function GuardaHistorialExhihidor(ByVal folio_historial As Integer, ByVal id_exhibidor As Integer, ByVal Exhibidor As Double) As Boolean
        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        cnn.Open()

        Dim SQL As New SqlCommand("select * from HawaiianBanana_Exhibidores_Historial_Det WHERE folio_historial = " & folio_historial & " AND id_exhibidor ='" & id_exhibidor & "'", cnn)
        Dim Tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQL)
        Data.Fill(Tabla)

        If Exhibidor <> 0 Then
            If Tabla.Rows.Count = 1 Then
                Dim SQLEditar As New SqlCommand("execute pcdt_HawaiianBanana_EditarHistorial_Exh_Det " & folio_historial & "," & id_exhibidor & "," & Exhibidor & "", cnn)
                SQLEditar.ExecuteNonQuery()
                SQLEditar.Dispose()
            Else
                Dim SQLNuevo As New SqlCommand("execute pcdt_HawaiianBanana_CrearHistorial_Exh_Det " & folio_historial & "," & id_exhibidor & "," & Exhibidor & "", cnn)
                SQLNuevo.ExecuteNonQuery()
                SQLNuevo.Dispose()
            End If
        Else
            If Tabla.Rows.Count = 1 Then
                Dim SQLEliminar As New SqlCommand("DELETE FROM HawaiianBanana_Exhibidores_Historial_Det WHERE folio_historial = " & folio_historial & " AND id_exhibidor= '" & id_exhibidor & "'", cnn)
                SQLEliminar.ExecuteNonQuery()
                SQLEliminar.Dispose()
            End If
        End If

        cnn.Close()
        cnn.Dispose()
    End Function

    Private Function GuardaHistorialProductos(ByVal folio_historial As Integer, ByVal id_producto As Integer, ByVal cantidad As Double) As Boolean
        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        cnn.Open()

        Dim SQL As New SqlCommand("select * from HawaiianBanana_Productos_Historial_Det WHERE folio_historial = " & folio_historial & " AND id_producto ='" & id_producto & "'", cnn)
        Dim Tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQL)
        Data.Fill(Tabla)

        If cantidad <> 0 Then
            If Tabla.Rows.Count = 1 Then
                Dim SQLEditar As New SqlCommand("execute pcdt_HawaiianBanana_EditarHistorial_Productos " & folio_historial & ",'" & id_producto & "'," & cantidad & "", cnn)
                SQLEditar.ExecuteNonQuery()
                SQLEditar.Dispose()
            Else
                Dim SQLNuevo As New SqlCommand("execute pcdt_HawaiianBanana_CrearHistorial_Productos " & folio_historial & ",'" & id_producto & "'," & cantidad & "", cnn)
                SQLNuevo.ExecuteNonQuery()
                SQLNuevo.Dispose()
            End If
        Else
            If Tabla.Rows.Count = 1 Then
                Dim SQLEliminar As New SqlCommand("DELETE FROM HawaiianBanana_Productos_Historial_Det WHERE folio_historial = " & folio_historial & " AND id_producto= '" & id_producto & "'", cnn)
                SQLEliminar.ExecuteNonQuery()
                SQLEliminar.Dispose()
            End If
        End If

        cnn.Close()
        cnn.Dispose()
    End Function

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("RutaHawaiianBanana.aspx")
    End Sub

    Private Function CambioEstatus(ByVal folio As Integer) As Boolean
        Datos()

        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        cnn.Open()

        Dim SQL As New SqlCommand("select * from HawaiianBanana_Productos_Historial_Det WHERE folio_historial =" & folio & "", cnn)
        Dim Tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQL)
        Data.Fill(Tabla)

        Dim Estatus As Integer
        If Tabla.Rows.Count > 0 Then
            Estatus = 1 : Else
            Estatus = 2 : End If

        Dim SQLEstatus As New SqlCommand("UPDATE HawaiianBanana_Rutas_Eventos SET estatus" & Marca & " =@estatus " & _
                "FROM HawaiianBanana_Rutas_Eventos " & _
                "WHERE id_periodo = @id_periodo AND id_usuario = @id_usuario AND id_tienda = @id_tienda", cnn)
        SQLEstatus.Parameters.AddWithValue("@id_periodo", IDPeriodo)
        SQLEstatus.Parameters.AddWithValue("@id_usuario", IDUsuario)
        SQLEstatus.Parameters.AddWithValue("@id_tienda", IDTienda)
        SQLEstatus.Parameters.AddWithValue("@estatus", Estatus)
        SQLEstatus.ExecuteNonQuery()
        SQLEstatus.Dispose()

        cnn.Close()
        cnn.Dispose()
    End Function

    Sub Datos()
        Marca = Request.Params("marca")
        FolioAct = Request.Params("folio")
        IDTienda = Request.Params("tienda")
        IDUsuario = Request.Params("usuario")
        IDPeriodo = Request.Params("periodo")
    End Sub

    Protected Sub lnkFormato_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkFormato.Click
        pnlDemostradora.Visible = True
        pnlPromos.Visible = False
        pnlFotos.Visible = False
    End Sub

    Private Sub lnkPromocionales_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPromocionales.Click
        pnlDemostradora.Visible = False
        pnlPromos.Visible = True
        pnlFotos.Visible = False
    End Sub

    Private Sub lnkFotos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkFotos.Click
        pnlDemostradora.Visible = False
        pnlPromos.Visible = False
        pnlFotos.Visible = True
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Datos()

        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        cnn.Open()

        Dim SQLNuevo As New SqlCommand("INSERT INTO HawaiianBanana_Promos_Historial" & _
                                        "(id_usuario,id_periodo,id_tienda,nombre_cliente,ticket,id_producto,id_marca) " & _
                                        "VALUES(@id_usuario,@id_periodo,@id_tienda,@nombre_cliente,@ticket,@id_producto,@id_marca) SELECT @@IDENTITY AS 'folio_historial'", cnn)
        SQLNuevo.Parameters.AddWithValue("@id_periodo", IDPeriodo)
        SQLNuevo.Parameters.AddWithValue("@id_usuario", IDUsuario)
        SQLNuevo.Parameters.AddWithValue("@id_tienda", IDTienda)
        SQLNuevo.Parameters.AddWithValue("@nombre_cliente", txtNombre.Text)
        SQLNuevo.Parameters.AddWithValue("@ticket", txtTicket.Text)
        SQLNuevo.Parameters.AddWithValue("@id_producto", 0)
        SQLNuevo.Parameters.AddWithValue("@id_marca", Marca)
        FolioPromo = Convert.ToInt32(SQLNuevo.ExecuteScalar())
        SQLNuevo.Dispose()

        cnn.Close()
        cnn.Dispose()

        GuardaDetallePromo(FolioPromo)

        ''//Deja en Blanco
        txtNombre.Text = ""
        txtTicket.Text = ""

        CargaGridVerPromos()

        CargaGridPromocionales()
        txtNombre.Focus()
    End Sub

    Private Function GuardaDetallePromo(ByVal folio_historial As Integer) As Boolean
        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        cnn.Open()
        Dim id_promo As Integer
        For I As Integer = 0 To gridPromos.Rows.Count - 1
            id_promo = gridPromos.DataKeys(I).Value.ToString()
            Dim txtCantidad As TextBox = CType(gridPromos.Rows(I).FindControl("txtCantidad"), TextBox)

            If txtCantidad.Text = "" Or txtCantidad.Text = " " Then
                txtCantidad.Text = "0" : End If

            If txtCantidad.Text <> "0" Then
                Dim SQLNuevoPromo As New SqlCommand("insert into HawaiianBanana_Promos_Historial_Det " & _
                                                    "(folio_historial,id_promo,cantidad) " & _
                                                    "values(@folio_historial,@id_promo,@cantidad)", cnn)
                SQLNuevoPromo.Parameters.AddWithValue("@folio_historial", folio_historial)
                SQLNuevoPromo.Parameters.AddWithValue("@id_promo", id_promo)
                SQLNuevoPromo.Parameters.AddWithValue("@cantidad", txtCantidad.Text)
                SQLNuevoPromo.ExecuteNonQuery()
                SQLNuevoPromo.Dispose()
            End If
        Next

        cnn.Close()
        cnn.Dispose()
    End Function

    Protected Sub gridPromocionales_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridPromocionales.RowDeleting
        Dim FolioHistorial As String = gridPromocionales.Rows(e.RowIndex).Cells(1).Text

        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        cnn.Open()
        Dim SQLEliminarH As New SqlCommand("DELETE FROM HawaiianBanana_Promos_Historial WHERE folio_historial = " & FolioHistorial & "", cnn)
        SQLEliminarH.ExecuteNonQuery()
        SQLEliminarH.Dispose()

        Dim SQLEliminarHDET As New SqlCommand("DELETE FROM HawaiianBanana_Promos_Historial_Det WHERE folio_historial = " & FolioHistorial & "", cnn)
        SQLEliminarHDET.ExecuteNonQuery()
        SQLEliminarHDET.Dispose()

        CargaGridPromocionales()
        CargaGridVerPromos()

        cnn.Close()
        cnn.Dispose()
    End Sub

    Protected Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridPromocionales.Columns(1).Visible = False
    End Sub

    Protected Sub btnSubir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubir.Click
        Dim NombreFoto As String
        Datos()

        Using cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
            Dim Busca As New SqlCommand("SELECT * From HawaiianBanana_Galerias_Historial WHERE id_periodo='" & IDPeriodo & "' AND id_tienda= '" & IDTienda & "' ORDER BY no_foto DESC", cnn)
            cnn.Open()
            Dim tabla As New DataTable
            Dim Data As New SqlDataAdapter(Busca)
            Data.Fill(tabla)

            Dim NoFoto As String
            If tabla.Rows.Count > 0 Then
                NoFoto = tabla.Rows(0)("no_foto") + 1 : Else
                NoFoto = "1" : End If

            Data.Dispose()
            tabla.Dispose()

            NombreFoto = (IDPeriodo + "-" + IDTienda + "-" + NoFoto + ".JPG")
            Dim SQLFoto As New SqlCommand("INSERT INTO HawaiianBanana_Galerias_Historial " & _
                         "(id_periodo,id_usuario, id_tienda, id_marca, descripcion, ruta, foto, no_foto) " & _
                         "VALUES(@id_periodo,@id_usuario, @id_tienda,@id_marca,@descripcion, @ruta, @foto, @no_foto)", cnn)
            SQLFoto.Parameters.AddWithValue("@id_periodo", IDPeriodo)
            SQLFoto.Parameters.AddWithValue("@id_usuario", IDUsuario)
            SQLFoto.Parameters.AddWithValue("@id_tienda", IDTienda)
            SQLFoto.Parameters.AddWithValue("@descripcion", txtDescripcion.Text)
            SQLFoto.Parameters.AddWithValue("@ruta", "/ARCHIVOS/CLIENTES/ENERGIZER/HB/IMAGENES/")
            SQLFoto.Parameters.AddWithValue("@foto", NombreFoto)
            SQLFoto.Parameters.AddWithValue("@no_foto", NoFoto)
            SQLFoto.Parameters.AddWithValue("@id_marca", Marca)
            SQLFoto.ExecuteNonQuery()
            SQLFoto.Dispose()

            cnn.Close()
            cnn.Dispose()
        End Using


        fileExt = System.IO.Path.GetExtension(FileUpload1.FileName)

        If (fileExt = ".exe") Then
            lblSubida.Text = "No puedes subir archivos .exe"
        Else
            If FileUpload1.HasFile = True Then
                Dim path As String = Server.MapPath("~/ARCHIVOS/CLIENTES/ENERGIZER/HB/IMAGENES/")
                FileUpload1.PostedFile.SaveAs(path & NombreFoto)
            End If
            lblSubida.Text = "El archivo fue subido correctamente"
        End If

        CargaImagenes()

        txtDescripcion.Text = ""
    End Sub

    Sub CargaImagenes()
        Datos()

        Using cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
            Dim sql As String = "SELECT DISTINCT * FROM HawaiianBanana_Galerias_Historial as HDET " & _
                            "WHERE HDET.id_usuario='" & IDUsuario & "' " & _
                            "AND HDET.id_periodo='" & IDPeriodo & "' " & _
                            "AND HDET.id_tienda='" & IDTienda & "' AND HDET.id_marca ='" & Marca & "' ORDER BY HDET.no_foto"

            Dim cmd As New SqlCommand(sql, cnn)
            Dim adapter As New SqlDataAdapter
            adapter.SelectCommand = cmd
            cnn.Open()
            Dim dataset As New DataSet
            adapter.Fill(dataset, "descripcion")

            Me.gridImagenes.DataSource = dataset
            Me.gridImagenes.DataBind()
            cnn.Close()
            cnn.Dispose()
        End Using
    End Sub

    Private Sub gridImagenes_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridImagenes.RowDeleting
        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        cnn.Open()
        Dim cmdCap As New SqlCommand("DELETE FROM HawaiianBanana_Galerias_Historial WHERE folio_foto = " & gridImagenes.DataKeys(e.RowIndex).Value.ToString() & "", cnn)

        Dim tabla As New DataTable
        Dim SQL As New SqlDataAdapter(cmdCap)
        SQL.Fill(tabla)

        cnn.Close()
        cnn.Dispose()

        CargaImagenes()
    End Sub

End Class