<%@ Page Language="vb" 
    Culture="es-MX" 
    MasterPageFile="~/sitios/Energizer/EnergizerConv/Energizer_Conv.Master" 
    AutoEventWireup="false" 
    CodeBehind="FormatoCapturaEnergizerConv.aspx.vb" 
    Inherits="procomlcd.FormatoCapturaEnergizerConv"
    Title="Energizer Conveniencia - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1EnergizerConv" runat="Server">
  
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
                    <td colspan="2" style="width: 27px">
                        &nbsp;</td>
                    <td style="width: 75px">
                        &nbsp;</td>
                    <td style="width: 75px">
                        &nbsp;</td>
                    <td style="text-align: right">
                        <img alt="" src="../../../../Img/arrow.gif" />
                        <a href="javascript:history.back(1)">Regresar</a></td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 27px">
                        &nbsp;</td>
                    <td colspan="2" style="text-align: right">
                        &nbsp;</td>
                    <td style="text-align: right">
                        Fecha ultima visita:
                                <asp:TextBox ID="txtFecha" runat="server" DataFormatString="{0:dd-MM-yyyy}"
                            MaxLength="10" Width="85px" ValidationGroup="Energizer"></asp:TextBox>
                            <a href="javascript:;" onclick="window.open('../../../../Calendario/popup.aspx?textbox=ctl00$ContentPlaceHolder1Finlandia$txtFecha','cal','width=250,height=225,left=270,top=180')">
                                <img alt="" border="0" src="../../../../Img/SmallCalendar.gif" /></a></td>
                </tr>
                <tr>
                    <td style="height: 34px; width: 102px">
                        &nbsp;</td>
                    <td colspan="4" style="height: 34px; text-align: right;">
                                <asp:RangeValidator ID="rvFecha" runat="server" ErrorMessage="RangeValidator" 
                                    MaximumValue="31/12/3000" MinimumValue="01/01/2011" Type="Date" 
                                    ControlToValidate="txtFecha"></asp:RangeValidator>
                            </td>
                </tr>
                <tr>
                    <td style="height: 34px; width: 102px">
                        Encargado Tienda</td>
                    <td colspan="4" style="height: 34px">
                                <asp:TextBox ID="txtEncargado" runat="server" Width="283px" 
                            MaxLength="50" style="margin-left: 0px; margin-right: 0px"></asp:TextBox>
                            </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: right">
                                <asp:CheckBox ID="chkMaterialEnergizer" runat="server" 
                            Text="Material p.o.p Energizer" TextAlign="Left" style="text-align: right" />
                            </td>
                    <td style="text-align: right; width: 75px">
                        Cantidad&nbsp;
                                <asp:TextBox ID="txtCantidadEnergizer" runat="server" MaxLength="3" 
                                    Width="50px" style="margin-left: 0px"></asp:TextBox>
                            </td>
                    <td>
                            <asp:RangeValidator id="rvaExistencia" runat="server" ErrorMessage="Solo Numeros" Display="Dynamic"
                                    MinimumValue="0" MaximumValue="9999" Type="Double" 
                                    ControlToValidate="txtCantidadEnergizer" ValidationGroup="Energizer">*</asp:RangeValidator>
                            </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: right">
                                <asp:CheckBox ID="chkMaterialOtro" runat="server" 
                            Text="Material p.o.p. Duracell" TextAlign="Left" />
                            </td>
                    <td style="text-align: right; width: 75px">
                        Cantidad&nbsp;
                                <asp:TextBox ID="txtCantidadOtro" runat="server" style="text-align: left" 
                                    MaxLength="3" Width="50px"></asp:TextBox>
                            </td>
                    <td>
                            <asp:RangeValidator id="rvaExistencia0" runat="server" ErrorMessage="Solo Numeros" Display="Dynamic"
                                    MinimumValue="0" MaximumValue="9999" Type="Double" 
                                    ControlToValidate="txtCantidadOtro" ValidationGroup="Energizer">*</asp:RangeValidator>
                            </td>
                </tr>
            </table>
            <div id ="panel" style="font-size:10pt;" >
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                                <asp:GridView ID="gridProductosCliente" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="id_producto" Width="100%" style="text-align: center" ShowFooter="True" 
                    Caption="PRODUCTOS ENERGIZER" CssClass="grid-view">
                    <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    <Columns>
                        <asp:BoundField HeaderText="Código" DataField="codigo" />
                        <asp:BoundField HeaderText="Producto" DataField="nombre_producto" />
                        <asp:TemplateField HeaderText="Existencia">
                            <ItemTemplate>
                                <asp:TextBox ID="txtExistencia" runat="server" MaxLength="3" Width="50" Text='<%# DataBinder.Eval(Container.DataItem, "Existencia") %>'></asp:TextBox>
                                <asp:RangeValidator id="rvaExistencia" runat="server" ErrorMessage="Solo Numeros" Display="Dynamic"
                                    MinimumValue="0" MaximumValue="9999" Type="Double" ControlToValidate="txtExistencia" ValidationGroup="Energizer">*</asp:RangeValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pedidos">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPedidos" runat="server" MaxLength="3" Width="50" Text='<%# DataBinder.Eval(Container.DataItem, "Pedidos") %>'></asp:TextBox>
                                <asp:RangeValidator id="rvaPedidos" runat="server" ErrorMessage="Solo Numeros" Display="Dynamic"
                                    MinimumValue="0" MaximumValue="9999" Type="Double" ControlToValidate="txtPedidos" ValidationGroup="Energizer">*</asp:RangeValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
 
                    </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                    <EmptyDataTemplate>
                        <h1>No hay datos</h1>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div> 

            <br />
                               
                       <asp:GridView ID="gridCompetencia" runat="server" 
                            AutoGenerateColumns="False" DataKeyNames="id_producto" Width="99%" 
                            style="text-align: center" ShowFooter="True" 
                            Caption="COMPETENCIA" CaptionAlign="Top" CssClass="grid-view">
                           <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    <Columns>
                        <asp:BoundField HeaderText="Producto" DataField="nombre_producto" />
                        <asp:TemplateField HeaderText="Existencia">
                            <ItemTemplate>
                                <asp:TextBox ID="txtExistencia" runat="server" MaxLength="3" Width="50" Text='<%#DataBinder.Eval(Container.DataItem, "Existencia") %>'></asp:TextBox>
                                <asp:RangeValidator id="rvaExistencia" runat="server" ErrorMessage="Solo Numeros" Display="Dynamic" 
                                    MinimumValue="0" MaximumValue="9999" Type="Double" ControlToValidate="txtExistencia" ValidationGroup="Energizer">*</asp:RangeValidator>
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
                  
                       <table style="border-style: groove; width: 100%; height: 99px; margin-right: 0px" >
                           <tr style="color: #FFFFFF">
                               <td style="text-align: center; background-color: #000000; height: 29px; width: 616px;" bgcolor="#507CD1" >
                                   PROMOCIONES EVEREADY&nbsp;</td></tr>
                           <tr style="font-size: small">
                               <td style="font-size: x-small; height: 75px; width: 616px;" >
                                   <asp:TextBox ID="txtPromoE" runat="server" Width="527px" Height="60px" 
                                       TextMode="MultiLine"></asp:TextBox></td></tr>
                           <tr style="color: #FFFFFF">
                               <td style="text-align: center; background-color: #000000; height: 29px; width: 616px;" bgcolor="#507CD1" >
                                   PROMOCIONES DURACELL&nbsp;</td></tr>
                           <tr style="font-size: small">
                               <td style="font-size: x-small; height: 75px; width: 616px;" >
                                   <asp:TextBox ID="txtPromoD" runat="server" Width="527px" Height="60px" 
                                       TextMode="MultiLine"></asp:TextBox></td></tr>
                           </table>
                               
                  <br />
                
                       <table style="border-style: groove; width: 100%; height: 99px; margin-right: 0px" >
                           <tr style="color: #FFFFFF">
                               <td style="text-align: center; background-color: #000000; height: 29px; width: 616px;" bgcolor="#507CD1" >
                                   COMENTARIOS ENERGIZER</td></tr>
                           <tr style="font-size: small">
                               <td style="font-size: x-small; height: 75px; width: 616px;" >
                                   <asp:TextBox ID="txtComentarios" runat="server" Width="527px" Height="60px" 
                                       TextMode="MultiLine"></asp:TextBox></td></tr>
                           </table>
                               
                <br />         
                         
                            <table style="border-style: groove; width: 99%; height: 15px; margin-right: 0px" >
                           <tr>
                               <td style="text-align: center; background-color: #000000; height: 15px; " 
                                    colspan="3" >
                                   FOTOGRAFÍAS</td>
                                </tr>
                           <tr style="color: #FFFFFF">
                               <td style="text-align: center; background-color: #000000; height: 15px; width: 538px;" 
                                   bgcolor="black" >
                                   Descripción</td>
                               <td style="text-align: left; height: 15px; " align="left" colspan="2" >
                                   <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" 
                                       Width="424px" MaxLength="50"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" 
                                       ControlToValidate="txtDescripcion" ErrorMessage="*" Font-Bold="True" 
                                       ValidationGroup="Foto"></asp:RequiredFieldValidator>
                                   </td></tr>
                           <tr style="color: #FFFFFF">
                               <td style="text-align: center; background-color: #000000; height: 15px; width: 538px;" 
                                   bgcolor="black" >
                                   Fotografía</td>
                               <td style="text-align: left; height: 15px; width: 616px;" align="left" >
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="312px" />
                                   <asp:RequiredFieldValidator ID="rfvFile" runat="server" 
                                       ControlToValidate="FileUpload1" ErrorMessage="*" Font-Bold="True" 
                                       ValidationGroup="Foto"></asp:RequiredFieldValidator>
                                   </td>
                               <td style="text-align: center; height: 15px; width: 616px;" align="left" >
                        <asp:Button ID="btnSubir" runat="server" Text="Subir" 
                            ValidationGroup="Foto" CssClass="button" />
                                   </td></tr>
                           <tr style="color: #FFFFFF">
                               <td style="text-align: center; background-color: #000000; height: 15px; width: 538px;" 
                                   bgcolor="black" >
                                   </td>
                               <td style="text-align: left; height: 15px; background-color: #FFFFFF;" 
                                   align="left" colspan="2" >
                                   <asp:Label ID="lblSubida" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                   </td></tr>
                           <tr style="color: #FFFFFF">
                               <td style="text-align: center; height: 15px; " colspan="3" >
                               
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">  
                    <ContentTemplate>   
                        <asp:GridView ID="gridImagenes" runat="server" AutoGenerateColumns="False" 
                            CellPadding="3" Width="515px" BackColor="White" BorderColor="#999999" 
                                       BorderStyle="None" BorderWidth="1px" 
                            GridLines="Vertical" ShowFooter="True">
                            <RowStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" 
                                VerticalAlign="Top" BackColor="#EEEEEE" ForeColor="Black" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" 
                                    DeleteImageUrl="~/sitios/Cloe/Imagenes/delete-icon.png" 
                                    ShowDeleteButton="True" />
                                <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                <asp:TemplateField HeaderText="Foto">
                                    <ItemTemplate>
                                        <img alt="Foto" height="200" src='<%#Eval("ruta")%><%#Eval("foto")%>' 
                                            width="180" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" 
                                HorizontalAlign="Center" VerticalAlign="Top" />
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="#DCDCDC" />
                        </asp:GridView>
                       </ContentTemplate>
                    </asp:UpdatePanel>
                   </td>
                    </tr>
               </table>      
                                <asp:UpdatePanel ID="UpdatePanel" runat="server">
                                    <ContentTemplate>
                                        <table style="width: 100%">
                                            <tr>
                                                <td colspan="2" style="text-align: center; height: 31px; ">
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
                                                        ValidationGroup="Energizer" />
                                                        <asp:Label ID="lblFecha" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                                        <br />
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
                                                    <asp:Button ID="btnGuardar" runat="server" CssClass="button" Text="Guardar" 
                                                        ValidationGroup="Energizer" />
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" 
                                                        CssClass="button" Text="Cancelar" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnGuardar" />
                                    </Triggers>
                                </asp:UpdatePanel>
                        </ContentTemplate>
                </asp:UpdatePanel>
                      
                        </div><!--CONTENT SIDE COLUMN AVISOS--><div id="content-side-two-column">
            </div><div class="clear">
            </div>
        </div>
   </div>
</asp:Content>