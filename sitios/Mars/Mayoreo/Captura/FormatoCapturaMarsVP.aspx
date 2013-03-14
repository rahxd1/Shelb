<%@ Page Language="vb" 
    Culture="es-MX" 
    MasterPageFile="~/sitios/MARS/Mayoreo/MayoreoMars.Master" 
    AutoEventWireup="false" 
    CodeBehind="FormatoCapturaMarsVP.aspx.vb" 
    Inherits="procomlcd.FormatoCapturaMarsVP"
    title="Mars Verificadores de Precio Mayoreo - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoMayoreoMars" runat="Server">

 
<!--titulo-pagina-->
    <div id="titulo-pagina">Formato captura de información</div>

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
                            <img alt="" src="../../../../Img/arrow.gif" />
                            <a href="javascript:history.back(1)">Regresar</a></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 57px">
                            Tienda:</td>
                        <td style="width: 434px">
                            <asp:Label ID="lblTienda" runat="server" Font-Bold="True"></asp:Label>
                        </td>
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
                        <asp:GridView ID="gridProductosClientes" runat="server" AutoGenerateColumns="False" 
                            DataKeyNames="id_producto" Width="99%" style="text-align: center" ShowFooter="True" 
                            Caption="Mars" CssClass="grid-view" CellPadding="3" 
                        GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="None" 
                        BorderWidth="1px">
                            <RowStyle HorizontalAlign="Left" BackColor="#EEEEEE" ForeColor="Black" />
                            <Columns>
                                <asp:BoundField HeaderText="Producto" DataField="nombre_producto" />
                                <asp:TemplateField HeaderText="Precio pieza">
                                    <HeaderStyle Font-Size="XX-Small" />
                                    <ItemTemplate>
                                        $<asp:TextBox ID="txtPrecioPza" runat="server" Width="50" MaxLength="6"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "precio_pieza") %>'></asp:TextBox>
                                        <asp:RangeValidator ID="rvaPrecioPza" runat="server" 
                                            ControlToValidate="txtPrecioPza" Display="Dynamic" 
                                            ErrorMessage="El precio del producto por pieza indicado con un (*) en productos de Mars supera la cantidad permitida"
                                            MaximumValue='<%# DataBinder.Eval(Container, "DataItem.precio_max")%>' 
                                            MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPrecioPza" runat="server" 
                                            ControlToValidate="txtPrecioPza" 
                                            ErrorMessage="Completa el precio por pieza del producto indicado con un (*) en productos de Mars" 
                                            ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Precio kilo">
                                    <HeaderStyle Font-Size="XX-Small" />
                                    <ItemTemplate>
                                       $<asp:TextBox ID="txtPrecioKl" runat="server" Width="50" MaxLength="6"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "precio_kilo") %>'></asp:TextBox>
                                       <asp:RangeValidator ID="rvaPrecioKl" runat="server" 
                                            ControlToValidate="txtPrecioKl" Display="Dynamic" 
                                            ErrorMessage="El precio del producto por kilo indicado con un (*) en productos de Mars supera la cantidad permitida"
                                            MaximumValue="100" 
                                            MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPrecioKl" runat="server" 
                                            ControlToValidate="txtPrecioKl" 
                                            ErrorMessage="Completa el precio del producto por kilo indicado con un (*) en productos de Mars" 
                                            ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No catálogado" HeaderStyle-Font-Size="XX-Small">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkNoCatalogado" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "no_catalogado"))%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Agotado" HeaderStyle-Font-Size="XX-Small">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAgotado" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "agotado"))%>' />
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
                    </asp:View>

                    <asp:View ID="View1" runat="server">  
                        <asp:GridView ID="gridProductosCompetencia" runat="server" AutoGenerateColumns="False" 
                            DataKeyNames="id_producto" Width="99%" style="text-align: center" ShowFooter="True" 
                            Caption="Competencia" CssClass="grid-view" CellPadding="3" 
                        GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="None" 
                        BorderWidth="1px">
                            <RowStyle HorizontalAlign="Left" BackColor="#EEEEEE" ForeColor="Black" />
                            <Columns>
                                <asp:BoundField HeaderText="Producto" DataField="nombre_producto" />
                                <asp:TemplateField HeaderText="Precio pieza">
                                    <ItemTemplate>
                                        $<asp:TextBox ID="txtPrecioPza" runat="server" Width="50" MaxLength="6"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "precio_pieza") %>'></asp:TextBox>
                                        <asp:RangeValidator ID="rvaPrecioPza" runat="server" 
                                            ControlToValidate="txtPrecioPza" Display="Dynamic" 
                                            ErrorMessage="El precio del producto por pieza indicado con un (*) en productos de la competencia supera la cantidad permitida"
                                            MaximumValue='<%# DataBinder.Eval(Container, "DataItem.precio_max")%>'
                                            MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPrecioPza" runat="server" 
                                            ControlToValidate="txtPrecioPza" 
                                            ErrorMessage="Completa el precio del producto por pieza indicado con un (*) en productos de la competencia" 
                                            ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Precio kilo">
                                    <ItemTemplate>
                                        $<asp:TextBox ID="txtPrecioKl" runat="server" Width="50" MaxLength="6"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "precio_kilo") %>'></asp:TextBox>
                                        <asp:RangeValidator ID="rvaPrecioKl" runat="server" 
                                            ControlToValidate="txtPrecioKl" Display="Dynamic" 
                                            ErrorMessage="El precio del producto por kilo indicado con un (*) en productos de la competencia supera la cantidad permitida"
                                            MaximumValue="200" 
                                            MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfvPrecioKl" runat="server" 
                                            ControlToValidate="txtPrecioKl" 
                                            ErrorMessage="Completa el precio del producto por kilo indicado con un (*) en productos de la competencia" 
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
                    </asp:View>
                </asp:MultiView>

                       <br />
                       
                       <table style="border-style: groove; width: 99%; height: 115px; margin-right: 0px" >
                
                           <tr style="color: #FFFFFF">
                               <td style="text-align: center; background-color: #02456F; height: 29px; width: 616px;" >
                                
                                   Comentarios:</td>
                           </tr>
                           <tr style="font-size: small">
                               <td style="font-size: x-small; height: 75px; width: 616px;" >
                                 
                                   <asp:TextBox ID="txtComentarioGeneral" runat="server" Width="639px" Height="60px" 
                                       TextMode="MultiLine" MaxLength="300"></asp:TextBox>
                               </td>
                           </tr>
                           <tr style="color: #FFFFFF">
                               <td style="text-align: center; background-color: #02456F; height: 29px; width: 616px;" >
                                
                                   Comentarios competencia:</td>
                           </tr>
                           <tr style="font-size: small">
                               <td style="font-size: x-small; height: 75px; width: 616px; color: #FFFFFF;" >
                                 
                                   <asp:TextBox ID="txtComentarioCompetencia" runat="server" Width="637px" Height="60px" 
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
                                       <br />
                                       <asp:Label ID="lblMinimos" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
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
        <div id="content-side-two-column" style="color: #FF3300; text-align: center">
            <b>Si no puedes capturar precios, reportalo en la página principal, en la sección "<span 
                style="color: #000000">¿Tienes problemas en el sitio web?</span>"</b></div>
        <div class="clear">
        </div>
    </div>
</asp:Content>
