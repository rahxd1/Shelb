<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/MARS/Mayoreo/MayoreoMars.Master"
    AutoEventWireup="false" 
    CodeBehind="RutaMarsVP.aspx.vb" 
    Inherits="procomlcd.RutaMarsVP"
    title="Mars Verificadores de Precio Mayoreo - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoMayoreoMars" runat="Server">
  
<!--CONTENT CONTAINER-->
    <div id="titulo-pagina">
        Ruta</div>
    
        <div id="contenido-columna-derecha">
        
            <!--CONTENT MAIN COLUMN-->
            <div id="content-main-two-column">
              <asp:Panel ID="panelMenu" runat="server" Visible="False" Width="100%">
                  <table style="width: 100%">
                      <tr>
                          <td style="text-align: right; width: 190px">
                              Región:</td>
                          <td>
                              <asp:DropDownList ID="cmbRegion" runat="server" AutoPostBack="True" 
                                  Height="22px" Width="150px">
                              </asp:DropDownList>
                          </td>
                      </tr>
                      <tr>
                          <td style="text-align: right; width: 190px">
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
              <asp:DropDownList ID="cmbPeriodo" runat="server" Height="22px" Width="182px" 
                              AutoPostBack="True">
                </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 190px">
                            Semana:</td>
                        <td>
              <asp:DropDownList ID="cmbSemana" runat="server" Height="21px" Width="139px" 
                              AutoPostBack="True">
                </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                           <asp:Panel ID="pnlTabla" runat="server" Visible="False" Width="100%">
                           <table style="text-align: justify; vertical-align: top; top: 0px; height: 191px; width: 660px;" 
                                   width="100%">
                               <tr>
                                   <td style="height: 18px; font-weight: 700; text-align: center; width: 325px;" 
                                       valign="top" bgcolor="#FFFFCC">
                                       LUNES</td>
                                   <td style="height: 18px; font-weight: 700; text-align: center;" valign="top" 
                                       bgcolor="#FFFFCC">
                                       MARTES</td>
                               </tr>
                               <tr>
                                   <td valign="top" style="width: 325px; text-align: center;">
                                       <asp:GridView ID="gridDia1" runat="server" AutoGenerateColumns="False" 
                                           Height="16px" 
                                           Width="96%" ShowFooter="True" CssClass="grid-view">
                                           <Columns>
                                               <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena" />
                                               <asp:TemplateField HeaderText="Tienda" >
                                                <ItemTemplate>
                                                <a href="FormatoCapturaMarsVP.aspx?Folio=<%#Eval("folio_historial")%>&Tienda=<%#Eval("id_tienda")%>&Dia=<%#Eval("id_dia")%>&Semana=<%#Eval("id_semana")%>&Periodo=<%#Eval("orden")%>&Usuario=<%#Eval("id_usuario")%>&Nombre=<%#Eval("nombre")%>"><%#Eval("nombre")%></a>
                                                </ItemTemplate></asp:TemplateField>
                                                <asp:BoundField DataField="estatus" HtmlEncode="false" />
                                           </Columns>
                                           <FooterStyle CssClass="grid-footer" />
                                       </asp:GridView>
                                   </td>
                                   <td align="left" valign="top" style="text-align: center">
                                       <asp:GridView ID="gridDia2" runat="server" AutoGenerateColumns="False" 
                                           Height="16px"
                                           Width="96%" ShowFooter="True" CssClass="grid-view">
                                           <Columns>
                                                <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena" />
                                                <asp:TemplateField HeaderText="Tienda" >
                                                <ItemTemplate>
                                                <a href="FormatoCapturaMarsVP.aspx?Folio=<%#Eval("folio_historial")%>&Tienda=<%#Eval("id_tienda")%>&Dia=<%#Eval("id_dia")%>&Semana=<%#Eval("id_semana")%>&Periodo=<%#Eval("orden")%>&Usuario=<%#Eval("id_usuario")%>&Nombre=<%#Eval("nombre")%>"><%#Eval("nombre")%></a>
                                                </ItemTemplate></asp:TemplateField>
                                                <asp:BoundField DataField="estatus" HtmlEncode="false" />
                                           </Columns>
                                           <FooterStyle CssClass="grid-footer" />
                                       </asp:GridView>
                                   </td>
                               </tr>
                               <tr>
                                   <td bgcolor="#FFFFCC" 
                                       style="font-weight: 700; text-align: center; width: 325px" valign="top">
                                       MIÉRCOLES</td>
                                   <td align="left" bgcolor="#FFFFCC" style="font-weight: 700; text-align: center" 
                                       valign="top">
                                       JUEVES</td>
                               </tr>
                               <tr>
                                   <td style="width: 325px; text-align: center;" valign="top">
                                       <asp:GridView ID="gridDia3" runat="server" AutoGenerateColumns="False" 
                                           CssClass="grid-view" Height="16px" ShowFooter="True" Width="96%">
                                           <Columns>
                                               <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena" />
                                               <asp:TemplateField HeaderText="Tienda">
                                                   <ItemTemplate>
                                                       <a href='FormatoCapturaMarsVP.aspx?Folio=<%#Eval("folio_historial")%>&amp;Tienda=<%#Eval("id_tienda")%>&amp;Dia=<%#Eval("id_dia")%>&amp;Semana=<%#Eval("id_semana")%>&amp;Periodo=<%#Eval("orden")%>&amp;Usuario=<%#Eval("id_usuario")%>&amp;Nombre=<%#Eval("nombre")%>'>
                                                       <%#Eval("nombre")%></a>
                                                   </ItemTemplate>
                                               </asp:TemplateField>
                                               <asp:BoundField DataField="estatus" HtmlEncode="false" />
                                           </Columns>
                                           <FooterStyle CssClass="grid-footer" />
                                       </asp:GridView>
                                   </td>
                                   <td align="left" valign="top" style="text-align: center">
                                       <asp:GridView ID="gridDia4" runat="server" AutoGenerateColumns="False" 
                                           CssClass="grid-view" Height="16px" ShowFooter="True" Width="96%">
                                           <Columns>
                                               <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena" />
                                               <asp:TemplateField HeaderText="Tienda">
                                                   <ItemTemplate>
                                                       <a href='FormatoCapturaMarsVP.aspx?Folio=<%#Eval("folio_historial")%>&amp;Tienda=<%#Eval("id_tienda")%>&amp;Dia=<%#Eval("id_dia")%>&amp;Semana=<%#Eval("id_semana")%>&amp;Periodo=<%#Eval("orden")%>&amp;Usuario=<%#Eval("id_usuario")%>&amp;Nombre=<%#Eval("nombre")%>'>
                                                       <%#Eval("nombre")%></a>
                                                   </ItemTemplate>
                                               </asp:TemplateField>
                                               <asp:BoundField DataField="estatus" HtmlEncode="false" />
                                           </Columns>
                                           <FooterStyle CssClass="grid-footer" />
                                       </asp:GridView>
                                   </td>
                               </tr>
                           </table>
                           
                           </asp:Panel>
                           </td>
                    </tr>
                </table>
        </div>
        
<!--CONTENT SIDE COLUMN AVISOS-->
        <div id="content-side-two-column">
            <h2>
              Estatus tienda:</h2>
            <ul class="list-of-links">
                <li><img src="../../../../Img/Capturado.gif" alt="C"/> Capturada</li>
                <li><img src="../../../../Img/Falta.gif" alt="C"/> Sin capturar</li>
                <li><img src="../../../../Img/Pendiente.gif" alt="C"/> Incompleta</li>
             </ul>
             <br />
            <h2>
                Estatus periodo:</h2>
                <ul class="list-of-links">    
                <li><img src="../../../../Img/Abierta.png" alt="C"/> Abierto</li>
                <li><img src="../../../../Img/Cerrado.png" alt="C"/> Cerrado</li>
            </ul>
        </div>
       
        
        <div class="clear"></div>
         </div>
         
</asp:Content>
