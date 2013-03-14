<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Admin/AdminMaster.Master"
    AutoEventWireup="false" 
    CodeBehind="Proyectos.aspx.vb" 
    Inherits="procomlcd.Clientes"
    Title="Administración" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">
  
<!--titulo-pagina-->
<div id="titulo-pagina">
        Administración clientes</div>
        
<!--CONTENT CONTAINER-->
 <div id="contenido-derecha">
        <!--CONTENT MAIN COLUMN-->
        <div id="contenido-dos-columnas-1">
             <asp:GridView ID="gridAccesos" runat="server" AutoGenerateColumns ="False" 
                 Width="60%" CssClass="grid-view-lk" 
                 ShowFooter="True"  ShowHeader="False">
                 <Columns>
                     <asp:TemplateField HeaderText="Clientes" >
                         <ItemTemplate>
                             <asp:HyperLink ID="Link" runat="server"
                                    NavigateUrl='<%# Eval("admin", "/sitios/Admin/{0}") %>'>
                                    <%#Eval("nombre_cliente")%>
                                </asp:HyperLink>
                         </ItemTemplate>
                     </asp:TemplateField>
                 </Columns>
             </asp:GridView>

             
        </div>
        
<!--CONTENT SIDE COLUMN AVISOS-->
        <div id="contenido-dos-columnas-2">
            <ul class="list-of-links">
                <li><a href="AdminClientes.aspx">Administración Clientes</a></li>
                <li><a href="AltaProyectos.aspx">Alta proyectos</a></li>
                <li><a href="Proyectos.aspx">Administración Proyectos</a></li>
             </ul>
        </div>
       
        <div class="clear"></div>
    </div>
</asp:Content>
