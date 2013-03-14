<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="../AdminMaster.master"
    AutoEventWireup="false" 
    CodeBehind="AdminTiendasJovy.aspx.vb" 
    Inherits="procomlcd.AdminTiendasJovy"
    Title="Administración Jovy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">
  
<!--titulo-pagina-->
    <div id="titulo-pagina">
        Catalogo de tiendas Jovy</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-derecha">

<!--CONTENT MAIN COLUMN-->
        <div id="contenido-dos-columnas-1">
            <asp:ScriptManager ID="ScriptManager" runat="server"/>
            
            <asp:UpdatePanel ID="updPnl" runat="server">
                <ContentTemplate>
                    <div ID="menu-edicion">
                        <ul>
                            <li><asp:LinkButton ID="lnkNuevo" runat="server" Font-Bold="False">Nuevo</asp:LinkButton></li>
                            <li><asp:LinkButton ID="lnkConsultas" runat="server" Font-Bold="False">Consultas</asp:LinkButton></li>
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
                        
                        <asp:Label ID="lblFiltroCadena" runat="server" Text="Nombre cadena" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbFiltroCadena" runat="server" AutoPostBack="True" 
                            CssClass="caja-texto-caja" />
                        <br />
                        
                        <asp:Label ID="lblFiltroTienda" runat="server" Text="Tienda" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbFiltroTienda" runat="server" AutoPostBack="True" 
                            CssClass="caja-texto-caja" />
                            
                        <asp:Panel ID="pnlTienda" runat="server" ScrollBars="Both"
                            CssClass="panel-grid">
                            <asp:GridView ID="gridTiendas" runat="server" AutoGenerateColumns="False" 
                                 GridLines="None" Width="100%" CssClass="grid-view" ShowFooter="True">
                                <Columns>       
                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Img/Editar.png" 
                                        ShowEditButton="True" />
                                    <asp:BoundField HeaderText="ID Tienda" DataField="id_tienda"/>
                                    <asp:BoundField HeaderText="Nombre" DataField="nombre"/>        
                                    <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena"/>
                                    <asp:BoundField HeaderText="Ciudad" DataField="Ciudad"/>
                                    <asp:BoundField HeaderText="Estado" DataField="nombre_estado"/> 
                                    <asp:BoundField HeaderText="Región" DataField="nombre_region"/> 
                                </Columns>
                                <FooterStyle CssClass="grid-footer" />
                            </asp:GridView>
                        </asp:Panel>
                    </asp:Panel>
 
                    <asp:Panel ID="pnlNuevo" runat="server" Visible="False"  Width="100%">
                        <asp:Label ID="IDTienda" runat="server" Visible="false" /><br />
                        
                        <asp:Label ID="lblNoTienda" runat="server" Text="No. de tienda" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtNoTienda" runat="server" MaxLength="10" CssClass="caja-texto-caja" />
                        <br />
                        
                        <asp:Label ID="lblNombreTienda" runat="server" Text="Nombre tienda" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtNombreTienda" runat="server" MaxLength="30" CssClass="caja-texto-caja" />
                        <asp:RequiredFieldValidator ID="rfvNombreTienda" runat="server" CssClass="caja-texto-rf"
                            ControlToValidate="txtNombreTienda" ErrorMessage="Nombre tienda" ValidationGroup="Tienda">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblCadena" runat="server" Text="Cadena" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbCadena" runat="server" CssClass="caja-texto-caja" />
                        <asp:RequiredFieldValidator ID="rfvCadena" runat="server" CssClass="caja-texto-rf"
                            ControlToValidate="cmbCadena" ErrorMessage="Cadena" ValidationGroup="Tienda">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblRegion" runat="server" Text="Región" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbRegion" runat="server" CssClass="caja-texto-caja" />
                        <asp:RequiredFieldValidator ID="rfvRegion" runat="server" CssClass="caja-texto-rf"
                            ControlToValidate="cmbRegion" ErrorMessage="Región" ValidationGroup="Tienda">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblCiudad" runat="server" Text="Ciudad" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtCiudad" runat="server" MaxLength="25" CssClass="caja-texto-caja" />
                        <asp:RequiredFieldValidator ID="rfvCiudad" runat="server" CssClass="caja-texto-rf"
                            ControlToValidate="txtCiudad" ErrorMessage="Ciudad" ValidationGroup="Tienda">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblEstado" runat="server" Text="Estado" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbEstado" runat="server" CssClass="caja-texto-caja"/>
                        <asp:RequiredFieldValidator ID="rfvEstado" runat="server" CssClass="caja-texto-rf"
                            ControlToValidate="cmbEstado" ErrorMessage="Estado" ValidationGroup="Tienda">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:ValidationSummary ID="vsTienda" runat="server" CssClass="aviso"
                            HeaderText="Completa los siguientes campos:" ShowMessageBox="True" 
                            ValidationGroup="Tienda" />  
                        <br />
                        
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="button" 
                            ValidationGroup="Tienda"/>
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                            CausesValidation="False" CssClass="button" />
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
