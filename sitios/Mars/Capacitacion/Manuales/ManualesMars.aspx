<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Mars/Capacitacion/Capacitacion_Mars.Master"
    AutoEventWireup="false" 
    CodeBehind="ManualesMars.aspx.vb" 
    Inherits="procomlcd.ManualesMars"
    title="Mars autoservicio - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMarsCapacitacion" runat="Server">

<!--
titulo-pagina
-->
        <div id="titulo-pagina">
            Manuales</div>
<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:GridView ID="gridManuales" runat="server" AutoGenerateColumns ="False" 
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
                    <h1>Sin manuales</h1>
                </EmptyDataTemplate>
              
            </asp:GridView>

        </div>
        <!-- END MAIN COLUMN -->
        <!--

  CONTENT SIDE 2 COLUMN

  -->
      
        <div class="clear">
        </div>
    </div>
    </asp:Content>