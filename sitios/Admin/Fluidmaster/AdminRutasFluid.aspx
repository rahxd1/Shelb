<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Admin/AdminMaster.Master" 
    AutoEventWireup="false" 
    CodeBehind="AdminRutasFluid.aspx.vb" 
    Inherits="procomlcd.AdminRutasFluid"
    Title="Administración Fluidmaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">
  
<!--titulo-pagina-->
    <div id="titulo-pagina">
        Rutas de promotores Fluidmaster</div>
        
<!--CONTENT CONTAINER-->
    <div id="contenido-derecha">

<!--CONTENT MAIN COLUMN-->
        <div id="contenido-dos-columnas-1">
            <asp:ScriptManager ID="ScriptManager1" runat="server"/>
            
            <asp:UpdatePanel ID="updPnl" runat="server">
                <ContentTemplate>
                    <div id="div-aviso">        
                        <asp:Label ID="lblAviso" runat="server" />
                    </div>
                    
                    <asp:Label ID="lblPromotor" runat="server" Text="Promotor" CssClass="caja-texto-lbl" />
                    <asp:DropDownList ID="cmbPromotor" runat="server" 
                        AutoPostBack="True" CssClass="caja-texto-caja" />
                    <br />
                
                    <asp:Panel ID="pnlRuta" runat="server" ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridRuta" runat="server" AutoGenerateColumns="False"
                            GridLines="None" Width="100%" CssClass="grid-view">
                            <Columns>
                                <asp:CommandField ButtonType="Image" 
                                    DeleteImageUrl="~/Img/delete-icon.png" ShowDeleteButton="True" />
                                <asp:BoundField DataField="id_usuario" HeaderText="Promotor" />
                                <asp:BoundField DataField="id_estado" HeaderText="ID Estado" />
                                <asp:BoundField DataField="nombre_estado" HeaderText="Estado" />
                                <asp:BoundField DataField="id_region" HeaderText="ID Region" />
                                <asp:BoundField DataField="nombre_region" HeaderText="Región" />
                            </Columns>
                            <FooterStyle CssClass="grid-footer" />
                        </asp:GridView>
                        
                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="button" />
                    </asp:Panel>
                        
                    <asp:Panel ID="pnlAgregar" runat="server" Visible="False">
                        <asp:Label ID="lblRegion" runat="server" Text="Región" CssClass="caja-texto-lbl" />
                        <asp:ListBox ID="ListRegion" runat="server" AutoPostBack="True" CssClass="caja-texto-caja" />
                        <br />
                        
                        <asp:Label ID="lblEstado" runat="server" Text="Estado" CssClass="caja-texto-lbl" />
                        <asp:ListBox ID="ListEstado" runat="server" AutoPostBack="True" CssClass="caja-texto-caja" />
                        
                        <asp:Label ID="lblAgregada" runat="server" CssClass="aviso" />
                        <br />
                        
                        <asp:Button ID="btnAceptar" runat="server" CssClass="button" 
                            Text="Agregar" />
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
            <br />
        </div>
        
        <div class="clear">
        </div>
    </div>
</asp:Content>
