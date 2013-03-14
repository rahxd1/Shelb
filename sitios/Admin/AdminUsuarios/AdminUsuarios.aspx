<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile ="~/sitios/Admin/AdminMaster.Master"
    AutoEventWireup="false" 
    CodeBehind="AdminUsuarios.aspx.vb" 
    Inherits="procomlcd.AdminUsuarios"
    Title="Administración"  %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">

<!--titulo-pagina-->
<div id="titulo-pagina">Administración usuarios</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="contenido-dos-columnas-1">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            
            <asp:UpdatePanel ID="updPnl" runat="server">
                <ContentTemplate>              
                
                    <div id="menu-edicion">
                        <ul>
                            <li><asp:LinkButton ID="lnkNuevo" runat="server">Nuevo</asp:LinkButton></li>
                            <li><asp:LinkButton ID="lnkConsultas" runat="server">Consultas</asp:LinkButton></li>
                        </ul>
                    </div>
                
                    <div id="div-aviso">        
                        <asp:Label ID="lblAviso" runat="server" />
                    </div>
            
                    <asp:Panel ID="pnlConsulta" runat="server" style="text-align: left">
                        <asp:Label ID="lblFiltroCliente" runat="server" Text="Cliente" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbFiltroCliente" runat="server" AutoPostBack="True" 
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
                        
                        <asp:Label ID="lblFiltroUsuario" runat="server" Text="Usuario" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbFiltroUsuario" runat="server" AutoPostBack="True" 
                            CssClass="caja-texto-caja" />
                        <br />
            
                        <asp:Panel ID="pnlGrid" runat="server" Height="500px" ScrollBars="Both">
                            <asp:GridView ID="gridUsuario" runat="server" AutoGenerateColumns="False" 
                                Width="100%" Visible="False" CssClass="grid-view" ShowFooter="True" 
                                GridLines="None">
                                <Columns>       
                                    <asp:CommandField ButtonType="Image" ShowDeleteButton="True" DeleteImageUrl="~/Img/delete-icon.png" />
                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Img/Editar.png" ShowEditButton="True" />
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
                        <asp:TextBox ID="txtIDUsuario" runat="server" CssClass="caja-texto-caja" 
                            MaxLength="20" />
                        <asp:RequiredFieldValidator ID="rfvIDUsuario" runat="server" 
                            ControlToValidate="txtIDUsuario" ErrorMessage="Nombre de usuario" 
                            ValidationGroup="Usuario" CssClass="caja-texto-rf">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblContrasena" runat="server" Text="Contraseña" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" 
                            CssClass="caja-texto-caja" MaxLength="10" />
                        <br />
                        
                        <asp:Label ID="lblNombreUsuario" runat="server" Text="Nombre del Usuario" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="caja-texto-caja" 
                             MaxLength="80" />
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" 
                             ControlToValidate="txtNombre" ErrorMessage="Nombre usuario" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblTipoUsuario" runat="server" Text="Tipo de usuario" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbTipoUsuario" runat="server" CssClass="caja-texto-caja" />
                        <asp:RequiredFieldValidator ID="rfvTipo" runat="server" 
                            ControlToValidate="cmbTipoUsuario" ErrorMessage="Tipo usuario" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblDepartamento" runat="server" Text="Departamento" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbDepartamento" runat="server" CssClass="caja-texto-caja" />
                        <asp:RequiredFieldValidator ID="rfvDepartamento" runat="server" 
                            ControlToValidate="cmbDepartamento" ErrorMessage="Departamento" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblCorreoElectronico" runat="server" Text="Correo electronico" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtCorreo" runat="server" CssClass="caja-texto-caja" MaxLength="40" />
                        <br />
                        
                        <asp:Label ID="lblCliente" runat="server" Text="Cliente" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbCliente" runat="server" CssClass="caja-texto-caja"/>
                        <asp:RequiredFieldValidator ID="rfvCliente" runat="server" 
                            ControlToValidate="cmbCliente" ErrorMessage="Cliente" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:GridView ID="gridAccesos" runat="server" AutoGenerateColumns="False" 
                            CssClass="grid-view" DataKeyNames="id_proyecto" 
                            ShowFooter="True" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Acceso">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkAcceso" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "acceso"))%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="nombre_proyecto" HeaderText="Proyecto" />
                                <asp:BoundField DataField="nombre_cliente" HeaderText="Cliente" />
                                <asp:BoundField DataField="id_cliente" HeaderText="Cliente" />
                                <asp:TemplateField HeaderText="Región">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cmbRegion" runat="server" Height="22px" CssClass="caja-texto-caja"
                                                style="margin-left: 0px" Width="200px">
                                                <asp:ListItem Value="0">0</asp:ListItem>
                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                <asp:ListItem Value="6">6</asp:ListItem>
                                                <asp:ListItem Value="7">7</asp:ListItem>
                                                <asp:ListItem Value="8">8</asp:ListItem>
                                                <asp:ListItem Value="9">9</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="XX-Small" />
                                        <ItemStyle VerticalAlign="top" Width="0%" />
                                    </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <h1>
                                    No hay datos</h1>
                            </EmptyDataTemplate>
                            <FooterStyle CssClass="grid-footer" />
                        </asp:GridView>
                        
                        <asp:ValidationSummary ID="vsUsuario" runat="server" CssClass="aviso"
                            HeaderText="Completa los siguientes campos:" ShowMessageBox="True" 
                            ValidationGroup="Usuario" />  
                        <br />
                        
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" 
                            ValidationGroup="Usuario" CssClass="button" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                            CausesValidation="False" CssClass="button" />
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
