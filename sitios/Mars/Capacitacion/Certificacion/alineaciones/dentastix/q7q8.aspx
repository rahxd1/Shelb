<%@ Page Language="vb" 
Masterpagefile="~/sitios/Mars/Capacitacion/Capacitacion_Mars.Master"
AutoEventWireup="false" 
CodeBehind="q7q8.aspx.vb" 
Inherits="procomlcd.q7q8" %>

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
                        7.-¿Qué es DentaStix®?</td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:RadioButtonList ID="Rb7" runat="server">
                            <asp:ListItem Value="1">Es un cepillo de dientes que ayuda a la limpieza de los mismos</asp:ListItem>
                            <asp:ListItem Value="2">Es una botana para perros adultos</asp:ListItem>
                            <asp:ListItem Value="3">Es una barra de cuidado oral que brinda una efectiva solución diaria para mantener sanos los dientes y encías de los perros.</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF; height: 19px;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="Rb7" ErrorMessage="* Debes de seleccionar alguna opcion"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FF9900; height: 19px;">
                        8.-¿Qué hace DentaStix® por los perros?</td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:RadioButtonList ID="Rb8" runat="server">
                            <asp:ListItem Value="1">Reduce la formación de Sarro ayudando a prevenir enfermedades de encías.</asp:ListItem>
                            <asp:ListItem Value="2">Estimula a los perros al recibir el premio</asp:ListItem>
                            <asp:ListItem Value="3">Satisface su antojo </asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
               
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="Rb8" ErrorMessage="* Debes de seleccionar alguna opcion"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Button ID="btnSiguiente" runat="server" Text="Siguiente" 
                            style="height: 26px" />
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
                                            7 y 8 de 20</td>
                </tr>
                <tr>
                    <td style="text-align: center; color: #FF0000;">
                        Pantalla</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        4 de 10</td>
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