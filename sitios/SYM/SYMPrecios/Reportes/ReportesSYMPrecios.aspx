<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/SYM/SYMPrecios/SYMPrecios.Master"
    AutoEventWireup="false" 
    CodeBehind="ReportesSYMPrecios.aspx.vb" 
    Inherits="procomlcd.ReportesSYMPrecios"
    title="SYM Precios - Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1SYMPrecios" runat="Server">

<!--
titulo-pagina
-->
        <div id="titulo-pagina">
            Reportes</div>
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
        <div id="content-side2-three-column"></div>
      
        <div class="clear"></div>
    </div>
</asp:Content>