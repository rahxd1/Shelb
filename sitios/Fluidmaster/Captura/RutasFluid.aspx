<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Fluidmaster/Fluidmaster.Master"
    AutoEventWireup="false" 
    CodeBehind="RutasFluid.aspx.vb" 
    Inherits="procomlcd.RutasFluid"
    Title="Fluidmaster - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoFluid" runat="Server">
  
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
                   
                                          <asp:Panel ID="pnlGrilla" runat="server" Visible="False">
                                          
                                           <asp:GridView ID="gridRutas" runat="server" AutoGenerateColumns ="False" 
                                               Width="100%" style="text-align: center" ShowFooter="True" 
                                                  GridLines="Horizontal"   
                                                  CssClass="grid-view-ruta" >
                                            <Columns>             
                                                <asp:BoundField HeaderText="" DataField="captura" HtmlEncode="false" />                                
                                                <asp:BoundField HeaderText="Distribuidor" DataField="nombre_cadena" />
                                                <asp:BoundField HeaderText="Tienda" DataField="nombre" />
                                                <asp:TemplateField HeaderText="Captura distribuidor" >
                                                      <ItemTemplate>
                                                            <a href="FormatoDistribuidorFluid.aspx?folio=<%#Eval("folio_historial")%>&cadena=<%#Eval("id_cadena")%>&usuario=<%#Eval("id_usuario")%>&periodo=<%#Eval("id_periodo")%>"><%#Eval("VerCaptura")%><%#Eval("Agregar")%></a>
                                                       </ItemTemplate>
                                                      <ItemStyle />
                                                 </asp:TemplateField>                               
                                                 <asp:TemplateField HeaderText="Captura subdistribuidor" >
                                                      <ItemTemplate>
                                                            <a href="FormatoSubDistribuidorFluid.aspx?folio=<%#Eval("folio_historial")%>&cadena=<%#Eval("id_cadena")%>&usuario=<%#Eval("id_usuario")%>&periodo=<%#Eval("id_periodo")%>"><%#Eval("VerCapturaSub")%><%#Eval("AgregarSub")%></a>
                                                       </ItemTemplate>
                                                      <ItemStyle />
                                                 </asp:TemplateField> 
                                                </Columns>
                                               <FooterStyle CssClass="grid-footer" />
                                        </asp:GridView>
                                        
                                          <br />
                                          <table style="width: 100%">
                                              <tr>
                                                  <td style="height: 21px; text-align: center; width: 31px;">
                                                        <img alt="" src="../../../Img/Agregar.png" /></td>
                                                  <td style="text-align: left; width: 158px;" valign="bottom">
                                                        <h3><asp:LinkButton ID="lnkFotografias" runat="server" Font-Bold="True">Agregar fotografías</asp:LinkButton></h3>
                                                  </td>
                                                  <td style="text-align: left" valign="bottom">
                                                      &nbsp;</td>
                                              </tr>
                                          </table>
                                        
                                        </asp:Panel>  
                                          
                                          </td>
                      </tr>
                      <tr>
                          <td style="width: 190px">
                              &nbsp;</td>
                          <td>
                              &nbsp;</td>
                      </tr>
                </table>
                  
                  
            </div>
            
       
        
<!--CONTENT SIDE COLUMN AVISOS-->
        <div id="content-side-two-column">
            <h2>Estatus periodo:</h2>
                <ul class="list-of-links">    
                <li><img src="../../../Img/Abierta.png" alt="C"/> Abierto</li>
                <li><img src="../../../Img/Cerrado.png" alt="C"/> Cerrado</li>
            </ul>
            
            <p></p>
            
        </div>
       
        
        <div class="clear"></div>
         </div>
         
</asp:Content>
