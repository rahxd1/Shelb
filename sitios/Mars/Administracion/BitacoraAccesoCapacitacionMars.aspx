<%@ Page Language="vb" 
    Masterpagefile="~/sitios/Mars/Capacitacion/Capacitacion.master"
    AutoEventWireup="false" 
    CodeBehind="BitacoraAccesoCapacitacionMars.aspx.vb" 
    Inherits="procomlcd.BitacoraAccesoCapacitacionMars"
    title="Capacitación Mars - Bitacora Accesos" %>

<asp:Content id="Content1" ContentPlaceHolderID="ContentPlaceHolderCapacitacion" runat="Server">
    <!--
Title Under Menu  
--> 
    <div id="titulo-pagina">
        Bitacora acceso usuarios</div>
    <div id="contenido-three-column" style="width: 770px">
        <!--

  CONTENT SIDE 1 COLUMN id=""

  -->
        <div id="content-side1-three-column" >
            <ul class="list-of-links">
                <li><asp:HyperLink ID="hyperEdicion" runat="server" NavigateUrl="~/sitios/Mars/Capacitacion/AdminCapacitacionMars.aspx" >Edición Promotores, Supervisores y Ejecutivos</asp:HyperLink></li>
                <li><asp:HyperLink ID="hyperAvisos" runat="server" NavigateUrl="~/sitios/Mars/Capacitacion/AdminAvisoCapacitacionMars.aspx" >Edición Avisos</asp:HyperLink></li>
                <li><asp:HyperLink ID="hyperBitacora" runat="server" NavigateUrl="~/sitios/Mars/Capacitacion/BitacoraAccesoCapacitacionMars.aspx" >Bitacora Accesos</asp:HyperLink></li>
                <li><asp:HyperLink ID="HyperParticipacion" runat="server" NavigateUrl="~/sitios/Mars/Capacitacion/AdminParticipacionMars.aspx" >Participación Usuarios</asp:HyperLink></li>
            </ul>
        </div>
        <!--

  CENTER COLUMN

  -->
        <div id="content-main-three-column">
            <table style="width: 100%">
                <tr>
                    <td style="width: 115px">
                        Promotor</td>
                    <td colspan="2" style="text-align: left">
                        <asp:DropDownList ID="cmbPromotor" runat="server" Height="22px" Width="250px" 
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 115px">
                        Supervisor</td>
                    <td colspan="2" style="text-align: left">
                        <asp:DropDownList ID="cmbSupervisor" runat="server" Height="22px" Width="250px" 
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 115px">
                        Ejecutivo Mars</td>
                    <td colspan="2" style="text-align: left">
                        <asp:DropDownList ID="cmbEjecutivoMars" runat="server" Height="22px" Width="250px" 
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 115px">
                        Ejecutivo Cuenta</td>
                    <td colspan="2" style="text-align: left">
                        <asp:DropDownList ID="cmbEjecutivoCuenta" runat="server" Height="22px" Width="250px" 
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 115px">
                        Periodo</td>
                    <td colspan="2">
                        
                        <asp:DropDownList ID="cmbPeriodo" runat="server" Height="22px" Width="250px" 
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 115px">
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 115px">
                        &nbsp;</td>
                    <td style="width: 238px; text-align: center;">
            
            <asp:Button ID="btnExcel" runat="server" Text="Exportar a Excel" 
                            style="text-align: center" Enabled="False" />
                    </td>
                    <td style="text-align: center">
                        <asp:Button ID="btnGenerar" runat="server" Text="Generar" />
                    </td>
                </tr>
            </table>
            
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
                <asp:UpdatePanel ID="FusionChartsUP" runat="server" Visible="False">
                    <ContentTemplate>
                        <asp:Panel ID="PanelFS" runat="server">
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel> 
            <asp:Panel ID="PnlGrid" runat="server" Visible="False" Width="487px">
                <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" 
                    GridLines="None" Width="533px" ShowFooter="True">
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField HeaderText="Usuario" DataField="id_usuario"/> 
                            <asp:BoundField HeaderText="Fecha" DataField="entrada"/> 
                        </Columns>
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                
             </asp:Panel>
                <br />
        </div>
        
        <!--CONTENT SIDE COLUMN-->
        <div class="clear">
        </div>
    </div>
</asp:Content>