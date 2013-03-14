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
Imports InfoSoftGlobal

Partial Public Class ReporteExhibicionesFluid
    Inherits System.Web.UI.Page

    Sub SQLCombo()
        Fluid.SQLsCombo(cmbPeriodo.SelectedValue, cmbRegion.SelectedValue, cmbEstado.SelectedValue, _
                     cmbPromotor.SelectedValue, cmbDistribuidor.SelectedValue)
    End Sub

    Sub CargarReporte()
        Dim RegionSQL, EstadoSQL, PromotorSQL, CadenaSQL, TiendaSQL, TiendaSQL2 As String

        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)
            EstadoSQL = Acciones.Slc.cmb("ES.id_estado", cmbEstado.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("CAD.id_cadena", cmbDistribuidor.SelectedValue)
            TiendaSQL = Acciones.Slc.cmb("TI.id_tienda", cmbTienda.SelectedValue)
            TiendaSQL2 = Acciones.Slc.cmb("H.id_tienda", cmbTienda.SelectedValue)

            CargaGrilla(ConexionFluidmaster.localSqlServer, _
                        "SELECT REG.nombre_region,SUM(HDET.cantidad)fluidmaster, SUM(ISNULL(HDET2.cantidad,0))competencia,  " & _
                        "CASE WHEN SUM(ISNULL(HDET2.cantidad,0))<>0 then  " & _
                        "100*SUM(HDET.cantidad)/(SUM(HDET.cantidad)+SUM(HDET2.cantidad))else 100 end por_fluidmaster, " & _
                        "CASE WHEN SUM(ISNULL(HDET2.cantidad,0))<>0 then " & _
                        "100*SUM(HDET2.cantidad)/(SUM(HDET.cantidad)+SUM(HDET2.cantidad))else 0 end por_competencia " & _
                        "from Distribuidor_Historial as H " & _
                        "INNER JOIN View_Tiendas as TI ON TI.id_tienda= H.id_tienda " & _
                        "INNER JOIN Regiones as REG ON REG.id_region=TI.id_region " & _
                        "INNER JOIN Distribuidor_Exhibidores_Det as HDET ON HDET.folio_historial=H.folio_historial " & _
                        "FULL JOIN Distribuidor_Exhibidores_Marcas_Det as HDET2 ON HDET2.folio_historial=H.folio_historial " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + " " & _
                        "GROUP BY REG.nombre_region  " & _
                        "UNION ALL " & _
                        "SELECT REG.nombre_region,SUM(HDET.cantidad)fluidmaster, SUM(ISNULL(HDET2.cantidad,0))competencia,  " & _
                        "CASE WHEN SUM(ISNULL(HDET2.cantidad,0))<>0 then  " & _
                        "100*SUM(HDET.cantidad)/(SUM(HDET.cantidad)+SUM(HDET2.cantidad))else 100 end por_fluidmaster, " & _
                        "CASE WHEN SUM(ISNULL(HDET2.cantidad,0))<>0 then " & _
                        "100*SUM(HDET2.cantidad)/(SUM(HDET.cantidad)+SUM(HDET2.cantidad))else 0 end por_competencia " & _
                        "from Subdistribuidor_Historial as H " & _
                        "INNER JOIN Regiones as REG ON REG.id_region=H.id_region " & _
                        "INNER JOIN Subdistribuidor_Exhibidores_Det as HDET ON HDET.folio_historial=H.folio_historial " & _
                        "FULL JOIN Subdistribuidor_Exhibidores_Marcas_Det as HDET2 ON HDET2.folio_historial=H.folio_historial " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + " " & _
                        "GROUP BY REG.nombre_region ORDER BY nombre_region ", _
                        Me.gridResumen)

            CargaGrilla(ConexionFluidmaster.localSqlServer, _
                        "SELECT H.id_usuario,TI.ciudad,REG.nombre_region,ES.nombre_estado,CAD.nombre_cadena,TI.nombre,'Distribución' tipo_tienda, " & _
                        "HDET.[1],HDET.[2],HDET.[3],HDET.[4],HDET.[5],HDET.[6], " & _
                        "HDET_Comp.[1]C_1,HDET_Comp.[2]C_2,HDET_Comp.[3]C_3,HDET_Comp.[4]C_4, " & _
                        "HDET_Comp.[5]C_5,HDET_Comp.[6]C_6,HDET_Comp.[7]C_7,HDET_Comp.[8]C_8, " & _
                        "fluidmaster, competencia, por_fluidmaster, por_competencia " & _
                        "FROM Distribuidor_Historial as H " & _
                        "INNER JOIN (select folio_historial, id_exhibidor, cantidad " & _
                        "FROM Distribuidor_Exhibidores_Det) PVT " & _
                        "PIVOT(SUM(cantidad) FOR id_exhibidor " & _
                        "IN([1],[2],[3],[4],[5],[6]))as HDET " & _
                        "ON HDET.folio_historial=H.folio_historial  " & _
                        "INNER JOIN (select folio_historial, id_marca, cantidad " & _
                        "FROM Distribuidor_Exhibidores_Marcas_Det) PVT PIVOT(SUM(cantidad) FOR id_marca " & _
                        "IN([1],[2],[3],[4],[5],[6],[7],[8]))as HDET_Comp " & _
                        "ON HDET_Comp.folio_historial=H.folio_historial  " & _
                        "INNER JOIN Tiendas as TI ON TI.id_tienda= H.id_tienda " & _
                        "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena=TI.id_cadena " & _
                        "INNER JOIN Estados as ES ON TI.id_estado=ES.id_estado " & _
                        "INNER JOIN Regiones as REG ON REG.id_region=TI.id_region " & _
                        "INNER JOIN(SELECT H.id_tienda,SUM(HDET.cantidad)fluidmaster, SUM(ISNULL(HDET2.cantidad,0))competencia,  " & _
                        "CASE WHEN SUM(ISNULL(HDET2.cantidad,0))<>0 then  " & _
                        "100*SUM(HDET.cantidad)/(SUM(HDET.cantidad)+SUM(HDET2.cantidad))else 100 end por_fluidmaster, " & _
                        "CASE WHEN SUM(ISNULL(HDET2.cantidad,0))<>0 then " & _
                        "100*SUM(HDET2.cantidad)/(SUM(HDET.cantidad)+SUM(HDET2.cantidad))else 0 end por_competencia " & _
                        "from Distribuidor_Historial as H " & _
                        "INNER JOIN Tiendas as TI ON TI.id_tienda= H.id_tienda " & _
                        "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena=TI.id_cadena " & _
                        "INNER JOIN Estados as ES ON TI.id_estado=ES.id_estado " & _
                        "INNER JOIN Regiones as REG ON REG.id_region=TI.id_region " & _
                        "INNER JOIN (select folio_historial, SUM(cantidad)cantidad " & _
                        "FROM Distribuidor_Exhibidores_Det GROUP BY folio_historial) as HDET ON HDET.folio_historial=H.folio_historial " & _
                        "FULL JOIN (select folio_historial, SUM(cantidad)cantidad " & _
                        "FROM Distribuidor_Exhibidores_Marcas_Det GROUP BY folio_historial) as HDET2 ON HDET2.folio_historial=H.folio_historial " & _
                        "WHERE H.id_periodo = " & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + EstadoSQL + " " & _
                        " " + PromotorSQL + CadenaSQL + TiendaSQL + " " & _
                        "GROUP BY H.id_tienda)HDET2 ON H.id_tienda =HDET2.id_tienda " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + EstadoSQL + " " & _
                        " " + PromotorSQL + CadenaSQL + TiendaSQL + " " & _
                        "UNION ALL " & _
                        "SELECT H.id_usuario,H.ciudad,''nombre_region,ES.nombre_estado,CAD.nombre_cadena,H.tienda as nombre, 'Subdistribucion' tipo_tienda, " & _
                        "HDET.[1],HDET.[2],HDET.[3],HDET.[4],HDET.[5],HDET.[6], " & _
                        "HDET_Comp.[1]C_1,HDET_Comp.[2]C_2,HDET_Comp.[3]C_3,HDET_Comp.[4]C_4, " & _
                        "HDET_Comp.[5]C_5,HDET_Comp.[6]C_6,HDET_Comp.[7]C_7,HDET_Comp.[8]C_8, " & _
                        "fluidmaster, competencia, por_fluidmaster, por_competencia " & _
                        "FROM Subdistribuidor_Historial as H " & _
                        "INNER JOIN Regiones as REG ON REG.id_region=H.id_region " & _
                        "INNER JOIN Estados as ES ON H.id_estado=ES.id_estado " & _
                        "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena=H.id_cadena " & _
                        "INNER JOIN (select folio_historial, id_exhibidor, cantidad " & _
                        "FROM Subdistribuidor_Exhibidores_Det) PVT " & _
                        "PIVOT(SUM(cantidad) FOR id_exhibidor " & _
                        "IN([1],[2],[3],[4],[5],[6]))as HDET " & _
                        "ON HDET.folio_historial=H.folio_historial  " & _
                        "INNER JOIN (select folio_historial, id_marca, cantidad " & _
                        "FROM Subdistribuidor_Exhibidores_Marcas_Det) PVT PIVOT(SUM(cantidad) FOR id_marca " & _
                        "IN([1],[2],[3],[4],[5],[6],[7],[8]))as HDET_Comp " & _
                        "ON HDET_Comp.folio_historial=H.folio_historial  " & _
                        "INNER JOIN(SELECT H.tienda,SUM(HDET.cantidad)fluidmaster, SUM(ISNULL(HDET2.cantidad,0))competencia,  " & _
                        "CASE WHEN SUM(ISNULL(HDET2.cantidad,0))<>0 then  " & _
                        "100*SUM(HDET.cantidad)/(SUM(HDET.cantidad)+SUM(HDET2.cantidad))else 100 end por_fluidmaster, " & _
                        "CASE WHEN SUM(ISNULL(HDET2.cantidad,0))<>0 then " & _
                        "100*SUM(HDET2.cantidad)/(SUM(HDET.cantidad)+SUM(HDET2.cantidad))else 0 end por_competencia " & _
                        "from Subdistribuidor_Historial as H " & _
                        "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena=H.id_cadena " & _
                        "INNER JOIN Estados as ES ON H.id_estado=ES.id_estado " & _
                        "INNER JOIN Regiones as REG ON REG.id_region=H.id_region " & _
                        "INNER JOIN (select folio_historial, SUM(cantidad)cantidad " & _
                        "FROM Subdistribuidor_Exhibidores_Det GROUP BY folio_historial) as HDET ON HDET.folio_historial=H.folio_historial " & _
                        "FULL JOIN (select folio_historial, SUM(cantidad)cantidad " & _
                        "FROM Subdistribuidor_Exhibidores_Marcas_Det GROUP BY folio_historial) as HDET2 ON HDET2.folio_historial=H.folio_historial " & _
                        "WHERE H.id_periodo = " & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + EstadoSQL + " " & _
                        " " + PromotorSQL + CadenaSQL + TiendaSQL2 + " " & _
                        "GROUP BY H.tienda)HDET2 ON H.tienda =HDET2.tienda " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + EstadoSQL + " " & _
                        " " + PromotorSQL + CadenaSQL + TiendaSQL2 + " " & _
                        "ORDER BY nombre_region, nombre_cadena,nombre ", _
                        Me.gridReporte)
        Else
            gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.PeriodoSQLCmb, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbDistribuidor)
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)
        End If
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbDistribuidor)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbDistribuidor)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbDistribuidor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbDistribuidor)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

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
        Me.pnlGrid.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.pnlGrid)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte faltantes " + cmbPeriodo.SelectedItem.ToString() + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub cmbEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEstado.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.PromotorSQLCmb, "id_usuario", "id_usuario", cmbPromotor)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbDistribuidor)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, Fluidmaster.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarReporte()
    End Sub

    Private Sub gridReporte_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridReporte.RowDataBound
        'Select Case e.Row.RowType
        '    Case DataControlRowType.Header
        '        For i = 0 To 5
        '            e.Row.Cells(i).Visible = True
        '        Next i

        '        e.Row.Cells(5).ColumnSpan = 17

        '        For i = 6 To 21
        '            e.Row.Cells(i).Visible = False : Next i

        '        e.Row.Cells(5).Controls.Clear()
        '        e.Row.Cells(5).Text = "<table style='width:1530px; font-weight: 700; color: #FFFFFF;' border='0' cellpadding='0' cellspacing='0'>" & _
        '                              "    <tr height='17'>" & _
        '                              "        <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>500135 Flapper</td>" & _
        '                              "        <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>501135 Flapper</td>" & _
        '                              "        <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>502135 Flapper</td>" & _
        '                              "        <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>503 Flapper</td>" & _
        '                              "        <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>200CM135 Combo</td>" & _
        '                              "        <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>200AK133 Combo</td>" & _
        '                              "        <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>555C135 Combo</td>" & _
        '                              "        <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>200AM133 Válvula de llenado</td>" & _
        '                              "        <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>400A133 Válvula de llenado</td>" & _
        '                              "        <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>400LS133 Válvula de llenado</td>" & _
        '                              "        <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>747UK Válvula de llenado</td>" & _
        '                              "        <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>507A133 Válvula de descarga</td>" & _
        '                              "        <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>681135 Palanca</td>" & _
        '                              "        <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>242135 Empaque</td>" & _
        '                              "        <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>400LSR133 linea 400LS BLISTER 1</td>" & _
        '                              "        <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>400LSCR133 linea 400LS BLISTER 2</td>" & _
        '                              "        <td style='border: thin solid #FFFFFF; padding: inherit; text-align: center; width: 90px; color: #FFFFFF;'>400LSCLR133 linea 400LS BLISTER 3</td></tr></table>"
        'End Select
    End Sub

End Class