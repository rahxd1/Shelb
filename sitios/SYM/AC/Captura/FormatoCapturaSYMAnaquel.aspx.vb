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

Partial Public Class FormatoCapturaSYMAnaquel
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
                                               "select * FROM Anaquel_Historial " & _
                                               "WHERE folio_historial =" & FolioAct & "")
        If Tabla.Rows.Count > 0 Then
            txtComentario_General.Text = Tabla.Rows(0)("comentario_general")
        End If

        Tabla.Dispose()

        GrillaBase(gridProductos1, FolioAct, 101)
        GrillaBase(gridProductos2, FolioAct, 103)
        GrillaBase(gridProductos3, FolioAct, 104)
        GrillaBase(gridProductos4, FolioAct, 110)
        GrillaBase(gridProductos5, FolioAct, 105)
        GrillaBase(gridProductos6, FolioAct, 109)
        GrillaBase(gridProductos7, FolioAct, 108)
    End Sub

    Private Function GrillaBase(ByVal Grilla As GridView, ByVal Folio As Integer, ByVal Grupo As Integer) As Boolean
        CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT DISTINCT PROD.id_empresa,PROD.id_linea,PROD.nombre_linea,EMP.nombre_empresa, " & _
                    "isnull(HDET.frentes,0)frentes " & _
                    "FROM Linea_Productos_Nuevo PROD " & _
                    "INNER JOIN Anaquel_Empresa EMP on EMP.id_empresa=PROD.id_empresa " & _
                    "FULL JOIN (SELECT * FROM Anaquel_Productos_Historial_Det " & _
                    "WHERE folio_historial=" & Folio & ")HDET ON HDET.id_linea = PROD.id_linea " & _
                    "WHERE PROD.tipo_grupo=" & Grupo & " AND PROD.activo =1 " & _
                    "order by PROD.id_empresa,PROD.nombre_linea ", Grilla)
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        btnGuardar.Enabled = False

        Guardar()
    End Sub

    Sub Guardar()
        Datos()

        If txtComentario_General.Text = "OK" Then
            txtComentario_General.Text = "" : End If

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "select * from Anaquel_Historial " & _
                                               "WHERE folio_historial = " & FolioAct & "")
        If Tabla.Rows.Count > 0 Then
            BD.Execute(ConexionSYM.localSqlServer, _
                       "execute Anaquel_EditarHistorial " & FolioAct & "," & _
                       "'" & txtComentario_General.Text & "'")
        Else
            FolioAct = BD.RT.Execute(ConexionSYM.localSqlServer, _
                       "execute Anaquel_CrearHistorial '" & IDUsuario & "'," & IDPeriodo & "," & _
                       "" & IDTienda & ",'" & txtComentario_General.Text & "'")
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

        Response.Redirect("RutaSYMAC.aspx")
    End Sub

    Private Function GuardarProductos(ByVal folio As Integer, ByVal Grilla As GridView) As Boolean
        ''//Lineas
        Dim IDLinea As Integer
        For I As Integer = 0 To Grilla.Rows.Count - 1
            IDLinea = Grilla.DataKeys(I).Value.ToString()
            Dim txtFrentes As TextBox = CType(Grilla.Rows(I).FindControl("txtFrentes"), TextBox)

            If txtFrentes.Text = "" Or txtFrentes.Text = " " Then
                txtFrentes.Text = 0 : End If

            GuardaDetalle(folio, IDLinea, txtFrentes.Text)
        Next
    End Function

    Private Function GuardaDetalle(ByVal FolioHistorial As Integer, ByVal IDLinea As Integer, _
                                ByVal Frentes As Double) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "select * from Anaquel_Productos_Historial_Det " & _
                                  "WHERE folio_historial = " & FolioHistorial & " " & _
                                  "AND id_linea=" & IDLinea & "")
        If Frentes <> 0 Then
            If Tabla.Rows.Count = 1 Then
                BD.Execute(ConexionSYM.localSqlServer, _
                           "execute Anaquel_EditarHistorial_Det " & _
                           "" & FolioHistorial & "," & IDLinea & "," & Frentes & "")
            Else
                BD.Execute(ConexionSYM.localSqlServer, _
                           "execute Anaquel_CrearHistorial_Det " & _
                           "" & FolioHistorial & "," & IDLinea & "," & Frentes & "")
            End If
        Else
            If Tabla.Rows.Count = 1 Then
                BD.Execute(ConexionSYM.localSqlServer, _
                           "DELETE FROM Anaquel_Productos_Historial_Det " & _
                           "WHERE folio_historial = " & FolioHistorial & " " & _
                           "AND id_linea=" & IDLinea & "")
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
                                               "select * from Anaquel_Productos_Historial_Det " & _
                                               "WHERE folio_historial =" & folio & "")
        Dim Estatus As Integer
        If Tabla.Rows.Count > 0 Then
            Estatus = 1 : Else
            Estatus = 2 : End If
        Tabla.Dispose()

        BD.Execute(ConexionSYM.localSqlServer, _
                   "UPDATE AC_Rutas_Eventos SET estatus_anaquel=" & Estatus & " " & _
                   "FROM AC_Rutas_Eventos " & _
                   "WHERE id_periodo=" & IDPeriodo & " AND id_usuario='" & IDUsuario & "' " & _
                   "AND id_tienda=" & IDTienda & "")
    End Function

    Sub Datos()
        FolioAct = Request.Params("folio")
        IDTienda = Request.Params("tienda")
        IDUsuario = Request.Params("usuario")
        IDPeriodo = Request.Params("periodo")
    End Sub

    Private Sub gridProductos1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridProductos1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            For i = 0 To CInt(gridProductos1.Rows.Count) - 0
                If e.Row.Cells(0).Text = 1 Then
                    e.Row.Cells(0).BackColor = Drawing.Color.GreenYellow
                    e.Row.Cells(1).BackColor = Drawing.Color.GreenYellow
                    e.Row.Cells(2).BackColor = Drawing.Color.GreenYellow
                    e.Row.Cells(3).BackColor = Drawing.Color.GreenYellow
                End If
            Next i
        End If
    End Sub

    Private Sub gridProductos2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridProductos2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            For i = 0 To CInt(gridProductos2.Rows.Count) - 0
                If e.Row.Cells(0).Text = 1 Then
                    e.Row.Cells(0).BackColor = Drawing.Color.GreenYellow
                    e.Row.Cells(1).BackColor = Drawing.Color.GreenYellow
                    e.Row.Cells(2).BackColor = Drawing.Color.GreenYellow
                    e.Row.Cells(3).BackColor = Drawing.Color.GreenYellow
                End If
            Next i
        End If
    End Sub

    Private Sub gridProductos3_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridProductos3.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            For i = 0 To CInt(gridProductos3.Rows.Count) - 0
                If e.Row.Cells(0).Text = 1 Then
                    e.Row.Cells(0).BackColor = Drawing.Color.GreenYellow
                    e.Row.Cells(1).BackColor = Drawing.Color.GreenYellow
                    e.Row.Cells(2).BackColor = Drawing.Color.GreenYellow
                    e.Row.Cells(3).BackColor = Drawing.Color.GreenYellow
                End If
            Next i
        End If
    End Sub

    Private Sub gridProductos4_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridProductos4.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            For i = 0 To CInt(gridProductos4.Rows.Count) - 0
                If e.Row.Cells(0).Text = 1 Then
                    e.Row.Cells(0).BackColor = Drawing.Color.GreenYellow
                    e.Row.Cells(1).BackColor = Drawing.Color.GreenYellow
                    e.Row.Cells(2).BackColor = Drawing.Color.GreenYellow
                    e.Row.Cells(3).BackColor = Drawing.Color.GreenYellow
                End If
            Next i
        End If
    End Sub

    Private Sub gridProductos5_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridProductos5.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            For i = 0 To CInt(gridProductos5.Rows.Count) - 0
                If e.Row.Cells(0).Text = 1 Then
                    e.Row.Cells(0).BackColor = Drawing.Color.GreenYellow
                    e.Row.Cells(1).BackColor = Drawing.Color.GreenYellow
                    e.Row.Cells(2).BackColor = Drawing.Color.GreenYellow
                    e.Row.Cells(3).BackColor = Drawing.Color.GreenYellow
                End If
            Next i
        End If
    End Sub

    Private Sub gridProductos6_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridProductos6.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            For i = 0 To CInt(gridProductos6.Rows.Count) - 0
                If e.Row.Cells(0).Text = 1 Then
                    e.Row.Cells(0).BackColor = Drawing.Color.GreenYellow
                    e.Row.Cells(1).BackColor = Drawing.Color.GreenYellow
                    e.Row.Cells(2).BackColor = Drawing.Color.GreenYellow
                    e.Row.Cells(3).BackColor = Drawing.Color.GreenYellow
                End If
            Next i
        End If
    End Sub

    Private Sub gridProductos7_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridProductos7.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            For i = 0 To CInt(gridProductos7.Rows.Count) - 0
                If e.Row.Cells(0).Text = 1 Then
                    e.Row.Cells(0).BackColor = Drawing.Color.GreenYellow
                    e.Row.Cells(1).BackColor = Drawing.Color.GreenYellow
                    e.Row.Cells(2).BackColor = Drawing.Color.GreenYellow
                    e.Row.Cells(3).BackColor = Drawing.Color.GreenYellow
                End If
            Next i
        End If
    End Sub

    Private Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridProductos1.Columns(0).Visible = False
        gridProductos2.Columns(0).Visible = False
        gridProductos3.Columns(0).Visible = False
        gridProductos4.Columns(0).Visible = False
        gridProductos5.Columns(0).Visible = False
        gridProductos6.Columns(0).Visible = False
        gridProductos7.Columns(0).Visible = False
    End Sub

    Protected Sub Menu1_MenuItemClick(ByVal sender As Object, ByVal e As MenuEventArgs) Handles Menu1.MenuItemClick
        MultiView1.ActiveViewIndex = Int32.Parse(e.Item.Value)
        Dim i, Sel As Integer
        Sel = e.Item.Value

        Menu1.Items(0).ImageUrl = "../../Img/unselectedtabDM.gif"
        Menu1.Items(1).ImageUrl = "../../Img/unselectedtabJL.gif"
        Menu1.Items(2).ImageUrl = "../../Img/unselectedtabJT.gif"
        Menu1.Items(3).ImageUrl = "../../Img/unselectedtabLL.gif"
        Menu1.Items(4).ImageUrl = "../../Img/unselectedtabL.gif"
        Menu2.Items(0).ImageUrl = "../../Img/unselectedtabLM.gif"
        Menu2.Items(1).ImageUrl = "../../Img/unselectedtabLC.gif"

        For i = 0 To Menu1.Items.Count - 1
            If Sel = 0 Then
                Menu1.Items(0).ImageUrl = "../../Img/selectedtabDM.gif" : End If
            If Sel = 1 Then
                Menu1.Items(1).ImageUrl = "../../Img/selectedtabJL.gif" : End If
            If Sel = 2 Then
                Menu1.Items(2).ImageUrl = "../../Img/selectedtabJT.gif" : End If
            If Sel = 3 Then
                Menu1.Items(3).ImageUrl = "../../Img/selectedtabLL.gif" : End If
            If Sel = 4 Then
                Menu1.Items(4).ImageUrl = "../../Img/selectedtabL.gif" : End If
        Next

        For i = 0 To Menu2.Items.Count - 1
            If Sel = 5 Then
                Menu2.Items(0).ImageUrl = "../../Img/selectedtabLM.gif" : End If
            If Sel = 6 Then
                Menu2.Items(1).ImageUrl = "../../Img/selectedtabLC.gif" : End If
        Next
    End Sub

End Class