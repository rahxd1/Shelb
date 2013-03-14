<%@ Page Language="vb" 
    Culture="es-MX" 
    MasterPageFile="~/sitios/SYM/Demos/SYMDemos.Master" 
    AutoEventWireup="false" 
    CodeBehind="FormatoCapturaSYMDemos.aspx.vb" 
    Inherits="procomlcd.FormatoCapturaSYMDemos"
    title="SYM Demos - Captura" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="Contenido_SYMDemos" runat="Server">

    <!--PAGE TITLE-->
<!--CONTENT CONTAINER-->
<div id="titulo-pagina">
    Formato captura de información</div>
        <div id="contenido">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main">
            <asp:ScriptManager ID="ScriptManager1" runat="server" scriptMode="Release"/>
                        
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: right">
                            <img alt="" src="../../../../Img/arrow.gif" /><asp:HyperLink ID="lnkRegresar" runat="server" NavigateUrl="~/sitios/SYM/Demos/Captura/RutaSYMDemos.aspx">Regresar</asp:HyperLink>
                        </td>
                    </tr>
            </table>
                        
                <table style="width: 100%">
                    <tr>
                        <td style="width: 140px">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 140px; text-align: right;">
                                                        Tienda</td>
                        <td>
                            <asp:TextBox ID="txtTienda" runat="server" Width="247px" MaxLength="30"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTienda" runat="server" 
                                ControlToValidate="txtTienda" ErrorMessage="Indica el nombre de la tienda." ValidationGroup="SYM">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 140px; text-align: right;">
                            Cadena</td>
                        <td>
                            <asp:Label ID="lblCadena" runat="server" 
                            Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 140px; text-align: right;" valign="top">
                            Nombre demostradora</td>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 98px; text-align: right">
                                        Apellido paterno</td>
                                    <td>
                                        <asp:TextBox ID="txtApellidoPaterno" runat="server" MaxLength="30"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvApellidoPaterno" runat="server" 
                                            ControlToValidate="txtApellidoPaterno" ErrorMessage="Indica el apellido paterno de la demostradora." ValidationGroup="SYM">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 98px; text-align: right">
                                        Apellido materno</td>
                                    <td>
                                        <asp:TextBox ID="txtApellidoMaterno" runat="server" MaxLength="30"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvApellidoMaterno" runat="server" 
                                            ControlToValidate="txtApellidoMaterno" ErrorMessage="Indica el apellido materno de la demostradora." ValidationGroup="SYM">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 98px; text-align: right">
                                        Nombre(s)</td>
                                    <td>
                                        <asp:TextBox ID="txtNombres" runat="server" Width="238px" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNombres" runat="server" 
                                            ControlToValidate="txtNombres" ErrorMessage="Indica el nombre de la demostradora." ValidationGroup="SYM">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
            </table>
                 
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <table style="width: 100%">
                        <tr>
                            <td valign="top" style="width: 451px">
                                <table style="width: 100%; height: 123px;">
                                    <tr>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <asp:GridView ID="gridProductos" runat="server" AutoGenerateColumns="False" 
                                                CssClass="grid-view" DataKeyNames="id_producto" ShowFooter="True" 
                                                Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="None" 
                                                BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                                                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Producto" ItemStyle-Width="75%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProducto" runat="server" Text='<%#Eval("nombre_producto")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="inv.inicial" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtInventario" runat="server" MaxLength="4" 
                                                                Text='<%#DataBinder.Eval(Container.DataItem, "inv_inicial") %>' Width="35"></asp:TextBox>
                                                            <asp:RangeValidator ID="rvaInventario" runat="server" 
                                                                ControlToValidate="txtInventario" Display="Dynamic" 
                                                                ErrorMessage="La cantidad de inventario del producto indicado con un (*) supera la cantidad permitida"
                                                                MaximumValue="1999" MinimumValue="0" Type="Double" ValidationGroup="SYM">*</asp:RangeValidator>
                                                            <asp:RequiredFieldValidator ID="rfvInventario" runat="server" 
                                                                ControlToValidate="txtInventario" 
                                                                ErrorMessage="Completa la cantidad de inventario del producto de Herradura indicado con un (*)" 
                                                                ValidationGroup="SYM">*</asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="precio" ItemStyle-Width="20%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtPrecio" runat="server" MaxLength="5" 
                                                                Text='<%#DataBinder.Eval(Container.DataItem, "precio") %>' Width="35"></asp:TextBox>
                                                            <asp:RangeValidator ID="rvaPrecio" runat="server" 
                                                                ControlToValidate="txtPrecio" Display="Dynamic" 
                                                                ErrorMessage="El precio del producto indicado con un (*) supera el precio permitido"
                                                                MaximumValue="1999" MinimumValue="0" Type="Double" ValidationGroup="SYM">*</asp:RangeValidator>
                                                            <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" 
                                                                ControlToValidate="txtPrecio" 
                                                                ErrorMessage="Completa el precio del producto de Herradura indicado con un (*)" 
                                                                ValidationGroup="SYM">*</asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grid-footer" BackColor="#CCCCCC" ForeColor="Black" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                <EmptyDataTemplate>
                                                    <h1>
                                                        No hay datos</h1>
                                                </EmptyDataTemplate>
                                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="text-align: center; font-weight: 700">
                                            <asp:CheckBox ID="chkViernes" runat="server" Text="Viernes" 
                                                AutoPostBack="True" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <asp:GridView ID="gridViernes" runat="server" AutoGenerateColumns="False" 
                                                CssClass="grid-view" DataKeyNames="id_producto" ShowFooter="True" 
                                                Width="100%" Enabled="False" BackColor="White" BorderColor="#999999" 
                                                BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                                                <RowStyle HorizontalAlign="Left" BackColor="#EEEEEE" ForeColor="Black" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Agotado">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkAgotado" runat="server" 
                                                                Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "agotado")) %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ventas">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtVentas" runat="server" MaxLength="3" 
                                                                Text='<%#DataBinder.Eval(Container.DataItem, "Ventas") %>' Width="30"></asp:TextBox>
                                                            <asp:RangeValidator ID="rvaVentas" runat="server" 
                                                                ControlToValidate="txtVentas" Display="Dynamic" 
                                                                ErrorMessage="La cantidad de ventas del producto indicado con un (*) del dia viernes supera la cantidad permitida"
                                                                MaximumValue="1999" MinimumValue="0" Type="Double" ValidationGroup="SYM">*</asp:RangeValidator>
                                                            <asp:RequiredFieldValidator ID="rfvVentas" runat="server" 
                                                                ControlToValidate="txtVentas" 
                                                                ErrorMessage="Completa la cantidad de ventas del producto indicado con un (*) del dia viernes" 
                                                                ValidationGroup="SYM">*</asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grid-footer" BackColor="#CCCCCC" ForeColor="Black" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                <EmptyDataTemplate>
                                                    <h1>
                                                        No hay datos</h1>
                                                </EmptyDataTemplate>
                                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gridViernesAb" runat="server" AutoGenerateColumns="False" 
                                                caption="Abordos" CssClass="grid-view" DataKeyNames="tipo_abordo" 
                                                ShowHeader="False" Width="100%" Enabled="False">
                                                <RowStyle HorizontalAlign="Left" />
                                                <Columns>
                                                    <asp:BoundField DataField="nombre_abordo" HeaderText="" />
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCantidad" runat="server" MaxLength="3" 
                                                                Text='<%#DataBinder.Eval(Container.DataItem, "cantidad") %>' Width="30"></asp:TextBox>
                                                            <asp:RangeValidator ID="rvaCantidad" runat="server" 
                                                                ControlToValidate="txtCantidad" Display="Dynamic" 
                                                                ErrorMessage="Los abordos del dia viernes es mayor al estimado" 
                                                                MaximumValue="200" MinimumValue="0" Type="Double" ValidationGroup="SYM">*</asp:RangeValidator>
                                                            <asp:RequiredFieldValidator ID="rfvCantidad" runat="server" 
                                                                ControlToValidate="txtCantidad" 
                                                                ErrorMessage="Completa la cantidad de abordos del dia viernes" 
                                                                ValidationGroup="SYM">*</asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grid-footer" />
                                                <EmptyDataTemplate>
                                                    <h1>
                                                        No hay datos</h1>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="font-weight: 700; text-align: center">
                                            <asp:CheckBox ID="chkSabado" runat="server" Text="Sábado" AutoPostBack="True" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <asp:GridView ID="gridSabado" runat="server" AutoGenerateColumns="False" 
                                                CssClass="grid-view" DataKeyNames="id_producto" ShowFooter="True" 
                                                Width="100%" Enabled="False" BackColor="White" BorderColor="#999999" 
                                                BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                                                <RowStyle HorizontalAlign="Left" BackColor="#EEEEEE" ForeColor="Black" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Agotado">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkAgotado" runat="server" 
                                                                Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "agotado")) %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ventas">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtVentas" runat="server" MaxLength="3" 
                                                                Text='<%#DataBinder.Eval(Container.DataItem, "Ventas") %>' Width="30"></asp:TextBox>
                                                            <asp:RangeValidator ID="rvaVentas" runat="server" 
                                                                ControlToValidate="txtVentas" Display="Dynamic" 
                                                                ErrorMessage="La cantidad de ventas del producto indicado con un (*) del dia sábado supera la cantidad permitida"
                                                                MaximumValue="200" MinimumValue="0" Type="Double" ValidationGroup="SYM">*</asp:RangeValidator>
                                                            <asp:RequiredFieldValidator ID="rfvVentas" runat="server" 
                                                                ControlToValidate="txtVentas" 
                                                                ErrorMessage="Completa la cantidad de ventas del producto indicado con un (*) del dia sábado" 
                                                                ValidationGroup="SYM">*</asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grid-footer" BackColor="#CCCCCC" ForeColor="Black" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                <EmptyDataTemplate>
                                                    <h1>
                                                        No hay datos</h1>
                                                </EmptyDataTemplate>
                                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gridSabadoAb" runat="server" AutoGenerateColumns="False" 
                                                caption="Abordos" CssClass="grid-view" DataKeyNames="tipo_abordo" 
                                                ShowHeader="False" Width="100%" Enabled="False">
                                                <RowStyle HorizontalAlign="Left" />
                                                <Columns>
                                                    <asp:BoundField DataField="nombre_abordo" HeaderText="" />
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCantidad" runat="server" MaxLength="3" 
                                                                Text='<%#DataBinder.Eval(Container.DataItem, "cantidad") %>' Width="30"></asp:TextBox>
                                                            <asp:RangeValidator ID="rvaCantidad" runat="server" 
                                                                ControlToValidate="txtCantidad" Display="Dynamic" 
                                                                ErrorMessage="Los abordos del dia sábado es mayor al estimado" 
                                                                MaximumValue="200" MinimumValue="0" Type="Double" ValidationGroup="SYM">*</asp:RangeValidator>
                                                            <asp:RequiredFieldValidator ID="rfvCantidad" runat="server" 
                                                                ControlToValidate="txtCantidad" 
                                                                ErrorMessage="Completa la cantidad de abordos del dia sábado" 
                                                                ValidationGroup="SYM">*</asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grid-footer" />
                                                <EmptyDataTemplate>
                                                    <h1>
                                                        No hay datos</h1>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="text-align: center; font-weight: 700">
                                            <asp:CheckBox ID="chkDomingo" runat="server" Text="Domingo" 
                                                AutoPostBack="True" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <asp:GridView ID="gridDomingo" runat="server" AutoGenerateColumns="False" 
                                                CssClass="grid-view" DataKeyNames="id_producto" ShowFooter="True" 
                                                Width="100%" Enabled="False" BackColor="White" BorderColor="#999999" 
                                                BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                                                <RowStyle HorizontalAlign="Left" BackColor="#EEEEEE" ForeColor="Black" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Agotado">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkAgotado" runat="server" 
                                                                Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "agotado")) %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ventas">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtVentas" runat="server" MaxLength="3" 
                                                                Text='<%#DataBinder.Eval(Container.DataItem, "Ventas") %>' Width="30"></asp:TextBox>
                                                            <asp:RangeValidator ID="rvaVentas" runat="server" 
                                                                ControlToValidate="txtVentas" Display="Dynamic" 
                                                                ErrorMessage="La cantidad de ventas del producto indicado con un (*) del dia domingo supera la cantidad permitida"
                                                                MaximumValue="1999" MinimumValue="0" Type="Double" ValidationGroup="SYM">*</asp:RangeValidator>
                                                            <asp:RequiredFieldValidator ID="rfvVentas" runat="server" 
                                                                ControlToValidate="txtVentas" 
                                                                ErrorMessage="Completa la cantidad de ventas del producto indicado con un (*) del dia domingo" 
                                                                ValidationGroup="SYM">*</asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grid-footer" BackColor="#CCCCCC" ForeColor="Black" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                <EmptyDataTemplate>
                                                    <h1>
                                                        No hay datos</h1>
                                                </EmptyDataTemplate>
                                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gridDomingoAb" runat="server" AutoGenerateColumns="False" 
                                                caption="Abordos" CssClass="grid-view" DataKeyNames="tipo_abordo" 
                                                ShowHeader="False" Width="100%" Enabled="False">
                                                <RowStyle HorizontalAlign="Left" />
                                                <Columns>
                                                    <asp:BoundField DataField="nombre_abordo" HeaderText="" />
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCantidad" runat="server" MaxLength="3" 
                                                                Text='<%#DataBinder.Eval(Container.DataItem, "cantidad") %>' Width="50"></asp:TextBox>
                                                            <asp:RangeValidator ID="rvaCantidad" runat="server" 
                                                                ControlToValidate="txtCantidad" Display="Dynamic" 
                                                                ErrorMessage="Los abordos del dia domingo es mayor al estimado" 
                                                                MaximumValue="200" MinimumValue="0" Type="Double" ValidationGroup="SYM">*</asp:RangeValidator>
                                                            <asp:RequiredFieldValidator ID="rfvCantidad" runat="server" 
                                                                ControlToValidate="txtCantidad" 
                                                                ErrorMessage="Completa la cantidad de abordos del dia domingo" 
                                                                ValidationGroup="SYM">*</asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grid-footer" />
                                                <EmptyDataTemplate>
                                                    <h1>
                                                        No hay datos</h1>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                        
                        </ContentTemplate>  
              </asp:UpdatePanel>   
                        
                        
                        <table style="border-style: groove; width: 99%; height: 99px; margin-right: 0px;" 
                width="100%" >
                            <tr>
                                <td style="text-align: center; height: 31px; color: #FFFFFF;" bgcolor="#003399">
                                    Comentarios del consumidor</td></tr>
                            <tr>
                                <td style="text-align: center; height: 31px; ">
                                    <asp:TextBox ID="txtComentarios" runat="server" Height="60px" 
                                        MaxLength="700" TextMode="MultiLine" Width="848px"></asp:TextBox>
                                </td>
                            </tr>
                            </table>
                        <br />   
                        
                          
            <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>                    
                    <table style="width: 100%">
                        <tr>
                            <td colspan="2" style="text-align: center">
                                <asp:ValidationSummary ID="vsSYM" runat="server" 
                                   style="text-align: center" ValidationGroup="SYM" 
                                    HeaderText="Verifica la información" ShowMessageBox="True" 
                                    ShowSummary="False" />
                                <asp:Label ID="lblMinimos" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                <br />
                                <asp:Label ID="lblInventarios" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                <br />
                                <asp:UpdateProgress ID="UpdateProgress" runat="server" 
                                       AssociatedUpdatePanelID="UpdatePanel" DisplayAfter="0">
                                       <ProgressTemplate>
                                           <div id="pnlSave" >
                                               <img alt="" src="../../../../Img/loading.gif" />
                                                    La información se esta guardando<br />
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
                            ValidationGroup="SYM" /></td>
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
        <div class="clear">
        </div>
    </div>
</asp:Content>

