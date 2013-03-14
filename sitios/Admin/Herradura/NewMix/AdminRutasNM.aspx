<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Admin/AdminMaster.Master" 
    AutoEventWireup="false" 
    CodeBehind="AdminRutasNM.aspx.vb" 
    Inherits="procomlcd.AdminRutasNM"
    Title="Administración New Mix" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">
  
<!--titulo-pagina-->
    <div id="titulo-pagina">
        Rutas de promotores New Mix</div>
        
<!--CONTENT CONTAINER-->
    <div id="contenido-derecha">

<!--CONTENT MAIN COLUMN-->
        <div id="contenido-dos-columnas-1">
            <asp:ScriptManager ID="ScriptManager2" runat="server"/>
            
            <asp:UpdatePanel ID="updPnl" runat="server">
                <ContentTemplate>
                    <div id="div-aviso">        
                        <asp:Label ID="lblAviso" runat="server"></asp:Label>
                    </div>
            
                    <asp:Panel ID="pnlPromotor" runat="server">
                        <asp:Label ID="lblRegion" runat="server" Text="Región" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbRegion" runat="server" AutoPostBack="True" 
                            CssClass="caja-texto-caja" />
                        <br />
                        
                        <asp:Label ID="lblPromotor" runat="server" Text="Promotor" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbPromotor" runat="server" AutoPostBack="True" 
                            CssClass="caja-texto-caja" />
                        <br />
                    </asp:Panel>
                    
                    <asp:Panel ID="pnlRuta" runat="server"  ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridRuta" runat="server" AutoGenerateColumns="False" 
                             GridLines="None" Width="100%" 
                            DataKeyNames="id_tienda" CssClass="grid-view" ShowFooter="True">
                            <Columns>
                                <asp:CommandField ShowDeleteButton="True" ButtonType="Image" 
                                    DeleteImageUrl="~/Img/delete-icon.png" />
                                <asp:BoundField DataField="id_usuario" HeaderText="Promotor" />
                                <asp:BoundField DataField="nombre_cadena" HeaderText="Cadena" />
                                <asp:BoundField DataField="id_tienda" HeaderText="ID Tienda" />
                                <asp:BoundField DataField="nombre" HeaderText="Tienda" />
                            </Columns>
                            <FooterStyle CssClass="grid-footer" />
                        </asp:GridView>
                    </asp:Panel>
                    
                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar Tienda" Width="119px" 
                        CssClass="button" Enabled="False" />
                            
                    <asp:Panel ID="pnlAgregar" runat="server" Visible="False">
                        <asp:Label ID="lblEstado" runat="server" Text="Estado" CssClass="caja-texto-lbl" />
                        <asp:ListBox ID="ListEstado" runat="server" AutoPostBack="True" CssClass="caja-texto-caja" />
                        <br />
                        
                        <asp:Label ID="lblCadena" runat="server" Text="Cadena" CssClass="caja-texto-lbl" />
                        <asp:ListBox ID="ListCadena" runat="server" AutoPostBack="True" CssClass="caja-texto-caja" />
                        <br />
                        
                        <asp:Label ID="lblTienda" runat="server" Text="Tienda" CssClass="caja-texto-lbl" />
                        <asp:ListBox ID="ListTienda" runat="server" CssClass="caja-texto-caja" />
                        <asp:RequiredFieldValidator ID="rfvTienda" runat="server" CssClass="caja-texto-rf"
                            ControlToValidate="ListTienda" ErrorMessage="*Selecciona la tienda" 
                            Font-Bold="True" ValidationGroup="Tienda" />
                        <br />
                        
                        <asp:Button ID="btnAceptar" runat="server" CssClass="button" 
                            Text="Agrega Tienda" ValidationGroup="Tienda"/>
                        <asp:Button ID="btnTerminar" runat="server" CausesValidation="False" 
                            CssClass="button" Text="Terminar"/>
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
                <li><a href="/sitios/Admin/Herradura/AdminUsuariosHerradura.aspx">Usuarios</a></li>
                <li><a href="/sitios/Admin/Herradura/AdminCadenasHerradura.aspx">Cadenas</a></li>
                <li><a href="/sitios/Admin/Herradura/AdminRegionesHerradura.aspx">Regiones</a></li>
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
                                Text="No hay Avisos Actuales"></asp:Label>
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
