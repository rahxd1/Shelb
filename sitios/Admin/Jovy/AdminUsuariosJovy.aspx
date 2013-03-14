<%@Page Language="vb"
    Culture="es-MX" 
    masterpagefile="~/sitios/Admin/AdminMaster.Master"
    AutoEventWireup="false" 
    Codebehind="AdminUsuariosJovy.aspx.vb" 
    Inherits="procomlcd.AdminUsuariosJovy"
    Title="Administración Jovy"  %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">

<!--titulo-pagina-->
<div id="titulo-pagina">
        Usuarios Jovy</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="contenido-dos-columnas-1">
            <asp:ScriptManager ID="ScriptManager" runat="server"/>
            
            <asp:UpdatePanel ID="updPnl" runat="server">
                <ContentTemplate>                
                    <div ID="menu-edicion">
                        <ul>
                            <li><asp:LinkButton ID="lnkNuevo" runat="server">Nuevo</asp:LinkButton></li>
                            <li><asp:LinkButton ID="lnkConsultas" runat="server">Consultas</asp:LinkButton></li>
                        </ul>
                    </div>
                    
                    <div id="div-aviso">        
                        <asp:Label ID="lblAviso" runat="server" />
                    </div>
             
                    <asp:Panel ID="pnlConsulta" runat="server">
                        <asp:Label ID="lblFiltroRegion" runat="server" Text="Región" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbFiltroRegion" runat="server" AutoPostBack="True" 
                            CssClass="caja-texto-caja" />
                        <br />
                        
                        <asp:Label ID="lblFiltroTipoUsuario" runat="server" Text="Tipo usuario" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbFiltroTipoUsuario" runat="server" AutoPostBack="True" 
                                CssClass="caja-texto-caja">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="1">Promotor</asp:ListItem>
                                <asp:ListItem Value="2">Supervisor</asp:ListItem>
                            </asp:DropDownList>
                        <br />
                        
                        <asp:Label ID="lblFiltroUsuario" runat="server" Text="Usuario" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbFiltroUsuario" runat="server" AutoPostBack="True" 
                           CssClass="caja-texto-caja" />
                        <br />
                        
                        <asp:Panel ID="pnlUsuarios" runat="server" ScrollBars="Both"
                            CssClass="panel-grid">
                            <asp:GridView ID="gridUsuario" runat="server" AutoGenerateColumns="False"
                                 GridLines="None" Width="100%" CssClass="grid-view" 
                                ShowFooter="True">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Img/Editar.png" 
                                        ShowEditButton="True" />
                                    <asp:CommandField ButtonType="Image" 
                                        DeleteImageUrl="~/Img/delete-icon.png" 
                                        ShowDeleteButton="True" />
                                    <asp:BoundField DataField="id_usuario" HeaderText="ID Usuario" />
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                    <asp:BoundField DataField="tipo_usuario" HeaderText="Tipo Usuario" />
                                </Columns>
                                <FooterStyle CssClass="grid-footer" />
                            </asp:GridView>
                        </asp:Panel>
                    </asp:Panel>

                    <asp:Panel ID="pnlNuevo" runat="server" Visible="False">
                        <asp:Label ID="lblIDUsuario" runat="server" Text="ID usuario" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtIDUsuario" runat="server" CssClass="caja-texto-caja" MaxLength="15" />
                        <asp:RequiredFieldValidator ID="rfvIDUsuario" runat="server" CssClass="caja-texto-rf"
                            ControlToValidate="txtIDUsuario" ErrorMessage="ID usuario" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblNombreUsuario" runat="server" Text="Nombre del usuario" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="caja-texto-caja" MaxLength="70" />
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" CssClass="caja-texto-rf"
                            ControlToValidate="txtNombre" ErrorMessage="Nombre usuario" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                        <br />
                    
                        <asp:Label ID="lblRegion" runat="server" Text="Región" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbRegion" runat="server" CssClass="caja-texto-caja" />
                        <br />
                        
                        <asp:Label ID="lblTipoUsuario" runat="server" Text="Tipo de usuario" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbTipoUsuario" runat="server" CssClass="caja-texto-caja" />
                        <asp:RequiredFieldValidator ID="rfvTipo" runat="server" CssClass="caja-texto-rf"
                            ControlToValidate="cmbTipoUsuario" ErrorMessage="Tipo usuario" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblProyecto" runat="server" Text="Proyecto" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbProyecto" runat="server" CssClass="caja-texto-caja">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="1">Promotor</asp:ListItem>
                            <asp:ListItem Value="2">Supervisor</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        
                        <asp:ValidationSummary ID="vsUsuario" runat="server" CssClass="aviso"
                            HeaderText="Completa los siguientes campos:" ShowMessageBox="True" 
                            ValidationGroup="Usuario" />  
                        <br />
                        
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" ValidationGroup="Usuario" CssClass="button" />
                        <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" Text="Cancelar" CssClass="button" />
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
        
        <div class="clear"></div>
    </div>
</asp:Content>
