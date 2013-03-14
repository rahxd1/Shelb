
Public Class FerreroVar
    Public Shared PeriodoSQLCmb As String
    Public Shared RegionSQLCmb As String
    Public Shared CadenaSQLCmb As String
    Public Shared PromotorSQLCmb As String
    Public Shared TiendaSQLCmb As String
    Public Shared ColoniaSQLCmb As String
End Class

Module Ferrero
    Public Sub SQLsComboAS(ByVal cmbPeriodo As String, ByVal cmbRegion As String, _
                        ByVal cmbPromotor As String, ByVal cmbCadena As String)
        Dim PeriodoSel, RegionSel, PromotorSel, CadenaSel As String
        RegionSel = ""

        If cmbPeriodo = "" Then
            PeriodoSel = "" : Else
            PeriodoSel = " AND H.id_periodo=" & cmbPeriodo & " " : End If

        If cmbRegion <> "" Then
            If cmbRegion <> 0 Then
                RegionSel = "AND US.id_region=" & cmbRegion & " " : Else
                RegionSel = "" : End If : End If

        If cmbPromotor = "" Then
            PromotorSel = "" : Else
            PromotorSel = " AND H.id_usuario='" & cmbPromotor & "' " : End If

        If cmbCadena = "" Then
            CadenaSel = "" : Else
            CadenaSel = " AND H.id_cadena=" & cmbCadena & " " : End If

        FerreroVar.PeriodoSQLCmb = "SELECT id_periodo, nombre_periodo " & _
                         "FROM AS_Periodos " & _
                         " ORDER BY fecha_inicio DESC"

        FerreroVar.RegionSQLCmb = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Regiones as REG " & _
                    "WHERE REG.id_proyecto=27 " & _
                    " ORDER BY REG.nombre_region"

        FerreroVar.PromotorSQLCmb = "SELECT DISTINCT H.id_usuario " & _
                    "FROM AS_Historial AS H " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario = H.id_usuario " & _
                    "WHERE H.id_usuario<>'' " & _
                    " " + PeriodoSel + RegionSel + " ORDER BY H.id_usuario"

        FerreroVar.CadenaSQLCmb = "SELECT DISTINCT H.id_cadena, CAD.nombre_cadena " & _
                    "FROM AS_Historial AS H " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario = H.id_usuario " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena=H.id_cadena " & _
                    "WHERE H.id_cadena<>'' " & _
                    " " + PeriodoSel + RegionSel + PromotorSel + " " & _
                    " ORDER BY CAD.nombre_cadena"

        FerreroVar.TiendaSQLCmb = "SELECT DISTINCT H.nombre_tienda " & _
                   "FROM AS_Historial AS H " & _
                   "INNER JOIN Usuarios as US ON US.id_usuario = H.id_usuario " & _
                   "WHERE H.id_tienda <>'' " & _
                   " " + PeriodoSel + RegionSel + " " & _
                   " " + PromotorSel + CadenaSel + " " & _
                   " ORDER BY H.nombre_tienda"
    End Sub

    Public Sub SQLsComboFD(ByVal cmbPeriodo As String, ByVal cmbRegion As String, _
                        ByVal cmbPromotor As String, ByVal cmbColonia As String)
        Dim PeriodoSel, RegionSel, PromotorSel As String
        RegionSel = ""

        If cmbPeriodo = "" Then
            PeriodoSel = "" : Else
            PeriodoSel = " AND H.id_periodo=" & cmbPeriodo & " " : End If

        If cmbRegion <> "" Then
            If cmbRegion <> 0 Then
                RegionSel = "AND US.id_region=" & cmbRegion & " " : Else
                RegionSel = "" : End If : End If

        If cmbPromotor = "" Then
            PromotorSel = "" : Else
            PromotorSel = " AND H.id_usuario='" & cmbPromotor & "' " : End If

        FerreroVar.PeriodoSQLCmb = "SELECT id_periodo, nombre_periodo " & _
                         "FROM Danone_Periodos " & _
                         " ORDER BY fecha_inicio DESC"

        FerreroVar.RegionSQLCmb = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Regiones as REG " & _
                    "WHERE REG.id_proyecto=30 " & _
                    " ORDER BY REG.nombre_region"

        FerreroVar.PromotorSQLCmb = "SELECT DISTINCT H.id_usuario " & _
                    "FROM Danone_Historial AS H " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario = H.id_usuario " & _
                    "WHERE H.id_usuario<>'' " & _
                    " " + PeriodoSel + RegionSel + " ORDER BY H.id_usuario"

        FerreroVar.ColoniaSQLCmb = "SELECT DISTINCT COL.nombre_colonia, COL.id_colonia " & _
                    "FROM Colonias_Leon as COL " & _
                    "INNER JOIN Danone_Historial as H ON H.colonia = COL.id_colonia " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario = H.id_usuario " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = US.id_region " & _
                    "WHERE COL.activo=1 " & _
                    " " + PeriodoSel + PromotorSel + RegionSel + " " & _
                    " ORDER BY COL.id_colonia"
    End Sub
End Module

