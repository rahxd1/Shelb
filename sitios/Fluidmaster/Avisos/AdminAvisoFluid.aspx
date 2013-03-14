<%@ Page Language="vb"
    Culture="es-MX" 
    Masterpagefile="~/sitios/Fluidmaster/Fluidmaster.Master"
    AutoEventWireup="false" 
    CodeBehind="AdminAvisoFluid.aspx.vb" 
    Inherits="procomlcd.AdminAvisoFluid"
    TITLE="Fluidmaster" %>

<asp:Content id="Content1" ContentPlaceHolderID="ContenidoFluid" runat="Server">
    <!--
Title Under Menu  
--> 
    <div id="titulo-pagina">
        Avisos</div>

    <div id="contenido-columna-derecha">
        <!--
CONTENT MAIN COLUMN
-->
        <!--

  CENTER COLUMN

  -->
        <div id="content-main-two-column">
        
            <asp:Panel ID="pnlVerAvisos" runat="server" Width="100%" Visible="False">
                <asp:GridView ID="gridAvisos" runat="server" ShowFooter="True" 
                    AutoGenerateColumns="False" Width="100%" Height="16px" Font-Bold="False" 
                    CssClass="grid-view">
                    <Columns>
                            <asp:CommandField ButtonType="Image" 
                                EditImageUrl="~/Img/Editar.png" ShowEditButton="True" />
                            <asp:CommandField ButtonType="Image" 
                                DeleteImageUrl="~/Img/delete-icon.png" 
                                ShowDeleteButton="True" />
                            <asp:BoundField HeaderText="Aviso" DataField="id_aviso"/>
                            <asp:BoundField HeaderText="Titulo" DataField="nombre_aviso"/>
                            <asp:BoundField HeaderText="Fecha inicio" DataField="fecha_inicio" DataFormatString="{0:d}" />
                            <asp:BoundField HeaderText="Fecha fin" DataField="fecha_fin" DataFormatString="{0:d}" />
                        </Columns>
                    <FooterStyle CssClass="grid-footer" />
                </asp:GridView>
                <br />
                <br />
            </asp:Panel>         
            <asp:Panel ID="pnlAvisos" runat="server" Width="100%" Visible="False">
                <table style="width: 99%">
                    <tr>
                        <td style="width: 102px">
                            Titulo</td>
                        <td colspan="2">
                            <asp:TextBox ID="txtTitulo" runat="server" Width="316px" MaxLength="80"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTitulo" runat="server" 
                                ControlToValidate="txtTitulo" ErrorMessage="*" ValidationGroup="Jovy"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            Descripción</td>
                        <td colspan="2">
                            <asp:TextBox ID="txtDescripcion" runat="server" Width="494px" Height="68px" 
                                TextMode="MultiLine" MaxLength="300"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" 
                                ControlToValidate="txtDescripcion" ErrorMessage="*" 
                                ValidationGroup="Jovy"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            Fecha inicio (dd/mm/aaaa)</td>
                        <td colspan="2">
                            <asp:TextBox ID="txtFechaInicio" runat="server" MaxLength="10" Width="92px"></asp:TextBox>
                            <asp:RequiredFieldValidator 
                                ID="rfvFechaInicio" runat="server" ControlToValidate="txtFechaInicio" 
                                ErrorMessage="*" ValidationGroup="Jovy"></asp:RequiredFieldValidator>
                                <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            Fecha fin (dd/mm/aaaa)</td>
                        <td colspan="2">
                            <asp:TextBox ID="txtFechaFin" runat="server" MaxLength="10" Width="89px"></asp:TextBox>
                            <asp:RequiredFieldValidator 
                                ID="rfvFechaFin" runat="server" 
                                ControlToValidate="txtFechaFin" ErrorMessage="*" 
                                ValidationGroup="Jovy"></asp:RequiredFieldValidator>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            &nbsp;</td>
                        <td colspan="2">
                            <asp:Label ID="lblIDAviso" runat="server" CssClass="aviso" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            &nbsp;</td>
                        <td style="text-align: center">
                            <asp:Button ID="btnGuardar" runat="server" style="text-align: center" 
                                Text="Guardar" ValidationGroup="Jovy" CssClass="button" />
                        </td>
                        <td style="text-align: center">
                            <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" 
                                style="text-align: center" Text="Cancelar" CssClass="button" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlLecturas" runat="server" Width="100%" Visible="False">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 96px">
                            Aviso</td>
                        <td>
                            <asp:DropDownList ID="cmbAviso" runat="server" AutoPostBack="True" 
                                CssClass="ddl">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 96px">
                            Promotor</td>
                        <td>
                            <asp:DropDownList ID="cmbPromotor" runat="server" AutoPostBack="True" 
                                CssClass="ddl">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:GridView ID="gridLecturas" runat="server" ShowFooter="True" 
                    AutoGenerateColumns="False" Width="100%" Height="16px" Font-Bold="False" 
                    CssClass="grid-view">
                        <Columns>
                            <asp:BoundField HeaderText="Titulo" DataField="nombre_aviso"/>
                            <asp:BoundField HeaderText="Promotor" DataField="id_usuario"/>
                            <asp:BoundField HeaderText="Fecha leido" DataField="fecha_leido" DataFormatString="{0:d}" />
                        </Columns>
                    <FooterStyle CssClass="grid-footer" />
                </asp:GridView>
                <br />
                <br />
            </asp:Panel>
            
        </div>
        
        <div id="content-side-two-column">
              <asp:LinkButton ID="LinkAviso" runat="server">Nuevo aviso</asp:LinkButton>
              <br /><asp:LinkButton ID="LinkVerAvisos" runat="server">Consultar avisos</asp:LinkButton>
              <br /><asp:LinkButton ID="LinkVerLecturas" runat="server">Reporte avisos leidos</asp:LinkButton>
        </div>
           
        <div class="clear">
        </div>
    </div>
</asp:Content>
