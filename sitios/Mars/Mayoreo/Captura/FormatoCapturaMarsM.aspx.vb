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

Partial Public Class FormatoCapturaMarsM
    Inherits System.Web.UI.Page

    Dim IDTienda, IDUsuario, IDPeriodo, IDQuincena, IDProducto, FolioAct As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionMars.localSqlServer, HttpContext.Current.User.Identity.Name)

            VerDatos()

            lblTienda.Text = Request.Params("nombre")

            If Edicion = 1 Then
                btnGuardar.Visible = True : Else
                btnGuardar.Visible = False : End If

            cmbTipoTienda.Items.Insert(0, New ListItem("", ""))
        End If
    End Sub

    Private Sub VerDatos()
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * from Mayoreo_Historial " & _
                                  "WHERE folio_historial =" & FolioAct & "")
        If Tabla.Rows.Count > 0 Then
            txtComentarioGeneral.Text = Tabla.Rows(0)("comentarios")
        End If
        Tabla.Dispose()

        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT PROD.id_producto,PROD.tipo_producto,CAT.categoria, PROD.nombre_producto, " & _
                    "ISNULL(HDET.precio_pieza,0) as precio_pieza, ISNULL(HDET.precio_kilo,0) as precio_kilo, " & _
                    "ISNULL(HDET.inventario,0) as inventario, ISNULL(HDET.no_catalogado,0) as no_catalogado, " & _
                    "ISNULL(HDET.agotado,0) as agotado,PROD.precio_min, PROD.precio_max " & _
                    "FROM Productos_Mayoreo AS PROD " & _
                    "INNER JOIN Mayoreo_Categoria as CAT ON CAT.id_categoria = PROD.id_categoria " & _
                    "FULL JOIN (SELECT * FROM Mayoreo_Productos_Historial_Det " & _
                    "WHERE folio_historial=" & FolioAct & ")AS HDET " & _
                    "ON HDET.id_producto = PROD.id_producto " & _
                    "WHERE PROD.propio =1 AND PROD.tipo_producto=1 AND formato_mayoreo=1" & _
                    "ORDER BY PROD.id_categoria,nombre_producto", gridProductos1)

        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT PROD.id_producto,PROD.tipo_producto,CAT.categoria, PROD.nombre_producto, " & _
                    "ISNULL(HDET.precio_pieza,0) as precio_pieza, ISNULL(HDET.precio_kilo,0) as precio_kilo, " & _
                    "ISNULL(HDET.inventario,0) as inventario, ISNULL(HDET.no_catalogado,0) as no_catalogado, " & _
                    "ISNULL(HDET.agotado,0) as agotado " & _
                    "FROM Productos_Mayoreo AS PROD " & _
                    "INNER JOIN Mayoreo_Categoria as CAT ON CAT.id_categoria = PROD.id_categoria " & _
                    "FULL JOIN (SELECT * FROM Mayoreo_Productos_Historial_Det " & _
                    "WHERE folio_historial=" & FolioAct & ")AS HDET " & _
                    "ON HDET.id_producto = PROD.id_producto " & _
                    "WHERE PROD.propio =1 AND PROD.tipo_producto=2 AND formato_mayoreo=1" & _
                    "ORDER BY PROD.id_categoria,nombre_producto", gridProductos2)

        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT PROD.id_producto,PROD.tipo_producto,CAT.categoria, PROD.nombre_producto, " & _
                    "ISNULL(HDET.precio_pieza,0) as precio_pieza, ISNULL(HDET.precio_kilo,0) as precio_kilo, " & _
                    "ISNULL(HDET.inventario,0) as inventario " & _
                    "FROM Productos_Mayoreo AS PROD " & _
                    "INNER JOIN Mayoreo_Categoria as CAT ON CAT.id_categoria = PROD.id_categoria " & _
                    "FULL JOIN (SELECT * FROM Mayoreo_Productos_Historial_Det " & _
                    "WHERE folio_historial=" & FolioAct & ")AS HDET " & _
                    "ON HDET.id_producto = PROD.id_producto " & _
                    "WHERE PROD.propio=0 AND PROD.tipo_producto=1 AND formato_mayoreo=1" & _
                    "ORDER BY PROD.id_categoria,nombre_producto", gridProductosCompetencia1)

        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT PROD.id_producto,PROD.tipo_producto,CAT.categoria, PROD.nombre_producto, " & _
                    "ISNULL(HDET.precio_pieza,0) as precio_pieza, ISNULL(HDET.precio_kilo,0) as precio_kilo, " & _
                    "ISNULL(HDET.inventario,0) as inventario " & _
                    "FROM Productos_Mayoreo AS PROD " & _
                    "INNER JOIN Mayoreo_Categoria as CAT ON CAT.id_categoria = PROD.id_categoria " & _
                    "FULL JOIN (SELECT * FROM Mayoreo_Productos_Historial_Det " & _
                    "WHERE folio_historial=" & FolioAct & ")AS HDET " & _
                    "ON HDET.id_producto = PROD.id_producto " & _
                    "WHERE PROD.propio=0 AND PROD.tipo_producto=2 AND formato_mayoreo=1" & _
                    "ORDER BY PROD.id_categoria,nombre_producto", gridProductosCompetencia2)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        For i = 0 To CInt(Me.gridProductos1.Rows.Count) - 1
            IDProducto = Me.gridProductos1.DataKeys(i).Value.ToString()
            Dim txtPrecioPza As TextBox = CType(Me.gridProductos1.Rows(i).FindControl("txtPrecioPza"), TextBox)

            If txtPrecioPza.Text = "" Or txtPrecioPza.Text = " " Then
                txtPrecioPza.Text = 0 : End If

            If txtPrecioPza.Text <> 0 Then
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                                       "select * from Productos_Mayoreo " & _
                                                       "WHERE id_producto=" & IDProducto & "")
                If tabla.Rows.Count > 0 Then
                    If txtPrecioPza.Text < tabla.Rows(0)("precio_min") Then
                        lblMinimos.Text = "El Producto '" & tabla.Rows(0)("nombre_producto") & "' tiene un precio muy bajo. Por favor rectificalo"
                        Exit Sub
                    End If
                End If

                Tabla.Dispose()
            End If
        Next

        Guardar()
    End Sub

    Sub Guardar()
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * from Mayoreo_Historial " & _
                                  "WHERE folio_historial=" & FolioAct & "")
        If Tabla.Rows.Count > 0 Then
            BD.Execute(ConexionMars.localSqlServer, _
                       "execute Mayoreo_EditarHistorial " & FolioAct & "," & _
                       "'" & txtComentarioGeneral.Text & "', ''")
 
            GuardarProductos(FolioAct)
        Else
            FolioAct = BD.RT.Execute(ConexionMars.localSqlServer, _
                       "execute Mayoreo_CrearHistorial '" & IDPeriodo & "'," & _
                       "'" & IDQuincena & "','','','" & IDUsuario & "'," & _
                       "'" & IDTienda & "','" & txtComentarioGeneral.Text & "', ''")

            GuardarProductos(FolioAct)
        End If

        Tabla.Dispose()

        GuardarTienda()
        CambioEstatus(FolioAct)
        Response.Redirect("RutaMarsM.aspx")
    End Sub

    Sub GuardarTienda()
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * from Mayoreo_tiendas WHERE id_tienda =" & IDTienda & "")
        If Tabla.Rows.Count > 0 Then
            BD.Execute(ConexionMars.localSqlServer, _
                       "UPDATE Mayoreo_tiendas SET tipo_tienda=" & cmbTipoTienda.SelectedValue & " " & _
                       "FROM Mayoreo_tiendas WHERE id_tienda=" & IDTienda & "")
        End If

        Tabla.Dispose()
    End Sub

    Private Function GuardarProductos(ByVal folio As Integer) As Boolean
        Dim NoCatalogado, Agotado As Integer
        ''//Productos seco
        Dim I As Integer
        For I = 0 To CInt(Me.gridProductos1.Rows.Count) - 1
            IDProducto = Me.gridProductos1.DataKeys(I).Value.ToString()
            Dim txtPrecioPza As TextBox = CType(Me.gridProductos1.Rows(I).FindControl("txtPrecioPza"), TextBox)
            Dim txtPrecioKl As TextBox = CType(Me.gridProductos1.Rows(I).FindControl("txtPrecioKl"), TextBox)
            Dim txtInventario As TextBox = CType(Me.gridProductos1.Rows(I).FindControl("txtInventario"), TextBox)
            Dim chkNoCatalogado As CheckBox = CType(Me.gridProductos1.Rows(I).FindControl("chkNoCatalogado"), CheckBox)
            Dim chkAgotado As CheckBox = CType(Me.gridProductos1.Rows(I).FindControl("chkAgotado"), CheckBox)

            If txtPrecioPza.Text = "" Or txtPrecioPza.Text = " " Then
                txtPrecioPza.Text = 0 : End If
            If txtPrecioKl.Text = "" Or txtPrecioKl.Text = " " Then
                txtPrecioKl.Text = 0 : End If
            If txtInventario.Text = "" Or txtInventario.Text = " " Then
                txtInventario.Text = 0 : End If

            If chkNoCatalogado.Checked = True Then
                NoCatalogado = 1 : Else
                NoCatalogado = 0 : End If
            If chkAgotado.Checked = True Then
                Agotado = 1 : Else
                Agotado = 0 : End If

            GuardaDet(folio, IDProducto, txtPrecioPza.Text, txtPrecioKl.Text, txtInventario.Text, _
                      NoCatalogado, Agotado)
        Next

        ''//Productos humedo
        For I = 0 To CInt(Me.gridProductos2.Rows.Count) - 1
            IDProducto = Me.gridProductos2.DataKeys(I).Value.ToString()
            Dim txtPrecioPza As TextBox = CType(Me.gridProductos2.Rows(I).FindControl("txtPrecioPza"), TextBox)
            Dim txtPrecioCj As TextBox = CType(Me.gridProductos2.Rows(I).FindControl("txtPrecioCj"), TextBox)
            Dim txtInventario As TextBox = CType(Me.gridProductos2.Rows(I).FindControl("txtInventario"), TextBox)
            Dim chkNoCatalogado As CheckBox = CType(Me.gridProductos2.Rows(I).FindControl("chkNoCatalogado"), CheckBox)
            Dim chkAgotado As CheckBox = CType(Me.gridProductos2.Rows(I).FindControl("chkAgotado"), CheckBox)

            If txtPrecioPza.Text = "" Or txtPrecioPza.Text = " " Then
                txtPrecioPza.Text = 0 : End If
            If txtPrecioCj.Text = "" Or txtPrecioCj.Text = " " Then
                txtPrecioCj.Text = 0 : End If
            If txtInventario.Text = "" Or txtInventario.Text = " " Then
                txtInventario.Text = 0 : End If

            If chkNoCatalogado.Checked = True Then
                NoCatalogado = 1 : Else
                NoCatalogado = 0 : End If
            If chkAgotado.Checked = True Then
                Agotado = 1 : Else
                Agotado = 0 : End If

            GuardaDet(folio, IDProducto, txtPrecioPza.Text, txtPrecioCj.Text, txtInventario.Text, _
                      NoCatalogado, Agotado)
        Next

        ''//Productos seco
        For I = 0 To CInt(Me.gridProductosCompetencia1.Rows.Count) - 1
            IDProducto = Me.gridProductosCompetencia1.DataKeys(I).Value.ToString()
            Dim txtPrecioPza As TextBox = CType(Me.gridProductosCompetencia1.Rows(I).FindControl("txtPrecioPza"), TextBox)
            Dim txtPrecioKl As TextBox = CType(Me.gridProductosCompetencia1.Rows(I).FindControl("txtPrecioKl"), TextBox)

            If txtPrecioPza.Text = "" Or txtPrecioPza.Text = " " Then
                txtPrecioPza.Text = 0 : End If
            If txtPrecioKl.Text = "" Or txtPrecioKl.Text = " " Then
                txtPrecioKl.Text = 0 : End If

            GuardaDet(folio, IDProducto, txtPrecioPza.Text, txtPrecioKl.Text, 0, 0, 0)
        Next

        ''//Productos humedo
        For I = 0 To CInt(Me.gridProductosCompetencia2.Rows.Count) - 1
            IDProducto = Me.gridProductosCompetencia2.DataKeys(I).Value.ToString()
            Dim txtPrecioPza As TextBox = CType(Me.gridProductosCompetencia2.Rows(I).FindControl("txtPrecioPza"), TextBox)
            Dim txtPrecioCj As TextBox = CType(Me.gridProductosCompetencia2.Rows(I).FindControl("txtPrecioCj"), TextBox)

            If txtPrecioPza.Text = "" Or txtPrecioPza.Text = " " Then
                txtPrecioPza.Text = 0 : End If
            If txtPrecioCj.Text = "" Or txtPrecioCj.Text = " " Then
                txtPrecioCj.Text = 0 : End If

            GuardaDet(folio, IDProducto, txtPrecioPza.Text, txtPrecioCj.Text, 0, 0, 0)
        Next
    End Function

    Private Function GuardaDet(ByVal folio As Integer, ByVal id_producto As Integer, _
                               ByVal precio_pza As Double, ByVal precio_kl As Double, _
                               ByVal Inventario As Double, ByVal NoCatalogado As Integer, _
                               ByVal Agotado As Integer) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * from Mayoreo_Productos_Historial_Det " & _
                                  "WHERE folio_historial=" & folio & " " & _
                                  "AND id_producto='" & id_producto & "'")
        If precio_pza <> 0 Or precio_kl <> 0 Or NoCatalogado <> 0 Or Agotado <> 0 Then
            If NoCatalogado = 1 Then
                precio_pza = 0
                precio_kl = 0
                Agotado = 0 : End If

            If Tabla.Rows.Count = 1 Then
                BD.Execute(ConexionMars.localSqlServer, _
                           "execute Mayoreo_EditarHistorial_Det " & folio & ", " & _
                           "'" & id_producto & "'," & precio_pza & "," & precio_kl & ", " & _
                           "" & Inventario & "," & NoCatalogado & "," & Agotado & "")
            Else
                BD.Execute(ConexionMars.localSqlServer, _
                           "execute Mayoreo_CrearHistorial_Det " & folio & ", " & _
                           "'" & id_producto & "'," & precio_pza & "," & precio_kl & "," & _
                           "" & Inventario & "," & NoCatalogado & "," & Agotado & "")
            End If
        Else
            If Tabla.Rows.Count = 1 Then
                BD.Execute(ConexionMars.localSqlServer, _
                           "DELETE FROM Mayoreo_Productos_Historial_Det " & _
                           "WHERE folio_historial = " & folio & " " & _
                           "AND id_producto= '" & IDProducto & "'")
            End If
        End If

        Tabla.Dispose()
    End Function

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("javascript:history.go(-1)")
    End Sub

    Private Function CambioEstatus(ByVal folio As Integer) As Boolean
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * from Mayoreo_Productos_Historial_Det " & _
                                  "WHERE folio_historial =" & folio & "")
        Dim Estatus As Integer
        If Tabla.Rows.Count > 0 Then
            Estatus = 1 : Else
            Estatus = 2 : End If
        Tabla.Dispose()

        BD.Execute(ConexionMars.localSqlServer, _
                   "UPDATE Mayoreo_Rutas_Eventos SET estatus=" & Estatus & " " & _
                   "FROM Mayoreo_Rutas_Eventos " & _
                   "WHERE orden=" & IDPeriodo & " AND id_quincena='" & IDQuincena & "' " & _
                   "AND id_usuario='" & IDUsuario & "' AND id_tienda=" & IDTienda & "")
    End Function

    Sub Datos()
        IDTienda = Request.Params("tienda")
        IDUsuario = Request.Params("usuario")
        IDPeriodo = Request.Params("periodo")
        IDQuincena = Request.Params("quincena")
        FolioAct = Request.Params("folio")
    End Sub

    Protected Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridProductos1.Columns(0).Visible = False
        gridProductos2.Columns(0).Visible = False
        gridProductosCompetencia1.Columns(0).Visible = False
        gridProductosCompetencia2.Columns(0).Visible = False
    End Sub

    Protected Sub Menu1_MenuItemClick(ByVal sender As Object, ByVal e As MenuEventArgs) Handles Menu1.MenuItemClick
        MultiView1.ActiveViewIndex = Int32.Parse(e.Item.Value)
        Dim i, Sel As Integer
        Sel = e.Item.Value

        Menu1.Items(0).ImageUrl = "../../Img/unselectedtabPp.gif"
        Menu1.Items(1).ImageUrl = "../../Img/unselectedtabPc.gif"

        For i = 0 To Menu1.Items.Count - 1
            If Sel = 0 Then
                Menu1.Items(0).ImageUrl = "../../Img/selectedtabPp.gif" : End If
            If Sel = 1 Then
                Menu1.Items(1).ImageUrl = "../../Img/selectedtabPc.gif" : End If
        Next
    End Sub
End Class