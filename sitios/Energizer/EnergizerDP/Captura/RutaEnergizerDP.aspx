<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Energizer/EnergizerDP/Energizer_DP.Master"
    AutoEventWireup="false" 
    CodeBehind="RutaEnergizerDP.aspx.vb" 
    Inherits="procomlcd.RutaEnergizerDP"
    title="Energizer Pilas Demo 2010 - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1EnergizerDP" runat="Server">
  
<!--CONTENT CONTAINER-->
<div id="titulo-pagina">
    Ruta</div>
        <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
              <asp:Panel ID="panelMenu" runat="server" Visible="False">
                  <table style="width: 100%">
                      <tr>
                          <td style="border-top-color: #cccccc; border-bottom-style: outset; border-top-style: outset; border-bottom-color: #ffffff; text-align: right;">
                              Region:
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
              SELECCIONA PERIODO DE CAPTURA:
              <asp:DropDownList ID="cmbPeriodo" runat="server" Height="21px" Width="309px" 
                  AutoPostBack="True">
                </asp:DropDownList>
            &nbsp;<div> 
               
                       <asp:GridView ID="gridRutas" runat="server" AutoGenerateColumns ="False" 
                           Caption ='- - - - - - - - - - LISTA DE TIENDAS EN TU RUTA - - - - - - - - - -' 
                           Width="540px" style="text-align: center" 
                           ShowFooter="True" CssClass="grid-view-ruta" >
                        <Columns>                                             
                            <asp:BoundField HeaderText="" DataField="captura" HtmlEncode="false" />
                            <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena" />
                            <asp:BoundField HeaderText="Tienda" DataField="nombre" />
                            <asp:BoundField HeaderText="Estatus" DataField="img_status" HtmlEncode="false" />
                            <asp:TemplateField >
                                <ItemTemplate>
                                <a href="FormatoCapturaEnergizerDP.aspx?folio=<%#Eval("folio_historial")%>&tienda=<%#Eval("id_tienda")%>&id_usuario=<%#Eval("id_usuario")%>&id_periodo=<%#Eval("id_periodo")%>">Ver</a>
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
                             
            </div>
            
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
