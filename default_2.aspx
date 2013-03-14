<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/MasterPage.master" 
    AutoEventWireup="false" 
    CodeBehind="default_2.aspx.vb" 
    Inherits="procomlcd.default_2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoProcomlcd" runat="Server">

    <!--POSTER PHOTO-->
    <div id="poster-contenedor">
        <div id="poster-photo-imagen"></div>
    </div>
    
    <!--CONTENT CONTAINER-->
    <div id="contenido-principal">
 
        <!--CONTENT MAIN COLUMN-->  
        <table style="width: 100%">
            <tr><td style="width: 114px; text-align: center; height: 45px;" align="center" 
                    valign="top"></td>
                <td style="width: 288px; text-align: center; height: 45px;" align="center" 
                    valign="top">
                    <asp:GridView ID="gridAccesos" runat="server" AutoGenerateColumns ="False" Height="16px" 
                            Width="240px" CssClass="grid-view-ruta" CellPadding="4" ForeColor="#333333" 
                            GridLines="Horizontal">
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField HeaderText="Plataforma" >
                                <ItemTemplate>
                                    <i><%#Eval("nombre_proyecto")%></i>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100%" />
                            </asp:TemplateField>                 
                            <asp:TemplateField HeaderText="" >
                               <ItemTemplate>
                                <a href ="<%#Eval("url") %>">Entrar...</a>
                               </ItemTemplate>    
                               <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="0%" />                      
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="grid-footer" BackColor="#507CD1" Font-Bold="True" 
                            ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView></td>
                 <td style="width: 288px; text-align: center; height: 45px;" align="center" 
                        valign="top"></td>
                 <td valign="top" style="height: 45px">
                        <h2 style="text-align: center" __designer:mapid="246">¿Tienes problemas? 
                        <asp:Label ID="lblUsuario" runat="server" 
                                Font-Bold="True" Visible="False"></asp:Label></h2>

                        <asp:Panel ID="pnlSoporte" runat="server" BackColor="Silver" 
                            BorderStyle="Solid" BorderWidth="1px"  Width="372px">
                            <table style="width: 99%">
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
                                                ControlToValidate="txtNombre" 
                                                ErrorMessage="Indica tu nombre por favor" 
                                                ValidationGroup="Admin">*</asp:RequiredFieldValidator> 
                                            </b>
                                        </td>
                                    </tr>
                                <tr>
                                    <td style="text-align: left">
                                        Correo</td>
                                    <td colspan="3" style="text-align: left">
                                        <b>
                                        <asp:TextBox ID="txtCorreo" runat="server" MaxLength="50" Width="270px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revCorreo" runat="server" 
                                            ControlToValidate="txtCorreo" 
                                            ErrorMessage="El formato del correo no es correcto" 
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                            ValidationGroup="Admin">*</asp:RegularExpressionValidator>
                                        </b>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: left">
                                        Confirma correo</td>
                                    <td colspan="2" style="text-align: left">
                                        <b>
                                        <asp:TextBox ID="txtCorreoConfirma" runat="server" MaxLength="50" Width="267px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCorreoConfirma" runat="server" 
                                            ControlToValidate="txtCorreoConfirma" 
                                            ErrorMessage="Confirma tu correo por favor" 
                                            ValidationGroup="Admin">*</asp:RequiredFieldValidator> 
                                        </b>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="width: 75px; text-align: left;">
                                        Teléfono</td>
                                    <td style="width: 268435488px; text-align: left;">
                                        <b>
                                        (<asp:TextBox ID="txtLada" runat="server" MaxLength="3" Width="33px"></asp:TextBox>)
                                        <asp:TextBox ID="txtTelefono" runat="server" MaxLength="15" Width="101px"></asp:TextBox>
                                        </b>
                                        <asp:RequiredFieldValidator ID="rfvTelefono" runat="server" 
                                            ControlToValidate="txtTelefono" 
                                            ErrorMessage="Indica un telefono por favor" 
                                            ValidationGroup="Admin">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" style="text-align: left">
                                        Problema</td>
                                    <td colspan="3" style="font-weight: 700; text-align: left;">
                                        <b style="text-align: left">
                                        <asp:TextBox ID="txtComentario" runat="server" Height="51px" MaxLength="300" 
                                            style="text-align: left" TextMode="MultiLine" Width="269px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvComentario" runat="server" 
                                            ControlToValidate="txtComentario" 
                                            ErrorMessage="Indica tu problema por favor" 
                                            ValidationGroup="Admin">*</asp:RequiredFieldValidator>    
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
                        <br />
          </asp:Panel>
                                                        
                </td>
            </tr>
            <tr>
                <td style="width: 114px; text-align: center;" align="center"></td>
                <td style="width: 288px; text-align: center;" align="center" colspan="2"></td>
                <td valign="top">                                                        
                    <asp:GridView ID="gridReportes" runat="server" AutoGenerateColumns="False" 
                        Caption="Reporte pendiente" Width="100%" ShowHeader="False" 
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
                            <h1>
                                Sin reportes pendientes</h1>
                        </EmptyDataTemplate>
                    </asp:GridView>                    
              </td>
            </tr>
            <tr>
                <td style="text-align: center;" align="center" colspan="4">
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
                                        CssClass="grid-view" ShowFooter="True" Width="250px" 
                                        Caption="Captura Mars Autoservicio">
                                        <Columns>
                                            <asp:BoundField DataField="id_usuario" HeaderText="Promotor" />
                                            <asp:BoundField DataField="Captura" HeaderText="Porcentaje" />
                                        </Columns>
                                        <FooterStyle CssClass="grid-footer" />
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
                                        Caption="Captura Sanchez y Martin Anaquel y Catalogación" CssClass="grid-view" 
                                        ShowFooter="True" Width="250px">
                                        <Columns>
                                            <asp:BoundField DataField="id_usuario" HeaderText="Promotor" />
                                            <asp:BoundField DataField="Captura" HeaderText="Porcentaje" />
                                        </Columns>
                                        <FooterStyle CssClass="grid-footer" />
                                        <EmptyDataTemplate>
                                            <h1>
                                                Sin información</h1>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
      </table>
    </div>
        <!--

CONTENT SIDE COLUMN AVISOS

-->
        
    <div class="clear"> </div>
    
</asp:Content>
