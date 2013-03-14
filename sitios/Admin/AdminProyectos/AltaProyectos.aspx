<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Admin/AdminMaster.Master"
    AutoEventWireup="false" 
    CodeBehind="AltaProyectos.aspx.vb" 
    Inherits="procomlcd.AltaProyectos"
    Title="Administración"  %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">
     
<!--PAGE TITLE-->
<div id="titulo-pagina">Alta y edición de proyectos</div>

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
                        <asp:GridView ID="gridProyectos" runat="server" AutoGenerateColumns="False" 
                            Width="100%" CssClass="grid-view" ShowFooter="True" GridLines="None">
                            <Columns>       
                                <asp:CommandField ButtonType="Image" EditImageUrl="~/Img/Editar.png" 
                                    ShowEditButton="True" />
                                <asp:BoundField HeaderText="ID Proyecto" DataField="id_proyecto"/>
                                <asp:BoundField HeaderText="Cliente" DataField="nombre_cliente"/>   
                                <asp:BoundField HeaderText="Proyecto" DataField="nombre_proyecto" />     
                            </Columns>
                            <FooterStyle CssClass="grid-footer" />
                        </asp:GridView>
                    </asp:Panel>
            
                    <asp:Panel ID="pnlNuevo" runat="server" Visible="False" Width="100%">
                        <asp:Label ID="lblIDProyecto" runat="server" Text="ID Proyecto" CssClass="caja-texto-lbl" /> 
                        <asp:TextBox ID="txtIDProyecto" runat="server" CssClass="caja-texto-caja" />
                        <asp:RequiredFieldValidator ID="rfvIDProyecto" runat="server" 
                            ControlToValidate="txtIDProyecto" ErrorMessage="ID del proyecto" 
                            ValidationGroup="Proyectos" CssClass="caja-texto-rf">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblNombreProyecto" runat="server" Text="Nombre proyecto" CssClass="caja-texto-lbl" /> 
                        <asp:TextBox ID="txtNombreProyecto" runat="server" CssClass="caja-texto-caja" MaxLength="30" />
                        <asp:RequiredFieldValidator ID="rfvNombreProyecto" runat="server" 
                            ControlToValidate="txtNombreProyecto" ErrorMessage="Nombre del proyecto" 
                            ValidationGroup="Proyectos" CssClass="caja-texto-rf">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblCliente" runat="server" Text="Nombre cliente" CssClass="caja-texto-lbl" /> 
                        <asp:DropDownList ID="cmbCliente" runat="server" AutoPostBack="True" CssClass="caja-texto-caja" />
                        <asp:RequiredFieldValidator ID="rfvCliente" runat="server" 
                            ControlToValidate="cmbCliente" ErrorMessage="Nombre del cliente" 
                            ValidationGroup="Proyectos" CssClass="caja-texto-rf">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:ValidationSummary ID="vsProyectos" runat="server" CssClass="aviso"
                            HeaderText="Completa los siguientes campos:" ShowMessageBox="True" 
                            style="text-align: center" ValidationGroup="Proyectos" />  
                        <br />
                                               
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="button" 
                            ValidationGroup="Proyectos" />
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
            <ul class="list-of-links">
                <li><a href="AdminClientes.aspx">Administración Clientes Clientes</a></li>
                <li><a href="AltaProyectos.aspx">Alta proyectos</a></li>
                <li><a href="Proyectos.aspx">Administración Proyectos</a></li>
             </ul>
        </div>
        
        <div class="clear"></div>
    </div>
</asp:Content>
