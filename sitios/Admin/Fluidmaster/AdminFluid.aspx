<%@ Page Language="VB" 
    Culture="es-MX" 
    MasterPageFile="../AdminMaster.master" 
    AutoEventWireup="true" 
    CodeBehind="AdminFluid.aspx.vb" 
    Inherits="procomlcd.AdminFluid"
    Title="Administración Fluidmaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">

<!--TITLE-->
    <div id="titulo-pagina">
        Administración Fluidmaster</div>
        
<!--CONTENT CONTAINER-->
    <div id="contenido-derecha">
    
<!--CONTENT MAIN COLUMN-->
        <div id="contenido-dos-columnas-1">
        
        </div>
        
<!--CONTENT SIDE 2 COLUMN-->
        <div id="contenido-dos-columnas-2">
            <h2>Generales</h2>
            <ul class="list-of-links">
                <li><a href="AdminUsuariosFluid.aspx">Usuarios</a></li>
                <li><a href="AdminTiendasFluid.aspx">Tiendas</a></li>
                <li><a href="AdminCadenasFluid.aspx">Distribuidor</a></li>
                <li><a href="AdminRegionesFluid.aspx">Regiones</a></li>
                <li><a href="AdminPeriodosFluid.aspx">Periodos</a></li>
            </ul>
            
            <h2>Por proyecto</h2>
            <ul class="list-of-links">
                <li><a href="AdminRutasFluid.aspx">Editar rutas</a></li>
            </ul>
        </div>
        
        <div class="clear">
        </div>
    </div>
</asp:Content>