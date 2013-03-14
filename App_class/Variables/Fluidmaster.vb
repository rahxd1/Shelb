
Public Class Fluidmaster
    Public Shared PeriodoSQLCmb As String
    Public Shared RegionSQLCmb As String
    Public Shared EstadoSQLCmb As String
    Public Shared CadenaSQLCmb As String
    Public Shared PromotorSQLCmb As String
    Public Shared TiendaSQLCmb As String
    Public Shared TipoComentarioSQLCmb As String
End Class

Module Fluid
    Public Sub SQLsCombo(ByVal cmbPeriodo As String, ByVal cmbRegion As String, _
                         ByVal cmbEstado As String, ByVal cmbPromotor As String, _
                         ByVal cmbCadena As String)
        Dim PeriodoSel, RegionSel, EstadoSel, PromotorSel, CadenaSel As String
        RegionSel = ""

        If cmbPeriodo = "" Then
            PeriodoSel = "" : Else
            PeriodoSel = " AND H.id_periodo=" & cmbPeriodo & " " : End If

        If cmbRegion = "" Then
            RegionSel = "" : Else
            RegionSel = "AND H.id_region=" & cmbRegion & " " : End If

        If cmbEstado = "" Then
            EstadoSel = "" : Else
            EstadoSel = " AND H.id_estado =" & cmbEstado & " " : End If

        If cmbPromotor = "" Then
            PromotorSel = "" : Else
            PromotorSel = " AND H.id_usuario='" & cmbPromotor & "' " : End If

        If cmbCadena = "" Then
            CadenaSel = "" : Else
            CadenaSel = " AND H.id_cadena=" & cmbCadena & " " : End If

        Fluidmaster.PeriodoSQLCmb = "SELECT * FROM Periodos ORDER BY id_periodo DESC"

        Fluidmaster.RegionSQLCmb = "SELECT DISTINCT H.id_region, H.nombre_region " & _
                    "FROM View_Historial as H WHERE H.id_region <>0  " & _
                    "" + PeriodoSel + " " & _
                    "UNION ALL " & _
                    "SELECT DISTINCT H.id_region, H.nombre_region " & _
                    "FROM View_Historial_Sub as H WHERE H.id_region <>0  " & _
                    "" + PeriodoSel + " " & _
                    "ORDER BY H.nombre_region"

        Fluidmaster.EstadoSQLCmb = "SELECT DISTINCT H.id_estado, H.nombre_estado " & _
                    "FROM View_Historial as H WHERE H.id_estado<>0  " & _
                    "" + PeriodoSel + RegionSel + " " & _
                    "UNION ALL " & _
                    "SELECT DISTINCT H.id_estado, H.nombre_estado " & _
                    "FROM View_Historial_Sub as H WHERE H.id_estado<>0  " & _
                    "" + PeriodoSel + RegionSel + " " & _
                    "ORDER BY H.nombre_estado"

        Fluidmaster.PromotorSQLCmb = "SELECT DISTINCT H.id_usuario " & _
                    "FROM View_Historial AS H WHERE H.id_usuario<>'' " & _
                    " " + PeriodoSel + RegionSel + EstadoSel + "  " & _
                    "UNION ALL " & _
                    "SELECT DISTINCT H.id_usuario " & _
                    "FROM View_Historial_Sub AS H WHERE H.id_usuario<>'' " & _
                    " " + PeriodoSel + RegionSel + EstadoSel + "  " & _
                    "ORDER BY H.id_usuario"

        Fluidmaster.CadenaSQLCmb = "SELECT DISTINCT H.id_cadena, H.nombre_cadena " & _
                    "FROM View_Historial AS H WHERE H.id_cadena<>0 " & _
                    " " + PeriodoSel + RegionSel + EstadoSel + PromotorSel + " " & _
                    "UNION ALL " & _
                    "SELECT DISTINCT H.id_cadena, H.nombre_cadena " & _
                    "FROM View_Historial_Sub AS H WHERE H.id_cadena<>0 " & _
                    " " + PeriodoSel + RegionSel + EstadoSel + PromotorSel + " " & _
                    "ORDER BY H.nombre_cadena"

        Fluidmaster.TiendaSQLCmb = "SELECT DISTINCT H.id_tienda, H.nombre " & _
                   "FROM View_Historial AS H WHERE H.id_tienda <>'' " & _
                   " " + PeriodoSel + RegionSel + EstadoSel + CadenaSel + PromotorSel + " " & _
                   "UNION ALL " & _
                   "SELECT DISTINCT H.id_tienda, H.nombre " & _
                   "FROM View_Historial_Sub AS H WHERE H.id_tienda <>'' " & _
                   " " + PeriodoSel + RegionSel + EstadoSel + CadenaSel + PromotorSel + " " & _
                   "ORDER BY H.nombre"

        Fluidmaster.TipoComentarioSQLCmb = "SELECT * FROM Tipo_Comentarios WHERE tipo_comentario<>0 ORDER BY descripcion_comentario"

    End Sub
End Module

