<%@ Page Language="vb" 
    Culture="es-MX" 
    MasterPagefile="~/sitios/Herradura/NewMix/NewMix.Master" 
    AutoEventWireup="false" 
    CodeBehind="FormatoCapturaNewMix.aspx.vb" 
    Inherits="procomlcd.FormatoCapturaNewMix"
    Title="New Mix - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderNewMix" runat="Server">
  
    <!--CONTENT CONTAINER-->
<div id="titulo-pagina">
    Formato captura de información</div>
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
                        <img alt="" src="../../../../Img/arrow.gif" /><asp:HyperLink ID="lnkRegresar" 
                                    runat="server" NavigateUrl="~/sitios/Herradura/NewMix/Captura/RutaNewMix.aspx">Regresar</asp:HyperLink></td>
                </tr>
                
                <tr>
                    <td style="width: 58px">
                        Tienda:</td>
                    <td style="text-align: left">
                        <asp:Label ID="lbltienda" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                
                <tr>
                    <td style="width: 58px">
                        &nbsp;</td>
                    <td style="text-align: right">
                        &nbsp;</td>
                </tr>
                
                </table>
                   <asp:Menu ID="Menu1" Width="99%" runat="server" Orientation="Horizontal" 
                        StaticEnableDefaultPopOutImage="False" 
                        OnMenuItemClick="Menu1_MenuItemClick">
                        <Items>
                            <asp:MenuItem ImageUrl="../../Img/selectedtabPp.GIF" Text=" " Value="0"></asp:MenuItem>
                            <asp:MenuItem ImageUrl="../../Img/unselectedtabEp.GIF" Text=" " Value="1"></asp:MenuItem>
                            <asp:MenuItem ImageUrl="../../Img/unselectedtabPc.GIF" Text=" " Value="2"></asp:MenuItem>
                            <asp:MenuItem ImageUrl="../../Img/unselectedtabEc.GIF" Text=" " Value="3"></asp:MenuItem>
                            <asp:MenuItem ImageUrl="../../Img/unselectedtab.GIF" Text=" " Value="4"></asp:MenuItem>
                        </Items>
                    </asp:Menu>
                        
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="Tab1" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: center">
                                    PRODUCTOS PROPIOS</td>
                            </tr>
                        </table>
                        <asp:GridView ID="gridProductosCliente" runat="server" AutoGenerateColumns="False" 
                            DataKeyNames="id_producto" Width="100%" CssClass="grid-view" 
                            CaptionAlign="Left">
                            <Columns>                            
                                <asp:BoundField HeaderText="Familia" DataField="id_familia" >
                                    <HeaderStyle Width="0%" /></asp:BoundField>
                                <asp:BoundField HeaderText="Producto" DataField="nombre_producto">
                                    <HeaderStyle Width="90%" />
                                    <ItemStyle Width="90%" VerticalAlign="Top" /></asp:BoundField>
                                <asp:BoundField HeaderText="Presentación" DataField="presentacion">
                                    <HeaderStyle Width="0%" /></asp:BoundField>
                                <asp:BoundField HeaderText="Código" DataField="codigo">                          
                                    <HeaderStyle Width="0%" /></asp:BoundField>
                                <asp:TemplateField HeaderText="Catálogado" HeaderStyle-Font-Size="XX-Small">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCatalogado" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "catalogado"))%>' />
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="XX-Small" />
                                    <ItemStyle Width="0%" VerticalAlign="top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sin pedido de la Tienda" HeaderStyle-Font-Size="XX-Small">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSinPedido" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "sin_pedido"))%>' />
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="XX-Small" />
                                    <ItemStyle Width="0%" VerticalAlign="top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Frentes fríos" HeaderStyle-Font-Size="XX-Small">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFrentesFrios" runat="server" MaxLength="3" Width="30" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "frentes_frios")%>'></asp:TextBox>
                                        <asp:RangeValidator ID="rvaFrentesFrios" runat="server" 
                                            ControlToValidate="txtFrentesFrios" Display="Dynamic" 
                                            ErrorMessage="La cantidad de frentes frios del producto indicado con un (*) de los productos de Herradura"
                                            MaximumValue="1999" MinimumValue="0" Type="Double" ValidationGroup="NewMix">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvFrentesFrios" runat="server" 
                                            ControlToValidate="txtFrentesFrios" 
                                            ErrorMessage="Completa la cantidad de frentes frios del producto indicado con un (*) de los productos de Herradura" 
                                            ValidationGroup="NewMix">*</asp:RequiredFieldValidator>
                                            
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="XX-Small" />
                                    <ItemStyle Width="0%" VerticalAlign="top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Frentes secos" HeaderStyle-Font-Size="XX-Small">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFrentesSecos" runat="server" MaxLength="3" Width="30" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "frentes_secos")%>'></asp:TextBox>
                                        <asp:RangeValidator ID="rvaFrentesSecos" runat="server" 
                                            ControlToValidate="txtFrentesSecos" Display="Dynamic" 
                                            ErrorMessage="La cantidad de frentes secos del producto indicado con un (*) de los productos de Herradura"
                                            MaximumValue="1999" MinimumValue="0" Type="Double" ValidationGroup="NewMix">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvFrentesSecos" runat="server" 
                                            ControlToValidate="txtFrentesSecos" 
                                            ErrorMessage="Completa la cantidad de secos frios del producto indicado con un (*) de los productos de Herradura" 
                                            ValidationGroup="NewMix">*</asp:RequiredFieldValidator>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="XX-Small" />
                                    <ItemStyle Width="0%" VerticalAlign="top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inven tario" HeaderStyle-Font-Size="XX-Small">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtInventario" runat="server" MaxLength="3" Width="30" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "Inventario")%>'></asp:TextBox>
                                        <asp:RangeValidator ID="rvaInventario" runat="server" 
                                            ControlToValidate="txtInventario" Display="Dynamic" 
                                            ErrorMessage="La cantidad de inventario del producto indicado con un (*) de los productos de Herradura"
                                            MaximumValue="1999" MinimumValue="0" Type="Double" ValidationGroup="NewMix">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvInventario" runat="server" 
                                            ControlToValidate="txtInventario" 
                                            ErrorMessage="Completa la cantidad de inventario del producto indicado con un (*) de los productos de Herradura" 
                                            ValidationGroup="NewMix">*</asp:RequiredFieldValidator>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="XX-Small" />
                                    <ItemStyle Width="0%" VerticalAlign="top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inven tario sistema" HeaderStyle-Font-Size=".7 em">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtInventarioSis" runat="server" MaxLength="3" Width="30" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "Inventario_sis")%>'></asp:TextBox>
                                        <asp:RangeValidator ID="rvaInventarioSis" runat="server" 
                                            ControlToValidate="txtInventarioSis" Display="Dynamic" 
                                            ErrorMessage="La cantidad de inventario sistema del producto indicado con un (*) de los productos de Herradura"
                                            MaximumValue="1999" MinimumValue="0" Type="Double" ValidationGroup="NewMix">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvInventarioSis" runat="server" 
                                            ControlToValidate="txtInventarioSis" 
                                            ErrorMessage="Completa la cantidad de inventario sistema del producto indicado con un (*) de los productos de Herradura" 
                                            ValidationGroup="NewMix">*</asp:RequiredFieldValidator>
                                            
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="0.7em" />
                                    <ItemStyle Width="0%" VerticalAlign="top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Agotado" HeaderStyle-Font-Size="XX-Small">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAgotado" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Agotado"))%>' />
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="XX-Small" />
                                    <ItemStyle Width="0%" VerticalAlign="top" />
                                </asp:TemplateField>
                            </Columns>
                        <FooterStyle CssClass="grid-footer" />
                    </asp:GridView>
                    </asp:View>
                    <asp:View ID="Tab2" runat="server">
                        <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:GridView ID="gridExhibicionesPropias1" runat="server" 
                                AutoGenerateColumns="False" Caption="Exhibidor propio No.1" CssClass="grid-view" 
                                DataKeyNames="id_producto" ShowFooter="True" Width="85%">
                                <Columns>
                                    <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                    <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSiNo" runat="server" 
                                                Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "SiNo"))%>' />
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="XX-Small" />
                                        <ItemStyle VerticalAlign="top" Width="0%" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grid-footer" />
                            </asp:GridView>
                        </td>
                        <td>
                            <asp:GridView ID="gridExhibicionesPropias2" runat="server" 
                                AutoGenerateColumns="False" Caption="Exhibidor propio No.2" CssClass="grid-view" 
                                DataKeyNames="id_producto" ShowFooter="True" Width="85%">
                                <Columns>
                                    <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                    <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSiNo" runat="server" 
                                                Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "SiNo"))%>' />
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="XX-Small" />
                                        <ItemStyle VerticalAlign="top" Width="0%" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grid-footer" />
                            </asp:GridView>
                        </td>
                        <td>
                            <asp:GridView ID="gridExhibicionesPropias3" runat="server" 
                                AutoGenerateColumns="False" Caption="Exhibidor propio No.3" CssClass="grid-view" 
                                DataKeyNames="id_producto" ShowFooter="True" Width="85%">
                                <Columns>
                                    <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                    <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSiNo" runat="server" 
                                                Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "SiNo"))%>' />
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="XX-Small" />
                                        <ItemStyle VerticalAlign="top" Width="0%" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grid-footer" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                    </asp:View>

                    <asp:View ID="Tab3" runat="server">
                        <asp:GridView ID="gridCompetencia" runat="server" 
                AutoGenerateColumns="False" DataKeyNames="id_producto" Width="99%" 
                style="text-align: center" ShowFooter="True" 
                Caption="COMPETENCIA" CaptionAlign="Top" CssClass="grid-view">
               <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
                <Columns>
                    <asp:BoundField HeaderText="Producto" DataField="nombre_producto">
                            <ItemStyle Width="90%" VerticalAlign="Top" /></asp:BoundField>
                        <asp:BoundField HeaderText="Código" DataField="codigo" />
                    <asp:TemplateField HeaderText="Frentes frios" HeaderStyle-Font-Size="XX-Small" >
                        <ItemTemplate>
                            <asp:TextBox ID="txtFrentesFrios" runat="server" MaxLength="3" Width="40" 
                                Text='<%# DataBinder.Eval(Container.DataItem, "frentes_frios") %>'></asp:TextBox>
                            <asp:RangeValidator ID="rvaFrentesFrios" runat="server" 
                                ControlToValidate="txtFrentesFrios" Display="Dynamic" 
                                ErrorMessage="La cantidad de frentes frios del producto indicado con un (*) de los productos de la competencia"
                                MaximumValue="1999" MinimumValue="0" Type="Double" ValidationGroup="NewMix">*</asp:RangeValidator>
                            <asp:RequiredFieldValidator ID="rfvFrentesFrios" runat="server" 
                                ControlToValidate="txtFrentesFrios" 
                                ErrorMessage="Completa la cantidad de frentes frios del producto indicado con un (*) de los productos de la competencia" 
                                ValidationGroup="NewMix">*</asp:RequiredFieldValidator>
                        </ItemTemplate>
                        <ItemStyle Width="0%" VerticalAlign="top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Frentes secos" HeaderStyle-Font-Size="XX-Small" >
                        <ItemTemplate>
                            <asp:TextBox ID="txtFrentesSecos" runat="server" MaxLength="3" Width="40" 
                                Text='<%# DataBinder.Eval(Container.DataItem, "frentes_secos") %>'></asp:TextBox>
                            <asp:RangeValidator ID="rvaFrentesSecos" runat="server" 
                                ControlToValidate="txtFrentesSecos" Display="Dynamic" 
                                ErrorMessage="La cantidad de frentes secos del producto indicado con un (*) de los productos de la competencia"
                                MaximumValue="1999" MinimumValue="0" Type="Double" ValidationGroup="NewMix">*</asp:RangeValidator>
                            <asp:RequiredFieldValidator ID="rfvFrentesSecos" runat="server" 
                                ControlToValidate="txtFrentesSecos" 
                                ErrorMessage="Completa la cantidad de frentes secos del producto indicado con un (*) de los productos de la competencia" 
                                ValidationGroup="NewMix">*</asp:RequiredFieldValidator>
                        </ItemTemplate>
                        <ItemStyle Width="0%" VerticalAlign="top" />
                    </asp:TemplateField>
                </Columns>
               <FooterStyle CssClass="grid-footer" />
               <EditRowStyle HorizontalAlign="Left" VerticalAlign="Top" />
            </asp:GridView>
                    </asp:View>
                    
                    <asp:View ID="Tab4" runat="server">
                        <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:GridView ID="gridExhibicionesCompetencia1" runat="server" 
                                AutoGenerateColumns="False" Caption="Exhibidor competencia No.1" 
                                CssClass="grid-view" DataKeyNames="id_producto" ShowFooter="True" Width="85%">
                                <Columns>
                                    <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                    <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSiNo" runat="server" 
                                                Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "SiNo"))%>' />
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="XX-Small" />
                                        <ItemStyle VerticalAlign="top" Width="0%" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grid-footer" />
                            </asp:GridView>
                        </td>
                        <td>
                            <asp:GridView ID="gridExhibicionesCompetencia2" runat="server" 
                                AutoGenerateColumns="False" Caption="Exhibidor competencia No.2" 
                                CssClass="grid-view" DataKeyNames="id_producto" ShowFooter="True" Width="85%">
                                <Columns>
                                    <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                    <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSiNo" runat="server" 
                                                Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "SiNo"))%>' />
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="XX-Small" />
                                        <ItemStyle VerticalAlign="top" Width="0%" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grid-footer" />
                            </asp:GridView>
                        </td>
                        <td>
                            <asp:GridView ID="gridExhibicionesCompetencia3" runat="server" 
                                AutoGenerateColumns="False" Caption="Exhibidor competencia No.3" 
                                CssClass="grid-view" DataKeyNames="id_producto" ShowFooter="True" Width="85%">
                                <Columns>
                                    <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                    <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSiNo" runat="server" 
                                                Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "SiNo"))%>' />
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="XX-Small" />
                                        <ItemStyle VerticalAlign="top" Width="0%" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grid-footer" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                    </asp:View>
                </asp:MultiView>
                <br />
                <table style="border-style: groove; width: 100%; height: 99px; margin-right: 0px">
                     <tr style="color: #FFFFFF">
                         <td bgcolor="#507CD1" 
                             style="text-align: center; background-color: #000000; height: 29px; width: 616px;">
                             Comentarios</td>
                     </tr>
                     <tr style="font-size: small">
                         <td style="font-size: x-small; height: 75px; width: 616px;">
                             <asp:TextBox ID="txtComentarios" runat="server" Height="80px" 
                                 TextMode="MultiLine" Width="645px" MaxLength="790"></asp:TextBox>
                         </td>
                     </tr>
                 </table>       
                <br />
      
                <asp:UpdatePanel ID="UpdatePanel" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: center; height: 31px; " colspan="2">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
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
                                        ValidationGroup="NewMix" />
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