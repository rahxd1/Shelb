Imports System.Data
Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class ReporteGaleriaJovy
    Inherits System.Web.UI.Page

    Dim PeriodoSel, RegionSel, CadenaSel As String
    Dim PeriodoSQL, RegionSQL, CadenaSQL, TiendaSQL, GridSQL As String

    Sub SQLCombo()
        PeriodoSel = Acciones.Slc.cmb("H.id_periodo", cmbPeriodo.SelectedValue)
        RegionSel = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
        CadenaSel = Acciones.Slc.cmb("TI.id_cadena", cmbCadena.SelectedValue)

        PeriodoSQL = "SELECT id_periodo, nombre_periodo " & _
                         "FROM Jovy_Periodos " & _
                         " ORDER BY id_periodo DESC"

        RegionSQL = "SELECT DISTINCT TI.nombre_region, TI.id_region " & _
                    "FROM Jovy_Galeria_Historial_Det as H " & _
                    "INNER JOIN View_Jovy_Tiendas as TI ON H.id_tienda = TI.id_tienda " & _
                    "WHERE TI.id_region<>0 " + PeriodoSel + "" & _
                    "ORDER BY TI.nombre_region"

        CadenaSQL = "SELECT DISTINCT TI.id_cadena, TI.nombre_cadena " & _
                    "FROM Jovy_Galeria_Historial_Det as H  " & _
                    "INNER JOIN View_Jovy_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                    "WHERE TI.id_tienda<>0 " & _
                    " " + PeriodoSel + RegionSel + " " & _
                    " ORDER BY TI.nombre_cadena"

        TiendaSQL = "SELECT DISTINCT TI.id_tienda, TI.nombre " & _
                    "FROM Jovy_Galeria_Historial_Det as H  " & _
                    "INNER JOIN View_Jovy_Tiendas as TI ON TI.id_tienda = H.id_tienda " & _
                    "WHERE TI.id_tienda<>0 " & _
                    " " + PeriodoSel + RegionSel + CadenaSel + " " & _
                    " ORDER BY TI.nombre"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()
            Combo.LlenaDrop(ConexionJovy.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionJovy.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionJovy.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
            Combo.LlenaDrop(ConexionJovy.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

        End If
    End Sub

    Sub CargarFotos()
        If cmbPeriodo.SelectedValue <> "" Then
            CargaGrilla(ConexionJovy.localSqlServer, _
                        "SELECT DISTINCT PER.id_periodo,PER.nombre_periodo, TI.id_region,TI.nombre_region, " & _
                        "TI.id_cadena,TI.nombre_cadena, TI.id_tienda,TI.nombre, GAL.ruta , GAL.foto, GAL.descripcion " & _
                        "FROM Jovy_Galeria_Historial_Det as GAL " & _
                        "INNER JOIN View_Jovy_Tiendas as TI ON GAL.id_tienda = TI.id_tienda " & _
                        "INNER JOIN Jovy_Periodos as PER ON PER.id_periodo= GAL.id_periodo " & _
                        "WHERE TI.id_tienda<>0 " & _
                        "AND PER.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                        "AND TI.id_region=" & cmbRegion.SelectedValue & " " & _
                        "AND TI.id_cadena=" & cmbCadena.SelectedValue & " " & _
                        "AND TI.id_tienda=" & cmbTienda.SelectedValue & " " & _
                        "ORDER BY GAL.descripcion", Me.gridImagenes)
        Else
            Me.gridImagenes.Visible = False
        End If
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCadena.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionJovy.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionJovy.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionJovy.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)
    End Sub

    Protected Sub cmbTienda_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbTienda.SelectedIndexChanged
        CargarFotos()
    End Sub

End Class