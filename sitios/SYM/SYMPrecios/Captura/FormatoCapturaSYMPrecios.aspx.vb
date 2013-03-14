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

Partial Public Class FormatoCapturaSYMPrecios
    Inherits System.Web.UI.Page

    Dim Salir As Integer
    Dim IDCadena, IDUsuario, IDPeriodo, NombreCadena, Catalogado, FolioAct As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionSYM.localSqlServer, HttpContext.Current.User.Identity.Name)

            Menu1.Items(0).Selected = True

            VerDatos()

            If Edicion = 1 Then
                btnGuardar.Visible = True : Else
                btnGuardar.Visible = False : End If
        End If
    End Sub

    Private Sub VerDatos()
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "select * FROM Precios_Historial WHERE folio_historial = " & FolioAct & "")
        If Tabla.Rows.Count > 0 Then
            txtComentario_General.Text = Tabla.Rows(0)("comentario_general") : Else
            txtComentario_General.Text = "" : End If

        lblCadena.Text = NombreCadena

        Tabla.Dispose()

        GrillaBase(gridProductos1, FolioAct, 104)
        GrillaBase(gridProductos2, FolioAct, 103)
        GrillaBase(gridProductos3, FolioAct, 100)
        GrillaBase(gridProductos4, FolioAct, 102)
        GrillaBase(gridProductos5, FolioAct, 101)
        GrillaBase(gridProductos6, FolioAct, 105)
        GrillaBase(gridProductos7, FolioAct, 108)
        GrillaBase(gridProductos8, FolioAct, 109)
    End Sub

    Private Function GrillaBase(ByVal Grilla As GridView, ByVal Folio As Integer, ByVal Grupo As Integer) As Boolean
        Datos()

        Dim ProductosCadena As String
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "select * FROM Productos_Cadenas WHERE id_cadena=" & IDCadena & "")
        If Tabla.Rows.Count > 0 Then
            ProductosCadena = "AND PROD.id_producto NOT IN(SELECT id_producto FROM Productos_Cadenas  " & _
                              "WHERE id_cadena=" & IDCadena & ") " : Else
            ProductosCadena = "AND PROD.id_producto NOT IN(SELECT id_producto FROM Productos_Cadenas " & _
                              "WHERE id_cadena=" & IDCadena & ") " : End If

        Tabla.Dispose()

        CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT PROD.id_producto, PROD.nombre_producto, PROD.presentacion,MAR.nombre_marca, " & _
                    "PROD.tipo_producto, isnull(HDET.precio,0) as precio,PROD.precio_min, PROD.precio_max, PROD.orden " & _
                    "FROM Productos AS PROD " & _
                    "FULL JOIN (SELECT * FROM Precios_Productos_Historial_Det " & _
                    "WHERE folio_historial=" & Folio & ") AS HDET " & _
                    "ON HDET.id_producto = PROD.id_producto " & _
                    "INNER JOIN Marcas as MAR ON MAR.id_marca= PROD.id_marca " & _
                    "WHERE PROD.tipo_grupo=" & Grupo & " AND PROD.orden<>0 AND PROD.tipo_producto=1 " & _
                    " " + ProductosCadena + " " & _
                    "UNION ALL " & _
                    "SELECT PROD.id_producto, PROD.nombre_producto, PROD.presentacion,MAR.nombre_marca, " & _
                    "PROD.tipo_producto, isnull(HDET.precio,0) as precio,PROD.precio_min, PROD.precio_max, PROD.orden " & _
                    "FROM Productos AS PROD " & _
                    "FULL JOIN (SELECT * FROM Precios_Productos_Historial_Det " & _
                    "WHERE folio_historial=" & Folio & ") AS HDET " & _
                    "ON HDET.id_producto = PROD.id_producto " & _
                    "INNER JOIN Marcas as MAR ON MAR.id_marca= PROD.id_marca " & _
                    "WHERE PROD.tipo_grupo=" & Grupo & " AND PROD.orden<>0 AND PROD.tipo_producto=2 " & _
                    "ORDER BY PROD.orden", Grilla)
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        VerificaMinimo(gridProductos1)

        If Salir = 1 Then
            Exit Sub
        Else
            VerificaMinimo(gridProductos2)
            If Salir = 1 Then
                Exit Sub
            Else
                VerificaMinimo(gridProductos3)
                If Salir = 1 Then
                    Exit Sub
                Else
                    VerificaMinimo(gridProductos4)
                    If Salir = 1 Then
                        Exit Sub
                    Else
                        VerificaMinimo(gridProductos5)
                        If Salir = 1 Then
                            Exit Sub
                        Else
                            VerificaMinimo(gridProductos6)
                            If Salir = 1 Then
                                Exit Sub
                            Else
                                VerificaMinimo(gridProductos7)
                                If Salir = 1 Then
                                    Exit Sub
                                Else
                                    VerificaMinimo(gridProductos8)
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If

        Guardar()
    End Sub

    Private Function VerificaMinimo(ByVal Grilla As GridView) As Boolean
        Dim IDProducto As Integer

        For i = 0 To CInt(Grilla.Rows.Count) - 1
            IDProducto = Grilla.DataKeys(i).Value.ToString()
            Dim txtPrecio As TextBox = CType(Grilla.Rows(i).FindControl("txtPrecio"), TextBox)

            If txtPrecio.Text = "" Or txtPrecio.Text = " " Then
                txtPrecio.Text = 0 : End If

            If txtPrecio.Text <> 0 Then
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                                       "select id_producto, precio_min, nombre_producto, GRUP.nombre_grupos " & _
                                            "from Productos as PROD " & _
                                            "INNER JOIN Grupos as GRUP ON GRUP.tipo_grupo= PROD.tipo_grupo " & _
                                            "WHERE id_producto=" & IDProducto & "")
                If Tabla.Rows.Count > 0 Then
                    If txtPrecio.Text < Tabla.Rows(0)("precio_min") Then
                        lblMinimos.Text = "El Producto '" & Tabla.Rows(0)("nombre_producto") & "' de la categoria de " & Tabla.Rows(0)("nombre_grupos") & ", tiene un precio muy bajo. Por favor rectificalo."
                        Salir = 1
                    End If
                Else
                    Salir = 0
                End If

                Tabla.Dispose()
            End If
        Next
    End Function

    Sub Guardar()
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "select * from Precios_Historial WHERE folio_historial = " & FolioAct & "")
        If Tabla.Rows.Count > 0 Then
            BD.Execute(ConexionSYM.localSqlServer, _
                       "execute Precios_EditarHistorial " & FolioAct & ",'" & IDPeriodo & "','" & IDUsuario & "','" & IDCadena & "','" & txtComentario_General.Text & "'")
        Else
            FolioAct = BD.RT.Execute(ConexionSYM.localSqlServer, _
                                     "execute Precios_CrearHistorial '" & IDUsuario & "','" & IDPeriodo & "'," & IDCadena & ",'" & txtComentario_General.Text & "'")
        End If

        GuardarProductos(FolioAct, gridProductos1)
        GuardarProductos(FolioAct, gridProductos2)
        GuardarProductos(FolioAct, gridProductos3)
        GuardarProductos(FolioAct, gridProductos4)
        GuardarProductos(FolioAct, gridProductos5)
        GuardarProductos(FolioAct, gridProductos6)
        GuardarProductos(FolioAct, gridProductos7)
        GuardarProductos(FolioAct, gridProductos8)
        CambioEstatus(FolioAct)

        Tabla.Dispose()

        Me.Dispose()

        Response.Redirect("RutaSYMPrecios.aspx")
    End Sub

    Private Function GuardarProductos(ByVal folio As Integer, ByVal Grilla As GridView) As Boolean
        ''//PRODUCTOS
        Dim IDProducto As Integer
        For I As Integer = 0 To Grilla.Rows.Count - 1
            IDProducto = Grilla.DataKeys(I).Value.ToString()
            Dim txtPrecio As TextBox = CType(Grilla.Rows(I).FindControl("txtPrecio"), TextBox)

            If txtPrecio.Text = "" Or txtPrecio.Text = " " Then
                txtPrecio.Text = 0 : End If

            GuardaDetalle(folio, IDProducto, txtPrecio.Text)
        Next
    End Function

    Private Function GuardaDetalle(ByVal FolioHistorial As Integer, ByVal IDProducto As Integer, _
                                ByVal Precio As Double) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "select * from Precios_Productos_Historial_Det " & _
                                               "WHERE folio_historial = " & FolioHistorial & " AND id_producto =" & IDProducto & "")
        If Precio <> 0 Then
            If Tabla.Rows.Count > 0 Then
                BD.Execute(ConexionSYM.localSqlServer, _
                           "execute Precios_EditarHistorial_Det " & FolioHistorial & "," & IDProducto & "," & Precio & "")
            Else
                BD.Execute(ConexionSYM.localSqlServer, _
                           "execute Precios_CrearHistorial_Det " & FolioHistorial & "," & IDProducto & "," & Precio & "")
            End If
        Else
            BD.Execute(ConexionSYM.localSqlServer, _
                       "DELETE FROM Precios_Productos_Historial_Det WHERE folio_historial = " & FolioHistorial & " AND id_producto= '" & IDProducto & "'")
        End If
        Tabla.Dispose()
    End Function

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("RutaSYMPrecios.aspx")
    End Sub

    Private Function CambioEstatus(ByVal folio As Integer) As Boolean
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "select * from Precios_Productos_Historial_Det WHERE folio_historial =" & folio & "")
        Dim Estatus As Integer
        If Tabla.Rows.Count > 0 Then
            Estatus = 1 : Else
            Estatus = 2 : End If
        Tabla.Dispose()

        BD.Execute(ConexionSYM.localSqlServer, _
                   "UPDATE Precios_Rutas_Eventos SET estatus=" & Estatus & " " & _
                   "FROM Precios_Rutas_Eventos " & _
                   "WHERE id_periodo=" & IDPeriodo & " AND id_usuario='" & IDUsuario & "' " & _
                   "AND id_cadena=" & IDCadena & "")
    End Function

    Sub Datos()
        FolioAct = Request.Params("folio")
        IDCadena = Request.Params("cadena")
        IDUsuario = Request.Params("usuario")
        IDPeriodo = Request.Params("periodo")
        nombrecadena = Request.Params("nombrecadena")
    End Sub

    Private Sub gridProductos1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridProductos1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(3).Text = 1 Then
                For i = 0 To 4
                    e.Row.Cells(i).BackColor = Drawing.Color.GreenYellow
                Next i
            End If
        End If
    End Sub

    Private Sub gridProductos2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridProductos2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(3).Text = 1 Then
                For i = 0 To 4
                    e.Row.Cells(i).BackColor = Drawing.Color.GreenYellow
                Next i
            End If
        End If
    End Sub

    Private Sub gridProductos3_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridProductos3.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(3).Text = 1 Then
                For i = 0 To 4
                    e.Row.Cells(i).BackColor = Drawing.Color.GreenYellow
                Next i
            End If
        End If
    End Sub

    Private Sub gridProductos4_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridProductos4.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(3).Text = 1 Then
                For i = 0 To 4
                    e.Row.Cells(i).BackColor = Drawing.Color.GreenYellow
                Next i
            End If
        End If
    End Sub

    Private Sub gridProductos5_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridProductos5.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(3).Text = 1 Then
                For i = 0 To 4
                    e.Row.Cells(i).BackColor = Drawing.Color.GreenYellow
                Next i
            End If
        End If
    End Sub

    Private Sub gridProductos6_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridProductos6.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(3).Text = 1 Then
                For i = 0 To 4
                    e.Row.Cells(i).BackColor = Drawing.Color.GreenYellow
                Next i
            End If
        End If
    End Sub

    Private Sub gridProductos7_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridProductos7.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(3).Text = 1 Then
                For i = 0 To 4
                    e.Row.Cells(i).BackColor = Drawing.Color.GreenYellow
                Next i
            End If
        End If
    End Sub

    Private Sub gridProductos8_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridProductos8.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(3).Text = 1 Then
                For i = 0 To 4
                    e.Row.Cells(i).BackColor = Drawing.Color.GreenYellow
                Next i
            End If
        End If
    End Sub

    Private Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridProductos1.Columns(3).Visible = False
        gridProductos2.Columns(3).Visible = False
        gridProductos3.Columns(3).Visible = False
        gridProductos4.Columns(3).Visible = False
        gridProductos5.Columns(3).Visible = False
        gridProductos6.Columns(3).Visible = False
        gridProductos7.Columns(3).Visible = False
        gridProductos8.Columns(3).Visible = False
    End Sub

    Protected Sub Menu1_MenuItemClick(ByVal sender As Object, ByVal e As MenuEventArgs) Handles Menu1.MenuItemClick
        MultiView1.ActiveViewIndex = Int32.Parse(e.Item.Value)
        Dim i,Sel As Integer
        Sel = e.Item.Value

        Menu1.Items(0).ImageUrl = "../../Img/unselectedtabJT.gif"
        Menu1.Items(1).ImageUrl = "../../Img/unselectedtabJL.gif"
        Menu1.Items(2).ImageUrl = "../../Img/unselectedtabDC.gif"
        Menu1.Items(3).ImageUrl = "../../Img/unselectedtabDR.gif"
        Menu1.Items(4).ImageUrl = "../../Img/unselectedtabDM.gif"
        Menu2.Items(0).ImageUrl = "../../Img/unselectedtabL.gif"
        Menu2.Items(1).ImageUrl = "../../Img/unselectedtabLC.gif"
        Menu2.Items(2).ImageUrl = "../../Img/unselectedtabLM.gif"

        For i = 0 To Menu1.Items.Count - 1
            If Sel = 0 Then
                Menu1.Items(0).ImageUrl = "../../Img/selectedtabJT.gif" : End If
            If Sel = 1 Then
                Menu1.Items(1).ImageUrl = "../../Img/selectedtabJL.gif" : End If
            If Sel = 2 Then
                Menu1.Items(2).ImageUrl = "../../Img/selectedtabDC.gif" : End If
            If Sel = 3 Then
                Menu1.Items(3).ImageUrl = "../../Img/selectedtabDR.gif" : End If
            If Sel = 4 Then
                Menu1.Items(4).ImageUrl = "../../Img/selectedtabDM.gif" : End If
        Next


        For i = 0 To Menu2.Items.Count - 1
            If Sel = 5 Then
                Menu2.Items(0).ImageUrl = "../../Img/selectedtabL.gif" : End If
            If Sel = 6 Then
                Menu2.Items(1).ImageUrl = "../../Img/selectedtabLC.gif" : End If
            If Sel = 7 Then
                Menu2.Items(2).ImageUrl = "../../Img/selectedtabLM.gif" : End If
        Next
    End Sub

End Class