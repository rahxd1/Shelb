<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/MasterPage.master" 
    AutoEventWireup="false" 
    CodeBehind="default.aspx.vb" 
    Inherits="procomlcd._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoProcomlcd" runat="Server">

    <!--POSTER PHOTO-->
    <div id="poster-contenedor">
        <div id="poster-photo-imagen"></div>
    </div>
    
    <!--CONTENT-->
    <div id="contenido-derecha">
    <!--CONTENT MAIN COLUMN-->
        <div id="contenido-dos-columnas-1">  
            <table style="width: 100%">
                <tr><td style="width: 128px">&nbsp;</td>
                    <td style="width: 613px">
                        <asp:GridView ID="gridAccesos" runat="server" AutoGenerateColumns ="False" 
                            GridLines="None" ShowHeader="False" Height="466px" Width="557px">
                            <Columns>
                                <asp:TemplateField >
                                    <ItemTemplate>
                                        <a href="<%#Eval("url1")%>">
                                        <img src="<%#Eval("imagen1")%>" alt="" style="border: thin solid #999999;" /></a>
                                    </ItemTemplate>
                                </asp:TemplateField>    
                                <asp:TemplateField >
                                    <ItemTemplate>
                                        <a href="<%#Eval("url2")%>">
                                        <img src="<%#Eval("imagen2")%>" alt="" style="border: thin solid #999999;" /></a>
                                    </ItemTemplate>
                                </asp:TemplateField>     
                            </Columns>
                        </asp:GridView></td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
        
        <div id="contenido-dos-columnas-2">
            <ul class="list-of-links">
                <li><asp:LinkButton ID="lnkReporte" runat="server" Font-Bold="True">Levantar reporte</asp:LinkButton>
                    </li>
             </ul>
             
             <br />
             
             <asp:Panel ID="pnlProcom" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:Panel ID="pnlASMars" runat="server">
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gridASMars" runat="server" AutoGenerateColumns="False" 
                                ShowFooter="False" Width="180px" Caption="Captura Mars Autoservicio" 
                                Font-Size="X-Small" GridLines="None" ShowHeader="False">
                                <Columns>
                                    <asp:BoundField DataField="id_usuario" HeaderText="Promotor" />
                                    <asp:BoundField DataField="Captura" HeaderText="%" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <h1>
                                        Sin información</h1>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 158px">
                            <asp:GridView ID="gridSYMAC" runat="server" AutoGenerateColumns="False" 
                                Caption="Captura Sanchez y Martin Anaquel y Catalogación" 
                                ShowFooter="False" Width="180px" Font-Size="X-Small" GridLines="None" 
                                ShowHeader="False">
                                <Columns>
                                    <asp:BoundField DataField="id_usuario" HeaderText="Promotor" />
                                    <asp:BoundField DataField="Captura" HeaderText="%" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <h1>
                                        Sin información</h1>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
            <%--CssClass="pnlDetalle"--%>
            <asp:Panel ID="pnlReporte" CssClass="pnlDetalle" runat="server" Visible="false">
                <table style="width: 800px" align="center">
                    <tr><td style="text-align: center; height: 45px; width: 388px;" align="center" 
                            valign="middle">
                            <h2 style="text-align: center">¿Tienes problemas? 
                            <asp:Label ID="lblUsuario" runat="server" 
                                Font-Bold="True" Visible="False"></asp:Label></h2>
                                
                            <asp:Panel ID="pnlSoporte" runat="server" BackColor="Silver" 
                                BorderStyle="Solid" BorderWidth="1px"  Width="372px">
                                <table style="width: 99%">
                                    <tr><td colspan="4" style="text-align: center">
                                            <asp:Label ID="lblAviso" runat="server" 
                                                style="color: #FF0000; font-weight: 700; text-align: center"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr><td style="text-align: left">Nombre</td>
                                        <td colspan="3" style="text-align: left">
                                            <b style="text-align: left">
                                                <asp:TextBox ID="txtNombre" runat="server" MaxLength="50" 
                                                    style="text-align: left" Width="270px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" 
                                                    ControlToValidate="txtNombre" ErrorMessage="El campo de nombre esta vacio" 
                                                    ValidationGroup="Admin">*</asp:RequiredFieldValidator>
                                            </b></td>
                                    </tr>
                                    <tr><td style="text-align: left">Correo</td>
                                        <td colspan="3" style="text-align: left">
                                            <b>
                                                <asp:TextBox ID="txtCorreo" runat="server" MaxLength="50" Width="270px"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="revCorreo" runat="server" 
                                                    ControlToValidate="txtCorreo" 
                                                    ErrorMessage="El formato del correo no es correcto" 
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                                    ValidationGroup="Admin">*</asp:RegularExpressionValidator>
                                            </b></td>
                                    </tr>
                                    <tr><td colspan="2" style="text-align: left">Confirma correo</td>
                                        <td colspan="2" style="text-align: left">
                                            <b>
                                                <asp:TextBox ID="txtCorreoConfirma" runat="server" MaxLength="50" Width="267px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvCorreo" runat="server" 
                                                    ControlToValidate="txtCorreo" ErrorMessage="El campo de correo esta vacio" 
                                                    ValidationGroup="Admin">*</asp:RequiredFieldValidator>
                                            </b></td>
                                    </tr>
                                    <tr><td colspan="3" style="width: 75px; text-align: left;">
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
                                                ValidationGroup="Admin">*</asp:RangeValidator></td>
                                        </tr>
                                    <tr>
                                        <td valign="top" style="text-align: left">Problema</td>
                                        <td colspan="3" style="font-weight: 700; text-align: left;">
                                            <b style="text-align: left">
                                                <asp:TextBox ID="txtComentario" runat="server" Height="51px" MaxLength="300" 
                                                    style="text-align: left" TextMode="MultiLine" Width="269px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvComentario" runat="server" 
                                                    ControlToValidate="txtComentario" ErrorMessage="*" ValidationGroup="Admin"></asp:RequiredFieldValidator>
                                            </b></td>
                                    </tr>
                                    <tr><td colspan="4" align="center">
                                            <asp:Label ID="lblCorreo" runat="server" 
                                                style="color: #FF0000; font-weight: 700; text-align: center"></asp:Label>
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                                ValidationGroup="Admin" HeaderText="Completa la información requerida: " 
                                                ShowMessageBox="True" ShowSummary="False" /></td>
                                    </tr>
                                    <tr><td colspan="4" style="text-align: center;">
                                        <asp:Button ID="btnEnviar" runat="server" CssClass="button" Text="Enviar" 
                                            ValidationGroup="Admin" /></td>
                                    </tr>
                                </table>
                            </asp:Panel></td>
                        <td style="width: 138px; text-align: right; height: 45px;" align="center" 
                            valign="top">&nbsp;</td>
                        <td style="text-align: right; height: 45px;" align="center" valign="middle">                                                        
                            <asp:GridView ID="gridReportes" runat="server" AutoGenerateColumns="False" 
                                Caption="Reporte pendiente" ShowHeader="False" 
                                BackColor="White" style="text-align: left" Visible="False">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <b>Ticket: <%#Eval("id_reporte")%></b>
                                            <br />Problema: <%#Eval("problema")%>
                                            <br /><i><%#Eval("Fecha")%></i>
                                            
                                            
                                            <br />
                                            <br /><i><font color="red">Fecha leido: <%#Eval("fecha_leido")%></font> </i>
                                            <br /><%#Eval("Comentario_resuelto")%>    
                                            <br />(<%#Eval("dirigido")%>)
                                            
                                        </ItemTemplate>
                                        <ItemStyle Width="2500px" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <font color="red">Sin reportes pendientes</font>
                                </EmptyDataTemplate>
                            </asp:GridView></td>
                        <td align="left" style="text-align: right; height: 45px;" valign="top">
                            <asp:LinkButton ID="lnkCerrar" runat="server" Font-Bold="True">Cerrar</asp:LinkButton>
                        </td>
                      </tr>
                </table>
            </asp:Panel>
        
        <div class="clear"></div>
    </div>
    
</asp:Content>
