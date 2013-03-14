<%@ Page Language="vb" 
    Culture="es-MX" 
    MasterPageFile="~/sitios/SYM/SYMPrecios/SYMPrecios.Master" 
    AutoEventWireup="false" 
    CodeBehind="FormatoCapturaSYMPrecios.aspx.vb" 
    Inherits="procomlcd.FormatoCapturaSYMPrecios"
    title="SYM Precios - Captura" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1SYMPrecios" runat="Server">

    <!--PAGE TITLE-->
<!--CONTENT CONTAINER-->
<div id="titulo-pagina">
    Formato captura de información</div>
        <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server" scriptMode="Release"/>
                        
                <table style="width: 99%">
                    <tr><td style="text-align: right"><img alt="" src="../../../../Img/arrow.gif" /><asp:HyperLink ID="lnkRegresar" runat="server" NavigateUrl="~/sitios/SYM/SYMPrecios/Captura/RutaSYMPrecios.aspx">Regresar</asp:HyperLink></td>
                    </tr>
                    <tr><td>Cadena: <asp:Label ID="lblCadena" runat="server" 
                            Font-Bold="True"></asp:Label></td>
                    </tr>
                    <tr><td>&nbsp;</td>
                    </tr>
                 </table>
                 
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    
                        <asp:Menu ID="Menu1" Width="168px" runat="server" Orientation="Horizontal" 
                            StaticEnableDefaultPopOutImage="False" OnMenuItemClick="Menu1_MenuItemClick">
                            <Items>
                                <asp:MenuItem ImageUrl="../../Img/selectedtabJT.GIF" Text=" " Value="0"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabJL.GIF" Text=" " Value="1"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabDC.GIF" Text=" " Value="2"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabDR.GIF" Text=" " Value="3"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabDM.GIF" Text=" " Value="4"></asp:MenuItem>   
                            </Items>
                        </asp:Menu> 
                        
                        <asp:Menu ID="Menu2" Width="168px" runat="server" Orientation="Horizontal" 
                            StaticEnableDefaultPopOutImage="False" OnMenuItemClick="Menu1_MenuItemClick">
                            <Items>                             
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabL.GIF" Text=" " Value="5"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabLC.GIF" Text=" " Value="6"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabLM.GIF" Text=" " Value="7"></asp:MenuItem>   
                            </Items>
                        </asp:Menu> 
                          
                        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                            <asp:View ID="Tab1" runat="server">
                                            <asp:GridView ID="gridProductos1" runat="server" AutoGenerateColumns="False" 
                                                Caption="Jabón de tocador" CssClass="grid-view" DataKeyNames="id_producto" 
                                                ShowFooter="True" Visible="true" Width="100%">
                                                <RowStyle HorizontalAlign="Left" />
                                                <Columns>
                                                    <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                                    <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                                    <asp:BoundField DataField="presentacion" HeaderText="Gramaje" />
                                                    <asp:BoundField DataField="tipo_producto" HeaderText="Tipo_producto" />
                                                    <asp:TemplateField HeaderText="Precio">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtPrecio" runat="server" MaxLength="6" 
                                                                Text='<%#DataBinder.Eval(Container.DataItem, "precio") %>' Width="50"></asp:TextBox>
                                                            <asp:RangeValidator ID="rvaPrecio" runat="server" ControlToValidate="txtPrecio" 
                                                                Display="Dynamic" 
                                                                ErrorMessage="El precio del producto indicado con un (*) de la sección 'Jabón de tocador' es mayor al permitido" 
                                                                MaximumValue='<%# DataBinder.Eval(Container, "DataItem.precio_max")%>' 
                                                                MinimumValue="0" Type="Double" ValidationGroup="SYM">*</asp:RangeValidator>
                                                            <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" 
                                                                ControlToValidate="txtPrecio" 
                                                                ErrorMessage="Completa el precio del producto indicado con un (*) de la sección 'Jabón de tocador'" 
                                                                ValidationGroup="SYM">*</asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="top" Width="0%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grid-footer" />
                                                <EmptyDataTemplate>
                                                    <h1>
                                                        No hay datos</h1>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                    
                            </asp:View>
                            <asp:View ID="Tab2" runat="server">                              
                                            <asp:GridView ID="gridProductos2" runat="server" AutoGenerateColumns="False" 
                                                Caption="Jabón de lavandería" CssClass="grid-view" DataKeyNames="id_producto" 
                                                ShowFooter="True" Width="100%">
                                                <RowStyle HorizontalAlign="Left" />
                                                <Columns>
                                                    <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                                    <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                                    <asp:BoundField DataField="presentacion" HeaderText="Gramaje" />
                                                    <asp:BoundField DataField="tipo_producto" HeaderText="Tipo_producto" />
                                                    <asp:TemplateField HeaderText="Precio">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtPrecio" runat="server" MaxLength="6" 
                                                                Text='<%#DataBinder.Eval(Container.DataItem, "precio") %>' Width="80"></asp:TextBox>
                                                            <asp:RangeValidator ID="rvaPrecio" runat="server" ControlToValidate="txtPrecio" 
                                                                Display="Dynamic" 
                                                                ErrorMessage="El precio del producto indicado con un (*) de la sección 'Jabón de lavandería' es mayor al permitido" 
                                                                MaximumValue='<%# DataBinder.Eval(Container, "DataItem.precio_max")%>' 
                                                                MinimumValue="0" Type="Double" ValidationGroup="SYM">*</asp:RangeValidator>
                                                            <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" 
                                                                ControlToValidate="txtPrecio" 
                                                                ErrorMessage="Completa el precio del producto indicado con un (*) de la sección 'Jabón de lavandería'" 
                                                                ValidationGroup="SYM">*</asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="top" Width="0%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grid-footer" />
                                                <EmptyDataTemplate>
                                                    <h1>
                                                        No hay datos</h1>
                                                </EmptyDataTemplate>
                                            </asp:GridView>

                            </asp:View>
                            <asp:View ID="Tab3" runat="server">
                                            <asp:GridView ID="gridProductos3" runat="server" AutoGenerateColumns="False" 
                                                Caption="Detergente concentrado" CssClass="grid-view" 
                                                DataKeyNames="id_producto" ShowFooter="True" Width="100%">
                                                <RowStyle HorizontalAlign="Left" />
                                                <Columns>
                                                    <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                                    <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                                    <asp:BoundField DataField="presentacion" HeaderText="Gramaje" />
                                                    <asp:BoundField DataField="tipo_producto" HeaderText="Tipo_producto" />
                                                    <asp:TemplateField HeaderText="Precio">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtPrecio" runat="server" MaxLength="6" 
                                                                Text='<%#DataBinder.Eval(Container.DataItem, "precio") %>' Width="80"></asp:TextBox>
                                                            <asp:RangeValidator ID="rvaPrecio" runat="server" ControlToValidate="txtPrecio" 
                                                                Display="Dynamic" 
                                                                ErrorMessage="El precio del producto indicado con un (*) de la sección 'Detergente concentrado' es mayor al permitido" 
                                                                MaximumValue='<%# DataBinder.Eval(Container, "DataItem.precio_max")%>' 
                                                                MinimumValue="0" Type="Double" ValidationGroup="SYM">*</asp:RangeValidator>
                                                            <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" 
                                                                ControlToValidate="txtPrecio" 
                                                                ErrorMessage="Completa el precio del producto indicado con un (*) de la sección 'Detergente concentrado'" 
                                                                ValidationGroup="SYM">*</asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="top" Width="0%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grid-footer" />
                                                <EmptyDataTemplate>
                                                    <h1>
                                                        No hay datos</h1>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                            </asp:View>
                            <asp:View ID="Tab4" runat="server">
                                <asp:GridView ID="gridProductos4" runat="server" AutoGenerateColumns="False" 
                                    Caption="Detergente regular" CssClass="grid-view" DataKeyNames="id_producto" 
                                    ShowFooter="True" Width="100%">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                        <asp:BoundField DataField="presentacion" HeaderText="Gramaje" />
                                        <asp:BoundField DataField="tipo_producto" HeaderText="Tipo_producto" />
                                        <asp:TemplateField HeaderText="Precio">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPrecio" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "precio") %>' Width="80"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaPrecio" runat="server" ControlToValidate="txtPrecio" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El precio del producto indicado con un (*) de la sección 'Detergente regular' es mayor al permitido" 
                                                    MaximumValue='<%# DataBinder.Eval(Container, "DataItem.precio_max")%>' 
                                                    MinimumValue="0" Type="Double" ValidationGroup="SYM">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" 
                                                    ControlToValidate="txtPrecio" 
                                                    ErrorMessage="Completa el precio del producto indicado con un (*) de la sección 'Detergente regular'" 
                                                    ValidationGroup="SYM">*</asp:RequiredFieldValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                    <EmptyDataTemplate>
                                        <h1>
                                            No hay datos</h1>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="Tab5" runat="server">
                                <asp:GridView ID="gridProductos5" runat="server" AutoGenerateColumns="False" 
                                    Caption="Detergentes multiusos" CssClass="grid-view" DataKeyNames="id_producto" 
                                    ShowFooter="True" Width="100%">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                        <asp:BoundField DataField="presentacion" HeaderText="Gramaje" />
                                        <asp:BoundField DataField="tipo_producto" HeaderText="Tipo_producto" />
                                        <asp:TemplateField HeaderText="Precio">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPrecio" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "precio") %>' Width="80"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaPrecio" runat="server" ControlToValidate="txtPrecio" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El precio del producto indicado con un (*) de la sección 'Detergentes multiusos' es mayor al permitido"  
                                                    MaximumValue='<%# DataBinder.Eval(Container, "DataItem.precio_max")%>' 
                                                    MinimumValue="0" Type="Double" ValidationGroup="SYM">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" 
                                                    ControlToValidate="txtPrecio" 
                                                    ErrorMessage="Completa el precio del producto indicado con un (*) de la sección 'Detergentes multiusos'" 
                                                    ValidationGroup="SYM">*</asp:RequiredFieldValidator>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>                                    
                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                    <EmptyDataTemplate>
                                        <h1>
                                            No hay datos</h1>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="Tab6" runat="server">
                                <asp:GridView ID="gridProductos6" runat="server" AutoGenerateColumns="False" 
                                    Caption="Lavatrastes" CssClass="grid-view" DataKeyNames="id_producto" 
                                    ShowFooter="True" Width="100%">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                        <asp:BoundField DataField="presentacion" HeaderText="Gramaje" />
                                        <asp:BoundField DataField="tipo_producto" HeaderText="Tipo_producto" />
                                        <asp:TemplateField HeaderText="Precio">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPrecio" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "precio") %>' Width="80"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaPrecio" runat="server" ControlToValidate="txtPrecio" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El precio del producto indicado con un (*) de la sección 'Lavatrastes' es mayor al permitido"  
                                                    MaximumValue='<%# DataBinder.Eval(Container, "DataItem.precio_max")%>' 
                                                    MinimumValue="0" Type="Double" ValidationGroup="SYM">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" 
                                                    ControlToValidate="txtPrecio" 
                                                    ErrorMessage="Completa el precio del producto indicado con un (*) de la sección 'Lavatrastes'" 
                                                    ValidationGroup="SYM">*</asp:RequiredFieldValidator> 
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                    <EmptyDataTemplate>
                                        <h1>
                                            No hay datos</h1>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="Tab7" runat="server">
                                <asp:GridView ID="gridProductos7" runat="server" AutoGenerateColumns="False" 
                                    Caption="Liquido corporal" CssClass="grid-view" DataKeyNames="id_producto" 
                                    ShowFooter="True" Width="100%">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                        <asp:BoundField DataField="presentacion" HeaderText="Gramaje" />
                                        <asp:BoundField DataField="tipo_producto" HeaderText="Tipo_producto" />
                                        <asp:TemplateField HeaderText="Precio">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPrecio" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "precio") %>' Width="80"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaPrecio" runat="server" ControlToValidate="txtPrecio" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El precio del producto indicado con un (*) de la sección 'Lavavajillas' es mayor al permitido"  
                                                    MaximumValue='<%# DataBinder.Eval(Container, "DataItem.precio_max")%>' 
                                                    MinimumValue="0" Type="Double" ValidationGroup="SYM">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" 
                                                    ControlToValidate="txtPrecio" 
                                                    ErrorMessage="Completa el precio del producto indicado con un (*) de la sección 'Lavavajillas'" 
                                                    ValidationGroup="SYM">*</asp:RequiredFieldValidator> 
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                    <EmptyDataTemplate>
                                        <h1>
                                            No hay datos</h1>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="Tab8" runat="server">
                                <asp:GridView ID="gridProductos8" runat="server" AutoGenerateColumns="False" 
                                    Caption="Liquido manos" CssClass="grid-view" DataKeyNames="id_producto" 
                                    ShowFooter="True" Width="100%">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                        <asp:BoundField DataField="presentacion" HeaderText="Gramaje" />
                                        <asp:BoundField DataField="tipo_producto" HeaderText="Tipo_producto" />
                                        <asp:TemplateField HeaderText="Precio">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPrecio" runat="server" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "precio") %>' Width="80"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaPrecio" runat="server" ControlToValidate="txtPrecio" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="El precio del producto indicado con un (*) de la sección 'Quitamanchas' es mayor al permitido"   
                                                    MaximumValue='<%# DataBinder.Eval(Container, "DataItem.precio_max")%>' 
                                                    MinimumValue="0" Type="Double" ValidationGroup="SYM">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" 
                                                    ControlToValidate="txtPrecio" 
                                                    ErrorMessage="Completa el precio del producto indicado con un (*) de la sección 'Quitamanchas'" 
                                                    ValidationGroup="SYM">*</asp:RequiredFieldValidator> 
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="top" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                    <EmptyDataTemplate>
                                        <h1>
                                            No hay datos</h1>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </asp:View>
                        </asp:MultiView>
                        <br />
                        
                        </ContentTemplate>  
              </asp:UpdatePanel>   
                        
                        
                        <table style="border-style: groove; width: 99%; height: 99px; margin-right: 0px;" 
                width="100%" >
                            <tr>
                                <td style="text-align: center; height: 31px; color: #FFFFFF;" bgcolor="#003399">
                                    Comentarios</td>
                            </tr>
                            <tr>
                                <td style="text-align: center; height: 31px; ">
                                    <asp:TextBox ID="txtComentario_General" runat="server" Height="60px" 
                                        MaxLength="700" TextMode="MultiLine" Width="635px"></asp:TextBox>
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
        <div id="content-side-two-column">
            <table style="width: 100%">
                <tr>
                    <td bgcolor="#99FF33" style="width: 56px">
                        &nbsp;</td>
                    <td>
                        Productos Propios </td></tr></table>
        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>

