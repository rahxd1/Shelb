<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Admin/AdminMaster.Master"
    AutoEventWireup="false" 
    CodeBehind="AdminClientes.aspx.vb" 
    Inherits="procomlcd.AdminClientes"
    Title="Administración General"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">
     
<!--PAGE TITLE-->
<div id="titulo-pagina">Alta y edición de clientes</div>

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
                    
                    <asp:Panel ID="pnlConsulta" runat="server" Visible="False">
                        <asp:GridView ID="gridClientes" runat="server" AutoGenerateColumns="False" 
                            Width="100%" CssClass="grid-view" ShowFooter="True" GridLines="None">
                            <Columns>       
                                <asp:CommandField ButtonType="Image" EditImageUrl="~/Img/Editar.png" 
                                    ShowEditButton="True" />
                                <asp:BoundField HeaderText="ID Cliente" DataField="id_cliente"/>
                                <asp:BoundField HeaderText="Cliente" DataField="nombre_cliente"/>   
                                <asp:BoundField HeaderText="Giro" DataField="giro" />     
                            </Columns>
                            <FooterStyle CssClass="grid-footer" />
                        </asp:GridView>
                    </asp:Panel>
            
                    <asp:Panel ID="pnlNuevo" runat="server" Visible="False" Width="100%" 
                        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                        <asp:Label ID="IDCliente" runat="server" Font-Bold="True" Visible="False" />
                        <br />
                                    
                        <asp:Label ID="lblNombreCliente" runat="server" Text="Cliente" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="caja-texto-caja" 
                            MaxLength="30" Width="249px" />
                        <asp:RequiredFieldValidator ID="rfvNombreCliente" runat="server" 
                            ControlToValidate="txtNombreCliente" ErrorMessage="Nombre del cliente" 
                            ValidationGroup="Cliente" CssClass="caja-texto-rf">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblGiroComercial" runat="server" Text="Giro comercial" CssClass="caja-texto-lbl" />    
                        <asp:TextBox ID="txtGiro" runat="server" CssClass="caja-texto-caja" 
                            MaxLength="30" Width="249px" />
                        <asp:RequiredFieldValidator ID="rfvGiro" runat="server" CssClass="caja-texto-rf"
                            ControlToValidate="txtGiro" ErrorMessage="Giro comercial" ValidationGroup="Cliente">*</asp:RequiredFieldValidator>    
                        <br />
                            
                        <asp:Label ID="lblAdmin" runat="server" Text="Archivo *ASP" CssClass="caja-texto-lbl" />        
                        <asp:TextBox ID="txtAdmin" runat="server" CssClass="caja-texto-caja" 
                            MaxLength="30" Width="248px" />
                        <asp:RequiredFieldValidator ID="rfvAdmin" runat="server" CssClass="caja-texto-rf"
                            ControlToValidate="txtAdmin" ErrorMessage="Archivo ASP" ValidationGroup="Cliente">*</asp:RequiredFieldValidator>    
                        <br />
                            
                        <asp:Label ID="lblActivo" runat="server" Text="Activo" CssClass="caja-texto-lbl" />            
                        <asp:DropDownList ID="cmbActivo" runat="server" CssClass="caja-texto-caja">
                            <asp:ListItem Value="1">Si</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvActivo" runat="server" CssClass="caja-texto-rf"
                            ControlToValidate="cmbActivo" ErrorMessage="Activo" ValidationGroup="Cliente">*</asp:RequiredFieldValidator>    
                        <br />
                        
                        <asp:ValidationSummary ID="vsClientes" runat="server" CssClass="aviso"
                            HeaderText="Completa los siguientes campos:" ShowMessageBox="True" 
                            style="text-align: center" ValidationGroup="Cliente" />  
                        <br />
                                                 
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="button" 
                            ValidationGroup="Cliente" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="button" />               
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
                <li><a href="AdminClientes.aspx">Administración Clientesyectos</a></li>
                <li><a href="Proyectos.aspx">Administración Proyectos</a></li>
             </ul>
        </div>
        
        <div class="clear"></div>
    </div>
</asp:Content>

