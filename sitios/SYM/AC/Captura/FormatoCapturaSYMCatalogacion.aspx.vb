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

Partial Public Class FormatoCapturaSYMCatalogacion
    Inherits System.Web.UI.Page

    Dim IDTienda, IDUsuario, IDPeriodo, Catalogado, FolioAct As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionSYM.localSqlServer, HttpContext.Current.User.Identity.Name)

            VerDatos()

            If Edicion = 1 Then
                btnGuardar.Visible = True : Else
                btnGuardar.Visible = False : End If
        End If
    End Sub

    Private Sub VerDatos()
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "select * FROM Catalogacion_Historial " & _
                                               "WHERE folio_historial =" & FolioAct & "")
        If Tabla.Rows.Count > 0 Then
            txtComentario_General.Text = Tabla.Rows(0)("comentario_general")
        End If
        Tabla.Dispose()

        GrillaBase(gridProductos1, FolioAct, 101)
        GrillaBase(gridProductos2, FolioAct, 100)
        GrillaBase(gridProductos3, FolioAct, 102)

        GrillaBase(gridProductos4, FolioAct, 103)
        GrillaBase(gridProductos5, FolioAct, 104)
        GrillaBase(gridProductos6, FolioAct, 110)
        GrillaBase(gridProductos7, FolioAct, 105)
        GrillaBase(gridProductos8, FolioAct, 109)
        GrillaBase(gridProductos9, FolioAct, 108)
    End Sub

    Private Function GrillaBase(ByVal Grilla As GridView, ByVal Folio As Integer, ByVal Grupo As Integer) As Boolean
        CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT PROD.codigo, PROD.id_producto, " & _
                    "PROD.nombre_producto as descripcion, LIN.nombre_linea,  " & _
                    "isnull(HDET.catalogado,0) as catalogado " & _
                    "FROM Productos_Nuevos AS PROD " & _
                    "INNER JOIN Linea_Productos_Nuevo LIN ON LIN.id_linea=PROD.id_linea " & _
                    "FULL JOIN (SELECT * FROM Catalogacion_Productos_Historial_Det " & _
                    "WHERE folio_historial=" & Folio & ") AS HDET " & _
                    "ON HDET.id_producto = PROD.id_producto " & _
                    "WHERE PROD.tipo_grupo=" & Grupo & " AND id_empresa=1 AND PROD.activo=1", Grilla)
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Datos()

        Guardar()
    End Sub

    Sub Guardar()
        If txtComentario_General.Text = "OK" Then
            txtComentario_General.Text = "" : End If

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "select * from Catalogacion_Historial " & _
                                               "WHERE folio_historial = " & FolioAct & "")
        If Tabla.Rows.Count > 0 Then
            BD.Execute(ConexionSYM.localSqlServer, _
                       "execute Catalogacion_EditarHistorial " & _
                       "" & FolioAct & ",'" & txtComentario_General.Text & "'")
        Else
            FolioAct = BD.RT.Execute(ConexionSYM.localSqlServer, _
                       "execute Catalogacion_CrearHistorial '" & IDUsuario & "'," & _
                       "" & IDPeriodo & "," & IDTienda & ",'" & txtComentario_General.Text & "'")
        End If
        Tabla.Dispose()

        GuardarProductos(FolioAct, gridProductos1)
        GuardarProductos(FolioAct, gridProductos2)
        GuardarProductos(FolioAct, gridProductos3)
        GuardarProductos(FolioAct, gridProductos4)
        GuardarProductos(FolioAct, gridProductos5)
        GuardarProductos(FolioAct, gridProductos6)
        GuardarProductos(FolioAct, gridProductos7)

        CambioEstatus(FolioAct)

        Me.Dispose()

        Response.Redirect("RutaSYMAC.aspx")
    End Sub

    Private Function GuardarProductos(ByVal folio As Integer, ByVal Grilla As GridView) As Boolean
        ''//PRODUCTOS
        Dim IDProducto As Integer
        For I As Integer = 0 To Grilla.Rows.Count - 1
            IDProducto = Grilla.DataKeys(I).Value.ToString()
            Dim chkCatalogado As CheckBox = CType(Grilla.Rows(I).FindControl("chkCatalogado"), CheckBox)

            If chkCatalogado.Checked = True Then
                Catalogado = 1 : Else
                Catalogado = 0 : End If

            GuardaDetalle(folio, IDProducto, Catalogado)
        Next
    End Function

    Private Function GuardaDetalle(ByVal FolioHistorial As Integer, ByVal IDProducto As Integer, _
                                ByVal Catalogado As Integer) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "select * from Catalogacion_Productos_Historial_Det " & _
                                               "WHERE folio_historial = " & FolioHistorial & " " & _
                                               "AND id_producto =" & IDProducto & "")
        If Catalogado <> 0 Then
            If Tabla.Rows.Count = 1 Then
                BD.Execute(ConexionSYM.localSqlServer, _
                           "execute Catalogacion_EditarHistorial_Det " & FolioHistorial & "," & _
                           "" & IDProducto & "," & Catalogado & "")
            Else
                BD.Execute(ConexionSYM.localSqlServer, _
                           "execute Catalogacion_CrearHistorial_Det " & FolioHistorial & "," & _
                           "" & IDProducto & "," & Catalogado & "")
            End If
        Else
            If Tabla.Rows.Count = 1 Then
                BD.Execute(ConexionSYM.localSqlServer, _
                           "DELETE FROM Catalogacion_Productos_Historial_Det " & _
                           "WHERE folio_historial=" & FolioHistorial & " " & _
                           "AND id_producto=" & IDProducto & "")
            End If
        End If

        Tabla.Dispose()
    End Function

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("javascript:history.go(-1)")
    End Sub

    Private Function CambioEstatus(ByVal folio As Integer) As Boolean
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "select * from Catalogacion_Productos_Historial_Det " & _
                                               "WHERE folio_historial =" & folio & "")
        Dim Estatus As Integer
        If Tabla.Rows.Count > 0 Then
            Estatus = 1 : Else
            Estatus = 2 : End If
        Tabla.Dispose()

        BD.Execute(ConexionSYM.localSqlServer, _
                   "UPDATE AC_Rutas_Eventos " & _
                   "SET estatus_catalogacion=" & Estatus & " " & _
                   "FROM AC_Rutas_Eventos " & _
                   "WHERE id_periodo=" & IDPeriodo & " " & _
                   "AND id_usuario='" & IDUsuario & "' AND id_tienda=" & IDTienda & "")

    End Function

    Sub Datos()
        FolioAct = Request.Params("folio")
        IDTienda = Request.Params("tienda")
        IDUsuario = Request.Params("usuario")
        IDPeriodo = Request.Params("periodo")
    End Sub

    Protected Sub Menu1_MenuItemClick(ByVal sender As Object, ByVal e As MenuEventArgs) Handles Menu1.MenuItemClick
        MultiView1.ActiveViewIndex = Int32.Parse(e.Item.Value)
        Dim i, Sel As Integer
        Sel = e.Item.Value

        Menu1.Items(0).ImageUrl = "../../Img/unselectedtabDM.gif"
        Menu1.Items(1).ImageUrl = "../../Img/unselectedtabConc.gif"
        Menu1.Items(2).ImageUrl = "../../Img/unselectedtabDR.gif"
        Menu1.Items(3).ImageUrl = "../../Img/unselectedtabJL.gif"
        Menu1.Items(4).ImageUrl = "../../Img/unselectedtabJT.gif"
        Menu2.Items(0).ImageUrl = "../../Img/unselectedtabLL.gif"
        Menu2.Items(1).ImageUrl = "../../Img/unselectedtabL.gif"
        Menu2.Items(2).ImageUrl = "../../Img/unselectedtabLM.gif"
        Menu2.Items(3).ImageUrl = "../../Img/unselectedtabLC.gif"

        For i = 0 To Menu1.Items.Count - 1
            If Sel = 0 Then
                Menu1.Items(0).ImageUrl = "../../Img/selectedtabDM.gif" : End If
            If Sel = 1 Then
                Menu1.Items(1).ImageUrl = "../../Img/selectedtabConc.gif" : End If
            If Sel = 2 Then
                Menu1.Items(2).ImageUrl = "../../Img/selectedtabDR.gif" : End If
            If Sel = 3 Then
                Menu1.Items(3).ImageUrl = "../../Img/selectedtabJL.gif" : End If
            If Sel = 4 Then
                Menu1.Items(4).ImageUrl = "../../Img/selectedtabJT.gif" : End If
        Next


        For i = 0 To Menu2.Items.Count - 1
            If Sel = 5 Then
                Menu2.Items(0).ImageUrl = "../../Img/selectedtabLL.gif" : End If
            If Sel = 6 Then
                Menu2.Items(1).ImageUrl = "../../Img/selectedtabL.gif" : End If
            If Sel = 7 Then
                Menu2.Items(2).ImageUrl = "../../Img/selectedtabLM.gif" : End If
            If Sel = 8 Then
                Menu2.Items(3).ImageUrl = "../../Img/selectedtabLC.gif" : End If
        Next
    End Sub
End Class