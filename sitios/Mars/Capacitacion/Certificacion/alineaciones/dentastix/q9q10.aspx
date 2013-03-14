<%@ Page Language="vb" 
Masterpagefile="~/sitios/Mars/Capacitacion/Capacitacion_Mars.Master"
AutoEventWireup="false"
 CodeBehind="q9q10.aspx.vb"
 Inherits="procomlcd.q9q10" %>
 
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
                        9.-¿Qué forma tiene DentaStix®?</td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:RadioButtonList ID="Rb9" runat="server">
                            <asp:ListItem Value="1">Tiene forma de Y</asp:ListItem>
                            <asp:ListItem Value="2">Tiene forma de X</asp:ListItem>
                            <asp:ListItem Value="3">Tiene forma de H</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="Rb9" ErrorMessage="* Debes de seleccionar alguna opcion"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FF9900; height: 12px;">
                        10.-¿Cuáles son los ingredientes activos de DentaStix®?</td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:RadioButtonList ID="Rb10" runat="server">
                            <asp:ListItem Value="1">Tripolifosfato de Sodio y Sulfato de Zinc.</asp:ListItem>
                            <asp:ListItem Value="2">Sulfato de Zinc y Condroitina.</asp:ListItem>
                            <asp:ListItem Value="3">Tripolifosfato de Sodio y Condroitina</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
               
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="Rb10" ErrorMessage="* Debes de seleccionar alguna opcion"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Button ID="btnSiguiente" runat="server" Text="Siguiente" />
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
                                            9 y 10 de 20</td>
                </tr>
                <tr>
                    <td style="text-align: center; color: #FF0000;">
                        Pantalla</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        5 de 10</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        &nbsp;</tr>
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