<%@ Page Language="vb" 
  masterpagefile="~/sitios/Mars/Capacitacion/Capacitacion_Mars.Master"
AutoEventWireup="false" 
CodeBehind="intro.aspx.vb" 
Inherits="procomlcd.intro" %>

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

                         <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540004" 
                             codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0" 
                             title="MARS" style="width: 650px; height: 450px">
                             <param name="movie" value="imagenes/instrucciones.swf" />
                             <param name="quality" value="high" />
                             <param name="wmode" value="opaque" />
                             <embed src="imagenes/instrucciones.swf" quality="high" wmode="opaque" pluginspage="http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash" type="application/x-shockwave-flash" width="650" height="450"></embed>
                          </object>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                         <asp:Button ID="Button1" runat="server" Text="Entrar" />
                    </td>
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
            <p style="text-align: center">
                    &quot;Para iniciar con tu Juego deberás de dar clic en el botón ENTRAR&quot;</p>
                    <br />
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center">
                        <b>Kroki</b></td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Image ID="Image1" ImageUrl="imagenes/MASCOTA.png" Width="100" Height = "100" runat="server" />
                    </td>
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

