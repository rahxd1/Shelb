<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/MasterPage.master"
    AutoEventWireup="false" 
    CodeBehind="AdminAvisos.aspx.vb" 
    Inherits="procomlcd.AdminAvisos" %>

<asp:Content id="Content1" ContentPlaceHolderID="ContenidoProcomlcd" runat="Server">
    <!--
Title Under Menu  
--> 
    <div id="titulo-pagina">
        Crear Avisos</div>

    <div id="contenido-columna-derecha">
        <!--
CONTENT MAIN COLUMN
-->
        <!--

  CENTER COLUMN

  -->
        <div id="content-main-two-column">
            <table>
                <tr>
                    <td style="width: 61px" bgcolor="#999999">
                        <asp:LinkButton ID="linkNuevo" runat="server">Nuevo</asp:LinkButton>
                    </td>
                    <td style="width: 92px" bgcolor="#999999">
                        <asp:LinkButton ID="LinkConsultas" runat="server">Consultas</asp:LinkButton>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                </table>
             <asp:Panel ID="pnlConsultas" runat="server" Width="100%" Visible="False">
                <asp:GridView ID="gridConsultas" runat="server" ShowFooter="True" 
                    AutoGenerateColumns="False" Width="100%" Height="16px" Font-Bold="False" 
                    CssClass="grid-view">
                    <Columns>
                            <asp:CommandField ButtonType="Image" 
                                EditImageUrl="~/Img/Editar.png" ShowEditButton="True" />
                            <asp:BoundField HeaderText="Aviso" DataField="id_aviso"/>
                            <asp:BoundField HeaderText="Fecha" DataField="fecha" DataFormatString="{0:d}" />
                            <asp:BoundField HeaderText="Titulo" DataField="titulo_aviso"/>
                            <asp:BoundField HeaderText="Usuario" DataField="id_usuario"/>
                            <asp:BoundField HeaderText="Leido" DataField="leido"/>
                            <asp:BoundField HeaderText="Fecha Leido" DataField="fecha_leido" DataFormatString="{0:d}" />
                            <asp:BoundField HeaderText="Estatus" DataField="estatus"/>
                            <asp:BoundField HeaderText="Respuesta" DataField="respuesta"/>
                        </Columns>
                    <FooterStyle CssClass="grid-footer" />
                </asp:GridView>
                <br />
                 <asp:Panel ID="pnlAviso" runat="server" Visible="False">
                     <table style="width: 100%">
                         <tr>
                             <td style="width: 135px">
                                 Titulo</td>
                             <td colspan="4">
                                 <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                             </td>
                         </tr>
                         <tr>
                             <td style="width: 135px">
                                 Fecha</td>
                             <td colspan="4">
                                 <asp:Label ID="lblFecha" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                             </td>
                         </tr>
                         <tr>
                             <td style="width: 135px">
                                 Descripción</td>
                             <td colspan="4">
                                 <asp:Label ID="lblDescripcion" runat="server" Font-Bold="True" 
                                     ForeColor="Black"></asp:Label>
                             </td>
                         </tr>
                         <tr>
                             <td style="width: 135px">
                                 Leido</td>
                             <td>
                                 <asp:Label ID="lblLeido" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                             </td>
                             <td colspan="2">
                                 Fecha leido</td>
                             <td>
                                 <asp:Label ID="lblFechaLeido" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                             </td>
                         </tr>
                         <tr>
                             <td style="width: 135px">
                                 Respuesta</td>
                             <td colspan="4">
                                 <asp:Label ID="lblRespuesta" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                             </td>
                         </tr>
                         <tr>
                             <td style="width: 135px; height: 19px;">
                                 Estatus</td>
                             <td colspan="4" style="height: 19px">
                                 <asp:Label ID="lblEstatus" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                             </td>
                         </tr>
                         <tr>
                             <td style="width: 135px">
                                 Cerrado por usuario</td>
                             <td colspan="4">
                                 <asp:Label ID="lblCerrado" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                             </td>
                         </tr>
                         <tr>
                             <td style="width: 135px">
                                 Cerrado por administrador</td>
                             <td colspan="4">
                                 <asp:DropDownList ID="cmbEstatus" runat="server" AutoPostBack="True" 
                                     Height="22px" Width="250px">
                                 </asp:DropDownList>
                             </td>
                         </tr>
                         <tr>
                             <td style="width: 135px">
                                 &nbsp;</td>
                             <td colspan="2">
                                 &nbsp;</td>
                             <td colspan="2" style="text-align: center">
                                 <asp:Button ID="btnEnviar" runat="server" CssClass="button" 
                                     style="text-align: center" Text="Enviar" ValidationGroup="" />
                             </td>
                         </tr>
                         <tr>
                             <td style="width: 135px">
                                 &nbsp;</td>
                             <td colspan="4">
                                 &nbsp;</td>
                         </tr>
                     </table>
                 </asp:Panel>
                <br />
            </asp:Panel>
        
            <asp:Panel ID="panelGrid" runat="server" Width="100%">
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
                            <asp:BoundField HeaderText="Titulo" DataField="titulo_aviso"/>
                            <asp:BoundField HeaderText="Fecha" DataField="fecha" DataFormatString="{0:d}" />
                            <asp:BoundField HeaderText="Leido" DataField="leido"/>
                            <asp:BoundField HeaderText="Estatus" DataField="estatus"/>
                        </Columns>
                    <FooterStyle CssClass="grid-footer" />
                </asp:GridView>
                <br />
                <br />
            </asp:Panel>         
            <asp:Panel ID="pnlAvisos" runat="server" Width="511px" Visible="False">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 102px">
                            Titulo</td>
                        <td colspan="2">
                            <asp:TextBox ID="txtTitulo" runat="server" Width="316px" MaxLength="30"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTitulo" runat="server" 
                                ControlToValidate="txtTitulo" ErrorMessage="*" ValidationGroup=""></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            Cliente</td>
                        <td colspan="2">
                            <asp:DropDownList ID="cmbCliente" runat="server" AutoPostBack="True" 
                                Height="22px" Width="250px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            Plataforma</td>
                        <td colspan="2">
                            <asp:DropDownList ID="cmbPlataforma" runat="server" AutoPostBack="True" 
                                Height="22px" Width="250px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            Tipo usuario</td>
                        <td colspan="2">
                            <asp:DropDownList ID="cmbTipoUsuario" runat="server" AutoPostBack="True" 
                                Height="22px" Width="250px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            Usuario</td>
                        <td colspan="2">
                            <asp:DropDownList ID="cmbUsuario" runat="server" AutoPostBack="True" 
                                Height="22px" Width="250px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            Descripción</td>
                        <td colspan="2">
                            <asp:TextBox ID="txtDescripcion" runat="server" Width="378px" Height="115px" 
                                TextMode="MultiLine" MaxLength="300"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" 
                                ControlToValidate="txtDescripcion" ErrorMessage="*" 
                                ValidationGroup=""></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" 
                                ControlToValidate="txtDescripcion" ErrorMessage="*" ValidateEmptyText="True"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            &nbsp;</td>
                        <td colspan="2">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            <asp:Label ID="lblIDAviso" runat="server" Visible="False" CssClass="aviso"></asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Button ID="btnGuardar" runat="server" style="text-align: center" 
                                Text="Guardar" ValidationGroup="" CssClass="button" />
                        </td>
                        <td style="text-align: center">
                            <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" 
                                style="text-align: center" Text="Cancelar" CssClass="button" />
                        </td>
                    </tr>
                </table>
            <br />
            </asp:Panel>
            
        </div>
        <!-- END MAIN COLUMN -->
        <!--

  CONTENT SIDE 2 COLUMN


  -->
        <div id="content-side-two-column">
            <ul class="list-of-links">
                <li><asp:LinkButton ID="lnkAvisos1" runat="server" Font-Bold="True">Avisos sin leer</asp:LinkButton></li>
                <li><asp:LinkButton ID="lnkAvisos2" runat="server" Font-Bold="True">Avisos leidos</asp:LinkButton></li>
                <li><asp:LinkButton ID="lnkAvisos3" runat="server" Font-Bold="True">Avisos "enterado"</asp:LinkButton></li>
                <li><asp:LinkButton ID="lnkAvisos4" runat="server" Font-Bold="True">Avisos "inconforme"</asp:LinkButton></li>
                <li><asp:LinkButton ID="lnkAvisos5" runat="server" Font-Bold="True">Avisos con respuestas</asp:LinkButton></li>
            </ul>
        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>
