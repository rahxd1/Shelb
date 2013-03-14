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

Partial Public Class FormatoInventariosNR
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
                                               "INNER JOIN Cadenas_Tiendas CAD ON CAD.id_cadena=TI.id_cadena " & _
                                               "WHERE id_tienda=" & IDTienda & "")
        If Tabla.Rows.Count > 0 Then
            lblTienda.Text = Tabla.Rows(0)("nombre")
            lblCadena.Text = Tabla.Rows(0)("nombre_cadena")
        End If

        Tabla.Dispose()

        GrillaBase(gridLapices, FolioAct, 1, 1)
        GrillaBase(gridChecadores, FolioAct, 17, 1)
        GrillaBase(gridLapiceros, FolioAct, 10, 1)
        GrillaBase(gridPuntillas, FolioAct, 14, 1)
        GrillaBase(gridGomas, FolioAct, 15, 1)
        GrillaBase(gridBoligrafos, FolioAct, 2, 1)
        GrillaBase(gridRollerGel, FolioAct, 3, 1)
        GrillaBase(gridCorrectores, FolioAct, 4, 1)

        GrillaBase(gridPlumones, FolioAct, 5, 1)
        GrillaBase(gridMarcadores, FolioAct, 6, 1)
        GrillaBase(gridResaltadores, FolioAct, 7, 1)
        GrillaBase(gridMarcadoresAgua, FolioAct, 11, 1)
        GrillaBase(gridMarcadoresPizarron, FolioAct, 12, 1)

        GrillaBase(gridCrayones, FolioAct, 8, 1)
        GrillaBase(gridLapicesColor, FolioAct, 9, 1)

        GrillaBase(gridSamsClub, FolioAct, 13, 1)

        GrillaBase(gridRepuestos, FolioAct, 17, 1)
        GrillaBase(gridRotuladores, FolioAct, 18, 1)
    End Sub

    Private Function GrillaBase(ByVal Grilla As GridView, ByVal Folio As Integer, _
                                ByVal TipoProducto As Integer, ByVal Propio As Integer) As Boolean
        CargaGrilla(ConexionBerol.localSqlServer, _
                    "SELECT PROD.id_producto, PROD.nombre_producto,PROD.codigo, PRES.nombre_presentacion,MAR.nombre_marca, " & _
                    "PROD.tipo_producto, isnull(HDET.catalogado,0)Cat, isnull(HDET.surtido,0)Sur, " & _
                    "isnull(HDET.inv_piso,0)inv_piso, isnull(HDET.inv_bodega,0)inv_bodega " & _
                    "FROM Productos PROD " & _
                    "INNER JOIN Tipo_Presentacion as PRES ON PRES.tipo_presentacion=PROD.tipo_presentacion " & _
                    "INNER JOIN Marcas as MAR ON MAR.id_marca = PROD.id_marca " & _
                    "INNER JOIN Tipo_Productos as TPROD ON TPROD.tipo_producto=PROD.tipo_producto " & _
                    "FULL JOIN (SELECT * FROM NR_Historial_Inventarios_Det " & _
                    "WHERE folio_historial=" & Folio & ") AS HDET ON HDET.id_producto = PROD.id_producto " & _
                    "WHERE PROD.tipo_producto=" & TipoProducto & " AND MAR.propia=" & Propio & " AND PROD.activo =1 " & _
                    "ORDER BY PROD.nombre_producto", Grilla)
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                               "select * from NR_Historial WHERE folio_historial=" & FolioAct & "")
        If Tabla.Rows.Count > 0 Then
            GuardarGrillas()

            CambioEstatus(FolioAct)
        Else
            FolioAct = BD.RT.Execute(ConexionBerol.localSqlServer, _
                                     "execute NR_CrearHistorial '" & IDUsuario & "'," & _
                                     "" & IDPeriodo & "," & IDTienda & "")
            GuardarGrillas()
            CambioEstatus(FolioAct)
        End If

        Tabla.Dispose()

        Response.Redirect("RutasPromotorNR.aspx")
    End Sub

    Sub GuardarGrillas()
        GuardarProductos(gridLapices, FolioAct)
        GuardarProductos(gridChecadores, FolioAct)
        GuardarProductos(gridLapiceros, FolioAct)
        GuardarProductos(gridPuntillas, FolioAct)
        GuardarProductos(gridGomas, FolioAct)
        GuardarProductos(gridBoligrafos, FolioAct)
        GuardarProductos(gridRollerGel, FolioAct)
        GuardarProductos(gridCorrectores, FolioAct)

        GuardarProductos(gridPlumones, FolioAct)
        GuardarProductos(gridMarcadores, FolioAct)
        GuardarProductos(gridResaltadores, FolioAct)
        GuardarProductos(gridMarcadoresAgua, FolioAct)
        GuardarProductos(gridMarcadoresPizarron, FolioAct)

        GuardarProductos(gridCrayones, FolioAct)
        GuardarProductos(gridLapicesColor, FolioAct)

        GuardarProductos(gridSamsClub, FolioAct)

        GuardarProductos(gridRotuladores, FolioAct)
        GuardarProductos(gridRepuestos, FolioAct)
    End Sub

    Private Function GuardarProductos(ByVal Grilla As GridView, ByVal folio As Integer) As Boolean
        ''//PRODUCTOS
        Dim IDProducto, Catalogado, Surtido As Integer
        For i As Integer = 0 To Grilla.Rows.Count - 1
            IDProducto = Grilla.DataKeys(i).Value.ToString()
            Dim chkCatalogo As CheckBox = CType(Grilla.Rows(i).FindControl("chkCatalogo"), CheckBox)
            Dim chkSurtido As CheckBox = CType(Grilla.Rows(i).FindControl("chkSurtido"), CheckBox)
            Dim txtInvPiso As TextBox = CType(Grilla.Rows(i).FindControl("txtInvPiso"), TextBox)
            Dim txtInvBodega As TextBox = CType(Grilla.Rows(i).FindControl("txtInvBodega"), TextBox)

            If chkCatalogo.Checked = True Then
                Catalogado = 1 : Else
                Catalogado = 0 : End If
            If chkSurtido.Checked = True Then
                Surtido = 1 : Else
                Surtido = 0 : End If

            If txtInvPiso.Text = "" Or txtInvPiso.Text = " " Then
                txtInvPiso.Text = 0 : End If
            If txtInvBodega.Text = "" Or txtInvBodega.Text = " " Then
                txtInvBodega.Text = 0 : End If

            GuardaDetalle(folio, IDProducto, Catalogado, Surtido, txtInvPiso.Text, txtInvBodega.Text)
        Next
    End Function

    Private Function GuardaDetalle(ByVal FolioHistorial As Integer, ByVal IDProducto As Integer, _
                                   ByVal Catalogado As Integer, ByVal Surtido As Integer, _
                                   ByVal Inv_Piso As Double, ByVal Inv_Bodega As Double) As Boolean

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                               "select * from NR_Historial_Inventarios_Det " & _
                                               "WHERE folio_historial=" & FolioHistorial & " " & _
                                               "AND id_producto=" & IDProducto & "")

        If Catalogado <> 0 Or Surtido <> 0 Or Inv_Piso <> 0 Or Inv_Bodega <> 0 Then
            If Tabla.Rows.Count > 0 Then
                BD.Execute(ConexionBerol.localSqlServer, _
                           "execute NR_Inventarios_EditarHistorial_Det " & FolioHistorial & "," & _
                           "" & IDProducto & "," & Catalogado & "," & Surtido & "," & _
                           "" & Inv_Piso & "," & Inv_Bodega & "")
            Else
                BD.Execute(ConexionBerol.localSqlServer, _
                           "execute NR_Inventarios_CrearHistorial_Det " & FolioHistorial & "," & _
                           "" & IDProducto & "," & Catalogado & "," & Surtido & "," & _
                           "" & Inv_Piso & "," & Inv_Bodega & "")
            End If
        Else
            BD.Execute(ConexionBerol.localSqlServer, _
                       "DELETE FROM NR_Historial_Inventarios_Det " & _
                       "WHERE folio_historial = " & FolioHistorial & " " & _
                       "AND id_producto=" & IDProducto & "")
        End If

        Tabla.Dispose()
    End Function

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("RutasPromotorNR.aspx")
    End Sub

    Private Function CambioEstatus(ByVal folio As Integer) As Boolean
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                               "select * from NR_Historial_Inventarios_Det " & _
                                               "WHERE folio_historial =" & folio & "")
        Dim Estatus As Integer
        If Tabla.Rows.Count > 0 Then
            Estatus = 1 : Else
            Estatus = 2 : End If

        BD.Execute(ConexionBerol.localSqlServer, _
                   "UPDATE NR_Rutas_Eventos " & _
                   "SET estatus_inventarios=" & Estatus & " " & _
                   "FROM NR_Rutas_Eventos " & _
                   "WHERE id_periodo=" & IDPeriodo & " " & _
                   "AND id_usuario='" & IDUsuario & "' " & _
                   "AND id_tienda=" & IDTienda & "")

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
        Dim i,Sel As Integer
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