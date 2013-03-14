<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Energizer/EnergizerDP/Energizer_DP.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteEnergizerDPPrecios.aspx.vb" 
    Inherits="procomlcd.ReporteEnergizerDPPrecios"
    title="Energizer Pilas Demo 2010 - Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1EnergizerDP" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
            Reporte precios</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width: 100%">
                        <tr><td style="width: 96px; height: 19px;"/>
                            <td style="width: 336px; height: 19px;"/>
                            <td style="height: 19px; text-align: right"><img alt="Regresar" src="../../../../Img/arrow.gif" /><asp:HyperLink ID="lnkRegresar" runat="server" NavigateUrl="~/sitios/Energizer/EnergizerDP/Reportes/ReportesEnergizerDP.aspx">Regresar</asp:HyperLink></td>
                        </tr>
                        <tr><td style="width: 96px">Periodo</td>
                            <td colspan="2"><asp:DropDownList ID="cmbPeriodo" runat="server" CssClass="ddl" AutoPostBack="True"/></td>
                        </tr>
                        <tr><td style="width: 96px">Región</td>
                            <td colspan="2"><asp:DropDownList ID="cmbRegion" runat="server" CssClass="ddl" AutoPostBack="True"/></td>
                        </tr>
                        <tr><td style="width: 96px">Promotor</td>
                            <td colspan="2"><asp:DropDownList ID="cmbPromotor" runat="server" CssClass="ddl" AutoPostBack="True"/></td>
                        </tr>
                        <tr><td style="width: 96px">Cadena</td>
                            <td colspan="2"><asp:DropDownList ID="cmbCadena" runat="server" CssClass="ddl" AutoPostBack="True"/></td>
                        </tr>
                        <tr><td style="width: 96px">Tienda</td>
                            <td colspan="2"><asp:DropDownList ID="cmbTienda" runat="server" CssClass="ddl" AutoPostBack="True"/></td>
                        </tr>
                        <tr><td style="width: 96px">Tipo Pilas</td>
                            <td colspan="2"><asp:DropDownList ID="cmbTipoPilas" runat="server" CssClass="ddl" AutoPostBack="True"/></td>
                        </tr>
                        <tr><td colspan="3" style="text-align: center;">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                <ProgressTemplate>
                                    <table id="pnlSave" >
                                        <tr><td>
                                            <br /><p><img alt="Cargando Reporte" src="../../../../Img/loading.gif" /> El Reporte se 
                                            esta generando.</p><br /></td></tr>
                                    </table>
                                </ProgressTemplate>
                            </asp:UpdateProgress></td>
                        </tr>
                    </table>
                    <asp:Panel ID="PanelFS" runat="server"/>
                    <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="id_producto" Width="100%" ShowFooter="True" CssClass="grid-view">
                            <Columns>
                                <asp:BoundField HeaderText="Periodo" DataField="nombre_periodo"/>
                                <asp:TemplateField HeaderText="Producto">
                                    <ItemTemplate>
                                        <a href="javascript:;" onclick="window.open('DetalleEnergizerProducto.aspx?id=<%#Eval("id_producto")%>&periodo=<%#Eval("id_periodo")%>','window','width=800,height=250,left=270,top=180, scrollbars=YES')">
                                        <%#Eval("nombre_producto")%></a>
                                     </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Precio Min">
                                    <ItemTemplate>
                                        <a href="javascript:;" onclick="window.open('DetalleEnergizerPrecio.aspx?id=<%#Eval("id_producto")%>&periodo=<%#Eval("id_periodo")%>&precio=<%#Eval("PrecioMin")%>&P=precio_pieza','window','width=800,height=250,left=270,top=180, scrollbars=YES')">
                                        <%#Eval("PrecioMin")%></a>
                                     </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Precio Max">
                                    <ItemTemplate>
                                        <a href="javascript:;" onclick="window.open('DetalleEnergizerPrecio.aspx?id=<%#Eval("id_producto")%>&periodo=<%#Eval("id_periodo")%>&precio=<%#Eval("PrecioMax")%>&P=precio_pieza','window','width=800,height=250,left=270,top=180,scrollbars=YES')">
                                        <%#Eval("PrecioMax")%></a>
                                     </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:BoundField HeaderText="Precio Prom" DataField="PrecioProm"/> 
                            </Columns>
                        <FooterStyle CssClass="grid-footer" />
                        <EmptyDataTemplate>
                            <h1>Sin información</h1>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cmbPeriodo" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cmbRegion" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cmbPromotor" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cmbCadena" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cmbTienda" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        
        <!--CONTENT SIDE COLUMN-->
        <div id="content-side-two-column">
            
            <asp:LinkButton ID="lnkExportar" runat="server" Font-Bold="True">Exportar Tabla a Excel</asp:LinkButton>
        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>