<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Ferrero/Autoservicio/FerreroAS.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteFrentesCatFerrero.aspx.vb" 
    Inherits="procomlcd.ReporteFrentesCatFerrero" 
    Title="Ferrero- Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoFerreroAS" runat="Server">

    <!--titulo-pagina-->
        <div id="titulo-pagina">
                        Reporte frentes por categor�a</div>

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
                            NavigateUrl="~/sitios/Ferrero/Autoservicio/Reportes/ReportesFerrero.aspx">Regresar</asp:HyperLink><br />
                    </div>
                    
                    <asp:Label ID="lblPeriodo" runat="server" Text="Periodo" CssClass="lbl" />
                    <asp:DropDownList ID="cmbPeriodo" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                            
                    <asp:Label ID="lblRegion" runat="server" Text="Regi�n" CssClass="lbl" />
                    <asp:DropDownList ID="cmbRegion" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblPromotor" runat="server" Text="Promotor" CssClass="lbl" />
                    <asp:DropDownList ID="cmbPromotor" runat="server" CssClass="cmb"
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblCadena" runat="server" Text="Cadena" CssClass="lbl" />
                    <asp:DropDownList ID="cmbCadena" runat="server" CssClass="cmb"
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
                    
                    <table style="width: 100%">
                        <tr><td><asp:Panel ID="Panel1" runat="server" /></td>
                            <td><asp:Panel ID="Panel2" runat="server" /></td>
                        </tr>
                        <tr><td><asp:Panel ID="Panel3" runat="server" /></td>
                            <td><asp:Panel ID="Panel4" runat="server" /></td>
                        </tr>
                    </table>
                    <br />
                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both" Height="350px">   
                        <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="True" 
                            Width="100%" ShowFooter="True" CssClass="grid-view">  
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
                    <asp:AsyncPostBackTrigger ControlID="cmbCadena" EventName="SelectedIndexChanged" />
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