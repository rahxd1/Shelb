﻿<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Admin/AdminMaster.Master" 
    AutoEventWireup="false" 
    CodeBehind="CrearEventosMarsAS.aspx.vb" 
    Inherits="procomlcd.CrearEventosMarsAS"
    Title="Administración Mars Autoservicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">
  
<!--titulo-pagina-->
    <div id="titulo-pagina">
        Eventos rutas de promotores Mars Autoservicio</div>
        
<!--CONTENT CONTAINER-->
    <div id="contenido-derecha">

<!--CONTENT MAIN COLUMN-->
        <div id="contenido-dos-columnas-1">
            <asp:ScriptManager ID="ScriptManager" runat="server"/>
            
            <asp:UpdatePanel ID="updPnl" runat="server">
                <ContentTemplate>     
                    <div id="div-aviso">        
                        <asp:Label ID="lblAviso" runat="server" />
                        <asp:Label ID="lblAviso2" runat="server" />
                    </div>
                    
                    <asp:Label ID="lblPeriodo" runat="server" Text="Periodo" CssClass="caja-texto-lbl" />
                    <asp:DropDownList ID="cmbPeriodo" runat="server" CssClass="caja-texto-caja" />
                    <asp:RequiredFieldValidator ID="rfvPeriodo" runat="server" CssClass="caja-texto-rf" 
                        ControlToValidate="cmbPeriodo" ErrorMessage="* Selecciona periodo" 
                        ValidationGroup="Eventos" />
                    <br />
                    
                    <asp:Label ID="lblQuincena" runat="server" Text="Quincena" CssClass="caja-texto-lbl" />
                    <asp:DropDownList ID="cmbQuincena" runat="server" CssClass="caja-texto-caja" />
                    <asp:RequiredFieldValidator ID="rfvQuincena" runat="server" CssClass="caja-texto-rf" 
                        ControlToValidate="cmbQuincena" ErrorMessage="* Selecciona quincena" 
                        ValidationGroup="Eventos" />
                    <br />
                                        
                    <asp:Label ID="lblPromotor" runat="server" Text="Promotor" CssClass="caja-texto-lbl" />
                    <asp:DropDownList ID="cmbUsuario" runat="server" CssClass="caja-texto-caja" />
                    <br />
                    
                    <asp:Button ID="btnCargar" runat="server" Text="Cargar" CssClass="button" 
                        ValidationGroup="Eventos" />

                    <asp:GridView ID="gridRuta" runat="server" AutoGenerateColumns="False" 
                        GridLines="None" Width="100%" 
                        Visible="False" CssClass="grid-view" ShowFooter="True">
                        <Columns>
                            <asp:BoundField HeaderText="Promotor" DataField="id_usuario"/> 
                            <asp:BoundField HeaderText="Tienda" DataField="id_tienda"/>  
                        </Columns>
                        <FooterStyle CssClass="grid-footer" />
                    </asp:GridView>
                    
                    <asp:GridView ID="gridRutaPrecios" runat="server" AutoGenerateColumns="False" 
                        CssClass="grid-view" Visible="False" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="id_usuario" HeaderText="Promotor" />
                            <asp:BoundField DataField="id_tienda" HeaderText="Tienda" />
                        </Columns>
                        <FooterStyle CssClass="grid-footer" />
                    </asp:GridView>
                    
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
        
        <div class="clear">
        </div>
    </div>
</asp:Content>