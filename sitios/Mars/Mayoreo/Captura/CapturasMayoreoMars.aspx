<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Mars/Mayoreo/MayoreoMars.Master"
    AutoEventWireup="false" 
    CodeBehind="CapturasMayoreoMars.aspx.vb" 
    Inherits="procomlcd.CapturasMayoreoMars"
    title="Mars Verificadores de Precio Mayoreo - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoMayoreoMars" runat="Server">

<!--
titulo-pagina
-->
        <div id="titulo-pagina">
            Levantamiento información</div>
    <!--
CONTENT CONTAINER
-->
    <div id="contenido-columna-derecha">
        <!--
CONTENT MAIN COLUMN
-->
        <div id="contenido-menu-columnas">
            <asp:GridView ID="gridCapturas" runat="server" AutoGenerateColumns ="False" 
                Width="646px" CssClass="grid-view" ShowFooter="True">
                <Columns>
                     <asp:TemplateField >
                        <ItemTemplate>
                        <a href="<%#Eval("ruta") %>"><%#Eval("Captura")%></a>
                        </ItemTemplate>
                        <ItemStyle />
                     </asp:TemplateField>
                </Columns>
                <FooterStyle CssClass="grid-footer" />
              
            </asp:GridView>
        </div>
        <!-- END MAIN COLUMN -->
        <!--

  CONTENT SIDE 2 COLUMN

  -->
    <div id="content-side2-three-column">
            
            <p>
               
            </p>
        </div>
      
        <div class="clear">
        </div>
    </div>
    </asp:Content>