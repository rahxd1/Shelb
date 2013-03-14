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

Partial Public Class FormatoCapturaMarsVP
    Inherits System.Web.UI.Page

    Dim IDTienda, IDUsuario, IDPeriodo, IDDia, IDProducto, IDSemana, FolioAct As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionMars.localSqlServer, HttpContext.Current.User.Identity.Name)

            VerDatos()
            lblTienda.Text = Request.Params("nombre")

            If Edicion = 1 Then
                btnGuardar.Visible = True
            Else
                btnGuardar.Visible = False
            End If
        End If
    End Sub

    Private Sub VerDatos()
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * from Mayoreo_Historial where folio_historial =" & FolioAct & "")
        If Tabla.Rows.Count > 0 Then
            txtComentarioGeneral.Text = Tabla.Rows(0)("comentarios")
            txtComentarioCompetencia.Text = Tabla.Rows(0)("comentarios_competencia")
        End If
        Tabla.Dispose()

        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT PROD.id_producto, PROD.nombre_producto, PROD.precio_min, PROD.precio_max, " & _
                    "ISNULL(HDET.precio_pieza,0) as precio_pieza, ISNULL(HDET.precio_kilo,0) as precio_kilo, " & _
                    "ISNULL(HDET.no_catalogado,0) as no_catalogado, ISNULL(HDET.agotado,0) as agotado " & _
                    "FROM Productos_Mayoreo AS PROD " & _
                    "FULL JOIN (SELECT * FROM Mayoreo_Productos_Historial_Det " & _
                    "WHERE folio_historial = '" & FolioAct & "')AS HDET " & _
                    "ON HDET.id_producto = PROD.id_producto " & _
                    "WHERE PROD.propio=1 AND formato_verificadores=1", gridProductosClientes)

        'carga productos capturados competencia
        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT PROD.id_producto, PROD.nombre_producto, PROD.precio_min, PROD.precio_max, " & _
                     "ISNULL(HDET.precio_pieza,0) as precio_pieza, ISNULL(HDET.precio_kilo,0) as precio_kilo " & _
                     "FROM Productos_Mayoreo AS PROD " & _
                     "FULL JOIN (SELECT * FROM Mayoreo_Productos_Historial_Det " & _
                     "WHERE folio_historial = '" & FolioAct & "')AS HDET " & _
                     "ON HDET.id_producto = PROD.id_producto " & _
                     "WHERE PROD.propio=0 AND formato_verificadores=1", gridProductosCompetencia)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        For i = 0 To CInt(Me.gridProductosClientes.Rows.Count) - 1
            IDProducto = Me.gridProductosClientes.DataKeys(i).Value.ToString()
            Dim txtPrecioPza As TextBox = CType(Me.gridProductosClientes.Rows(i).FindControl("txtPrecioPza"), TextBox)

            If txtPrecioPza.Text = "" Or txtPrecioPza.Text = " " Then
                txtPrecioPza.Text = 0 : End If

            If txtPrecioPza.Text <> 0 Then
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                                       "select * from Productos_Mayoreo WHERE id_producto=" & IDProducto & "")
                If Tabla.Rows.Count > 0 Then
                    If txtPrecioPza.Text <= Tabla.Rows(0)("precio_min") Then
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
                                               "select * from Mayoreo_Historial WHERE folio_historial=" & FolioAct & "")
        If Tabla.Rows.Count > 0 Then
            BD.Execute(ConexionMars.localSqlServer, _
                       "execute Mayoreo_EditarHistorial " & _
                       "" & FolioAct & ",'" & txtComentarioGeneral.Text & "'," & _
                       "'" & txtComentarioCompetencia.Text & "'")
            GuardarProductos(FolioAct)
            CambioEstatus(FolioAct)
        Else
            FolioAct = BD.RT.Execute(ConexionMars.localSqlServer, _
                       "execute Mayoreo_CrearHistorial " & _
                       "'" & IDPeriodo & "','','" & IDSemana & "','" & IDDia & "'," & _
                       "'" & IDUsuario & "','" & IDTienda & "'," & _
                       "'" & txtComentarioGeneral.Text & "'," & _
                       "'" & txtComentarioCompetencia.Text & "'")

            GuardarProductos(FolioAct)
            CambioEstatus(FolioAct)
        End If

        Tabla.Dispose()

        Response.Redirect("RutaMarsVP.aspx")
    End Sub

    Private Function GuardarProductos(ByVal folio As Integer) As Boolean
        ''//Productos propios
        Dim I, NoCatalogado, Agotado As Integer
        For I = 0 To CInt(Me.gridProductosClientes.Rows.Count) - 1
            IDProducto = Me.gridProductosClientes.DataKeys(I).Value.ToString()
            Dim chkNoCatalogado As CheckBox = CType(gridProductosClientes.Rows(I).FindControl("chkNoCatalogado"), CheckBox)
            Dim chkAgotado As CheckBox = CType(gridProductosClientes.Rows(I).FindControl("chkAgotado"), CheckBox)
            Dim txtPrecioPza As TextBox = CType(Me.gridProductosClientes.Rows(I).FindControl("txtPrecioPza"), TextBox)
            Dim txtPrecioKl As TextBox = CType(Me.gridProductosClientes.Rows(I).FindControl("txtPrecioKl"), TextBox)

            If chkNoCatalogado.Checked = True Then
                NoCatalogado = "1" : Else
                NoCatalogado = "0" : End If
            If chkAgotado.Checked = True Then
                Agotado = "1" : Else
                Agotado = "0" : End If
            If txtPrecioPza.Text = "" Or txtPrecioPza.Text = " " Then
                txtPrecioPza.Text = 0 : End If
            If txtPrecioKl.Text = "" Or txtPrecioKl.Text = " " Then
                txtPrecioKl.Text = 0 : End If

            GuardaDet(folio, IDProducto, NoCatalogado, Agotado, txtPrecioPza.Text, txtPrecioKl.Text)
        Next

        ''//Productos competencia
        For I = 0 To CInt(Me.gridProductosCompetencia.Rows.Count) - 1
            IDProducto = Me.gridProductosCompetencia.DataKeys(I).Value.ToString()
            Dim txtPrecioPza As TextBox = CType(Me.gridProductosCompetencia.Rows(I).FindControl("txtPrecioPza"), TextBox)
            Dim txtPrecioKl As TextBox = CType(Me.gridProductosCompetencia.Rows(I).FindControl("txtPrecioKl"), TextBox)

            If txtPrecioPza.Text = "" Or txtPrecioPza.Text = " " Then
                txtPrecioPza.Text = 0 : End If
            If txtPrecioKl.Text = "" Or txtPrecioKl.Text = " " Then
                txtPrecioKl.Text = 0 : End If

            GuardaDet(folio, IDProducto, 0, 0, txtPrecioPza.Text, txtPrecioKl.Text)
        Next
    End Function

    Private Function GuardaDet(ByVal folio As Integer, ByVal id_producto As Integer, _
                               ByVal NoCatalogado As Integer, ByVal Agotado As Integer, _
                               ByVal precio_pza As Double, ByVal precio_kl As Double) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * from Mayoreo_Productos_Historial_Det WHERE folio_historial=" & folio & " AND id_producto='" & id_producto & "'")
        If precio_pza <> 0 Or precio_kl <> 0 Or NoCatalogado <> 0 Or Agotado <> 0 Then
            If NoCatalogado = 1 Then
                precio_pza = 0
                precio_kl = 0
                Agotado = 0 : End If

            If Tabla.Rows.Count = 1 Then
                BD.Execute(ConexionMars.localSqlServer, _
                           "execute Mayoreo_EditarHistorial_Det " & folio & ",'" & id_producto & "'," & _
                           "" & precio_pza & "," & precio_kl & ",0," & NoCatalogado & "," & Agotado & "")
            Else
                BD.Execute(ConexionMars.localSqlServer, _
                           "execute Mayoreo_CrearHistorial_Det " & folio & ",'" & id_producto & "'," & _
                           "" & precio_pza & "," & precio_kl & ",0," & NoCatalogado & "," & _
                           "" & Agotado & "")
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
                   "UPDATE Mayoreo_Verificadores_Rutas_Eventos SET estatus=" & Estatus & " " & _
                   "FROM Mayoreo_Verificadores_Rutas_Eventos " & _
                   "WHERE orden=" & IDPeriodo & " AND id_usuario='" & IDUsuario & "' " & _
                   "AND id_tienda=" & IDTienda & " " & _
                   "AND id_semana='" & IDSemana & "' AND id_dia='" & IDDia & "'")
    End Function

    Sub Datos()
        IDTienda = Request.Params("tienda")
        IDUsuario = Request.Params("usuario")
        IDPeriodo = Request.Params("periodo")
        IDDia = Request.Params("dia")
        IDSemana = Request.Params("semana")
        FolioAct = Request.Params("folio")
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