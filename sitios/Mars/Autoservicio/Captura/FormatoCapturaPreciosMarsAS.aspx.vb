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
Imports procomlcd.Permisos

Partial Public Class FormatoCapturaPreciosMarsAS
    Inherits System.Web.UI.Page

    Dim IDTienda, IDUsuario, IDPeriodo, IDProducto, IDQuincena, FolioAct As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionMars.localSqlServer, HttpContext.Current.User.Identity.Name)

            VerDatos()
            lblTienda.Text = Request.Params("nombre")

            If Edicion = 1 Then
                btnGuardar.Visible = True : Else
                btnGuardar.Visible = False : End If
        End If
    End Sub

    Private Sub VerDatos()
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * from AS_Precios_Historial " & _
                                               "where folio_historial =" & FolioAct & "")
        If Tabla.Rows.Count > 0 Then
            txtComentarioGeneral.Text = Tabla.Rows(0)("comentarios")
        End If
        Tabla.Dispose()

        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT PROD.id_producto, PROD.nombre_producto,PROD.precio_min, PROD.precio_max, " & _
                    "ISNULL(HDET.precio,0) as precio " & _
                    "FROM AS_Precios_Productos AS PROD " & _
                    "FULL JOIN (SELECT * FROM AS_Precios_Historial_Det " & _
                    "WHERE folio_historial = '" & FolioAct & "')AS HDET " & _
                    "ON HDET.id_producto = PROD.id_producto " & _
                    "WHERE PROD.tipo_producto =1", Me.gridProductosPropios)

        'carga productos capturados competencia
        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT PROD.id_producto, PROD.nombre_producto, " & _
                    "ISNULL(HDET.precio,0) as precio " & _
                    "FROM AS_Precios_Productos AS PROD " & _
                    "FULL JOIN (SELECT * FROM AS_Precios_Historial_Det " & _
                    "WHERE folio_historial = '" & FolioAct & "')AS HDET " & _
                    "ON HDET.id_producto = PROD.id_producto " & _
                    "WHERE PROD.tipo_producto =2", Me.gridProductosCompetencia)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        btnGuardar.Enabled = False
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * from AS_Precios_Historial " & _
                                               "WHERE folio_historial=" & FolioAct & "")
        If txtComentarioGeneral.Text = "NINGUNO" Then
            txtComentarioGeneral.Text = "" : End If
        If txtComentarioGeneral.Text = "OK" Then
            txtComentarioGeneral.Text = "" : End If

        If Tabla.Rows.Count > 0 Then
            BD.Execute(ConexionMars.localSqlServer, _
                       "execute AS_EditarHistorial_Precios " & FolioAct & ",'" & txtComentarioGeneral.Text & "'")

            GuardarProductos(FolioAct)
            CambioEstatus(FolioAct)
        Else
            FolioAct = BD.RT.Execute(ConexionMars.localSqlServer, _
                       "execute AS_CrearHistorial_Precios '" & IDUsuario & "'," & IDPeriodo & "," & _
                       "'" & IDQuincena & "'," & IDTienda & ",'" & txtComentarioGeneral.Text & "'")
            GuardarProductos(FolioAct)
            CambioEstatus(FolioAct)
        End If
        Tabla.Dispose()

        Response.Redirect("RutaMarsAS.aspx")
    End Sub

    Private Function GuardarProductos(ByVal folio As Integer) As Boolean
        ''//Productos propios
        Dim I As Integer
        For I = 0 To CInt(Me.gridProductosPropios.Rows.Count) - 1
            IDProducto = Me.gridProductosPropios.DataKeys(I).Value.ToString()
            Dim txtPrecio As TextBox = CType(Me.gridProductosPropios.Rows(I).FindControl("txtPrecio"), TextBox)

            If txtPrecio.Text = "" Or txtPrecio.Text = " " Then
                txtPrecio.Text = 0 : End If

            GuardaDet(folio, IDProducto, txtPrecio.Text)
        Next

        ''//Productos competencia
        For I = 0 To CInt(Me.gridProductosCompetencia.Rows.Count) - 1
            IDProducto = Me.gridProductosCompetencia.DataKeys(I).Value.ToString()
            Dim txtPrecio As TextBox = CType(Me.gridProductosCompetencia.Rows(I).FindControl("txtPrecio"), TextBox)

            If txtPrecio.Text = "" Or txtPrecio.Text = " " Then
                txtPrecio.Text = 0 : End If

            GuardaDet(folio, IDProducto, txtPrecio.Text)
        Next
    End Function

    Private Function GuardaDet(ByVal folio As Integer, ByVal id_producto As Integer, ByVal precio As Double) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * from AS_Precios_Historial_Det " & _
                                               "WHERE folio_historial=" & folio & " " & _
                                               "AND id_producto=" & id_producto & "")
        If precio <> 0 Then
            If Tabla.Rows.Count = 1 Then
                BD.Execute(ConexionMars.localSqlServer, _
                           "execute AS_EditarHistorial_Precios_Det " & folio & "," & id_producto & "," & _
                           "" & precio & "")
            Else
                BD.Execute(ConexionMars.localSqlServer, _
                           "execute AS_CrearHistorial_Precios_Det " & folio & "," & id_producto & "," & _
                           "" & precio & "")
            End If
        Else
            If Tabla.Rows.Count = 1 Then
                BD.Execute(ConexionMars.localSqlServer, _
                           "DELETE FROM AS_Precios_Historial_Det " & _
                           "WHERE folio_historial=" & folio & " AND id_producto=" & id_producto & "")
            End If
        End If

        Tabla.Dispose()
    End Function

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("javascript:history.go(-1);javascript:history.go()")
    End Sub

    Private Function CambioEstatus(ByVal folio As Integer) As Boolean
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * from AS_Precios_Historial_Det " & _
                                               "WHERE folio_historial=" & folio & "")
        Dim Estatus As Integer
        If Tabla.Rows.Count > 0 Then
            Estatus = 1 : Else
            Estatus = 2 : End If

        Tabla.Dispose()

        BD.Execute(ConexionMars.localSqlServer, _
                   "UPDATE AS_Precios_Rutas_Eventos SET estatus_precio=" & Estatus & " " & _
                   "FROM AS_Precios_Rutas_Eventos " & _
                   "WHERE orden=" & IDPeriodo & " AND id_usuario='" & IDUsuario & "' " & _
                   "AND id_tienda=" & IDTienda & " AND id_quincena='" & IDQuincena & "'")
    End Function

    Sub Datos()
        IDTienda = Request.Params("tienda")
        IDQuincena = Request.Params("quincena")
        IDUsuario = Request.Params("usuario")
        IDPeriodo = Request.Params("periodo")
        FolioAct = Request.Params("folio")
    End Sub

End Class