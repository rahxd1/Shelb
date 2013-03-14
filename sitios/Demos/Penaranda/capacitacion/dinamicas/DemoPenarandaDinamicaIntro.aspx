<%@ Page Language="vb" 
MasterPagefile="~/sitios/Demos/Penaranda/DemoPenaranda.Master"
AutoEventWireup="false" 
CodeBehind="DemoPenarandaDinamicaIntro.aspx.vb" 
Inherits="procomlcd.DemoPenarandaDinamicaIntro" %>

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
                    <td>
                        <asp:Label ID="lblAviso" runat="server" 
                            style="color: #FF0000; font-weight: 700"></asp:Label>
                    </td>
                </tr>
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
                    Demo de dinamica Shelby</p>
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


