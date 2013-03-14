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

Partial Public Class FormatoDistribuidorFluid
    Inherits System.Web.UI.Page

    Dim IDUsuario, IDPeriodo, IDRegion, IDEstado, IDCadena As String
    Dim FolioAct As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionFluidmaster.localSqlServer, HttpContext.Current.User.Identity.Name)

            Combo.LlenaDropSin(ConexionFluidmaster.localSqlServer, "select * from Tipo_Comentarios ORDER BY descripcion_comentario", "descripcion_comentario", "tipo_comentario", cmbTipoComentario)

            VerDatos()

            If Edicion = 1 Then
                btnGuardar.Visible = True : Else
                btnGuardar.Visible = False : End If
        End If
    End Sub

    Private Sub VerDatos()
        Datos()

        Dim TablaCad As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                                               "select * FROM View_Tiendas " & _
                                               "WHERE id_cadena = " & IDCadena & "")
        If TablaCad.Rows.Count > 0 Then
            lblDistribuidor.Text = TablaCad.Rows(0)("nombre_cadena")
        End If

        TablaCad.Dispose()

        If FolioAct = 0 Then
            cmbTienda.Enabled = True

            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, _
                        "select TI.id_tienda,TI.nombre+' ('+TI.ciudad+' - '+TI.nombre_estado+')'as nombre " & _
                        "FROM CatRutas as RUT " & _
                        "INNER JOIN View_Tiendas as TI " & _
                        "ON TI.id_estado=RUT.id_estado AND TI.id_region=RUT.id_region " & _
                        "WHERE id_usuario='" & IDUsuario & "' and id_cadena=" & IDCadena & " " & _
                        "AND id_tienda NOT IN(SELECT id_tienda FROM Distribuidor_Historial WHERE id_periodo=" & IDPeriodo & ")", "nombre", "id_tienda", cmbTienda)

        Else
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, _
                        "select TI.id_tienda,TI.nombre+' ('+TI.ciudad+' - '+TI.nombre_estado+')'as nombre " & _
                        "FROM CatRutas as RUT " & _
                        "INNER JOIN View_Tiendas as TI " & _
                        "ON TI.id_estado=RUT.id_estado AND TI.id_region=RUT.id_region " & _
                        "WHERE id_usuario='" & IDUsuario & "' and id_cadena=" & IDCadena & "", "nombre", "id_tienda", cmbTienda)

            Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                                                      "select * FROM Distribuidor_Historial as H  " & _
                                            "WHERE folio_historial = " & FolioAct & "")
            If Tabla.Rows.Count <> 0 Then
                cmbTienda.SelectedValue = Tabla.Rows(0)("id_tienda")
                txtComentarios.Text = Tabla.Rows(0)("comentarios")
                cmbTipoComentario.Text = Tabla.Rows(0)("tipo_comentarios")
                cmbTienda.Enabled = False
            End If

            Tabla.Dispose()
        End If

        ''//Productos
        CargaGrilla(ConexionFluidmaster.localSqlServer, _
                    "SELECT PROD.id_producto, PROD.nombre_producto, PROD.codigo, PROD.caja_master, " & _
                    "ISNULL(HDET.piso,0)piso ,ISNULL(HDET.bodega,0)bodega,ISNULL(HDET.faltantes,0)faltantes, " & _
                    "ISNULL(HDET.catalogado,0)catalogado " & _
                    "FROM Productos AS PROD " & _
                    "FULL JOIN (select * from Distribuidor_Productos_Det " & _
                    "WHERE folio_historial=" & FolioAct & ")AS HDET ON HDET.id_producto = PROD.id_producto " & _
                    "WHERE PROD.activo=1 ORDER BY PROD.orden", Me.gridProductos)

        ''//frentes
        CargaGrilla(ConexionFluidmaster.localSqlServer, _
                    "SELECT MAR.id_marca,MAR.nombre_marca,ISNULL(HDET.ganchos,0)ganchos ,ISNULL(HDET.charolas,0)charolas " & _
                    "FROM Marcas_Frentes AS MAR " & _
                    "FULL JOIN (select * from Distribuidor_Frentes_Marcas_Det " & _
                    "WHERE folio_historial=" & FolioAct & ")AS HDET ON HDET.id_marca = MAR.id_marca " & _
                    "WHERE MAR.activo=1 ORDER BY MAR.id_marca", Me.gridFrentes)

        ''//exhibiciones
        CargaGrilla(ConexionFluidmaster.localSqlServer, _
                    "SELECT EXH.id_exhibidor,EXH.nombre_exhibidor,ISNULL(HDET.cantidad,0)cantidad " & _
                    "FROM Exhibidores AS EXH " & _
                    "FULL JOIN (select * from Distribuidor_Exhibidores_Det " & _
                    "WHERE folio_historial= " & FolioAct & ")AS HDET ON HDET.id_exhibidor = EXH.id_exhibidor " & _
                    "WHERE EXH.activo=1 ORDER BY EXH.nombre_exhibidor", Me.gridExhibiciones)

        ''//exhibiciones competencia
        CargaGrilla(ConexionFluidmaster.localSqlServer, _
                    "SELECT MAR.id_marca,MAR.nombre_marca,ISNULL(HDET.cantidad,0)cantidad " & _
                    "FROM Marcas AS MAR " & _
                    "FULL JOIN (select * from Distribuidor_Exhibidores_Marcas_Det " & _
                    "WHERE folio_historial=" & FolioAct & ")AS HDET ON HDET.id_marca = MAR.id_marca " & _
                    "WHERE MAR.activo=1 ORDER BY MAR.nombre_marca", Me.gridExhibicionesCompetencia)

        ''//material POP
        CargaGrilla(ConexionFluidmaster.localSqlServer, _
                    "SELECT MAR.id_material,MAR.nombre_material,MAR.maximo,ISNULL(HDET.cantidad,0)cantidad " & _
                    "FROM Material_POP AS MAR " & _
                    "FULL JOIN (select * from Distribuidor_MaterialPOP_Det " & _
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
                                               "select * from Distribuidor_Historial " & _
                                               "WHERE folio_historial=" & FolioAct & "")
        If Tabla.Rows.Count > 0 Then
            BD.Execute(ConexionFluidmaster.localSqlServer, _
                       "execute Distribuidor_EditarHistorial " & FolioAct & "," & _
                       "'" & txtComentarios.Text & "', " & cmbTipoComentario.SelectedValue & "")
            GuardarFormato(FolioAct)
        Else
            FolioAct = BD.RT.Execute(ConexionFluidmaster.localSqlServer, _
                          "execute Distribuidor_CrearHistorial " & IDPeriodo & "," & _
                          "'" & IDUsuario & "'," & cmbTienda.SelectedValue & "," & _
                          "'" & txtComentarios.Text & "', " & cmbTipoComentario.SelectedValue & "")

            GuardarFormato(FolioAct)
        End If

        Tabla.Dispose()

        Response.Redirect("RutasFluid.aspx")
    End Sub

    Private Function GuardarFormato(ByVal folio As Integer) As Boolean
        Dim Catalogado, IDProducto, IDMarca, IDExhibidor, IDMaterial As Integer

        ''//gridProductos
        For I As Integer = 0 To Me.gridProductos.Rows.Count - 1
            IDProducto = Me.gridProductos.DataKeys(I).Value.ToString()
            Dim chkCatalogado As CheckBox = CType(Me.gridProductos.Rows(I).FindControl("chkCatalogado"), CheckBox)
            Dim txtPiso As TextBox = CType(Me.gridProductos.Rows(I).FindControl("txtPiso"), TextBox)
            Dim txtBodega As TextBox = CType(Me.gridProductos.Rows(I).FindControl("txtBodega"), TextBox)

            If txtPiso.Text = "" Or txtPiso.Text = " " Then
                txtPiso.Text = 0 : End If
            If txtBodega.Text = "" Or txtBodega.Text = " " Then
                txtBodega.Text = 0 : End If

            If chkCatalogado.Checked = True Then
                Catalogado = 1 : Else
                Catalogado = 0 : End If

            GuardaProductos(folio, IDProducto, txtPiso.Text, txtBodega.Text, Catalogado)
        Next

        ''//gridFrentes
        For I As Integer = 0 To gridFrentes.Rows.Count - 1
            IDMarca = Me.gridFrentes.DataKeys(I).Value.ToString()
            Dim txtGanchos As TextBox = CType(Me.gridFrentes.Rows(I).FindControl("txtGanchos"), TextBox)
            Dim txtCharolas As TextBox = CType(Me.gridFrentes.Rows(I).FindControl("txtCharolas"), TextBox)

            If txtGanchos.Text = "" Or txtGanchos.Text = " " Then
                txtGanchos.Text = 0 : End If
            If txtCharolas.Text = "" Or txtCharolas.Text = " " Then
                txtCharolas.Text = 0 : End If

            GuardaFrentes(folio, IDMarca, txtGanchos.Text, txtCharolas.Text)
        Next

        ''//gridExhibiciones
        For I As Integer = 0 To Me.gridExhibiciones.Rows.Count - 1
            IDExhibidor = Me.gridExhibiciones.DataKeys(I).Value.ToString()
            Dim txtCantidad As TextBox = CType(Me.gridExhibiciones.Rows(I).FindControl("txtCantidad"), TextBox)

            If txtCantidad.Text = "" Or txtCantidad.Text = " " Then
                txtCantidad.Text = 0 : End If

            GuardaExhibiciones(folio, IDExhibidor, txtCantidad.Text)
        Next

        'gridExhibicionesCompetencia
        For I As Integer = 0 To Me.gridExhibicionesCompetencia.Rows.Count - 1
            IDMarca = Me.gridExhibicionesCompetencia.DataKeys(I).Value.ToString()
            Dim txtCantidad As TextBox = CType(Me.gridExhibicionesCompetencia.Rows(I).FindControl("txtCantidad"), TextBox)

            If txtCantidad.Text = "" Or txtCantidad.Text = " " Then
                txtCantidad.Text = 0 : End If

            GuardaExhibicionesCompetencia(folio, IDMarca, txtCantidad.Text)
        Next

        'gridMaterialPOP
        For I As Integer = 0 To Me.gridMaterialPOP.Rows.Count - 1
            IDMaterial = Me.gridMaterialPOP.DataKeys(I).Value.ToString()
            Dim txtCantidad As TextBox = CType(Me.gridMaterialPOP.Rows(I).FindControl("txtCantidad"), TextBox)

            If txtCantidad.Text = "" Or txtCantidad.Text = " " Then
                txtCantidad.Text = 0 : End If

            GuardaMateriaPOP(folio, IDMaterial, txtCantidad.Text)
        Next
    End Function

    Private Function GuardaProductos(ByVal FolioHistorial As Integer, ByVal IDProducto As Integer, _
                                     ByVal Piso As Double, ByVal Bodega As Double, _
                                     ByVal Catalogado As Integer) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                                               "select * from Distribuidor_Productos_Det " & _
                                               "WHERE folio_historial=" & FolioHistorial & " " & _
                                               "AND id_producto =" & IDProducto & "")

        If Piso <> 0 Or Bodega <> 0 Or Catalogado <> 0 Then
            If Tabla.Rows.Count = 0 Then
                BD.Execute(ConexionFluidmaster.localSqlServer, _
                           "INSERT INTO Distribuidor_Productos_Det( " & _
                            "folio_historial,id_producto,piso,bodega,catalogado) " & _
                            "values(" & FolioHistorial & ", " & IDProducto & ", " & _
                            "" & Piso & ", " & Bodega & ", " & Catalogado & ")")
            Else
                BD.Execute(ConexionFluidmaster.localSqlServer, _
                           "UPDATE Distribuidor_Productos_Det " & _
                           "SET piso =" & Piso & ",bodega=" & Bodega & ",catalogado=" & Catalogado & " " & _
                           "FROM Distribuidor_Productos_Det " & _
                           "WHERE folio_historial=" & FolioHistorial & " " & _
                           "AND id_producto=" & IDProducto & " ")
            End If
        Else
            If Tabla.Rows.Count > 0 Then
                BD.Execute(ConexionFluidmaster.localSqlServer, _
                           "DELETE FROM Distribuidor_Productos_Det " & _
                          "WHERE folio_historial = " & FolioHistorial & " " & _
                          "AND id_producto=" & IDProducto & "")
            End If
        End If

        Tabla.Dispose()
    End Function

    Private Function GuardaFrentes(ByVal FolioHistorial As Integer, ByVal IDMarca As Integer, _
                                   ByVal Ganchos As Double, ByVal Charolas As Double) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                                               "select * from Distribuidor_Frentes_Marcas_Det " & _
                                               "WHERE folio_historial=" & FolioHistorial & " " & _
                                               "AND id_marca =" & IDMarca & "")

        If Ganchos <> 0 And Charolas <> 0 Then
            If tabla.Rows.Count = 0 Then
                BD.Execute(ConexionFluidmaster.localSqlServer, _
                           "INSERT INTO Distribuidor_Frentes_Marcas_Det( " & _
                           "folio_historial,id_marca,ganchos,charolas) " & _
                           "values(" & FolioHistorial & ", " & IDMarca & ", " & _
                           "" & Ganchos & ", " & Charolas & ")")
            Else
                BD.Execute(ConexionFluidmaster.localSqlServer, _
                           "UPDATE Distribuidor_Frentes_Marcas_Det " & _
                           "SET ganchos =" & Ganchos & ",charolas=" & Charolas & " " & _
                           "FROM Distribuidor_Frentes_Marcas_Det " & _
                           "WHERE folio_historial=" & FolioHistorial & " " & _
                           "AND id_marca=" & IDMarca & " ")
            End If
        Else
            If tabla.Rows.Count > 0 Then
                BD.Execute(ConexionFluidmaster.localSqlServer, _
                           "DELETE FROM Distribuidor_Frentes_Marcas_Det " & _
                           "WHERE folio_historial = " & FolioHistorial & " " & _
                           "AND id_marca=" & IDMarca & "")
            End If
        End If

        Tabla.Dispose()
    End Function

    Private Function GuardaExhibiciones(ByVal FolioHistorial As Integer, ByVal IDExhibicion As Integer, _
                            ByVal Cantidad As Double) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                                               "select * from Distribuidor_Exhibidores_Det " & _
                                               "WHERE folio_historial=" & FolioHistorial & " " & _
                                               "AND id_exhibidor =" & IDExhibicion & "")
        If Cantidad <> 0 Then
            If Tabla.Rows.Count = 0 Then
                BD.Execute(ConexionFluidmaster.localSqlServer, _
                           "INSERT INTO Distribuidor_Exhibidores_Det( " & _
                           "folio_historial,id_exhibidor,cantidad) " & _
                           "values(" & FolioHistorial & ", " & IDExhibicion & ", " & _
                           "" & Cantidad & ")")
            Else
                BD.Execute(ConexionFluidmaster.localSqlServer, _
                           "UPDATE Distribuidor_Exhibidores_Det " & _
                           "SET cantidad =" & Cantidad & " " & _
                           "FROM Distribuidor_Exhibidores_Det " & _
                           "WHERE folio_historial=" & FolioHistorial & " " & _
                           "AND id_exhibidor=" & IDExhibicion & " ")
            End If
        Else
            If Tabla.Rows.Count > 0 Then
                BD.Execute(ConexionFluidmaster.localSqlServer, _
                           "DELETE FROM Distribuidor_Exhibidores_Det " & _
                           "WHERE folio_historial = " & FolioHistorial & " " & _
                           "AND id_exhibidor=" & IDExhibicion & "")
            End If
        End If

        Tabla.Dispose()
    End Function

    Private Function GuardaExhibicionesCompetencia(ByVal FolioHistorial As Integer, ByVal IDMarca As Integer, _
                        ByVal Cantidad As Double) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                                               "select * from Distribuidor_Exhibidores_Marcas_Det " & _
                                  "WHERE folio_historial=" & FolioHistorial & " " & _
                                  "AND id_marca =" & IDMarca & "")
        If Cantidad <> 0 Then
            If Tabla.Rows.Count = 0 Then
                BD.Execute(ConexionFluidmaster.localSqlServer, _
                           "INSERT INTO Distribuidor_Exhibidores_Marcas_Det( " & _
                           "folio_historial,id_marca,cantidad) " & _
                           "values(" & FolioHistorial & ", " & IDMarca & ", " & _
                           "" & Cantidad & ")")
            Else
                BD.Execute(ConexionFluidmaster.localSqlServer, _
                           "UPDATE Distribuidor_Exhibidores_Marcas_Det " & _
                           "SET cantidad =" & Cantidad & " " & _
                           "FROM Distribuidor_Exhibidores_Marcas_Det " & _
                           "WHERE folio_historial=" & FolioHistorial & " " & _
                           "AND id_marca=" & IDMarca & " ")
            End If
        Else
            If Tabla.Rows.Count > 0 Then
                BD.Execute(ConexionFluidmaster.localSqlServer, _
                           "DELETE FROM Distribuidor_Exhibidores_Marcas_Det " & _
                           "WHERE folio_historial = " & FolioHistorial & " " & _
                           "AND id_marca=" & IDMarca & "")
            End If
        End If

        Tabla.Dispose()
    End Function

    Private Function GuardaMateriaPOP(ByVal FolioHistorial As Integer, ByVal IDMaterial As Integer, _
                        ByVal Cantidad As Double) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                                               "select * from Distribuidor_MaterialPOP_Det " & _
                                  "WHERE folio_historial=" & FolioHistorial & " " & _
                                  "AND id_material =" & IDMaterial & "")
        If Cantidad <> 0 Then
            If Tabla.Rows.Count = 0 Then
                BD.Execute(ConexionFluidmaster.localSqlServer, _
                           "INSERT INTO Distribuidor_MaterialPOP_Det( " & _
                           "folio_historial,id_material,cantidad) " & _
                           "values(" & FolioHistorial & ", " & IDMaterial & ", " & _
                           "" & Cantidad & ")")
            Else
                BD.Execute(ConexionFluidmaster.localSqlServer, _
                           "UPDATE Distribuidor_MaterialPOP_Det " & _
                           "SET cantidad =" & Cantidad & " " & _
                           "FROM Distribuidor_MaterialPOP_Det " & _
                           "WHERE folio_historial=" & FolioHistorial & " " & _
                           "AND id_material=" & IDMaterial & " ")
            End If
        Else
            If Tabla.Rows.Count > 0 Then
                BD.Execute(ConexionFluidmaster.localSqlServer, _
                           "DELETE FROM Distribuidor_MaterialPOP_Det " & _
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