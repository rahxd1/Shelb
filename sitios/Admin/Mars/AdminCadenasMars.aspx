<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Admin/AdminMaster.Master"
    AutoEventWireup="false" 
    CodeBehind="AdminCadenasMars.aspx.vb" 
    Inherits="procomlcd.AdminCadenasMars"
    Title="Administración Mars"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">
     
<!--PAGE TITLE-->
<div id="titulo-pagina">Cadenas Mars</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-derecha">

<!--CONTENT MAIN COLUMN-->
        <div id="contenido-dos-columnas-1">  
            <asp:ScriptManager ID="ScriptManager" runat="server"/>
            
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

                    <asp:Panel ID="pnlConsulta" runat="server"  ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridCadena" runat="server" AutoGenerateColumns="False" 
                            GridLines="None" Width="100%" CssClass="grid-view" ShowFooter="True">
                            <Columns>       
                                <asp:CommandField ButtonType="Image" EditImageUrl="~/Img/Editar.png" 
                                    ShowEditButton="True" />
                                <asp:BoundField HeaderText="ID Cadena" DataField="id_cadena"/>
                                <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena"/>        
                                <asp:BoundField HeaderText="Tipo Cadena" DataField="nombre_tipocadena"/> 
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
                            ControlToValidate="txtNombreCadena" ErrorMessage="Nombre cadena" ValidationGroup="Cadena">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblTipoCadena" runat="server" Text="Tipo cadena" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbTipoCadena" runat="server" AutoPostBack="True" 
                            CssClass="caja-texto-caja" />
                        <asp:RequiredFieldValidator ID="rfvTipoCadena" runat="server" 
                            ControlToValidate="cmbTipoCadena" ErrorMessage="Tipo cadena" ValidationGroup="Cadena">*</asp:RequiredFieldValidator>
                        <br />
                        
                         <asp:ValidationSummary ID="vsCadena" runat="server" CssClass="aviso"
                            HeaderText="Completa los siguientes campos:" ShowMessageBox="True" 
                            ValidationGroup="Cadena" />  
                        <br />
                        
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="button" 
                            ValidationGroup="Cadena"/>
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
        
 <!--CONTENT SIDE COLUMN AVISOS -->
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
</asp:Content>
