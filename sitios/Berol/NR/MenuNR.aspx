<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Berol/NR/Rubbermaid.Master"
    AutoEventWireup="false" 
    CodeBehind="MenuNR.aspx.vb" 
    Inherits="procomlcd.MenuNR"
    TITLE="Newell Rubbermaid" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoNR" runat="Server">
  
     
    <!--

POSTER PHOTO

-->
    <div id="poster-photo-container">
    
    <div id="poster-photo-image"></div>
         
          <div id="feature-area-home">Bienvenid@ a la Plataforma Berol - Newell Rubbermaid</div>
          </div>
    <!--

CONTENT CONTAINER

-->
    <div id="contenido-columna-derecha">
        <!--

CONTENT MAIN COLUMN

-->
        <div id="content-main-two-column">
            <p>
              <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0" width="662" height="380" title="Intro Newell Rubbermaid">
                <param name="movie" value="../Img/Viedo_NR.swf" />
                <param name="quality" value="high" />
                <param name="wmode" value="opaque" />
                <embed src="../Img/Viedo_NR.swf" quality="high" wmode="opaque" pluginspage="http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash" type="application/x-shockwave-flash" width="662" height="380"></embed>
              </object>
            </p>
            <div id="three-column-container">
              <div id="three-column-side1">
                    <h2>
                        <a href="Captura/CapturasNR.aspx">Captura</a></h2>
                    <p>
                        Captura la información del levantamiento en tiendas.</p></div>
                <div id="three-column-side2">
                    <h2>
                        <a href="Documentos/DocumentosNR.aspx">Documentos y videos</a></h2>
                    <p>
                        Cartas y documentos para bajar.</p></div>
                <div id="three-column-middle">
                    <h2>
                        <a href="Reportes/ReportesNR.aspx">Reportes</a></h2>
                    <p>
                        Consulta reportes, bitácora de capturas, graficas, etc., de capturas de 
                        periodos actuales o anteriores.</p></div>
            </div>
        </div>
        <!--

CONTENT SIDE COLUMN AVISOS

-->
 

        <div id="content-side-two-column">
           <h3>Avisos</h3>
            <p>
                    <asp:GridView ID="gridAvisos" runat="server" AutoGenerateColumns="False" 
                        GridLines="None" Height="16px" ShowHeader="False" Width="125px" 
                        CellPadding="3" style="text-align: center">
                        <RowStyle ForeColor="#000066" />
                       <Columns>
                           <asp:TemplateField HeaderText="TIENDA" >
                            <ItemTemplate>
                            <ul class="list-of-links">
                                <a href="Avisos/AvisosNR.aspx?ID=<%#Eval("id_aviso")%>"><%#Eval("nombre_aviso")%></a>
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
                    <br />
                    <br />
                    <br />
            <h3>Video</h3> 
            <p>
         
            <asp:HyperLink ID="HyperLink6" runat="server" 
                NavigateUrl="~/ARCHIVOS/CLIENTES/BEROL/NR/VIDEOS/exhBA.wmv">Instructivo de armado de exhibidores Bodega Aurrera</asp:HyperLink>
                   </p>
                   
              
              
              
              <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <b>
                   
              
              
            <b><asp:HyperLink ID="lnkAdmin" runat="server" 
                NavigateUrl="Avisos/AdminAvisoNR.aspx">Administración avisos</asp:HyperLink></b>     
        </div>
        
        <div class="clear">
        
        </div>
        
    </div>
    </b>
</asp:Content>