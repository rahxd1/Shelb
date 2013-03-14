<%@ Page Language="vb" 
    Culture="es-MX" 
    MasterPageFile="~/sitios/Jovy/Jovy.Master" 
    AutoEventWireup="false" 
    CodeBehind="FormatoCapturaJovy.aspx.vb" 
    Inherits="procomlcd.FormatoCapturaJovy"
    title="Jovy - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderJovy" runat="Server">
    <!--PAGE TITLE-->
<div id="titulo-pagina">Formato captura información</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha2">

<!--CONTENT MAIN COLUMN-->
        <div id="content-main-column">
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
                            <tr><td style="text-align: left; width: 50px;">
                                Tienda</td>
                            <td style="text-align: left;">
                                <asp:DropDownList ID="cmbTienda" runat="server" 
                                       style="margin-left: 0px" Height="21px" Width="325px"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvTienda" runat="server" 
                                    ControlToValidate="cmbTienda" ErrorMessage="Selecciona la tienda" ValidationGroup="Jovy">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 50px;">
                                    &nbsp;</td>
                                <td style="text-align: center;">
                                    <br />
                                    <asp:Label ID="lblAdvertencia" runat="server" BackColor="Yellow" 
                                        Font-Bold="True" Text="Ahora puedes guardar y continuar en la pagina actual. Puedes dar click en 'Guardar', para ir salvando tu información y no te regresara al menú de rutas. Para regresar al menú de rutas da click en 'Regresar' "></asp:Label>
                                    <br />
                                </td>
                            </tr>
                        </table>
                        
                        <asp:Menu ID="Menu1" Width="60%" runat="server" Orientation="Horizontal" 
                            StaticEnableDefaultPopOutImage="False" 
                            OnMenuItemClick="Menu1_MenuItemClick">
                            <Items>
                                <asp:MenuItem ImageUrl="~/sitios/Jovy/Img/selectedtabPol.GIF" Text=" " Value="0"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="~/sitios/Jovy/Img/unselectedtabPul.GIF" Text=" " Value="1"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="~/sitios/Jovy/Img/unselectedtabCar.GIF" Text=" " Value="2"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="~/sitios/Jovy/Img/unselectedtabPal.GIF" Text=" " Value="3"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="~/sitios/Jovy/Img/unselectedtabRF.GIF" Text=" " Value="4"></asp:MenuItem>
                            </Items>
                        </asp:Menu>
                                                
                            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                <asp:View ID="Tab1" runat="server">
                                    <asp:GridView ID="gridProductos1" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="id_producto" Width="100%" ShowFooter="True" 
                                    GridLines="Horizontal" Caption="Polvos" CssClass="grid-view-ruta">
                                    <Columns>
                                        <asp:BoundField HeaderText="Código" DataField="codigo" HeaderStyle-Font-Size="X-Small" />
                                        <asp:BoundField HeaderText="Producto" DataField="nombre_producto" HeaderStyle-Font-Size="X-Small" />
                                        <asp:BoundField HeaderText="Piezas" DataField="bolsa" HeaderStyle-Font-Size="X-Small" />
                                        <asp:BoundField HeaderText="Marca" DataField="nombre_marca" HeaderStyle-Font-Size="X-Small" />
                                        <asp:TemplateField HeaderText="Fal- tantes" HeaderStyle-Font-Size="X-Small">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkFaltantes" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Faltante")) %>' />
                                            </ItemTemplate><ItemStyle Width="35px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Precio" HeaderStyle-Font-Size="X-Small" ItemStyle-VerticalAlign="Top" >
                                            <ItemTemplate>
                                                <asp:RangeValidator id="rvaPrecio" runat="server" 
                                                    ErrorMessage="El precio del producto indicado en 'Polvos' es muy alto" Display="Dynamic"
                                                    MinimumValue="0" MaximumValue="300" Type="Double" 
                                                    ControlToValidate="txtPrecio" ValidationGroup="Jovy">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" 
                                                    ControlToValidate="txtPrecio" 
                                                    ErrorMessage="Completa el precio del producto indicado en 'Polvos'" 
                                                    ValidationGroup="Jovy">*</asp:RequiredFieldValidator>    
                                                                                                        
                                                $ <asp:TextBox ID="txtPrecio" runat="server" Width="40" MaxLength="6" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "precio") %>'></asp:TextBox>
                                            </ItemTemplate><ItemStyle Width="75px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="inv. piso" HeaderStyle-Font-Size="X-Small" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:RangeValidator id="rvaInventarios" runat="server" 
                                                    ErrorMessage="El inventario de piso del producto indicado en 'Polvos' es muy alto" Display="Dynamic"
                                                    MinimumValue="0" MaximumValue="999" Type="Double" ControlToValidate="txtInventarios" 
                                                    ValidationGroup="Jovy">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvInventarios" runat="server" 
                                                    ControlToValidate="txtInventarios" 
                                                    ErrorMessage="Completa inventarios del producto indicado en 'Polvos'" 
                                                    ValidationGroup="Jovy">*</asp:RequiredFieldValidator>  
                                                    
                                                <asp:TextBox ID="txtInventarios" runat="server" Width="40" MaxLength="3" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Inventarios") %>'></asp:TextBox>
                                                <asp:Label ID="lblbolsas1" runat="server" Text="bolsas"></asp:Label>
                                            </ItemTemplate><ItemStyle Width="95px" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="inv. bodega" HeaderStyle-Font-Size="X-Small" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:RangeValidator id="rvaInventarios_bodega" runat="server" 
                                                    ErrorMessage="El inventario en bodega del producto indicado en 'Polvos' es muy alto" Display="Dynamic"
                                                    MinimumValue="0" MaximumValue="999" Type="Double" 
                                                    ControlToValidate="txtInventarios_bodega" ValidationGroup="Jovy">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvInventarios_bodega" runat="server" 
                                                    ControlToValidate="txtInventarios_bodega" 
                                                    ErrorMessage="Completa inventarios de bodega del producto indicado en 'Polvos'" 
                                                    ValidationGroup="Jovy">*</asp:RequiredFieldValidator>  
                                                <asp:TextBox ID="txtInventarios_bodega" runat="server" Width="40" MaxLength="3" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Inventarios_bodega") %>'></asp:TextBox>
                                                <asp:Label ID="lblbolsas2" runat="server" Text="bolsas"></asp:Label>
                                            </ItemTemplate><ItemStyle Width="95px" />
                                        </asp:TemplateField>
                                                                            
                                        <asp:TemplateField HeaderText="Caducidad  (dd/mm/aaaa)" HeaderStyle-Font-Size="X-Small" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:RegularExpressionValidator id="rvaCaducidad" runat="server" 
                                                    ErrorMessage="La fecha en el producto indicado en 'Polvos' no tiene el formato 'dd/mm/aaaa'" 
                                                    Display="Dynamic" 
                                                    ValidationExpression="\d{2}/\d{2}/\d{4}" ControlToValidate="txtCaducidad" 
                                                    ValidationGroup="Jovy" >*</asp:RegularExpressionValidator>
                                                <asp:RangeValidator ID="rvCaducidad" runat="server" 
                                                    ErrorMessage="La fecha en el producto indicado en 'Polvos' no es valida" 
                                                    ValidationGroup="Jovy" ControlToValidate="txtCaducidad"
                                                    MaximumValue="31/12/3000" MinimumValue="01/01/2010" Type="Date">*</asp:RangeValidator>
                                                <asp:TextBox ID="txtCaducidad" runat="server" Width="70" MaxLength="10" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "Caducidad","{0:d}")%>' ></asp:TextBox>
                                            </ItemTemplate><ItemStyle Width="95px" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Cantidad caducada" HeaderStyle-Font-Size="X-Small" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:RangeValidator id="rvacantidad_caducada" runat="server" 
                                                    ErrorMessage="La cantidad caducada del producto indicado en 'Polvos' es muy alta" Display="Dynamic"
                                                    MinimumValue="0" MaximumValue="999" Type="Double" 
                                                    ControlToValidate="txtcantidad_caducada" ValidationGroup="Jovy">*</asp:RangeValidator>
                                                <asp:TextBox ID="txtcantidad_caducada" runat="server" Width="40" MaxLength="3" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "cantidad_caducada") %>'></asp:TextBox>
                                                <asp:Label ID="lblbolsas3" runat="server" Text="bolsas"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="95px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                </asp:GridView>
                                    <br />
                                </asp:View>
                                
                                <asp:View ID="Tab2" runat="server">
                                    <asp:GridView ID="gridProductos2" runat="server" AutoGenerateColumns="False" 
                                    GridLines="Horizontal" Caption="Pulpas" CssClass="grid-view-ruta" DataKeyNames="id_producto" 
                                    ShowFooter="True" Width="100%">
                                    <Columns>
                                        <asp:BoundField HeaderText="Código" DataField="codigo" HeaderStyle-Font-Size="X-Small" />
                                        <asp:BoundField HeaderText="Producto" DataField="nombre_producto" HeaderStyle-Font-Size="X-Small" />
                                        <asp:BoundField HeaderText="Piezas" DataField="bolsa" HeaderStyle-Font-Size="X-Small" />
                                        <asp:BoundField HeaderText="Marca" DataField="nombre_marca" HeaderStyle-Font-Size="X-Small" />
                                        <asp:TemplateField HeaderText="Fal- tantes" HeaderStyle-Font-Size="X-Small">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkFaltantes" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Faltante")) %>' />
                                            </ItemTemplate><ItemStyle Width="35px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Precio" HeaderStyle-Font-Size="X-Small" ItemStyle-VerticalAlign="Top" >
                                            <ItemTemplate>
                                                <asp:RangeValidator id="rvaPrecio" runat="server" 
                                                    ErrorMessage="El precio del producto indicado en 'Pulpas' es muy alto" Display="Dynamic"
                                                    MinimumValue="0" MaximumValue="300" Type="Double" 
                                                    ControlToValidate="txtPrecio" ValidationGroup="Jovy">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" 
                                                    ControlToValidate="txtPrecio" 
                                                    ErrorMessage="Completa el precio del producto indicado en 'Pulpas'" 
                                                    ValidationGroup="Jovy">*</asp:RequiredFieldValidator>    
                                                                                                        
                                                $ <asp:TextBox ID="txtPrecio" runat="server" Width="40" MaxLength="6" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "precio") %>'></asp:TextBox>
                                            </ItemTemplate><ItemStyle Width="75px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="inv. piso" HeaderStyle-Font-Size="X-Small" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:RangeValidator id="rvaInventarios" runat="server" 
                                                    ErrorMessage="El inventario de piso del producto indicado en 'Pulpas' es muy alto" Display="Dynamic"
                                                    MinimumValue="0" MaximumValue="999" Type="Double" ControlToValidate="txtInventarios" 
                                                    ValidationGroup="Jovy">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvInventarios" runat="server" 
                                                    ControlToValidate="txtInventarios" 
                                                    ErrorMessage="Completa inventarios del producto indicado en 'Pulpas'" 
                                                    ValidationGroup="Jovy">*</asp:RequiredFieldValidator>  
                                                    
                                                <asp:TextBox ID="txtInventarios" runat="server" Width="40" MaxLength="3" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Inventarios") %>'></asp:TextBox>
                                                <asp:Label ID="lblbolsas1" runat="server" Text="bolsas"></asp:Label>
                                            </ItemTemplate><ItemStyle Width="95px" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="inv. bodega" HeaderStyle-Font-Size="X-Small" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:RangeValidator id="rvaInventarios_bodega" runat="server" 
                                                    ErrorMessage="El inventario en bodega del producto indicado en 'Pulpas' es muy alto" Display="Dynamic"
                                                    MinimumValue="0" MaximumValue="999" Type="Double" 
                                                    ControlToValidate="txtInventarios_bodega" ValidationGroup="Jovy">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvInventarios_bodega" runat="server" 
                                                    ControlToValidate="txtInventarios_bodega" 
                                                    ErrorMessage="Completa inventarios de bodega del producto indicado en 'Pulpas'" 
                                                    ValidationGroup="Jovy">*</asp:RequiredFieldValidator>  
                                                <asp:TextBox ID="txtInventarios_bodega" runat="server" Width="40" MaxLength="3" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Inventarios_bodega") %>'></asp:TextBox>
                                                <asp:Label ID="lblbolsas2" runat="server" Text="bolsas"></asp:Label>
                                            </ItemTemplate><ItemStyle Width="95px" />
                                        </asp:TemplateField>
                                                                            
                                        <asp:TemplateField HeaderText="Caducidad  (dd/mm/aaaa)" HeaderStyle-Font-Size="X-Small" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:RegularExpressionValidator id="rvaCaducidad" runat="server" 
                                                    ErrorMessage="La fecha en el producto indicado en 'Pulpas' no tiene el formato 'dd/mm/aaaa'" 
                                                    Display="Dynamic" 
                                                    ValidationExpression="\d{2}/\d{2}/\d{4}" ControlToValidate="txtCaducidad" 
                                                    ValidationGroup="Jovy" >*</asp:RegularExpressionValidator>
                                                <asp:RangeValidator ID="rvCaducidad" runat="server" 
                                                    ErrorMessage="La fecha en el producto indicado en 'Pulpas' no es valida" 
                                                    ValidationGroup="Jovy" ControlToValidate="txtCaducidad"
                                                    MaximumValue="31/12/3000" MinimumValue="01/01/2010" Type="Date">*</asp:RangeValidator>
                                                <asp:TextBox ID="txtCaducidad" runat="server" Width="70" MaxLength="10" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "Caducidad","{0:d}")%>' ></asp:TextBox>
                                            </ItemTemplate><ItemStyle Width="95px" />
                                        </asp:TemplateField>
                                                                                
                                        <asp:TemplateField HeaderText="Cantidad caducada" HeaderStyle-Font-Size="X-Small" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:RangeValidator id="rvacantidad_caducada" runat="server" 
                                                    ErrorMessage="La cantidad caducada del producto indicado en 'Pulpas' es muy alta" Display="Dynamic"
                                                    MinimumValue="0" MaximumValue="999" Type="Double" 
                                                    ControlToValidate="txtcantidad_caducada" ValidationGroup="Jovy">*</asp:RangeValidator>
                                                <asp:TextBox ID="txtcantidad_caducada" runat="server" Width="40" MaxLength="3" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "cantidad_caducada") %>'></asp:TextBox>
                                                <asp:Label ID="lblbolsas3" runat="server" Text="bolsas"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="95px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                </asp:GridView>
                                    <br />
                                </asp:View>
                                <asp:View ID="Tab3" runat="server"> 
                                    <asp:GridView ID="gridProductos3" runat="server" AutoGenerateColumns="False" 
                                    GridLines="Horizontal" Caption="Caramelos" CssClass="grid-view-ruta" DataKeyNames="id_producto" 
                                    ShowFooter="True" Width="100%">
                                    <Columns>
                                        <asp:BoundField HeaderText="Código" DataField="codigo" HeaderStyle-Font-Size="X-Small" />
                                        <asp:BoundField HeaderText="Producto" DataField="nombre_producto" HeaderStyle-Font-Size="X-Small" />
                                        <asp:BoundField HeaderText="Piezas" DataField="bolsa" HeaderStyle-Font-Size="X-Small" />
                                        <asp:BoundField HeaderText="Marca" DataField="nombre_marca" HeaderStyle-Font-Size="X-Small" />
                                        <asp:TemplateField HeaderText="Fal- tantes" HeaderStyle-Font-Size="X-Small">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkFaltantes" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Faltante")) %>' />
                                            </ItemTemplate><ItemStyle Width="35px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Precio" HeaderStyle-Font-Size="X-Small" ItemStyle-VerticalAlign="Top" >
                                            <ItemTemplate>
                                                <asp:RangeValidator id="rvaPrecio" runat="server" 
                                                    ErrorMessage="El precio del producto indicado en 'Caramelos' es muy alto" Display="Dynamic"
                                                    MinimumValue="0" MaximumValue="300" Type="Double" 
                                                    ControlToValidate="txtPrecio" ValidationGroup="Jovy">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" 
                                                    ControlToValidate="txtPrecio" 
                                                    ErrorMessage="Completa el precio del producto indicado en 'Caramelos'" 
                                                    ValidationGroup="Jovy">*</asp:RequiredFieldValidator>    
                                                                                                        
                                                $ <asp:TextBox ID="txtPrecio" runat="server" Width="40" MaxLength="6" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "precio") %>'></asp:TextBox>
                                            </ItemTemplate><ItemStyle Width="75px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="inv. piso" HeaderStyle-Font-Size="X-Small" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:RangeValidator id="rvaInventarios" runat="server" 
                                                    ErrorMessage="El inventario de piso del producto indicado en 'Caramelos' es muy alto" Display="Dynamic"
                                                    MinimumValue="0" MaximumValue="999" Type="Double" ControlToValidate="txtInventarios" 
                                                    ValidationGroup="Jovy">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvInventarios" runat="server" 
                                                    ControlToValidate="txtInventarios" 
                                                    ErrorMessage="Completa inventarios del producto indicado en 'Caramelos'" 
                                                    ValidationGroup="Jovy">*</asp:RequiredFieldValidator>  
                                                    
                                                <asp:TextBox ID="txtInventarios" runat="server" Width="40" MaxLength="3" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Inventarios") %>'></asp:TextBox>
                                                <asp:Label ID="lblbolsas1" runat="server" Text="bolsas"></asp:Label>
                                            </ItemTemplate><ItemStyle Width="95px" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="inv. bodega" HeaderStyle-Font-Size="X-Small" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:RangeValidator id="rvaInventarios_bodega" runat="server" 
                                                    ErrorMessage="El inventario en bodega del producto indicado en 'Caramelos' es muy alto" Display="Dynamic"
                                                    MinimumValue="0" MaximumValue="999" Type="Double" 
                                                    ControlToValidate="txtInventarios_bodega" ValidationGroup="Jovy">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvInventarios_bodega" runat="server" 
                                                    ControlToValidate="txtInventarios_bodega" 
                                                    ErrorMessage="Completa inventarios de bodega del producto indicado en 'Caramelos'" 
                                                    ValidationGroup="Jovy">*</asp:RequiredFieldValidator>  
                                                <asp:TextBox ID="txtInventarios_bodega" runat="server" Width="40" MaxLength="3" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Inventarios_bodega") %>'></asp:TextBox>
                                                <asp:Label ID="lblbolsas2" runat="server" Text="bolsas"></asp:Label>
                                            </ItemTemplate><ItemStyle Width="95px" />
                                        </asp:TemplateField>
                                                                            
                                        <asp:TemplateField HeaderText="Caducidad  (dd/mm/aaaa)" HeaderStyle-Font-Size="X-Small" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:RegularExpressionValidator id="rvaCaducidad" runat="server" 
                                                    ErrorMessage="La fecha en el producto indicado en 'Caramelos' no tiene el formato 'dd/mm/aaaa'" 
                                                    Display="Dynamic" 
                                                    ValidationExpression="\d{2}/\d{2}/\d{4}" ControlToValidate="txtCaducidad" 
                                                    ValidationGroup="Jovy" >*</asp:RegularExpressionValidator>
                                                <asp:RangeValidator ID="rvCaducidad" runat="server" 
                                                    ErrorMessage="La fecha en el producto indicado en 'Caramelos' no es valida" 
                                                    ValidationGroup="Jovy" ControlToValidate="txtCaducidad"
                                                    MaximumValue="31/12/3000" MinimumValue="01/01/2010" Type="Date">*</asp:RangeValidator>
                                                <asp:TextBox ID="txtCaducidad" runat="server" Width="70" MaxLength="10" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "Caducidad","{0:d}")%>' ></asp:TextBox>
                                            </ItemTemplate><ItemStyle Width="95px" />
                                        </asp:TemplateField>
                                                                                
                                        <asp:TemplateField HeaderText="Cantidad caducada" HeaderStyle-Font-Size="X-Small" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:RangeValidator id="rvacantidad_caducada" runat="server" 
                                                    ErrorMessage="La cantidad caducada del producto indicado en 'Caramelos' es muy alta" Display="Dynamic"
                                                    MinimumValue="0" MaximumValue="999" Type="Double" 
                                                    ControlToValidate="txtcantidad_caducada" ValidationGroup="Jovy">*</asp:RangeValidator>
                                                <asp:TextBox ID="txtcantidad_caducada" runat="server" Width="40" MaxLength="3" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "cantidad_caducada") %>'></asp:TextBox>
                                                <asp:Label ID="lblbolsas3" runat="server" Text="bolsas"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="95px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                </asp:GridView>
                                
                                    <br />
                                
                                </asp:View>
                                <asp:View ID="Tab4" runat="server">
                                    <asp:GridView ID="gridProductos4" runat="server" AutoGenerateColumns="False" 
                                    GridLines="Horizontal" Caption="Paletas" CssClass="grid-view-ruta" DataKeyNames="id_producto" 
                                    ShowFooter="True" Width="100%">
                                    <Columns>
                                        <asp:BoundField HeaderText="Código" DataField="codigo" HeaderStyle-Font-Size="X-Small" />
                                        <asp:BoundField HeaderText="Producto" DataField="nombre_producto" HeaderStyle-Font-Size="X-Small" />
                                        <asp:BoundField HeaderText="Piezas" DataField="bolsa" HeaderStyle-Font-Size="X-Small" />
                                        <asp:BoundField HeaderText="Marca" DataField="nombre_marca" HeaderStyle-Font-Size="X-Small" />
                                        <asp:TemplateField HeaderText="Fal- tantes" HeaderStyle-Font-Size="X-Small">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkFaltantes" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Faltante")) %>' />
                                            </ItemTemplate><ItemStyle Width="35px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Precio" HeaderStyle-Font-Size="X-Small" ItemStyle-VerticalAlign="Top" >
                                            <ItemTemplate>
                                                <asp:RangeValidator id="rvaPrecio" runat="server" 
                                                    ErrorMessage="El precio del producto indicado en 'Paletas' es muy alto" Display="Dynamic"
                                                    MinimumValue="0" MaximumValue="300" Type="Double" 
                                                    ControlToValidate="txtPrecio" ValidationGroup="Jovy">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" 
                                                    ControlToValidate="txtPrecio" 
                                                    ErrorMessage="Completa el precio del producto indicado en 'Paletas'" 
                                                    ValidationGroup="Jovy">*</asp:RequiredFieldValidator>    
                                                                                                        
                                                $ <asp:TextBox ID="txtPrecio" runat="server" Width="40" MaxLength="6" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "precio") %>'></asp:TextBox>
                                            </ItemTemplate><ItemStyle Width="75px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="inv. piso" HeaderStyle-Font-Size="X-Small" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:RangeValidator id="rvaInventarios" runat="server" 
                                                    ErrorMessage="El inventario de piso del producto indicado en 'Paletas' es muy alto" Display="Dynamic"
                                                    MinimumValue="0" MaximumValue="999" Type="Double" ControlToValidate="txtInventarios" 
                                                    ValidationGroup="Jovy">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvInventarios" runat="server" 
                                                    ControlToValidate="txtInventarios" 
                                                    ErrorMessage="Completa inventarios del producto indicado en 'Paletas'" 
                                                    ValidationGroup="Jovy">*</asp:RequiredFieldValidator>  
                                                    
                                                <asp:TextBox ID="txtInventarios" runat="server" Width="40" MaxLength="3" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Inventarios") %>'></asp:TextBox>
                                                <asp:Label ID="lblbolsas1" runat="server" Text="bolsas"></asp:Label>
                                            </ItemTemplate><ItemStyle Width="95px" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="inv. bodega" HeaderStyle-Font-Size="X-Small" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:RangeValidator id="rvaInventarios_bodega" runat="server" 
                                                    ErrorMessage="El inventario en bodega del producto indicado en 'Paletas' es muy alto" Display="Dynamic"
                                                    MinimumValue="0" MaximumValue="999" Type="Double" 
                                                    ControlToValidate="txtInventarios_bodega" ValidationGroup="Jovy">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvInventarios_bodega" runat="server" 
                                                    ControlToValidate="txtInventarios_bodega" 
                                                    ErrorMessage="Completa inventarios de bodega del producto indicado en 'Paletas'" 
                                                    ValidationGroup="Jovy">*</asp:RequiredFieldValidator>  
                                                <asp:TextBox ID="txtInventarios_bodega" runat="server" Width="40" MaxLength="3" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Inventarios_bodega") %>'></asp:TextBox>
                                                <asp:Label ID="lblbolsas2" runat="server" Text="bolsas"></asp:Label>
                                            </ItemTemplate><ItemStyle Width="95px" />
                                        </asp:TemplateField>
                                                                            
                                        <asp:TemplateField HeaderText="Caducidad  (dd/mm/aaaa)" HeaderStyle-Font-Size="X-Small" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:RegularExpressionValidator id="rvaCaducidad" runat="server" 
                                                    ErrorMessage="La fecha en el producto indicado en 'Paletas' no tiene el formato 'dd/mm/aaaa'" 
                                                    Display="Dynamic" 
                                                    ValidationExpression="\d{2}/\d{2}/\d{4}" ControlToValidate="txtCaducidad" 
                                                    ValidationGroup="Jovy" >*</asp:RegularExpressionValidator>
                                                <asp:RangeValidator ID="rvCaducidad" runat="server" 
                                                    ErrorMessage="La fecha en el producto indicado en 'Paletas' no es valida" 
                                                    ValidationGroup="Jovy" ControlToValidate="txtCaducidad"
                                                    MaximumValue="31/12/3000" MinimumValue="01/01/2010" Type="Date">*</asp:RangeValidator>
                                                <asp:TextBox ID="txtCaducidad" runat="server" Width="70" MaxLength="10" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "Caducidad","{0:d}")%>' ></asp:TextBox>
                                            </ItemTemplate><ItemStyle Width="95px" />
                                        </asp:TemplateField>
                                                                                
                                        <asp:TemplateField HeaderText="Cantidad caducada" HeaderStyle-Font-Size="X-Small" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:RangeValidator id="rvacantidad_caducada" runat="server" 
                                                    ErrorMessage="La cantidad caducada del producto indicado en 'Paletas' es muy alta" Display="Dynamic"
                                                    MinimumValue="0" MaximumValue="999" Type="Double" 
                                                    ControlToValidate="txtcantidad_caducada" ValidationGroup="Jovy">*</asp:RangeValidator>
                                                <asp:TextBox ID="txtcantidad_caducada" runat="server" Width="40" MaxLength="3" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "cantidad_caducada") %>'></asp:TextBox>
                                                <asp:Label ID="lblbolsas3" runat="server" Text="bolsas"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="95px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                </asp:GridView>
                                    <br />
                                </asp:View>
                                <asp:View ID="Tab5" runat="server">
                                    <asp:GridView ID="gridProductos5" runat="server" AutoGenerateColumns="False" 
                                    GridLines="Horizontal" Caption="Rollo de fruta natural" CssClass="grid-view-ruta" DataKeyNames="id_producto" 
                                    ShowFooter="True" Width="100%">
                                    <Columns>
                                        <asp:BoundField HeaderText="Código" DataField="codigo" HeaderStyle-Font-Size="X-Small" />
                                        <asp:BoundField HeaderText="Producto" DataField="nombre_producto" HeaderStyle-Font-Size="X-Small" />
                                        <asp:BoundField HeaderText="Piezas" DataField="bolsa" HeaderStyle-Font-Size="X-Small" />
                                        <asp:BoundField HeaderText="Marca" DataField="nombre_marca" HeaderStyle-Font-Size="X-Small" />
                                        <asp:TemplateField HeaderText="Fal- tantes" HeaderStyle-Font-Size="X-Small">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkFaltantes" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Faltante")) %>' />
                                            </ItemTemplate><ItemStyle Width="35px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Precio" HeaderStyle-Font-Size="X-Small" ItemStyle-VerticalAlign="Top" >
                                            <ItemTemplate>
                                                <asp:RangeValidator id="rvaPrecio" runat="server" 
                                                    ErrorMessage="El precio del producto indicado en 'Rollo de fruta natural' es muy alto" Display="Dynamic"
                                                    MinimumValue="0" MaximumValue="300" Type="Double" 
                                                    ControlToValidate="txtPrecio" ValidationGroup="Jovy">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" 
                                                    ControlToValidate="txtPrecio" 
                                                    ErrorMessage="Completa el precio del producto indicado en 'Rollo de fruta natural'" 
                                                    ValidationGroup="Jovy">*</asp:RequiredFieldValidator>    
                                                                                                        
                                                $ <asp:TextBox ID="txtPrecio" runat="server" Width="40" MaxLength="6" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "precio") %>'></asp:TextBox>
                                            </ItemTemplate><ItemStyle Width="75px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="inv. piso" HeaderStyle-Font-Size="X-Small" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:RangeValidator id="rvaInventarios" runat="server" 
                                                    ErrorMessage="El inventario de piso del producto indicado en 'Rollo de fruta natural' es muy alto" Display="Dynamic"
                                                    MinimumValue="0" MaximumValue="999" Type="Double" ControlToValidate="txtInventarios" 
                                                    ValidationGroup="Jovy">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvInventarios" runat="server" 
                                                    ControlToValidate="txtInventarios" 
                                                    ErrorMessage="Completa inventarios del producto indicado en 'Rollo de fruta natural'" 
                                                    ValidationGroup="Jovy">*</asp:RequiredFieldValidator>  
                                                    
                                                <asp:TextBox ID="txtInventarios" runat="server" Width="40" MaxLength="3" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Inventarios") %>'></asp:TextBox>
                                                <asp:Label ID="lblbolsas1" runat="server" Text="bolsas"></asp:Label>
                                            </ItemTemplate><ItemStyle Width="95px" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="inv. bodega" HeaderStyle-Font-Size="X-Small" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:RangeValidator id="rvaInventarios_bodega" runat="server" 
                                                    ErrorMessage="El inventario en bodega del producto indicado en 'Rollo de fruta natural' es muy alto" Display="Dynamic"
                                                    MinimumValue="0" MaximumValue="999" Type="Double" 
                                                    ControlToValidate="txtInventarios_bodega" ValidationGroup="Jovy">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvInventarios_bodega" runat="server" 
                                                    ControlToValidate="txtInventarios_bodega" 
                                                    ErrorMessage="Completa inventarios de bodega del producto indicado en 'Rollo de fruta natural'" 
                                                    ValidationGroup="Jovy">*</asp:RequiredFieldValidator>  
                                                <asp:TextBox ID="txtInventarios_bodega" runat="server" Width="40" MaxLength="3" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Inventarios_bodega") %>'></asp:TextBox>
                                                <asp:Label ID="lblbolsas2" runat="server" Text="bolsas"></asp:Label>
                                            </ItemTemplate><ItemStyle Width="95px" />
                                        </asp:TemplateField>
                                                                            
                                        <asp:TemplateField HeaderText="Caducidad  (dd/mm/aaaa)" HeaderStyle-Font-Size="X-Small" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:RegularExpressionValidator id="rvaCaducidad" runat="server" 
                                                    ErrorMessage="La fecha en el producto indicado en 'Rollo de fruta natural' no tiene el formato 'dd/mm/aaaa'" 
                                                    Display="Dynamic" 
                                                    ValidationExpression="\d{2}/\d{2}/\d{4}" ControlToValidate="txtCaducidad" 
                                                    ValidationGroup="Jovy" >*</asp:RegularExpressionValidator>
                                                <asp:RangeValidator ID="rvCaducidad" runat="server" 
                                                    ErrorMessage="La fecha en el producto indicado en 'Rollo de fruta natural' no es valida" 
                                                    ValidationGroup="Jovy" ControlToValidate="txtCaducidad"
                                                    MaximumValue="31/12/3000" MinimumValue="01/01/2010" Type="Date">*</asp:RangeValidator>
                                                <asp:TextBox ID="txtCaducidad" runat="server" Width="70" MaxLength="10" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "Caducidad","{0:d}")%>' ></asp:TextBox>
                                            </ItemTemplate><ItemStyle Width="95px" />
                                        </asp:TemplateField>
                                                                                
                                        <asp:TemplateField HeaderText="Cantidad caducada" HeaderStyle-Font-Size="X-Small" ItemStyle-VerticalAlign="Top">
                                            <ItemTemplate>
                                                <asp:RangeValidator id="rvacantidad_caducada" runat="server" 
                                                    ErrorMessage="La cantidad caducada del producto indicado en 'Rollo de fruta natural' es muy alta" Display="Dynamic"
                                                    MinimumValue="0" MaximumValue="999" Type="Double" 
                                                    ControlToValidate="txtcantidad_caducada" ValidationGroup="Jovy">*</asp:RangeValidator>
                                                <asp:TextBox ID="txtcantidad_caducada" runat="server" Width="40" MaxLength="3" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "cantidad_caducada") %>'></asp:TextBox>
                                                <asp:Label ID="lblbolsas3" runat="server" Text="bolsas"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="95px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                </asp:GridView>
                                    <br />
                                    
                                </asp:View>
                            </asp:MultiView>
                            <br />
                                                    
                    <br />
                            <table style="width: 100%">
                               <tr>
                                   <td style="text-align: center; height: 31px; " colspan="2">
                                   
                                       <asp:ValidationSummary ID="vsJovy" runat="server" 
                                           ValidationGroup="Jovy" ShowMessageBox="True" ShowSummary="False" />
                                   <asp:UpdateProgress ID="UpdateProgress" runat="server" 
                                           AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0">
                                           <ProgressTemplate>
                                               <div id="pnlSave">
                                                   <img alt="" src="../../../Img/loading.gif" />La información se esta guardando.<br />
                                                        <br />
                                                        Por favor espera.<br /><br />  
                                                </div>
                                           </ProgressTemplate>
                                       </asp:UpdateProgress>
                                       <asp:Label ID="lblAviso" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
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
                            
                            <asp:AsyncPostBackTrigger ControlID="Menu1" />
                            
                        </Triggers>
                    </asp:UpdatePanel> 
                    
               
        </div>

 <!--CONTENT SIDE COLUMN AVISOS -->
        <div class="clear"></div>
    
    </div>
</asp:Content>
