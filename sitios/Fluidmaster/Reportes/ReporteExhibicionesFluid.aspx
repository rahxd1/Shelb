<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Fluidmaster/Fluidmaster.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteExhibicionesFluid.aspx.vb" 
    Inherits="procomlcd.ReporteExhibicionesFluid"
    title="Fluidmaster - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoFluid" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
                        Reporte exhibiciones</div>

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
                        <asp:GridView ID="gridResumen" Caption="Exhibiciones por región" runat="server" AutoGenerateColumns="False" Width="450px" 
                                    ShowFooter="True" CssClass="grid-view">
                                <Columns>  
                                    <asp:BoundField HeaderText="Región" DataField="nombre_region"/> 
                                    
                                    <asp:BoundField HeaderText="Fluidmaster" DataField="fluidmaster" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="%" DataField="por_fluidmaster" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="Competencia" DataField="competencia" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="%" DataField="por_competencia" ItemStyle-Width="90px"/>     
                                </Columns>
                            <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>Sin información</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <br />
                        <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" Width="645px" 
                                    ShowFooter="True" CssClass="grid-view">
                                <Columns>  
                                    <asp:BoundField HeaderText="Región" DataField="nombre_region"/> 
                                    <asp:BoundField HeaderText="Ciudad" DataField="ciudad"/> 
                                    <asp:BoundField HeaderText="Promotor" DataField="id_usuario"/> 
                                    <asp:BoundField HeaderText="Distribuidor" DataField="nombre_cadena"/>  
                                    <asp:BoundField HeaderText="Tienda" DataField="nombre"/> 
                                    <asp:BoundField HeaderText="Tipo tienda" DataField="tipo_tienda"/> 
                                                                        
                                    <asp:BoundField HeaderText="FM01 - FERRETERO" DataField="1" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="FM02 - MOSTRADOR" DataField="2" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="FM03 - GONDOLA" DataField="3" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="FM04 - DE PISO" DataField="4" ItemStyle-Width="90px"/>             
                                    <asp:BoundField HeaderText="FM05 - EXHIBICION DE PARED" DataField="5" ItemStyle-Width="90px"/>      
                                    <asp:BoundField HeaderText="OTRO - EXHIBICION ESPECIAL " DataField="6" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="Fluidmaster" DataField="fluidmaster" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="% participación fluidmaster" DataField="por_fluidmaster" ItemStyle-Width="90px"/>      
                                    
                                    <asp:BoundField HeaderText="COFLEX" DataField="C_2" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="ELPRO" DataField="C_3" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="FILPRO" DataField="C_4" ItemStyle-Width="90px"/>             
                                    <asp:BoundField HeaderText="HALCON" DataField="C_5" ItemStyle-Width="90px"/>      
                                    <asp:BoundField HeaderText="PLOMER" DataField="C_6" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="URREA" DataField="C_7" ItemStyle-Width="90px"/>      
                                    <asp:BoundField HeaderText="OTROS" DataField="C_8" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="Competencia" DataField="competencia" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="% participación competencia" DataField="por_competencia" ItemStyle-Width="90px"/>   
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