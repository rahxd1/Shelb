<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/SYM/AC/SYMAC.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteCatalogacionHistoricoACSYM.aspx.vb" 
    Inherits="procomlcd.ReporteCatalogacionHistoricoACSYM" 
    Title="SYM Catalogaci�n - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSYMAC" runat="Server">

    <!--titulo-pagina-->
        <div id="titulo-pagina">
                        Historico catalogaci�n</div>

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
                            NavigateUrl="~/sitios/SYM/AC/Reportes/ReportesSYMAC.aspx">Regresar</asp:HyperLink><br />
                    </div>
                    
                    <asp:Label ID="lblPeriodo1" runat="server" Text="Periodo" CssClass="lbl" />
                    <asp:DropDownList ID="cmbPeriodo1" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblPeriodo2" runat="server" Text="Periodo" CssClass="lbl" />
                    <asp:DropDownList ID="cmbPeriodo2" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />        
                        
                    <asp:Label ID="lblRegion" runat="server" Text="Divisi�n" CssClass="lbl" />
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
                                Cargando informaci�n.
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    
                    <asp:Panel ID="PanelFS" runat="server" />
                    <br />
                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridReporte" runat="server" 
                            Width="100%" ShowFooter="True" CssClass="grid-view" 
                            AutoGenerateColumns="true">
                                <FooterStyle CssClass="grid-footer" />                   
                            <EmptyDataTemplate>
                                <h1>Sin informaci�n</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmbPeriodo1" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbPeriodo2" EventName="SelectedIndexChanged" />
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
        
        <div class="clear">
        </div>
    </div>
</asp:Content>