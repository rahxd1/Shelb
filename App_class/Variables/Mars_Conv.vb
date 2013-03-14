
Public Class Mars_Conv
    Public Shared PeriodoSQLCmb, QuincenaSQLCmb As String
    Public Shared RegionSQLCmb, SupervisorSQLCmb, PromotorSQLCmb As String
    Public Shared CadenaSQLCmb, TiendaSQLCmb As String
End Class

Module MarsConv
    Public Sub SQLsCombo(ByVal cmbPeriodo As String, ByVal cmbQuincena As String, _
                         ByVal cmbRegion As String, ByVal cmbPromotor As String, _
                         ByVal cmbCadena As String, ByVal Tabla As String)

        Dim PeriodoSel, QuincenaSel, RegionSel, PromotorSel, CadenaSel As String
        RegionSel = ""

        If cmbPeriodo = "" Then
            PeriodoSel = "" : Else
            PeriodoSel = " AND orden=" & cmbPeriodo & " " : End If

        If cmbQuincena = "" Then
            QuincenaSel = "" : Else
            QuincenaSel = " AND id_quincena= '" & cmbQuincena & "' " : End If

        If cmbRegion = "" Then
            RegionSel = "" : Else
            RegionSel = "AND id_region=" & cmbRegion & " " : End If

        If cmbPromotor = "" Then
            PromotorSel = "" : Else
            PromotorSel = " AND id_usuario= '" & cmbPromotor & "' " : End If

        If cmbCadena = "" Then
            CadenaSel = "" : Else
            CadenaSel = " AND id_cadena= " & cmbCadena & " " : End If

        Mars_Conv.PeriodoSQLCmb = "SELECT DISTINCT orden,nombre_periodo " & _
                     "FROM(select * from " & Tabla & " )H ORDER BY orden DESC"

        Mars_Conv.QuincenaSQLCmb = "SELECT DISTINCT id_quincena, nombre_quincena " & _
                    "FROM(select * from " & Tabla & ")H " & _
                    "WHERE id_quincena<>'' " + PeriodoSel + " " & _
                    "ORDER BY nombre_quincena"

        Mars_Conv.RegionSQLCmb = "SELECT DISTINCT id_region, nombre_region " & _
                    "FROM(select * from " & Tabla & ")H " & _
                    "WHERE nombre_region<>'' " + PeriodoSel + QuincenaSel + " " & _
                    "ORDER BY nombre_region"

        Mars_Conv.PromotorSQLCmb = "SELECT DISTINCT id_usuario " & _
                    "FROM (select * from " & Tabla & ")H " & _
                    "WHERE id_usuario<>'' " + PeriodoSel + QuincenaSel + " " & _
                    " " + RegionSel + " " & _
                    "ORDER BY id_usuario"

        Mars_Conv.CadenaSQLCmb = "SELECT DISTINCT id_cadena, nombre_cadena " & _
                    "FROM (select * from " & Tabla & ")H " & _
                    "WHERE nombre_cadena<>'' " + PeriodoSel + QuincenaSel + " " & _
                    " " + RegionSel + PromotorSel + " " & _
                    "ORDER BY nombre_cadena"

        Mars_Conv.TiendaSQLCmb = "SELECT DISTINCT id_tienda, nombre " & _
                   "FROM (select * from " & Tabla & ")H " & _
                    "WHERE nombre<>'' " + PeriodoSel + QuincenaSel + " " & _
                    " " + RegionSel + PromotorSel + CadenaSel + " " & _
                    "ORDER BY nombre"
    End Sub
End Module
