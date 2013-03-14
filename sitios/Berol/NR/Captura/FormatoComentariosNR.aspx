<%@ Page Language="vb" 
    Culture="es-MX" 
    MasterPageFile="~/sitios/Berol/NR/Rubbermaid.Master" 
    AutoEventWireup="false" 
    CodeBehind="FormatoComentariosNR.aspx.vb" 
    Inherits="procomlcd.FormatoComentariosNR"
    title="Newell Rubbermaid - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoNR" runat="Server">
    <!--PAGE TITLE-->
<div id="titulo-pagina">Formato comentarios</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">

<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"/>

                    <table style="width: 100%">
                        <tr><td style="text-align: right;">
                                <img alt="" src="../../../../Img/arrow.gif" />
                                <asp:HyperLink ID="lnkRegresar" runat="server" 
                                    NavigateUrl="~/sitios/Berol/NR/Captura/RutasPromotorNR.aspx">Regresar</asp:HyperLink></td>
                        </tr>
                        </table>
                    
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        Tienda</td>
                                    <td>
                                        <asp:Label ID="lblTienda" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Cadena</td>
                                    <td>
                                        <asp:Label ID="lblCadena" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Tipo comentarios</td>
                                    <td>
                                        <asp:DropDownList ID="cmbTipoComentario" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Comentarios</td>
                                    <td>
                                        <asp:TextBox ID="txtComentarios" runat="server" Height="113px" Width="523px" 
                                            TextMode="MultiLine"></asp:TextBox>
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
                                   
                                       <asp:ValidationSummary ID="vsRubbermaid" runat="server" 
                                           ValidationGroup="Rubbermaid" />
                                   <asp:UpdateProgress ID="UpdateProgress" runat="server" 
                                           AssociatedUpdatePanelID="UpdatePanel" DisplayAfter="0">
                                           <ProgressTemplate>
                                               <div id="pnlSave" >
                                                   <img alt="" src="../../../../Img/loading.gif" />La información se esta guardando<br />
                                                       Por favor espera...<br />
                                                       <br />
                                                       <br />
                                                </div>
                                           </ProgressTemplate>
                                       </asp:UpdateProgress>
                                   </td>
                               </tr>
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" 
                                            ValidationGroup="Rubbermaid" CssClass="button" />
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
