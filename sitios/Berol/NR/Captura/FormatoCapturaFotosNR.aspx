<%@ Page Language="vb" 
    Culture="es-MX" 
    MasterPageFile="~/sitios/Berol/NR/Rubbermaid.Master" 
    AutoEventWireup="false" 
    CodeBehind="FormatoCapturaFotosNR.aspx.vb" 
    Inherits="procomlcd.FormatoCapturaFotosNR"
    Title="Newell Rubbermaid - Fotografías" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoNR" runat="Server">
  
    <!--CONTENT CONTAINER-->
<div id="titulo-pagina">
    Levantamiento fotografías</div>
        <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
    
            <table style="width: 100%">
                <tr>
                    <td style="width: 27px">
                        &nbsp;</td>
                    <td style="width: 75px">
                        &nbsp;</td>
                    <td style="width: 75px">
                        &nbsp;</td>
                    <td style="text-align: right">
                        <img alt="" src="../../../../Img/arrow.gif" />
                        <a href="RutasPromotorNR.aspx">Regresar</a></td>
                </tr>
                <tr>
                    <td style="height: 21px; " colspan="2">
                        &nbsp;</td>
                    <td style="height: 21px; " colspan="2">
                        &nbsp;</td>
                </tr>
                </table>

                        <table style="border-style: groove; width: 99%; height: 15px; margin-right: 0px">
                            <tr style="color: #FFFFFF">
                                <td bgcolor="black" 
                                    style="text-align: center; background-color:black; height: 15px; width: 538px;">
                                    Descripción</td>
                                <td align="left" colspan="2" style="text-align: left; height: 15px; ">
                                    <table style="width: 100%; color: #000000">
                                        <tr>
                                            <td colspan="2">
                                                Solo puedes subir 5 fotos, sube fotos de diferentes productos y diferente 
                                                ubicación</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <asp:RadioButtonList ID="chkTipo" runat="server">
                                                    <asp:ListItem Value="1">Newell rubbermaid</asp:ListItem>
                                                    <asp:ListItem Value="2">Competencia</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                Ubicación</td>
                                            <td>
                                                <asp:DropDownList ID="cmbUbicacion" runat="server">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvUbicacion" runat="server" 
                                                    ControlToValidate="cmbUbicacion" ErrorMessage="*" Font-Bold="True" 
                                                    ValidationGroup="Foto"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                Familia</td>
                                            <td>
                                                <asp:DropDownList ID="cmbFamilia" runat="server">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvFamilia" runat="server" 
                                                    ControlToValidate="cmbFamilia" ErrorMessage="*" Font-Bold="True" 
                                                    ValidationGroup="Foto"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                Comentarios</td>
                                            <td>
                                    <asp:TextBox ID="txtDescripcion" runat="server" MaxLength="50" 
                                        TextMode="MultiLine" Width="355px" Height="51px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="color: #FFFFFF">
                                <td bgcolor="black" 
                                    style="text-align: center; background-color:black; height: 15px; width: 538px;">
                                    Seleccionar fotografía</td>
                                <td align="left" style="text-align: left; height: 15px; width: 616px;">
                                    <asp:FileUpload ID="FileUpload1" runat="server" Width="312px" />
                                    <asp:RequiredFieldValidator ID="rfvFile" runat="server" 
                                        ControlToValidate="FileUpload1" ErrorMessage="*" Font-Bold="True" 
                                        ValidationGroup="Foto"></asp:RequiredFieldValidator>
                                </td>
                                <td align="left" style="text-align: center; height: 15px; width: 92px;">
                                    <asp:Button ID="btnSubir" runat="server" CssClass="button" Text="Subir" 
                                        ValidationGroup="Foto" />
                                </td>
                            </tr>
                            <tr style="color: #FFFFFF">
                                <td bgcolor="black" 
                                    style="text-align: center; background-color:black; height: 15px; width: 538px;">
                                </td>
                                <td align="left" colspan="2" 
                                    style="text-align: left; height: 15px; background-color: #FFFFFF;">
                                    <asp:Label ID="lblSubida" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                                    
                                </td>
                            </tr>
                            <tr style="color: #FFFFFF">
                                <td colspan="3" style="text-align: center; height: 15px; ">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <fieldset>
                                                 <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both" Height="350px"> 
                                                    <asp:GridView ID="gridImagenes" runat="server" AutoGenerateColumns="False" 
                                                        DataKeyNames="folio_foto" ShowHeader="False" Width="100%" 
                                                        ForeColor="Black">
                                                        <RowStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" 
                                                            VerticalAlign="Top" />
                                                        <Columns>
                                                            <asp:CommandField ButtonType="Image" 
                                                                DeleteImageUrl="~/Img/delete-icon.png" 
                                                                ShowDeleteButton="True" />
                                                            <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                                            <asp:TemplateField HeaderText="Foto">
                                                                <ItemTemplate>
                                                                    <img alt="Foto" src='<%# Eval("foto", "/ARCHIVOS/CLIENTES/BEROL/NR/IMAGENES/ANAQUEL/PROMOTOR/{0}") %>' width="230" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:GridView>  
                                                </asp:Panel>
                                                <asp:UpdateProgress ID="UpdateProgress" runat="server" 
                                                   AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0">
                                                   <ProgressTemplate>
                                                       <div id="pnlSave">
                                                            <img alt="" src="../../../../Img/loading.gif" />La información se esta guardando<br />
                                                                   Por favor espera...<br />
                                                                   <br />
                                                       </div>
                                                   </ProgressTemplate>
                                               </asp:UpdateProgress>
                                            </fieldset>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnsubir" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                      
                        </div><!--CONTENT SIDE COLUMN AVISOS--><div id="content-side-two-column">
            </div><div class="clear">
            </div>
        </div>
   </div>
</asp:Content>