Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteCatalogacionHistoricoACSYM
    Inherits System.Web.UI.Page

   Dim TituloCampos, CamposCad, SumaTiendas(100), SumaTotalTiendas As String
    Dim CampoTituloC(50), CamposTitulosC, CampoC(50), CamposC As String
    Dim CampoTitulo(50), CamposTitulos, Campo(50), Campos As String

    Sub SQLCombo()
        Combos_SYMAC.SQLsCombo(cmbPeriodo1.SelectedValue, cmbRegion.SelectedValue, _
                            cmbEstado.SelectedValue, cmbSupervisor.SelectedValue, _
                            cmbCiudad.SelectedValue, cmbPromotor.SelectedValue, _
                            cmbCadena.SelectedValue, "View_SYM_Catalogacion_Historial")
    End Sub

    Sub CargarReporte()
        Dim RegionSQL, CadenaSQL, EstadoSQL, SupervisorSQL, CiudadSQL, PromotorSQL, TiendaSQL, LineaSQL As String

        If cmbPeriodo1.SelectedValue <> "" And cmbPeriodo2.SelectedValue <> "" Then
            If cmbPeriodo1.SelectedValue <= cmbPeriodo2.SelectedValue Then
                gridReporte.Visible = True

                Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                                       "SELECT * FROM Catalogacion_Tipo_Productos")
                If Tabla.Rows.Count > 0 Then
                    For i = 0 To Tabla.Rows.Count - 1
                        CampoTituloC(i) = ",ISNULL([" & Tabla.Rows(i)("id_tipo") & "],0)as [" & Tabla.Rows(i)("id_tipo") & "]"
                        CamposTitulosC = CamposTitulosC + CampoTituloC(i)

                        If i = 0 Then
                            CampoC(i) = "[" & Tabla.Rows(i)("id_tipo") & "]" : Else
                            CampoC(i) = ",[" & Tabla.Rows(i)("id_tipo") & "]" : End If

                        CamposC = CamposC + CampoC(i)
                    Next i
                End If
                Tabla.Dispose()

                Dim Tabla2 As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                                       "SELECT * FROM AC_Periodos WHERE id_periodo >=" & cmbPeriodo1.SelectedValue & " AND id_periodo<=" & cmbPeriodo2.SelectedValue & "")
                If Tabla2.Rows.Count > 0 Then
                    For i = 0 To Tabla2.Rows.Count - 1
                        CampoTitulo(i) = ",ISNULL([" & Tabla2.Rows(i)("id_periodo") & "],0)as '" & Tabla2.Rows(i)("nombre_periodo") & "'"
                        CamposTitulos = CamposTitulos + CampoTitulo(i)

                        If i = 0 Then
                            Campo(i) = "[" & Tabla2.Rows(i)("id_periodo") & "]" : Else
                            Campo(i) = ",[" & Tabla2.Rows(i)("id_periodo") & "]" : End If

                        Campos = Campos + Campo(i)
                    Next i
                End If
                Tabla2.Dispose()

                RegionSQL = Acciones.Slc.cmb("id_region", cmbRegion.SelectedValue)
                EstadoSQL = Acciones.Slc.cmb("id_estado", cmbEstado.SelectedValue)
                SupervisorSQL = Acciones.Slc.cmb("id_supervisor", cmbSupervisor.SelectedValue)
                CiudadSQL = Acciones.Slc.cmb("ciudad", cmbCiudad.SelectedValue)
                CadenaSQL = Acciones.Slc.cmb("id_cadena", cmbCadena.SelectedValue)
                PromotorSQL = Acciones.Slc.cmb("id_usuario", cmbPromotor.SelectedValue)
                TiendaSQL = Acciones.Slc.cmb("id_tienda", cmbTienda.SelectedValue)

                Dim SQLGrafica As String = "SELECT DISTINCT id_periodo,nombre_periodo " & _
                                        " " + CamposTitulosC + " " & _
                                        "FROM(SELECT id_periodo,nombre_periodo,id_tipo,sum(catalogado)catalogado " & _
                                        "FROM View_SYM_Catalogacion_Historial  " & _
                                        "WHERE id_periodo >=" & cmbPeriodo1.SelectedValue & " AND id_periodo<=" & cmbPeriodo2.SelectedValue & " " & _
                                        " " + RegionSQL + " " & _
                                        " " + CadenaSQL + " " & _
                                        " " + EstadoSQL + " " & _
                                        " " + SupervisorSQL + " " & _
                                        " " + CiudadSQL + " " & _
                                        " " + PromotorSQL + " " & _
                                        " " + TiendaSQL + " " & _
                                        "GROUP BY id_periodo,nombre_periodo,id_tipo) AS Datos PIVOT(SUM(catalogado) " & _
                                        "FOR id_tipo IN (" + CamposC + ")) AS PivotTable " & _
                                        "ORDER BY id_periodo "

                Dim SQLGrilla As String = "SELECT DISTINCT nombre_region as 'División',nombre_cadena as 'Cadena',nombre_estado as 'Estado'," & _
                                        "id_supervisor as 'Supervisor',Ciudad,id_usuario as 'Promotor',nombre as 'Tienda'" & _
                                        " " + CamposTitulos + " " & _
                                        "FROM(SELECT DISTINCT nombre_region,nombre_cadena,nombre_estado,id_periodo, " & _
                                        "id_supervisor,id_usuario,ciudad,nombre,SUM(catalogado)catalogado " & _
                                        "FROM View_SYM_Catalogacion_Historial " & _
                                        "WHERE id_periodo >=" & cmbPeriodo1.SelectedValue & " AND id_periodo<=" & cmbPeriodo2.SelectedValue & " " & _
                                        " " + RegionSQL + " " & _
                                        " " + CadenaSQL + " " & _
                                        " " + EstadoSQL + " " & _
                                        " " + SupervisorSQL + " " & _
                                        " " + CiudadSQL + " " & _
                                        " " + PromotorSQL + " " & _
                                        " " + TiendaSQL + " " & _
                                        "GROUP BY id_periodo,nombre_region,nombre_cadena,nombre_estado,id_periodo, " & _
                                        "id_supervisor,id_usuario,ciudad,nombre) AS Datos PIVOT(SUM(catalogado)  " & _
                                        "FOR id_periodo IN (" + Campos + ")) AS PivotTable  " & _
                                        "ORDER BY nombre_region, nombre_cadena,nombre_estado,nombre"

                CargaGrilla(ConexionSYM.localSqlServer, SQLGrilla, Me.gridReporte)

                Dim TablaGraf As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, SQLGrafica)

                ''//CARGAR GRAFICA TARIMAS PROMOTOR
                Dim Linea1, Linea2, Linea3, Linea4 As New StringBuilder()
                Dim strXML As New StringBuilder()
                Dim strCategories As New StringBuilder()

                ''//GRAFICA FRENTES
                strXML.Append("<chart caption='Historico' lineThickness='1' showValues='0' formatNumberScale='0'  yAxisMinValue='20' rotateLabels='1' anchorRadius='2' divLineAlpha='20' divLineColor='CC3300' divLineIsDashed='1' showAlternateHGridColor='1' alternateHGridAlpha='5' alternateHGridColor='000000' shadowAlpha='40' numvdivlines='5' chartRightMargin='35' bgColor='FFFFFF,000000' bgAngle='270' bgAlpha='10,10'>")
                strCategories.Append("<categories>")
                Linea1.Append("<dataset seriesName='" & Tabla.Rows(0)("nombre_tipoproducto") & "'>")
                Linea2.Append("<dataset seriesName='" & Tabla.Rows(1)("nombre_tipoproducto") & "'>")
                Linea3.Append("<dataset seriesName='" & Tabla.Rows(2)("nombre_tipoproducto") & "'>")
                Linea4.Append("<dataset seriesName='" & Tabla.Rows(3)("nombre_tipoproducto") & "'>")

                For i = 0 To TablaGraf.Rows.Count - 1
                    strCategories.Append("<category name='" & TablaGraf.Rows(i)("nombre_periodo") & "' />")
                    Linea1.Append("<set value='" & TablaGraf.Rows(i)("1") & "' />")
                    Linea2.Append("<set value='" & TablaGraf.Rows(i)("2") & "' />")
                    Linea3.Append("<set value='" & TablaGraf.Rows(i)("3") & "' />")
                    Linea4.Append("<set value='" & TablaGraf.Rows(i)("4") & "' />")
                Next

                strCategories.Append("</categories>")
                Linea1.Append("</dataset>")
                Linea2.Append("</dataset>")
                Linea3.Append("</dataset>")
                Linea4.Append("</dataset>")

                strXML.Append(strCategories.ToString() & Linea1.ToString() & Linea2.ToString() & Linea3.ToString() & Linea4.ToString())
                strXML.Append("</chart>")

                Dim outPut As String = ""
                outPut = FusionCharts.RenderChartHTML("../../../../FusionCharts/MSLine.swf", "", strXML.ToString(), "chart1", "850", "350", False, False)

                PanelFS.Controls.Clear()
                PanelFS.Visible = True
                PanelFS.Controls.Add(New LiteralControl(outPut))

                TablaGraf.Dispose()
            Else
                PanelFS.Visible = False
                gridReporte.Visible = False
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PeriodoSQLCmb, "nombre_periodo", "id_periodo", cmbPeriodo1)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PeriodoSQLCmb, "nombre_periodo", "id_periodo", cmbPeriodo2)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.EstadoSQLcmb, "nombre_estado", "id_estado", cmbEstado)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.SupervisorSQLcmb, "supervisor", "id_supervisor", cmbSupervisor)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CiudadSQLCmb, "ciudad", "ciudad", cmbCiudad)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.EstadoSQLcmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.SupervisorSQLcmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CiudadSQLCmb, "ciudad", "ciudad", cmbCiudad)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCadena.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo1.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.EstadoSQLcmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.SupervisorSQLcmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CiudadSQLCmb, "ciudad", "ciudad", cmbCiudad)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbTienda_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbTienda.SelectedIndexChanged
        CargarReporte()
    End Sub

    Protected Sub lnkExportar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.gridReporte.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.gridReporte)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Historico Catalogacion " + cmbPeriodo1.SelectedItem.ToString + "al " + cmbPeriodo2.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEstado.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.SupervisorSQLcmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CiudadSQLCmb, "ciudad", "ciudad", cmbCiudad)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Private Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CiudadSQLCmb, "ciudad", "ciudad", cmbCiudad)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Private Sub cmbCiudad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCiudad.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Private Sub cmbPeriodo2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPeriodo2.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.EstadoSQLcmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.SupervisorSQLcmb, "supervisor", "id_supervisor", cmbSupervisor)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CiudadSQLCmb, "ciudad", "ciudad", cmbCiudad)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

End Class