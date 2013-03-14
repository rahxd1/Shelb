<%@ Page Language="vb"
masterpagefile="~/sitios/Shelby/Shelby.Master"
 AutoEventWireup="false"
  CodeBehind="Menucertifica.aspx.vb" 
  Inherits="procomlcd.Menucertifica" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderShelby" runat="Server">


     
    <!--

POSTER PHOTO

-->
    <div id="poster-photo-container" >
        <img src="Img/banner.jpg" width="900px" height ="110px" />
    <div id="poster-photo-image">
        
    </div>
         
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
                   <a href="Sistemas/Certificacion/Procomlcd/Mcertificacion.aspx">
                        <img src="../../Img/Menu_Captura.jpg" class="photo-border" alt="Enter Alt Text Here" /></a>
                    <h2><a href="Sistemas/Certificacion/Procomlcd/Mcertificacion.aspx">Sistemas</a></h2>
                    </div>
                <div id="three-column-side2">
                    <a href="Sistemas/Certificacion/Procomlcd/Manual.aspx">
                        <img src="../../Img/Menu_Indicadores.jpg" class="photo-border" alt="Enter Alt Text Here" /></a>
                    <h2><a href="#">Nominas</a></h2>
                   </div>
                
            </div>
        </div>
        <!--

CONTENT SIDE COLUMN AVISOS

-->
        
        <div class="clear">
        
        </div>
        
    </div>





</asp:Content>
