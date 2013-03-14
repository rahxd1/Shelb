<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Admin/AdminMaster.Master"
    AutoEventWireup="false" 
    CodeBehind="AdminFotografiasNR.aspx.vb" 
    Inherits="procomlcd.AdminFotgrafiasNR"
    Title="Administración Newell Rubbermaid" %>

<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">
  
<!--titulo-pagina -->
    <div id="titulo-pagina">
       Periodos promotor Newell Rubbermaid</div>    

<!--CONTENT CONTAINER-->
    <div id="contenido-derecha">

<!--CONTENT MAIN COLUMN-->
        <div id="contenido-dos-columnas-1">          
            <asp:ScriptManager ID="ScriptManager1" runat="server"/>
           
            <asp:UpdatePanel ID="updPnl" runat="server">
                <ContentTemplate>
                    <div ID="menu-edicion">
                        <ul>
                            <li><asp:LinkButton ID="lnkNuevo" runat="server" Font-Bold="False">Nuevo</asp:LinkButton></li>
                            <li><asp:LinkButton ID="lnkConsultas" runat="server" Font-Bold="False">Consultas</asp:LinkButton></li>
                        </ul>
                    </div>
            
                    <div id="div-aviso">        
                        <asp:Label ID="lblAviso" runat="server" />
                    </div>
                    
                    <asp:Panel ID="pnlConsulta" runat="server"  ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridPeriodo" runat="server" AutoGenerateColumns="False" 
                            Width="100%" CssClass="grid-view" ShowFooter="True" GridLines="None">
                            <Columns>       
                                <asp:CommandField ButtonType="Image" EditImageUrl="~/Img/Editar.png" 
                                    ShowEditButton="True" />
                                <asp:BoundField DataField="folio_foto" HeaderText="ID Perido" />
                                <asp:BoundField DataField="id_ubicacion" HeaderText="Ubicacion" />
                            </Columns>
                            <FooterStyle CssClass="grid-footer" />
                        </asp:GridView>
                    </asp:Panel>
                    
                    <asp:Panel ID="pnlNuevo" runat="server" Visible="False" Width="100%" >
                        <asp:Label ID="lblIDPeriodo" runat="server" Text="" CssClass="caja-texto-lbl" />
                        <br />
                        
                        <asp:Label ID="lblFolio" runat="server" Text="" CssClass="caja-texto-lbl" />
                        <br />
                        
                        <asp:Label ID="lblFamilia" runat="server" Text="Familia" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbFamilia" runat="server" CssClass="caja-texto-caja" />
                        <br />
                        
                        <asp:Label ID="lblUbicacion" runat="server" Text="Familia" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbUbicacion" runat="server" CssClass="caja-texto-caja" />
                        <br />
                        
                        <asp:Image ID="Image2" runat="server" Height="500px" /><br />

                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="button" />
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
            <h2>Generales</h2>
            <ul class="list-of-links">
                <li><a href="/sitios/Admin/Herradura/AdminUsuariosBerol.aspx">Usuarios</a></li>
                <li><a href="/sitios/Admin/Herradura/AdminCadenasBerol.aspx">Cadenas</a></li>
                <li><a href="/sitios/Admin/Herradura/AdminRegionesBerol.aspx">Regiones</a></li>
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
