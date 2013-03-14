<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Admin/AdminMaster.Master"
    AutoEventWireup="false" 
    CodeBehind="MenuAdmin.aspx.vb" 
    Inherits="procomlcd.MenuAdmin"
    title="Administración" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">
     
<!--POSTER PHOTO-->
    <div id="titulo-pagina">Administración</div>
<!--CONTENT CONTAINER-->
    <div id="contenido-derecha">
    
        <!--CONTENT MAIN COLUMN-->
        <div id="contenido-dos-columnas-1">
            <div id="contenedor-tres-columnas">
                <div id="contenedor-columna-1">
                    <a href="AdminUsuarios/AdminUsuarios.aspx">
                        <img src="../../Img/home-photo-1.jpg" class="photo-border" alt="Enter Alt Text Here" /></a>
                    <h2>Administración Usuarios</h2>
                    <br />
                    <a href="AdminUsuarios/AdminUsuarios.aspx">Ir...</a><img class="arrow" src="../../Img/arrow.gif" alt="" />
                </div>
                <div id="contenedor-columna-3">
                    <a href="AdminProyectos/Proyectos.aspx">
                        <img src="../../Img/home-photo-3.jpg" class="photo-border" alt="Enter Alt Text Here" /></a>
                    <h2>Administracion&nbsp; Clientes</h2>
                    <br />
                    <a href="AdminProyectos/Proyectos.aspx">Ir...</a><img class="arrow" src="../../Img/arrow.gif" alt="" />
                </div>
                <div id="contenedor-columna-2">
                    <a href="AdminReportes/AdminReporte.aspx">
                        <img src="../../Img/home-photo-2.jpg" class="photo-border" alt="Enter Alt Text Here" /></a>
                    <h2>Administración de Reportes</h2>
                    <br />
                    <a href="AdminReportes/AdminReporte.aspx">Ir...</a><img class="arrow" src="../../Img/arrow.gif" alt="" />
                </div>
            </div>
        </div>

        <!--CONTENIDO AVISOS-->
        <div id="contenido-dos-columnas-2">
            <br />
            <br />
            <br />
        </div>
        
        <div class="clear"></div>
        
    </div>
</asp:Content>