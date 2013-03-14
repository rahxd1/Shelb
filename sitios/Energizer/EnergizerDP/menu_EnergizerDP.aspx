<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Energizer/EnergizerDP/Energizer_DP.Master"
    AutoEventWireup="false" 
    CodeBehind="menu_EnergizerDP.aspx" 
    Inherits="procomlcd.menu_EnergizerDP"
    title="Energizer Pilas Demo 2010" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1EnergizerDP" runat="Server">
  
     
    <!--

POSTER PHOTO

-->
    <div id="poster-photo-container">
         
          <div id="feature-area-home">Bienvenido a la plataforma Energizer Demo Pilas</div>
          <asp:Image ID="Image1" runat="server" 
              ImageUrl="~/sitios/Energizer/Imagenes/Poster_Energizer.jpg" />
          
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
                Bienvenido al portal de Energizer Demo Pilas, donde encuentras el formato de captura para el levantamiento de la informacion</p>
            <div id="three-column-container">
                <div id="three-column-side1">
                    <table style="width: 100%">
                        <tr>
                            <td align="center" valign="middle">
                                <a href="Captura/RutaEnergizerDP.aspx">
                                <img src="../../../Img/Menu_Captura.jpg" class="photo-border" alt="Enter Alt Text Here" /></a></td>
                        </tr>
                    </table>
&nbsp;<h2>
                        Captura</h2>
                    <p>
                        Accedes al Formato de Captura de tu levantamiento en sistema; con la informacion de tus tiendas.
                        
                        </p>
                    <a href="Captura/RutaEnergizerDP.aspx">Ir...</a><img class="arrow" src="../../../Img/arrow.gif" alt="" /></div>
                <div id="three-column-side2">
                    <table style="width: 100%; height: 132px;">
                        <tr>
                            <td align="center" style="text-align: center">
                                <a href="Reportes/ReportesEnergizerDP.aspx">
                                <img src="../../../Img/Menu_Reportes.jpg" class="photo-border" alt="Enter Alt Text Here" /></a></td>
                        </tr>
                    </table>
&nbsp;<h2>
                        Reportes</h2>
                    <p>
                        Consulta reportes, bitacora de capturas, graficas, etc... de capturas de 
                        periodos actuales o anteriores.</p>
                    <a href="Reportes/ReportesEnergizerDP.aspx">Ir...</a><img class="arrow" src="../../../Img/arrow.gif" alt="" /></div>
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