<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Energizer/Schick/Schick.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteGaleriaSchick.aspx.vb" 
    Inherits="procomlcd.ReporteGaleriaSchick"
    title="Schick - Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1Schick" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
            Galería de Imagenes</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width: 100%">
                        <tr>
                            <td style="height: 19px; text-align: right;" colspan="2">
                                <img alt="" src="../../../../Img/arrow.gif" />
                                <asp:HyperLink ID="lnkRegresar" runat="server" 
                                    NavigateUrl="~/sitios/Energizer/Schick/Reportes/ReportesSchick.aspx">Regresar</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 58px">Periodo</td>
                            <td><asp:DropDownList ID="cmbPeriodo" runat="server" Height="22px" Width="250px" AutoPostBack="True" /></td>
                        </tr>
                        <tr>
                            <td style="width: 58px">Región</td>
                            <td><asp:DropDownList ID="cmbRegion" runat="server" Height="22px" Width="250px" AutoPostBack="True" /></td>
                        </tr>
                        <tr>
                            <td style="width: 58px">Departamental</td>
                            <td><asp:DropDownList ID="cmbCadena" runat="server" Height="22px" Width="250px" AutoPostBack="True" /> </td>
                        </tr>
                        <tr>
                            <td style="width: 58px">Tienda</td>
                            <td><asp:DropDownList ID="cmbTienda" runat="server" Height="22px" Width="250px" AutoPostBack="True" /></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center;">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                    <ProgressTemplate>
                                        <table id="pnlSave" >
                                            <tr><td>
                                                <br /><p><img alt="Cargando Reporte" src="../../../../Img/loading.gif" /> El Reporte se 
                                                esta generando.</p><br /></td></tr>
                                        </table>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:GridView ID="gridImagenes" runat="server" Width="100%" AutoGenerateColumns="False" 
                        ShowFooter="True" CssClass="grid-view">
                        <RowStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" VerticalAlign="Top" />
                             <Columns>
                                 <asp:TemplateField>
                                    <ItemTemplate>
                                        <img src="<%#Eval("ruta")%><%#Eval("foto")%>" width="250" alt="<%#Eval("ruta")%><%#Eval("foto")%>"/>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="top" Width="0%" />
                                 </asp:TemplateField>
                                 <asp:TemplateField>
                                    <ItemTemplate>
                                        <br />Periodo: <i><%#Eval("nombre_periodo")%></i>
                                        <br />Región: <i><%#Eval("nombre_region")%></i>
                                        <br />Cadena: <i><%#Eval("nombre_cadena")%></i>
                                        <br />Tienda: <i><%#Eval("nombre")%></i>
                                        <br />
                                        <br />Descripción: <b><font color="black"><%#Eval("descripcion")%></font></b>
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
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>

        <!-- END MAIN COLUMN -->
        <!--CONTENT SIDE 2 COLUMN-->
        
        <div class="clear"></div>
    </div>
    </asp:Content>