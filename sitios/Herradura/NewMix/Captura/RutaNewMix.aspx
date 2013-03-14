<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Herradura/NewMix/NewMix.Master"
    AutoEventWireup="false" 
    CodeBehind="RutaNewMix.aspx.vb" 
    Inherits="procomlcd.RutaNewMix"
    Title="New Mix - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderNewMix" runat="Server">
  
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
                            <asp:BoundField HeaderText="" DataField="captura" HtmlEncode="false" />
                            <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena" />
                            <asp:BoundField HeaderText="Tienda" DataField="nombre" />
                            <asp:TemplateField HeaderText="Estatus precios"  >
                                <ItemTemplate >
                                    <%#Eval("estatus_cadena")%>
                                    <a href="FormatoPreciosNewMix.aspx?usuario=<%#Eval("id_usuario")%>&periodo=<%#Eval("id_periodo")%>&cadena=<%#Eval("nombre_cadena")%>&id_cadena=<%#Eval("id_cadena")%>&folio=<%#Eval("folio_historial")%>"><%#Eval("ver_cadena")%></a>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="Estatus tienda" >
                                <ItemTemplate>
                                    <%#Eval("estatus_tienda")%>
                                    <a href="FormatoCapturaNewMix.aspx?tienda=<%#Eval("id_tienda")%>&usuario=<%#Eval("id_usuario")%>&periodo=<%#Eval("id_periodo")%>&folio=<%#Eval("folio_historial")%>&nombre=<%#Eval("nombre")%>"><%#Eval("ver_tienda")%></a>
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
            <h2>Estatus tienda:</h2>
            <ul class="list-of-links">
                <li><img src="../../../../Img/Capturado.gif" alt="Capturado"/> Capturada</li>
                <li><img src="../../../../Img/Falta.gif" alt="Falta"/> Sin capturar</li>
                <li><img src="../../../../Img/Pendiente.gif" alt="Pendiente"/> Incompleta</li>
             </ul>
             <br />
            <h2>
                Estatus periodo:</h2>
                <ul class="list-of-links">    
                <li><img src="../../../../Img/Abierta.png" alt="Tienda abierta"/> Abierto</li>
                <li><img src="../../../../Img/Cerrado.png" alt="Tienda Cerrada"/> Cerrado</li>
            </ul>
        </div>
       
        
        <div class="clear">
        
        </div>
    </div>
    
</asp:Content>
