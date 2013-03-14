<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Mars/Autoservicio/Mars_Autoservicio.Master"
    AutoEventWireup="false" 
    CodeBehind="CapturasASMars.aspx.vb" 
    Inherits="procomlcd.CapturasASMars"
    title="Mars Autoservicio - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMarsAutoservicio" runat="Server">

<!--
titulo-pagina
-->
        <div id="titulo-pagina">
            Captura información anaquel y exhibiciones</div>
    <!--
CONTENT CONTAINER
-->
    <div id="contenido-columna-derecha">
        <!--
CONTENT MAIN COLUMN
-->
        <div id="content-main-two-column">
            <asp:GridView ID="gridCapturas" runat="server" AutoGenerateColumns ="False" 
                Width="100%" CssClass="grid-view-lst" ShowHeader="false" ShowFooter="False" GridLines="None" >
                <Columns>
                     <asp:TemplateField HeaderText="CAPTURA" >
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