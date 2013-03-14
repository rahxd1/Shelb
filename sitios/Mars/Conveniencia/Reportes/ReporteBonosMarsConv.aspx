<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Mars/Conveniencia/MarsConveniencia.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteBonosMarsConv.aspx.vb" 
    Inherits="procomlcd.ReporteBonosMarsConv"
    title="Mars Conveniencia - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoMarsConveniencia" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
                        Reporte bonos</div>

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
                            NavigateUrl="~/sitios/Mars/Conveniencia/Reportes/ReportesMarsConv.aspx">Regresar</asp:HyperLink><br />
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
                    <asp:Panel ID="PanelFS" runat="server"/>
                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" Width="100%" 
                                    ShowFooter="True" CssClass="grid-view">
                                <Columns>
                                    <asp:BoundField HeaderText="Promotor" DataField="id_usuario"/> 
                                    <asp:BoundField HeaderText="Tiendas" DataField="tiendas"/> 
                                    <asp:BoundField HeaderText="% Captura" DataField="Capturas"/> 
                                    <asp:BoundField HeaderText="Material POP" DataField="MaterialPop"/> 
                                    <asp:BoundField HeaderText="% Objetivo Material POP" DataField="ObjMaterialPop"/> 
                                    <asp:BoundField HeaderText="Exhibición Cesar" DataField="Cesar"/> 
                                    <asp:BoundField HeaderText="% Objetivo Exhibición Cesar" DataField="ObjCesar"/> 
                                    <asp:BoundField HeaderText="Material Plastival" DataField="Plastival"/> 
                                    <asp:BoundField HeaderText="% Objetivo Plastival" DataField="ObjPlastival"/> 
                                    <asp:BoundField HeaderText="Tiras" DataField="Tiras"/> 
                                    <asp:BoundField HeaderText="% Objetivo Tiras" DataField="ObjTiras"/> 
                                    <asp:BoundField HeaderText="Wet Parking" DataField="Wet"/> 
                                    <asp:BoundField HeaderText="% Objetivo Wet Parking" DataField="ObjWet"/> 
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