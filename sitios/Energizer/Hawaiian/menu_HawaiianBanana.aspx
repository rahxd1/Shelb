<%@ Page Language="vb"
    Culture="es-MX"  
    masterpagefile="~/sitios/Energizer/Hawaiian/HawaiianBanana.Master"
    AutoEventWireup="false" 
    CodeBehind="menu_HawaiianBanana.aspx.vb" 
    Inherits="procomlcd.menu_HawaiianBanana"
    Title="Menú Hawaiian" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1HawaiianBanana" runat="Server">
  
     
    <!--

POSTER PHOTO

-->
    <div id="poster-photo-container">
         
          <div id="feature-area-home">Bienvenido a la plataforma Hawaiian-Banana </div>
          <asp:Image ID="Image1" runat="server" 
              ImageUrl="~/sitios/Energizer/Imagenes/Poster_HB.jpg" />
          
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
                Bienvenido al portal de Hawaiian-Banana, donde encuentras el formato de captura para el levantamiento de la informacion</p>
            <div id="three-column-container">
                <div id="three-column-side1">
                    <table style="width: 100%">
                        <tr>
                            <td align="center" valign="middle">
                                <a href="Captura/RutaHawaiianBanana.aspx">
                                <img src="../../../Img/Menu_Captura.jpg" class="photo-border" alt="Enter Alt Text Here" /></a></td>
                        </tr>
                    </table>
&nbsp;<h2>
                        Captura</h2>
                    <p>
                        Accedes al Formato de Captura de tu levantamiento en sistema; con la informacion de tus tiendas.
                        
                        </p>
                    <a href="Captura/RutaHawaiianBanana.aspx">Ir...</a><img class="arrow" src="../../../Img/arrow.gif" alt="" /></div>
                <div id="three-column-side2">
                    <table style="width: 100%; height: 132px;">
                        <tr>
                            <td align="center" style="text-align: center">
                                <a href="Reportes/ReportesHawaiianBanana.aspx">
                                <img src="../../../Img/Menu_Reportes.jpg" class="photo-border" alt="Enter Alt Text Here" /></a></td>
                        </tr>
                    </table>
&nbsp;<h2>
                        Reportes</h2>
                    <p>
                        Consulta reportes, bitacora de capturas, graficas, etc... de capturas de 
                        periodos actuales o anteriores.</p>
                    <a href="Reportes/ReportesHawaiianBanana.aspx">Ir...</a><img class="arrow" src="../../../Img/arrow.gif" alt="" /></div>
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