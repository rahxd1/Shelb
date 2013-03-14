<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Berol/NR/Rubbermaid.Master"
    AutoEventWireup="false" 
    CodeBehind="RutasPromotorNR.aspx.vb" 
    Inherits="procomlcd.RutasPromotorNR"
    Title="Newell Rubbermaid - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoNR" runat="Server">
  
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
                          <td style="width: 190px; text-align: right">
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
                                  CssClass="grid-view-ruta" GridLines="Horizontal" >
                                    <Columns>             
                                <asp:BoundField HeaderText="" DataField="captura" HtmlEncode="false" />                                
                                <asp:BoundField HeaderText="Tienda" DataField="nombre" />
                                <asp:BoundField HeaderText="Formato" DataField="nombre_cadena" />
                                <asp:TemplateField HeaderText="Frentes y exhibiciones" >
                                      <ItemTemplate>
                                            <%#Eval("estatus_frentes")%>
                                            <a href="FormatoFrentesNR.aspx?folio=<%#Eval("folio_historial")%>&tienda=<%#Eval("id_tienda")%>&usuario=<%#Eval("id_usuario")%>&periodo=<%#Eval("id_periodo")%>"><%#Eval("ver_frentes")%></a>
                                       </ItemTemplate>
                                      <ItemStyle />
                                 </asp:TemplateField>  
                                 <asp:TemplateField HeaderText="Inventarios" >
                                      <ItemTemplate>
                                            <%#Eval("estatus_inventarios")%>
                                            <a href="FormatoInventariosNR.aspx?folio=<%#Eval("folio_historial")%>&tienda=<%#Eval("id_tienda")%>&usuario=<%#Eval("id_usuario")%>&periodo=<%#Eval("id_periodo")%>"><%#Eval("ver_inventarios")%></a>
                                       </ItemTemplate>
                                      <ItemStyle />
                                 </asp:TemplateField>  
                                 <asp:TemplateField HeaderText="Fotografias" >
                                      <ItemTemplate>
                                            <%#Eval("estatus_fotos")%>
                                            <a href="FormatoCapturaFotosNR.aspx?tienda=<%#Eval("id_tienda")%>&usuario=<%#Eval("id_usuario")%>&periodo=<%#Eval("id_periodo")%>"><%#Eval("ver_fotos")%></a>
                                       </ItemTemplate>
                                      <ItemStyle />
                                 </asp:TemplateField>  
                                 <asp:TemplateField HeaderText="Comentarios" >
                                      <ItemTemplate>
                                            <%#Eval("estatus_comentarios")%>
                                            <a href="FormatoComentariosNR.aspx?folio=<%#Eval("folio_historial")%>&tienda=<%#Eval("id_tienda")%>&usuario=<%#Eval("id_usuario")%>&periodo=<%#Eval("id_periodo")%>"><%#Eval("ver_comentarios")%></a>
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
