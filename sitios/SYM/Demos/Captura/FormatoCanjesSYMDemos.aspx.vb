Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports procomlcd
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.Page
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports ISODates.Dates
Imports System.Security.Cryptography

Partial Public Class FormatoCanjesSYMDemos
    Inherits System.Web.UI.Page

    Dim IDCadena, IDUsuario, IDPeriodo, NombreCadena, Folio As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            VerPermisos(ConexionSYM.localSqlServer, HttpContext.Current.User.Identity.Name)

            VerDatos()

            If Edicion = 1 Then
                btnAgregar.Visible = True : Else
                btnAgregar.Visible = False : End If
        End If
    End Sub

    Private Sub VerDatos()
        Datos()

        Dim Tabla As DataTable = Tablas.Ver.DT(ConexionSYM.localSqlServer, _
                                               "select * FROM Demos_Historial " & _
                                               "WHERE(folio_historial = " & Folio & "")
        If Tabla.Rows.Count > 0 Then
            lblTienda.Text = Tabla.Rows(0)("nombre_tienda")
            lblNombres.Text = Tabla.Rows(0)("nombre_demo")
            lblApellidoPaterno.Text = Tabla.Rows(0)("apellido_paterno")
            lblApellidoMaterno.Text = Tabla.Rows(0)("apellido_materno")
        End If

        lblCadena.Text = NombreCadena

        Tabla.Dispose()

        CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT PROD.id_producto, PROD.nombre_producto, 0 as ventas " & _
                    "FROM Productos_Demos AS PROD  " & _
                    "WHERE canje=1 ORDER BY PROD.id_producto", gridProductosCanjes)

        CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT * FROM Demos_Canjes_Historial_Det as HDET1 " & _
                     "FULL JOIN (select folio_historial_det,id_producto, ventas " & _
                     "FROM Demos_Productos_Canjes_Historial_Det )PVT " & _
                     "PIVOT(SUM(ventas) FOR id_producto  " & _
                     "IN([1],[2],[3],[4]))as HDET2  " & _
                     "ON HDET1.folio_historial_det=HDET2.folio_historial_det " & _
                     "WHERE HDET1.folio_historial=" & Folio & "  " & _
                     "ORDER BY fecha,no_ticket", gridCanjes)
    End Sub

    Sub Datos()
        Folio = Request.Params("folio")
        IDCadena = Request.Params("cadena")
        IDUsuario = Request.Params("usuario")
        IDPeriodo = Request.Params("periodo")
        NombreCadena = Request.Params("nombrecadena")
    End Sub

    Protected Sub lnkOtraCaptura_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkOtraCaptura.Click
        Response.Redirect("~/sitios/SYM/Demos/Captura/RutaSYMDemos.aspx")
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAgregar.Click
        Folio = Request.Params("folio")
        Dim Fecha As String = ISODates.Dates.SQLServerDate(CDate(txtFecha.Text))

        Dim Folio_Det As Integer

        Folio_Det = BD.RT.Execute(ConexionSYM.localSqlServer, _
                   "INSERT INTO Demos_Canjes_Historial_Det" & _
                   "(folio_historial, fecha, no_ticket,nombre_cliente, telefono, toallas) " & _
                   "VALUES(" & Folio & ",'" & Fecha & "'," & txtTicket.Text & ", " & _
                   "'" & txtNombreCliente.Text & "','" & txtTelefono.Text & "'," & txtToallas.Text & ") " & _
                   "SELECT @@IDENTITY AS 'folio_historial_det' ")

        Dim IDProducto As Integer
        For i = 0 To gridProductosCanjes.Rows.Count - 1
            IDProducto = gridProductosCanjes.DataKeys(i).Value.ToString()
            Dim txtVentas As TextBox = CType(gridProductosCanjes.Rows(i).FindControl("txtVentas"), TextBox)

            If txtVentas.Text = "" Or txtVentas.Text = " " Then
                txtVentas.Text = 0 : End If

            BD.Execute(ConexionSYM.localSqlServer, _
                       "INSERT INTO Demos_Productos_Canjes_Historial_Det" & _
                       "(folio_historial_det,folio_historial, id_producto, ventas) " & _
                       "VALUES(" & Folio_Det & "," & Folio & "," & IDProducto & ", " & _
                       "" & txtVentas.Text & ")")
        Next

        txtFecha.Text = ""
        txtTicket.Text = ""
        txtNombreCliente.Text = ""
        txtTelefono.Text = ""
        txtToallas.Text = ""

        CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT PROD.id_producto, PROD.nombre_producto, 0 as ventas " & _
                    "FROM Productos_Demos AS PROD  " & _
                    "WHERE canje=1 ORDER BY PROD.id_producto", gridProductosCanjes)

        CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT * FROM Demos_Canjes_Historial_Det as HDET1 " & _
                     "FULL JOIN (select folio_historial_det,id_producto, ventas " & _
                     "FROM Demos_Productos_Canjes_Historial_Det )PVT " & _
                     "PIVOT(SUM(ventas) FOR id_producto  " & _
                     "IN([1],[2],[3],[4]))as HDET2  " & _
                     "ON HDET1.folio_historial_det=HDET2.folio_historial_det " & _
                     "WHERE HDET1.folio_historial=" & Folio & "  " & _
                     "ORDER BY fecha,no_ticket", gridCanjes)
    End Sub

    Private Sub gridCanjes_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridCanjes.RowDeleting
        BD.Execute(ConexionSYM.localSqlServer, _
                   "DELETE FROM Demos_Canjes_Historial_Det " & _
                   "WHERE folio_historial_det=" & gridCanjes.DataKeys(e.RowIndex).Value.ToString() & "")


        ''Datos productos
        BD.Execute(ConexionSYM.localSqlServer, _
                   "DELETE FROM Demos_Productos_Canjes_Historial_Det " & _
                   "WHERE folio_historial_det=" & gridCanjes.DataKeys(e.RowIndex).Value.ToString() & "")

        CargaGrilla(ConexionSYM.localSqlServer, _
                    "SELECT * FROM Demos_Canjes_Historial_Det as HDET1 " & _
                     "FULL JOIN (select folio_historial_det,id_producto, ventas " & _
                     "FROM Demos_Productos_Canjes_Historial_Det )PVT " & _
                     "PIVOT(SUM(ventas) FOR id_producto  " & _
                     "IN([1],[2],[3],[4]))as HDET2  " & _
                     "ON HDET1.folio_historial_det=HDET2.folio_historial_det " & _
                     "WHERE HDET1.folio_historial=" & Request.Params("folio") & "  " & _
                     "ORDER BY fecha,no_ticket", gridCanjes)
    End Sub
End Class