<%@ Page Language="vb"
 MasterPageFile="~/sitios/Demos/Bancomer/DemoBancomer.Master"
 AutoEventWireup="false" 
 CodeBehind="DemoBancomerConsultas.aspx.vb" 
 Inherits="procomlcd.DemoBancomerConsultas" %>
 <asp:Content ID="Content1" ContentPlaceHolderID="DemoBancomerContent" runat="Server">

<!--
titulo-pagina
-->
        <div id="titulo-pagina">
            Consultas</div>
<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:GridView ID="gridReportes" runat="server" AutoGenerateColumns ="False" 
                Width="100%" GridLines="None" ShowHeader="False" 
                CssClass="grid-view-lst">
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
      
        <div class="clear">
        </div>
    </div>
    </asp:Content>

