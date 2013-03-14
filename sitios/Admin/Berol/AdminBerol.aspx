<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Admin/AdminMaster.Master"
    AutoEventWireup="false" 
    CodeBehind="AdminBerol.aspx.vb" 
    Inherits="procomlcd.AdminBerol"
    title="Administración Berol" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">
     
<!--TITLE-->
    <div id="titulo-pagina">
        Administración Berol</div>
        
    <!--CONTENT CONTAINER-->
    <div id="contenido-derecha">
    
        <!--CONTENT MAIN COLUMN-->
        <div id="contenido-dos-columnas-1">
       
        </div>
        <!--CONTENT SIDE COLUMN AVISOS-->
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
        
        <div class="clear">
        
        </div>
        
    </div>
</asp:Content>