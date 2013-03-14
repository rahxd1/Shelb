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

Partial Public Class FormatoCapturaJovy
    Inherits System.Web.UI.Page

    Dim IDTienda, IDUsuario, IDPeriodo, IDRegion, IDCadena, FolioAct As String
    Dim Faltante As String
    Dim Salir As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionJovy.localSqlServer, HttpContext.Current.User.Identity.Name)

            VerDatos()

            Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                                   "select id_tienda, nombre FROM Jovy_Tiendas " & _
                                                   "WHERE id_cadena = " & IDCadena & " " & _
                                                   "AND id_region = " & IDRegion & " " & _
                                                   "AND id_tienda not in (select id_tienda " & _
                                                   "FROM Jovy_Rutas_Eventos " & _
                                                   "WHERE id_cadena = " & IDCadena & " " & _
                                                   "AND id_periodo=" & IDPeriodo & ")")
            If Tabla.Rows.Count <= 0 And IDTienda = "" Then
                Response.Redirect("RutasJovy.aspx")
                Exit Sub
            End If

            tabla.Dispose()

            If IDTienda = "" Then
                Combo.LlenaDrop(ConexionJovy.localSqlServer, "select id_tienda, nombre FROM Jovy_Tiendas WHERE id_cadena = " & IDCadena & " AND id_region = " & IDRegion & " AND id_tienda not in (select id_tienda FROM Jovy_Rutas_Eventos WHERE id_cadena = " & IDCadena & " AND id_periodo=" & IDPeriodo & ")", "nombre", "id_tienda", cmbTienda) : Else
                Combo.LlenaDrop(ConexionJovy.localSqlServer, "select id_tienda, nombre FROM Jovy_Tiendas WHERE id_cadena = " & IDCadena & " AND id_region = " & IDRegion & "", "nombre", "id_tienda", cmbTienda) : End If

            If Edicion = 1 Then
                btnGuardar.Visible = True : Else
                btnGuardar.Visible = False : End If
        End If
    End Sub

    Private Sub VerDatos()
        Datos()

        CargaGrillas(FolioAct, 1, gridProductos1) ''//polvos
        CargaGrillas(FolioAct, 2, gridProductos2) ''//pulpas
        CargaGrillas(FolioAct, 3, gridProductos3) ''//caramelos
        CargaGrillas(FolioAct, 4, gridProductos4) ''//paletas
        CargaGrillas(FolioAct, 5, gridProductos5) ''//rollo de fruta
    End Sub

    Private Function CargaGrillas(ByVal Folio As Integer, ByVal Tipo_producto As Integer, _
                                  ByVal Grilla As GridView) As Boolean
        CargaGrilla(ConexionJovy.localSqlServer, _
                    "SELECT PROD.id_producto, MAR.id_marca, MAR.nombre_marca, PROD.nombre_producto, PROD.bolsa, PROD.codigo, " & _
                    "ISNULL(HDET.precio, '0') as precio, ISNULL(HDET.inventarios, '0') as inventarios,ISNULL(HDET.inventarios_bodega, '0') as inventarios_bodega, ISNULL(HDET.faltante, '0') as faltante, HDET.caducidad,ISNULL(HDET.cantidad_caducada, '0') as cantidad_caducada " & _
                    "FROM Jovy_Productos AS PROD " & _
                    "INNER JOIN Jovy_Marcas AS MAR ON PROD.id_marca = MAR.id_marca " & _
                    "FULL JOIN (SELECT * FROM Jovy_Productos_Historial_Det WHERE folio_historial = '" & Folio & "')AS HDET " & _
                    "ON HDET.id_producto = PROD.id_producto " & _
                    "WHERE PROD.tipo_producto = " & Tipo_producto & " AND PROD.activo=1 " & _
                    "ORDER BY PROD.orden", Grilla)
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        VerificaCantidad(gridProductos1)

        If Salir = 1 Then
            Exit Sub
        Else
            VerificaCantidad(gridProductos2)
            If Salir = 1 Then
                Exit Sub
            Else
                VerificaCantidad(gridProductos3)
                If Salir = 1 Then
                    Exit Sub
                Else
                    VerificaCantidad(gridProductos4)
                    If Salir = 1 Then
                        Exit Sub
                    Else
                        VerificaCantidad(gridProductos5)
                    End If
                End If
            End If
        End If

        Guardar()
    End Sub

    Private Function VerificaCantidad(ByVal Grilla As GridView) As Boolean
        Dim IDProducto As Integer
        Dim FechaCaducidad, FechaProxima As Date
        FechaProxima = DateAdd(DateInterval.Day, 90, Date.Today)

        For i = 0 To CInt(Grilla.Rows.Count) - 1
            IDProducto = Grilla.DataKeys(i).Value.ToString()
            Dim txtCaducidad As TextBox = CType(Grilla.Rows(i).FindControl("txtCaducidad"), TextBox)
            Dim txtcantidad_caducada As TextBox = CType(Grilla.Rows(i).FindControl("txtcantidad_caducada"), TextBox)

            If txtCaducidad.Text <> "" Then
                FechaCaducidad = FormatDateTime(txtCaducidad.Text, DateFormat.ShortDate)

                If FechaCaducidad < FechaProxima Then
                    If txtcantidad_caducada.Text = 0 Then
                        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                                               "select * from View_Jovy_Productos " & _
                                                  "WHERE id_producto =" & IDProducto & "")
                        If Tabla.Rows.Count > 0 Then
                            lblAviso.Text = "Captura la cantidad de caducados de la sección de " + Tabla.Rows(0)("nombre_categoria") + " " & _
                                    " el producto '" + Tabla.Rows(0)("nombre_producto") + "'."

                            Salir = 1

                            Exit Function
                        End If

                        Tabla.Dispose()
                    End If
                End If
            End If
        Next
    End Function

    Sub Guardar()
        lblAviso.Visible = True

        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                               "select * from Jovy_Historial " & _
                                               "WHERE folio_historial=" & FolioAct & "")
        If Tabla.Rows.Count <> 0 Then
            ''//Guarda los productos
            Select Case MultiView1.ActiveViewIndex
                Case 0
                    lblAviso.Text = "La información de 'Polvos' se ha guardado correctamente, continua capturando la información o da click en 'Regresar' para ir al menú de las tiendas"
                    GuardarProductos(FolioAct, gridProductos1)
                Case 1
                    lblAviso.Text = "La información de 'Pulpas' se ha guardado correctamente, continua capturando la información o da click en 'Regresar' para ir al menú de las tiendas"
                    GuardarProductos(FolioAct, gridProductos2)
                Case 2
                    lblAviso.Text = "La información de 'Caramelos' se ha guardado correctamente, continua capturando la información o da click en 'Regresar' para ir al menú de las tiendas"
                    GuardarProductos(FolioAct, gridProductos3)
                Case 3
                    lblAviso.Text = "La información de 'Paletas' se ha guardado correctamente, continua capturando la información o da click en 'Regresar' para ir al menú de las tiendas"
                    GuardarProductos(FolioAct, gridProductos4)
                Case 4
                    lblAviso.Text = "La información de 'Rollo de fruta' se ha guardado correctamente, continua capturando la información o da click en 'Regresar' para ir al menú de las tiendas"
                    GuardarProductos(FolioAct, gridProductos5)
            End Select

        Else
            Dim TablaE As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                                   "select * from Jovy_Rutas_Eventos " & _
                                                   "WHERE id_periodo=" & IDPeriodo & " " & _
                                                   "AND id_usuario='" & IDUsuario & "' " & _
                                                   "AND id_tienda=" & IDTienda & "")
            If TablaE.Rows.Count = 0 Then
                BD.Execute(ConexionJovy.localSqlServer, _
                           "INSERT INTO Jovy_Rutas_Eventos" & _
                           "(id_periodo, id_usuario, id_tienda, id_cadena) " & _
                           "VALUES(" & IDPeriodo & ",'" & IDUsuario & "'," & IDTienda & "," & _
                           "" & IDCadena & ")")
            End If
            TablaE.Dispose()

            FolioAct = BD.RT.Execute(ConexionJovy.localSqlServer, _
                                     "execute CrearHistorial " & IDPeriodo & ",'" & IDUsuario & "'," & IDTienda & "")

            ''//Guarda los productos
            Select Case MultiView1.ActiveViewIndex
                Case 0
                    lblAviso.Text = "La información de 'Polvos' se ha guardado correctamente, continua capturando la información o da click en 'Regresar' para ir al menú de las tiendas"
                    GuardarProductos(FolioAct, gridProductos1)
                Case 1
                    lblAviso.Text = "La información de 'Pulpas' se ha guardado correctamente, continua capturando la información o da click en 'Regresar' para ir al menú de las tiendas"
                    GuardarProductos(FolioAct, gridProductos2)
                Case 2
                    lblAviso.Text = "La información de 'Caramelos' se ha guardado correctamente, continua capturando la información o da click en 'Regresar' para ir al menú de las tiendas"
                    GuardarProductos(FolioAct, gridProductos3)
                Case 3
                    lblAviso.Text = "La información de 'Paletas' se ha guardado correctamente, continua capturando la información o da click en 'Regresar' para ir al menú de las tiendas"
                    GuardarProductos(FolioAct, gridProductos4)
                Case 4
                    lblAviso.Text = "La información de 'Rollo de fruta' se ha guardado correctamente, continua capturando la información o da click en 'Regresar' para ir al menú de las tiendas"
                    GuardarProductos(FolioAct, gridProductos5)
            End Select

        End If

        CambioEstatus(FolioAct)
    End Sub

    Private Function GuardarProductos(ByVal folio As Integer, ByVal Grilla As GridView) As Boolean
        ''//todas las grillas de productos
        Dim IDProducto As Integer
        For I As Integer = 0 To Grilla.Rows.Count - 1
            IDProducto = Grilla.DataKeys(I).Value.ToString()
            Dim txtPrecio As TextBox = CType(Grilla.Rows(I).FindControl("txtPrecio"), TextBox)
            Dim txtInventarios As TextBox = CType(Grilla.Rows(I).FindControl("txtInventarios"), TextBox)
            Dim txtinventarios_bodega As TextBox = CType(Grilla.Rows(I).FindControl("txtinventarios_bodega"), TextBox)
            Dim chkFaltantes As CheckBox = CType(Grilla.Rows(I).FindControl("chkFaltantes"), CheckBox)
            Dim txtCaducidad As TextBox = CType(Grilla.Rows(I).FindControl("txtCaducidad"), TextBox)
            Dim txtcantidad_caducada As TextBox = CType(Grilla.Rows(I).FindControl("txtcantidad_caducada"), TextBox)

            If txtPrecio.Text = "" Or txtPrecio.Text = " " Then
                txtPrecio.Text = 0 : End If

            If txtInventarios.Text = "" Or txtInventarios.Text = " " Then
                txtInventarios.Text = 0 : End If

            If txtinventarios_bodega.Text = "" Or txtinventarios_bodega.Text = " " Then
                txtinventarios_bodega.Text = 0 : End If

            If txtcantidad_caducada.Text = "" Or txtcantidad_caducada.Text = " " Then
                txtcantidad_caducada.Text = 0 : End If

            If chkFaltantes.Checked = True Then
                Faltante = "1" : Else
                Faltante = "0" : End If

            GuardaEditaProducto(folio, IDProducto, txtPrecio.Text, txtInventarios.Text, txtinventarios_bodega.Text, Faltante, txtcantidad_caducada.Text, txtCaducidad.Text)
        Next
    End Function

    Private Function GuardaEditaProducto(ByVal FolioHistorial As Integer, ByVal IDProducto As Integer, _
                                ByVal Precio As Double, ByVal Inventarios As Double, ByVal Inventarios_bodega As Double, ByVal Faltante As Double, ByVal cantidad_caducada As Double, ByVal Caducidad As String) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                                "SELECT * From Jovy_Productos_Historial_Det " & _
                                                "WHERE folio_historial=" & FolioHistorial & " " & _
                                                "AND id_producto=" & IDProducto & " ")
        If Precio <> 0 Or Inventarios <> 0 Or Inventarios_bodega <> 0 Then
            If Tabla.Rows.Count = 1 Then
                Dim cnn As New SqlConnection(ConexionJovy.localSqlServer)
                cnn.Open()

                Dim SQLEdita As New SqlCommand("execute EditarHistorial_DetNuevaVer " & FolioHistorial & ",'" & IDProducto & "'," & Precio & "," & Inventarios & "," & Inventarios_bodega & ", " & Faltante & "," & cantidad_caducada & ", @Caducidad", cnn)

                If Caducidad <> "" Then
                    SQLEdita.Parameters.AddWithValue("@Caducidad", ISODates.Dates.SQLServerDate(CDate(Caducidad)))
                Else
                    SQLEdita.Parameters.AddWithValue("@Caducidad", DBNull.Value)
                    SQLEdita.Parameters.AddWithValue(cantidad_caducada, DBNull.Value)
                End If

                SQLEdita.ExecuteNonQuery()
                SQLEdita.Dispose()

                cnn.Dispose()
                cnn.Close()
            Else
                Dim cnn As New SqlConnection(ConexionJovy.localSqlServer)
                cnn.Open()

                Dim SQLNuevo As New SqlCommand("execute CrearHistorial_DetNuevaVer " & FolioHistorial & ",'" & IDProducto & "'," & Precio & "," & Inventarios & "," & Inventarios_bodega & "," & Faltante & "," & cantidad_caducada & ", @Caducidad", cnn)

                If Caducidad <> "" Then
                    SQLNuevo.Parameters.AddWithValue("@Caducidad", ISODates.Dates.SQLServerDate(CDate(Caducidad)))
                Else
                    SQLNuevo.Parameters.AddWithValue("@Caducidad", DBNull.Value)
                    SQLNuevo.Parameters.AddWithValue(cantidad_caducada, DBNull.Value)
                End If

                SQLNuevo.ExecuteNonQuery()
                SQLNuevo.Dispose()

                cnn.Dispose()
                cnn.Close()
            End If
        Else
            If tabla.Rows.Count = 1 Then
                BD.Execute(ConexionJovy.localSqlServer, _
                           "DELETE FROM Jovy_Productos_Historial_Det " & _
                           "WHERE folio_historial = " & FolioHistorial & " " & _
                           "AND id_producto= '" & IDProducto & "'")
            End If
        End If

        Tabla.Dispose()
    End Function

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("javascript:history.go(-1)")
    End Sub

    Private Function CambioEstatus(ByVal folio As Integer) As Boolean
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                               "select * from Jovy_Productos_Historial_Det " & _
                                               "WHERE folio_historial =" & folio & "")
        Dim Estatus As Integer
        If Tabla.Rows.Count > 0 Then
            Estatus = 1 : Else
            Estatus = 2 : End If
        Tabla.Dispose()

        BD.Execute(ConexionJovy.localSqlServer, _
                   "UPDATE Jovy_Rutas_Eventos SET estatus=" & Estatus & " " & _
                   "FROM Jovy_Rutas_Eventos " & _
                   "WHERE id_periodo=" & IDPeriodo & " AND id_usuario='" & IDUsuario & "' " & _
                   "AND id_tienda=" & IDTienda & "")
    End Function

    Sub Datos()
        IDTienda = Request.Params("tienda")

        If IDTienda = 0 Then
            IDTienda = cmbTienda.SelectedValue
        Else
            cmbTienda.SelectedValue = IDTienda
            cmbTienda.Enabled = False
        End If

        FolioAct = Request.Params("folio")
        IDUsuario = Request.Params("usuario")
        IDPeriodo = Request.Params("periodo")
        IDCadena = Request.Params("cadena")

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionJovy.localSqlServer, _
                                               "select * from Usuarios " & _
                                               "WHERE id_usuario='" & IDUsuario & "' ")
        If Tabla.Rows.Count > 0 Then
            IDRegion = Tabla.Rows(0)("id_region")
        End If
        Tabla.Dispose()
    End Sub

    Protected Sub gridProductos1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridProductos1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(3).Text <> "JOVY" Then
                e.Row.FindControl("txtInventarios").Visible = False
                e.Row.FindControl("txtInventarios_bodega").Visible = False
                e.Row.FindControl("txtCaducidad").Visible = False
                e.Row.FindControl("txtcantidad_caducada").Visible = False
                e.Row.FindControl("lblbolsas1").Visible = False
                e.Row.FindControl("lblbolsas2").Visible = False
                e.Row.FindControl("lblbolsas3").Visible = False
            End If
        End If
    End Sub

    Protected Sub gridProductos2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridProductos2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(3).Text <> "JOVY" Then
                e.Row.FindControl("txtInventarios").Visible = False
                e.Row.FindControl("txtInventarios_bodega").Visible = False
                e.Row.FindControl("txtCaducidad").Visible = False
                e.Row.FindControl("txtcantidad_caducada").Visible = False
                e.Row.FindControl("lblbolsas1").Visible = False
                e.Row.FindControl("lblbolsas2").Visible = False
                e.Row.FindControl("lblbolsas3").Visible = False
            End If
        End If
    End Sub

    Protected Sub gridProductos3_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridProductos3.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(3).Text <> "JOVY" Then
                e.Row.FindControl("txtInventarios").Visible = False
                e.Row.FindControl("txtInventarios_bodega").Visible = False
                e.Row.FindControl("txtCaducidad").Visible = False
                e.Row.FindControl("txtcantidad_caducada").Visible = False
                e.Row.FindControl("lblbolsas1").Visible = False
                e.Row.FindControl("lblbolsas2").Visible = False
                e.Row.FindControl("lblbolsas3").Visible = False
            End If
        End If
    End Sub

    Protected Sub gridProductos4_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridProductos4.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(3).Text <> "JOVY" Then
                e.Row.FindControl("txtInventarios").Visible = False
                e.Row.FindControl("txtInventarios_bodega").Visible = False
                e.Row.FindControl("txtCaducidad").Visible = False
                e.Row.FindControl("txtcantidad_caducada").Visible = False
                e.Row.FindControl("lblbolsas1").Visible = False
                e.Row.FindControl("lblbolsas2").Visible = False
                e.Row.FindControl("lblbolsas3").Visible = False
            End If
        End If
    End Sub

    Protected Sub gridProductos5_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridProductos5.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(3).Text <> "JOVY" Then
                e.Row.FindControl("txtInventarios").Visible = False
                e.Row.FindControl("txtInventarios_bodega").Visible = False
                e.Row.FindControl("txtCaducidad").Visible = False
                e.Row.FindControl("txtcantidad_caducada").Visible = False
                e.Row.FindControl("lblbolsas1").Visible = False
                e.Row.FindControl("lblbolsas2").Visible = False
                e.Row.FindControl("lblbolsas3").Visible = False
            End If
        End If
    End Sub

    Protected Sub Menu1_MenuItemClick(ByVal sender As Object, ByVal e As MenuEventArgs) Handles Menu1.MenuItemClick
        MultiView1.ActiveViewIndex = Int32.Parse(e.Item.Value)
        Dim i, Sel As Integer
        Sel = e.Item.Value

        Menu1.Items(0).ImageUrl = "~/sitios/Jovy/Img/unselectedtabPol.GIF"
        Menu1.Items(1).ImageUrl = "~/sitios/Jovy/Img/unselectedtabPul.GIF"
        Menu1.Items(2).ImageUrl = "~/sitios/Jovy/Img/unselectedtabCar.GIF"
        Menu1.Items(3).ImageUrl = "~/sitios/Jovy/Img/unselectedtabPal.GIF"
        Menu1.Items(4).ImageUrl = "~/sitios/Jovy/Img/unselectedtabRF.GIF"

        For i = 0 To Menu1.Items.Count - 1
            If Sel = 0 Then
                Menu1.Items(0).ImageUrl = "~/sitios/Jovy/Img/selectedtabPol.GIF" : End If
            If Sel = 1 Then
                Menu1.Items(1).ImageUrl = "~/sitios/Jovy/Img/selectedtabPul.GIF" : End If
            If Sel = 2 Then
                Menu1.Items(2).ImageUrl = "~/sitios/Jovy/Img/selectedtabCar.GIF" : End If
            If Sel = 3 Then
                Menu1.Items(3).ImageUrl = "~/sitios/Jovy/Img/selectedtabPal.GIF" : End If
            If Sel = 4 Then
                Menu1.Items(4).ImageUrl = "~/sitios/Jovy/Img/selectedtabRF.GIF" : End If
        Next
    End Sub

End Class