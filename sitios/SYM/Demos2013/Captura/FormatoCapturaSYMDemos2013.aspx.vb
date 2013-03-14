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
Partial Public Class FormatoCapturaSYMDemos2013
    Inherits System.Web.UI.Page

    Dim Salir, Folio As Integer
    Dim IDTienda, IDUsuario, IDPeriodo, NombreCadena, Catalogado, EsDia As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionSYM.localSqlServer, HttpContext.Current.User.Identity.Name)

            VerDatos()

            'If Edicion = 1 Then
            '    btnGuardar.Visible = True : Else
            '    btnGuardar.Visible = False : End If
        End If
    End Sub

    Private Sub VerDatos()
        Datos()

        Dim cnn As New SqlConnection(ConexionSYM.localSqlServer)
        cnn.Open()
        Dim SQL As New SqlCommand("select * FROM SYM_D_Historial_Ventas WHERE folio_historial =" & Folio & "", cnn)
        Dim Tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQL)
        Data.Fill(Tabla)

        If Tabla.Rows.Count > 0 Then

            txtComentarios.Text = Tabla.Rows(0)("comentarios")
          
        End If

        SQL.Dispose()
        Tabla.Dispose()
        Data.Dispose()

        cnn.Close()
        cnn.Dispose()

        CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT PROD.id_producto, PROD.nombre_producto " & _
                    "FROM Productos_Demos AS PROD  " & _
                    "WHERE PROD.activo = 1 ORDER BY PROD.id_producto", gridProductos)

        GrillaVentas(gridViernes, Folio, 1)
        GrillaAbordos(gridViernesAb, Folio, 1)
        GrillaCanjes(gridViernesCanjes, Folio, 1)
        VerificaDias(1, chkViernes, gridViernes, gridViernesAb, gridViernesCanjes)

        GrillaVentas(gridSabado, Folio, 2)
        GrillaAbordos(gridSabadoAb, Folio, 2)
        GrillaCanjes(gridSabadoCanjes, Folio, 2)
        VerificaDias(2, chkSabado, gridSabado, gridSabadoAb, gridSabadoCanjes)

        GrillaVentas(gridDomingo, Folio, 3)
        GrillaAbordos(gridDomingoAb, Folio, 3)
        GrillaCanjes(gridDomingoCanjes, Folio, 3)
        VerificaDias(3, chkDomingo, gridDomingo, gridDomingoAb, gridDomingoCanjes)

    End Sub

    Private Function VerificaDias(ByVal Dia As Integer, ByVal Check As CheckBox, _
                                  ByVal Grilla1 As GridView, ByVal Grilla2 As GridView, ByVal Grilla3 As GridView) As Boolean
        Dim cnn As New SqlConnection(ConexionSYM.localSqlServer)
        cnn.Open()

        Datos()

        Dim SQLDias As New SqlCommand("select DISTINCT id_dia from SYM_D_Historial_Ventas_Detalle  " & _
                                          "WHERE folio_historial=" & Folio & " AND id_dia =" & Dia & "", cnn)
        Dim TablaDias As New DataTable
        Dim DataDias As New SqlDataAdapter(SQLDias)
        DataDias.Fill(TablaDias)

        If TablaDias.Rows.Count <> 0 Then
            Check.Checked = True
            Grilla1.Enabled = True
            Grilla2.Enabled = True
            Grilla3.Enabled = True

        End If

        DataDias.Dispose()
        TablaDias.Dispose()

        cnn.Close()
        cnn.Dispose()
    End Function

    Private Function GrillaVentas(ByVal Grilla As GridView, ByVal Folio As Integer, ByVal Dia As Integer) As Boolean
        CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT PROD.id_producto, PROD.nombre_producto, " & _
                    "ISNULL(HDET.ventas,0)ventas " & _
                    "FROM Productos_Demos AS PROD " & _
                    "FULL JOIN (SELECT * FROM SYM_D_Historial_Ventas_Detalle  " & _
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
                    "FULL JOIN (SELECT * FROM SYM_D_Historial_Abordos_Det " & _
                    "WHERE folio_historial=" & Folio & " and id_dia=" & Dia & ")AS HDET ON HDET.tipo_abordo = PROD.tipo_abordo  " & _
                    "ORDER BY PROD.tipo_abordo", Grilla)
    End Function

    Private Function GrillaCanjes(ByVal Grilla As GridView, ByVal Folio As Integer, ByVal Dia As Integer) As Boolean
        CargaGrilla(ConexionSYM.localSqlServer, _
                   "SELECT PROD.id_canje, PROD.nombre_canje, " & _
                    "ISNULL(HDET.cantidad,0)cantidad " & _
                    "FROM SYM_D_Canjes AS PROD  " & _
                    "FULL JOIN (SELECT * FROM SYM_D_Historial_Canjes_Detalle " & _
                    "WHERE folio_historial=" & Folio & " and id_dia=" & Dia & ")AS HDET ON HDET.id_canje = PROD.id_canje  " & _
                    "ORDER BY PROD.id_canje", Grilla)


                    
    End Function



    Sub VerificaCantidades()
        'Dim Total, Cantidad1, Cantidad2, Cantidad3 As Integer
        'Dim Filas As Integer = gridProductos.Rows.Count

        'For i = 0 To Filas - 1
        '    Dim txtInventario As TextBox = CType(gridProductos.Rows(i).FindControl("txtInventario"), TextBox)
        '    Dim lblProducto As Label = CType(gridProductos.Rows(i).FindControl("lblProducto"), Label)

        '    Dim txtCantidad1 As TextBox = CType(gridViernes.Rows(i).FindControl("txtVentas"), TextBox)
        '    Dim txtCantidad2 As TextBox = CType(gridSabado.Rows(i).FindControl("txtVentas"), TextBox)
        '    Dim txtCantidad3 As TextBox = CType(gridDomingo.Rows(i).FindControl("txtVentas"), TextBox)

        '    Cantidad1 = txtCantidad1.Text
        '    Cantidad2 = txtCantidad2.Text
        '    Cantidad3 = txtCantidad3.Text

        '    Total = Cantidad1 + Cantidad2 + Cantidad3

        '    If Total > txtInventario.Text Then
        '        lblInventarios.Text = "Las ventas superan el inventario incial del producto '" & lblProducto.Text & "'."
        '        Salir = 1
        '        Exit Sub
        '    End If
        'Next i
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        'lblMinimos.Text = ""
        'lblInventarios.Text = ""

        'If chkViernes.Checked = True Or chkSabado.Checked = True Or chkDomingo.Checked = True Then
        '    VerificaCantidades()
        'End If

        Guardar()
    End Sub

    Sub Guardar()
        Datos()

        Dim cnn As New SqlConnection(ConexionSYM.localSqlServer)
        cnn.Open()
        Dim SQL As New SqlCommand("select * from SYM_D_Historial_Ventas WHERE folio_historial = " & Folio & "", cnn)
        Dim Tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQL)
        Data.Fill(Tabla)

        If Tabla.Rows.Count > 0 Then
            Dim SQLEditar As New SqlCommand("execute proc_SYM_D_EditarHistorial " & Folio & ", " & _
                                            "'" & txtComentarios.Text & "'", cnn)
            SQLEditar.ExecuteNonQuery()
            SQLEditar.Dispose()

            'GuardarDetalle(Folio, gridProductos)

            GuardarProductos(Folio, gridViernes, 1)
            GuardarProductos(Folio, gridSabado, 2)
            GuardarProductos(Folio, gridDomingo, 3)

            GuardarAbordos(Folio, gridViernesAb, 1)
            GuardarAbordos(Folio, gridSabadoAb, 2)
            GuardarAbordos(Folio, gridDomingoAb, 3)


            GuardarCanjes(Folio, gridViernesCanjes, 1)
            GuardarCanjes(Folio, gridSabadoCanjes, 2)
            GuardarCanjes(Folio, gridDomingoCanjes, 3)

        Else
            Dim SQLNuevo As New SqlCommand("execute proc_SYM_D_Crear_Historial_Ventas '" & IDUsuario & "'," & IDPeriodo & "," & _
                                           "" & IDTienda & ", " & _
                                           "'" & txtComentarios.Text & "'", cnn)
            Folio = Convert.ToInt32(SQLNuevo.ExecuteScalar())
            SQLNuevo.Dispose()

            'GuardarDetalle(Folio, gridProductos)

            GuardarProductos(Folio, gridViernes, 1)
            GuardarProductos(Folio, gridSabado, 2)
            GuardarProductos(Folio, gridDomingo, 3)

            GuardarAbordos(Folio, gridViernesAb, 1)
            GuardarAbordos(Folio, gridSabadoAb, 2)
            GuardarAbordos(Folio, gridDomingoAb, 3)

            GuardarCanjes(Folio, gridViernesCanjes, 1)
            GuardarCanjes(Folio, gridSabadoCanjes, 2)
            GuardarCanjes(Folio, gridDomingoCanjes, 3)



        End If

        Tabla.Dispose()
        Data.Dispose()
        cnn.Dispose()
        cnn.Close()

        CambioEstatus(Folio)
        Response.Redirect("RutasSYMDemos2013.aspx")
    End Sub

    Private Function GuardarDetalle(ByVal folio As Integer, ByVal Grilla As GridView) As Boolean
        ' ''//PRODUCTOS
        'Dim IDProducto As Integer
        'For i = 0 To Grilla.Rows.Count - 1
        '    IDProducto = Grilla.DataKeys(i).Value.ToString()
        '    Dim txtInventario As TextBox = CType(Grilla.Rows(i).FindControl("txtInventario"), TextBox)
        '    Dim txtPrecio As TextBox = CType(Grilla.Rows(i).FindControl("txtPrecio"), TextBox)

        '    If txtInventario.Text = "" Or txtInventario.Text = " " Then
        '        txtInventario.Text = 0 : End If
        '    If txtPrecio.Text = "" Or txtPrecio.Text = " " Then
        '        txtPrecio.Text = 0 : End If

        '    GuardaDetalle(folio, IDProducto, txtInventario.Text, txtPrecio.Text)
        'Next
    End Function

    Private Function GuardaDetalle(ByVal FolioHistorial As Integer, ByVal IDProducto As Integer, _
                        ByVal InvInicial As Integer, ByVal Precio As Double) As Boolean
        'Dim cnn As New SqlConnection(ConexionSYM.localSqlServer)
        'cnn.Open()

        'Dim SQL As New SqlCommand("select * from Demos_Productos_Historial_Det  " & _
        '                          "WHERE folio_historial = " & FolioHistorial & " " & _
        '                          "AND id_producto =" & IDProducto & "", cnn)
        'Dim Tabla As New DataTable
        'Dim Data As New SqlDataAdapter(SQL)
        'Data.Fill(Tabla)

        'If InvInicial <> 0 Or Precio <> 0 Then
        '    If Tabla.Rows.Count > 0 Then
        '        Dim SQLEdita As New SqlCommand("execute Demos_EditarHistorial_Det " & _
        '                                       "" & FolioHistorial & "," & IDProducto & ", " & _
        '                                       "" & InvInicial & ", " & Precio & "", cnn)
        '        SQLEdita.ExecuteNonQuery()
        '        SQLEdita.Dispose()
        '    Else
        '        Dim SQLNuevo As New SqlCommand("execute Demos_CrearHistorial_Det " & _
        '                                       "" & FolioHistorial & "," & IDProducto & ", " & _
        '                                       "" & InvInicial & ", " & Precio & "", cnn)
        '        SQLNuevo.ExecuteNonQuery()
        '        SQLNuevo.Dispose()
        '    End If
        'Else
        '    Dim SQLEliminar As New SqlCommand("DELETE FROM Demos_Productos_Historial_Det " & _
        '                                      "WHERE folio_historial = " & FolioHistorial & " " & _
        '                                      "AND id_producto=" & IDProducto & "", cnn)
        '    SQLEliminar.ExecuteNonQuery()
        '    SQLEliminar.Dispose()
        'End If

        'cnn.Close()
        'cnn.Dispose()
    End Function


    Private Function GuardarProductos(ByVal folio As Integer, ByVal Grilla As GridView, ByVal Dia As Integer) As Boolean
        ''//PRODUCTOS
        Dim IDProducto As Integer
        For i = 0 To Grilla.Rows.Count - 1
            IDProducto = Grilla.DataKeys(i).Value.ToString()
            Dim txtVentas As TextBox = CType(Grilla.Rows(i).FindControl("txtVentas"), TextBox)

            If txtVentas.Text = "" Or txtVentas.Text = " " Then
                txtVentas.Text = 0 : End If

            GuardaDetalleProductos(folio, IDProducto, txtVentas.Text, Dia)
        Next
    End Function

    Private Function GuardaDetalleProductos(ByVal FolioHistorial As Integer, ByVal IDProducto As Integer, _
                                ByVal Ventas As Double, ByVal Dia As Integer) As Boolean
        Dim cnn As New SqlConnection(ConexionSYM.localSqlServer)
        cnn.Open()

        Dim SQL As New SqlCommand("select * from SYM_D_Historial_Ventas_Detalle " & _
                                  "WHERE folio_historial=" & FolioHistorial & " " & _
                                  "AND id_producto=" & IDProducto & " AND id_dia=" & Dia & "", cnn)
        Dim Tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQL)
        Data.Fill(Tabla)

        If Ventas <> 0 Then

            If Tabla.Rows.Count > 0 Then
                Dim SQLEdita As New SqlCommand("execute proc_SYM_D_EditarHistorial_Ventas_Det " & _
                                               "" & FolioHistorial & "," & IDProducto & ", " & _
                                               "" & Dia & "," & Ventas & "", cnn)
                SQLEdita.ExecuteNonQuery()
                SQLEdita.Dispose()
            Else
                Dim SQLNuevo As New SqlCommand("execute proc_SYM_D_CrearHistorial_Ventas_Det " & _
                                               "" & FolioHistorial & "," & IDProducto & ", " & _
                                               "" & Dia & "," & Ventas & "", cnn)
                SQLNuevo.ExecuteNonQuery()
                SQLNuevo.Dispose()
            End If
        Else
            Dim SQLEliminar As New SqlCommand("DELETE FROM SYM_D_Historial_Ventas_Detalle " & _
                                              "WHERE folio_historial = " & FolioHistorial & " " & _
                                              "AND id_producto=" & IDProducto & " AND id_dia=" & Dia & "", cnn)
            SQLEliminar.ExecuteNonQuery()
            SQLEliminar.Dispose()
        End If

        cnn.Close()
        cnn.Dispose()
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
        Dim cnn As New SqlConnection(ConexionSYM.localSqlServer)
        cnn.Open()

        Dim SQL As New SqlCommand("select * from SYM_D_Historial_Abordos_Det  " & _
                                  "WHERE folio_historial = " & FolioHistorial & " " & _
                                  "AND tipo_abordo =" & TipoAbordo & " AND id_dia=" & Dia & "", cnn)
        Dim Tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQL)
        Data.Fill(Tabla)

        If Cantidad <> 0 Then
            If Tabla.Rows.Count > 0 Then
                Dim SQLEdita As New SqlCommand("execute proc_SYM_D_EditarHistorial_Abordos_Det " & _
                                               "" & FolioHistorial & "," & Dia & ", " & _
                                               "" & TipoAbordo & "," & Cantidad & "", cnn)
                SQLEdita.ExecuteNonQuery()
                SQLEdita.Dispose()
            Else
                Dim SQLNuevo As New SqlCommand("execute proc_SYM_D_CrearHistorial_Abordos_Det " & _
                                               "" & FolioHistorial & "," & Dia & ", " & _
                                               "" & TipoAbordo & "," & Cantidad & "", cnn)
                SQLNuevo.ExecuteNonQuery()
                SQLNuevo.Dispose()
            End If
        Else
            Dim SQLEliminar As New SqlCommand("DELETE FROM SYM_D_Historial_Abordos_Det " & _
                                              "WHERE folio_historial = " & FolioHistorial & " " & _
                                              "AND tipo_abordo=" & TipoAbordo & " AND id_dia=" & Dia & "", cnn)
            SQLEliminar.ExecuteNonQuery()
            SQLEliminar.Dispose()
        End If

        cnn.Close()
        cnn.Dispose()
    End Function

    Private Function GuardarCanjes(ByVal folio As Integer, ByVal Grilla As GridView, ByVal Dia As Integer) As Boolean
        ''//PRODUCTOS
        Dim IdCanje As Integer
        For i = 0 To Grilla.Rows.Count - 1
            IdCanje = Grilla.DataKeys(i).Value.ToString()
            Dim txtCantidad As TextBox = CType(Grilla.Rows(i).FindControl("txtCantidad"), TextBox)

            If txtCantidad.Text = "" Or txtCantidad.Text = " " Then
                txtCantidad.Text = 0 : End If

            GuardaDetalleCanje(folio, IdCanje, txtCantidad.Text, Dia)
        Next
    End Function

    Private Function GuardaDetalleCanje(ByVal FolioHistorial As Integer, ByVal Canje As Integer, _
                            ByVal Cantidad As Integer, ByVal Dia As Integer) As Boolean
        Dim cnn As New SqlConnection(ConexionSYM.localSqlServer)
        cnn.Open()

        Dim SQL As New SqlCommand("select * from SYM_D_Historial_Canjes_Detalle  " & _
                                  "WHERE folio_historial = " & FolioHistorial & " " & _
                                  "AND id_dia=" & Dia & "", cnn)
        Dim Tabla As New DataTable
        Dim Data As New SqlDataAdapter(SQL)
        Data.Fill(Tabla)

        If Cantidad <> 0 Then
            If Tabla.Rows.Count > 0 Then
                Dim SQLEdita As New SqlCommand("execute proc_SYM_D_EditarHistorial_Canjes_Det " & _
                                               "" & FolioHistorial & "," & Dia & ", " & _
                                               "" & Cantidad & ", " & Canje & "", cnn)
                SQLEdita.ExecuteNonQuery()
                SQLEdita.Dispose()
            Else
                Dim SQLNuevo As New SqlCommand("execute proc_SYM_D_CrearHistorial_Canjes_Det " & _
                                               "" & FolioHistorial & "," & Dia & ", " & _
                                               "" & Cantidad & ", " & Canje & "", cnn)
                SQLNuevo.ExecuteNonQuery()
                SQLNuevo.Dispose()
            End If
        Else
            Dim SQLEliminar As New SqlCommand("DELETE FROM SYM_D_Historial_Canjes_Detalle " & _
                                              "WHERE folio_historial = " & FolioHistorial & " " & _
                                              "AND id_dia=" & Dia & "", cnn)
            SQLEliminar.ExecuteNonQuery()
            SQLEliminar.Dispose()
        End If

        cnn.Close()
        cnn.Dispose()
    End Function



    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("RutasSYMDemos2013.aspx")
    End Sub

    Sub Datos()
        Folio = Request.Params("folio")
        IDTienda = Request.Params("tienda")
        IDUsuario = Request.Params("usuario")
        IDPeriodo = Request.Params("periodo")

    End Sub

    Protected Sub chkViernes_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkViernes.CheckedChanged
        If chkViernes.Checked = True Then
            gridViernes.Enabled = True
            gridViernesAb.Enabled = True
            gridViernesCanjes.Enabled = True
        Else
            gridViernes.Enabled = False
            gridViernesAb.Enabled = False
            gridViernesCanjes.Enabled = False

            ''//Deja en blanco
            CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT PROD.id_producto, PROD.nombre_producto, " & _
                     "ISNULL(HDET.ventas,0)ventas " & _
                    "FROM Productos_Demos AS PROD " & _
                    "FULL JOIN (SELECT * FROM SYM_D_Historial_Ventas_Detalle  " & _
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


            CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT PROD.id_canje, PROD.nombre_canje, " & _
                    "ISNULL(HDET.cantidad,0)cantidad " & _
                    "FROM SYM_D_Canjes AS PROD  " & _
                    "FULL JOIN (SELECT * FROM SYM_D_Historial_Canjes_Detalle " & _
                    "WHERE folio_historial=0 and id_dia=1)AS HDET ON HDET.id_canje = PROD.id_canje  " & _
                    "ORDER BY PROD.id_canje", gridViernesCanjes)



        End If
    End Sub

    Private Sub chkSabado_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSabado.CheckedChanged
        If chkSabado.Checked = True Then
            gridSabado.Enabled = True
            gridSabadoAb.Enabled = True
            gridSabadoCanjes.Enabled = True
        Else
            gridSabado.Enabled = False
            gridSabadoAb.Enabled = False
            gridSabadoCanjes.Enabled = False

            ''//Deja en blanco
            CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT PROD.id_producto, PROD.nombre_producto, " & _
                    "ISNULL(HDET.ventas,0)ventas " & _
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


            CargaGrilla(ConexionSYM.localSqlServer, _
                     "SELECT PROD.id_canje, PROD.nombre_canje, " & _
                    "ISNULL(HDET.cantidad,0)cantidad " & _
                    "FROM SYM_D_Canjes AS PROD  " & _
                    "FULL JOIN (SELECT * FROM SYM_D_Historial_Canjes_Detalle " & _
                    "WHERE folio_historial=0 and id_dia=2)AS HDET ON HDET.id_canje = PROD.id_canje  " & _
                    "ORDER BY PROD.id_canje", gridSabadoCanjes)

        End If
    End Sub

    Private Sub chkDomingo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDomingo.CheckedChanged
        If chkDomingo.Checked = True Then
            gridDomingo.Enabled = True
            gridDomingoAb.Enabled = True
            gridDomingoCanjes.Enabled = True
        Else
            gridDomingo.Enabled = False
            gridDomingoAb.Enabled = False
            gridDomingoCanjes.Enabled = False

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


            CargaGrilla(ConexionSYM.localSqlServer, _
                     "SELECT PROD.id_canje, PROD.nombre_canje, " & _
                    "ISNULL(HDET.cantidad,0)cantidad " & _
                    "FROM SYM_D_Canjes AS PROD  " & _
                    "FULL JOIN (SELECT * FROM SYM_D_Historial_Canjes_Detalle " & _
                    "WHERE folio_historial=0 and id_dia=3)AS HDET ON HDET.id_canje = PROD.id_canje  " & _
                    "ORDER BY PROD.id_canje", gridDomingoCanjes)


        End If
    End Sub

    Private Function CambioEstatus(ByVal folio2 As Integer) As Boolean
        Datos()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                             "select * from SYM_D_Historial_Ventas_Detalle " & _
                                               "WHERE folio_historial =" & folio2 & "")
        Dim Estatus As Integer
        If Tabla.Rows.Count > 0 Then
            Estatus = 1 : End If

        Tabla.Dispose()

        BD.Execute(ConexionSYM.localSqlServer, _
                   "UPDATE SYM_D_Rutas_Eventos SET status_ventas=" & Estatus & " " & _
                   "FROM SYM_D_Rutas_Eventos " & _
                   "WHERE id_periodo=" & IDPeriodo & " " & _
                   "AND id_usuario='" & IDUsuario & "' " & _
                   "AND id_tienda=" & IDTienda & "")
    End Function

End Class