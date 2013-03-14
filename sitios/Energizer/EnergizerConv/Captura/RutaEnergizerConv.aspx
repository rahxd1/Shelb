<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Energizer/EnergizerConv/Energizer_Conv.Master"
    AutoEventWireup="false" 
    CodeBehind="RutaEnergizerConv.aspx.vb" 
    Inherits="procomlcd.RutaEnergizerConv"
    title="Energizer Conveniencia - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1EnergizerConv" runat="Server">
  
    <!--CONTENT CONTAINER-->
<div id="titulo-pagina">
    Rutas</div>
        <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
              <asp:Panel ID="panelMenu" runat="server" Visible="False">
                  <table style="width: 100%">
                      <tr>
                          <td style="border-top-color: #cccccc; border-bottom-style: outset; border-top-style: outset; border-bottom-color: #ffffff; text-align: right;">
                              Región:
                          </td>
                          <td style="border-top-color: #cccccc; border-bottom-style: outset; border-top-style: outset; border-bottom-color: #ffffff">
                              <asp:DropDownList ID="cmbRegion" runat="server" Height="22px" Width="150px" 
                                  AutoPostBack="True" >
                              </asp:DropDownList>
                          </td>
                          <td style="border-top-color: #cccccc; border-bottom-style: outset; border-top-style: outset; border-bottom-color: #ffffff; text-align: right;">
                              Promotor:
                          </td>
                          <td style="border-top-color: #cccccc; border-bottom-style: outset; border-top-style: outset; border-bottom-color: #ffffff">
                              <asp:DropDownList ID="cmbPromotor" runat="server" Height="22px" Width="150px" 
                                  AutoPostBack="True" >
                              </asp:DropDownList>
                          </td>
                      </tr>
                  </table>
              </asp:Panel>
              <br />
              <table style="width: 100%">
                  <tr>
                      <td style="border-top-style: outset; border-bottom-style: outset; border-top-color: #CCCCCC; border-bottom-color: #000000; text-align: right; width: 123px;">
                          Periodo Captura</td>
                      <td style="border-top-style: outset; border-bottom-style: outset; border-top-color: #CCCCCC; border-bottom-color: #000000">
                              <asp:DropDownList ID="cmbPeriodo" runat="server" Height="21px" 
                                  Width="296px" AutoPostBack="True">
                </asp:DropDownList>
                          </td>
                  </tr>
                  <tr>
                      <td colspan="2" 
                          
                          style="border-top-style: outset; border-bottom-style: outset; border-top-color: #CCCCCC; border-bottom-color: #000000">
               
                       <asp:GridView ID="gridRutas" runat="server" AutoGenerateColumns ="False" 
                           Caption ='- - - - - - - - - - TIENDAS EN TU RUTA - - - - - - - - - -' 
                           Width="540px" style="text-align: center" ShowFooter="True" 
                              CssClass="grid-view-ruta" >
                        <Columns>                                             
                            <asp:BoundField HeaderText="" DataField="captura" HtmlEncode="false" />
                            <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena" />
                            <asp:BoundField HeaderText="Tienda" DataField="nombre" />
                            <asp:BoundField HeaderText="Estatus" DataField="img_status" HtmlEncode="false" />
                            <asp:TemplateField >
                                <ItemTemplate>
                                <a href="FormatoCapturaEnergizerConv.aspx?folio=<%#Eval("folio_historial")%>&tienda=<%#Eval("id_tienda")%>&usuario=<%#Eval("id_usuario")%>&periodo=<%#Eval("id_periodo")%>">Ver</a>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>                                      
                            </Columns>
                           <FooterStyle CssClass="grid-footer" />
                      
                    </asp:GridView>
                    <div style="text-align: center">
                           <asp:Label ID="Label1" runat="server" Text="-&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; 
                           -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; 
                           -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; 
                           -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -
                       "></asp:Label>
                           </div>
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
       
        
        <div class="clear">
        
        </div>
    </div>
    
</asp:Content>
