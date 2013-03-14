<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Ferrero/Ferrero-Danone/FerreroDanone.Master"
    AutoEventWireup="false" 
    CodeBehind="CapturasFerreroDanone.aspx.vb" 
    Inherits="procomlcd.CapturasFerreroDanone"
    Title="Ferrero - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentFerreroDanone" runat="Server">

<!--
titulo-pagina
-->
        <div id="titulo-pagina">
            Captura Información</div>
    <!--
CONTENT CONTAINER
-->
    <div id="contenido-columna-derecha">
        <!--
CONTENT MAIN COLUMN
-->
        <div id="content-main-two-column">
            <asp:GridView ID="gridCapturas" runat="server" AutoGenerateColumns ="False" 
                Width="100%" GridLines="Horizontal" ShowHeader="False" BorderStyle="None">
                <Columns>
                     <asp:TemplateField>
                        <ItemTemplate>
                        <a href="<%#Eval("ruta") %>"><%#Eval("Captura")%></a>
                        </ItemTemplate>
                        <ItemStyle />
                     </asp:TemplateField>
                </Columns>              
            </asp:GridView>
        </div>
        <!-- END MAIN COLUMN -->
      
        <div class="clear">
        </div>
    </div>
    </asp:Content>