Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class AdminEntrenamientosMars
    Inherits System.Web.UI.Page

    Dim SeleccionIDProceso As String
    Dim UsuarioSel As String
    Dim Suma, Generados As Integer
    Dim DuplicaRutas As String

    Private Sub VerDatos()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT * FROM AS_Entrenamientos_Tecnicos WHERE id_entrenamiento=" & SeleccionIDProceso & "")
        If Tabla.Rows.Count > 0 Then
            txtNombreEntrenamiento.Text = Tabla.Rows(0)("nombre_entrenamiento")
            txtDescripcion.Text = Tabla.Rows(0)("descripcion")
            txtFechaInicio.Text = Tabla.Rows(0)("fecha_inicio")
            txtFechaFin.Text = Tabla.Rows(0)("fecha_fin")
        End If

        tabla.Dispose()
    End Sub

    Public Function CargarProcesos(ByVal TipoProceso As String) As Integer
        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT * FROM AS_Entrenamientos_Tecnicos ORDER BY id_entrenamiento", _
                    Me.gridEntrenamiento)

        pnlConsulta.Visible = True
        pnlNuevo.Visible = False
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Guardar(lblIDEntrenamiento.Text, txtNombreEntrenamiento.Text, txtDescripcion.Text, _
                txtFechaInicio.Text, txtFechaFin.Text)
        Borrar()

        pnlNuevo.Visible = False
        pnlConsulta.Visible = True
    End Sub

    Public Function Guardar(ByVal IDEntrenamiento As String, ByVal NombreEntrenamiento As String, _
                            ByVal Descripcion As String, ByVal FechaInicio As String, _
                            ByVal FechaFin As String) As Integer
        Dim fecha_inicio As String = ISODates.Dates.SQLServerDate(CDate(FechaInicio))
        Dim fecha_fin As String = ISODates.Dates.SQLServerDate(CDate(FechaFin))

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT * From AS_Entrenamientos_Tecnicos " & _
                                               "WHERE id_entrenamiento=" & IDEntrenamiento & "")
        If Tabla.Rows.Count = 1 Then
            BD.Execute(ConexionMars.localSqlServer, _
                       "UPDATE AS_Entrenamientos_Tecnicos " & _
                        "SET nombre_entrenamiento='" & NombreEntrenamiento & "', " & _
                        "descripcion='" & Descripcion & "', fecha_inicio='" & fecha_inicio & "', " & _
                        "fecha_fin='" & fecha_fin & "' ")

            lblAviso.Text = "LOS CAMBIOS DEL USUARIO SE REALIZARON CORRECTAMENTE"
        Else
            BD.Execute(ConexionMars.localSqlServer, _
                       "INSERT INTO AS_Entrenamientos_Tecnicos" & _
                       "(nombre_entrenamiento, descripcion,fecha_inicio, fecha_fin) " & _
                       "VALUES('" & NombreEntrenamiento & "','" & Descripcion & "', " & _
                       "'" & fecha_inicio & "','" & fecha_fin & "')")

            lblAviso.Text = "LA INFORMACIÓN SE GUARDÓ CORRECTAMENTE"
        End If

        pnlNuevo.Visible = False
        'CargarProcesos(TipoProceso)
        CrearEventos()

        If Generados >= 1 Then
            lblAviso.Text = "SE CARGO EL PROCESO SELECCIONADO PARA " & Generados & " PROMOTORES EXITOSAMENTE."
        End If

    End Function

    Sub Borrar()
        txtNombreEntrenamiento.Text = ""
        txtDescripcion.Text = ""
        txtFechaInicio.Text = ""
        txtFechaFin.Text = ""
        lblAviso.Text = ""
    End Sub

    Protected Sub lnkNuevo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkNuevo.Click
        pnlNuevo.Visible = True
        pnlConsulta.Visible = False
        txtNombreEntrenamiento.Focus()
        Borrar()
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Borrar()
        pnlNuevo.Visible = False
        pnlConsulta.Visible = False
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

        End If
    End Sub

    Private Sub gridProcesos_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gridEntrenamiento.RowEditing
        SeleccionIDProceso = gridEntrenamiento.Rows(e.NewEditIndex).Cells(1).Text
        lblIDEntrenamiento.Text = gridEntrenamiento.Rows(e.NewEditIndex).Cells(1).Text

        If SeleccionIDProceso = "" Then
            e.Cancel = True
            lblAviso.Text = "Error al intentar editar"
        Else
            pnlNuevo.Visible = True
            pnlConsulta.Visible = False
            txtNombreEntrenamiento.Focus()
            VerDatos()
        End If
    End Sub

    Private Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete
        gridEntrenamiento.Columns(1).Visible = False
    End Sub

    Protected Sub lnkConsultas_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkConsultas.Click
        CargarProcesos(2)
    End Sub

    Sub CrearEventos()
        Generados = 0
        DuplicaRutas = "DUPLICAR"

        CargaGrilla(ConexionMars.localSqlServer, "SELECT * FROM AS_CatRutas", Me.gridRuta)

        DuplicaRutas = ""
    End Sub

    Private Sub gridRuta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridRuta.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            If DuplicaRutas = "DUPLICAR" Then
                UsuarioSel = e.Row.Cells(0).Text

                Duplicalo(lblIDEntrenamiento.Text, UsuarioSel)
            End If
        End If
    End Sub

    Public Function Duplicalo(ByVal IDProceso As String, ByVal IDUsuario As String) As Integer
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT * FROM AS_Entrenamientos_Rutas_Eventos " & _
                                         "WHERE id_entrenamiento=" & IDProceso & " " & _
                                         "AND id_usuario='" & IDUsuario & "'")
        If tabla.Rows.Count = 1 Then
            lblAviso.Text = "NO SE CARGO NINGUN PROCESO PARA LOS PROMOTORES"
            Exit Function
        Else
            BD.Execute(ConexionMars.localSqlServer, _
                       "INSERT INTO AS_Entrenamientos_Rutas_Eventos" & _
                       "(id_entrenamiento, id_usuario) " & _
                       "VALUES(" & IDProceso & ", '" & IDUsuario & "')")

            Generados = Generados + 1
        End If
    End Function
End Class