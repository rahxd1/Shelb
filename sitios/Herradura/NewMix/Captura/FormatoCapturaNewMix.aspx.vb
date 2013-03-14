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

Partial Public Class FormatoCapturaNewMix
    Inherits System.Web.UI.Page

    Dim IDPeriodo, IDUsuario, IDTienda As String
    Dim FolioAct As String
    Dim Agotado, Catalogado, SinPedido As String
    Dim MaterialNewMix, MaterialOtro As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionHerradura.localSqlServer, HttpContext.Current.User.Identity.Name)

            VerDatos()
            lbltienda.Text = Request.Params("Nombre")

            If Edicion = 1 Then
                btnGuardar.Visible = True : Else
                btnGuardar.Visible = False : End If
        End If
    End Sub

    Private Sub VerDatos()
        Datos()

        ''//SI LA TIENDA YA ESTA CAPTURADA, SE ACTUALIZARA
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionHerradura.localSqlServer, _
                   "SELECT * FROM NewMix_Historial " & _
                   "WHERE id_periodo=" & IDPeriodo & " AND id_usuario='" & IDUsuario & "' " & _
                   "AND id_tienda=" & IDTienda & " ")

        If Tabla.Rows.Count > 0 Then
            txtComentarios.Text = Tabla.Rows(0)("comentario_general") : End If

        Tabla.Dispose()

        Productos(FolioAct, 1, gridProductosCliente)
        Productos(FolioAct, 2, gridCompetencia)

        Exhibiciones(FolioAct, 1, 1, gridExhibicionesPropias1)
        Exhibiciones(FolioAct, 1, 2, gridExhibicionesPropias2)
        Exhibiciones(FolioAct, 1, 3, gridExhibicionesPropias3)
        Exhibiciones(FolioAct, 2, 1, gridExhibicionesCompetencia1)
        Exhibiciones(FolioAct, 2, 2, gridExhibicionesCompetencia2)
        Exhibiciones(FolioAct, 2, 3, gridExhibicionesCompetencia3)
    End Sub

    Private Function Productos(ByVal Folio As Integer, ByVal TipoProducto As Integer, ByVal Grilla As GridView) As Boolean
        CargaGrilla(ConexionHerradura.localSqlServer, _
                    "SELECT PROD.codigo,PROD.catalogacion,PROD.id_producto, PROD.presentacion, " & _
                    "PROD.id_familia,PROD.nombre_producto,isnull(HDET.frentes_frios,0)frentes_frios, " & _
                    "isnull(HDET.frentes_secos,0)frentes_secos, isnull(HDET.inventario,0)inventario, " & _
                    "isnull(HDET.inventario_sis,0)inventario_sis, isnull(HDET.agotado,0)agotado, " & _
                    "isnull(HDET.catalogado,0)catalogado, isnull(HDET.sin_pedido,0)sin_pedido " & _
                    "FROM  NewMix_Productos AS PROD " & _
                    "FULL JOIN (SELECT * FROM NewMix_Productos_Historial_Det WHERE folio_historial = " & Folio & ")AS HDET " & _
                    "ON HDET.id_producto = PROD.id_producto " & _
                    "WHERE PROD.tipo_producto= " & TipoProducto & " AND PROD.activo=1 ORDER BY PROD.id_producto", Grilla)
    End Function

    Private Function Exhibiciones(ByVal Folio As Integer, ByVal TipoProducto As Integer, ByVal NoExhibicion As Integer, ByVal Grilla As GridView) As Boolean
        CargaGrilla(ConexionHerradura.localSqlServer, _
                    "SELECT PROD.id_producto, PROD.nombre_producto, " & _
                    "CASE isnull(HDET.id_producto,0) when 0 then 0 else 1 end as SiNo " & _
                    "FROM NewMix_Productos as PROD " & _
                    "FULL JOIN (select * FROM NewMix_Exhibiciones_Historial_Det " & _
                    "WHERE folio_historial=" & Folio & " AND no_exhibicion=" & NoExhibicion & ") as HDET " & _
                    "ON PROD.id_producto = HDET.id_producto " & _
                    "WHERE PROD.tipo_producto=" & TipoProducto & "", Grilla)
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
                       "execute NewMix_EditarHistorial " & _
                       "" & FolioAct & ",'" & txtComentarios.Text & "'")
        Else
            FolioAct = BD.RT.Execute(ConexionHerradura.localSqlServer, _
                                     "execute NewMix_CrearHistorial " & _
                                     "" & IDPeriodo & ",'" & IDUsuario & "'," & _
                                     "" & IDTienda & ",'" & txtComentarios.Text & "'")
        End If

        GuardaDetalles(FolioAct)
        GuardaDetallesExhibiciones(FolioAct, gridExhibicionesPropias1, 1)
        GuardaDetallesExhibiciones(FolioAct, gridExhibicionesPropias2, 2)
        GuardaDetallesExhibiciones(FolioAct, gridExhibicionesPropias3, 3)
        GuardaDetallesExhibiciones(FolioAct, gridExhibicionesCompetencia1, 1)
        GuardaDetallesExhibiciones(FolioAct, gridExhibicionesCompetencia2, 2)
        GuardaDetallesExhibiciones(FolioAct, gridExhibicionesCompetencia3, 3)

        CambioEstatus(FolioAct)

        Me.Dispose()
        Response.Redirect("RutaNewMix.aspx")
    End Sub

    Private Function GuardaDetalles(ByVal folio As Integer) As Boolean
        Dim id_producto As Integer
        For I As Integer = 0 To gridProductosCliente.Rows.Count - 1
            id_producto = gridProductosCliente.DataKeys(I).Value.ToString()
            Dim txtFrentesFrios As TextBox = CType(gridProductosCliente.Rows(I).FindControl("txtFrentesFrios"), TextBox)
            Dim txtFrentesSecos As TextBox = CType(gridProductosCliente.Rows(I).FindControl("txtFrentesSecos"), TextBox)
            Dim txtInventario As TextBox = CType(gridProductosCliente.Rows(I).FindControl("txtInventario"), TextBox)
            Dim txtInventarioSis As TextBox = CType(gridProductosCliente.Rows(I).FindControl("txtInventarioSis"), TextBox)
            Dim chkAgotado As CheckBox = CType(gridProductosCliente.Rows(I).FindControl("chkAgotado"), CheckBox)
            Dim chkCatalogado As CheckBox = CType(gridProductosCliente.Rows(I).FindControl("chkCatalogado"), CheckBox)
            Dim chkSinPedido As CheckBox = CType(gridProductosCliente.Rows(I).FindControl("chkSinPedido"), CheckBox)

            If txtFrentesFrios.Text = "" Or txtFrentesFrios.Text = " " Then
                txtFrentesFrios.Text = "0" : End If

            If txtFrentesSecos.Text = "" Or txtFrentesSecos.Text = " " Then
                txtFrentesSecos.Text = "0" : End If

            If txtInventario.Text = "" Or txtInventario.Text = " " Then
                txtInventario.Text = "0" : End If

            If txtInventarioSis.Text = "" Or txtInventarioSis.Text = " " Then
                txtInventarioSis.Text = "0" : End If

            If chkAgotado.Checked = True Then
                Agotado = 1 : Else
                Agotado = 0 : End If

            If chkSinPedido.Checked = True Then
                SinPedido = 1 : Else
                SinPedido = 0 : End If

            If chkCatalogado.Checked = True Then
                Catalogado = 1:Else
                Catalogado = 0:End If

            GuardaHistorial(folio, id_producto, txtFrentesFrios.Text, _
                            txtFrentesSecos.Text, txtInventario.Text, txtInventarioSis.Text, _
                            Agotado, Catalogado, SinPedido)
        Next

        ''//COMPETENCIA
        For I As Integer = 0 To gridCompetencia.Rows.Count - 1
            id_producto = gridCompetencia.DataKeys(I).Value.ToString()
            Dim txtFrentesFrios As TextBox = CType(gridCompetencia.Rows(I).FindControl("txtFrentesFrios"), TextBox)
            Dim txtFrentesSecos As TextBox = CType(gridCompetencia.Rows(I).FindControl("txtFrentesSecos"), TextBox)

            If txtFrentesFrios.Text = "" Or txtFrentesFrios.Text = " " Then
                txtFrentesFrios.Text = "0" : End If

            If txtFrentesSecos.Text = "" Or txtFrentesSecos.Text = " " Then
                txtFrentesSecos.Text = "0" : End If

            GuardaHistorial(folio, id_producto, txtFrentesFrios.Text, _
                            txtFrentesSecos.Text, 0, 0, 0, 0, 0)
        Next

    End Function

    Private Function GuardaDetallesExhibiciones(ByVal folio As Integer, ByVal grilla As GridView, _
                                                ByVal NoExhibicion As Integer) As Boolean
        Dim id_producto As Integer
        For I As Integer = 0 To grilla.Rows.Count - 1
            id_producto = grilla.DataKeys(I).Value.ToString()
            Dim chkSiNo As CheckBox = CType(grilla.Rows(I).FindControl("chkSiNo"), CheckBox)

            Dim Tabla As DataTable = Tablas.Ver.DT(ConexionHerradura.localSqlServer, _
                                                   "select * from NewMix_Exhibiciones_Historial_Det " & _
                                                   "WHERE folio_historial =" & folio & " " & _
                                                   "AND id_producto =" & id_producto & " AND no_exhibicion=" & NoExhibicion & "")
            If chkSiNo.Checked = True Then
                If Tabla.Rows.Count = 0 Then
                    BD.Execute(ConexionHerradura.localSqlServer, _
                               "execute NewMix_CrearExhibicion_Det " & _
                               "" & folio & "," & id_producto & "," & NoExhibicion & "")
                End If
            Else
                If Tabla.Rows.Count > 0 Then
                    BD.Execute(ConexionHerradura.localSqlServer, _
                               "DELETE FROM NewMix_Exhibiciones_Historial_Det " & _
                               "WHERE folio_historial = " & folio & " " & _
                               "AND id_producto=" & id_producto & " AND no_exhibicion=" & NoExhibicion & "")
                End If
            End If

            Tabla.Dispose()
        Next
    End Function

    Private Function GuardaHistorial(ByVal folio_historial As Integer, ByVal id_producto As Integer, _
                                     ByVal Frentes_frios As Double, ByVal Frentes_secos As Double, _
                                     ByVal Inventario As Double, ByVal Inventario_sis As Double, _
                                     ByVal Agotado As Integer, ByVal Catalogado As Integer, _
                                     ByVal SinPedido As Integer) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionHerradura.localSqlServer, _
                                               "select * from NewMix_Productos_Historial_Det " & _
                                  "WHERE folio_historial =" & folio_historial & " " & _
                                  "AND id_producto=" & id_producto & "")
        Dim dato As Integer = Frentes_frios + Frentes_secos + Inventario + Inventario_sis

        If dato <> 0 Then
            If Tabla.Rows.Count > 0 Then
                BD.Execute(ConexionHerradura.localSqlServer, _
                           "execute NewMix_EditarHistorial_Det " & _
                            "" & folio_historial & "," & id_producto & ", " & _
                            "" & Frentes_frios & "," & _
                            "" & Frentes_secos & "," & Inventario & "," & _
                            "" & Inventario_sis & "," & Agotado & ", " & _
                            "" & Catalogado & "," & SinPedido & "")
            Else
                BD.Execute(ConexionHerradura.localSqlServer, _
                           "execute NewMix_CrearHistorial_Det " & _
                           "" & folio_historial & "," & id_producto & ", " & _
                           "" & Frentes_frios & ", " & _
                           "" & Frentes_secos & "," & Inventario & " " & _
                           "," & Inventario_sis & "," & Agotado & ", " & _
                           "" & Catalogado & "," & SinPedido & "")
            End If
        Else
            If Tabla.Rows.Count > 0 Then
                BD.Execute(ConexionHerradura.localSqlServer, _
                           "DELETE FROM NewMix_Productos_Historial_Det " & _
                          "WHERE folio_historial = " & folio_historial & " " & _
                          "AND id_producto=" & id_producto & "")
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
                                               "select * from NewMix_Productos_Historial_Det " & _
                                  "WHERE folio_historial =" & folio & "")
        Dim Estatus As Integer
        If Tabla.Rows.Count > 0 Then
            Estatus = 1 : Else
            Estatus = 2 : End If
        Tabla.Dispose()

        BD.Execute(ConexionHerradura.localSqlServer, _
                   "UPDATE NewMix_Rutas_Eventos " & _
                   "SET estatus=" & Estatus & " " & _
                   "FROM NewMix_Rutas_Eventos " & _
                   "WHERE id_periodo=" & IDPeriodo & " AND id_usuario='" & IDUsuario & "' " & _
                   "AND id_tienda=" & IDTienda & "")

    End Function

    Sub Datos()
        FolioAct = Request.Params("folio")
        IDPeriodo = Request.Params("periodo")
        IDUsuario = Request.Params("usuario")
        IDTienda = Request.Params("tienda")
    End Sub

    Protected Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridProductosCliente.Columns(0).Visible = False
    End Sub

    Protected Sub gridProductosCliente_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridProductosCliente.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            For i = 0 To CInt(gridProductosCliente.Rows.Count) - 0
                For fn = 1 To 6 Step 2
                    If e.Row.Cells(0).Text = fn Then
                        For iC = 0 To 10
                            e.Row.Cells(iC).BackColor = Drawing.Color.LightYellow : Next iC
                    End If
                Next fn
            Next i
        End If
    End Sub

    Protected Sub gridCompetencia_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridCompetencia.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            For i = 0 To CInt(gridCompetencia.Rows.Count) - 0
                If e.Row.Cells(0).Text = "0" Then
                    e.Row.FindControl("chkCatalogado").Visible = False
                End If
            Next i
        End If
    End Sub

    Protected Sub Menu1_MenuItemClick(ByVal sender As Object, ByVal e As MenuEventArgs) Handles Menu1.MenuItemClick
        MultiView1.ActiveViewIndex = Int32.Parse(e.Item.Value)
        Dim i, Sel As Integer
        Sel = e.Item.Value

        Menu1.Items(0).ImageUrl = "../../Img/unselectedtabPp.gif"
        Menu1.Items(1).ImageUrl = "../../Img/unselectedtabEp.gif"
        Menu1.Items(2).ImageUrl = "../../Img/unselectedtabPc.gif"
        Menu1.Items(3).ImageUrl = "../../Img/unselectedtabEc.gif"

        For i = 0 To Menu1.Items.Count - 1
            If Sel = 0 Then
                Menu1.Items(0).ImageUrl = "../../Img/selectedtabPp.gif" : End If
            If Sel = 1 Then
                Menu1.Items(1).ImageUrl = "../../Img/selectedtabEp.gif" : End If
            If Sel = 2 Then
                Menu1.Items(2).ImageUrl = "../../Img/selectedtabPc.gif" : End If
            If Sel = 3 Then
                Menu1.Items(3).ImageUrl = "../../Img/selectedtabEc.gif" : End If
        Next
    End Sub
End Class