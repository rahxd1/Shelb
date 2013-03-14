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
Imports procomlcd.Permisos

Partial Public Class FormatoCapturaMarsAS
    Inherits System.Web.UI.Page

    Dim IDTienda, IDUsuario, IDPeriodo, IDSegmento, IDQuincena, FolioAct As String
    Dim IDRegion, IDCadena As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionMars.localSqlServer, HttpContext.Current.User.Identity.Name)

            ComboProducto(cmbProductoAnaquel12A) : ComboProducto(cmbProductoAnaquel12B) : ComboProducto(cmbProductoAnaquel12C)
            ComboProducto(cmbProductoAnaquel11A) : ComboProducto(cmbProductoAnaquel11B) : ComboProducto(cmbProductoAnaquel11C)
            ComboProducto(cmbProductoAnaquel10A) : ComboProducto(cmbProductoAnaquel10B) : ComboProducto(cmbProductoAnaquel10C)
            ComboProducto(cmbProductoAnaquel9A) : ComboProducto(cmbProductoAnaquel9B) : ComboProducto(cmbProductoAnaquel9C)
            ComboProducto(cmbProductoPasillo6A) : ComboProducto(cmbProductoPasillo6B) : ComboProducto(cmbProductoPasillo6C)
            ComboProducto(cmbProductoPasillo1A) : ComboProducto(cmbProductoPasillo1B) : ComboProducto(cmbProductoPasillo1C)
            ComboProducto(cmbProductoPasillo15A) : ComboProducto(cmbProductoPasillo15B) : ComboProducto(cmbProductoPasillo15C)
            ComboProducto(cmbProductoCaliente1A) : ComboProducto(cmbProductoCaliente1B) : ComboProducto(cmbProductoCaliente1C)
            ComboProducto(cmbProductoCaliente15A) : ComboProducto(cmbProductoCaliente15B) : ComboProducto(cmbProductoCaliente15C)
            ComboProducto(cmbProductoCaliente3A) : ComboProducto(cmbProductoCaliente3B) : ComboProducto(cmbProductoCaliente3C)
            ComboProducto(cmbProductoCaliente5A) : ComboProducto(cmbProductoCaliente5B) : ComboProducto(cmbProductoCaliente5C)
            ComboProducto(cmbProductoEntrada5A) : ComboProducto(cmbProductoEntrada5B) : ComboProducto(cmbProductoEntrada5C)
            ComboProducto(cmbProductoEntrada16A) : ComboProducto(cmbProductoEntrada16B) : ComboProducto(cmbProductoEntrada16C)

            ComboProducto(cmbProductoExhibicionesPasillo1A) : ComboProducto(cmbProductoExhibicionesPasillo1B) : ComboProducto(cmbProductoExhibicionesPasillo1C)
            ComboProducto(cmbProductoExhibicionesPasillo2A) : ComboProducto(cmbProductoExhibicionesPasillo2B) : ComboProducto(cmbProductoExhibicionesPasillo2C)
            ComboProducto(cmbProductoExhibicionesPasillo3A) : ComboProducto(cmbProductoExhibicionesPasillo3B) : ComboProducto(cmbProductoExhibicionesPasillo3C)
            ComboProducto(cmbProductoExhibicionesCaliente1A) : ComboProducto(cmbProductoExhibicionesCaliente1B) : ComboProducto(cmbProductoExhibicionesCaliente1C)
            ComboProducto(cmbProductoExhibicionesCaliente2A) : ComboProducto(cmbProductoExhibicionesCaliente2B) : ComboProducto(cmbProductoExhibicionesCaliente2C)
            ComboProducto(cmbProductoExhibicionesCaliente3A) : ComboProducto(cmbProductoExhibicionesCaliente3B) : ComboProducto(cmbProductoExhibicionesCaliente3C)
            ComboProducto(cmbProductoExhibicionesEntrada1A) : ComboProducto(cmbProductoExhibicionesEntrada1B) : ComboProducto(cmbProductoExhibicionesEntrada1C)
            ComboProducto(cmbProductoExhibicionesEntrada2A) : ComboProducto(cmbProductoExhibicionesEntrada2B) : ComboProducto(cmbProductoExhibicionesEntrada2C)
            ComboProducto(cmbProductoExhibicionesEntrada3A) : ComboProducto(cmbProductoExhibicionesEntrada3B) : ComboProducto(cmbProductoExhibicionesEntrada3C)

            ComboExhibiciones(2, cmbExhibicionesPasillo1) : ComboExhibiciones(2, cmbExhibicionesPasillo2) : ComboExhibiciones(2, cmbExhibicionesPasillo3)
            ComboExhibiciones(3, cmbExhibicionesCaliente1) : ComboExhibiciones(3, cmbExhibicionesCaliente2) : ComboExhibiciones(3, cmbExhibicionesCaliente3)
            ComboExhibiciones(4, cmbExhibicionesEntrada1) : ComboExhibiciones(4, cmbExhibicionesEntrada2) : ComboExhibiciones(4, cmbExhibicionesEntrada3)

            ComboComentarios()

            VerDatos()

            If Edicion = 1 Then
                If Tipo_usuario = 1 Or Tipo_usuario = 100 Then
                    btnGuardar.Visible = True
                    btnGuardarImplementacion.Visible = True : Else
                    btnGuardar.Visible = False
                    btnGuardarImplementacion.Visible = False : End If
            Else
                btnGuardar.Visible = False
                btnGuardarImplementacion.Visible = False : End If

            If Tipo_usuario = 1 Then
                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                                       "select * from AS_Rutas_Eventos " & _
                                          "WHERE orden=" & IDPeriodo & " " & _
                                          "AND id_quincena='" & IDQuincena & "' " & _
                                          "AND id_tienda =" & IDTienda & " " & _
                                          "AND id_usuario='" & IDUsuario & "'")

                If Tabla.Rows.Count > 0 Then
                    If Tabla.Rows(0)("estatus_anaquel") <> 1 Then
                        btnGuardar.Enabled = True : Else
                        btnGuardar.Enabled = False : End If
                End If
                Tabla.Dispose()

            End If
        End If
    End Sub

    Public Function ComboProducto(ByVal Combo As DropDownList) As Integer
        LlenaDropSin(ConexionMars.localSqlServer, _
                  "Select * from AS_Productos order by nombre_producto", _
                   "nombre_producto", "id_producto", Combo)
    End Function

    Public Function ComboExhibiciones(ByVal Punto As Integer, ByVal Combo As DropDownList) As Integer
        LlenaDropSin(ConexionMars.localSqlServer, _
                  "Select * from AS_Exhibiciones as EX " & _
                  "INNER JOIN AS_Exhibiciones_EnPuntos REL ON EX.id_exhibidor = REL.id_exhibidor " & _
                  "WHERE REL.id_punto=" & Punto & " order by nombre_exhibidor", _
                   "nombre_exhibidor", "id_exhibidor", Combo)
    End Function

    Sub ComboComentarios()
        LlenaDropSin(ConexionMars.localSqlServer, _
                  "Select * from AS_Tipo_Comentarios order by tipo_comentario", _
                   "descripcion_comentario", "tipo_comentario", cmbComentarios)
    End Sub

    Public Function ComboImplementacion(ByVal Region As Integer, ByVal Cadena As Integer) As Integer
        'lstImplementacion.Items.Clear()

        'Using cnn As New SqlConnection(ConexionMars.localSqlServer)
        '    Dim SQL As New SqlDataAdapter("select * from AS_Procesos " & _
        '                                "WHERE id_cadena=0 And id_region=0 " & _
        '                                "UNION ALL select * from AS_Procesos " & _
        '                                "WHERE id_cadena=0 And id_region=" & Region & " " & _
        '                                "UNION ALL select * from AS_Procesos " & _
        '                                "WHERE id_cadena=" & Cadena & " And id_region=0 " & _
        '                                "UNION ALL select * from AS_Procesos  " & _
        '                                "WHERE id_cadena=" & Cadena & " AND id_region=" & Region & "", cnn)
        '    Dim DatosCadenas As New DataTable
        '    SQL.Fill(DatosCadenas)
        '    lstImplementacion.DataSource = DatosCadenas
        '    lstImplementacion.DataMember = "AS_Procesos"
        '    lstImplementacion.DataValueField = "id_proceso"
        '    lstImplementacion.DataTextField = "nombre_proceso"
        '    lstImplementacion.DataBind()
        '    SQL.Dispose()
        '    DatosCadenas.Dispose()
        '    cnn.Close()
        '    cnn.Dispose()
        'End Using
    End Function
    '''044-55-10-17-63-39 HECTOR GLEZ.

    Private Sub VerDatos()
        Datos()

        Dim TablaTienda As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT * FROM AS_Tiendas as TI " & _
                                        "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = TI.id_cadena " & _
                                        "WHERE id_tienda=" & IDTienda & "")
        If TablaTienda.Rows.Count > 0 Then
            lblTienda.Text = TablaTienda.Rows(0)("nombre")
            lblCadena.Text = TablaTienda.Rows(0)("nombre_cadena")
            IDRegion = TablaTienda.Rows(0)("id_region")
            IDCadena = TablaTienda.Rows(0)("id_cadena")
        End If
        TablaTienda.Dispose()

        ComboImplementacion(IDRegion, IDCadena)

        Dim TablaHistorial As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                                     "select * from AS_Historial " & _
                                                     "where folio_historial=" & FolioAct & "")
        If TablaHistorial.Rows.Count > 0 Then
            txtComentarios.Text = TablaHistorial.Rows(0)("comentarios")
            cmbComentarios.SelectedValue = TablaHistorial.Rows(0)("tipo_comentario")
            'rdMedidas.SelectedValue = TablaHistorial.Rows(0)("id_medida")

            'If TablaHistorial.Rows(0)("fecha_medida") Is DBNull.Value Then
            '    txtFechaMedicion.Text = "" : Else
            '    txtFechaMedicion.Text = TablaHistorial.Rows(0)("fecha_medida") : End If

            If TablaHistorial.Rows(0)("PDQ") = 1 Then
                chkExhibidores13.Checked = True : Else
                chkExhibidores13.Checked = False : End If

            If TablaHistorial.Rows(0)("Floor_display") = 1 Then
                chkExhibidores17.Checked = True : Else
                chkExhibidores17.Checked = False : End If
        End If
        TablaHistorial.Dispose()

        Dim TablaSegmento As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                                        "select * from AS_Segmentos_Historial_Det " & _
                                                        "where folio_historial=" & FolioAct & "")
        If TablaSegmento.Rows.Count > 0 Then
            txtFrentes1.Text = TablaSegmento.Rows(0)("1")
            txtFrentes2.Text = TablaSegmento.Rows(0)("2")
            txtFrentes3.Text = TablaSegmento.Rows(0)("3")
            txtFrentes4.Text = TablaSegmento.Rows(0)("4")
            txtFrentes5.Text = TablaSegmento.Rows(0)("5")
            txtFrentes6.Text = TablaSegmento.Rows(0)("6")
            txtFrentes7.Text = TablaSegmento.Rows(0)("7")
            txtFrentes8.Text = TablaSegmento.Rows(0)("8")
            txtFrentes9.Text = TablaSegmento.Rows(0)("9")
            txtFrentes10.Text = TablaSegmento.Rows(0)("10")
            txtFrentes11.Text = TablaSegmento.Rows(0)("11")
            txtFrentes12.Text = TablaSegmento.Rows(0)("12")
            txtFrentes13.Text = TablaSegmento.Rows(0)("13")
            txtFrentes14.Text = TablaSegmento.Rows(0)("14")
            txtFrentes15.Text = TablaSegmento.Rows(0)("15")
            txtFrentes16.Text = TablaSegmento.Rows(0)("16")
        End If
        TablaSegmento.Dispose()

        VerExhibiciones(FolioAct, 12, 1, txtAnaquel12, cmbProductoAnaquel12A, cmbProductoAnaquel12B, cmbProductoAnaquel12C)
        VerExhibiciones(FolioAct, 11, 1, txtAnaquel11, cmbProductoAnaquel11A, cmbProductoAnaquel11B, cmbProductoAnaquel11C)
        VerExhibiciones(FolioAct, 10, 1, txtAnaquel10, cmbProductoAnaquel10A, cmbProductoAnaquel10B, cmbProductoAnaquel10C)
        VerExhibiciones(FolioAct, 9, 1, txtAnaquel9, cmbProductoAnaquel9A, cmbProductoAnaquel9B, cmbProductoAnaquel9C)

        VerExhibiciones(FolioAct, 6, 2, txtPasillo6, cmbProductoPasillo6A, cmbProductoPasillo6B, cmbProductoPasillo6C)
        VerExhibiciones(FolioAct, 1, 2, txtPasillo1, cmbProductoPasillo1A, cmbProductoPasillo1B, cmbProductoPasillo1C)
        VerExhibiciones(FolioAct, 15, 2, txtPasillo15, cmbProductoPasillo15A, cmbProductoPasillo15B, cmbProductoPasillo15C)

        VerExhibicionesOtras(FolioAct, 1, 14, 2, cmbExhibicionesPasillo1, txtExhibicionesPasillo1, cmbProductoExhibicionesPasillo1A, cmbProductoExhibicionesPasillo1B, cmbProductoExhibicionesPasillo1C)
        VerExhibicionesOtras(FolioAct, 2, 14, 2, cmbExhibicionesPasillo2, txtExhibicionesPasillo2, cmbProductoExhibicionesPasillo2A, cmbProductoExhibicionesPasillo2B, cmbProductoExhibicionesPasillo2C)
        VerExhibicionesOtras(FolioAct, 3, 14, 2, cmbExhibicionesPasillo3, txtExhibicionesPasillo3, cmbProductoExhibicionesPasillo3A, cmbProductoExhibicionesPasillo3B, cmbProductoExhibicionesPasillo3C)

        VerExhibiciones(FolioAct, 1, 3, txtCaliente1, cmbProductoCaliente1A, cmbProductoCaliente1B, cmbProductoCaliente1C)
        VerExhibiciones(FolioAct, 15, 3, txtCaliente15, cmbProductoCaliente15A, cmbProductoCaliente15B, cmbProductoCaliente15C)
        VerExhibiciones(FolioAct, 3, 3, txtCaliente3, cmbProductoCaliente3A, cmbProductoCaliente3B, cmbProductoCaliente3C)
        VerExhibiciones(FolioAct, 5, 3, txtCaliente5, cmbProductoCaliente5A, cmbProductoCaliente5B, cmbProductoCaliente5C)
        VerExhibicionesOtras(FolioAct, 1, 14, 3, cmbExhibicionesCaliente1, txtExhibicionesCaliente1, cmbProductoExhibicionesCaliente1A, cmbProductoExhibicionesCaliente1B, cmbProductoExhibicionesCaliente1C)
        VerExhibicionesOtras(FolioAct, 2, 14, 3, cmbExhibicionesCaliente2, txtExhibicionesCaliente2, cmbProductoExhibicionesCaliente2A, cmbProductoExhibicionesCaliente2B, cmbProductoExhibicionesCaliente2C)
        VerExhibicionesOtras(FolioAct, 3, 14, 3, cmbExhibicionesCaliente3, txtExhibicionesCaliente3, cmbProductoExhibicionesCaliente3A, cmbProductoExhibicionesCaliente3B, cmbProductoExhibicionesCaliente3C)

        VerExhibiciones(FolioAct, 5, 4, txtEntrada5, cmbProductoEntrada5A, cmbProductoEntrada5B, cmbProductoEntrada5C)
        VerExhibiciones(FolioAct, 16, 4, txtEntrada16, cmbProductoEntrada16A, cmbProductoEntrada16B, cmbProductoEntrada16C)
        VerExhibicionesOtras(FolioAct, 1, 14, 4, cmbExhibicionesEntrada1, txtExhibicionesEntrada1, cmbProductoExhibicionesEntrada1A, cmbProductoExhibicionesEntrada1B, cmbProductoExhibicionesEntrada1C)
        VerExhibicionesOtras(FolioAct, 2, 14, 4, cmbExhibicionesEntrada2, txtExhibicionesEntrada2, cmbProductoExhibicionesEntrada2A, cmbProductoExhibicionesEntrada2B, cmbProductoExhibicionesEntrada2C)
        VerExhibicionesOtras(FolioAct, 3, 14, 4, cmbExhibicionesEntrada3, txtExhibicionesEntrada3, cmbProductoExhibicionesEntrada3A, cmbProductoExhibicionesEntrada3B, cmbProductoExhibicionesEntrada3C)

        If FolioAct <> 0 Then
            If txtAnaquel12.Text = "" Then
                txtAnaquel12.Text = "0" : End If
            If txtAnaquel11.Text = "" Then
                txtAnaquel11.Text = "0" : End If
            If txtAnaquel10.Text = "" Then
                txtAnaquel10.Text = "0" : End If
            If txtAnaquel9.Text = "" Then
                txtAnaquel9.Text = "0" : End If
            If txtPasillo6.Text = "" Then
                txtPasillo6.Text = "0" : End If
            If txtPasillo1.Text = "" Then
                txtPasillo1.Text = "0" : End If
            If txtPasillo15.Text = "" Then
                txtPasillo15.Text = "0" : End If
            If txtExhibicionesPasillo1.Text = "" Then
                txtExhibicionesPasillo1.Text = "0" : End If
            If txtExhibicionesPasillo2.Text = "" Then
                txtExhibicionesPasillo2.Text = "0" : End If
            If txtExhibicionesPasillo3.Text = "" Then
                txtExhibicionesPasillo3.Text = "0" : End If
            If txtCaliente1.Text = "" Then
                txtCaliente1.Text = "0" : End If
            If txtCaliente15.Text = "" Then
                txtCaliente15.Text = "0" : End If
            If txtCaliente3.Text = "" Then
                txtCaliente3.Text = "0" : End If
            If txtCaliente5.Text = "" Then
                txtCaliente5.Text = "0" : End If
            If txtExhibicionesCaliente1.Text = "" Then
                txtExhibicionesCaliente1.Text = "0" : End If
            If txtExhibicionesCaliente2.Text = "" Then
                txtExhibicionesCaliente2.Text = "0" : End If
            If txtExhibicionesCaliente3.Text = "" Then
                txtExhibicionesCaliente3.Text = "0" : End If
            If txtEntrada5.Text = "" Then
                txtEntrada5.Text = "0" : End If
            If txtEntrada16.Text = "" Then
                txtEntrada16.Text = "0" : End If
            If txtExhibicionesEntrada1.Text = "" Then
                txtExhibicionesEntrada1.Text = "0" : End If
            If txtExhibicionesEntrada2.Text = "" Then
                txtExhibicionesEntrada2.Text = "0" : End If
            If txtExhibicionesEntrada3.Text = "" Then
                txtExhibicionesEntrada3.Text = "0" : End If
        End If
    End Sub

    Public Function VerExhibiciones(ByVal Folio As Integer, ByVal IDExhibidor As Integer, ByVal IDPunto As Integer, _
                                    ByVal CajaTexto As TextBox, ByVal Producto1 As DropDownList, _
                                    ByVal Producto2 As DropDownList, ByVal Producto3 As DropDownList) As Integer
        Dim TablaExh As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * from AS_Puntos_Interrupcion_Historial_Det " & _
                                               "where folio_historial=" & Folio & " and id_punto=" & IDPunto & " " & _
                                               "and id_exhibidor=" & IDExhibidor & "")
        If TablaExh.Rows.Count > 0 Then
            CajaTexto.Text = TablaExh.Rows(0)("cantidad")
            Producto1.SelectedValue = TablaExh.Rows(0)("id_producto_A")
            Producto2.SelectedValue = TablaExh.Rows(0)("id_producto_B")
            Producto3.SelectedValue = TablaExh.Rows(0)("id_producto_C")
        End If

        TablaExh.Dispose()
    End Function

    Public Function VerExhibicionesOtras(ByVal Folio As Integer, ByVal Orden As Integer, ByVal IDExhibidor As Integer, _
                                ByVal IDPunto As Integer, ByVal TipoExhibicion As DropDownList, _
                                ByVal CajaTexto As TextBox, ByVal Producto1 As DropDownList, _
                                ByVal Producto2 As DropDownList, ByVal Producto3 As DropDownList) As Integer
        Dim TablaExh As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                                  "select * from AS_Puntos_Interrupcion_Historial_Det " & _
                                     "where folio_historial=" & Folio & " and id_punto=" & IDPunto & " " & _
                                     "and orden =" & Orden & "")
        If TablaExh.Rows.Count > 0 Then
            TipoExhibicion.SelectedValue = TablaExh.Rows(0)("id_exhibidor")
            CajaTexto.Text = TablaExh.Rows(0)("cantidad")
            Producto1.SelectedValue = TablaExh.Rows(0)("id_producto_A")
            Producto2.SelectedValue = TablaExh.Rows(0)("id_producto_B")
            Producto3.SelectedValue = TablaExh.Rows(0)("id_producto_C")
        End If

        TablaExh.Dispose()
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Dim PDQ, Floor As Integer
        btnGuardar.Enabled = False
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                                  "select * from AS_Historial " & _
                                                  "WHERE folio_historial=" & FolioAct & "")
        If chkExhibidores13.Checked = True Then
            PDQ = 1 : Else
            PDQ = 0 : End If
        If chkExhibidores17.Checked = True Then
            Floor = 1 : Else
            Floor = 0 : End If

        If tabla.Rows.Count > 0 Then
            BD.Execute(ConexionMars.localSqlServer, _
                       "execute AS_EditarHistorial_2 " & _
                       "" & FolioAct & "," & cmbComentarios.SelectedValue & "," & _
                       "'" & txtComentarios.Text & "','" & PDQ & "','" & Floor & "'")
            
            '@id_medida=rdMedidas.SelectedValue
            '@fecha_medida=ISODates.Dates.SQLServerDate(CDate(txtFechaMedicion.Text))

            GuardarProductos(FolioAct)
            CambioEstatus(FolioAct)
        Else
            FolioAct = BD.RT.Execute(ConexionMars.localSqlServer, _
                       "execute AS_CrearHistorial_2 " & _
                       "'" & IDUsuario & "'," & IDPeriodo & ",'" & IDQuincena & "'," & _
                       "" & IDTienda & "," & cmbComentarios.SelectedValue & "," & _
                       "'" & txtComentarios.Text & "','" & PDQ & "','" & Floor & "'")
            '@id_medida=rdMedidas.SelectedValue
            '@fecha_medida=ISODates.Dates.SQLServerDate(CDate(txtFechaMedicion.Text))

            GuardarProductos(FolioAct)
            CambioEstatus(FolioAct)
        End If

        Tabla.Dispose()

        Response.Redirect("RutaMarsAS.aspx")
    End Sub

    Private Function GuardarProductos(ByVal folio As Integer) As Boolean
        GuardaFrentes(folio)
        GuardaExhibiciones(folio)
    End Function

    Private Function GuardaFrentes(ByVal folio As Integer) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * from AS_Segmentos_Historial_Det " & _
                                               "WHERE folio_historial=" & folio & "")
        If Tabla.Rows.Count = 1 Then
            BD.Execute(ConexionMars.localSqlServer, _
                       "execute AS_EditarHistorial_Segmentos_Det_2 " & folio & ", " & _
                       "" & txtFrentes1.Text & "," & txtFrentes2.Text & "," & _
                       "" & txtFrentes3.Text & "," & txtFrentes4.Text & "," & _
                       "" & txtFrentes5.Text & "," & txtFrentes6.Text & "," & _
                       "" & txtFrentes7.Text & "," & txtFrentes8.Text & "," & _
                       "" & txtFrentes9.Text & "," & txtFrentes10.Text & "," & _
                       "" & txtFrentes11.Text & "," & txtFrentes12.Text & "," & _
                       "" & txtFrentes13.Text & "," & txtFrentes14.Text & "," & _
                       "" & txtFrentes15.Text & "," & txtFrentes16.Text & "")
        Else
            BD.Execute(ConexionMars.localSqlServer, _
                       "execute AS_CrearHistorial_Segmentos_Det_2 " & folio & ", " & _
                       "" & txtFrentes1.Text & "," & txtFrentes2.Text & "," & _
                       "" & txtFrentes3.Text & "," & txtFrentes4.Text & "," & _
                       "" & txtFrentes5.Text & "," & txtFrentes6.Text & "," & _
                       "" & txtFrentes7.Text & "," & txtFrentes8.Text & "," & _
                       "" & txtFrentes9.Text & "," & txtFrentes10.Text & "," & _
                       "" & txtFrentes11.Text & "," & txtFrentes12.Text & "," & _
                       "" & txtFrentes13.Text & "," & txtFrentes14.Text & "," & _
                       "" & txtFrentes15.Text & "," & txtFrentes16.Text & "")
        End If

        Tabla.Dispose()
    End Function

    Private Function GuardaExhibiciones(ByVal folio As Integer) As Boolean
        GuardaExhibicion(folio, 0, 12, 1, txtAnaquel12, cmbProductoAnaquel12A, cmbProductoAnaquel12B, cmbProductoAnaquel12C)
        GuardaExhibicion(folio, 0, 11, 1, txtAnaquel11, cmbProductoAnaquel11A, cmbProductoAnaquel11B, cmbProductoAnaquel11C)
        GuardaExhibicion(folio, 0, 10, 1, txtAnaquel10, cmbProductoAnaquel10A, cmbProductoAnaquel10B, cmbProductoAnaquel10C)
        GuardaExhibicion(folio, 0, 9, 1, txtAnaquel9, cmbProductoAnaquel9A, cmbProductoAnaquel9B, cmbProductoAnaquel9C)

        GuardaExhibicion(folio, 0, 6, 2, txtPasillo6, cmbProductoPasillo6A, cmbProductoPasillo6B, cmbProductoPasillo6C)
        GuardaExhibicion(folio, 0, 1, 2, txtPasillo1, cmbProductoPasillo1A, cmbProductoPasillo1B, cmbProductoPasillo1C)
        GuardaExhibicion(folio, 0, 15, 2, txtPasillo15, cmbProductoPasillo15A, cmbProductoPasillo15B, cmbProductoPasillo15C)
        GuardaExhibicion(folio, 1, cmbExhibicionesPasillo1.SelectedValue, 2, txtExhibicionesPasillo1, cmbProductoExhibicionesPasillo1A, cmbProductoExhibicionesPasillo1B, cmbProductoExhibicionesPasillo1C)
        GuardaExhibicion(folio, 2, cmbExhibicionesPasillo2.SelectedValue, 2, txtExhibicionesPasillo2, cmbProductoExhibicionesPasillo2A, cmbProductoExhibicionesPasillo2B, cmbProductoExhibicionesPasillo2C)
        GuardaExhibicion(folio, 3, cmbExhibicionesPasillo3.SelectedValue, 2, txtExhibicionesPasillo3, cmbProductoExhibicionesPasillo3A, cmbProductoExhibicionesPasillo3B, cmbProductoExhibicionesPasillo3C)

        GuardaExhibicion(folio, 0, 1, 3, txtCaliente1, cmbProductoCaliente1A, cmbProductoCaliente1B, cmbProductoCaliente1C)
        GuardaExhibicion(folio, 0, 15, 3, txtCaliente15, cmbProductoCaliente15A, cmbProductoCaliente15B, cmbProductoCaliente15C)
        GuardaExhibicion(folio, 0, 3, 3, txtCaliente3, cmbProductoCaliente3A, cmbProductoCaliente3B, cmbProductoCaliente3C)
        GuardaExhibicion(folio, 0, 5, 3, txtCaliente5, cmbProductoCaliente5A, cmbProductoCaliente5B, cmbProductoCaliente5C)
        GuardaExhibicion(folio, 1, cmbExhibicionesCaliente1.SelectedValue, 3, txtExhibicionesCaliente1, cmbProductoExhibicionesCaliente1A, cmbProductoExhibicionesCaliente1B, cmbProductoExhibicionesCaliente1C)
        GuardaExhibicion(folio, 2, cmbExhibicionesCaliente2.SelectedValue, 3, txtExhibicionesCaliente2, cmbProductoExhibicionesCaliente2A, cmbProductoExhibicionesCaliente2B, cmbProductoExhibicionesCaliente2C)
        GuardaExhibicion(folio, 3, cmbExhibicionesCaliente3.SelectedValue, 3, txtExhibicionesCaliente3, cmbProductoExhibicionesCaliente3A, cmbProductoExhibicionesCaliente3B, cmbProductoExhibicionesCaliente3C)

        GuardaExhibicion(folio, 0, 5, 4, txtEntrada5, cmbProductoEntrada5A, cmbProductoEntrada5B, cmbProductoEntrada5C)
        GuardaExhibicion(folio, 0, 16, 4, txtEntrada16, cmbProductoEntrada16A, cmbProductoEntrada16B, cmbProductoEntrada16C)
        GuardaExhibicion(folio, 1, cmbExhibicionesEntrada1.SelectedValue, 4, txtExhibicionesEntrada1, cmbProductoExhibicionesEntrada1A, cmbProductoExhibicionesEntrada1B, cmbProductoExhibicionesEntrada1C)
        GuardaExhibicion(folio, 2, cmbExhibicionesEntrada2.SelectedValue, 4, txtExhibicionesEntrada2, cmbProductoExhibicionesEntrada2A, cmbProductoExhibicionesEntrada2B, cmbProductoExhibicionesEntrada2C)
        GuardaExhibicion(folio, 3, cmbExhibicionesEntrada3.SelectedValue, 4, txtExhibicionesEntrada3, cmbProductoExhibicionesEntrada3A, cmbProductoExhibicionesEntrada3B, cmbProductoExhibicionesEntrada3C)

    End Function

    Private Function GuardaExhibicion(ByVal folio As Integer, ByVal Orden As Integer, _
                                      ByVal IDExhibidor As Integer, ByVal IDPunto As Integer, _
                                      ByVal Cantidad As TextBox, _
                                      ByVal ProductoA As DropDownList, _
                                      ByVal ProductoB As DropDownList, _
                                      ByVal ProductoC As DropDownList) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * from AS_Puntos_Interrupcion_Historial_Det " & _
                                               "WHERE folio_historial=" & FolioAct & " " & _
                                               "and id_exhibidor=" & IDExhibidor & " " & _
                                               "and id_punto=" & IDPunto & " ")
        If Tabla.Rows.Count > 0 Then
            If Cantidad.Text <> "0" Then
                BD.Execute(ConexionMars.localSqlServer, _
                           "execute AS_EditarHistorial_Punto_Det " & _
                           "" & folio & "," & Orden & "," & IDExhibidor & "," & IDPunto & "," & _
                           "" & Cantidad.Text & "," & ProductoA.SelectedValue & "," & _
                           "" & ProductoB.SelectedValue & "," & ProductoC.SelectedValue & "")
            Else
                BD.Execute(ConexionMars.localSqlServer, _
                           "DELETE FROM AS_Puntos_Interrupcion_Historial_Det " & _
                           "WHERE folio_historial=" & folio & " AND id_exhibidor=" & IDExhibidor & " " & _
                           "AND id_punto=" & IDPunto & " AND orden=" & Orden & "")
            End If
        Else
            If Cantidad.Text <> "0" Then
                BD.Execute(ConexionMars.localSqlServer, _
                           "execute AS_CrearHistorial_Punto_Det " & _
                           "" & folio & "," & Orden & "," & IDExhibidor & "," & IDPunto & "," & _
                           "" & Cantidad.Text & "," & ProductoA.SelectedValue & "," & _
                           "" & ProductoB.SelectedValue & "," & ProductoC.SelectedValue & "")
            End If
        End If

        Tabla.Dispose()
    End Function

    Private Function GuardaImplementacion(ByVal folio As Integer, ByVal Orden As Integer, _
                                          ByVal IDExhibidor As Integer, ByVal IDPunto As Integer, _
                                          ByVal Cantidad As TextBox, _
                                          ByVal ProductoA As DropDownList, _
                                          ByVal ProductoB As DropDownList, _
                                          ByVal ProductoC As DropDownList) As Boolean
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * from AS_Procesos_Historial_Det " & _
                                               "WHERE folio_historial=" & FolioAct & " " & _
                                               "and id_proceso=" & IDExhibidor & "")

        If tabla.Rows.Count > 0 Then
            If Cantidad.Text <> "0" Then
                BD.Execute(ConexionMars.localSqlServer, _
                           "execute AS_EditarHistorial_Punto_Det " & _
                           "" & folio & "," & Orden & "," & IDExhibidor & "," & IDPunto & "," & _
                           "" & Cantidad.Text & "," & ProductoA.SelectedValue & ", " & _
                           "" & ProductoB.SelectedValue & ", " & ProductoC.SelectedValue & "")
            Else
                BD.Execute(ConexionMars.localSqlServer, _
                           "DELETE FROM AS_Puntos_Interrupcion_Historial_Det " & _
                           "WHERE folio_historial=" & folio & " AND id_exhibidor=" & IDExhibidor & " " & _
                           "AND id_punto=" & IDPunto & " AND orden=" & Orden & "")
            End If
        Else
            If Cantidad.Text <> "0" Then
                BD.Execute(ConexionMars.localSqlServer, _
                           "execute AS_CrearHistorial_Punto_Det " & _
                           "" & folio & "," & Orden & "," & IDExhibidor & "," & IDPunto & "," & _
                           "" & Cantidad.Text & "," & ProductoA.SelectedValue & ", " & _
                           "" & ProductoB.SelectedValue & ", " & ProductoC.SelectedValue & "")
            End If
        End If

        Tabla.Dispose()
    End Function

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("javascript:history.go(-1)")
    End Sub

    Private Function CambioEstatus(ByVal folio As Integer) As Boolean
        Datos()

        BD.Execute(ConexionMars.localSqlServer, _
                   "UPDATE AS_Rutas_Eventos SET estatus_anaquel=1 " & _
                   "FROM AS_Rutas_Eventos " & _
                   "WHERE orden=" & IDPeriodo & " AND id_usuario='" & IDUsuario & "' " & _
                   "AND id_tienda=" & IDTienda & " AND id_quincena='" & IDQuincena & "'")
    End Function

    Sub Datos()
        IDTienda = Request.Params("tienda")
        IDQuincena = Request.Params("quincena")
        IDUsuario = Request.Params("usuario")
        IDPeriodo = Request.Params("periodo")
        FolioAct = Request.Params("folio")
    End Sub

    Protected Sub btnGuardarImplementacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarImplementacion.Click
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * from AS_Procesos_Historial_Det " & _
                                    "WHERE orden=" & IDPeriodo & " " & _
                                    "AND id_quincena='" & IDQuincena & "' " & _
                                    "AND id_tienda=" & IDTienda & " " & _
                                    "AND id_usuario='" & IDUsuario & "' " & _
                                    "AND id_proceso=" & lstImplementacion.SelectedValue & " ")
        Dim Material1, Material2, Material3, Material4, Material5, _
        Material6, Material7, Material8, Material9 As Integer

        If chkMaterialPOP1.Checked = True Then
            Material1 = 1 : Else
            Material1 = 0 : End If
        If chkMaterialPOP2.Checked = True Then
            Material2 = 1 : Else
            Material2 = 0 : End If
        If chkMaterialPOP3.Checked = True Then
            Material3 = 1 : Else
            Material3 = 0 : End If
        If chkMaterialPOP4.Checked = True Then
            Material4 = 1 : Else
            Material4 = 0 : End If
        If chkMaterialPOP5.Checked = True Then
            Material5 = 1 : Else
            Material5 = 0 : End If
        If chkMaterialPOP6.Checked = True Then
            Material6 = 1 : Else
            Material6 = 0 : End If
        If chkMaterialPOP7.Checked = True Then
            Material7 = 1 : Else
            Material7 = 0 : End If
        If chkMaterialPOP8.Checked = True Then
            Material8 = 1 : Else
            Material8 = 0 : End If
        If chkMaterialPOP9.Checked = True Then
            Material9 = 1 : Else
            Material9 = 0 : End If

        'SQLNuevo.Parameters.AddWithValue("@Caducidad", DBNull.Value)

        If Tabla.Rows.Count > 0 Then
            BD.Execute(ConexionMars.localSqlServer, _
                       "execute AS_EditarHistorial_Proceso " & IDPeriodo & ",'" & IDQuincena & "'," & _
                       "'" & IDUsuario & "'," & IDTienda & ",'" & lstImplementacion.SelectedValue & "'," & _
                       "'" & ISODates.Dates.SQLServerDate(CDate(txtFechaImplementacion.Text)) & "'," & _
                       "" & Material1 & "," & Material2 & "," & Material3 & "," & Material4 & ", " & _
                       "" & Material5 & "," & Material6 & "," & Material7 & "," & Material8 & ", " & _
                       "" & Material9 & ",'" & txtOtros.Text & "'")
            GuardarProductos(FolioAct)
            CambioEstatus(FolioAct)
        Else
            BD.Execute(ConexionMars.localSqlServer, _
                       "execute AS_CrearHistorial_Proceso " & IDPeriodo & ",'" & IDQuincena & "'," & _
                       "'" & IDUsuario & "'," & IDTienda & ",'" & lstImplementacion.SelectedValue & "'," & _
                       "'" & ISODates.Dates.SQLServerDate(CDate(txtFechaImplementacion.Text)) & "'," & _
                       "" & Material1 & "," & Material2 & "," & Material3 & "," & Material4 & ", " & _
                       "" & Material5 & "," & Material6 & "," & Material7 & "," & Material8 & ", " & _
                       "" & Material9 & ",'" & txtOtros.Text & "'")
        End If

        Tabla.Dispose()
    End Sub

    Private Sub lstImplementacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstImplementacion.SelectedIndexChanged
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select * from AS_Procesos_Historial_Det " & _
                                    "WHERE orden=" & IDPeriodo & " " & _
                                    "AND id_quincena='" & IDQuincena & "' " & _
                                    "AND id_tienda=" & IDTienda & " " & _
                                    "AND id_usuario='" & IDUsuario & "' " & _
                                    "AND id_proceso=" & lstImplementacion.SelectedValue & " ")
        If Tabla.Rows.Count > 0 Then
            txtFechaImplementacion.Text = Format(Tabla.Rows(0)("fecha_implementacion"), "dd/MM/yyyy")
            If Tabla.Rows(0)("1") = 1 Then
                chkMaterialPOP1.Checked = True : Else
                chkMaterialPOP1.Checked = False : End If
            If Tabla.Rows(0)("2") = 1 Then
                chkMaterialPOP2.Checked = True : Else
                chkMaterialPOP2.Checked = False : End If
            If Tabla.Rows(0)("3") = 1 Then
                chkMaterialPOP3.Checked = True : Else
                chkMaterialPOP3.Checked = False : End If
            If Tabla.Rows(0)("4") = 1 Then
                chkMaterialPOP4.Checked = True : Else
                chkMaterialPOP4.Checked = False : End If
            If Tabla.Rows(0)("5") = 1 Then
                chkMaterialPOP5.Checked = True : Else
                chkMaterialPOP5.Checked = False : End If
            If Tabla.Rows(0)("6") = 1 Then
                chkMaterialPOP6.Checked = True : Else
                chkMaterialPOP6.Checked = False : End If
            If Tabla.Rows(0)("7") = 1 Then
                chkMaterialPOP7.Checked = True : Else
                chkMaterialPOP7.Checked = False : End If
            If Tabla.Rows(0)("8") = 1 Then
                chkMaterialPOP8.Checked = True : Else
                chkMaterialPOP8.Checked = False : End If
            If Tabla.Rows(0)("9") = 1 Then
                chkMaterialPOP9.Checked = True : Else
                chkMaterialPOP9.Checked = False : End If
            txtOtros.Text = Tabla.Rows(0)("otros")
        Else
            txtFechaImplementacion.Text = ""
            chkMaterialPOP1.Checked = False
            chkMaterialPOP2.Checked = False
            chkMaterialPOP3.Checked = False
            chkMaterialPOP4.Checked = False
            chkMaterialPOP5.Checked = False
            chkMaterialPOP6.Checked = False
            chkMaterialPOP7.Checked = False
            chkMaterialPOP8.Checked = False
            chkMaterialPOP9.Checked = False
            txtOtros.Text = ""
        End If

        Tabla.Dispose()
    End Sub
End Class