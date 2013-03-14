<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/SYM/Demos/SYMDemos.Master"
    AutoEventWireup="false" 
    CodeBehind="RutaSYMDemos.aspx.vb" 
    Inherits="procomlcd.RutaSYMDemos"
    title="SYM Demos - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Contenido_SYMDemos" runat="Server">
  
<!--CONTENT CONTAINER-->
    <div id="titulo-pagina">
        Ruta</div>
    
        <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
              <asp:Panel ID="pnlMenu" runat="server" Visible="False" Width="100%">
                  <table style="width: 100%">
                      <tr>
                          <td style="text-align: right; width: 190px">
                              División:</td>
                          <td>
                              <asp:DropDownList ID="cmbRegion" runat="server" AutoPostBack="True" 
                                  Height="22px" Width="150px">
                              </asp:DropDownList>
                          </td>
                      </tr>
                      <tr>
                          <td style="text-align: right; width: 190px">
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
                                  Width="370px" style="text-align: left" AutoPostBack="True">
                </asp:DropDownList>
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
                            <asp:BoundField HeaderText="Tienda" DataField="nombre_tienda" />
                             <asp:TemplateField >
                                  <ItemTemplate>
                                  <%#Eval("Agregar")%>
                                   <a href="FormatoCapturaSYMDemos.aspx?cadena=<%#Eval("id_cadena")%>&nombrecadena=<%#Eval("nombre_cadena")%>&periodo=<%#Eval("id_periodo")%>&usuario=<%#Eval("id_usuario")%>&folio=<%#Eval("folio_historial")%>"><%#Eval("Ver")%></a>
                                   </ItemTemplate>
                                  <ItemStyle />
                             </asp:TemplateField>                                        
                             <asp:TemplateField >
                                  <ItemTemplate>
                                   <a href="FormatoCanjesSYMDemos.aspx?cadena=<%#Eval("id_cadena")%>&nombrecadena=<%#Eval("nombre_cadena")%>&periodo=<%#Eval("id_periodo")%>&usuario=<%#Eval("id_usuario")%>&folio=<%#Eval("folio_historial")%>"><%#Eval("Canjes")%></a>
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
                Estatus periodo:</h2>
                <ul class="list-of-links">    
                <li><img src="../../../../Img/Abierta.png" alt="C"/> Abierto</li>
                <li><img src="../../../../Img/Cerrado.png" alt="C"/> Cerrado</li>
            </ul>
        </div>
       
        
        <div class="clear">
        
        </div>
    </div>
    
</asp:Content>
