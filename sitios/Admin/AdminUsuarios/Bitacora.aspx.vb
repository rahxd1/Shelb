Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class Bitacora
    Inherits System.Web.UI.Page

    Dim AnioSel, ClienteSel, TipoSel As String
    Dim ClienteSQL, UsuarioSQL, TipoUsuarioSQL, MesSQL, AnioSQL As String

    Sub SQLCombos()
        AnioSel = Acciones.Slc.cmb("DATEPART(YEAR,entrada)", cmbAnio.SelectedValue)
        TipoSel = Acciones.Slc.cmb("id_tipo", cmbTipoUsuario.SelectedValue)
        ClienteSel = Acciones.Slc.cmb("US.id_cliente", cmbCliente.SelectedValue)

        MesSQL = "select DISTINCT DATEPART(M,entrada)Num, " & _
                    "case when  DATEPART(M,entrada) = 1 then 'Enero'  " & _
                    "when DATEPART(m,entrada) = 2 then 'Febrero' " & _
                    "when DATEPART(m,entrada) = 3 then 'Marzo' " & _
                    "when DATEPART(m,entrada) = 4 then 'Abril' " & _
                    "when DATEPART(m,entrada) = 5 then 'Mayo' " & _
                    "when DATEPART(m,entrada) = 6 then 'Junio' " & _
                    "when DATEPART(m,entrada) = 7 then 'Julio' " & _
                    "when DATEPART(m,entrada) = 8 then 'Agosto' " & _
                    "when DATEPART(m,entrada) = 9 then 'Septiembre' " & _
                    "when DATEPART(m,entrada) = 10 then 'Octubre' " & _
                    "when DATEPART(m,entrada) = 11 then 'Noviembre' " & _
                    "when DATEPART(m,entrada) = 12 then 'Diciembre' end Mes " & _
                    "FROM Bitacora as BI " & _
                    "INNER JOIN Usuarios as US ON BI.id_usuario =  US.id_usuario " & _
                    "WHERE US.id_usuario <>'' " & _
                    " " + AnioSel + " " & _
                    "ORDER BY Num"

        AnioSQL = "select DISTINCT DATEPART(YEAR,entrada)Anio " & _
                    "FROM Bitacora as BI " & _
                    "INNER JOIN Usuarios as US ON BI.id_usuario =  US.id_usuario " & _
                    "ORDER BY Anio DESC"

        ClienteSQL = "SELECT DISTINCT CLI.id_cliente, CLI.nombre_cliente " & _
                    "FROM Usuarios as US " & _
                    "INNER JOIN Bitacora as BIT ON US.id_usuario= BIT.id_usuario " & _
                    "INNER JOIN Clientes as CLI ON CLI.id_cliente = US.id_cliente  " & _
                    "WHERE US.id_usuario <>'' " & _
                    "ORDER BY CLI.nombre_cliente"

        TipoUsuarioSQL = "SELECT DISTINCT US.id_tipo, TUS.tipo_usuario " & _
                    "FROM Usuarios as US " & _
                    "INNER JOIN Bitacora as BIT ON US.id_usuario= BIT.id_usuario " & _
                    "INNER JOIN Tipo_Usuarios as TUS ON TUS.id_tipo = US.id_tipo " & _
                    "WHERE US.id_usuario <>'' " & _
                    " " + ClienteSel + " " & _
                    "ORDER BY TUS.tipo_usuario"

        UsuarioSQL = "SELECT DISTINCT US.id_usuario, US.id_tipo " & _
                    "FROM Usuarios as US " & _
                    "INNER JOIN Bitacora as BIT ON US.id_usuario= BIT.id_usuario " & _
                    "WHERE US.id_usuario <>'' " & _
                    " " + ClienteSel + TipoSel + " " & _
                    "ORDER BY US.id_usuario"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SQLCombos()

            Combo.LlenaDrop(ConexionAdmin.localSqlServer, ClienteSQL, "nombre_cliente", "id_cliente", cmbCliente)
            Combo.LlenaDrop(ConexionAdmin.localSqlServer, TipoUsuarioSQL, "tipo_usuario", "id_tipo", cmbTipoUsuario)
            Combo.LlenaDrop(ConexionAdmin.localSqlServer, UsuarioSQL, "id_usuario", "id_usuario", cmbUsuario)
            Combo.LlenaDrop(ConexionAdmin.localSqlServer, MesSQL, "Mes", "Num", cmbMes)
            Combo.LlenaDrop(ConexionAdmin.localSqlServer, AnioSQL, "Anio", "Anio", cmbAnio)
        End If
    End Sub

    Sub CargarBitacora()
        pnlConsulta.Visible = True

        ClienteSQL = Acciones.Slc.cmb("US.id_cliente", cmbCliente.SelectedValue)
        UsuarioSQL = Acciones.Slc.cmb("BI.id_usuario", cmbUsuario.SelectedValue)
        TipoUsuarioSQL = Acciones.Slc.cmb("id_tipo", cmbTipoUsuario.SelectedValue)
        MesSQL = Acciones.Slc.cmb("DATEPART(m,entrada)", cmbMes.SelectedValue)
        AnioSQL = Acciones.Slc.cmb("DATEPART(YEAR,entrada)", cmbAnio.SelectedValue)

        CargaGrilla(ConexionAdmin.localSqlServer, _
                    "select BI.id_usuario, " & _
                        "Convert(Char(8), entrada, 108) As Hora, " & _
                        "Convert(varchar, entrada, 3) AS Fecha, " & _
                        "left(Convert(Char(8), entrada, 108),2) Horas, " & _
                        "case when  DATEPART(dw,entrada) = 1 then 'domingo'  " & _
                        "when DATEPART(dw,entrada) = 2 then 'Lunes' " & _
                        "when DATEPART(dw,entrada) = 3 then 'Martes' " & _
                        "when DATEPART(dw,entrada) = 4 then 'Miercoles' " & _
                        "when DATEPART(dw,entrada) = 5 then 'Jueves' " & _
                        "when DATEPART(dw,entrada) = 6 then 'Viernes' " & _
                        "when DATEPART(dw,entrada) = 7 then 'sabado' end as Dia " & _
                        "FROM Bitacora as BI " & _
                        "INNER JOIN Usuarios as US ON BI.id_usuario =  US.id_usuario " & _
                        "where BI.id_usuario<>'' " & _
                        " " + ClienteSQL + UsuarioSQL + TipoUsuarioSQL + MesSQL + AnioSQL + " " & _
                        "order by BI.entrada, BI.id_usuario", gridBitacora)
    End Sub

    Private Sub cmbTipoUsuario_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoUsuario.SelectedIndexChanged
        SQLCombos()
        Combo.LlenaDrop(ConexionAdmin.localSqlServer, UsuarioSQL, "id_usuario", "id_usuario", cmbUsuario)

        CargarBitacora()
    End Sub

    Private Sub cmbUsuario_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbUsuario.SelectedIndexChanged
        CargarBitacora()
    End Sub

    Protected Sub cmbMes_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbMes.SelectedIndexChanged
        CargarBitacora()
    End Sub

    Private Sub cmbAnio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAnio.SelectedIndexChanged
        CargarBitacora()
    End Sub

    Private Sub cmbCliente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCliente.SelectedIndexChanged
        SQLCombos()
        Combo.LlenaDrop(ConexionAdmin.localSqlServer, TipoUsuarioSQL, "tipo_usuario", "id_tipo", cmbTipoUsuario)
        Combo.LlenaDrop(ConexionAdmin.localSqlServer, UsuarioSQL, "id_usuario", "id_usuario", cmbUsuario)

        CargarBitacora()
    End Sub
End Class