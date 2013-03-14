<%@ Page Language="vb" 
    Culture="es-MX" 
    MasterPageFile="~/sitios/Berol/NR/Rubbermaid.Master" 
    AutoEventWireup="false" 
    CodeBehind="FormatoInventariosNR.aspx.vb" 
    Inherits="procomlcd.FormatoInventariosNR"
    title="Newell Rubbermaid - Captura" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoNR" runat="Server">

    <!--PAGE TITLE-->
<!--CONTENT CONTAINER-->
<div id="titulo-pagina">
    Formato inventarios</div>
        <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server" scriptMode="Release"/>
                        
                <table style="width: 99%">
                    <tr>
                                    <td __designer:mapid="1e0" style="width: 56px; text-align: right">
                                        &nbsp;</td>
                                    <td __designer:mapid="1e1" style="text-align: right">
                                        <img alt="" src="../../../../Img/arrow.gif" /><asp:HyperLink ID="lnkRegresar" runat="server" NavigateUrl="~/sitios/Berol/NR/Captura/RutasPromotorNR.aspx">Regresar</asp:HyperLink>
                                    </td>
                                </tr>
                    <tr>
                                    <td __designer:mapid="1e0" style="width: 56px; text-align: right">
                                        Tienda</td>
                                    <td __designer:mapid="1e1">
                                        <asp:Label ID="lblTienda" runat="server" 
                            Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                    <tr>
                                    <td __designer:mapid="1e0" style="width: 56px; text-align: right">
                                        Cadena</td>
                                    <td __designer:mapid="1e1">
                                        <asp:Label ID="lblCadena" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                 </table>
                 
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    
                        <asp:Menu ID="Menu1" Width="98px" runat="server" Orientation="Horizontal" 
                            StaticEnableDefaultPopOutImage="False" 
                            OnMenuItemClick="Menu1_MenuItemClick">
                            <Items>
                                <asp:MenuItem ImageUrl="../../Img/selectedtabL.GIF" Text=" " Value="0"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabChe.GIF" Text=" " Value="1"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabLs.GIF" Text=" " Value="2"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabPun.GIF" Text=" " Value="3"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabGom.GIF" Text=" " Value="4"></asp:MenuItem>
                                </Items>
                        </asp:Menu> 
                        <asp:Menu ID="Menu2" Width="98%" runat="server" Orientation="Horizontal" 
                            StaticEnableDefaultPopOutImage="False" 
                            OnMenuItemClick="Menu1_MenuItemClick">
                            <Items>
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabBol.GIF" Text=" " Value="5"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabRg.GIF" Text=" " Value="6"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabCor.GIF" Text=" " Value="7"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabPl.GIF" Text=" " Value="8"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabM.GIF" Text=" " Value="9"></asp:MenuItem>
                            </Items>
                        </asp:Menu>
                        
                         <asp:Menu ID="Menu3" Width="98%" runat="server" Orientation="Horizontal" 
                            StaticEnableDefaultPopOutImage="False" 
                            OnMenuItemClick="Menu1_MenuItemClick">
                            <Items>
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabRes.GIF" Text=" " Value="10"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabMB.GIF" Text=" " Value="11"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabMP.GIF" Text=" " Value="12"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabC.GIF" Text=" " Value="13"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabLC.GIF" Text=" " Value="14"></asp:MenuItem>
                            </Items>
                        </asp:Menu>
                        
                        <asp:Menu ID="Menu4" Width="98%" runat="server" Orientation="Horizontal" 
                            StaticEnableDefaultPopOutImage="False" 
                            OnMenuItemClick="Menu1_MenuItemClick">
                            <Items>
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabS.GIF" Text=" " Value="15"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabRot.GIF" Text=" " Value="16"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabRep.GIF" Text=" " Value="17"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="../../Img/tab.GIF" Text=" " Value="18"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="../../Img/tab.GIF" Text=" " Value="19"></asp:MenuItem>
                            </Items>
                        </asp:Menu>
                          
                        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                            <asp:View ID="Tab1" runat="server">
                                <asp:GridView ID="gridLapices" runat="server" AutoGenerateColumns="False" 
                                    Caption="Lápices" CssClass="grid-view" DataKeyNames="id_producto" 
                                    ShowFooter="False" Visible="true" Width="100%">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="codigo" HeaderText="UPC" />
                                        <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                        <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="nombre_presentacion" HeaderText="Presentación" />
                                        <asp:TemplateField HeaderText="Catálogo">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCatalogo" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "cat")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Surtido">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSurtido" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "sur")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. piso">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvPiso" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_piso") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvPiso" runat="server" ControlToValidate="txtInvPiso" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios piso de la sección 'Lápices' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. bodega">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvBodega" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_bodega") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvBodega" runat="server" ControlToValidate="txtInvBodega" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios bodega de la sección 'Lápices' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:View>
                            
                            <asp:View ID="Tab2" runat="server">
                              <asp:GridView ID="gridChecadores" runat="server" AutoGenerateColumns="False" 
                                    Caption="Checadores" CssClass="grid-view" DataKeyNames="id_producto" 
                                    ShowFooter="False" Visible="true" Width="100%">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="codigo" HeaderText="UPC" />
                                        <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                        <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="nombre_presentacion" HeaderText="Presentación" />
                                        <asp:TemplateField HeaderText="Catálogo">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCatalogo" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "cat")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Surtido">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSurtido" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "sur")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. piso">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvPiso" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_piso") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvPiso" runat="server" ControlToValidate="txtInvPiso" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios piso de la sección 'Sams club' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. bodega">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvBodega" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_bodega") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvBodega" runat="server" ControlToValidate="txtInvBodega" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios bodega de la sección 'Sams club' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:View>    
                            
                            <asp:View ID="Tab3" runat="server">  
                                <asp:GridView ID="gridLapiceros" runat="server" AutoGenerateColumns="False" 
                                    Caption="Lapiceros" CssClass="grid-view" DataKeyNames="id_producto" 
                                    ShowFooter="False" Visible="true" Width="100%">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="codigo" HeaderText="UPC" />
                                        <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                        <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="nombre_presentacion" HeaderText="Presentación" />
                                        <asp:TemplateField HeaderText="Catálogo">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCatalogo" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Cat")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Surtido">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSurtido" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Sur")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. piso">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvPiso" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_piso") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvPiso" runat="server" ControlToValidate="txtInvPiso" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios piso de la sección 'Lapiceros' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. bodega">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvBodega" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_bodega") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvBodega" runat="server" ControlToValidate="txtInvBodega" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios bodega de la sección 'Lapiceros' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>    
                            </asp:View>
                            <asp:View ID="Tab4" runat="server">
                                <asp:GridView ID="gridPuntillas" runat="server" AutoGenerateColumns="False" 
                                    Caption="Puntillas" CssClass="grid-view" DataKeyNames="id_producto" 
                                    ShowFooter="False" Visible="true" Width="100%">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="codigo" HeaderText="UPC" />
                                        <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                        <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="nombre_presentacion" HeaderText="Presentación" />
                                        <asp:TemplateField HeaderText="Catálogo">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCatalogo" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "cat")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Surtido">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSurtido" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "sur")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. piso">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvPiso" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_piso") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvPiso" runat="server" ControlToValidate="txtInvPiso" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios piso de la sección 'Puntillas' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. bodega">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvBodega" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_bodega") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvBodega" runat="server" ControlToValidate="txtInvBodega" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios bodega de la sección 'Puntillas' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="Tab5" runat="server">
                                <asp:GridView ID="gridGomas" runat="server" AutoGenerateColumns="False" 
                                    Caption="Gomas" CssClass="grid-view" DataKeyNames="id_producto" 
                                    ShowFooter="False" Visible="true" Width="100%">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="codigo" HeaderText="UPC" />
                                        <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                        <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="nombre_presentacion" HeaderText="Presentación" />
                                        <asp:TemplateField HeaderText="Catálogo">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCatalogo" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "cat")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Surtido">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSurtido" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "sur")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. piso">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvPiso" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_piso") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvPiso" runat="server" ControlToValidate="txtInvPiso" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios piso de la sección 'Gomas' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. bodega">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvBodega" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_bodega") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvBodega" runat="server" ControlToValidate="txtInvBodega" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios bodega de la sección 'Gomas' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="Tab6" runat="server">
                                <asp:GridView ID="gridBoligrafos" runat="server" AutoGenerateColumns="False" 
                                    Caption="Bolígrafos" CssClass="grid-view" DataKeyNames="id_producto" 
                                    ShowFooter="False" Visible="true" Width="100%">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="codigo" HeaderText="UPC" />
                                        <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                        <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="nombre_presentacion" HeaderText="Presentación" />
                                        <asp:TemplateField HeaderText="Catálogo">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCatalogo" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "cat")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Surtido">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSurtido" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "sur")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. piso">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvPiso" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_piso") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvPiso" runat="server" ControlToValidate="txtInvPiso" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios piso de la sección 'Bolígrafos' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. bodega">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvBodega" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_bodega") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvBodega" runat="server" ControlToValidate="txtInvBodega" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios bodega de la sección 'Bolígrafos' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="Tab7" runat="server">
                                <asp:GridView ID="gridRollerGel" runat="server" AutoGenerateColumns="False" 
                                    Caption="Roller y gel" CssClass="grid-view" DataKeyNames="id_producto" 
                                    ShowFooter="False" Visible="true" Width="100%">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="codigo" HeaderText="UPC" />
                                        <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                        <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="nombre_presentacion" HeaderText="Presentación" />
                                        <asp:TemplateField HeaderText="Catálogo">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCatalogo" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "cat")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Surtido">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSurtido" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "sur")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. piso">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvPiso" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_piso") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvPiso" runat="server" ControlToValidate="txtInvPiso" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios piso de la sección 'Roller y gel' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. bodega">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvBodega" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_bodega") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvBodega" runat="server" ControlToValidate="txtInvBodega" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios bodega de la sección 'Roller y gel' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="Tab8" runat="server">
                                <asp:GridView ID="gridCorrectores" runat="server" AutoGenerateColumns="False" 
                                    Caption="Correctores" CssClass="grid-view" DataKeyNames="id_producto" 
                                    ShowFooter="False" Visible="true" Width="100%">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="codigo" HeaderText="UPC" />
                                        <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                        <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="nombre_presentacion" HeaderText="Presentación" />
                                        <asp:TemplateField HeaderText="Catálogo">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCatalogo" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "cat")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Surtido">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSurtido" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "sur")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. piso">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvPiso" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_piso") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvPiso" runat="server" ControlToValidate="txtInvPiso" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios piso de la sección 'Correctores' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. bodega">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvBodega" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_bodega") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvBodega" runat="server" ControlToValidate="txtInvBodega" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios bodega de la sección 'Correctores' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="Tab9" runat="server">
                                <asp:GridView ID="gridPlumones" runat="server" AutoGenerateColumns="False" 
                                    Caption="Plumones" CssClass="grid-view" DataKeyNames="id_producto" 
                                    ShowFooter="False" Visible="true" Width="100%">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="codigo" HeaderText="UPC" />
                                        <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                        <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="nombre_presentacion" HeaderText="Presentación" />
                                        <asp:TemplateField HeaderText="Catálogo">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCatalogo" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "cat")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Surtido">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSurtido" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "sur")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. piso">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvPiso" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_piso") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvPiso" runat="server" ControlToValidate="txtInvPiso" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios piso de la sección 'Plumones' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. bodega">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvBodega" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_bodega") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvBodega" runat="server" ControlToValidate="txtInvBodega" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios bodega de la sección 'Plumones' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="Tab10" runat="server">
                                <asp:GridView ID="gridMarcadores" runat="server" AutoGenerateColumns="False" 
                                    Caption="Marcadores" CssClass="grid-view" DataKeyNames="id_producto" 
                                    ShowFooter="False" Visible="true" Width="100%">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="codigo" HeaderText="UPC" />
                                        <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                        <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="nombre_presentacion" HeaderText="Presentación" />
                                        <asp:TemplateField HeaderText="Catálogo">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCatalogo" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "cat")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Surtido">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSurtido" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "sur")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. piso">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvPiso" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_piso") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvPiso" runat="server" ControlToValidate="txtInvPiso" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios piso de la sección 'Marcadores' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. bodega">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvBodega" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_bodega") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvBodega" runat="server" ControlToValidate="txtInvBodega" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios bodega de la sección 'Marcadores' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="Tab11" runat="server">
                                <asp:GridView ID="gridResaltadores" runat="server" AutoGenerateColumns="False" 
                                    Caption="Resaltadores" CssClass="grid-view" DataKeyNames="id_producto" 
                                    ShowFooter="False" Visible="true" Width="100%">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="codigo" HeaderText="UPC" />
                                        <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                        <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="nombre_presentacion" HeaderText="Presentación" />
                                        <asp:TemplateField HeaderText="Catálogo">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCatalogo" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "cat")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Surtido">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSurtido" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "sur")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. piso">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvPiso" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_piso") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvPiso" runat="server" ControlToValidate="txtInvPiso" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios piso de la sección 'Resaltadores' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. bodega">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvBodega" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_bodega") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvBodega" runat="server" ControlToValidate="txtInvBodega" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios bodega de la sección 'Resaltadores' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="Tab12" runat="server">
                                <asp:GridView ID="gridMarcadoresAgua" runat="server" AutoGenerateColumns="False" 
                                    Caption="Marcadores base agua" CssClass="grid-view" DataKeyNames="id_producto" 
                                    ShowFooter="False" Visible="true" Width="100%">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="codigo" HeaderText="UPC" />
                                        <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                        <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="nombre_presentacion" HeaderText="Presentación" />
                                        <asp:TemplateField HeaderText="Catálogo">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCatalogo" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "cat")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Surtido">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSurtido" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "sur")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. piso">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvPiso" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_piso") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvPiso" runat="server" ControlToValidate="txtInvPiso" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios piso de la sección 'Marcadores base agua' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. bodega">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvBodega" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_bodega") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvBodega" runat="server" ControlToValidate="txtInvBodega" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios bodega de la sección 'Marcadores base agua' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="Tab13" runat="server">
                                <asp:GridView ID="gridMarcadoresPizarron" runat="server" AutoGenerateColumns="False" 
                                    Caption="Marcadores pizarrón" CssClass="grid-view" DataKeyNames="id_producto" 
                                    ShowFooter="False" Visible="true" Width="100%">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="codigo" HeaderText="UPC" />
                                        <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                        <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="nombre_presentacion" HeaderText="Presentación" />
                                        <asp:TemplateField HeaderText="Catálogo">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCatalogo" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "cat")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Surtido">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSurtido" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "sur")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. piso">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvPiso" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_piso") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvPiso" runat="server" ControlToValidate="txtInvPiso" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios piso de la sección 'Marcadores pizarrón' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. bodega">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvBodega" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_bodega") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvBodega" runat="server" ControlToValidate="txtInvBodega" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios bodega de la sección 'Marcadores pizarrón' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:View>
                            
                            <asp:View ID="Tab14" runat="server">
                                <asp:GridView ID="gridCrayones" runat="server" AutoGenerateColumns="False" 
                                    Caption="Crayones" CssClass="grid-view" DataKeyNames="id_producto" 
                                    ShowFooter="False" Visible="true" Width="100%">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="codigo" HeaderText="UPC" />
                                        <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                        <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="nombre_presentacion" HeaderText="Presentación" />
                                        <asp:TemplateField HeaderText="Catálogo">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCatalogo" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "cat")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Surtido">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSurtido" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "sur")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. piso">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvPiso" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_piso") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvPiso" runat="server" ControlToValidate="txtInvPiso" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios piso de la sección 'Crayones' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. bodega">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvBodega" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_bodega") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvBodega" runat="server" ControlToValidate="txtInvBodega" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios bodega de la sección 'Crayones' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="Tab15" runat="server">
                                <asp:GridView ID="gridLapicesColor" runat="server" AutoGenerateColumns="False" 
                                    Caption="Lápices de color" CssClass="grid-view" DataKeyNames="id_producto" 
                                    ShowFooter="False" Visible="true" Width="100%">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="codigo" HeaderText="UPC" />
                                        <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                        <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="nombre_presentacion" HeaderText="Presentación" />
                                        <asp:TemplateField HeaderText="Catálogo">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCatalogo" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "cat")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Surtido">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSurtido" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "sur")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. piso">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvPiso" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_piso") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvPiso" runat="server" ControlToValidate="txtInvPiso" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios piso de la sección 'Lápices de color' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. bodega">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvBodega" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_bodega") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvBodega" runat="server" ControlToValidate="txtInvBodega" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios bodega de la sección 'Lápices de color' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="Tab16" runat="server">
                                <asp:GridView ID="gridSamsClub" runat="server" AutoGenerateColumns="False" 
                                    Caption="Sams club" CssClass="grid-view" DataKeyNames="id_producto" 
                                    ShowFooter="False" Visible="true" Width="100%">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="codigo" HeaderText="UPC" />
                                        <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                        <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="nombre_presentacion" HeaderText="Presentación" />
                                        <asp:TemplateField HeaderText="Catálogo">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCatalogo" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "cat")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Surtido">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSurtido" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "sur")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. piso">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvPiso" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_piso") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvPiso" runat="server" ControlToValidate="txtInvPiso" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios piso de la sección 'Sams club' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. bodega">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvBodega" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_bodega") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvBodega" runat="server" ControlToValidate="txtInvBodega" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios bodega de la sección 'Sams club' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:View>
                            
                            <asp:View ID="Tab17" runat="server">
                                <asp:GridView ID="gridRotuladores" runat="server" AutoGenerateColumns="False" 
                                    Caption="Rotuladores" CssClass="grid-view" DataKeyNames="id_producto" 
                                    ShowFooter="False" Visible="true" Width="100%">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="codigo" HeaderText="UPC" />
                                        <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                        <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="nombre_presentacion" HeaderText="Presentación" />
                                        <asp:TemplateField HeaderText="Catálogo">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCatalogo" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "cat")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Surtido">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSurtido" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "sur")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. piso">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvPiso" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_piso") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvPiso" runat="server" ControlToValidate="txtInvPiso" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios piso de la sección 'Sams club' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. bodega">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvBodega" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_bodega") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvBodega" runat="server" ControlToValidate="txtInvBodega" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios bodega de la sección 'Sams club' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:View>
                            
                            <asp:View ID="Tab18" runat="server">
                                <asp:GridView ID="gridRepuestos" runat="server" AutoGenerateColumns="False" 
                                    Caption="Rotuladores" CssClass="grid-view" DataKeyNames="id_producto" 
                                    ShowFooter="False" Visible="true" Width="100%">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="codigo" HeaderText="UPC" />
                                        <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                        <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="nombre_presentacion" HeaderText="Presentación" />
                                        <asp:TemplateField HeaderText="Catálogo">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCatalogo" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "cat")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Surtido">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSurtido" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "sur")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. piso">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvPiso" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_piso") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvPiso" runat="server" ControlToValidate="txtInvPiso" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios piso de la sección 'Sams club' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inv. bodega">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInvBodega" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "inv_bodega") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaInvBodega" runat="server" ControlToValidate="txtInvBodega" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El campo inventarios bodega de la sección 'Sams club' no contiene números" 
                                                    Type="Double" ValidationGroup="Berol">*</asp:RangeValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:View>
                        </asp:MultiView>
                        <br />
                        
                        </ContentTemplate>  
              </asp:UpdatePanel>   
                       
                          
            <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>                    
                    <table style="width: 100%">
                        <tr>
                            <td colspan="2" style="text-align: center">
                                <asp:ValidationSummary ID="vsSYM" runat="server" 
                                   style="text-align: center" ValidationGroup="Berol" 
                                    HeaderText="Verifica la información" ShowMessageBox="True" 
                                    ShowSummary="False" />
                                <asp:UpdateProgress ID="UpdateProgress" runat="server" 
                                       AssociatedUpdatePanelID="UpdatePanel" DisplayAfter="0">
                                       <ProgressTemplate>
                                           <div id="pnlSave" >
                                               <img alt="" src="../../../../Img/loading.gif" />La información se esta guardando<br />
                                                   Por favor espera...<br />
                                                   <br />
                                                   <br />
                                            </div>
                                           <br />
                                       </ProgressTemplate>
                                   </asp:UpdateProgress>
                             </td></tr>
                    <tr><td style="text-align: center">
                        <asp:Button ID="btnGuardar" runat="server" CssClass="button" Text="Guardar" 
                            ValidationGroup="Berol" /></td>
                    <td style="text-align: center">
                        <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" 
                            CssClass="button" Text="Cancelar" /></td></tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" />
                </Triggers>
            </asp:UpdatePanel>             
        </div>    
        
        
 <!--CONTENT SIDE COLUMN DATOS -->
        <div id="content-side-two-column">
        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>

