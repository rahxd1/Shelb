<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Ferrero/Ferrero-Danone/FerreroDanone.Master"
    AutoEventWireup="false" 
    CodeBehind="IndicadoresFerreroDanone.aspx.vb" 
    Inherits="procomlcd.IndicadoresFerreroDanone"
    TITLE="Ferrero - Indicadores" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentFerreroDanone" runat="Server">
  
     
    <!--

POSTER PHOTO

-->
    <div id="poster-photo-container">
    
    <div id="poster-photo-image"></div>
         
          <div id="feature-area-home">Indicadores</div>
          </div>
    <!--

CONTENT CONTAINER

-->
    <div id="contenido" style="background-color: #FFFFFF">
        <!--

CONTENT MAIN COLUMN

-->

        <div id="content-main">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width: 100%">
                        <tr>
                            <td style="height: 22px; text-align: right;">
                                Periodo</td>
                            <td style="height: 22px; ">
                                <asp:DropDownList ID="cmbPeriodo" runat="server" AutoPostBack="True" 
                                    Height="22px" Width="250px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%">
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pFrentes0" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                    <table align="center" style="width: 100%">
                                        <tr>
                                            <td style="color: #FFFFFF; font-weight: 700; text-align: center; background-color: #000000">
                                                CANAL DETALLE</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlFaltantesCatalogados" runat="server" style="text-align: center">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:HyperLink ID="HyperLink11" runat="server" Font-Bold="True" 
                                                    NavigateUrl="~/sitios/Ferrero/Ferrero-Danone/Reportes/ReporteFaltantesFD.aspx">Ver Detalles</asp:HyperLink>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Panel ID="Panel1" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <h3>
                                                Caducidad</h3>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlCaducidad" runat="server" style="text-align: center">
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:HyperLink ID="HyperLink13" runat="server" Font-Bold="True" 
                                                NavigateUrl="~/sitios/Ferrero/Ferrero-Danone/Reportes/ReporteCaducidadFD.aspx">Ver Detalles</asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                                </asp:Panel>
                            </td>
                            <td>
                                <asp:Panel ID="Panel2" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <h3>
                                                    Encuesta</h3>
                                            </td>
                                            <td style="text-align: right">
                                                (<asp:Label ID="lblTiendas" runat="server" Font-Bold="True"></asp:Label>
                                                &nbsp;tiendas levantadas)</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="pnl1" runat="server" style="text-align: center">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center" colspan="2">
                                                <asp:HyperLink ID="HyperLink6" runat="server" Font-Bold="True" 
                                                    NavigateUrl="~/sitios/Ferrero/Ferrero-Danone/Reportes/ReporteSurtidorFD.aspx">Ver Detalles</asp:HyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="pnl2" runat="server" style="text-align: center">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center" colspan="2">
                                                <asp:HyperLink ID="HyperLink7" runat="server" Font-Bold="True" 
                                                    NavigateUrl="~/sitios/Ferrero/Ferrero-Danone/Reportes/ReporteExhibidosFD.aspx">Ver Detalles</asp:HyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="pnl3" runat="server" style="text-align: center">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center" colspan="2">
                                                <asp:HyperLink ID="HyperLink8" runat="server" Font-Bold="True" 
                                                    NavigateUrl="~/sitios/Ferrero/Ferrero-Danone/Reportes/ReporteMaterialPOPFD.aspx">Ver Detalles</asp:HyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="pnl4" runat="server" style="text-align: center">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center" colspan="2">
                                                <asp:HyperLink ID="HyperLink9" runat="server" Font-Bold="True" 
                                                    NavigateUrl="~/sitios/Ferrero/Ferrero-Danone/Reportes/ReporteVisitaFD.aspx">Ver Detalles</asp:HyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="pnl5" runat="server" style="text-align: center">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center" colspan="2">
                                                <asp:HyperLink ID="HyperLink10" runat="server" Font-Bold="True" 
                                                    NavigateUrl="~/sitios/Ferrero/Ferrero-Danone/Reportes/ReporteEntregaFD.aspx">Ver Detalles</asp:HyperLink>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmbPeriodo" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        
        <!--

CONTENT SIDE COLUMN AVISOS

-->
        
        <div class="clear">
        
        </div>
        
    </div>
</asp:Content>