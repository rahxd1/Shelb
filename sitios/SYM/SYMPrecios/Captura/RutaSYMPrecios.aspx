<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/SYM/SYMPrecios/SYMPrecios.Master"
    AutoEventWireup="false" 
    CodeBehind="RutaSYMPrecios.aspx.vb" 
    Inherits="procomlcd.RutaSYMPrecios"
    title="SYM Precios - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1SYMPrecios" runat="Server">
  
<!--CONTENT CONTAINER-->
    <div id="titulo-pagina">
        Ruta</div>
    
        <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
              <asp:Panel ID="pnlMenu" runat="server" Visible="False" Width="100%">
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: right; width: 190px;">
                                División:</td>
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
                                <asp:TemplateField >
                                    <ItemTemplate>
                                        <%#Eval("img_status")%>
                                        <a href="FormatoCapturaSYMPrecios.aspx?cadena=<%#Eval("id_cadena")%>&nombrecadena=<%#Eval("nombre_cadena")%>&periodo=<%#Eval("id_periodo")%>&usuario=<%#Eval("id_usuario")%>&folio=<%#Eval("folio_historial")%>">Ver</a>
                                   </ItemTemplate>
                                  <ItemStyle />
                             </asp:TemplateField>                                        
                            </Columns>
                           <FooterStyle CssClass="grid-footer" />
                      
                    </asp:GridView>
                       </td>
                  </tr>
              </table>
              <br />
        </div>
        
<!--CONTENT MAIN COLUMN-->
        
<!--CONTENT SIDE COLUMN AVISOS-->
   <div id="content-side-two-column">
            <h2>
              Estatus tienda:</h2>
            <ul class="list-of-links">
                <li><img src="../../../../Img/Capturado.gif" alt="C"/> Capturada</li>
                <li><img src="../../../../Img/Falta.gif" alt="C"/> Falta por capturar</li>
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
    
</asp:Content>
