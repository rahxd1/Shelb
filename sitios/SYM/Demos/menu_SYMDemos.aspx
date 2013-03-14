<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/SYM/Demos/SYMDemos.Master"
    AutoEventWireup="false" 
    CodeBehind="menu_SYMDemos.aspx.vb" 
    Inherits="procomlcd.menu_SYMDemos"
    title= "Sanchez y Martin (Precios)" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="Contenido_SYMDemos" runat="Server">
  
     
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
            <div id="three-column-container">
                <div id="three-column-side1">
                    <table style="width: 100%">
                        <tr>
                            <td align="center">
                                <a href="Captura/RutaSYMDemos.aspx">
                                <img src="../../../Img/Menu_Captura.jpg" class="photo-border" alt="Enter Alt Text Here" /></a></td>
                        </tr>
                    </table>
                <h2><a href="Captura/RutaSYMDemos.aspx">Capturas</a></h2>
                    <p>
                        Accedes al Formato de Captura de tu levantamiento en sistema; con la informacion de tus tiendas.
                        
                        </p></div>
                <div id="three-column-side2">
                    <table style="width: 100%">
                        <tr>
                            <td align="center" style="text-align: center">
                                <a href="Reportes/ReportesSYMDemos.aspx">
                                <img src="../../../Img/Menu_Reportes.jpg" class="photo-border" alt="Enter Alt Text Here" /></a></td>
                        </tr>
                    </table>
            <h2><a href="Reportes/ReportesSYMDemos.aspx">Reportes</a></h2>
                    <p>
                        Consulta reportes, bitacora de capturas, graficas, etc... de capturas de 
                        periodos actuales o anteriores.</p></div>
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