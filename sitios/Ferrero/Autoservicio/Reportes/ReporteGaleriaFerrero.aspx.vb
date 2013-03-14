Imports System.Data
Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteGaleriaFerrero
    Inherits System.Web.UI.Page

    Dim RegionSel, CadenaSel As String
    Dim PeriodoSQL, TiendaSQL, RegionSQL, CadenaSQL As String

    Sub SQLCombo()
        RegionSQL = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)
        CadenaSQL = Acciones.Slc.cmb("GAL.id_cadena", cmbCadena.SelectedValue)

        PeriodoSQL = "SELECT id_periodo, nombre_periodo " & _
                     "FROM AS_Periodos " & _
                     " ORDER BY fecha_inicio DESC"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Regiones as REG " & _
                    "WHERE REG.id_proyecto=27 " & _
                    " ORDER BY REG.nombre_region"

        TiendaSQL = "SELECT DISTINCT nombre_tienda " & _
                    "FROM AS_Galeria_Historial_Det as GAL " & _
                    "INNER JOIN Usuarios as US ON US.id_usuario = GAL.id_usuario " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = US.id_region " & _
                    "WHERE GAL.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                    " " + CadenaSel + " " & _
                    " " + RegionSel + " " & _
                    " ORDER BY nombre_tienda "

        CadenaSQL = "SELECT DISTINCT CAD.id_cadena, CAD.nombre_cadena " & _
                    "FROM Cadenas_Tiendas as CAD " & _
                    " ORDER BY CAD.nombre_cadena"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            LlenaDrop(ConexionFerrero.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo)
            LlenaDrop(ConexionFerrero.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            LlenaDrop(ConexionFerrero.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
            LlenaDrop(ConexionFerrero.localSqlServer, TiendaSQL, "nombre_tienda", "nombre_tienda", cmbTienda)
            CargarFotos()
        End If
    End Sub

    Sub CargarFotos()
        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("REG.id_region", cmbRegion.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("H.id_cadena", cmbCadena.SelectedValue)
            TiendaSQL = Acciones.Slc.cmb("GAL.nombre_tienda", cmbTienda.SelectedValue)

            CargaGrilla(ConexionFerrero.localSqlServer, _
                        "SELECT DISTINCT PER.id_periodo,PER.nombre_periodo, REG.nombre_region, CAD.nombre_cadena, GAL.nombre_tienda, GAL.ruta , GAL.foto, GAL.descripcion " & _
                        "FROM AS_Galeria_Historial_Det as GAL " & _
                        "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena =  GAL.id_cadena " & _
                        "INNER JOIN Usuarios as US ON US.id_usuario = GAL.id_usuario " & _
                        "INNER JOIN Regiones as REG ON REG.id_region = US.id_region AND REG.id_proyecto=27 " & _
                        "INNER JOIN AS_Periodos as PER ON PER.id_periodo= GAL.id_periodo " & _
                        "WHERE GAL.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        " " + RegionSQL + " " & _
                        " " + TiendaSQL + " " & _
                        " " + CadenaSQL + " ", Me.gridImagenes)
        End If
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCadena.SelectedIndexChanged
        SQLCombo()

        LlenaDrop(ConexionFerrero.localSqlServer, TiendaSQL, "nombre_tienda", "nombre_tienda", cmbTienda)
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        LlenaDrop(ConexionFerrero.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
    End Sub

    Protected Sub cmbTienda_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbTienda.SelectedIndexChanged
        SQLCombo()
        CargarFotos()
    End Sub

End Class