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

Partial Public Class FormatoCapturaFotosSupNR
    Inherits System.Web.UI.Page

    Dim IDTienda, IDUsuario, IDPeriodo As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionBerol.localSqlServer, HttpContext.Current.User.Identity.Name)

            If Tipo_usuario = 2 Or Tipo_usuario = 100 Then
                CargaImagenes()
            Else
                Response.Redirect("RutasSupervisorNR.aspx")
            End If
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

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                               "SELECT * From NR_Historial_Galeria_Det " & _
                                            "WHERE id_periodo=" & IDPeriodo & " " & _
                                            "AND id_tienda=" & IDTienda & " " & _
                                            "AND id_usuario='" & IDUsuario & "' ORDER BY no_foto DESC")
        Dim NoFoto As String
        If Tabla.Rows.Count > 0 Then
            NoFoto = Tabla.Rows(0)("no_foto") + 1 : Else
            NoFoto = "1" : End If

        NombreFoto = (IDPeriodo + "-" + IDTienda + "-" + NoFoto + ".JPG")
        BD.Execute(ConexionBerol.localSqlServer, _
                   "INSERT INTO NR_Historial_Galeria_Det " & _
                   "(id_periodo, id_tienda,id_usuario, descripcion, ruta, foto, no_foto) " & _
                   "VALUES(" & IDPeriodo & "," & IDTienda & ",'" & IDUsuario & "', " & _
                   "'" & txtDescripcion.Text & "',"",'" & NombreFoto & "', " & NoFoto & ")")

        Dim fileExt As String
        fileExt = System.IO.Path.GetExtension(FileUpload1.FileName)

        If (fileExt = ".exe") Then
            lblSubida.Text = "No puedes subir archivos .exe"
        Else
            If FileUpload1.HasFile = True Then
                Dim path As String = Server.MapPath("~/ARCHIVOS/CLIENTES/BEROL/NR/IMAGENES/ANAQUEL/SUPERVISOR/")
                FileUpload1.PostedFile.SaveAs(path & NombreFoto)
                lblSubida.Text = "El archivo fue subido correctamente"
                txtDescripcion.Text = ""
            End If

        End If

        BD.Execute(ConexionBerol.localSqlServer, _
                   "UPDATE NR_Rutas_Eventos_Sup SET estatus_fotos=1 " & _
                   "FROM NR_Rutas_Eventos_Sup " & _
                   "WHERE id_periodo=" & IDPeriodo & " " & _
                   "AND id_usuario='" & IDUsuario & "'" & _
                   "AND id_tienda=" & IDTienda & "")

        CargaImagenes()
    End Sub

    Sub CargaImagenes()
        Datos()

        CargaGrilla(ConexionBerol.localSqlServer, _
                    "SELECT * FROM NR_Historial_Galeria_Det " & _
                    "WHERE id_periodo=" & IDPeriodo & " " & _
                    "AND id_usuario='" & IDUsuario & "' ORDER BY no_foto", Me.gridImagenes)
    End Sub

    Private Sub gridImagenes_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridImagenes.RowDeleting
        BD.Execute(ConexionBerol.localSqlServer, _
                   "DELETE FROM NR_Historial_Galeria_Det " & _
                   "WHERE folio_foto=" & gridImagenes.DataKeys(e.RowIndex).Value.ToString() & "")
        CargaImagenes()
    End Sub
End Class