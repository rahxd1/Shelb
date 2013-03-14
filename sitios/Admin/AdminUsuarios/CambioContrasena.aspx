<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Admin/AdminMaster.Master"
    AutoEventWireup="false" 
    CodeBehind="CambioContrasena.aspx.vb" 
    Inherits="procomlcd.CambioContrasena"
    Title="Administración"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">

<!--titulo-pagina-->
<div id="titulo-pagina">
        Cambio de contraseña</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="contenido-dos-columnas-1">                      
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            
            <asp:UpdatePanel ID="updPnl" runat="server">
                <ContentTemplate>
                    <div id="div-aviso">        
                        <asp:Label ID="lblAviso" runat="server" />
                    </div>
            
                    <asp:Panel ID="pnlConsulta" runat="server">
                        <asp:Label ID="lblFiltroNombreCliente" runat="server" Text="Cliente" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbFiltroCliente" runat="server" 
                            CssClass="caja-texto-caja" />
                        <br />
                        
                        <asp:Label ID="lblFiltroDepartamento" runat="server" Text="Departamento" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbFiltroDepartamento" runat="server" AutoPostBack="True" 
                            CssClass="caja-texto-caja" />
                        <br />
                        
                        <asp:Label ID="lblFiltroTipoUsuario" runat="server" Text="Tipo usuario" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbFiltroTipoUsuario" runat="server" AutoPostBack="True" 
                            CssClass="caja-texto-caja" />
                        <br />
 
                        <asp:Panel ID="pnlGrid" runat="server" Height="500px" ScrollBars="Both">
                             <asp:GridView ID="gridUsuario" runat="server" AutoGenerateColumns="False" 
                                Width="100%" Visible="False" CssClass="grid-view" ShowFooter="True" 
                                GridLines="None">
                                <Columns>       
                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Img/Editar.png" 
                                        ShowEditButton="True" />
                                    <asp:BoundField HeaderText="ID Usuario" DataField="id_usuario"/>
                                    <asp:BoundField HeaderText="Nombre" DataField="nombre"/>        
                                    <asp:BoundField HeaderText="Tipo Usuario" DataField="tipo_usuario"/> 
                                </Columns>
                                  <FooterStyle CssClass="grid-footer" />
                            </asp:GridView>
                        </asp:Panel>
                    </asp:Panel>

                    <asp:Panel ID="pnlNuevo" runat="server" Visible="False">
                        <asp:Label ID="lblIDUsuario" runat="server" Text="ID Usuario" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="lblUsuario" runat="server" CssClass="caja-texto-caja" Enabled="false" />
                        <br />
                        
                        <asp:Label ID="lblContrasena" runat="server" Text="Contraseña" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" 
                            CssClass="caja-texto-caja" MaxLength="10" />
                        <br />
                        
                        <asp:Label ID="lblConfirmaContrasena" runat="server" Text="Confirma contraseña" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtPasswordConfirma" runat="server" 
                            TextMode="Password" CssClass="caja-texto-caja" MaxLength="10" />
                        <br />
                        
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="button" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="button" />
                    </asp:Panel>
                    
                    <asp:UpdateProgress ID="UpdateProgress" runat="server" 
                        AssociatedUpdatePanelID="updPnl">
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
            <ul class="list-of-links">
                <li><a href="AdminUsuarios.aspx">Administración Usuarios</a></li>
                <li><a href="CambioContrasena.aspx">Cambio contraseña</a></li>
                <li><a href="PermisosUsuarios.aspx">Permisos usuarios</a></li>
                <li><a href="Bitacora.aspx">Bitácora accesos</a></li>
             </ul>
        </div>
        
        <div class="clear"></div>
    </div>
</asp:Content>
