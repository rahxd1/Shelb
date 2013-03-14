<%@ Page Language="vb" 
    Culture="es-MX" 
    AutoEventWireup="false" 
    masterpagefile="~/sitios/Energizer/Schick/Schick.Master"
    CodeBehind="MenuSchick.aspx.vb" 
    Inherits="procomlcd.MenuSchick"
    Title="Schick" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1Schick" runat="Server">
  
     
    <!--

POSTER PHOTO

-->
    <div id="poster-photo-container">
         
          <div id="feature-area-home">Bienvenido a la Plataforma Schick - Shelby</div>
          <asp:Image ID="Image1" runat="server" 
              ImageUrl="~/sitios/Energizer/Imagenes/posterfinal.jpg" />
          
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
                Bienvenido al portal de SYM Precios, donde encuentras el formato de captura para el levantamiento de la informacion</p>
            <div id="three-column-container">
                <div id="three-column-side1">
                    <table style="width: 100%">
                        <tr>
                            <td align="center" valign="middle">
                                <a href="Captura/RutaSchick.aspx">
                                <img src="../Img/home-photo-1.jpg" class="photo-border" alt="Enter Alt Text Here" /></a></td>
                        </tr>
                    </table>
&nbsp;<h2>
                        Sección Captura</h2>
                    <p>
                        Accedes al Formato de Captura de tu levantamiento en sistema; con la informacion de tus tiendas.
                        
                        </p>
                    <a href="Captura/RutaSchick.aspx">Ir...</a><img class="arrow" src="../Img/arrow.gif" alt="" /></div>
                <div id="three-column-side2">
                    <table style="width: 100%; height: 132px;">
                        <tr>
                            <td align="center" style="text-align: center">
                                <a href="Reportes/ReportesSchick.aspx">
                                <img src="../Img/home-photo-2.jpg" class="photo-border" alt="Enter Alt Text Here" /></a></td>
                        </tr>
                    </table>
&nbsp;<h2>
                        Reportes</h2>
                    <p>
                        Consulta reportes, bitacora de capturas, graficas, etc... de capturas de 
                        periodos actuales o anteriores.</p>
                    <a href="Reportes/ReportesSchick.aspx">Ir...</a><img class="arrow" src="../Img/arrow.gif" alt="" /></div>
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
