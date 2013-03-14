<%@ Page Language="vb" 
    Culture="es-MX" 
    MASTERPAGEFILE="~/sitios/Ferrero/Ferrero-Danone/FerreroDanone.Master" 
    AutoEventWireup="false" 
    CodeBehind="FormatoCapturaFerreroDanone.aspx.vb" 
    Inherits="procomlcd.FormatoCapturaFerreroDanone"
    title="Ferrero - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentFerreroDanone" runat="Server">
    <!--PAGE TITLE-->
<div id="titulo-pagina">Formato captura información</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">

<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"/>

                    <table style="width: 100%">
                        <tr><td style="text-align: right;" colspan="2">
                                <img alt="" src="../../../../Img/arrow.gif" />
                                <asp:HyperLink ID="lnkRegresar" runat="server" 
                                    NavigateUrl="~/sitios/Ferrero/Ferrero-Danone/Captura/RutasFerreroDanone.aspx">Regresar</asp:HyperLink></td>
                        </tr>
                        <tr><td style="width: 65px">Tienda</td>
                            <td>
                                <asp:TextBox ID="txtTienda" runat="server" Height="18px" Width="308px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvTienda" runat="server" 
                                       ErrorMessage="Selecciona la tienda" ValidationGroup="Ferrero" 
                                       ControlToValidate="txtTienda">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr><td style="width: 65px">Direccion</td>
                            <td>
                                <asp:TextBox ID="txtDireccion" runat="server" Height="18px" Width="308px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDireccion" runat="server" 
                                       ErrorMessage="Selecciona la tienda" ValidationGroup="Ferrero" 
                                       ControlToValidate="txtDireccion">*</asp:RequiredFieldValidator>
                                </td>
                        </tr>
                        <tr><td style="width: 65px">Colonia</td>
                            <td>
                                <asp:DropDownList ID="cmbColonia" runat="server" 
                                    Height="21px" Width="466px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvColonia" runat="server" 
                                       ErrorMessage="Selecciona la tienda" ValidationGroup="Ferrero" 
                                       ControlToValidate="cmbColonia">*</asp:RequiredFieldValidator>
                                </td>
                        </tr>
                        </table>
                    
                            <asp:Panel ID="pnl1" Visible="true" runat="server" style="text-align: center">
                                <asp:GridView ID="gridProductos1" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="id_producto" Width="100%" ShowFooter="True" 
                                    Caption="PRODUCTOS" CssClass="grid-view">
                                    <Columns>
                                        <asp:BoundField HeaderText="Producto" DataField="nombre_producto" />
                                        <asp:TemplateField HeaderText="Catalogado">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCatalogado" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Catalogado")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Faltantes">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkFaltantes" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Faltante")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Caducidad (dd/mm/aaaa)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCaducidad" runat="server" Width="80" MaxLength="10" Text='<%#DataBinder.Eval(Container.DataItem, "Caducidad","{0:d}")%>' ></asp:TextBox>
                                                <asp:RegularExpressionValidator id="rvaCaducidad" runat="server" ErrorMessage="La fecha no es correcta formato 'dd/mm/aaaa'" Display="Dynamic" 
                                                    ValidationExpression="\d{2}/\d{2}/\d{4}" ControlToValidate="txtCaducidad" ValidationGroup="Ferrero" >*</asp:RegularExpressionValidator>
                                                <asp:RangeValidator ID="rvCaducidad" runat="server" ErrorMessage="La fecha no es valida" 
                                                    ValidationGroup="Ferrero" ControlToValidate="txtCaducidad"
                                                    MaximumValue="31/12/3000" MinimumValue="01/01/2010" Type="Date">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle Width="0%" VerticalAlign="top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                    <EmptyDataTemplate>
                                        <h1>Se ha producido un error</h1>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </asp:Panel>
                            <br />
                            <table style="border-style: groove; width: 99%; height: 15px; margin-right: 0px">
                                    <tr style="color: #FFFFFF">
                                        <td bgcolor="black" 
                                            style="text-align: center; background-color: #000000; height: 15px; " 
                                            colspan="3">
                                            ENCUESTA</td>
                                    </tr>
                                    <tr style="color: #FFFFFF">
                                        <td style="text-align: center; height: 15px; color: #000000;">
                                            1.</td>
                                        <td style="text-align: left; height: 15px; color: #000000; width: 339px;">
                                            ¿QUIEN SURTE LOS PRODUCTOS DE FERRERO?</td>
                                        <td style="text-align: left; height: 15px; color: #000000;">
                                            <asp:DropDownList ID="cmb1" runat="server" 
                                                Height="21px" Width="134px">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>DANONE</asp:ListItem>
                                                <asp:ListItem>OTROS</asp:ListItem>
                                                <asp:ListItem>YO LOS COMPRO</asp:ListItem>
                                            </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv1" runat="server" 
                                       ErrorMessage="Contesta pregunta 1" ValidationGroup="Ferrero" 
                                       ControlToValidate="cmb1">*</asp:RequiredFieldValidator>
                                            </td>
                                    </tr>
                                    <tr style="color: #FFFFFF">
                                        <td style="text-align: center; height: 15px; color: #000000;">
                                            2.</td>
                                        <td style="text-align: left; height: 15px; color: #000000; width: 339px;">
                                            ¿TIENE EXHIBIDO EL PRODUCTO?</td>
                                        <td style="text-align: left; height: 15px; color: #000000;">
                                            <asp:DropDownList ID="cmb2" runat="server" 
                                                Height="21px" Width="75px">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>SI</asp:ListItem>
                                                <asp:ListItem>NO</asp:ListItem>
                                            </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv2" runat="server" 
                                       ErrorMessage="Constesta pregunta 2" ValidationGroup="Ferrero" 
                                       ControlToValidate="cmb2">*</asp:RequiredFieldValidator>
                                            </td>
                                    </tr>
                                    <tr style="color: #FFFFFF">
                                        <td style="text-align: center; height: 15px; color: #000000;">
                                            &nbsp;</td>
                                        <td style="text-align: right; height: 15px; color: #000000; width: 339px;">
                                            ¿DONDE?</td>
                                        <td style="text-align: left; height: 15px; color: #000000;">
                                            <asp:CheckBox ID="ch2A" runat="server" Text="REFRIGERADOR DANONE" />
                                            <br />
                                            <asp:CheckBox ID="ch2B" runat="server" Text="REFRIGERADOR OTROS" />
                                            <br />
                                            <asp:CheckBox ID="ch2C" runat="server" Text="EXTERIOR" />
                                        </td>
                                    </tr>
                                    <tr style="color: #FFFFFF">
                                        <td style="text-align: center; height: 15px; color: #000000;">
                                            3.</td>
                                        <td style="text-align: left; height: 15px; color: #000000; width: 339px;">
                                            ¿HAY MATERIAL POP?</td>
                                        <td style="text-align: left; height: 15px; color: #000000;">
                                            <asp:DropDownList ID="cmb3" runat="server" 
                                                Height="21px" Width="78px">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>SI</asp:ListItem>
                                                <asp:ListItem>NO</asp:ListItem>
                                            </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv3" runat="server" 
                                       ErrorMessage="Contesta pregunta 3" ValidationGroup="Ferrero" 
                                       ControlToValidate="cmb3">*</asp:RequiredFieldValidator>
                                            </td>
                                    </tr>
                                    <tr style="color: #FFFFFF">
                                        <td style="text-align: center; height: 15px; color: #000000;">
                                            &nbsp;</td>
                                        <td style="text-align: right; height: 15px; color: #000000; width: 339px;">
                                            ¿QUE TIPO?</td>
                                        <td style="text-align: left; height: 15px; color: #000000;">
                                            <asp:CheckBox ID="ch3A" runat="server" Text="CHUPONERA" />
                                            <br />
                                            <asp:CheckBox ID="ch3B" runat="server" Text="POSTER" />
                                            <br />
                                            <asp:CheckBox ID="ch3C" runat="server" Text="OTRO" />
                                        </td>
                                    </tr>
                                    <tr style="color: #FFFFFF">
                                        <td style="text-align: center; height: 15px; color: #000000;">
                                            4.</td>
                                        <td style="text-align: left; height: 15px; color: #000000; width: 339px;">
                                            ¿CUAL ES LA FRECUENCIA DE VISITA DEL PREVENTISTA DE FERRERO PARA LEVANTAR 
                                            PEDIDO?</td>
                                        <td style="text-align: left; height: 15px; color: #000000;">
                                            <asp:DropDownList ID="cmb4" runat="server" 
                                                Height="21px" Width="138px">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>SEMANA</asp:ListItem>
                                                <asp:ListItem>QUINCENA</asp:ListItem>
                                                <asp:ListItem>MES</asp:ListItem>
                                                <asp:ListItem>NUNCA</asp:ListItem>
                                            </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv4" runat="server" 
                                       ErrorMessage="Contesta pregunta 4" ValidationGroup="Ferrero" 
                                       ControlToValidate="cmb4">*</asp:RequiredFieldValidator>
                                            </td>
                                    </tr>
                                    <tr style="color: #FFFFFF">
                                        <td style="text-align: center; height: 15px; color: #000000;">
                                            5.</td>
                                        <td style="text-align: left; height: 15px; color: #000000; width: 339px;">
                                            ¿EN CUANTO TIEMPO TE ENTREGAN EL PEDIDO REALIZADO?</td>
                                        <td style="text-align: left; height: 15px; color: #000000;">
                                            <asp:DropDownList ID="cmb5" runat="server" 
                                                Height="21px" Width="138px">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>1 DIA</asp:ListItem>
                                                <asp:ListItem>2 A 3 DIAS</asp:ListItem>
                                                <asp:ListItem>1 SEMANA</asp:ListItem>
                                                <asp:ListItem>MÁS TIEMPO</asp:ListItem>
                                            </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv5" runat="server" 
                                       ErrorMessage="Contesta pregunta 5" ValidationGroup="Ferrero" 
                                       ControlToValidate="cmb5">*</asp:RequiredFieldValidator>
                                            </td>
                                    </tr>
                               </table>
                               <br />
                               
                                <table style="border-style: groove; width: 99%; height: 15px; margin-right: 0px">
                                    <tr style="color: #FFFFFF">
                                        <td bgcolor="black" 
                                            style="text-align: center; background-color: #000000; height: 15px; width: 538px;">
                                            Descripción</td>
                                        <td align="left" colspan="2" style="text-align: left; height: 15px; ">
                                <asp:TextBox ID="txtDescripcion" runat="server" Height="18px" Width="308px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" 
                                                ControlToValidate="txtDescripcion" ErrorMessage="*" Font-Bold="True" 
                                                ValidationGroup="Foto"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr style="color: #FFFFFF">
                                        <td bgcolor="black" 
                                            style="text-align: center; background-color: #000000; height: 15px; width: 538px;">
                                            Fotografía</td>
                                        <td align="left" style="text-align: left; height: 15px; width: 616px;">
                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                            <asp:RequiredFieldValidator ID="rfvFile" runat="server" 
                                                ControlToValidate="FileUpload1" ErrorMessage="*" Font-Bold="True" 
                                                ValidationGroup="Foto"></asp:RequiredFieldValidator>
                                        </td>
                                        <td align="left" style="text-align: center; height: 15px; width: 616px;">
                                            <asp:Button ID="btnSubir" runat="server" CssClass="button" Text="Subir" 
                                                ValidationGroup="Foto" />
                                        </td>
                                    </tr>
                                    <tr style="color: #FFFFFF">
                                        <td bgcolor="black" 
                                            style="text-align: center; background-color: #000000; height: 15px; width: 538px;">
                                        </td>
                                        <td align="left" colspan="2" 
                                            style="text-align: left; height: 15px; background-color: #FFFFFF;">
                                            <asp:Label ID="lblSubida" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="color: #FFFFFF">
                                        <td colspan="3" style="text-align: center; height: 15px; ">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gridImagenes" runat="server" AutoGenerateColumns="False" 
                                                        BorderStyle="None" DataKeyNames="folio_foto" ForeColor="Black" 
                                                        ShowHeader="False" Width="100%">
                                                        <RowStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" 
                                                            VerticalAlign="Top" />
                                                        <Columns>
                                                            <asp:CommandField ButtonType="Image" 
                                                                DeleteImageUrl="~/sitios/Ferrero/Imagenes/delete-icon.png" 
                                                                ShowDeleteButton="True" />
                                                            <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                                            <asp:TemplateField HeaderText="Foto">
                                                                <ItemTemplate>
                                                                    <img alt="Foto" src='<%#Eval("ruta")%><%#Eval("foto")%>' width="200" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:GridView>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnSubir" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                    <br />
            
            
                    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <fieldset>
                            <table style="width: 100%">
                               <tr>
                                   <td style="text-align: center; height: 31px; " colspan="2">
                                   
                                       <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                           ValidationGroup="Ferrero" />
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
                                            ValidationGroup="Ferrero" CssClass="button" />
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
