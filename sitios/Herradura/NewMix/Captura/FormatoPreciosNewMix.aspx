<%@ Page Language="vb" 
    Culture="es-MX" 
    MasterPagefile="~/sitios/Herradura/NewMix/NewMix.Master" 
    AutoEventWireup="false" 
    CodeBehind="FormatoPreciosNewMix.aspx.vb" 
    Inherits="procomlcd.FormatoPreciosNewMix"
    Title="New Mix - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderNewMix" runat="Server">
  
    <!--CONTENT CONTAINER-->
<div id="titulo-pagina">
    Formato precios</div>
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
                        Cadena:</td>
                    <td style="text-align: left">
                        <asp:Label ID="lblcadena" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                
                </table>
                   <asp:Menu ID="Menu1" Width="99%" runat="server" Orientation="Horizontal" 
                        StaticEnableDefaultPopOutImage="False" 
                        OnMenuItemClick="Menu1_MenuItemClick">
                        <Items>
                            <asp:MenuItem ImageUrl="../../Img/selectedtabPp.GIF" Text=" " Value="0"></asp:MenuItem>
                            <asp:MenuItem ImageUrl="../../Img/unselectedtabPc.GIF" Text=" " Value="1"></asp:MenuItem>
                            <asp:MenuItem ImageUrl="../../Img/unselectedtabPt.GIF" Text=" " Value="2"></asp:MenuItem>
                            <asp:MenuItem ImageUrl="../../Img/unselectedtab.GIF" Text=" " Value="3"></asp:MenuItem>
                            <asp:MenuItem ImageUrl="../../Img/unselectedtab.GIF" Text=" " Value="4"></asp:MenuItem>
                        </Items>
                    </asp:Menu>
                        
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="Tab1" runat="server">
                        <asp:GridView ID="gridProductosCliente" runat="server" AutoGenerateColumns="False"
                            Caption="Productos propios" 
                            DataKeyNames="id_producto" Width="100%" CssClass="grid-view" 
                            CaptionAlign="Top" BackColor="LightGoldenrodYellow" BorderColor="Tan" 
                            BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None">
                            <Columns>                            
                                <asp:BoundField HeaderText="Familia" DataField="id_familia" />
                                <asp:BoundField HeaderText="Producto" DataField="nombre_producto" />
                                <asp:BoundField HeaderText="Presentación" DataField="presentacion" />
                                <asp:BoundField HeaderText="Código" DataField="codigo" />  
                                <asp:TemplateField HeaderText="Precio">
                                    <ItemTemplate>
                                        $ <asp:TextBox ID="txtPrecio" runat="server" MaxLength="6" Width="45" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "precio")%>'></asp:TextBox>
                                        <asp:RangeValidator ID="rvaPrecio" runat="server" 
                                            ControlToValidate="txtPrecio" Display="Dynamic" 
                                            ErrorMessage="El precio del producto indicado con un (*) de los productos de Herradura"
                                            MaximumValue="1999" MinimumValue="0" Type="Double" ValidationGroup="NewMix">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" 
                                            ControlToValidate="txtPrecio" 
                                            ErrorMessage="Completa el precio del producto indicado con un (*) de los productos de Herradura" 
                                            ValidationGroup="NewMix">*</asp:RequiredFieldValidator> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        <FooterStyle CssClass="grid-footer" BackColor="Tan" />
                            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
                                HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                            <HeaderStyle BackColor="Tan" Font-Bold="True" />
                            <AlternatingRowStyle BackColor="PaleGoldenrod" />
                    </asp:GridView>
                    </asp:View>
                    <asp:View ID="Tab2" runat="server">
                        <asp:GridView ID="gridProductosCompetencia" runat="server" AutoGenerateColumns="False" 
                            Caption="Productos competencia" CaptionAlign="Top" CssClass="grid-view" 
                            DataKeyNames="id_producto" ShowFooter="True" style="text-align: center" 
                            Width="99%" BackColor="LightGoldenrodYellow" BorderColor="Tan" 
                            BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None">
                            <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                <asp:BoundField DataField="codigo" HeaderText="Código" />
                                <asp:TemplateField HeaderText="Precios">
                                    <ItemTemplate>
                                        $ <asp:TextBox ID="txtPrecio" runat="server" MaxLength="6" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "precio")%>' Width="45"></asp:TextBox>
                                        <asp:RangeValidator ID="rvaPrecio" runat="server" 
                                            ControlToValidate="txtPrecio" Display="Dynamic" 
                                            ErrorMessage="El precio del producto indicado con un (*) de los productos de la competencia"
                                            MaximumValue="1999" MinimumValue="0" Type="Double" ValidationGroup="NewMix">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" 
                                            ControlToValidate="txtPrecio" 
                                            ErrorMessage="Completa el precio del producto indicado con un (*) de los productos de la competencia" 
                                            ValidationGroup="NewMix">*</asp:RequiredFieldValidator> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grid-footer" BackColor="Tan" />
                            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
                                HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                            <HeaderStyle BackColor="Tan" Font-Bold="True" />
                            <EditRowStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="PaleGoldenrod" />
                        </asp:GridView>
                    </asp:View>
                    <asp:View ID="Tab3" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <span style="text-align: left; background-color: #FFCC00">*Por favor captura 
                                    solo los precios de los productos que tengan precios diferentes en comparacion 
                                    de la misma cadena.<br />
                                    </span>
                                </td>
                            </tr>
                        </table>
                            
                        <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both" Height="500px">    
                        <asp:GridView ID="gridTiendas" runat="server" AutoGenerateColumns="False" 
                            Caption="Tiendas" CaptionAlign="Top" CssClass="grid-view" 
                            DataKeyNames="id_tienda" ShowFooter="True" style="text-align: center" 
                            Width="99%" BackColor="LightGoldenrodYellow" BorderColor="Tan" 
                            BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None">
                            <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundField DataField="nombre" HeaderText="Tienda" />
                                <asp:BoundField DataField="no_tienda" HeaderText="No de tienda" />
                                <asp:BoundField DataField="ciudad" HeaderText="Ciudad" />
                                <asp:TemplateField HeaderText="JACK DANIEL´S 700">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPrecio100" runat="server" MaxLength="6" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "100")%>' Width="45"></asp:TextBox>
                                        <asp:RangeValidator ID="rvaPrecio100" runat="server" 
                                            ControlToValidate="txtPrecio100" Display="Dynamic" 
                                            ErrorMessage="El precio 'JACK DANIEL´S 700' indicado con un (*) en precios por tienda supera al máximo permitido"
                                            MaximumValue="1999" MinimumValue="0" Type="Double" ValidationGroup="NewMix">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPrecio100" runat="server" 
                                            ControlToValidate="txtPrecio100" 
                                            ErrorMessage="Completa el precio de 'JACK DANIEL´S 700' indicado con un (*) de los productos de Herradura" 
                                            ValidationGroup="NewMix">*</asp:RequiredFieldValidator> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="JACK DANIEL´S GINGER 350">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPrecio101" runat="server" MaxLength="6" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "101")%>' Width="45"></asp:TextBox>
                                        <asp:RangeValidator ID="rvaPrecio101" runat="server" 
                                            ControlToValidate="txtPrecio101" Display="Dynamic" 
                                            ErrorMessage="El precio 'JACK DANIEL´S GINGER 350' indicado con un (*) en precios por tienda supera al máximo permitido"
                                            MaximumValue="999" MinimumValue="0" Type="Double" ValidationGroup="NewMix">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPrecio101" runat="server" 
                                            ControlToValidate="txtPrecio101" 
                                            ErrorMessage="Completa el precio de 'JACK DANIEL´S GINGER 350' indicado con un (*) de los productos de Herradura" 
                                            ValidationGroup="NewMix">*</asp:RequiredFieldValidator> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="JACK DANIEL´S COLA 350">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPrecio102" runat="server" MaxLength="6" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "102")%>' Width="45"></asp:TextBox>
                                        <asp:RangeValidator ID="rvaPrecio102" runat="server" 
                                            ControlToValidate="txtPrecio102" Display="Dynamic" 
                                            ErrorMessage="El precio 'JACK DANIEL´S COLA 350' indicado con un (*) en precios por tienda supera al máximo permitido"
                                            MaximumValue="999" MinimumValue="0" Type="Double" ValidationGroup="NewMix">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPrecio102" runat="server" 
                                            ControlToValidate="txtPrecio102" 
                                            ErrorMessage="Completa el precio de 'JACK DANIEL´S COLA 350' indicado con un (*) de los productos de Herradura" 
                                            ValidationGroup="NewMix">*</asp:RequiredFieldValidator> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="JACK DANIEL´S AGUA MINERAL">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPrecio134" runat="server" MaxLength="6" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "134")%>' Width="45"></asp:TextBox>
                                        <asp:RangeValidator ID="rvaPrecio134" runat="server" 
                                            ControlToValidate="txtPrecio134" Display="Dynamic" 
                                            ErrorMessage="El precio 'JACK DANIEL´S AGUA MINERAL' indicado con un (*) en precios por tienda supera al máximo permitido"
                                            MaximumValue="999" MinimumValue="0" Type="Double" ValidationGroup="NewMix">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPrecio134" runat="server" 
                                            ControlToValidate="txtPrecio134" 
                                            ErrorMessage="Completa el precio de 'JACK DANIEL´S AGUA MINERAL' indicado con un (*) de los productos de Herradura" 
                                            ValidationGroup="NewMix">*</asp:RequiredFieldValidator> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="REPOSADO 950">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPrecio103" runat="server" MaxLength="6" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "103")%>' Width="45"></asp:TextBox>
                                        <asp:RangeValidator ID="rvaPrecio103" runat="server" 
                                            ControlToValidate="txtPrecio103" Display="Dynamic" 
                                            ErrorMessage="El precio 'REPOSADO 950' indicado con un (*) en precios por tienda supera al máximo permitido"
                                            MaximumValue="999" MinimumValue="0" Type="Double" ValidationGroup="NewMix">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPrecio103" runat="server" 
                                            ControlToValidate="txtPrecio103" 
                                            ErrorMessage="Completa el precio de 'REPOSADO 950' indicado con un (*) de los productos de Herradura" 
                                            ValidationGroup="NewMix">*</asp:RequiredFieldValidator> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="REPOSADO 700">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPrecio104" runat="server" MaxLength="6" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "104")%>' Width="45"></asp:TextBox>
                                        <asp:RangeValidator ID="rvaPrecio104" runat="server" 
                                            ControlToValidate="txtPrecio104" Display="Dynamic" 
                                            ErrorMessage="El precio 'REPOSADO 700' indicado con un (*) en precios por tienda supera al máximo permitido"
                                            MaximumValue="999" MinimumValue="0" Type="Double" ValidationGroup="NewMix">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPrecio104" runat="server" 
                                            ControlToValidate="txtPrecio104" 
                                            ErrorMessage="Completa el precio de 'REPOSADO 700' indicado con un (*) de los productos de Herradura" 
                                            ValidationGroup="NewMix">*</asp:RequiredFieldValidator> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="REPOSADO 375">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPrecio105" runat="server" MaxLength="6" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "105")%>' Width="45"></asp:TextBox>
                                        <asp:RangeValidator ID="rvaPrecio105" runat="server" 
                                            ControlToValidate="txtPrecio105" Display="Dynamic" 
                                            ErrorMessage="El precio 'REPOSADO 375' indicado con un (*) en precios por tienda supera al máximo permitido"
                                            MaximumValue="999" MinimumValue="0" Type="Double" ValidationGroup="NewMix">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPrecio105" runat="server" 
                                            ControlToValidate="txtPrecio105" 
                                            ErrorMessage="Completa el precio de 'REPOSADO 375' indicado con un (*) de los productos de Herradura" 
                                            ValidationGroup="NewMix">*</asp:RequiredFieldValidator> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="REPOSADO 200">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPrecio106" runat="server" MaxLength="6" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "106")%>' Width="45"></asp:TextBox>
                                        <asp:RangeValidator ID="rvaPrecio106" runat="server" 
                                            ControlToValidate="txtPrecio106" Display="Dynamic" 
                                            ErrorMessage="El precio 'REPOSADO 200' indicado con un (*) en precios por tienda supera al máximo permitido"
                                            MaximumValue="999" MinimumValue="0" Type="Double" ValidationGroup="NewMix">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPrecio106" runat="server" 
                                            ControlToValidate="txtPrecio106" 
                                            ErrorMessage="Completa el precio de 'REPOSADO 200' indicado con un (*) de los productos de Herradura" 
                                            ValidationGroup="NewMix">*</asp:RequiredFieldValidator> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="NEW MIX PALOMA 350">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPrecio108" runat="server" MaxLength="6" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "108")%>' Width="45"></asp:TextBox>
                                        <asp:RangeValidator ID="rvaPrecio108" runat="server" 
                                            ControlToValidate="txtPrecio108" Display="Dynamic" 
                                            ErrorMessage="El precio 'NEW MIX PALOMA 350' indicado con un (*) en precios por tienda supera al máximo permitido"
                                            MaximumValue="999" MinimumValue="0" Type="Double" ValidationGroup="NewMix">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPrecio108" runat="server" 
                                            ControlToValidate="txtPrecio108" 
                                            ErrorMessage="Completa el precio de 'NEW MIX PALOMA 350' indicado con un (*) de los productos de Herradura" 
                                            ValidationGroup="NewMix">*</asp:RequiredFieldValidator> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="NEW MIX VAMPIRO 350">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPrecio109" runat="server" MaxLength="6" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "109")%>' Width="45"></asp:TextBox>
                                        <asp:RangeValidator ID="rvaPrecio109" runat="server" 
                                            ControlToValidate="txtPrecio109" Display="Dynamic" 
                                            ErrorMessage="El precio 'NEW MIX VAMPIRO 350' indicado con un (*) en precios por tienda supera al máximo permitido"
                                            MaximumValue="999" MinimumValue="0" Type="Double" ValidationGroup="NewMix">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPrecio109" runat="server" 
                                            ControlToValidate="txtPrecio109" 
                                            ErrorMessage="Completa el precio de 'NEW MIX VAMPIRO 350' indicado con un (*) de los productos de Herradura" 
                                            ValidationGroup="NewMix">*</asp:RequiredFieldValidator> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="NEW MIX CHARRO NEGRO 350">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPrecio110" runat="server" MaxLength="6" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "110")%>' Width="45"></asp:TextBox>
                                        <asp:RangeValidator ID="rvaPrecio110" runat="server" 
                                            ControlToValidate="txtPrecio110" Display="Dynamic" 
                                            ErrorMessage="El precio 'NEW MIX CHARRO NEGRO 350' indicado con un (*) en precios por tienda supera al máximo permitido"
                                            MaximumValue="999" MinimumValue="0" Type="Double" ValidationGroup="NewMix">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPrecio110" runat="server" 
                                            ControlToValidate="txtPrecio110" 
                                            ErrorMessage="Completa el precio de 'NEW MIX CHARRO NEGRO 350' indicado con un (*) de los productos de Herradura" 
                                            ValidationGroup="NewMix">*</asp:RequiredFieldValidator> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="NEW MIX MARGARITA 350">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPrecio111" runat="server" MaxLength="6" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "111")%>' Width="45"></asp:TextBox>
                                        <asp:RangeValidator ID="rvaPrecio111" runat="server" 
                                            ControlToValidate="txtPrecio111" Display="Dynamic" 
                                            ErrorMessage="El precio 'NEW MIX MARGARITA 350' indicado con un (*) en precios por tienda supera al máximo permitido"
                                            MaximumValue="999" MinimumValue="0" Type="Double" ValidationGroup="NewMix">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPrecio111" runat="server" 
                                            ControlToValidate="txtPrecio111" 
                                            ErrorMessage="Completa el precio de 'NEW MIX MARGARITA 350' indicado con un (*) de los productos de Herradura" 
                                            ValidationGroup="NewMix">*</asp:RequiredFieldValidator> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="NEW MIX SPICY MANGO 350">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPrecio112" runat="server" MaxLength="6" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "112")%>' Width="45"></asp:TextBox>
                                        <asp:RangeValidator ID="rvaPrecio112" runat="server" 
                                            ControlToValidate="txtPrecio112" Display="Dynamic" 
                                            ErrorMessage="El precio 'NEW MIX SPICY MANGO 350' indicado con un (*) en precios por tienda supera al máximo permitido"
                                            MaximumValue="999" MinimumValue="0" Type="Double" ValidationGroup="NewMix">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPrecio112" runat="server" 
                                            ControlToValidate="txtPrecio112" 
                                            ErrorMessage="Completa el precio de 'NEW MIX SPICY MANGO 350' indicado con un (*) de los productos de Herradura" 
                                            ValidationGroup="NewMix">*</asp:RequiredFieldValidator> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="NEW MIX PALOMA 2000">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPrecio113" runat="server" MaxLength="6" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "113")%>' Width="45"></asp:TextBox>
                                        <asp:RangeValidator ID="rvaPrecio113" runat="server" 
                                            ControlToValidate="txtPrecio113" Display="Dynamic" 
                                            ErrorMessage="El precio 'NEW MIX PALOMA 2000' indicado con un (*) en precios por tienda supera al máximo permitido"
                                            MaximumValue="999" MinimumValue="0" Type="Double" ValidationGroup="NewMix">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPrecio113" runat="server" 
                                            ControlToValidate="txtPrecio113" 
                                            ErrorMessage="Completa el precio de 'NEW MIX PALOMA 2000' indicado con un (*) de los productos de Herradura" 
                                            ValidationGroup="NewMix">*</asp:RequiredFieldValidator> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="FINLANDIA FROST LIMÓN">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPrecio132" runat="server" MaxLength="6" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "132")%>' Width="45"></asp:TextBox>
                                        <asp:RangeValidator ID="rvaPrecio132" runat="server" 
                                            ControlToValidate="txtPrecio132" Display="Dynamic" 
                                            ErrorMessage="El precio 'FINLANDIA FROST LIMÓN' indicado con un (*) en precios por tienda supera al máximo permitido"
                                            MaximumValue="999" MinimumValue="0" Type="Double" ValidationGroup="NewMix">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPrecio132" runat="server" 
                                            ControlToValidate="txtPrecio132" 
                                            ErrorMessage="Completa el precio de 'FINLANDIA FROST LIMÓN' indicado con un (*) de los productos de Herradura" 
                                            ValidationGroup="NewMix">*</asp:RequiredFieldValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 
                                 <asp:TemplateField HeaderText="FINLANDIA FROST ARANDANO">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPrecio133" runat="server" MaxLength="6" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "133")%>' Width="45"></asp:TextBox>
                                        <asp:RangeValidator ID="rvaPrecio133" runat="server" 
                                            ControlToValidate="txtPrecio133" Display="Dynamic" 
                                            ErrorMessage="El precio 'FINLANDIA FROST ARANDANO' indicado con un (*) en precios por tienda supera al máximo permitido"
                                            MaximumValue="999" MinimumValue="0" Type="Double" ValidationGroup="NewMix">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPrecio133" runat="server" 
                                            ControlToValidate="txtPrecio133" 
                                            ErrorMessage="Completa el precio de 'FINLANDIA FROST ARANDANO' indicado con un (*) de los productos de Herradura" 
                                            ValidationGroup="NewMix">*</asp:RequiredFieldValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="FINLANDIA FROST MANGO">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPrecio135" runat="server" MaxLength="6" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "135")%>' Width="45"></asp:TextBox>
                                        <asp:RangeValidator ID="rvaPrecio135" runat="server" 
                                            ControlToValidate="txtPrecio135" Display="Dynamic" 
                                            ErrorMessage="El precio 'FINLANDIA FROST MANGO' indicado con un (*) en precios por tienda supera al máximo permitido"
                                            MaximumValue="999" MinimumValue="0" Type="Double" ValidationGroup="NewMix">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPrecio135" runat="server" 
                                            ControlToValidate="txtPrecio135" 
                                            ErrorMessage="Completa el precio de 'FINLANDIA FROST MANGO' indicado con un (*) de los productos de Herradura" 
                                            ValidationGroup="NewMix">*</asp:RequiredFieldValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="VINO SANTA ELENA">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPrecio136" runat="server" MaxLength="6" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "136")%>' Width="45"></asp:TextBox>
                                        <asp:RangeValidator ID="rvaPrecio136" runat="server" 
                                            ControlToValidate="txtPrecio136" Display="Dynamic" 
                                            ErrorMessage="El precio 'VINO SANTA ELENA' indicado con un (*) en precios por tienda supera al máximo permitido"
                                            MaximumValue="999" MinimumValue="0" Type="Double" ValidationGroup="NewMix">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPrecio136" runat="server" 
                                            ControlToValidate="txtPrecio136" 
                                            ErrorMessage="Completa el precio de 'VINO SANTA ELENA' indicado con un (*) de los productos de Herradura" 
                                            ValidationGroup="NewMix">*</asp:RequiredFieldValidator> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="RON APPLETON AÑEJO">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPrecio137" runat="server" MaxLength="6" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "137")%>' Width="45"></asp:TextBox>
                                        <asp:RangeValidator ID="rvaPrecio137" runat="server" 
                                            ControlToValidate="txtPrecio137" Display="Dynamic" 
                                            ErrorMessage="El precio 'JRON APPLETON AÑEJO' indicado con un (*) en precios por tienda supera al máximo permitido"
                                            MaximumValue="999" MinimumValue="0" Type="Double" ValidationGroup="NewMix">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPrecio137" runat="server" 
                                            ControlToValidate="txtPrecio137" 
                                            ErrorMessage="Completa el precio de 'RON APPLETON AÑEJO' indicado con un (*) de los productos de Herradura" 
                                            ValidationGroup="NewMix">*</asp:RequiredFieldValidator> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="RON APPLETON BLANCO">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPrecio138" runat="server" MaxLength="6" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "138")%>' Width="45"></asp:TextBox>
                                        <asp:RangeValidator ID="rvaPrecio138" runat="server" 
                                            ControlToValidate="txtPrecio138" Display="Dynamic" 
                                            ErrorMessage="El precio 'RON APPLETON BLANCO' indicado con un (*) en precios por tienda supera al máximo permitido"
                                            MaximumValue="999" MinimumValue="0" Type="Double" ValidationGroup="NewMix">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPrecio138" runat="server" 
                                            ControlToValidate="txtPrecio138" 
                                            ErrorMessage="Completa el precio de 'RON APPLETON BLANCO' indicado con un (*) de los productos de Herradura" 
                                            ValidationGroup="NewMix">*</asp:RequiredFieldValidator> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grid-footer" BackColor="Tan" />
                            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
                                HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                            <HeaderStyle BackColor="Tan" Font-Bold="True" />
                            <EditRowStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="PaleGoldenrod" />
                        </asp:GridView>
                        </asp:Panel>
                    </asp:View>
                </asp:MultiView>
                <br />
                <table style="border-style: groove; width: 100%; height: 99px; margin-right: 0px">
                     <tr style="color: #FFFFFF">
                         <td bgcolor="#507CD1" 
                             style="text-align: center; background-color: #000000; height: 29px; width: 616px;">
                             Comentarios:</td>
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