<%@ Page Language="vb" 
    Culture="es-MX" 
    AutoEventWireup="false" 
    CodeBehind="DetalleObjetivo1.aspx.vb" 
    Inherits="procomlcd.DetalleObjetivo1"
    title="Mars Autoservicio - PDP detalle 1" %>

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
                            
            </tr>
            <tr>
                <td class="style3" colspan="2">
                                                OBJETIVO 1<br />
                                                &nbsp;ALCANCE DE PUNTO DE VENTA IDEAL</td>
                <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
                <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="style2">
            <asp:LinkButton ID="lnkExportar" runat="server" Font-Bold="True">Exportar a Excel</asp:LinkButton>
                    </td>
                </table>
    
    <asp:Panel ID="pnlGrid" runat="server" HorizontalAlign="Center">
        <asp:GridView ID="gridDetalle" runat="server" CellPadding="4" Height="375px" 
            Width="1500px" AutoGenerateColumns="False" 
                    style="text-align: center" ShowFooter="True" Font-Names="Arial" 
            Font-Size="Small" ForeColor="#333333" GridLines="None">
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:BoundField HeaderText="Periodo" DataField="nombre_periodo"/>
                <asp:BoundField HeaderText="Ejecutivo" DataField="EjecutivoMars"/>
                <asp:BoundField HeaderText="Promotor" DataField="id_usuario"/> 
                <asp:BoundField HeaderText="ID Tienda" DataField="codigo"/> 
                <asp:BoundField HeaderText="Tipo tienda" DataField="clasificacion_tienda"/> 
                <asp:BoundField HeaderText="Región" DataField="nombre_region"/>  
                <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena"/> 
                <asp:BoundField HeaderText="Tienda" DataField="nombre"/> 
                <asp:BoundField HeaderText="PVI" DataField="PVI"/> 
                <asp:BoundField HeaderText="Islas" DataField="1"/> 
                <asp:BoundField HeaderText="Rack de bulto grande" DataField="2"/> 
                <asp:BoundField HeaderText="Mix feeding" DataField="3"/> 
                <asp:BoundField HeaderText="Mega Exhibidor" DataField="4"/> 
                <asp:BoundField HeaderText="Mini rack" DataField="5"/>
                <asp:BoundField HeaderText="Cabecera" DataField="6"/>
                <asp:BoundField HeaderText="Plastival sencillo" DataField="7"/>
                <asp:BoundField HeaderText="Totales" DataField="T_12345"/>
                <asp:BoundField HeaderText="Objetivo" DataField="O_12345"/>
                <asp:BoundField HeaderText="%" DataField="Por_12345"/>
                <asp:BoundField HeaderText="Cabecera" DataField="6_B"/>
                <asp:BoundField HeaderText="Plastival sencillo" DataField="7_B"/>
                <asp:BoundField HeaderText="Exhibidor generico" DataField="8_B"/>
                <asp:BoundField HeaderText="Totales" DataField="T_678"/>
                <asp:BoundField HeaderText="Objetivo" DataField="O_678"/>
                <asp:BoundField HeaderText="%" DataField="Por_678"/>
                <asp:BoundField HeaderText="Balcones" DataField="9"/>
                <asp:BoundField HeaderText="Objetivo" DataField="O_9"/>
                <asp:BoundField HeaderText="%" DataField="Por_9"/>
                <asp:BoundField HeaderText="Tiras" DataField="10"/>
                <asp:BoundField HeaderText="Objetivo" DataField="O_10"/>
                <asp:BoundField HeaderText="%" DataField="Por_10"/>
                <asp:BoundField HeaderText="Lateros" DataField="11"/>
                <asp:BoundField HeaderText="Objetivo" DataField="O_11"/>
                <asp:BoundField HeaderText="%" DataField="Por_11"/>
                <asp:BoundField HeaderText="Poucheros" DataField="12"/>
                <asp:BoundField HeaderText="Objetivo" DataField="O_12"/>
                <asp:BoundField HeaderText="%" DataField="Por_12"/>
                <asp:BoundField HeaderText="PDQ's" DataField="13"/>
                <asp:BoundField HeaderText="Objetivo" DataField="O_13"/>
                <asp:BoundField HeaderText="%" DataField="Por_13"/>
                <asp:BoundField HeaderText="Otros Exhibidores" DataField="14"/>
                <asp:BoundField HeaderText="Objetivo" DataField="O_14"/>
                <asp:BoundField HeaderText="%" DataField="Por_14"/>
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
