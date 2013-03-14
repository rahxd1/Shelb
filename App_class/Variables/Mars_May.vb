
Public Class Mars_May
    Public Shared PeriodoSQLCmb, QuincenaSQLCmb, SemanaSQLCmb As String
    Public Shared RegionSQLCmb, EjecutivoSQLCmb, SupervisorSQLCmb, PromotorSQLCmb As String
    Public Shared CadenaSQLCmb, TiendaSQLCmb As String
    Public Shared TipoComentarioSQLCmb As String
End Class

Module MarsMay
    Public Sub SQLsCombo(ByVal cmbPeriodo As String, ByVal cmbQuincena As String, _
                         ByVal cmbSemana As String, ByVal cmbRegion As String, _
                         ByVal cmbEjecutivo As String, ByVal cmbPromotor As String, _
                         ByVal cmbCadena As String)

        Dim PeriodoSel, QuincenaSel, SemanaSel, RegionSel, EjecutivoSel, PromotorSel, CadenaSel As String
        RegionSel = ""

        If cmbPeriodo = "" Then
            PeriodoSel = "" : Else
            PeriodoSel = " AND orden=" & cmbPeriodo & " " : End If

        If cmbQuincena = "" Then
            QuincenaSel = "" : Else
            QuincenaSel = " AND id_quincena= '" & cmbQuincena & "' " : End If

        If cmbSemana = "" Then
            SemanaSel = "" : Else
            SemanaSel = " AND id_semana= '" & cmbSemana & "' " : End If

        If cmbRegion = "" Then
            RegionSel = "" : Else
            RegionSel = "AND id_region=" & cmbRegion & " " : End If

        If cmbEjecutivo = "" Then
            EjecutivoSel = "" : Else
            EjecutivoSel = " AND ruta_ejecutivo= " & cmbEjecutivo & " " : End If

        If cmbPromotor = "" Then
            PromotorSel = "" : Else
            PromotorSel = " AND id_usuario= " & cmbPromotor & " " : End If

        If cmbCadena = "" Then
            CadenaSel = "" : Else
            CadenaSel = " AND id_cadena= " & cmbCadena & " " : End If

        Mars_May.PeriodoSQLCmb = "SELECT DISTINCT orden,nombre_periodo " & _
                     "FROM View_Historial_Mayoreo ORDER BY orden DESC"

        Mars_May.QuincenaSQLCmb = "SELECT DISTINCT id_quincena, nombre_quincena " & _
                    "FROM View_Historial_Mayoreo " & _
                    "WHERE id_quincena<>'' " + PeriodoSel + " " & _
                    "ORDER BY nombre_quincena"

        Mars_May.SemanaSQLCmb = "SELECT DISTINCT id_semana, nombre_semana " & _
                    "FROM View_Historial_Mayoreo " & _
                    "WHERE id_semana<>'' " + PeriodoSel + QuincenaSel + " " & _
                    "ORDER BY nombre_semana"

        Mars_May.RegionSQLCmb = "SELECT DISTINCT id_region, nombre_region " & _
                    "FROM(select * from View_Historial_Mayoreo " & _
                    "UNION ALL select * from View_Historial_PM_Ant " & _
                    "UNION ALL select * from View_Historial_May_Ant)H " & _
                    "WHERE nombre_region<>'' " + PeriodoSel + QuincenaSel + SemanaSel + " " & _
                    "ORDER BY nombre_region"

        Mars_May.EjecutivoSQLCmb = "SELECT DISTINCT ruta_ejecutivo, Ejecutivo " & _
                    "FROM(select * from View_Historial_Mayoreo " & _
                    "UNION ALL select * from View_Historial_PM_Ant " & _
                    "UNION ALL select * from View_Historial_May_Ant)H " & _
                    "WHERE ruta_ejecutivo IS NOT NULL " + PeriodoSel + QuincenaSel + SemanaSel + " " & _
                    "ORDER BY ruta_ejecutivo"

        Mars_May.PromotorSQLCmb = "SELECT DISTINCT id_usuario " & _
                    "FROM View_Historial_Mayoreo " & _
                    "WHERE id_usuario<>'' " + PeriodoSel + QuincenaSel + SemanaSel + " " & _
                    " " + RegionSel + " " & _
                    "ORDER BY id_usuario"

        Mars_May.CadenaSQLCmb = "SELECT DISTINCT id_cadena, nombre_cadena " & _
                    "FROM View_Historial_Mayoreo " & _
                    "WHERE nombre_cadena<>'' " + PeriodoSel + QuincenaSel + SemanaSel + " " & _
                    " " + RegionSel + PromotorSel + " " & _
                    "ORDER BY nombre_cadena"

        Mars_May.TiendaSQLCmb = "SELECT DISTINCT id_tienda, nombre " & _
                   "FROM View_Historial_Mayoreo " & _
                    "WHERE id_tienda<>0 " + PeriodoSel + QuincenaSel + SemanaSel + " " & _
                    " " + RegionSel + PromotorSel + CadenaSel + " " & _
                    "ORDER BY nombre"
    End Sub
End Module
