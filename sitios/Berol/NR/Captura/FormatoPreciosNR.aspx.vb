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

Partial Public Class FormatoPreciosNR
    Inherits System.Web.UI.Page

    Dim IDTienda, IDUsuario, IDPeriodo, FolioAct As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionBerol.localSqlServer, HttpContext.Current.User.Identity.Name)

            Menu1.Items(0).Selected = True

            VerDatos()

            If Edicion = 1 Then
                btnGuardar.Visible = True : Else
                btnGuardar.Visible = False : End If
        End If
    End Sub

    Private Sub VerDatos()
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                               "select * FROM NR_Tiendas as TI " & _
                                               "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena=TI.id_cadena " & _
                                               "WHERE id_tienda = " & IDTienda & "")
        If Tabla.Rows.Count > 0 Then
            lblTienda.Text = Tabla.Rows(0)("nombre")
            lblCadena.Text = Tabla.Rows(0)("nombre_cadena")
        End If

        Tabla.Dispose()

        GrillaBase(gridLapices_P, FolioAct, 1, 1)
        GrillaBase(gridLapices_C, FolioAct, 1, 2)
        GrillaBase(gridChecadores_P, FolioAct, 16, 1)
        GrillaBase(gridChecadores_C, FolioAct, 16, 2)
        GrillaBase(gridLapiceros_P, FolioAct, 10, 1)
        GrillaBase(gridLapiceros_C, FolioAct, 10, 2)
        GrillaBase(gridPuntillas_P, FolioAct, 14, 1)
        GrillaBase(gridPuntillas_C, FolioAct, 14, 2)
        GrillaBase(gridGomas_P, FolioAct, 15, 1)
        GrillaBase(gridGomas_C, FolioAct, 15, 2)
        GrillaBase(gridBoligrafos_P, FolioAct, 2, 1)
        GrillaBase(gridBoligrafos_C, FolioAct, 2, 2)
        GrillaBase(gridRollerGel_P, FolioAct, 3, 1)
        GrillaBase(gridRollerGel_C, FolioAct, 3, 2)
        GrillaBase(gridCorrectores_P, FolioAct, 4, 1)
        GrillaBase(gridCorrectores_C, FolioAct, 4, 2)

        GrillaBase(gridPlumones_P, FolioAct, 5, 1)
        GrillaBase(gridPlumones_C, FolioAct, 5, 2)
        GrillaBase(gridMarcadores_P, FolioAct, 6, 1)
        GrillaBase(gridMarcadores_C, FolioAct, 6, 2)
        GrillaBase(gridResaltadores_P, FolioAct, 7, 1)
        GrillaBase(gridResaltadores_C, FolioAct, 7, 2)
        GrillaBase(gridMarcadoresAgua_P, FolioAct, 11, 1)
        GrillaBase(gridMarcadoresAgua_C, FolioAct, 11, 2)
        GrillaBase(gridMarcadoresPizarron_P, FolioAct, 12, 1)
        GrillaBase(gridMarcadoresPizarron_C, FolioAct, 12, 2)

        GrillaBase(gridCrayones_P, FolioAct, 8, 1)
        GrillaBase(gridCrayones_C, FolioAct, 8, 2)
        GrillaBase(gridLapicesColor_P, FolioAct, 9, 1)
        GrillaBase(gridLapicesColor_C, FolioAct, 9, 2)

        GrillaBase(gridSamsClub_P, FolioAct, 13, 1)
        GrillaBase(gridSamsClub_C, FolioAct, 13, 2)

        GrillaBase(gridRotuladores_P, FolioAct, 18, 1)
        GrillaBase(gridRotuladores_C, FolioAct, 18, 2)
        GrillaBase(gridRepuestos_P, FolioAct, 17, 1)
        GrillaBase(gridRepuestos_C, FolioAct, 17, 2)
    End Sub

    Private Function GrillaBase(ByVal Grilla As GridView, ByVal Folio As Integer, _
                                ByVal TipoProducto As Integer, ByVal Propio As Integer) As Boolean
        CargaGrilla(ConexionBerol.localSqlServer, _
                    "SELECT PROD.id_producto,PROD.codigo,PROD.nombre_producto, PRES.nombre_presentacion,MAR.nombre_marca, " & _
                    "PROD.tipo_producto, isnull(HDET.precio,0) as precio, PROD.precio_max " & _
                    "FROM Productos AS PROD " & _
                    "INNER JOIN Tipo_Presentacion as PRES ON PRES.tipo_presentacion=PROD.tipo_presentacion " & _
                    "INNER JOIN Marcas as MAR ON MAR.id_marca = PROD.id_marca " & _
                    "INNER JOIN Tipo_Productos as TPROD ON TPROD.tipo_producto=PROD.tipo_producto " & _
                    "FULL JOIN (SELECT * FROM NR_Historial_Precios_Det " & _
                    "WHERE folio_historial=" & Folio & ") AS HDET ON HDET.id_producto = PROD.id_producto " & _
                    "WHERE PROD.tipo_producto=" & TipoProducto & " AND MAR.propia=" & Propio & " AND PROD.activo =1 " & _
                    "ORDER BY PROD.nombre_producto", Grilla)
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                               "select * from NR_Historial_Precios WHERE folio_historial=" & FolioAct & "")
        If Tabla.Rows.Count > 0 Then
            GuardarGrillas()

            CambioEstatus(FolioAct)
        Else
            FolioAct = BD.RT.Execute(ConexionBerol.localSqlServer, _
                                     "execute NR_Precios_CrearHistorial '" & IDUsuario & "'," & _
                                     "'" & IDPeriodo & "'," & IDTienda & "")
            GuardarGrillas()

            CambioEstatus(FolioAct)
        End If

        Tabla.Dispose()

        Response.Redirect("RutasSupervisorNR.aspx")
    End Sub

    Sub GuardarGrillas()
        GuardarProductos(gridLapices_P, FolioAct)
        GuardarProductos(gridLapices_C, FolioAct)
        GuardarProductos(gridChecadores_P, FolioAct)
        GuardarProductos(gridChecadores_C, FolioAct)
        GuardarProductos(gridLapiceros_P, FolioAct)
        GuardarProductos(gridLapiceros_C, FolioAct)
        GuardarProductos(gridPuntillas_P, FolioAct)
        GuardarProductos(gridPuntillas_C, FolioAct)
        GuardarProductos(gridGomas_P, FolioAct)
        GuardarProductos(gridGomas_C, FolioAct)
        GuardarProductos(gridBoligrafos_P, FolioAct)
        GuardarProductos(gridBoligrafos_C, FolioAct)
        GuardarProductos(gridRollerGel_P, FolioAct)
        GuardarProductos(gridRollerGel_C, FolioAct)
        GuardarProductos(gridCorrectores_P, FolioAct)
        GuardarProductos(gridCorrectores_C, FolioAct)

        GuardarProductos(gridPlumones_P, FolioAct)
        GuardarProductos(gridPlumones_C, FolioAct)
        GuardarProductos(gridMarcadores_P, FolioAct)
        GuardarProductos(gridMarcadores_C, FolioAct)
        GuardarProductos(gridResaltadores_P, FolioAct)
        GuardarProductos(gridResaltadores_C, FolioAct)
        GuardarProductos(gridMarcadoresAgua_P, FolioAct)
        GuardarProductos(gridMarcadoresAgua_C, FolioAct)
        GuardarProductos(gridMarcadoresPizarron_P, FolioAct)
        GuardarProductos(gridMarcadoresPizarron_C, FolioAct)

        GuardarProductos(gridCrayones_P, FolioAct)
        GuardarProductos(gridCrayones_C, FolioAct)
        GuardarProductos(gridLapicesColor_P, FolioAct)
        GuardarProductos(gridLapicesColor_C, FolioAct)

        GuardarProductos(gridSamsClub_P, FolioAct)
        GuardarProductos(gridSamsClub_C, FolioAct)

        GuardarProductos(gridRotuladores_P, FolioAct)
        GuardarProductos(gridRotuladores_C, FolioAct)
        GuardarProductos(gridRepuestos_P, FolioAct)
        GuardarProductos(gridRepuestos_C, FolioAct)
    End Sub

    Private Function GuardarProductos(ByVal Grilla As GridView, ByVal folio As Integer) As Boolean
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
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                              "select * from NR_Historial_Precios_Det " & _
                                              "WHERE folio_historial = " & FolioHistorial & " " & _
                                              "AND id_producto =" & IDProducto & "")
        If Precio <> 0 Then
            If Tabla.Rows.Count > 0 Then
                BD.Execute(ConexionBerol.localSqlServer, _
                           "execute NR_Precios_EditarHistorial_Det " & FolioHistorial & "," & _
                           "" & IDProducto & "," & Precio & "")
            Else
                BD.Execute(ConexionBerol.localSqlServer, _
                           "execute NR_Precios_CrearHistorial_Det " & FolioHistorial & "," & _
                           "" & IDProducto & "," & Precio & "")
            End If
        Else
            BD.Execute(ConexionBerol.localSqlServer, _
                       "DELETE FROM NR_Historial_Precios_Det " & _
                       "WHERE folio_historial = " & FolioHistorial & " " & _
                       "AND id_producto= '" & IDProducto & "'")
        End If

        Tabla.Dispose()
    End Function

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("RutasSupervisorNR.aspx")
    End Sub

    Private Function CambioEstatus(ByVal folio As Integer) As Boolean
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                               "select * from NR_Historial_Precios_Det " & _
                                               "WHERE folio_historial =" & folio & "")
        Dim Estatus As Integer
        If Tabla.Rows.Count > 0 Then
            Estatus = 1 : Else
            Estatus = 2 : End If

        BD.Execute(ConexionBerol.localSqlServer, _
                   "UPDATE NR_Rutas_Eventos_Sup SET estatus_precios=" & Estatus & " " & _
                    "FROM NR_Rutas_Eventos_Sup WHERE id_periodo=" & IDPeriodo & " " & _
                    "AND id_usuario='" & IDUsuario & "' AND id_tienda=" & IDTienda & "")

        Tabla.Dispose()
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

        Menu1.Items(0).ImageUrl = "../../Img/unselectedtabL.gif"
        Menu1.Items(1).ImageUrl = "../../Img/unselectedtabChe.gif"
        Menu1.Items(2).ImageUrl = "../../Img/unselectedtabLs.gif"
        Menu1.Items(3).ImageUrl = "../../Img/unselectedtabPun.gif"
        Menu1.Items(4).ImageUrl = "../../Img/unselectedtabGom.gif"

        Menu2.Items(0).ImageUrl = "../../Img/unselectedtabBol.gif"
        Menu2.Items(1).ImageUrl = "../../Img/unselectedtabRg.gif"
        Menu2.Items(2).ImageUrl = "../../Img/unselectedtabCor.gif"
        Menu2.Items(3).ImageUrl = "../../Img/unselectedtabPl.gif"
        Menu2.Items(4).ImageUrl = "../../Img/unselectedtabM.gif"

        Menu3.Items(0).ImageUrl = "../../Img/unselectedtabRes.gif"
        Menu3.Items(1).ImageUrl = "../../Img/unselectedtabMB.gif"
        Menu3.Items(2).ImageUrl = "../../Img/unselectedtabMP.gif"
        Menu3.Items(3).ImageUrl = "../../Img/unselectedtabC.gif"
        Menu3.Items(4).ImageUrl = "../../Img/unselectedtabLC.gif"

        Menu4.Items(0).ImageUrl = "../../Img/unselectedtabS.gif"
        Menu4.Items(1).ImageUrl = "../../Img/unselectedtabRot.gif"
        Menu4.Items(2).ImageUrl = "../../Img/unselectedtabRep.gif"
        Menu4.Items(3).ImageUrl = "../../Img/tab.gif"
        Menu4.Items(4).ImageUrl = "../../Img/tab.gif"

        For i = 0 To Menu1.Items.Count - 1
            If Sel = 0 Then
                Menu1.Items(0).ImageUrl = "../../Img/selectedtabL.gif" : End If
            If Sel = 1 Then
                Menu1.Items(1).ImageUrl = "../../Img/selectedtabChe.gif" : End If
            If Sel = 2 Then
                Menu1.Items(2).ImageUrl = "../../Img/selectedtabLs.gif" : End If
            If Sel = 3 Then
                Menu1.Items(3).ImageUrl = "../../Img/selectedtabPun.gif" : End If
            If Sel = 4 Then
                Menu1.Items(4).ImageUrl = "../../Img/selectedtabGom.gif" : End If
        Next

        For i = 0 To Menu2.Items.Count - 1
            If Sel = 5 Then
                Menu2.Items(0).ImageUrl = "../../Img/selectedtabbol.gif" : End If
            If Sel = 6 Then
                Menu2.Items(1).ImageUrl = "../../Img/selectedtabRg.gif" : End If
            If Sel = 7 Then
                Menu2.Items(2).ImageUrl = "../../Img/selectedtabCor.gif" : End If
            If Sel = 8 Then
                Menu2.Items(3).ImageUrl = "../../Img/selectedtabPl.gif" : End If
            If Sel = 9 Then
                Menu2.Items(4).ImageUrl = "../../Img/selectedtabM.gif" : End If
        Next

        For i = 0 To Menu3.Items.Count - 1
            If Sel = 10 Then
                Menu3.Items(0).ImageUrl = "../../Img/selectedtabRes.gif" : End If
            If Sel = 11 Then
                Menu3.Items(1).ImageUrl = "../../Img/selectedtabMB.gif" : End If
            If Sel = 12 Then
                Menu3.Items(2).ImageUrl = "../../Img/selectedtabMP.gif" : End If
            If Sel = 13 Then
                Menu3.Items(3).ImageUrl = "../../Img/selectedtabC.gif" : End If
            If Sel = 14 Then
                Menu3.Items(4).ImageUrl = "../../Img/selectedtabLC.gif" : End If
        Next


        For i = 0 To Menu4.Items.Count - 1
            If Sel = 15 Then
                Menu4.Items(0).ImageUrl = "../../Img/selectedtabS.gif" : End If
            If Sel = 16 Then
                Menu4.Items(1).ImageUrl = "../../Img/selectedtabRot.gif" : End If
            If Sel = 17 Then
                Menu4.Items(2).ImageUrl = "../../Img/selectedtabRep.gif" : End If
        Next
    End Sub

End Class