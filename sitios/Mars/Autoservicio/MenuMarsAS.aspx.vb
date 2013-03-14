Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports ISODates.Dates
Imports procomlcd

Partial Public Class MenuMarsAS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionMars.localSqlServer, HttpContext.Current.User.Identity.Name)

            Avisos()
            'Documentos()
            Calendario()

        End If
    End Sub

    Sub Avisos()
        CargaGrilla(ConexionMars.localSqlServer, _
                    "SELECT PRO.id_aviso,PRO.nombre_aviso,PRO.fecha_inicio,PRO.fecha_fin,CAD.nombre_cadena " & _
                    "FROM AS_Avisos as PRO " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = PRO.id_cadena " & _
                    "WHERE fecha_inicio<='" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                    "AND fecha_fin>='" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' AND PRO.id_cadena=0 " & _
                    "UNION ALL " & _
                    "SELECT PRO.id_aviso,PRO.nombre_aviso,PRO.fecha_inicio,PRO.fecha_fin,CAD.nombre_cadena " & _
                    "FROM AS_Avisos as PRO " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena = PRO.id_cadena " & _
                    "INNER JOIN (SELECT DISTINCT CAD.id_cadena FROM AS_CatRutas as RUT " & _
                    "INNER JOIN AS_Tiendas as TI ON TI.id_tienda=RUT.id_tienda " & _
                    "INNER JOIN Cadenas_Tiendas as CAD ON CAD.id_cadena=TI.id_cadena " & _
                    "WHERE RUT.id_usuario='" & HttpContext.Current.User.Identity.Name & "')RUT ON RUT.id_cadena= PRO.id_cadena " & _
                    "WHERE fecha_inicio<='" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' " & _
                    "AND fecha_fin>='" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "' AND PRO.id_cadena<>0", _
                    gridAvisos)
    End Sub

    'Sub Documentos()
    '    CargaGrilla(ConexionMars.localSqlServer, _
    '                "SELECT * FROM AS_Avisos " & _
    '                "WHERE id_tipo_Si=" & Tipo_usuario & " " & _
    '                "AND id_tipo_No<>" & Tipo_usuario & "", gridOtros)
    'End Sub

    Sub Calendario()
        CargaGrilla(ConexionMars.localSqlServer, _
                    "execute Ver_Calendario '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "'", _
                    gridCalendario)

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "execute Ver_Calendario '" & ISODates.Dates.SQLServerDate(CDate(Date.Today)) & "'")
        If Tabla.Rows.Count > 0 Then
            lblPeriodo.Text = Tabla.Rows(0)("periodo")
        End If

        Tabla.Dispose()
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        Panel1.Visible = True
        Button1.Visible = False
        Button2.Visible = True
        Panel2.Visible = False
        Panel3.Visible = False

    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        Panel1.Visible = False
        Button1.Visible = True
        Button2.Visible = False
        Panel2.Visible = True
        Panel3.Visible = True
    End Sub
End Class