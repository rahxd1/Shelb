Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.Page
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports ISODates.Dates
Imports System.Security.Cryptography

Partial Public Class FormatoCapturaSYMDemos
    Inherits System.Web.UI.Page

    Dim Salir, Folio As Integer
    Dim IDCadena, IDUsuario, IDPeriodo, NombreCadena, Catalogado, EsDia As String

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
                                               "select * FROM Demos_Historial WHERE folio_historial =" & Folio & "")
        If Tabla.Rows.Count > 0 Then
            txtTienda.Text = Tabla.Rows(0)("nombre_tienda")
            txtComentarios.Text = Tabla.Rows(0)("comentarios")
            txtNombres.Text = Tabla.Rows(0)("nombre_demo")
            txtApellidoPaterno.Text = Tabla.Rows(0)("apellido_paterno")
            txtApellidoMaterno.Text = Tabla.Rows(0)("apellido_materno")
        End If

        lblCadena.Text = NombreCadena

        Tabla.Dispose()

        CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT PROD.id_producto, PROD.nombre_producto, " & _
                    "ISNULL(HDET.inv_inicial,0)inv_inicial, ISNULL(HDET.precio,0)precio " & _
                    "FROM Productos_Demos AS PROD  " & _
                    "FULL JOIN (SELECT * FROM Demos_Productos_Historial_Det  " & _
                    "WHERE folio_historial =" & Folio & ")AS HDET  " & _
                    "ON HDET.id_producto = PROD.id_producto  " & _
                    "WHERE PROD.activo = 1 ORDER BY PROD.id_producto", gridProductos)

        GrillaVentas(gridViernes, Folio, 1)
        GrillaAbordos(gridViernesAb, Folio, 1)
        VerificaDias(1, chkViernes, gridViernes, gridViernesAb)
        
        GrillaVentas(gridSabado, Folio, 2)
        GrillaAbordos(gridSabadoAb, Folio, 2)
        VerificaDias(2, chkSabado, gridSabado, gridSabadoAb)

        GrillaVentas(gridDomingo, Folio, 3)
        GrillaAbordos(gridDomingoAb, Folio, 3)
        VerificaDias(3, chkDomingo, gridDomingo, gridDomingoAb)

    End Sub

    Private Function VerificaDias(ByVal Dia As Integer, ByVal Check As CheckBox, _
                                  ByVal Grilla1 As GridView, ByVal Grilla2 As GridView) As Boolean
        Dim TablaDias As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "select DISTINCT id_dia from Demos_Productos_Historial_Dias_Det  " & _
                                          "WHERE folio_historial=" & Folio & " AND id_dia =" & Dia & "")
        If TablaDias.Rows.Count <> 0 Then
            Check.Checked = True
            Grilla1.Enabled = True
            Grilla2.Enabled = True
        End If

        TablaDias.Dispose()
    End Function

    Private Function GrillaVentas(ByVal Grilla As GridView, ByVal Folio As Integer, ByVal Dia As Integer) As Boolean
        CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT PROD.id_producto, PROD.nombre_producto, " & _
                    "ISNULL(HDET.agotado,0)agotado, ISNULL(HDET.ventas,0)ventas " & _
                    "FROM Productos_Demos AS PROD " & _
                    "FULL JOIN (SELECT * FROM Demos_Productos_Historial_Dias_Det  " & _
                    "WHERE folio_historial=" & Folio & " and id_dia=" & Dia & ")AS HDET  " & _
                    "ON HDET.id_producto=PROD.id_producto  " & _
                    "WHERE PROD.activo=1 ORDER BY PROD.id_producto", Grilla)
        Grilla.Visible = True
    End Function

    Private Function GrillaAbordos(ByVal Grilla As GridView, ByVal Folio As Integer, ByVal Dia As Integer) As Boolean
        CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT PROD.tipo_abordo, PROD.nombre_abordo, " & _
                    "ISNULL(HDET.cantidad,0)cantidad " & _
                    "FROM Tipo_Abordos_demos AS PROD  " & _
                    "FULL JOIN (SELECT * FROM Demos_Abordos_Historial_Dias_Det " & _
                    "WHERE folio_historial=" & Folio & " and id_dia=" & Dia & ")AS HDET ON HDET.tipo_abordo = PROD.tipo_abordo  " & _
                    "ORDER BY PROD.tipo_abordo", Grilla)
    End Function

    Sub VerificaCantidades()
        Dim Total, Cantidad1, Cantidad2, Cantidad3 As Integer
        Dim Filas As Integer = gridProductos.Rows.Count

        For i = 0 To Filas - 1
            Dim txtInventario As TextBox = CType(gridProductos.Rows(i).FindControl("txtInventario"), TextBox)
            Dim lblProducto As Label = CType(gridProductos.Rows(i).FindControl("lblProducto"), Label)

            Dim txtCantidad1 As TextBox = CType(gridViernes.Rows(i).FindControl("txtVentas"), TextBox)
            Dim txtCantidad2 As TextBox = CType(gridSabado.Rows(i).FindControl("txtVentas"), TextBox)
            Dim txtCantidad3 As TextBox = CType(gridDomingo.Rows(i).FindControl("txtVentas"), TextBox)

            Cantidad1 = txtCantidad1.Text
            Cantidad2 = txtCantidad2.Text
            Cantidad3 = txtCantidad3.Text

            Total = Cantidad1 + Cantidad2 + Cantidad3

            If Total > txtInventario.Text Then
                lblInventarios.Text = "Las ventas superan el inventario incial del producto '" & lblProducto.Text & "'."
                Salir = 1
                Exit Sub
            End If
        Next i
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        lblMinimos.Text = ""
        lblInventarios.Text = ""

        If chkViernes.Checked = True Or chkSabado.Checked = True Or chkDomingo.Checked = True Then
            VerificaCantidades()
        End If

        Guardar()
    End Sub

    Sub Guardar()
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "select * from Demos_Historial WHERE folio_historial = " & Folio & "")
        If Tabla.Rows.Count > 0 Then
            BD.Execute(ConexionSYM.localSqlServer, _
                       "execute Demos_EditarHistorial " & Folio & ", " & _
                       "'" & txtTienda.Text & "','" & txtNombres.Text & "', " & _
                       "'" & txtApellidoPaterno.Text & "', '" & txtApellidoMaterno.Text & "', " & _
                       "'" & txtComentarios.Text & "'")
            GuardarDetalle(Folio, gridProductos)

            GuardarProductos(Folio, gridViernes, 1)
            GuardarProductos(Folio, gridSabado, 2)
            GuardarProductos(Folio, gridDomingo, 3)

            GuardarAbordos(Folio, gridViernesAb, 1)
            GuardarAbordos(Folio, gridSabadoAb, 2)
            GuardarAbordos(Folio, gridDomingoAb, 3)
        Else
            Folio = BD.RT.Execute(ConexionSYM.localSqlServer, _
                      "execute Demos_CrearHistorial '" & IDUsuario & "'," & IDPeriodo & "," & _
                      "" & IDCadena & ",'" & txtTienda.Text & "','" & txtNombres.Text & "', " & _
                      "'" & txtApellidoPaterno.Text & "', '" & txtApellidoMaterno.Text & "', " & _
                      "'" & txtComentarios.Text & "'")
            GuardarDetalle(Folio, gridProductos)

            GuardarProductos(Folio, gridViernes, 1)
            GuardarProductos(Folio, gridSabado, 2)
            GuardarProductos(Folio, gridDomingo, 3)

            GuardarAbordos(Folio, gridViernesAb, 1)
            GuardarAbordos(Folio, gridSabadoAb, 2)
            GuardarAbordos(Folio, gridDomingoAb, 3)
        End If

        Tabla.Dispose()

        Response.Redirect("RutaSYMDemos.aspx")
    End Sub

    Private Function GuardarDetalle(ByVal folio As Integer, ByVal Grilla As GridView) As Boolean
        ''//PRODUCTOS
        Dim IDProducto As Integer
        For i = 0 To Grilla.Rows.Count - 1
            IDProducto = Grilla.DataKeys(i).Value.ToString()
            Dim txtInventario As TextBox = CType(Grilla.Rows(i).FindControl("txtInventario"), TextBox)
            Dim txtPrecio As TextBox = CType(Grilla.Rows(i).FindControl("txtPrecio"), TextBox)

            If txtInventario.Text = "" Or txtInventario.Text = " " Then
                txtInventario.Text = 0 : End If
            If txtPrecio.Text = "" Or txtPrecio.Text = " " Then
                txtPrecio.Text = 0 : End If

            GuardaDetalle(folio, IDProducto, txtInventario.Text, txtPrecio.Text)
        Next
    End Function

    Private Function GuardaDetalle(ByVal FolioHistorial As Integer, ByVal IDProducto As Integer, _
                        ByVal InvInicial As Integer, ByVal Precio As Double) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "select * from Demos_Productos_Historial_Det  " & _
                   "WHERE folio_historial = " & FolioHistorial & " " & _
                   "AND id_producto =" & IDProducto & "")
        If InvInicial <> 0 Or Precio <> 0 Then
            If Tabla.Rows.Count > 0 Then
                BD.Execute(ConexionSYM.localSqlServer, _
                           "execute Demos_EditarHistorial_Det " & _
                           "" & FolioHistorial & "," & IDProducto & ", " & _
                           "" & InvInicial & ", " & Precio & "")
            Else
                BD.Execute(ConexionSYM.localSqlServer, _
                           "execute Demos_CrearHistorial_Det " & _
                           "" & FolioHistorial & "," & IDProducto & ", " & _
                           "" & InvInicial & ", " & Precio & "")
            End If
        Else
            BD.Execute(ConexionSYM.localSqlServer, _
                       "DELETE FROM Demos_Productos_Historial_Det " & _
                       "WHERE folio_historial = " & FolioHistorial & " " & _
                       "AND id_producto=" & IDProducto & "")
        End If
    End Function


    Private Function GuardarProductos(ByVal folio As Integer, ByVal Grilla As GridView, ByVal Dia As Integer) As Boolean
        ''//PRODUCTOS
        Dim IDProducto As Integer
        For i = 0 To Grilla.Rows.Count - 1
            IDProducto = Grilla.DataKeys(i).Value.ToString()
            Dim chkAgotado As CheckBox = CType(Grilla.Rows(i).FindControl("chkAgotado"), CheckBox)
            Dim txtVentas As TextBox = CType(Grilla.Rows(i).FindControl("txtVentas"), TextBox)

            If txtVentas.Text = "" Or txtVentas.Text = " " Then
                txtVentas.Text = 0 : End If

            Dim Agotado As Integer
            If chkAgotado.Checked = True Then
                Agotado = 1 : Else
                Agotado = 0 : End If

            GuardaDetalleProductos(folio, IDProducto, Agotado, txtVentas.Text, Dia)
        Next
    End Function

    Private Function GuardaDetalleProductos(ByVal FolioHistorial As Integer, ByVal IDProducto As Integer, _
                                ByVal Agotado As Integer, ByVal Ventas As Double, ByVal Dia As Integer) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                   "select * from Demos_Productos_Historial_Dias_Det " & _
                   "WHERE folio_historial=" & FolioHistorial & " " & _
                   "AND id_producto=" & IDProducto & " AND id_dia=" & Dia & "")
        If Ventas <> 0 Or Agotado <> 0 Then
            If Ventas <> 0 Then
                Agotado = 0 : End If

            If Tabla.Rows.Count > 0 Then
                BD.Execute(ConexionSYM.localSqlServer, _
                           "execute Demos_EditarHistorial_Dias_Det " & _
                           "" & FolioHistorial & "," & IDProducto & ", " & _
                           "" & Dia & "," & Agotado & "," & Ventas & "")
            Else
                BD.Execute(ConexionSYM.localSqlServer, _
                           "execute Demos_CrearHistorial_Dias_Det " & _
                           "" & FolioHistorial & "," & IDProducto & ", " & _
                           "" & Dia & "," & Agotado & "," & Ventas & "")
            End If
        Else
            BD.Execute(ConexionSYM.localSqlServer, _
                       "DELETE FROM Demos_Productos_Historial_Dias_Det " & _
                       "WHERE folio_historial = " & FolioHistorial & " " & _
                       "AND id_producto=" & IDProducto & " AND id_dia=" & Dia & "")
        End If
    End Function

    Private Function GuardarAbordos(ByVal folio As Integer, ByVal Grilla As GridView, ByVal Dia As Integer) As Boolean
        ''//PRODUCTOS
        Dim TipoAbordo As Integer
        For i = 0 To Grilla.Rows.Count - 1
            TipoAbordo = Grilla.DataKeys(i).Value.ToString()
            Dim txtCantidad As TextBox = CType(Grilla.Rows(i).FindControl("txtCantidad"), TextBox)

            If txtCantidad.Text = "" Or txtCantidad.Text = " " Then
                txtCantidad.Text = 0 : End If

            GuardaDetalleAbordo(folio, TipoAbordo, txtCantidad.Text, Dia)
        Next
    End Function

    Private Function GuardaDetalleAbordo(ByVal FolioHistorial As Integer, ByVal TipoAbordo As Integer, _
                            ByVal Cantidad As Integer, ByVal Dia As Integer) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                   "select * from Demos_Abordos_Historial_Dias_Det  " & _
                   "WHERE folio_historial = " & FolioHistorial & " " & _
                   "AND tipo_abordo =" & TipoAbordo & " AND id_dia=" & Dia & "")
        If Cantidad <> 0 Then
            If Tabla.Rows.Count > 0 Then
                BD.Execute(ConexionSYM.localSqlServer, _
                           "execute Demos_EditarHistorial_Abordos_Det " & _
                           "" & FolioHistorial & "," & Dia & ", " & _
                           "" & TipoAbordo & "," & Cantidad & "")
            Else
                BD.Execute(ConexionSYM.localSqlServer, _
                           "execute Demos_CrearHistorial_Abordos_Det " & _
                           "" & FolioHistorial & "," & Dia & ", " & _
                           "" & TipoAbordo & "," & Cantidad & "")
            End If
        Else
            BD.Execute(ConexionSYM.localSqlServer, _
                       "DELETE FROM Demos_Abordos_Historial_Dias_Det " & _
                       "WHERE folio_historial = " & FolioHistorial & " " & _
                       "AND tipo_abordo=" & TipoAbordo & " AND id_dia=" & Dia & "")
        End If
    End Function

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("RutaSYMDemos.aspx")
    End Sub

    Sub Datos()
        Folio = Request.Params("folio")
        IDCadena = Request.Params("cadena")
        IDUsuario = Request.Params("usuario")
        IDPeriodo = Request.Params("periodo")
        NombreCadena = Request.Params("nombrecadena")
    End Sub

    Protected Sub chkViernes_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkViernes.CheckedChanged
        If chkViernes.Checked = True Then
            gridViernes.Enabled = True
            gridViernesAb.Enabled = True
        Else
            gridViernes.Enabled = False
            gridViernesAb.Enabled = False

            ''//Deja en blanco
            CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT PROD.id_producto, PROD.nombre_producto, " & _
                    "ISNULL(HDET.agotado,0)agotado, ISNULL(HDET.ventas,0)ventas " & _
                    "FROM Productos_Demos AS PROD " & _
                    "FULL JOIN (SELECT * FROM Demos_Productos_Historial_Dias_Det  " & _
                    "WHERE folio_historial=0 and id_dia=1)AS HDET  " & _
                    "ON HDET.id_producto=PROD.id_producto  " & _
                    "WHERE PROD.activo=1 ORDER BY PROD.id_producto", gridViernes)

            CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT PROD.tipo_abordo, PROD.nombre_abordo, " & _
                    "ISNULL(HDET.cantidad,0)cantidad " & _
                    "FROM Tipo_Abordos_demos AS PROD  " & _
                    "FULL JOIN (SELECT * FROM Demos_Abordos_Historial_Dias_Det " & _
                    "WHERE folio_historial=0 and id_dia=1)AS HDET ON HDET.tipo_abordo = PROD.tipo_abordo  " & _
                    "ORDER BY PROD.tipo_abordo", gridViernesAb)
        End If
    End Sub

    Private Sub chkSabado_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSabado.CheckedChanged
        If chkSabado.Checked = True Then
            gridSabado.Enabled = True
            gridSabadoAb.Enabled = True
        Else
            gridSabado.Enabled = False
            gridSabadoAb.Enabled = False

            ''//Deja en blanco
            CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT PROD.id_producto, PROD.nombre_producto, " & _
                    "ISNULL(HDET.agotado,0)agotado, ISNULL(HDET.ventas,0)ventas " & _
                    "FROM Productos_Demos AS PROD " & _
                    "FULL JOIN (SELECT * FROM Demos_Productos_Historial_Dias_Det  " & _
                    "WHERE folio_historial=0 and id_dia=2)AS HDET  " & _
                    "ON HDET.id_producto=PROD.id_producto  " & _
                    "WHERE PROD.activo=1 ORDER BY PROD.id_producto", gridSabado)

            CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT PROD.tipo_abordo, PROD.nombre_abordo, " & _
                    "ISNULL(HDET.cantidad,0)cantidad " & _
                    "FROM Tipo_Abordos_demos AS PROD  " & _
                    "FULL JOIN (SELECT * FROM Demos_Abordos_Historial_Dias_Det " & _
                    "WHERE folio_historial=0 and id_dia=2)AS HDET ON HDET.tipo_abordo = PROD.tipo_abordo  " & _
                    "ORDER BY PROD.tipo_abordo", gridSabadoAb)
        End If
    End Sub

    Private Sub chkDomingo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDomingo.CheckedChanged
        If chkDomingo.Checked = True Then
            gridDomingo.Enabled = True
            gridDomingoAb.Enabled = True
        Else
            gridDomingo.Enabled = False
            gridDomingoAb.Enabled = False

            ''//Deja en blanco
            CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT PROD.id_producto, PROD.nombre_producto, " & _
                    "ISNULL(HDET.agotado,0)agotado, ISNULL(HDET.ventas,0)ventas " & _
                    "FROM Productos_Demos AS PROD " & _
                    "FULL JOIN (SELECT * FROM Demos_Productos_Historial_Dias_Det  " & _
                    "WHERE folio_historial=0 and id_dia=3)AS HDET  " & _
                    "ON HDET.id_producto=PROD.id_producto  " & _
                    "WHERE PROD.activo=1 ORDER BY PROD.id_producto", gridDomingo)

            CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT PROD.tipo_abordo, PROD.nombre_abordo, " & _
                    "ISNULL(HDET.cantidad,0)cantidad " & _
                    "FROM Tipo_Abordos_demos AS PROD  " & _
                    "FULL JOIN (SELECT * FROM Demos_Abordos_Historial_Dias_Det " & _
                    "WHERE folio_historial=0 and id_dia=3)AS HDET ON HDET.tipo_abordo = PROD.tipo_abordo  " & _
                    "ORDER BY PROD.tipo_abordo", gridDomingoAb)
        End If
    End Sub
End Class