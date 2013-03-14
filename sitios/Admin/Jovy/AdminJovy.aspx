<%@ Page Language="VB" 
    Culture="es-MX" 
    MasterPageFile="../AdminMaster.master" 
    AutoEventWireup="true" 
    CodeBehind="AdminJovy.aspx.vb" 
    Inherits="procomlcd.AdminJovy"
    Title="Administración Jovy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">

<!--TITLE-->
    <div id="titulo-pagina">
        Administración Jovy</div>
        
<!--CONTENT CONTAINER-->
    <div id="contenido-derecha">
    
<!--CONTENT MAIN COLUMN-->
        <div id="contenido-dos-columnas-1">
        
        </div>
        
<!--CONTENT SIDE 2 COLUMN-->
        <div id="contenido-dos-columnas-2">
            <h2>Generales</h2>
            <ul class="list-of-links">
                <li><a href="AdminUsuariosJovy.aspx">Usuarios</a></li>
                <li><a href="AdminTiendasJovy.aspx">Tiendas</a></li>
                <li><a href="AdminCadenasJovy.aspx">Cadenas</a></li>
                <li><a href="AdminRegionesJovy.aspx">Regiones</a></li>
                <li><a href="AdminPeriodosJovy.aspx">Periodos</a></li>
            </ul>
            
            <h2>Por proyecto</h2>
            <ul class="list-of-links">
                <li><a href="CrearEventosJovy.aspx">Crear eventos</a></li>
                <li><a href="EliminarEventosJovy.aspx">Eliminar eventos</a></li>
                <li><a href="AdminRutasJovy.aspx">Editar rutas</a></li>
            </ul>
        </div>
        
        <div class="clear">
        </div>
    </div>
</asp:Content>