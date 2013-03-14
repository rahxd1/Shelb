<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Shelby/Shelby.Master"
    AutoEventWireup="false" 
    CodeBehind="ReportesShelby.aspx.vb" 
    Inherits="procomlcd.ReportesShelby"
    title="Shelby - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoShelby" runat="Server">

<!--
titulo-pagina
-->
        <div id="titulo-pagina">
            Reportes</div>
<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:GridView ID="grid" runat="server" AutoGenerateColumns ="False" 
                Width="100%" GridLines="Horizontal" ShowHeader="False" BorderStyle="None">
                <Columns>
                     <asp:TemplateField>
                        <ItemTemplate>
                        <a href="<%#Eval("ruta") %>"><%#Eval("Reporte")%></a>
                        </ItemTemplate>
                        <ItemStyle />
                     </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <h1>Sin información</h1>
                </EmptyDataTemplate>
              
            </asp:GridView>
                        <br />
            
        </div>
        <!-- END MAIN COLUMN -->
        <!--

  CONTENT SIDE 2 COLUMN

  -->
        <div id="content-side2-three-column"></div>
      
        <div class="clear"></div>
    </div>
</asp:Content>