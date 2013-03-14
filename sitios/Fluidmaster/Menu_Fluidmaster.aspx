<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Fluidmaster/Fluidmaster.Master"
    AutoEventWireup="false" 
    CodeBehind="Menu_Fluidmaster.aspx.vb" 
    Inherits="procomlcd.Menu_Fluidmaster"
    Title="Fluidmaster" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoFluid" runat="Server">
  
     
    <!--

POSTER PHOTO

-->
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

            <div id="three-column-container">
              <div id="three-column-side1">
                <a href="captura/RutasFluid.aspx">
                        <img src="../../Img/Menu_Captura.jpg" class="photo-border" alt="Enter Alt Text Here" /></a>
                    <h2><a href="Captura/RutasFluid.aspx">Captura</a></h2>
                    <p>Captura la información del levantamiento en tiendas.</p></div>
                <div id="three-column-side2">
                    <a href="Reportes/ReportesFluid.aspx">
                        <img src="../../Img/Menu_Reportes.jpg" class="photo-border" alt="Enter Alt Text Here" /></a>
                    <h2><a href="Reportes/ReportesFluid.aspx">Reportes</a></h2>
                    <p>Reportes y resumenes de avances.</p></div>
            </div>
        </div>
        <!--

CONTENT SIDE COLUMN AVISOS

-->
        <div id="content-side-two-column">
            <h3>
               Avisos</h3>
            <p>
                    <asp:GridView ID="gridAvisos" runat="server" AutoGenerateColumns="False" 
                        GridLines="None" Height="16px" ShowHeader="False" Width="159px" 
                        CellPadding="3" style="text-align: center">
                        <RowStyle ForeColor="#000066" />
                       <Columns>
                           <asp:TemplateField HeaderText="TIENDA" >
                            <ItemTemplate>
                            <ul class="list-of-links">
                                <a href='../Fluidmaster/Avisos/AvisosFluid.aspx?ID=<%#Eval("id_aviso")%>'><%#Eval("nombre_aviso")%></a>
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
                    </p>
                    <br />
                    
                     <b><asp:HyperLink ID="lnkAdmin" runat="server" 
                NavigateUrl="Avisos/AdminAvisoFluid.aspx">Administración avisos</asp:HyperLink></b>     
        </div>
        
        <div class="clear">
        
        </div>
        
    </div>
</asp:Content>