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

Partial Public Class FormatoFrentesNR
    Inherits System.Web.UI.Page

    Dim IDTienda, IDUsuario, IDPeriodo As String
    Dim FolioAct As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionBerol.localSqlServer, HttpContext.Current.User.Identity.Name)

            VerDatos()

            If Edicion = 1 Then
                btnGuardar.Visible = True : Else
                btnGuardar.Visible = False : End If
        End If
    End Sub

    Private Sub VerDatos()
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                               "select * FROM NR_Tiendas as TI  " & _
                                               "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena=TI.id_cadena " & _
                                               "WHERE id_tienda = " & IDTienda & "")
        If Tabla.Rows.Count > 0 Then
            lblTienda.Text = Tabla.Rows(0)("nombre")
            lblCadena.Text = Tabla.Rows(0)("nombre_cadena")
        End If

        ''//frentes
        CargaGrilla(ConexionBerol.localSqlServer, _
                    "SELECT TPROD.tipo_producto, TPROD.nombre_tipo, isnull(HDET.[1],0)NR, isnull(HDET.[2],0)Competencia " & _
                    "FROM Tipo_Productos AS TPROD " & _
                    "FULL JOIN (SELECT folio_historial,tipo_producto,ISNULL([1],0)[1], ISNULL([2],0)[2] " & _
                    "FROM (SELECT folio_historial,tipo_producto, cantidad, propia " & _
                    "FROM  NR_Historial_Frentes_Det) PVT " & _
                    "PIVOT(MIN(Cantidad) FOR [propia] IN([1], [2]))HDET WHERE folio_historial=" & FolioAct & "" & _
                    ")AS HDET ON HDET.tipo_producto = TPROD.tipo_producto", Me.gridFrentes)

        ''//exhibiciones
        CargaGrilla(ConexionBerol.localSqlServer, _
                    "SELECT EXH.id_exhibicion, EXH.nombre_exhibicion, isnull(HDET.cantidad,0) as cantidad " & _
                    "FROM Tipo_Exhibiciones AS EXH " & _
                    "FULL JOIN (select * from NR_Historial_Exhibiciones_Det " & _
                    "WHERE folio_historial=" & FolioAct & ")AS HDET " & _
                    "ON HDET.id_exhibicion = EXH.id_exhibicion ", Me.gridExhibiciones)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        btnGuardar.Enabled = False

        Guardar()
    End Sub

    Sub Guardar()
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                               "select * from NR_Historial WHERE folio_historial=" & FolioAct & "")
        If Tabla.Rows.Count = 0 Then
            GuardarProductos(FolioAct)
            CambioEstatus(FolioAct)
        Else
            FolioAct = BD.RT.Execute(ConexionBerol.localSqlServer, _
                          "execute NR_CrearHistorial '" & IDUsuario & "', " & IDPeriodo & "," & IDTienda & "")
        End If

        GuardarProductos(FolioAct)
        CambioEstatus(FolioAct)

        Tabla.Dispose()
        Response.Redirect("RutasPromotorNR.aspx")
    End Sub

    Private Function GuardarProductos(ByVal folio As Integer) As Boolean
        ''//frentes
        Dim IDTipoProducto, IDExhibicion As Integer
        For I As Integer = 0 To gridFrentes.Rows.Count - 1
            IDTipoProducto = gridFrentes.DataKeys(I).Value.ToString()
            Dim txtPropio As TextBox = CType(gridFrentes.Rows(I).FindControl("txtPropio"), TextBox)
            Dim txtCompetencia As TextBox = CType(gridFrentes.Rows(I).FindControl("txtCompetencia"), TextBox)

            If txtPropio.Text = "" Or txtPropio.Text = " " Then
                txtPropio.Text = 0 : End If
            If txtCompetencia.Text = "" Or txtCompetencia.Text = " " Then
                txtCompetencia.Text = 0 : End If

            GuardaFrentes(folio, IDTipoProducto, txtPropio.Text, txtCompetencia.Text)
        Next

        ''//exhibiciones
        For I As Integer = 0 To gridExhibiciones.Rows.Count - 1
            IDExhibicion = gridExhibiciones.DataKeys(I).Value.ToString()
            Dim txtCantidad As TextBox = CType(gridExhibiciones.Rows(I).FindControl("txtCantidad"), TextBox)

            If txtCantidad.Text = "" Or txtCantidad.Text = " " Then
                txtCantidad.Text = 0 : End If

            GuardaExhibiciones(folio, IDExhibicion, txtCantidad.Text)
        Next

    End Function

    Private Function GuardaFrentes(ByVal FolioHistorial As Integer, ByVal IDTipoProducto As Integer, _
                                ByVal Propio As Double, ByVal Competencia As Double) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                                    "select * from NR_Historial_Frentes_Det " & _
                                  "WHERE folio_historial=" & FolioHistorial & " " & _
                                  "AND tipo_producto =" & IDTipoProducto & "")
        If Propio <> 0 Then
            If tabla.Rows.Count > 0 Then
                BD.Execute(ConexionBerol.localSqlServer, _
                           "execute NR_Frentes_EditarHistorial_Det " & FolioHistorial & ",1," & _
                           "" & IDTipoProducto & "," & Propio & "")
            Else
                BD.Execute(ConexionBerol.localSqlServer, _
                           "execute NR_Frentes_CrearHistorial_Det " & FolioHistorial & ",1," & _
                           "" & IDTipoProducto & "," & Propio & "")
            End If
        Else
            If tabla.Rows.Count > 0 Then
                BD.Execute(ConexionBerol.localSqlServer, _
                           "DELETE FROM NR_Historial_Frentes_Det " & _
                           "WHERE folio_historial = " & FolioHistorial & " " & _
                           "AND tipo_producto=" & IDTipoProducto & " AND propia=1")
            End If
        End If

        If Competencia <> 0 Then
            If tabla.Rows.Count > 0 Then
                BD.Execute(ConexionBerol.localSqlServer, _
                           "execute NR_Frentes_EditarHistorial_Det " & FolioHistorial & ",2," & _
                           "" & IDTipoProducto & "," & Competencia & "")
            Else
                BD.Execute(ConexionBerol.localSqlServer, _
                           "execute NR_Frentes_CrearHistorial_Det " & FolioHistorial & ",2," & _
                           "" & IDTipoProducto & "," & Competencia & "")
            End If
        Else
            If tabla.Rows.Count > 0 Then
                BD.Execute(ConexionBerol.localSqlServer, _
                           "DELETE FROM NR_Historial_Frentes_Det " & _
                           "WHERE folio_historial = " & FolioHistorial & " " & _
                           "AND tipo_producto=" & IDTipoProducto & " AND propia=2")
            End If
        End If
    End Function

    Private Function GuardaExhibiciones(ByVal FolioHistorial As Integer, ByVal IDExhibicion As Integer, _
                            ByVal Cantidad As Double) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                               "select * from NR_Historial_Exhibiciones_Det " & _
                                               "WHERE folio_historial=" & FolioHistorial & " " & _
                                               "AND id_exhibicion =" & IDExhibicion & "")
        If Cantidad <> 0 Then
            If Tabla.Rows.Count > 0 Then
                BD.Execute(ConexionBerol.localSqlServer, _
                           "execute NR_Exhibiciones_EditarHistorial_Det " & FolioHistorial & "," & _
                           "'" & IDExhibicion & "'," & Cantidad & "")
            Else
                BD.Execute(ConexionBerol.localSqlServer, _
                           "execute NR_Exhibiciones_CrearHistorial_Det " & FolioHistorial & "," & _
                           "'" & IDExhibicion & "'," & Cantidad & "")
            End If
        Else
            If Tabla.Rows.Count > 0 Then
                BD.Execute(ConexionBerol.localSqlServer, _
                           "DELETE FROM NR_Historial_Exhibiciones_Det " & _
                           "WHERE folio_historial = " & FolioHistorial & " " & _
                           "AND id_exhibicion=" & IDExhibicion & "")
            End If
        End If
    End Function

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("RutasPromotorNR.aspx")
    End Sub

    Private Function CambioEstatus(ByVal folio As Integer) As Boolean
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                               "select * from NR_Historial_Exhibiciones_Det " & _
                                               "WHERE folio_historial =" & folio & "")
        Dim Estatus As Integer
        If Tabla.Rows.Count > 0 Then
            Estatus = 1 : Else
            Estatus = 2 : End If

        BD.Execute(ConexionBerol.localSqlServer, _
                   "UPDATE NR_Rutas_Eventos SET estatus_frentes=" & Estatus & " " & _
                   "FROM NR_Rutas_Eventos " & _
                   "WHERE id_periodo=" & IDPeriodo & " AND id_usuario='" & IDUsuario & "' " & _
                   "AND id_tienda=" & IDTienda & "")
        Tabla.Dispose()
    End Function

    Sub Datos()
        IDUsuario = Request.Params("usuario")
        IDPeriodo = Request.Params("periodo")
        IDTienda = Request.Params("tienda")
        FolioAct = Request.Params("folio")
    End Sub

End Class