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

Partial Public Class FormatoCapturaFotosFluid
    Inherits System.Web.UI.Page

    Dim IDUsuario, IDPeriodo As String
    Dim SQLCadena, SQLTienda, CadenaSel As String

    Sub SQLCombo()
        Datos()

        CadenaSel = Acciones.Slc.cmb("id_cadena", cmbCadena.SelectedValue)

        SQLCadena = "select DISTINCT id_cadena, nombre_cadena from View_Tiendas as TI " & _
                    "INNER JOIN CatRutas as RUT ON TI.id_region=RUT.id_region AND RUT.id_estado=TI.id_estado  " & _
                    "WHERE RUT.id_usuario='" & IDUsuario & "'  " & _
                    "order by nombre_cadena"

        SQLTienda = "select id_tienda, nombre from View_Tiendas as TI " & _
                    "INNER JOIN CatRutas as RUT ON TI.id_region=RUT.id_region AND RUT.id_estado=TI.id_estado  " & _
                    "WHERE RUT.id_usuario='" & IDUsuario & "' " & _
                    " " + CadenaSel + " order by nombre"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionFluidmaster.localSqlServer, HttpContext.Current.User.Identity.Name)
            Datos()

            If Tipo_usuario = 1 Or Tipo_usuario = 100 Then
                CargaImagenes() : Else
                Response.Redirect("RutasFluid.aspx") : End If

            SQLCombo()

            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, "select * from Tipo_Fotografias order by descripcion_fotografia", "descripcion_fotografia", "tipo_fotografia", cmbTipoFotografia)
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, SQLCadena, "nombre_cadena", "id_cadena", cmbCadena)
        End If
    End Sub

    Sub Datos()
        IDUsuario = Request.Params("usuario")
        IDPeriodo = Request.Params("periodo")
    End Sub

    Protected Sub btnSubir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubir.Click
        Dim NombreFoto As String
        Datos()

        Dim TablaFotos As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                                               "SELECT COUNT(folio_foto)Fotos " & _
                                               "From Historial_Galeria_Promotor_Det " & _
                                               "WHERE id_periodo=" & IDPeriodo & " AND id_tienda=" & cmbTienda.SelectedValue & " " & _
                                               "AND id_usuario='" & IDUsuario & "'")
        Dim fotos As Integer
        fotos = tablaFotos.Rows(0)("Fotos")

        If fotos >= 5 Then
            lblSubida.Text = "Solo puedes subir 10 fotografías por tienda"
        Else
            Dim Tabla As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                                                        "SELECT * From Historial_Galeria_Promotor_Det " & _
                                            "WHERE id_periodo=" & IDPeriodo & " AND id_tienda=" & cmbTienda.SelectedValue & " " & _
                                            "AND id_usuario='" & IDUsuario & "' ORDER BY no_foto DESC")
            Dim NoFoto As String
            If tabla.Rows.Count > 0 Then
                NoFoto = tabla.Rows(0)("no_foto") + 1 : Else
                NoFoto = "1" : End If

            Dim TablaRep As DataTable = Tablas.Ver.DT(ConexionFluidmaster.localSqlServer, _
                                                   "SELECT * From Historial_Galeria_Promotor_Det " & _
                                            "WHERE id_periodo=" & IDPeriodo & " " & _
                                            "AND id_tienda=" & cmbTienda.SelectedValue & " " & _
                                            "AND id_usuario='" & IDUsuario & "' " & _
                                            "AND tipo_fotografia=" & cmbTipoFotografia.SelectedValue & " ")
            If TablaRep.Rows.Count = 0 Then
                Dim fecha As String = ISODates.Dates.SQLServerDate(CDate(txtfecha.Text))
                NombreFoto = (IDPeriodo + "-" + cmbTienda.SelectedValue + "-" + NoFoto + ".JPG")
                BD.Execute(ConexionFluidmaster.localSqlServer, _
                           "INSERT INTO Historial_Galeria_Promotor_Det " & _
                           "(id_periodo,id_tienda,id_usuario,tipo_fotografia,fecha," & _
                           "tipo, descripcion,foto, no_foto) " & _
                           "VALUES(" & IDPeriodo & "," & cmbTienda.SelectedValue & "," & _
                           "'" & IDUsuario & "'," & cmbTipoFotografia.SelectedValue & ", " & _
                           "'" & Fecha & "'," & chkTipo.SelectedValue & "," & _
                           "'" & txtDescripcion.Text & "','" & NombreFoto & "'," & NoFoto & ")")

                Dim fileExt As String
                fileExt = System.IO.Path.GetExtension(FileUpload1.FileName)

                If (fileExt = ".exe") Then
                    lblSubida.Text = "No puedes subir archivos .exe"
                Else
                    If FileUpload1.HasFile = True Then
                        Dim path As String = Server.MapPath("~/ARCHIVOS/CLIENTES/FLUIDMASTER/IMAGENES/")
                        FileUpload1.PostedFile.SaveAs(path & NombreFoto)
                        lblSubida.Text = "El archivo fue subido correctamente"
                        txtDescripcion.Text = ""
                        cmbTipoFotografia.SelectedValue = ""
                        txtfecha.Text = ""
                    End If
                End If
            Else
                lblSubida.Text = "Por favor, sube una foto diferente."
            End If

            Tabla.Dispose()
            TablaFotos.Dispose()
            TablaRep.Dispose()
        End If

        CargaImagenes()
    End Sub

    Sub CargaImagenes()
        Datos()

        CargaGrilla(ConexionFluidmaster.localSqlServer, _
                   "SELECT * FROM Historial_Galeria_Promotor_Det " & _
                   "WHERE id_periodo=" & IDPeriodo & " AND id_usuario='" & IDUsuario & "'  " & _
                   "ORDER BY no_foto", Me.gridImagenes)
    End Sub

    Private Sub gridImagenes_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridImagenes.RowDeleting
        BD.Execute(ConexionFluidmaster.localSqlServer, _
                   "DELETE FROM Historial_Galeria_Promotor_Det " & _
                   "WHERE folio_foto=" & gridImagenes.DataKeys(e.RowIndex).Value.ToString() & "")

        CargaImagenes()
    End Sub

    Private Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCadena.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, SQLTienda, "nombre", "id_tienda", cmbTienda)
    End Sub
End Class