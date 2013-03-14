<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Mars/Capacitacion/Capacitacion_Mars.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteResultadosMars.aspx.vb" 
    Inherits="procomlcd.ReporteResultadosMars"
    title="Mars Capacitación - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMarsCapacitacion" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
                        Reporte resultados</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width: 100%">
                <tr>
                    <td style="width: 96px">
                        &nbsp;</td>
                    <td style="width: 448px">
                        &nbsp;</td>
                    <td style="text-align: right">
                        <img alt="" src="../../../../Img/arrow.gif" /><asp:HyperLink ID="lnkRegresar" runat="server" 
                                NavigateUrl="~/sitios/Mars/Capacitacion/Cuestionarios/ReportesCapacitacionAS.aspx">Regresar</asp:HyperLink></td>
                </tr>
                <tr>
                    <td style="width: 96px">
                        Periodo</td>
                    <td colspan="2">
                        <asp:DropDownList ID="cmbPeriodo" runat="server" CssClass="ddl" 
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                        <tr>
                            <td style="width: 96px">
                                Cuestionario</td>
                            <td colspan="2">
                                <asp:DropDownList ID="cmbCuestionario" runat="server" AutoPostBack="True" 
                                    CssClass="ddl">
                                </asp:DropDownList>
                            </td>
                        </tr>
                <tr>
                    <td style="width: 96px">
                        Región</td>
                    <td colspan="2">
                        <asp:DropDownList ID="cmbRegion" runat="server" CssClass="ddl" 
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                        <tr>
                            <td style="width: 96px">
                                Ejecutivo Mars</td>
                            <td colspan="2">
                                <asp:DropDownList ID="cmbEjecutivo" runat="server" AutoPostBack="True" 
                                    CssClass="ddl" />
                            </td>
                        </tr>
                <tr>
                    <td style="width: 96px">
                        Promotor</td>
                    <td colspan="2">
                        <asp:DropDownList ID="cmbPromotor" runat="server" CssClass="ddl"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
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
                    <asp:Panel ID="PanelFS" runat="server">
                    </asp:Panel>
                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridReporte" runat="server" Width="645px" 
                                    ShowFooter="True" CssClass="grid-view">
                            <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>Sin información</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmbPeriodo" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbRegion" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbEjecutivo" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbPromotor" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        
        <!--CONTENT SIDE COLUMN-->
        <div id="content-side-two-column">
            <asp:LinkButton ID="lnkExportar" runat="server" Font-Bold="True">Exportar a Excel</asp:LinkButton>
        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>