<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/MARS/Mayoreo/MayoreoMars.Master"
    AutoEventWireup="false" 
    CodeBehind="MenuMayoreo.aspx.vb" 
    Inherits="procomlcd.MenuMayoreo"
    title="Mars Mayoreo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoMayoreoMars" runat="Server">
     
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
                <a href="Captura/CapturasMayoreoMars.aspx">
                        <img src="../../../Img/Menu_Captura.jpg" class="photo-border" alt="Enter Alt Text Here" /></a>
                       <h2><a href="Captura/CapturasMayoreoMars.aspx">Captura</a></h2>
                    <p>Accedes al Formato de Captura de tu levantamiento en sistema; con la informacion de tus tiendas.
                        
                        </p></div>
                        
                <div id="three-column-middle">
                    <a href="Reportes/ReportesMayoreoMars.aspx">
                        <img src="../../../Img/Menu_Reportes.jpg" class="photo-border" alt="Enter Alt Text Here" /></a>
                    <h2><a href="Reportes/ReportesMayoreoMars.aspx">Reportes</a></h2>
                    <p>
                        Consulta reportes, bitacora de capturas, graficas, etc... de capturas de 
                        periodos actuales o anteriores.</p></div>
            </div>
            </div>
        <!--

CONTENT SIDE COLUMN AVISOS

-->
        <div id="content-side-two-column">
            <h2>
               Calendario Mars</h2>
               
                   <h2><asp:Label ID="lblPeriodo" runat="server" Text="" 
                       style="font-weight: 700; text-align: center; color: #006699; "></asp:Label></h2>
                    <asp:GridView ID="gridCalendario" runat="server" AutoGenerateColumns="False" 
                        Height="16px" Width="165px" 
                        CellPadding="3" BackColor="White" 
                        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                GridLines="None" ShowFooter="True">
                        <RowStyle ForeColor="Black" BackColor="#EEEEEE" />
                           <Columns>
                             <asp:BoundField HeaderText="" DataField="quincena"/> 
                             <asp:BoundField HeaderText="D" DataField="Dia_1"/> 
                             <asp:BoundField HeaderText="L" DataField="Dia_2"/> 
                             <asp:BoundField HeaderText="M" DataField="Dia_3"/> 
                             <asp:BoundField HeaderText="M" DataField="Dia_4"/> 
                             <asp:BoundField HeaderText="J" DataField="Dia_5"/> 
                             <asp:BoundField HeaderText="V" DataField="Dia_6"/> 
                             <asp:BoundField HeaderText="S" DataField="Dia_7"/> 
                           </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                    </asp:GridView>
        </div>
        
        <div class="clear">
        
        </div>
        
    </div>
    </div>
    </div>
</asp:Content>