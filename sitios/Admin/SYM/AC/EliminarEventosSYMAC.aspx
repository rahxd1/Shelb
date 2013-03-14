<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Admin/AdminMaster.Master" 
    AutoEventWireup="false" 
    CodeBehind="EliminarEventosSYMAC.aspx.vb" 
    Inherits="procomlcd.EliminarEventosSYMAC"
    Title="Administración SYM Anaquel y Catalogación" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">
  
<!--titulo-pagina-->
    <div id="titulo-pagina">
        Eliminar eventos SYM Anaquel y Catalogación</div>
        
<!--CONTENT CONTAINER-->
    <div id="contenido-derecha">

<!--CONTENT MAIN COLUMN-->
        <div id="contenido-dos-columnas-1">
            <asp:ScriptManager ID="ScriptManager" runat="server"/>
            
            <asp:UpdatePanel ID="updPnl" runat="server">
                <ContentTemplate>
                    <div id="div-aviso">        
                        <asp:Label ID="lblAviso" runat="server" />
                    </div>
            
                    <asp:Label ID="lblPeriodo" runat="server" Text="Periodo" CssClass="caja-texto-lbl" />
                    <asp:DropDownList ID="cmbPeriodo" runat="server" 
                        AutoPostBack="True" CssClass="caja-texto-caja" />
                    <br />
                    
                    <asp:Label ID="lblPromotor" runat="server" Text="Promotor" CssClass="caja-texto-lbl" />
                    <asp:DropDownList ID="cmbPromotor" runat="server" 
                        AutoPostBack="True" CssClass="caja-texto-caja" />
                    <br />
                    
                    <asp:Panel ID="pnlGrid" runat="server" CssClass="panel-grid" ScrollBars="Both">
                        <asp:GridView ID="gridRuta" runat="server" AutoGenerateColumns="False" DataKeyNames="id_tienda" 
                            GridLines="None" CssClass="grid-view" ShowFooter="True" Width="100%">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkEliminar" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="id_usuario" HeaderText="Promotor" />
                                <asp:BoundField DataField="nombre" HeaderText="Tienda" />
                                <asp:BoundField DataField="nombre_cadena" HeaderText="Cadena" />
                                <asp:BoundField DataField="estatus_anaquel" HeaderText="Estatus anaquel" />
                                <asp:BoundField DataField="estatus_catalogacion" HeaderText="Estatus catalogacíón" />
                            </Columns>
                            <FooterStyle CssClass="grid-footer" />
                        </asp:GridView>
                    </asp:Panel>
                    
                    <asp:Button ID="btnEliminarTodos" runat="server" CssClass="button" 
                        Enabled="False" Text="Eliminar todos" />
                    <asp:Button ID="btnEliminarSeleccion" runat="server" CssClass="button" 
                        Enabled="False" Text="Eliminar seleccion" />
                
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
                <li><a href="../AdminUsuariosSYM.aspx">Usuarios</a></li>
                <li><a href="../AdminCadenasSYM.aspx">Cadenas</a></li>
                <li><a href="../AdminRegionesSYM.aspx">Regiones</a></li>
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

