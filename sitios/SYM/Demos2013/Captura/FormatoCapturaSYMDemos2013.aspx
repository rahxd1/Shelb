<%@ Page Language="vb" 
MasterPageFile="~/sitios/SYM/Demos2013/SYMDemos2013.Master"
AutoEventWireup="false" 
CodeBehind="FormatoCapturaSYMDemos2013.aspx.vb" 
Inherits="procomlcd.FormatoCapturaSYMDemos2013" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Contenido_SYMDemos2013" runat="Server">

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
                            <img alt="" src="../../../../Img/arrow.gif" /><asp:HyperLink ID="lnkRegresar" 
                                runat="server" 
                                NavigateUrl="~/sitios/SYM/Demos2013/Captura/RutasSYMDemos2013.aspx">Regresar</asp:HyperLink>
                        </td>
                    </tr>
            </table>
                        
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <table style="width: 100%">
                        <tr>
                            <td valign="top" style="width: 451px">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <asp:GridView ID="gridProductos" runat="server" AutoGenerateColumns="False" 
                                                CssClass="grid-view-productos" DataKeyNames="id_producto" ShowFooter="True" 
                                                Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="None" 
                                                BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                                                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Producto" ItemStyle-Width="75%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProducto" runat="server" Text='<%#Eval("nombre_producto")%>'></asp:Label>
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
                                    <tr>
                                      <%--esta seccion sera para los canjes --%>
                                        <td>
                                            <asp:GridView ID="gridViernesCanjes" runat="server" AutoGenerateColumns="False" 
                                                caption="Canjes" CssClass="grid-view" DataKeyNames="id_canje" 
                                                ShowHeader="False" Width="100%" Enabled="False">
                                                <RowStyle HorizontalAlign="Left" />
                                                <Columns>
                                                    <asp:BoundField DataField="nombre_canje" HeaderText="" />
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCantidad" runat="server" MaxLength="3" 
                                                                Text='<%#DataBinder.Eval(Container.DataItem, "cantidad") %>' Width="30"></asp:TextBox>
                                                            
                                                            
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
                                      <tr>
                                      <%--esta seccion sera para los canjes --%>
                                        <td>
                                            <asp:GridView ID="gridSabadoCanjes" runat="server" AutoGenerateColumns="False" 
                                                caption="Canjes" CssClass="grid-view" DataKeyNames="id_canje" 
                                                ShowHeader="False" Width="100%" Enabled="False">
                                                <RowStyle HorizontalAlign="Left" />
                                                <Columns>
                                                    <asp:BoundField DataField="nombre_canje" HeaderText="" />
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCantidad" runat="server" MaxLength="3" 
                                                                Text='<%#DataBinder.Eval(Container.DataItem, "cantidad") %>' Width="30"></asp:TextBox>
                                                            
                                                            
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
                                      <tr>
                                      <%--esta seccion sera para los canjes --%>
                                        <td>
                                            <asp:GridView ID="gridDomingoCanjes" runat="server" AutoGenerateColumns="False" 
                                                caption="Canjes" CssClass="grid-view" DataKeyNames="id_canje" 
                                                ShowHeader="False" Width="100%" Enabled="False">
                                                <RowStyle HorizontalAlign="Left" />
                                                <Columns>
                                                    <asp:BoundField DataField="nombre_canje" HeaderText="" />
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCantidad" runat="server" MaxLength="3" 
                                                                Text='<%#DataBinder.Eval(Container.DataItem, "cantidad") %>' Width="30"></asp:TextBox>
                                                            
                                                            
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