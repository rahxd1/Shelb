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

Partial Public Class FormatoExhibicionesMarsConv
    Inherits System.Web.UI.Page

    Dim IDTienda, IDUsuario, IDPeriodo, IDExhibidor As String
    Dim FolioAct As String

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
                                               "select * from Conv_Historial_Exhibiciones where folio_historial =" & FolioAct & "")
        If Tabla.Rows.Count > 0 Then
            If Tabla.Rows(0)("material_pop") = 1 Then
                chkMaterialPOP.Checked = True : Else
                chkMaterialPOP.Checked = False : End If

            txtComentarioGeneral.Text = Tabla.Rows(0)("comentarios")
        End If

        Tabla.Dispose()

        CargaGrilla(ConexionMars.localSqlServer, "SELECT EXH.id_exhibidor, EXH.nombre_exhibidor, " & _
                    "ISNULL(HDET.cantidad,0) as cantidad " & _
                    "FROM Conv_Exhibiciones AS EXH " & _
                    "FULL JOIN (SELECT * FROM Conv_Exhibiciones_Historial_Det " & _
                    "WHERE folio_historial=" & FolioAct & ")AS HDET " & _
                    "ON HDET.id_exhibidor = EXH.id_exhibidor " & _
                    "WHERE EXH.activo=1 AND EXH.tipo_exhibidor=1", gridExhibiciones)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        btnGuardar.Enabled = False

        Guardar()
    End Sub

    Sub Guardar()
        Dim POP As Integer
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * from Conv_Historial_Exhibiciones WHERE folio_historial=" & FolioAct & "")

        If chkMaterialPOP.Checked = True Then
            POP = "1" : Else
            POP = "0" : End If

        If tabla.Rows.Count > 0 Then
            BD.Execute(ConexionMars.localSqlServer, _
                       "UPDATE Conv_Historial_Exhibiciones SET material_pop=" & POP & ", comentarios='" & txtComentarioGeneral.Text & "' " & _
                       "FROM Conv_Historial_Exhibiciones WHERE folio_historial=" & FolioAct & "")

            GuardarProductos(FolioAct)
            CambioEstatus(FolioAct)
        Else
            FolioAct = BD.RT.Execute(ConexionMars.localSqlServer, _
                                     "insert into Conv_Historial_Exhibiciones(orden,id_usuario,id_tienda,material_pop,comentarios)" & _
                                     "values(" & IDPeriodo & ",'" & IDUsuario & "'," & _
                                     "" & IDTienda & "," & POP & ",'" & txtComentarioGeneral.Text & "') SELECT @@IDENTITY AS 'folio_historial'")
            GuardarProductos(FolioAct)
            CambioEstatus(FolioAct)
        End If

        Tabla.Dispose()
        Response.Redirect("RutaMarsConv.aspx")
    End Sub

    Private Function GuardarProductos(ByVal folio As Integer) As Boolean
        ''//Productos
        Dim I As Integer
        For I = 0 To CInt(Me.gridExhibiciones.Rows.Count) - 1
            IDExhibidor = Me.gridExhibiciones.DataKeys(I).Value.ToString()
            Dim txtCantidad As TextBox = CType(Me.gridExhibiciones.Rows(I).FindControl("txtCantidad"), TextBox)

            If txtCantidad.Text = "" Or txtCantidad.Text = " " Then
                txtCantidad.Text = 0 : End If

            GuardaDetExh(folio, IDExhibidor, txtCantidad.Text)
        Next
    End Function

    Private Function GuardaDetExh(ByVal folio As Integer, ByVal id_exhibidor As Integer, ByVal cantidad As Double) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * from Conv_Exhibiciones_Historial_Det " & _
                                               "WHERE folio_historial=" & folio & " AND id_exhibidor='" & id_exhibidor & "'")
        If cantidad <> 0 Then
            If Tabla.Rows.Count = 1 Then
                BD.Execute(ConexionMars.localSqlServer, _
                           "UPDATE Conv_Exhibiciones_Historial_Det SET cantidad=" & cantidad & " " & _
                           "FROM Conv_Exhibiciones_Historial_Det " & _
                           "WHERE folio_historial=" & folio & " AND id_exhibidor=" & id_exhibidor & "")
            Else
                BD.Execute(ConexionMars.localSqlServer, _
                           "insert into Conv_Exhibiciones_Historial_Det(folio_historial,id_exhibidor,cantidad)" & _
                           "values(" & folio & "," & id_exhibidor & "," & cantidad & ")")
            End If
        Else
            If Tabla.Rows.Count = 1 Then
                BD.Execute(ConexionMars.localSqlServer, _
                           "DELETE FROM Conv_Exhibiciones_Historial_Det " & _
                           "WHERE folio_historial=" & folio & " AND id_exhibidor=" & id_exhibidor & "")
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
                   "UPDATE Conv_Rutas_Eventos_Exhibiciones SET estatus_exhibiciones=1 " & _
                   "FROM Conv_Rutas_Eventos_Exhibiciones " & _
                   "WHERE orden=" & IDPeriodo & " AND id_usuario='" & IDUsuario & "' " & _
                   "AND id_tienda=" & IDTienda & "")
    End Function

    Sub Datos()
        IDTienda = Request.Params("tienda")
        IDUsuario = Request.Params("usuario")
        IDPeriodo = Request.Params("periodo")
        FolioAct = Request.Params("folio")
    End Sub
End Class