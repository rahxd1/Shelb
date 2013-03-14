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

Partial Public Class FormatoCapturaSYMInventario
    Inherits System.Web.UI.Page

    Dim IDUsuario, IDPeriodo, Catalogado, FolioAct As String

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
                                               "select * FROM InventarioPOP_Historial " & _
                                               "WHERE folio_historial =" & FolioAct & "")
        If Tabla.Rows.Count > 0 Then
            txtComentario_General.Text = Tabla.Rows(0)("comentario_general")
        End If

        Tabla.Dispose()

        GrillaBase(gridProductos1, FolioAct, 1)
        GrillaBase(gridProductos2, FolioAct, 2)
    End Sub

    Private Function GrillaBase(ByVal Grilla As GridView, ByVal Folio As Integer, ByVal Grupo As Integer) As Boolean
        CargaGrilla(ConexionSYM.localSqlServer, "SELECT PROD.id_material, PROD.nombre_material, isnull(HDET.cantidad,0) as cantidad " & _
                                      "FROM POP_Productos AS PROD " & _
                                      "FULL JOIN (SELECT * FROM InventarioPOP_Historial_Det " & _
                                      "WHERE folio_historial = '" & Folio & "') AS HDET " & _
                                      "ON HDET.id_material = PROD.id_material " & _
                                      "WHERE PROD.id_tipo=" & Grupo & " AND PROD.activo =1", Grilla)
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        btnGuardar.Enabled = False

        Guardar()
    End Sub

    Sub Guardar()
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "select * from InventarioPOP_Historial " & _
                                               "WHERE folio_historial = " & FolioAct & "")
        If Tabla.Rows.Count > 0 Then
            BD.Execute(ConexionSYM.localSqlServer, _
                       "execute InventarioPOP_EditarHistorial " & FolioAct & "," & _
                       "" & IDPeriodo & ",'" & IDUsuario & "','" & txtComentario_General.Text & "'")
        Else
            FolioAct = BD.RT.Execute(ConexionSYM.localSqlServer, _
                       "execute InventarioPOP_CrearHistorial '" & IDUsuario & "'," & _
                       "" & IDPeriodo & ",'" & txtComentario_General.Text & "'")
        End If

        GuardarProductos(FolioAct, gridProductos1)
        GuardarProductos(FolioAct, gridProductos2)
        CambioEstatus(FolioAct)

        Tabla.Dispose()

        Me.Dispose()

        Response.Redirect("RutaSYMPOP.aspx")
    End Sub

    Private Function GuardarProductos(ByVal folio As Integer, ByVal Grilla As GridView) As Boolean
        ''//PRODUCTOS
        Dim IDMaterial As Integer
        For I As Integer = 0 To Grilla.Rows.Count - 1
            IDMaterial = Grilla.DataKeys(I).Value.ToString()
            Dim txtCantidad As TextBox = CType(Grilla.Rows(I).FindControl("txtCantidad"), TextBox)

            If txtCantidad.Text = "" Or txtCantidad.Text = " " Then
                txtCantidad.Text = 0 : End If

            GuardaDetalle(folio, IDMaterial, txtCantidad.Text)
        Next
    End Function

    Private Function GuardaDetalle(ByVal FolioHistorial As Integer, ByVal IDMaterial As Integer, _
                                ByVal Cantidad As Double) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "select * from InventarioPOP_Historial_Det " & _
                                               "WHERE folio_historial = " & FolioHistorial & " " & _
                                               "AND id_material =" & IDMaterial & "")
        If Cantidad <> 0 Then
            If Tabla.Rows.Count = 1 Then
                BD.Execute(ConexionSYM.localSqlServer, _
                           "execute InventarioPOP_EditarHistorial_Det " & FolioHistorial & "," & _
                           "" & IDMaterial & "," & Cantidad & "")
            Else
                BD.Execute(ConexionSYM.localSqlServer, _
                           "execute InventarioPOP_CrearHistorial_Det " & FolioHistorial & "," & _
                           "" & IDMaterial & "," & Cantidad & "")
            End If
        Else
            If Tabla.Rows.Count = 1 Then
                BD.Execute(ConexionSYM.localSqlServer, _
                           "DELETE FROM InventarioPOP_Historial_Det " & _
                           "WHERE folio_historial = " & FolioHistorial & " " & _
                           "AND id_material= '" & IDMaterial & "'")
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
                                               "select * from POP_Historial " & _
                                               "WHERE folio_historial =" & folio & "")
        Dim Estatus As Integer
        If Tabla.Rows.Count > 0 Then
            Estatus = 1 : Else
            Estatus = 2 : End If
        Tabla.Dispose()

        BD.Execute(ConexionSYM.localSqlServer, _
                   "UPDATE POP_Rutas_Eventos SET estatus_inventario=" & Estatus & " " & _
                   "FROM POP_Rutas_Eventos " & _
                   "WHERE id_periodo=" & IDPeriodo & " AND id_usuario='" & IDUsuario & "'")
        Tabla.Dispose()
    End Function

    Sub Datos()
        FolioAct = Request.Params("folio")
        IDUsuario = Request.Params("usuario")
        IDPeriodo = Request.Params("periodo")
    End Sub

End Class