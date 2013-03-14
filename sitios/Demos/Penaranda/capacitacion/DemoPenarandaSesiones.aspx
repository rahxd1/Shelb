<%@ Page Language="vb" 
MasterPageFIle="~/sitios/Demos/Penaranda/DemoPenaranda.Master"
AutoEventWireup="false" 
CodeBehind="DemoPenarandaSesiones.aspx.vb"
 Inherits="procomlcd.DemoPenarandaSesiones" %>
 
 <asp:Content ID="Content1" ContentPlaceHolderID="DemoPenarandaContent" runat="Server">
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
                    <td style="height: 19px; text-align: right;">

                        
                        <img alt="" src="../../../../Img/arrow.gif" /><asp:HyperLink ID="HyperLink7" runat="server" 
                            NavigateUrl="~/sitios/Demos/Penaranda/capacitacion/DemoPenarandaCapacitacion.aspx">Regresar</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                <!-- celda animacion flash-->
                    <td style="height: 19px">

                        
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; color: #FFFFFF; background-color: #004020">
                        Sesiones Activas</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                         
                         <asp:GridView ID="gridSesiones" runat="server" AutoGenerateColumns="False" 
                            Width="100%" ShowFooter="True" Height="132px" 
                             Font-Size="Smaller" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                               
                                    <asp:BoundField HeaderText="Fecha" DataField = "fecha" />
                                    <asp:BoundField HeaderText="Capacitador" DataField="id_supervisor"/> 
                                    <asp:BoundField HeaderText="ID Join Me" DataField="id_joinme" />
                                    <asp:TemplateField HeaderText="Acceder"  ItemStyle-BorderColor ="DarkBlue" >
                                      <ItemTemplate>
                                       <a href="https://join.me/<%#Eval("id_joinme")%>">Acceder</a>
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
                    <td style="background-color: #004020">
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
                    <b>&quot;Para Crear una Sesion no debes de tener una sesion activa, deberas de 
                    cerrarla antes de crear una nueva&quot;</b></p>
                    <br />
        </div>
        
        <div class="clear" style="color: #FFFFFF">
            .<br />
      
        
        </div>
        
    </div>
</asp:Content>



