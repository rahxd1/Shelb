<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Jovy/Jovy.Master"
    AutoEventWireup="false" 
    CodeBehind="IndicadoresJovy.aspx.vb" 
    Inherits="procomlcd.IndicadoresJovy"
    title="Jovy - Indicadores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderJovy" runat="Server">
  
     
    <!--diana flores -----55-48-99-54-57 cel

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
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pFrentes1" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                    <table align="center" style="width: 100%">
                                        <tr>
                                            <td style="height: 31px">
                                                <h3>
                                                    Exhibidores</h3>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlExhibidores" runat="server" style="text-align: center">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td colspan="2">
                                <asp:Panel ID="pCaducidad" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                    <table align="center" style="width: 100%">
                                        <tr>
                                            <td style="height: 31px">
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
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Panel ID="pFrentes0" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                    <table align="center" style="width: 100%">
                                        <tr>
                                            <td style="height: 31px">
                                                <h3>
                                                    Precios</h3>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlPrecioPromedio" runat="server" style="text-align: center">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Panel ID="pFrentes" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                    <table align="center" style="width: 100%">
                                        <tr>
                                            <td>
                                                <h3>
                                                    Inventarios</h3>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlInventarios" runat="server" style="text-align: center">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlInventariosTienda" runat="server" style="text-align: center">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
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