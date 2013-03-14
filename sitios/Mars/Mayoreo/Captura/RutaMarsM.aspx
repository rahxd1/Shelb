<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Mars/Mayoreo/MayoreoMars.Master"
    AutoEventWireup="false" 
    CodeBehind="RutaMarsM.aspx.vb" 
    Inherits="procomlcd.RutaMarsM"
    Title="Mars Mayoreo - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoMayoreoMars" runat="Server">
  
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
                                  Regi�n:</td>
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
                          <td style="text-align: right; width: 190px;">
                              Periodo:</td>
                          <td>
                  <asp:DropDownList ID="cmbPeriodo" runat="server" Height="22px" Width="210px" 
                                  AutoPostBack="True">
                    </asp:DropDownList>
                          </td>
                      </tr>
                      <tr>
                          <td colspan="2">
                   
                           <asp:GridView ID="gridRutas" runat="server" AutoGenerateColumns ="False" 
                               Width="100%" style="text-align: center" ShowFooter="True" 
                                  CssClass="grid-view" GridLines="Horizontal" >
                            <Columns>             
                                <asp:BoundField HeaderText="" DataField="captura" HtmlEncode="false" />                                
                                <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena" />
                                <asp:BoundField HeaderText="Tienda" DataField="nombre" />
                                 <asp:TemplateField HeaderText="Levantamiento Q1" >
                                      <ItemTemplate>
                                            <%#Eval("estatus_q1")%>
                                            <a href="FormatoCapturaMarsM.aspx?folio=<%#Eval("folio_historial1")%>&tienda=<%#Eval("id_tienda")%>&usuario=<%#Eval("id_usuario")%>&periodo=<%#Eval("orden")%>&quincena=Q1&nombre=<%#Eval("nombre")%>"><%#Eval("ver_q1")%></a>
                                       </ItemTemplate>
                                      <ItemStyle />
                                 </asp:TemplateField> 
                                 <asp:TemplateField HeaderText="Levantamiento Q2" >
                                      <ItemTemplate>
                                            <%#Eval("estatus_q2")%>
                                            <a href="FormatoCapturaMarsM.aspx?folio=<%#Eval("folio_historial2")%>&tienda=<%#Eval("id_tienda")%>&usuario=<%#Eval("id_usuario")%>&periodo=<%#Eval("orden")%>&quincena=Q2&nombre=<%#Eval("nombre")%>"><%#Eval("ver_q2")%></a>
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
