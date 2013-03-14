
Public Class Jovy
    Public Shared PeriodoSQLCmb As String
    Public Shared RegionSQLCmb As String
    Public Shared CadenaSQLCmb As String
    Public Shared PromotorSQLCmb As String
    Public Shared TiendaSQLCmb As String
End Class

Module Variables_Jovy
    Public Sub SQLsCombo(ByVal cmbPeriodo As String, ByVal cmbRegion As String, _
                         ByVal cmbPromotor As String, ByVal cmbCadena As String)
        Dim PeriodoSel, RegionSel, PromotorSel, CadenaSel As String
        RegionSel = ""

        If cmbPeriodo = "" Then
            PeriodoSel = "" : Else
            PeriodoSel = " AND H.id_periodo=" & cmbPeriodo & " " : End If

        If cmbRegion = "" Then
            RegionSel = "" : Else
            RegionSel = "AND H.id_region=" & cmbRegion & " " : End If

        If cmbPromotor = "" Then
            PromotorSel = "" : Else
            PromotorSel = " AND H.id_usuario='" & cmbPromotor & "' " : End If

        If cmbCadena = "" Then
            CadenaSel = "" : Else
            CadenaSel = " AND H.id_cadena=" & cmbCadena & " " : End If

        Jovy.PeriodoSQLCmb = "SELECT * FROM Jovy_Periodos ORDER BY id_periodo DESC"

        Jovy.RegionSQLCmb = "SELECT DISTINCT H.id_region, H.nombre_region " & _
                    "FROM View_Historial_Jovy as H " & _
                    "WHERE H.id_region <>0  " & _
                    "" + PeriodoSel + " " & _
                    " ORDER BY H.nombre_region"

        Jovy.PromotorSQLCmb = "SELECT DISTINCT H.id_usuario " & _
                    "FROM View_Historial_Jovy AS H " & _
                    "WHERE H.id_usuario<>'' " & _
                    " " + PeriodoSel + RegionSel + "  " & _
                    "ORDER BY H.id_usuario"

        Jovy.CadenaSQLCmb = "SELECT DISTINCT H.id_cadena, H.nombre_cadena " & _
                    "FROM View_Historial_Jovy AS H " & _
                    "WHERE H.id_cadena<>0 " & _
                    " " + PeriodoSel + RegionSel + " " & _
                    " " + PromotorSel + " " & _
                    " ORDER BY H.nombre_cadena"

        Jovy.TiendaSQLCmb = "SELECT DISTINCT H.id_tienda, H.nombre " & _
                   "FROM View_Historial_Jovy AS H " & _
                   "WHERE H.id_tienda <>'' " & _
                   " " + PeriodoSel + RegionSel + CadenaSel + " " & _
                   " " + PromotorSel + CadenaSel + " " & _
                   " ORDER BY H.nombre"
    End Sub
End Module

