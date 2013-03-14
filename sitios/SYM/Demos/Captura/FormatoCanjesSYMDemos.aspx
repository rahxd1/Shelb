<%@ Page Language="vb" 
    Culture="es-MX" 
    MasterPageFile="~/sitios/SYM/Demos/SYMDemos.Master" 
    AutoEventWireup="false" 
    CodeBehind="FormatoCanjesSYMDemos.aspx.vb" 
    Inherits="procomlcd.FormatoCanjesSYMDemos"
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
                            <asp:Label ID="lblTienda" runat="server" 
                            Font-Bold="True"></asp:Label>
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
                            <asp:Label ID="lblApellidoPaterno" runat="server" 
                            Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 98px; text-align: right">
                                        Apellido materno</td>
                                    <td>
                            <asp:Label ID="lblApellidoMaterno" runat="server" 
                            Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 98px; text-align: right">
                                        Nombre(s)</td>
                                    <td>
                            <asp:Label ID="lblNombres" runat="server" 
                            Font-Bold="True"></asp:Label>
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
                            <td valign="top">
                                <table style="width: 100%">
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Panel ID="Panel1" runat="server" BorderStyle="Solid" BorderWidth="1px">
                                    <table style="width: 100%">
                                        <tr>
                                            <td bgcolor="#003399" colspan="2" style="text-align: center; color: #FFFFFF;">
                                                Reporte canjes</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                Fecha (dd/mm/aaaa)</td>
                                            <td style="width: 705px">
                                                <asp:TextBox ID="txtFecha" runat="server" MaxLength="10" 
                                                    style="margin-left: 0px" Width="81px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvFecha" runat="server" 
                                                    ControlToValidate="txtFecha" ErrorMessage="Indica la fecha del ticket." 
                                                    ValidationGroup="SYM">*</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="rvaFecha" runat="server" 
                                                    ControlToValidate="txtFecha" Display="Dynamic" 
                                                    ErrorMessage="La fecha indicada no tiene el formato 'dd/mm/aaaa'" 
                                                    ValidationExpression="\d{2}/\d{2}/\d{4}" ValidationGroup="SYM">*</asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                Nombre del cliente</td>
                                            <td style="width: 705px">
                                                <asp:TextBox ID="txtNombreCliente" runat="server" MaxLength="50" 
                                                    style="margin-left: 0px" Width="336px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvNombreCliente" runat="server" 
                                                    ControlToValidate="txtNombreCliente" ErrorMessage="Indica el nombre del cliente." 
                                                    ValidationGroup="SYM">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                Telefono</td>
                                            <td style="width: 705px">
                                                <asp:TextBox ID="txtTelefono" runat="server" MaxLength="16" 
                                                    style="margin-left: 0px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                No. de ticket</td>
                                            <td style="width: 705px">
                                                <asp:TextBox ID="txtTicket" runat="server" MaxLength="16" 
                                                    style="margin-left: 0px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvTicket" runat="server" 
                                                    ControlToValidate="txtTicket" ErrorMessage="Indica el numero de ticket." 
                                                    ValidationGroup="SYM">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                Cantidad de toallas</td>
                                            <td style="width: 705px">
                                                <asp:TextBox ID="txtToallas" runat="server" MaxLength="2" 
                                                    style="margin-left: 0px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvToallas" runat="server" 
                                                    ControlToValidate="txtToallas" ErrorMessage="Indica el numero de toallas entregadas." 
                                                    ValidationGroup="SYM">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                &nbsp;</td>
                                            <td style="width: 705px; text-align: center;">
                                                <asp:GridView ID="gridProductosCanjes" runat="server" 
                                                    AutoGenerateColumns="False" CssClass="grid-view" DataKeyNames="id_producto" 
                                                    ShowFooter="True" Width="70%">
                                                    <RowStyle HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                                        <asp:TemplateField HeaderText="Ventas">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtVentas" runat="server" MaxLength="3" 
                                                                    Text='<%#DataBinder.Eval(Container.DataItem, "Ventas") %>' Width="50"></asp:TextBox>
                                                                <asp:RangeValidator ID="rvaVentas" runat="server" ControlToValidate="txtVentas" 
                                                                    Display="Dynamic" 
                                                                    ErrorMessage="El campo ventas del producto en canjes no contiene números" 
                                                                    MaximumValue="999" MinimumValue="0" Type="Double" ValidationGroup="SYM">*</asp:RangeValidator>
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
                                            <td style="text-align: right">
                                                &nbsp;</td>
                                            <td style="width: 705px; text-align: center;">
                                                <asp:Button ID="btnAgregar" runat="server" Text="Agregar ticket" Height="36px" 
                                                    ValidationGroup="SYM" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align: left">
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:GridView ID="gridCanjes" runat="server" AutoGenerateColumns="False"
                                    CssClass="grid-view" DataKeyNames="folio_historial_det"
                                    ShowFooter="True" Width="100%">
                                    <RowStyle HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:CommandField ShowDeleteButton="True" ButtonType="Image" 
                                            DeleteImageUrl="~/Img/delete-icon.png" />
                                        <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="no_ticket" HeaderText="Ticket" />
                                        <asp:BoundField DataField="nombre_cliente" HeaderText="Cliente" />
                                        <asp:BoundField DataField="telefono" HeaderText="Telefono" />
                                        <asp:BoundField DataField="toallas" HeaderText="Cantidad de toallas" />
                                        <asp:BoundField DataField="1" HeaderText="Jabón Liquido Manos Dermatológico 221ml" />
                                        <asp:BoundField DataField="2" HeaderText="Jabón Liquido Manos Neutro 221ml" />
                                        <asp:BoundField DataField="3" HeaderText="Jabón Liquido Corporal Dermatológico 400ml" />
                                        <asp:BoundField DataField="4" HeaderText="Jabón Liquido Corporal Neutro 400ml" />
                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                    <EmptyDataTemplate>
                                        <h1>
                                            Sin canjes registrados</h1>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
                        
                          
            <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>                    
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: center">
                                <asp:ValidationSummary ID="vsSYM" runat="server" 
                                   style="text-align: center" ValidationGroup="SYM" 
                                    HeaderText="Verifica la información" ShowMessageBox="True" 
                                    ShowSummary="False" />
                                <asp:Label ID="lblGuardado" runat="server" Font-Bold="True" BackColor="Yellow"></asp:Label>
                                <br />
                                <asp:LinkButton ID="lnkOtraCaptura" runat="server" Font-Bold="True" 
                                    Font-Underline="True">Capturar otra tienda</asp:LinkButton>
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
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnAgregar" />
                </Triggers>
            </asp:UpdatePanel>             
        </div>    
        
        
 <!--CONTENT SIDE COLUMN DATOS -->
        <div class="clear">
        </div>
    </div>
</asp:Content>

