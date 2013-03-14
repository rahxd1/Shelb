<%@ Page Language="vb" 
masterpagefile="~/sitios/Mars/Capacitacion/Capacitacion_Mars.Master"
AutoEventWireup="false"
 CodeBehind="formatos_tipos.aspx.vb" 
 Inherits="procomlcd.formatos_tipos" %>

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
                    <td colspan="4" 
                        style="text-align: right; color: #FFFFFF; background-color: #FFFFFF">
                        
                                <asp:HyperLink ID="lnkRegresar" runat="server" 
                                    
                            NavigateUrl="~/sitios/Mars/Capacitacion/Dinamicas/Autoservicio/PuntosInterrupcion/menu_logros.aspx">Regresar</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" 
                        style="text-align: center; color: #FFFFFF; background-color: #FFFFFF">
                        
                        <asp:Image ID="imgFormato" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" 
                        style="text-align: center; color: #FFFFFF; background-color: #336699">
                        
                        TIPOS DE TIENDA</td>
                </tr>
                <tr>
                    <td style="text-align: center; background-color: #CCFFFF">
                        <span style="background-color: #CCFFFF">DIAMANTE</span></td>
                    <td style="text-align: center; background-color: #FFCC00">
                        ORO</td>
                    <td style="text-align: center; background-color: #CCCCFF">
                        PLATA</td>
                    <td style="text-align: center; background-color: #CC9900">
                        BRONCE</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <br />
                        <asp:ImageButton ID="btnDiamante" runat="server" Height="60px" 
                            ImageUrl="imagenes/Diamante.png" 
                            Width="60px" />
                        <br />
                    </td>
                    <td style="text-align: center">
                        <asp:ImageButton ID="btnOro" runat="server" Height="60px" 
                            ImageUrl="imagenes/Oro.png"
                            Width="60px" />
                    </td>
                    <td style="text-align: center">
                        <asp:ImageButton ID="btnPlata" runat="server" Height="60px" 
                            ImageUrl="imagenes/Plata.png" 
                            Width="60px" />
                    </td>
                    <td style="text-align: center">
                        <asp:ImageButton ID="btnBronce" runat="server" Height="60px" 
                            ImageUrl="imagenes/Bronce.png" 
                            Width="60px" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCFFFF; text-align: center;">
                        <asp:Label ID="lblDiamante" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="background-color: #FFCC66; text-align: center;">
                        <asp:Label ID="lblOro" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="background-color: #CCCCFF; text-align: center;">
                        <asp:Label ID="lblPlata" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="background-color: #CC9900; text-align: center;">
                        <asp:Label ID="lblBronce" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        
                        <asp:Image ID="ImagenDiamante" runat="server" />
                    </td>
                    <td style="text-align: center">
                        
                        <asp:Image ID="ImagenOro" runat="server" />
                    </td>
                    <td style="text-align: center">
                        
                        <asp:Image ID="ImagenPlata" runat="server" />
                    </td>
                    <td style="text-align: center">
                        
                        <asp:Image ID="ImagenBronce" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        
                        <asp:Image ID="imagenLogroD" runat="server" />
                    </td>
                    <td style="text-align: center">
                        
                        <asp:Image ID="ImagenLogroO" runat="server" />
                    </td>
                    <td style="text-align: center">
                        
                        <asp:Image ID="ImagenLogroP" runat="server" />
                    </td>
                    <td style="text-align: center">
                        
                        <asp:Image ID="ImagenLogroB" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #CCFFFF; text-align: center;">
                        <asp:Label ID="lblDiamante0" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="background-color: #FFCC66; text-align: center;">
                        <asp:Label ID="lblOro0" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="background-color: #CCCCFF; text-align: center;">
                        <asp:Label ID="lblPlata0" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="background-color: #CC9900; text-align: center;">
                        <asp:Label ID="lblBronce0" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center">
                                                                      
                        <asp:Label ID="lblAVISO" runat="server" Font-Bold="True" Font-Size="13pt"></asp:Label>
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
                    &quot;<span style="color: #CC0000"><b>Selecciona el tipo de tienda a evaluar<br />
&nbsp;clic sobre la imagen&quot;</b></span></p>
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
                         <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540050" 
                             codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0" 
                             title="MARS" style="width: 150px; height: 150px">
                             <param name="movie" value="imagenes/tiposdetienda.swf" />
                             <param name="quality" value="high" />
                             <param name="wmode" value="opaque" />
                             <embed src="imagenes/tiposdetienda.swf" quality="high" wmode="opaque" pluginspage="http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash" type="application/x-shockwave-flash" width="150" height="150"></embed>
                          </object>&nbsp;</td>
                </tr>
            </table>
        </div>
        
        <div class="clear">
        
        </div>
        
    </div>
</asp:Content>
