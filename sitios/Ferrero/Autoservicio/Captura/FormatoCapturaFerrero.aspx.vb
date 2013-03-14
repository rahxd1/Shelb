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

Partial Public Class FormatoCapturaFerrero
    Inherits System.Web.UI.Page

    Dim IDCadena, IDUsuario, IDPeriodo, IDRegion As String
    Dim Faltante, FolioAct As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionFerrero.localSqlServer, HttpContext.Current.User.Identity.Name)

            VerDatos()
            CargaImagenes()

            If Edicion = 1 Then
                btnGuardar.Visible = True : Else
                btnGuardar.Visible = False : End If
        End If
    End Sub

    Private Sub VerDatos()
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                                    "SELECT * FROM AS_Historial " & _
                                                    "WHERE folio_historial =" & FolioAct & " ")
        If Tabla.Rows.Count > 0 Then
            txtComentarios.Text = Tabla.Rows(0)("comentarios")
            txtTienda.Text = Tabla.Rows(0)("nombre_tienda")
        End If

        Tabla.Dispose()

        ''//estuches
        CargaGrilla(ConexionFerrero.localSqlServer, _
                    "SELECT PROD.id_producto, MAR.id_marca, MAR.nombre_marca, PROD.nombre_producto, PROD.presentacion, PROD.gramaje, " & _
                    "isnull(HDET.precio,0) as precio, isnull(HDET.frentes,0) as frentes, isnull(HDET.faltante,0) as faltante " & _
                    "FROM AS_Productos AS PROD " & _
                    "INNER JOIN Marcas AS MAR ON PROD.id_marca = MAR.id_marca " & _
                    "FULL JOIN (select * from AS_Productos_Historial_Det " & _
                    "WHERE folio_historial=" & FolioAct & ")AS HDET " & _
                    "ON HDET.id_producto = PROD.id_producto " & _
                    "WHERE PROD.tipo_producto =1", Me.gridProductos1)

        ''//untables
        CargaGrilla(ConexionFerrero.localSqlServer, _
                    "SELECT PROD.id_producto, MAR.id_marca, MAR.nombre_marca, PROD.nombre_producto, PROD.presentacion, PROD.gramaje, " & _
                    "isnull(HDET.precio,0) as precio, isnull(HDET.frentes,0) as frentes, isnull(HDET.faltante,0) as faltante " & _
                    "FROM AS_Productos AS PROD " & _
                    "INNER JOIN Marcas AS MAR ON PROD.id_marca = MAR.id_marca " & _
                    "FULL JOIN (select * from AS_Productos_Historial_Det " & _
                    "WHERE folio_historial=" & FolioAct & ")AS HDET " & _
                    "ON HDET.id_producto = PROD.id_producto " & _
                    "WHERE PROD.tipo_producto =2", Me.gridProductos2)

        ''//tabletas
        CargaGrilla(ConexionFerrero.localSqlServer, _
                    "SELECT PROD.id_producto, MAR.id_marca, MAR.nombre_marca, PROD.nombre_producto, PROD.presentacion, PROD.gramaje, " & _
                    "isnull(HDET.precio,0) as precio, isnull(HDET.frentes,0) as frentes, isnull(HDET.faltante,0) as faltante " & _
                    "FROM AS_Productos AS PROD " & _
                    "INNER JOIN Marcas AS MAR ON PROD.id_marca = MAR.id_marca " & _
                    "FULL JOIN (select * from AS_Productos_Historial_Det " & _
                    "WHERE folio_historial=" & FolioAct & ")AS HDET " & _
                    "ON HDET.id_producto = PROD.id_producto " & _
                    "WHERE PROD.tipo_producto =3", Me.gridProductos3)

        ''//check out
        CargaGrilla(ConexionFerrero.localSqlServer, _
                    "SELECT PROD.id_producto, MAR.id_marca, MAR.nombre_marca, PROD.nombre_producto, PROD.presentacion, PROD.gramaje, " & _
                    "isnull(HDET.precio,0) as precio, isnull(HDET.frentes,0) as frentes, isnull(HDET.faltante,0) as faltante " & _
                    "FROM AS_Productos AS PROD " & _
                    "INNER JOIN Marcas AS MAR ON PROD.id_marca = MAR.id_marca " & _
                    "FULL JOIN (select * from AS_Productos_Historial_Det " & _
                    "WHERE folio_historial=" & FolioAct & ")AS HDET " & _
                    "ON HDET.id_producto = PROD.id_producto " & _
                    "WHERE PROD.tipo_producto =4", Me.gridProductos4)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        btnGuardar.Enabled = False

        Guardar()
    End Sub

    Sub Guardar()
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                               "select * from AS_Historial " & _
                                               "WHERE folio_historial=" & FolioAct & "")
        If Tabla.Rows.Count > 0 Then
            BD.Execute(ConexionFerrero.localSqlServer, _
                       "execute AS_EditarHistorial " & FolioAct & ",'" & IDPeriodo & "'," & _
                       "'" & IDUsuario & "','" & IDCadena & "','" & txtTienda.Text & " '," & _
                       "'" & txtComentarios.Text & "'")

            GuardarProductos(FolioAct)
            CambioEstatus(FolioAct)
        Else
            FolioAct = BD.RT.Execute(ConexionFerrero.localSqlServer, _
                                     "execute AS_CrearHistorial '" & IDPeriodo & "'," & _
                                     "'" & IDUsuario & "','" & IDCadena & "'," & _
                                     "'" & txtTienda.Text & "','" & txtComentarios.Text & "'")
            GuardarProductos(FolioAct)
            CambioEstatus(FolioAct)
        End If

        Tabla.Dispose()

        Response.Redirect("RutasFerrero.aspx")
    End Sub

    Private Function GuardarProductos(ByVal folio As Integer) As Boolean
        ''//estuches
        Dim IDProducto As Integer
        For I As Integer = 0 To gridProductos1.Rows.Count - 1
            IDProducto = gridProductos1.DataKeys(I).Value.ToString()
            Dim txtPrecio As TextBox = CType(gridProductos1.Rows(I).FindControl("txtPrecio"), TextBox)
            Dim txtFrentes As TextBox = CType(gridProductos1.Rows(I).FindControl("txtFrentes"), TextBox)
            Dim chkFaltantes As CheckBox = CType(gridProductos1.Rows(I).FindControl("chkFaltantes"), CheckBox)

            If txtPrecio.Text = "" Or txtPrecio.Text = " " Then
                txtPrecio.Text = 0 : End If
            If txtFrentes.Text = "" Or txtFrentes.Text = " " Then
                txtFrentes.Text = 0 : End If
            If chkFaltantes.Checked = True Then
                Faltante = "1" : Else
                Faltante = "0" : End If

            fnGrabaDet(folio, IDProducto, txtPrecio.Text, txtFrentes.Text, Faltante)
        Next

        ''//untables
        For I As Integer = 0 To gridProductos2.Rows.Count - 1
            IDProducto = gridProductos2.DataKeys(I).Value.ToString()
            Dim txtPrecio As TextBox = CType(gridProductos2.Rows(I).FindControl("txtPrecio"), TextBox)
            Dim txtFrentes As TextBox = CType(gridProductos2.Rows(I).FindControl("txtFrentes"), TextBox)
            Dim chkFaltantes As CheckBox = CType(gridProductos2.Rows(I).FindControl("chkFaltantes"), CheckBox)

            If txtPrecio.Text = "" Or txtPrecio.Text = " " Then
                txtPrecio.Text = 0 : End If
            If txtFrentes.Text = "" Or txtFrentes.Text = " " Then
                txtFrentes.Text = 0 : End If
            If chkFaltantes.Checked = True Then
                Faltante = "1" : Else
                Faltante = "0" : End If

            fnGrabaDet(folio, IDProducto, txtPrecio.Text, txtFrentes.Text, Faltante)
        Next

        ''//tabletas
        For I As Integer = 0 To gridProductos3.Rows.Count - 1
            IDProducto = gridProductos3.DataKeys(I).Value.ToString()
            Dim txtPrecio As TextBox = CType(gridProductos3.Rows(I).FindControl("txtPrecio"), TextBox)
            Dim txtFrentes As TextBox = CType(gridProductos3.Rows(I).FindControl("txtFrentes"), TextBox)
            Dim chkFaltantes As CheckBox = CType(gridProductos3.Rows(I).FindControl("chkFaltantes"), CheckBox)

            If txtPrecio.Text = "" Or txtPrecio.Text = " " Then
                txtPrecio.Text = 0 : End If
            If txtFrentes.Text = "" Or txtFrentes.Text = " " Then
                txtFrentes.Text = 0 : End If
            If chkFaltantes.Checked = True Then
                Faltante = "1" : Else
                Faltante = "0" : End If

            fnGrabaDet(folio, IDProducto, txtPrecio.Text, txtFrentes.Text, Faltante)
        Next

        ''//check out
        For I As Integer = 0 To gridProductos4.Rows.Count - 1
            IDProducto = gridProductos4.DataKeys(I).Value.ToString()
            Dim txtPrecio As TextBox = CType(gridProductos4.Rows(I).FindControl("txtPrecio"), TextBox)
            Dim txtFrentes As TextBox = CType(gridProductos4.Rows(I).FindControl("txtFrentes"), TextBox)
            Dim chkFaltantes As CheckBox = CType(gridProductos4.Rows(I).FindControl("chkFaltantes"), CheckBox)

            If txtPrecio.Text = "" Then
                txtPrecio.Text = 0 : End If
            If txtFrentes.Text = "" Then
                txtFrentes.Text = 0 : End If
            If chkFaltantes.Checked = True Then
                Faltante = "1"
            Else
                Faltante = "0"
            End If

            fnGrabaDet(folio, IDProducto, txtPrecio.Text, txtFrentes.Text, Faltante)
        Next

    End Function

    Private Function fnGrabaDet(ByVal FolioHistorial As Integer, ByVal IDProducto As Integer, _
                                ByVal Precio As Double, ByVal Frentes As Double, ByVal Faltante As Double) As Boolean

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                               "select * from AS_Productos_Historial_Det " & _
                                               "WHERE folio_historial=" & FolioHistorial & " " & _
                                               "AND id_producto =" & IDProducto & "")
        If Precio <> 0 Or Frentes <> 0 Then
            If Tabla.Rows.Count > 0 Then
                BD.Execute(ConexionFerrero.localSqlServer, _
                           "execute AS_EditarHistorial_Det " & FolioHistorial & "," & _
                           "'" & IDProducto & "'," & Precio & "," & Frentes & ", " & Faltante & "")
            Else
                BD.Execute(ConexionFerrero.localSqlServer, _
                           "execute AS_CrearHistorial_Det " & FolioHistorial & "," & _
                           "'" & IDProducto & "'," & Precio & "," & Frentes & "," & Faltante & "")
            End If
        Else
            If Tabla.Rows.Count > 0 Then
                BD.Execute(ConexionFerrero.localSqlServer, _
                           "DELETE FROM AS_Productos_Historial_Det " & _
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

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                               "select * from AS_Productos_Historial_Det " & _
                                               "WHERE folio_historial =" & folio & "")
        Dim Estatus As Integer
        If Tabla.Rows.Count > 0 Then
            Estatus = 1 : Else
            Estatus = 2 : End If

        Tabla.Dispose()

        BD.Execute(ConexionFerrero.localSqlServer, _
                   "UPDATE AS_Rutas_Eventos SET estatus=" & Estatus & " " & _
                   "FROM AS_Rutas_Eventos " & _
                   "WHERE id_periodo=" & IDPeriodo & " AND id_usuario='" & IDUsuario & "' " & _
                   "AND id_cadena=" & IDCadena & "")
    End Function

    Sub Datos()
        IDUsuario = Request.Params("usuario")
        IDPeriodo = Request.Params("periodo")
        IDCadena = Request.Params("cadena")
        FolioAct = Request.Params("folio")
    End Sub

    Protected Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridProductos1.Columns(0).Visible = False
        gridProductos2.Columns(0).Visible = False
        gridProductos3.Columns(0).Visible = False
        gridProductos4.Columns(0).Visible = False
    End Sub

    Protected Sub gridProductos1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridProductos1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            For i = 0 To CInt(gridProductos1.Rows.Count) - 0
                If e.Row.Cells(0).Text <> 1 Then
                    e.Row.FindControl("chkFaltantes").Visible = False
                End If
            Next i
        End If
    End Sub

    Protected Sub gridProductos2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridProductos2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            For i = 0 To CInt(gridProductos2.Rows.Count) - 0
                If e.Row.Cells(0).Text <> 1 Then
                    e.Row.FindControl("chkFaltantes").Visible = False
                End If
            Next i
        End If
    End Sub

    Protected Sub gridProductos3_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridProductos3.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            For i = 0 To CInt(gridProductos3.Rows.Count) - 0
                If e.Row.Cells(0).Text <> 1 Then
                    e.Row.FindControl("chkFaltantes").Visible = False
                End If
            Next i
        End If
    End Sub

    Protected Sub gridProductos4_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridProductos4.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            For i = 0 To CInt(gridProductos4.Rows.Count) - 0
                If e.Row.Cells(0).Text <> 1 Then
                    e.Row.FindControl("chkFaltantes").Visible = False
                End If
            Next i
        End If
    End Sub

    Protected Sub btnSubir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubir.Click
        Dim NombreFoto As String

        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                               "SELECT * From AS_Galeria_Historial_Det " & _
                                               "WHERE id_periodo=" & IDPeriodo & " " & _
                                               "AND id_cadena=" & IDCadena & " ORDER BY no_foto DESC")
        Dim NoFoto As String
        If tabla.Rows.Count > 0 Then
            NoFoto = tabla.Rows(0)("no_foto") + 1
        Else
            NoFoto = "1"
        End If

        Tabla.Dispose()

        NombreFoto = (IDPeriodo + "-" + IDCadena + "-" + NoFoto + ".JPG")
        BD.Execute(ConexionFerrero.localSqlServer, _
                   "INSERT INTO AS_Galeria_Historial_Det " & _
                   "(id_periodo,id_usuario, id_cadena, descripcion, ruta, foto, no_foto, nombre_tienda) " & _
                   "VALUES(" & IDPeriodo & ",'" & IDUsuario & "'," & IDCadena & ", " & _
                   "'" & cmbDescripcion.SelectedValue & "','/ARCHIVOS/CLIENTES/FERRERO/IMAGENES/', " & _
                   "'" & NombreFoto & "'," & NoFoto & ",'" & txtTienda.Text & "')")

        Dim fileExt As String
        fileExt = System.IO.Path.GetExtension(FileUpload1.FileName)

        If (fileExt = ".exe") Then
            lblSubida.Text = "No puedes subir archivos .exe"
        Else
            If FileUpload1.HasFile = True Then
                Dim path As String = Server.MapPath("~/ARCHIVOS/CLIENTES/FERRERO/IMAGENES/")
                FileUpload1.PostedFile.SaveAs(path & NombreFoto)
                lblSubida.Text = "El archivo fue subido correctamente"
            End If
        End If

        CargaImagenes()
    End Sub

    Sub CargaImagenes()
        Datos()

        CargaGrilla(ConexionFerrero.localSqlServer, _
                    "SELECT * FROM AS_Galeria_Historial_Det WHERE id_usuario='" & IDUsuario & "' " & _
                    "AND id_periodo='" & IDPeriodo & "' AND id_cadena='" & IDCadena & "' " & _
                    "ORDER BY no_foto", Me.gridImagenes)
    End Sub

    Private Sub gridImagenes_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridImagenes.RowDeleting
        BD.Execute(ConexionFerrero.localSqlServer, _
                   "DELETE FROM AS_Galeria_Historial_Det " & _
                   "WHERE folio_foto = " & gridImagenes.DataKeys(e.RowIndex).Value.ToString() & "")

        CargaImagenes()
    End Sub

    Protected Sub lnkProd1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkProd1.Click
        pnl1.Visible = True
        pnl2.Visible = False
        pnl3.Visible = False
        pnl4.Visible = False
    End Sub

    Protected Sub lnkProd2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkProd2.Click
        pnl1.Visible = False
        pnl2.Visible = True
        pnl3.Visible = False
        pnl4.Visible = False
    End Sub

    Protected Sub lnkProd3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkProd3.Click
        pnl1.Visible = False
        pnl2.Visible = False
        pnl3.Visible = True
        pnl4.Visible = False
    End Sub

    Protected Sub lnkProd4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkProd4.Click
        pnl1.Visible = False
        pnl2.Visible = False
        pnl3.Visible = False
        pnl4.Visible = True
    End Sub
End Class