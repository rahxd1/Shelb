<%@ Page Language="vb" 
    Culture="es-MX" 
    AutoEventWireup="false" 
    CodeBehind="DetalleObjetivo2.aspx.vb" 
    Inherits="procomlcd.DetalleObjetivo2"
    title="Mars Autoservicio - PDP detalle 2" %>

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
                    <img alt="" src="../../Img/arrow.gif" class="style2" />
                            <asp:LinkButton ID="lnkObjetivo1" runat="server" Font-Bold="True">Regresar</asp:LinkButton>
                                            </td>
            </tr>
            <tr>
                <td class="style3" colspan="2">
                                                OBJETIVO 2<br />
                                                &nbsp;ALCANCE SEGMENTO</td>
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
            <tr>
                <td class="style2" colspan="2">
                    &nbsp;</td>
            </tr>
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
                <asp:BoundField HeaderText="Area nielsen" DataField="area_nielsen"/>  
                <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena"/> 
                <asp:BoundField HeaderText="Tienda" DataField="nombre"/> 
                <asp:BoundField HeaderText="Perro seco" DataField="PS"/>
                <asp:BoundField HeaderText="Objetivo" DataField="O_ps"/>
                <asp:BoundField HeaderText="Cachorro seco" DataField="PC"/>
                <asp:BoundField HeaderText="Objetivo" DataField="O_pc"/>
                <asp:BoundField HeaderText="Perro húmedo" DataField="PH"/>
                <asp:BoundField HeaderText="Objetivo" DataField="O_ph"/>
                <asp:BoundField HeaderText="Perro botanas" DataField="PB"/>
                <asp:BoundField HeaderText="Objetivo" DataField="O_pb"/>
                <asp:BoundField HeaderText="Gato seco" DataField="GS"/>
                <asp:BoundField HeaderText="Objetivo" DataField="O_gs"/>
                <asp:BoundField HeaderText="Gato húmedo" DataField="GH"/>
                <asp:BoundField HeaderText="Objetivo" DataField="O_gh"/>
                <asp:BoundField HeaderText="Gato botanas" DataField="GB"/>
                <asp:BoundField HeaderText="Objetivo" DataField="O_gb"/>
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
