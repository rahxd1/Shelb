<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Demo/Demo.Master"
    AutoEventWireup="false" 
    CodeBehind="IndicadoresDemo.aspx.vb" 
    Inherits="procomlcd.IndicadoresDemo"
    title="Indicadores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoDemo" runat="Server">
  
  <!--POSTER PHOTO-->
    <div id="poster-photo-container">
         
          <div id="feature-area-home">Indicadores</div>
          <asp:Image ID="Image1" runat="server" 
              ImageUrl="~/sitios/Demo/Imagenes/posterfinal.jpg" />
          
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
        
            <br />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    
                    <asp:Panel ID="pnlCapturas" runat="server" BorderWidth="1px">
                        <table width="100%">
                        <tr>
                            <td style="height: 23px; text-align: right;">
                                &nbsp;</td>
                            <td style="height: 23px; text-align: left;">
                                <asp:DropDownList ID="cmbPeriodo" runat="server" AutoPostBack="True" 
                                    Height="22px" Width="250px" Visible="False">
                                </asp:DropDownList>
                            </td>
                            <td style="height: 23px; text-align: right;">
                                &nbsp;</td>
                            <td style="text-align: left;">
                                <asp:DropDownList ID="cmbRegion" runat="server" AutoPostBack="True" 
                                    Height="22px" Width="250px" Visible="False">
                                </asp:DropDownList>
                            </td>
                        </tr>
                            <tr>
                                <td colspan="4" style="height: 23px; text-align: left;">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <table bgcolor="White" style="width: 100%">
                                            <tr>
                                                <td bgcolor="Black">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <h3 style="top: auto">
                                                        Avance Objetivo Ventas</h3>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Panel ID="pnlObjetivo" runat="server">
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="height: 19px; text-align: left">
                                                    <asp:LinkButton ID="lnk01" runat="server" Font-Bold="True">Ver Detalles</asp:LinkButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="Black">
                                                    &nbsp;</td>
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
                                                <asp:Panel ID="pnlFaltantes" runat="server" style="text-align: center">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <h3 style="top: auto">
                                                    Faltantes</h3>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4" valign="top" style="font-weight: 700">
                                <asp:Panel ID="pFrentes2" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                    <table align="center" style="width: 100%">
                                        <tr>
                                            <td bgcolor="SteelBlue" colspan="3" valign="top">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" valign="top">
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
                                <td align="center" colspan="4" style="font-weight: 700" valign="top">
                                    <asp:Panel ID="pParticipacion" runat="server" BorderStyle="Solid" 
                                        BorderWidth="1px">
                                        <table align="center" style="width: 100%">
                                            <tr>
                                                <td colspan="3" style="height: 31px">
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
                    </table>
                    </asp:Panel>
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