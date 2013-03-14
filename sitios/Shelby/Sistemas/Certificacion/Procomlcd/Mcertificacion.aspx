<%@ Page Language="vb"
masterpagefile="~/sitios/Shelby/Shelby.Master"
 AutoEventWireup="false" 
 CodeBehind="Mcertificacion.aspx.vb" 
 Inherits="procomlcd.Mcertificacion" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderShelby" runat="Server">

   
     
    <!--

POSTER PHOTO

-->
    <div id="poster-photo-container">
    
    <div id="poster-photo-image">
        <img src="../../../Img/banner.jpg" width="900px" height ="110px" />
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
              <div id="three-column-side10">
                <a href="Certificacion/Procomcertificate.aspx">
                         <img src="../../../Img/cuestionario.jpg" class="photo-border" alt="Enter Alt Text Here" /></a>
                    <h2><a href="Certificacion/Procomcertificate.aspx">Evaluacion</a></h2>
                    </div>
                
                <div id="three-column-side20">
                    <a href="Manual.aspx">
                         <img src="../../../Img/manuales.png" class="photo-border" alt="Enter Alt Text Here" /></a>
                    <h2><a href="Manual.aspx">Manuales</a></h2>
                   </div>
                 <div id="three-column-middle">
                    <a href="Creportes.aspx">
                         <img src="../../../Img/resultados.jpg" class="photo-border" alt="Enter Alt Text Here" /></a>
                    <h2><a href="Creportes.aspx">Resultados</a></h2>
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