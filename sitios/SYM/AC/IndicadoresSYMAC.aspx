<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/SYM/AC/SYMAC.Master"
    AutoEventWireup="false" 
    CodeBehind="IndicadoresSYMAC.aspx.vb" 
    Inherits="procomlcd.IndicadoresSYMAC"
    Title="SYM Anaquel y Catalogación - Indicadores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSYMAC" runat="Server">
  
     
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
                                <asp:Panel ID="pFrentes1" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                    <table align="center" style="width: 100%">
                                        <tr>
                                            <td style="height: 31px" colspan="2">
                                                <h3>
                                                    Frentes</h3>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:Panel ID="pnlFrentes" runat="server" style="text-align: center">
                                                </asp:Panel>
                                            </td>
                                            <td valign="top">
                                                <asp:Panel ID="pnlFrentesLinea" runat="server" style="text-align: center">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="pnlCadena" runat="server" style="text-align: center">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="pnlHistorico" runat="server" style="text-align: center">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pFrentes2" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                    <table align="center" style="width: 100%">
                                        <tr>
                                            <td bgcolor="SteelBlue" valign="top" colspan="3">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td valign="top" colspan="3">
                                                <h3>
                                                    Frentes por Región</h3>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:Panel ID="pnlFrentesDivision1" runat="server" style="text-align: center">
                                                </asp:Panel>
                                            </td>
                                            <td valign="top">
                                                <asp:Panel ID="pnlFrentesDivision2" runat="server" style="text-align: center">
                                                </asp:Panel>
                                            </td>
                                            <td valign="top">
                                                <asp:Panel ID="pnlFrentesDivision3" runat="server" style="text-align: center">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" valign="top">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="pnlFrentesDivision4" runat="server" style="text-align: center">
                                                            </asp:Panel>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="pnlFrentesDivision5" runat="server" style="text-align: center">
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td bgcolor="SteelBlue" colspan="3">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pParticipacion" runat="server" BorderStyle="Solid" 
                                    BorderWidth="1px">
                                    <table align="center" style="width: 100%">
                                        <tr>
                                            <td style="height: 31px" colspan="3">
                                                <h3>
                                                    Participación</h3>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Panel ID="pnlParticipacion" runat="server" style="text-align: center">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlParticipacion1" runat="server" style="text-align: center">
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlParticipacion2" runat="server" style="text-align: center">
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlParticipacion3" runat="server" style="text-align: center">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlParticipacion4" runat="server" style="text-align: center">
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlParticipacion6" runat="server" style="text-align: center">
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlParticipacion5" runat="server" style="text-align: center">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlParticipacion7" runat="server" style="text-align: center">
                                                </asp:Panel>
                                            </td>
                                            <td colspan="2">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="center" bgcolor="SteelBlue" colspan="3">
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
                                            <td>
                                                <h3>
                                                    Historico catalogación por linea</h3>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlHistoricoCatalogacion" runat="server" 
                                                    style="text-align: center">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
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