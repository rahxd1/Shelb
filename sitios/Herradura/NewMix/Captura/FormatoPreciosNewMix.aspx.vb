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

Partial Public Class FormatoPreciosNewMix
    Inherits System.Web.UI.Page

    Dim IDPeriodo, IDUsuario, IDCadena As String
    Dim FolioAct As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionHerradura.localSqlServer, HttpContext.Current.User.Identity.Name)

            VerDatos()

            lblcadena.Text = Request.Params("cadena")

            If Edicion = 1 Then
                btnGuardar.Visible = True : Else
                btnGuardar.Visible = False : End If
        End If
    End Sub

    Private Sub VerDatos()
        Datos()

        ''//SI LA TIENDA YA ESTA CAPTURADA, SE ACTUALIZARA
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionHerradura.localSqlServer, _
                                               "SELECT * FROM NewMix_Historial_Precios as H " & _
                                               "WHERE id_periodo=" & IDPeriodo & " " & _
                                               "AND id_usuario='" & IDUsuario & "' " & _
                                               "AND id_cadena=" & IDCadena & "")
        If Tabla.Rows.Count > 0 Then
            txtComentarios.Text = Tabla.Rows(0)("comentarios")
        End If

        Tabla.Dispose()

        Productos(FolioAct, 1, gridProductosCliente)
        Productos(FolioAct, 2, gridProductosCompetencia)

        CargaGrilla(ConexionHerradura.localSqlServer, _
                    "select RE.id_tienda,no_tienda, nombre, ciudad," & _
                    "ROUND(ISNULL([100],0),2)[100],ROUND(ISNULL([101],0),2)[101],ROUND(ISNULL([102],0),2)[102], " & _
                    "ROUND(ISNULL([103],0),2)[103],ROUND(ISNULL([104],0),2)[104],ROUND(ISNULL([105],0),2)[105], " & _
                    "ROUND(ISNULL([106],0),2)[106],ROUND(ISNULL([107],0),2)[107],ROUND(ISNULL([108],0),2)[108], " & _
                    "ROUND(ISNULL([109],0),2)[109],ROUND(ISNULL([110],0),2)[110],ROUND(ISNULL([111],0),2)[111], " & _
                    "ROUND(ISNULL([112],0),2)[112],ROUND(ISNULL([113],0),2)[113],ROUND(ISNULL([114],0),2)[114], " & _
                    "ROUND(ISNULL([115],0),2)[115],ROUND(ISNULL([116],0),2)[116],ROUND(ISNULL([117],0),2)[117], " & _
                    "ROUND(ISNULL([118],0),2)[118],ROUND(ISNULL([119],0),2)[119],ROUND(ISNULL([120],0),2)[120], " & _
                    "ROUND(ISNULL([121],0),2)[121],ROUND(ISNULL([122],0),2)[122],ROUND(ISNULL([123],0),2)[123], " & _
                    "ROUND(ISNULL([124],0),2)[124],ROUND(ISNULL([125],0),2)[125],ROUND(ISNULL([126],0),2)[126], " & _
                    "ROUND(ISNULL([127],0),2)[127],ROUND(ISNULL([128],0),2)[128],ROUND(ISNULL([129],0),2)[129], " & _
                    "ROUND(ISNULL([130],0),2)[130],ROUND(ISNULL([131],0),2)[131],ROUND(ISNULL([132],0),2)[132], " & _
                    "ROUND(ISNULL([133],0),2)[133],ROUND(ISNULL([134],0),2)[134],ROUND(ISNULL([135],0),2)[135], " & _
                    "ROUND(ISNULL([136],0),2)[136],ROUND(ISNULL([137],0),2)[137],ROUND(ISNULL([138],0),2)[138], " & _
                    "ROUND(ISNULL([139],0),2)[139],ROUND(ISNULL([140],0),2)[140],ROUND(ISNULL([141],0),2)[141], " & _
                    "ROUND(ISNULL([142],0),2)[142],ROUND(ISNULL([143],0),2)[143],ROUND(ISNULL([144],0),2)[144], " & _
                    "ROUND(ISNULL([145],0),2)[145],ROUND(ISNULL([146],0),2)[146],ROUND(ISNULL([147],0),2)[147], " & _
                    "ROUND(ISNULL([148],0),2)[148],ROUND(ISNULL([149],0),2)[149],ROUND(ISNULL([150],0),2)[150], " & _
                    "ROUND(ISNULL([151],0),2)[151] " & _
                    "FROM NewMix_Rutas_Eventos as RE " & _
                    "INNER JOIN NewMix_Tiendas as TI ON TI.id_tienda=RE.id_tienda " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena=TI.id_cadena " & _
                    "FULL JOIN (SELECT '" & IDUsuario & "'id_usuario,HDET.id_tienda, PROD.id_producto, ISNULL(HDET.precio,0)cantidad " & _
                    "FROM NewMix_Productos as PROD " & _
                    "FULL JOIN NewMix_Precios_Historial_Tienda as HDET " & _
                    "ON HDET.id_producto=PROD.id_producto " & _
                    "AND HDET.id_periodo=" & IDPeriodo & " AND HDET.id_usuario='" & IDUsuario & "' " & _
                    "WHERE tipo_producto=1 AND activo=1)PVT " & _
                    "PIVOT(AVG(cantidad) FOR id_producto " & _
                    "IN([100],[101],[102],[103],[104],[105],[106],[107],[108],[109],[110],[111],[112],[113],[114],[115], " & _
                    "[116],[117],[118],[119],[120],[121],[122],[123],[124],[125],[126],[127],[128],[129], " & _
                    "[130],[131],[132],[133],[134],[135],[136],[137],[138],[139],[140],[141],[142],[143], " & _
                    "[144],[145],[146],[147],[148],[149],[150],[151]))PROD " & _
                    "ON PROD.id_usuario=RE.id_usuario AND PROD.id_tienda=RE.id_tienda " & _
                    "WHERE RE.id_usuario='" & IDUsuario & "'   " & _
                    "AND id_periodo=" & IDPeriodo & " AND TI.id_cadena=" & IDCadena & " ", gridTiendas)

    End Sub

    Private Function Productos(ByVal Folio As Integer, ByVal TipoProducto As Integer, ByVal Grilla As GridView) As Boolean
        CargaGrilla(ConexionHerradura.localSqlServer, _
                    "SELECT PROD.codigo, PROD.catalogacion,PROD.id_producto, PROD.presentacion, PROD.id_familia, PROD.nombre_producto, " & _
                    "isnull(HDET.precio,0) as precio  " & _
                    "FROM  NewMix_Productos AS PROD " & _
                    "FULL JOIN (SELECT * FROM NewMix_Precios_Historial_Det WHERE folio_historial = " & Folio & ")AS HDET " & _
                    "ON HDET.id_producto = PROD.id_producto " & _
                    "WHERE PROD.tipo_producto= " & TipoProducto & " AND PROD.activo=1 ORDER BY PROD.id_familia", Grilla)
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        btnGuardar.Enabled = False

        Guardar()
    End Sub

    Sub Guardar()
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionHerradura.localSqlServer, _
                                               "select * from NewMix_Historial " & _
                                               "WHERE folio_historial =" & FolioAct & "")
        If Tabla.Rows.Count > 0 Then
            BD.Execute(ConexionHerradura.localSqlServer, _
                       "execute NewMix_EditarHistorial_Precios " & _
                       "" & FolioAct & ",'" & txtComentarios.Text & "'")
        Else
            Dim cnn As New SqlConnection(ConexionHerradura.localSqlServer)
            cnn.Open()

            Dim SQLNuevo As New SqlCommand("execute NewMix_CrearHistorial_Precios " & _
                                           "" & IDPeriodo & ",'" & IDUsuario & "'," & IDCadena & "," & _
                                           "'" & txtComentarios.Text & "'", cnn)
            FolioAct = Convert.ToInt32(SQLNuevo.ExecuteScalar())
            SQLNuevo.Dispose()

            cnn.Dispose()
            cnn.Close()
        End If

        GuardaDetalles(FolioAct)
        CambioEstatus(FolioAct)
        GuardaDetallesTiendas(FolioAct)

        Me.Dispose()
        Response.Redirect("RutaNewMix.aspx")
    End Sub

    Private Function GuardaDetalles(ByVal folio As Integer) As Boolean
        Dim IDProducto As Integer
        For i = 0 To gridProductosCliente.Rows.Count - 1
            IDProducto = gridProductosCliente.DataKeys(i).Value.ToString()
            Dim txtPrecio As TextBox = CType(gridProductosCliente.Rows(i).FindControl("txtPrecio"), TextBox)

            If txtPrecio.Text = "" Or txtPrecio.Text = " " Then
                txtPrecio.Text = "0" : End If

            GuardaHistorial(folio, IDProducto, txtPrecio.Text)
        Next i

        ''//COMPETENCIA
        For i = 0 To gridProductosCompetencia.Rows.Count - 1
            IDProducto = gridProductosCompetencia.DataKeys(i).Value.ToString()
            Dim txtPrecio As TextBox = CType(gridProductosCompetencia.Rows(i).FindControl("txtPrecio"), TextBox)

            If txtPrecio.Text = "" Or txtPrecio.Text = " " Then
                txtPrecio.Text = "0" : End If

            GuardaHistorial(folio, IDProducto, txtPrecio.Text)
        Next i
    End Function

    Private Function GuardaHistorial(ByVal folio_historial As Integer, ByVal id_producto As Integer, _
                                     ByVal Precio As Double) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionHerradura.localSqlServer, _
                                               "select * from NewMix_Precios_Historial_Det " & _
                                  "WHERE folio_historial =" & folio_historial & " " & _
                                  "AND id_producto =" & id_producto & "")
        If Precio <> 0 Then
            If Tabla.Rows.Count > 0 Then
                BD.Execute(ConexionHerradura.localSqlServer, _
                           "execute NewMix_EditarHistorial_Precios_Det " & _
                           "" & folio_historial & "," & id_producto & ", " & _
                           "" & Precio & "")
            Else
                BD.Execute(ConexionHerradura.localSqlServer, _
                           "execute NewMix_CrearHistorial_Precios_Det " & _
                           "" & folio_historial & "," & id_producto & ", " & _
                           "" & Precio & "")
            End If
        Else
            If Tabla.Rows.Count > 0 Then
                BD.Execute(ConexionHerradura.localSqlServer, _
                           "DELETE FROM NewMix_Precios_Historial_Det " & _
                           "WHERE folio_historial = " & folio_historial & " " & _
                           "AND id_producto=" & id_producto & "")
            End If
        End If

        Tabla.Dispose()
    End Function

    Private Function GuardaDetallesTiendas(ByVal folio As Integer) As Boolean
        Dim IDTienda As Integer
        Dim Precio(20) As String

        For i = 0 To gridTiendas.Rows.Count - 1
            IDTienda = gridTiendas.DataKeys(i).Value.ToString()

            Dim Tabla As DataTable = Tablas.Ver.DT(ConexionHerradura.localSqlServer, _
                                                   "SELECT * FROM NewMix_Productos " & _
                                    "WHERE activo = 1 And tipo_producto = 1 ORDER BY id_producto")
            If Tabla.Rows.Count > 0 Then
                For iC = 0 To 19
                    Precio(iC) = CType(gridTiendas.Rows(i).FindControl("txtPrecio" & Tabla.Rows(iC)("id_producto") & ""), TextBox).Text

                    GuardaHistorialTienda(IDTienda, Tabla.Rows(iC)("id_producto"), Precio(iC))
                Next iC
            End If

            Tabla.Dispose()
        Next i
    End Function

    Private Function GuardaHistorialTienda(ByVal id_tienda As Integer, _
                                            ByVal Producto As Integer, ByVal Precio As Double) As Boolean

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionHerradura.localSqlServer, _
                                               "select * from NewMix_Precios_Historial_Tienda " & _
                                  "WHERE id_tienda =" & id_tienda & " " & _
                                  "AND id_periodo =" & IDPeriodo & " " & _
                                  "AND id_producto =" & Producto & "")
        If Precio <> 0 Then
            If Tabla.Rows.Count > 0 Then
                BD.Execute(ConexionHerradura.localSqlServer, _
                           "execute NewMix_EditarHistorial_Precios_Tienda " & _
                           "" & IDPeriodo & ",'" & IDUsuario & "'," & _
                           "" & id_tienda & ", " & _
                           "" & Producto & "," & Precio & "")
            Else
                BD.Execute(ConexionHerradura.localSqlServer, _
                           "execute NewMix_CrearHistorial_Precios_Tienda " & _
                           "" & IDPeriodo & ",'" & IDUsuario & "'," & _
                           "" & id_tienda & ", " & _
                           "" & Producto & "," & Precio & "")
            End If
        Else
            If Tabla.Rows.Count > 0 Then
                BD.Execute(ConexionHerradura.localSqlServer, _
                           "DELETE FROM NewMix_Precios_Historial_Tienda " & _
                           "WHERE id_tienda =" & id_tienda & " " & _
                           "AND id_periodo =" & IDPeriodo & " " & _
                           "AND id_producto =" & Producto & "")
            End If
        End If

        Tabla.Dispose()
    End Function

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("RutaNewMix.aspx")
    End Sub

    Private Function CambioEstatus(ByVal folio As Integer) As Boolean
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionHerradura.localSqlServer, _
                                               "select * from NewMix_Precios_Historial_Det " & _
                                  "WHERE folio_historial =" & folio & "")

        Dim Estatus As Integer
        If Tabla.Rows.Count > 0 Then
            Estatus = 1 : Else
            Estatus = 2 : End If

        BD.Execute(ConexionHerradura.localSqlServer, _
                   "UPDATE NewMix_Rutas_Eventos_Cadenas  " & _
                   "SET estatus=" & Estatus & " " & _
                   "FROM NewMix_Rutas_Eventos_Cadenas " & _
                   "WHERE id_periodo=" & IDPeriodo & " AND id_usuario='" & IDUsuario & "' " & _
                   "AND id_cadena=" & IDCadena & "")
    End Function

    Sub Datos()
        FolioAct = Request.Params("folio")
        IDPeriodo = Request.Params("periodo")
        IDUsuario = Request.Params("usuario")
        IDCadena = Request.Params("id_cadena")
    End Sub

    Protected Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridProductosCliente.Columns(0).Visible = False
    End Sub

    Protected Sub Menu1_MenuItemClick(ByVal sender As Object, ByVal e As MenuEventArgs) Handles Menu1.MenuItemClick
        MultiView1.ActiveViewIndex = Int32.Parse(e.Item.Value)
        Dim i, Sel As Integer
        Sel = e.Item.Value

        Menu1.Items(0).ImageUrl = "../../Img/unselectedtabPp.gif"
        Menu1.Items(1).ImageUrl = "../../Img/unselectedtabPc.gif"
        Menu1.Items(2).ImageUrl = "../../Img/unselectedtabPt.gif"

        For i = 0 To Menu1.Items.Count - 1
            If Sel = 0 Then
                Menu1.Items(0).ImageUrl = "../../Img/selectedtabPp.gif" : End If
            If Sel = 1 Then
                Menu1.Items(1).ImageUrl = "../../Img/selectedtabPc.gif" : End If
            If Sel = 2 Then
                Menu1.Items(2).ImageUrl = "../../Img/selectedtabPt.gif" : End If
        Next
    End Sub
End Class