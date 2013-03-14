<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/SYM/AC/SYMAC.Master" 
    AutoEventWireup="false" 
    CodeBehind="AvisosSYMAC.aspx.vb" 
    Inherits="procomlcd.AvisosSYMAC"
    TITLE="SYM Anaquel y Catalogación" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSYMAC" runat="Server">
    <!--

CONTENT CONTAINER

-->
    <div id="titulo-pagina">
        Avisos</div>

    <div id="contenido-columna-derecha">
        <!--
CONTENT MAIN COLUMN
-->
        <div id="content-main-two-column">
            <table style="width: 100%">
                            <tr><td style="width: 96px">&nbsp;</td>
                                <td style="width: 434px">&nbsp;</td>
                                <td style="text-align: right">
                                    <img alt="" src="../../../../Img/arrow.gif" /><asp:HyperLink ID="lnkRegresar" runat="server" 
                                            NavigateUrl="~/sitios/SYM/AC/menu_SYMAC.aspx">Regresar</asp:HyperLink></td>
                            </tr>
                            </table>
        
            <h2>
                Avisos</h2>
                    <asp:GridView ID="gridAvisos" runat="server" Width="100%" 
                AutoGenerateColumns="False" CssClass="grid-view" BorderWidth="0px" 
                ShowHeader="False">
                        <RowStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" 
                            VerticalAlign="Top" />
                             <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <img src="<%# Eval("foto", "/ARCHIVOS/CLIENTES/SYM/AVISOS/{0}") %>" width="250" 
                                            alt="<%#Eval("nombre_aviso")%>"/>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="top" Width="0%" />
                                 </asp:TemplateField>
                                 <asp:TemplateField>
                                    <ItemTemplate>
                                        <br /><i><font color="red"><%#Eval("nombre_aviso")%></font></i>
                                        <br />Fecha inicio: <i><%#Eval("fecha_inicio")%></i>
                                        <br />Fecha fin: <i><%#Eval("fecha_fin")%></i>
                                        <br />
                                        <br />Descripción: <b>
                                        <font color="black"><%#Eval("descripcion")%></font></b>
                                        <br />
                                        <br />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="top" Width="90%" />
                                </asp:TemplateField>
                             </Columns>
                             <FooterStyle CssClass="grid-footer" />
                        <FooterStyle 
                            HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:GridView>
            
            
        </div>
        
        <div class="clear">
        
        </div>
    </div>
    
</asp:Content>
