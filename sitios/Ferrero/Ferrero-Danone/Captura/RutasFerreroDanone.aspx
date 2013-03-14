<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Ferrero/Ferrero-Danone/FerreroDanone.Master"
    AutoEventWireup="false" 
    CodeBehind="RutasFerreroDanone.aspx.vb" 
    Inherits="procomlcd.RutasFerreroDanone"
    Title="Ferrero - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentFerreroDanone" runat="Server">
  
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
                          <td style="text-align: right; width: 190px;">
                              Periodo:</td>
                          <td>
                  <asp:DropDownList ID="cmbPeriodo" runat="server" Height="22px" Width="210px" 
                                  AutoPostBack="True">
                    </asp:DropDownList>
                          </td>
                      </tr>
                      <tr>
                          <td colspan="2" style="text-align: center">
                              
                              <asp:Button ID="btnAgregaTienda" runat="server" Text="Agrega Tienda" 
                                  Height="37px" Width="173px" style="text-align: center" />
                            
                              </td>
                      </tr>
                      <tr>
                          <td colspan="2">
                           <asp:GridView ID="gridRutas" runat="server" AutoGenerateColumns ="False" 
                               Width="100%" style="text-align: center" ShowFooter="True" 
                                  CssClass="grid-view-ruta" >
                            <Columns>             
                                <asp:BoundField HeaderText="" DataField="captura" HtmlEncode="false" />                                
                                <asp:BoundField HeaderText="Tienda" DataField="nombre_tienda" />
                                <asp:BoundField HeaderText="Colonia" DataField="colonia" />
                                 <asp:TemplateField >
                                      <ItemTemplate>
                                       <a href="FormatoCapturaFerreroDanone.aspx?folio=<%#Eval("folio_historial")%>">Ver</a>
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
              Estatus Captura:</h2>
            <ul class="list-of-links">
                <li><img src="../../../../Img/Capturado.gif" alt="C"/> Tienda Capturada</li>
                <li><img src="../../../../Img/Falta.gif" alt="C"/> Tienda falta por capturar</li>
                <li><img src="../../../../Img/Pendiente.gif" alt="C"/> Tienda incompleta</li>
             </ul>
             <br />
            <h2>
                Estatus Tienda:</h2>
                <ul class="list-of-links">    
                <li><img src="../../../../Img/Abierta.png" alt="C"/> Tienda Abierta</li>
                <li><img src="../../../../Img/Cerrado.png" alt="C"/> Tienda Cerrada</li>
            </ul>
        </div>
       
        
        <div class="clear"></div>
         </div>
         
</asp:Content>
