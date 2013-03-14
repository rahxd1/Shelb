<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Ferrero/Ferrero-Danone/FerreroDanone.Master"
    AutoEventWireup="false" 
    CodeBehind="Menu_FerreroDanone.aspx.vb" 
    Inherits="procomlcd.Menu_FerreroDanone"
    TITLE="Ferrero" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentFerreroDanone" runat="Server">
  
     
    <!--

POSTER PHOTO

-->
    <div id="poster-photo-container">
    
    <div id="poster-photo-image"></div>
         
          <div id="feature-area-home">Bienvenido a la plataforma Ferrero</div>
          </div>
    <!--

CONTENT CONTAINER

-->
    <div id="contenido-columna-derecha">
        <!--

CONTENT MAIN COLUMN

-->
        <div id="content-main-two-column">
            <p>
              <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0" width="662" height="380" title="Intro Ferrero-Shelby">
                <param name="movie" value="../Img/videoFerreroDanone.swf" />
                <param name="quality" value="high" />
                <param name="wmode" value="opaque" />
                <embed src="../Img/videoFerreroDanone.swf" quality="high" wmode="opaque" pluginspage="http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash" type="application/x-shockwave-flash" width="662" height="380"></embed>
              </object>
            </p>
            <div id="three-column-container">
              <div id="three-column-side1">
                    <h2>
                        Seccion Captura</h2>
                    <p>
                       Captura la información del levantamiento en tiendas.</p>
                    <a href="captura/CapturasFerreroDanone.aspx">Ir...</a><img class="arrow" src="../../../Img/arrow.gif" alt="" /></div>
                <div id="three-column-side2">
                    <h2>
                        Galeria Fotografica</h2>
                    <p>
                        Sección dedicada a las fotografias tomadas en campo.</p>
                    <a href="Reportes/ReporteGaleriaFD.aspx">Ir...</a><img class="arrow" src="../../../Img/arrow.gif" alt="" /></div>
                <div id="three-column-middle">
                    <h2>
                        Reportes</h2>
                    <p>
                        Consulta reportes, bitácora de capturas, graficas, etc... de capturas de 
                        periodos actuales o anteriores.</p>
                    <a href="Reportes/ReportesFerreroDanone.aspx">Ir...</a><img class="arrow" src="../../../Img/arrow.gif" alt="" /></div>
            </div>
        </div>
        <!--

CONTENT SIDE COLUMN AVISOS

-->
        <div id="content-side-two-column">
        </div>
        
        <div class="clear">
        
        </div>
        
    </div>
</asp:Content>