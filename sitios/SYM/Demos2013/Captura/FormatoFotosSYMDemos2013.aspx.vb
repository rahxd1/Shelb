Public Partial Class FormatoFotosSYMDemos2013
    Inherits System.Web.UI.Page
   Dim IDTienda, IDUsuario, IDPeriodo, Catalogado, FolioAct As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionSYM.localSqlServer, HttpContext.Current.User.Identity.Name)
            VerDatos()
            If Edicion = 1 Then
                btnSubir.Visible = True : Else
                btnSubir.Visible = False : End If
        End If



    End Sub

    Private Sub VerDatos()
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "select * FROM SYM_D_Historial_Imagenes_Detalle " & _
                                               "WHERE folio_historial =" & FolioAct & "")
        If Tabla.Rows.Count > 0 Then
            CargaImagenes(FolioAct)
        End If

        Tabla.Dispose()

       
    End Sub

    Private Function CargaImagenes(ByVal folio As Integer) As Boolean

        CargaGrilla(ConexionSYM.localSqlServer, "SELECT * FROM SYM_D_Historial_Imagenes_Detalle " & _
                                "WHERE folio_historial=" & folio & " " & _
                                "ORDER BY no_foto", _
                                Me.gridImagenes)
    End Function

    Private Function CambioEstatus(ByVal folio As Integer) As Boolean
        Datos()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                             "select * from SYM_D_Historial_Imagenes_Detalle " & _
                                               "WHERE folio_historial =" & folio & "")
        Dim Estatus As Integer
        If Tabla.Rows.Count > 0 Then
            Estatus = 1 : End If

        Tabla.Dispose()

        BD.Execute(ConexionSYM.localSqlServer, _
                   "UPDATE SYM_D_Rutas_Eventos SET status_imagenes=" & Estatus & " " & _
                   "FROM SYM_D_Rutas_Eventos " & _
                   "WHERE id_periodo=" & IDPeriodo & " " & _
                   "AND id_usuario='" & IDUsuario & "' " & _
                   "AND id_tienda=" & IDTienda & "")
    End Function

    Protected Sub btnSubir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubir.Click
        Datos()
        Dim NombreFoto As String

        Dim Tabla2 As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "select * from SYM_D_Historial_Imagenes " & _
                                               "WHERE folio_historial = " & FolioAct & "")


        If Tabla2.Rows.Count = 0 Then
            FolioAct = BD.RT.Execute(ConexionSYM.localSqlServer, _
                         "execute proc_SYM_D_Crear_Historial_Imagenes " & _
                          "" & IDPeriodo & ", " & IDTienda & ", '" & IDUsuario & "'")


        End If


        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT * From SYM_D_Historial_Imagenes_Detalle " & _
                                               "WHERE folio_historial = " & FolioAct & "" & _
                                               "ORDER BY no_foto DESC")


        Dim NoFoto As String

        If Tabla.Rows.Count > 0 Then
            NoFoto = Tabla.Rows(0)("no_foto") + 1 : Else
            NoFoto = "1" : End If
        Tabla.Dispose()

        NombreFoto = (IDPeriodo + "-" + IDTienda + "-" + NoFoto + ".JPG")
        BD.Execute(ConexionSYM.localSqlServer, _
                   "INSERT INTO SYM_D_Historial_Imagenes_Detalle " & _
                   "(folio_historial, descripcion,ruta, foto, no_foto) " & _
                   "VALUES(" & FolioAct & ",'" & txtDescripcion.Text & "', " & _
                   "'/ARCHIVOS/CLIENTES/SYM/DEMOS2013/IMAGENES/','" & NombreFoto & "'," & NoFoto & ")")

        Dim fileExt As String
        fileExt = System.IO.Path.GetExtension(FileUpload1.FileName)

        If (fileExt = ".exe") Then
            lblSubida.Text = "No puedes subir archivos .exe"
        Else
            If FileUpload1.HasFile = True Then
                Dim path As String = Server.MapPath("~/ARCHIVOS/CLIENTES/SYM/DEMOS2013/IMAGENES/")
                FileUpload1.PostedFile.SaveAs(path & NombreFoto)
                'lblSubida.Text = "El archivo fue subido correctamente"
                txtDescripcion.Text = ""

            End If

        End If

        CargaImagenes(FolioAct)
        CambioEstatus(FolioAct)
      
    End Sub

    Private Sub gridImagenes_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridImagenes.RowDeleting
        BD.Execute(ConexionSYM.localSqlServer, _
                   "DELETE FROM SYM_D_Historial_Imagenes_Detalle " & _
                   "WHERE folio_historial_detalle=" & gridImagenes.DataKeys(e.RowIndex).Value.ToString() & "")

        CargaImagenes(FolioAct)
    End Sub

    Sub Datos()
        FolioAct = Request.Params("folio")
        IDTienda = Request.Params("tienda")
        IDUsuario = Request.Params("usuario")
        IDPeriodo = Request.Params("periodo")
    End Sub

  
End Class