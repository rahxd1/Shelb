<%@ Page Language="vb" 
MASTERPAGEFILE="~/sitios/SYM/Demos2013/SYMDemos2013.Master"
AutoEventWireup="false"
 CodeBehind="RutasSYMDemos2013.aspx.vb" 
 Inherits="procomlcd.RutasSYMDemos2013" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Contenido_SYMDemos2013" runat="Server">
  
     <!--CONTENT CONTAINER-->
    <div id="titulo-pagina">Ruta</div>
    
        <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
              <asp:Panel ID="pnlMenu" runat="server" Visible="False" Width="100%">
                  <table style="width: 100%">
                      <tr>
                          <td style="text-align: right; width: 190px;">
                              Plaza:</td>
                          <td>
                              <asp:DropDownList ID="cmbRegion" runat="server" AutoPostBack="True" 
                                  Height="22px" Width="150px">
                              </asp:DropDownList>
                          </td>
                      </tr>
                      <tr>
                          <td style="text-align: right; width: 190px;">
                              Supervisor:</td>
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
                                  Width="318px" style="text-align: left" AutoPostBack="True">
                </asp:DropDownList>
                           <asp:Label ID="lblAviso" runat="server" ForeColor="Red"></asp:Label>
                          </td>
                  </tr>
                  <tr>
                      <td colspan="2">
               
                       <asp:GridView ID="gridRutas" runat="server" AutoGenerateColumns ="False" 
                           Width="100%" style="text-align: center" ShowFooter="True" 
                               CssClass="grid-view-ruta" GridLines="Horizontal" >
                        <Columns>                                 
                            <asp:BoundField HeaderText="" DataField="captura" HtmlEncode="false" />            
                            <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena" />
                            <asp:BoundField HeaderText="Tienda" DataField="nombre" />
                             <asp:TemplateField HeaderText="Ventas y Abordos" >
                                  <ItemTemplate>
                                  <%#Eval("status_vtas")%>
                                   <a href="FormatoCapturaSYMDemos2013.aspx?tienda=<%#Eval("id_tienda")%>&periodo=<%#Eval("id_periodo")%>&usuario=<%#Eval("id_usuario")%>&folio=<%#Eval("folio_historial_HV")%>">Entrar</a>
                                   </ItemTemplate>
                                  <ItemStyle />
                             </asp:TemplateField> 
                             <asp:TemplateField HeaderText="Imagenes">
                                  <ItemTemplate>
                                  <%#Eval("status_ima")%>  
                                   <a href="FormatoFotosSYMDemos2013.aspx?tienda=<%#Eval("id_tienda")%>&periodo=<%#Eval("id_periodo")%>&usuario=<%#Eval("id_usuario")%>&folio=<%#Eval("folio_historial_HI")%>">Entrar</a>
                                   </ItemTemplate>
                                  <ItemStyle />
                             </asp:TemplateField>                                        
                            </Columns>
                           <FooterStyle CssClass="grid-footer" />
                      
                    </asp:GridView>
                       </td>
                  </tr>
              </table>
              
              
              <div> 
               
                       
                             
            </div>
        </div>
        
<!--CONTENT MAIN COLUMN-->
        
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
       
        
        <div class="clear">
        
        </div>
    </div>
 
        
    </div>
</asp:Content>