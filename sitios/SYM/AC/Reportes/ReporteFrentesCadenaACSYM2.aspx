<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/SYM/AC/SYMAC.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteFrentesCadenaACSYM2.aspx.vb" 
    Inherits="procomlcd.ReporteFrentesCadenaACSYM2" 
    Title="SYM Catalogación - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSYMAC" runat="Server">

    <!--titulo-pagina-->
        <div id="titulo-pagina">
                        Frentes totales por producto por cadena</div>

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
                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="True" 
                            CssClass="grid-view" ShowFooter="True" Width="100%">
                            <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>
                                    Sin información</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <asp:GridView ID="gridReporte2" runat="server" AutoGenerateColumns="True" 
                            CssClass="grid-view" ShowFooter="True" Width="100%">
                            <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>
                                    Sin información</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <asp:GridView ID="gridReporte3" runat="server" AutoGenerateColumns="True" 
                            CssClass="grid-view" ShowFooter="True" Width="100%">
                            <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>
                                    Sin información</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <asp:GridView ID="gridReporte4" runat="server" AutoGenerateColumns="True" 
                            CssClass="grid-view" ShowFooter="True" Width="100%">
                            <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>
                                    Sin información</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <asp:GridView ID="gridReporte5" runat="server" AutoGenerateColumns="True" 
                            CssClass="grid-view" ShowFooter="True" Width="100%">
                            <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>
                                    Sin información</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <asp:GridView ID="gridReporte6" runat="server" AutoGenerateColumns="True" 
                            CssClass="grid-view" ShowFooter="True" Width="100%">
                            <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>
                                    Sin información</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <asp:GridView ID="gridReporte7" runat="server" AutoGenerateColumns="True" 
                            CssClass="grid-view" ShowFooter="True" Width="100%">
                            <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>
                                    Sin información</h1>
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