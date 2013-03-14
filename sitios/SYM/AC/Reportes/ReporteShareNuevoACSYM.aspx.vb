Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteShareNuevoACSYM
    Inherits System.Web.UI.Page

    Dim Suma(7) As Double

    Sub SQLCombo()
        Combos_SYMAC.SQLsCombo(cmbPeriodo.SelectedValue, "", _
                             "", "", "", "", "", "View_SYM_Anaquel_Historial")
    End Sub

    Sub CargarReporte()
        CargarGrilla(gridReporte1, 1, 0)
        CargarGrilla(gridReporte2, 3, 0)
        CargarGrilla(gridReporte3, 2, 0)

        CargarGrilla(gridReporte1_R1, 1, 1)
        CargarGrilla(gridReporte2_R1, 3, 1)
        CargarGrilla(gridReporte3_R1, 2, 1)

        CargarGrilla(gridReporte1_R2, 1, 2)
        CargarGrilla(gridReporte2_R2, 3, 2)
        CargarGrilla(gridReporte3_R2, 2, 2)

        CargarGrilla(gridReporte1_R3, 1, 3)
        CargarGrilla(gridReporte2_R3, 3, 3)
        CargarGrilla(gridReporte3_R3, 2, 3)

        CargarGrilla(gridReporte1_R4, 1, 4)
        CargarGrilla(gridReporte2_R4, 3, 4)
        CargarGrilla(gridReporte3_R4, 2, 4)

        CargarGrilla(gridReporte1_R5, 1, 5)
        CargarGrilla(gridReporte2_R5, 3, 5)
        CargarGrilla(gridReporte3_R5, 2, 5)
    End Sub

    Private Function CargarGrilla(ByVal grilla As GridView, ByVal Tipo As Integer, ByVal Region As Integer) As Boolean
        Dim RegionSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("H.id_region", Region)

            CargaGrilla(ConexionSYM.localSqlServer, _
                        "SELECT id_marca,id_tipo,tipo_producto,nombre_marca, " & _
                        "convert(nvarchar(10),ROUND(ISNULL([1],0),2))+'%'[1], " & _
                        "convert(nvarchar(10),ROUND(ISNULL([2],0),2))+'%'[2], " & _
                        "convert(nvarchar(10),ROUND(ISNULL([3],0),2))+'%'[3], " & _
                        "convert(nvarchar(10),ROUND(ISNULL([4],0),2))+'%'[4], " & _
                        "convert(nvarchar(10),ROUND(ISNULL([5],0),2))+'%'[5], " & _
                        "convert(nvarchar(10),ROUND(ISNULL([6],0),2))+'%'[6], " & _
                        "convert(nvarchar(10),ROUND(ISNULL([7],0),2))+'%'[7], " & _
                        "convert(nvarchar(10),ROUND(ISNULL([100],0),2))+'%'[100] " & _
                        "FROM (select H.tipo_producto, H.id_tipo,H.nombre_marca,H.id_marca,CADS.id_cadena_share, " & _
                        "100*ROUND((sum(H.frentes)/Tiendas.Tiendas),2)/Totales.Total Porcentaje " & _
                        "from View_SYM_AC_Anaquel_HDET as H " & _
                        "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario=H.id_usuario " & _
                        "INNER JOIN Cadenas_Share as CADS ON CADS.id_cadena=H.id_cadena " & _
                        "INNER JOIN (select CADS.id_cadena_share, H.id_marca, COUNT(DISTINCT H.id_tienda)Tiendas " & _
                        "from View_SYM_AC_Anaquel_HDET as H " & _
                        "INNER JOIN Cadenas_Share as CADS ON CADS.id_cadena=H.id_cadena " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " and H.reporte_share=1  " & _
                        " " + RegionSQL + " " & _
                        "GROUP BY CADS.id_cadena_share,H.id_marca)Tiendas  " & _
                        "ON Tiendas.id_marca=H.id_marca AND Tiendas.id_cadena_share=CADS.id_cadena_share " & _
                        "INNER JOIN (SELECT id_tipo,id_cadena_share,SUM(Promedio)Total " & _
                        "FROM(select H.id_tipo,H.id_marca,CADS.id_cadena_share, " & _
                        "ROUND((sum(frentes)/Tiendas.Tiendas),2)Promedio " & _
                        "from View_SYM_AC_Anaquel_HDET as H " & _
                        "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario=H.id_usuario " & _
                        "INNER JOIN Cadenas_Share as CADS ON CADS.id_cadena=H.id_cadena " & _
                        "INNER JOIN (select CADS.id_cadena_share, H.id_marca, COUNT(DISTINCT H.id_tienda)Tiendas " & _
                        "from View_SYM_AC_Anaquel_HDET as H " & _
                        "INNER JOIN Cadenas_Share as CADS ON CADS.id_cadena=H.id_cadena " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " and H.reporte_share=1  " & _
                        " " + RegionSQL + " " & _
                        "GROUP BY CADS.id_cadena_share,H.id_marca,id_marca)Tiendas  " & _
                        "ON Tiendas.id_marca=H.id_marca AND Tiendas.id_cadena_share=CADS.id_cadena_share " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " and H.reporte_share=1  " & _
                        " " + RegionSQL + " " & _
                        "group by H.id_tipo,H.id_marca,CADS.id_cadena_share,Tiendas.Tiendas)H " & _
                        "GROUP BY id_tipo,id_cadena_share)Totales  " & _
                        "ON Totales.id_tipo=H.id_tipo AND Totales.id_cadena_share=CADS.id_cadena_share " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " and H.reporte_share=1  " & _
                        " " + RegionSQL + " " & _
                        "group by H.tipo_producto,H.id_tipo,H.id_marca,H.nombre_marca, " & _
                        "CADS.id_cadena_share,Tiendas.Tiendas,Totales.Total " & _
                        "UNION ALL " & _
                        "SELECT tipo_producto,id_tipo,nombre_marca,id_marca,100 id_cadena_share, " & _
                        "ROUND(100*sum(frentes)/sum(Total),2) Porcentaje " & _
                        "FROM(select H.tipo_producto, H.id_tipo,H.nombre_marca,H.id_marca, " & _
                        "sum(H.frentes)/Tiendas.Tiendas frentes,Totales.Total " & _
                        "from View_SYM_AC_Anaquel_HDET as H " & _
                        "INNER JOIN Cadenas_Share as CADS ON CADS.id_cadena=H.id_cadena " & _
                        "INNER JOIN (select CADS.id_cadena_share, H.id_marca, COUNT(DISTINCT H.id_tienda)Tiendas " & _
                        "from View_SYM_AC_Anaquel_HDET as H " & _
                        "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario=H.id_usuario " & _
                        "INNER JOIN Cadenas_Share as CADS ON CADS.id_cadena=H.id_cadena " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " and H.reporte_share=1  " & _
                        " " + RegionSQL + " " & _
                        "GROUP BY CADS.id_cadena_share,H.id_marca)Tiendas  " & _
                        "ON Tiendas.id_marca=H.id_marca AND Tiendas.id_cadena_share=CADS.id_cadena_share " & _
                        "INNER JOIN (SELECT id_tipo,id_cadena_share,SUM(Promedio)Total " & _
                        "FROM(select H.id_tipo,H.id_marca,CADS.id_cadena_share, " & _
                        "ROUND((sum(frentes)/Tiendas.Tiendas),2)Promedio " & _
                        "from View_SYM_AC_Anaquel_HDET as H " & _
                        "INNER JOIN Cadenas_Share as CADS ON CADS.id_cadena=H.id_cadena " & _
                        "INNER JOIN (select CADS.id_cadena_share, H.id_marca, COUNT(DISTINCT H.id_tienda)Tiendas " & _
                        "from View_SYM_AC_Anaquel_HDET as H " & _
                        "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario=H.id_usuario " & _
                        "INNER JOIN Cadenas_Share as CADS ON CADS.id_cadena=H.id_cadena " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " and H.reporte_share=1  " & _
                        " " + RegionSQL + " " & _
                        "GROUP BY CADS.id_cadena_share,H.id_marca,id_marca)Tiendas  " & _
                        "ON Tiendas.id_marca=H.id_marca AND Tiendas.id_cadena_share=CADS.id_cadena_share " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " and H.reporte_share=1  " & _
                        " " + RegionSQL + " " & _
                        "group by H.id_tipo,H.id_marca,CADS.id_cadena_share,Tiendas.Tiendas)H " & _
                        "GROUP BY id_tipo,id_cadena_share)Totales  " & _
                        "ON Totales.id_tipo=H.id_tipo AND Totales.id_cadena_share=CADS.id_cadena_share " & _
                        "WHERE H.id_periodo=" & cmbPeriodo.SelectedValue & " and H.reporte_share=1  " & _
                        " " + RegionSQL + " " & _
                        "group by H.tipo_producto,H.id_tipo,H.id_marca,H.nombre_marca, " & _
                        "CADS.id_cadena_share,Tiendas.Tiendas,Totales.Total)H " & _
                        "GROUP BY tipo_producto,id_tipo,nombre_marca,id_marca)PVT " & _
                        "PIVOT(SUM(Porcentaje) FOR id_cadena_share  " & _
                        "IN([1],[2],[3],[4],[5],[6],[7],[100])) AS H " & _
                        "WHERE H.id_tipo=" & Tipo & "" & _
                        "order by tipo_producto,[100] DESC", grilla)
        Else
            grilla.Visible = False
        End If
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PeriodoSQLCmb, "nombre_periodo", "id_periodo", cmbPeriodo)
        End If
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        CargarReporte()
        pnlGrid.Visible = True
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Comentarios " + cmbPeriodo.SelectedItem.ToString + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

End Class