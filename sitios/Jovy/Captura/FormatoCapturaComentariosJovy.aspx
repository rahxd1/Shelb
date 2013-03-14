<%@ Page Language="vb" 
    Culture="es-MX" 
    MasterPageFile="~/sitios/Jovy/Jovy.Master" 
    AutoEventWireup="false" 
    CodeBehind="FormatoCapturaComentariosJovy.aspx.vb" 
    Inherits="procomlcd.FormatoCapturaComentariosJovy"
    title="Jovy - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderJovy" runat="Server">
    <!--PAGE TITLE-->
<div id="titulo-pagina">Formato captura comentarios</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">

<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"/>

                   
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                        
                        <table style="width: 100%; border-collapse: 0; border-spacing: 0px; empty-cells: 0;" 
                            cellpadding="0" cellspacing="0">
                            <tr><td style="text-align: right;" colspan="2">
                                <img alt="" src="../../../Img/arrow.gif" />
                                <asp:HyperLink ID="lnkRegresar" runat="server" 
                                    NavigateUrl="~/sitios/Jovy/Captura/RutasJovy.aspx">Regresar</asp:HyperLink></td>
                            </tr>
                            <tr><td style="text-align: left;">
                                Tienda</td>
                            <td style="text-align: left; width: 596px;">
                                <asp:Label ID="lblTienda" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left;">
                                    Cadena</td>
                                <td style="text-align: left; width: 596px;">
                                    <asp:Label ID="lblCadena" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        
                            <br />
                    <table style="border-style: groove; width: 100%; height: 99px; margin-right: 0px" >
                        <tr style="color: #FFFFFF">
                           <td style="text-align: center; background-color: #000000; height: 29px; width: 616px;" >
                            
                               COMENTARIOS</td>
                        </tr>
                        <tr style="font-size: small">
                           <td style="font-size: x-small; height: 75px; width: 616px;" >
                             
                               <asp:TextBox ID="txtComentarios" runat="server" Width="646px" Height="60px" 
                                   TextMode="MultiLine" MaxLength="300"></asp:TextBox>
                           </td>
                        </tr>
                    </table>
                                                    
                    <br />
            
                    <table style="border-style: groove; width: 100%; height: 99px; margin-right: 0px" >
                        <tr style="color: #FFFFFF">
                           <td style="text-align: center; background-color: #000000; height: 29px; width: 616px;" >
                            
                               COMENTARIOS COMPETENCIA</td>
                        </tr>
                        <tr style="font-size: small">
                           <td style="font-size: x-small; height: 75px; width: 616px;" >
                             
                               <asp:TextBox ID="txtComentariosCompetencia" runat="server" Width="646px" Height="60px" 
                                   TextMode="MultiLine" MaxLength="300"></asp:TextBox>
                           </td>
                        </tr>
                    </table>
                 </ContentTemplate>
               </asp:UpdatePanel>
                        
                    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table style="width: 100%">
                               <tr>
                                   <td style="text-align: center; height: 31px; " colspan="2">
                                   
                                       <asp:ValidationSummary ID="vsJovy" runat="server" 
                                           ValidationGroup="Jovy" />
                                   <asp:UpdateProgress ID="UpdateProgress" runat="server" 
                                           AssociatedUpdatePanelID="UpdatePanel" DisplayAfter="0">
                                           <ProgressTemplate>
                                               <div id="pnlSave">
                                                   <img alt="" src="../../../Img/loading.gif" />La información se esta guardando.<br />
                                                        <br />
                                                        Por favor espera.<br /><br />  
                                                </div>
                                           </ProgressTemplate>
                                       </asp:UpdateProgress>
                                   </td>
                               </tr>
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" 
                                            ValidationGroup="Jovy" CssClass="button" />
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" 
                                            Text="Cancelar" CssClass="button" />
                                    </td>
                                </tr>
                            </table>
                            </ContentTemplate>  
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnGuardar" />
                        </Triggers>
                    </asp:UpdatePanel> 
                    
               
        </div>

 <!--CONTENT SIDE COLUMN AVISOS -->
        <div class="clear"></div>
    
    </div>
</asp:Content>
