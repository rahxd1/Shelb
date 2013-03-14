<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/SYM/AC/SYMAC.Master"
    AutoEventWireup="false" 
    CodeBehind="CapturasSYMAC.aspx.vb" 
    Inherits="procomlcd.CapturasSYMAC"
    Title="SYM Anaquel y Catalogación - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSYMAC" runat="Server">

<!--
titulo-pagina
-->
        <div id="titulo-pagina">
            Captura</div>
    <!--
CONTENT CONTAINER
-->
    <div id="contenido-columna-derecha">
        <!--
CONTENT MAIN COLUMN
-->
        <div id="content-main-two-column">
            <asp:GridView ID="gridCapturas" runat="server" AutoGenerateColumns ="False" 
                Width="100%" CssClass="grid-view" ShowFooter="True">
                <Columns>
                     <asp:TemplateField>
                        <ItemTemplate>
                        <a href="<%#Eval("ruta") %>"><%#Eval("Captura")%></a>
                        </ItemTemplate>
                        <ItemStyle />
                     </asp:TemplateField>
                </Columns>
                <FooterStyle CssClass="grid-footer" />
              
            </asp:GridView>
            <br />
            <br />
            <br />
        </div>
        <!-- END MAIN COLUMN -->
        <!--

  CONTENT SIDE 2 COLUMN

  -->
      
        <div class="clear">
        </div>
    </div>
    </asp:Content>