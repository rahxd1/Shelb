<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="../AdminMaster.master" 
    AutoEventWireup="false" 
    CodeBehind="EliminarEventosJovy.aspx.vb" 
    Inherits="procomlcd.EliminarEventosJovy"
    Title="Administración Jovy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">
  
<!--titulo-pagina-->
    <div id="titulo-pagina">
        Eliminar eventos promotores Jovy</div>
        
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
            
                    <asp:Label ID="lblPeriodo" runat="server" Text="Periodo" CssClass="caja-texto-lbl" />
                    <asp:DropDownList ID="cmbPeriodo" runat="server" AutoPostBack="True" CssClass="caja-texto-caja" />
                    <br />
                    
                    <asp:Label ID="lblRegion" runat="server" Text="Región" CssClass="caja-texto-lbl" />
                    <asp:DropDownList ID="cmbRegion" runat="server" AutoPostBack="True" CssClass="caja-texto-caja" />
                    <br />
                    
                    <asp:Label ID="lblPromotor" runat="server" Text="Promotor" CssClass="caja-texto-lbl" />
                    <asp:DropDownList ID="cmbPromotor" runat="server" AutoPostBack="True" CssClass="caja-texto-caja" />
                    <br />
                    
                    <asp:Panel ID="pnlGrid" runat="server" Visible="False" ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridRuta" runat="server" AutoGenerateColumns="False" DataKeyNames="id_tienda"
                            GridLines="None" Width="100%" CssClass="grid-view" ShowFooter="True">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkEliminar" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblIDCadena" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id_cadena") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Promotor" DataField="id_usuario"/>
                                <asp:BoundField HeaderText="Tienda" DataField="nombre"/>
                                <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena"/>
                                <asp:BoundField HeaderText="Estatus" DataField="estatus"/>
                            </Columns>
                            <FooterStyle CssClass="grid-footer" />
                        </asp:GridView>
                    </asp:Panel>
            
                    <asp:Button ID="btnEliminarTodos" runat="server" CssClass="button" 
                        Text="Eliminar todos" Enabled="False" />
                    <asp:Button ID="btnEliminarSeleccion" runat="server" CssClass="button" 
                        Text="Eliminar seleccion" Enabled="False" />
                    
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

