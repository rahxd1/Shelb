
Public Class SYM_AC
    Public Shared PeriodoSQLCmb As String
    Public Shared RegionSQLCmb As String
    Public Shared EstadoSQLcmb As String
    Public Shared SupervisorSQLcmb As String
    Public Shared CiudadSQLCmb As String
    Public Shared PromotorSQLCmb As String
    Public Shared CadenaSQLCmb As String
    Public Shared TiendaSQLCmb As String
    Public Shared LineaSQLCmb As String
End Class

Module Combos_SYMAC
    Public Sub SQLsCombo(ByVal cmbPeriodo As String, ByVal cmbRegion As String, _
                           ByVal cmbEstado As String, ByVal cmbSupervisor As String, _
                           ByVal cmbCiudad As String, ByVal cmbPromotor As String, _
                           ByVal cmbCadena As String, ByVal Tabla As String)
        Dim PeriodoSel, RegionSel, EstadoSel, SupervisorSel, _
            CiudadSel, PromotorSel, CadenaSel As String

        PeriodoSel = Acciones.Slc.cmb("id_periodo", cmbPeriodo)
        RegionSel = Acciones.Slc.cmb("id_region", cmbRegion)
        EstadoSel = Acciones.Slc.cmb("id_estado", cmbEstado)
        SupervisorSel = Acciones.Slc.cmb("id_supervisor", cmbSupervisor)
        CiudadSel = Acciones.Slc.cmb("ciudad", cmbCiudad)
        PromotorSel = Acciones.Slc.cmb("id_usuario", cmbPromotor)
        CadenaSel = Acciones.Slc.cmb("id_cadena", cmbCadena)

        SYM_AC.PeriodoSQLCmb = "SELECT DISTINCT id_periodo, nombre_periodo " & _
                     "FROM " + Tabla + "  " & _
                     " ORDER BY id_periodo DESC"

        SYM_AC.RegionSQLCmb = "SELECT DISTINCT nombre_region, id_region " & _
                    "FROM " + Tabla + "  " & _
                    "WHERE id_region<>0  " & _
                    " " + PeriodoSel + " " & _
                    "ORDER BY nombre_region"

        SYM_AC.EstadoSQLcmb = "SELECT DISTINCT id_estado, nombre_estado " & _
                    "FROM " + Tabla + "  " & _
                    "WHERE id_estado<>0 " & _
                    " " + PeriodoSel + RegionSel + " " & _
                    "ORDER BY nombre_estado"

        SYM_AC.SupervisorSQLcmb = "SELECT DISTINCT id_supervisor, supervisor " & _
                    "FROM " + Tabla + " " & _
                    "WHERE id_supervisor<>'' " & _
                    " " + PeriodoSel + RegionSel + EstadoSel + " " & _
                    "ORDER BY id_supervisor "

        SYM_AC.CiudadSQLCmb = "SELECT DISTINCT ciudad " & _
                    "FROM " + Tabla + " " & _
                    "WHERE ciudad<>'' " & _
                    " " + PeriodoSel + RegionSel + EstadoSel + SupervisorSel + " " & _
                    "ORDER BY ciudad"

        SYM_AC.PromotorSQLCmb = "SELECT DISTINCT id_usuario " & _
                    "FROM " + Tabla + " " & _
                    "WHERE id_usuario<>'' " & _
                    " " + PeriodoSel + RegionSel + EstadoSel + SupervisorSel + CiudadSel + " " & _
                    "ORDER BY id_usuario "

        SYM_AC.CadenaSQLCmb = "SELECT DISTINCT id_cadena,nombre_cadena " & _
                    "FROM " + Tabla + " " & _
                    "WHERE id_usuario<>'' " & _
                    " " + PeriodoSel + RegionSel + EstadoSel + SupervisorSel + " " & _
                    " " + CiudadSel + PromotorSel + " " & _
                    "ORDER BY nombre_cadena "

        SYM_AC.TiendaSQLCmb = "SELECT DISTINCT id_tienda,nombre " & _
                    "FROM " + Tabla + " " & _
                    "WHERE id_usuario<>'' " & _
                    " " + PeriodoSel + RegionSel + EstadoSel + SupervisorSel + " " & _
                    " " + CiudadSel + PromotorSel + CadenaSel + " " & _
                    "ORDER BY nombre "

        SYM_AC.LineaSQLCmb = "SELECT DISTINCT id_tipo, nombre_tipoproducto " & _
                        "FROM Anaquel_Tipo_Productos " & _
                        " ORDER BY nombre_tipoproducto"
    End Sub

End Module

