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

Partial Public Class FormatoComentariosNR
    Inherits System.Web.UI.Page

    Dim IDTienda, IDUsuario, IDPeriodo As String
    Dim FolioAct As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionBerol.localSqlServer, HttpContext.Current.User.Identity.Name)

            VerDatos()

            Combo.LlenaDropSin(ConexionBerol.localSqlServer, "select * from Tipo_Comentarios", "descripcion_comentario", "tipo_comentario", cmbTipoComentario)

            If Edicion = 1 Then
                btnGuardar.Visible = True : Else
                btnGuardar.Visible = False : End If
        End If
    End Sub

    Private Sub VerDatos()
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                               "select * FROM NR_Historial as H " & _
                                               "WHERE folio_historial = " & FolioAct & "")
        If Tabla.Rows.Count > 0 Then
            cmbTipoComentario.SelectedValue = Tabla.Rows(0)("tipo_comentario")
            txtComentarios.Text = Tabla.Rows(0)("comentarios")
        End If
        Tabla.Dispose()

        Dim TablaTienda As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                               "select * FROM NR_Tiendas as TI  " & _
                                               "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena=TI.id_cadena " & _
                                               "WHERE id_tienda = " & IDTienda & "")
        If TablaTienda.Rows.Count > 0 Then
            lblTienda.Text = TablaTienda.Rows(0)("nombre")
            lblCadena.Text = TablaTienda.Rows(0)("nombre_cadena")
        End If

        TablaTienda.Dispose()
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        btnGuardar.Enabled = False
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionBerol.localSqlServer, _
                                                     "SELECT * From NR_Historial " & _
                                                     "WHERE folio_historial=" & FolioAct & "")

        If Tabla.Rows.Count = 1 Then
            BD.Execute(ConexionBerol.localSqlServer, _
                       "UPDATE NR_Historial " & _
                        "SET tipo_comentario=" & cmbTipoComentario.SelectedValue & "," & _
                        "comentarios='" & txtComentarios.Text & "' " & _
                        "WHERE folio_historial=" & FolioAct & "")
        Else
            BD.Execute(ConexionBerol.localSqlServer, _
                       "INSERT INTO NR_Historial" & _
                       "(tipo_comentario, comentarios, id_periodo, id_tienda, id_usuario) " & _
                       "VALUES(" & cmbTipoComentario.SelectedValue & ",'" & txtComentarios.Text & "', " & _
                       "" & IDPeriodo & "," & IDTienda & ",'" & IDUsuario & "')")
        End If
        Tabla.Dispose()

        BD.Execute(ConexionBerol.localSqlServer, _
                   "UPDATE NR_Rutas_Eventos SET estatus_comentarios=1 " & _
                   "FROM NR_Rutas_Eventos " & _
                   "WHERE id_periodo=" & IDPeriodo & " AND id_usuario='" & IDUsuario & "' " & _
                   "AND id_tienda=" & IDTienda & "")

        Response.Redirect("RutasPromotorNR.aspx")
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("RutasPromotorNR.aspx")
    End Sub

    Sub Datos()
        IDUsuario = Request.Params("usuario")
        IDPeriodo = Request.Params("periodo")
        IDTienda = Request.Params("tienda")
        FolioAct = Request.Params("folio")
    End Sub

End Class