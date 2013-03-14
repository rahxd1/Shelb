Imports System.Data
Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc

Partial Public Class GaleriaEnergizerConv
    Inherits System.Web.UI.Page

    Dim PeriodoSel, RegionSel, CadenaSel As String
    Dim PeriodoSQL, RegionSQL, CadenaSQL, TiendaSQL, ProductoSQL, GridSQL As String

    Sub SQLs()
        If Not cmbPeriodo.SelectedValue = "" Then
            PeriodoSel = "AND H.id_periodo=" & cmbPeriodo.SelectedValue & " " : Else
            PeriodoSel = "" : End If

        If cmbRegion.SelectedValue <> "" Then
            If cmbRegion.SelectedValue <> 0 Then
                RegionSel = "AND REG.id_region=" & cmbRegion.SelectedValue & " " : Else
                RegionSel = "" : End If : End If

        If Not cmbCadena.SelectedValue = "" Then
            CadenaSel = "AND CAD.id_cadena=" & cmbCadena.SelectedValue & " " : Else
            CadenaSel = "" : End If

        PeriodoSQL = "SELECT DISTINCT PER.id_periodo, PER.nombre_periodo " & _
                    "FROM Energizer_Conv_Galerias_Historial as H " & _
                    "INNER JOIN Energizer_Conv_Periodos as PER ON PER.id_periodo = H.id_periodo" & _
                    " ORDER BY id_periodo DESC"

        RegionSQL = "SELECT DISTINCT REG.nombre_region, REG.id_region " & _
                    "FROM Regiones as REG " & _
                    "INNER JOIN Energizer_Conv_Tiendas AS TI ON REG.id_region = TI.id_region " & _
                    "INNER JOIN Energizer_Conv_Galerias_Historial as H ON H.id_tienda = TI.id_tienda " & _
                    "WHERE TI.estatus = 1 " & _
                    " " + PeriodoSel + " " & _
                    " ORDER BY REG.nombre_region"

        CadenaSQL = "SELECT DISTINCT CAD.id_cadena, CAD.nombre_cadena " & _
                    "FROM Cadenas_Tiendas as CAD " & _
                    "INNER JOIN Energizer_Conv_Tiendas AS TI ON TI.id_cadena = CAD.id_cadena " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                    "INNER JOIN Energizer_Conv_Galerias_Historial AS H ON TI.id_tienda = H.id_tienda " & _
                    "INNER JOIN Energizer_Conv_Usuarios as U ON U.id_usuario = H.id_usuario " & _
                    "WHERE TI.estatus = 1 " & _
                    " " + PeriodoSel + " " & _
                    " " + RegionSel + " " & _
                    " ORDER BY CAD.nombre_cadena"

        TiendaSQL = "SELECT DISTINCT TI.id_tienda, TI.nombre " & _
                   "FROM Energizer_Conv_Galerias_Historial AS H " & _
                   "INNER JOIN Energizer_Conv_Tiendas AS TI ON H.id_tienda = TI.id_tienda " & _
                   "INNER JOIN Cadenas_Tiendas as CAD ON TI.id_cadena = CAD.id_cadena " & _
                   "INNER JOIN Regiones as REG ON REG.id_region = TI.id_region " & _
                   "INNER JOIN Energizer_Conv_Usuarios as U ON U.id_usuario = H.id_usuario " & _
                   "WHERE TI.estatus = 1 " & _
                   " " + PeriodoSel + " " & _
                   " " + RegionSel + " " & _
                   " " + CadenaSel + " " & _
                   " ORDER BY TI.nombre"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If tipo_usuario = 1 Then
                Response.Redirect("GaleriaSubirBio.aspx")
            Else
                If Not Page.IsPostBack Then
                    SQLs()
                    Combo.LlenaDrop(ConexionEnergizer.localSqlServer, PeriodoSQL, "nombre_periodo", "id_periodo", cmbPeriodo)
                    Combo.LlenaDrop(ConexionEnergizer.localSqlServer, RegionSQL, "nombre_region", "id_region", cmbRegion)
                    Combo.LlenaDrop(ConexionEnergizer.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
                    Combo.LlenaDrop(ConexionEnergizer.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

                    CargarFotos()
                End If
            End If
        End If
    End Sub

    Sub CargarFotos()
        CargaGrilla(ConexionEnergizer.localSqlServer, _
                    "SELECT DISTINCT PER.id_periodo,PER.nombre_periodo, TI.id_region,REG.nombre_region, TI.id_cadena,CAD.nombre_cadena, TI.id_tienda,TI.nombre, GAL.ruta , GAL.foto, GAL.descripcion " & _
                    "FROM Energizer_Conv_Galerias_Historial as GAL " & _
                    "INNER JOIN Energizer_Conv_Tiendas as TI ON GAL.id_tienda = TI.id_tienda " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena =  TI.id_cadena " & _
                    "INNER JOIN Regiones as REG ON REG.id_region = Ti.id_region " & _
                    "INNER JOIN Energizer_Conv_Periodos as PER ON PER.id_periodo= GAL.id_periodo " & _
                    "WHERE TI.estatus = 1 " & _
                    "AND PER.id_periodo=" & cmbPeriodo.SelectedValue & " " & _
                    "AND REG.id_region=" & cmbRegion.SelectedValue & " " & _
                    "AND CAD.id_cadena=" & cmbCadena.SelectedValue & " " & _
                    "AND TI.id_tienda=" & cmbTienda.SelectedValue & " " & _
                    "ORDER BY GAL.descripcion", Me.gridImagenes)
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCadena.SelectedIndexChanged
        SQLs()

        Combo.LlenaDrop(ConexionEnergizer.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

        CargarFotos()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLs()

        Combo.LlenaDrop(ConexionEnergizer.localSqlServer, CadenaSQL, "nombre_cadena", "id_cadena", cmbCadena)
        Combo.LlenaDrop(ConexionEnergizer.localSqlServer, TiendaSQL, "nombre", "id_tienda", cmbTienda)

        CargarFotos()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        CargarFotos()
    End Sub

    Protected Sub cmbTienda_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbTienda.SelectedIndexChanged
        CargarFotos()
    End Sub

End Class