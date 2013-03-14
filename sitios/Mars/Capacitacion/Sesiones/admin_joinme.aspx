<%@ Page Language="vb" 
 Culture="es-MX" 
  Masterpagefile="~/sitios/Mars/Capacitacion/Capacitacion_Mars.Master"
AutoEventWireup="false" 
CodeBehind="admin_joinme.aspx.vb" 
Inherits="procomlcd.admin_joinme" %>

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
                    <td style="height: 19px; width: 422px;">

                        
                        &nbsp;</td>
                    <td style="height: 19px; width: 224px; text-align: left;">

                        
                        &nbsp;</td>
                    <td style="height: 19px; text-align: right;">

                        
                        <asp:HyperLink ID="HyperLink7" runat="server" 
                            NavigateUrl="~/sitios/Mars/Capacitacion/Sesiones/joinme.aspx">&lt;-- Regresar</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                <!-- celda animacion flash-->
                    <td style="height: 19px; width: 422px;">

                        
                        <asp:Label ID="Label2" runat="server" 
                            Text="Introduce el ID de la sesion Join Me | https://join.me/"></asp:Label>
&nbsp;<br />
                    </td>
                    <td style="height: 19px; width: 224px; text-align: left;">

                        
                        <asp:TextBox ID="txtID" runat="server" style="margin-left: 0px" Width="118px"></asp:TextBox>
                    </td>
                    <td style="height: 19px; text-align: right;">

                        
                        <asp:Button ID="btnGuardar" runat="server" style="text-align: left" 
                            Text="Nueva Sesion" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; color: #FFFFFF; background-color: #336699" 
                        colspan="3">
                        Mis sesiones</td>
                </tr>
                <tr>
                    <td style="text-align: center" colspan="3">
                         
                         <asp:GridView ID="gridSesiones" runat="server" AutoGenerateColumns="False" 
                            Width="100%" ShowFooter="True" Height="132px" 
                             Font-Size="Smaller" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                               
                                     <asp:CommandField ButtonType="Image" HeaderText ="Cerrar Sesion"
                                           ShowDeleteButton="True" DeleteImageUrl="~/Img/delete-icon.png" />
                                    <asp:BoundField HeaderText="ID Sesion" DataField = "id_sesion" />
                                    <asp:BoundField HeaderText="Fecha" DataField = "fecha" />
                                    <asp:BoundField HeaderText="Capacitador" DataField="id_supervisor"/> 
                                    <asp:BoundField HeaderText="ID Join Me" DataField="id_joinme" />
                                   
                                   
                                   
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
                    <td style="background-color: #FF9900" colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
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
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center">
                        <asp:Label ID="Label1" runat="server" Text="Label" 
                            style="font-weight: 700; color: #FF0000"></asp:Label>
                    </td>
                </tr>
                </table>
        </div>
        
        <div class="clear" style="color: #FFFFFF">
            .<br />
      
        
        </div>
        
    </div>
</asp:Content>