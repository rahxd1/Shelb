<%@ Page Language="vb"
    Culture="es-MX" 
    Masterpagefile="~/sitios/Jovy/Jovy.Master"
    AutoEventWireup="false" 
    CodeBehind="AdminAvisoJovy.aspx.vb" 
    Inherits="procomlcd.AdminAvisoJovy" %>

<asp:Content id="Content1" ContentPlaceHolderID="ContentPlaceHolderJovy" runat="Server">
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
        <div id="content-main-three-column">
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
        
            <asp:Panel ID="panelGrid" runat="server" Width="648px">
                <asp:GridView ID="gridAvisos" runat="server" ShowFooter="True" 
                    AutoGenerateColumns="False" Width="536px" Height="16px" Font-Bold="False" 
                    CssClass="grid-view">
                    <Columns>
                            <asp:CommandField ButtonType="Image" 
                                EditImageUrl="~/sitios/Jovy/Imagenes/Editar.png" ShowEditButton="True" />
                            <asp:CommandField ButtonType="Image" 
                                DeleteImageUrl="~/sitios/Jovy/Imagenes/delete-icon.png" 
                                ShowDeleteButton="True" />
                            <asp:BoundField HeaderText="Aviso" DataField="id_aviso"/>
                            <asp:BoundField HeaderText="Titulo" DataField="nombre_aviso"/>
                            <asp:BoundField HeaderText="Fecha Inicio" DataField="fecha_inicio" DataFormatString="{0:d}" />
                            <asp:BoundField HeaderText="Fecha Fin" DataField="fecha_fin" DataFormatString="{0:d}" />
                        </Columns>
                    <FooterStyle CssClass="grid-footer" />
                </asp:GridView>
                <br />
                <br />
            </asp:Panel>         
            <asp:Panel ID="pnlAvisos" runat="server" Width="628px" Visible="False">
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
                            <asp:TextBox ID="txtDescripcion" runat="server" Width="320px" Height="115px" 
                                TextMode="MultiLine" MaxLength="300"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" 
                                ControlToValidate="txtDescripcion" ErrorMessage="*" 
                                ValidationGroup="Jovy"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            Fecha Inicio</td>
                        <td colspan="2">
                            <asp:TextBox ID="txtFechaInicio" runat="server"></asp:TextBox>
                            <a href="javascript:;" 
                                onclick="window.open('../../../Calendario/popup.aspx?textbox=ctl00$ContentPlaceHolderJovy$txtFechaInicio','cal','width=250,height=225,left=270,top=180')">
                            <img alt="" border="0" src="../Img/SmallCalendar.gif" /></a><asp:RequiredFieldValidator 
                                ID="rfvFechaInicio" runat="server" ControlToValidate="txtFechaInicio" 
                                ErrorMessage="*" ValidationGroup="Jovy"></asp:RequiredFieldValidator>
                                <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            Fecha Fin</td>
                        <td colspan="2">
                            <asp:TextBox ID="txtFechaFin" runat="server"></asp:TextBox>
                            <a href="javascript:;" 
                                onclick="window.open('../../../../Calendario/popup.aspx?textbox=ctl00$ContentPlaceHolderJovy$txtFechaFin','cal','width=250,height=225,left=270,top=180')">
                            <img alt="" border="0" src="../Img/SmallCalendar.gif" /></a><asp:RequiredFieldValidator 
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
                            <asp:ImageButton ID="ImagenAviso" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            Imagen</td>
                        <td colspan="2">
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                            <asp:Label ID="lblSubida" runat="server" CssClass="aviso" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            <asp:Label ID="lblIDAviso" runat="server" Visible="False" CssClass="aviso"></asp:Label>
                        </td>
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
            <br />
            </asp:Panel>
            
        </div>
        <!-- END MAIN COLUMN -->
        <!--

  CONTENT SIDE 2 COLUMN


  -->
        <div class="clear">
        </div>
    </div>
</asp:Content>
