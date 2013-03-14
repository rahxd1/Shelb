<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Admin/AdminMaster.Master" 
    AutoEventWireup="false" 
    CodeBehind="AdminRutasJovy.aspx.vb" 
    Inherits="procomlcd.AdminRutasJovy"
    Title="Administración Jovy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">
  
<!--titulo-pagina-->
    <div id="titulo-pagina">
        Rutas de promotores Jovy</div>
        
<!--CONTENT CONTAINER-->
    <div id="contenido-derecha">

<!--CONTENT MAIN COLUMN-->
        <div id="contenido-dos-columnas-1">
            <asp:ScriptManager ID="ScriptManager" runat="server"/>
            
            <asp:UpdatePanel ID="updPnl" runat="server">
                <ContentTemplate>
                    <div id="div-aviso">        
                        <asp:Label ID="lblAviso" runat="server" />
                    </div>
                    
                    <asp:Label ID="lblPromotor" runat="server" Text="Promotor" CssClass="caja-texto-lbl" />
                    <asp:DropDownList ID="cmbPromotor" runat="server" AutoPostBack="True" CssClass="caja-texto-caja"/>
 
                    <asp:Panel ID="pnlRuta" runat="server" ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridRuta" runat="server" AutoGenerateColumns="False" 
                             GridLines="None" Width="100%" CssClass="grid-view">
                            <Columns>
                                <asp:CommandField ButtonType="Image" 
                                    DeleteImageUrl="~/Img/delete-icon.png" ShowDeleteButton="True" />
                                <asp:BoundField DataField="id_usuario" HeaderText="Promotor" />
                                <asp:BoundField DataField="id_cadena" HeaderText="ID Cadena" />
                                <asp:BoundField DataField="nombre_cadena" HeaderText="Cadena" />
                                <asp:BoundField DataField="id_tienda" HeaderText="ID Tienda" />
                                <asp:BoundField DataField="nombre" HeaderText="Tienda" />
                            </Columns>
                            <FooterStyle CssClass="grid-footer" />
                        </asp:GridView>
                        
                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="button" />
                    </asp:Panel>
                    
                    <asp:Panel ID="pnlAgregar" runat="server" Visible="False">
                        <asp:Label ID="lblEstado" runat="server" Text="Estado" CssClass="caja-texto-lbl" />
                        <asp:ListBox ID="ListEstado" runat="server" AutoPostBack="True" CssClass="caja-texto-caja" />
                        <br />
                        
                        <asp:Label ID="lblCadena" runat="server" Text="Cadena" CssClass="caja-texto-lbl" />
                        <asp:ListBox ID="ListCadena" runat="server" AutoPostBack="True" CssClass="caja-texto-caja" />
                        <br />
                        
                        <asp:Label ID="lblTienda" runat="server" Text="Tienda" CssClass="caja-texto-lbl" />
                        <asp:ListBox ID="ListTienda" runat="server" AutoPostBack="True" CssClass="caja-texto-caja" />
                        <br />
                        
                        <asp:Label ID="lblAgregada" runat="server" CssClass="aviso" />
                        <br />
                        
                        <asp:Button ID="btnAceptar" runat="server" CssClass="button" 
                            Text="Agrega Tienda o Cadena" />
                        <asp:Button ID="btnTerminar" runat="server" CssClass="button" Text="Terminar" />
                    </asp:Panel>
                        
                    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updPnl">
                        <ProgressTemplate>
                            <div id="pnlSave" >
                                <img alt="Cargando..." src="~/Img/loading.gif" /> 
                                Cargando información.
                            </div>
                         </ProgressTemplate>
                    </asp:UpdateProgress>
                </ContentTemplate>
            </asp:UpdatePanel> 
        </div>
        
<!--CONTENT SIDE COLUMN-->
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
            <br />
        </div>
        
        <div class="clear">
        </div>
    </div>
</asp:Content>
