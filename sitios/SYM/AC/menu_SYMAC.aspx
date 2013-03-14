<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/SYM/AC/SYMAC.Master"
    AutoEventWireup="false" 
    CodeBehind="menu_SYMAC.aspx.vb" 
    Inherits="procomlcd.menu_SYMAC"
    title= "SYM Anaquel y Catalogación" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSYMAC" runat="Server">
  
     
<!--POSTER PHOTO-->
    <div id="poster-photo-container">
        <div id="poster-photo-image"></div>
    </div>
    <!--

CONTENT CONTAINER

-->
    <div id="contenido-columna-derecha">
        <!--

CONTENT MAIN COLUMN

-->
       <div id="content-main-two-column">
            <div id="three-column-container2">
                <div id="three-column-side1B">
                    <a href="Captura/CapturasSYMAC.aspx">
                        <img src="../../../Img/Menu_Captura.jpg" class="photo-border" alt="Enter Alt Text Here" /></a>
                    <h2><a href="captura/CapturasSYMAC.aspx">Captura</a></h2>
                    <p>Captura la información del levantamiento en tiendas.</p></div>
                <div id="three-column-side2B">
                    <a href="Documentos/DocumentosSYMAC.aspx">
                        <img src="../../../Img/Menu_Documentos.jpg" class="photo-border" alt="Enter Alt Text Here" /></a>
                    <h2><a href="Documentos/DocumentosSYMAC.aspx">Documentos</a></h2>
                    <p>Cartas y documentos para bajar.</p></div>
                <div id="three-column-middleB">
                    <a href="Reportes/ReportesSYMAC.aspx">
                        <img src="../../../Img/Menu_Reportes.jpg" class="photo-border" alt="Enter Alt Text Here" /></a>
                    <h2><a href="Reportes/ReportesSYMAC.aspx">Reportes</a></h2>
                    <p>Consulta reportes, bitacora de capturas, graficas, etc... de capturas de 
                        periodos actuales o anteriores.</p></div>
            </div>
        </div>
        <!--

CONTENT SIDE COLUMN AVISOS

-->
        <div id="content-side-two-column">
           <h3>Avisos</h3>
                    <asp:GridView ID="gridAvisos" runat="server" AutoGenerateColumns="False" 
                        GridLines="None" Height="16px" ShowHeader="False" Width="171px" 
                        CellPadding="3" style="text-align: center">
                        <RowStyle ForeColor="#000066" />
                       <Columns>
                           <asp:TemplateField HeaderText="TIENDA" >
                            <ItemTemplate>
                            <ul class="list-of-links">
                                <a href='Avisos/AvisosSYMAC.aspx?ID=<%#Eval("id_aviso")%>'><%#Eval("nombre_aviso")%></a>
                            </ul>
                            </ItemTemplate></asp:TemplateField>
                       </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="lblAvisos" runat="server" Font-Bold="True" ForeColor="Red" 
                                    Text="No hay Avisos Actuales"></asp:Label>
                        </EmptyDataTemplate>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                    
                    <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
                    
                    <asp:HyperLink ID="lnkAdmin" runat="server" 
                NavigateUrl="Avisos/AdminAvisosSYMAC.aspx">Administración avisos</asp:HyperLink>
        </div>
        <div class="clear">
        
        </div>
        
    </div>
</asp:Content>