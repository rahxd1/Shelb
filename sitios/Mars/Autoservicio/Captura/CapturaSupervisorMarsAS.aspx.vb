Imports System.Data
Imports System.Data.SqlClient
Imports procomlcd
Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports ISODates.Dates
Imports System.Security.Cryptography
Imports procomlcd.Permisos

Partial Public Class CapturaSupervisorMarsAS
    Inherits System.Web.UI.Page

    Dim Procesos, Entrenamientos As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionMars.localSqlServer, HttpContext.Current.User.Identity.Name)

            VerDatos()
        End If
    End Sub

    Private Sub VerDatos()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select PRO.id_proceso,PRO.nombre_proceso,TPRO.nombre_tipoproceso, RE.id_usuario, RE.fecha " & _
                        "FROM AS_Procesos_Rutas_Eventos as RE " & _
                        "INNER JOIN AS_Procesos as PRO ON PRO.id_proceso = RE.id_proceso " & _
                        "INNER JOIN AS_Tipo_Procesos as TPRO ON TPRO.tipo_proceso = PRO.tipo_proceso " & _
                        "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario = RE.id_usuario " & _
                        "WHERE REL.id_supervisor='" & HttpContext.Current.User.Identity.Name & "' AND RE.estatus=0 " & _
                        "ORDER BY RE.id_usuario")

        If Tabla.Rows.Count = 0 Then
            Procesos = "select DISTINCT PRO.id_proceso,PRO.nombre_proceso,TPRO.nombre_tipoproceso, RE.id_usuario, RE.fecha " & _
                        "FROM AS_Procesos_Rutas_Eventos as RE " & _
                        "INNER JOIN AS_Procesos as PRO ON PRO.id_proceso = RE.id_proceso " & _
                        "INNER JOIN AS_Tipo_Procesos as TPRO ON TPRO.tipo_proceso = PRO.tipo_proceso " & _
                        "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario = RE.id_usuario " & _
                        "INNER JOIN Usuarios as US ON US.ciudad = REL.ejecutivo " & _
                        "WHERE US.id_usuario='" & HttpContext.Current.User.Identity.Name & "' AND REL.id_supervisor='' AND RE.estatus=0 " & _
                        "ORDER BY RE.id_usuario"
        End If
        Tabla.Dispose()

        CargaGrilla(ConexionMars.localSqlServer, Procesos, Me.gridProcesos)
        Dim TablaEnt As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "select ENT.id_entrenamiento,ENT.nombre_entrenamiento,RE.id_usuario, RE.fecha " & _
                        "FROM AS_Entrenamientos_Rutas_Eventos as RE " & _
                        "INNER JOIN AS_Entrenamientos_Tecnicos as ENT ON ENT.id_entrenamiento = RE.id_entrenamiento " & _
                        "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario = RE.id_usuario " & _
                        "WHERE REL.id_supervisor='" & HttpContext.Current.User.Identity.Name & "' AND RE.estatus=0 " & _
                        "ORDER BY RE.id_usuario")
        If TablaEnt.Rows.Count = 0 Then
            Entrenamientos = "select DISTINCT ENT.id_entrenamiento,ENT.nombre_entrenamiento,RE.id_usuario, RE.fecha " & _
                        "FROM AS_Entrenamientos_Rutas_Eventos as RE " & _
                        "INNER JOIN AS_Entrenamientos_Tecnicos as ENT ON ENT.id_entrenamiento = RE.id_entrenamiento " & _
                        "INNER JOIN Usuarios_Relacion as REL ON REL.id_usuario = RE.id_usuario " & _
                        "INNER JOIN Usuarios as US ON US.ciudad = REL.ejecutivo " & _
                        "WHERE US.id_usuario='" & HttpContext.Current.User.Identity.Name & "' AND REL.id_supervisor='' AND RE.estatus=0 " & _
                        "ORDER BY RE.id_usuario"
        End If
        TablaEnt.Dispose()

        CargaGrilla(ConexionMars.localSqlServer, Entrenamientos, Me.gridEntrenamiento)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        btnGuardar.Enabled = False

        GuardarFechas()

        Response.Redirect("CapturasASMars.aspx")
    End Sub

    Sub GuardarFechas()
        ''//Productos propios
        Dim IDProceso As Integer
        For i = 0 To CInt(Me.gridProcesos.Rows.Count) - 1
            IDProceso = Me.gridProcesos.DataKeys(i).Value.ToString()
            Dim lblUsuario As Label = CType(Me.gridProcesos.Rows(i).FindControl("lblUsuario"), Label)
            Dim txtFecha As TextBox = CType(Me.gridProcesos.Rows(i).FindControl("txtFecha"), TextBox)

            GuardaFechaProceso(lblUsuario.Text, IDProceso, txtFecha.Text)
        Next

        Dim IDEntrenamiento As Integer
        For i = 0 To CInt(Me.gridEntrenamiento.Rows.Count) - 1
            IDEntrenamiento = Me.gridEntrenamiento.DataKeys(i).Value.ToString()
            Dim lblUsuario As Label = CType(Me.gridEntrenamiento.Rows(i).FindControl("lblUsuario"), Label)
            Dim txtFecha As TextBox = CType(Me.gridEntrenamiento.Rows(i).FindControl("txtFecha"), TextBox)

            GuardaFechaEntrenamiento(lblUsuario.Text, IDEntrenamiento, txtFecha.Text)
        Next
    End Sub

    Private Function GuardaFechaProceso(ByVal Usuario As String, ByVal Proceso As Integer, ByVal Fecha As String) As Boolean
        Dim cnn As New SqlConnection(ConexionMars.localSqlServer)
        cnn.Open()

        Dim SQLEditar As New SqlCommand("execute AS_EditarProceso @id_usuario,@proceso,@fecha", cnn)
        SQLEditar.Parameters.AddWithValue("@id_usuario", Usuario)
        SQLEditar.Parameters.AddWithValue("@proceso", Proceso)
        If Fecha <> "" Then
            SQLEditar.Parameters.AddWithValue("@fecha", ISODates.Dates.SQLServerDate(CDate(Fecha))) : Else
            SQLEditar.Parameters.AddWithValue("@fecha", DBNull.Value) : End If

        SQLEditar.ExecuteNonQuery()
        SQLEditar.Dispose()

        cnn.Close()
        cnn.Dispose()
    End Function

    Private Function GuardaFechaEntrenamiento(ByVal Usuario As String, ByVal Entrenamiento As Integer, ByVal Fecha As String) As Boolean
        Dim cnn As New SqlConnection(ConexionMars.localSqlServer)
        cnn.Open()

        Dim SQLEditar As New SqlCommand("execute AS_EditarEntrenamiento @id_usuario,@id_entrenamiento,@fecha", cnn)
        SQLEditar.Parameters.AddWithValue("@id_usuario", Usuario)
        SQLEditar.Parameters.AddWithValue("@id_entrenamiento", Entrenamiento)
        If Fecha <> "" Then
            SQLEditar.Parameters.AddWithValue("@fecha", ISODates.Dates.SQLServerDate(CDate(Fecha))) : Else
            SQLEditar.Parameters.AddWithValue("@fecha", DBNull.Value) : End If

        SQLEditar.ExecuteNonQuery()
        SQLEditar.Dispose()

        cnn.Close()
        cnn.Dispose()
    End Function

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("CapturasASMars.aspx")
    End Sub

End Class