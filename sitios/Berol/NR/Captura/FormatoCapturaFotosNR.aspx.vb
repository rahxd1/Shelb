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

Partial Public Class FormatoCapturaFotosNR
    Inherits System.Web.UI.Page

    Dim IDTienda, IDUsuario, IDPeriodo As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionBerol.localSqlServer, HttpContext.Current.User.Identity.Name)
            Datos()

            If Tipo_usuario = 1 Or Tipo_usuario = 100 Then
                CargaImagenes():Else
                Response.Redirect("RutasSupervisorNR.aspx") : End If

            Combo.LlenaDrop(ConexionBerol.localSqlServer, "select * from Ubicacion where activo=1 order by nombre_ubicacion", "nombre_ubicacion", "id_ubicacion", cmbUbicacion)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, "select * from Tipo_Productos where activo=1 order by nombre_tipo", "nombre_tipo", "tipo_producto", cmbFamilia)
            
        End If
    End Sub

    Sub Datos()
        IDUsuario = Request.Params("usuario")
        IDPeriodo = Request.Params("periodo")
        IDTienda = Request.Params("tienda")
    End Sub

    Protected Sub btnSubir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubir.Click
        Dim NombreFoto As String
        Datos()

        Dim TablaFotos As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                               "SELECT COUNT(folio_foto)Fotos " & _
                                               "From NR_Historial_Galeria_Promotor_Det " & _
                                               "WHERE id_periodo=" & IDPeriodo & " AND id_tienda=" & IDTienda & " " & _
                                               "AND id_usuario='" & IDUsuario & "'")
        Dim fotos As Integer
        If TablaFotos.Rows.Count > 0 Then
            fotos = TablaFotos.Rows(0)("Fotos") : End If
        TablaFotos.Dispose()

        If fotos >= 5 Then
            lblSubida.Text = "Solo puedes subir 5 fotografías por tienda"
        Else
            Dim Tabla As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                               "SELECT * From NR_Historial_Galeria_Promotor_Det " & _
                                                "WHERE id_periodo=" & IDPeriodo & " AND id_tienda=" & IDTienda & " " & _
                                                "AND id_usuario='" & IDUsuario & "' " & _
                                                "ORDER BY no_foto DESC")
            Dim NoFoto As String
            If tabla.Rows.Count > 0 Then
                NoFoto = tabla.Rows(0)("no_foto") + 1 : Else
                NoFoto = "1" : End If
            Tabla.Dispose()

            Dim TablaRep As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                                   "SELECT * From NR_Historial_Galeria_Promotor_Det " & _
                                                    "WHERE id_periodo=" & IDPeriodo & " " & _
                                                    "AND id_tienda=" & IDTienda & " " & _
                                                    "AND id_usuario='" & IDUsuario & "' " & _
                                                    "AND id_ubicacion=" & cmbUbicacion.SelectedValue & " " & _
                                                    "AND tipo_producto=" & cmbFamilia.SelectedValue & "")

            If TablaRep.Rows.Count = 0 Then
                NombreFoto = (IDPeriodo + "-" + IDTienda + "-" + NoFoto + ".JPG")
                BD.Execute(ConexionBerol.localSqlServer, _
                           "INSERT INTO NR_Historial_Galeria_Promotor_Det " & _
                             "(id_periodo,id_tienda,id_usuario,id_ubicacion,tipo_producto," & _
                             "tipo, descripcion,foto, no_foto) " & _
                             "VALUES(" & IDPeriodo & "," & IDTienda & ",'" & IDUsuario & "', " & _
                             "" & cmbUbicacion.SelectedValue & "," & cmbFamilia.SelectedValue & ", " & _
                             "" & chkTipo.SelectedValue & ",'" & txtDescripcion.Text & "'," & _
                             "'" & NombreFoto & "'," & NoFoto & ")")
                Dim fileExt As String
                fileExt = System.IO.Path.GetExtension(FileUpload1.FileName)

                If (fileExt = ".exe") Then
                    lblSubida.Text = "No puedes subir archivos .exe"
                Else
                    If FileUpload1.HasFile = True Then
                        Dim path As String = Server.MapPath("~/ARCHIVOS/CLIENTES/BEROL/NR/IMAGENES/ANAQUEL/PROMOTOR/")
                        FileUpload1.PostedFile.SaveAs(path & NombreFoto)
                        lblSubida.Text = "El archivo fue subido correctamente"
                        txtDescripcion.Text = ""
                        cmbFamilia.SelectedValue = ""
                        cmbUbicacion.SelectedValue = ""
                    End If
                End If

                BD.Execute(ConexionBerol.localSqlServer, _
                           "UPDATE NR_Rutas_Eventos SET estatus_fotos=1 " & _
                           "FROM NR_Rutas_Eventos " & _
                           "WHERE id_periodo=" & IDPeriodo & " " & _
                           "AND id_usuario='" & IDUsuario & "' " & _
                           "AND id_tienda=" & IDTienda & "")
            Else
                lblSubida.Text = "Por favor, sube una foto diferente."
            End If

            TablaRep.Dispose()

        End If

        CargaImagenes()
    End Sub

    Sub CargaImagenes()
        Datos()

        CargaGrilla(ConexionBerol.localSqlServer, _
                   "SELECT * FROM NR_Historial_Galeria_Promotor_Det " & _
                   "WHERE id_periodo=" & IDPeriodo & " AND id_usuario='" & IDUsuario & "'  " & _
                   "AND id_tienda=" & IDTienda & " ORDER BY no_foto", Me.gridImagenes)
    End Sub

    Private Sub gridImagenes_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridImagenes.RowDeleting
        BD.Execute(ConexionBerol.localSqlServer, _
                   "DELETE FROM NR_Historial_Galeria_Promotor_Det " & _
                   "WHERE folio_foto=" & gridImagenes.DataKeys(e.RowIndex).Value.ToString() & "")
        CargaImagenes()
    End Sub
End Class