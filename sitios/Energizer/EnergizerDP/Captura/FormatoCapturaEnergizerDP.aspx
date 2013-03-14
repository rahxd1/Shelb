<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Energizer/EnergizerDP/Energizer_DP.Master"
    AutoEventWireup="false" 
    CodeBehind="FormatoCapturaEnergizerDP.aspx.vb" 
    Inherits="procomlcd.FormatoCapturaEnergizerDP"
    title="Energizer Pilas Demo 2010 - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1EnergizerDP" runat="Server">
  
<!--CONTENT CONTAINER-->
<div id="titulo-pagina">
    Formato captura información</div>
        <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
    
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 427px">
                                &nbsp;</td>
                            <td style="text-align: right">
                                <img alt="" src="../../../../Img/arrow.gif" />
                                <a href="javascript:history.back(1)">Regresar</a></td>
                        </tr>
                        <tr>
                            <td style="width: 427px">
    
                    <asp:Label ID="lblAviso" runat="server" Text="Captura tu Informacion"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                Info: <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
            </table>
                <asp:GridView ID="gridProductosCliente" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="id_producto" Width="100%" style="text-align: center" ShowFooter="True" 
                    Caption="PRODUCTOS ENERGIZER" CssClass="grid-view">
                    <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    <Columns>
                        <asp:BoundField HeaderText="Tipo pilas" DataField="grupo" />
                        <asp:BoundField HeaderText="Producto" DataField="nombre_producto" />
                        <asp:TemplateField HeaderText="Precio">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPrecio" runat="server" Width="50" Text='<%# DataBinder.Eval(Container.DataItem, "precio") %>'></asp:TextBox>
                                <asp:RangeValidator id="rvaPrecio" runat="server" ErrorMessage="Solo Numeros" Display="Dynamic"
                                    MinimumValue="0" MaximumValue="9999" Type="Double" ControlToValidate="txtPrecio" ValidationGroup="Energizer" />
                            </ItemTemplate>
                        </asp:TemplateField>
 
                    </Columns>
                    <FooterStyle CssClass="grid-footer" />
                    <EmptyDataTemplate>
                        <h1>No hay datos</h1>
                    </EmptyDataTemplate>
                </asp:GridView>
                  <br />

                               
                       <asp:GridView ID="gridCompetencia" runat="server" 
                            AutoGenerateColumns="False" DataKeyNames="id_competencia" Width="99%" 
                            style="text-align: center" ShowFooter="True" 
                            Caption="COMPETENCIA" CaptionAlign="Top" CssClass="grid-view">
                           <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    <Columns>
                        <asp:BoundField HeaderText="Marca" DataField="nombre_competencia" />
                        <asp:TemplateField HeaderText="Exhibidores">
                            <ItemTemplate>
                                <asp:TextBox ID="txtExhibidores" runat="server" Width="50" Text='<%#DataBinder.Eval(Container.DataItem, "exhibidores") %>'></asp:TextBox>
                                <asp:RangeValidator id="rvaExhibidores" runat="server" ErrorMessage="Solo Numeros" Display="Dynamic" 
                                    MinimumValue="0" MaximumValue="9999" Type="Double" ControlToValidate="txtExhibidores" ValidationGroup="Energizer" />
                            </ItemTemplate>
                        </asp:TemplateField>                      
                        <asp:TemplateField HeaderText="Demos">
                            <ItemTemplate>
                                <asp:TextBox ID="txtDemos" runat="server" Width="50" Text='<%#DataBinder.Eval(Container.DataItem, "demos") %>'></asp:TextBox>
                                <asp:RangeValidator id="rvaDemos" runat="server" ErrorMessage="Solo Numeros" Display="Dynamic" 
                                    MinimumValue="0" MaximumValue="9999" Type="Double" ControlToValidate="txtDemos" ValidationGroup="Energizer" />
                            </ItemTemplate>
                        </asp:TemplateField>   
                        <asp:TemplateField HeaderText="Tipo Promocionales" >
                            <ItemTemplate>
                                <asp:TextBox ID="txtTipoPromocionales" runat="server" TextMode="MultiLine" Width="250" Height="60px" Text='<%#DataBinder.Eval(Container.DataItem, "tipo_promocionales") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>   
                    </Columns>
                           <FooterStyle CssClass="grid-footer" />
                    <EmptyDataTemplate>
                        <h1>No hay datos</h1>
                    </EmptyDataTemplate>
                           <EditRowStyle HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:GridView>
                
                <br />         
                         
                <asp:GridView ID="gridPromo" runat="server" 
                    AutoGenerateColumns="False" DataKeyNames="id_producto" Width="100%" 
                    style="text-align: center" ShowFooter="True" 
                    Caption="PROMOCIONALES" CssClass="grid-view">
                           <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    <Columns>
                        <asp:BoundField HeaderText="Producto" DataField="nombre_producto" />
                        <asp:TemplateField HeaderText="Cantidad">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCantidad" runat="server" Width="50" 
                                    Text='<%#DataBinder.Eval(Container.DataItem, "Cantidad") %>'></asp:TextBox>
                                <asp:RangeValidator id="rvaCantidad" runat="server" 
                                    ErrorMessage="Solo Numeros" Display="Dynamic" 
                                    MinimumValue="0" MaximumValue="9999" Type="Double" 
                                    ControlToValidate="txtCantidad" ValidationGroup="Energizer" />
                            </ItemTemplate>
                        </asp:TemplateField>                      
                    </Columns>
                           <FooterStyle CssClass="grid-footer" />
                    <EmptyDataTemplate>
                        <h1>No hay datos</h1>
                    </EmptyDataTemplate>
                </asp:GridView>
        
                  <br />
                  
                       <table style="border-style: groove; width: 100%; height: 99px; margin-right: 0px" >
                           <tr style="color: #FFFFFF">
                               <td style="text-align: center; background-color: #000000; height: 29px; width: 616px;" bgcolor="#ffffff" >
                                   COMENTARIOS ENERGIZER</td></tr>
                           <tr style="font-size: small">
                               <td style="font-size: x-small; height: 75px; width: 616px;" >
                                   <asp:TextBox ID="txtComentarios" runat="server" Width="527px" Height="60px" 
                                       TextMode="MultiLine" MaxLength="300"></asp:TextBox></td></tr>
                           <tr style="color: #FFFFFF">
                               <td style="text-align: center; background-color: #000000; height: 29px; width: 616px;" bgcolor="#ffffff" >
                                   ACTIVIDAD COMPETENCIA</td></tr>
                           <tr style="font-size: small">
                               <td style="font-size: x-small; height: 75px; width: 616px;" >
                                   <asp:TextBox ID="txtActCompetencia" runat="server" Width="527px" Height="60px" 
                                       TextMode="MultiLine" MaxLength="300"></asp:TextBox></td></tr>
                           </table>
                               
                  <br />
      
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center; height: 31px; ">
                                 
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
                                    <td style="text-align: center">
                                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" 
                                            ValidationGroup="Energizer" CssClass="button" />
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
                    </td>
                </tr>
                </table>
                      
                        </div><!--CONTENT SIDE COLUMN AVISOS--><div id="content-side-two-column">
            </div><div class="clear">
            </div>
        </div>
   </div>
</asp:Content>
