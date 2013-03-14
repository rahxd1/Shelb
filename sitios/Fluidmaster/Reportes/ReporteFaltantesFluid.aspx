<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Fluidmaster/Fluidmaster.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteFaltantesFluid.aspx.vb" 
    Inherits="procomlcd.ReporteFaltantesFluid"
    title="Fluidmaster - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoFluid" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
                        Reporte faltantes</div>

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
                            NavigateUrl="~/sitios/Fluidmaster/Reportes/ReportesFluid.aspx">Regresar</asp:HyperLink><br />
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
                        
                    <asp:Label ID="lblPromotor" runat="server" Text="Promotor" CssClass="lbl" />
                    <asp:DropDownList ID="cmbPromotor" runat="server" CssClass="cmb"
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblDistribuidor" runat="server" Text="Distribuidor" CssClass="lbl" />
                    <asp:DropDownList ID="cmbDistribuidor" runat="server" CssClass="cmb"
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblTienda" runat="server" Text="Tienda" CssClass="lbl" />
                    <asp:DropDownList ID="cmbTienda" runat="server" CssClass="cmb"
                        AutoPostBack="True" /><br />

                    <asp:UpdateProgress ID="UpdateProgress" runat="server" 
                        AssociatedUpdatePanelID="updPnl">
                        <ProgressTemplate>
                            <div id="pnlSave" >
                                <img alt="Cargando..." src="../../../Img/loading.gif" /> 
                                Cargando información.
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    
                    <asp:Panel ID="PanelFS" runat="server" />
                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" Width="645px" 
                                    ShowFooter="True" CssClass="grid-view">
                                <Columns>  
                                    <asp:BoundField HeaderText="Región" DataField="nombre_region"/> 
                                    <asp:BoundField HeaderText="Ciudad" DataField="ciudad"/> 
                                    <asp:BoundField HeaderText="Promotor" DataField="id_usuario"/> 
                                    <asp:BoundField HeaderText="Distribuidor" DataField="nombre_cadena"/>  
                                    <asp:BoundField HeaderText="Tienda" DataField="nombre"/> 
                                    <asp:BoundField HeaderText="Tipo tienda" DataField="tipo_tienda"/> 
                                    
                                    <asp:BoundField HeaderText="200AK133 Combo" DataField="3" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="200CM135 Combo" DataField="2" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="555C135 Combo" DataField="9" ItemStyle-Width="90px"/> 
                                                                        
                                    <asp:BoundField HeaderText="400LSR133 linea 400LS BLISTER 1" DataField="13" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="400LSCR133 linea 400LS BLISTER 2" DataField="14" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="400LSCLR133 linea 400LS BLISTER 3" DataField="15" ItemStyle-Width="90px"/>  
                                    
                                    <asp:BoundField HeaderText="242135 Empaque" DataField="12" ItemStyle-Width="90px"/>    
                                                                        
                                    <asp:BoundField HeaderText="500135 Flapper" DataField="6" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="503 Flapper" DataField="16" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="5403 Flapper (3 pulgadas)" DataField="19" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="501135 Flapper" DataField="7" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="502135 Flapper" DataField="8" ItemStyle-Width="90px"/> 
                                    
                                    <asp:BoundField HeaderText="681135 Palanca" DataField="11" ItemStyle-Width="90px"/>  
                                    <asp:BoundField HeaderText="Boton cromado 691" DataField="18" ItemStyle-Width="90px"/>  
                                    
                                    <asp:BoundField HeaderText="507A133 Válvula de descarga" DataField="10" ItemStyle-Width="90px"/>  
                                    
                                    <asp:BoundField HeaderText="200AM133 Válvula de llenado" DataField="1" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="400A133 Válvula de llenado" DataField="4" ItemStyle-Width="90px"/>
                                    <asp:BoundField HeaderText="747UK Válvula de llenado" DataField="17" ItemStyle-Width="90px"/>  
                                    <asp:BoundField HeaderText="400LS133 Válvula de llenado" DataField="5" ItemStyle-Width="90px"/>
                                                             
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
                    <asp:AsyncPostBackTrigger ControlID="cmbEstado" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbPromotor" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbDistribuidor" EventName="SelectedIndexChanged" />                    
                    <asp:AsyncPostBackTrigger ControlID="cmbTienda" EventName="SelectedIndexChanged" />                    
                </Triggers>
            </asp:UpdatePanel>
        </div>
        
        <!--CONTENT SIDE COLUMN-->
        <div id="content-side-two-column">
            <asp:LinkButton ID="lnkExportar" runat="server" Font-Bold="True">Exportar a Excel</asp:LinkButton>
        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>