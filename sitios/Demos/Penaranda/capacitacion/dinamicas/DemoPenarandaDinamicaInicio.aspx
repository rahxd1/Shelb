<%@ Page Language="vb" 
Masterpagefile="~/sitios/Demos/Penaranda/DemoPenaranda.Master"
AutoEventWireup="false" 
CodeBehind="DemoPenarandaDinamicaInicio.aspx.vb" 
Inherits="procomlcd.DemoPenarandaDinamicaInicio" %>

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
            <td>
            <asp:HyperLink ID="HyperLink1" runat="server" 
                NavigateUrl="~/sitios/Demos/Penaranda/capacitacion/DemoPenarandaCapacitacion.aspx">&lt;-Regresar</asp:HyperLink>
            </td>
            </tr>
                <tr>
                <!-- celda animacion flash-->
                     <td style="height: 19px">
                          <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540004" 
                             codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0" 
                             title="MARS" style="width: 600px; height: 400px">
                             <param name="movie" value="imagenes/introdinamica.swf" />
                             <param name="quality" value="high" />
                             <param name="wmode" value="opaque" />
                             <embed src="imagenes/introdinamica.swf" quality="high" wmode="opaque" pluginspage="http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash" type="application/x-shockwave-flash" width="600" height="400"></embed>
                          </object>

                        
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
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
                Accede al Juego</h3>
                    <br />
            <table style="width: 100%">
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
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center; background-color: #FFFFFF;">
                        <asp:Button ID="btnENTRAR" runat="server" Text="Entrar" Visible="False" />
                    </td>
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
