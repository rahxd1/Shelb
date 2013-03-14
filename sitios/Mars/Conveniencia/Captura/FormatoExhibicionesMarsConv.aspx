<%@ Page Language="vb" 
    Culture="es-MX" 
    MasterPageFile="~/sitios/Mars/Conveniencia/MarsConveniencia.Master" 
    AutoEventWireup="false" 
    CodeBehind="FormatoExhibicionesMarsConv.aspx.vb" 
    Inherits="procomlcd.FormatoExhibicionesMarsConv"
    title="Mars Conveniencia - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoMarsConveniencia" runat="Server">

 
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
                            NavigateUrl="~/sitios/Mars/Conveniencia/Captura/RutaMarsConv.aspx">Regresar</asp:HyperLink></td>
                    </tr>
                    <tr>
                        <td style="width: 61px">
                            Tienda</td>
                        <td style="width: 434px">
                            <asp:Label ID="lblTienda" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                        <td style="text-align: right">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 434px" colspan="2">
                            &nbsp;</td>
                        <td style="text-align: right">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 434px" colspan="2">
                            <asp:CheckBox ID="chkMaterialPOP" runat="server" Font-Bold="True" 
                                Text="Material POP" />
                        </td>
                        <td style="text-align: right">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 434px" colspan="2">
                            &nbsp;</td>
                        <td style="text-align: right">
                            &nbsp;</td>
                    </tr>
                    </table>
                    
            <asp:Panel ID="pnlPropios" runat="server">
                <asp:GridView ID="gridExhibiciones" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="id_exhibidor" Width="99%" style="text-align: center" 
                    ShowFooter="True" CssClass="grid-view" CellPadding="3" 
                    GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="None" 
                    BorderWidth="1px">
                    <RowStyle HorizontalAlign="Left" BackColor="#EEEEEE" ForeColor="Black" />
                    <Columns>
                        <asp:BoundField HeaderText="Exhibidor" DataField="nombre_exhibidor" />
                        <asp:TemplateField HeaderText="Cantidad">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCantidad" runat="server" Width="50" Text='<%# DataBinder.Eval(Container.DataItem, "cantidad") %>'></asp:TextBox>
                                <asp:RangeValidator id="rvaCantidad" runat="server" ErrorMessage="Solo números en Cantidad" Display="Dynamic"
                                    MinimumValue="0" MaximumValue="9999" Type="Double" ControlToValidate="txtCantidad" ValidationGroup="Mars">*</asp:RangeValidator>    
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
            </asp:Panel> 
            <br />
            <br />
                       <table style="border-style: groove; width: 99%; height: 115px; margin-right: 0px" >
                
                           <tr style="color: #FFFFFF">
                               <td style="text-align: center; background-color: #02456F; height: 29px; width: 616px;" >
                                
                                   COMENTARIOS</td>
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
                                   
                                       <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                           ValidationGroup="Biosal" />
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
                                    <td style="text-align: center; width: 327px;">
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
