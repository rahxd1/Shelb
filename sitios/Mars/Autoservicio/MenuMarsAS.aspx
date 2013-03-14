<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Mars/Autoservicio/Mars_Autoservicio.Master"
    AutoEventWireup="false" 
    CodeBehind="MenuMarsAS.aspx.vb" 
    Inherits="procomlcd.MenuMarsAS"
    title="Mars Autoservicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMarsAutoservicio" runat="Server">
     
    <!--

POSTER PHOTO

-->
    <div id="poster-photo-container">
    
    <div id="poster-photo-image">
    </div>
         
          </div>
    <!--

CONTENT CONTAINER

-->
    <div id="contenido-columna-derecha">
        <!--

CONTENT MAIN COLUMN

-->
        <asp:Panel ID="Panel2" runat="server">
            <div id="content-main-two-column">

                <div id="three-column-container">
                  <div id="three-column-side1">
                    <a href="Captura/CapturasASMars.aspx">
                            <img src="../../../Img/Menu_Captura.jpg" class="photo-border" alt="Enter Alt Text Here" /></a>
                           <h2><a href="Captura/CapturasASMars.aspx">Capturas</a></h2>
                  </div>
                            
                <div id="three-column-side2">
                    <a href="Reportes/ReportesMarsAS.aspx">
                        <img src="../../../Img/Menu_Capacitacion.jpg" class="photo-border" alt="Enter Alt Text Here" /></a>
                    <h2><a href="../Capacitacion/MenuCapacitacion.aspx">Capacitación</a></div>
                  
                <div id="three-column-middle">
                    <a href="Reportes/ReportesMarsAS.aspx">
                        <img src="../../../Img/Menu_Reportes.jpg" class="photo-border" alt="Enter Alt Text Here" /></a>
                    <h2><a href="Reportes/ReportesMarsAS.aspx">Reportes</a></h2>
                </div>
        </asp:Panel>
            
             <!-- celda animacion flash-->
             
                <asp:Panel ID="Panel1" runat="server" Visible="false">
                <object classid="&#8221;clsid:D27CDB6E-AE6D-11cf-96B8-444553540004&#8243;" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0"
                            title="MARS" style="width: 600px; height: 400px">
                            <param name="movie" value="Img/pedigree.swf" />
                            <param name="quality" value="high" />
                            <param name="wmode" value="opaque" />
                            <embed src="Img/pedigree.swf" quality="high" wmode="opaque" pluginspage="http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash"
                                type="application/x-shockwave-flash" width="600" height="400"></embed>
                        </object>
                </asp:Panel>
                
                <div style="background-color:Yellow; padding-left:0%; width: 593px;" >
                <asp:Button ID="Button1" runat="server" Text="Video   --Exhibidor Pedigree B---" Width="593px" />
                <asp:Button ID="Button2" runat="server" Text="Cerrar  --Exhibidor Pedigree B---" Width="594px" 
                        Visible="false" />
                </div>
             
             
                
            
                
                        
            
            </div>
            </div>
        <!--

CONTENT SIDE COLUMN AVISOS

-->
    <asp:Panel ID="Panel3" runat="server">
  

        <div id="content-side-two-column">
            <h2>
               Calendario Mars</h2>
               
                   <h2><asp:Label ID="lblPeriodo" runat="server" Text="" 
                       style="font-weight: 700; text-align: center; color: #006699; "></asp:Label></h2>
                    <asp:GridView ID="gridCalendario" runat="server" AutoGenerateColumns="False" 
                        Height="16px" Width="165px" BackColor="White" 
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                GridLines="None" ShowFooter="True">
                           <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
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
            
            <h2>
               Avisos</h2>
                    <asp:GridView ID="gridAvisos" runat="server" AutoGenerateColumns="False" 
                        GridLines="None" Height="16px" ShowHeader="False" Width="165px" 
                        CellPadding="3" style="text-align: center">
                        <RowStyle ForeColor="#000066" />
                       <Columns>
                         <asp:TemplateField HeaderText="" >
                           <ItemTemplate>
                            <ul class="list-of-links">
                                <li>
                                <b><a href="javascript:;" onclick="window.open('AvisoMars.aspx?id=<%#Eval("id_aviso")%>','window','width=400,height=300,left=300,top=300, scrollbars=YES')">
                                <%#Eval("nombre_aviso")%></a></b>
                                <%--<br />Fecha fin: <i><%#DataBinder.Eval(Container.DataItem, "fecha_fin", "{0:dd-MMM-yyyy}")%></i>--%>
                                </li>
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
               
<%--              <h2>Documentos y videos</h2>
              <asp:GridView ID="gridOtros" runat="server" AutoGenerateColumns="False" 
                        GridLines="None" Height="16px" ShowHeader="False" Width="165px" 
                        CellPadding="3" style="text-align: center">
                        <RowStyle ForeColor="#000066" />
                       <Columns>
                         <asp:TemplateField HeaderText="" >
                           <ItemTemplate>
                            <ul class="list-of-links">
                                <li>
                                <b><asp:HyperLink ID="HyperLink1" runat="server" 
                                NavigateUrl='<%#Eval("ruta_aviso")%>'><%#Eval("nombre_aviso")%></asp:HyperLink></b>
                                </li>
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
                --%>
</div>
          </asp:Panel>
        <div class="clear">
        
        </div>
        
    </div>
    </a>
</asp:Content>