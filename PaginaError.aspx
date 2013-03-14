<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/MasterPage.master" 
    AutoEventWireup="false" 
    CodeBehind="PaginaError.aspx.vb" 
    Inherits="procomlcd.PaginaError" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoProcomlcd" runat="Server">

    <!--

POSTER PHOTO

-->
    <!--

CONTENT CONTAINER

-->
 <div id="contenido-columna-derecha">
        <!--

CONTENT MAIN COLUMN

-->
        <div id="content-main-two-column">
                    <div id="three-column-container">    
    
    
    
        <table class="style5" align="center">
             <tr>
                <td class="style12" rowspan="2">
                                        <img alt="" src="Imagenes/error.PNG" 
                                            style="width: 89px; height: 90px" /><br />
                                        <br />
                                        </td>
                <td class="style12" style="height: 64px">
                                        <asp:Label ID="lblMensaje" runat="server" ForeColor="Black" 
                                            Font-Size="XX-Large" style="font-size: small; font-weight: 700;" 
                                            
                                            
                                            Text="Ha ocurrido un problema en el sitio web, por favor intenta de nuevo o reportanos el problema."></asp:Label>
                    </td>
            </tr>
            
            
             <tr>
                <td class="style12">
                    
            
                    <asp:Panel ID="pnlSoporte" runat="server" BackColor="Silver" 
                                        BorderStyle="Solid" BorderWidth="1px"  Width="516px">
                                        <table style="width: 96%">
                                        <tr>
                                            <td colspan="4" style="text-align: center">
                                                <asp:Label ID="lblAviso" runat="server" 
                                                    style="color: #FF0000; font-weight: 700; text-align: center"></asp:Label>
                                            </td>
                                        </tr>
                                            <tr>
                                                <td style="text-align: left">
                                                    Nombre</td>
                                                <td colspan="3" style="text-align: left">
                                                    <b style="text-align: left">
                                                    <asp:TextBox ID="txtNombre" runat="server" MaxLength="50" 
                                                        style="text-align: left" Width="270px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvNombre" runat="server" 
                                                        ControlToValidate="txtNombre" ErrorMessage="El campo de nombre esta vacio" 
                                                        ValidationGroup="Admin">*</asp:RequiredFieldValidator>
                                                    </b>
                                                </td>
                                            </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                Correo<asp:RegularExpressionValidator ID="revCorreo" runat="server" 
                                                    ControlToValidate="txtCorreo" 
                                                    ErrorMessage="El formato del correo no es correcto" 
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                                    ValidationGroup="Admin">*</asp:RegularExpressionValidator>
                                            </td>
                                            <td colspan="3" style="text-align: left">
                                                <b>
                                                <asp:TextBox ID="txtCorreo" runat="server" MaxLength="50" Width="270px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvCorreo" runat="server" 
                                                    ControlToValidate="txtCorreo" ErrorMessage="El campo de correo esta vacio" 
                                                    ValidationGroup="Admin">*</asp:RequiredFieldValidator>
                                                </b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align: left">
                                                Confirma correo</td>
                                            <td colspan="2" style="text-align: left">
                                                <b>
                                                <asp:TextBox ID="txtCorreoConfirma" runat="server" MaxLength="50" Width="267px"></asp:TextBox>
                                                </b>
                                            </td>
                                        </tr>
                                            <tr>
                                                <td colspan="3" style="width: 75px">
                                                    Teléfono</td>
                                                <td style="width: 268435488px; text-align: left;">
                                                    <b>
                                                    (<asp:TextBox ID="txtLada" runat="server" MaxLength="3" Width="33px"></asp:TextBox>)
                                                    <asp:TextBox ID="txtTelefono" runat="server" MaxLength="15" Width="101px"></asp:TextBox>
                                                    </b>
                                                    <asp:RangeValidator ID="rvaTelefono" runat="server" 
                                                        ControlToValidate="txtTelefono" Display="Dynamic" 
                                                        ErrorMessage="El telefono contiene caracteres que no son números" 
                                                        MaximumValue="999999999999999" MinimumValue="0" Type="Double" 
                                                        ValidationGroup="Admin">*</asp:RangeValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    Problema</td>
                                                <td colspan="3" style="font-weight: 700; text-align: left;">
                                                    <b style="text-align: left">
                                                    <asp:TextBox ID="txtComentario" runat="server" Height="51px" MaxLength="300" 
                                                        style="text-align: left" TextMode="MultiLine" Width="379px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvComentario" runat="server" 
                                                        ControlToValidate="txtComentario" ErrorMessage="*" ValidationGroup="Admin"></asp:RequiredFieldValidator>
                                                    </b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" align="center">
                                                    <asp:Label ID="lblCorreo" runat="server" 
                                                        style="color: #FF0000; font-weight: 700; text-align: center"></asp:Label>
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                                        ValidationGroup="Admin" HeaderText="Completa la información requerida: " 
                                                        ShowMessageBox="True" ShowSummary="False" />
                                                </td>
                                            </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: center;">
                                                <asp:Button ID="btnEnviar" runat="server" CssClass="button" Text="Enviar" 
                                                    ValidationGroup="Admin" />
                                            </td>
                                        </tr>
                                    </table>
          </asp:Panel>
                                                        
                 </td>
            </tr>
            
            
             </table>
    
    
    
                    </div>
    </div>
        <!--

CONTENT SIDE COLUMN AVISOS

-->
        <div id="content-side-two-column">
                    <br />
                    
            
        </div>
        
        <div class="clear">
        
        </div>
    </div>
    
</asp:Content>
