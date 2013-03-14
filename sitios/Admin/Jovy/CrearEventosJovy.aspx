<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="../AdminMaster.master" 
    AutoEventWireup="false" 
    CodeBehind="CrearEventosJovy.aspx.vb" 
    Inherits="procomlcd.CrearEventosJovy"
    Title="Administración Jovy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">
  
<!--titulo-pagina-->
    <div id="titulo-pagina">
        Eventos de promotores Jovy</div>
        
<!--CONTENT CONTAINER-->
    <div id="contenido-derecha">

<!--CONTENT MAIN COLUMN-->
        <div id="contenido-dos-columnas-1">
            <asp:ScriptManager ID="ScriptManager" runat="server"/>
            
            <asp:UpdatePanel ID="updPnl" runat="server">
                <ContentTemplate>  
                    <div id="div-aviso">        
                        <asp:Label ID="lblAviso" runat="server" />
                        <asp:Label ID="lblAviso1" runat="server" />
                    </div>
                    
                    <asp:Label ID="lblPeriodo" runat="server" Text="Periodo" CssClass="caja-texto-lbl" />
                    <asp:DropDownList ID="cmbPeriodo" runat="server" CssClass="caja-texto-caja" />
                    <asp:RequiredFieldValidator ID="rfvPeriodo" runat="server" CssClass="caja-texto-rf" 
                        ControlToValidate="cmbPeriodo" ErrorMessage="* Selecciona periodo" 
                        ValidationGroup="Eventos" />
                    <br />
                    
                    <asp:Label ID="lblPromotor" runat="server" Text="Promotor" CssClass="caja-texto-lbl" />
                    <asp:DropDownList ID="cmbdemo" runat="server" CssClass="caja-texto-caja"/>
                    <br />

                    <asp:Button ID="btnCargar" runat="server" Text="Cargar" CssClass="button"
                        ValidationGroup="Eventos" />

                    <asp:GridView ID="gridRuta" runat="server" AutoGenerateColumns="False" 
                        GridLines="None" Width="100%" 
                        Visible="False" CssClass="grid-view" ShowFooter="True" >
                        <Columns>
                            <asp:BoundField HeaderText="Promotor" DataField="id_usuario"/> 
                            <asp:BoundField HeaderText="Cadena" DataField="id_cadena"/> 
                            <asp:BoundField HeaderText="Tienda" DataField="id_tienda"/> 
                        </Columns>
                        <FooterStyle CssClass="grid-footer" />
                    </asp:GridView>
            
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
