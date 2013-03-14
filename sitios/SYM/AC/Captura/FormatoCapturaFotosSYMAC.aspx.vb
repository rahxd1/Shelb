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

Partial Public Class FormatoCapturaFotosSYMAC
    Inherits System.Web.UI.Page

    Dim TiendaSQL, PeriodoSQL As String

    Sub SQLCombo()
        PeriodoSQL = "SELECT * FROM AC_Periodos " & _
                    "WHERE fecha_inicio<='" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "'" & _
                    "AND fecha_cierre>='" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "'"

        TiendaSQL = "SELECT (TI.nombre + ' ('+ CAD.nombre_cadena + ')')as tienda, TI.id_tienda " & _
                    "FROM AC_Tiendas as TI " & _
                    "INNER JOIN AC_CatRutas as RUT ON RUT.id_tienda=TI.id_tienda " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = TI.id_cadena " & _
                    "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario=RUT.id_usuario " & _
                    "WHERE TI.estatus=1 AND REL.id_supervisor='" & HttpContext.Current.User.Identity.Name & "' " & _
                    "ORDER BY TI.nombre"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionSYM.localSqlServer, HttpContext.Current.User.Identity.Name)

            SQLCombo()
            Combo.LlenaDrop(ConexionSYM.localSqlServer, TiendaSQL, "tienda", "id_tienda", cmbTienda)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo)
        End If
    End Sub

    Private Function CambioEstatus(ByVal folio As Integer) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "select * from AC_Galeria_Historial_Det " & _
                                               "WHERE folio_historial=" & folio & "")
        Dim Estatus As Integer
        If Tabla.Rows.Count > 0 Then
            Estatus = 1 : Else
            Estatus = 2 : End If
        Tabla.Dispose()

        BD.Execute(ConexionSYM.localSqlServer, _
                   "UPDATE AC_Fotos_CatRutas SET estatus=" & Estatus & " " & _
                   "FROM AC_Fotos_CatRutas " & _
                   "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                   "AND id_usuario='" & HttpContext.Current.User.Identity.Name & "' " & _
                   "AND id_tienda=" & cmbTienda.SelectedValue & "")
    End Function

    Protected Sub btnSubir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubir.Click
        Dim NombreFoto As String
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT * From AC_Galeria_Historial_Det " & _
                                               "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                                               "AND id_tienda=" & cmbTienda.SelectedValue & " " & _
                                               "AND id_usuario='" & HttpContext.Current.User.Identity.Name & "' " & _
                                               "ORDER BY no_foto DESC")
        Dim NoFoto As String
        If tabla.Rows.Count > 0 Then
            NoFoto = tabla.Rows(0)("no_foto") + 1 : Else
            NoFoto = "1" : End If
        Tabla.Dispose()

        NombreFoto = (cmbPeriodo.SelectedValue + "-" + cmbTienda.SelectedValue + "-" + NoFoto + ".JPG")
        BD.Execute(ConexionSYM.localSqlServer, _
                   "INSERT INTO AC_Galeria_Historial_Det " & _
                   "(id_periodo, id_tienda,id_usuario, descripcion, ruta, foto, no_foto) " & _
                   "VALUES(" & cmbPeriodo.SelectedValue & "," & cmbTienda.SelectedValue & ", " & _
                   "'" & HttpContext.Current.User.Identity.Name & "','" & txtDescripcion.Text & "', " & _
                   "'/ARCHIVOS/CLIENTES/SYM/AC/IMAGENES/ANAQUEL/','" & NombreFoto & "'," & NoFoto & ")")

        Dim fileExt As String
        fileExt = System.IO.Path.GetExtension(FileUpload1.FileName)

        If (fileExt = ".exe") Then
            lblSubida.Text = "No puedes subir archivos .exe"
        Else
            If FileUpload1.HasFile = True Then
                Dim path As String = Server.MapPath("~/ARCHIVOS/CLIENTES/SYM/AC/IMAGENES/ANAQUEL/")
                FileUpload1.PostedFile.SaveAs(path & NombreFoto)
                lblSubida.Text = "El archivo fue subido correctamente"
                txtDescripcion.Text = ""
                cmbTienda.SelectedValue = ""
            End If

        End If

        CargaImagenes()
    End Sub

    Sub CargaImagenes()
        CargaGrilla(ConexionSYM.localSqlServer, "SELECT * FROM AC_Galeria_Historial_Det " & _
                                "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                                "AND id_usuario='" & HttpContext.Current.User.Identity.Name & "' ORDER BY no_foto", _
                                Me.gridImagenes)
    End Sub

    Private Sub gridImagenes_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridImagenes.RowDeleting
        BD.Execute(ConexionSYM.localSqlServer, _
                   "DELETE FROM AC_Galeria_Historial_Det " & _
                   "WHERE folio_foto=" & gridImagenes.DataKeys(e.RowIndex).Value.ToString() & "")

        CargaImagenes()
    End Sub
End Class