
Public Class Berol
    Public Shared PeriodoSQLCmb As String
    Public Shared RegionSQLCmb As String
    Public Shared EstadoSQLCmb As String
    Public Shared CadenaSQLCmb As String
    Public Shared FormatoSQLCmb As String
    Public Shared SupervisorSQLCmb As String
    Public Shared PromotorSQLCmb As String
    Public Shared TiendaSQLCmb As String
    Public Shared TipoComentarioSQLCmb As String
    Public Shared CuentaClave As String
End Class

Module NR
    Public Sub SQLsCombo(ByVal cmbPeriodo As String, ByVal cmbRegion As String, _
                         ByVal cmbEstado As String, ByVal cmbSupervisor As String, _
                         ByVal cmbPromotor As String, ByVal cmbFormato As String, ByVal cmbCadena As String)
        Dim PeriodoSel, RegionSel, EstadoSel, SupervisorSel, PromotorSel, CadenaSel, FormatoSel As String
        RegionSel = ""

        If cmbPeriodo = "" Then
            PeriodoSel = "" : Else
            PeriodoSel = " AND id_periodo=" & cmbPeriodo & " " : End If

        If cmbRegion = "" Then
            RegionSel = "" : Else
            RegionSel = "AND id_region=" & cmbRegion & " " : End If

        If cmbEstado = "" Then
            EstadoSel = "" : Else
            EstadoSel = " AND id_estado =" & cmbEstado & " " : End If

        If cmbSupervisor = "" Then
            SupervisorSel = "" : Else
            SupervisorSel = " AND id_supervisor='" & cmbSupervisor & "' " : End If

        If cmbPromotor = "" Then
            PromotorSel = "" : Else
            PromotorSel = " AND id_usuario='" & cmbPromotor & "' " : End If

        If cmbCadena = "" Then
            CadenaSel = "" : Else
            CadenaSel = " AND id_cadena=" & cmbCadena & " " : End If

        If cmbFormato = "" Then
            FormatoSel = "" : Else
            FormatoSel = " AND id_formato=" & cmbFormato & " " : End If

        Berol.PeriodoSQLCmb = "SELECT * FROM NR_Periodos ORDER BY id_periodo DESC"

        Berol.RegionSQLCmb = "SELECT DISTINCT id_region, nombre_region " & _
                    "FROM View_Historial_NR " & _
                    "WHERE id_region <>0  " & _
                    "" + PeriodoSel + " " & _
                    " ORDER BY nombre_region"

        Berol.EstadoSQLCmb = "SELECT DISTINCT id_estado, nombre_estado " & _
                    "FROM View_Historial_NR " & _
                    "WHERE id_estado<>0  " & _
                    "" + PeriodoSel + RegionSel + " " & _
                    " ORDER BY nombre_estado"

        Berol.SupervisorSQLCmb = "SELECT DISTINCT id_supervisor, supervisor " & _
                     "FROM View_Historial_NR " & _
                     "WHERE id_usuario<>'' " & _
                     " " + PeriodoSel + RegionSel + EstadoSel + " ORDER BY id_supervisor"

        Berol.PromotorSQLCmb = "SELECT DISTINCT id_usuario " & _
                    "FROM View_Historial_NR " & _
                    "WHERE id_usuario<>'' " & _
                    " " + PeriodoSel + RegionSel + EstadoSel + "  " & _
                    " " + SupervisorSel + " ORDER BY id_usuario"

        If Tipo_usuario = 7 Then
            Berol.CuentaClave = "INNER JOIN (select * FROM Usuarios_cadenas WHERE id_usuario='" & HttpContext.Current.User.Identity.Name & "' )as CC " & _
                            "ON CC.id_cadena=id_cadena " : Else
            Berol.CuentaClave = "" : End If

        Berol.CadenaSQLCmb = "SELECT DISTINCT id_cadena, nombre_cadena " & _
                    "FROM View_Historial_NR AS H " & _
                    " " + Berol.CuentaClave + " " & _
                    "WHERE id_cadena<>0 " & _
                    " " + PeriodoSel + RegionSel + EstadoSel + " " & _
                    " " + PromotorSel + SupervisorSel + " " & _
                    " ORDER BY nombre_cadena"

        Berol.FormatoSQLCmb = "SELECT DISTINCT id_formato, nombre_formato " & _
                    "FROM View_Historial_NR AS H " & _
                    " " + Berol.CuentaClave + " " & _
                    "WHERE id_formato<>0 " & _
                    " " + PeriodoSel + RegionSel + EstadoSel + " " & _
                    " " + CadenaSel + PromotorSel + SupervisorSel + " " & _
                    " ORDER BY nombre_formato"

        Berol.TiendaSQLCmb = "SELECT DISTINCT id_tienda, nombre " & _
                   "FROM View_Historial_NR AS H " & _
                   " " + Berol.CuentaClave + " " & _
                   "WHERE id_tienda <>'' " & _
                   " " + PeriodoSel + RegionSel + EstadoSel + CadenaSel + " " & _
                   " " + PromotorSel + CadenaSel + SupervisorSel + " " & _
                   " ORDER BY nombre"

        Berol.TipoComentarioSQLCmb = "SELECT * FROM Tipo_Comentarios ORDER BY descripcion_comentario"

    End Sub
End Module

