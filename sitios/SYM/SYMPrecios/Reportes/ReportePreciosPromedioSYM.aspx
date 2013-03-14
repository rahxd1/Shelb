<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/SYM/SYMPrecios/SYMPrecios.Master"
    AutoEventWireup="false" 
    CodeBehind="ReportePreciosPromedioSYM.aspx.vb" 
    Inherits="procomlcd.ReportePreciosPromedioSYM"
    Title="SYM Precios - Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1SYMPrecios" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
            Reporte precios promedio por cadena</div>

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
                            NavigateUrl="~/sitios/SYM/SYMPrecios/Reportes/ReportesSYMPrecios.aspx">Regresar</asp:HyperLink><br />
                    </div>
                    
                    <asp:Label ID="lblPeriodo" runat="server" Text="Periodo" CssClass="lbl" />
                    <asp:DropDownList ID="cmbPeriodo" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />    
                        
                    <asp:Label ID="lblRegion" runat="server" Text="División" CssClass="lbl" />
                    <asp:DropDownList ID="cmbRegion" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblCiudad" runat="server" Text="Ciudad" CssClass="lbl" />
                    <asp:DropDownList ID="cmbCiudad" runat="server" CssClass="cmb" 
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
                    <asp:Panel ID="PnlGrid" runat="server" ScrollBars="Both" CssClass="panel-grid">
                            <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="True" 
                                Width="723px" ShowFooter="True" Caption="JABON DE TOCADOR">
                                    <FooterStyle CssClass="grid-footer" />
                                <EmptyDataTemplate>
                                    <h1>Sin información</h1>
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#154B68" ForeColor="White" />
                            </asp:GridView>
                            <asp:GridView ID="gridReporte2" runat="server" AutoGenerateColumns="true" 
                                Width="723px" ShowFooter="True" Caption="JABON DE LAVANDERIA" >
                                    <FooterStyle CssClass="grid-footer" />
                                <EmptyDataTemplate>
                                    <h1>Sin información</h1>
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#154B68" ForeColor="White" />
                            </asp:GridView>
                            <asp:GridView ID="gridReporte3" runat="server" AutoGenerateColumns="true" 
                                Width="723px" ShowFooter="True" Caption="DETERGENTE CONCENTRADO">
                                    <FooterStyle CssClass="grid-footer" />
                                <EmptyDataTemplate>
                                    <h1>Sin información</h1>
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#154B68" ForeColor="White" />
                            </asp:GridView>
                            <asp:GridView ID="gridReporte4" runat="server" AutoGenerateColumns="true" 
                                Width="723px" ShowFooter="True" Caption="DETERGENTE REGULAR">
                                    <FooterStyle CssClass="grid-footer" />
                                <EmptyDataTemplate>
                                    <h1>Sin información</h1>
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#154B68" ForeColor="White" />
                            </asp:GridView>
                            <asp:GridView ID="gridReporte5" runat="server" AutoGenerateColumns="true" 
                                Width="723px" ShowFooter="True" Caption="DETERGENTE MULTIUSOS">
                                    <FooterStyle CssClass="grid-footer" />
                                <EmptyDataTemplate>
                                    <h1>Sin información</h1>
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#154B68" ForeColor="White" />
                            </asp:GridView>
                            <asp:GridView ID="gridReporte6" runat="server" AutoGenerateColumns="true" 
                                Width="723px" ShowFooter="True" Caption="LAVATRASTES">
                                    <FooterStyle CssClass="grid-footer" />
                                <EmptyDataTemplate>
                                    <h1>Sin información</h1>
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#154B68" ForeColor="White" />
                            </asp:GridView>
                              <asp:GridView ID="gridReporte7" runat="server" AutoGenerateColumns="true" 
                                Width="723px" ShowFooter="True" Caption="JABON LIQUIDO CORPORAL">
                                    <FooterStyle CssClass="grid-footer" />
                                <EmptyDataTemplate>
                                    <h1>Sin información</h1>
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#154B68" ForeColor="White" />
                            </asp:GridView>
                            <asp:GridView ID="gridReporte8" runat="server" AutoGenerateColumns="true" 
                                Width="723px" ShowFooter="True" Caption="JABON LIQUIDO MANOS">
                                    <FooterStyle CssClass="grid-footer" />
                                <EmptyDataTemplate>
                                    <h1>Sin información</h1>
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#154B68" ForeColor="White" />
                            </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmbPeriodo" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbRegion" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbCiudad" EventName="SelectedIndexChanged" />
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