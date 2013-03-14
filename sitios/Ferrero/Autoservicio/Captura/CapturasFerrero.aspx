<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Ferrero/Autoservicio/FerreroAS.Master"
    AutoEventWireup="false" 
    CodeBehind="CapturasFerrero.aspx.vb" 
    Inherits="procomlcd.CapturasFerrero"
    title="Ferrero - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoFerreroAS" runat="Server">

<!--
titulo-pagina
-->
        <div id="titulo-pagina">
            Captura información</div>
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
                     <asp:TemplateField >
                        <ItemTemplate>
                        <a href="<%#Eval("ruta") %>"><%#Eval("Captura")%></a>
                        </ItemTemplate>
                        <ItemStyle />
                     </asp:TemplateField>
                </Columns>
                <FooterStyle />
              
            </asp:GridView>
        </div>
        <!-- END MAIN COLUMN -->

        <div class="clear">
        </div>
    </div>
    </asp:Content>