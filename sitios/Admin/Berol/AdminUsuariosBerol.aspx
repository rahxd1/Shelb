<%@Page Language="vb"
    Culture="es-MX" 
    masterpagefile="~/sitios/Admin/AdminMaster.Master"
    AutoEventWireup="false" 
    Codebehind="AdminUsuariosBerol.aspx.vb" 
    Inherits="procomlcd.AdminUsuariosBerol"
    Title="Administración Berol"  %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">

<!--titulo-pagina-->
<div id="titulo-pagina">
        Usuarios Berol</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="contenido-dos-columnas-1">
            <asp:ScriptManager ID="ScriptManager1" runat="server"/>
            
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
            
                    <asp:Panel ID="pnlConsulta" runat="server">
                        <asp:Label ID="lblFiltroRegion" runat="server" Text="Región" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbFiltroRegion" runat="server" AutoPostBack="True" 
                            CssClass="caja-texto-caja" />
                        <br />
                        
                        <asp:Label ID="lblFiltroTipoUsuario" runat="server" Text="Tipo usuario" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbFiltroTipoUsuario" runat="server" AutoPostBack="True" 
                            CssClass="caja-texto-caja">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="1">Promotor</asp:ListItem>
                            <asp:ListItem Value="2">Supervisor</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                                    
                        <asp:Label ID="lblFiltroUsuario" runat="server" Text="Usuario" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbFiltroUsuario" runat="server" AutoPostBack="True" 
                            CssClass="caja-texto-caja" />
                        <br />

                        <asp:Panel ID="pnlUsuarios" runat="server" ScrollBars="Both"
                            CssClass="panel-grid">
                            <asp:GridView ID="gridUsuario" runat="server" AutoGenerateColumns="False" 
                                Width="100%" CssClass="grid-view" 
                                ShowFooter="True" GridLines="None">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Img/Editar.png" 
                                        ShowEditButton="True" />
                                    <asp:CommandField ButtonType="Image" 
                                        DeleteImageUrl="~/Img/delete-icon.png" 
                                        ShowDeleteButton="True" />
                                    <asp:BoundField DataField="id_usuario" HeaderText="ID Usuario" />
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                    <asp:BoundField DataField="tipo_usuario" HeaderText="Tipo Usuario" />
                                </Columns>
                                <FooterStyle CssClass="grid-footer" />
                            </asp:GridView>
                        </asp:Panel>
                    </asp:Panel>

                    <asp:Panel ID="pnlNuevo" runat="server" Visible="False">
                        <asp:Label ID="lblIDUsuario" runat="server" Text="ID Usuario" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtIDUsuario" runat="server" CssClass="caja-texto-caja" MaxLength="15" />
                        <asp:RequiredFieldValidator ID="rfvIDUsuario" runat="server" CssClass="caja-texto-rf"
                            ControlToValidate="txtIDUsuario" ErrorMessage="ID usuario" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblNombreUsuario" runat="server" Text="Nombre del usuario" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="caja-texto-caja" MaxLength="70" />
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" CssClass="caja-texto-rf"
                            ControlToValidate="txtNombre" ErrorMessage="Nombre usuario" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblRegion" runat="server" Text="Región" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbRegion" runat="server" CssClass="caja-texto-caja" />
                        <br />
                        
                        <asp:Label ID="lblTipoUsuario" runat="server" Text="Tipo usuario" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbTipoUsuario" runat="server" CssClass="caja-texto-caja" />
                        <asp:RequiredFieldValidator ID="rfvTipo" runat="server"  CssClass="caja-texto-rf"
                            ControlToValidate="cmbTipoUsuario" ErrorMessage="Tipo de usuario" ValidationGroup="Usuario">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblProyecto" runat="server" Text="Proyecto" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbProyecto" runat="server" CssClass="caja-texto-caja">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="1">Promotor</asp:ListItem>
                            <asp:ListItem Value="2">Supervisor</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        
                        <asp:ValidationSummary ID="vsUsuario" runat="server" CssClass="aviso"
                            HeaderText="Completa los siguientes campos:" ShowMessageBox="True" 
                            ValidationGroup="Usuario" />  
                        <br />
                        
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" 
                            ValidationGroup="Usuario" CssClass="button" />
                        <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" 
                            Text="Cancelar" CssClass="button" />
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
            <h2>Generales</h2>
            <ul class="list-of-links">
                <li><a href="/sitios/Admin/Berol/AdminUsuariosBerol.aspx">Usuarios</a></li>
                <li><a href="/sitios/Admin/Berol/AdminCadenasBerol.aspx">Cadenas</a></li>
                <li><a href="/sitios/Admin/Berol/AdminRegionesBerol.aspx">Regiones</a></li>
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
        
        <div class="clear"/>
    </div>
</asp:Content>
