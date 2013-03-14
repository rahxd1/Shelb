<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Mars/Capacitacion/Capacitacion_Mars.Master"
    AutoEventWireup="false" 
    CodeBehind="CuestionariosMars.aspx.vb" 
    Inherits="procomlcd.CuestionariosMars"
    Title="Mars - Capacitacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMarsCapacitacion" runat="Server">
  
    <!--CONTENT CONTAINER-->
<div id="titulo-pagina">
    Cuestionarios</div>
        <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
              <asp:Panel ID="panelMenu" runat="server" Visible="False">
                  <table style="width: 100%">
                      <tr>
                          <td style="text-align: right; width: 190px;">
                              Región:</td>
                          <td>
                              <asp:DropDownList ID="cmbRegion" runat="server" AutoPostBack="True" 
                                  Height="22px" Width="150px">
                              </asp:DropDownList>
                          </td>
                      </tr>
                      <tr>
                          <td style="text-align: right; width: 190px;">
                              Promotor:</td>
                          <td>
                              <asp:DropDownList ID="cmbPromotor" runat="server" AutoPostBack="True" 
                                  Height="22px" Width="150px">
                              </asp:DropDownList>
                          </td>
                      </tr>
                  </table>
              </asp:Panel>
               <table style="width: 100%">
                   <tr>
                       <td style="text-align: right; width: 190px">
                           Periodo:</td>
                       <td>
                              <asp:DropDownList ID="cmbPeriodo" runat="server" Height="21px" 
                                  Width="340px" AutoPostBack="True">
                </asp:DropDownList>
                          </td>
                   </tr>
                   <tr>
                       <td colspan="2">
               
                       <asp:GridView ID="gridRutas" runat="server" AutoGenerateColumns ="False" 
                           Width="100%" style="text-align: center" ShowFooter="True" 
                              CssClass="grid-view-ruta" GridLines="Horizontal" >
                        <Columns>                                             
                            <asp:TemplateField HeaderText="Cuestionario">
                                <ItemTemplate >
                                    <a href="FormatoCuestionariosMars.aspx?usuario=<%#Eval("id_usuario")%>&orden=<%#Eval("orden")%>&nombre=<%#Eval("nombre_cuestionario")%>&cuestionario=<%#Eval("id_cuestionario")%>&folio=<%#Eval("folio_historial")%>&preguntas=<%#Eval("preguntas")%>&estatus=<%#Eval("N_estatus")%>&oportunidad=<%#Eval("oportunidad")%>&completado=<%#Eval("completado")%>"><%#Eval("nombre_cuestionario")%></a>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>     
                            <asp:TemplateField HeaderText=""  >
                                <ItemTemplate >
                                    <%#Eval("estatus")%>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>                                
                            </Columns>
                           <FooterStyle CssClass="grid-footer" />
                      
                    </asp:GridView>
                       </td>
                   </tr>
                   
              </table>
            <asp:Panel ID="pnlDatos" runat="server" CssClass="pnlDetalle">
                <table style="width: 100%">
                    <tr>
                        <td colspan="2" style="font-weight: 700">
                            Por favor completa los siguientes campos.</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            Nombre del programa de entrenamiento:</td>
                        <td>
                            <asp:DropDownList ID="cmbPrograma" runat="server">
                            </asp:DropDownList>
                            
                            <asp:RequiredFieldValidator ID="rfvPrograma" runat="server" 
                                ControlToValidate="cmbPrograma" 
                                ErrorMessage="Indica el nombre del programa de entrenamiento" 
                                ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            Tipo de programa:</td>
                        <td>
                            <asp:DropDownList ID="cmbOnline" runat="server">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="3">Online</asp:ListItem>
                                <asp:ListItem Value="2">Presencial</asp:ListItem>
                            </asp:DropDownList>
                            
                            <asp:RequiredFieldValidator ID="rfvOnline" runat="server" 
                                ControlToValidate="cmbOnline" 
                                ErrorMessage="Indica el tipo de programa de entrenamiento" 
                                ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            Tu canal:</td>
                        <td>
                            <asp:DropDownList ID="cmbCanal" runat="server" Width="133px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>Moderno</asp:ListItem>
                                <asp:ListItem>Tradicional</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvCanal" runat="server" 
                                ControlToValidate="cmbCanal" 
                                ErrorMessage="Indica tu canal" 
                                ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            Tu ciudad:</td>
                        <td>
                            <asp:TextBox ID="txtCiudad" runat="server" Width="223px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCiudad" runat="server" 
                                ControlToValidate="txtCiudad" 
                                ErrorMessage="Indica tu ciudad" 
                                ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            Tu nombre:</td>
                        <td>
                            <asp:TextBox ID="txtNombre" runat="server" Width="379px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFechaImplementacion2" runat="server" 
                                ControlToValidate="txtNombre" 
                                ErrorMessage="Indica tu nombre" 
                                ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            El nombre de tu jefe:</td>
                        <td>
                            <asp:TextBox ID="txtJefe" runat="server" Width="377px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvJefe" runat="server" 
                                ControlToValidate="txtJefe" 
                                ErrorMessage="Indica el nombre de tu jefe" 
                                ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            &nbsp;</td>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td style="text-align: center">
                                        <asp:ValidationSummary ID="vsMars" runat="server" 
                                            HeaderText="Por favor corrige los siguientes datos" ShowMessageBox="True" 
                                            ShowSummary="False" ValidationGroup="Mars" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Button ID="btnGuardar" runat="server" style="text-align: center" 
                                            Text="Guardar" ValidationGroup="Mars" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                
            </asp:Panel>
            
        </div>
        
<!--CONTENT SIDE COLUMN AVISOS-->
   <div id="content-side-two-column">
            <h2>Estatus cuestionario:</h2>
            <ul class="list-of-links">
                <li><img src="../../../../Img/Capturado.gif" alt="Capturado"/> Completada</li>
                <li><img src="../../../../Img/Falta.gif" alt="Falta"/> Sin realizar</li>
             </ul>
    </div>
       
        
    <div class="clear">
    </div>
 </div>
    
</asp:Content>
