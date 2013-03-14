<%@ Page Language="vb" 
    Culture="es-MX" 
    AutoEventWireup="false" 
    CodeBehind="DetallePrecioJovy.aspx.vb" 
    Inherits="procomlcd.DetallePrecioJovy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    
    <asp:Button ID="btnExcel" runat="server" Text="Exportar a Excel" 
    onclick="btnExcel_Click" />
    
        <br />
    
    <br />
    
<asp:GridView ID="gridDetalle" runat="server" Height="121px" Width="663px" AutoGenerateColumns="False" 
            style="text-align: center" ShowFooter="True">
    <Columns>
        <asp:BoundField HeaderText="Region" DataField="nombre_region"/>
        <asp:BoundField HeaderText="Promotor" DataField="id_usuario"/>
        <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena"/> 
        <asp:BoundField HeaderText="Tienda" DataField="nombre"/> 
        <asp:BoundField HeaderText="Producto" DataField="nombre_producto"/> 
        <asp:BoundField HeaderText="Precio" DataField="precio"/> 
    </Columns>
    <FooterStyle BackColor="Yellow" />
    <HeaderStyle BackColor="Black" />
</asp:GridView>
    
    </div>
    </form>
</body>
</html>
