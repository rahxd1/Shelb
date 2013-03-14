<%@ Page Language="vb" 
    Culture="es-MX" 
    MasterPagefile="~/sitios/Mars/Capacitacion/Capacitacion_Mars.Master" 
    AutoEventWireup="false" 
    CodeBehind="FormatoCuestionariosMars.aspx.vb" 
    Inherits="procomlcd.FormatoCuestionariosMars"
    Title="Mars - Capacitacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMarsCapacitacion" runat="Server">
  
    <!--CONTENT CONTAINER-->
<div id="titulo-pagina">
    Cuestionario</div>
        <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
    
                <table style="width: 100%">
                <tr>
                    <td style="width: 58px">
                        &nbsp;</td>
                    <td style="text-align: right">
                        <asp:Label ID="lblFolioAct" runat="server" Text="0" Visible="False"></asp:Label>
                        <asp:Label ID="lblIDSeccion" runat="server" Text="1" Visible="False"></asp:Label>
                        <img alt="" src="../../../../Img/arrow.gif" /><asp:HyperLink ID="lnkRegresar" runat="server" 
                            NavigateUrl="~/sitios/Mars/Capacitacion/Cuestionarios/CuestionariosMars.aspx">Regresar</asp:HyperLink></td>
                </tr>
                
                <tr>
                    <td style="width: 58px">
                        Cuestionario:</td>
                    <td style="text-align: left">
                        <asp:Label ID="lblCuestionario" runat="server" Font-Bold="True"></asp:Label></td>
                </tr>
                
                <tr>
                    <td colspan="2">
                            
                    </td>
                </tr>
                
                </table>
                
                <asp:Panel ID="pnlCompletado" runat="server" CssClass="pnlCompletado" 
                    Visible="False">
                    <br /><br /><br /><br /><br /><br />
                    <br />El cuestionario ya ha sido completado. 
                    <br />Por favor espera los resultados con tu supervisor y/o ejecutivo.
                    <br />
                    <asp:Button ID="btnRegresar" runat="server" CausesValidation="False" 
                                  CssClass="button" Text="Regresar" />
                </asp:Panel>
                            
                <table style="width: 100%">
                    <tr>
                        <td style="border: thin solid #000000">
                        <asp:Label ID="lblSeccionPregunta" runat="server" Font-Bold="True"></asp:Label>
                        <asp:GridView ID="gridRespuestas" runat="server" AutoGenerateColumns="False" 
                            DataKeyNames="id_pregunta" Width="100%" CssClass="grid-view" 
                            GridLines="Horizontal" ShowHeader="False">
                            <Columns>       
                                <asp:BoundField HeaderText="imagen" DataField="imagen"></asp:BoundField>                     
                                <asp:BoundField HeaderText="Opcion" DataField="id_pregunta"></asp:BoundField>
                                
                                <asp:TemplateField HeaderText="Respuesta">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPreguntaSeccion" runat="server" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "pregunta")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <img src="<%#Eval("ruta_imagen")%>" alt="Img"/>
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                 
                                <asp:TemplateField HeaderText="Contestacion">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRespuesta" runat="server" MaxLength="2" Width="30" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "respuesta_abierta")%>'></asp:TextBox>                                          
                                        <asp:RequiredFieldValidator ID="rfvRespuesta" runat="server" 
                                                ControlToValidate="txtRespuesta" 
                                                ErrorMessage="Completa la información" 
                                                ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                         <asp:RegularExpressionValidator ID="revRespuesta" 
                                                runat="server" ControlToValidate="txtRespuesta"
                                                ErrorMessage="La opción no es correcta" ValidationGroup="Mars"
                                                ValidationExpression='<%# DataBinder.Eval(Container.DataItem, "tipo_respuesta")%>'>Opción no valida</asp:RegularExpressionValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>
                        <FooterStyle CssClass="grid-footer" />
                    </asp:GridView>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left" colspan="2">
                        <asp:Label ID="lblComentarios" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="2">
                                    <asp:TextBox ID="txtComentarios" runat="server" Height="108px" 
                                        TextMode="MultiLine" Visible="False" Width="662px"></asp:TextBox>
                                </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="2">
                                    &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                                    <asp:Button ID="btnAnterior" runat="server" CssClass="button" Text="Anterior" 
                                        ValidationGroup="Mars" Enabled="False" />
                                </td>
                        <td style="text-align: center">
                                    <asp:Button ID="btnSiguiente" runat="server" CssClass="button" Text="Siguiente" 
                                        ValidationGroup="Mars" />
                                </td>
                    </tr>
            </table>
                        </td>
                    </tr>
            </table>
                <br />
                
                <asp:UpdatePanel ID="UpdatePanel" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: center; height: 31px; " colspan="2">
                                    <asp:ValidationSummary ID="vsMars" runat="server" 
                                           ValidationGroup="Mars" HeaderText="Por favor corrige los siguientes datos: " 
                                           ShowMessageBox="True" ShowSummary="False" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; height: 31px; " colspan="2">
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
                                <td style="text-align: center; height: 31px; width: 271px;">
                                    <asp:Button ID="btnGuardar" runat="server" CssClass="button" Text="Guardar" 
                                        ValidationGroup="Mars" />
                                </td>
                                <td style="text-align: center; height: 31px;">
                                    <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" 
                                        CssClass="button" Text="Cancelar" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;</td></tr></table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnGuardar" />
                    </Triggers>
                </asp:UpdatePanel>
                </div>  
                      
       <!--CONTENT SIDE COLUMN AVISOS-->
       
       <div id="content-side-two-column">
            </div><div class="clear">
            </div>
    </div>
</asp:Content>