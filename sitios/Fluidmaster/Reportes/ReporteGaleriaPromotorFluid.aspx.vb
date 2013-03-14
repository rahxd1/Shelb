Imports System.Data
Imports System.Text
Imports InfoSoftGlobal
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports System.Web.UI.WebControls.ImageField
Imports System.Web.UI.WebControls.Unit

Partial Public Class ReporteGaleriaPromotorFluid
    Inherits System.Web.UI.Page

    Dim PeriodoSQLCmb, RegionSQLCmb, EstadoSQLCmb, CadenaSQLCmb, TiendaSQLCmb As String

    Sub SQLCombo()
        Dim PeriodoSel, RegionSel, EstadoSel, CadenaSel As String

        PeriodoSel = Acciones.Slc.cmb("H.id_periodo", cmbPeriodo.SelectedValue)
        RegionSel = Acciones.Slc.cmb("H.id_region", cmbRegion.SelectedValue)
        EstadoSel = Acciones.Slc.cmb("H.id_estado", cmbEstado.SelectedValue)
        CadenaSel = Acciones.Slc.cmb("H.id_cadena", cmbDistribuidor.SelectedValue)

        PeriodoSQLCmb = "SELECT * FROM Periodos ORDER BY id_periodo DESC"

        RegionSQLCmb = "SELECT DISTINCT H.id_region, H.nombre_region " & _
                    "FROM View_Historial_Galeria_Promotor as H " & _
                    "WHERE H.id_region <>0  " & _
                    "" + PeriodoSel + " " & _
                    " ORDER BY H.nombre_region"

        EstadoSQLCmb = "SELECT DISTINCT H.id_estado, H.nombre_estado " & _
                    "FROM View_Historial_Galeria_Promotor as H " & _
                    "WHERE H.id_estado<>0  " & _
                    "" + PeriodoSel + RegionSel + " " & _
                    " ORDER BY H.nombre_estado"

        CadenaSQLCmb = "SELECT DISTINCT H.id_cadena, H.nombre_cadena " & _
                    "FROM View_Historial_Galeria_Promotor AS H " & _
                    " " + Berol.CuentaClave + " " & _
                    "WHERE H.id_cadena<>0 " & _
                    " " + PeriodoSel + RegionSel + EstadoSel + " " & _
                    " ORDER BY H.nombre_cadena"

        TiendaSQLCmb = "SELECT DISTINCT H.id_tienda, H.nombre " & _
                   "FROM View_Historial_Galeria_Promotor AS H " & _
                   "WHERE H.id_tienda <>'' " & _
                   " " + PeriodoSel + RegionSel + EstadoSel + CadenaSel + " " & _
                   " " + CadenaSel + " " & _
                   " ORDER BY H.nombre"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombo()

            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, PeriodoSQLCmb, "nombre_periodo", "id_periodo", cmbPeriodo)
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbDistribuidor)
            Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)
        End If
    End Sub

    Sub CargarFotos()
        Dim RegionSQL, EstadoSQL, CadenaSQL, TiendaSQL As String

        If cmbPeriodo.SelectedValue <> "" Then
            RegionSQL = Acciones.Slc.cmb("TI.id_region", cmbRegion.SelectedValue)
            EstadoSQL = Acciones.Slc.cmb("TI.id_estado", cmbEstado.SelectedValue)
            CadenaSQL = Acciones.Slc.cmb("TI.id_cadena", cmbDistribuidor.SelectedValue)
            TiendaSQL = Acciones.Slc.cmb("TI.id_tienda", cmbTienda.SelectedValue)

            CargaGrilla(ConexionFluidmaster.localSqlServer, _
                        "SELECT TI.nombre_region,TI.nombre_estado, TI.ciudad, TI.nombre_cadena, TI.nombre,H1.id_usuario,  " & _
                        "H1.foto1, H1.descripcion_fotografia_1, H1.descripcion_1, " & _
                        "H2.foto2, H2.descripcion_fotografia_2, H2.descripcion_2, " & _
                        "H3.foto3, H3.descripcion_fotografia_3, H3.descripcion_3, " & _
                        "H4.foto4, H4.descripcion_fotografia_4, H4.descripcion_4, " & _
                        "H5.foto5, H5.descripcion_fotografia_5, H5.descripcion_5 " & _
                        "FROM View_Tiendas as TI  " & _
                        "INNER JOIN (select H.foto as foto1, TF.descripcion_fotografia as descripcion_fotografia_1, " & _
                        " H.descripcion as descripcion_1, H.id_tienda, H.id_usuario " & _
                        "FROM Historial_Galeria_Promotor_Det as H " & _
                        "INNER JOIN View_Tiendas as TI ON TI.id_tienda=H.id_tienda " & _
                        "INNER JOIN Tipo_Fotografias as TF ON TF.tipo_fotografia=H.tipo_fotografia " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " and no_foto=1 " & _
                        " " + RegionSQL + EstadoSQL + CadenaSQL + TiendaSQL + " " & _
                        ") as H1  ON TI.id_tienda=H1.id_tienda  " & _
                        "FULL JOIN (select H.foto as foto2, TF.descripcion_fotografia as descripcion_fotografia_2, " & _
                        "H.descripcion as descripcion_2, H.id_tienda " & _
                        "FROM Historial_Galeria_Promotor_Det as H " & _
                        "INNER JOIN View_Tiendas as TI ON TI.id_tienda=H.id_tienda " & _
                        " " + Berol.CuentaClave + " " & _
                        "INNER JOIN Tipo_Fotografias as TF ON TF.tipo_fotografia=H.tipo_fotografia " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " and no_foto=2 " & _
                        " " + RegionSQL + EstadoSQL + CadenaSQL + TiendaSQL + " " & _
                        ") as H2  ON TI.id_tienda=H2.id_tienda  " & _
                        "FULL JOIN (select H.foto as foto3, TF.descripcion_fotografia as descripcion_fotografia_3, " & _
                        "H.descripcion as descripcion_3, H.id_tienda " & _
                        "FROM Historial_Galeria_Promotor_Det as H " & _
                        "INNER JOIN View_Tiendas as TI ON TI.id_tienda=H.id_tienda " & _
                        " " + Berol.CuentaClave + " " & _
                        "INNER JOIN Tipo_Fotografias as TF ON TF.tipo_fotografia=H.tipo_fotografia " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " and no_foto=3 " & _
                        " " + RegionSQL + EstadoSQL + CadenaSQL + TiendaSQL + " " & _
                        ") as H3  ON TI.id_tienda=H3.id_tienda  " & _
                        "FULL JOIN (select H.foto as foto4, TF.descripcion_fotografia as descripcion_fotografia_4, " & _
                        "H.descripcion as descripcion_4, H.id_tienda " & _
                        "FROM Historial_Galeria_Promotor_Det as H " & _
                        "INNER JOIN View_Tiendas as TI ON TI.id_tienda=H.id_tienda " & _
                        " " + Berol.CuentaClave + " " & _
                        "INNER JOIN Tipo_Fotografias as TF ON TF.tipo_fotografia=H.tipo_fotografia " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " and no_foto=4 " & _
                        " " + RegionSQL + EstadoSQL + CadenaSQL + TiendaSQL + " " & _
                        ") as H4  ON TI.id_tienda=H4.id_tienda " & _
                        "FULL JOIN (select H.foto as foto5, TF.descripcion_fotografia as descripcion_fotografia_5, " & _
                        "H.descripcion as descripcion_5, H.id_tienda " & _
                        "FROM Historial_Galeria_Promotor_Det as H " & _
                        "INNER JOIN View_Tiendas as TI ON TI.id_tienda=H.id_tienda " & _
                        " " + Berol.CuentaClave + " " & _
                        "INNER JOIN Tipo_Fotografias as TF ON TF.tipo_fotografia=H.tipo_fotografia " & _
                        "WHERE id_periodo=" & cmbPeriodo.SelectedValue & " and no_foto=5 " & _
                        " " + RegionSQL + EstadoSQL + CadenaSQL + TiendaSQL + " " & _
                        ") as H5  ON TI.id_tienda=H5.id_tienda  " & _
                        "WHERE TI.id_tienda <>0 " & _
                        " " + RegionSQL + EstadoSQL + CadenaSQL + TiendaSQL + " " & _
                        "ORDER BY TI.nombre_region,TI.nombre_estado,TI.ciudad ", gridImagenes)
        Else
            gridImagenes.Visible = False
        End If
    End Sub

    Protected Sub cmbCadena_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbDistribuidor.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarFotos()
    End Sub

    Protected Sub cmbRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbDistribuidor)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarFotos()
    End Sub

    Protected Sub cmbPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, RegionSQLCmb, "nombre_region", "id_region", cmbRegion)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, EstadoSQLCmb, "nombre_estado", "id_estado", cmbEstado)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbDistribuidor)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarFotos()
    End Sub

    Protected Sub cmbTienda_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbTienda.SelectedIndexChanged
        CargarFotos()
    End Sub

    Private Sub cmbEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEstado.SelectedIndexChanged
        SQLCombo()

        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, CadenaSQLCmb, "nombre_cadena", "id_cadena", cmbDistribuidor)
        Combo.LlenaDrop(ConexionFluidmaster.localSqlServer, TiendaSQLCmb, "nombre", "id_tienda", cmbTienda)

        CargarFotos()
    End Sub

End Class