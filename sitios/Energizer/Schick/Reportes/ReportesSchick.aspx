<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Energizer/Schick/Schick.Master"
    AutoEventWireup="false" 
    CodeBehind="ReportesSchick.aspx.vb" 
    Inherits="procomlcd.ReportesSchick"
    title="Schick - Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1Schick" runat="Server">

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
            <asp:GridView ID="grid" runat="server" BackColor="White" AutoGenerateColumns ="False" 
            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" Width="100%" GridLines="Vertical" ShowFooter="True">
            <RowStyle ForeColor="Black" BackColor="#EEEEEE" />
                <Columns>
                     <asp:TemplateField HeaderText="REPORTES" >
                        <ItemTemplate>
                        <a href="<%#Eval("ruta") %>"><%#Eval("Reporte")%></a>
                        </ItemTemplate>
                        <ItemStyle />
                     </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
              
                <AlternatingRowStyle BackColor="#DCDCDC" />
              
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
