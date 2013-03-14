<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Jovy/Jovy.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteCaducidadesJovy.aspx.vb" 
    Inherits="procomlcd.ReporteCaducidadesJovy" 
    Title="Jovy- Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderJovy" runat="Server">

    <!--titulo-pagina-->
        <div id="titulo-pagina">
                        Reporte caducidades</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            
            <asp:UpdatePanel ID="updPnl" runat="server">
                <ContentTemplate>
                    <div id="menu-regresar">
                        <img alt="" src="../../../Img/arrow.gif" />
                        <asp:HyperLink ID="lnkRegresar" runat="server" 
                            NavigateUrl="~/sitios/Jovy/Reportes/ReportesJovy.aspx">Regresar</asp:HyperLink><br />
                    </div>
                    
                    <asp:Label ID="lblPeriodo" runat="server" Text="Periodo" CssClass="lbl" />
                    <asp:DropDownList ID="cmbPeriodo" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblRegion" runat="server" Text="Región" CssClass="lbl" />
                    <asp:DropDownList ID="cmbRegion" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblPromotor" runat="server" Text="Promotor" CssClass="lbl" />
                    <asp:DropDownList ID="cmbPromotor" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblCadena" runat="server" Text="Cadena" CssClass="lbl" />
                    <asp:DropDownList ID="cmbCadena" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblTienda" runat="server" Text="Tienda" CssClass="lbl" />
                    <asp:DropDownList ID="cmbTienda" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblCaducidad" runat="server" Text="Caducidad" CssClass="lbl" />
                    <asp:DropDownList ID="cmbCaducidad" runat="server" CssClass="cmb" 
                        AutoPostBack="True">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem Value="1">en 1 mes o caducados</asp:ListItem>
                        <asp:ListItem Value="2">en 2 meses</asp:ListItem>
                        <asp:ListItem Value="3">mas de 3 meses</asp:ListItem>
                    </asp:DropDownList><br />
                    
                    <br />
                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both" Height="350px"> 
                        <asp:Panel ID="pnlGrafica" runat="server" /> 
                        <asp:GridView ID="gridResumen" runat="server" AutoGenerateColumns="False" 
                            Width="100%" ShowFooter="True" CssClass="grid-view">
                                <Columns>
                                    <asp:BoundField HeaderText="" DataField="Mes"/> 
                                    <asp:BoundField HeaderText="Tiendas con caducidades" DataField="Tiendas"/>  
                                </Columns>   
                                <FooterStyle CssClass="grid-footer" />                   
                            <EmptyDataTemplate>
                                <h1>Sin información</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>                           
                        <br />
                            
                        <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" 
                            Width="100%" ShowFooter="True" CssClass="grid-view">
                                <Columns>
                                    <asp:BoundField HeaderText="Plaza" DataField="nombre_region"/> 
                                    <asp:BoundField HeaderText="Demostrador" DataField="id_usuario"/> 
                                    <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena"/> 
                                    <asp:BoundField HeaderText="Tienda" DataField="nombre"/> 
                                    <asp:BoundField HeaderText="Marca" DataField="nombre_marca"/> 
                                    <asp:BoundField HeaderText="Categoría" DataField="nombre_categoria"/> 
                                    <asp:BoundField HeaderText="Producto" DataField="nombre_producto"/> 
                                    <asp:BoundField HeaderText="Caducidad" DataField="caducidad" DataFormatString="{0:d}" />
                                    <asp:BoundField HeaderText="Cantidad" DataField="cantidad_caducada"/>  
                                </Columns>   
                                <FooterStyle CssClass="grid-footer" />                   
                            <EmptyDataTemplate>
                                <h1>Sin información</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmbPeriodo" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbRegion" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbPromotor" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbCadena" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbTienda" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbCaducidad" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>        
        </div>
        
        <!--CONTENT SIDE COLUMN-->
        <div id="content-side-two-column">
            <asp:LinkButton ID="lnkExportar" runat="server" Font-Bold="True">Exportar a Excel</asp:LinkButton>
        </div>
        
        <div class="clear"></div>
    </div>
</asp:Content>