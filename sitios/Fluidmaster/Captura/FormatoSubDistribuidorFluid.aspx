<%@ Page Language="vb" 
    Culture="es-MX" 
    MasterPageFile="~/sitios/Fluidmaster/Fluidmaster.Master" 
    AutoEventWireup="false" 
    CodeBehind="FormatoSubDistribuidorFluid.aspx.vb" 
    Inherits="procomlcd.FormatoSubDistribuidorFluid"
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
                                    <td __designer:mapid="1e1" style="text-align: right" colspan="3">
                                <img alt="" src="../../../Img/arrow.gif" /><asp:HyperLink ID="lnkRegresar" runat="server" 
                                    NavigateUrl="~/sitios/Fluidmaster/Captura/RutasFluid.aspx">Regresar</asp:HyperLink>
                                    </td>
                                </tr>
                        <tr>
                                    <td __designer:mapid="1e0" style="width: 78px; text-align: right">
                                        Distribuidor</td>
                                    <td __designer:mapid="1e1" colspan="3">
                                        <asp:Label ID="lblDistribuidor" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                        <tr>
                                    <td __designer:mapid="1e0" style="width: 78px; text-align: right">
                                        Tienda</td>
                                    <td __designer:mapid="1e1" colspan="3">
                                        <asp:TextBox ID="txtTienda" runat="server" MaxLength="50" Width="280px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvTienda" runat="server" 
                                            ControlToValidate="txtTienda" ErrorMessage="*" ValidationGroup="Fluidmaster"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                        <tr>
                                    <td __designer:mapid="1e0" style="width: 78px; text-align: right">
                                        Dirección</td>
                                    <td __designer:mapid="1e1">
                                        <asp:TextBox ID="txtDireccion" runat="server" MaxLength="50" Width="282px"></asp:TextBox>
                                    </td>
                                    <td __designer:mapid="1e1">
                                        Colonia</td>
                                    <td __designer:mapid="1e1">
                                        <asp:TextBox ID="txtColonia" runat="server" MaxLength="25" Width="206px"></asp:TextBox>
                                    </td>
                                </tr>
                        <tr>
                                    <td __designer:mapid="1e0" style="width: 78px; text-align: right">
                                        Ciudad</td>
                                    <td __designer:mapid="1e1">
                                        <asp:TextBox ID="txtCiudad" runat="server" MaxLength="25"></asp:TextBox>
                                    </td>
                                    <td __designer:mapid="1e1">
                                        Estado</td>
                                    <td __designer:mapid="1e1">
                                        <asp:DropDownList ID="cmbEstado" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                        <tr>
                                    <td __designer:mapid="1e0" style="width: 78px; text-align: right">
                                        &nbsp;</td>
                                    <td __designer:mapid="1e1">
                                        &nbsp;</td>
                                    <td __designer:mapid="1e1">
                                        Región</td>
                                    <td __designer:mapid="1e1">
                                        <asp:DropDownList ID="cmbRegion" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                        <tr>
                                    <td __designer:mapid="1e0" style="width: 78px; text-align: right">
                                        Correo</td>
                                    <td __designer:mapid="1e1">
                                        <asp:TextBox ID="txtCorreo" runat="server" MaxLength="20" Width="216px"></asp:TextBox>
                                    </td>
                                    <td __designer:mapid="1e1">
                                        Telefono</td>
                                    <td __designer:mapid="1e1">
                                        <asp:TextBox ID="txtLada" runat="server" MaxLength="3" Width="31px"></asp:TextBox>
                                        -<asp:TextBox ID="txtTelefono" runat="server" MaxLength="15"></asp:TextBox>
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
                                    Caption="Productos Fluidmaster" CssClass="grid-view">
                                    <Columns>
                                        <asp:BoundField HeaderText="Modelo" DataField="codigo" />
                                        <asp:BoundField HeaderText="Producto" DataField="nombre_producto" />
                                        <asp:BoundField HeaderText="Caja master" DataField="caja_master" />
                                        <asp:TemplateField HeaderText="pedido">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPedido" runat="server" MaxLength="2" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Pedido") %>' Width="35"></asp:TextBox> piezas
                                                <asp:RangeValidator ID="rvaPedido" runat="server" 
                                                    ControlToValidate="txtPedido" Display="Dynamic" 
                                                    ErrorMessage="En cantidad de Pedido solo se permite números" MaximumValue="99" 
                                                    MinimumValue="0" Type="Double" ValidationGroup="Fluidmaster">*</asp:RangeValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                    <EmptyDataTemplate>
                                        <h1>Se ha producido un error</h1>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            
                            <br />
                            
                               <br />
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <asp:GridView ID="gridExhibiciones" runat="server" AutoGenerateColumns="False" 
                                            Caption="Exhibidores Fluidmaster" CssClass="grid-view" 
                                            DataKeyNames="id_exhibidor" ShowFooter="True" Width="80%">
                                            <Columns>
                                                <asp:BoundField DataField="nombre_exhibidor" HeaderText="Exhibiciones" />
                                                <asp:TemplateField HeaderText="cantidad">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCantidad" runat="server" MaxLength="1" 
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "Cantidad") %>' Width="50"></asp:TextBox>
                                                        <asp:RangeValidator ID="rvaCantidad" runat="server" 
                                                            ControlToValidate="txtCantidad" Display="Dynamic" 
                                                            ErrorMessage="En cantidad de exhibidores de fluidmaster solo se permite números" 
                                                            MaximumValue="9" MinimumValue="0" Type="Double" ValidationGroup="Rubbermaid">*</asp:RangeValidator>
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
                                            AutoGenerateColumns="False" Caption="Exhibiciones competencia" 
                                            CssClass="grid-view" DataKeyNames="id_marca" ShowFooter="True" Width="80%">
                                            <Columns>
                                                <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                                <asp:TemplateField HeaderText="cantidad">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCantidad" runat="server" MaxLength="1" 
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "Cantidad") %>' Width="50"></asp:TextBox>
                                                        <asp:RangeValidator ID="rvaCantidad" runat="server" 
                                                            ControlToValidate="txtCantidad" Display="Dynamic" 
                                                            ErrorMessage="En cantidad de exhibidores de competencia solo se permite números" 
                                                            MaximumValue="9" MinimumValue="0" Type="Double" ValidationGroup="Rubbermaid">*</asp:RangeValidator>
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
                                    Caption="Material POP" CssClass="grid-view" DataKeyNames="id_material" 
                                    ShowFooter="True" Width="57%">
                                    <Columns>
                                        <asp:BoundField DataField="nombre_material" HeaderText="Material POP" />
                                        <asp:TemplateField HeaderText="cantidad">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCantidad" runat="server" MaxLength="4" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Cantidad") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaCantidad" runat="server" 
                                                    ControlToValidate="txtCantidad" Display="Dynamic" 
                                                    ErrorMessage="En cantidad de exhibidores de competencia solo se permite números" MaximumValue="1000" 
                                                    MinimumValue="0" Type="Double" ValidationGroup="Rubbermaid">*</asp:RangeValidator>
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
                                        <asp:DropDownList ID="cmbTipoComentario" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvTipoComentario" runat="server" 
                                            ControlToValidate="cmbTipoComentario" ErrorMessage="*" ValidationGroup="Fluidmaster"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Comentarios</td>
                                    <td>
                                        <asp:TextBox ID="txtComentarios" runat="server" Height="113px" 
                                            TextMode="MultiLine" Width="523px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvComentarios" runat="server" 
                                            ControlToValidate="txtComentarios" ErrorMessage="*" ValidationGroup="Fluidmaster"></asp:RequiredFieldValidator>
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
                                           ValidationGroup="Rubbermaid" ShowMessageBox="True" ShowSummary="False" />
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
