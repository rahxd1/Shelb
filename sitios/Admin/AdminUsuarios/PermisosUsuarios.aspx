<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Admin/AdminMaster.Master"
    AutoEventWireup="false" 
    CodeBehind="PermisosUsuarios.aspx.vb" 
    Inherits="procomlcd.PermisosUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">
  
<!--PAGE TITLE-->
<div id="titulo-pagina">Permisos</div>
     
<!--CONTENT CONTAINER-->
 <div id="contenido-derecha">
       
<!--CONTENT MAIN COLUMN-->
        <div id="contenido-dos-columnas-1">
            <asp:UpdatePanel ID="updPnl" runat="server">
                <ContentTemplate>
                
                    <asp:Label ID="lblTipoUsuario" runat="server" Text="Tipo Usuario" CssClass="caja-texto-lbl" />
                    <asp:DropDownList ID="cmbTipoUsuario" runat="server" AutoPostBack="True" 
                            CssClass="caja-texto-caja" />
                    <br />
            
                    <asp:Label ID="lblUsuario" runat="server" Text="Usuario" CssClass="caja-texto-lbl" />
                    <asp:DropDownList ID="cmbUsuario" runat="server" AutoPostBack="True" 
                        CssClass="caja-texto-caja" />
                    <br />
             
                    <asp:Panel ID="pnlPermisos" runat="server" Visible="False">
                     <table width="100%">
                        <tr>
                            <td style="color: #FFFFFF; font-weight: bold; background-color: #000000;" 
                                bgcolor="#999999" colspan="4" width="100%">
                                PERMISOS</td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:CheckBox ID="chkNuevo" runat="server" 
                                    Text="Nuevo" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:CheckBox ID="chkEdicion" runat="server" Text="Edición" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:CheckBox ID="chkEliminacion" runat="server" 
                                    Text="Eliminación" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:CheckBox ID="chkConsultas" runat="server" Text="Consultas" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Región</td>
                            <td colspan="3">
                                <asp:DropDownList ID="cmbRegion" runat="server" AutoPostBack="True" 
                                    CssClass="caja-texto-caja">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td colspan="3">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
                                &nbsp;</td>
                            <td style="text-align: center">
                                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="button" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="button" />
                            </td>
                        </tr>
                        </table>
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
        
        <div class="clear">
        
        </div>
    </div>
    
</asp:Content>
