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

Partial Public Class FormatoCapturaExhibicionesFotosSYMAC
    Inherits System.Web.UI.Page

    Dim TiendaSQL, CadenaSQL, EstadoSQL, PeriodoSQL, CadenaSel, EstadoSel As String

    Sub SQLCombo()
        EstadoSel = Acciones.Slc.cmb("TI.id_estado", cmbEstado.SelectedValue)
        CadenaSel = Acciones.Slc.cmb("TI.id_cadena", cmbCadena.SelectedValue)

        PeriodoSQL = "SELECT * FROM AC_Periodos " & _
                    "WHERE fecha_inicio<='" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "'" & _
                    "AND fecha_cierre>='" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "'"

        EstadoSQL = "SELECT DISTINCT ES.nombre_estado, ES.id_estado " & _
                    "FROM AC_Tiendas as TI " & _
                    "INNER JOIN Estados as ES ON ES.id_estado = Ti.id_estado " & _
                    "INNER JOIN AC_CatRutas as RUT ON RUT.id_tienda=TI.id_tienda " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = TI.id_cadena " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario=RUT.id_usuario " & _
                    "WHERE TI.estatus=1 AND TI.id_region=" & Region & " " & _
                    "ORDER BY ES.nombre_estado"

        CadenaSQL = "SELECT DISTINCT CAD.id_cadena, CAD.nombre_cadena " & _
                    "FROM AC_Tiendas as TI " & _
                    "INNER JOIN AC_CatRutas as RUT ON RUT.id_tienda=TI.id_tienda " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = TI.id_cadena " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario=RUT.id_usuario " & _
                    "WHERE TI.estatus=1 AND TI.id_region=" & Region & " " & _
                    " " + EstadoSel + " " & _
                    "ORDER BY CAD.nombre_cadena"

        TiendaSQL = "SELECT (TI.nombre + ' ('+ CAD.nombre_cadena + ')')as tienda, TI.id_tienda " & _
                    "FROM AC_Tiendas as TI " & _
                    "INNER JOIN AC_CatRutas as RUT ON RUT.id_tienda=TI.id_tienda " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = TI.id_cadena " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario=RUT.id_usuario " & _
                    "WHERE TI.estatus=1 AND TI.id_region=" & Region & " " & _
                    " " + EstadoSel + CadenaSel + " " & _
                    "ORDER BY TI.nombre"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionSYM.localSqlServer, HttpContext.Current.User.Identity.Name)

            If Region <> 0 Then
                SQLCombo()
                Combo.LlenaDrop(ConexionSYM.localSqlServer, TiendaSQL, "tienda", "id_tienda", cmbTienda)
                Combo.LlenaDrop(ConexionSYM.localSqlServer, EstadoSQL, "nombre_estado", "id_estado", cmbEstado)
                Combo.LlenaDrop(ConexionSYM.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
                Combo.LlenaDrop(ConexionSYM.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo)

                CargaImagenes()
            End If
        End If
    End Sub

    Private Function CambioEstatus(ByVal folio As Integer) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "select * from AC_Galeria_Exh_Historial_Det " & _
                                               "WHERE folio_historial=" & folio & "")
        Dim Estatus As Integer
        If Tabla.Rows.Count > 0 Then
            Estatus = 1 : Else
            Estatus = 2 : End If
        Tabla.Dispose()

        BD.Execute(ConexionSYM.localSqlServer, _
                   "UPDATE AC_Fotos_CatRutas SET estatus=" & Estatus & "" & _
                   "FROM AC_Fotos_CatRutas " & _
                   "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                   "AND id_usuario='" & HttpContext.Current.User.Identity.Name & "' " & _
                   "AND id_tienda=" & cmbTienda.SelectedValue & "")
    End Function

    Protected Sub btnSubir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubir.Click
        Dim NombreFoto As String

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "SELECT * From AC_Galeria_Exh_Historial_Det " & _
                                               "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                                               "AND id_tienda=" & cmbTienda.SelectedValue & " " & _
                                               "AND id_usuario='" & HttpContext.Current.User.Identity.Name & "' " & _
                                               "ORDER BY no_foto DESC")
        Dim NoFoto As String
        If tabla.Rows.Count > 0 Then
            NoFoto = tabla.Rows(0)("no_foto") + 1 : Else
            NoFoto = "1" : End If

        NombreFoto = (cmbPeriodo.SelectedValue + "-" + cmbTienda.SelectedValue + "-" + NoFoto + ".JPG")
        BD.Execute(ConexionSYM.localSqlServer, "INSERT INTO AC_Galeria_Exh_Historial_Det " & _
                   "(id_periodo, id_tienda,id_usuario, ruta, foto, no_foto) " & _
                   "VALUES(" & cmbPeriodo.SelectedValue & "," & cmbTienda.SelectedValue & ", " & _
                   "'" & HttpContext.Current.User.Identity.Name & "', " & _
                   "'/ARCHIVOS/CLIENTES/SYM/AC/IMAGENES/EXHIBICIONES/'," & NombreFoto & "," & NoFoto & ")")

        Dim fileExt As String
        fileExt = System.IO.Path.GetExtension(FileUpload1.FileName)

        If (fileExt = ".exe") Then
            lblSubida.Text = "No puedes subir archivos .exe"
        Else
            If FileUpload1.HasFile = True Then
                Dim path As String = Server.MapPath("~/ARCHIVOS/CLIENTES/SYM/AC/IMAGENES/EXHIBICIONES/")
                FileUpload1.PostedFile.SaveAs(path & NombreFoto)
                lblSubida.Text = "El archivo fue subido correctamente"
                cmbTienda.SelectedValue = ""
            End If

        End If

        CargaImagenes()
    End Sub

    Sub CargaImagenes()
        CargaGrilla(ConexionSYM.localSqlServer, "SELECT * FROM AC_Galeria_Exh_Historial_Det " & _
                                "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                                "AND id_usuario='" & HttpContext.Current.User.Identity.Name & "' ORDER BY no_foto", _
                                Me.gridImagenes)
    End Sub

    Private Sub gridImagenes_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridImagenes.RowDeleting
        BD.Execute(ConexionSYM.localSqlServer, _
                   "DELETE FROM AC_Galeria_Exh_Historial_Det " & _
                   "WHERE folio_foto=" & gridImagenes.DataKeys(e.RowIndex).Value.ToString() & "")

        CargaImagenes()
    End Sub

    Private Sub cmbEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEstado.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionSYM.localSqlServer, TiendaSQL, "tienda", "id_tienda", cmbTienda)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
    End Sub

    Private Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCadena.SelectedIndexChanged
        SQLCombo()
        Combo.LlenaDrop(ConexionSYM.localSqlServer, TiendaSQL, "tienda", "id_tienda", cmbTienda)
    End Sub
End Class