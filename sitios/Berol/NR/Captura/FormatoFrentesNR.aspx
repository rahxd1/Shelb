<%@ Page Language="vb" 
    Culture="es-MX" 
    MasterPageFile="~/sitios/Berol/NR/Rubbermaid.Master" 
    AutoEventWireup="false" 
    CodeBehind="FormatoFrentesNR.aspx.vb" 
    Inherits="procomlcd.FormatoFrentesNR"
    title="Newell Rubbermaid - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoNR" runat="Server">
    <!--PAGE TITLE-->
<div id="titulo-pagina">Formato captura frentes y exhibiciones</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">

<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"/>

                    <table style="width: 100%">
                        <tr>
                                    <td __designer:mapid="1e0" style="width: 56px">
                                        &nbsp;</td>
                                    <td __designer:mapid="1e1" style="text-align: right">
                                <img alt="" src="../../../../Img/arrow.gif" /><asp:HyperLink ID="lnkRegresar" runat="server" 
                                    NavigateUrl="~/sitios/Berol/NR/Captura/RutasPromotorNR.aspx">Regresar</asp:HyperLink>
                                    </td>
                                </tr>
                        <tr>
                                    <td __designer:mapid="1e0" style="width: 56px; text-align: right">
                                        Tienda</td>
                                    <td __designer:mapid="1e1">
                                        <asp:Label ID="lblTienda" runat="server" 
                            Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                        <tr>
                                    <td __designer:mapid="1e0" style="width: 56px; text-align: right">
                                        Cadena</td>
                                    <td __designer:mapid="1e1">
                                        <asp:Label ID="lblCadena" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                        <tr><td style="text-align: right; width: 56px;">
                                &nbsp;</td>
                        </tr>
                        </table>
                    
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                
                            <asp:GridView ID="gridFrentes" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="tipo_producto" Width="61%" ShowFooter="True" 
                                    Caption="Frentes por categoría" CssClass="grid-view">
                                    <Columns>
                                        <asp:BoundField HeaderText="Producto" DataField="nombre_tipo" />
                                        <asp:TemplateField HeaderText="Rubbermaid">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPropio" runat="server" MaxLength="3" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "NR") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaPropio" runat="server" 
                                                    ControlToValidate="txtPropio" Display="Dynamic" 
                                                    ErrorMessage="En cantidad de frentes de Rubbermaid solo se permite números" MaximumValue="100" 
                                                    MinimumValue="0" Type="Double" ValidationGroup="Rubbermaid">*</asp:RangeValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Competencia">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCompetencia" runat="server" MaxLength="3" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Competencia") %>' Width="50"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaCompetencia" runat="server" 
                                                    ControlToValidate="txtCompetencia" Display="Dynamic" 
                                                    ErrorMessage="En cantidad de frentes de la competencia solo se permite números" MaximumValue="100" 
                                                    MinimumValue="0" Type="Double" ValidationGroup="Rubbermaid">*</asp:RangeValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                    <EmptyDataTemplate>
                                        <h1>Se ha producido un error</h1>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            
                            <br />
                            
                            <asp:GridView ID="gridExhibiciones" runat="server" AutoGenerateColumns="False" 
                                    Caption="Exhibiciones" CssClass="grid-view" DataKeyNames="id_exhibicion" 
                                    ShowFooter="True" Width="57%">
                                    <Columns>
                                        <asp:BoundField DataField="nombre_exhibicion" HeaderText="Exhibicionesº" />
                                        <asp:TemplateField HeaderText="Cantidad">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCantidad" runat="server" MaxLength="3" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Cantidad") %>' Width="80"></asp:TextBox>
                                                <asp:RangeValidator ID="rvaCantidad" runat="server" 
                                                    ControlToValidate="txtCantidad" Display="Dynamic" 
                                                    ErrorMessage="En cantidad de exhibiciones solo se permite números" MaximumValue="100" 
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
                                
                </ContentTemplate>
            </asp:UpdatePanel>                                    
                
            
                    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table style="width: 100%">
                               <tr>
                                   <td style="text-align: center; height: 31px; " colspan="2">
                                   
                                       <asp:ValidationSummary ID="vsRubbermaid" runat="server" 
                                           ValidationGroup="Rubbermaid" />
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
                                            ValidationGroup="Rubbermaid" CssClass="button" />
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
