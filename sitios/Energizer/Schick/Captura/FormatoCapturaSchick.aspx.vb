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

Partial Public Class FormatoCapturaSchick
    Inherits System.Web.UI.Page

    Dim IDTienda, IDUsuario, IDPeriodo As String
    Dim FolioAct, FolioPromo As String

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

        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        cnn.Open()
        Dim SQLBusca As New SqlCommand("SELECT * FROM Schick_Historial WHERE folio_historial=" & FolioAct & "", cnn)
        Dim tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQLBusca)
        Data.Fill(tabla)

        If tabla.Rows.Count > 0 Then
            txtComentarios.Text = tabla.Rows(0)("comentarios_competencia")
            txtOfertaciones.Text = tabla.Rows(0)("ofertaciones_competencia")
        End If

        SQLBusca.Dispose()
        tabla.Dispose()
        Data.Dispose()

        ''//DEMOS
        Dim DSdemos As New DataSet
        Dim SQLDemos As New SqlDataAdapter("select MAR.id_marca, MAR.nombre_marca, ISNULL(HDET.cantidad_demos,0)cantidad_demos " & _
                        "from Schick_Marcas as MAR " & _
                        "FULL JOIN (select * FROM Schick_Competencia_Historial_Det " & _
                        "WHERE folio_historial=" & FolioAct & ") as HDET " & _
                        "ON MAR.id_marca= HDET.id_marca " & _
                        "WHERE MAR.id_marca<>1 ", cnn)
        SQLDemos.Fill(DSdemos, "Schick_Competencia_Historial_Det")
        gridDemos.DataSource = DSdemos.Tables("Schick_Competencia_Historial_Det")
        gridDemos.DataBind()
        SQLDemos.Dispose()
        DSdemos.Dispose()

        ''//EXHIBIDORES PROPIOS
        Dim DSexhibidores As New DataSet
        Dim SQLExhibidores As New SqlDataAdapter("select EXH.id_exhibidor, EXH.imagen, EXH.nombre_exhibidor, ISNULL(HDET.cantidad,0)cantidad " & _
                        "from Schick_Exhibidores as EXH " & _
                        "FULL JOIN (SELECT * FROM Schick_Exhibidores_Historial_Det " & _
                        "WHERE folio_historial=" & FolioAct & ") as HDET " & _
                        "ON EXH.id_exhibidor= HDET.id_exhibidor " & _
                        "WHERE EXH.activo=1", cnn)
        SQLExhibidores.Fill(DSexhibidores, "Schick_Exhibidores_Historial_Det")
        gridExhibidores.DataSource = DSexhibidores.Tables("Schick_Exhibidores_Historial_Det")
        gridExhibidores.DataBind()
        SQLExhibidores.Dispose()
        DSexhibidores.Dispose()

        ''//EXHIBIDORES COMPETENCIA
        Dim DSComp As New DataSet
        Dim SQLComp As New SqlDataAdapter("select MAR.id_marca, MAR.nombre_marca, ISNULL(HDET.cantidad_exhibidor,0)cantidad_exhibidor " & _
                        "from Schick_Marcas as MAR " & _
                        "FULL JOIN (SELECT * FROM Schick_Competencia_Historial_Det " & _
                        "WHERE folio_historial=" & FolioAct & ")as HDET ON MAR.id_marca= HDET.id_marca " & _
                        "WHERE MAR.id_marca<>1", cnn)
        SQLComp.Fill(DSComp, "Schick_Competencia_Historial_Det")
        gridCompetencia.DataSource = DSComp.Tables("Schick_Competencia_Historial_Det")
        gridCompetencia.DataBind()
        SQLComp.Dispose()
        DSComp.Dispose()

        ''//CARGAR PRODUCTOS
        Dim DSProductos As New DataSet
        Dim SQLProductos As New SqlDataAdapter("select PRO.id_producto, PRO.nombre_producto, ISNULL(HDET.cantidad,0)cantidad " & _
                        "from Schick_Productos as PRO " & _
                        "FULL JOIN (select * FROM Schick_Productos_Historial_Det " & _
                        "WHERE folio_historial=" & FolioAct & ") as HDET " & _
                        "ON PRO.id_producto= HDET.id_producto ", cnn)
        SQLProductos.Fill(DSProductos, "Schick_Productos_Historial_Det")
        GridProductos.DataSource = DSProductos.Tables("Schick_Productos_Historial_Det")
        GridProductos.DataBind()
        SQLProductos.Dispose()
        DSProductos.Dispose()

        cnn.Close()
        cnn.Dispose()

        ''//PROMOCIONALES
        CargaGridPromocionales()

        ''//PROMOCIONALES
        CargaGridVerPromos()
    End Sub

    Sub CargaGridPromocionales()
        Datos()

        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        cnn.Open()
        Dim DSprom As New DataSet
        Dim SQLProm As New SqlDataAdapter("select H.folio_historial, H.nombre_cliente, H.ticket, SUM(HDET.cantidad) as subtotales " & _
                        "From Schick_Promos_Historial as H " & _
                        "INNER JOIN Schick_Promos_Historial_Det as HDET ON H.folio_historial= HDET.folio_historial " & _
                        "WHERE H.id_periodo = '" & IDPeriodo & "' AND H.id_usuario='" & IDUsuario & "' AND H.id_tienda ='" & IDTienda & "' " & _
                        "GROUP BY H.folio_historial,H.nombre_cliente, H.ticket", cnn)
        SQLProm.Fill(DSprom, "Schick_Promos_Historial_Det")
        gridPromocionales.DataSource = DSprom.Tables("Schick_Promos_Historial_Det")
        gridPromocionales.DataBind()
        SQLProm.Dispose()
        DSprom.Dispose()

        cnn.Close()
        cnn.Dispose()
    End Sub

    Sub CargaGridVerPromos()
        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        cnn.Open()
        Dim DS As New DataSet

        ''//PROMOCIONALES
        Dim SQL As New SqlDataAdapter("select PR.id_promo, PR.nombre_promo, 0 as cantidad, 0 as subtotales " & _
                        "from Schick_Promos as PR", cnn)
        SQL.Fill(DS, "DatosPromos")
        gridPromos.DataSource = DS.Tables("DatosPromos")
        gridPromos.DataBind()

        DS.Dispose()
        SQL.Dispose()

        cnn.Close()
        cnn.Dispose()
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        btnGuardar.Enabled = False

        Guardar()
    End Sub

    Sub Guardar()
        Datos()

        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        cnn.Open()

        Dim SQL As New SqlCommand("select * from Schick_Historial WHERE folio_historial = " & FolioAct & "", cnn)
        Dim Tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQL)
        Data.Fill(Tabla)

        If Tabla.Rows.Count = 1 Then
            Dim SQLEditar As New SqlCommand("execute pcdt_Schick_EditarHistorial " & FolioAct & ",'" & IDPeriodo & "','" & IDUsuario & "','" & IDTienda & "','" & txtComentarios.Text & "','" & txtOfertaciones.Text & "'", cnn)
            SQLEditar.ExecuteNonQuery()
            SQLEditar.Dispose()

            GuardaDetalles(FolioAct)
            CambioEstatus(FolioAct)
        Else
            Dim SQLNuevo As New SqlCommand("execute pcdt_Schick_CrearHistorial '" & IDPeriodo & "','" & IDUsuario & "','" & IDTienda & "','" & txtComentarios.Text & "','" & txtOfertaciones.Text & "'", cnn)
            FolioAct = Convert.ToInt32(SQLNuevo.ExecuteScalar())
            SQLNuevo.Dispose()

            GuardaDetalles(FolioAct)
            CambioEstatus(FolioAct)
        End If

        Me.Dispose()

        cnn.Close()
        cnn.Dispose()

        Response.Redirect("RutaSchick.aspx")
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
        For I As Integer = 0 To GridProductos.Rows.Count - 1
            id_producto = GridProductos.DataKeys(I).Value.ToString()
            Dim txtCantidadProductos As TextBox = CType(GridProductos.Rows(I).FindControl("txtcantidadPRODUCTOS"), TextBox)

            If txtCantidadProductos.Text = "" Or txtCantidadProductos.Text = " " Then
                txtCantidadProductos.Text = "0" : End If

            GuardaHistorialProductos(folio, id_producto, txtCantidadProductos.Text)
        Next

    End Function

    Private Function GuardaHistorial(ByVal folio_historial As Integer, ByVal id_marca As Integer, ByVal Demos As Double, ByVal Exhibidor As Double) As Boolean
        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        cnn.Open()

        Dim SQL As New SqlCommand("select * from Schick_Competencia_Historial_Det WHERE folio_historial = " & folio_historial & " AND id_marca ='" & id_marca & "'", cnn)
        Dim Tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQL)
        Data.Fill(Tabla)

        If Demos <> 0 Or Exhibidor <> 0 Then
            If Tabla.Rows.Count = 1 Then
                Dim SQLEditar As New SqlCommand("execute pcdt_Schick_EditarHistorial_Comp_Det " & folio_historial & ",'" & id_marca & "'," & Demos & "," & Exhibidor & "", cnn)
                SQLEditar.ExecuteNonQuery()
                SQLEditar.Dispose()
            Else
                Dim SQLNuevo As New SqlCommand("execute pcdt_Schick_CrearHistorial_Comp_Det " & folio_historial & ",'" & id_marca & "'," & Demos & "," & Exhibidor & "", cnn)
                SQLNuevo.ExecuteNonQuery()
                SQLNuevo.Dispose()
            End If
        Else
            If Tabla.Rows.Count = 1 Then
                Dim SQLEliminar As New SqlCommand("DELETE FROM Schick_Competencia_Historial_Det WHERE folio_historial = " & folio_historial & " AND id_marca= '" & id_marca & "'", cnn)
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

        Dim SQL As New SqlCommand("select * from Schick_Exhibidores_Historial_Det WHERE folio_historial = " & folio_historial & " AND id_exhibidor ='" & id_exhibidor & "'", cnn)
        Dim Tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQL)
        Data.Fill(Tabla)

        If Exhibidor <> 0 Then
            If Tabla.Rows.Count = 1 Then
                Dim SQLEditar As New SqlCommand("execute pcdt_Schick_EditarHistorial_Exh_Det " & folio_historial & "," & id_exhibidor & "," & Exhibidor & "", cnn)
                SQLEditar.ExecuteNonQuery()
                SQLEditar.Dispose()
            Else
                Dim SQLNuevo As New SqlCommand("execute pcdt_Schick_CrearHistorial_Exh_Det " & folio_historial & "," & id_exhibidor & "," & Exhibidor & "", cnn)
                SQLNuevo.ExecuteNonQuery()
                SQLNuevo.Dispose()
            End If
        Else
            If Tabla.Rows.Count = 1 Then
                Dim SQLEliminar As New SqlCommand("DELETE FROM Schick_Exhibidores_Historial_Det WHERE folio_historial = " & folio_historial & " AND id_exhibidor= '" & id_exhibidor & "'", cnn)
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

        Dim SQL As New SqlCommand("select * from Schick_Productos_Historial_Det WHERE folio_historial = " & folio_historial & " AND id_producto ='" & id_producto & "'", cnn)
        Dim Tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQL)
        Data.Fill(Tabla)

        If cantidad <> 0 Then
            If Tabla.Rows.Count = 1 Then
                Dim SQLEditar As New SqlCommand("execute pcdt_Schick_EditarHistorial_Productos " & folio_historial & ",'" & id_producto & "'," & cantidad & "", cnn)
                SQLEditar.ExecuteNonQuery()
                SQLEditar.Dispose()
            Else
                Dim SQLNuevo As New SqlCommand("execute pcdt_Schick_CrearHistorial_Productos " & folio_historial & ",'" & id_producto & "'," & cantidad & "", cnn)
                SQLNuevo.ExecuteNonQuery()
                SQLNuevo.Dispose()
            End If
        Else
            If Tabla.Rows.Count = 1 Then
                Dim SQLEliminar As New SqlCommand("DELETE FROM Schick_Productos_Historial_Det WHERE folio_historial = " & folio_historial & " AND id_producto= '" & id_producto & "'", cnn)
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

        Dim SQL As New SqlCommand("select * from Schick_Productos_Historial_Det WHERE folio_historial =" & folio & "", cnn)
        Dim Tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQL)
        Data.Fill(Tabla)

        Dim Estatus As Integer
        If Tabla.Rows.Count > 0 Then
            Estatus = 1 : Else
            Estatus = 2 : End If

        Dim SQLEstatus As New SqlCommand("UPDATE Schick_Rutas_Eventos SET estatus =@estatus " & _
                "FROM Schick_Rutas_Eventos " & _
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

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Datos()

        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        cnn.Open()

        Dim SQLNuevo As New SqlCommand("INSERT INTO Schick_Promos_Historial" & _
                                        "(id_usuario,id_periodo,id_tienda,nombre_cliente,ticket) " & _
                                        "VALUES(@id_usuario,@id_periodo,@id_tienda,@nombre_cliente,@ticket) SELECT @@IDENTITY AS 'folio_historial'", cnn)
        SQLNuevo.Parameters.AddWithValue("@id_periodo", IDPeriodo)
        SQLNuevo.Parameters.AddWithValue("@id_usuario", IDUsuario)
        SQLNuevo.Parameters.AddWithValue("@id_tienda", IDTienda)
        SQLNuevo.Parameters.AddWithValue("@nombre_cliente", txtNombre.Text)
        SQLNuevo.Parameters.AddWithValue("@ticket", txtTicket.Text)
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
                Dim SQLNuevoPromo As New SqlCommand("insert into Schick_Promos_Historial_Det " & _
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
        Dim cmdCap As New SqlCommand("DELETE FROM Schick_Promos_Historial WHERE folio_historial = " & FolioHistorial & "", cnn)

        Dim tabla As New DataTable
        Dim da As New SqlDataAdapter(cmdCap)
        da.Fill(tabla)

        Dim cmdCap2 As New SqlCommand("DELETE FROM Schick_Promos_Historial_Det WHERE folio_historial = " & FolioHistorial & "", cnn)

        Dim tabla2 As New DataTable
        Dim da2 As New SqlDataAdapter(cmdCap2)
        da2.Fill(tabla)

        CargaGridPromocionales()
        CargaGridVerPromos()
    End Sub

    Protected Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridPromocionales.Columns(1).Visible = False
    End Sub

    Protected Sub btnSubir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubir.Click
        Dim NombreFoto As String
        Datos()

        Using cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
            Dim cmdval As New SqlCommand("SELECT * From Schick_Galerias_Historial WHERE id_periodo='" & IDPeriodo & "' AND id_tienda= '" & IDTienda & "' ORDER BY no_foto DESC", cnn)
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
            sqlGuardar = "INSERT INTO Schick_Galerias_Historial " & _
                         "(id_periodo,id_usuario, id_tienda, descripcion, ruta, foto, no_foto) " & _
                         "VALUES(@id_periodo,@id_usuario, @id_tienda,@descripcion, @ruta, @foto, @no_foto)"

            NombreFoto = (IDPeriodo + "-" + IDTienda + "-" + NoFoto + ".JPG")
            Dim cmd As New SqlCommand(sqlGuardar, cnn)
            cmd.Parameters.AddWithValue("@id_periodo", IDPeriodo)
            cmd.Parameters.AddWithValue("@id_usuario", IDUsuario)
            cmd.Parameters.AddWithValue("@id_tienda", IDTienda)
            cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text)
            cmd.Parameters.AddWithValue("@ruta", "/ARCHIVOS/CLIENTES/ENERGIZER/SCHICK/IMAGENES/")
            cmd.Parameters.AddWithValue("@foto", NombreFoto)
            cmd.Parameters.AddWithValue("@no_foto", NoFoto)

            Dim T As Integer = CInt(cmd.ExecuteScalar())

            cmd.Dispose()

            cnn.Close()
            cnn.Dispose()
        End Using

        Dim fileExt As String
        fileExt = System.IO.Path.GetExtension(FileUpload1.FileName)


        If (fileExt = ".exe") Then
            lblSubida.Text = "No puedes subir archivos .exe"
        Else
            If FileUpload1.HasFile = True Then
                Dim path As String = Server.MapPath("~/ARCHIVOS/CLIENTES/ENERGIZER/SCHICK/IMAGENES/")
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
            Dim sql As String = "SELECT DISTINCT * FROM Schick_Galerias_Historial as HDET " & _
                            "WHERE HDET.id_usuario='" & IDUsuario & "' " & _
                            "AND HDET.id_periodo='" & IDPeriodo & "' " & _
                            "AND HDET.id_tienda='" & IDTienda & "' ORDER BY HDET.no_foto"

            Dim cmd As New SqlCommand(sql, cnn)
            Dim adapter As New SqlDataAdapter
            adapter.SelectCommand = cmd
            cnn.Open()
            Dim dataset As New DataSet
            adapter.Fill(dataset, "descripcion")
            cnn.Close()
            Me.gridImagenes.DataSource = dataset
            Me.gridImagenes.DataBind()
            cnn.Dispose()
        End Using
    End Sub

    Private Sub gridImagenes_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridImagenes.RowDeleting
        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        cnn.Open()
        Dim cmdCap As New SqlCommand("DELETE FROM Schick_Galerias_Historial WHERE folio_foto = " & gridImagenes.DataKeys(e.RowIndex).Value.ToString() & "", cnn)

        Dim tabla As New DataTable
        Dim SQL As New SqlDataAdapter(cmdCap)
        SQL.Fill(tabla)

        cnn.Close()
        cnn.Dispose()

        CargaImagenes()
    End Sub

    Protected Sub lnkFotos_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkFotos.Click
        pnlDemostradora.Visible = False
        pnlPromos.Visible = False
        pnlFotos.Visible = True
    End Sub
End Class