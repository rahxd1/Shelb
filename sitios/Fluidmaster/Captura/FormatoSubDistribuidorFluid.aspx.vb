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

Partial Public Class FormatoSubDistribuidorFluid
    Inherits System.Web.UI.Page

    Dim IDUsuario, IDPeriodo, IDCadena As String
    Dim FolioAct As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionFluidmaster.localSqlServer, HttpContext.Current.User.Identity.Name)

            Combo.LlenaDropSin(ConexionFluidmaster.localSqlServer, "select * from Tipo_Comentarios", "descripcion_comentario", "tipo_comentario", cmbTipoComentario)

            VerDatos()
            Combo.LlenaDropSin(ConexionFluidmaster.localSqlServer, "SELECT DISTINCT ES.id_estado, ES.nombre_estado " & _
                                "FROM CatRutas as RUT " & _
                                "INNER JOIN Estados as ES ON ES.id_estado=RUT.id_estado " & _
                                "WHERE id_usuario='" & IDUsuario & "'", _
                                "nombre_estado", "id_estado", cmbEstado)
            Combo.LlenaDropSin(ConexionFluidmaster.localSqlServer, "SELECT DISTINCT REG.id_region, REG.nombre_region " & _
                                "FROM CatRutas as RUT " & _
                                "INNER JOIN Regiones as REG ON REG.id_region=RUT.id_region " & _
                                "WHERE id_usuario='" & IDUsuario & "'", _
                                "nombre_region", "id_region", cmbRegion)

            If Edicion = 1 Then
                btnGuardar.Visible = True : Else
                btnGuardar.Visible = False : End If
        End If
    End Sub

    Private Sub VerDatos()
        Datos()

        Dim TablaCad As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                                               "select * FROM View_Tiendas " & _
                                               "WHERE id_cadena=" & IDCadena & "")
        If TablaCad.Rows.Count > 0 Then
            lblDistribuidor.Text = TablaCad.Rows(0)("nombre_cadena")
        End If

        TablaCad.Dispose()

        If FolioAct = 0 Then
            txttienda.Enabled = True
        Else
            Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                                                      "select * FROM Subdistribuidor_Historial as H  " & _
                                             "WHERE folio_historial = " & FolioAct & "")
            If Tabla.Rows.Count > 0 Then
                txtTienda.Text = Tabla.Rows(0)("tienda")
                txtDireccion.Text = Tabla.Rows(0)("direccion")
                txtColonia.Text = Tabla.Rows(0)("colonia")
                txtCiudad.Text = Tabla.Rows(0)("ciudad")
                cmbEstado.SelectedValue = Tabla.Rows(0)("id_estado")
                txtCorreo.Text = Tabla.Rows(0)("correo")
                txtLada.Text = Tabla.Rows(0)("lada")
                txtTelefono.Text = Tabla.Rows(0)("telefono")
                txtComentarios.Text = Tabla.Rows(0)("comentarios")
                cmbTipoComentario.SelectedValue = Tabla.Rows(0)("tipo_comentarios")
                cmbRegion.SelectedValue = Tabla.Rows(0)("id_region")

                txtTienda.Enabled = False
            End If

            Tabla.Dispose()
        End If

        TablaCad.Dispose()

        ''//Productos
        CargaGrilla(ConexionFluidmaster.localSqlServer, _
                    "SELECT PROD.id_producto, PROD.nombre_producto, PROD.codigo, PROD.caja_master, " & _
                    "ISNULL(HDET.pedido,0)pedido " & _
                    "FROM Productos AS PROD " & _
                    "FULL JOIN (select * from Subdistribuidor_Productos_Det " & _
                    "WHERE folio_historial=" & FolioAct & ")AS HDET ON HDET.id_producto = PROD.id_producto " & _
                    "WHERE PROD.activo=1 ORDER BY PROD.nombre_producto, PROD.codigo", Me.gridProductos)

        ''//exhibiciones
        CargaGrilla(ConexionFluidmaster.localSqlServer, _
                    "SELECT EXH.id_exhibidor,EXH.nombre_exhibidor,ISNULL(HDET.cantidad,0)cantidad " & _
                    "FROM Exhibidores AS EXH " & _
                    "FULL JOIN (select * from Subdistribuidor_Exhibidores_Det " & _
                    "WHERE folio_historial= " & FolioAct & ")AS HDET ON HDET.id_exhibidor = EXH.id_exhibidor " & _
                    "WHERE EXH.activo=1 ORDER BY EXH.nombre_exhibidor", Me.gridExhibiciones)

        ''//exhibiciones competencia
        CargaGrilla(ConexionFluidmaster.localSqlServer, _
                    "SELECT MAR.id_marca,MAR.nombre_marca,ISNULL(HDET.cantidad,0)cantidad " & _
                    "FROM Marcas AS MAR " & _
                    "FULL JOIN (select * from Subdistribuidor_Exhibidores_Marcas_Det " & _
                    "WHERE folio_historial=" & FolioAct & ")AS HDET ON HDET.id_marca = MAR.id_marca " & _
                    "WHERE MAR.activo=1 ORDER BY MAR.nombre_marca", Me.gridExhibicionesCompetencia)

        ''//material POP
        CargaGrilla(ConexionFluidmaster.localSqlServer, _
                    "SELECT MAR.id_material,MAR.nombre_material,ISNULL(HDET.cantidad,0)cantidad " & _
                    "FROM Material_POP AS MAR " & _
                    "FULL JOIN (select * from Subdistribuidor_MaterialPOP_Det " & _
                    "WHERE folio_historial=" & FolioAct & ")AS HDET ON HDET.id_material = MAR.id_material " & _
                    "WHERE MAR.activo=1 ORDER BY MAR.nombre_material", Me.gridMaterialPOP)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        btnGuardar.Enabled = False

        Guardar()
    End Sub

    Sub Guardar()
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                                                  "select * from Subdistribuidor_Historial " & _
                                                  "WHERE folio_historial=" & FolioAct & "")
        If tabla.Rows.Count > 0 Then
            Dim SQLEditar As New SqlCommand("execute Subdistribuidor_EditarHistorial " & FolioAct & "," & _
                                            "'" & txtTienda.Text & "','" & txtCiudad.Text & "'," & cmbEstado.SelectedValue & ", " & _
                                            "" & cmbRegion.SelectedValue & ", " & _
                                            "'" & txtDireccion.Text & "','" & txtColonia.Text & "','" & txtCorreo.Text & "', " & _
                                            "'" & txtLada.Text & "','" & txtTelefono.Text & "', " & _
                                            "'" & txtComentarios.Text & "', " & cmbTipoComentario.SelectedValue & "")
            SQLEditar.ExecuteNonQuery()
            SQLEditar.Dispose()

            GuardarFormato(FolioAct)
        Else
            Dim SQLNuevo As New SqlCommand("execute Subdistribuidor_CrearHistorial " & IDPeriodo & ",'" & IDUsuario & "'," & IDCadena & ", " & _
                                           "'" & txtTienda.Text & "','" & txtCiudad.Text & "'," & cmbEstado.SelectedValue & ", " & _
                                           "" & cmbRegion.SelectedValue & ", " & _
                                           "'" & txtDireccion.Text & "','" & txtColonia.Text & "','" & txtCorreo.Text & "', " & _
                                           "'" & txtLada.Text & "','" & txtTelefono.Text & "', " & _
                                           "'" & txtComentarios.Text & "', " & cmbTipoComentario.SelectedValue & "")
            FolioAct = Convert.ToInt32(SQLNuevo.ExecuteScalar())
            SQLNuevo.Dispose()

            GuardarFormato(FolioAct)
        End If

        Tabla.Dispose()

        Response.Redirect("RutasFluid.aspx")
    End Sub

    Private Function GuardarFormato(ByVal folio As Integer) As Boolean
        Dim IDProducto, IDMarca, IDExhibidor, IDMaterial As Integer

        ''//gridProductos
        For I As Integer = 0 To gridProductos.Rows.Count - 1
            IDProducto = gridProductos.DataKeys(I).Value.ToString()
            Dim txtPedido As TextBox = CType(gridProductos.Rows(I).FindControl("txtPedido"), TextBox)

            If txtPedido.Text = "" Or txtPedido.Text = " " Then
                txtPedido.Text = 0 : End If

            GuardaProductos(folio, IDProducto, txtPedido.Text)
        Next

        ''//gridExhibiciones
        For I As Integer = 0 To gridExhibiciones.Rows.Count - 1
            IDExhibidor = gridExhibiciones.DataKeys(I).Value.ToString()
            Dim txtCantidad As TextBox = CType(gridExhibiciones.Rows(I).FindControl("txtCantidad"), TextBox)

            If txtCantidad.Text = "" Or txtCantidad.Text = " " Then
                txtCantidad.Text = 0 : End If

            GuardaExhibiciones(folio, IDExhibidor, txtCantidad.Text)
        Next

        'gridExhibicionesCompetencia
        For I As Integer = 0 To gridExhibicionesCompetencia.Rows.Count - 1
            IDMarca = gridExhibicionesCompetencia.DataKeys(I).Value.ToString()
            Dim txtCantidad As TextBox = CType(gridExhibicionesCompetencia.Rows(I).FindControl("txtCantidad"), TextBox)

            If txtCantidad.Text = "" Or txtCantidad.Text = " " Then
                txtCantidad.Text = 0 : End If

            GuardaExhibicionesCompetencia(folio, IDMarca, txtCantidad.Text)
        Next

        'gridMaterialPOP
        For I As Integer = 0 To gridMaterialPOP.Rows.Count - 1
            IDMaterial = gridMaterialPOP.DataKeys(I).Value.ToString()
            Dim txtCantidad As TextBox = CType(gridMaterialPOP.Rows(I).FindControl("txtCantidad"), TextBox)

            If txtCantidad.Text = "" Or txtCantidad.Text = " " Then
                txtCantidad.Text = 0 : End If

            GuardaMateriaPOP(folio, IDMaterial, txtCantidad.Text)
        Next
    End Function

    Private Function GuardaProductos(ByVal FolioHistorial As Integer, ByVal IDProducto As Integer, _
                                     ByVal Pedido As Double) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                                               "select * from Subdistribuidor_Productos_Det " & _
                                  "WHERE folio_historial=" & FolioHistorial & " " & _
                                  "AND id_producto =" & IDProducto & "")
        If Pedido <> 0 Then
            If Tabla.Rows.Count = 0 Then
                BD.Execute(ConexionFluidmaster.localSqlServer, _
                           "INSERT INTO Subdistribuidor_Productos_Det( " & _
                           "folio_historial,id_producto,pedido) " & _
                           "values(" & FolioHistorial & ", " & IDProducto & "," & Pedido & ")")
            Else
                BD.Execute(ConexionFluidmaster.localSqlServer, _
                           "UPDATE Subdistribuidor_Productos_Det " & _
                           "SET pedido =" & Pedido & " " & _
                           "FROM Subdistribuidor_Productos_Det " & _
                           "WHERE folio_historial=" & FolioHistorial & " " & _
                           "AND id_producto=" & IDProducto & " ")
            End If
        Else
            If Tabla.Rows.Count > 0 Then
                BD.Execute(ConexionFluidmaster.localSqlServer, _
                           "DELETE FROM Subdistribuidor_Productos_Det " & _
                           "WHERE folio_historial = " & FolioHistorial & " " & _
                           "AND id_producto=" & IDProducto & "")
            End If
        End If

        Tabla.Dispose()
    End Function

    Private Function GuardaExhibiciones(ByVal FolioHistorial As Integer, _
                                        ByVal IDExhibicion As Integer, ByVal Cantidad As Double) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                                               "select * from Subdistribuidor_Exhibidores_Det " & _
                                  "WHERE folio_historial=" & FolioHistorial & " " & _
                                  "AND id_exhibidor =" & IDExhibicion & "")
        If Cantidad <> 0 Then
            If tabla.Rows.Count = 0 Then
                BD.Execute(ConexionFluidmaster.localSqlServer, _
                           "INSERT INTO Subdistribuidor_Exhibidores_Det( " & _
                           "folio_historial,id_exhibidor,cantidad) " & _
                           "values(" & FolioHistorial & ", " & IDExhibicion & ", " & _
                           "" & Cantidad & ")")
            Else
                BD.Execute(ConexionFluidmaster.localSqlServer, _
                           "UPDATE Subdistribuidor_Exhibidores_Det " & _
                           "SET cantidad =" & Cantidad & " " & _
                           "FROM Subdistribuidor_Exhibidores_Det " & _
                           "WHERE folio_historial=" & FolioHistorial & " " & _
                           "AND id_exhibidor=" & IDExhibicion & " ")
            End If
        Else
            If tabla.Rows.Count > 0 Then
                BD.Execute(ConexionFluidmaster.localSqlServer, _
                           "DELETE FROM Subdistribuidor_Exhibidores_Det " & _
                           "WHERE folio_historial = " & FolioHistorial & " " & _
                           "AND id_exhibidor=" & IDExhibicion & "")
            End If
        End If

        Tabla.Dispose()
    End Function

    Private Function GuardaExhibicionesCompetencia(ByVal FolioHistorial As Integer, ByVal IDMarca As Integer, _
                        ByVal Cantidad As Double) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                                               "select * from Subdistribuidor_Exhibidores_Marcas_Det " & _
                                  "WHERE folio_historial=" & FolioHistorial & " " & _
                                  "AND id_marca =" & IDMarca & "")
        If Cantidad <> 0 Then
            If Tabla.Rows.Count = 0 Then
                BD.Execute(ConexionFluidmaster.localSqlServer, _
                           "INSERT INTO Subdistribuidor_Exhibidores_Marcas_Det( " & _
                           "folio_historial,id_marca,cantidad) " & _
                           "values(" & FolioHistorial & ", " & IDMarca & ", " & _
                           "" & Cantidad & ")")
            Else
                BD.Execute(ConexionFluidmaster.localSqlServer, _
                           "UPDATE Subdistribuidor_Exhibidores_Marcas_Det " & _
                           "SET cantidad =" & Cantidad & " " & _
                           "FROM Subdistribuidor_Exhibidores_Marcas_Det " & _
                           "WHERE folio_historial=" & FolioHistorial & " " & _
                           "AND id_marca=" & IDMarca & " ")
            End If
        Else
            If Tabla.Rows.Count > 0 Then
                BD.Execute(ConexionFluidmaster.localSqlServer, _
                           "DELETE FROM Subdistribuidor_Exhibidores_Marcas_Det " & _
                           "WHERE folio_historial = " & FolioHistorial & " " & _
                           "AND id_marca=" & IDMarca & "")
            End If
        End If

        Tabla.Dispose()
    End Function

    Private Function GuardaMateriaPOP(ByVal FolioHistorial As Integer, ByVal IDMaterial As Integer, _
                        ByVal Cantidad As Double) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                                               "select * from Subdistribuidor_MaterialPOP_Det " & _
                                  "WHERE folio_historial=" & FolioHistorial & " " & _
                                  "AND id_material =" & IDMaterial & "")
        If Cantidad <> 0 Then
            If Tabla.Rows.Count = 0 Then
                BD.Execute(ConexionFluidmaster.localSqlServer, _
                            "INSERT INTO Subdistribuidor_MaterialPOP_Det( " & _
                            "folio_historial,id_material,cantidad) " & _
                            "values(" & FolioHistorial & ", " & IDMaterial & ", " & _
                            "" & Cantidad & ")")
            Else
                BD.Execute(ConexionFluidmaster.localSqlServer, _
                           "UPDATE Subdistribuidor_MaterialPOP_Det " & _
                           "SET cantidad =" & Cantidad & " " & _
                           "FROM Subdistribuidor_MaterialPOP_Det " & _
                           "WHERE folio_historial=" & FolioHistorial & " " & _
                           "AND id_material=" & IDMaterial & " ")
            End If
        Else
            If Tabla.Rows.Count > 0 Then
                BD.Execute(ConexionFluidmaster.localSqlServer, _
                           "DELETE FROM Subdistribuidor_MaterialPOP_Det " & _
                           "WHERE folio_historial = " & FolioHistorial & " " & _
                           "AND id_material=" & IDMaterial & "")
            End If
        End If

        Tabla.Dispose()
    End Function

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("RutasFluid.aspx")
    End Sub

    Sub Datos()
        IDUsuario = Request.Params("usuario")
        IDCadena = Request.Params("cadena")
        IDPeriodo = Request.Params("periodo")
        FolioAct = Request.Params("folio")
    End Sub

End Class