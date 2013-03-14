<%@ Page Language="vb"
    Culture="es-MX" 
    masterpagefile="../AdminMaster.master"
    AutoEventWireup="false" 
    CodeBehind="AdminRegionesJovy.aspx.vb" 
    Inherits="procomlcd.AdminRegionesJovy" 
    Title="Administración Jovy"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">
  
<!--titulo-pagina-->
    <div id="titulo-pagina">
        Catalogo de regiones Jovy</div>

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
                
                    <asp:Panel ID="pnlConsulta" runat="server" ScrollBars="Both"
                            CssClass="panel-grid">
                        <asp:GridView ID="gridRegiones" runat="server" AutoGenerateColumns="False" 
                             GridLines="None" Width="100%" CssClass="grid-view" ShowFooter="True">
                            <Columns>       
                                <asp:CommandField ButtonType="Image" EditImageUrl="~/Img/Editar.png" 
                                    ShowEditButton="True" />
                                <asp:BoundField HeaderText="ID Región" DataField="id_region"/>
                                <asp:BoundField HeaderText="Región" DataField="nombre_region"/>      
                            </Columns>
                            <FooterStyle CssClass="grid-footer" />
                        </asp:GridView>
                    </asp:Panel>

                    <asp:Panel ID="pnlNuevo" runat="server" Visible="False">
                        <asp:Label ID="lblIDRegion" runat="server" Visible="false" /><br />
                        
                        <asp:Label ID="lblNombreRegion" runat="server" Text="Nombre región" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtNombreRegion" runat="server" CssClass="caja-texto-caja" MaxLength="20" />
                        <asp:RequiredFieldValidator ID="rfvNombreRegion" runat="server" CssClass="caja-texto-rf"
                            ControlToValidate="txtNombreRegion" ErrorMessage="Nombre región" ValidationGroup="Region">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:ValidationSummary ID="vsRegion" runat="server" CssClass="aviso"
                            HeaderText="Completa los siguientes campos:" ShowMessageBox="True" 
                            ValidationGroup="Region" />  
                        <br />
                        
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" ValidationGroup="Region" CssClass="button" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="False" CssClass="button" />
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