<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Admin/AdminMaster.Master"
    AutoEventWireup="false" 
    CodeBehind="AdminCadenasFluid.aspx.vb" 
    Inherits="procomlcd.AdminCadenasFluid"
    Title="Administración Fluidmaster"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">
     
<!--PAGE TITLE-->
<div id="titulo-pagina">Catalogo de distribuidores Fluidmaster</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-derecha">

<!--CONTENT MAIN COLUMN-->
        <div id="contenido-dos-columnas-1">      
            <asp:ScriptManager ID="ScriptManager1" runat="server"/>
            
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
            
                    <asp:Panel ID="pnlConsulta" runat="server" Width="100%" ScrollBars="Both"
                            CssClass="panel-grid">
                        <asp:GridView ID="gridCadena" runat="server" AutoGenerateColumns="False" 
                             GridLines="None" Width="100%" CssClass="grid-view" ShowFooter="True">
                            <Columns>       
                                <asp:CommandField ButtonType="Image" EditImageUrl="~/Img/Editar.png" 
                                    ShowEditButton="True" />
                                <asp:BoundField HeaderText="ID Distribuidor" DataField="id_cadena"/>
                                <asp:BoundField HeaderText="Distribuidor" DataField="nombre_cadena"/>        
                                <asp:BoundField HeaderText="Tipo distribuidor" DataField="nombre_tipocadena"/> 
                            </Columns>
                            <FooterStyle CssClass="grid-footer" />
                        </asp:GridView>
                    </asp:Panel>
            
                    <asp:Panel ID="pnlNuevo" runat="server" Visible="False" Width="100%">
                        <asp:Label ID="lblIDCadena" runat="server" Visible="false" /><br />
                        
                        <asp:Label ID="lblNombreCadena" runat="server" Text="Nombre cadena" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtNombreCadena" runat="server" CssClass="caja-texto-caja" 
                            MaxLength="50" />
                        <asp:RequiredFieldValidator ID="rfvNombreCadena" runat="server" 
                            ControlToValidate="txtNombreCadena" ErrorMessage="Nombre cadena" 
                            ValidationGroup="Cadena">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblTipoCadena" runat="server" Text="Tipo cadena" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbTipoCadena" runat="server" AutoPostBack="True" 
                            CssClass="caja-texto-caja" />
                        <asp:RequiredFieldValidator ID="rfvTipoCadena" runat="server" 
                            ControlToValidate="cmbTipoCadena" ErrorMessage="Tipo cadena" 
                            ValidationGroup="Cadena">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:ValidationSummary ID="vsCadena" runat="server" CssClass="aviso"
                            HeaderText="Completa los siguientes campos:" ShowMessageBox="True" 
                            ValidationGroup="Cadena" />  
                        <br />
                        
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="button" 
                            ValidationGroup="Cadena" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="button" />
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
        
 <!--CONTENT SIDE COLUMN -->
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
        
        <div class="clear"></div>
    </div>
    </div>
</asp:Content>
