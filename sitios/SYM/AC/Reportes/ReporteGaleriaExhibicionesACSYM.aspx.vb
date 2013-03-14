Imports System.Data
Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteGaleriaExhibicionesACSYM
    Inherits System.Web.UI.Page

    Sub SQLCombo()
        Combos_SYMAC.SQLsCombo(cmbPeriodo.SelectedValue, cmbRegion.SelectedValue, _
                              "", "", "", "", cmbCadena.SelectedValue, "View_SYM_AC_Galeria_Exh")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.PeriodoSQLCmb, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
            Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)
        End If
    End Sub

    Sub CargarFotos()
        Dim RegionSQL, CadenaSQL, TiendaSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("CAD.id_cadena", cmbCadena.SelectedValue)
            TiendaSQL = Acciones.Slc.cmb("TI.id_tienda", cmbTienda.SelectedValue)

            CargaGrilla(ConexionSYM.localSqlServer, _
                        "SELECT DISTINCT PER.id_periodo,PER.nombre_periodo, TI.id_region,TI.nombre_estado, TI.ciudad, REL.id_supervisor, US.nombre as nombre_sup," & _
                        "TI.nombre_region, TI.id_cadena,TI.nombre_cadena, TI.id_tienda,TI.nombre, GAL.id_usuario, GAL.ruta , GAL.foto " & _
                        "FROM AC_Galeria_Exh_Historial_Det as GAL " & _
                        "INNER JOIN View_SYM_AC_Tiendas as TI ON GAL.id_tienda = TI.id_tienda " & _
                        "INNER JOIN AC_Periodos as PER ON PER.id_periodo= GAL.id_periodo " & _
                        "INNER JOIN AC_Rutas_Eventos as RE ON RE.id_periodo= GAL.id_periodo AND RE.id_tienda = GAL.id_tienda " & _
                        "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario = RE.id_usuario " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario = REL.id_supervisor " & _
                        "WHERE PER.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + " " & _
                        " " + CadenaSQL + " " & _
                        " " + TiendaSQL + " " & _
                        "ORDER BY TI.nombre_region,TI.nombre_estado,TI.ciudad ", gridImagenes)
        Else
            gridImagenes.Visible = False
        End If
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCadena.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarFotos()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarFotos()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionSYM.localSqlServer, SYM_AC.TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarFotos()
    End Sub

    Protected Sub cmbTienda_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbTienda.SelectedIndexChanged
        CargarFotos()
    End Sub

End Class