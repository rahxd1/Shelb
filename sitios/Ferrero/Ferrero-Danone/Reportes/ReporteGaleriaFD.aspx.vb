Imports System.Data
Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteGaleriaFD
    Inherits System.Web.UI.Page

    Dim RegionSel, ColoniaSel As String
    Dim PeriodoSQL, PromotorSQL, RegionSQL, TiendaSQL, ColoniaSQL As String

    Sub SQLCombo()
        RegionSel = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)
        ColoniaSel = Acciones.Slc.cmb("GAL.colonia", cmbColonia.SelectedValue)

        PeriodoSQL = "SELECT DISTINCT PER.id_periodo, PER.nombre_periodo " & _
                         "FROM Danone_Galeria_Historial_Det as GAL " & _
                         "INNER JOIN Danone_Periodos as PER ON GAL.id_periodo = PER.id_periodo " & _
                         "ORDER BY PER.id_periodo DESC"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Danone_Galeria_Historial_Det as GAL " & _
                    "INNER JOIN Danone_CatRutas as RUT ON GAL.id_usuario = RUT.id_usuario " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = RUT.id_region " & _
                    "WHERE REG.id_proyecto =30 " & _
                    "ORDER BY REG.nombre_region "

        ColoniaSQL = "SELECT DISTINCT COL.id_colonia, COL.nombre_colonia " & _
                    "FROM Danone_Galeria_Historial_Det as GAL " & _
                    "INNER JOIN Colonias_Leon as COL ON COL.id_colonia = GAL.colonia " & _
                    "INNER JOIN Danone_CatRutas as RUT ON GAL.id_usuario = RUT.id_usuario " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = RUT.id_region " & _
                    "WHERE GAL.no_foto <>'' " & _
                    " " + RegionSel + " " & _
                    "ORDER BY COL.nombre_colonia "

        TiendaSQL = "SELECT DISTINCT nombre_tienda " & _
                    "FROM Danone_Galeria_Historial_Det as GAL " & _
                    "INNER JOIN Danone_CatRutas as RUT ON GAL.id_usuario = RUT.id_usuario " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = RUT.id_region " & _
                    "WHERE no_foto <>'' " & _
                    " " + RegionSel + " " & _
                    " " + ColoniaSel + " " & _
                    "ORDER BY nombre_tienda "
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionFerrero.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionFerrero.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionFerrero.localSqlServer, ColoniaSQL, "nombre_colonia", "id_colonia", cmbColonia)
            Combo.LlenaDrop(ConexionFerrero.localSqlServer, TiendaSQL, "nombre_tienda", "nombre_tienda", cmbTienda)

        End If
    End Sub

    Sub CargarFotos()
        CargaGrilla(ConexionFerrero.localSqlServer, _
                    "SELECT DISTINCT PER.id_periodo,PER.nombre_periodo, REG.nombre_region, COL.nombre_colonia, GAL.nombre_tienda, GAL.ruta , GAL.foto, GAL.descripcion " & _
                    "FROM Danone_Galeria_Historial_Det as GAL " & _
                    "INNER JOIN Danone_CatRutas as RUT ON GAL.id_usuario = RUT.id_usuario " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = RUT.id_region AND REG.id_proyecto =30 " & _
                    "INNER JOIN Danone_Periodos as PER ON PER.id_periodo= GAL.id_periodo " & _
                    "INNER JOIN Colonias_Leon as COL ON COL.id_colonia = GAL.colonia " & _
                    "WHERE PER.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                    "AND REG.id_region=" & cmbRegion.SelectedValue & " " & _
                    "AND GAL.colonia='" & cmbColonia.SelectedValue & "' " & _
                    "AND GAL.nombre_tienda='" & cmbTienda.SelectedValue & "' " & _
                    "ORDER BY GAL.nombre_tienda", Me.gridImagenes)
    End Sub

    Protected Sub cmbColonia_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbColonia.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionFerrero.localSqlServer, TiendaSQL, "nombre_tienda", "nombre_tienda", cmbTienda)
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionFerrero.localSqlServer, ColoniaSQL, "nombre_colonia", "id_colonia", cmbColonia)
        Combo.LlenaDrop(ConexionFerrero.localSqlServer, TiendaSQL, "nombre_tienda", "nombre_tienda", cmbTienda)
    End Sub

    Protected Sub cmbTienda_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbTienda.SelectedIndexChanged
        CargarFotos()
    End Sub

End Class