<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Energizer/EnergizerConv/Energizer_Conv.Master"
    AutoEventWireup="false" 
    CodeBehind="GaleriaEnergizerConv.aspx.vb" 
    Inherits="procomlcd.GaleriaEnergizerConv"
    Title= "Energizer Conveniencia - Galería" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1EnergizerConv" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
            Galería de imagenes</div>

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
                            <td style="width: 58px">&nbsp;</td>
                            <td style="width: 314px"><asp:Label ID="lblAviso" runat="server"/></td>
                            <td style="text-align: right"><img alt="Regresar" src="../../../../Img/arrow.gif" />
                                <asp:HyperLink ID="lnkRegresar" runat="server" NavigateUrl="~/sitios/Energizer/EnergizerConv/Reportes/ReportesEnergizerConv.aspx">Regresar</asp:HyperLink></td>
                        </tr>
                        <tr>
                            <td style="width: 58px">Periodo</td>
                            <td colspan="2"><asp:DropDownList ID="cmbPeriodo" runat="server" AutoPostBack="True" CssClass="ddl"/></td>
                        </tr>
                        <tr>
                            <td style="width: 58px">Región</td>
                            <td colspan="2"><asp:DropDownList ID="cmbRegion" runat="server" CssClass="ddl" AutoPostBack="True"/></td>
                        </tr>
                        <tr>
                            <td style="width: 58px">Cadena</td>
                            <td colspan="2"><asp:DropDownList ID="cmbCadena" runat="server" CssClass="ddl" AutoPostBack="True"/></td>
                        </tr>
                        <tr>
                            <td style="width: 58px">Tienda</td>
                            <td colspan="2"><asp:DropDownList ID="cmbTienda" runat="server" CssClass="ddl" AutoPostBack="True"/></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center;">
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
                    <asp:GridView ID="gridImagenes" runat="server" Width="100%" 
                        AutoGenerateColumns="False" ShowHeader="False" CssClass="grid-view">
                            <RowStyle BorderStyle="Solid" 
                                BorderWidth="1px" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:TemplateField HeaderText="Foto" >
                                    <ItemTemplate>
                                        <img alt="<%#Eval("foto")%>" src="<%#Eval("ruta")%><%#Eval("foto")%>" height="300" width="250" alt="Foto"/>
                                    </ItemTemplate>
                                    <ItemStyle Width="0%" VerticalAlign="top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Foto" >
                                    <ItemTemplate>
                                        <br />Periodo: <%#Eval("nombre_periodo")%>
                                        <br />Región: <%#Eval("nombre_region")%>
                                        <br />Cadena: <%#Eval("nombre_cadena")%>
                                        <br />Tienda: <%#Eval("nombre")%>
                                        <br />Descripción: <%#Eval("descripcion")%>
                                    </ItemTemplate>
                                    <ItemStyle Width="90%" VerticalAlign="top" />
                                </asp:TemplateField>
                            </Columns>
                
                            <FooterStyle 
                                HorizontalAlign="Center" VerticalAlign="Top" />
                        <EmptyDataTemplate>
                            <h1>Sin información</h1>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <!-- END MAIN COLUMN -->
        <!--CONTENT SIDE 2 COLUMN-->
        <div id="content-side2-three-column">
        </div>
      
        <div class="clear">
        </div>
    </div>
    </asp:Content>