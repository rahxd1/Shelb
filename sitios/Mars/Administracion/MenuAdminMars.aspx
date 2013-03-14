<%@ Page Language="vb" 
    Masterpagefile="~/sitios/Mars/Administracion/AdministracionMars.Master"
    AutoEventWireup="false" 
    CodeBehind="MenuAdminMars.aspx.vb" 
    Inherits="procomlcd.MenuAdminMars"
    title="Administración Mars" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdminMars" runat="Server">
     
     
    <!--POSTER PHOTO-->
    <div id="poster-photo-container">
        <div id="poster-photo-image"></div>
    </div>
    
    <!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">
        
    <!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <div id="three-column-container">
                <div id="three-column-side1">
                    <a href="AdminPromotoresMars.aspx">
                        <img src="../../../Img/home-photo-1.jpg" class="photo-border" alt="Enter Alt Text Here" /></a>
                    <h2><a href="AdminPromotoresMars.aspx">Promotores</a></h2>
                    <p>
                                                Edición nombres, fechas de ingreso y calificaciones de promotores.</p>
                    </div>
                
                <div id="three-column-side2">
                    <a href="AdminEntrenamientosMars.aspx">
                        <img src="../../../Img/home-photo-3.jpg" class="photo-border" alt="Enter Alt Text Here" /></a>
                    <h2><a href="AdminEntrenamientosMars.aspx">Entrenamientos técnicos</a></h2>
                    <p>
                        Alta y Consulta de Entrenamientos tecnicos para promotores.</p></div>
                    
                <div id="three-column-middle">
                    <a href="AdminProcesosMars.aspx">
                        <img src="../../../Img/home-photo-2.jpg" class="photo-border" alt="Enter Alt Text Here" /></a>
                    <h2><a href="AdminProcesosMars.aspx">Alineaciones e Implementaciones</a></h2>
                    <p>
                        Altas y Consultas de Alineaciones e Implementaciones.</p></div>
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