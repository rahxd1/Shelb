<%@ Page Language="vb" 
    Culture="es-MX" 
    MasterPageFile="~/sitios/SYM/AC/SYMAC.Master"
    AutoEventWireup="false" 
    CodeBehind="FormatoCapturaSYMInventario.aspx.vb" 
    Inherits="procomlcd.FormatoCapturaSYMInventario"
    title="SYM Material POP - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSYMAC" runat="Server">
    <!--PAGE TITLE-->
<!--CONTENT CONTAINER-->
<div id="titulo-pagina">
    Inventario material POP</div>
        <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server"/>
                <table style="width: 99%">
                    <tr><td style="width: 396px">&nbsp;</td>
                        <td align="right"><img alt="" src="../../../../Img/arrow.gif" /><asp:HyperLink ID="lnkRegresar" runat="server" NavigateUrl="~/sitios/SYM/AC/Captura/RutaSYMPOP.aspx">Regresar</asp:HyperLink></td>
                    </tr>
                    </table>
                
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <asp:GridView ID="gridProductos1" runat="server" AutoGenerateColumns="False" 
                                        Caption="MATERIAL POP LA CREM" CssClass="grid-view" DataKeyNames="id_material" 
                                        ShowFooter="True" Width="100%">
                                        <RowStyle HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="nombre_material" HeaderText="Material" />
                                            <asp:TemplateField HeaderText="Frentes">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCantidad" runat="server" MaxLength="6" 
                                                        Text='<%#DataBinder.Eval(Container.DataItem, "Cantidad") %>' Width="50"></asp:TextBox>
                                                    <asp:RangeValidator ID="rvaPrecio" runat="server" 
                                                        ControlToValidate="txtCantidad" Display="Dynamic" 
                                                        ErrorMessage="Cantidad solo permite números" MaximumValue="9999" 
                                                        MinimumValue="0" Type="Double" ValidationGroup="SYM">*</asp:RangeValidator>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="top" Width="50%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grid-footer" />
                                        <EmptyDataTemplate>
                                            <h1>
                                                No hay datos</h1>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                                <td style="width: 60px">
                                    &nbsp;</td>
                                <td>
                                    <asp:GridView ID="gridProductos2" runat="server" AutoGenerateColumns="False" 
                                        Caption="MATERIAL POP LIRIO LAVANDERIA" CssClass="grid-view" 
                                        DataKeyNames="id_material" ShowFooter="True" Width="100%">
                                        <RowStyle HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="nombre_material" HeaderText="Material" />
                                            <asp:TemplateField HeaderText="Frentes">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCantidad" runat="server" MaxLength="6" 
                                                        Text='<%#DataBinder.Eval(Container.DataItem, "Cantidad") %>' Width="50"></asp:TextBox>
                                                    <asp:RangeValidator ID="rvaPrecio" runat="server" 
                                                        ControlToValidate="txtCantidad" Display="Dynamic" 
                                                        ErrorMessage="Cantidad solo permite números" MaximumValue="9999" 
                                                        MinimumValue="0" Type="Double" ValidationGroup="SYM">*</asp:RangeValidator>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="top" Width="50%" />
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
                    <br />
                    
                        <br />
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        
                        <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                        <table style="border-style: groove; width: 99%; height: 99px; margin-right: 0px;" >
                            <tr>
                                <td style="text-align: center; height: 31px; color: #FFFFFF;" bgcolor="#003399" 
                                    colspan="2">
                                    COMENTARIOS GENERALES</td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center; height: 31px; ">
                                    <asp:TextBox ID="txtComentario_General" runat="server" Height="60px" 
                                        MaxLength="300" TextMode="MultiLine" Width="635px"></asp:TextBox>
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
