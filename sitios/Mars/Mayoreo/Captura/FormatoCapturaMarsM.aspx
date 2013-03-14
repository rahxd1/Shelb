<%@ Page Language="vb" 
    Culture="es-MX" 
    MasterPageFile="~/sitios/Mars/Mayoreo/MayoreoMars.Master" 
    AutoEventWireup="false" 
    CodeBehind="FormatoCapturaMarsM.aspx.vb" 
    Inherits="procomlcd.FormatoCapturaMarsM"
    title="Mars Mayoreo - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoMayoreoMars" runat="Server">

 
<!--titulo-pagina-->
    <div id="titulo-pagina">Información</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
                <table style="width: 100%">
                    <tr>
                        <td style="width: 434px" colspan="2">
                            &nbsp;</td>
                        <td style="text-align: right">
                            <img alt="" src="../../../../Img/arrow.gif" /><asp:HyperLink 
                            ID="lnkRegresar" runat="server" 
                            NavigateUrl="~/sitios/Mars/Mayoreo/Captura/RutaMarsM.aspx">Regresar</asp:HyperLink></td>
                    </tr>
                    <tr>
                        <td style="width: 94px">
                            Tienda</td>
                        <td style="width: 434px">
                            <asp:Label ID="lblTienda" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                        <td style="text-align: right">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 94px">
                            Tipo tienda</td>
                        <td style="width: 434px" valign="middle">
                            <asp:DropDownList ID="cmbTipoTienda" runat="server">
                                <asp:ListItem Value="2">Mostrador</asp:ListItem>
                                <asp:ListItem Value="3">Autoservicio</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvTipoTienda" runat="server" 
                                ControlToValidate="cmbTipoTienda" 
                                ErrorMessage="Por favor indica el tipo de tienda" 
                                ValidationGroup="Mars">* Selecciona el tipo de tienda por favor.</asp:RequiredFieldValidator>
                        </td>
                        <td style="text-align: right">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 434px" colspan="2">
                            &nbsp;</td>
                        <td style="text-align: right">
                            &nbsp;</td>
                    </tr>
                    </table>
                    
            <asp:Menu ID="Menu1" Width="168px" runat="server" Orientation="Horizontal" 
                StaticEnableDefaultPopOutImage="False" OnMenuItemClick="Menu1_MenuItemClick">
                <Items>
                    <asp:MenuItem ImageUrl="../../Img/selectedtabPp.GIF" Text=" " Value="0"></asp:MenuItem>
                    <asp:MenuItem ImageUrl="../../Img/unselectedtabPc.GIF" Text=" " Value="1"></asp:MenuItem>
                </Items>
            </asp:Menu> 
                    
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="Tab1" runat="server">        
                    <asp:Panel ID="pnlPropios" runat="server">
                        <asp:GridView ID="gridProductos1" runat="server" AutoGenerateColumns="False" 
                            DataKeyNames="id_producto" Width="99%" style="text-align: center" ShowFooter="True" 
                            Caption="Producto Mars seco" CssClass="grid-view" CellPadding="3" 
                            GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="None" 
                            BorderWidth="1px">
                            <RowStyle HorizontalAlign="Left" BackColor="#EEEEEE" ForeColor="Black" />
                            <Columns>
                                <asp:BoundField HeaderText="tipo" DataField="tipo_producto" />
                                <asp:BoundField HeaderText="Categoria" DataField="categoria" />
                                <asp:BoundField HeaderText="Producto" DataField="nombre_producto" />
                                <asp:TemplateField HeaderText="Precio pza.">
                                    <HeaderStyle Font-Size="X-Small" />
                                    <ItemTemplate>
                                        $<asp:TextBox ID="txtPrecioPza" runat="server" Width="50" MaxLength="6" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "precio_pieza") %>'></asp:TextBox>
                                        <asp:RangeValidator ID="rvaPrecioPza" runat="server" 
                                            ControlToValidate="txtPrecioPza" Display="Dynamic" 
                                            ErrorMessage="El precio del producto indicado con un (*) en productos secos de Mars supera la cantidad permitida"
                                            MaximumValue='<%# DataBinder.Eval(Container, "DataItem.precio_max")%>' 
                                            MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPrecioPza" runat="server" 
                                            ControlToValidate="txtPrecioPza" 
                                            ErrorMessage="Completa el precio del producto indicado con un (*) en productos secos de Mars" 
                                            ValidationGroup="Mars">*</asp:RequiredFieldValidator>    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Precio kilo">
                                    <HeaderStyle Font-Size="X-Small" />
                                    <ItemTemplate>
                                        $<asp:TextBox ID="txtPrecioKl" runat="server" Width="50"  MaxLength="6" Text='<%# DataBinder.Eval(Container.DataItem, "precio_kilo") %>'></asp:TextBox>
                                        <asp:RangeValidator ID="rvaPrecioKl" runat="server" 
                                            ControlToValidate="txtPrecioKl" Display="Dynamic" 
                                            ErrorMessage="El precio por kilo del producto indicado con un (*) en productos secos de Mars supera la cantidad permitida"
                                            MaximumValue="600" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPrecioKl" runat="server" 
                                            ControlToValidate="txtPrecioKl" 
                                            ErrorMessage="Completa el precio por kilo del producto indicado con un (*) en productos secos de Mars" 
                                            ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inventario pza.">
                                    <HeaderStyle Font-Size="X-Small" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtInventario" runat="server" Width="50" MaxLength="4" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "inventario") %>'></asp:TextBox>
                                        <asp:RangeValidator ID="rvaInventario" runat="server" 
                                            ControlToValidate="txtInventario" Display="Dynamic" 
                                            ErrorMessage="El inventario del producto indicado con un (*) en productos secos de Mars supera la cantidad permitida"
                                            MaximumValue="9999" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvInventario" runat="server" 
                                            ControlToValidate="txtInventario" 
                                            ErrorMessage="Completa el precio del producto indicado con un (*) en productos secos de Mars" 
                                            ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No catalogado">
                                    <HeaderStyle Font-Size="X-Small" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkNoCatalogado" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "no_catalogado")) %>' />                           
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Agotado">
                                    <HeaderStyle Font-Size="X-Small" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAgotado" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Agotado")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                                <h1>No hay datos</h1>
                            </EmptyDataTemplate>
                            <FooterStyle ForeColor="Black" CssClass="grid-footer" 
                                BackColor="#CCCCCC" />
                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle HorizontalAlign="Left" BackColor="#DCDCDC" />
                        </asp:GridView>
                        <br />
                        <asp:GridView ID="gridProductos2" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="id_producto" Width="99%" style="text-align: center" ShowFooter="True" 
                    Caption="Producto Mars humedo" CssClass="grid-view" CellPadding="3" 
                    GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="None" 
                    BorderWidth="1px">
                    <RowStyle HorizontalAlign="Left" BackColor="#EEEEEE" ForeColor="Black" />
                    <Columns>
                        <asp:BoundField HeaderText="tipo" DataField="tipo_producto" />
                        <asp:BoundField HeaderText="Categoria" DataField="categoria" />
                        <asp:BoundField HeaderText="Producto" DataField="nombre_producto" />
                        <asp:TemplateField HeaderText="Precio pza.">
                            <HeaderStyle Font-Size="X-Small" />
                            <ItemTemplate>
                                $<asp:TextBox ID="txtPrecioPza" runat="server" Width="50" MaxLength="6" 
                                    Text='<%# DataBinder.Eval(Container.DataItem, "precio_pieza") %>'></asp:TextBox>
                                <asp:RangeValidator ID="rvaPrecioPza" runat="server" 
                                    ControlToValidate="txtPrecioPza" Display="Dynamic" 
                                    ErrorMessage="El precio del producto indicado con un (*) en productos humedos de Mars supera la cantidad permitida"
                                    MaximumValue="800" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                <asp:RequiredFieldValidator ID="rfvPrecioPza" runat="server" 
                                    ControlToValidate="txtPrecioPza" 
                                    ErrorMessage="Completa el precio del producto indicado con un (*) en productos humedos de Mars" 
                                    ValidationGroup="Mars">*</asp:RequiredFieldValidator> 
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Precio caja">
                            <HeaderStyle Font-Size="X-Small" />
                            <ItemTemplate>
                                $<asp:TextBox ID="txtPrecioCj" runat="server" Width="50" MaxLength="6" 
                                    Text='<%# DataBinder.Eval(Container.DataItem, "precio_kilo") %>'></asp:TextBox>
                                <asp:RangeValidator ID="rvaPrecioCj" runat="server" 
                                    ControlToValidate="txtPrecioCj" Display="Dynamic" 
                                    ErrorMessage="El precio por caja del producto indicado con un (*) en productos humedos de Mars supera la cantidad permitida"
                                    MaximumValue="800" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                <asp:RequiredFieldValidator ID="rfvPrecioCj" runat="server" 
                                    ControlToValidate="txtPrecioCj" 
                                    ErrorMessage="Completa el precio por caja del producto indicado con un (*) en productos humedos de Mars" 
                                    ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Inventario pza.">
                            <HeaderStyle Font-Size="X-Small" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtInventario" runat="server" Width="50" MaxLength="4" 
                                    Text='<%# DataBinder.Eval(Container.DataItem, "inventario") %>'></asp:TextBox>
                                <asp:RangeValidator ID="rvaInventario" runat="server" 
                                    ControlToValidate="txtInventario" Display="Dynamic" 
                                    ErrorMessage="El inventario del producto indicado con un (*) en productos humedos de Mars supera la cantidad permitida"
                                    MaximumValue="800" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                <asp:RequiredFieldValidator ID="rfvInventario" runat="server" 
                                    ControlToValidate="txtInventario" 
                                    ErrorMessage="Completa el inventario del producto indicado con un (*) en productos humedos de Mars" 
                                    ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No catalogado">
                            <HeaderStyle Font-Size="X-Small" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkNoCatalogado" runat="server" 
                                    Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "no_catalogado")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Agotado">
                            <HeaderStyle Font-Size="X-Small" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkAgotado" runat="server" 
                                    Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Agotado")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                        <h1>No hay datos</h1>
                    </EmptyDataTemplate>
                    <FooterStyle ForeColor="Black" CssClass="grid-footer" 
                        BackColor="#CCCCCC" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle HorizontalAlign="Left" BackColor="#DCDCDC" />
                </asp:GridView>
                </asp:Panel> 
            </asp:View>
            
            <asp:View ID="Tab2" runat="server">
                <asp:Panel ID="pnlCompetencia" runat="server">
               <asp:GridView ID="gridProductosCompetencia1" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="id_producto" Width="99%" style="text-align: center" ShowFooter="True" 
                    Caption="Productos competencia seco" CssClass="grid-view" CellPadding="3" 
                    GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="None" 
                    BorderWidth="1px">
                    <RowStyle HorizontalAlign="Left" BackColor="#EEEEEE" ForeColor="Black" />
                    <Columns>
                        <asp:BoundField HeaderText="tipo" DataField="tipo_producto" />
                        <asp:BoundField HeaderText="Categoria" DataField="categoria" />
                        <asp:BoundField HeaderText="Producto" DataField="nombre_producto" />
                        <asp:TemplateField HeaderText="Precio pza.">
                            <HeaderStyle Font-Size="X-Small" />
                            <ItemTemplate>
                                $<asp:TextBox ID="txtPrecioPza" runat="server" Width="50" 
                                    Text='<%# DataBinder.Eval(Container.DataItem, "precio_pieza") %>'></asp:TextBox>
                                <asp:RangeValidator ID="rvaPrecioPza" runat="server" 
                                    ControlToValidate="txtPrecioPza" Display="Dynamic" 
                                    ErrorMessage="El precio por pieza del producto indicado con un (*) en productos secos de la competencia supera la cantidad permitida"
                                    MaximumValue="800" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                <asp:RequiredFieldValidator ID="rfvPrecioPza" runat="server" 
                                    ControlToValidate="txtPrecioPza" 
                                    ErrorMessage="Completa el precio por caja del producto indicado con un (*) en productos secos de la competencia" 
                                    ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Precio kilo">
                            <HeaderStyle Font-Size="X-Small" />
                            <ItemTemplate>
                                $<asp:TextBox ID="txtPrecioKl" runat="server" Width="50" 
                                    Text='<%# DataBinder.Eval(Container.DataItem, "precio_kilo") %>'></asp:TextBox>
                                <asp:RangeValidator ID="rvaPrecioKl" runat="server" 
                                    ControlToValidate="txtPrecioKl" Display="Dynamic" 
                                    ErrorMessage="El precio por kilo del producto indicado con un (*) en productos secos de la competencia supera la cantidad permitida"
                                    MaximumValue="800" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                <asp:RequiredFieldValidator ID="rfvPrecioKl" runat="server" 
                                    ControlToValidate="txtPrecioKl" 
                                    ErrorMessage="Completa el precio por kilo del producto indicado con un (*) en productos secos de la competencia" 
                                    ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                        <h1>No hay datos</h1>
                    </EmptyDataTemplate>
                    <FooterStyle ForeColor="Black" CssClass="grid-footer" 
                        BackColor="#CCCCCC" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle HorizontalAlign="Left" BackColor="#DCDCDC" />
                </asp:GridView>
                <br />
                <asp:GridView ID="gridProductosCompetencia2" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="id_producto" Width="99%" style="text-align: center" ShowFooter="True" 
                    Caption="Productos competencia humedos" CssClass="grid-view" CellPadding="3" 
                    GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="None" 
                    BorderWidth="1px">
                    <RowStyle HorizontalAlign="Left" BackColor="#EEEEEE" ForeColor="Black" />
                    <Columns>
                        <asp:BoundField HeaderText="tipo" DataField="tipo_producto" />
                        <asp:BoundField HeaderText="Categoria" DataField="categoria" />
                        <asp:BoundField HeaderText="Producto" DataField="nombre_producto" />
                        <asp:TemplateField HeaderText="Precio pza.">
                            <HeaderStyle Font-Size="X-Small" />
                            <ItemTemplate>
                                $<asp:TextBox ID="txtPrecioPza" runat="server" Width="50" 
                                    Text='<%# DataBinder.Eval(Container.DataItem, "precio_pieza") %>'></asp:TextBox>
                                <asp:RangeValidator ID="rvaPrecioPza" runat="server" 
                                    ControlToValidate="txtPrecioPza" Display="Dynamic" 
                                    ErrorMessage="El precio por pieza del producto indicado con un (*) en productos humedos de la competencia supera la cantidad permitida"
                                    MaximumValue="800" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                <asp:RequiredFieldValidator ID="rfvPrecioPza" runat="server" 
                                    ControlToValidate="txtPrecioPza" 
                                    ErrorMessage="Completa el precio por pieza del producto indicado con un (*) en productos humedos de la competencia" 
                                    ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Precio caja">
                            <HeaderStyle Font-Size="X-Small" />
                            <ItemTemplate>
                                $<asp:TextBox ID="txtPrecioCj" runat="server" Width="50" 
                                    Text='<%# DataBinder.Eval(Container.DataItem, "precio_kilo") %>'></asp:TextBox>
                                <asp:RangeValidator ID="rvaPrecioCj" runat="server" 
                                    ControlToValidate="txtPrecioCj" Display="Dynamic" 
                                    ErrorMessage="El precio por caja del producto indicado con un (*) en productos humedos de la competencia supera la cantidad permitida"
                                    MaximumValue="800" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                <asp:RequiredFieldValidator ID="rfvPrecioCj" runat="server" 
                                    ControlToValidate="txtPrecioCj" 
                                    ErrorMessage="Completa el precio por caja del producto indicado con un (*) en productos humedos de la competencia" 
                                    ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                        <h1>No hay datos</h1>
                    </EmptyDataTemplate>
                    <FooterStyle ForeColor="Black" CssClass="grid-footer" 
                        BackColor="#CCCCCC" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle HorizontalAlign="Left" BackColor="#DCDCDC" />
                </asp:GridView>
            </asp:Panel> 
            </asp:View>
        </asp:MultiView>
            <br />
            <br />
                       <table style="border-style: groove; width: 99%; height: 115px; margin-right: 0px" >
                
                           <tr style="color: #FFFFFF">
                               <td style="text-align: center; background-color: #02456F; height: 29px; width: 616px;" >
                                
                                   Comentarios:</td>
                           </tr>
                           <tr style="font-size: small">
                               <td style="font-size: x-small; height: 75px; width: 616px;" >
                                 
                                   <asp:TextBox ID="txtComentarioGeneral" runat="server" Width="658px" Height="60px" 
                                       TextMode="MultiLine" MaxLength="300"></asp:TextBox>
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
                                           ValidationGroup="Mars" />
                                           <asp:Label ID="lblFecha" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                       <br />
                                       <asp:Label ID="lblMinimos" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
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
                                            ValidationGroup="Mars" CssClass="button" />
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
        
        <!--CONTENT SIDE COLUMN-->
        <div id="content-side-two-column">
            <asp:LinkButton ID="lnkExportar" runat="server" Font-Bold="True">Exportar a Excel</asp:LinkButton>
        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>
