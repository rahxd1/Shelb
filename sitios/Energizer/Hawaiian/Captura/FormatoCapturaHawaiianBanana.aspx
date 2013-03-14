<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Energizer/Hawaiian/HawaiianBanana.Master" 
    AutoEventWireup="false" 
    CodeFiCodeBehindle="FormatoCapturaHawaiianBanana.aspx.vb" 
    Inherits="procomlcd.FormatoCapturaHawaiianBanana"
    Title="Hawaiian/Banana - Captura" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1HawaiianBanana" runat="Server">
  
    <!--CONTENT CONTAINER-->
<div id="titulo-pagina">
    Formato captura de información</div>
        <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server"/>
                    <table style="width: 100%">
                        <tr>
                            <td colspan="5" style="text-align: right">
                                <img alt="" src="../../../../Img/arrow.gif" /><asp:HyperLink ID="lnkRegresar" 
                                            runat="server" NavigateUrl="~/sitios/Energizer/Hawaiian/Captura/RutaHawaiianBanana.aspx">Regresar</asp:HyperLink></td>
                        </tr>
                        <tr>
                            <td style="border-left: 1px solid #000000; border-right: 1px solid #000000; border-top: 1px solid #000000; text-align: left; width: 140px; height: 37px; border-bottom-color: #000000; border-bottom-width: 1px;" 
                                bgcolor="#CCCCCC">
                                <asp:LinkButton ID="lnkFormato" runat="server" Font-Bold="True">Formato demostradora</asp:LinkButton>
                            </td>
                            <td style="width: 138px; height: 37px;">
                                </td>
                            <td style="border-left: 1px solid #000000; border-right: 1px solid #000000; border-top: 1px solid #000000; text-align: left; width: 112px; height: 37px; border-bottom-color: #000000; border-bottom-width: 1px;" 
                                bgcolor="#CCCCCC">
                                <asp:LinkButton ID="lnkPromocionales" runat="server" Font-Bold="True">Promocionales</asp:LinkButton>
                            </td>
                            <td style="text-align: left; height: 37px;">
                                </td>
                            <td bgcolor="#CCCCCC" 
                                style="border-left: 1px solid #000000; border-right: 1px solid #000000; border-top: 1px solid #000000; text-align: left; height: 37px; width: 112px; border-bottom-color: #000000; border-bottom-width: 1px;">
                                <asp:LinkButton ID="lnkFotos" runat="server" Font-Bold="True">Fotografías</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    
            <asp:UpdatePanel ID="UpdatePanel" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlDemostradora" runat="server" BorderColor="Black" 
                        BorderStyle="Solid" BorderWidth="1px">
                        <table style="width: 100%">
                            <tr>
                                <td align="left" valign="top">
                                    <asp:GridView ID="gridDemos" runat="server" 
                                        AutoGenerateColumns="False" Caption="DEMOSTRADORAS" 
                                        DataKeyNames="id_marca" ShowFooter="True" 
                                        style="text-align: center" Width="100%" CssClass="grid-view">
                                        <RowStyle HorizontalAlign="Left" 
                                            VerticalAlign="Top" />
                                        <Columns>
                                            <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                            <asp:TemplateField HeaderText="Cantidad demos">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDemos" runat="server" MaxLength="3" 
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "cantidad_demos") %>' Width="50"></asp:TextBox>
                                                    <asp:RangeValidator ID="rvaDemos" runat="server" ControlToValidate="txtDemos" 
                                                        Display="Dynamic" ErrorMessage="La cantidad de demos debe ser con números" MaximumValue="9999" 
                                                        MinimumValue="0" Type="Double" ValidationGroup="Energizer">*</asp:RangeValidator>
                                                </ItemTemplate>
                                                <ItemStyle Width="0%" VerticalAlign="top" />
                                            </asp:TemplateField>
                                            
                                        </Columns>
                                        <FooterStyle CssClass="grid-footer" />
                                        <EmptyDataTemplate>
                                            <h1>Sin información</h1>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                                <td style="width: 88px">
                                    &nbsp;</td>
                                <td>
                                    <asp:GridView ID="gridFrentes" runat="server" AutoGenerateColumns="False" 
                                        Caption="FRENTES" DataKeyNames="id_marca" 
                                        ShowFooter="True" style="text-align: center" Width="100%" 
                                        CssClass="grid-view">
                                        <RowStyle HorizontalAlign="Left" 
                                            VerticalAlign="Top" />
                                        <Columns>
                                            <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                            <asp:TemplateField HeaderText="Cantidad frentes">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtFrentes" runat="server" MaxLength="3" 
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "cantidad_frentes") %>' 
                                                        Width="50"></asp:TextBox>
                                                    <asp:RangeValidator ID="rvaFrentes" runat="server" 
                                                        ControlToValidate="txtFrentes" Display="Dynamic" ErrorMessage="La cantidad de frentes debe ser con números"
                                                        MaximumValue="9999" MinimumValue="0" Type="Double" 
                                                        ValidationGroup="Energizer">*</asp:RangeValidator>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="top" Width="0%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grid-footer" />
                                        <EmptyDataTemplate>
                                            <h1>Sin información</h1>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td style="width: 88px">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:GridView ID="gridExhibidores" runat="server" AutoGenerateColumns="False" 
                                        Caption="EXHIBIDORES EN TIENDA" DataKeyNames="id_exhibidor" 
                                        ShowFooter="True" style="text-align: center" Width="100%" 
                                        CssClass="grid-view">
                                        <RowStyle HorizontalAlign="Left" 
                                            VerticalAlign="Top" />
                                        <Columns>
                                            <asp:BoundField DataField="nombre_exhibidor" HeaderText="Tipo Exhibidor" />
                                            <asp:BoundField HeaderText="Imagenes" DataField="imagen" HtmlEncode="false" />
                                            <asp:TemplateField HeaderText="Cantidad Exhibidores">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtExhibidor" runat="server" MaxLength="3" 
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "cantidad") %>' 
                                                        Width="50"></asp:TextBox>
                                                    <asp:RangeValidator ID="rvaExhibidor" runat="server" 
                                                        ControlToValidate="txtExhibidor" Display="Dynamic" ErrorMessage="La cantidad de exhibidores debe ser con números" 
                                                        MaximumValue="9999" MinimumValue="0" Type="Double" 
                                                        ValidationGroup="Energizer">*</asp:RangeValidator>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="top" Width="0%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grid-footer" />
                                        <EmptyDataTemplate>
                                            <h1>Sin información</h1>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:GridView ID="gridCompetencia" runat="server" AutoGenerateColumns="False" 
                                        Caption="EXHIBIDOR COMPETENCIA" DataKeyNames="id_marca" ShowFooter="True" 
                                        style="text-align: left" Width="49%" CssClass="grid-view">
                                        <RowStyle HorizontalAlign="Left" 
                                            VerticalAlign="Top" />
                                        <Columns>
                                            <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                            <asp:TemplateField HeaderText="Cantidad Exhibidores">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtExhibidor" runat="server" MaxLength="3" 
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "cantidad_exhibidor") %>' 
                                                        Width="50"></asp:TextBox>
                                                    <asp:RangeValidator ID="rvaExhibidor" runat="server" 
                                                        ControlToValidate="txtExhibidor" Display="Dynamic" ErrorMessage="La cantidad de exhibidores de la competencia debe ser con números"
                                                        MaximumValue="9999" MinimumValue="0" Type="Double" 
                                                        ValidationGroup="Energizer">*</asp:RangeValidator>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="top" Width="0%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grid-footer" />
                                        <EmptyDataTemplate>
                                            <h1>
                                                Sin información</h1>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                  
                        <br />
                
                        <table style="width: 100%" >
                           <tr style="color: #FFFFFF">
                               <td style="text-align: center; background-color: #000000; height: 29px; width: 616px;" bgcolor="#507CD1" >
                                   ACTIVIDADES Y PROMOCIONES HT Y COMPETENCIA</td></tr>
                           <tr style="font-size: small">
                               <td style="font-size: x-small; height: 75px; width: 616px;" >
                                   <asp:TextBox ID="txtComentarios" runat="server" Width="655px" Height="60px" 
                                       TextMode="MultiLine"></asp:TextBox></td></tr>
                           </table>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
               </Triggers>
            </asp:UpdatePanel>
            
                    <asp:Panel ID="pnlPromos" runat="server" Visible="False" 
                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
                         <asp:UpdatePanel ID="UpdatePanelPromos" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gridPromocionales" runat="server" AutoGenerateColumns="False" DataKeyNames="folio_historial" 
                                    ShowFooter="True" style="text-align: left" Width="100%" Caption="Tickets" 
                                    CssClass="grid-view">
                                    <RowStyle HorizontalAlign="Left" 
                                        VerticalAlign="Top" />
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" 
                                            DeleteImageUrl="~/sitios/Energizer/Imagenes/delete-icon.png" 
                                            ShowDeleteButton="True" />
                                        <asp:BoundField DataField="folio_historial" HeaderText="Folio" />
                                        <asp:BoundField DataField="nombre_cliente" HeaderText="Nombre Cliente" />
                                        <asp:BoundField DataField="ticket" HeaderText="No. Ticket" />
                                        <asp:BoundField DataField="subtotales" HeaderText="Cantidad de promocionales" />
                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                    <EmptyDataTemplate>
                                        <h1>
                                            Sin tickets capturados</h1>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                        <br />
                            <table style="width: 100%">
                                <tr>
                                    <td style="color: #FF0000; text-align: center">
                                        <b>*Para guardar los datos del ticket completa los datos siguientes, por ticket 
                                        y selecciona agregar y sigue llenando la cantidad de tickets que requieras* </b></td>
                                </tr>
                                <tr>
                                    <td style="color: #FF0000; text-align: center">
                                        <table style="width: 100%">
                                            <tr>
                                                <td bgcolor="#000FFF" colspan="2" style="color: #FFFFFF">
                                                    <b>PROMOCIONALES ENTREGADOS HAWAIIAN TROPIC</b></td>
                                            </tr>
                                            <tr>
                                                <td align="left" bgcolor="#000FFF" style="color: #FFFFFF">
                                                    <b style="text-align: left">Nombre del Cliente</b></td>
                                                <td style="text-align: left">
                                                    <asp:TextBox ID="txtNombre" runat="server" MaxLength="50" 
                                                        style="margin-left: 0px; margin-right: 0px" Width="283px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvCliente" runat="server" 
                                                        ControlToValidate="txtNombre" ErrorMessage="Completa el Nombre del Cliente" 
                                                        Font-Bold="True" ValidationGroup="Ticket">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#000FFF" style="color: #FFFFFF; text-align: left">
                                                    <b>Ticket</b></td>
                                                <td style="text-align: left">
                                                    <asp:TextBox ID="txtTicket" runat="server" MaxLength="50" 
                                                        style="margin-left: 0px; margin-right: 0px" Width="283px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvTicket" runat="server" 
                                                        ControlToValidate="txtTicket" ErrorMessage="Completa el numero de ticket" 
                                                        Font-Bold="True" ValidationGroup="Ticket">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">
                                        <asp:GridView ID="gridPromos" runat="server" AutoGenerateColumns="False" 
                                            CssClass="grid-view" DataKeyNames="id_promo" ShowFooter="True" 
                                            style="text-align: left" Width="100%">
                                            <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <Columns>
                                                <asp:BoundField DataField="nombre_promo" HeaderText="Promocional" />
                                                <asp:TemplateField HeaderText="Cantidad">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCantidad" runat="server" MaxLength="3" 
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "cantidad") %>' Width="50"></asp:TextBox>
                                                        <asp:RangeValidator ID="rvaCantidad" runat="server" 
                                                            ControlToValidate="txtCantidad" Display="Dynamic" 
                                                            ErrorMessage="La cantidad de promocionales es solo Números" MaximumValue="9999" 
                                                            MinimumValue="0" Type="Double" ValidationGroup="Ticket">*</asp:RangeValidator>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="top" Width="20%" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="grid-footer" />
                                            <EmptyDataTemplate>
                                                <h1>
                                                    Sin información</h1>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; height: 4px;">
                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Black" 
                                            style="text-align: center" 
                                            Text="*Da click en agregar para completar los datos del ticket, y asegurate que la información se muestre en la tabla superior llamada Tickets.*"></asp:Label>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Button ID="btnAgregar" runat="server" CssClass="button" Text="Agregar" 
                                            ValidationGroup="Ticket" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                            ValidationGroup="Ticket" />
                                    </td>
                                </tr>
                                
                                
                                  <tr>
                                    <td style="text-align: right">
                                    <asp:GridView ID="gridProductosVendidos" runat="server" AutoGenerateColumns="False" 
                                        Caption="PRODUCTOS VENDIDOS PARA CANJES" CssClass="grid-view" DataKeyNames="id_producto" 
                                        ShowFooter="True" style="text-align: center" Width="100%">
                                        <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        <Columns>
                                            <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                            <asp:TemplateField HeaderText="Cantidad vendida">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCantidad" runat="server" MaxLength="3" 
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "cantidad") %>' Width="50"></asp:TextBox>
                                                    <asp:RangeValidator ID="rvaCantidad" runat="server" ControlToValidate="txtCantidad" 
                                                        Display="Dynamic" ErrorMessage="La cantidad debe ser con números" 
                                                        MaximumValue="9999" MinimumValue="0" Type="Double" ValidationGroup="Energizer">*</asp:RangeValidator>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="top" Width="0%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grid-footer" />
                                        <EmptyDataTemplate>
                                            <h1>
                                                Sin información</h1>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                      
                                      
                                    </td>
                                </tr>  
                                
                            </table>
                            
                            
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnAgregar" EventName="Click" /> 
                            </Triggers>
                         </asp:UpdatePanel>
                    </asp:Panel>

                    <asp:Panel ID="pnlFotos" runat="server" Visible="False" Width="100%">
                        <table style="border-style: groove; width: 99%; height: 15px; margin-right: 0px">
                            <tr style="color: #FFFFFF">
                                <td bgcolor="black" 
                                    style="text-align: left; background-color: #000000; height: 15px; width: 538px;">
                                    Descripción</td>
                                <td align="left" colspan="2" style="text-align: left; height: 15px; ">
                                    <asp:TextBox ID="txtDescripcion" runat="server" Height="60px" MaxLength="50" 
                                        TextMode="MultiLine" Width="522px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" 
                                        ControlToValidate="txtDescripcion" ErrorMessage="*" Font-Bold="True" 
                                        ValidationGroup="Foto"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr style="color: #FFFFFF">
                                <td bgcolor="black" 
                                    style="text-align: left; background-color: #000000; height: 15px; width: 538px;">
                                    Fotografía</td>
                                <td align="left" style="text-align: left; height: 15px; width: 616px;">
                                    <asp:FileUpload ID="FileUpload1" runat="server" Width="312px" />
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
                                    <asp:UpdatePanel ID="UpdatePanelFotos" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="gridImagenes" runat="server" AutoGenerateColumns="False" 
                                                CssClass="grid-view" DataKeyNames="folio_foto" Width="100%">
                                                <RowStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" 
                                                    VerticalAlign="Top" />
                                                <Columns>
                                                    <asp:CommandField ButtonType="Image" 
                                                        DeleteImageUrl="~/sitios/Energizer/Imagenes/delete-icon.png" 
                                                        ShowDeleteButton="True" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <br />
                                                            <b>Descripción: </b><%#Eval("descripcion")%>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="top" Width="90%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Foto">
                                                        <ItemTemplate>
                                                            <img alt="Foto" src='<%#Eval("ruta")%><%#Eval("foto")%>' width="230" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grid-footer" HorizontalAlign="Center" 
                                                    VerticalAlign="Top" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnsubir" />
                                       </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
            
            <br />
                            <table style="width: 100%">
                        <tr>
                            <td style="text-align: center; height: 36px; " colspan="2">
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
                            <td colspan="2" style="text-align: center; height: 36px; ">
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
                                    ValidationGroup="Energizer" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; height: 31px; width: 271px;">
                                <asp:Button ID="btnGuardar" runat="server" CssClass="button" Text="Guardar" 
                                    ValidationGroup="Energizer" />
                            </td>
                            <td style="text-align: center; height: 31px;">
                                <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" 
                                    CssClass="button" Text="Cancelar" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;</td></tr></table>
                        
            </div>
                      
        <!--CONTENT SIDE COLUMN AVISOS-->
            <div class="clear"></div>
        </div>
</asp:Content>