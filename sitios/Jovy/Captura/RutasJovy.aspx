<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Jovy/Jovy.Master"
    AutoEventWireup="false" 
    CodeBehind="RutasJovy.aspx.vb" 
    Inherits="procomlcd.RutasJovy"
    Title="Jovy - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderJovy" runat="Server">
  
<!--CONTENT CONTAINER-->
    <div id="titulo-pagina">
        Ruta</div>
    
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
                                  Demostradora:</td>
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
                          <td style="text-align: right; width: 190px;">
                              Periodo:</td>
                          <td>
                  <asp:DropDownList ID="cmbPeriodo" runat="server" Height="22px" Width="350px" 
                                  AutoPostBack="True">
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
                                <asp:BoundField HeaderText="Tienda" DataField="nombre" />
                                <asp:TemplateField HeaderText="Precios e inventarios" >
                                        <ItemTemplate>
                                            <%#Eval("estatus")%>
                                            <a href="FormatoCapturaJovy.aspx?folio=<%#Eval("folio_historial")%>&tienda=<%#Eval("id_tienda")%>&cadena=<%#Eval("id_cadena")%>&usuario=<%#Eval("id_usuario")%>&periodo=<%#Eval("id_periodo")%>">Ver</a>
                                       </ItemTemplate>
                                      <ItemStyle />
                                </asp:TemplateField>     
                                <asp:TemplateField HeaderText="Comentarios (opcional)">
                                      <ItemTemplate>
                                      <%#Eval("estatus_comentarios")%>
                                       <a href="FormatoCapturaComentariosJovy.aspx?folio=<%#Eval("folio_historial")%>&tienda=<%#Eval("id_tienda")%>&usuario=<%#Eval("id_usuario")%>&periodo=<%#Eval("id_periodo")%>"><%#Eval("Ver_Comentarios")%></a>
                                       </ItemTemplate>
                                      <ItemStyle />
                                 </asp:TemplateField>                              
                            </Columns>
                           <FooterStyle CssClass="grid-footer" />
                          
                        </asp:GridView>
                          </td>
                      </tr>
                  </table>
                  
            </div>
            
       
        
<!--CONTENT SIDE COLUMN AVISOS-->
        <div id="content-side-two-column">
            <h2>
              Estatus tienda:</h2>
            <ul class="list-of-links">
                <li><img src="../../../Img/Capturado.gif" alt="C"/> Capturada</li>
                <li><img src="../../../Img/Falta.gif" alt="C"/> Sin capturar</li>
                <li><img src="../../../Img/Pendiente.gif" alt="C"/> Incompleta</li>
             </ul>
             <br />
            <h2>
                Estatus periodo:</h2>
                <ul class="list-of-links">    
                <li><img src="../../../Img/Abierta.png" alt="C"/> Abierto</li>
                    <li>
                        <img src="../../../Img/Cerrado.png" alt="C"/> Cerrado</li>
            </ul>
        </div>
       
        
        <div class="clear"></div>
         </div>
         
</asp:Content>
