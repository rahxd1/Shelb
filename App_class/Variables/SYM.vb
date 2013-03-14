
Public Class SYMVar
    Public Shared PeriodoSQLCmb As String
    Public Shared RegionSQLCmb As String
    Public Shared PlazasSQLcmb As String
    Public Shared CadenaSQLCmb As String
    Public Shared PromotorSQLCmb As String
    Public Shared TiendaSQLCmb As String
    Public Shared ColoniaSQLCmb As String
End Class

Module SYMPrec
    Public Sub SQLsComboPrecios(ByVal cmbPeriodo As String, ByVal cmbRegion As String, _
                           ByVal cmbPlazas As String, ByVal cmbPromotor As String, ByVal cmbCadena As String)
        Dim PeriodoSel, RegionSel, PlazaSel, PromotorSel, CadenaSel As String
        RegionSel = ""

        If cmbPeriodo = "" Then
            PeriodoSel = "" : Else
            PeriodoSel = " AND H.id_periodo=" & cmbPeriodo & " " : End If

        If cmbRegion <> "" Then
            If cmbRegion <> 0 Then
                RegionSel = "AND US.id_region=" & cmbRegion & " " : Else
                RegionSel = "" : End If : End If

        If cmbPlazas = "" Then
            PlazaSel = "" : Else
            PlazaSel = "AND US.plaza = '" & cmbPlazas & "' " : End If

        If cmbPromotor = "" Then
            PromotorSel = "" : Else
            PromotorSel = " AND H.id_usuario='" & cmbPromotor & "' " : End If

        If cmbCadena = "" Then
            CadenaSel = "" : Else
            CadenaSel = " AND H.id_cadena=" & cmbCadena & " " : End If

        SYMVar.PeriodoSQLCmb = "SELECT id_periodo, nombre_periodo " & _
                         "FROM SYM_Precios_Periodos " & _
                         " ORDER BY fecha_inicio DESC"

        SYMVar.RegionSQLCmb = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM SYM_Regiones as REG " & _
                    "WHERE REG.id_region<>0 " & _
                    " ORDER BY REG.nombre_region"

        SYMVar.PlazasSQLcmb = "SELECT DISTINCT US.plaza " & _
                    "FROM Usuarios as US " & _
                    "INNER JOIN SYM_Precios_Historial as H ON H.id_usuario=US.id_usuario " & _
                    "WHERE US.plaza<>'' " & _
                    " " + PeriodoSel + RegionSel + " " & _
                    " ORDER BY US.plaza"

        SYMVar.CadenaSQLCmb = "SELECT DISTINCT CAD.id_cadena, CAD.nombre_cadena " & _
                    "FROM SYM_Precios_Historial as H " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = H.id_cadena " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario = H.id_usuario " & _
                    "WHERE CAD.id_cadena<>0 " & _
                    " " + PeriodoSel + RegionSel + PlazaSel + " ORDER BY CAD.nombre_cadena "

        SYMVar.PromotorSQLCmb = "SELECT DISTINCT H.id_usuario " & _
                    "FROM SYM_Precios_Historial as H " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario=H.id_usuario " & _
                    "WHERE US.id_usuario<>'' " & _
                    " " + PeriodoSel + RegionSel + PlazaSel + " " & _
                    " ORDER BY H.id_usuario "
    End Sub

End Module

