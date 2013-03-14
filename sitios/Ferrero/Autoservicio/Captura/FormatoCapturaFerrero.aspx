<%@ Page Language="vb" 
    Culture="es-MX" 
    MasterPageFile="~/sitios/Ferrero/Autoservicio/FerreroAS.Master" 
    AutoEventWireup="false" 
    CodeBehind="FormatoCapturaFerrero.aspx.vb" 
    Inherits="procomlcd.FormatoCapturaFerrero"
    title="Ferrero - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoFerreroAS" runat="Server">
    <!--PAGE TITLE-->
<div id="titulo-pagina">Formato captura información</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">

<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"/>

                    <table style="width: 100%">
                        <tr><td style="text-align: right;" colspan="5">
                                <img alt="" src="../../../../Img/arrow.gif" />
                                <asp:HyperLink ID="lnkRegresar" runat="server" 
                                    NavigateUrl="~/sitios/Ferrero/Autoservicio/Captura/RutasFerrero.aspx">Regresar</asp:HyperLink></td>
                        </tr>
                        <tr><td style="width: 65px" colspan="2">Tienda</td>
                            <td colspan="3">
                                <asp:TextBox ID="txtTienda" runat="server" Height="18px" Width="308px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvTienda" runat="server" 
                                       ErrorMessage="Indica el nombre de la tienda" ValidationGroup="Ferrero" 
                                       ControlToValidate="txtTienda">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr><td>
                                    <asp:LinkButton ID="lnkProd1" runat="server" Font-Bold="True">Estuches</asp:LinkButton>
                                </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                    <asp:LinkButton ID="lnkProd2" runat="server" Font-Bold="True">Untables</asp:LinkButton>
                                </td>
                            <td>
                                    <asp:LinkButton ID="lnkProd3" runat="server" Font-Bold="True">Tabletas</asp:LinkButton>
                                </td>
                            <td>
                                    <asp:LinkButton ID="lnkProd4" runat="server" Font-Bold="True">Check out</asp:LinkButton>
                                </td>
                        </tr>
                    </table>
                    
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                
                            <asp:Panel ID="pnl1" Visible="true" runat="server" style="text-align: center">
                                <asp:GridView ID="gridProductos1" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="id_producto" Width="100%" ShowFooter="True" 
                                    Caption="ESTUCHES" CssClass="grid-view">
                                    <Columns>
                                        <asp:BoundField DataField="id_marca" HeaderText="marca" />
                                        <asp:BoundField HeaderText="Producto" DataField="nombre_producto" />
                                        <asp:BoundField HeaderText="Presentación" DataField="presentacion" />
                                        <asp:BoundField HeaderText="Gramaje" DataField="gramaje" />
                                        <asp:BoundField HeaderText="Marca" DataField="nombre_marca" />
                                        <asp:TemplateField HeaderText="Precio">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPrecio" runat="server" MaxLength="7" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "precio") %>' Width="80"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaPrecio" runat="server" 
                                                    ControlToValidate="txtPrecio" Display="Dynamic" 
                                                    ErrorMessage="En precios solo se permite números" MaximumValue="9999" 
                                                    MinimumValue="0" Type="Double" ValidationGroup="Ferrero">*</asp:RangeValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Frentes">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtFrentes" runat="server" Width="80" MaxLength="3" Text='<%# DataBinder.Eval(Container.DataItem, "Frentes") %>'></asp:TextBox>
                                                <asp:RangeValidator id="rvaFrentes" runat="server" ErrorMessage="En precios solo se permite números" Display="Dynamic"
                                                    MinimumValue="0" MaximumValue="9999" Type="Double" ControlToValidate="txtFrentes" ValidationGroup="Ferrero">*</asp:RangeValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Faltantes">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkFaltantes" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Faltante")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                    <EmptyDataTemplate>
                                        <h1>Se ha producido un error</h1>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </asp:Panel>
                            <asp:Panel ID="pnl2" Visible="false" runat="server" style="text-align: center">
                                <asp:GridView ID="gridProductos2" runat="server" AutoGenerateColumns="False" 
                                    Caption="UNTABLES" CssClass="grid-view" DataKeyNames="id_producto" 
                                    ShowFooter="True" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="id_marca" HeaderText="marca" />
                                        <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                        <asp:BoundField DataField="presentacion" HeaderText="Presentación" />
                                        <asp:BoundField DataField="gramaje" HeaderText="Gramaje" />
                                        <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                        <asp:TemplateField HeaderText="Precio">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPrecio" runat="server" MaxLength="7" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "precio") %>' Width="80"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaPrecio" runat="server" 
                                                    ControlToValidate="txtPrecio" Display="Dynamic" 
                                                    ErrorMessage="En precios solo se permite números" MaximumValue="9999" 
                                                    MinimumValue="0" Type="Double" ValidationGroup="Ferrero">*</asp:RangeValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Frentes">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtFrentes" runat="server" Width="80" MaxLength="3" Text='<%# DataBinder.Eval(Container.DataItem, "Frentes") %>'></asp:TextBox>
                                                <asp:RangeValidator id="rvaFrentes" runat="server" ErrorMessage="En precios solo se permite números" Display="Dynamic"
                                                    MinimumValue="0" MaximumValue="9999" Type="Double" ControlToValidate="txtFrentes" ValidationGroup="Ferrero">*</asp:RangeValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Faltantes">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkFaltantes" runat="server" 
                                                    Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Faltante")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                    <EmptyDataTemplate>
                                        <h1>
                                            Se ha producido un error</h1>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </asp:Panel>
                            <asp:Panel ID="pnl3" Visible="false" runat="server" style="text-align: center">  
                                <asp:GridView ID="gridProductos3" runat="server" AutoGenerateColumns="False" 
                                    Caption="TABLETAS" CssClass="grid-view" DataKeyNames="id_producto" 
                                    ShowFooter="True" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="id_marca" HeaderText="marca" />
                                        <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                        <asp:BoundField DataField="presentacion" HeaderText="Presentación" />
                                        <asp:BoundField DataField="gramaje" HeaderText="Gramaje" />
                                        <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                        <asp:TemplateField HeaderText="Precio">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPrecio" runat="server" MaxLength="7" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "precio") %>' Width="80"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaPrecio" runat="server" 
                                                    ControlToValidate="txtPrecio" Display="Dynamic" 
                                                    ErrorMessage="En precios solo se permite números" MaximumValue="9999" 
                                                    MinimumValue="0" Type="Double" ValidationGroup="Ferrero">*</asp:RangeValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Frentes">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtFrentes" runat="server" Width="80" MaxLength="3" Text='<%# DataBinder.Eval(Container.DataItem, "Frentes") %>'></asp:TextBox>
                                                <asp:RangeValidator id="rvaFrentes" runat="server" ErrorMessage="En precios solo se permite números" Display="Dynamic"
                                                    MinimumValue="0" MaximumValue="9999" Type="Double" ControlToValidate="txtFrentes" ValidationGroup="Ferrero">*</asp:RangeValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Faltantes">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkFaltantes" runat="server" 
                                                    Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Faltante")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                    <EmptyDataTemplate>
                                        <h1>
                                            Se ha producido un error</h1>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </asp:Panel>
                            <asp:Panel ID="pnl4" Visible="false" runat="server" style="text-align: center"> 
                                <asp:GridView ID="gridProductos4" runat="server" AutoGenerateColumns="False" 
                                    Caption="CHECK OUT" CssClass="grid-view" DataKeyNames="id_producto" 
                                    ShowFooter="True" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="id_marca" HeaderText="marca" />
                                        <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                        <asp:BoundField DataField="presentacion" HeaderText="Presentación" />
                                        <asp:BoundField DataField="gramaje" HeaderText="Gramaje" />
                                        <asp:BoundField DataField="nombre_marca" HeaderText="Marca" />
                                        <asp:TemplateField HeaderText="Precio">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPrecio" runat="server" MaxLength="7" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "precio") %>' Width="80"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaPrecio" runat="server" 
                                                    ControlToValidate="txtPrecio" Display="Dynamic" 
                                                    ErrorMessage="En precios solo se permite números" MaximumValue="9999" 
                                                    MinimumValue="0" Type="Double" ValidationGroup="Ferrero">*</asp:RangeValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Frentes">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtFrentes" runat="server" MaxLength="3" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Frentes") %>' Width="80"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaFrentes" runat="server" 
                                                    ControlToValidate="txtFrentes" Display="Dynamic" 
                                                    ErrorMessage="En precios solo se permite números" MaximumValue="9999" 
                                                    MinimumValue="0" Type="Double" ValidationGroup="Ferrero">*</asp:RangeValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Faltantes">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkFaltantes" runat="server" 
                                                    Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Faltante")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                    <EmptyDataTemplate>
                                        <h1>
                                            Se ha producido un error</h1>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </asp:Panel>
                            <br />
                                <table style="border-style: groove; width: 99%; height: 15px; margin-right: 0px">
                                    <tr style="color: #FFFFFF">
                                        <td bgcolor="black" 
                                            style="text-align: center; background-color: #000000; height: 15px; width: 538px;">
                                            Descripción</td>
                                        <td align="left" colspan="2" style="text-align: left; height: 15px; ">
                                            <asp:DropDownList ID="cmbDescripcion" runat="server" 
                                                Height="22px" Width="250px">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>ESTUCHES</asp:ListItem>
                                                <asp:ListItem>UNTABLES</asp:ListItem>
                                                <asp:ListItem>TABLETAS</asp:ListItem>
                                                <asp:ListItem>CHECK OUT</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" 
                                                ControlToValidate="cmbDescripcion" ErrorMessage="*" Font-Bold="True" 
                                                ValidationGroup="Foto"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr style="color: #FFFFFF">
                                        <td bgcolor="black" 
                                            style="text-align: center; background-color: #000000; height: 15px; width: 538px;">
                                            Fotografía</td>
                                        <td align="left" style="text-align: left; height: 15px; width: 616px;">
                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                            <asp:RequiredFieldValidator ID="rfvFile" runat="server" 
                                                ControlToValidate="FileUpload1" ErrorMessage="*" Font-Bold="True" 
                                                ValidationGroup="Foto"></asp:RequiredFieldValidator>
                                        </td>
                                        <td align="left" style="text-align: center; height: 15px; width: 616px;">
                                            <asp:Button ID="btnSubir" runat="server" CssClass="button" Text="Subir" 
                                                ValidationGroup="Foto" />
                                        </td>
                                    </tr>
                                    <tr style="color: #FFFFFF">
                                        <td bgcolor="black" 
                                            style="text-align: center; background-color: #000000; height: 15px; width: 538px;">
                                        </td>
                                        <td align="left" colspan="2" 
                                            style="text-align: left; height: 15px; background-color: #FFFFFF;">
                                            <asp:Label ID="lblSubida" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="color: #FFFFFF">
                                        <td colspan="3" style="text-align: center; height: 15px; ">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gridImagenes" runat="server" AutoGenerateColumns="False" 
                                                        BorderStyle="None" DataKeyNames="folio_foto" ForeColor="Black" 
                                                        ShowHeader="False" Width="100%">
                                                        <RowStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" 
                                                            VerticalAlign="Top" />
                                                        <Columns>
                                                            <asp:CommandField ButtonType="Image" 
                                                                DeleteImageUrl="~/sitios/Ferrero/Imagenes/delete-icon.png" 
                                                                ShowDeleteButton="True" />
                                                            <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                                            <asp:TemplateField HeaderText="Foto">
                                                                <ItemTemplate>
                                                                    <img alt="Foto" src='<%#Eval("ruta")%><%#Eval("foto")%>' width="200" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:GridView>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnSubir" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                    <br />
                    <table style="border-style: groove; width: 100%; height: 99px; margin-right: 0px" >
                        <tr style="color: #FFFFFF">
                           <td style="text-align: center; background-color: #000000; height: 29px; width: 616px;" >
                            
                               COMENTARIOS</td>
                        </tr>
                        <tr style="font-size: small">
                           <td style="font-size: x-small; height: 75px; width: 616px;" >
                             
                               <asp:TextBox ID="txtComentarios" runat="server" Width="646px" Height="60px" 
                                   TextMode="MultiLine" MaxLength="300"></asp:TextBox>
                           </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>                                    
                
                  <br />
            
            
                    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <fieldset>
                            <table style="width: 100%">
                               <tr>
                                   <td style="text-align: center; height: 31px; " colspan="2">
                                   
                                       <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                           ValidationGroup="Ferrero" />
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
                                            ValidationGroup="Ferrero" CssClass="button" />
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
