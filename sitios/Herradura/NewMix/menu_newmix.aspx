<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Herradura/NewMix/NewMix.Master"
    AutoEventWireup="false" 
    CodeBehind="menu_newmix.aspx.vb" 
    Inherits="procomlcd.menu_newmix"
    title= "New Mix - JD " %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderNewMix" runat="Server">
  
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
            <p>
                Bienvenido al portal de New Mix, donde encuentras el formato de captura para el levantamiento de la informacion</p>
            <div id="three-column-container">
                <div id="three-column-side1">
                    <a href="captura/RutaNewMix.aspx">
                        <img src="../../../Img/Menu_Captura.jpg" class="photo-border" alt="Enter Alt Text Here" /></a>
                    <h2><a href="captura/RutaNewMix.aspx">Captura</a></h2>
                    <p>Accedes al Formato de Captura de levantamiento en sistema, con la informacion de tus tiendas.
                        
                        </p></div>
                <div id="three-column-side2">
                    <a href="Reportes/ReportesNewMix.aspx">
                        <img src="../../../Img/Menu_Reportes.jpg" class="photo-border" 
                        alt="Enter Alt Text Here" /></a>
                    <h2><a href="Reportes/ReportesNewMix.aspx">Reportes</a></h2>
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