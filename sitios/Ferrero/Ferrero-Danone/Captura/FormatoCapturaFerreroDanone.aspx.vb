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

Partial Public Class FormatoCapturaFerreroDanone
    Inherits System.Web.UI.Page

    Dim IDUsuario, IDPeriodo, Faltante, Catalogado As String
    Dim FolioAct As Integer

    Sub Datos()
        PeriodoActivo()
        IDUsuario = HttpContext.Current.User.Identity.Name

        FolioAct = Request.Params("folio")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionFerrero.localSqlServer, HttpContext.Current.User.Identity.Name)

            Combo.LlenaDrop(ConexionFerrero.localSqlServer, "Select * from Colonias_Leon order by nombre_colonia", "nombre_colonia", "id_colonia", cmbColonia)
            VerDatos()
            CargaImagenes()

            If Edicion = 1 Then
                btnGuardar.Visible = True:Else
                btnGuardar.Visible = False : End If
        End If
    End Sub

    Private Sub VerDatos()
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                               "select * from Danone_Historial " & _
                                               "WHERE folio_historial =" & FolioAct & "")
        If Tabla.Rows.Count > 0 Then
            txtTienda.Text = Tabla.Rows(0)("nombre_tienda")
            txtDireccion.Text = Tabla.Rows(0)("direccion")
            cmbColonia.SelectedValue = Tabla.Rows(0)("colonia")
        End If

        ''//Encuesta
        Dim Tabla2 As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                               "select * from Danone_Encuesta " & _
                                               "WHERE folio_historial = " & FolioAct & "")
        If Tabla2.Rows.Count > 0 Then
            cmb1.SelectedValue = Tabla2.Rows(0)("1")
            cmb2.SelectedValue = Tabla2.Rows(0)("2")
            If ch2A.Text = Tabla2.Rows(0)("2A") Then
                ch2A.Checked = True : Else
                ch2A.Checked = False : End If
            If ch2B.Text = Tabla2.Rows(0)("2B") Then
                ch2B.Checked = True : Else
                ch2B.Checked = False : End If
            If ch2C.Text = Tabla2.Rows(0)("2C") Then
                ch2C.Checked = True : Else
                ch2C.Checked = False : End If
            cmb3.SelectedValue = Tabla2.Rows(0)("3")
            If ch3A.Text = Tabla2.Rows(0)("3A") Then
                ch3A.Checked = True : Else
                ch3A.Checked = False : End If
            If ch3B.Text = Tabla2.Rows(0)("3B") Then
                ch3B.Checked = True : Else
                ch3B.Checked = False : End If
            If ch3C.Text = Tabla2.Rows(0)("3C") Then
                ch3C.Checked = True : Else
                ch3C.Checked = False : End If
            cmb4.SelectedValue = Tabla2.Rows(0)("4")
            cmb5.SelectedValue = Tabla2.Rows(0)("5")
        End If

        Tabla.Dispose()
        Tabla2.Dispose()

        CargaGrilla(ConexionFerrero.localSqlServer, _
                    "SELECT PROD.id_producto, PROD.nombre_producto, " & _
                    "ISNULL(HDET.Catalogado, '0') as Catalogado, ISNULL(HDET.Faltante, '0') as Faltante, HDET.caducidad " & _
                    "FROM Danone_Productos AS PROD " & _
                    "FULL JOIN (SELECT * FROM Danone_Productos_Historial_Det WHERE folio_historial = '" & FolioAct & "')AS HDET " & _
                    "ON HDET.id_producto = PROD.id_producto " & _
                    "WHERE PROD.activo = 1", Me.gridProductos1)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        btnGuardar.Enabled = False

        Guardar()
    End Sub

    Sub Guardar()
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                               "select * from Danone_Historial " & _
                                               "WHERE folio_historial =" & FolioAct & " ")
        If Tabla.Rows.Count = 0 Then
            BD.Execute(ConexionFerrero.localSqlServer, _
                       "execute Danone_CrearHistorial '" & IDPeriodo & "','" & IDUsuario & "'," & _
                       "'" & txtTienda.Text & "','" & txtDireccion.Text & "'," & _
                       "'" & cmbColonia.SelectedValue & "'")

            ''//Guarda los productos
            GuardarProductos(FolioAct)
            GuardaEncuesta()
        Else
            FolioAct = Tabla.Rows(0)("folio_historial")

            BD.Execute(ConexionFerrero.localSqlServer, _
                       "execute Danone_EditarHistorial " & FolioAct & ",'" & IDPeriodo & "'," & _
                       "'" & IDUsuario & "','" & txtTienda.Text & "','" & txtDireccion.Text & "'," & _
                       "'" & cmbColonia.SelectedValue & "'")

            ''//Guarda los productos
            GuardarProductos(FolioAct)
            GuardaEncuesta()
        End If

        Tabla.Dispose()

        Response.Redirect("RutasFerreroDanone.aspx")
    End Sub

    Sub PeriodoActivo()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                               "SELECT * FROM Danone_Periodos " & _
                                               "WHERE fecha_cierre>='" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "'")
        If Tabla.Rows.Count > 0 Then
            IDPeriodo = Tabla.Rows(0)("id_periodo"):Else
            Response.Redirect("RutasFerreroDanone.aspx") : End If

        Tabla.Dispose()
    End Sub

    Private Function GuardarProductos(ByVal folio As Integer) As Boolean
        ''//estuches
        Dim IDProducto As Integer
        For I As Integer = 0 To gridProductos1.Rows.Count - 1
            IDProducto = gridProductos1.DataKeys(I).Value.ToString()
            Dim txtCaducidad As TextBox = CType(gridProductos1.Rows(I).FindControl("txtCaducidad"), TextBox)
            Dim chkFaltantes As CheckBox = CType(gridProductos1.Rows(I).FindControl("chkFaltantes"), CheckBox)
            Dim chkCatalogado As CheckBox = CType(gridProductos1.Rows(I).FindControl("chkCatalogado"), CheckBox)

            If txtCaducidad.Text = "" Or txtCaducidad.Text = " " Then
                txtCaducidad.Text = 0 : End If

            If chkFaltantes.Checked = True Then
                Faltante = "1" : Else
                Faltante = "0" : End If

            If chkCatalogado.Checked = True Then
                Catalogado = "1" : Else
                Catalogado = "0" : End If

            If Catalogado = 1 Then
                fnGrabaDet(folio, IDProducto, txtCaducidad.Text, Catalogado, Faltante)
            Else
                BD.Execute(ConexionFerrero.localSqlServer, _
                           "DELETE FROM Danone_Productos_Historial_Det " & _
                           "WHERE folio_historial = " & FolioAct & " " & _
                           "AND id_producto= '" & IDProducto & "'")
            End If
        Next
    End Function

    Private Function fnGrabaDet(ByVal FolioHistorial As Integer, ByVal IDProducto As Integer, _
                                ByVal Caducidad As String, ByVal Catalogado As Double, ByVal Faltante As Double) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                               "SELECT * From Danone_Productos_Historial_Det " & _
                                               "WHERE folio_historial=" & FolioHistorial & " " & _
                                               "AND id_producto=" & IDProducto & " ")
        If Tabla.Rows.Count = 1 Then
            Dim cnn As New SqlConnection(ConexionBerol.localSqlServer)
            cnn.Open()

            Dim SQLEdita As New SqlCommand("execute Danone_EditarHistorial_Det " & FolioAct & "," & _
                                           "" & IDProducto & ", " & Faltante & "," & Catalogado & "," & _
                                           "@Caducidad ", cnn)

            If Caducidad <> "" Then
                SQLEdita.Parameters.AddWithValue("@Caducidad", ISODates.Dates.SQLServerDate(CDate(Caducidad)))
            Else
                SQLEdita.Parameters.AddWithValue("@Caducidad", DBNull.Value)
            End If

            SQLEdita.ExecuteNonQuery()
            SQLEdita.Dispose()
            cnn.Dispose()
            cnn.Close()
        Else
            Dim cnn As New SqlConnection(ConexionBerol.localSqlServer)
            cnn.Open()

            Dim SQLNuevo As New SqlCommand("execute Danone_CrearHistorial_Det " & FolioAct & ", " & _
                                           "" & IDProducto & ", @Caducidad, " & Faltante & "," & _
                                           "" & Catalogado & "", cnn)

            If Caducidad <> "" Then
                SQLNuevo.Parameters.AddWithValue("@Caducidad", ISODates.Dates.SQLServerDate(CDate(Caducidad)))
            Else
                SQLNuevo.Parameters.AddWithValue("@Caducidad", DBNull.Value)
            End If

            Dim T As Integer = CInt(SQLNuevo.ExecuteScalar())
            SQLNuevo.Dispose()

            cnn.Dispose()
            cnn.Close()
        End If
        Tabla.Dispose()
    End Function

    Sub GuardaEncuesta()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                               "SELECT * From Danone_Encuesta " & _
                                               "WHERE folio_historial=" & FolioAct & "")
        If Tabla.Rows.Count = 1 Then
            Dim cnn As New SqlConnection(ConexionBerol.localSqlServer)
            cnn.Open()

            Dim SQLEdita As New SqlCommand("execute Danone_EditarEncuesta @folio_historial,@1,@2,@2A,@2B,@2C,@3,@3A,@3B,@3C,@4,@5", cnn)

            With SQLEdita
                .Parameters.AddWithValue("@folio_historial", FolioAct)
                .Parameters.AddWithValue("@1", cmb1.SelectedValue)
                .Parameters.AddWithValue("@2", cmb2.SelectedValue)
                If ch2A.Checked = True Then
                    .Parameters.AddWithValue("@2A", ch2A.Text) : Else
                    .Parameters.AddWithValue("@2A", "") : End If
                If ch2B.Checked = True Then
                    .Parameters.AddWithValue("@2B", ch2B.Text) : Else
                    .Parameters.AddWithValue("@2B", "") : End If
                If ch2C.Checked = True Then
                    .Parameters.AddWithValue("@2C", ch2C.Text) : Else
                    .Parameters.AddWithValue("@2C", "") : End If

                .Parameters.AddWithValue("@3", cmb3.SelectedValue)
                If ch3A.Checked = True Then
                    .Parameters.AddWithValue("@3A", ch3A.Text) : Else
                    .Parameters.AddWithValue("@3A", "") : End If
                If ch3B.Checked = True Then
                    .Parameters.AddWithValue("@3B", ch3B.Text) : Else
                    .Parameters.AddWithValue("@3B", "") : End If
                If ch3C.Checked = True Then
                    .Parameters.AddWithValue("@3C", ch3C.Text) : Else
                    .Parameters.AddWithValue("@3C", "") : End If

                .Parameters.AddWithValue("@4", cmb4.SelectedValue)
                .Parameters.AddWithValue("@5", cmb5.SelectedValue)
                .ExecuteNonQuery()
                .Dispose()
            End With

            cnn.Dispose()
            cnn.Close()
        Else
            Dim cnn As New SqlConnection(ConexionBerol.localSqlServer)
            cnn.Open()

            Dim SQLNuevo As New SqlCommand("execute Danone_CrearEncuesta @folio_historial,@1,@2,@2A,@2B,@2C,@3,@3A,@3B,@3C,@4,@5", cnn)
            With SQLNuevo
                .Parameters.AddWithValue("@folio_historial", FolioAct)
                .Parameters.AddWithValue("@1", cmb1.SelectedValue)
                .Parameters.AddWithValue("@2", cmb2.SelectedValue)
                If ch2A.Checked = True Then
                    .Parameters.AddWithValue("@2A", ch2A.Text) : Else
                    .Parameters.AddWithValue("@2A", "") : End If
                If ch2B.Checked = True Then
                    .Parameters.AddWithValue("@2B", ch2B.Text) : Else
                    .Parameters.AddWithValue("@2B", "") : End If
                If ch2C.Checked = True Then
                    .Parameters.AddWithValue("@2C", ch2C.Text) : Else
                    .Parameters.AddWithValue("@2C", "") : End If

                .Parameters.AddWithValue("@3", cmb3.SelectedValue)
                If ch3A.Checked = True Then
                    .Parameters.AddWithValue("@3A", ch3A.Text) : Else
                    .Parameters.AddWithValue("@3A", "") : End If
                If ch3B.Checked = True Then
                    .Parameters.AddWithValue("@3B", ch3B.Text) : Else
                    .Parameters.AddWithValue("@3B", "") : End If
                If ch3C.Checked = True Then
                    .Parameters.AddWithValue("@3C", ch3C.Text) : Else
                    .Parameters.AddWithValue("@3C", "") : End If

                .Parameters.AddWithValue("@4", cmb4.SelectedValue)
                .Parameters.AddWithValue("@5", cmb5.SelectedValue)
                .ExecuteNonQuery()
                .Dispose()
            End With

            cnn.Close()
            cnn.Dispose()
        End If
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("javascript:history.go(-1)")
    End Sub

    Protected Sub btnSubir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubir.Click
        Dim NombreFoto As String
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFerrero.localSqlServer, _
                                                   "SELECT * From Danone_Galeria_Historial_Det " & _
                                                   "WHERE id_periodo='" & IDPeriodo & "' " & _
                                                   "ORDER BY no_foto DESC")
        Dim NoFoto As String
        If Tabla.Rows.Count > 0 Then
            NoFoto = Tabla.Rows(0)("no_foto") + 1
        Else
            NoFoto = "1"
        End If

        NombreFoto = (IDPeriodo + "-" + NoFoto + ".JPG")
        BD.Execute(ConexionFerrero.localSqlServer, _
                   "INSERT INTO Danone_Galeria_Historial_Det " & _
                   "(id_periodo,id_usuario, descripcion, ruta, foto, no_foto, nombre_tienda, colonia) " & _
                   "VALUES(" & IDPeriodo & ",'" & IDUsuario & "', '" & txtDescripcion.Text & "', " & _
                   "'/ARCHIVOS/CLIENTES/FERRERO/DANONE/IMAGENES/','" & NombreFoto & "'," & NoFoto & "," & _
                   "'" & txtTienda.Text & "', " & cmbColonia.SelectedValue & ")")

        Dim fileExt As String
        fileExt = System.IO.Path.GetExtension(FileUpload1.FileName)

        If (fileExt = ".exe") Then
            lblSubida.Text = "No puedes subir archivos .exe"
        Else
            If FileUpload1.HasFile = True Then
                Dim path As String = Server.MapPath("~/ARCHIVOS/CLIENTES/FERRERO/DANONE/IMAGENES/")
                FileUpload1.PostedFile.SaveAs(path & NombreFoto)
                lblSubida.Text = "El archivo con la descripción: '" & txtDescripcion.Text & "' fue subido correctamente"
            End If
        End If

        CargaImagenes()
        txtDescripcion.Text = ""
    End Sub

    Sub CargaImagenes()
        CargaGrilla(ConexionFerrero.localSqlServer, _
                    "SELECT * FROM Danone_Galeria_Historial_Det WHERE id_usuario='" & IDUsuario & "' " & _
                    "AND id_periodo='" & IDPeriodo & "' AND nombre_tienda = '" & txtTienda.Text & "' " & _
                    "AND colonia = '" & cmbColonia.SelectedValue & "' ORDER BY no_foto", Me.gridImagenes)
    End Sub

    Private Sub gridImagenes_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridImagenes.RowDeleting
        BD.Execute(ConexionFerrero.localSqlServer, _
                   "DELETE FROM Danone_Galeria_Historial_Det " & _
                   "WHERE folio_foto = " & gridImagenes.DataKeys(e.RowIndex).Value.ToString() & "")
        CargaImagenes()
    End Sub

End Class