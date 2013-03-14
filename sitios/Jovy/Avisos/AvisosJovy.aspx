<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Jovy/Jovy.Master" 
    AutoEventWireup="false" 
    CodeBehind="AvisosJovy.aspx.vb" 
    Inherits="procomlcd.AvisosJovy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderJovy" runat="Server">
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
                                        <img src="/ARCHIVOS/CLIENTES/JOVY/AVISOS/<%#Eval("id_aviso")%>.jpg" width="250" 
                                            alt="<%#Eval("id_aviso")%>"/>
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
