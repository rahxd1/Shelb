<%@ Page Language="vb" 
    Culture="es-MX" 
    MasterPageFile="~/sitios/Fluidmaster/Fluidmaster.Master" 
    AutoEventWireup="false" 
    CodeBehind="FormatoCalendarioFluid.aspx.vb" 
    Inherits="procomlcd.FormatoCapturaFotosFluid"
    Title="Fluidmaster - Fotografías" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoFluid" runat="Server">
  
    <!--CONTENT CONTAINER-->
<div id="titulo-pagina">
    Calendario mensual de visitas</div>
        <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
    
            <table style="width: 100%">
                <tr>
                    <td style="width: 27px">
                        &nbsp;</td>
                    <td style="width: 75px">
                        &nbsp;</td>
                    <td style="width: 75px">
                        &nbsp;</td>
                    <td style="text-align: right">
                        <img alt="" src="../../../Imagenes/arrow.gif" />
                        <a href="RutasFluid.aspx">Regresar</a></td>
                </tr>
                <tr>
                    <td style="width: 27px">
                        &nbsp;</td>
                    <td style="width: 75px">
                        &nbsp;</td>
                    <td style="width: 75px">
                        &nbsp;</td>
                    <td style="text-align: right">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="height: 21px; " colspan="4">
                        <table style="width: 100%">
                            <tr>
                                <td bgcolor="Red" style="text-align: center">
                                    <b>Domingo</b></td>
                                <td bgcolor="Red" style="text-align: center">
                                    <b>Lunes</b></td>
                                <td bgcolor="Red" style="text-align: center">
                                    <b>Martes</b></td>
                                <td bgcolor="Red" style="text-align: center">
                                    <b>Miércoles</b></td>
                                <td bgcolor="Red" style="text-align: center">
                                    <b>Jueves</b></td>
                                <td bgcolor="Red" style="text-align: center">
                                    <b>Viernes</b></td>
                                <td bgcolor="Red" style="text-align: center">
                                    <b>Sábado</b></td>
                            </tr>
                            <tr>
                                <td>
                                        <asp:GridView ID="gridDia1" runat="server" AutoGenerateColumns="False" 
                                            GridLines="Horizontal" Caption="" CssClass="grid-view-ruta" 
                                            DataKeyNames="id_dia" ShowFooter="True" Width="80%">
                                            <Columns>
                                                <asp:BoundField DataField="nombre_exhibidor" HeaderText="Exhibiciones" />
                                                <asp:TemplateField HeaderText="cantidad">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="cmbTienda" runat="server">
                                                        </asp:DropDownList>
                                                        <asp:RangeValidator ID="rvaCantidad" runat="server" 
                                                            ControlToValidate="txtCantidad" Display="Dynamic" 
                                                            ErrorMessage="La cantidad de exhibidores de fluidmaster supera al limite permitido" 
                                                            MaximumValue="9" MinimumValue="0" Type="Double" 
                                                            ValidationGroup="Fluidmaster">*</asp:RangeValidator>
                                                        <asp:RequiredFieldValidator ID="rfvCantidad" runat="server" 
                                                            ControlToValidate="txtCantidad" 
                                                            ErrorMessage="Indica cantidad de exhibidores de fluidmaster" 
                                                            ValidationGroup="Fluidmaster">*</asp:RequiredFieldValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="grid-footer" />
                                            <EmptyDataTemplate>
                                                <h1>
                                                    Se ha producido un error</h1>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                </table>

                        </div><!--CONTENT SIDE COLUMN AVISOS--><div class="clear">
            </div>
        </div>
   </div>
</asp:Content>