Imports System.Data.SqlClient

Partial Public Class ReporteInventariosTiendaNR
    Inherits System.Web.UI.Page

    Dim ProductoPropio, GramosPropio As Double
    Dim Campos(200), Columnas(200), SeleccionCampos, SeleccionColumna As String
    Dim ColumnasGrilla As Integer
    Dim PrecioGramoSYM, PrecioGramo As Double

    Sub SQLCombo()
        NR.SQLsCombo(cmbPeriodo.SelectedValue, cmbRegion.SelectedValue, cmbEstado.SelectedValue, _
                    cmbSupervisor.SelectedValue, cmbPromotor.SelectedValue, cmbFormato.SelectedValue, _
                    cmbCadena.SelectedValue)
    End Sub

    Public Function SeleccionProductos(ByVal Grupo As Integer) As Integer
        Using cnn As New SqlConnection(ConexionBerol.localSqlServer)
            Dim SQL As New SqlCommand("SELECT * FROM Productos WHERE tipo_producto= " & Grupo & " ", cnn)
            cnn.Open()
            Dim Data As New SqlDataAdapter(SQL)
            Dim Tabla As New DataTable
            Data.Fill(Tabla)

            If Tabla.Rows.Count > 1 Then
                For i = 0 To Tabla.Rows.Count - 1
                    If i = 0 Then
                        Campos(0) = "[" & Tabla.Rows(0)("id_producto") & "]" : Else
                        Campos(i) = ",[" & Tabla.Rows(i)("id_producto") & "]" : End If
                Next i

                For i = 0 To Tabla.Rows.Count - 1
                    If i = 0 Then
                        Columnas(0) = ",CASE WHEN ISNULL([" & Tabla.Rows(0)("id_producto") & "],0)= 1 then 'SI' ELSE 'NO' END '" & Tabla.Rows(0)("nombre_producto") & "'" : Else
                        Columnas(i) = ",CASE WHEN ISNULL([" & Tabla.Rows(i)("id_producto") & "],0)= 1 then 'SI' ELSE 'NO' END '" & Tabla.Rows(i)("nombre_producto") & "'" : End If
                Next i

                For i = 0 To Tabla.Rows.Count - 1
                    SeleccionCampos = SeleccionCampos + Campos(i)
                    SeleccionColumna = SeleccionColumna + Columnas(i)
                Next i
            End If

            SQL.Dispose()
            Data.Dispose()
            Tabla.Dispose()
            cnn.Dispose()
            cnn.Close()
        End Using
    End Function

    Public Function VerGrilla(ByVal Grilla As GridView, ByVal TipoGrupo As Integer) As Integer
        Dim SQL As String
        Dim RegionSQL, EstadoSQL, SupervisorSQL, PromotorSQL, CadenaSQL, FormatoSQL, TiendaSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("H.id_region", cmbRegion.SelectedValue)
            EstadoSQL = Acciones.Slc.cmb("H.id_estado", cmbEstado.SelectedValue)
            SupervisorSQL = Acciones.Slc.cmb("H.id_supervisor", cmbSupervisor.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("H.id_cadena", cmbCadena.SelectedValue)
            FormatoSQL = Acciones.Slc.cmb("H.id_formato", cmbFormato.SelectedValue)
            TiendaSQL = Acciones.Slc.cmb("H.id_tienda", cmbTienda.SelectedValue)

            If Tipo_usuario = 7 Then
                Berol.CuentaClave = "INNER JOIN (select * FROM Usuarios_cadenas WHERE id_usuario='" & HttpContext.Current.User.Identity.Name & "' )as CC " & _
                                "ON CC.id_cadena=H.id_cadena " : Else
                Berol.CuentaClave = "" : End If

            SeleccionCampos = ""
            SeleccionColumna = ""
            SeleccionProductos(TipoGrupo)

            If SeleccionColumna = "" Then
                SQL = ""
                Grilla.Visible = False
                Exit Function
            Else
                SQL = "SELECT H.nombre_region as 'Región', H.ciudad as 'Ciudad',H.id_usuario as 'Promotor', " & _
                        "H.supervisor as 'Supervisor', H.nombre_cadena as 'Cadena',H.nombre_formato as 'Formato', " & _
                        "H.codigo as 'ID',H.nombre as 'Tienda' " & _
                        " " + SeleccionColumna + " " & _
                        "FROM View_Historial_NR as H  " & _
                        "INNER JOIN (SELECT folio_historial,id_producto,(inv_piso+inv_bodega)inventarios " & _
                        "FROM NR_Historial_Inventarios_Det as H) PVT " & _
                        "PIVOT(SUM(inventarios) FOR id_producto  " & _
                        "IN (" + SeleccionCampos + ")) AS HDET " & _
                        "ON HDET.folio_historial=H.folio_historial  " & _
                        " " + Berol.CuentaClave + " " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + EstadoSQL + " " & _
                        " " + SupervisorSQL + PromotorSQL + " " & _
                        " " + CadenaSQL + FormatoSQL + TiendaSQL + " " & _
                        " " + TiendaSQL + " " & _
                        "ORDER BY H.nombre_region, H.ciudad, H.nombre_cadena, H.nombre_formato, H.nombre"

                CargaGrilla(ConexionBerol.localSqlServer, SQL, Grilla)
            End If
        End If

    End Function

    Sub CargarReporte()
        If cmbPeriodo.SelectedValue <> "" Then
            VerGrilla(gridReporte, 1)
            VerGrilla(gridReporte2, 2)
            VerGrilla(gridReporte3, 3)
            VerGrilla(gridReporte4, 4)
            VerGrilla(gridReporte5, 5)
            VerGrilla(gridReporte6, 6)
        Else
            gridReporte.Visible = False
            gridReporte2.Visible = False
            gridReporte3.Visible = False
            gridReporte4.Visible = False
            gridReporte5.Visible = False
            gridReporte6.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.PeriodoSQLCmb, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.FormatoSQLCmb, "nombre_formato", "id_formato", cmbFormato)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)
        End If
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.FormatoSQLCmb, "nombre_formato", "id_formato", cmbFormato)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.FormatoSQLCmb, "nombre_formato", "id_formato", cmbFormato)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub lnkExportar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.PnlGrid.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.PnlGrid)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte inventarios por tienda " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEstado.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.SupervisorSQLCmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.FormatoSQLCmb, "nombre_formato", "id_formato", cmbFormato)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Private Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.FormatoSQLCmb, "nombre_formato", "id_formato", cmbFormato)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Private Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.FormatoSQLCmb, "nombre_formato", "id_formato", cmbFormato)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Private Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCadena.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.FormatoSQLCmb, "nombre_formato", "id_formato", cmbFormato)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Private Sub cmbFormato_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFormato.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, Berol.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Private Sub cmbTienda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTienda.SelectedIndexChanged
        SQLCombo()

        CargarReporte()
    End Sub
End Class