<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Jovy/Jovy.Master"
    AutoEventWireup="false" 
    CodeBehind="Menu_Jovy.aspx.vb" 
    Inherits="procomlcd.Menu_Jovy"
    Title="Jovy" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderJovy" runat="Server">
  
     
    <!--

POSTER PHOTO

-->
    <div id="poster-photo-container">
    
    <div id="poster-photo-image"></div>
         
          <div id="feature-area-home">Bienvenido a la Plataforma Jovy</div>
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
                <a href="captura/RutasJovy.aspx">
                        <img src="../../Img/Menu_Captura.jpg" class="photo-border" alt="Enter Alt Text Here" /></a>
                    <h2><a href="Captura/RutasJovy.aspx">Captura</a></h2>
                    <p>Captura la información del levantamiento en tiendas.</p></div>
                <div id="three-column-side2">
                    <a href="IndicadoresJovy.aspx">
                        <img src="../../Img/Menu_Indicadores.jpg" class="photo-border" alt="Enter Alt Text Here" /></a>
                    <h2><a href="IndicadoresJovy.aspx">Indicadores</a></h2>
                    <p>Reportes y resumenes de avances.</p></div>
                <div id="three-column-middle">
                    <a href="Reportes/ReportesJovy.aspx">
                        <img src="../../Img/Menu_Reportes.jpg" class="photo-border" alt="Enter Alt Text Here" /></a>
                    <h2><a href="Reportes/ReportesJovy.aspx">Reportes</a></h2>
                    <p>
                        Consulta reportes, bitacora de capturas, graficas, etc... de capturas de 
                        periodos actuales o anteriores.</p></div>
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
                        GridLines="None" Height="16px" ShowHeader="False" Width="125px" 
                        CellPadding="3" style="text-align: center">
                        <RowStyle ForeColor="#000066" />
                       <Columns>
                           <asp:TemplateField HeaderText="TIENDA" >
                            <ItemTemplate>
                            <ul class="list-of-links">
                                <a href="Avisos/AvisosJovy.aspx"><%#Eval("nombre_aviso")%></a>
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
        </div>
        
        <div class="clear">
        
        </div>
        
    </div>
</asp:Content>