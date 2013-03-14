<%@ Page Language="vb" 
    Culture="es-MX" 
    AutoEventWireup="false" 
    CodeBehind="AvisoMars.aspx.vb" 
    Inherits="procomlcd.AvisoMars" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    
        <br />
    
    <div style="text-align: center">
    
<asp:GridView ID="gridDetalle" runat="server" Height="225px" Width="375px" AutoGenerateColumns="False" 
            style="text-align: center" ShowHeader="False">
    <Columns>
        <asp:BoundField HeaderText="Aviso" DataField="notas"/> 
    </Columns>
</asp:GridView>
    
    </div>
    
    <br />
    
    </div>
    </form>
</body>
</html>
