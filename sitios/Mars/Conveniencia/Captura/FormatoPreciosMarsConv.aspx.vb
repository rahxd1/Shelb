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

Partial Public Class FormatoPreciosMarsConv
    Inherits System.Web.UI.Page

    Dim IDCadena, IDUsuario, IDPeriodo, IDProducto, IDQuincena As String
    Dim FolioAct As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionMars.localSqlServer, HttpContext.Current.User.Identity.Name)

            VerDatos()

            lblCadena.Text = Request.Params("nombre_cadena")

            If Edicion = 1 Then
                btnGuardar.Visible = True : Else
                btnGuardar.Visible = False : End If
        End If
    End Sub

    Private Sub VerDatos()
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * from Conv_Historial_Precios where folio_historial =" & FolioAct & "")
        If Tabla.Rows.Count > 0 Then
            txtComentarioGeneral.Text = Tabla.Rows(0)("comentarios")
        End If

        Tabla.Dispose()

        Dim Producto As String
        If FolioAct = 0 Then
            Producto = "SELECT PROD.id_producto,PROD.codigo, PROD.nombre_producto, PROD.precio_min, PROD.precio_max, " & _
                    "ISNULL(convert(nvarchar(5),HDET.precio),'')precio  " & _
                    "FROM Conv_Productos AS PROD " & _
                    "FULL JOIN (SELECT * FROM Conv_Productos_Historial_Det " & _
                    "WHERE folio_historial=0)AS HDET " & _
                    "ON HDET.id_producto = PROD.id_producto " & _
                    "WHERE PROD.activo=1"
        Else
            Producto = "SELECT PROD.id_producto,PROD.codigo, PROD.nombre_producto, PROD.precio_min, PROD.precio_max, " & _
                    "ISNULL(HDET.precio,0)precio  " & _
                    "FROM Conv_Productos AS PROD " & _
                    "FULL JOIN (SELECT * FROM Conv_Productos_Historial_Det " & _
                    "WHERE folio_historial=" & FolioAct & ")AS HDET " & _
                    "ON HDET.id_producto = PROD.id_producto " & _
                    "WHERE PROD.activo=1"
        End If

        CargaGrilla(ConexionMars.localSqlServer, Producto, gridProductos)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        For i = 0 To CInt(Me.gridProductos.Rows.Count) - 1
            IDProducto = Me.gridProductos.DataKeys(i).Value.ToString()
            Dim txtPrecio As TextBox = CType(Me.gridProductos.Rows(i).FindControl("txtPrecio"), TextBox)

            If txtPrecio.Text = "" Or txtPrecio.Text = " " Then
                txtPrecio.Text = 0 : End If

            If txtPrecio.Text <> 0 Then
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                                       "select * from Conv_Productos WHERE id_producto=" & IDProducto & "")
                If Tabla.Rows.Count > 0 Then
                    If txtPrecio.Text <= Tabla.Rows(0)("precio_min") Then
                        lblMinimos.Text = "El Producto '" & Tabla.Rows(0)("nombre_producto") & "' tiene un precio muy bajo. Por favor rectificalo"
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
                                               "select * from Conv_Historial_Precios WHERE folio_historial=" & FolioAct & "")
        If tabla.Rows.Count > 0 Then
            BD.Execute(ConexionMars.localSqlServer, _
                       "UPDATE Conv_Historial_Precios SET comentarios='" & txtComentarioGeneral.Text & "' " & _
                       "FROM Conv_Historial_Precios WHERE folio_historial=" & FolioAct & "")
            GuardarProductos(FolioAct)
            CambioEstatus(FolioAct)
        Else
            FolioAct = BD.RT.Execute(ConexionMars.localSqlServer, _
                       "insert into Conv_Historial_Precios(orden,id_usuario,id_cadena,id_quincena,comentarios)" & _
                       "values(" & IDPeriodo & ",'" & IDUsuario & "'," & IDCadena & ", " & _
                       "'" & IDQuincena & "','" & txtComentarioGeneral.Text & "') SELECT @@IDENTITY AS 'folio_historial'")
            GuardarProductos(FolioAct)
            CambioEstatus(FolioAct)
        End If

        Tabla.Dispose()

        Response.Redirect("RutaMarsConv.aspx")
    End Sub

    Private Function GuardarProductos(ByVal folio As Integer) As Boolean
        ''//Productos
        Dim I As Integer
        For I = 0 To CInt(Me.gridProductos.Rows.Count) - 1
            IDProducto = Me.gridProductos.DataKeys(I).Value.ToString()
            Dim txtPrecio As TextBox = CType(Me.gridProductos.Rows(I).FindControl("txtPrecio"), TextBox)

            If txtPrecio.Text = "" Or txtPrecio.Text = " " Then
                txtPrecio.Text = 0 : End If

            GuardaDet(folio, IDProducto, txtPrecio.Text)
        Next
    End Function

    Private Function GuardaDet(ByVal folio As Integer, ByVal id_producto As Integer, ByVal precio As Double) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * from Conv_Productos_Historial_Det WHERE folio_historial=" & folio & " AND id_producto='" & id_producto & "'")
        If precio <> 0 Then
            If Tabla.Rows.Count = 1 Then
                BD.Execute(ConexionMars.localSqlServer, _
                           "UPDATE Conv_Productos_Historial_Det SET precio=" & precio & " " & _
                           "FROM Conv_Productos_Historial_Det " & _
                           "WHERE folio_historial=" & folio & " AND id_producto=" & id_producto & "")
            Else
                BD.Execute(ConexionMars.localSqlServer, _
                           "insert into Conv_Productos_Historial_Det(folio_historial,id_producto,precio)" & _
                           "values(" & folio & "," & id_producto & "," & precio & ")")
            End If
        Else
            If Tabla.Rows.Count = 1 Then
                BD.Execute(ConexionMars.localSqlServer, _
                           "DELETE FROM Conv_Productos_Historial_Det " & _
                           "WHERE folio_historial=" & folio & " AND id_producto=" & id_producto & "")
            End If
        End If

        Tabla.Dispose()
    End Function

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("javascript:history.go(-1)")
    End Sub

    Private Function CambioEstatus(ByVal folio As Integer) As Boolean
        Datos()

        BD.Execute(ConexionMars.localSqlServer, _
                   "UPDATE Conv_Rutas_Eventos_Precios SET estatus_precio=1 " & _
                   "FROM Conv_Rutas_Eventos_Precios " & _
                   "WHERE orden=" & IDPeriodo & " AND id_usuario='" & IDUsuario & "' " & _
                   "AND id_cadena=" & IDCadena & " AND id_quincena='" & IDQuincena & "' ")
    End Function

    Sub Datos()
        IDQuincena = Request.Params("quincena")
        IDUsuario = Request.Params("usuario")
        IDCadena = Request.Params("cadena")
        IDPeriodo = Request.Params("periodo")
        FolioAct = Request.Params("folio")
    End Sub

End Class