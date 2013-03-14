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

Partial Public Class ReporteResultadosMars
    Inherits System.Web.UI.Page

    Dim PeriodoSQL, CuestionarioSQL, RegionSQL, EjecutivoSQL, PromotorSQL As String
    Dim PeriodoSel, CuestionarioSel, RegionSel, EjecutivoSel, PromotorSel As String
    Dim CantidadPreguntas As Integer
    Dim TituloPreguntas, TituloRegunta(100) As String
    Dim Preguntas, Pregunta(100) As String

    Sub SQLCombo()
        PeriodoSel = Acciones.Slc.cmb("orden", cmbPeriodo.SelectedValue)
        CuestionarioSel = Acciones.Slc.cmb("id_cuestionario", cmbCuestionario.SelectedValue)
        RegionSel = Acciones.Slc.cmb("US.id_region", cmbRegion.SelectedValue)
        EjecutivoSel = Acciones.Slc.cmb("US.ruta_ejecutivo", cmbEjecutivo.SelectedValue)
        PromotorSel = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)

        PeriodoSQL = "SELECT distinct PER.orden, PER.nombre_periodo " & _
                    "FROM Cap_Cuestionarios_Historial as H " & _
                    "INNER JOIN Periodos_Nuevo as PER ON PER.orden=H.orden"

        CuestionarioSQL = "SELECT DISTINCT H.id_cuestionario, CT.nombre_cuestionario " & _
                        "FROM Cap_Cuestionarios_Historial as H " & _
                        "INNER JOIN View_Usuarios as US ON US.id_usuario=H.id_usuario " & _
                        "INNER JOIN Cap_Cuestionarios as CT ON CT.id_cuestionario=H.id_cuestionario " & _
                        "WHERE CT.tipo_cuestionario=1 " & _
                        " " + PeriodoSel + " "

        RegionSQL = "SELECT DISTINCT REG.id_region, REG.nombre_region " & _
                        "FROM Cap_Cuestionarios_Historial as H " & _
                        "INNER JOIN View_Usuarios as US ON US.id_usuario=H.id_usuario " & _
                        "INNER JOIN Regiones as REG ON REG.id_region=US.id_region " & _
                        "WHERE REG.id_region<>0 " & _
                        " " + PeriodoSel + CuestionarioSel + " "

        EjecutivoSQL = "SELECT DISTINCT ruta_ejecutivo, ejecutivo " & _
                        "FROM Cap_Cuestionarios_Historial as H " & _
                        "INNER JOIN View_Usuarios as US ON US.id_usuario=H.id_usuario " & _
                        "WHERE ruta_ejecutivo<>'' " & _
                        " " + PeriodoSel + CuestionarioSel + RegionSel + " "

        PromotorSQL = "SELECT H.id_usuario FROM Cap_Cuestionarios_Historial as H " & _
                        "INNER JOIN View_Usuarios as US ON US.id_usuario=H.id_usuario " & _
                        "WHERE H.id_usuario<>'' " & _
                        " " + PeriodoSel + CuestionarioSel + RegionSel + PromotorSel + " "
    End Sub

    Public Function SeleccionPreguntas(ByVal Cuestionario As String) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select ROW_NUMBER() OVER(order by (SELECT 1))Titulo,((100*SC.id_seccion)+PR.id_pregunta)id_pregunta " & _
                                    "FROM Cap_Cuestionarios as CT " & _
                                    "INNER JOIN Cap_Cuestionarios_Seccion_Preguntas as SC  " & _
                                    "ON SC.id_cuestionario=CT.id_cuestionario " & _
                                    "INNER JOIN Cap_Cuestionarios_Preguntas as PR  " & _
                                    "ON  PR.id_cuestionario=CT.id_cuestionario " & _
                                    "AND PR.id_seccion=SC.id_seccion " & _
                                    "WHERE CT.id_cuestionario=" & Cuestionario & "")
        If Tabla.Rows.Count > 0 Then
            CantidadPreguntas = Tabla.Rows.Count

            For i = 0 To Tabla.Rows.Count - 1
                TituloRegunta(i) = ",[" & Tabla.Rows(i)("id_pregunta") & "]'" & Tabla.Rows(i)("Titulo") & "' "

                If i = 0 Then
                    Pregunta(0) = "[" & Tabla.Rows(i)("id_pregunta") & "]" : Else
                    Pregunta(i) = ",[" & Tabla.Rows(i)("id_pregunta") & "]" : End If
            Next i

            For i = 0 To Tabla.Rows.Count - 1
                TituloPreguntas = TituloPreguntas + TituloRegunta(i)
                Preguntas = Preguntas + Pregunta(i)
            Next i
        End If

        Tabla.Dispose()
    End Function

    Sub CargarReporte()
        If cmbCuestionario.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("US.id_region", cmbRegion.SelectedValue)
            EjecutivoSQL = Acciones.Slc.cmb("US.ruta_ejecutivo", cmbEjecutivo.SelectedValue)
            PromotorSQL = Acciones.Slc.cmb("H.id_usuario", cmbPromotor.SelectedValue)

            SeleccionPreguntas(cmbCuestionario.SelectedValue)

            CargaGrilla(ConexionMars.localSqlServer, _
                        "SELECT nombre_cuestionario as 'Cuestionario',fecha_registro as 'Fecha y hora',H.id_usuario as 'Promotor' " & _
                        " " + TituloPreguntas + ", RES.Porcentaje, RES.Estatus " & _
                        "FROM(SELECT H.id_cuestionario,CT.nombre_cuestionario, H.fecha_registro, " & _
                        "H.id_usuario,((100*SC.id_seccion)+PR.id_pregunta)id_pregunta,  " & _
                        "CASE WHEN HDET.respuesta_abierta=RES.respuesta then 1 else 0 end Respuesta " & _
                        "FROM Cap_Cuestionarios as CT " & _
                        "INNER JOIN Cap_Cuestionarios_Seccion_Preguntas as SC ON SC.id_cuestionario=CT.id_cuestionario " & _
                        "INNER JOIN Cap_Cuestionarios_Preguntas as PR  " & _
                        "ON PR.id_cuestionario=CT.id_cuestionario AND PR.id_seccion=SC.id_seccion " & _
                        "INNER JOIN Cap_Cuestionarios_Historial as H ON H.id_cuestionario=CT.id_cuestionario " & _
                        "INNER JOIN Cap_Cuestionarios_Historial_Det as HDET  " & _
                        "ON H.folio_historial=HDET.folio_historial AND HDET.id_seccion=SC.id_seccion " & _
                        "AND HDET.id_pregunta=PR.id_pregunta " & _
                        "INNER JOIN Cap_Cuestionarios_Respuestas as RES  " & _
                        "ON RES.id_cuestionario=CT.id_cuestionario AND RES.id_seccion=SC.id_seccion " & _
                        "AND RES.id_pregunta=PR.id_pregunta " & _
                        "INNER JOIN View_Usuarios as US ON US.id_usuario=H.id_usuario " & _
                        "WHERE CT.id_cuestionario=1 " & _
                        " " + RegionSQL + EjecutivoSQL + PromotorSQL + ") PVT " & _
                        "PIVOT(SUM(respuesta) FOR id_pregunta  " & _
                        "IN(" + Preguntas + ")) AS H INNER JOIN Cap_Resultados_cuestionarios as RES " & _
                        "ON RES.id_cuestionario=H.id_cuestionario AND H.id_usuario=RES.id_usuario", Me.gridReporte)
        Else
            gridReporte.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionMars.localSqlServer, PeriodoSQL, "nombre_periodo", "orden", cmbPeriodo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, CuestionarioSQL, "nombre_cuestionario", "id_cuestionario", cmbCuestionario)
            Combo.LlenaDrop(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionMars.localSqlServer, EjecutivoSQL, "ejecutivo", "ruta_ejecutivo", cmbEjecutivo)
            Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
        End If
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, CuestionarioSQL, "nombre_cuestionario", "id_cuestionario", cmbCuestionario)
        Combo.LlenaDrop(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionMars.localSqlServer, EjecutivoSQL, "ejecutivo", "ruta_ejecutivo", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)
    End Sub

    Private Sub cmbCuestionario_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCuestionario.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionMars.localSqlServer, EjecutivoSQL, "ejecutivo", "ruta_ejecutivo", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, EjecutivoSQL, "ejecutivo", "ruta_ejecutivo", cmbEjecutivo)
        Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Private Sub cmbEjecutivo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEjecutivo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionMars.localSqlServer, PromotorSQL, "id_usuario", "id_usuario", cmbPromotor)

        CargarReporte()
    End Sub

    Protected Sub cmbPromotor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPromotor.SelectedIndexChanged
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Resultados " + cmbCuestionario.SelectedItem.ToString() + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub


End Class