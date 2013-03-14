<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Demo/Demo.Master"
    AutoEventWireup="false" 
    CodeBehind="MenuDemo.aspx.vb" 
    Inherits="procomlcd.MenuDemo"
    title="Demo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoDemo" runat="Server">
     
    <!--

POSTER PHOTO

-->
    <div id="poster-photo-container">
    
    <div id="poster-photo-image"></div>
         
          <div id="feature-area-home">Bienvenido a la Plataforma Demo</div>
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
                <a href="#">
                        <img src="imagenes/home-photo-1.jpg" class="photo-border" alt="Enter Alt Text Here" /></a>
                       <h2>Seccion Captura</h2>
                    <p>
                        Accedes al Formato de Captura de tu levantamiento en sistema; con la informacion de tus tiendas.
                        
                        </p>
                    <a href="#">Ir...</a><img class="arrow" src="imagenes/arrow.gif" alt="" /></div>
                <div id="three-column-side2">
                    <a href="IndicadoresDemo.aspx">
                        <img src="imagenes/home-photo-3.jpg" class="photo-border" alt="Enter Alt Text Here" /></a>
                    <h2>
                        Indicadores</h2>
                    <p>
                        Indicadores de información actual</p>
                    <a href="IndicadoresDemo.aspx">Ir...</a><img class="arrow" src="imagenes/arrow.gif" alt="" /></div>
                  
                <div id="three-column-middle">
                    <a href="Reportes/ReportesDemo.aspx">
                        <img src="imagenes/home-photo-2.jpg" class="photo-border" alt="Enter Alt Text Here" /></a>
                    <h2>
                        Reportes</h2>
                    <p>
                        Consulta reportes, bitacora de capturas, graficas, etc... de capturas de 
                        periodos actuales o anteriores.</p>
                    <a href="Reportes/ReportesDemo.aspx">Ir...</a><img class="arrow" src="imagenes/arrow.gif" alt="" /></div>
            </div>
            </div>
        <!--

CONTENT SIDE COLUMN AVISOS

-->
        <div id="content-side-two-column">
            <h2>
               Avisos</h2>
            <p>
                Sin avisos actuales</p>
</div>
        
        <div class="clear">
        
        </div>
        
    </div>
    </div>
    </div>
</asp:Content>