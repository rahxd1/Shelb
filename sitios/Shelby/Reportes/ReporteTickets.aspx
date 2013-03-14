<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Shelby/Shelby.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteTickets.aspx.vb" 
    Inherits="procomlcd.ReporteTickets"
    title="Shelby - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoShelby" runat="Server">

<!--titulo-pagina-->
    <div id="titulo-pagina">Reporte tickets sitio web</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width: 100%">
                        <tr><td style="width: 96px">&nbsp;</td>
                            <td style="width: 452px">&nbsp;</td>
                            <td style="text-align: right">
                                <img alt="" src="../Img/arrow.gif" /><asp:HyperLink ID="lnkRegresar" runat="server" 
                                        NavigateUrl="~/sitios/Shelby/Reportes/ReportesShelby.aspx">Regresar</asp:HyperLink></td>
                        </tr>
                        <tr><td style="width: 96px">Mes</td>
                            <td colspan="2">
                                    <asp:DropDownList ID="cmbMes" runat="server" AutoPostBack="True" 
                                        CssClass="ddl" />
                            </td>
                        </tr>
                        <tr><td colspan="3" style="text-align: center;">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                    <ProgressTemplate>
                                        <table id="pnlSave" >
                                            <tr><td>
                                                <br /><p><img alt="Cargando Reporte" src="../../Imagenes/loading.gif" /> El Reporte se 
                                                esta generando.</p><br /></td></tr>
                                        </table>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridTotal" runat="server" AutoGenerateColumns="True" 
                            CssClass="grid-view" ShowFooter="True" Width="50%">
                            <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>
                                    Sin información</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <br />
                        <asp:GridView ID="gridReporte" runat="server" 
                            CssClass="grid-view" ShowFooter="True" Width="100%">
                            <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>
                                    Sin información</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                    <br />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmbPeriodo" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbQuincena" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbRegion" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbSupervisor" EventName="SelectedIndexChanged" />
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