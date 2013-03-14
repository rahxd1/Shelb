Imports System.Data
Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports System.Web.UI.WebControls.ImageField
Imports System.Web.UI.WebControls.Unit

Partial Public Class ReporteGaleriaPromotorNR
    Inherits System.Web.UI.Page

    Dim PeriodoSQLCmb, RegionSQLCmb, EstadoSQLCmb, CadenaSQLCmb, FormatoSQLCmb, TiendaSQLCmb As String

    Sub SQLCombo()
        Dim PeriodoSel, RegionSel, EstadoSel, CadenaSel, FormatoSel As String

        PeriodoSel = Acciones.Slc.cmb("H.id_periodo", cmbPeriodo.SelectedValue)
        RegionSel = Acciones.Slc.cmb("H.id_region", cmbRegion.SelectedValue)
        EstadoSel = Acciones.Slc.cmb("H.id_estado", cmbEstado.SelectedValue)
        CadenaSel = Acciones.Slc.cmb("H.id_cadena", cmbCadena.SelectedValue)
        FormatoSel = Acciones.Slc.cmb("H.id_formato", cmbFormato.SelectedValue)

        PeriodoSQLCmb = "SELECT * FROM NR_Periodos ORDER BY id_periodo DESC"

        RegionSQLCmb = "SELECT DISTINCT H.id_region, H.nombre_region " & _
                    "FROM View_Historial_Galeria_Promotor_NR as H " & _
                    "WHERE H.id_region <>0  " & _
                    "" + PeriodoSel + " " & _
                    " ORDER BY H.nombre_region"

        EstadoSQLCmb = "SELECT DISTINCT H.id_estado, H.nombre_estado " & _
                    "FROM View_Historial_Galeria_Promotor_NR as H " & _
                    "WHERE H.id_estado<>0  " & _
                    "" + PeriodoSel + RegionSel + " " & _
                    " ORDER BY H.nombre_estado"

        If Tipo_usuario = 7 Then
            Berol.CuentaClave = "INNER JOIN (select * FROM Usuarios_cadenas WHERE id_usuario='" & HttpContext.Current.User.Identity.Name & "' )as CC " & _
                            "ON CC.id_cadena=H.id_cadena " : Else
            Berol.CuentaClave = "" : End If

        CadenaSQLCmb = "SELECT DISTINCT H.id_cadena, H.nombre_cadena " & _
                    "FROM View_Historial_Galeria_Promotor_NR AS H " & _
                    " " + Berol.CuentaClave + " " & _
                    "WHERE H.id_cadena<>0 " & _
                    " " + PeriodoSel + RegionSel + EstadoSel + " " & _
                    " ORDER BY H.nombre_cadena"

        FormatoSQLCmb = "SELECT DISTINCT H.id_formato, H.nombre_formato " & _
                    "FROM View_Historial_Galeria_Promotor_NR AS H " & _
                    " " + Berol.CuentaClave + " " & _
                    "WHERE H.id_formato<>0 " & _
                    " " + PeriodoSel + RegionSel + EstadoSel + " " & _
                    " " + CadenaSel + " " & _
                    " ORDER BY H.nombre_formato"

        TiendaSQLCmb = "SELECT DISTINCT H.id_tienda, H.nombre " & _
                   "FROM View_Historial_Galeria_Promotor_NR AS H " & _
                   " " + Berol.CuentaClave + " " & _
                   "WHERE H.id_tienda <>'' " & _
                   " " + PeriodoSel + RegionSel + EstadoSel + CadenaSel + " " & _
                   " " + CadenaSel + " " & _
                   " ORDER BY H.nombre"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionBerol.localSqlServer, PeriodoSQLCmb, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, FormatoSQLCmb, "nombre_formato", "id_formato", cmbFormato)
            Combo.LlenaDrop(ConexionBerol.localSqlServer, TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)
        End If
    End Sub

    Sub CargarFotos()
        Dim RegionSQL, EstadoSQL, CadenaSQL, FormatoSQL, TiendaSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
            EstadoSQL = Acciones.Slc.cmb("TI.id_estado", cmbEstado.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("TI.id_cadena", cmbCadena.SelectedValue)
            FormatoSQL = Acciones.Slc.cmb("TI.id_formato", cmbFormato.SelectedValue)
            TiendaSQL = Acciones.Slc.cmb("TI.id_tienda", cmbTienda.SelectedValue)

            If Tipo_usuario = 7 Then
                Berol.CuentaClave = "INNER JOIN (select * FROM Usuarios_cadenas WHERE id_usuario='" & HttpContext.Current.User.Identity.Name & "' )as CC " & _
                                "ON CC.id_cadena=TI.id_cadena " : Else
                Berol.CuentaClave = "" : End If

            CargaGrilla(ConexionBerol.localSqlServer, _
                        "SELECT TI.nombre_region,TI.nombre_estado, TI.ciudad, " & _
                        "TI.nombre_formato, TI.nombre_cadena, TI.nombre,H1.id_usuario,  " & _
                        "H1.foto1, H1.nombre_ubicacion_1, H1.nombre_tipo_1, H1.descripcion_1, " & _
                        "H2.foto2, H2.nombre_ubicacion_2, H2.nombre_tipo_2, H2.descripcion_2, " & _
                        "H3.foto3, H3.nombre_ubicacion_3, H3.nombre_tipo_3, H3.descripcion_3, " & _
                        "H4.foto4, H4.nombre_ubicacion_4, H4.nombre_tipo_4, H4.descripcion_4, " & _
                        "H5.foto5, H5.nombre_ubicacion_5, H5.nombre_tipo_5, H5.descripcion_5 " & _
                        "FROM View_Tiendas_NR as TI  " & _
                        "INNER JOIN (select H.foto as foto1, UB.nombre_ubicacion as nombre_ubicacion_1, " & _
                        "'('+GR.nombre_tipo+')' as nombre_tipo_1, H.descripcion as descripcion_1, H.id_tienda, H.id_usuario " & _
                        "FROM NR_Historial_Galeria_Promotor_Det as H " & _
                        "INNER JOIN View_Tiendas_NR as TI ON TI.id_tienda=H.id_tienda " & _
                        " " + Berol.CuentaClave + " " & _
                        "INNER JOIN ubicacion as UB ON UB.id_ubicacion=H.id_ubicacion " & _
                        "INNER JOIN Tipo_Productos as GR ON GR.tipo_producto=H.tipo_producto " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " and no_foto=1 " & _
                        " " + RegionSQL + EstadoSQL + CadenaSQL + FormatoSQL + TiendaSQL + " " & _
                        ") as H1  ON TI.id_tienda=H1.id_tienda  " & _
                        "FULL JOIN (select H.foto as foto2, UB.nombre_ubicacion as nombre_ubicacion_2, " & _
                        "'('+GR.nombre_tipo+')' as nombre_tipo_2, H.descripcion as descripcion_2, H.id_tienda " & _
                        "FROM NR_Historial_Galeria_Promotor_Det as H " & _
                        "INNER JOIN View_Tiendas_NR as TI ON TI.id_tienda=H.id_tienda " & _
                        " " + Berol.CuentaClave + " " & _
                        "INNER JOIN ubicacion as UB ON UB.id_ubicacion=H.id_ubicacion " & _
                        "INNER JOIN Tipo_Productos as GR ON GR.tipo_producto=H.tipo_producto " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " and no_foto=2 " & _
                        " " + RegionSQL + EstadoSQL + CadenaSQL + FormatoSQL + TiendaSQL + " " & _
                        ") as H2  ON TI.id_tienda=H2.id_tienda  " & _
                        "FULL JOIN (select H.foto as foto3, UB.nombre_ubicacion as nombre_ubicacion_3, " & _
                        "'('+GR.nombre_tipo+')' as nombre_tipo_3, H.descripcion as descripcion_3, H.id_tienda " & _
                        "FROM NR_Historial_Galeria_Promotor_Det as H " & _
                        "INNER JOIN View_Tiendas_NR as TI ON TI.id_tienda=H.id_tienda " & _
                        " " + Berol.CuentaClave + " " & _
                        "INNER JOIN ubicacion as UB ON UB.id_ubicacion=H.id_ubicacion " & _
                        "INNER JOIN Tipo_Productos as GR ON GR.tipo_producto=H.tipo_producto " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " and no_foto=3 " & _
                        " " + RegionSQL + EstadoSQL + CadenaSQL + FormatoSQL + TiendaSQL + " " & _
                        ") as H3  ON TI.id_tienda=H3.id_tienda  " & _
                        "FULL JOIN (select H.foto as foto4, UB.nombre_ubicacion as nombre_ubicacion_4, " & _
                        "'('+GR.nombre_tipo+')' as nombre_tipo_4, H.descripcion as descripcion_4, H.id_tienda " & _
                        "FROM NR_Historial_Galeria_Promotor_Det as H " & _
                        "INNER JOIN View_Tiendas_NR as TI ON TI.id_tienda=H.id_tienda " & _
                        " " + Berol.CuentaClave + " " & _
                        "INNER JOIN ubicacion as UB ON UB.id_ubicacion=H.id_ubicacion " & _
                        "INNER JOIN Tipo_Productos as GR ON GR.tipo_producto=H.tipo_producto " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " and no_foto=4 " & _
                        " " + RegionSQL + EstadoSQL + CadenaSQL + FormatoSQL + TiendaSQL + " " & _
                        ") as H4  ON TI.id_tienda=H4.id_tienda " & _
                        "FULL JOIN (select H.foto as foto5, UB.nombre_ubicacion as nombre_ubicacion_5, " & _
                        "'('+GR.nombre_tipo+')' as nombre_tipo_5, H.descripcion as descripcion_5, H.id_tienda " & _
                        "FROM NR_Historial_Galeria_Promotor_Det as H " & _
                        "INNER JOIN View_Tiendas_NR as TI ON TI.id_tienda=H.id_tienda " & _
                        " " + Berol.CuentaClave + " " & _
                        "INNER JOIN ubicacion as UB ON UB.id_ubicacion=H.id_ubicacion " & _
                        "INNER JOIN Tipo_Productos as GR ON GR.tipo_producto=H.tipo_producto " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " and no_foto=5 " & _
                        " " + RegionSQL + EstadoSQL + CadenaSQL + FormatoSQL + TiendaSQL + " " & _
                        ") as H5  ON TI.id_tienda=H5.id_tienda  " & _
                        "WHERE TI.id_tienda <>0 " & _
                        " " + RegionSQL + EstadoSQL + CadenaSQL + FormatoSQL + TiendaSQL + " " & _
                        "ORDER BY TI.nombre_region,TI.nombre_estado,TI.ciudad ", gridImagenes)
        Else
            gridImagenes.Visible = False
        End If
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCadena.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, FormatoSQLCmb, "nombre_formato", "id_formato", cmbFormato)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarFotos()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, FormatoSQLCmb, "nombre_formato", "id_formato", cmbFormato)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarFotos()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, FormatoSQLCmb, "nombre_formato", "id_formato", cmbFormato)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarFotos()
    End Sub

    Protected Sub cmbTienda_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbTienda.SelectedIndexChanged
        CargarFotos()
    End Sub

    Private Sub cmbEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEstado.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, FormatoSQLCmb, "nombre_formato", "id_formato", cmbFormato)
        Combo.LlenaDrop(ConexionBerol.localSqlServer, TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarFotos()
    End Sub

    Private Sub cmbFormato_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFormato.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionBerol.localSqlServer, TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarFotos()
    End Sub

End Class