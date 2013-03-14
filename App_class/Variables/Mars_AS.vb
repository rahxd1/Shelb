
Public Class Mars_AS
    Public Shared PeriodoSQLCmb, QuincenaSQLCmb As String
    Public Shared RegionSQLCmb, EjecutivoSQLCmb, SupervisorSQLCmb, PromotorSQLCmb As String
    Public Shared CadenaSQLCmb, TiendaSQLCmb As String
    Public Shared TipoComentarioSQLCmb As String
    Public Shared ProductoPre_SQLCmb As String

    Public Shared TablaAreaNielsen As String = "<table border='0' cellpadding='0' cellspacing='0' style='border-collapse:" & _
                                          "  collapse;width:367pt' width='488px'> " & _
                                          "    <colgroup> <col style='mso-width-source:userset;mso-width-alt:1316;width:27pt' width='36px'/>" & _
                                          "        <col style='mso-width-source:userset;mso-width-alt:2889;width:59pt' width='80px'/>" & _
                                          "        <col style='mso-width-source:userset;mso-width-alt:3364;width:69pt' width='80px'/>" & _
                                          "        <col style='mso-width-source:userset;mso-width-alt:2194;width:45pt' width='70px'/>" & _
                                          "        <col style='mso-width-source:userset;mso-width-alt:2048;width:42pt' width='56px'/>" & _
                                          "        <col style='mso-width-source:userset;mso-width-alt:1828;width:38pt' width='50px'/>" & _
                                          "        <col style='mso-width-source:userset;mso-width-alt:2194;width:45pt' width='60px'/>" & _
                                          "        <col style='mso-width-source:userset;mso-width-alt:2048;width:42pt' width='56px'/></colgroup>" & _
                                          "    <tr height='20'><td colspan='8' style='border-left: .5pt solid windowtext; border-right: .5pt solid windowtext; border-top: .5pt solid windowtext; background: #17375D; height: 20px; color: white; font-size: 11.0pt; font-weight: 700; font-style: normal; text-decoration: none; font-family: Calibri, sans-serif; text-align: center; vertical-align: middle; white-space: nowrap; padding-left: 1px; padding-right: 1px; padding-top: 1px;'>" & _
                                          "      Objetivos participación anaquel por area Nielsen</td>" & _
                                          "    </tr><tr height='20'>" & _
                                          "        <td style='border-left: .5pt solid windowtext; border-right: .5pt solid windowtext; border-top: .5pt solid windowtext; background: #17375D; height: 20px; width: 27pt; color: white; font-size: 11.0pt; font-weight: 700; font-style: normal; text-decoration: none; font-family: Calibri, sans-serif; text-align: center; vertical-align: middle; white-space: nowrap; padding-left: 1px; padding-right: 1px; padding-top: 1px;'width='36'>&nbsp;</td>" & _
                                          "        <td colspan='4' style='border: .5pt solid windowtext; background: #17375D; width: 215pt; color: white; font-size: 11.0pt; font-weight: 700; font-style: normal; text-decoration: none; font-family: Calibri, sans-serif; text-align: center; vertical-align: middle; white-space: nowrap; padding-left: 1px; padding-right: 1px; padding-top: 1px;' width='287'>Perro</td>" & _
                                          "        <td colspan='3' style='border: .5pt solid windowtext; background: #17375D; width: 125pt; color: white; font-size: 11.0pt; font-weight: 700; font-style: normal; text-decoration: none; font-family: Calibri, sans-serif; text-align: center; vertical-align: middle; white-space: nowrap; padding-left: 1px; padding-right: 1px; padding-top: 1px;' width='166'>Gato</td>" & _
                                          "    </tr><tr height='20'><td height='40' style='border-left: .5pt solid windowtext; border-right: .5pt solid windowtext; border-top: .5pt solid windowtext; background: #17375D; height: 20px; width: 27pt; color: white; font-size: 11.0pt; font-weight: 700; font-style: normal; text-decoration: none; font-family: Calibri, sans-serif; text-align: center; vertical-align: middle; white-space: nowrap; padding-left: 1px; padding-right: 1px; padding-top: 1px;' width='36'>Area</td>" & _
                                          "        <td height='20' style='border: .5pt solid windowtext; background: #17375D; height: 15.0pt; color: white; font-size: 11.0pt; font-weight: 700; font-style: normal; text-decoration: none; font-family: Calibri, sans-serif; text-align: center; vertical-align: middle; white-space: nowrap; padding-left: 1px; padding-right: 1px; padding-top: 1px;'>Seco <br />adulto</td>" & _
                                          "        <td style='border: .5pt solid windowtext; background: #17375D; color: white; font-size: 11.0pt; font-weight: 700; font-style: normal; text-decoration: none; font-family: Calibri, sans-serif; text-align: center; vertical-align: middle; white-space: nowrap; padding-left: 1px; padding-right: 1px; padding-top: 1px;'>Seco <br />cachorro</td>" & _
                                          "        <td style='border: .5pt solid windowtext; background: #17375D; color: white; font-size: 11.0pt; font-weight: 700; font-style: normal; text-decoration: none; font-family: Calibri, sans-serif; text-align: center; vertical-align: middle; white-space: nowrap; padding-left: 1px; padding-right: 1px; padding-top: 1px;'>Húmedo</td>" & _
                                          "        <td style='border: .5pt solid windowtext; background: #17375D; color: white; font-size: 11.0pt; font-weight: 700; font-style: normal; text-decoration: none; font-family: Calibri, sans-serif; text-align: center; vertical-align: middle; white-space: nowrap; padding-left: 1px; padding-right: 1px; padding-top: 1px;'>Botanas</td>" & _
                                          "        <td style='border: .5pt solid windowtext; background: #17375D; color: white; font-size: 11.0pt; font-weight: 700; font-style: normal; text-decoration: none; font-family: Calibri, sans-serif; text-align: center; vertical-align: middle; white-space: nowrap; padding-left: 1px; padding-right: 1px; padding-top: 1px;'>Seco</td>" & _
                                          "        <td style='border: .5pt solid windowtext; background: #17375D; color: white; font-size: 11.0pt; font-weight: 700; font-style: normal; text-decoration: none; font-family: Calibri, sans-serif; text-align: center; vertical-align: middle; white-space: nowrap; padding-left: 1px; padding-right: 1px; padding-top: 1px;'>Húmedo</td>" & _
                                          "        <td style='border: .5pt solid windowtext; background: #17375D; color: white; font-size: 11.0pt; font-weight: 700; font-style: normal; text-decoration: none; font-family: Calibri, sans-serif; text-align: center; vertical-align: middle; white-space: nowrap; padding-left: 1px; padding-right: 1px; padding-top: 1px;'>Botanas</td></tr></table>"

End Class

Module MarsAS
    Public Sub SQLsCombo(ByVal cmbPeriodo As String, ByVal cmbQuincena As String, _
                        ByVal cmbRegion As String, ByVal cmbEjecutivo As String, _
                        ByVal cmbSupervisor As String, ByVal cmbPromotor As String, _
                        ByVal cmbCadena As String, ByVal Tabla As String)

        Dim PeriodoSel, QuincenaSel, RegionSel, EjecutivoSel, SupervisorSel, PromotorSel, CadenaSel As String
        RegionSel = ""

        If cmbPeriodo = "" Then
            PeriodoSel = "" : Else
            PeriodoSel = " AND orden=" & cmbPeriodo & " " : End If

        If cmbQuincena = "" Then
            QuincenaSel = "" : Else
            QuincenaSel = " AND id_quincena='" & cmbQuincena & "' " : End If

        If cmbRegion = "" Then
            RegionSel = "" : Else
            RegionSel = "AND id_region=" & cmbRegion & " " : End If

        If cmbEjecutivo = "" Then
            EjecutivoSel = "" : Else
            EjecutivoSel = " AND region_mars='" & cmbEjecutivo & "' " : End If

        If cmbSupervisor = "" Then
            SupervisorSel = "" : Else
            SupervisorSel = " AND id_supervisor='" & cmbSupervisor & "' " : End If

        If cmbPromotor = "" Then
            PromotorSel = "" : Else
            PromotorSel = " AND id_usuario='" & cmbPromotor & "' " : End If

        If cmbCadena = "" Then
            CadenaSel = "" : Else
            CadenaSel = " AND id_cadena=" & cmbCadena & " " : End If

        Mars_AS.PeriodoSQLCmb = "SELECT DISTINCT orden, nombre_periodo " & _
                        "FROM " & Tabla & "  " & _
                        "ORDER BY orden DESC"

        Mars_AS.QuincenaSQLCmb = "SELECT DISTINCT id_quincena, nombre_quincena " & _
                        "FROM " & Tabla & "  " & _
                        "ORDER BY id_quincena"

        Mars_AS.RegionSQLCmb = "SELECT DISTINCT id_region, nombre_region " & _
                        "FROM " & Tabla & " " & _
                        "ORDER BY nombre_region"

        Mars_AS.EjecutivoSQLCmb = "SELECT DISTINCT region_mars, Ejecutivo " & _
                        "FROM " & Tabla & " " & _
                        "WHERE region_mars<>'' " + PeriodoSel + QuincenaSel + RegionSel + " " & _
                        "ORDER BY region_mars"

        Mars_AS.SupervisorSQLCmb = "SELECT DISTINCT id_supervisor, supervisor " & _
                        "FROM " & Tabla & " " & _
                        "WHERE id_supervisor<>'' " + PeriodoSel + QuincenaSel + RegionSel + " " & _
                        "ORDER BY id_supervisor"

        Mars_AS.PromotorSQLCmb = "SELECT DISTINCT id_usuario " & _
                        "FROM " & Tabla & " " & _
                        "WHERE id_usuario<>'' " + PeriodoSel + QuincenaSel + RegionSel + " " & _
                        " " + EjecutivoSel + SupervisorSel + " " & _
                        "ORDER BY id_usuario"

        Mars_AS.CadenaSQLCmb = "SELECT DISTINCT id_cadena, nombre_cadena " & _
                        "FROM " & Tabla & " " & _
                        "WHERE id_cadena<>0 " + PeriodoSel + QuincenaSel + RegionSel + " " & _
                        " " + EjecutivoSel + SupervisorSel + PromotorSel + " " & _
                        "ORDER BY nombre_cadena"

        Mars_AS.TiendaSQLCmb = "SELECT DISTINCT id_tienda, nombre " & _
                        "FROM " & Tabla & " " & _
                        "WHERE id_tienda<>0 " + PeriodoSel + QuincenaSel + RegionSel + " " & _
                        " " + EjecutivoSel + SupervisorSel + PromotorSel + CadenaSel + " " & _
                        "ORDER BY nombre"

        Mars_AS.TipoComentarioSQLCmb = "SELECT * FROM AS_Tipo_Comentarios ORDER BY descripcion_comentario"

        Mars_AS.ProductoPre_SQLCmb = "SELECT * FROM AS_Precios_Productos " & _
                                    "WHERE tipo_producto=1 " & _
                                    "ORDER BY nombre_producto"
    End Sub
End Module
