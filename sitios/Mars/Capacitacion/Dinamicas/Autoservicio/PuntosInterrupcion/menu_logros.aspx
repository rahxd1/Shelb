<%@ Page Language="vb"
  masterpagefile="~/sitios/Mars/Capacitacion/Capacitacion_Mars.Master"
 AutoEventWireup="false" 
 CodeBehind="menu_logros.aspx.vb" 
 Inherits="procomlcd.menu_logros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMarsCapacitacion" runat="Server">
   <!--

POSTER PHOTO

-->
    <div id="poster-photo-container">
    
    <div id="poster-photo-image"></div>
         
          </div>
    <!--

CONTENT CONTAINER

-->
    <div id="contenido-columna-derecha">
        <!--

CONTENT MAIN COLUMN

-->
        <div id="content-main-two-column">

            <table style="width: 100%">
                <tr>
                <!-- celda animacion flash-->
                    <td style="height: 19px">
                          <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540001" 
                             codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0" 
                             title="MARS" style="width: 650px; height: 90px">
                             <param name="movie" value="imagenes/anima1.swf" />
                             <param name="quality" value="high" />
                             <param name="wmode" value="opaque" />
                             <embed src="imagenes/anima1.swf" quality="high" wmode="opaque" pluginspage="http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash" type="application/x-shockwave-flash" width="650" height="90"></embed>
                          </object>

                        
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; color: #FFFFFF; background-color: #336699">
                        Mi panel de logros</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                         <asp:GridView ID="gridLogros" runat="server" AutoGenerateColumns="False" 
                            Width="100%" ShowFooter="True" Height="132px" 
                             Font-Size="Smaller" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:TemplateField HeaderText="ENTRAR"  ItemStyle-BorderColor ="DarkBlue" >
                                      <ItemTemplate>
                                       <a href="formatos_tipos.aspx?id_formato=<%#Eval("id_formato")%>">Entrar</a>
                                       </ItemTemplate>
                                      <ItemStyle />
                                 </asp:TemplateField>  
                                    <asp:BoundField HeaderText="Formato" DataField="nombre_formato"/> 
                                    <asp:BoundField HeaderText="Diamante" DataField="Diamante" HtmlEncode ="false" 
                                        ItemStyle-HorizontalAlign = "Center"  >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Oro" DataField="Oro" HtmlEncode = "false" 
                                        ItemStyle-HorizontalAlign = "Center" > 
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Plata" DataField="Plata" HtmlEncode = "false" 
                                        ItemStyle-HorizontalAlign = "Center" > 
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Bronce" DataField="Bronce" HtmlEncode = "false" 
                                        ItemStyle-HorizontalAlign = "Center" >
                                   
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                   
                                 </Columns>   
                                <FooterStyle CssClass="grid-footer" BackColor="#5D7B9D" Font-Bold="True" 
                                    ForeColor="White" />                   
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                                <h1>Sin información</h1>
                            </EmptyDataTemplate>
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            </asp:GridView>

                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FF9900">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        <!--

CONTENT SIDE COLUMN AVISOS

-->
        <div id="content-side-two-column">
            <h3>
               Avisos</h3>
            <p style="text-align: center; color: #CC0000;">
                    <b>&quot;Para Iniciar con tu juego deberás de dar clic en el link ENTRAR de cada 
                    formato de tienda&quot;</b></p>
                    <br />
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center">
                        <b>Nivel de avance</b></td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Label ID="lblnivel" runat="server" style="text-align: center"></asp:Label>
                    &nbsp;%</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Image ID="imgNIVEL" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540020" 
                             codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0" 
                             title="MARS" style="width: 150px; height: 150px">
                             <param name="movie" value="imagenes/perroLogro.swf" />
                             <param name="quality" value="high" />
                             <param name="wmode" value="opaque" />
                             <embed src="imagenes/PerroLogro.swf" quality="high" wmode="opaque" pluginspage="http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash" type="application/x-shockwave-flash" width="150" height="150"></embed>
                          </object>
                </tr>
                <tr>
                    <td style="text-align: center">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        
        <div class="clear" style="color: #FFFFFF">
            .<br />
            .<br />
            .<br />
            .<br />
            .<br />
            .<br />
            .<br />
            .<br />
            .<br />
            .<br />
            .<br />
        
        </div>
        
    </div>
</asp:Content>

