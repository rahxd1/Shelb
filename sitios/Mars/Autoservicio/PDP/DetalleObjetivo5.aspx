<%@ Page Language="vb" 
    Culture="es-MX" 
    AutoEventWireup="false" 
    CodeBehind="DetalleObjetivo5.aspx.vb" 
    Inherits="procomlcd.DetalleObjetivo5" 
    title="Mars Autoservicio - PDP detalle 5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">

        .style1
        {
            width: 100%;
        }
        .style2
        {
            color: #FF0000;
            text-align: right;
        }
    

a:link {
	color:#d61719;
	text-decoration:none;
}

        .style3
        {
            color: #000000;
            text-align: center;
            font-weight: bold;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    
        <table class="style1">
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td style="text-align: right">
                    <img alt="" src="../../../../Img/arrow.gif" class="style2" />
                            <asp:LinkButton ID="lnkObjetivo1" runat="server" Font-Bold="True">Regresar</asp:LinkButton>
                                            </td>
            </tr>
            <tr>
                <td class="style3" colspan="2">
                                                OBJETIVO 5<br />
                                                &nbsp;CUMPLIMIENTO DE CAPTURA</td>
            </tr>
            <tr>
                <td class="style2" colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2" colspan="2">
            <asp:LinkButton ID="lnkExportar" runat="server" Font-Bold="True">Exportar a Excel</asp:LinkButton>
                </td>
            </tr>
        </table>
    
    <asp:Panel ID="pnlGrid" runat="server" HorizontalAlign="Center">
        <asp:GridView ID="gridDetalle" runat="server" CellPadding="4" Height="86px" 
            Width="42%" AutoGenerateColumns="False" 
                    style="text-align: center" ShowFooter="True" Font-Names="Arial" 
            Font-Size="Small" ForeColor="#333333" GridLines="None">
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:BoundField HeaderText="Periodo" DataField="nombre_periodo"/>
                <asp:BoundField HeaderText="Tiendas" DataField="Tiendas"/>
                <asp:BoundField HeaderText="Q1" DataField="Q1"/> 
                <asp:BoundField HeaderText="Q2" DataField="Q2"/>
            </Columns>
            <FooterStyle BackColor="#D0D8E8" ForeColor="White" Font-Bold="True" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#4F81BD" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="#D0D8E8" />
        </asp:GridView>
        <br />
        <asp:GridView ID="gridDetalle2" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" Font-Names="Arial" Font-Size="Small" Height="86px" 
            ShowFooter="True" Width="100%" 
            ForeColor="#333333" GridLines="None">
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:BoundField DataField="nombre_periodo" HeaderText="Periodo" />
                <asp:BoundField DataField="nombre_cadena" HeaderText="Cadena" />
                <asp:BoundField DataField="nombre" HeaderText="Tienda" />
                <asp:BoundField DataField="Q1" HeaderText="Q1" />
                <asp:BoundField DataField="Q2" HeaderText="Q2" />
            </Columns>
            <FooterStyle BackColor="#D0D8E8" ForeColor="White" Font-Bold="True" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#4F81BD" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="#D0D8E8" />
        </asp:GridView>
     </asp:Panel>
    </div>
    </form>
</body>
</html>
