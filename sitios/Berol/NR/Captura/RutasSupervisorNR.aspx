<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Berol/NR/Rubbermaid.Master"
    AutoEventWireup="false" 
    CodeBehind="RutasSupervisorNR.aspx.vb" 
    Inherits="procomlcd.RutasSupervisorNR"
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
                          <td style="text-align: right; width: 190px">
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
                                <asp:TemplateField HeaderText="Precios" >
                                      <ItemTemplate>
                                            <%#Eval("estatus_precios")%>
                                            <a href="FormatoPreciosNR.aspx?folio=<%#Eval("folio_historial1")%>&tienda=<%#Eval("id_tienda")%>&usuario=<%#Eval("id_usuario")%>&periodo=<%#Eval("id_periodo")%>">Ver</a>
                                       </ItemTemplate>
                                      <ItemStyle />
                                 </asp:TemplateField>                                      
                                 <asp:TemplateField HeaderText="Fotografias" >
                                      <ItemTemplate>
                                            <%#Eval("estatus_fotos")%>
                                            <a href="FormatoCapturaFotosSupNR.aspx?folio=<%#Eval("folio_historial2")%>&tienda=<%#Eval("id_tienda")%>&usuario=<%#Eval("id_usuario")%>&periodo=<%#Eval("id_periodo")%>">Ver</a>
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
