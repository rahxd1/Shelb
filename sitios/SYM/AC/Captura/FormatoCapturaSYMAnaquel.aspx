<%@ Page Language="vb" 
    Culture="es-MX" 
    MasterPageFile="~/sitios/SYM/AC/SYMAC.Master" 
    AutoEventWireup="false" 
    CodeBehind="FormatoCapturaSYMAnaquel.aspx.vb" 
    Inherits="procomlcd.FormatoCapturaSYMAnaquel"
    title="SYM Anaquel - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSYMAC" runat="Server">
    <!--PAGE TITLE-->
<!--CONTENT CONTAINER-->
<div id="titulo-pagina">
    Anaquel</div>
        <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server"/>
                <table style="width: 99%">
                    <tr><td style="width: 396px">&nbsp;</td>
                        <td align="right"><img alt="" src="../../../../Img/arrow.gif" /><asp:HyperLink ID="lnkRegresar" runat="server" NavigateUrl="~/sitios/SYM/AC/Captura/RutaSYMAC.aspx">Regresar</asp:HyperLink></td>
                    </tr>
                    </table>
                
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Menu ID="Menu1" Width="168px" runat="server" Orientation="Horizontal" 
                            StaticEnableDefaultPopOutImage="False" OnMenuItemClick="Menu1_MenuItemClick">
                            <Items>
                                <asp:MenuItem ImageUrl="../../Img/selectedtabDM.GIF" Text=" " Value="0"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabJL.GIF" Text=" " Value="1"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabJT.GIF" Text=" " Value="2"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabLL.GIF" Text=" " Value="3"></asp:MenuItem> 
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabL.GIF" Text=" " Value="4"></asp:MenuItem>  
                            </Items>
                        </asp:Menu> 
                        
                        <asp:Menu ID="Menu2" Width="168px" runat="server" Orientation="Horizontal" 
                            StaticEnableDefaultPopOutImage="False" OnMenuItemClick="Menu1_MenuItemClick">
                            <Items>                             
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabLM.GIF" Text=" " Value="5"></asp:MenuItem> 
                                <asp:MenuItem ImageUrl="../../Img/unselectedtabLC.GIF" Text=" " Value="6"></asp:MenuItem> 
                            </Items>
                        </asp:Menu> 
                        
                        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                            <asp:View ID="Tab1" runat="server">
                                <asp:GridView ID="gridProductos1" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="id_linea" Width="100%"  ShowFooter="True" 
                                     Visible="False" CssClass="grid-view">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>                            
                                        <asp:BoundField HeaderText="Marca" DataField="id_empresa" />
                                        <asp:BoundField HeaderText="Marca empresa" DataField="nombre_empresa" />
                                        <asp:BoundField HeaderText="Producto" DataField="nombre_linea" />
                                        <asp:TemplateField HeaderText="Frentes">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtFrentes" runat="server" Width="50" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "Frentes") %>'></asp:TextBox>
                                                <asp:RangeValidator id="rvaFrentes" runat="server" 
                                                    ErrorMessage="La cantidad de frentes del producto indicado con un (*) en la sección 'Detergente multiusos' supera el máximo permitido" Display="Dynamic" 
                                                    MinimumValue="0" MaximumValue="9999" Type="Double" 
                                                    ControlToValidate="txtFrentes" ValidationGroup="SYM">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvFrentes" runat="server" 
                                                    ControlToValidate="txtFrentes" 
                                                    ErrorMessage="Completa la cantidad de frentes del producto indicado con un (*) en la seccion 'Detergente multiusos'" 
                                                    ValidationGroup="SYM">*</asp:RequiredFieldValidator>
                                            </ItemTemplate>
                                            <ItemStyle Width="0%" VerticalAlign="top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                    <EmptyDataTemplate>
                                        <h1>No hay información para capturar</h1>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </asp:View>
                            
                            <asp:View ID="Tab2" runat="server">
                                <asp:GridView ID="gridProductos2" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="id_linea" Width="100%" 
                                    CssClass="grid-view" ShowFooter="True" 
                                     Visible="False">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Marca" DataField="id_empresa" />
                                        <asp:BoundField HeaderText="Marca empresa" DataField="nombre_empresa" />
                                        <asp:BoundField HeaderText="Producto" DataField="nombre_linea" />
                                        <asp:TemplateField HeaderText="Frentes">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtFrentes" runat="server" Width="50" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "Frentes") %>'></asp:TextBox>
                                                <asp:RangeValidator id="rvaFrentes" runat="server" 
                                                    ErrorMessage="La cantidad de frentes del producto indicado con un (*) en la sección 'Jabón de lavandería' supera el máximo permitido" Display="Dynamic" 
                                                    MinimumValue="0" MaximumValue="9999" Type="Double" 
                                                    ControlToValidate="txtFrentes" ValidationGroup="SYM">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvFrentes" runat="server" 
                                                    ControlToValidate="txtFrentes" 
                                                    ErrorMessage="Completa la cantidad de frentes del producto indicado con un (*) en la seccion 'Jabón de lavandería'" 
                                                    ValidationGroup="SYM">*</asp:RequiredFieldValidator>
                                            </ItemTemplate>
                                            <ItemStyle Width="0%" VerticalAlign="top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                    <EmptyDataTemplate>
                                        <h1>No hay información para capturar</h1>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </asp:View>
                            
                            <asp:View ID="Tab3" runat="server">
                                <asp:GridView ID="gridProductos3" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="id_linea" Width="100%" 
                                    CssClass="grid-view" ShowFooter="True"  Visible="False">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Marca" DataField="id_empresa" />
                                        <asp:BoundField HeaderText="Marca empresa" DataField="nombre_empresa" />
                                        <asp:BoundField HeaderText="Producto" DataField="nombre_linea" />
                                        <asp:TemplateField HeaderText="Frentes">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtFrentes" runat="server" Width="50" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "Frentes") %>'></asp:TextBox>
                                                <asp:RangeValidator id="rvaFrentes" runat="server" 
                                                    ErrorMessage="La cantidad de frentes del producto indicado con un (*) en la sección 'Jabón de tocador' supera el máximo permitido" Display="Dynamic" 
                                                    MinimumValue="0" MaximumValue="9999" Type="Double" 
                                                    ControlToValidate="txtFrentes" ValidationGroup="SYM">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvFrentes" runat="server" 
                                                    ControlToValidate="txtFrentes" 
                                                    ErrorMessage="Completa la cantidad de frentes del producto indicado con un (*) en la seccion 'Jabón de tocador'" 
                                                    ValidationGroup="SYM">*</asp:RequiredFieldValidator>
                                            </ItemTemplate>
                                            <ItemStyle Width="0%" VerticalAlign="top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                    <EmptyDataTemplate>
                                        <h1>No hay información para capturar</h1>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </asp:View>
                            
                            <asp:View ID="Tab4" runat="server">
                                <asp:GridView ID="gridProductos4" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="id_linea" Width="100%" 
                                    CssClass="grid-view" ShowFooter="True" 
                                     Visible="False">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Marca" DataField="id_empresa" />
                                        <asp:BoundField HeaderText="Marca empresa" DataField="nombre_empresa" />
                                        <asp:BoundField HeaderText="Producto" DataField="nombre_linea" />
                                        <asp:TemplateField HeaderText="Frentes">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtFrentes" runat="server" Width="50" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "Frentes") %>'></asp:TextBox>
                                                <asp:RangeValidator id="rvaFrentes" runat="server" 
                                                    ErrorMessage="La cantidad de frentes del producto indicado con un (*) en la sección 'Lavandería líquido' supera el máximo permitido" Display="Dynamic" 
                                                    MinimumValue="0" MaximumValue="9999" Type="Double" 
                                                    ControlToValidate="txtFrentes" ValidationGroup="SYM">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvFrentes" runat="server" 
                                                    ControlToValidate="txtFrentes" 
                                                    ErrorMessage="Completa la cantidad de frentes del producto indicado con un (*) en la seccion 'Lavandería líquido'" 
                                                    ValidationGroup="SYM">*</asp:RequiredFieldValidator>
                                            </ItemTemplate>
                                            <ItemStyle Width="0%" VerticalAlign="top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                    <EmptyDataTemplate>
                                        <h1>No hay información para capturar</h1>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </asp:View>
                            
                            <asp:View ID="Tab5" runat="server">
                                <asp:GridView ID="gridProductos5" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="id_linea" Width="100%" 
                                    CssClass="grid-view" ShowFooter="True"  
                                    Visible="False">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Marca" DataField="id_empresa" />
                                        <asp:BoundField HeaderText="Marca empresa" DataField="nombre_empresa" />
                                        <asp:BoundField HeaderText="Producto" DataField="nombre_linea" />
                                        <asp:TemplateField HeaderText="Frentes">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtFrentes" runat="server" Width="50" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "Frentes") %>'></asp:TextBox>
                                                <asp:RangeValidator id="rvaFrentes" runat="server" 
                                                    ErrorMessage="La cantidad de frentes del producto indicado con un (*) en la sección 'Lavatrastes' supera el máximo permitido" Display="Dynamic" 
                                                    MinimumValue="0" MaximumValue="9999" Type="Double" 
                                                    ControlToValidate="txtFrentes" ValidationGroup="SYM">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvFrentes" runat="server" 
                                                    ControlToValidate="txtFrentes" 
                                                    ErrorMessage="Completa la cantidad de frentes del producto indicado con un (*) en la seccion 'Lavatrastes'" 
                                                    ValidationGroup="SYM">*</asp:RequiredFieldValidator>
                                            </ItemTemplate>
                                            <ItemStyle Width="0%" VerticalAlign="top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                    <EmptyDataTemplate>
                                        <h1>No hay información para capturar</h1>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </asp:View>
                            
                            <asp:View ID="Tab6" runat="server">
                                <asp:GridView ID="gridProductos6" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="id_linea" Width="100%" 
                                    ShowFooter="True" CssClass="grid-view" Visible="False">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Marca" DataField="id_empresa" />
                                        <asp:BoundField HeaderText="Marca empresa" DataField="nombre_empresa" />
                                        <asp:BoundField HeaderText="Producto" DataField="nombre_linea" />
                                        <asp:TemplateField HeaderText="Frentes">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtFrentes" runat="server" Width="50" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "Frentes") %>'></asp:TextBox>
                                                <asp:RangeValidator id="rvaFrentes" runat="server" 
                                                    ErrorMessage="La cantidad de frentes del producto indicado con un (*) en la sección 'Líquido manos' supera el máximo permitido" Display="Dynamic" 
                                                    MinimumValue="0" MaximumValue="9999" Type="Double" 
                                                    ControlToValidate="txtFrentes" ValidationGroup="SYM">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvFrentes" runat="server" 
                                                    ControlToValidate="txtFrentes" 
                                                    ErrorMessage="Completa la cantidad de frentes del producto indicado con un (*) en la seccion 'Líquido manos'" 
                                                    ValidationGroup="SYM">*</asp:RequiredFieldValidator>
                                            </ItemTemplate>
                                            <ItemStyle Width="0%" VerticalAlign="top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                    <EmptyDataTemplate>
                                        <h1>No hay información para capturar</h1>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </asp:View>
                            
                            <asp:View ID="Tab7" runat="server">
                                <asp:GridView ID="gridProductos7" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="id_linea" Width="100%" 
                                    ShowFooter="True" CssClass="grid-view" Visible="False">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Marca" DataField="id_empresa" />
                                        <asp:BoundField HeaderText="Marca empresa" DataField="nombre_empresa" />
                                        <asp:BoundField HeaderText="Producto" DataField="nombre_linea" />
                                        <asp:TemplateField HeaderText="Frentes">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtFrentes" runat="server" Width="50" MaxLength="6" 
                                                    Text='<%#DataBinder.Eval(Container.DataItem, "Frentes") %>'></asp:TextBox>
                                                <asp:RangeValidator id="rvaFrentes" runat="server" 
                                                    ErrorMessage="La cantidad de frentes del producto indicado con un (*) en la sección 'Líquido corporal' supera el máximo permitido" Display="Dynamic" 
                                                    MinimumValue="0" MaximumValue="9999" Type="Double" 
                                                    ControlToValidate="txtFrentes" ValidationGroup="SYM">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvFrentes" runat="server" 
                                                    ControlToValidate="txtFrentes" 
                                                    ErrorMessage="Completa la cantidad de frentes del producto indicado con un (*) en la seccion 'Líquido corporal'" 
                                                    ValidationGroup="SYM">*</asp:RequiredFieldValidator>
                                            </ItemTemplate>
                                            <ItemStyle Width="0%" VerticalAlign="top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                    <EmptyDataTemplate>
                                        <h1>No hay información para capturar</h1>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </asp:View>
                        </asp:MultiView>    
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
                        
                <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table style="border-style: groove; width: 99%; height: 99px; margin-right: 0px;" >
                            <tr>
                                <td style="text-align: center; height: 31px; color: #FFFFFF;" bgcolor="#003399" 
                                    colspan="2">
                                    Comentarios</td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center; height: 31px; ">
                                    <asp:TextBox ID="txtComentario_General" runat="server" Height="60px" 
                                        MaxLength="700" TextMode="MultiLine" Width="635px"></asp:TextBox>
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
                                <td style="text-align: center; height: 31px; width: 354px;">
                                    <asp:Button ID="btnGuardar" runat="server" CssClass="button" Text="Guardar" 
                                        ValidationGroup="SYM" />
                                </td>
                                <td style="text-align: center; height: 31px; width: 354px;">
                                    <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" 
                                        CssClass="button" Text="Cancelar" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                                
            </div>    
        
 <!--CONTENT SIDE COLUMN AVISOS -->
        <div id="content-side-two-column">
            <table style="width: 100%">
                <tr>
                    <td bgcolor="#99FF33" style="width: 56px">
                        &nbsp;</td>
                    <td>
                        Productos Propios</td>
                </tr>
            </table>
        </div>
        
        <div class="clear">
        </div>
    </div>
</asp:Content>
