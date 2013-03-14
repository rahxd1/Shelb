<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Admin/AdminMaster.Master"
    AutoEventWireup="false" 
    CodeBehind="AdminTiendasMarsAS.aspx.vb" 
    Inherits="procomlcd.AdminTiendasMarsAS"
    Title="Administración Mars Autoservicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">
  
<!--titulo-pagina-->
    <div id="titulo-pagina">
        Tiendas Mars Autoservicio</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-derecha">

<!--CONTENT MAIN COLUMN-->
        <div id="contenido-dos-columnas-1">
            <asp:ScriptManager ID="ScriptManager" runat="server"/>
            
            <asp:UpdatePanel ID="updPnl" runat="server">
                <ContentTemplate>                
                    <div id="menu-edicion">
                        <ul>
                            <li><asp:LinkButton ID="lnkNuevo" runat="server" Font-Bold="False">Nuevo</asp:LinkButton></li>
                            <li><asp:LinkButton ID="lnkConsultas" runat="server" Font-Bold="False">Consultas</asp:LinkButton></li>
                        </ul>
                    </div>
                    
                    <div id="div-aviso">        
                        <asp:Label ID="lblAviso" runat="server" />
                    </div>

                    <asp:Panel ID="pnlNuevo" runat="server" Visible="False" Width="100%">
                        <asp:Label ID="IDTienda" runat="server" Visible="false" /><br />
                        
                        <asp:Label ID="lblSAP" runat="server" Text="No. SAP" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtNoTienda" runat="server" CssClass="caja-texto-caja" MaxLength="20" />
                        <br />
                        
                        <asp:Label ID="lblNombreTienda" runat="server" Text="Nombre tienda" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtNombreTienda" runat="server" CssClass="caja-texto-caja" 
                            MaxLength="100" Width="385px" />
                        <asp:RequiredFieldValidator ID="rfvNombreTienda" runat="server" ControlToValidate="txtNombreTienda" 
                            ErrorMessage="Tienda" ValidationGroup="Tienda">*</asp:RequiredFieldValidator>
                        <br />
                                                                
                        <asp:Label ID="lblCadena" runat="server" Text="Cadena" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbCadena" runat="server" CssClass="caja-texto-caja" />
                        <asp:RequiredFieldValidator ID="rfvCadena" runat="server" CssClass="caja-texto-rf"
                            ControlToValidate="cmbCadena" ErrorMessage="Cadena" 
                            ValidationGroup="Tienda">*</asp:RequiredFieldValidator>
                        <br />
                                                    
                        <asp:Label ID="lblFormato" runat="server" Text="Formato" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbFormato" runat="server" CssClass="caja-texto-caja" />
                        <asp:RequiredFieldValidator ID="rfvFormato" runat="server" CssClass="caja-texto-rf" 
                            ControlToValidate="cmbFormato" ErrorMessage="Formato" 
                            ValidationGroup="Tienda">*</asp:RequiredFieldValidator>
                        <br />

                        <asp:Label ID="lblGrupo" runat="server" Text="Grupo" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbGrupo" runat="server" CssClass="caja-texto-caja" />
                        <asp:RequiredFieldValidator ID="rfvGrupo" runat="server" CssClass="caja-texto-rf" 
                            ControlToValidate="cmbGrupo" ErrorMessage="Grupo" 
                            ValidationGroup="Tienda">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblCiudad" runat="server" Text="Ciudad" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtCiudad" runat="server" CssClass="caja-texto-caja" MaxLength="30" />
                        <asp:RequiredFieldValidator ID="rfvCiudad" runat="server" CssClass="caja-texto-rf" 
                            ControlToValidate="txtCiudad" ErrorMessage="Indica la ciudad" 
                            ValidationGroup="Tienda">*</asp:RequiredFieldValidator>
                        <br />
                            
                        <asp:Label ID="lblEstado" runat="server" Text="Estado" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbEstado" runat="server" CssClass="caja-texto-caja" />
                        <asp:RequiredFieldValidator ID="rfvEstado" runat="server" CssClass="caja-texto-rf" 
                            ControlToValidate="cmbEstado" ErrorMessage="Estado" 
                            ValidationGroup="Tienda">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblRegion" runat="server" Text="Región" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbRegion" runat="server" CssClass="caja-texto-caja" />
                        <asp:RequiredFieldValidator ID="rfvRegion" runat="server" CssClass="caja-texto-rf" 
                            ControlToValidate="cmbRegion" ErrorMessage="Región" 
                            ValidationGroup="Tienda">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblAreaNielsen" runat="server" Text="Area nielsen" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbAreaNielsen" runat="server" CssClass="caja-texto-caja" />
                        <asp:RequiredFieldValidator ID="rfvAreaNielsen" runat="server" CssClass="caja-texto-rf" 
                            ControlToValidate="cmbAreaNielsen" ErrorMessage="Área nielsen" 
                            ValidationGroup="Tienda">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblClasificacion" runat="server" Text="Clasificación" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbClasificacion" runat="server" CssClass="caja-texto-caja" />
                        <asp:RequiredFieldValidator ID="rfvClasificacion" runat="server" CssClass="caja-texto-rf" 
                            ControlToValidate="cmbClasificacion" 
                            ErrorMessage="Clasificación de la tienda" ValidationGroup="Tienda">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblRankNacional" runat="server" Text="Rank nacional" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtRank" runat="server" CssClass="caja-texto-caja" MaxLength="30" 
                            Width="88px" />
                        <asp:RequiredFieldValidator ID="rfvRank" runat="server"  CssClass="caja-texto-rf"
                            ControlToValidate="txtRank" ErrorMessage="Rank nacional" 
                            ValidationGroup="Tienda">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:ValidationSummary ID="vsTienda" runat="server" CssClass="aviso"
                            HeaderText="Completa los siguientes campos:" ShowMessageBox="True" 
                            ValidationGroup="Tienda" />  
                        <br />
                        
                        <asp:Button ID="btnGuardar" runat="server" CssClass="button" Text="Guardar" 
                            ValidationGroup="Tienda" />
                        <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" 
                            CssClass="button" Text="Cancelar" />
                    </asp:Panel>
                    
                    <asp:Panel ID="pnlConsulta" runat="server" Visible="False">
                        <asp:Label ID="lblFiltroRegion" runat="server" Text="Región" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbFiltroRegion" runat="server" AutoPostBack="True" 
                            CssClass="caja-texto-caja" />
                        <br />
                        
                        <asp:Label ID="lblFiltroEstado" runat="server" Text="Estado" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbFiltroEstado" runat="server" AutoPostBack="True" 
                            CssClass="caja-texto-caja" />
                        <br />
                        
                        <asp:Label ID="lblFiltroCadena" runat="server" Text="Cadena" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbFiltroCadena" runat="server" AutoPostBack="True" 
                            CssClass="caja-texto-caja" />
                        <br />
                        
                        <asp:Label ID="lblFiltroTienda" runat="server" Text="Tienda" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbFiltroTienda" runat="server" AutoPostBack="True" 
                            CssClass="caja-texto-caja" />
                        <br />
                        
                        <asp:Label ID="lblFiltroCodigo" runat="server" Text="Código" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtBuscaCodigo" runat="server" CssClass="caja-texto-caja" MaxLength="50" />
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar por código" ValidationGroup="Tienda" />
                        <br />
                        
                        <asp:Label ID="lblFiltroNombreTienda" runat="server" Text="Nombre tienda" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtBuscaTienda" runat="server" CssClass="caja-texto-caja" 
                            MaxLength="50" Width="200px" />
                        <asp:Button ID="btnBuscarNombre" runat="server" 
                            Text="Buscar por nombre" ValidationGroup="Tienda" />
            
                        <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both"
                            CssClass="panel-grid">
                            <asp:GridView ID="gridTiendas" runat="server" AutoGenerateColumns="False" 
                                 GridLines="None" Width="100%" CssClass="grid-view" ShowFooter="True">
                                <Columns>       
                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Img/Editar.png" 
                                        ShowEditButton="True" />
                                    <asp:BoundField HeaderText="ID Tienda" DataField="id_tienda"/>
                                    <asp:BoundField HeaderText="Codigo" DataField="codigo"/>
                                    <asp:BoundField HeaderText="Nombre" DataField="nombre"/>        
                                    <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena"/>
                                    <asp:BoundField HeaderText="Tipo tienda" DataField="clasificacion_tienda"/>
                                    <asp:BoundField HeaderText="En ruta de:" DataField="id_usuario"/>
                                    <asp:BoundField HeaderText="Ciudad" DataField="Ciudad"/>
                                    <asp:BoundField HeaderText="Estado" DataField="nombre_estado"/> 
                                    <asp:BoundField HeaderText="Región" DataField="nombre_region"/> 
                                </Columns>
                                <FooterStyle CssClass="grid-footer" />
                            </asp:GridView>
                        </asp:Panel>
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
                <li><a href="/sitios/Admin/Mars/AdminUsuariosMars.aspx">Usuarios</a></li>
                <li><a href="/sitios/Admin/Mars/AdminCadenasMars.aspx">Cadenas</a></li>
                <li><a href="/sitios/Admin/Mars/AdminCadenasMayoreoMars.aspx">Cadenas mayoreo</a></li>
                <li><a href="/sitios/Admin/Mars/AdminRegionesMars.aspx">Regiones</a></li>
                <li><a href="/sitios/Admin/Mars/AdminPeriodosMars.aspx">Periodos</a></li>
            </ul>
            
            <h2>Por proyecto</h2>
                <asp:GridView ID="gridProyectos" runat="server" AutoGenerateColumns="False" 
                    GridLines="None" Height="16px" ShowHeader="False" Width="165px" 
                    CellPadding="3" style="text-align: center">
                    <RowStyle ForeColor="#000066" HorizontalAlign="Left" />
                   <Columns>
                     <asp:TemplateField HeaderText="" >
                       <ItemTemplate>
                        <ul class="list-of-links">
                            <a href='<%#Eval("ruta")%>'><%#Eval("pagina")%></a> 
                        </ul>                           </ItemTemplate></asp:TemplateField>
                   </Columns>
                    <EmptyDataTemplate>
                        <asp:Label ID="lblAvisos" runat="server" Font-Bold="True" ForeColor="Red" 
                                Text="No hay Avisos Actuales" />
                    </EmptyDataTemplate>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle HorizontalAlign="Left" />
                </asp:GridView>
            <br />
        </div>
        
        <div class="clear"></div>
    </div>
    </div>
</asp:Content>
