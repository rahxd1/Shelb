<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Berol/NR/Rubbermaid.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteCatalogacionCategoriaNR.aspx.vb" 
    Inherits="procomlcd.ReporteCatalogacionCategoriaNR"
    Title="Newell Rubbermaid - Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoNR" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
            Reporte catalogación por tienda</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            
            <asp:UpdatePanel ID="updPnl" runat="server">
                <ContentTemplate>
                
                    <div id="menu-regresar">
                        <img alt="" src="../../../../Img/arrow.gif" />
                        <asp:HyperLink ID="lnkRegresar" runat="server"
                            NavigateUrl="~/sitios/Berol/NR/Reportes/ReportesNR.aspx">Regresar</asp:HyperLink><br />
                    </div>
                        
                    <asp:Label ID="lblPeriodo" runat="server" Text="Periodo" CssClass="lbl" />
                    <asp:DropDownList ID="cmbPeriodo" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                            
                    <asp:Label ID="lblRegion" runat="server" Text="Región" CssClass="lbl" />
                    <asp:DropDownList ID="cmbRegion" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                
                    <asp:Label ID="lblEstado" runat="server" Text="Estado" CssClass="lbl" />
                    <asp:DropDownList ID="cmbEstado" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                            
                    <asp:Label ID="lblSupervisor" runat="server" Text="Supervisor" CssClass="lbl" />
                    <asp:DropDownList ID="cmbSupervisor" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblPromotor" runat="server" Text="Promotor" CssClass="lbl" />
                    <asp:DropDownList ID="cmbPromotor" runat="server" CssClass="cmb"
                        AutoPostBack="True" /><br />
                
                    <asp:Label ID="lblCadena" runat="server" Text="Cadena" CssClass="lbl" />
                    <asp:DropDownList ID="cmbCadena" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                    
                    <asp:Label ID="lblFormato" runat="server" Text="Formato" CssClass="lbl" />
                    <asp:DropDownList ID="cmbFormato" runat="server" CssClass="cmb"  
                        AutoPostBack="True" /><br />
                    
                    <asp:Label ID="lblTienda" runat="server" Text="Tienda" CssClass="lbl" />
                    <asp:DropDownList ID="cmbTienda" runat="server" CssClass="cmb"
                        AutoPostBack="True" /><br />
                        
                    <asp:UpdateProgress ID="UpdateProgress" runat="server" 
                        AssociatedUpdatePanelID="updPnl">
                        <ProgressTemplate>
                            <div id="pnlSave" >
                                <img alt="Cargando..." src="../../../../Img/loading.gif" /> 
                                Cargando información.
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    
                    <asp:Panel ID="PnlGrid" runat="server" ScrollBars="Both" CssClass="panel-grid">
                        <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="True" 
                            Width="723px" ShowFooter="True" Caption="LAPICES">
                                <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>Sin información</h1>
                            </EmptyDataTemplate>
                            <HeaderStyle BackColor="#154B68" ForeColor="White" />
                        </asp:GridView>
                        <asp:GridView ID="gridReporte2" runat="server" AutoGenerateColumns="true" 
                            Width="723px" ShowFooter="True" Caption="MARCADORES" >
                                <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>Sin información</h1>
                            </EmptyDataTemplate>
                            <HeaderStyle BackColor="#154B68" ForeColor="White" />
                        </asp:GridView>
                        <asp:GridView ID="gridReporte3" runat="server" AutoGenerateColumns="true" 
                            Width="723px" ShowFooter="True" Caption="CRAYONES">
                                <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>Sin información</h1>
                            </EmptyDataTemplate>
                            <HeaderStyle BackColor="#154B68" ForeColor="White" />
                        </asp:GridView>
                        <asp:GridView ID="gridReporte4" runat="server" AutoGenerateColumns="true" 
                            Width="723px" ShowFooter="True" Caption="SAMS CLUB">
                                <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>Sin información</h1>
                            </EmptyDataTemplate>
                            <HeaderStyle BackColor="#154B68" ForeColor="White" />
                        </asp:GridView>
                        <asp:GridView ID="gridReporte5" runat="server" AutoGenerateColumns="true" 
                            Width="723px" ShowFooter="True" Caption="REPUESTOS">
                                <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>Sin información</h1>
                            </EmptyDataTemplate>
                            <HeaderStyle BackColor="#154B68" ForeColor="White" />
                        </asp:GridView>
                        <asp:GridView ID="gridReporte6" runat="server" AutoGenerateColumns="true" 
                            Width="723px" ShowFooter="True" Caption="ROTULADORES">
                                <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>Sin información</h1>
                            </EmptyDataTemplate>
                            <HeaderStyle BackColor="#154B68" ForeColor="White" />
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        
        <!--CONTENT SIDE COLUMN-->
        <div id="content-side-two-column">
            <asp:LinkButton ID="lnkExportar" runat="server" Font-Bold="True">Exportar a Excel</asp:LinkButton>
        </div>
        
        <div class="clear"></div>
    </div>
</asp:Content>