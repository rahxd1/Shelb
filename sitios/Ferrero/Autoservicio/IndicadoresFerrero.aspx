<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Ferrero/Autoservicio/FerreroAS.Master"
    AutoEventWireup="false" 
    CodeBehind="IndicadoresFerrero.aspx.vb" 
    Inherits="procomlcd.IndicadoresFerrero"
    TITLE="Ferrero - Indicadores" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoFerreroAS" runat="Server">
  
     
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
                            <td>
                                Periodo</td>
                            <td>
                                <asp:DropDownList ID="cmbPeriodo" runat="server" AutoPostBack="True" 
                                    Height="22px" Width="250px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pFrentes0" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                    <table align="center" style="width: 100%">
                                        <tr>
                                            <td style="height: 31px" colspan="2">
                                                <h3>
                                                    Precios</h3>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td rowspan="2">
                                                <asp:Panel ID="pnlPrecioPromedio" runat="server">
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlPrecioWalmart" runat="server">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlPrecioSoriana" runat="server">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pFrentes" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                    <table align="center" style="width: 100%">
                                        <tr>
                                            <td colspan="2">
                                                <h3>
                                                    Frentes</h3>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td rowspan="4">
                                                <asp:Panel ID="pnlFrentesPromedio" runat="server">
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlFrentes1" runat="server">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlFrentes2" runat="server">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlFrentes3" runat="server">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlFrentes4" runat="server">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pFaltantes" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <h3 style="top: auto">
                                                    Faltantes</h3>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlFaltantes" runat="server" style="text-align: center">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
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