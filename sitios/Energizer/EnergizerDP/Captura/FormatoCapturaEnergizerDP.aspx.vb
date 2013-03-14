Imports System.Data
Imports System.Data.SqlClient
Imports procomlcd
Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports ISODates.Dates
Imports System.Security.Cryptography

Partial Public Class FormatoCapturaEnergizerDP
    Inherits System.Web.UI.Page

    Dim IDTienda, IDUsuario, IDPeriodo As String
    Dim FolioAct As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionEnergizer.localSqlServer, HttpContext.Current.User.Identity.Name)

            VerDatos()

            If Edicion = 1 Then
                btnGuardar.Visible = True : Else
                btnGuardar.Visible = False : End If
        End If
    End Sub

    Private Sub VerDatos()
        Datos()
        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        cnn.Open()

        Dim SQLVer As New SqlCommand("SELECT * FROM Energizer_DP_Productos_Historial " & _
                            "WHERE id_periodo = @id_periodoAND id_usuario = @id_usuario AND id_tienda = @id_tienda", cnn)
        SQLVer.Parameters.AddWithValue("@id_periodo", IDPeriodo)
        SQLVer.Parameters.AddWithValue("@id_usuario", IDUsuario)
        SQLVer.Parameters.AddWithValue("@id_tienda", IDTienda)

        Dim Tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQLVer)
        Data.Fill(Tabla)

        If Tabla.Rows.Count > 0 Then
            txtComentarios.Text = Tabla.Rows(0)("comentarios")
            txtActCompetencia.Text = Tabla.Rows(0)("act_competencia")
        End If

        SQLVer.Dispose()
        Tabla.Dispose()
        Data.Dispose()
        cnn.Close()
        cnn.Dispose()

        CargaGrilla(ConexionEnergizer.localSqlServer, _
                    "SELECT PROD.grupo, PROD.id_producto, PROD.nombre_producto, ISNULL(HDET.precio,0) as precio " & _
                    "FROM Energizer_DP_Productos AS PROD " & _
                    "FULL JOIN (SELECT * FROM Energizer_DP_Productos_Historial_Det " & _
                    "WHERE folio_historial = " & FolioAct & ")AS HDET " & _
                    "ON HDET.id_producto = PROD.id_producto ", Me.gridProductosCliente)

        ''Carga Dcompetencia
        CargaGrilla(ConexionEnergizer.localSqlServer, _
                    "SELECT Comp.id_competencia, Comp.nombre_competencia, ISNULL(HComp.exhibidores,0) AS exhibidores, ISNULL(HComp.demos,0)as demos, ISNULL(HComp.tipo_promocionales,0) as tipo_promocionales " & _
                    "FROM Energizer_DP_Competencia AS Comp " & _
                    "FULL JOIN (SELECT * FROM Energizer_DP_Productos_Historial_Comp " & _
                    "WHERE folio_historial = " & FolioAct & ")AS HComp " & _
                    "ON HComp.id_competencia = Comp.id_competencia", Me.gridCompetencia)

        ''Carga Canjes
        CargaGrilla(ConexionEnergizer.localSqlServer, _
                    "SELECT PRODC.id_producto, PRODC.nombre_producto, ISNULL(HPromo.cantidad ,0)as cantidad " & _
                    "FROM Energizer_DP_Productos_Promo AS PRODC " & _
                    "FULL JOIN (SELECT * FROM Energizer_DP_Productos_Historial_Promo " & _
                    "WHERE folio_historial = " & FolioAct & ")AS HPromo " & _
                    "ON HPromo.id_producto = PRODC.id_producto", Me.gridPromo)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        btnGuardar.Enabled = False

        Guardar()
    End Sub

    Sub Guardar()
        Datos()
        lblMsg.Text = ""

        Datos()
        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        cnn.Open()
        Dim SQLBusca As New SqlCommand("select * from Energizer_DP_Productos_Historial WHERE folio_historial = " & FolioAct & "", cnn)
        Dim Tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQLBusca)
        Data.Fill(Tabla)

        If Tabla.Rows.Count > 0 Then
            Dim SQLEditar As New SqlCommand("execute pcdt_Energizer_DP_EditarHistorial " & FolioAct & ",'" & IDPeriodo & "','" & IDUsuario & "','" & IDTienda & "','" & txtComentarios.Text & "','" & txtActCompetencia.Text & "'", cnn)
            SQLEditar.ExecuteNonQuery()
            SQLEditar.Dispose()

            GuardaDetalles(FolioAct)
            CambioEstatus(FolioAct)
        Else
            Dim SQLNuevo As New SqlCommand("execute pcdt_Energizer_DP_CrearHistorial '" & IDUsuario & "','" & IDPeriodo & "','" & IDTienda & "','" & txtComentarios.Text & "','" & txtActCompetencia.Text & "' ", cnn)
            FolioAct = Convert.ToInt32(SQLNuevo.ExecuteScalar())
            SQLNuevo.Dispose()

            GuardaDetalles(FolioAct)
            CambioEstatus(FolioAct)
        End If

        Me.Dispose()
        Response.Redirect("RutaEnergizerDP.aspx")
    End Sub

    Private Function GuardaDetalles(ByVal folio As Integer) As Boolean
        Dim id_articulo As Integer
        For I As Integer = 0 To gridProductosCliente.Rows.Count - 1
            id_articulo = gridProductosCliente.DataKeys(I).Value.ToString()
            Dim txtPrecio As TextBox = CType(gridProductosCliente.Rows(I).FindControl("txtPrecio"), TextBox)

            If txtPrecio.Text = "" Or txtPrecio.Text = " " Then
                txtPrecio.Text = "0" : End If

            GuardaHistorial(folio, id_articulo, txtPrecio.Text)
        Next

        ''//COMPETENCIA
        For I As Integer = 0 To gridCompetencia.Rows.Count - 1
            id_articulo = gridCompetencia.DataKeys(I).Value.ToString()
            Dim txtExhibidores As TextBox = CType(gridCompetencia.Rows(I).FindControl("txtExhibidores"), TextBox)
            Dim txtDemos As TextBox = CType(gridCompetencia.Rows(I).FindControl("txtDemos"), TextBox)
            Dim txtTipoPromocionales As TextBox = CType(gridCompetencia.Rows(I).FindControl("txtTipoPromocionales"), TextBox)

            If txtExhibidores.Text = "" Or txtExhibidores.Text = " " Then
                txtExhibidores.Text = "0" : End If
            If txtDemos.Text = "" Or txtDemos.Text = " " Then
                txtDemos.Text = "0" : End If

            GuardaCompetencia(folio, id_articulo, txtExhibidores.Text, txtDemos.Text, txtTipoPromocionales.Text)
        Next

        ''//PRODUCTOS RPOMOCIONALES
        For I As Integer = 0 To gridPromo.Rows.Count - 1
            id_articulo = gridPromo.DataKeys(I).Value.ToString()
            Dim txtCantidad As TextBox = CType(gridPromo.Rows(I).FindControl("txtCantidad"), TextBox)

            If txtCantidad.Text = "" Or txtCantidad.Text = " " Then
                txtCantidad.Text = "0" : End If

            GuardaCanje(folio, id_articulo, txtCantidad.Text)
        Next
    End Function

    Private Function GuardaHistorial(ByVal FolioHistorial As Integer, ByVal IDProducto As Integer, ByVal Precio As Double) As Boolean
        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        Dim SQLBusca As New SqlCommand("SELECT * From Energizer_DP_Productos_Historial_Det WHERE folio_historial=@folio_historial AND id_producto = @id_producto ", cnn)
        cnn.Open()
        SQLBusca.Parameters.AddWithValue("@id_producto", IDProducto)
        SQLBusca.Parameters.AddWithValue("@folio_historial", FolioHistorial)

        Dim tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQLBusca)
        Data.Fill(tabla)

        If Precio <> 0 Then
            If tabla.Rows.Count = 1 Then
                Dim SQLEdita As New SqlCommand("execute pcdt_Energizer_DP_EditarHistorial_Det " & FolioHistorial & ",'" & IDProducto & "'," & Precio & "", cnn)
                SQLEdita.ExecuteNonQuery()
                SQLEdita.Dispose()
            Else
                Dim SQLNuevo As New SqlCommand("execute Energizer_DP_Productos_Historial_Det " & FolioHistorial & ",'" & IDProducto & "'," & Precio & "", cnn)
                SQLNuevo.ExecuteNonQuery()
                SQLNuevo.Dispose()
            End If
        Else
            If tabla.Rows.Count = 1 Then
                Dim SQLEliminar As New SqlCommand("DELETE FROM Energizer_DP_Productos_Historial_Det WHERE folio_historial = " & FolioHistorial & " AND id_producto= '" & IDProducto & "'", cnn)
                SQLEliminar.ExecuteNonQuery()
                SQLEliminar.Dispose()
            End If
        End If

        cnn.Close()
        cnn.Dispose()
    End Function

    Private Function GuardaCompetencia(ByVal FolioHistorial As Integer, ByVal IDCompetencia As Integer, _
                                ByVal Exhibidores As String, ByVal Demos As String, ByVal TipoPromocionales As String) As Boolean
        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        Dim SQLBusca As New SqlCommand("SELECT * From Energizer_DP_Productos_Historial_Comp WHERE folio_historial=@folio_historial AND id_competencia = @id_competencia ", cnn)
        cnn.Open()
        SQLBusca.Parameters.AddWithValue("@id_competencia", IDCompetencia)
        SQLBusca.Parameters.AddWithValue("@folio_historial", FolioHistorial)

        Dim tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQLBusca)
        Data.Fill(tabla)

        If Exhibidores <> 0 Or Demos <> 0 Then
            If tabla.Rows.Count = 1 Then
                Dim SQLEdita As New SqlCommand("execute pcdt_Energizer_DP_EditarHistorial_Comp " & FolioHistorial & ",'" & IDCompetencia & "','" & Exhibidores & "','" & Demos & "','" & TipoPromocionales & "'", cnn)
                SQLEdita.ExecuteNonQuery()
                SQLEdita.Dispose()
            Else
                Dim SQLNuevo As New SqlCommand("execute pcdt_Energizer_DP_CrearHistorial_Comp " & FolioHistorial & ",'" & IDCompetencia & "','" & Exhibidores & "','" & Demos & "','" & TipoPromocionales & "'", cnn)
                SQLNuevo.ExecuteNonQuery()
                SQLNuevo.Dispose()
            End If
        Else
            If tabla.Rows.Count = 1 Then
                Dim SQLEliminar As New SqlCommand("DELETE FROM Energizer_DP_Productos_Historial_Comp WHERE folio_historial = " & FolioHistorial & " AND id_competencia= '" & IDCompetencia & "'", cnn)
                SQLEliminar.ExecuteNonQuery()
                SQLEliminar.Dispose()
            End If
        End If

        cnn.Close()
        cnn.Dispose()
    End Function

    Private Function GuardaCanje(ByVal FolioHistorial As Integer, ByVal IDProducto As Integer, _
                            ByVal Cantidad As Double) As Boolean
        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        Dim SQLBusca As New SqlCommand("SELECT * From Energizer_DP_Productos_Historial_Promo WHERE folio_historial=@folio_historial AND id_producto = @id_producto ", cnn)
        cnn.Open()
        SQLBusca.Parameters.AddWithValue("@id_producto", IDProducto)
        SQLBusca.Parameters.AddWithValue("@folio_historial", FolioHistorial)

        Dim tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQLBusca)
        Data.Fill(tabla)

        If Cantidad <> 0 Then
            If tabla.Rows.Count = 1 Then
                Dim SQLEdita As New SqlCommand("execute pcdt_Energizer_DP_EditarHistorial_Promo " & FolioHistorial & ",'" & IDProducto & "'," & Cantidad & "", cnn)
                SQLEdita.ExecuteNonQuery()
                SQLEdita.Dispose()
            Else
                Dim SQLNuevo As New SqlCommand("execute pcdt_Energizer_DP_CrearHistorial_Promo " & FolioHistorial & ",'" & IDProducto & "'," & Cantidad & "", cnn)
                SQLNuevo.ExecuteNonQuery()
                SQLNuevo.Dispose()
            End If
        Else
            If tabla.Rows.Count = 1 Then
                Dim SQLEliminar As New SqlCommand("DELETE FROM Energizer_DP_Productos_Historial_Promo WHERE folio_historial = " & FolioHistorial & " AND id_producto= '" & IDProducto & "'", cnn)
                SQLEliminar.ExecuteNonQuery()
                SQLEliminar.Dispose()
            End If
        End If

        cnn.Close()
        cnn.Dispose()
    End Function

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("javascript:history.back(2)")
    End Sub

    Private Function CambioEstatus(ByVal folio As Integer) As Boolean
        Datos()

        Dim cnn As New SqlConnection(ConexionEnergizer.localSqlServer)
        cnn.Open()

        Dim SQL As New SqlCommand("select * from Energizer_DP_Productos_Historial_Det WHERE folio_historial =" & folio & "", cnn)
        Dim Tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQL)
        Data.Fill(Tabla)

        Dim Estatus As Integer
        If Tabla.Rows.Count > 0 Then
            Estatus = 1 : Else
            Estatus = 2 : End If

        Dim SQLEstatus As New SqlCommand("UPDATE Energizer_DP_Rutas_Eventos SET estatus =@estatus " & _
                "FROM Energizer_DP_Rutas_Eventos " & _
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
        IDTienda = Request.Params("tienda")
        IDUsuario = Request.Params("usuario")
        IDPeriodo = Request.Params("periodo")
        FolioAct = Request.Params("folio")
    End Sub

End Class