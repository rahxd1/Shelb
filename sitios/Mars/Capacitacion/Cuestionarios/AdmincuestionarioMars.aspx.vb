Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports ISODates.Dates

Partial Public Class AdmincuestionarioMars
    Inherits System.Web.UI.Page

    Sub VerDatos()
        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionMars.localSqlServer, _
                                               "SELECT * FROM Cap_Cuestionarios_Respuestas " & _
                                  "WHERE id_cuestionario=1 AND id_pregunta=3 and id_respuesta=3")
        If Tabla.Rows.Count > 0 Then
            txtRespuesta.Text = Tabla.Rows(0)("respuesta")
        End If

        tabla.Dispose()
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Guardar(txtRespuesta.Text)
    End Sub

    Public Function Guardar(ByVal Respuesta As String) As Integer
        BD.Execute(ConexionMars.localSqlServer, _
                   "UPDATE Cap_Cuestionarios_Respuestas " & _
                    "SET respuesta=" & Respuesta & " " & _
                    "WHERE id_cuestionario=1 AND id_pregunta=3 and id_respuesta=3")
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerDatos()
        End If
    End Sub
End Class