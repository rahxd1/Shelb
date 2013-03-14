<%@ Page Language="vb" 
    Culture="es-MX" 
    MasterPageFile="~/sitios/Fluidmaster/Fluidmaster.Master" 
    AutoEventWireup="false" 
    CodeBehind="FormatoDistribuidorFluid.aspx.vb" 
    Inherits="procomlcd.FormatoDistribuidorFluid"
    title="Fluidmaster - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoFluid" runat="Server">
    <!--PAGE TITLE-->
<div id="titulo-pagina">Formato captura</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">

<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"/>

                    <table style="width: 100%">
                        <tr>
                                    <td __designer:mapid="1e0" style="width: 78px">
                                        &nbsp;</td>
                                    <td __designer:mapid="1e1" style="text-align: right">
                                <img alt="" src="../../../Img/arrow.gif" /><asp:HyperLink ID="lnkRegresar" runat="server" 
                                    NavigateUrl="~/sitios/Fluidmaster/Captura/RutasFluid.aspx">Regresar</asp:HyperLink>
                                    </td>
                                </tr>
                        <tr>
                                    <td __designer:mapid="1e0" style="width: 78px; text-align: right">
                                        Distribuidor</td>
                                    <td __designer:mapid="1e1">
                                        <asp:Label ID="lblDistribuidor" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                        <tr>
                                    <td __designer:mapid="1e0" style="width: 78px; text-align: right">
                                        Tienda</td>
                                    <td __designer:mapid="1e1">
                                        <asp:DropDownList ID="cmbTienda" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvTienda" runat="server" 
                                            ControlToValidate="cmbTienda" ErrorMessage="Selecciona la tienda" ValidationGroup="Fluidmaster">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                        <tr><td style="text-align: right; width: 78px;">
                                &nbsp;</td>
                        </tr>
                        </table>
                    
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                
                            <asp:GridView ID="gridProductos" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="id_producto" Width="100%" ShowFooter="True" 
                                    GridLines="Horizontal"   
                                    Caption="Productos Fluidmaster" CssClass="grid-view-ruta">
                                    <Columns>
                                        <asp:BoundField HeaderText="Producto" DataField="nombre_producto" />
                                        <asp:BoundField HeaderText="Modelo" DataField="codigo" />
                                        <asp:BoundField HeaderText="Caja master" DataField="caja_master" />
                                        
                                        <asp:TemplateField HeaderText="Catalogado">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCatalogado" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "catalogado")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="en piso">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPiso" runat="server" MaxLength="2" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Piso") %>' Width="35"></asp:TextBox> piezas
                                                <asp:RangeValidator ID="rvaPiso" runat="server" 
                                                    ControlToValidate="txtPiso" Display="Dynamic" 
                                                    ErrorMessage="La cantidad de producto en piso supera al limite permitido" MaximumValue="99" 
                                                    MinimumValue="0" Type="Double" ValidationGroup="Fluidmaster">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvPiso" runat="server" 
                                                    ControlToValidate="txtPiso" 
                                                    ErrorMessage="Indica cantidad de producto en piso" 
                                                    ValidationGroup="Fluidmaster">*</asp:RequiredFieldValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="en bodega">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtBodega" runat="server" MaxLength="3" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Bodega") %>' Width="35"></asp:TextBox> piezas
                                                <asp:RangeValidator ID="rvaBodega" runat="server" 
                                                    ControlToValidate="txtBodega" Display="Dynamic" 
                                                    ErrorMessage="La cantidad de producto en bodega supera al limite permitido" MaximumValue="999" 
                                                    MinimumValue="0" Type="Double" ValidationGroup="Fluidmaster">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvBodega" runat="server" 
                                                    ControlToValidate="txtBodega" 
                                                    ErrorMessage="Indica cantidad de producto en bodega" 
                                                    ValidationGroup="Fluidmaster">*</asp:RequiredFieldValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                    <EmptyDataTemplate>
                                        <h1>Se ha producido un error</h1>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            
                            <br />
                            
                               <asp:GridView ID="gridFrentes" runat="server" AutoGenerateColumns="False" 
                                    GridLines="Horizontal"   
                                    Caption="Cantidad de frentes" CssClass="grid-view-ruta" DataKeyNames="id_marca" 
                                    ShowFooter="True" Width="50%">
                                    <Columns>
                                        <asp:BoundField DataField="nombre_marca" HeaderText="" />
                                        <asp:TemplateField HeaderText="ganchos">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtGanchos" runat="server" MaxLength="2" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Ganchos") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaGanchos" runat="server" 
                                                    ControlToValidate="txtGanchos" Display="Dynamic" 
                                                    ErrorMessage="La cantidad de ganchos supera al limite permitido" MaximumValue="100" 
                                                    MinimumValue="0" Type="Double" ValidationGroup="Fluidmaster">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvGanchos" runat="server" 
                                                    ControlToValidate="txtGanchos" 
                                                    ErrorMessage="Indica cantidad de ganchos" 
                                                    ValidationGroup="Fluidmaster">*</asp:RequiredFieldValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="charolas">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCharolas" runat="server" MaxLength="2" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Charolas") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaCharolas" runat="server" 
                                                    ControlToValidate="txtCharolas" Display="Dynamic" 
                                                    ErrorMessage="La cantidad de charolas supera al limite permitido" MaximumValue="100" 
                                                    MinimumValue="0" Type="Double" ValidationGroup="Fluidmaster">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvCharolas" runat="server" 
                                                    ControlToValidate="txtCharolas" 
                                                    ErrorMessage="Indica cantidad de charolas" 
                                                    ValidationGroup="Fluidmaster">*</asp:RequiredFieldValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                    <EmptyDataTemplate>
                                        <h1>
                                            Se ha producido un error</h1>
                                    </EmptyDataTemplate>
                              </asp:GridView>
                                
                               <br />
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <asp:GridView ID="gridExhibiciones" runat="server" AutoGenerateColumns="False" 
                                            GridLines="Horizontal"   
                                            Caption="Exhibidores Fluidmaster" CssClass="grid-view-ruta" 
                                            DataKeyNames="id_exhibidor" ShowFooter="True" Width="80%">
                                            <Columns>
                                                <asp:BoundField DataField="nombre_exhibidor" HeaderText="Exhibiciones" />
                                                <asp:TemplateField HeaderText="cantidad">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCantidad" runat="server" MaxLength="1" 
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "Cantidad") %>' Width="50"></asp:TextBox>
                                                        <asp:RangeValidator ID="rvaCantidad" runat="server" 
                                                            ControlToValidate="txtCantidad" Display="Dynamic" 
                                                            ErrorMessage="La cantidad de exhibidores de fluidmaster supera al limite permitido" 
                                                            MaximumValue="9" MinimumValue="0" Type="Double" ValidationGroup="Fluidmaster">*</asp:RangeValidator>
                                                        <asp:RequiredFieldValidator ID="rfvCantidad" runat="server" 
                                                            ControlToValidate="txtCantidad" 
                                                            ErrorMessage="Indica cantidad de exhibidores de fluidmaster" 
                                                            ValidationGroup="Fluidmaster">*</asp:RequiredFieldValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="grid-footer" />
                                            <EmptyDataTemplate>
                                                <h1>
                                                    Se ha producido un error</h1>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:GridView ID="gridExhibicionesCompetencia" runat="server" 
                                            GridLines="Horizontal"   
                                            AutoGenerateColumns="False" Caption="Exhibiciones competencia" 
                                            CssClass="grid-view-ruta" DataKeyNames="id_marca" ShowFooter="True" Width="80%">
                                            <Columns>
                                                <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                                <asp:TemplateField HeaderText="cantidad">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCantidad" runat="server" MaxLength="1" 
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "Cantidad") %>' Width="50"></asp:TextBox>
                                                        <asp:RangeValidator ID="rvaCantidad" runat="server" 
                                                            ControlToValidate="txtCantidad" Display="Dynamic" 
                                                            ErrorMessage="La cantidad de exhibidores de la competencia supera al limite permitido" 
                                                            MaximumValue="9" MinimumValue="0" Type="Double" ValidationGroup="Fluidmaster">*</asp:RangeValidator>
                                                        <asp:RequiredFieldValidator ID="rfvCantidad" runat="server" 
                                                            ControlToValidate="txtCantidad" 
                                                            ErrorMessage="Indica cantidad de exhibidores de la competencia" 
                                                            ValidationGroup="Fluidmaster">*</asp:RequiredFieldValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns> 
                                            <FooterStyle CssClass="grid-footer" />
                                            <EmptyDataTemplate>
                                                <h1>
                                                    Se ha producido un error</h1>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                                
                               <br />
                            
                                <asp:GridView ID="gridMaterialPOP" runat="server" AutoGenerateColumns="False"
                                    GridLines="Horizontal" CssClass="grid-view-ruta" DataKeyNames="id_material" 
                                    ShowFooter="True" Width="57%">
                                    <Columns>
                                        <asp:BoundField DataField="nombre_material" HeaderText="Material POP" />
                                        <asp:TemplateField HeaderText="cantidad">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCantidad" runat="server" MaxLength="4" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Cantidad") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaCantidad" runat="server" 
                                                    ControlToValidate="txtCantidad" Display="Dynamic" 
                                                    ErrorMessage="La cantidad de material supera al limite permitido" 
                                                    MaximumValue='<%# DataBinder.Eval(Container, "DataItem.maximo")%>'
                                                    MinimumValue="0" Type="Double" ValidationGroup="Fluidmaster">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvCantidad" runat="server" 
                                                    ControlToValidate="txtCantidad" 
                                                    ErrorMessage="Indica cantidad de material POP" 
                                                    ValidationGroup="Fluidmaster">*</asp:RequiredFieldValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                    <EmptyDataTemplate>
                                        <h1>
                                            Se ha producido un error</h1>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                
                            <br />
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        Tipo comentarios</td>
                                    <td>
                                        <asp:DropDownList ID="cmbTipoComentario" runat="server" />
                                        <asp:RequiredFieldValidator ID="rfvTipoComentario" runat="server" 
                                            ControlToValidate="cmbTipoComentario" 
                                            ErrorMessage="Selecciona el tipo de comentarios" ValidationGroup="Fluidmaster">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Comentarios</td>
                                    <td>
                                        <asp:TextBox ID="txtComentarios" runat="server" Height="113px" 
                                            TextMode="MultiLine" Width="523px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvComentarios" runat="server" 
                                            ControlToValidate="txtComentarios" 
                                            ErrorMessage="Completa la información comentarios" ValidationGroup="Fluidmaster">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                                
                </ContentTemplate>
            </asp:UpdatePanel>                                    
                
            
                    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table style="width: 100%">
                               <tr>
                                   <td style="text-align: center; height: 31px; " colspan="2">
                                   
                                       <asp:ValidationSummary ID="vsFluidmaster" runat="server" 
                                           ValidationGroup="Fluidmaster" ShowMessageBox="True" ShowSummary="False" />
                                   <asp:UpdateProgress ID="UpdateProgress" runat="server" 
                                           AssociatedUpdatePanelID="UpdatePanel" DisplayAfter="0">
                                           <ProgressTemplate>
                                               <div id="pnlSave" >
                                                   <img alt="" src="../../../Img/loading.gif" />La información se esta guardando<br />
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
                                            ValidationGroup="Fluidmaster" CssClass="button" />
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
