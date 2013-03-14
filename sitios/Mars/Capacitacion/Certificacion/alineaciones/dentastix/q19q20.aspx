<%@ Page Language="vb" 
Masterpagefile="~/sitios/Mars/Capacitacion/Capacitacion_Mars.Master"
AutoEventWireup="false"

 CodeBehind="q19q20.aspx.vb" 
 Inherits="procomlcd.q19q20" %>
 
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
                    <td style="height: 19px; text-align: right;">

                        
                        <asp:HyperLink ID="HyperLink7" runat="server" 
                            NavigateUrl="~/sitios/Mars/Capacitacion/Certificacion/Certificacion.aspx">&lt;-- Salir de la Certificacion</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                <!-- celda animacion flash-->
                    <td style="height: 19px">

                        
                        <asp:Label ID="lblAviso" runat="server" 
                            style="color: #FF3300; font-weight: 700" Text="Label" Visible="False"></asp:Label>

                        
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; color: #FFFFFF; background-color: #336699; height: 19px;">
                        Examen de Certificacion DentaStix</td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                         
                         

                         

                        Selecciona la respuesta Correcta&nbsp;:</td>
                </tr>
         
              
                <tr>
                    <td style="background-color: #3399FF">
                        19.-¿Cuáles son los materiales que implementaremos en tienda?</td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:RadioButtonList ID="Rb19" runat="server">
                            <asp:ListItem Value="1">Stopper, Cenefas, Danglers y esquineros</asp:ListItem>
                            <asp:ListItem Value="2">Stopper, Folletos, Balcón, Floordisplay y tira</asp:ListItem>
                            <asp:ListItem Value="3">Balcones, Stopper, Maxistopper</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="Rb19" 
                            ErrorMessage="* Debes de seleccionar alguna opcion"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FF9900">
                        20.-¿En qué va a beneficiar el que nosotros implementemos correctamente en 
                        tienda?</td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:RadioButtonList ID="Rb20" runat="server">
                            <asp:ListItem Value="1">El anaque y exhibiciones se verán más bonitas</asp:ListItem>
                            <asp:ListItem Value="2">Nuestro producto lucirá mas</asp:ListItem>
                            <asp:ListItem Value="3">Invitamos a los clientes de la categoría a que exploren el anaquel y que se den cuenta que existen más opciones para las necesidades específicas de su mascota</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="Rb20" 
                            ErrorMessage="* Debes de seleccionar alguna opcion"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Button ID="btnSiguiente" runat="server" Text="Finalizar" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        &nbsp;</td>
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
                &nbsp;</h3>
            <p style="text-align: center; color: #CC0000;">
                    &nbsp;</p>
                    <br />
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center; color: #FF0000;">
                        Preguntas&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                                            19 y 20 de 20</td>
                </tr>
                <tr>
                    <td style="text-align: center; color: #FF0000;">
                        Pantalla</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        10 de 10</td>
                </tr>
               
                <tr>
                    <td style="text-align: center">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        
        <div class="clear" style="color: #FFFFFF">
            .<br />
      
        
        </div>
        
    </div>
</asp:Content>

