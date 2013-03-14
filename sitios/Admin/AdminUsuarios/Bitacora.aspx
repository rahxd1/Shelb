<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Admin/AdminMaster.Master"
    AutoEventWireup="false" 
    CodeBehind="Bitacora.aspx.vb" 
    Inherits="procomlcd.Bitacora"
    Title="Bitácora accesos" %>

<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">
  
<!--titulo-pagina -->
    <div id="titulo-pagina">Bitácora accesos</div>    

<!--CONTENT CONTAINER-->
    <div id="contenido-derecha">

<!--CONTENT MAIN COLUMN-->
        <div id="contenido-dos-columnas-1">
            <asp:ScriptManager ID="ScriptManager" runat="server" />
            
            <asp:UpdatePanel ID="updPnl" runat="server">
                <ContentTemplate> 
                
                    <asp:Label ID="lblCliente" runat="server" Text="Cliente" CssClass="caja-texto-lbl" />
                    <asp:DropDownList ID="cmbCliente" runat="server" AutoPostBack="True" 
                        CssClass="caja-texto-caja" />
                    <br />
                                
                    <asp:Label ID="lblTipoUsuario" runat="server" Text="Tipo usuario" CssClass="caja-texto-lbl" />
                    <asp:DropDownList ID="cmbTipoUsuario" runat="server" AutoPostBack="True" 
                        CssClass="caja-texto-caja"/>
                    <br />
                    
                    <asp:Label ID="lblUsuario" runat="server" Text="Usuario" CssClass="caja-texto-lbl" />
                    <asp:DropDownList ID="cmbUsuario" runat="server" AutoPostBack="True" 
                        CssClass="caja-texto-caja" />
                    <br />
                    
                    <asp:Label ID="lblMes" runat="server" Text="Mes" CssClass="caja-texto-lbl" />
                    <asp:DropDownList ID="cmbMes" runat="server" AutoPostBack="True" 
                        CssClass="caja-texto-caja" />
                    <br />
                    
                    <asp:Label ID="lblAno" runat="server" Text="Año" CssClass="caja-texto-lbl" />
                    <asp:DropDownList ID="cmbAnio" runat="server" AutoPostBack="True" 
                        CssClass="caja-texto-caja" />
                    <br />
                                                  
                    <asp:Panel ID="pnlConsulta" runat="server" ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridBitacora" runat="server" AutoGenerateColumns="False" 
                            Width="100%" CssClass="grid-view" ShowFooter="True" GridLines="None">
                            <Columns>       
                                <asp:BoundField DataField="id_usuario" HeaderText="Usuario" />
                                <asp:BoundField DataField="dia" HeaderText="Dia" />
                                <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="hora" HeaderText="Hora ingreso" />
                                </Columns>
                            <FooterStyle CssClass="grid-footer" />
                        </asp:GridView>
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
    </div>
</asp:Content>
