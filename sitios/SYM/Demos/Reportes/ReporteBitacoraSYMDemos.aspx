<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/SYM/Demos/SYMDemos.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteBitacoraSYMDemos.aspx.vb" 
    Inherits="procomlcd.ReporteBitacoraSYMDemos" 
    Title="SYM Demos - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Contenido_SYMDemos" runat="Server">

    <!--titulo-pagina-->
        <div id="titulo-pagina">
                        Reporte bit�cora captura</div>

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
                        
                    <asp:Label ID="lblRegion" runat="server" Text="Divisi�n" CssClass="lbl" />
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
                                Cargando informaci�n.
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    
                    <br /> 
                    <asp:Panel ID="PnlGrid" runat="server" ScrollBars="Both" CssClass="panel-grid" Height="500">
                        <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" 
                            Width="100%" ShowFooter="True" CssClass="grid-view">
                                <Columns>
                                    <asp:BoundField HeaderText="Regi�n" DataField="nombre_region"/> 
                                    <asp:BoundField HeaderText="Promotor" DataField="id_usuario"/> 
                                    <asp:BoundField HeaderText="Demos" DataField="demos"/> 
                                    <asp:BoundField HeaderText="Capturas" DataField="Tiendas"/>
                                    <asp:BoundField HeaderText="% Captura" DataField="Porcentaje"/>
                                </Columns>   
                                <FooterStyle CssClass="grid-footer" />                   
                            <EmptyDataTemplate>
                                <h1>Sin informaci�n</h1>
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