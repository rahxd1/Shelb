<%@ Page Language="vb" 
    Culture="es-MX" 
    MasterPageFile="~/sitios/Mars/Autoservicio/Mars_Autoservicio.Master" 
    AutoEventWireup="false" 
    CodeBehind="FormatoCapturaPreciosMarsAS.aspx.vb" 
    Inherits="procomlcd.FormatoCapturaPreciosMarsAS"
    title="Mars Autoservicio - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMarsAutoservicio" runat="Server">

 
<!--titulo-pagina-->
    <div id="titulo-pagina">Formato captura información</div>

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
                            NavigateUrl="~/sitios/Mars/Autoservicio/Captura/RutaMarsAS.aspx">Regresar</asp:HyperLink></td>
                    </tr>
                    <tr>
                        <td style="width: 79px">
                            Tienda:</td>
                        <td style="width: 434px">
                            <asp:Label ID="lblTienda" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                        <td style="text-align: right">
                            &nbsp;</td>
                    </tr>
                    </table>
                
                <asp:GridView ID="gridProductosPropios" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="id_producto" Width="99%" style="text-align: center" ShowFooter="True" 
                    Caption="Precios Productos Propios" CssClass="grid-view" 
                CellPadding="3" GridLines="Vertical" BackColor="White" BorderColor="#999999" 
                BorderStyle="None" BorderWidth="1px">
                    <RowStyle HorizontalAlign="Left" BackColor="#EEEEEE" ForeColor="Black" />
                    <Columns>
                        <asp:BoundField HeaderText="Producto" DataField="nombre_producto" />
                        <asp:TemplateField HeaderText="Precios">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPrecio" runat="server" Width="50" Text='<%# DataBinder.Eval(Container.DataItem, "Precio") %>'></asp:TextBox>
                                <asp:RangeValidator id="rvaPrecio" runat="server" ErrorMessage="El campo precios de la sección 'PRODUCTOS MARS' no contiene números o el precio es mayor al permitido" Display="Dynamic" 
                                MinimumValue='<%# DataBinder.Eval(Container, "DataItem.precio_min")%>' MaximumValue='<%# DataBinder.Eval(Container, "DataItem.precio_max")%>' Type="Double" ControlToValidate="txtPrecio" ValidationGroup="Mars" >*</asp:RangeValidator>
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
                
                <asp:GridView ID="gridProductosCompetencia" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="id_producto" Width="99%" style="text-align: center" ShowFooter="True" 
                    Caption="Precios productos competencia" CssClass="grid-view" 
                CellPadding="3" GridLines="Vertical" BackColor="White" BorderColor="#999999" 
                BorderStyle="None" BorderWidth="1px">
                    <RowStyle HorizontalAlign="Left" BackColor="#EEEEEE" ForeColor="Black" />
                    <Columns>
                        <asp:BoundField HeaderText="Producto" DataField="nombre_producto" />
                        <asp:TemplateField HeaderText="Precios">
                            <ItemTemplate>
                                <asp:RangeValidator id="rvaPrecio" runat="server" ErrorMessage="Solo números en precio pieza de la competencia" Display="Dynamic"
                                    MinimumValue="0" MaximumValue="9999" Type="Double" ControlToValidate="txtPrecio" ValidationGroup="Mars">*</asp:RangeValidator>
                                <asp:TextBox ID="txtPrecio" runat="server" Width="50" Text='<%# DataBinder.Eval(Container.DataItem, "Precio") %>'></asp:TextBox>
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
                       
                       <table style="border-style: groove; width: 99%; height: 115px; margin-right: 0px" >
                
                           <tr style="color: #FFFFFF">
                               <td style="text-align: center; background-color: #02456F; height: 29px; width: 616px;" >
                                
                                   COMENTARIOS GENERALES</td>
                           </tr>
                           <tr style="font-size: small">
                               <td style="font-size: x-small; height: 75px; width: 616px;" >
                                 
                                   <asp:TextBox ID="txtComentarioGeneral" runat="server" Width="639px" Height="60px" 
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
