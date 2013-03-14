<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Herradura/NewMix/NewMix.Master"
    AutoEventWireup="false" 
    CodeBehind="ReportesNewMix.aspx.vb" 
    Inherits="procomlcd.ReportesNewMix"
    title="NewMix - Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderNewMix" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">Reportes</div>
            
    <!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">
    
        <!--CONTENT MAIN COLUMN-->
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
        <!--CONTENT SIDE 2 COLUMN -->
        
        <div id="content-side2-three-column">
        </div>
      
        <div class="clear"></div>
    </div>
</asp:Content>