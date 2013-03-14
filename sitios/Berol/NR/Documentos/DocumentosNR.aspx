<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Berol/NR/Rubbermaid.Master"
    AutoEventWireup="false" 
    CodeBehind="DocumentosNR.aspx.vb" 
    Inherits="procomlcd.DocumentosNR"
    title="Newell Rubbermaid - Documentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoNR" runat="Server">

<!--
titulo-pagina
-->
        <div id="titulo-pagina">
            Documentos</div>
    <!--
CONTENT CONTAINER
-->
    <div id="contenido-columna-derecha">
        <!--
CONTENT MAIN COLUMN
-->
        <div id="content-main-two-column">
            <asp:GridView ID="gridDocumentos" runat="server" AutoGenerateColumns ="False" 
                Width="100%" GridLines="Horizontal" ShowHeader="False" BorderStyle="None">
                <Columns>
                     <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HyperLink ID="descarga" runat="server" target="_blank"
                                NavigateUrl='<%#Eval("ruta", "~/ARCHIVOS/CLIENTES/BEROL/NR/DOCUMENTOS/{0}")%>'><%#Eval("documento")%></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle />
                     </asp:TemplateField>
                </Columns>
                <FooterStyle CssClass="grid-footer" />
                <EmptyDataTemplate>
                    <h1>Sin información</h1>
                </EmptyDataTemplate>
              
            </asp:GridView>
            <br />
            <br />
            <br />
            
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