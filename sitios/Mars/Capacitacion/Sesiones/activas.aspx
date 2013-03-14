<%@ Page Language="vb"
   Masterpagefile="~/sitios/Mars/Capacitacion/Capacitacion_Mars.Master"
 AutoEventWireup="false" 
 CodeBehind="activas.aspx.vb" 
 Inherits="procomlcd.menu_Cap" %>
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

                        
                        <asp:HyperLink ID="HyperLink6" runat="server" 
                            NavigateUrl="~/sitios/Mars/Capacitacion/MenuCapacitacion.aspx">&lt;-- Regresar</asp:HyperLink>

                        
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; color: #FFFFFF; background-color: #336699">
                        Capacitaciones Activas</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                         
                         <asp:GridView ID="gridCap" runat="server" AutoGenerateColumns="False" 
                            Width="100%" ShowFooter="True" Height="132px" 
                             Font-Size="Smaller" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Material"  ItemStyle-BorderColor ="DarkBlue" >
                                      <ItemTemplate>
                                      <a href="Materiales.aspx">Obtener</a>
                                       </ItemTemplate>
                                      <ItemStyle />
                                    </asp:TemplateField>  
                                    <asp:BoundField HeaderText="Capacitacion" DataField="nombre"/> 
                                    <asp:BoundField HeaderText="Tipo" DataField="tipo"/> 
                                    <asp:TemplateField HeaderText="Acceder"  ItemStyle-BorderColor ="DarkBlue" >
                                      <ItemTemplate>
                                       <a href="<%#Eval("link")%>">Acceder</a>
                                       </ItemTemplate>
                                      <ItemStyle />
                                 </asp:TemplateField>  
                                   
                                   
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
               Aviso</h3>
            <p style="text-align: center; color: #CC0000;">
                    <b>&quot;Para Iniciar con tu capacitacion da clic en Acceder&quot;</b></p>
                    <br />
        </div>
        
        <div class="clear" style="color: #FFFFFF">
            .<br />
      
        
        </div>
        
    </div>
</asp:Content>

