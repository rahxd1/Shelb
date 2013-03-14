<%@ Page Language="vb" 
MASTERPAGEFILE="~/sitios/SYM/Demos2013/SYMDemos2013.Master"
AutoEventWireup="false" 
CodeBehind="ReportesSYMDemos2013.aspx.vb" 
Inherits="procomlcd.ReportesSYMDemos2013" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Contenido_SYMDemos2013" runat="Server">
  <!--
titulo-pagina
-->
        <div id="titulo-pagina">
            Reportes SYM Demos 2013</div>
    <!--
CONTENT CONTAINER
-->
    <div id="contenido-columna-derecha">
        <!--
CONTENT MAIN COLUMN
-->
        <div id="content-main-two-column">
            <asp:GridView ID="gridReportes" runat="server" AutoGenerateColumns ="False" 
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
            
        </div>
        <!-- END MAIN COLUMN -->
        <!--

  CONTENT SIDE 2 COLUMN

  -->
    <div id="content-side2-three-column">
            
        </div>
      
        <div class="clear">
        </div>
    </div>
 
</asp:Content>