<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/SYM/Demos/SYMDemos.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteCanjesSYMDemos.aspx.vb" 
    Inherits="procomlcd.ReporteCanjesSYMDemos" 
    Title="SYM Demos - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Contenido_SYMDemos" runat="Server">

    <!--titulo-pagina-->
        <div id="titulo-pagina">
                        Reporte canjes</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            
            <asp:UpdatePanel ID="updPnl" runat="server">
                <ContentTemplate>
                    <div id="menu-regresar">
                        <img alt="" src="../../../../../Img/arrow.gif" />
                        <asp:HyperLink ID="lnkRegresar" runat="server" 
                            NavigateUrl="~/sitios/SYM/Demos/Reportes/ReportesSYMDemos.aspx">Regresar</asp:HyperLink><br />
                    </div>
                    
                    <asp:Label ID="lblPeriodo" runat="server" Text="Periodo" CssClass="lbl" />
                    <asp:DropDownList ID="cmbPeriodo" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />    
                        
                    <asp:Label ID="lblRegion" runat="server" Text="División" CssClass="lbl" />
                    <asp:DropDownList ID="cmbRegion" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblEstado" runat="server" Text="Estado" CssClass="lbl" />
                    <asp:DropDownList ID="cmbEstado" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />    
                        
                    <asp:Label ID="lblSupervisor" runat="server" Text="Supervisor" CssClass="lbl" />
                    <asp:DropDownList ID="cmbSupervisor" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />   
                        
                    <asp:Label ID="lblCiudad" runat="server" Text="Ciudad" CssClass="lbl" />
                    <asp:DropDownList ID="cmbCiudad" runat="server" CssClass="cmb" 
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
                        
                    <asp:UpdateProgress ID="UpdateProgress" runat="server" 
                        AssociatedUpdatePanelID="updPnl">
                        <ProgressTemplate>
                            <div id="pnlSave" >
                                <img alt="Cargando..." src="../../../../Img/loading.gif" /> 
                                Cargando información.
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    
                    <br /> 
                    <asp:Panel ID="PnlGrid" runat="server" ScrollBars="Both" CssClass="panel-grid" Height="500">
                        <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" 
                            Width="100%" ShowFooter="True" CssClass="grid-view">
                                <Columns>
                                    <asp:BoundField HeaderText="Región" DataField="nombre_region"/> 
                                    <asp:BoundField HeaderText="Plaza" DataField="plaza"/> 
                                    <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena"/>
                                    <asp:BoundField HeaderText="Tienda" DataField="nombre_tienda"/>
                                    <asp:BoundField HeaderText="No. de ticket" DataField="no_ticket"/>
                                    <asp:BoundField HeaderText="cliente" DataField="nombre_cliente"/>
                                    <asp:BoundField HeaderText="telefono" DataField="telefono"/>
                                    <asp:BoundField HeaderText="toallas" DataField="toallas"/>
                                    <asp:BoundField HeaderText="Jabón Liquido Manos Dermatológico 221ml" DataField="1"/>
                                    <asp:BoundField HeaderText="Jabón Liquido Manos Neutro 221ml" DataField="2"/>
                                    <asp:BoundField HeaderText="Jabón Liquido Corporal Dermatológico 400ml" DataField="3"/>
                                    <asp:BoundField HeaderText="Jabón Liquido Corporal Neutro 400ml" DataField="4"/>
                                </Columns>   
                                <FooterStyle CssClass="grid-footer" />                   
                            <EmptyDataTemplate>
                                <h1>Sin comentarios</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmbPeriodo" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbRegion" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbEstado" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbSupervisor" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbCiudad" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbPromotor" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbCadena" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbTienda" EventName="SelectedIndexChanged" />
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